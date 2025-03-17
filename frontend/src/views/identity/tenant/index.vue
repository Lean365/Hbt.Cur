<template>
  <div class="tenant-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.tenant.fields.tenantCode.label')">
          <a-input
            v-model:value="queryParams.tenantCode"
            :placeholder="t('identity.tenant.fields.tenantCode.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.tenant.fields.tenantName.label')">
          <a-input
            v-model:value="queryParams.tenantName"
            :placeholder="t('identity.tenant.fields.tenantName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.tenant.fields.contactPerson.label')">
          <a-input
            v-model:value="queryParams.contactPerson"
            :placeholder="t('identity.tenant.fields.contactPerson.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.tenant.fields.contactPhone.label')">
          <a-input
            v-model:value="queryParams.contactPhone"
            :placeholder="t('identity.tenant.fields.contactPhone.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.tenant.fields.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            :placeholder="t('identity.tenant.fields.status.placeholder')"
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
      :add-permission="['identity:tenant:create']"
      :edit-permission="['identity:tenant:update']"
      :delete-permission="['identity:tenant:delete']"
      :export-permission="['identity:tenant:export']"
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
      :pagination="pagination"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      row-key="tenantId"
      size="middle"
      bordered
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
      @change="onTableChange"
    >
      <template #bodyCell="{ column, record }">
        <!-- 状态 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 操作 -->
        <template v-else-if="column.dataIndex === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['identity:tenant:update']"
            :delete-permission="['identity:tenant:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </a-table>

    <!-- 表单弹窗 -->
    <tenant-form
      v-model:visible="formVisible"
      :title="formTitle"
      :tenant-id="formTenantId"
      @success="getList"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Tenant, TenantQuery } from '@/types/identity/tenant'
import { getPagedList, deleteTenant, batchDeleteTenant, exportTenant } from '@/api/identity/tenant'
import TenantForm from './components/TenantForm.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tenantCode',
    label: t('identity.tenant.fields.tenantCode.label'),
    type: 'input',
    props: {
      placeholder: t('identity.tenant.fields.tenantCode.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'tenantName',
    label: t('identity.tenant.fields.tenantName.label'),
    type: 'input',
    props: {
      placeholder: t('identity.tenant.fields.tenantName.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'contactPerson',
    label: t('identity.tenant.fields.contactPerson.label'),
    type: 'input',
    props: {
      placeholder: t('identity.tenant.fields.contactPerson.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'contactPhone',
    label: t('identity.tenant.fields.contactPhone.label'),
    type: 'input',
    props: {
      placeholder: t('identity.tenant.fields.contactPhone.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.tenant.fields.status.label'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      placeholder: t('identity.tenant.fields.status.placeholder'),
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<TenantQuery>({
  pageIndex: 1,
  pageSize: 10,
  tenantCode: undefined,
  tenantName: undefined,
  contactPerson: undefined,
  contactPhone: undefined,
  status: undefined
})

// 加载状态
const loading = ref(false)

// 租户列表数据
const list = ref<Tenant[]>([])

// 分页配置
const pagination = ref<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// 选中的租户ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的租户ID
const formTenantId = ref<number>()

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.tenant.fields.tenantCode.label'),
    dataIndex: 'tenantCode',
    width: 120
  },
  {
    title: t('identity.tenant.fields.tenantName.label'),
    dataIndex: 'tenantName',
    width: 150
  },
  {
    title: t('identity.tenant.fields.contactPerson.label'),
    dataIndex: 'contactPerson',
    width: 120
  },
  {
    title: t('identity.tenant.fields.contactPhone.label'),
    dataIndex: 'contactPhone',
    width: 120
  },
  {
    title: t('identity.tenant.fields.contactEmail.label'),
    dataIndex: 'contactEmail',
    width: 180
  },
  {
    title: t('identity.tenant.fields.domain.label'),
    dataIndex: 'domain',
    width: 180
  },
  {
    title: t('identity.tenant.fields.maxUserCount.label'),
    dataIndex: 'maxUserCount',
    width: 100
  },
  {
    title: t('identity.tenant.fields.status.label'),
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

// 获取租户列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getPagedList(queryParams.value)
    if (res.code === 200) {
      list.value = res.data.rows
      pagination.value.total = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[租户管理] 获取租户列表出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  selectedKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    tenantCode: undefined,
    tenantName: undefined,
    contactPerson: undefined,
    contactPhone: undefined,
    status: undefined
  }
  getList()
}

// 处理表格变化
const onTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current as number
  queryParams.value.pageSize = pagination.pageSize as number
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: Tenant[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.tenant.dialog.create')
  formTenantId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const tenantId = selectedKeys.value[0]
  formTitle.value = t('identity.tenant.dialog.update')
  formTenantId.value = Number(tenantId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.tenant.dialog.update')
  formTenantId.value = Number(record.tenantId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteTenant(Number(record.tenantId))
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
        const res = await batchDeleteTenant(selectedKeys.value.map(id => Number(id)))
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
    const res = await exportTenant()
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
.tenant-container {
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