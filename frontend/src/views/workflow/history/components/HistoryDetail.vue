<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.history.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-history-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.history.fields.instanceId')">
          {{ detail.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.fields.nodeId')">
          {{ detail.nodeId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.fields.operationType')">
          <hbt-dict-tag dict-type="workflow_operation_type" :value="detail.operationType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.fields.operationResult')">
          <hbt-dict-tag dict-type="workflow_operation_result" :value="detail.operationResult ?? 0" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.fields.operationComment')" :span="2">
          {{ detail.operationComment }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 时间信息 -->
      <div class="time-info">
        <h3>{{ t('common.timeInfo') }}</h3>
        <a-descriptions :column="2" bordered>
          <a-descriptions-item :label="t('common.createBy')">
            {{ detail.createBy }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.createTime')">
            {{ detail.createTime }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.updateBy')">
            {{ detail.updateBy }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.updateTime')">
            {{ detail.updateTime }}
          </a-descriptions-item>
        </a-descriptions>
      </div>
    </div>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtHistory } from '@/types/workflow/history'
import { getWorkflowHistory } from '@/api/workflow/history'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  historyId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<HbtHistory>({
  historyId: 0,
  instanceId: 0,
  nodeId: 0,
  operationType: 0,
  operationResult: 0,
  operationComment: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: ''
} as HbtHistory)

// 获取历史详情
const fetchData = async () => {
  if (!props.historyId) {
    return
  }
  try {
    const response = await getWorkflowHistory(props.historyId)
    if (response.data.code === 200) {
      detail.value = response.data.data
    }
  } catch (error) {
    console.error('获取历史详情失败:', error)
  }
}

// 监听historyId变化
watch(() => props.historyId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })
</script>

<style lang="less" scoped>
.workflow-history-detail {
  .time-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }
}
</style> 