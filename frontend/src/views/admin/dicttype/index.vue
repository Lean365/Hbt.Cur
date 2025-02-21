//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-02-21
// 版本号 : v1.0.0
// 描述    : 字典类型管理组件
//===================================================================

<template>
  <div class="dicttype-container">
    <a-card :bordered="false">
      <!-- 工具栏 -->
      <template #title>
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon><plus-outlined /></template>
            新增
          </a-button>
          <a-button @click="handleRefresh">
            <template #icon><reload-outlined /></template>
            刷新
          </a-button>
        </a-space>
      </template>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="dataSource"
        :loading="loading"
        :pagination="pagination"
        @change="handleTableChange"
      >
        <!-- 状态列 -->
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'status'">
            <a-tag :color="record.status ? 'success' : 'error'">
              {{ record.status ? '启用' : '禁用' }}
            </a-tag>
          </template>
          
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <a-space>
              <a @click="handleEdit(record)">编辑</a>
              <a-divider type="vertical" />
              <a-popconfirm
                title="确定要删除这条记录吗?"
                @confirm="handleDelete(record)"
              >
                <a class="text-danger">删除</a>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 编辑表单对话框 -->
    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
    >
      <a-form
        ref="formRef"
        :model="formState"
        :rules="rules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item label="字典名称" name="name">
          <a-input v-model:value="formState.name" placeholder="请输入字典名称" />
        </a-form-item>
        <a-form-item label="字典类型" name="type">
          <a-input v-model:value="formState.type" placeholder="请输入字典类型" />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-switch v-model:checked="formState.status" />
        </a-form-item>
        <a-form-item label="备注" name="remark">
          <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { TablePaginationConfig, TableColumnType } from 'ant-design-vue'
import { PlusOutlined, ReloadOutlined } from '@ant-design/icons-vue'

// 表格列定义
const columns: TableColumnType[] = [
  {
    title: '字典名称',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: '字典类型',
    dataIndex: 'type',
    key: 'type'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status'
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime'
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark'
  },
  {
    title: '操作',
    key: 'action',
    fixed: 'right' as const,
    width: 120
  }
]

// 分页配置
const pagination = reactive<TablePaginationConfig>({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true
})

// 表格数据
const loading = ref(false)
const dataSource = ref<any[]>([])

// 加载数据
const loadData = async () => {
  loading.value = true
  try {
    // TODO: 调用API加载数据
    dataSource.value = []
    pagination.total = 0
  } catch (error) {
    console.error('加载数据失败:', error)
    message.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

// 表格变化事件
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  loadData()
}

// 表单状态
interface FormState {
  id?: number
  name: string
  type: string
  status: boolean
  remark: string
}

const formRef = ref<FormInstance>()
const formState = reactive<FormState>({
  name: '',
  type: '',
  status: true,
  remark: ''
})

// 表单验证规则
const rules: Partial<Record<keyof FormState, Rule[]>> = {
  name: [{ required: true, message: '请输入字典名称', trigger: 'blur' }],
  type: [{ required: true, message: '请输入字典类型', trigger: 'blur' }]
}

// 对话框控制
const modalVisible = ref(false)
const modalTitle = ref('新增字典类型')

// 新增按钮点击
const handleAdd = () => {
  formState.id = undefined
  formState.name = ''
  formState.type = ''
  formState.status = true
  formState.remark = ''
  modalTitle.value = '新增字典类型'
  modalVisible.value = true
}

// 编辑按钮点击
const handleEdit = (record: any) => {
  formState.id = record.id
  formState.name = record.name
  formState.type = record.type
  formState.status = record.status
  formState.remark = record.remark
  modalTitle.value = '编辑字典类型'
  modalVisible.value = true
}

// 删除按钮点击
const handleDelete = async (record: any) => {
  try {
    // TODO: 调用API删除数据
    message.success('删除成功')
    loadData()
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 刷新按钮点击
const handleRefresh = () => {
  loadData()
}

// 对话框确认
const handleModalOk = async () => {
  try {
    await formRef.value?.validate()
    // TODO: 调用API保存数据
    message.success('保存成功')
    modalVisible.value = false
    loadData()
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 对话框取消
const handleModalCancel = () => {
  modalVisible.value = false
}

// 初始化加载数据
loadData()
</script>

<style lang="less" scoped>
.dicttype-container {
  padding: 24px;
  background: #f0f2f5;
  min-height: 100%;

  .text-danger {
    color: #ff4d4f;
  }
}
</style> 