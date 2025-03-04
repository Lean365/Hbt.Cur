<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: Operation/index.vue  
创建日期: 2024-03-20
描述: 通用操作按钮组件，提供常用的操作按钮
=================================================================== 
-->

<template>
  <div class="hbt-operation" :class="{ 'hbt-operation--vertical': direction === 'vertical' }">
    <!-- 基础操作按钮 -->
    <a-tooltip v-if="showView" :title="t('common.actions.view')">
      <a-button 
        :type="buttonType"
        :size="size"
        @click="handleView"
        class="hbt-btn-view"
      >
        <template #icon><eye-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showEdit" :title="t('common.actions.edit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEdit"
        class="hbt-btn-edit"
      >
        <template #icon><edit-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-popconfirm
      v-if="showDelete"
      :title="t('common.message.deleteConfirm')"
      @confirm="handleDelete"
    >
      <a-tooltip :title="t('common.actions.delete')">
        <a-button
          :type="buttonType"
          :size="size"
          danger
          class="hbt-btn-delete"
        >
          <template #icon><delete-outlined /></template>
        </a-button>
      </a-tooltip>
    </a-popconfirm>

    <a-tooltip v-if="showCopy" :title="t('common.actions.copy')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleCopy"
        class="hbt-btn-copy"
      >
        <template #icon><copy-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 工作流按钮组 -->
    <a-tooltip v-if="showStartFlow" :title="t('common.actions.startFlow')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleStartFlow"
        class="hbt-btn-start-flow"
      >
        <template #icon><play-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showEndFlow" :title="t('common.actions.endFlow')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEndFlow"
        class="hbt-btn-end-flow"
      >
        <template #icon><stop-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showAudit" :title="t('common.actions.audit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAudit"
        class="hbt-btn-audit"
      >
        <template #icon><audit-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showRevoke" :title="t('common.actions.revoke')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleRevoke"
        class="hbt-btn-revoke"
      >
        <template #icon><rollback-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 授权按钮组 -->
    <a-tooltip v-if="showAuthorize" :title="t('common.actions.authorize')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAuthorize"
        class="hbt-btn-authorize"
      >
        <template #icon><safety-certificate-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showAssignUser" :title="t('common.actions.assign')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAssignUser"
        class="hbt-btn-user"
      >
        <template #icon><user-add-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 代码生成按钮组 -->
    <a-tooltip v-if="showGenerate" :title="t('common.actions.generate')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleGenerate"
        class="hbt-btn-generate"
      >
        <template #icon><code-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showSync" :title="t('common.actions.sync')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSync"
        class="hbt-btn-sync"
      >
        <template #icon><sync-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 更多操作下拉菜单 -->
    <a-dropdown v-if="hasMoreActions">
      <a-tooltip :title="t('common.actions.more')">
        <a-button :type="buttonType" :size="size" class="hbt-btn-more">
          <template #icon><more-outlined /></template>
        </a-button>
      </a-tooltip>
      <template #overlay>
        <a-menu>
          <!-- 数据操作组 -->
          <a-menu-item-group :title="t('common.actions.dataOperations')">
            <a-menu-item v-if="showSort" @click="handleSort" class="hbt-btn-sort">
              <sort-ascending-outlined />{{ t('common.actions.sort') }}
            </a-menu-item>
            <a-menu-item v-if="showPreview" @click="handlePreview" class="hbt-btn-preview">
              <eye-outlined />{{ t('common.actions.preview') }}
            </a-menu-item>
          </a-menu-item-group>

          <!-- 工作流操作组 -->
          <a-menu-item-group :title="t('common.actions.workflowOperations')">
            <a-menu-item v-if="showTransfer" @click="handleTransfer" class="hbt-btn-transfer">
              <swap-outlined />{{ t('common.actions.transfer') }}
            </a-menu-item>
            <a-menu-item v-if="showDelegate" @click="handleDelegate" class="hbt-btn-delegate">
              <user-switch-outlined />{{ t('common.actions.delegate') }}
            </a-menu-item>
            <a-menu-item v-if="showUrge" @click="handleUrge" class="hbt-btn-urge">
              <clock-circle-outlined />{{ t('common.actions.urge') }}
            </a-menu-item>
          </a-menu-item-group>

          <!-- 系统操作组 -->
          <a-menu-item-group :title="t('common.actions.systemOperations')">
            <a-menu-item v-if="showExport" @click="handleExport" class="hbt-btn-export">
              <export-outlined />{{ t('common.actions.export') }}
            </a-menu-item>
            <a-menu-item v-if="showPrint" @click="handlePrint" class="hbt-btn-print">
              <printer-outlined />{{ t('common.actions.print') }}
            </a-menu-item>
            <a-menu-item v-if="showLog" @click="handleLog" class="hbt-btn-log">
              <file-text-outlined />{{ t('common.actions.log') }}
            </a-menu-item>
          </a-menu-item-group>
        </a-menu>
      </template>
    </a-dropdown>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  CopyOutlined,
  MoreOutlined,
  PlayCircleOutlined,
  StopOutlined,
  AuditOutlined,
  RollbackOutlined,
  SafetyCertificateOutlined,
  UserAddOutlined,
  CodeOutlined,
  SyncOutlined,
  SortAscendingOutlined,
  SwapOutlined,
  UserSwitchOutlined,
  ClockCircleOutlined,
  ExportOutlined,
  PrinterOutlined,
  FileTextOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

// === 类型定义 ===
interface Props {
  record?: any // 数据记录
  showView?: boolean // 是否显示查看按钮
  showEdit?: boolean // 是否显示编辑按钮
  showDelete?: boolean // 是否显示删除按钮
  showCopy?: boolean // 是否显示复制按钮
  showStartFlow?: boolean // 是否显示开始流程按钮
  showEndFlow?: boolean // 是否显示结束流程按钮
  showAudit?: boolean // 是否显示审核按钮
  showRevoke?: boolean // 是否显示撤销按钮
  showExport?: boolean // 是否显示导出按钮
  showPrint?: boolean // 是否显示打印按钮
  showLog?: boolean // 是否显示日志按钮
  showAuthorize?: boolean // 是否显示授权按钮
  showAssignUser?: boolean // 是否显示分配用户按钮
  showGenerate?: boolean // 是否显示代码生成按钮
  showSync?: boolean // 是否显示同步按钮
  showSort?: boolean // 是否显示排序按钮
  showPreview?: boolean // 是否显示预览按钮
  showTransfer?: boolean // 是否显示转移按钮
  showDelegate?: boolean // 是否显示委托按钮
  showUrge?: boolean // 是否显示催办按钮
  buttonType?: 'link' | 'text' | 'default' | 'primary' | 'dashed' // 按钮类型
  size?: 'small' | 'middle' | 'large' // 按钮大小
  showText?: boolean // 是否显示按钮文本
  direction?: 'horizontal' | 'vertical' // 按钮排列方向
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  record: undefined,
  showView: false,
  showEdit: false,
  showDelete: false,
  showCopy: false,
  showStartFlow: false,
  showEndFlow: false,
  showAudit: false,
  showRevoke: false,
  showExport: false,
  showPrint: false,
  showLog: false,
  showAuthorize: false,
  showAssignUser: false,
  showGenerate: false,
  showSync: false,
  showSort: false,
  showPreview: false,
  showTransfer: false,
  showDelegate: false,
  showUrge: false,
  buttonType: 'link',
  size: 'middle',
  showText: true,
  direction: 'horizontal'
})

// === 事件定义 ===
const emit = defineEmits([
  'view',
  'edit',
  'delete',
  'copy',
  'start-flow',
  'end-flow',
  'audit',
  'revoke',
  'transfer',
  'delegate',
  'urge',
  'authorize',
  'assign-user',
  'generate',
  'sync',
  'sort',
  'preview',
  'export',
  'print',
  'log'
])

// === 计算属性 ===
const hasMoreActions = computed(() => {
  return props.showSort ||
         props.showPreview ||
         props.showTransfer ||
         props.showDelegate ||
         props.showUrge ||
         props.showExport ||
         props.showPrint ||
         props.showLog
})

// === 事件处理 ===
const handleView = () => emit('view', props.record)
const handleEdit = () => emit('edit', props.record)
const handleDelete = () => emit('delete', props.record)
const handleCopy = () => emit('copy', props.record)
const handleStartFlow = () => emit('start-flow', props.record)
const handleEndFlow = () => emit('end-flow', props.record)
const handleAudit = () => emit('audit', props.record)
const handleRevoke = () => emit('revoke', props.record)
const handleTransfer = () => emit('transfer', props.record)
const handleDelegate = () => emit('delegate', props.record)
const handleUrge = () => emit('urge', props.record)
const handleAuthorize = () => emit('authorize', props.record)
const handleAssignUser = () => emit('assign-user', props.record)
const handleGenerate = () => emit('generate', props.record)
const handleSync = () => emit('sync', props.record)
const handleSort = () => emit('sort', props.record)
const handlePreview = () => emit('preview', props.record)
const handleExport = () => emit('export', props.record)
const handlePrint = () => emit('print', props.record)
const handleLog = () => emit('log', props.record)
</script>

<style lang="less" scoped>
.hbt-operation {
  display: inline-flex;
  gap: 8px;
  align-items: center;

  &--vertical {
    flex-direction: column;
    align-items: flex-start;
  }

  :deep(.ant-dropdown-trigger) {
    margin-left: 8px;
  }
}
</style> 