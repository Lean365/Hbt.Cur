<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.definition.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-definition-detail">
      <!-- 多Tabs内容 -->
      <a-tabs v-model:activeKey="activeTab" class="workflow-tabs">
        <!-- 基本信息Tab -->
        <a-tab-pane key="basic" tab="基本信息">
          <a-card title="流程定义基本信息" class="basic-info-card">
            <a-descriptions :column="2" bordered>
              <a-descriptions-item label="流程定义键">
                {{ detail.schemeKey }}
              </a-descriptions-item>
              <a-descriptions-item label="流程定义名称">
                {{ detail.schemeName }}
              </a-descriptions-item>
              <a-descriptions-item label="流程分类">
                <hbt-dict-tag dict-type="workflow_category" :value="detail.schemeCategory || ''" />
              </a-descriptions-item>
              <a-descriptions-item label="版本">
                {{ detail.version }}
              </a-descriptions-item>
              <a-descriptions-item label="状态">
                <hbt-dict-tag dict-type="workflow_status" :value="detail.status" />
              </a-descriptions-item>
              <a-descriptions-item label="表单ID">
                {{ detail.formId || '未配置' }}
              </a-descriptions-item>
              <a-descriptions-item label="描述" :span="2">
                {{ detail.description || '无' }}
              </a-descriptions-item>
              <a-descriptions-item label="备注" :span="2">
                {{ detail.remark || '无' }}
              </a-descriptions-item>
              <a-descriptions-item label="创建人">
                {{ detail.createBy }}
              </a-descriptions-item>
              <a-descriptions-item label="创建时间">
                {{ detail.createTime ? formatDateTime(detail.createTime) : '-' }}
              </a-descriptions-item>
              <a-descriptions-item label="更新人">
                {{ detail.updateBy || '-' }}
              </a-descriptions-item>
              <a-descriptions-item label="更新时间">
                {{ detail.updateTime ? formatDateTime(detail.updateTime) : '-' }}
              </a-descriptions-item>
            </a-descriptions>
          </a-card>
        </a-tab-pane>

        <!-- 表单信息Tab -->
        <a-tab-pane key="form" tab="表单信息">
          <a-card title="关联表单信息" class="form-info-card">
            <div v-if="loadingForm" class="form-loading">
              <a-spin tip="加载表单信息中..." />
            </div>
            <div v-else-if="formDetail" class="form-detail">
              <a-descriptions :column="2" bordered>
                <a-descriptions-item label="表单键">
                  {{ formDetail.formKey }}
                </a-descriptions-item>
                <a-descriptions-item label="表单名称">
                  {{ formDetail.formName }}
                </a-descriptions-item>
                <a-descriptions-item label="表单分类">
                  <hbt-dict-tag dict-type="form_category" :value="formDetail.formCategory" />
                </a-descriptions-item>
                <a-descriptions-item label="表单类型">
                  <hbt-dict-tag dict-type="form_type" :value="formDetail.formType" />
                </a-descriptions-item>
                <a-descriptions-item label="版本">
                  {{ formDetail.version }}
                </a-descriptions-item>
                <a-descriptions-item label="状态">
                  <hbt-dict-tag dict-type="form_status" :value="formDetail.status" />
                </a-descriptions-item>
                <a-descriptions-item label="描述" :span="2">
                  {{ formDetail.description || '无' }}
                </a-descriptions-item>
              </a-descriptions>
              
              <!-- 表单字段列表 -->
              <div class="form-fields-section">
                <h4>表单字段</h4>
                <a-table
                  :columns="formColumns"
                  :data-source="formFields"
                  :pagination="false"
                  row-key="fieldId"
                  size="small"
                >
                  <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'fieldType'">
                      <hbt-dict-tag dict-type="form_field_type" :value="record.fieldType" />
                    </template>
                    <template v-else-if="column.key === 'required'">
                      <a-tag :color="record.required ? 'red' : 'green'">
                        {{ record.required ? '是' : '否' }}
                      </a-tag>
                    </template>
                  </template>
                </a-table>
              </div>
            </div>
            <div v-else class="form-empty">
              <a-empty description="未配置关联表单" />
            </div>
          </a-card>
        </a-tab-pane>

        <!-- 流程信息Tab -->
        <a-tab-pane key="flow" tab="流程信息">
          <a-card title="流程配置信息" class="flow-info-card">
            <div v-if="loadingFlow" class="flow-loading">
              <a-spin tip="解析流程配置中..." />
            </div>
            <div v-else-if="workflowConfig && workflowConfig.nodes && workflowConfig.nodes.length > 0" class="flow-detail">
              <!-- 流程节点列表 -->
              <div class="flow-nodes-section">
                <h4>流程节点</h4>
                <a-table
                  :columns="nodeColumns"
                  :data-source="workflowNodes"
                  :pagination="false"
                  row-key="id"
                  size="small"
                >
                  <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'nodeType'">
                      <a-tag :color="getNodeTypeColor(record.type)">
                        {{ getNodeTypeText(record.type) }}
                      </a-tag>
                    </template>
                  </template>
                </a-table>
              </div>
              
              <!-- 流程图预览 -->
              <div class="flow-diagram-section">
                <h4>流程图预览</h4>
                <div class="flow-diagram">
                  <hbt-flow 
                    v-model:value="workflowConfig" 
                    :width="800" 
                    :height="400"
                    :readonly="true"
                  />
                </div>
              </div>
            </div>
            <div v-else class="flow-empty">
              <a-empty description="流程配置为空或解析失败" />
            </div>
          </a-card>
        </a-tab-pane>
      </a-tabs>
    </div>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtScheme } from '@/types/workflow/scheme'
import { getSchemeById } from '@/api/workflow/scheme'
import { getFormById } from '@/api/workflow/form'
import { formatDateTime } from '@/utils/format'


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

const activeTab = ref('basic')
const loadingForm = ref(false)
const loadingFlow = ref(false)

const detail = ref<HbtScheme>({
  schemeId: 0,
  schemeKey: '',
  schemeName: '',
  schemeCategory: 1,
  version: '',
  schemeConfig: '',
  formId: 0,
  status: 0,
  description: '',
  remark: '',
  createBy: '',
  createTime: '',
  isDeleted: 0
} as HbtScheme)

const formDetail = ref<any>(null)
const formFields = ref<any[]>([])
const workflowConfig = ref<any>({ nodes: [], edges: [] })
const workflowNodes = ref<any[]>([])

// 节点表格列定义
const nodeColumns = [
  {
    title: '节点名称',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: '节点类型',
    dataIndex: 'type',
    key: 'nodeType'
  },
  {
    title: '节点ID',
    dataIndex: 'id',
    key: 'id'
  },
  {
    title: '描述',
    dataIndex: 'description',
    key: 'description'
  }
]

// 表单表格列定义
const formColumns = [
  {
    title: '字段名称',
    dataIndex: 'fieldName',
    key: 'fieldName'
  },
  {
    title: '字段类型',
    dataIndex: 'fieldType',
    key: 'fieldType'
  },
  {
    title: '是否必填',
    dataIndex: 'required',
    key: 'required'
  },
  {
    title: '默认值',
    dataIndex: 'defaultValue',
    key: 'defaultValue'
  },
  {
    title: '描述',
    dataIndex: 'description',
    key: 'description'
  }
]

// 获取节点类型颜色
const getNodeTypeColor = (type: string) => {
  switch (type) {
    case 'start':
      return 'green'
    case 'end':
      return 'red'
    case 'userTask':
      return 'blue'
    case 'gateway':
      return 'orange'
    default:
      return 'default'
  }
}

// 获取节点类型文本
const getNodeTypeText = (type: string) => {
  switch (type) {
    case 'start':
      return '开始节点'
    case 'end':
      return '结束节点'
    case 'userTask':
      return '用户任务'
    case 'gateway':
      return '网关'
    default:
      return type
  }
}

// 解析工作流配置
const parseWorkflowConfig = (configStr: string) => {
  if (!configStr) return null
  
  try {
    const config = typeof configStr === 'string' ? JSON.parse(configStr) : configStr
    workflowConfig.value = config
    
    // 提取节点信息
    if (config.nodes && Array.isArray(config.nodes)) {
      workflowNodes.value = config.nodes.map((node: any) => ({
        id: node.id,
        name: node.name || node.id,
        type: node.type || 'task',
        description: node.description || ''
      }))
    }
    
    return config
  } catch (error) {
    console.error('解析工作流配置失败:', error)
    return null
  }
}

// 加载表单详情
const loadFormDetail = async (formId: number) => {
  if (!formId) return
  
  loadingForm.value = true
  try {
    const response = await getFormById(formId)
    if (response.data.code === 200) {
      formDetail.value = response.data.data
      
      // 解析表单字段
      if (formDetail.value.formConfig) {
        try {
          const config = JSON.parse(formDetail.value.formConfig)
          if (config.rule && Array.isArray(config.rule)) {
            formFields.value = config.rule.map((field: any, index: number) => ({
              fieldId: index,
              fieldName: field.title || field.field,
              fieldType: field.type || 'input',
              required: field.validate && field.validate.some((v: any) => v.required),
              defaultValue: field.value || '',
              description: field.description || ''
            }))
          }
        } catch (error) {
          console.error('解析表单配置失败:', error)
          formFields.value = []
        }
      }
    }
  } catch (error) {
    console.error('获取表单详情失败:', error)
    message.error('获取表单详情失败')
  } finally {
    loadingForm.value = false
  }
}

// 获取工作流定义详情
const fetchData = async () => {
  if (!props.definitionId) {
    return
  }
  try {
    const response = await getSchemeById(props.definitionId)
    if (response.data.code === 200) {
      detail.value = response.data.data
      
      // 加载表单详情
      if (detail.value.formId) {
        await loadFormDetail(detail.value.formId)
      }
      
      // 解析工作流配置
      if (detail.value.schemeConfig) {
        loadingFlow.value = true
        parseWorkflowConfig(detail.value.schemeConfig)
        loadingFlow.value = false
      }
    }
  } catch (error) {
    console.error('获取工作流定义详情失败:', error)
    message.error('获取工作流定义详情失败')
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
  .workflow-tabs {
    margin-bottom: 16px;
  }

  .basic-info-card,
  .form-info-card,
  .flow-info-card {
    margin-bottom: 16px;
  }

  .form-fields-section,
  .flow-nodes-section,
  .flow-diagram-section {
    margin-top: 24px;

    h4 {
      margin-bottom: 16px;
      font-weight: 500;
      color: #333;
    }
  }

  .form-loading,
  .flow-loading {
    padding: 40px;
    text-align: center;
  }

  .form-empty,
  .flow-empty {
    padding: 40px;
    text-align: center;
  }

  .flow-diagram {
    padding: 20px;
    border: 1px solid #e1e5e9;
    border-radius: 4px;
    background: #fafafa;
    
    :deep(.flow-root) {
      width: 100%;
      height: 400px;
    }
    
    :deep(.flow-canvas-wrap) {
      width: 100%;
      height: 100%;
    }
    
    :deep(.flow-canvas) {
      width: 100%;
      height: 100%;
    }
  }

  :deep(.ant-descriptions-item-label) {
    font-weight: 500;
    width: 120px;
  }

  :deep(.ant-tabs-content-holder) {
    min-height: 400px;
  }
}
</style> 