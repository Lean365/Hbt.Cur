<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: table/index.vue
创建日期: 2024-03-20
描述: 表格组件，提供数据展示、分页、排序、筛选等功能
=================================================================== 
-->

<template>
  <div class="hbt-table-container">
    <!-- 工具栏 -->
    <div class="hbt-table-toolbar">
      <!-- 左侧按钮组 -->
      <div class="toolbar-left">
        <a-space :size="4">
          <a-button v-if="showAdd" @click="handleAdd" class="hbt-btn-add">
            <template #icon><plus-outlined /></template>
            {{ t('common.actions.add') }}
          </a-button>
          <a-button v-if="showEdit" @click="handleEdit" class="hbt-btn-edit">
            <template #icon><edit-outlined /></template>
            {{ t('common.actions.edit') }}
          </a-button>
          <a-button v-if="showDelete" @click="handleDelete" class="hbt-btn-delete">
            <template #icon><delete-outlined /></template>
            {{ t('common.actions.delete') }}
          </a-button>
          <a-dropdown v-if="showImport" overlay-class-name="hbt-dropdown">
            <a-button class="hbt-btn-import">
              <template #icon><import-outlined /></template>
              {{ t('common.actions.import') }}
            </a-button>
            <template #overlay>
              <a-menu>
                <a-menu-item key="import" @click="handleImport" class="ant-dropdown-menu-item hbt-dropdown-import">
                  <template #icon><upload-outlined /></template>
                  {{ t('common.import.file') }}
                </a-menu-item>
                <a-menu-item key="template" @click="handleTemplate" class="ant-dropdown-menu-item hbt-dropdown-template">
                  <template #icon><file-outlined /></template>
                  {{ t('common.import.template') }}
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          <a-button v-if="showExport" @click="handleExport" class="hbt-btn-export">
            <template #icon><export-outlined /></template>
            {{ t('common.actions.export') }}
          </a-button>
          <a-button v-if="showAudit" @click="handleAudit" class="hbt-btn-audit">
            <template #icon><audit-outlined /></template>
            {{ t('common.actions.audit') }}
          </a-button>
          <a-button v-if="showRevoke" @click="handleRevoke" class="hbt-btn-revoke">
            <template #icon><rollback-outlined /></template>
            {{ t('common.actions.revoke') }}
          </a-button>
        </a-space>
      </div>

      <!-- 右侧按钮组 -->
      <div class="toolbar-right">
        <a-space :size="4">
          <a-button @click="handleRefresh" class="hbt-btn-refresh">
            <template #icon><reload-outlined /></template>
          </a-button>
          <a-button @click="handleColumnSetting" class="hbt-btn-column-setting">
            <template #icon><setting-outlined /></template>
          </a-button>
          <a-button @click="toggleSearch" class="hbt-btn-search">
            <template #icon><search-outlined /></template>
          </a-button>
          <a-button @click="toggleFullscreen" class="hbt-btn-fullscreen">
            <template #icon>
              <fullscreen-outlined v-if="!isFullscreen" />
              <fullscreen-exit-outlined v-else />
            </template>
          </a-button>
        </a-space>
      </div>
    </div>

    <!-- 表格主体 -->
    <a-table
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="false"
      :row-selection="rowSelection"
      :scroll="scroll"
      @change="handleTableChange"
      :bordered="bordered"
      :size="size"
    >
      <!-- 动态插槽渲染 -->
      <template #[item]="{ record, index, column }" v-for="item in Object.keys($slots)">
        <slot :name="item" :record="record" :index="index" :column="column"></slot>
      </template>
    </a-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-if="showPagination"
      :Current="current"
      :PageSize="pageSize"
      :Total="total"
      :ShowQuickJumper="showQuickJumper"
      :ShowSizeChanger="showSizeChanger"
      :Disabled="disabled"
      :Size="paginationSize"
      @PageChange="handlePageChange"
      @SizeChange="handleSizeChange"
    />
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { TableProps } from 'ant-design-vue'
import HbtPagination from '@/components/Business/Pagination/index.vue'
import {
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  ImportOutlined,
  ExportOutlined,
  AuditOutlined,
  RollbackOutlined,
  ReloadOutlined,
  SettingOutlined,
  SearchOutlined,
  FullscreenOutlined,
  FullscreenExitOutlined,
  UploadOutlined,
  FileOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

// === 类型定义 ===
interface Props {
  columns?: any[] // 表格列配置
  dataSource?: any[] // 表格数据源
  loading?: boolean // 加载状态
  showPagination?: boolean // 是否显示分页
  current?: number // 当前页码
  pageSize?: number // 每页条数
  total?: number // 总条数
  showQuickJumper?: boolean // 是否可以快速跳转至某页
  showSizeChanger?: boolean // 是否可以改变 pageSize
  disabled?: boolean // 是否禁用
  paginationSize?: 'small' | 'default' // 分页组件大小
  rowSelection?: TableProps['rowSelection'] // 行选择配置
  scroll?: TableProps['scroll'] // 滚动配置
  bordered?: boolean // 是否显示边框
  size?: 'small' | 'middle' | 'large' // 表格大小
  showAdd?: boolean // 是否显示新增按钮
  showEdit?: boolean // 是否显示编辑按钮
  showDelete?: boolean // 是否显示删除按钮
  showImport?: boolean // 是否显示导入按钮
  showExport?: boolean // 是否显示导出按钮
  showAudit?: boolean // 是否显示审核按钮
  showRevoke?: boolean // 是否显示撤消按钮
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  columns: () => [],
  dataSource: () => [],
  loading: false,
  showPagination: true,
  current: 1,
  pageSize: 10,
  total: 0,
  showQuickJumper: true,
  showSizeChanger: true,
  disabled: false,
  paginationSize: 'default',
  rowSelection: undefined,
  scroll: undefined,
  bordered: false,
  size: 'large',
  showAdd: true,
  showEdit: true,
  showDelete: true,
  showImport: true,
  showExport: true,
  showAudit: true,
  showRevoke: true
})

// === 事件定义 ===
const emit = defineEmits([
  'change',
  'update:selectedRowKeys',
  'update:current',
  'update:pageSize',
  'add',
  'edit',
  'delete',
  'import',
  'template',
  'export',
  'audit',
  'revoke',
  'refresh',
  'column-setting',
  'toggle-search',
  'toggle-fullscreen'
])

// === 工具栏事件处理 ===
const handleAdd = () => emit('add')
const handleEdit = () => emit('edit')
const handleDelete = () => emit('delete')
const handleImport = () => emit('import')
const handleTemplate = () => emit('template')
const handleExport = () => emit('export')
const handleAudit = () => emit('audit')
const handleRevoke = () => emit('revoke')
const handleRefresh = () => emit('refresh')
const handleColumnSetting = () => emit('column-setting')

// === 搜索和全屏控制 ===
const showSearch = ref(true)
const isFullscreen = ref(false)

const toggleSearch = () => {
  showSearch.value = !showSearch.value
  emit('toggle-search', showSearch.value)
}

const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
  emit('toggle-fullscreen', isFullscreen.value)
}

// === 表格事件处理 ===
// 表格变化事件处理
const handleTableChange = (_: any, filters: any, sorter: any) => {
  emit('change', { current: props.current, pageSize: props.pageSize, filters, sorter })
}

// 页码变化事件处理
const handlePageChange = (page: number, size: number) => {
  emit('update:current', page)
  emit('change', { current: page, pageSize: size })
}

// 每页条数变化事件处理
const handleSizeChange = (size: number, current: number) => {
  emit('update:pageSize', size)
  emit('change', { current, pageSize: size })
}

// === 行选择相关 ===
// 选中行的 key 数组
const selectedRowKeys = ref<(string | number)[]>([])

// 监听选中行变化
watch(
  () => selectedRowKeys.value,
  (newVal) => {
    emit('update:selectedRowKeys', newVal)
  }
)

// 计算行选择配置
const rowSelection = computed(() => {
  if (!props.rowSelection) return undefined
  return {
    ...props.rowSelection,
    selectedRowKeys: selectedRowKeys.value,
    onChange: (keys: (string | number)[]) => {
      selectedRowKeys.value = keys
    }
  }
})
</script>

<style lang="less">
@import '@/assets/styles/components/button.less';
@import '@/assets/styles/components/dropdown.less';

.hbt-table-container {
  // 表格工具栏布局
  .hbt-table-toolbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: @padding-md 6;
    margin-bottom: @margin-md;

    // 左侧按钮组
    .toolbar-left {
      display: inline-flex;
      align-items: center;

      .ant-space {
        display: inline-flex;
        flex-wrap: nowrap;
        white-space: nowrap;
        gap: 4px;
      }
    }

    // 右侧按钮组
    .toolbar-right {
      margin-left: auto;

      .ant-space {
        display: inline-flex;
        flex-wrap: nowrap;
        white-space: nowrap;
        gap: 4px;
      }
    }

    // 修复按钮组最后一个按钮的右边框
    .ant-space .ant-btn:last-child {
      margin-right: 0;
    }
  }

  // 表格操作列布局
  .hbt-table-operation {
    display: flex;
    gap: 8px;
  }

  // 表格设置下拉菜单布局
  .hbt-table-setting-dropdown {
    &:extend(.ant-dropdown-menu all);
    min-width: 160px;

    .ant-dropdown-menu-item {
      display: flex;
      align-items: center;
      
      .anticon {
        margin-right: 8px;
      }
    }
  }
}
</style> 