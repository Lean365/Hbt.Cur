<template>
  <div class="oauth-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['identity:oauth:create']"
      :show-edit="true"
      :edit-permission="['identity:oauth:update']"
      :show-delete="true"
      :delete-permission="['identity:oauth:delete']"
      :show-import="true"
      :import-permission="['identity:oauth:import']"
      :show-export="true"
      :export-permission="['identity:oauth:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 提供商列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'provider'">
          <a-tag :color="getProviderColor(record.provider)">
            {{ getProviderName(record.provider) }}
          </a-tag>
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <a-switch
            :checked="record.status === 0"
            checked-children="正常"
            un-checked-children="禁用"
            @change="val => handleStatusChange(record, !!val)"
            :loading="record.statusLoading"
          />
        </template>

        <!-- 是否主要账号列 -->
        <template v-if="column.dataIndex === 'isPrimary'">
          <a-tag :color="record.isPrimary === 1 ? 'blue' : 'default'">
            {{ record.isPrimary === 1 ? '主要' : '次要' }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:oauth:update']"
            :show-delete="true"
            :delete-permission="['identity:oauth:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- OAuth表单对话框 -->
    <oauth-form
      v-model:visible="oauthFormVisible"
      :title="formTitle"
      :oauth-id="selectedOAuthId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <hbt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="OAuth账号导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的OAuth账号信息字段,\n如用户ID,提供商,OAuth用户ID,OAuth用户名,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:oauth:template']"
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

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { HbtOAuth, HbtOAuthQuery } from '@/types/identity/oauth'
import type { QueryField } from '@/types/components/query'
import {
  getOAuthList,
  deleteOAuth,
  exportOAuthAccounts,
  importOAuthAccounts,
  getOAuthImportTemplate,
  updateOAuthStatus
} from '@/api/identity/auth/oatuh'
import OAuthForm from './components/OAuthForm.vue'

const { t } = useI18n()

// 表格列定义
interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width?: number
  ellipsis?: boolean
  fixed?: string
}

const columns: TableColumn[] = [
  {
    title: 'OAuth账号ID',
    dataIndex: 'id',
    key: 'id',
    width: 100
  },
  {
    title: '用户ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 100
  },
  {
    title: '提供商',
    dataIndex: 'provider',
    key: 'provider',
    width: 120
  },
  {
    title: 'OAuth用户ID',
    dataIndex: 'oauthUserId',
    key: 'oauthUserId',
    width: 150
  },
  {
    title: 'OAuth用户名',
    dataIndex: 'oauthUserName',
    key: 'oauthUserName',
    width: 150
  },
  {
    title: '绑定时间',
    dataIndex: 'bindTime',
    key: 'bindTime',
    width: 180
  },
  {
    title: '是否主要账号',
    dataIndex: 'isPrimary',
    key: 'isPrimary',
    width: 120
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: '创建者',
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '更新者',
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<HbtOAuthQuery>({
  pageIndex: 1,
  pageSize: 10,
  userId: undefined,
  provider: undefined,
  oauthUserId: undefined,
  status: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userId',
    label: '用户ID',
    type: 'number' as const
  },
  {
    name: 'provider',
    label: '提供商',
    type: 'select' as const,
    props: {
      options: [
        { label: 'GitHub', value: 'github' },
        { label: 'Google', value: 'google' },
        { label: 'Facebook', value: 'facebook' },
        { label: 'Twitter', value: 'twitter' },
        { label: 'QQ', value: 'qq' },
        { label: '微信', value: 'wechat' },
        { label: '支付宝', value: 'alipay' },
        { label: 'Gitee', value: 'gitee' }
      ],
      showAll: true
    }
  },
  {
    name: 'oauthUserId',
    label: 'OAuth用户ID',
    type: 'input' as const
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '正常', value: 0 },
        { label: '禁用', value: 1 }
      ],
      showAll: true
    }
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtOAuth[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const oauthFormVisible = ref(false)
const formTitle = ref('')
const selectedOAuthId = ref<number>()

// 导入对话框
const importVisible = ref(false)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('oauthColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getOAuthList(queryParams.value)
    console.log('res:', res)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    userId: undefined,
    provider: undefined,
    oauthUserId: undefined,
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
  formTitle.value = t('common.actions.add')
  selectedOAuthId.value = undefined
  oauthFormVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtOAuth) => {
  formTitle.value = t('common.actions.edit')
  selectedOAuthId.value = record.id
  oauthFormVisible.value = true
}

// 处理删除
const handleDelete = async (record: HbtOAuth) => {
  try {
    const res = await deleteOAuth(record.id)
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportOAuthAccounts({
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
      fileName = `OAuth账号数据_${new Date().getTime()}.xlsx`
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

// 处理表单提交成功
const handleSuccess = () => {
  oauthFormVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.id) === String(selectedRowKeys.value[0])
  )
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteOAuth(Number(id))))
    const hasError = results.some(res => res.data.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      const firstError = results.find(res => res.data.code !== 200)
      message.error(firstError?.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('oauthColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: HbtOAuth) => {
  console.log('行点击:', record)
}

// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importOAuthAccounts(file)
    console.log('导入响应数据:', res)
    console.log('res.data:', res.data)
    console.log('res.data.Data:', (res.data as any).Data)
    
    // 根据其他页面的模式，使用 (res.data as any).Data
    const { success = 0, fail = 0 } = (res.data as any).Data || {}
    
    // 将数据结构转换为组件期望的格式，使用正确的大写字段名
    return {
      code: res.data.code,
      msg: res.data.msg,
      data: {
        success,
        fail
      }
    }
  } catch (error: any) {
    console.error('导入失败:', error)
    throw error
  }
}

// 处理下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getOAuthImportTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}

// 处理导入成功
const handleImportSuccess = () => {
  fetchData()
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 处理状态变化
const handleStatusChange = async (record: HbtOAuth, checked: boolean) => {
  // @ts-ignore
  record.statusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    const res = await updateOAuthStatus(record.id, newStatus)
    if (res.data.code === 200) {
      record.status = newStatus
      message.success('状态更新成功')
    } else {
      message.error(res.data.msg || '状态更新失败')
    }
  } catch (error) {
    message.error('状态更新失败')
  }
  // @ts-ignore
  record.statusLoading = false
}

// 获取提供商颜色
const getProviderColor = (provider: string) => {
  const colors: Record<string, string> = {
    github: 'black',
    google: 'red',
    facebook: 'blue',
    twitter: 'cyan',
    qq: 'blue',
    wechat: 'green',
    alipay: 'blue',
    gitee: 'red'
  }
  return colors[provider] || 'default'
}

// 获取提供商名称
const getProviderName = (provider: string) => {
  const names: Record<string, string> = {
    github: 'GitHub',
    google: 'Google',
    facebook: 'Facebook',
    twitter: 'Twitter',
    qq: 'QQ',
    wechat: '微信',
    alipay: '支付宝',
    gitee: 'Gitee'
  }
  return names[provider] || provider
}

onMounted(() => {
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.oauth-container {
  height: 100%;
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