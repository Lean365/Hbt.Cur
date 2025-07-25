<template>
  <div class="workflow-designer-container">
    <!-- 顶部工具栏 -->
    <div class="designer-toolbar">
      <div class="toolbar-left">
        <a-button type="primary" @click="handleSave">
          <template #icon><SaveOutlined /></template>
          保存流程
        </a-button>
        <a-button @click="handlePreview">
          <template #icon><EyeOutlined /></template>
          预览流程
        </a-button>
        <a-button @click="handlePublish" :disabled="!canPublish">
          <template #icon><SendOutlined /></template>
          发布流程
        </a-button>
      </div>
      <div class="toolbar-right">
        <a-button @click="handleUndo" :disabled="!canUndo">
          <template #icon><UndoOutlined /></template>
          撤销
        </a-button>
        <a-button @click="handleRedo" :disabled="!canRedo">
          <template #icon><RedoOutlined /></template>
          重做
        </a-button>
        <a-button @click="handleZoomIn">
          <template #icon><ZoomInOutlined /></template>
          放大
        </a-button>
        <a-button @click="handleZoomOut">
          <template #icon><ZoomOutOutlined /></template>
          缩小
        </a-button>
        <a-button @click="handleFitView">
          <template #icon><FullscreenOutlined /></template>
          适应画布
        </a-button>
      </div>
    </div>

    <!-- 主要内容区域 -->
    <div class="designer-content">
      <!-- 左侧节点面板 -->
      <div class="node-panel">
        <div class="panel-header">
          <h3>节点类型</h3>
        </div>
        <div class="node-list">
          <div
            v-for="nodeType in nodeTypes"
            :key="nodeType.type"
            class="node-item"
            draggable="true"
            @dragstart="handleNodeDragStart(nodeType)"
          >
            <div class="node-icon">
              <component :is="nodeType.icon" />
            </div>
            <div class="node-label">{{ nodeType.label }}</div>
          </div>
        </div>
      </div>

      <!-- 中间设计器区域 -->
      <div class="designer-main">
        <div class="designer-header">
          <div class="scheme-info">
            <a-input
              v-model:value="schemeName"
              placeholder="请输入流程名称"
              class="scheme-name-input"
            />
            <hbt-select
              v-model:value="schemeCategory"
              dict-type="workflow_scheme_category"
              placeholder="请选择流程分类"
              style="width: 150px"
            />
            <hbt-select
              v-model:value="selectedFormId"
              placeholder="请选择关联表单"
              style="width: 200px"
              :loading="formLoading"
              :options="formSelectOptions"
              allow-clear
            />
          </div>
          <div class="designer-actions">
            <a-button @click="handleClear">清空画布</a-button>
            <a-button @click="handleImport">导入流程</a-button>
            <a-button @click="handleExport">导出流程</a-button>
          </div>
        </div>
        
        <!-- 流程设计器 -->
        <div class="flow-container">
          <hbt-flow
            ref="flowRef"
            v-model:value="workflowConfig"
            :readonly="false"
            :height="'600px'"
            @node-click="handleNodeClick"
            @node-dblclick="handleNodeDblClick"
            @edge-click="handleEdgeClick"
            @canvas-click="handleCanvasClick"
          />
        </div>
      </div>

      <!-- 右侧属性面板 -->
      <div class="property-panel">
        <div class="panel-header">
          <h3>属性配置</h3>
        </div>
        <div class="property-content">
          <!-- 流程定义属性 -->
          <div v-if="!selectedNode && !selectedEdge" class="scheme-property">
            <h4>流程定义属性</h4>
            <a-form :model="schemeForm" layout="vertical">
              <a-form-item label="流程键">
                <a-input v-model:value="schemeForm.schemeKey" placeholder="请输入流程键" />
              </a-form-item>
              <a-form-item label="流程名称">
                <a-input v-model:value="schemeForm.schemeName" placeholder="请输入流程名称" />
              </a-form-item>
              <a-form-item label="流程分类">
                <hbt-select
                  v-model:value="schemeForm.schemeCategory"
                  dict-type="workflow_scheme_category"
                  placeholder="请选择流程分类"
                />
              </a-form-item>
              <a-form-item label="流程版本">
                <a-input v-model:value="schemeForm.version" placeholder="请输入版本号" />
              </a-form-item>
              <a-form-item label="关联表单">
                <hbt-select
                  v-model:value="schemeForm.formId"
                  placeholder="请选择关联表单"
                  :loading="formLoading"
                  :options="formSelectOptions"
                  allow-clear
                />
              </a-form-item>
              <a-form-item label="流程描述">
                <a-textarea v-model:value="schemeForm.description" :rows="3" placeholder="请输入流程描述" />
              </a-form-item>
              <a-form-item label="备注">
                <a-textarea v-model:value="schemeForm.remark" :rows="2" placeholder="请输入备注" />
              </a-form-item>
            </a-form>
          </div>

          <!-- 节点属性 -->
          <div v-else-if="selectedNode" class="node-property">
            <h4>节点属性</h4>
            <a-form :model="selectedNode" layout="vertical">
              <a-form-item label="节点ID">
                <a-input v-model:value="selectedNode.id" disabled />
              </a-form-item>
              <a-form-item label="节点名称">
                <a-input v-model:value="selectedNode.name" placeholder="请输入节点名称" />
              </a-form-item>
              <a-form-item label="节点类型">
                <hbt-select
                  v-model:value="selectedNode.type"
                  dict-type="workflow_node_type"
                  placeholder="请选择节点类型"
                  disabled
                />
              </a-form-item>
              <a-form-item label="节点描述">
                <a-textarea v-model:value="selectedNode.description" :rows="3" placeholder="请输入节点描述" />
              </a-form-item>
              
              <!-- 任务节点特有属性 -->
              <template v-if="selectedNode.type === 'task'">
                <a-divider>任务配置</a-divider>
                <a-form-item label="审批人类型">
                  <hbt-select
                    v-model:value="selectedNode.approverType"
                    dict-type="workflow_approver_type"
                    placeholder="请选择审批人类型"
                  />
                </a-form-item>
                <a-form-item label="审批人配置">
                  <a-textarea v-model:value="selectedNode.approverConfig" :rows="3" placeholder="请输入审批人配置" />
                </a-form-item>
                <a-form-item label="超时时间(分钟)">
                  <a-input-number v-model:value="selectedNode.timeout" :min="0" placeholder="请输入超时时间" style="width: 100%" />
                </a-form-item>
                <a-form-item label="是否允许转办">
                  <a-switch v-model:checked="selectedNode.allowTransfer" />
                </a-form-item>
                <a-form-item label="是否允许撤回">
                  <a-switch v-model:checked="selectedNode.allowWithdraw" />
                </a-form-item>
              </template>

              <!-- 网关节点特有属性 -->
              <template v-if="selectedNode.type === 'gateway'">
                <a-divider>网关配置</a-divider>
                <a-form-item label="网关类型">
                  <hbt-select
                    v-model:value="selectedNode.gatewayType"
                    dict-type="workflow_gateway_type"
                    placeholder="请选择网关类型"
                  />
                </a-form-item>
                <a-form-item label="默认路径">
                  <a-input v-model:value="selectedNode.defaultPath" placeholder="请输入默认路径" />
                </a-form-item>
              </template>

              <!-- 位置信息 -->
              <a-divider>位置信息</a-divider>
              <a-row :gutter="8">
                <a-col :span="12">
                  <a-form-item label="X坐标">
                    <a-input-number v-model:value="selectedNode.x" :min="0" style="width: 100%" />
                  </a-form-item>
                </a-col>
                <a-col :span="12">
                  <a-form-item label="Y坐标">
                    <a-input-number v-model:value="selectedNode.y" :min="0" style="width: 100%" />
                  </a-form-item>
                </a-col>
              </a-row>
            </a-form>
          </div>

          <!-- 连线属性 -->
          <div v-else-if="selectedEdge" class="edge-property">
            <h4>连线属性</h4>
            <a-form :model="selectedEdge" layout="vertical">
              <a-form-item label="连线ID">
                <a-input v-model:value="selectedEdge.id" disabled />
              </a-form-item>
              <a-form-item label="连线标签">
                <a-input v-model:value="selectedEdge.label" placeholder="请输入连线标签" />
              </a-form-item>
              <a-form-item label="连线类型">
                <hbt-select
                  v-model:value="selectedEdge.type"
                  dict-type="workflow_edge_type"
                  placeholder="请选择连线类型"
                />
              </a-form-item>
              <a-form-item label="条件表达式">
                <a-textarea v-model:value="selectedEdge.condition" :rows="3" placeholder="请输入条件表达式" />
              </a-form-item>
              <a-form-item label="优先级">
                <a-input-number v-model:value="selectedEdge.priority" :min="1" :max="100" style="width: 100%" />
              </a-form-item>
              <a-form-item label="连线描述">
                <a-textarea v-model:value="selectedEdge.description" :rows="2" placeholder="请输入连线描述" />
              </a-form-item>
            </a-form>
          </div>
        </div>
      </div>
    </div>

    <!-- 保存对话框 -->
    <a-modal
      v-model:open="saveModalVisible"
      title="保存流程定义"
      @ok="handleSaveConfirm"
      @cancel="saveModalVisible = false"
    >
      <a-form :model="saveForm" layout="vertical">
        <a-form-item label="流程键" required>
          <a-input v-model:value="saveForm.schemeKey" placeholder="请输入流程键" />
        </a-form-item>
        <a-form-item label="流程名称" required>
          <a-input v-model:value="saveForm.schemeName" placeholder="请输入流程名称" />
        </a-form-item>
        <a-form-item label="流程分类" required>
          <hbt-select
            v-model:value="saveForm.schemeCategory"
            dict-type="workflow_scheme_category"
            placeholder="请选择流程分类"
          />
        </a-form-item>
        <a-form-item label="流程版本" required>
          <a-input v-model:value="saveForm.version" placeholder="请输入版本号" />
        </a-form-item>
        <a-form-item label="关联表单">
          <hbt-select
            v-model:value="saveForm.formId"
            placeholder="请选择关联表单"
            :loading="formLoading"
            :options="formSelectOptions"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="流程描述">
          <a-textarea v-model:value="saveForm.description" :rows="3" placeholder="请输入流程描述" />
        </a-form-item>
        <a-form-item label="备注">
          <a-textarea v-model:value="saveForm.remark" :rows="2" placeholder="请输入备注" />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 预览对话框 -->
    <a-modal
      v-model:open="previewModalVisible"
      title="流程预览"
      width="80%"
      :footer="null"
    >
      <div class="preview-container">
        <hbt-flow
          :value="workflowConfig"
          :readonly="true"
          :height="'500px'"
        />
      </div>
    </a-modal>

    <!-- 导入对话框 -->
    <a-modal
      v-model:open="importModalVisible"
      title="导入流程"
      @ok="handleImportConfirm"
      @cancel="importModalVisible = false"
    >
      <a-upload
        v-model:file-list="importFileList"
        :before-upload="beforeImportUpload"
        :max-count="1"
        accept=".json"
      >
        <a-button>
          <template #icon><UploadOutlined /></template>
          选择文件
        </a-button>
      </a-upload>
      <div class="upload-tip">
        <p>支持导入JSON格式的流程定义文件</p>
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'
import {
  SaveOutlined,
  EyeOutlined,
  SendOutlined,
  UndoOutlined,
  RedoOutlined,
  ZoomInOutlined,
  ZoomOutOutlined,
  FullscreenOutlined,
  PlayCircleOutlined,
  UserOutlined,
  ApartmentOutlined,
  BranchesOutlined,
  StopOutlined,
  UploadOutlined
} from '@ant-design/icons-vue'
import { createScheme, updateScheme } from '@/api/workflow/scheme'
import { getFormList } from '@/api/workflow/form'
import { useDictStore } from '@/stores/dict'
import type { HbtSchemeCreate } from '@/types/workflow/scheme'
import type { HbtForm } from '@/types/workflow/form'

// 字典store
const dictStore = useDictStore()

// 响应式数据
const flowRef = ref()
const schemeName = ref('')
const schemeCategory = ref(1)
const selectedFormId = ref<number | undefined>(undefined)
const workflowConfig = ref<any>({
  nodes: [],
  edges: []
})

const selectedNode = ref<any>(null)
const selectedEdge = ref<any>(null)
const saveModalVisible = ref(false)
const previewModalVisible = ref(false)
const importModalVisible = ref(false)
const importFileList = ref<any[]>([])

// 表单相关
const formLoading = ref(false)
const formOptions = ref<HbtForm[]>([])

// 保存表单
const saveForm = reactive<HbtSchemeCreate>({
  schemeKey: '',
  schemeName: '',
  schemeCategory: 1,
  version: '1.0',
  schemeConfig: '',
  formId: undefined,
  description: '',
  remark: ''
})

// 流程定义表单
const schemeForm = reactive({
  schemeKey: '',
  schemeName: '',
  schemeCategory: 1,
  version: '1.0',
  formId: undefined as number | undefined,
  description: '',
  remark: ''
})

// 节点类型定义
const nodeTypes = [
  { type: 'start', label: '开始节点', icon: PlayCircleOutlined },
  { type: 'task', label: '任务节点', icon: UserOutlined },
  { type: 'gateway', label: '网关节点', icon: BranchesOutlined },
  { type: 'end', label: '结束节点', icon: StopOutlined }
]

// 计算属性
const canPublish = computed(() => {
  return workflowConfig.value.nodes.length > 0 && schemeName.value
})

const canUndo = computed(() => {
  return flowRef.value?.canUndo() || false
})

const canRedo = computed(() => {
  return flowRef.value?.canRedo() || false
})

// 表单选择选项
const formSelectOptions = computed(() => {
  return formOptions.value.map(form => ({
    label: form.formName,
    value: form.formId,
    disabled: form.status !== 1 // 只有已发布的表单可选
  }))
})

// 方法
const loadDictData = async () => {
  await dictStore.loadDicts([
    'workflow_scheme_category',
    'workflow_node_type',
    'workflow_approver_type',
    'workflow_gateway_type',
    'workflow_edge_type'
  ])
}

const fetchFormOptions = async () => {
  formLoading.value = true
  try {
    const result = await getFormList({
      pageIndex: 1,
      pageSize: 1000,
      status: 1 // 只获取已发布的表单
    })
    if (result.data.code === 200) {
      formOptions.value = result.data.data.rows || []
    }
  } catch (error) {
    console.error('获取表单列表失败:', error)
    message.error('获取表单列表失败')
  } finally {
    formLoading.value = false
  }
}

const handleNodeDragStart = (nodeType: any) => {
  // 处理节点拖拽开始
  console.log('拖拽节点:', nodeType)
}

const handleNodeClick = (node: any) => {
  selectedNode.value = node
  selectedEdge.value = null
}

const handleNodeDblClick = (node: any) => {
  // 双击节点编辑
  console.log('双击节点:', node)
}

const handleEdgeClick = (edge: any) => {
  selectedEdge.value = edge
  selectedNode.value = null
}

const handleCanvasClick = () => {
  selectedNode.value = null
  selectedEdge.value = null
}

const handleSave = () => {
  if (!schemeName.value) {
    message.warning('请输入流程名称')
    return
  }
  if (workflowConfig.value.nodes.length === 0) {
    message.warning('请先设计流程')
    return
  }
  
  saveForm.schemeName = schemeName.value
  saveForm.schemeCategory = schemeCategory.value
  saveForm.formId = selectedFormId.value || undefined
  saveForm.schemeConfig = JSON.stringify(workflowConfig.value)
  saveModalVisible.value = true
}

const handleSaveConfirm = async () => {
  try {
    const result = await createScheme(saveForm)
    if (result.data.code === 200) {
      message.success('流程保存成功')
      saveModalVisible.value = false
    } else {
      message.error(result.data.msg || '保存失败')
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  }
}

const handlePreview = () => {
  if (workflowConfig.value.nodes.length === 0) {
    message.warning('请先设计流程')
    return
  }
  previewModalVisible.value = true
}

const handlePublish = () => {
  message.info('发布功能待实现')
}

const handleUndo = () => {
  flowRef.value?.undo()
}

const handleRedo = () => {
  flowRef.value?.redo()
}

const handleZoomIn = () => {
  flowRef.value?.zoomIn()
}

const handleZoomOut = () => {
  flowRef.value?.zoomOut()
}

const handleFitView = () => {
  flowRef.value?.fitView()
}

const handleClear = () => {
  workflowConfig.value = { nodes: [], edges: [] }
  selectedNode.value = null
  selectedEdge.value = null
  message.success('画布已清空')
}

const handleImport = () => {
  importModalVisible.value = true
}

const beforeImportUpload = (file: File) => {
  const isJson = file.type === 'application/json' || file.name.endsWith('.json')
  if (!isJson) {
    message.error('只能上传JSON文件!')
    return false
  }
  return false
}

const handleImportConfirm = () => {
  if (importFileList.value.length === 0) {
    message.warning('请选择要导入的文件')
    return
  }
  
  const file = importFileList.value[0].originFileObj
  const reader = new FileReader()
  reader.onload = (e) => {
    try {
      const config = JSON.parse(e.target?.result as string)
      workflowConfig.value = config
      message.success('导入成功')
      importModalVisible.value = false
      importFileList.value = []
    } catch (error) {
      message.error('文件格式错误')
    }
  }
  reader.readAsText(file)
}

const handleExport = () => {
  if (workflowConfig.value.nodes.length === 0) {
    message.warning('请先设计流程')
    return
  }
  
  const dataStr = JSON.stringify(workflowConfig.value, null, 2)
  const dataBlob = new Blob([dataStr], { type: 'application/json' })
  const url = URL.createObjectURL(dataBlob)
  const link = document.createElement('a')
  link.href = url
  link.download = `${schemeName.value || 'workflow'}.json`
  link.click()
  URL.revokeObjectURL(url)
  message.success('导出成功')
}

// 监听表单信息变化
watch(schemeName, (newVal) => {
  schemeForm.schemeName = newVal
})

watch(schemeCategory, (newVal) => {
  schemeForm.schemeCategory = newVal
})

watch(selectedFormId, (newVal) => {
  schemeForm.formId = newVal || undefined
})

onMounted(async () => {
  // 加载字典数据
  await loadDictData()
  
  // 获取表单列表
  await fetchFormOptions()
  
  // 初始化默认流程配置
  if (!workflowConfig.value.nodes.length) {
    workflowConfig.value = {
      nodes: [
        {
          id: 'start',
          name: '开始',
          type: 'start',
          x: 100,
          y: 100
        },
        {
          id: 'end',
          name: '结束',
          type: 'end',
          x: 500,
          y: 100
        }
      ],
      edges: [
        {
          id: 'start-end',
          source: 'start',
          target: 'end',
          label: '流程'
        }
      ]
    }
  }
})
</script>

<style lang="less" scoped>
.workflow-designer-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.designer-toolbar {
  height: 50px;
  background-color: #fff;
  border-bottom: 1px solid #d9d9d9;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 16px;

  .toolbar-left,
  .toolbar-right {
    display: flex;
    gap: 8px;
  }
}

.designer-content {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.node-panel {
  width: 200px;
  background-color: #fff;
  border-right: 1px solid #d9d9d9;
  display: flex;
  flex-direction: column;

  .panel-header {
    padding: 12px 16px;
    border-bottom: 1px solid #d9d9d9;
    
    h3 {
      margin: 0;
      font-size: 14px;
      font-weight: 500;
    }
  }

  .node-list {
    flex: 1;
    padding: 16px;

    .node-item {
      display: flex;
      align-items: center;
      padding: 8px 12px;
      margin-bottom: 8px;
      border: 1px solid #d9d9d9;
      border-radius: 4px;
      cursor: grab;
      transition: all 0.3s;

      &:hover {
        border-color: #1890ff;
        background-color: #f0f8ff;
      }

      .node-icon {
        margin-right: 8px;
        font-size: 16px;
        color: #666;
      }

      .node-label {
        font-size: 12px;
        color: #333;
      }
    }
  }
}

.designer-main {
  flex: 1;
  display: flex;
  flex-direction: column;
  background-color: #fff;
}

.designer-header {
  height: 60px;
  border-bottom: 1px solid #d9d9d9;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 16px;

  .scheme-info {
    display: flex;
    align-items: center;
    gap: 16px;

    .scheme-name-input {
      width: 200px;
    }
  }

  .designer-actions {
    display: flex;
    gap: 8px;
  }
}

.flow-container {
  flex: 1;
  padding: 16px;
  overflow: hidden;
}

.property-panel {
  width: 350px;
  background-color: #fff;
  border-left: 1px solid #d9d9d9;
  display: flex;
  flex-direction: column;

  .panel-header {
    padding: 12px 16px;
    border-bottom: 1px solid #d9d9d9;
    
    h3 {
      margin: 0;
      font-size: 14px;
      font-weight: 500;
    }
  }

  .property-content {
    flex: 1;
    padding: 16px;
    overflow-y: auto;

    .scheme-property,
    .node-property,
    .edge-property {
      h4 {
        margin-bottom: 16px;
        font-size: 14px;
        font-weight: 500;
      }
    }
  }
}

.preview-container {
  .hbt-flow {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}

.upload-tip {
  margin-top: 16px;
  color: #666;
  font-size: 12px;
}
</style> 