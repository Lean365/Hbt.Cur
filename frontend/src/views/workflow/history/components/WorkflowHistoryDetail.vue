<template>
  <div class="workflow-history-detail">
    <!-- 基本信息 -->
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('workflow.history.fields.workflowInstanceId.label')">
        {{ detail.workflowInstanceId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.nodeId.label')">
        {{ detail.nodeId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.operationType.label')">
        <hbt-dict-tag dict-type="workflow_operation_type" :value="detail.operationType ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.operatorName.label')">
        {{ detail.operatorName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.operationResult.label')">
        <hbt-dict-tag dict-type="workflow_operation_result" :value="detail.operationResult ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.operationTime.label')">
        {{ detail.operationTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.history.fields.operationComment.label')">
        {{ detail.operationComment }}
      </a-descriptions-item>
    </a-descriptions>

    <!-- 表单数据 -->
    <div class="form-data" v-if="detail.workflowInstance?.formData">
      <h3>{{ t('workflow.history.form.title') }}</h3>
      <a-descriptions :column="2" bordered>
        <template v-for="(value, key) in JSON.parse(detail.workflowInstance.formData)" :key="key">
          <a-descriptions-item :label="key">
            {{ value }}
          </a-descriptions-item>
        </template>
      </a-descriptions>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtWorkflowHistory } from '@/types/workflow/workflowHistory'
import { getWorkflowHistory } from '@/api/workflow/workflowHistory'

const { t } = useI18n()

const props = defineProps<{
  historyId: number
}>()

// 详情数据
const detail = ref<Partial<HbtWorkflowHistory>>({})

// 获取详情
const getDetail = async () => {
  try {
    const res = await getWorkflowHistory(props.historyId)
    if (res.data.code === 200) {
      detail.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

onMounted(() => {
  getDetail()
})
</script>

<style lang="less" scoped>
.workflow-history-detail {
  padding: 24px;
  background-color: #fff;

  .form-data {
    margin-top: 24px;

    h3 {
      margin-bottom: 16px;
      font-size: 16px;
      font-weight: 500;
    }
  }
}
</style> 