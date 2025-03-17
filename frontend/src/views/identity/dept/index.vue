<template>
  <div class="dept-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.dept.fields.deptName.label')">
          <a-input
            v-model:value="queryParams.deptName"
            :placeholder="t('identity.dept.fields.deptName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.dept.fields.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            :placeholder="t('identity.dept.fields.status.placeholder')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :add-permission="['identity:dept:create']"
      :edit-permission="['identity:dept:update']"
      :delete-permission="['identity:dept:delete']"
      :export-permission="['identity:dept:export']"
      :disabled-edit="selectedKeys.length !== 1"
      :disabled-delete="selectedKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
    />

    <!-- 数据表格 -->
    <a-table
      :columns="columns"
      :data-source="list"
      :loading="loading"
      :pagination="false"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      row-key="deptId"
      size="middle"
      bordered
      :default-expand-all="true"
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
    >
      <template #bodyCell="{ column, record }">
        <!-- 部门名称 -->
        <template v-if="column.dataIndex === 'deptName'">
          <span>
            <folder-outlined />
            {{ record.deptName }}
          </span>
        </template>
        <!-- 状态 -->
        <template v-else-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 操作 -->
        <template v-else-if="column.dataIndex === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['identity:dept:update']"
            :delete-permission="['identity:dept:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </a-table>

    <!-- 表单弹窗 -->
    <dept-form
      v-model:visible="formVisible"
      :title="formTitle"
      :dept-id="formDeptId"
      @success="getList"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { FolderOutlined } from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Dept, DeptQuery } from '@/types/identity/dept'
import { getDeptTree, deleteDept, batchDeleteDept, exportDept } from '@/api/identity/dept'
import DeptForm from './components/DeptForm.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'deptName',
    label: t('identity.dept.fields.deptName.label'),
    type: 'input',
    props: {
      placeholder: t('identity.dept.fields.deptName.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.dept.fields.status.label'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      placeholder: t('identity.dept.fields.status.placeholder'),
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<DeptQuery>({
  pageIndex: 1,
  pageSize: 10,
  deptName: undefined,
  status: undefined
})

// 加载状态
const loading = ref(false)

// 部门列表数据
const list = ref<Dept[]>([])

// 选中的部门ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的部门ID
const formDeptId = ref<number>()

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.dept.fields.deptName.label'),
    dataIndex: 'deptName',
    width: 220
  },
  {
    title: t('identity.dept.fields.orderNum.label'),
    dataIndex: 'orderNum',
    width: 80
  },
  {
    title: t('identity.dept.fields.leader.label'),
    dataIndex: 'leader',
    width: 100
  },
  {
    title: t('identity.dept.fields.phone.label'),
    dataIndex: 'phone',
    width: 120
  },
  {
    title: t('identity.dept.fields.email.label'),
    dataIndex: 'email',
    width: 150
  },
  {
    title: t('identity.dept.fields.status.label'),
    dataIndex: 'status',
    width: 100
  },
  {
    title: t('common.createTime'),
    dataIndex: 'createTime',
    width: 180
  },
  {
    title: t('common.action'),
    dataIndex: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 获取部门列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getDeptTree()
    if (res.code === 200) {
      list.value = res.data
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[部门管理] 获取部门列表出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理查询
const handleQuery = () => {
  selectedKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    deptName: undefined,
    status: undefined
  }
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: Dept[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.dept.dialog.create')
  formDeptId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const deptId = selectedKeys.value[0]
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = Number(deptId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = Number(record.deptId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteDept(Number(record.deptId))
    if (res.code === 200) {
      message.success(t('common.delete.success'))
      getList()
    } else {
      message.error(res.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理批量删除
const handleBatchDelete = () => {
  Modal.confirm({
    title: t('common.delete.confirm'),
    content: t('common.delete.message', { count: selectedKeys.value.length }),
    async onOk() {
      try {
        const res = await batchDeleteDept(selectedKeys.value.map(id => Number(id)))
        if (res.code === 200) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          getList()
        } else {
          message.error(res.msg || t('common.delete.failed'))
        }
      } catch (error) {
        console.error(error)
        message.error(t('common.delete.failed'))
      }
    }
  })
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportDept()
    if (res.code === 200) {
      message.success(t('common.export.success'))
    } else {
      message.error(res.msg || t('common.export.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

onMounted(() => {
  getList()
})
</script>

<style lang="less" scoped>
.dept-container {
  padding: 16px;
  background-color: #fff;
  height: 100%;
  display: flex;
  flex-direction: column;

  .ant-table-wrapper {
    flex: 1;
  }
}
</style>
