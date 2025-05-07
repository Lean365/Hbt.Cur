<template>
  <div class="workflow-definition-detail">
    <!-- 基本信息 -->
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('workflow.definition.fields.workflowName.label')">
        {{ detail.workflowName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.definition.fields.workflowCategory.label')">
        {{ detail.workflowCategory }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.definition.fields.workflowVersion.label')">
        {{ detail.workflowVersion }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.definition.fields.status.label')">
        <hbt-dict-tag dict-type="workflow_definition_status" :value="detail.status" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.definition.fields.remark.label')">
        {{ detail.remark }}
      </a-descriptions-item>
    </a-descriptions>

    <!-- 节点信息 -->
    <div class="node-info">
      <h3>{{ t('workflow.definition.node.title') }}</h3>
      <a-table
        :columns="nodeColumns"
        :data-source="detail.workflowNodes"
        :pagination="false"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'nodeType'">
            <hbt-dict-tag dict-type="workflow_node_type" :value="record.nodeType" />
          </template>
          <template v-if="column.key === 'assigneeType'">
            <hbt-dict-tag dict-type="workflow_assignee_type" :value="record.assigneeType" />
          </template>
          <template v-if="column.key === 'formType'">
            <hbt-dict-tag dict-type="workflow_form_type" :value="record.formType" />
          </template>
        </template>
      </a-table>
    </div>

    <!-- 表单信息 -->
    <div class="form-info">
      <h3>{{ t('workflow.definition.form.title') }}</h3>
      <a-table
        :columns="formColumns"
        :data-source="formFields"
        :pagination="false"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'fieldType'">
            <hbt-dict-tag dict-type="workflow_form_field_type" :value="record.fieldType" />
          </template>
          <template v-if="column.key === 'required'">
            <a-tag :color="record.required ? 'red' : 'green'">
              {{ record.required ? t('common.yes') : t('common.no') }}
            </a-tag>
          </template>
        </template>
      </a-table>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtWorkflowDefinition } from '@/types/workflow/workflowDefinition'
import { getWorkflowDefinition } from '@/api/workflow/workflowDefinition'

const { t } = useI18n()

const props = defineProps<{
  definitionId: string
}>()

const detail = ref<HbtWorkflowDefinition>({} as HbtWorkflowDefinition)
const formFields = ref<any[]>([])

// 节点表格列定义
const nodeColumns = [
  {
    title: t('workflow.node.fields.nodeName.label'),
    dataIndex: 'nodeName',
    key: 'nodeName'
  },
  {
    title: t('workflow.node.fields.nodeType.label'),
    dataIndex: 'nodeType',
    key: 'nodeType'
  },
  {
    title: t('workflow.node.fields.assigneeType.label'),
    dataIndex: 'assigneeType',
    key: 'assigneeType'
  },
  {
    title: t('workflow.node.fields.formType.label'),
    dataIndex: 'formType',
    key: 'formType'
  }
]

// 表单表格列定义
const formColumns = [
  {
    title: t('workflow.form.fields.fieldName.label'),
    dataIndex: 'fieldName',
    key: 'fieldName'
  },
  {
    title: t('workflow.form.fields.fieldType.label'),
    dataIndex: 'fieldType',
    key: 'fieldType'
  },
  {
    title: t('workflow.form.fields.required.label'),
    dataIndex: 'required',
    key: 'required'
  },
  {
    title: t('workflow.form.fields.remark.label'),
    dataIndex: 'remark',
    key: 'remark'
  }
]

// 获取工作流定义详情
const fetchData = async () => {
  try {
    const response = await getWorkflowDefinition(Number(props.definitionId))
    if (response.data.data) {
      detail.value = response.data.data
      // 解析表单配置
      if (detail.value.formConfig) {
        try {
          const formConfig = JSON.parse(detail.value.formConfig)
          formFields.value = formConfig.fields || []
        } catch (error) {
          console.error('解析表单配置失败:', error)
        }
      }
    }
  } catch (error) {
    console.error('获取工作流定义详情失败:', error)
  }
}

onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-definition-detail {
  .node-info,
  .form-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }
}
</style> 