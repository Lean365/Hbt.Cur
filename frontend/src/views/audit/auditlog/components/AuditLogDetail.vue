# 审计日志详情组件
<template>
  <hbt-modal
    v-model:visible="visible"
    title="审计日志详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item label="用户名" :span="1">
        {{ auditInfo?.userName }}
      </a-descriptions-item>
      <a-descriptions-item label="模块" :span="1">
        {{ auditInfo?.module }}
      </a-descriptions-item>
      <a-descriptions-item label="操作" :span="1">
        {{ auditInfo?.operation }}
      </a-descriptions-item>
      <a-descriptions-item label="方法" :span="1">
        {{ auditInfo?.method }}
      </a-descriptions-item>
      <a-descriptions-item label="请求方法" :span="1">
        {{ auditInfo?.requestMethod }}
      </a-descriptions-item>
      <a-descriptions-item label="请求URL" :span="1">
        {{ auditInfo?.requestUrl }}
      </a-descriptions-item>
      <a-descriptions-item label="IP地址" :span="1">
        {{ auditInfo?.ipAddress }}
      </a-descriptions-item>
      <a-descriptions-item label="耗时" :span="1">
        {{ auditInfo?.elapsed }}ms
      </a-descriptions-item>
      <a-descriptions-item label="日志级别" :span="1">
        <a-tag :color="auditInfo?.logLevel === 1 ? 'success' : 'error'">
          {{ auditInfo?.logLevel === 1 ? '成功' : '失败' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="创建时间" :span="1">
        {{ auditInfo?.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="请求参数" :span="2">
        <pre>{{ auditInfo?.parameters }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="执行结果" :span="2">
        <pre>{{ auditInfo?.result }}</pre>
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps } from 'vue'
import type { HbtAuditLogDto } from '@/types/audit/auditLog'

defineProps<{
  auditInfo?: HbtAuditLogDto
}>()

const visible = ref(false)

/** 打开弹窗 */
const open = () => {
  visible.value = true
}

/** 关闭弹窗 */
const close = () => {
  visible.value = false
}

defineExpose({
  open,
  close
})
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-content) {
  pre {
    margin: 0;
    padding: 8px;
    background-color: #f5f5f5;
    border-radius: 4px;
    max-height: 300px;
    overflow: auto;
  }
}
</style> 