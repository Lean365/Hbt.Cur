<template>
  <a-form layout="vertical" class="message-detail-form">
    <a-form-item :label="t('message.detail.sender')">
      <a-input v-model:value="formData.senderName" disabled />
    </a-form-item>
    
    <a-form-item :label="t('message.detail.receiver')">
      <a-input v-model:value="formData.receiverName" disabled />
    </a-form-item>
    
    <a-form-item :label="t('message.detail.sendTime')">
      <a-input v-model:value="formData.sendTime" disabled />
    </a-form-item>
    
    <a-form-item :label="t('message.detail.type')">
      <a-input v-model:value="formData.messageType" disabled />
    </a-form-item>
    
    <a-form-item :label="t('message.detail.status')">
      <a-input v-model:value="formData.messageStatus" disabled />
    </a-form-item>
    
    <a-form-item :label="t('message.detail.content')">
      <a-textarea
        v-model:value="formData.content"
        :rows="6"
        disabled
        class="message-content"
      />
    </a-form-item>
  </a-form>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtOnlineMessageDto } from '@/types/signalr/onlineMessage'

const { t } = useI18n()

interface Props {
  messageData?: HbtOnlineMessageDto
}

const props = withDefaults(defineProps<Props>(), {
  messageData: undefined
})

const formData = ref({
  senderName: '',
  receiverName: '',
  sendTime: '',
  messageType: '',
  messageStatus: '',
  content: ''
})

// 监听消息数据变化
watch(() => props.messageData, (newVal) => {
  if (newVal) {
    formData.value = {
      senderName: newVal.senderName || '',
      receiverName: newVal.receiverName || '',
      sendTime: newVal.sendTime ? new Date(newVal.sendTime).toLocaleString() : '',
      messageType: getMessageTypeText(newVal.messageType),
      messageStatus: getMessageStatusText(newVal.messageStatus),
      content: newVal.messageContent || ''
    }
  }
}, { immediate: true })

// 获取消息类型文本
const getMessageTypeText = (type: string) => {
  switch (type) {
    case 'System':
      return t('message.types.system')
    case 'Task':
      return t('message.types.task')
    case 'Chat':
      return t('message.types.chat')
    default:
      return t('message.types.unknown')
  }
}

// 获取消息状态文本
const getMessageStatusText = (status: number) => {
  switch (status) {
    case 0:
      return t('message.status.unread')
    case 1:
      return t('message.status.read')
    default:
      return t('message.status.unknown')
  }
}
</script>

<style lang="less" scoped>
.message-detail-form {
  padding: 24px;
  
  :deep(.ant-form-item) {
    margin-bottom: 16px;
    
    &:last-child {
      margin-bottom: 0;
    }
  }
  
  .message-content {
    resize: none;
    background-color: var(--ant-color-bg-container);
    border: 1px solid var(--ant-color-border);
    border-radius: 4px;
    padding: 8px 12px;
    font-size: 14px;
    line-height: 1.5;
    color: var(--ant-color-text);
    
    &:disabled {
      color: var(--ant-color-text);
      background-color: var(--ant-color-bg-container);
      cursor: default;
    }
  }
}
</style> 