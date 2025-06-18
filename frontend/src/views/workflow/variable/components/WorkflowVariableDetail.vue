<template>
  <div class="workflow-variable-detail">
    <!-- 基本信息卡片 -->
    <a-card title="基本信息" class="detail-card">
      <a-descriptions :column="2">
        <a-descriptions-item label="工作流实例ID">
          {{ workflowVariable?.workflowInstanceId }}
        </a-descriptions-item>
        <a-descriptions-item label="节点ID">
          {{ workflowVariable?.nodeId || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="变量名称">
          {{ workflowVariable?.variableName }}
        </a-descriptions-item>
        <a-descriptions-item label="变量类型">
          <hbt-dict-tag dict-type="workflow_variable_type" :value="workflowVariable?.variableType || ''" />
        </a-descriptions-item>
        <a-descriptions-item label="变量值">
          {{ workflowVariable?.variableValue }}
        </a-descriptions-item>
        <a-descriptions-item label="作用域">
          <a-tag :color="workflowVariable?.scope === 1 ? 'blue' : 'green'">
            {{ workflowVariable?.scope === 1 ? '全局' : '节点' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ workflowVariable?.createTime }}
        </a-descriptions-item>
        <a-descriptions-item label="更新时间">
          {{ workflowVariable?.updateTime }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- 操作按钮 -->
    <div class="action-buttons">
      <a-space>
        <a-button type="primary" @click="handleEdit">
          编辑
        </a-button>
        <a-button @click="handleBack">返回</a-button>
      </a-space>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { message } from 'ant-design-vue'
import type { HbtWorkflowVariable } from '@/types/workflow/variable'
import { getWorkflowVariable } from '@/api/workflow/variable'

const route = useRoute()
const router = useRouter()

const workflowVariable = ref<HbtWorkflowVariable>()

// 获取工作流变量详情
const fetchData = async () => {
  const id = Number(route.params.id)
  if (!id) {
    message.error('参数错误')
    return
  }

  try {
    const res = await getWorkflowVariable(id)
    if (res.data.code === 200) {
      workflowVariable.value = res.data.data
    } else {
      message.error(res.data.msg || '获取工作流变量详情失败')
    }
  } catch (error) {
    console.error('获取工作流变量详情失败:', error)
    message.error('获取工作流变量详情失败')
  }
}

// 处理编辑
const handleEdit = () => {
  if (workflowVariable.value?.id) {
    router.push(`/workflow/variable/edit/${workflowVariable.value.id}`)
  }
}

// 处理返回
const handleBack = () => {
  router.back()
}

onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-variable-detail {
  padding: 24px;
  background-color: #fff;

  .detail-card {
    margin-bottom: 24px;
  }

  .action-buttons {
    display: flex;
    justify-content: center;
    margin-top: 24px;
  }
}
</style> 