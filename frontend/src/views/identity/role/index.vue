<template>
  <div class="role-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.role.fields.roleName.label')">
          <a-input
            v-model:value="queryParams.roleName"
            :placeholder="t('identity.role.fields.roleName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.role.fields.roleKey.label')">
          <a-input
            v-model:value="queryParams.roleKey"
            :placeholder="t('identity.role.fields.roleKey.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.role.fields.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            type="radio"
            :placeholder="t('identity.role.fields.status.placeholder')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedRowKeys.length"
      :show-selected-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :add-permission="['identity:role:create']"
      :edit-permission="['identity:role:update']"
      :delete-permission="['identity:role:delete']"
      :export-permission="['identity:role:export']"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
    />

    <!-- 数据表格 -->
    <hbt-table
      row-key="roleId"
      :loading="loading"
      :columns="columns"
      :data-source="tableData"
      :pagination="{
        total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize
      }"
      :row-selection="{
        selectedRowKeys,
        onChange: (keys: (string | number)[]) => selectedRowKeys = keys
      }"
      @change="handleTableChange"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:role:update']"
            :show-delete="true"
            :delete-permission="['identity:role:delete']"
            :show-authorize="true"
            :authorize-permission="['identity:role:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @authorize="handleAuthorize"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 角色表单对话框 -->
    <role-form
      v-model:visible="formVisible"
      :title="formTitle"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />

    <!-- 菜单分配对话框 -->
    <menu-allocate
      v-model:visible="menuAllocateVisible"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Role, RoleQuery } from '@/types/identity/role'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import { getPagedList, deleteRole } from '@/api/identity/role'
import RoleForm from './components/RoleForm.vue'
import MenuAllocate from './components/MenuAllocate.vue'

const { t } = useI18n()

// 表格列定义
const columns = [
  {
    title: t('identity.role.fields.roleName.label'),
    dataIndex: 'roleName',
    key: 'roleName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleKey.label'),
    dataIndex: 'roleKey',
    key: 'roleKey',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleSort.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('identity.role.fields.dataScope.label'),
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 120
  },
  {
    title: t('identity.role.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('common.datetime.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.description.label'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('common.table.header.operation'),
    key: 'action',
    width: 150,
    fixed: 'right' as const
  }
]

// 查询参数
const queryParams = ref<RoleQuery>({
  pageIndex: 1,
  pageSize: 10,
  roleName: '',
  roleKey: '',
  status: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'roleName',
    label: t('identity.role.fields.roleName.label'),
    type: 'input' as const
  },
  {
    name: 'roleKey',
    label: t('identity.role.fields.roleKey.label'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('identity.role.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<Role[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedRoleId = ref<number>()

// 菜单分配弹窗
const menuAllocateVisible = ref(false)

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList(queryParams.value)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询方法
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    roleName: '',
    roleKey: '',
    status: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('common.add')
  selectedRoleId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Role) => {
  formTitle.value = t('common.edit')
  selectedRoleId.value = record.roleId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Role) => {
  try {
    const res = await deleteRole(record.roleId)
    if (res.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理导出
const handleExport = () => {
  message.info(t('common.developing'))
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }
  
  const record = tableData.value.find(item => String(item.roleId) === String(selectedRowKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }
  
  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteRole(Number(id))))
    const hasError = results.some(res => res.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理授权
const handleAuthorize = (record: Role) => {
  formTitle.value = t('identity.role.actions.authorize')
  selectedRoleId.value = record.roleId
  menuAllocateVisible.value = true
}

// 初始化
fetchData()
</script>

<style lang="less" scoped>
.role-container {
  padding: 16px;
  background-color: #fff;
}
</style>
