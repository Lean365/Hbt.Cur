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
    <!-- 开始按钮：启动流程或任务 -->
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

    <!-- 暂停按钮：暂停流程或任务执行 -->
    <a-tooltip v-if="showPauseButton" :title="t('common.actions.pause')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handlePause"
        class="hbt-btn-pause"
      >
        <template #icon><pause-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 恢复按钮：恢复暂停的流程或任务 -->
    <a-tooltip v-if="showResumeButton" :title="t('common.actions.resume')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleResume"
        class="hbt-btn-resume"
      >
        <template #icon><caret-right-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 强制结束按钮：强制终止流程或任务 -->
    <a-popconfirm
      v-if="showForceButton"
      :title="t('common.message.forceConfirm')"
      @confirm="handleForce"
    >
      <a-tooltip :title="t('common.actions.force')">
        <a-button
          :type="buttonType"
          :size="size"
          danger
          class="hbt-btn-force"
        >
          <template #icon><stop-outlined /></template>
        </a-button>
      </a-tooltip>
    </a-popconfirm>

    <!-- 审核按钮：审核文件或数据 -->
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

    <!-- 审批按钮：审批工作流任务 -->
    <a-tooltip v-if="showApproveButton" :title="t('common.actions.approve')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleApprove"
        class="hbt-btn-approve"
      >
        <template #icon><check-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 拒绝按钮：拒绝工作流任务 -->
    <a-tooltip v-if="showRejectButton" :title="t('common.actions.reject')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleReject"
        class="hbt-btn-reject"
      >
        <template #icon><close-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 撤销按钮：撤销已提交的操作 -->
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

    <!-- 授权按钮：授权用户权限 -->
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

    <!-- 分配用户按钮：分配用户到角色或部门 -->
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

    <!-- 代码生成按钮：生成代码文件 -->
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

    <!-- 同步按钮：同步数据 -->
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

    <!-- 初始化按钮：初始化系统或数据 -->
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

    <!-- 结束按钮：结束流程或任务 -->
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

    <!-- 运行按钮：运行任务或脚本 -->
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

    <!-- 停止按钮：停止运行中的任务 -->
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

    <!-- 排序按钮：对数据进行排序 -->
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

    <!-- 预览按钮：预览文件或数据 -->
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

    <!-- 转办按钮：转办工作流任务 -->
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

    <!-- 委托按钮：委托工作流任务 -->
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

    <!-- 催办按钮：催办工作流任务 -->
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

    <!-- 打印按钮：打印文件或数据 -->
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

    <!-- 日志按钮：查看操作日志 -->
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

    <!-- 在线状态按钮：查看用户在线状态 -->
    <a-tooltip v-if="showStatusButton" :title="t('common.actions.status')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleStatus"
        class="hbt-btn-status"
      >
        <template #icon><desktop-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 历史记录按钮：查看历史记录 -->
    <a-tooltip v-if="showHistoryButton" :title="t('common.actions.history')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleHistory"
        class="hbt-btn-history"
      >
        <template #icon><history-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 发送按钮：发送邮件或消息 -->
    <a-tooltip v-if="showSendButton" :title="t('common.actions.send')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSend"
        class="hbt-btn-send"
      >
        <template #icon><mail-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 模板按钮：管理邮件或消息模板 -->
    <a-tooltip v-if="showTemplateButton" :title="t('common.actions.template')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleTemplate"
        class="hbt-btn-template"
      >
        <template #icon><file-text-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 挂起按钮：挂起任务或流程 -->
    <a-tooltip v-if="showSuspendButton" :title="t('common.actions.suspend')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSuspend"
        class="hbt-btn-suspend"
      >
        <template #icon><pause-circle-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 设置按钮 -->
    <a-tooltip v-if="showSettingButton" :title="t('common.actions.setting')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSetting"
        class="hbt-btn-setting"
      >
        <template #icon><setting-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 发布按钮 -->
    <a-tooltip v-if="showPublishButton" :title="t('common.actions.publish')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handlePublish"
        class="hbt-btn-publish"
      >
        <template #icon><upload-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 撤回按钮 -->
    <a-tooltip v-if="showWithdrawButton" :title="t('common.actions.withdraw')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleWithdraw"
        class="hbt-btn-withdraw"
      >
        <template #icon><rollback-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 点赞按钮 -->
    <a-tooltip v-if="showLikeButton" :title="t('common.actions.like')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleLike"
        class="hbt-btn-like"
      >
        <template #icon><like-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 收藏按钮 -->
    <a-tooltip v-if="showFavoriteButton" :title="t('common.actions.favorite')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleFavorite"
        class="hbt-btn-favorite"
      >
        <template #icon><star-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 已读按钮 -->
    <a-tooltip v-if="showReadButton" :title="t('common.actions.read')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleRead"
        class="hbt-btn-read"
      >
        <template #icon><eye-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 未读按钮 -->
    <a-tooltip v-if="showUnreadButton" :title="t('common.actions.unread')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleUnread"
        class="hbt-btn-unread"
      >
        <template #icon><eye-invisible-outlined /></template>
      </a-button>
    </a-tooltip>

    <!-- 克隆按钮：克隆数据 -->
    <a-tooltip v-if="showCloneButton" :title="t('common.actions.clone')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleClone"
        class="hbt-btn-clone"
      >
        <template #icon><copy-outlined /></template>
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
  PlayCircleOutlined,
  StopOutlined,
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
  SettingOutlined,
  ThunderboltOutlined,
  PauseCircleOutlined,
  CloseOutlined,
  CaretRightOutlined,
  AuditOutlined,
  UploadOutlined,
  LikeOutlined,
  StarOutlined,
  EyeInvisibleOutlined
} from '@ant-design/icons-vue'


const { t } = useI18n()

// === 类型定义 ===
interface Props {
  record?: any
  // 基础操作按钮
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
  showClone?: boolean
  clonePermission?: string[]
  showImport?: boolean
  importPermission?: string[]
  showExport?: boolean
  exportPermission?: string[]
  showStart?: boolean
  startPermission?: string[]
  showPause?: boolean
  pausePermission?: string[]
  showResume?: boolean
  resumePermission?: string[]
  showEnd?: boolean
  endPermission?: string[]
  showRun?: boolean
  runPermission?: string[]
  showStop?: boolean
  stopPermission?: string[]
  showSuspend?: boolean
  suspendPermission?: string[]
  showAudit?: boolean
  auditPermission?: string[]
  showApprove?: boolean
  approvePermission?: string[]
  showReject?: boolean
  rejectPermission?: string[]
  showRevoke?: boolean
  revokePermission?: string[]
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
  showForce?: boolean
  forcePermission?: string[]
  showStatus?: boolean
  statusPermission?: string[]
  showHistory?: boolean
  historyPermission?: string[]
  showSend?: boolean
  sendPermission?: string[]
  showTemplate?: boolean
  templatePermission?: string[]
  showPrint?: boolean
  printPermission?: string[]
  showLog?: boolean
  logPermission?: string[]
  showInitialize?: boolean
  initializePermission?: string[]
  showSetting?: boolean
  settingPermission?: string[]
  showPublish?: boolean
  publishPermission?: string[]
  showWithdraw?: boolean
  withdrawPermission?: string[]
  showLike?: boolean
  likePermission?: string[]
  showFavorite?: boolean
  favoritePermission?: string[]
  showRead?: boolean
  readPermission?: string[]
  showUnread?: boolean
  unreadPermission?: string[]
  // 通用属性
  buttonType?: 'link' | 'text' | 'default' | 'primary' | 'dashed'
  size?: 'small' | 'middle' | 'large'
  showText?: boolean
  direction?: 'horizontal' | 'vertical'
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
  showClone: false,
  clonePermission: () => [],
  showImport: false,
  importPermission: () => [],
  showExport: false,
  exportPermission: () => [],
  showStart: false,
  startPermission: () => [],
  showPause: false,
  pausePermission: () => [],
  showResume: false,
  resumePermission: () => [],
  showEnd: false,
  endPermission: () => [],
  showRun: false,
  runPermission: () => [],
  showStop: false,
  stopPermission: () => [],
  showSuspend: false,
  suspendPermission: () => [],
  showAudit: false,
  auditPermission: () => [],
  showApprove: false,
  approvePermission: () => [],
  showReject: false,
  rejectPermission: () => [],
  showRevoke: false,
  revokePermission: () => [],
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
  showForce: false,
  forcePermission: () => [],
  showStatus: false,
  statusPermission: () => [],
  showHistory: false,
  historyPermission: () => [],
  showSend: false,
  sendPermission: () => [],
  showTemplate: false,
  templatePermission: () => [],
  showPrint: false,
  printPermission: () => [],
  showLog: false,
  logPermission: () => [],
  showInitialize: false,
  initializePermission: () => [],
  showSetting: false,
  settingPermission: () => [],
  showPublish: false,
  publishPermission: () => [],
  showWithdraw: false,
  withdrawPermission: () => [],
  showLike: false,
  likePermission: () => [],
  showFavorite: false,
  favoritePermission: () => [],
  showRead: false,
  readPermission: () => [],
  showUnread: false,
  unreadPermission: () => [],
  buttonType: 'link',
  size: 'middle',
  showText: true,
  direction: 'horizontal'
})

// === 事件定义 ===
const emit = defineEmits([
  'save',
  'view',
  'edit',
  'delete',
  'copy',
  'clone',
  'import',
  'export',
  'start',
  'end',
  'run',
  'stop',
  'suspend',
  'resume',
  'pause',
  'force',
  'audit',
  'approve',
  'reject',
  'revoke',
  'authorize',
  'assign-user',
  'generate',
  'sync',
  'sort',
  'preview',
  'transfer',
  'delegate',
  'urge',
  'status',
  'history',
  'send',
  'template',
  'print',
  'log',
  'initialize',
  'setting',
  'publish',
  'withdraw',
  'like',
  'favorite',
  'read',
  'unread'
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

const showCloneButton = computed(() => {
  if (!props.showClone) return false
  if (!props.clonePermission?.length) return false
  return props.clonePermission.some(permission => userStore.permissions.includes(permission))
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

const showRejectButton = computed(() => {
  if (!props.showReject) return false
  if (!props.rejectPermission?.length) return false
  return props.rejectPermission.some(permission => userStore.permissions.includes(permission))
})

const showSuspendButton = computed(() => {
  if (!props.showSuspend) return false
  if (!props.suspendPermission?.length) return false
  return props.suspendPermission.some(permission => userStore.permissions.includes(permission))
})

const showResumeButton = computed(() => {
  if (!props.showResume) return false
  if (!props.resumePermission?.length) return false
  return props.resumePermission.some(permission => userStore.permissions.includes(permission))
})

const showPauseButton = computed(() => {
  if (!props.showPause) return false
  if (!props.pausePermission?.length) return false
  return props.pausePermission.some(permission => userStore.permissions.includes(permission))
})

const showForceButton = computed(() => {
  if (!props.showForce) return false
  if (!props.forcePermission?.length) return false
  return props.forcePermission.some(permission => userStore.permissions.includes(permission))
})

const showAuditButton = computed(() => {
  if (!props.showAudit) return false
  if (!props.auditPermission?.length) return false
  return props.auditPermission.some(permission => userStore.permissions.includes(permission))
})

const showApproveButton = computed(() => {
  if (!props.showApprove) return false
  if (!props.approvePermission?.length) return false
  return props.approvePermission.some(permission => userStore.permissions.includes(permission))
})

const showRevokeButton = computed(() => {
  if (!props.showRevoke) return false
  if (!props.revokePermission?.length) return false
  return props.revokePermission.some(permission => userStore.permissions.includes(permission))
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

const showStatusButton = computed(() => {
  if (!props.showStatus) return false
  if (!props.statusPermission?.length) return false
  return props.statusPermission.some(permission => userStore.permissions.includes(permission))
})

const showHistoryButton = computed(() => {
  if (!props.showHistory) return false
  if (!props.historyPermission?.length) return false
  return props.historyPermission.some(permission => userStore.permissions.includes(permission))
})

const showSendButton = computed(() => {
  if (!props.showSend) return false
  if (!props.sendPermission?.length) return false
  return props.sendPermission.some(permission => userStore.permissions.includes(permission))
})

const showTemplateButton = computed(() => {
  if (!props.showTemplate) return false
  if (!props.templatePermission?.length) return false
  return props.templatePermission.some(permission => userStore.permissions.includes(permission))
})

// 新增通用按钮的computed属性
const showSettingButton = computed(() => {
  if (!props.showSetting) return false
  if (!props.settingPermission?.length) return false
  return props.settingPermission.some(permission => userStore.permissions.includes(permission))
})

const showPublishButton = computed(() => {
  if (!props.showPublish) return false
  if (!props.publishPermission?.length) return false
  return props.publishPermission.some(permission => userStore.permissions.includes(permission))
})

const showWithdrawButton = computed(() => {
  if (!props.showWithdraw) return false
  if (!props.withdrawPermission?.length) return false
  return props.withdrawPermission.some(permission => userStore.permissions.includes(permission))
})

const showLikeButton = computed(() => {
  if (!props.showLike) return false
  if (!props.likePermission?.length) return false
  return props.likePermission.some(permission => userStore.permissions.includes(permission))
})

const showFavoriteButton = computed(() => {
  if (!props.showFavorite) return false
  if (!props.favoritePermission?.length) return false
  return props.favoritePermission.some(permission => userStore.permissions.includes(permission))
})

const showReadButton = computed(() => {
  if (!props.showRead) return false
  if (!props.readPermission?.length) return false
  return props.readPermission.some(permission => userStore.permissions.includes(permission))
})

const showUnreadButton = computed(() => {
  if (!props.showUnread) return false
  if (!props.unreadPermission?.length) return false
  return props.unreadPermission.some(permission => userStore.permissions.includes(permission))
})

const showInitializeButton = computed(() => {
  if (!props.showInitialize) return false
  if (!props.initializePermission?.length) return false
  return props.initializePermission.some(permission => userStore.permissions.includes(permission))
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

// === 事件处理 ===
const handleSave = () => emit('save', props.record)
const handleView = () => emit('view', props.record)
const handleEdit = () => emit('edit', props.record)
const handleDelete = () => emit('delete', props.record)
const handleCopy = () => emit('copy', props.record)
const handleClone = () => emit('clone', props.record)
const handleImport = () => emit('import', props.record)
const handleExport = () => emit('export', props.record)
const handleStart = () => emit('start', props.record)
const handleEnd = () => emit('end', props.record)
const handleRun = () => emit('run', props.record)
const handleStop = () => emit('stop', props.record)
const handleSuspend = () => emit('suspend', props.record)
const handleResume = () => emit('resume', props.record)
const handlePause = () => emit('pause', props.record)
const handleForce = () => emit('force', props.record)
const handleAudit = () => emit('audit', props.record)
const handleApprove = () => emit('approve', props.record)
const handleReject = () => emit('reject', props.record)
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
const handleStatus = () => emit('status', props.record)
const handleHistory = () => emit('history', props.record)
const handleSend = () => emit('send', props.record)
const handleTemplate = () => emit('template', props.record)
const handleInitialize = () => emit('initialize', props.record)

// 新增通用按钮的事件处理
const handleSetting = () => emit('setting', props.record)
const handlePublish = () => emit('publish', props.record)
const handleWithdraw = () => emit('withdraw', props.record)
const handleLike = () => emit('like', props.record)
const handleFavorite = () => emit('favorite', props.record)
const handleRead = () => emit('read', props.record)
const handleUnread = () => emit('unread', props.record)
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