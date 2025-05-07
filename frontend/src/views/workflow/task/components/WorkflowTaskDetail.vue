<template>
  <div class="workflow-task-detail">
    <!-- 基本信息卡片 -->
    <a-card title="基本信息" class="info-card">
      <a-descriptions :column="2">
        <a-descriptions-item label="任务名称">
          {{ taskData.taskTitle }}
        </a-descriptions-item>
        <a-descriptions-item label="任务类型">
          <hbt-dict-tag dict-type="workflow_task_type" :value="taskData.taskType" />
        </a-descriptions-item>
        <a-descriptions-item label="状态">
          <hbt-dict-tag dict-type="workflow_task_status" :value="taskData.status" />
        </a-descriptions-item>
        <a-descriptions-item label="备注">
          {{ taskData.remark }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- 操作按钮 -->
    <div class="operation-buttons">
      <a-button @click="handleBack">返回</a-button>
      <a-button
        type="primary"
        @click="handleEdit"
        :permission="['workflow:task:update']"
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
import type { HbtWorkflowTask } from '@/types/workflow/workflowTask'
import { getWorkflowTask } from '@/api/workflow/workflowTask'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()
const route = useRoute()

// 任务数据
const taskData = ref<HbtWorkflowTask>({} as HbtWorkflowTask)

// 获取任务详情
const fetchTaskDetail = async () => {
  const id = Number(route.params.id)
  try {
    const res = await getWorkflowTask(id)
    if (res.data.code === 200) {
      taskData.value = res.data.data
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
  router.push(`/workflow/task/edit/${route.params.id}`)
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_task_status', 'workflow_task_type'])
  // 加载任务数据
  fetchTaskDetail()
})
</script>

<style lang="less" scoped>
.workflow-task-detail {
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