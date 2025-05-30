<template>
  <div class="tenant-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedRowKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :add-permission="['identity:tenant:create']"
      :edit-permission="['identity:tenant:update']"
      :delete-permission="['identity:tenant:delete']"
      :export-permission="['identity:tenant:export']"
      :import-permission="['identity:tenant:import']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @import="handleImport"
      @template="handleTemplate"
      @refresh="getList"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="list"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      row-key="tenantId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
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
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 表单弹窗 -->
    <tenant-form
      v-model:visible="formVisible"
      :title="formTitle"
      :tenant-id="formTenantId"
      @success="getList"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :visible="columnSettingVisible"
      title="列设置"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <a-checkbox-group
        :value="Object.keys(columnSettings).filter(key => columnSettings[key])"
        @change="handleColumnSettingChange"
        class="column-setting-group"
      >
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtTenant, HbtTenantQuery } from '@/types/identity/tenant'
import { getTenantList, deleteTenant, batchDeleteTenant, exportTenant, importTenant, downloadTemplate } from '@/api/identity/tenant'
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
    name: 'contactUser',
    label: t('identity.tenant.fields.contactUser.label'),
    type: 'input',
    props: {
      placeholder: t('identity.tenant.fields.contactUser.placeholder'),
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
const queryParams = ref<HbtTenantQuery>({
  pageIndex: 1,
  pageSize: 10,
  tenantCode: undefined,
  tenantName: undefined,
  contactUser: undefined,
  contactPhone: undefined,
  status: undefined
})

// 加载状态
const loading = ref(false)

// 租户列表数据
const list = ref<HbtTenant[]>([])

// 选中的租户ID
const selectedRowKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的租户ID
const formTenantId = ref<number>()

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.tenant.fields.tenantId.label'),
    dataIndex: 'tenantId',
    key: 'tenantId',
    width: 120
  },
  {
    title: t('identity.tenant.fields.tenantName.label'),
    dataIndex: 'tenantName',
    key: 'tenantName',
    width: 150
  },
  {
    title: t('identity.tenant.fields.tenantCode.label'),
    dataIndex: 'tenantCode',
    key: 'tenantCode',
    width: 120
  },
  {
    title: t('identity.tenant.fields.contactUser.label'),
    dataIndex: 'contactUser',
    key: 'contactUser',
    width: 120
  },
  {
    title: t('identity.tenant.fields.contactPhone.label'),
    dataIndex: 'contactPhone',
    key: 'contactPhone',
    width: 120
  },
  {
    title: t('identity.tenant.fields.contactEmail.label'),
    dataIndex: 'contactEmail',
    key: 'contactEmail',
    width: 180
  },
  {
    title: t('identity.tenant.fields.address.label'),
    dataIndex: 'address',
    key: 'address',
    width: 180
  },
  {
    title: t('identity.tenant.fields.license.label'),
    dataIndex: 'license',
    key: 'license',
    width: 100
  },
  {
    title: t('identity.tenant.fields.expireTime.label'),
    dataIndex: 'expireTime',
    key: 'expireTime',
    width: 100
  },
  {
    title: t('identity.tenant.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('identity.tenant.fields.isDefault.label'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: t('identity.tenant.fields.dbConnection.label'),
    dataIndex: 'dbConnection',
    key: 'dbConnection',
    width: 100
  },
  {
    title: t('identity.tenant.fields.logoUrl.label'),
    dataIndex: 'logoUrl',
    key: 'logoUrl',
    width: 100
  },
  {
    title: t('identity.tenant.fields.theme.label'),
    dataIndex: 'theme',
    key: 'theme',
    width: 100
  },
  {
    title: t('identity.tenant.fields.licenseStartTime.label'),
    dataIndex: 'licenseStartTime',
    key: 'licenseStartTime',
    width: 100
  },
  {
    title: t('identity.tenant.fields.licenseEndTime.label'),
    dataIndex: 'licenseEndTime',
    key: 'licenseEndTime',
    width: 100
  },
  {
    title: t('identity.tenant.fields.maxUserCount.label'),
    dataIndex: 'maxUserCount',
    key: 'maxUserCount',
    width: 100
  },
  {
    title: t('identity.menu.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.isDeleted'),
    dataIndex: 'isDeleted',
    key: 'isDeleted',
    width: 100
  },
  {
    title: t('identity.menu.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200
  },
  {
    title: t('identity.menu.columns.action'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right' as const
  }
]

// 添加 total
const total = ref(0)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('tenantColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前6列（不包含操作列）
  const firstSixColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 6)

  // 设置前6列为true
  firstSixColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else if (col.key) {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('tenantColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索区域显示状态
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏显示
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
  console.log('切换全屏显示')
}

// 获取租户列表
const getList = async () => {
  try {
    loading.value = true
    const { data } = await getTenantList(queryParams.value)
    if (data.code === 200) {
      list.value = data.data.rows
      total.value = data.data.totalNum
    } else {
      message.error(data.msg || t('common.failed'))
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
  selectedRowKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    tenantCode: undefined,
    tenantName: undefined,
    contactUser: undefined,
    contactPhone: undefined,
    status: undefined
  }
  getList()
}

// 处理表格变化
const onTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  getList()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.tenant.actions.create')
  formTenantId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const tenantId = selectedRowKeys.value[0]
  formTitle.value = t('identity.tenant.actions.update')
  formTenantId.value = Number(tenantId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.tenant.actions.update')
  formTenantId.value = Number(record.tenantId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const { data } = await deleteTenant(Number(record.tenantId))
    if (data.code === 200) {
      message.success(t('common.delete.success'))
      getList()
    } else {
      message.error(data.msg || t('common.delete.failed'))
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
    content: t('common.delete.message', { count: selectedRowKeys.value.length }),
    async onOk() {
      try {
        const { data } = await batchDeleteTenant(selectedRowKeys.value.map(id => Number(id)))
        if (data.code === 200) {
          message.success(t('common.delete.success'))
          selectedRowKeys.value = []
          getList()
        } else {
          message.error(data.msg || t('common.delete.failed'))
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
    const res = await exportTenant({
      ...queryParams.value
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    console.log('disposition', disposition)
    let fileName = ''
    if (disposition) {
      // 优先匹配 filename*（带中文）
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        // 再匹配 filename
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `租户数据_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

// 添加分页处理方法
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  getList()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  getList()
}

// 处理导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const res = await importTenant(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
      console.log(
        'fail:',
        (res.data as any).Data?.fail,
        'success:',
        (res.data as any).Data?.success
      )

      if (success > 0 && fail === 0) {
        message.success(`导入成功${success}条，全部成功！`)
      } else if (success > 0 && fail > 0) {
        message.warning(`导入成功${success}条，失败${fail}条`)
      } else if (success === 0 && fail > 0) {
        message.error(`全部导入失败，共${fail}条`)
      } else {
        message.info('未读取到任何数据')
      }
      if (success > 0) getList()
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await downloadTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `租户导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

onMounted(() => {
  initColumnSettings()
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

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style>
