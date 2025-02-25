<template>
  <div class="role-container">
    <a-card :bordered="false">
      <template #title>{{ t('system.role') }}</template>
      <template #extra>
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon><plus-outlined /></template>
            {{ t('common.add') }}
          </a-button>
          <a-button @click="handleRefresh">
            <template #icon><reload-outlined /></template>
            {{ t('common.refresh') }}
          </a-button>
        </a-space>
      </template>

      <!-- 搜索表单 -->
      <a-form
        layout="inline"
        :model="searchForm"
        @finish="handleSearch"
        class="search-form"
      >
        <a-form-item label="角色名称" name="roleName">
          <a-input
            v-model:value="searchForm.roleName"
            placeholder="请输入角色名称"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-select
            v-model:value="searchForm.status"
            placeholder="请选择状态"
            style="width: 120px"
            allow-clear
          >
            <a-select-option value="normal">正常</a-select-option>
            <a-select-option value="disabled">禁用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">
              <template #icon><search-outlined /></template>
              查询
            </a-button>
            <a-button @click="handleReset">
              <template #icon><redo-outlined /></template>
              重置
            </a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="table-toolbar">
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon><plus-outlined /></template>
            新增
          </a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="tableData"
        :loading="loading"
        :pagination="pagination"
        @change="handleTableChange"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <!-- 状态列 -->
          <template v-if="column.key === 'status'">
            <a-tag :color="(record as unknown as RoleInfo).status === 'normal' ? 'success' : 'error'">
              {{ (record as unknown as RoleInfo).status === 'normal' ? '正常' : '禁用' }}
            </a-tag>
          </template>
          
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleEdit(record as unknown as RoleInfo)">编辑</a-button>
              <a-button type="link" @click="handlePermission(record as unknown as RoleInfo)">权限</a-button>
              <a-button type="link" danger>
                <a-popconfirm
                  title="确定要删除此角色吗？"
                  @confirm="handleDelete(record as unknown as RoleInfo)"
                >
                  <template #default>
                    <span>删除</span>
                  </template>
                </a-popconfirm>
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 编辑对话框 -->
    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
    >
      <a-form
        ref="formRef"
        :model="formData"
        :rules="formRules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item label="角色名称" name="roleName">
          <a-input
            v-model:value="formData.roleName"
            placeholder="请输入角色名称"
          />
        </a-form-item>
        <a-form-item label="角色编码" name="roleCode">
          <a-input
            v-model:value="formData.roleCode"
            placeholder="请输入角色编码"
            :disabled="!!formData.id"
          />
        </a-form-item>
        <a-form-item label="显示顺序" name="orderNum">
          <a-input-number
            v-model:value="formData.orderNum"
            placeholder="请输入显示顺序"
            style="width: 100%"
            :min="0"
            :max="999"
          />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-select
            v-model:value="formData.status"
            placeholder="请选择状态"
          >
            <a-select-option value="normal">正常</a-select-option>
            <a-select-option value="disabled">禁用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="备注" name="remark">
          <a-textarea
            v-model:value="formData.remark"
            placeholder="请输入备注"
            :rows="4"
          />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 权限对话框 -->
    <a-modal
      v-model:visible="permissionVisible"
      title="分配权限"
      @ok="handlePermissionOk"
      @cancel="handlePermissionCancel"
    >
      <a-tree
        v-model:checkedKeys="checkedKeys"
        :tree-data="permissionTree"
        checkable
        :defaultExpandAll="true"
      />
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import type { FormInstance, TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { DataNode } from 'ant-design-vue/es/tree'
import {
  SearchOutlined,
  RedoOutlined,
  PlusOutlined
} from '@ant-design/icons-vue'

interface RoleInfo {
  id: number
  roleName: string
  roleCode: string
  orderNum: number
  status: string
  remark: string
  createdTime: string
}

const { t } = useI18n()

// 查询参数
const searchForm = reactive({
  roleName: '',
  status: undefined as string | undefined
})

// 表格列定义
const columns: TableColumnsType<RoleInfo> = [
  {
    title: '角色名称',
    dataIndex: 'roleName',
    key: 'roleName',
    width: 150
  },
  {
    title: '角色编码',
    dataIndex: 'roleCode',
    key: 'roleCode',
    width: 150
  },
  {
    title: '显示顺序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100,
    slots: {
      customRender: 'status'
    }
  },
  {
    title: '创建时间',
    dataIndex: 'createdTime',
    key: 'createdTime',
    width: 180
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const,
    slots: {
      customRender: 'action'
    }
  }
]

// 表格数据
const loading = ref(false)
const tableData = ref<RoleInfo[]>([])
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true
})

// 编辑表单数据
const modalVisible = ref(false)
const modalTitle = ref('')
const formRef = ref<FormInstance>()
const formData = reactive<RoleInfo>({
  id: 0,
  roleName: '',
  roleCode: '',
  orderNum: 0,
  status: 'normal',
  remark: '',
  createdTime: ''
})

// 表单验证规则
const formRules = {
  roleName: [
    { required: true, message: '请输入角色名称' },
    { min: 2, max: 20, message: '角色名称长度必须在2-20个字符之间' }
  ],
  roleCode: [
    { required: true, message: '请输入角色编码' },
    { min: 2, max: 20, message: '角色编码长度必须在2-20个字符之间' },
    { pattern: /^[A-Z_]+$/, message: '角色编码只能包含大写字母和下划线' }
  ],
  orderNum: [
    { required: true, message: '请输入显示顺序' }
  ],
  status: [
    { required: true, message: '请选择状态' }
  ]
}

// 权限树数据
const permissionVisible = ref(false)
const checkedKeys = ref<(string | number)[]>([])
const permissionTree = ref<DataNode[]>([])
const currentRoleId = ref<number>()

// 查询方法
const handleSearch = () => {
  pagination.current = 1
  fetchData()
}

// 重置方法
const handleReset = () => {
  searchForm.roleName = ''
  searchForm.status = undefined
  handleSearch()
}

// 处理表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  fetchData()
}

// 新增方法
const handleAdd = () => {
  modalTitle.value = '新增角色'
  formData.id = 0
  formData.roleName = ''
  formData.roleCode = ''
  formData.orderNum = 0
  formData.status = 'normal'
  formData.remark = ''
  formData.createdTime = ''
  modalVisible.value = true
}

// 编辑方法
const handleEdit = (record: RoleInfo) => {
  modalTitle.value = '编辑角色'
  const { id, roleName, roleCode, orderNum, status, remark, createdTime } = record
  Object.assign(formData, { id, roleName, roleCode, orderNum, status, remark, createdTime })
  modalVisible.value = true
}

// 删除方法
const handleDelete = async (record: RoleInfo) => {
  try {
    // 调用删除API
    console.log('删除角色:', record.id)
    // 重新加载数据
    fetchData()
  } catch (error) {
    console.error('删除角色失败:', error)
  }
}

// 处理权限分配
const handlePermission = (record: RoleInfo) => {
  currentRoleId.value = record.id
  // 加载权限树数据
  loadPermissionTree()
  // 加载角色已有权限
  loadRolePermissions(record.id)
  permissionVisible.value = true
}

// 处理模态框确认
const handleModalOk = () => {
  formRef.value?.validate().then(() => {
    // 调用保存API
    console.log('保存角色:', formData)
    modalVisible.value = false
    // 重新加载数据
    fetchData()
  })
}

// 处理模态框取消
const handleModalCancel = () => {
  modalVisible.value = false
}

// 处理权限确认
const handlePermissionOk = async () => {
  try {
    // 调用保存权限API
    console.log('保存权限:', {
      roleId: currentRoleId.value,
      permissionIds: checkedKeys.value
    })
    permissionVisible.value = false
  } catch (error) {
    console.error('保存权限失败:', error)
  }
}

// 处理权限取消
const handlePermissionCancel = () => {
  permissionVisible.value = false
}

// 加载权限树数据
const loadPermissionTree = () => {
  // 模拟API调用
  permissionTree.value = [
    {
      title: '系统管理',
      key: '1',
      children: [
        {
          title: '用户管理',
          key: '1-1',
          children: [
            { title: '查询用户', key: '1-1-1' },
            { title: '新增用户', key: '1-1-2' },
            { title: '修改用户', key: '1-1-3' },
            { title: '删除用户', key: '1-1-4' }
          ]
        },
        {
          title: '角色管理',
          key: '1-2',
          children: [
            { title: '查询角色', key: '1-2-1' },
            { title: '新增角色', key: '1-2-2' },
            { title: '修改角色', key: '1-2-3' },
            { title: '删除角色', key: '1-2-4' }
          ]
        }
      ]
    }
  ]
}

// 加载角色权限
const loadRolePermissions = (roleId: number) => {
  // 模拟API调用
  checkedKeys.value = ['1-1-1', '1-1-2']
}

// 获取表格数据
const fetchData = () => {
  loading.value = true
  // 模拟API调用
  setTimeout(() => {
    tableData.value = [
      {
        id: 1,
        roleName: '管理员',
        roleCode: 'ADMIN',
        orderNum: 1,
        status: 'normal',
        remark: '系统管理员，拥有所有权限',
        createdTime: '2024-01-22 10:00:00'
      }
    ]
    pagination.total = 1
    loading.value = false
  }, 500)
}

// 刷新方法
const handleRefresh = () => {
  handleSearch()
}

// 初始化加载
onMounted(() => {
  handleSearch()
})
</script>

<style lang="less" scoped>
.role-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
  
  .search-form {
    margin-bottom: 24px;
  }

  .table-toolbar {
    margin-bottom: 16px;
  }

  .text-danger {
    color: #ff4d4f;
  }
}
</style> 