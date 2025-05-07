<template>
  <div class="workflow-instance-detail">
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('workflow.instance.fields.workflowTitle.label')">
        {{ detail.workflowTitle }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.workflowDefinitionId.label')">
        {{ detail.workflowDefinitionId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.currentNodeId.label')">
        {{ detail.currentNodeId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.initiatorId.label')">
        {{ detail.initiatorId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.status.label')">
        <hbt-dict-tag dict-type="workflow_instance_status" :value="detail.status ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.startTime.label')">
        {{ detail.startTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.endTime.label')">
        {{ detail.endTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.fields.formData.label')">
        {{ detail.formData }}
      </a-descriptions-item>
    </a-descriptions>

    <!-- 流程图 -->
    <div class="workflow-diagram">
      <h3>{{ t('workflow.instance.diagram.title') }}</h3>
      <!-- 这里可以添加流程图组件 -->
    </div>

    <!-- 审批历史 -->
    <div class="approval-history">
      <h3>{{ t('workflow.instance.history.title') }}</h3>
      <a-timeline>
        <a-timeline-item v-for="history in approvalHistory" :key="history.id">
          <template #dot>
            <CheckCircleOutlined v-if="history.status === 1" style="color: #52c41a" />
            <CloseCircleOutlined v-else-if="history.status === 2" style="color: #ff4d4f" />
            <LoadingOutlined v-else style="color: #1890ff" />
          </template>
          <div class="history-item">
            <div class="history-header">
              <span class="operator">{{ history.operatorName }}</span>
              <span class="time">{{ history.operateTime }}</span>
            </div>
            <div class="history-content">
              <p>{{ history.remark }}</p>
            </div>
          </div>
        </a-timeline-item>
      </a-timeline>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { CheckCircleOutlined, CloseCircleOutlined, LoadingOutlined } from '@ant-design/icons-vue'
import type { HbtWorkflowInstance } from '@/types/workflow/workflowInstance'
import { getWorkflowInstance } from '@/api/workflow/workflowInstance'

const { t } = useI18n()

const props = defineProps<{
  instanceId: number
}>()

// 实例详情
const detail = ref<Partial<HbtWorkflowInstance>>({})

// 审批历史
const approvalHistory = ref<any[]>([])

// 获取实例详情
const getDetail = async () => {
  try {
    const res = await getWorkflowInstance(props.instanceId)
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
.workflow-instance-detail {
  padding: 24px;
  background-color: #fff;

  .workflow-diagram,
  .approval-history {
    margin-top: 24px;

    h3 {
      margin-bottom: 16px;
      font-size: 16px;
      font-weight: 500;
    }
  }

  .history-item {
    .history-header {
      display: flex;
      justify-content: space-between;
      margin-bottom: 8px;

      .operator {
        font-weight: 500;
      }

      .time {
        color: rgba(0, 0, 0, 0.45);
      }
    }

    .history-content {
      color: rgba(0, 0, 0, 0.65);
    }
  }
}
</style> 