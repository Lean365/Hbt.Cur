<template>
  <div class="workflow-node-detail">
    <!-- 基本信息卡片 -->
    <a-card title="基本信息" class="info-card">
      <a-descriptions :column="2">
        <a-descriptions-item label="节点名称">
          {{ nodeData.NodeName }}
        </a-descriptions-item>
        <a-descriptions-item label="节点类型">
          <hbt-dict-tag dict-type="workflow_node_type" :value="nodeData.nodeType" />
        </a-descriptions-item>
        <a-descriptions-item label="节点配置">
          <pre>{{ nodeData.nodeConfig }}</pre>
        </a-descriptions-item>
        <a-descriptions-item label="备注">
          {{ nodeData.remark }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- 操作按钮 -->
    <div class="operation-buttons">
      <a-button @click="handleBack">返回</a-button>
      <a-button
        type="primary"
        @click="handleEdit"
        :permission="['workflow:node:update']"
      >
        编辑
      </a-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useDictStore } from '@/stores/dict'
import type { HbtWorkflowNode } from '@/types/workflow/workflowNode'
import { getWorkflowNode } from '@/api/workflow/workflowNode'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()
const route = useRoute()

// 节点数据
const nodeData = ref<HbtWorkflowNode>({} as HbtWorkflowNode)

// 获取节点详情
const fetchNodeDetail = async () => {
  const id = Number(route.params.id)
  try {
    const res = await getWorkflowNode(id)
    if (res.data.code === 200) {
      nodeData.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 处理返回
const handleBack = () => {
  router.back()
}

// 处理编辑
const handleEdit = () => {
  router.push(`/workflow/node/edit/${route.params.id}`)
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_node_status', 'workflow_node_type'])
  // 加载节点数据
  fetchNodeDetail()
})
</script>

<style lang="less" scoped>
.workflow-node-detail {
  padding: 24px;
  background-color: #fff;

  .info-card {
    margin-bottom: 24px;
  }

  pre {
    margin: 0;
    padding: 8px;
    background-color: #f5f5f5;
    border-radius: 4px;
    white-space: pre-wrap;
    word-wrap: break-word;
  }

  .operation-buttons {
    display: flex;
    justify-content: flex-end;
    gap: 8px;
    margin-top: 24px;
  }
}
</style> 