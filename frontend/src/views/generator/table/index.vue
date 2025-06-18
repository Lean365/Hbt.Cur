<template>
  <div class="table-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleSearch"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('generator.table.name')">
          <a-input
            v-model:value="queryParams.tableName"
            :placeholder="t('generator.table.placeholder.name')"
            allow-clear
            @keyup.enter="handleSearch"
          />
        </a-form-item>
        <a-form-item :label="t('generator.table.comment')">
          <a-input
            v-model:value="queryParams.tableComment"
            :placeholder="t('generator.table.placeholder.comment')"
            allow-clear
            @keyup.enter="handleSearch"
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="false"
      :add-permission="['generator:table:create']"
      :show-edit="true"
      :edit-permission="['generator:table:update']"
      :show-delete="true"
      :delete-permission="['generator:table:delete']"
      :show-import="true"
      :import-permission="['generator:table:import']"
      :show-export="true"
      :export-permission="['generator:table:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
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
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 1000 }"
      :row-key="(record: HbtGenTable) => String(record.genTableId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['generator:table:query']"
            :show-edit="true"
            :edit-permission="['generator:table:update']"
            :show-delete="true"
            :delete-permission="['generator:table:delete']"
            :show-generate="true"
            :generate-permission="['generator:table:generate']"
            :show-sync="true"
            :sync-permission="['generator:table:sync']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
            @generate="handleGenerate"
            @sync="handleSync"
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

    <!-- 表单项对话框 -->
    <table-form
      v-model:open="formVisible"
      :title="formTitle"
      :id="selectedTableId"
      @success="handleSuccess"
    />

    <!-- 表详情对话框 -->
    <table-detail
      v-model:open="detailVisible"
      :table-id="selectedTableId"
      @update:open="handleDetailClose"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.columnSetting')"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <!-- 导入对话框 -->
    <import-table
      v-model:visible="importVisible"
      @success="handleImportSuccess"
    />
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { HbtGenTable, HbtGenTableQuery } from '@/types/generator/genTable.d'
import type { QueryField } from '@/types/components/query'
import {
  getTableList,
  deleteTable, 
  downloadCode,
  generateCode,
  syncTable
} from '@/api/generator/genTable'
import { getTableDefineTemplate } from '@/api/generator/genTableDefine'
import TableForm from './components/TableForm.vue'
import TableDetail from './components/TableDetail.vue'
import ImportTable from './components/ImportTable.vue'
import { formatDateTime } from '@/utils/datetime'

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(true)
const tableData = ref<HbtGenTable[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedTableId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const columnSettingVisible = ref(false)
const total = ref(0)
const importVisible = ref(false)

// === 查询参数 ===
const queryParams = ref<HbtGenTableQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableName: '',
  tableComment: ''
})

// === 查询字段定义 ===
const queryFields = computed<QueryField[]>(() => [
  {
    name: 'tableName',
    label: t('generator.table.name'),
    type: 'input',
    placeholder: t('generator.table.placeholder.name')
  },
  {
    name: 'tableComment',
    label: t('generator.table.comment'),
    type: 'input',
    placeholder: t('generator.table.placeholder.comment')
  }
])

// === 表格列定义 ===
const defaultColumns = [
  {
    title: 'ID',
    dataIndex: 'genTableId',
    key: 'id',
    width: 80,
    fixed: 'left',
  },
  {
    title: t('generator.table.fields.databaseName'),
    dataIndex: 'databaseName',
    key: 'databaseName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.tableName'),
    dataIndex: 'tableName',
    key: 'tableName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.tableComment'),
    dataIndex: 'tableComment',
    key: 'tableComment',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.baseNamespace'),
    dataIndex: 'baseNamespace',
    key: 'baseNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.entityNamespace'),
    dataIndex: 'entityNamespace',
    key: 'entityNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.entityClassName'),
    dataIndex: 'entityClassName',
    key: 'entityClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.dtoNamespace'),
    dataIndex: 'dtoNamespace',
    key: 'dtoNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.dtoClassName'),
    dataIndex: 'dtoClassName',
    key: 'dtoClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.dtoType'),
    dataIndex: 'dtoType',
    key: 'dtoType',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.serviceNamespace'),
    dataIndex: 'serviceNamespace',
    key: 'serviceNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.iServiceClassName'),
    dataIndex: 'iServiceClassName',
    key: 'iServiceClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.serviceClassName'),
    dataIndex: 'serviceClassName',
    key: 'serviceClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.iRepositoryNamespace'),
    dataIndex: 'iRepositoryNamespace',
    key: 'iRepositoryNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.iRepositoryClassName'),
    dataIndex: 'iRepositoryClassName',
    key: 'iRepositoryClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.repositoryNamespace'),
    dataIndex: 'repositoryNamespace',
    key: 'repositoryNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.repositoryClassName'),
    dataIndex: 'repositoryClassName',
    key: 'repositoryClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.controllerNamespace'),
    dataIndex: 'controllerNamespace',
    key: 'controllerNamespace',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.controllerClassName'),
    dataIndex: 'controllerClassName',
    key: 'controllerClassName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.tplType'),
    dataIndex: 'tplType',
    key: 'tplType',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.tplCategory'),
    dataIndex: 'tplCategory',
    key: 'tplCategory',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.subTableName'),
    dataIndex: 'subTableName',
    key: 'subTableName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.subTableFkName'),
    dataIndex: 'subTableFkName',
    key: 'subTableFkName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.treeCode'),
    dataIndex: 'treeCode',
    key: 'treeCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.treeName'),
    dataIndex: 'treeName',
    key: 'treeName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.treeParentCode'),
    dataIndex: 'treeParentCode',
    key: 'treeParentCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.moduleName'),
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.businessName'),
    dataIndex: 'businessName',
    key: 'businessName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.functionName'),
    dataIndex: 'functionName',
    key: 'functionName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.author'),
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.genMethod'),
    dataIndex: 'genMethod',
    key: 'genMethod',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.genPath'),
    dataIndex: 'genPath',
    key: 'genPath',
    width: 200,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.ParentMenuId'),
    dataIndex: 'ParentMenuId',
    key: 'ParentMenuId',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.sortType'),
    dataIndex: 'sortType',
    key: 'sortType',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.sortField'),
    dataIndex: 'sortField',
    key: 'sortField',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.permsPrefix'),
    dataIndex: 'permsPrefix',
    key: 'permsPrefix',
    width: 150,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.generateMenu'),
    dataIndex: 'generateMenu',
    key: 'generateMenu',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.frontTpl'),
    dataIndex: 'frontTpl',
    key: 'frontTpl',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.btnStyle'),
    dataIndex: 'btnStyle',
    key: 'btnStyle',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.frontStyle'),
    dataIndex: 'frontStyle',
    key: 'frontStyle',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.table.fields.isGenCode'),
    dataIndex: 'isGenCode',
    key: 'isGenCode',
    width: 100,
    ellipsis: true
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// 列设置
const columnSettings = ref<Record<string, boolean>>({})
const columns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getTableList(queryParams.value)
    console.log('获取表列表响应:', res)
    if (res.data.code === 200) {
      // 检查数据是否包含必要的字段
      const validData = res.data.data.rows.map((item: any) => {
        if (!item.genTableId || typeof item.genTableId !== 'number') {
          console.error('记录缺少有效的 id:', item)
          return null
        }
        return {
          ...item,
          genTableId: Number(item.genTableId) // 确保 id 是数字类型
        }
      }).filter(Boolean)
      
      if (validData.length !== res.data.data.rows.length) {
        console.warn('部分记录缺少有效的 id，已过滤')
      }
      
      tableData.value = validData
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载表列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleSearch = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    tableName: '',
    tableComment: ''
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  if (pag.current) {
    queryParams.value.pageIndex = pag.current
  }
  if (pag.pageSize) {
    queryParams.value.pageSize = pag.pageSize
  }
  fetchData()
}

// 行点击
const handleRowClick = (record: HbtGenTable) => {
  console.log('handleRowClick - 记录:', record)
  if (!record.genTableId || typeof record.genTableId !== 'number') {
    console.error('handleRowClick - 记录缺少有效的 id:', record)
    message.error(t('common.message.invalidRecord'))
    return
  }
  
  const tableId = Number(record.genTableId)
  if (isNaN(tableId)) {
    console.error('handleRowClick - id 转换为数字失败:', record.genTableId)
    message.error(t('common.message.invalidRecord'))
    return
  }
  
  selectedTableId.value = tableId
  console.log('handleRowClick - 设置 id:', tableId)
}

// 选择行变化
const onSelectChange = (keys: string[]) => {
  selectedRowKeys.value = keys
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: (string | number | boolean)[]) => {
  defaultColumns.forEach(col => {
    columnSettings.value[col.key] = checkedValues.includes(col.key)
  })
  // 保存列设置到本地存储
  localStorage.setItem('genTableColumns', JSON.stringify(columnSettings.value))
}

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('genTableColumns')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    // 默认显示所有列
    defaultColumns.forEach(col => {
      columnSettings.value[col.key] = true
    })
  }
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // TODO: 实现全屏切换
}

// 新增
const handleAdd = () => {
  selectedTableId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.genTableId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtGenTable) => {
  selectedTableId.value = record.genTableId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 查看
const handleView = (record: HbtGenTable) => {
  console.log('handleView - 记录:', record)
  if (!record.genTableId || typeof record.genTableId !== 'number') {
    console.error('handleView - 记录缺少有效的 id:', record)
    message.error(t('common.message.invalidRecord'))
    return
  }
  
  const tableId = Number(record.genTableId)
  if (isNaN(tableId)) {
    console.error('handleView - id 转换为数字失败:', record.genTableId)
    message.error(t('common.message.invalidRecord'))
    return
  }
  
  selectedTableId.value = tableId
  console.log('handleView - 设置 id:', tableId)
  // 确保在设置 tableId 后再打开对话框
  nextTick(() => {
    detailVisible.value = true
  })
}

// 处理详情对话框关闭
const handleDetailClose = () => {
  detailVisible.value = false
  selectedTableId.value = undefined
}

// 删除
const handleDelete = async (record: HbtGenTable) => {
  try {
    const res = await deleteTable(record.genTableId)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  try {
    // 使用 Promise.all 并行删除选中的记录
    const promises = selectedRowKeys.value.map(key => deleteTable(Number(key)))
    await Promise.all(promises)
    message.success(t('common.message.deleteSuccess'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入成功
const handleImportSuccess = () => {
  message.success(t('common.import.success'))
  fetchData()
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const res = await downloadCode(selectedTableId.value!)
    if (res instanceof Blob) {
      // 创建下载链接
      const url = window.URL.createObjectURL(res)
      const link = document.createElement('a')
      link.href = url
      link.download = `代码生成表_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
      message.success(t('common.message.exportSuccess'))
    } else {
      message.error(t('common.message.exportFailed'))
    }
  } catch (error) {
    console.error('导出失败:', error)
    message.error(t('common.message.exportFailed'))
  } finally {
    loading.value = false
  }
}

// 列设置
const handleColumnSetting = () => {
  // 初始化列设置状态
  if (Object.keys(columnSettings.value).length === 0) {
    defaultColumns.forEach(col => {
      columnSettings.value[col.key] = true
    })
  }
  columnSettingVisible.value = true
}

// 页码变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 每页条数变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchData()
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedTableId.value = undefined
  selectedRowKeys.value = []
  fetchData()
}

// 工具栏事件处理
const handleTemplate = async () => {
  try {
    loading.value = true
    const res = await getTableDefineTemplate()
    if (res.data) {
      const blob = res.data
      const url = window.URL.createObjectURL(blob)
      const link = document.createElement('a')
      link.href = url
      link.download = `代码生成表导入模板_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
      message.success(t('common.message.downloadSuccess'))
    } else {
      message.error(t('common.message.downloadFailed'))
    }
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('common.message.downloadFailed'))
  } finally {
    loading.value = false
  }
}

// 生成代码
const handleGenerate = async (record: HbtGenTable) => {
  try {
    const res = await generateCode(record.genTableId)
    if (res.data.code === 200) {
      message.success(t('common.message.generateSuccess'))
    } else {
      message.error(res.data.msg || t('common.message.generateFailed'))
    }
  } catch (error) {
    console.error('生成代码失败:', error)
    message.error(t('common.message.generateFailed'))
  }
}

// 同步表结构
const handleSync = async (record: HbtGenTable) => {
  try {
    const res = await syncTable(record.genTableId)
    if (res.data.code === 200) {
      message.success(t('common.message.syncSuccess'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.syncFailed'))
    }
  } catch (error) {
    console.error('同步表结构失败:', error)
    message.error(t('common.message.syncFailed'))
  }
}

// === 生命周期 ===
onMounted(() => {
  // 初始化列设置
  initColumnSettings()
  // 加载数据
  fetchData()
})
</script>

<style lang="less" scoped>
.table-container {
  padding: 24px;
  background-color: #fff;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}
</style> 