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
    <!-- 基础操作按钮组 -->
    <a-tooltip v-if="showSaveButton" :title="t('common.actions.save')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSave"
        class="hbt-btn-save"
      >
        <template #icon><save-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showViewButton" :title="t('common.actions.view')">
      <a-button 
        :type="buttonType"
        :size="size"
        @click="handleView"
        class="hbt-btn-view"
      >
        <template #icon><eye-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showEditButton" :title="t('common.actions.edit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEdit"
        class="hbt-btn-edit"
      >
        <template #icon><edit-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showImportButton" :title="t('common.actions.import')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleImport"
        class="hbt-btn-import"
      >
        <template #icon><import-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showExportButton" :title="t('common.actions.export')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleExport"
        class="hbt-btn-export"
      >
        <template #icon><export-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-popconfirm
      v-if="showDeleteButton"
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

    <a-tooltip v-if="showCopyButton" :title="t('common.actions.copy')">
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
    <a-tooltip v-if="showStartFlowButton" :title="t('common.actions.startFlow')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleStartFlow"
        class="hbt-btn-start-flow"
      >
        <template #icon><play-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showEndFlowButton" :title="t('common.actions.endFlow')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEndFlow"
        class="hbt-btn-end-flow"
      >
        <template #icon><stop-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showAuditButton" :title="t('common.actions.audit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAudit"
        class="hbt-btn-audit"
      >
        <template #icon><audit-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showRevokeButton" :title="t('common.actions.revoke')">
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
    <a-tooltip v-if="showAuthorizeButton" :title="t('common.actions.authorize')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAuthorize"
        class="hbt-btn-authorize"
      >
        <template #icon><safety-certificate-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showAssignUserButton" :title="t('common.actions.assign')">
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
    <a-tooltip v-if="showGenerateButton" :title="t('common.actions.generate')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleGenerate"
        class="hbt-btn-generate"
      >
        <template #icon><code-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showSyncButton" :title="t('common.actions.sync')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSync"
        class="hbt-btn-sync"
      >
        <template #icon><sync-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 初始化按钮组 -->
    <a-tooltip v-if="showInitializeButton" :title="t('common.actions.initialize')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleInitialize"
        class="hbt-btn-initialize"
      >
        <template #icon><thunderbolt-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 开始按钮组 -->
    <a-tooltip v-if="showStartButton" :title="t('common.actions.start')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleStart"
        class="hbt-btn-start"
      >
        <template #icon><play-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 结束按钮组 -->
    <a-tooltip v-if="showEndButton" :title="t('common.actions.end')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEnd"
        class="hbt-btn-end"
      >
        <template #icon><stop-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 运行按钮组 -->
    <a-tooltip v-if="showRunButton" :title="t('common.actions.run')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleRun"
        class="hbt-btn-run"
      >
        <template #icon><thunderbolt-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 停止按钮组 -->
    <a-tooltip v-if="showStopButton" :title="t('common.actions.stop')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleStop"
        class="hbt-btn-stop"
      >
        <template #icon><pause-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 更多操作按钮组 -->
    <a-tooltip v-if="showSortButton" :title="t('common.actions.sort')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSort"
        class="hbt-btn-sort"
      >
        <template #icon><sort-ascending-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showPreviewButton" :title="t('common.actions.preview')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handlePreview"
        class="hbt-btn-preview"
      >
        <template #icon><eye-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showTransferButton" :title="t('common.actions.transfer')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleTransfer"
        class="hbt-btn-transfer"
      >
        <template #icon><swap-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showDelegateButton" :title="t('common.actions.delegate')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleDelegate"
        class="hbt-btn-delegate"
      >
        <template #icon><user-switch-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showUrgeButton" :title="t('common.actions.urge')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleUrge"
        class="hbt-btn-urge"
      >
        <template #icon><clock-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showPrintButton" :title="t('common.actions.print')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handlePrint"
        class="hbt-btn-print"
      >
        <template #icon><printer-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showLogButton" :title="t('common.actions.log')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleLog"
        class="hbt-btn-log"
      >
        <template #icon><file-text-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 在线管理按钮组 -->
    <a-tooltip v-if="showOnlineStatusButton" :title="t('common.actions.onlineStatus')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleOnlineStatus"
        class="hbt-btn-online-status"
      >
        <template #icon><desktop-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showLoginHistoryButton" :title="t('common.actions.loginHistory')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleLoginHistory"
        class="hbt-btn-login-history"
      >
        <template #icon><history-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-popconfirm
      v-if="showForceOfflineButton"
      :title="t('common.message.forceOfflineConfirm')"
      @confirm="handleForceOffline"
    >
      <a-tooltip :title="t('common.actions.forceOffline')">
        <a-button
          :type="buttonType"
          :size="size"
          danger
          class="hbt-btn-force-offline"
        >
          <template #icon><logout-outlined /></template>
        </a-button>
      </a-tooltip>
    </a-popconfirm>

    <!-- 邮件操作按钮组 -->
    <a-tooltip v-if="showSendMailButton" :title="t('common.actions.sendMail')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSendMail"
        class="hbt-btn-send-mail"
      >
        <template #icon><mail-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showViewMailButton" :title="t('common.actions.viewMail')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleViewMail"
        class="hbt-btn-view-mail"
      >
        <template #icon><inbox-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showMailTemplateButton" :title="t('common.actions.mailTemplate')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleMailTemplate"
        class="hbt-btn-mail-template"
      >
        <template #icon><file-text-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 通知操作按钮组 -->
    <a-tooltip v-if="showSendNotificationButton" :title="t('common.actions.sendNotification')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSendNotification"
        class="hbt-btn-send-notification"
      >
        <template #icon><bell-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showViewNotificationButton" :title="t('common.actions.viewNotification')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleViewNotification"
        class="hbt-btn-view-notification"
      >
        <template #icon><notification-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showNotificationSettingButton" :title="t('common.actions.notificationSetting')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleNotificationSetting"
        class="hbt-btn-notification-setting"
      >
        <template #icon><setting-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 消息操作按钮组 -->
    <a-tooltip v-if="showSendMessageButton" :title="t('common.actions.sendMessage')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSendMessage"
        class="hbt-btn-send-message"
      >
        <template #icon><message-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showViewMessageButton" :title="t('common.actions.viewMessage')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleViewMessage"
        class="hbt-btn-view-message"
      >
        <template #icon><send-outlined /></template>
      </a-button>
    </a-tooltip>

    <a-tooltip v-if="showMessageSettingButton" :title="t('common.actions.messageSetting')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleMessageSetting"
        class="hbt-btn-message-setting"
      >
        <template #icon><setting-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 支持自定义按钮插槽 -->
    <slot name="extra"></slot>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/user'
import {
  SaveOutlined,
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
  ImportOutlined,
  PrinterOutlined,
  FileTextOutlined,
  LogoutOutlined,
  DesktopOutlined,
  HistoryOutlined,
  MailOutlined,
  InboxOutlined,
  BellOutlined,
  NotificationOutlined,
  SettingOutlined,
  MessageOutlined,
  SendOutlined,
  ReloadOutlined,
  ThunderboltOutlined,
  PauseCircleOutlined
} from '@ant-design/icons-vue'


const { t } = useI18n()

// === 类型定义 ===
interface Props {
  record?: any
  showSave?: boolean
  savePermission?: string[]
  showView?: boolean
  viewPermission?: string[]
  showEdit?: boolean
  editPermission?: string[]
  showDelete?: boolean
  deletePermission?: string[]
  showCopy?: boolean
  copyPermission?: string[]
  showImport?: boolean
  importPermission?: string[]
  showExport?: boolean
  exportPermission?: string[]
  showStartFlow?: boolean
  startFlowPermission?: string[]
  showEndFlow?: boolean
  endFlowPermission?: string[]
  showAudit?: boolean
  auditPermission?: string[]
  showRevoke?: boolean
  revokePermission?: string[]
  showPrint?: boolean
  printPermission?: string[]
  showLog?: boolean
  logPermission?: string[]
  showAuthorize?: boolean
  authorizePermission?: string[]
  showAssignUser?: boolean
  assignUserPermission?: string[]
  showGenerate?: boolean
  generatePermission?: string[]
  showSync?: boolean
  syncPermission?: string[]
  showSort?: boolean
  sortPermission?: string[]
  showPreview?: boolean
  previewPermission?: string[]
  showTransfer?: boolean
  transferPermission?: string[]
  showDelegate?: boolean
  delegatePermission?: string[]
  showUrge?: boolean
  urgePermission?: string[]
  showForceOffline?: boolean
  forceOfflinePermission?: string[]
  showOnlineStatus?: boolean
  onlineStatusPermission?: string[]
  showLoginHistory?: boolean
  loginHistoryPermission?: string[]
  showSendMail?: boolean
  sendMailPermission?: string[]
  showViewMail?: boolean
  viewMailPermission?: string[]
  showMailTemplate?: boolean
  mailTemplatePermission?: string[]
  showSendNotification?: boolean
  sendNotificationPermission?: string[]
  showViewNotification?: boolean
  viewNotificationPermission?: string[]
  showNotificationSetting?: boolean
  notificationSettingPermission?: string[]
  showSendMessage?: boolean
  sendMessagePermission?: string[]
  showViewMessage?: boolean
  viewMessagePermission?: string[]
  showMessageSetting?: boolean
  messageSettingPermission?: string[]
  buttonType?: 'link' | 'text' | 'default' | 'primary' | 'dashed'
  size?: 'small' | 'middle' | 'large'
  showText?: boolean
  direction?: 'horizontal' | 'vertical'
  showInitialize?: boolean
  initializePermission?: string[]
  showStart?: boolean
  startPermission?: string[]
  showEnd?: boolean
  endPermission?: string[]
  showRun?: boolean
  runPermission?: string[]
  showStop?: boolean
  stopPermission?: string[]
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  record: undefined,
  showSave: false,
  savePermission: () => [],
  showView: false,
  viewPermission: () => [],
  showEdit: false,
  editPermission: () => [],
  showDelete: false,
  deletePermission: () => [],
  showCopy: false,
  copyPermission: () => [],
  showImport: false,
  importPermission: () => [],
  showExport: false,
  exportPermission: () => [],
  showStartFlow: false,
  startFlowPermission: () => [],
  showEndFlow: false,
  endFlowPermission: () => [],
  showAudit: false,
  auditPermission: () => [],
  showRevoke: false,
  revokePermission: () => [],
  showPrint: false,
  printPermission: () => [],
  showLog: false,
  logPermission: () => [],
  showAuthorize: false,
  authorizePermission: () => [],
  showAssignUser: false,
  assignUserPermission: () => [],
  showGenerate: false,
  generatePermission: () => [],
  showSync: false,
  syncPermission: () => [],
  showSort: false,
  sortPermission: () => [],
  showPreview: false,
  previewPermission: () => [],
  showTransfer: false,
  transferPermission: () => [],
  showDelegate: false,
  delegatePermission: () => [],
  showUrge: false,
  urgePermission: () => [],
  showForceOffline: false,
  forceOfflinePermission: () => [],
  showOnlineStatus: false,
  onlineStatusPermission: () => [],
  showLoginHistory: false,
  loginHistoryPermission: () => [],
  showSendMail: false,
  sendMailPermission: () => [],
  showViewMail: false,
  viewMailPermission: () => [],
  showMailTemplate: false,
  mailTemplatePermission: () => [],
  showSendNotification: false,
  sendNotificationPermission: () => [],
  showViewNotification: false,
  viewNotificationPermission: () => [],
  showNotificationSetting: false,
  notificationSettingPermission: () => [],
  showSendMessage: false,
  sendMessagePermission: () => [],
  showViewMessage: false,
  viewMessagePermission: () => [],
  showMessageSetting: false,
  messageSettingPermission: () => [],
  buttonType: 'link',
  size: 'middle',
  showText: true,
  direction: 'horizontal',
  showInitialize: false,
  initializePermission: () => [],
  showStart: false,
  startPermission: () => [],
  showEnd: false,
  endPermission: () => [],
  showRun: false,
  runPermission: () => [],
  showStop: false,
  stopPermission: () => []
})

// === 事件定义 ===
const emit = defineEmits([
  'save',
  'view',
  'edit',
  'delete',
  'copy',
  'import',
  'export',
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
  'print',
  'log',
  'force-offline',
  'online-status',
  'login-history',
  'send-mail',
  'view-mail',
  'mail-template',
  'send-notification',
  'view-notification',
  'notification-setting',
  'send-message',
  'view-message',
  'message-setting',
  'initialize',
  'start',
  'end',
  'run',
  'stop'
])

// === 权限验证方法 ===
const userStore = useUserStore()

// === 计算属性：根据权限判断是否显示按钮 ===
const showSaveButton = computed(() => {
  if (!props.showSave) return false
  if (!props.savePermission?.length) return false
  return props.savePermission.some(permission => userStore.permissions.includes(permission))
})

const showViewButton = computed(() => {
  if (!props.showView) return false
  if (!props.viewPermission?.length) return false
  return props.viewPermission.some(permission => userStore.permissions.includes(permission))
})

const showEditButton = computed(() => {
  if (!props.showEdit) return false
  if (!props.editPermission?.length) return false
  return props.editPermission.some(permission => userStore.permissions.includes(permission))
})

const showDeleteButton = computed(() => {
  if (!props.showDelete) return false
  if (!props.deletePermission?.length) return false
  return props.deletePermission.some(permission => userStore.permissions.includes(permission))
})

const showCopyButton = computed(() => {
  if (!props.showCopy) return false
  if (!props.copyPermission?.length) return false
  return props.copyPermission.some(permission => userStore.permissions.includes(permission))
})

const showImportButton = computed(() => {
  if (!props.showImport) return false
  if (!props.importPermission?.length) return false
  return props.importPermission.some(permission => userStore.permissions.includes(permission))
})

const showExportButton = computed(() => {
  if (!props.showExport) return false
  if (!props.exportPermission?.length) return false
  return props.exportPermission.some(permission => userStore.permissions.includes(permission))
})

const showStartFlowButton = computed(() => {
  if (!props.showStartFlow) return false
  if (!props.startFlowPermission?.length) return false
  return props.startFlowPermission.some(permission => userStore.permissions.includes(permission))
})

const showEndFlowButton = computed(() => {
  if (!props.showEndFlow) return false
  if (!props.endFlowPermission?.length) return false
  return props.endFlowPermission.some(permission => userStore.permissions.includes(permission))
})

const showAuditButton = computed(() => {
  if (!props.showAudit) return false
  if (!props.auditPermission?.length) return false
  return props.auditPermission.some(permission => userStore.permissions.includes(permission))
})

const showRevokeButton = computed(() => {
  if (!props.showRevoke) return false
  if (!props.revokePermission?.length) return false
  return props.revokePermission.some(permission => userStore.permissions.includes(permission))
})

const showPrintButton = computed(() => {
  if (!props.showPrint) return false
  if (!props.printPermission?.length) return false
  return props.printPermission.some(permission => userStore.permissions.includes(permission))
})

const showLogButton = computed(() => {
  if (!props.showLog) return false
  if (!props.logPermission?.length) return false
  return props.logPermission.some(permission => userStore.permissions.includes(permission))
})

const showAuthorizeButton = computed(() => {
  if (!props.showAuthorize) return false
  if (!props.authorizePermission?.length) return false
  return props.authorizePermission.some(permission => userStore.permissions.includes(permission))
})

const showAssignUserButton = computed(() => {
  if (!props.showAssignUser) return false
  if (!props.assignUserPermission?.length) return false
  return props.assignUserPermission.some(permission => userStore.permissions.includes(permission))
})

const showGenerateButton = computed(() => {
  if (!props.showGenerate) return false
  if (!props.generatePermission?.length) return false
  return props.generatePermission.some(permission => userStore.permissions.includes(permission))
})

const showSyncButton = computed(() => {
  if (!props.showSync) return false
  if (!props.syncPermission?.length) return false
  return props.syncPermission.some(permission => userStore.permissions.includes(permission))
})

const showSortButton = computed(() => {
  if (!props.showSort) return false
  if (!props.sortPermission?.length) return false
  return props.sortPermission.some(permission => userStore.permissions.includes(permission))
})

const showPreviewButton = computed(() => {
  if (!props.showPreview) return false
  if (!props.previewPermission?.length) return false
  return props.previewPermission.some(permission => userStore.permissions.includes(permission))
})

const showTransferButton = computed(() => {
  if (!props.showTransfer) return false
  if (!props.transferPermission?.length) return false
  return props.transferPermission.some(permission => userStore.permissions.includes(permission))
})

const showDelegateButton = computed(() => {
  if (!props.showDelegate) return false
  if (!props.delegatePermission?.length) return false
  return props.delegatePermission.some(permission => userStore.permissions.includes(permission))
})

const showUrgeButton = computed(() => {
  if (!props.showUrge) return false
  if (!props.urgePermission?.length) return false
  return props.urgePermission.some(permission => userStore.permissions.includes(permission))
})

const showForceOfflineButton = computed(() => {
  if (!props.showForceOffline) return false
  if (!props.forceOfflinePermission?.length) return false
  return props.forceOfflinePermission.some(permission => userStore.permissions.includes(permission))
})

const showOnlineStatusButton = computed(() => {
  if (!props.showOnlineStatus) return false
  if (!props.onlineStatusPermission?.length) return false
  return props.onlineStatusPermission.some(permission => userStore.permissions.includes(permission))
})

const showLoginHistoryButton = computed(() => {
  if (!props.showLoginHistory) return false
  if (!props.loginHistoryPermission?.length) return false
  return props.loginHistoryPermission.some(permission => userStore.permissions.includes(permission))
})

const showSendMailButton = computed(() => {
  if (!props.showSendMail) return false
  if (!props.sendMailPermission?.length) return false
  return props.sendMailPermission.some(permission => userStore.permissions.includes(permission))
})

const showViewMailButton = computed(() => {
  if (!props.showViewMail) return false
  if (!props.viewMailPermission?.length) return false
  return props.viewMailPermission.some(permission => userStore.permissions.includes(permission))
})

const showMailTemplateButton = computed(() => {
  if (!props.showMailTemplate) return false
  if (!props.mailTemplatePermission?.length) return false
  return props.mailTemplatePermission.some(permission => userStore.permissions.includes(permission))
})

const showSendNotificationButton = computed(() => {
  if (!props.showSendNotification) return false
  if (!props.sendNotificationPermission?.length) return false
  return props.sendNotificationPermission.some(permission => userStore.permissions.includes(permission))
})

const showViewNotificationButton = computed(() => {
  if (!props.showViewNotification) return false
  if (!props.viewNotificationPermission?.length) return false
  return props.viewNotificationPermission.some(permission => userStore.permissions.includes(permission))
})

const showNotificationSettingButton = computed(() => {
  if (!props.showNotificationSetting) return false
  if (!props.notificationSettingPermission?.length) return false
  return props.notificationSettingPermission.some(permission => userStore.permissions.includes(permission))
})

const showSendMessageButton = computed(() => {
  if (!props.showSendMessage) return false
  if (!props.sendMessagePermission?.length) return false
  return props.sendMessagePermission.some(permission => userStore.permissions.includes(permission))
})

const showViewMessageButton = computed(() => {
  if (!props.showViewMessage) return false
  if (!props.viewMessagePermission?.length) return false
  return props.viewMessagePermission.some(permission => userStore.permissions.includes(permission))
})

const showMessageSettingButton = computed(() => {
  if (!props.showMessageSetting) return false
  if (!props.messageSettingPermission?.length) return false
  return props.messageSettingPermission.some(permission => userStore.permissions.includes(permission))
})

const showInitializeButton = computed(() => {
  if (!props.showInitialize) return false
  if (!props.initializePermission?.length) return false
  return props.initializePermission.some(permission => userStore.permissions.includes(permission))
})

const showStartButton = computed(() => {
  if (!props.showStart) return false
  if (!props.startPermission?.length) return false
  return props.startPermission.some(permission => userStore.permissions.includes(permission))
})

const showEndButton = computed(() => {
  if (!props.showEnd) return false
  if (!props.endPermission?.length) return false
  return props.endPermission.some(permission => userStore.permissions.includes(permission))
})

const showRunButton = computed(() => {
  if (!props.showRun) return false
  if (!props.runPermission?.length) return false
  return props.runPermission.some(permission => userStore.permissions.includes(permission))
})

const showStopButton = computed(() => {
  if (!props.showStop) return false
  if (!props.stopPermission?.length) return false
  return props.stopPermission.some(permission => userStore.permissions.includes(permission))
})

// === 事件处理 ===
const handleSave = () => emit('save', props.record)
const handleView = () => emit('view', props.record)
const handleEdit = () => emit('edit', props.record)
const handleDelete = () => emit('delete', props.record)
const handleCopy = () => emit('copy', props.record)
const handleImport = () => emit('import', props.record)
const handleExport = () => emit('export', props.record)
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
const handlePrint = () => emit('print', props.record)
const handleLog = () => emit('log', props.record)
const handleForceOffline = () => emit('force-offline', props.record)
const handleOnlineStatus = () => emit('online-status', props.record)
const handleLoginHistory = () => emit('login-history', props.record)
const handleSendMail = () => emit('send-mail', props.record)
const handleViewMail = () => emit('view-mail', props.record)
const handleMailTemplate = () => emit('mail-template', props.record)
const handleSendNotification = () => emit('send-notification', props.record)
const handleViewNotification = () => emit('view-notification', props.record)
const handleNotificationSetting = () => emit('notification-setting', props.record)
const handleSendMessage = () => emit('send-message', props.record)
const handleViewMessage = () => emit('view-message', props.record)
const handleMessageSetting = () => emit('message-setting', props.record)
const handleInitialize = () => emit('initialize', props.record)
const handleStart = () => emit('start', props.record)
const handleEnd = () => emit('end', props.record)
const handleRun = () => emit('run', props.record)
const handleStop = () => emit('stop', props.record)
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