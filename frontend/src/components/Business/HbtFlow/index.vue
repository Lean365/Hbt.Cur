<template>
  <div class="flow-root">
    <div class="flow-canvas-wrap">
      <div class="node-palette">
        <a-tooltip title="开始" placement="right">
          <div 
            class="palette-item" 
            :class="{ 'disabled': nodeStatus.hasStart }"
            @mousedown="e => startDrag('start', e)"
          >
            <span class="palette-icon start-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="任务" placement="right">
          <div class="palette-item" @mousedown="e => startDrag('task', e)">
            <span class="palette-icon task-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="网关" placement="right">
          <div class="palette-item" @mousedown="e => startDrag('gateway', e)">
            <span class="palette-icon gateway-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="结束" placement="right">
          <div 
            class="palette-item" 
            :class="{ 'disabled': nodeStatus.hasEnd }"
            @mousedown="e => startDrag('end', e)"
          >
            <span class="palette-icon end-icon"></span>
          </div>
        </a-tooltip>
      </div>
      <div id="container" class="flow-canvas"></div>
      <NodePropertyPanel
        :visible="propertyPanelVisible"
        :node="selectedNode"
        :onSave="handleNodePropertySave"
        :onCancel="handleNodePropertyCancel"
      />
      <div class="canvas-ops-panel">
        <a-tooltip title="导入" placement="left">
          <button class="ops-btn" @click="handleImport">
            <span class="ops-icon import-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="导出" placement="left">
          <button class="ops-btn" @click="handleExport">
            <span class="ops-icon export-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="放大" placement="left">
          <button class="ops-btn" @click="handleZoomIn">
            <span class="ops-icon zoom-in-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="缩小" placement="left">
          <button class="ops-btn" @click="handleZoomOut">
            <span class="ops-icon zoom-out-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="重置" placement="left">
          <button class="ops-btn" @click="handleReset">
            <span class="ops-icon reset-icon"></span>
          </button>
        </a-tooltip>
      </div>
      <div
        v-if="contextMenu.visible"
        class="context-menu"
        :style="{ left: contextMenu.x + 'px', top: contextMenu.y + 'px' }"
        @contextmenu.prevent
      >
        <template v-if="contextMenu.type === 'edge'">
          <div class="context-menu-item" @click="deleteEdge">删除</div>
        </template>
        <template v-else-if="contextMenu.type === 'node'">
          <div class="context-menu-item" @click="showNodeProps">属性</div>
          <div class="context-menu-item" @click="deleteNode">删除</div>
        </template>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, nextTick, watch } from 'vue'
import { Graph } from '@antv/x6'
import { Dnd } from '@antv/x6-plugin-dnd'
import NodePropertyPanel from './components/NodePropertyPanel.vue'
import { message } from 'ant-design-vue'

// Props 和 Emits 定义
const props = defineProps({
  value: { type: Object, default: () => ({}) },
  width: { type: Number, default: 1600 },
  height: { type: Number, default: 800 }
})

const emit = defineEmits(['update:value'])

const container = ref(null)
let graph = null
let dnd = null

// 属性面板相关
const propertyPanelVisible = ref(false)
const selectedNode = ref(null)

// 右键菜单相关
const contextMenu = ref({
  visible: false,
  x: 0,
  y: 0,
  type: '', // 'node' | 'edge'
  target: null
})

// 节点状态管理
const nodeStatus = ref({
  hasStart: false,
  hasEnd: false
})

// 内部流程数据
const localConfig = ref(props.value || {})

// 监听父传入的 value，变化时同步到本地
watch(() => props.value, (val) => {
  if (val && JSON.stringify(val) !== JSON.stringify(localConfig.value)) {
    localConfig.value = val
    // 如果有初始数据，加载到画布
    if (graph && val.nodes && val.edges) {
      loadWorkflowData(val)
    }
  }
}, { deep: true })

// 检查节点状态
const checkNodeStatus = () => {
  if (!graph) return
  
  const nodes = graph.getNodes()
  nodeStatus.value.hasStart = nodes.some(node => 
    node.shape === 'circle' && node.attr('body/fill') === '#52c41a'
  )
  nodeStatus.value.hasEnd = nodes.some(node => 
    node.shape === 'circle' && node.attr('body/fill') === '#ff4d4f'
  )
}

const nodeTemplates = {
  start: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: {
        r: 30,
        fill: '#52c41a',
        stroke: '#389e0d',
        strokeWidth: 2
      },
      label: {
        text: '开始',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        out: {
          position: 'right',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#52c41a',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'out1', group: 'out' }
      ]
    }
  },
  task: {
    shape: 'rect',
    width: 100,
    height: 40,
    attrs: {
      body: {
        fill: '#1890ff',
        stroke: '#096dd9',
        strokeWidth: 2,
        rx: 8,
        ry: 8
      },
      label: {
        text: '任务',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        top: {
          position: 'top',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        right: {
          position: 'right',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        bottom: {
          position: 'bottom',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        left: {
          position: 'left',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'top', group: 'top' },
        { id: 'right', group: 'right' },
        { id: 'bottom', group: 'bottom' },
        { id: 'left', group: 'left' }
      ]
    }
  },
  gateway: {
    shape: 'polygon',
    width: 80,
    height: 80,
    attrs: {
      body: {
        refPoints: '0,10 10,0 20,10 10,20',
        fill: '#faad14',
        stroke: '#d48806',
        strokeWidth: 2
      },
      label: {
        text: '网关',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        top: {
          position: 'top',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        right: {
          position: 'right',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        bottom: {
          position: 'bottom',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        left: {
          position: 'left',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'top', group: 'top' },
        { id: 'right', group: 'right' },
        { id: 'bottom', group: 'bottom' },
        { id: 'left', group: 'left' }
      ]
    }
  },
  end: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: {
        r: 30,
        fill: '#ff4d4f',
        stroke: '#cf1322',
        strokeWidth: 2
      },
      label: {
        text: '结束',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        in: {
          position: 'left',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#ff4d4f',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'end-in', group: 'in' }
      ]
    }
  }
}

function startDrag(type, e) {
  if (!graph || !dnd) return

  // 唯一性校验：开始和结束节点只能有一个
  if (type === 'start' || type === 'end') {
    const exist = graph.getNodes().some(node => {
      // 根据节点形状判断类型
      if (type === 'start') {
        // 开始节点是绿色圆形
        return node.shape === 'circle' && 
               node.attr('body/fill') === '#52c41a'
      } else if (type === 'end') {
        // 结束节点是红色圆形
        return node.shape === 'circle' && 
               node.attr('body/fill') === '#ff4d4f'
      }
      return false
    })
    if (exist) {
      message.warning(`只能有一个${type === 'start' ? '开始' : '结束'}节点`)
      return
    }
  }

  const config = nodeTemplates[type]
  if (!config) return
  const node = graph.createNode(config)
  dnd.start(node, e)
  
  // 更新节点状态
  checkNodeStatus()
}

function handleImport() {
  // 创建文件输入元素
  const input = document.createElement('input')
  input.type = 'file'
  input.accept = '.json'
  input.style.display = 'none'
  
  input.onchange = (e) => {
    const file = e.target.files[0]
    if (!file) return
    
    const reader = new FileReader()
    reader.onload = (event) => {
      try {
        const data = JSON.parse(event.target.result)
        
        // 清空现有图形
        graph.clearCells()
        
        // 导入节点
        if (data.nodes && Array.isArray(data.nodes)) {
          data.nodes.forEach(nodeData => {
            const node = graph.addNode({
              id: nodeData.id,
              shape: nodeData.shape,
              x: nodeData.x,
              y: nodeData.y,
              width: nodeData.width,
              height: nodeData.height,
              attrs: nodeData.attrs,
              ports: nodeData.ports,
              data: nodeData.data || {}
            })
          })
        }
        
        // 导入边
        if (data.edges && Array.isArray(data.edges)) {
          data.edges.forEach(edgeData => {
            graph.addEdge({
              id: edgeData.id,
              source: edgeData.source,
              target: edgeData.target,
              sourcePort: edgeData.sourcePort,
              targetPort: edgeData.targetPort,
              attrs: edgeData.attrs || {}
            })
          })
        }
        
        message.success('流程导入成功')
        console.log('[流程设计器] 导入数据:', data)
      } catch (error) {
        console.error('[流程设计器] 导入失败:', error)
        message.error('导入失败：文件格式错误')
      }
    }
    
    reader.readAsText(file)
  }
  
  document.body.appendChild(input)
  input.click()
  document.body.removeChild(input)
}

function handleExport() {
  if (!graph) {
    message.warning('没有可导出的数据')
    return
  }
  
  // 检查是否有节点
  const nodes = graph.getNodes()
  if (nodes.length === 0) {
    message.warning('空白流程不能导出')
    return
  }
  
  try {
    // 获取所有节点
    const nodeData = nodes.map(node => ({
      id: node.id,
      shape: node.shape,
      x: node.getPosition().x,
      y: node.getPosition().y,
      width: node.getSize().width,
      height: node.getSize().height,
      attrs: node.getAttrs(),
      ports: node.getPorts(),
      data: node.getData()
    }))
    
    // 获取所有边
    const edges = graph.getEdges().map(edge => ({
      id: edge.id,
      source: edge.getSource(),
      target: edge.getTarget(),
      sourcePort: edge.getSourcePortId(),
      targetPort: edge.getTargetPortId(),
      attrs: edge.getAttrs()
    }))
    
    // 构建导出数据
    const exportData = {
      version: '1.0',
      timestamp: new Date().toISOString(),
      nodes: nodeData,
      edges,
      metadata: {
        nodeCount: nodeData.length,
        edgeCount: edges.length,
        exportTime: new Date().toLocaleString()
      }
    }
    
    // 创建下载链接
    const dataStr = JSON.stringify(exportData, null, 2)
    const dataBlob = new Blob([dataStr], { type: 'application/json' })
    const url = URL.createObjectURL(dataBlob)
    
    const link = document.createElement('a')
    link.href = url
    link.download = `workflow_${new Date().getTime()}.json`
    link.style.display = 'none'
    
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    
    URL.revokeObjectURL(url)
    
    message.success('流程导出成功')
    console.log('[流程设计器] 导出数据:', exportData)
  } catch (error) {
    console.error('[流程设计器] 导出失败:', error)
    message.error('导出失败：' + error.message)
  }
}

function handleZoomIn() {
  if (graph) graph.zoom(0.1)
}
function handleZoomOut() {
  if (graph) graph.zoom(-0.1)
}
function handleReset() {
  if (graph) {
    // 清空画布
    graph.clearCells()
    // 重置缩放
    graph.zoomTo(1)
    graph.centerContent()
    // 重新创建默认流程
    createDefaultWorkflow()
    message.success('已重置为默认流程')
  }
}

function deleteEdge() {
  if (contextMenu.value.target && graph) {
    graph.removeEdge(contextMenu.value.target)
  }
  contextMenu.value.visible = false
  updateWorkflowConfig()
}
function deleteNode() {
  if (contextMenu.value.target && graph) {
    graph.removeNode(contextMenu.value.target)
    // 更新节点状态
    checkNodeStatus()
  }
  contextMenu.value.visible = false
  updateWorkflowConfig()
}
function showNodeProps() {
  const node = contextMenu.value.target
  selectedNode.value = {
    nodeId: node.id,
    nodeName: node.attr('label/text'),
    ...node.getData()
  }
  propertyPanelVisible.value = true
  contextMenu.value.visible = false
}

function closeContextMenu() {
  contextMenu.value.visible = false
}

function handleNodePropertySave(data) {
  if (graph && data && data.nodeId) {
    const node = graph.getCellById(data.nodeId)
    if (node) {
      node.setData({ ...data })
      if (data.nodeName) {
        node.attr('label/text', data.nodeName)
      }
    }
  }
  propertyPanelVisible.value = false
  updateWorkflowConfig()
}
function handleNodePropertyCancel() {
  propertyPanelVisible.value = false
}

function createDefaultWorkflow() {
  // 创建开始节点
  const startNode = graph.addNode({
    ...nodeTemplates.start,
    x: 100,
    y: 100
  })

  // 创建第一个任务节点
  const taskNode1 = graph.addNode({
    ...nodeTemplates.task,
    x: 250,
    y: 100
  })

  // 创建第一个网关节点
  const gatewayNode1 = graph.addNode({
    ...nodeTemplates.gateway,
    x: 400,
    y: 100
  })

  // 创建第二个任务节点
  const taskNode2 = graph.addNode({
    ...nodeTemplates.task,
    x: 550,
    y: 100
  })

  // 创建第二个网关节点
  const gatewayNode2 = graph.addNode({
    ...nodeTemplates.gateway,
    x: 700,
    y: 100
  })

  // 创建结束节点
  const endNode = graph.addNode({
    ...nodeTemplates.end,
    x: 850,
    y: 100
  })

  // 创建边：开始 → 任务1 → 网关1 → 任务2 → 网关2 → 结束
  graph.addEdge({
    source: startNode.id,
    target: taskNode1.id,
    sourcePort: 'out1',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: taskNode1.id,
    target: gatewayNode1.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: gatewayNode1.id,
    target: taskNode2.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: taskNode2.id,
    target: gatewayNode2.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: gatewayNode2.id,
    target: endNode.id,
    sourcePort: 'right',
    targetPort: 'end-in'
  })

  // 添加驳回路径：任务1 → 结束（驳回）
  graph.addEdge({
    source: taskNode1.id,
    target: endNode.id,
    sourcePort: 'bottom',
    targetPort: 'end-in'
  })

  // 更新节点状态
  checkNodeStatus()
}

// 导出当前流程图数据
const exportCurrentWorkflowData = () => {
  if (!graph) return {}
  
  const nodes = graph.getNodes()
  const edges = graph.getEdges()
  
  const nodeData = nodes.map(node => ({
    id: node.id,
    shape: node.shape,
    x: node.getPosition().x,
    y: node.getPosition().y,
    width: node.getSize().width,
    height: node.getSize().height,
    attrs: node.getAttrs(),
    ports: node.getPorts(),
    data: node.getData()
  }))
  
  const edgeData = edges.map(edge => ({
    id: edge.id,
    source: edge.getSource(),
    target: edge.getTarget(),
    sourcePort: edge.getSourcePortId(),
    targetPort: edge.getTargetPortId(),
    attrs: edge.getAttrs()
  }))
  
  return {
    version: '1.0',
    timestamp: new Date().toISOString(),
    nodes: nodeData,
    edges: edgeData,
    metadata: {
      nodeCount: nodeData.length,
      edgeCount: edgeData.length,
      exportTime: new Date().toLocaleString()
    }
  }
}

// 加载流程图数据到画布
const loadWorkflowData = (data) => {
  if (!graph || !data) return
  
  // 清空现有图形
  graph.clearCells()
  
  // 导入节点
  if (data.nodes && Array.isArray(data.nodes)) {
    data.nodes.forEach(nodeData => {
      graph.addNode({
        id: nodeData.id,
        shape: nodeData.shape,
        x: nodeData.x,
        y: nodeData.y,
        width: nodeData.width,
        height: nodeData.height,
        attrs: nodeData.attrs,
        ports: nodeData.ports,
        data: nodeData.data || {}
      })
    })
  }
  
  // 导入边
  if (data.edges && Array.isArray(data.edges)) {
    data.edges.forEach(edgeData => {
      graph.addEdge({
        id: edgeData.id,
        source: edgeData.source,
        target: edgeData.target,
        sourcePort: edgeData.sourcePort,
        targetPort: edgeData.targetPort,
        attrs: edgeData.attrs || {}
      })
    })
  }
  
  // 更新节点状态
  checkNodeStatus()
}

// 更新工作流配置并同步到父组件
const updateWorkflowConfig = () => {
  const data = exportCurrentWorkflowData()
  localConfig.value = data
  emit('update:value', data)
}

onMounted(() => {
  graph = new Graph({
    container: document.getElementById('container'),
    autoResize: true,
    background: { color: '#f5f6fa' },
    grid: { size: 10, visible: true, type: 'dot' },
    panning: true,
    mousewheel: true,
    connecting: {
      sourceAnchor: 'right',
      targetAnchor: 'left',
      connectionPoint: 'anchor',
      allowBlank: false,
      allowLoop: false,
      highlight: true,
      connector: 'rounded',
      router: {
        name: 'manhattan',
        args: {
          padding: 10
        }
      }
    }
  })
  dnd = new Dnd({ target: graph })

  // 初始化节点状态
  checkNodeStatus()

  // 监听节点变化
  graph.on('node:added', () => {
    checkNodeStatus()
    updateWorkflowConfig()
  })
  
  graph.on('node:removed', () => {
    checkNodeStatus()
    updateWorkflowConfig()
  })

  // 监听边变化
  graph.on('edge:connected', () => {
    updateWorkflowConfig()
  })

  graph.on('edge:disconnected', () => {
    updateWorkflowConfig()
  })

  // 监听节点移动
  graph.on('node:moved', () => {
    updateWorkflowConfig()
  })

  // 如果有初始数据，加载到画布
  if (props.value && props.value.nodes && props.value.edges) {
    loadWorkflowData(props.value)
  } else {
    // 创建默认流程
    createDefaultWorkflow()
  }

  // 右键连接线弹菜单
  graph.on('edge:contextmenu', ({ edge, e }) => {
    e.preventDefault()
    contextMenu.value = {
      visible: true,
      x: e.clientX,
      y: e.clientY,
      type: 'edge',
      target: edge
    }
  })
  // 右键节点弹菜单
  graph.on('node:contextmenu', ({ node, e }) => {
    e.preventDefault()
    contextMenu.value = {
      visible: true,
      x: e.clientX,
      y: e.clientY,
      type: 'node',
      target: node
    }
  })
  // 点击其它区域关闭菜单
  document.addEventListener('click', closeContextMenu)
})
</script>

<style scoped>
.flow-root {
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: stretch;
  justify-content: flex-start;
  background: #f5f6fa;
}
.flow-canvas-wrap {
  flex: 1;
  min-width: 400px;
  min-height: 400px;
  max-width: 100vw;
  max-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
}
.flow-canvas {
  width: 100%;
  height: 100%;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 12px rgba(0,0,0,0.08);
  display: block;
}
.node-palette {
  position: absolute;
  top: 64px;
  left: 24px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  z-index: 10;
}
.palette-item {
  width: 48px;
  height: 48px;
  background: #f0f5ff;
  border: 1px solid #e6e6e6;
  border-radius: 12px;
  color: #1890ff;
  font-size: 14px;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
  box-sizing: border-box;
}
.palette-item:hover {
  background: #e6f7ff;
  border-color: #91d5ff;
}
.palette-item.disabled {
  opacity: 0.5;
  cursor: not-allowed;
  background: #f5f5f5;
  border-color: #d9d9d9;
}
.palette-item.disabled:hover {
  background: #f5f5f5;
  border-color: #d9d9d9;
  transform: none;
}
.canvas-ops-panel {
  position: absolute;
  top: 64px;
  right: 24px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  z-index: 10;
}
.ops-btn {
  width: 48px;
  height: 48px;
  background: #f0f5ff;
  border: 1px solid #e6e6e6;
  border-radius: 8px;
  color: #1890ff;
  font-size: 32px;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
}
.ops-btn:hover {
  background: #e6f7ff;
  border-color: #91d5ff;
  transform: translateY(-1px);
  box-shadow: 0 2px 8px rgba(24, 144, 255, 0.2);
}
.palette-icon {
  width: 32px;
  height: 32px;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}
.start-icon {
  background: #52c41a;
  border-radius: 50%;
  width: 32px;
  height: 32px;
}
.task-icon {
  background: #1890ff;
  border-radius: 8px;
  width: 32px;
  height: 20px;
  margin: 6px 0;
}
.gateway-icon {
  width: 32px;
  height: 32px;
  background: #faad14;
  clip-path: polygon(50% 0%, 100% 50%, 50% 100%, 0% 50%);
}
.end-icon {
  background: #ff4d4f;
  border-radius: 50%;
  width: 32px;
  height: 32px;
  position: relative;
}
.end-icon::after {
  content: '';
  display: block;
  width: 16px;
  height: 3px;
  background: #fff;
  position: absolute;
  left: 8px;
  top: 14.5px;
  border-radius: 1.5px;
}
.ops-icon {
  width: 32px;
  height: 32px;
  display: inline-block;
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
}
.import-icon {
  background-color: #52c41a;
  border-radius: 4px;
  position: relative;
}
.import-icon::before {
  content: '↓';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.export-icon {
  background-color: #1890ff;
  border-radius: 4px;
  position: relative;
}
.export-icon::before {
  content: '↑';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.zoom-in-icon {
  background-color: #faad14;
  border-radius: 50%;
  position: relative;
}
.zoom-in-icon::before {
  content: '+';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 18px;
  font-weight: bold;
}
.zoom-out-icon {
  background-color: #722ed1;
  border-radius: 50%;
  position: relative;
}
.zoom-out-icon::before {
  content: '−';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 18px;
  font-weight: bold;
}
.reset-icon {
  background-color: #ff4d4f;
  border-radius: 4px;
  position: relative;
}
.reset-icon::before {
  content: '↺';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.context-menu {
  position: fixed;
  z-index: 9999;
  background: #fff;
  border: 1px solid #e6e6e6;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.15);
  min-width: 100px;
  padding: 4px 0;
}
.context-menu-item {
  padding: 8px 16px;
  cursor: pointer;
}
.context-menu-item:hover {
  background: #f5f5f5;
}
</style>
