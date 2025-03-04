<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: table/index.vue
创建日期: 2024-03-20
描述: 统一封装的表格组件，提供数据展示、分页、排序、筛选、行选择等功能
功能特点:
1. 支持数据展示和分页
2. 支持行选择（单选/多选）
3. 支持表格排序和筛选
4. 支持虚拟滚动优化性能
5. 支持自定义列配置
6. 支持表格大小和边框样式设置
=================================================================== 
-->

<template>
  <div class="hbt-table-container">
    <!-- 表格主体 - 基于 Ant Design Vue 的 Table 组件封装 -->
    <a-table
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="paginationConfig"
      :row-selection="rowSelectionConfig"
      :scroll="scrollConfig"
      :bordered="bordered"
      :size="size"
      :virtual="enableVirtual"
      :row-height="rowHeight"
      :row-key="rowKey"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 动态插槽渲染 - 支持自定义列内容 -->
      <template #[item]="data" v-for="item in Object.keys($slots)">
        <slot :name="item" v-bind="data"></slot>
      </template>
    </a-table>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, watch } from 'vue'
import type { TableProps } from 'ant-design-vue'

// === 类型定义 ===
interface Props {
  // 表格列配置 - 定义表格的列结构，包括标题、数据字段、宽度等
  columns?: any[]
  
  // 表格数据源 - 要展示的数据数组
  dataSource?: any[]
  
  // 加载状态 - 控制表格的加载中状态显示
  loading?: boolean
  
  // 分页配置 - 设置分页器的参数，如每页条数、是否显示快速跳转等
  pagination?: any
  
  // 行选择配置 - 设置行选择的类型（单选/多选）和选择事件回调
  rowSelection?: TableProps['rowSelection']
  
  // 滚动配置 - 设置表格的水平和垂直滚动
  scroll?: TableProps['scroll']
  
  // 是否显示边框 - 控制表格单元格的边框显示
  bordered?: boolean
  
  // 表格大小 - 设置表格的尺寸，可选值：small/middle/large
  size?: 'small' | 'middle' | 'large'
  
  // 是否启用虚拟滚动 - 大数据量时优化性能
  enableVirtual?: boolean
  
  // 行高 - 虚拟滚动时的行高设置
  rowHeight?: number
  
  // 启用虚拟滚动的数据量阈值 - 超过该值时自动启用虚拟滚动
  threshold?: number
  
  // 行数据的唯一标识字段 - 用于选择功能的键值
  rowKey?: string | ((record: any) => string)
  
  // 当前选中行的 key 数组
  selectedRowKeys?: (string | number)[]
}

// === 属性定义 - 设置属性的默认值 ===
const props = withDefaults(defineProps<Props>(), {
  columns: () => [],
  dataSource: () => [],
  loading: false,
  pagination: undefined,
  bordered: false,
  size: 'middle',
  enableVirtual: false,
  rowHeight: 54,
  threshold: 100,
  rowKey: 'id',
  selectedRowKeys: () => []
})

// === 事件定义 - 声明组件对外暴露的事件 ===
const emit = defineEmits<{
  // 分页、排序、筛选变化时触发
  'change': [pagination: any, filters: any, sorter: any]
  // 行点击事件
  'row-click': [record: any, index: number, event: Event]
  // 选中项发生变化时触发
  'update:selectedRowKeys': [(string | number)[]]
}>()

// === 计算属性 ===
// 行选择配置 - 处理行选择的相关配置和事件
const rowSelectionConfig = computed(() => {
  if (!props.rowSelection) return undefined
  
  const baseConfig = props.rowSelection
  return {
    ...baseConfig, // 保留用户传入的配置
    selectedRowKeys: props.selectedRowKeys,
    onChange: (selectedKeys: (string | number)[], selectedRows: any[]) => {
      console.log('=== HbtTable 选择变化 ===')
      console.log('选中键值:', selectedKeys)
      console.log('选中行数据:', selectedRows)
      
      // 直接发送选中的键值
      emit('update:selectedRowKeys', selectedKeys)
      
      // 调用用户传入的 onChange 回调
      baseConfig.onChange?.(selectedKeys, selectedRows)
    }
  }
})

// 分页配置 - 处理分页器的显示和功能
const paginationConfig = computed(() => {
  if (!props.pagination) return false
  return {
    ...props.pagination,
    showTotal: (total: number) => `共 ${total} 条`
  }
})

// 滚动配置 - 处理表格的滚动行为
const scrollConfig = computed(() => {
  const defaultScroll = { x: '100%', y: '400px' }
  if (!props.scroll) return defaultScroll

  if (props.dataSource.length > props.threshold) {
    return {
      ...props.scroll,
      ...defaultScroll,
      y: props.scroll.y || '400px'
    }
  }

  return {
    ...props.scroll,
    ...defaultScroll
  }
})

// === 事件处理 ===
// 表格变化事件处理 - 处理分页、排序、筛选等变化
const handleTableChange = (pagination: any, filters: any, sorter: any) => {
  emit('change', pagination, filters, sorter)
}

// 行点击事件处理
const handleRowClick = (record: any, index: number, event: Event) => {
  emit('row-click', record, index, event)
}

// === 虚拟滚动配置 ===
// 是否启用虚拟滚动 - 根据数据量自动判断
const enableVirtual = computed(() => {
  return props.enableVirtual || props.dataSource.length > props.threshold
})
</script>

<style lang="less">
.hbt-table-container {
  width: 100%;

  .ant-table {
    // 优化虚拟滚动性能
    .ant-table-body {
      overflow-y: auto;
      overflow-x: auto;
      
      // 自定义滚动条样式
      &::-webkit-scrollbar {
        width: 6px;
        height: 6px;
      }
      
      &::-webkit-scrollbar-thumb {
        background: rgba(0, 0, 0, 0.2);
        border-radius: 3px;
      }
      
      &::-webkit-scrollbar-track {
        background: rgba(0, 0, 0, 0.1);
      }
    }
  }
}
</style> 