<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.definition.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-definition-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.definition.fields.workflowName')">
          {{ detail.workflowName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.definition.fields.workflowCategory')">
          <hbt-dict-tag dict-type="workflow_category" :value="detail.workflowCategory || ''" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.definition.fields.workflowVersion')">
          {{ detail.workflowVersion }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.definition.fields.status')">
          <hbt-dict-tag dict-type="workflow_status" :value="detail.status" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.definition.fields.remark')">
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
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtDefinition } from '@/types/workflow/definition'
import { getWorkflowDefinition } from '@/api/workflow/definition'
import { getForm } from '@/api/workflow/form'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  definitionId: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<HbtDefinition>({
  definitionId: 0,
  workflowName: '',
  workflowCategory: '',
  workflowVersion: '',
  status: 0,
  remark: '',
  workflowNodes: [],
  formId: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0,
  workflowConfig: ''
} as HbtDefinition)

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
  if (!props.definitionId) {
    return
  }
  try {
    const response = await getWorkflowDefinition(props.definitionId)
    if (response.data.data) {
      detail.value = response.data.data
      // 通过formId获取表单详情
      if (detail.value.formId) {
        try {
          const formRes = await getForm(detail.value.formId)
          const formConfig = JSON.parse(formRes.data.data.formConfig || '{}')
          formFields.value = formConfig.fields || []
        } catch (error) {
          console.error('获取表单配置失败:', error)
        }
      }
    }
  } catch (error) {
    console.error('获取工作流定义详情失败:', error)
  }
}

// 监听definitionId变化
watch(() => props.definitionId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })
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