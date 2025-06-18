<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.node.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-node-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.node.fields.nodeName')">
          {{ detail.nodeName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.nodeType')">
          <hbt-dict-tag dict-type="workflow_node_type" :value="detail.nodeType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.definitionId')">
          {{ detail.definitionId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.parentNodeId')">
          {{ detail.parentNodeId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.status')">
          <hbt-dict-tag dict-type="workflow_node_status" :value="detail.status" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.orderNum')">
          {{ detail.orderNum }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.nodeConfig')" :span="2">
          <pre>{{ detail.nodeConfig }}</pre>
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.node.fields.remark')" :span="2">
          {{ detail.remark }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 时间信息 -->
      <div class="time-info">
        <h3>{{ t('common.timeInfo') }}</h3>
        <a-descriptions :column="2" bordered>
          <a-descriptions-item :label="t('workflow.node.fields.startTime')">
            {{ detail.startTime }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('workflow.node.fields.endTime')">
            {{ detail.endTime }}
          </a-descriptions-item>
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
import type { HbtNode } from '@/types/workflow/node'
import { getWorkflowNode } from '@/api/workflow/node'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  nodeId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<HbtNode>({
  nodeId: 0,
  nodeName: '',
  nodeType: 0,
  definitionId: 0,
  parentNodeId: undefined,
  nodeConfig: '',
  orderNum: 0,
  remark: '',
  instanceId: 0,
  status: 0,
  startTime: '',
  endTime: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: ''
} as HbtNode)

// 获取节点详情
const fetchData = async () => {
  if (!props.nodeId) {
    return
  }
  try {
    const response = await getWorkflowNode(props.nodeId)
    if (response.data.code === 200) {
      detail.value = response.data.data
    }
  } catch (error) {
    console.error('获取节点详情失败:', error)
  }
}

// 监听nodeId变化
watch(() => props.nodeId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })
</script>

<style lang="less" scoped>
.workflow-node-detail {
  .time-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }

  pre {
    margin: 0;
    padding: 8px;
    background-color: #f5f5f5;
    border-radius: 4px;
    white-space: pre-wrap;
    word-wrap: break-word;
  }
}
</style> 