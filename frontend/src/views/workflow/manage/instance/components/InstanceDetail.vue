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
        <a-descriptions-item :label="t('workflow.instance.fields.instanceTitle')">
          {{ detail.instanceTitle }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.schemeId')">
          {{ detail.schemeId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.priority')">
          {{ detail.priority }}
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
          :data-source="[]"
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
                <span>简化后的工作流不包含节点操作功能</span>
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
// 简化后的工作流不包含task功能，暂时注释掉API调用
// import { getInstanceById } from '@/api/workflow/instance'
// import { getFormById } from '@/api/workflow/form'
// 简化后的工作流不包含task功能，使用工作流引擎API
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
  schemeId: 0,
  instanceTitle: '',
  businessKey: '',
  initiatorId: 0,
  currentNodeId: '',
  currentNodeName: '',
  status: 0,
  priority: 2,
  urgency: 1,
  startTime: '',
  endTime: '',
  variables: '',
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
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

// 获取工作流实例详情（使用模拟数据）
const fetchData = async () => {
  if (!props.instanceId) {
    return
  }
  try {
    // 使用模拟数据
    detail.value = {
      instanceId: props.instanceId,
      schemeId: 1,
      instanceTitle: '请假申请实例',
      businessKey: 'LEAVE_20241219_001',
      initiatorId: 1001,
      currentNodeId: 'deptManager',
      currentNodeName: '部门经理审批',
      status: 1,
      priority: 3,
      urgency: 2,
      startTime: '2024-12-19 09:00:00',
      endTime: '',
      variables: '{"leaveDays": 3, "leaveType": "病假"}',
      remark: '身体不适，需要请假',
      createBy: '张三',
      createTime: '2024-12-19 09:00:00',
      updateBy: '',
      updateTime: '',
      isDeleted: 0
    }
    
    // 简化后的工作流不包含formId，直接使用模拟数据
    formFields.value = [
      { fieldName: '申请人', fieldType: 'text', required: true, remark: '申请人姓名' },
      { fieldName: '申请日期', fieldType: 'date', required: true, remark: '申请日期' },
      { fieldName: '申请原因', fieldType: 'textarea', required: true, remark: '申请原因说明' }
    ]
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

// 简化后的工作流不包含task功能，这些操作方法已不再需要
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