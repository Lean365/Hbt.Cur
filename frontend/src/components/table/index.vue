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
import { Space, Button, Popconfirm } from 'ant-design-vue'
import type { ButtonProps } from 'ant-design-vue'
import type {
  HbtTableProps,
  HbtTableColumn,
  HbtTableAction,
  HbtTableInstance
} from '@/types/components/table'
import { formatDate } from '@/utils/datetime'
import { formatNumber, formatPercent, formatMoney } from '@/utils/numberformat'

// 格式化单元格值
const formatCellValue = (value: any, column: HbtTableColumn) => {
  if (!column.valueType) return value
  
  switch (column.valueType) {
    case 'date':
      return formatDate(value, column.dateFormat || 'YYYY-MM-DD')
    case 'datetime':
      return formatDate(value, column.dateFormat || 'YYYY-MM-DD HH:mm:ss')
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
  size: 'middle',
  bordered: false,
  virtual: false,
  lazyLoad: false
})

// 定义组件事件
const emit = defineEmits<{
  (e: 'change', pagination: any, filters: any, sorter: any): void
  (e: 'select', selectedRowKeys: any[], selectedRows: any[]): void
  (e: 'action', action: string, record: any): void
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

// 计算选择列配置
const selectionConfig = computed(() => {
  if (!props.showSelection) return undefined

  return {
    onChange: (selectedRowKeys: any[], selectedRows: any[]) => {
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
  },
  exportData: (options?: { filename?: string; columns?: HbtTableColumn[] }) => {
    // 导出数据
    const { filename = 'export.xlsx', columns = props.columns } = options || {}
    const data = props.dataSource.map(record => {
      const row: Record<string, any> = {}
      columns.forEach(col => {
        if (col.exportable !== false) {
          row[col.exportTitle || col.title] = record[col.dataIndex]
        }
      })
      return row
    })
    // TODO: 实现导出逻辑
  }
})
</script>

<style lang="less" scoped>
.hbt-table {
  .load-more {
    text-align: center;
    margin: 12px 0;
  }

  :deep(.primary-border) {
    color: #1890ff;
    border-color: #1890ff;
    background: transparent;

    &:hover {
      color: #fff;
      border-color: #1890ff;
      background: #1890ff;
    }

    &:focus {
      color: #1890ff;
      border-color: #1890ff;
      background: transparent;
    }
  }

  :deep(.danger-border) {
    color: #ff4d4f;
    border-color: #ff4d4f;
    background: transparent;

    &:hover {
      color: #fff;
      border-color: #ff4d4f;
      background: #ff4d4f;
    }

    &:focus {
      color: #ff4d4f;
      border-color: #ff4d4f;
      background: transparent;
    }
  }
}
</style> 