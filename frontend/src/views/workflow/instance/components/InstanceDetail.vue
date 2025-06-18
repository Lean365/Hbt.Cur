<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.instance.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-instance-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.instance.fields.instanceName')">
          {{ detail.instanceName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.workflowCategory')">
          <hbt-dict-tag dict-type="workflow_category" :value="detail.workflowCategory || ''" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.workflowVersion')">
          {{ detail.workflowVersion }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.status')">
          <hbt-dict-tag dict-type="workflow_instance_status" :value="detail.status" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.remark')">
          {{ detail.remark }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 节点信息 -->
      <div class="node-info">
        <h3>{{ t('workflow.instance.node.title') }}</h3>
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
            <template v-if="column.key === 'action'">
              <a-space>
                <a-button 
                  v-if="record.id === detail.currentNodeId"
                  type="link" 
                  size="small"
                  @click="handleComplete(record)"
                >
                  {{ t('workflow.instance.node.complete') }}
                </a-button>
                <a-button 
                  v-if="record.id === detail.currentNodeId"
                  type="link" 
                  size="small"
                  @click="handleReject(record)"
                >
                  {{ t('workflow.instance.node.reject') }}
                </a-button>
                <a-button 
                  v-if="record.id === detail.currentNodeId"
                  type="link" 
                  size="small"
                  @click="handleCancel(record)"
                >
                  {{ t('workflow.instance.node.cancel') }}
                </a-button>
              </a-space>
            </template>
          </template>
        </a-table>
      </div>

      <!-- 表单信息 -->
      <div class="form-info">
        <h3>{{ t('workflow.instance.form.title') }}</h3>
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
import type { HbtInstance } from '@/types/workflow/instance'
import { getWorkflowInstance } from '@/api/workflow/instance'
import { getForm } from '@/api/workflow/form'
import { completeWorkflowTask, rejectWorkflowTask, cancelWorkflowTask } from '@/api/workflow/task'
import { message } from 'ant-design-vue'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  instanceId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<HbtInstance>({
  instanceId: 0,
  instanceName: '',
  businessKey: '',
  definitionId: 0,
  currentNodeId: 0,
  initiatorId: 0,
  formData: '',
  startTime: '',
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
} as HbtInstance)

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
  },
  {
    title: t('common.action'),
    key: 'action',
    width: 200,
    fixed: 'right' as const
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

// 获取工作流实例详情
const fetchData = async () => {
  if (!props.instanceId) {
    return
  }
  try {
    const response = await getWorkflowInstance(props.instanceId)
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
    console.error('获取工作流实例详情失败:', error)
  }
}

// 监听instanceId变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })

// 添加节点操作方法
const handleComplete = async (record: any) => {
  try {
    const res = await completeWorkflowTask(record.id, '通过', '')
    if (res.data.code === 200) {
      message.success(t('workflow.instance.node.complete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.node.complete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.node.complete.failed'))
  }
}

const handleReject = async (record: any) => {
  try {
    const res = await rejectWorkflowTask(record.id, '退回')
    if (res.data.code === 200) {
      message.success(t('workflow.instance.node.reject.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.node.reject.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.node.reject.failed'))
  }
}

const handleCancel = async (record: any) => {
  try {
    const res = await cancelWorkflowTask(record.id, '撤销')
    if (res.data.code === 200) {
      message.success(t('workflow.instance.node.cancel.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.node.cancel.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.node.cancel.failed'))
  }
}
</script>

<style lang="less" scoped>
.workflow-instance-detail {
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