<template>
  <div class="workflow-config-container">
    <a-button type="primary" @click="handleOpen">
      <template #icon><ApiOutlined /></template>
      配置工作流
    </a-button>
    <a-modal
      v-model:open="visible"
      title="工作流配置"
      :width="1700"
      :footer="null"
      @cancel="handleClose"
    >

        <div class="workflow-editor" ref="container"></div>

    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted, onUnmounted, nextTick } from 'vue'
import { ApiOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import LogicFlow from "@logicflow/core";
import "@logicflow/core/lib/style/index.css";
import { BpmnElement, Snapshot, Control, Menu, SelectionSelect, DndPanel } from '@logicflow/extension'
import { StartEventView, EndEventView, UserTaskView, ServiceTaskView, ExclusiveGatewayView } from '@logicflow/extension/lib/bpmn'

// 定义节点类型
interface WorkflowNode {
  id: string
  type: string
  x: number
  y: number
  text?: string
  properties?: Record<string, any>
}

// 定义边类型
interface WorkflowEdge {
  id: string
  type: string
  sourceNodeId: string
  targetNodeId: string
  text?: string
  properties?: Record<string, any>
}

// 定义图数据类型
interface GraphData {
  nodes: WorkflowNode[]
  edges: WorkflowEdge[]
}

const props = defineProps<{
  value?: string
}>()

const emit = defineEmits<{
  (e: 'update:value', value: string): void
}>()

const visible = ref(false)
const container = ref<HTMLElement>()
let lf: LogicFlow | null = null

const initLogicFlow = () => {
  if (!container.value) return
  if (lf) return // 避免重复初始化
  lf = new LogicFlow({
    container: container.value,
    grid: true,
    plugins: [BpmnElement, Snapshot, Control, Menu, SelectionSelect, DndPanel],
    nodeTextEdit: true,
    edgeTextEdit: true,
    width: 1600,
    height: 900,
    isShowControl: true,
    dndPanel: {
      container: document.querySelector('.lf-dndpanel') as HTMLElement
    },
    style: {
      rect: { width: 120, height: 60, radius: 5 },
      circle: { r: 30 },
      diamond: { width: 80, height: 80 }
    }
  })
  registerNodes()
  registerEvents()
  lf.render({ nodes: [], edges: [] })
}

// 注册自定义节点
const registerNodes = () => {
  // 开始节点
  lf?.register({
    type: 'start',
    view: StartEventView,
    model: (BpmnElement as any).StartEvent
  })

  // 结束节点
  lf?.register({
    type: 'end',
    view: EndEventView,
    model: (BpmnElement as any).EndEvent
  })

  // 用户任务节点
  lf?.register({
    type: 'userTask',
    view: UserTaskView,
    model: (BpmnElement as any).UserTask
  })

  // 服务任务节点
  lf?.register({
    type: 'serviceTask',
    view: ServiceTaskView,
    model: (BpmnElement as any).ServiceTask
  })

  // 网关节点
  lf?.register({
    type: 'gateway',
    view: ExclusiveGatewayView,
    model: (BpmnElement as any).ExclusiveGateway
  })
}

// 注册事件
const registerEvents = () => {
  // 节点点击事件
  lf?.on('node:click', ({ data }) => {
    console.log('节点点击:', data)
  })

  // 边点击事件
  lf?.on('edge:click', ({ data }) => {
    console.log('边点击:', data)
  })

  // 画布点击事件
  lf?.on('blank:click', () => {
    console.log('画布点击')
  })

  // 节点拖拽事件
  lf?.on('node:drag', ({ data }) => {
    console.log('节点拖拽:', data)
  })

  // 边连接事件
  lf?.on('edge:add', ({ data }) => {
    console.log('边连接:', data)
  })
}

// 监听value变化
watch(() => props.value, (newVal) => {
  if (newVal && lf) {
    try {
      const config = JSON.parse(newVal)
      // 清空现有数据
      lf?.clearData()
      // 渲染新配置
      lf?.render(config)
      // 适应画布
      lf?.fitView()
    } catch (error) {
      console.error('解析工作流配置失败:', error)
    }
  }
}, { immediate: true })

// 监听visible变化
watch(() => visible.value, (newVal) => {
  if (newVal) {
    nextTick(() => {
      initLogicFlow()
      // 如果有初始值，同步到设计器
      if (props.value && lf) {
        try {
          const config = JSON.parse(props.value)
          lf?.render(config)
          lf?.fitView()
        } catch (error) {
          console.error('解析工作流配置失败:', error)
        }
      }
    })
  }
})

// 打开配置
const handleOpen = () => {
  visible.value = true
  nextTick(() => {
    initLogicFlow()
    // 如果有初始值，同步到设计器
    if (props.value && lf) {
      try {
        const config = JSON.parse(props.value)
        lf?.render(config)
        lf?.fitView()
      } catch (error) {
        console.error('解析工作流配置失败:', error)
      }
    }
  })
}

// 关闭配置
const handleClose = () => {
  visible.value = false
}

// 保存配置
const handleSave = () => {
  try {
    const graphData = lf ? lf.getGraphData() as GraphData : null
    // 验证流程配置
    if (!graphData || !graphData.nodes || graphData.nodes.length === 0) {
      message.warning('请先设计工作流')
      return
    }
    // 验证是否包含开始和结束节点
    const hasStart = graphData.nodes.some((node: WorkflowNode) => node.type === 'start')
    const hasEnd = graphData.nodes.some((node: WorkflowNode) => node.type === 'end')
    if (!hasStart) {
      message.warning('工作流必须包含开始节点')
      return
    }
    if (!hasEnd) {
      message.warning('工作流必须包含结束节点')
      return
    }
    emit('update:value', JSON.stringify(graphData))
    message.success('保存成功')
    handleClose()
  } catch (error) {
    console.error('保存工作流配置失败:', error)
    message.error('保存失败')
  }
}

// 清空画布
const handleClear = () => {
  lf?.clearData()
}

// 撤销
const handleUndo = () => {
  lf?.undo()
}

// 重做
const handleRedo = () => {
  lf?.redo()
}

// 放大
const handleZoomIn = () => {
  lf?.zoom(true)
}

// 缩小
const handleZoomOut = () => {
  lf?.zoom(false)
}

// 适应画布
const handleFitView = () => {
  lf?.fitView()
}

// 组件卸载时销毁实例
onUnmounted(() => {
  if (lf) {
    lf.destroy()
    lf = null
  }
})
</script>
<style lang="less" scoped>
.workflow-config-container {
  width: 100%;
}

.workflow-config-content {
  display: flex;
}

.lf-dndpanel {
  width: 80px;
  margin-right: 16px;
}

.workflow-editor {
  flex: 1;
  height: 900px;
  border: 1px solid #ddd;
  border-radius: 4px;
}
</style> 
