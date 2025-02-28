//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 表格组件
//===================================================================

<template>
  <div class="hbt-table">
    <!-- 工具栏 -->
    <div class="table-toolbar" v-if="showToolbar">
      <!-- 左侧按钮组 -->
      <div class="toolbar-left">
        <a-space class="button-group">
          <!-- 新增按钮 -->
          <a-tooltip title="新增数据">
            <a-button class="hbt-btn hbt-btn-add" @click="handleToolbarAction('add')">
              <template #icon><plus-outlined /></template>
              新增
            </a-button>
          </a-tooltip>
          
          <!-- 编辑按钮 -->
          <a-tooltip title="编辑选中数据">
            <a-button class="hbt-btn hbt-btn-edit" :disabled="!hasSelected" @click="handleToolbarAction('edit')">
              <template #icon><edit-outlined /></template>
              编辑
            </a-button>
          </a-tooltip>
          
          <!-- 删除按钮 -->
          <a-tooltip title="删除选中数据">
            <a-button class="hbt-btn hbt-btn-delete" :disabled="!hasSelected" @click="handleToolbarAction('delete')">
              <template #icon><delete-outlined /></template>
              删除
            </a-button>
          </a-tooltip>
          
          <!-- 导入按钮 -->
          <a-dropdown>
            <a-tooltip title="导入数据">
              <a-button class="hbt-btn hbt-btn-import">
                <template #icon><import-outlined /></template>
                导入
              </a-button>
            </a-tooltip>
            <template #overlay>
              <a-menu class="hbt-dropdown-menu">
                <a-menu-item key="template" class="hbt-btn hbt-btn-download" @click="handleToolbarAction('download-template')">
                  <download-outlined />
                  下载模板
                </a-menu-item>
                <a-menu-item key="import" class="hbt-btn hbt-btn-import" @click="handleToolbarAction('import-data')">
                  <upload-outlined />
                  导入数据
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          
          <!-- 导出按钮 -->
          <a-tooltip title="导出数据">
            <a-button class="hbt-btn hbt-btn-export" @click="handleToolbarAction('export')">
              <template #icon><export-outlined /></template>
              导出
            </a-button>
          </a-tooltip>
          
          <!-- 审核按钮 -->
          <a-tooltip title="审核选中数据">
            <a-button class="hbt-btn hbt-btn-audit" :disabled="!hasSelected" @click="handleToolbarAction('audit')">
              <template #icon><audit-outlined /></template>
              审核
            </a-button>
          </a-tooltip>
          
          <!-- 撤销按钮 -->
          <a-tooltip title="撤销审核">
            <a-button class="hbt-btn hbt-btn-revoke" :disabled="!hasSelected" @click="handleToolbarAction('revoke')">
              <template #icon><rollback-outlined /></template>
              撤销
            </a-button>
          </a-tooltip>
          
          <slot name="toolbar-left" />
        </a-space>
      </div>
      
      <!-- 右侧按钮组 -->
      <div class="toolbar-right">
        <a-space class="button-group">
          <a-tooltip title="刷新数据">
            <a-button class="hbt-btn hbt-btn-refresh hbt-table-right-button" @click="handleRefresh">
              <template #icon><reload-outlined /></template>
            </a-button>
          </a-tooltip>
          <a-tooltip title="显示/隐藏列">
            <a-button class="hbt-btn hbt-btn-config hbt-table-right-button" @click="handleColumnDisplay">
              <template #icon><setting-outlined /></template>
            </a-button>
          </a-tooltip>
          <a-tooltip title="显示/隐藏查询">
            <a-button class="hbt-btn hbt-btn-search hbt-table-right-button" @click="handleSearchDisplay">
              <template #icon><search-outlined /></template>
            </a-button>
          </a-tooltip>
          <a-tooltip title="全屏显示">
            <a-button class="hbt-btn hbt-btn-expand hbt-table-right-button" @click="handleFullScreen">
              <template #icon><fullscreen-outlined /></template>
            </a-button>
          </a-tooltip>
        </a-space>
        <slot name="toolbar-right" />
      </div>
    </div>

    <a-table
      ref="tableRef"
      v-bind="$attrs"
      :columns="renderColumns"
      :data-source="virtualData"
      :loading="loading"
      :pagination="paginationConfig"
      :row-selection="selectionConfig"
      :scroll="scrollConfig"
      :size="size"
      :bordered="bordered"
      :row-key="rowKey"
      @change="handleChange"
      @scroll="handleScroll"
    >
      <!-- 自定义列插槽 -->
      <template v-for="col in columns" #[col.dataIndex]="{ text, record, index }">
        <slot :name="col.dataIndex" :text="text" :record="record" :index="index">
          {{ text }}
        </slot>
      </template>

      <!-- 加载更多 -->
      <template #footer v-if="showLoadMore">
        <div class="load-more">
          <a-button :loading="loading" @click="loadMore">
            {{ loadMoreText }}
          </a-button>
        </div>
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, h, onMounted, watch } from 'vue'
import { usePermission } from '@/hooks/usePermission'
import { 
  Space, 
  Button, 
  Popconfirm, 
  Tooltip, 
  Dropdown,
  Menu
} from 'ant-design-vue'
import type { ButtonProps } from 'ant-design-vue'
import type {
  HbtTableProps,
  HbtTableColumn,
  HbtTableAction,
  HbtTableInstance
} from '@/types/components/table'
import { formatDate } from '@/utils/datetime'
import { formatNumber, formatPercent, formatMoney } from '@/utils/numberformat'
import { 
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  ImportOutlined,
  ExportOutlined,
  DownloadOutlined,
  UploadOutlined,
  AuditOutlined,
  RollbackOutlined,
  ReloadOutlined, 
  SettingOutlined, 
  SearchOutlined, 
  FullscreenOutlined 
} from '@ant-design/icons-vue'

// 格式化单元格值
const formatCellValue = (value: any, column: HbtTableColumn) => {
  if (!column.valueType) return value
  
  switch (column.valueType) {
    case 'date':
      return formatDate(value, column.dateFormat || 'yyyy-MM-dd')
    case 'datetime':
      return formatDate(value, column.dateFormat || 'yyyy-MM-dd HH:mm:ss')
    case 'number':
      return formatNumber(value, column.numberFormat)
    case 'percent':
      return formatPercent(value, column.numberFormat?.precision)
    case 'money':
      return formatMoney(value, column.numberFormat)
    default:
      return value
  }
}

// 定义组件属性
const props = withDefaults(defineProps<HbtTableProps>(), {
  loading: false,
  showIndex: false,
  showSelection: false,
  showToolbar: false,
  toolbarButtons: () => [],
  size: 'middle',
  bordered: false,
  virtual: false,
  lazyLoad: false,
  rowKey: 'id'
})

// 定义组件事件
const emit = defineEmits<{
  (e: 'change', pagination: any, filters: any, sorter: any): void
  (e: 'select', selectedRowKeys: any[], selectedRows: any[]): void
  (e: 'action', action: string, record: any): void
  (e: 'toolbar-action', action: string): void
  (e: 'load-more'): void
}>()

// 表格实例
const tableRef = ref()

// 权限检查
const { hasPermission } = usePermission()

// 虚拟滚动相关
const startIndex = ref(0)
const endIndex = ref(0)
const virtualConfig = computed(() => ({
  pageSize: 50,
  buffer: 10,
  rowHeight: 54,
  dynamicHeight: false,
  ...props.virtualConfig
}))

// 计算虚拟数据
const virtualData = computed(() => {
  if (!props.virtual) return props.dataSource

  const { pageSize, buffer } = virtualConfig.value
  const start = Math.max(0, startIndex.value - buffer)
  const end = Math.min(props.dataSource.length, endIndex.value + buffer)
  
  return props.dataSource.slice(start, end)
})

// 计算滚动配置
const scrollConfig = computed(() => {
  if (!props.scroll) return undefined

  const config = { ...props.scroll }
  
  if (props.virtual && config.y) {
    const { rowHeight, pageSize } = virtualConfig.value
    config.y = rowHeight * pageSize
  }

  return config
})

// 懒加载相关
const showLoadMore = computed(() => {
  if (!props.lazyLoad) return false
  return props.lazyLoadConfig?.showLoadMore ?? true
})

const loadMoreText = computed(() => {
  return props.lazyLoadConfig?.loadMoreText ?? '加载更多'
})

// 计算序号列配置
const indexColumn = computed<HbtTableColumn>(() => ({
  title: '序号',
  dataIndex: 'index',
  width: 80,
  align: 'center',
  customRender: ({ index }) => startIndex.value + index + 1,
  ...props.indexColumn
}))

// 计算表格列配置
const renderColumns = computed(() => {
  const columns: HbtTableColumn[] = []

  // 添加序号列
  if (props.showIndex) {
    columns.push(indexColumn.value)
  }

  // 添加数据列
  columns.push(...props.columns.filter(col => col.visible !== false).map(col => ({
    ...col,
    ellipsis: true,
    customRender: col.customRender || (({ text }) => formatCellValue(text, col))
  })))

  // 添加操作列
  if (props.actionColumn) {
    columns.push({
      title: '操作',
      dataIndex: 'action',
      key: 'action',
      width: props.actionColumn.width,
      fixed: props.actionColumn.fixed,
      ellipsis: false,
      customRender: ({ record }) => {
        const actions = props.actionColumn?.actions.filter(action => 
          !action.permission || hasPermission(action.permission)
        ) || []

        if (!actions.length) return null

        return h(
          Button.Group,
          { size: 'small' },
          {
            default: () => actions.map(action => renderActionButton(action, record))
          }
        )
      }
    })
  }

  return columns
})

// 计算分页配置
const paginationConfig = computed(() => {
  if (!props.pagination) return false

  return {
    current: props.pagination.pageNum,
    pageSize: props.pagination.pageSize,
    total: props.pagination.totalNum,
    showSizeChanger: props.pagination.showSizeChanger ?? true,
    showQuickJumper: props.pagination.showQuickJumper ?? true,
    showTotal: props.pagination.showTotal ?? ((total: number) => `共 ${total} 条`)
  }
})

// 计算行键
const rowKey = computed(() => {
  return (record: any) => {
    if (typeof props.rowKey === 'function') {
      return props.rowKey(record)
    }
    const keyName = props.rowKey as string
    const key = keyName ? record[keyName] : undefined
    if (key !== undefined) return key
    // 如果指定的 rowKey 不存在,尝试使用其他可能的键
    return record.id || record.configId || record.userId || record.key
  }
})

// 选中行状态
const hasSelected = computed(() => {
  return (props.rowSelection?.selectedRowKeys?.length || 0) > 0
})

// 计算选择列配置
const selectionConfig = computed(() => {
  if (!props.showSelection) return undefined

  return {
    type: 'checkbox' as const,
    selectedRowKeys: props.rowSelection?.selectedRowKeys,
    hideSelectAll: false,
    preserveSelectedRowKeys: true,
    onChange: (selectedRowKeys: any[], selectedRows: any[]) => {
      console.log('表格选择变化:', {
        selectedRowKeys,
        selectedRows: selectedRows.length
      })
      emit('select', selectedRowKeys, selectedRows)
    },
    ...props.rowSelection
  }
})

// 处理表格变化
const handleChange = (pagination: any, filters: any, sorter: any) => {
  const pag = {
    pageNum: pagination.current,
    pageSize: pagination.pageSize,
    totalNum: pagination.total,
    showSizeChanger: pagination.showSizeChanger,
    showQuickJumper: pagination.showQuickJumper
  }
  emit('change', pag, filters, sorter)
}

// 处理滚动
const handleScroll = (e: Event) => {
  if (!props.virtual && !props.lazyLoad) return

  const { scrollTop, scrollHeight, clientHeight } = e.target as HTMLElement
  
  if (props.virtual) {
    const { rowHeight, pageSize } = virtualConfig.value
    startIndex.value = Math.floor(scrollTop / rowHeight)
    endIndex.value = Math.min(
      startIndex.value + pageSize,
      props.dataSource.length
    )
  }

  if (props.lazyLoad) {
    const threshold = props.lazyLoadConfig?.threshold ?? 50
    const remainingDistance = scrollHeight - scrollTop - clientHeight
    
    if (remainingDistance <= threshold) {
      emit('load-more')
    }
  }
}

// 加载更多
const loadMore = () => {
  emit('load-more')
}

// 监听数据源变化
watch(() => props.dataSource, () => {
  if (props.virtual) {
    const { pageSize } = virtualConfig.value
    endIndex.value = Math.min(pageSize, props.dataSource.length)
  }
}, { immediate: true })

// 渲染操作按钮
const renderActionButton = (action: HbtTableAction, record: any) => {
  const buttonProps: ButtonProps = {
    type: 'default',
    danger: action.type === 'danger',
    disabled: action.disabled?.(record),
    onClick: () => !action.confirm && emit('action', action.key, record)
  }

  const button = h(
    Button,
    {
      ...buttonProps,
      class: action.type === 'danger' ? 'danger-border' : 'primary-border'
    },
    {
      default: () => [
        action.icon && h(action.icon),
        action.label
      ]
    }
  )

  if (action.confirm) {
    return h(
      Popconfirm,
      {
        title: action.confirm,
        onConfirm: () => emit('action', action.key, record)
      },
      { default: () => button }
    )
  }

  return button
}

// 处理工具栏按钮点击
const handleToolbarAction = (action: string) => {
  emit('toolbar-action', action)
}

// 添加工具栏按钮处理方法
const handleRefresh = () => {
  tableRef.value?.refresh()
}

const handleColumnDisplay = () => {
  // TODO: 实现列显示/隐藏功能
  emit('toolbar-action', 'column-display')
}

const handleSearchDisplay = () => {
  // TODO: 实现查询显示/隐藏功能
  emit('toolbar-action', 'search-display')
}

const handleFullScreen = () => {
  // TODO: 实现全屏显示功能
  emit('toolbar-action', 'full-screen')
}

// 暴露组件实例方法
defineExpose<HbtTableInstance>({
  refresh: () => {
    // 刷新表格数据
    tableRef.value?.refresh()
  },
  reset: () => {
    // 重置表格状态
    tableRef.value?.clearFilters()
    tableRef.value?.clearSorter()
    if (props.virtual) {
      startIndex.value = 0
      const { pageSize } = virtualConfig.value
      endIndex.value = Math.min(pageSize, props.dataSource.length)
    }
  },
  getSelectedRows: () => {
    // 获取选中行
    return tableRef.value?.getSelectedRows() || []
  },
  clearSelection: () => {
    // 清空选中行
    tableRef.value?.clearSelection()
  }
})
</script>

<style lang="less">
@import '@/assets/styles/components/table-button.less';

.hbt-table {
  // 表格容器样式
  &-container {
    position: relative;
    width: 100%;
  }

  // 表格内容样式
  &-content {
    width: 100%;
    overflow: auto;
  }
}
</style> 