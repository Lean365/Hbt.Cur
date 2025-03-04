<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: Toolbar/index.vue
创建日期: 2024-03-20
描述: 统一封装的工具栏组件，提供常用的操作按钮和工具按钮
=================================================================== 
-->

<template>
  <div class="hbt-toolbar">
    <!-- 左侧按钮组 -->
    <div class="toolbar-left">
      <a-space :size="4">
        <div v-if="showAdd" v-hasPermi="addPermission">
          <a-tooltip :title="t('common.actions.add')">
            <a-button @click="handleAdd" class="hbt-btn-add">
              <template #icon><plus-outlined /></template>
              {{ t('common.actions.add') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showEdit" v-hasPermi="editPermission">
          <a-tooltip :title="t('common.actions.edit')">
            <a-button 
              @click="handleEdit" 
              class="hbt-btn-edit"
              :disabled="disabledEdit"
            >
              <template #icon><edit-outlined /></template>
              {{ t('common.actions.edit') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showDelete" v-hasPermi="deletePermission">
          <a-tooltip :title="t('common.actions.delete')">
            <a-button 
              @click="handleDelete" 
              class="hbt-btn-delete"
              :disabled="disabledDelete"
            >
              <template #icon><delete-outlined /></template>
              {{ t('common.actions.delete') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showImport" v-hasPermi="importPermission">
          <a-dropdown overlay-class-name="hbt-dropdown">
            <a-tooltip :title="t('common.actions.import')">
              <a-button class="hbt-btn-import">
                <template #icon><import-outlined /></template>
                {{ t('common.actions.import') }}
              </a-button>
            </a-tooltip>
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
        </div>
        <div v-if="showExport" v-hasPermi="exportPermission">
          <a-tooltip :title="t('common.actions.export')">
            <a-button @click="handleExport" class="hbt-btn-export">
              <template #icon><export-outlined /></template>
              {{ t('common.actions.export') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showAudit" v-hasPermi="auditPermission">
          <a-tooltip :title="t('common.actions.audit')">
            <a-button @click="handleAudit" class="hbt-btn-audit">
              <template #icon><audit-outlined /></template>
              {{ t('common.actions.audit') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showRevoke" v-hasPermi="revokePermission">
          <a-tooltip :title="t('common.actions.revoke')">
            <a-button @click="handleRevoke" class="hbt-btn-revoke">
              <template #icon><rollback-outlined /></template>
              {{ t('common.actions.revoke') }}
            </a-button>
          </a-tooltip>
        </div>
        <!-- 自定义按钮插槽 -->
        <slot v-if="false" name="buttons"></slot>
      </a-space>
    </div>

    <!-- 右侧按钮组 -->
    <div class="toolbar-right">
      <a-space :size="4">
        <a-tooltip :title="t('common.actions.refresh')">
          <a-button @click="handleRefresh" class="hbt-btn-refresh">
            <template #icon><reload-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="t('table.config.columnSetting')">
          <a-button @click="handleColumnSetting" class="hbt-btn-column-setting">
            <template #icon><setting-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="t('common.actions.search')">
          <a-button @click="toggleSearch" class="hbt-btn-search">
            <template #icon><search-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="isFullscreen ? t('header.fullscreen.exit') : t('header.fullscreen.enter')">
          <a-button @click="toggleFullscreen" class="hbt-btn-fullscreen">
            <template #icon>
              <fullscreen-outlined v-if="!isFullscreen" />
              <fullscreen-exit-outlined v-else />
            </template>
          </a-button>
        </a-tooltip>
        <!-- 自定义工具插槽 -->
        <slot v-if="false" name="buttons"></slot>
      </a-space>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
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
  showAdd?: boolean // 是否显示新增按钮
  addPermission?: string[] // 新增按钮权限
  showEdit?: boolean // 是否显示编辑按钮
  editPermission?: string[] // 编辑按钮权限
  showDelete?: boolean // 是否显示删除按钮
  deletePermission?: string[] // 删除按钮权限
  showImport?: boolean // 是否显示导入按钮
  importPermission?: string[] // 导入按钮权限
  showExport?: boolean // 是否显示导出按钮
  exportPermission?: string[] // 导出按钮权限
  showAudit?: boolean // 是否显示审核按钮
  auditPermission?: string[] // 审核按钮权限
  showRevoke?: boolean // 是否显示撤销按钮
  revokePermission?: string[] // 撤销按钮权限
  disabledEdit?: boolean // 是否禁用编辑按钮
  disabledDelete?: boolean // 是否禁用删除按钮
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  showAdd: false,
  addPermission: () => [],
  showEdit: false,
  editPermission: () => [],
  showDelete: false,
  deletePermission: () => [],
  showImport: false,
  importPermission: () => [],
  showExport: false,
  exportPermission: () => [],
  showAudit: false,
  auditPermission: () => [],
  showRevoke: false,
  revokePermission: () => [],
  disabledEdit: false,
  disabledDelete: false
})

// === 事件定义 ===
const emit = defineEmits([
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

// === 状态管理 ===
const isFullscreen = ref(false)

// === 事件处理 ===
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

const toggleSearch = () => {
  emit('toggle-search')
}

const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
  emit('toggle-fullscreen', isFullscreen.value)
}
</script>

<style lang="less">
@import '@/assets/styles/components/button.less';
@import '@/assets/styles/components/dropdown.less';

.hbt-toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px 6px;
  margin-bottom: 16px;
  //background-color: #fff;
  border-radius: 2px;

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
</style> 