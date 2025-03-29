<template>
  <div class="server-monitor">
    <!-- 基本信息卡片 -->
    <a-row :gutter="[16, 16]">
      <a-col :span="6">
        <a-card class="monitor-card" :loading="loading.server">
          <template #title>
            <span>
              <desktop-outlined /> CPU使用率
            </span>
          </template>
          <div class="card-content">
            <a-progress
              type="dashboard"
              :percent="serverInfo.cpuUsage"
              :format="(percent: number) => `${percent}%`"
              :status="getStatusByPercent(serverInfo.cpuUsage)"
            />
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card class="monitor-card" :loading="loading.server">
          <template #title>
            <span>
              <monitor-outlined /> 内存使用率
            </span>
          </template>
          <div class="card-content">
            <a-progress
              type="dashboard"
              :percent="serverInfo.memoryUsage"
              :format="(percent: number) => `${percent}%`"
              :status="getStatusByPercent(serverInfo.memoryUsage)"
            />
            <div class="detail-text">
              已用: {{ serverInfo.usedMemory.toFixed(2) }}GB / 总计: {{ serverInfo.totalMemory.toFixed(2) }}GB
            </div>
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card class="monitor-card" :loading="loading.server">
          <template #title>
            <span>
              <hdd-outlined /> 磁盘使用率
            </span>
          </template>
          <div class="card-content">
            <a-progress
              type="dashboard"
              :percent="serverInfo.diskUsage"
              :format="(percent: number) => `${percent}%`"
              :status="getStatusByPercent(serverInfo.diskUsage)"
            />
            <div class="detail-text">
              已用: {{ serverInfo.usedDiskSpace.toFixed(2) }}GB / 总计: {{ serverInfo.totalDiskSpace.toFixed(2) }}GB
            </div>
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card class="monitor-card" :loading="loading.server">
          <template #title>
            <span>
              <clock-circle-outlined /> 系统运行时间
            </span>
          </template>
          <div class="card-content center-content">
            <div class="uptime-info">
              <p>启动时间: {{ formatDateTime(serverInfo.systemStartTime) }}</p>
              <p>运行天数: {{ serverInfo.systemUptime.toFixed(2) }} 天</p>
            </div>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <!-- 系统信息 -->
    <a-card class="mt-4" :loading="loading.server">
      <template #title>
        <span>
          <laptop-outlined /> 系统信息
        </span>
      </template>
      <a-descriptions :column="3">
        <a-descriptions-item label="操作系统">
          {{ serverInfo.osName }}
        </a-descriptions-item>
        <a-descriptions-item label="系统架构">
          {{ serverInfo.osArchitecture }}
        </a-descriptions-item>
        <a-descriptions-item label="系统版本">
          {{ serverInfo.osVersion }}
        </a-descriptions-item>
        <a-descriptions-item label="处理器">
          {{ serverInfo.processorName }}
        </a-descriptions-item>
        <a-descriptions-item label="处理器核心数">
          {{ serverInfo.processorCount }}
        </a-descriptions-item>
        <a-descriptions-item label=".NET运行时">
          {{ serverInfo.dotNetRuntimeVersion }}
        </a-descriptions-item>
        <a-descriptions-item label="CLR版本">
          {{ serverInfo.clrVersion }}
        </a-descriptions-item>
        <a-descriptions-item label="运行时目录" :span="2">
          {{ serverInfo.dotNetRuntimeDirectory }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- 功能区域 -->
    <a-tabs v-model:activeKey="activeTab" class="mt-4" @change="handleTabChange">
      <!-- 进程列表 -->
      <a-tab-pane key="processes" tab="进程列表">
        <a-table
          :columns="processColumns"
          :data-source="processList"
          :loading="loading.processes"
          :pagination="{ showSizeChanger: true, showQuickJumper: true }"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'action'">
              <a-space>
                <a-button type="link" @click="viewProcessDetail(record)">
                  详情
                </a-button>
              </a-space>
            </template>
          </template>
        </a-table>
      </a-tab-pane>

      <!-- 网络信息 -->
      <a-tab-pane key="network" tab="网络信息">
        <a-table
          :columns="networkColumns"
          :data-source="networkList"
          :loading="loading.network"
          :pagination="{ showSizeChanger: true, showQuickJumper: true }"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'action'">
              <a-space>
                <a-button type="link" @click="viewNetworkDetail(record)">
                  详情
                </a-button>
              </a-space>
            </template>
          </template>
        </a-table>
      </a-tab-pane>

      <!-- 系统服务 -->
      <a-tab-pane key="services" tab="系统服务">
        <a-table
          :columns="serviceColumns"
          :data-source="serviceList"
          :loading="loading.services"
          :pagination="{ showSizeChanger: true, showQuickJumper: true }"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'status'">
              <a-tag :color="record.status === 0 ? 'success' : 'error'">
                {{ record.status === 0 ? '运行中' : '已停止' }}
              </a-tag>
            </template>
            <template v-if="column.key === 'action'">
              <a-space>
                <a-button type="link" @click="viewServiceDetail(record)">
                  详情
                </a-button>
              </a-space>
            </template>
          </template>
        </a-table>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import { 
  DesktopOutlined,
  MonitorOutlined,
  HddOutlined,
  ClockCircleOutlined,
  LaptopOutlined
} from '@ant-design/icons-vue'
import type { HbtServerMonitorDto, HbtProcessDto, HbtNetworkDto, HbtServiceDto } from '@/types/audit/serverMonitor'
import { formatDateTime } from '@/utils/format'
import request from '@/utils/request'
import { message } from 'ant-design-vue'

// 状态变量
const serverInfo = ref<HbtServerMonitorDto>({
  cpuUsage: 0,
  totalMemory: 0,
  usedMemory: 0,
  memoryUsage: 0,
  totalDiskSpace: 0,
  usedDiskSpace: 0,
  diskUsage: 0,
  osName: '',
  osArchitecture: '',
  osVersion: '',
  processorName: '',
  processorCount: 0,
  systemStartTime: new Date(),
  systemUptime: 0,
  dotNetRuntimeVersion: '',
  clrVersion: '',
  dotNetRuntimeDirectory: ''
})

const processList = ref<HbtProcessDto[]>([])
const networkList = ref<HbtNetworkDto[]>([])
const serviceList = ref<HbtServiceDto[]>([])
const activeTab = ref('processes')
const loading = ref({
  server: false,
  processes: false,
  network: false,
  services: false
})

// 添加重试配置
const retryConfig = {
  maxRetries: 3,
  retryDelay: 1000,
  currentRetry: 0
}

// 表格列定义
const processColumns = [
  { title: '进程ID', dataIndex: 'processId', key: 'processId' },
  { title: '进程名称', dataIndex: 'processName', key: 'processName' },
  { title: '描述', dataIndex: 'description', key: 'description' },
  { 
    title: 'CPU使用率', 
    dataIndex: 'cpuUsage', 
    key: 'cpuUsage', 
    customRender: ({ text }: { text: number }) => `${text}%` 
  },
  { 
    title: '内存使用', 
    dataIndex: 'memoryUsage', 
    key: 'memoryUsage', 
    customRender: ({ text }: { text: number }) => `${text}MB` 
  },
  { 
    title: '启动时间', 
    dataIndex: 'startTime', 
    key: 'startTime', 
    customRender: ({ text }: { text: Date }) => formatDateTime(text) 
  },
  { 
    title: '运行时间', 
    dataIndex: 'runningTime', 
    key: 'runningTime', 
    customRender: ({ text }: { text: number }) => `${text}分钟` 
  },
  { title: '操作', key: 'action' }
]

const networkColumns = [
  { title: '网卡名称', dataIndex: 'adapterName', key: 'adapterName' },
  { title: 'MAC地址', dataIndex: 'macAddress', key: 'macAddress' },
  { title: 'IP地址', dataIndex: 'ipAddress', key: 'ipAddress' },
  { title: 'IP位置', dataIndex: 'ipLocation', key: 'ipLocation' },
  { 
    title: '发送速率', 
    dataIndex: 'sendRate', 
    key: 'sendRate', 
    customRender: ({ text }: { text: number }) => `${text}KB/s` 
  },
  { 
    title: '接收速率', 
    dataIndex: 'receiveRate', 
    key: 'receiveRate', 
    customRender: ({ text }: { text: number }) => `${text}KB/s` 
  },
  { title: '操作', key: 'action' }
]

const serviceColumns = [
  { title: '服务名称', dataIndex: 'serviceName', key: 'serviceName' },
  { title: '显示名称', dataIndex: 'displayName', key: 'displayName' },
  { title: '服务类型', dataIndex: 'serviceType', key: 'serviceType' },
  { title: '状态', dataIndex: 'status', key: 'status' },
  { title: '启动类型', dataIndex: 'startType', key: 'startType' },
  { title: '运行账户', dataIndex: 'account', key: 'account' },
  { title: '操作', key: 'action' }
]

// 方法定义
const getStatusByPercent = (percent: number): 'normal' | 'exception' | 'active' | 'success' => {
  if (percent >= 90) return 'exception'
  if (percent >= 70) return 'active'
  return 'normal'
}

const fetchServerInfo = async () => {
  loading.value.server = true
  try {
    const { data } = await request.get('/api/audit/server/info')
    if (data) {
      serverInfo.value = data
    }
  } catch (error) {
    console.error('获取服务器信息失败:', error)
  } finally {
    loading.value.server = false
  }
}

const fetchProcessList = async (retry = false) => {
  if (!retry) {
    loading.value.processes = true
    retryConfig.currentRetry = 0
  }
  
  try {
    const { data } = await request.get('/api/audit/server/processes', {
      timeout: 10000 // 降低超时时间到10秒
    })
    if (data) {
      processList.value = data
      retryConfig.currentRetry = 0
    }
  } catch (error: any) {
    console.error('获取进程列表失败:', error)
    
    if (retryConfig.currentRetry < retryConfig.maxRetries) {
      retryConfig.currentRetry++
      message.warning(`获取进程列表超时，正在进行第${retryConfig.currentRetry}次重试...`)
      await new Promise(resolve => setTimeout(resolve, retryConfig.retryDelay))
      return fetchProcessList(true)
    } else {
      message.error('获取进程列表失败，请稍后重试')
    }
  } finally {
    if (!retry) {
      loading.value.processes = false
    }
  }
}

const fetchNetworkInfo = async () => {
  loading.value.network = true
  try {
    const { data } = await request.get('/api/audit/server/network')
    if (data) {
      networkList.value = data
    }
  } catch (error) {
    console.error('获取网络信息失败:', error)
  } finally {
    loading.value.network = false
  }
}

const fetchServiceList = async () => {
  loading.value.services = true
  try {
    const { data } = await request.get('/api/audit/server/services')
    if (data) {
      serviceList.value = data
    }
  } catch (error) {
    console.error('获取服务列表失败:', error)
  } finally {
    loading.value.services = false
  }
}

const viewProcessDetail = (record: any) => {
  // TODO: 实现进程详情查看
  console.log('查看进程详情:', record as HbtProcessDto)
}

const viewNetworkDetail = (record: any) => {
  // TODO: 实现网络详情查看
  console.log('查看网络详情:', record as HbtNetworkDto)
}

const viewServiceDetail = (record: any) => {
  // TODO: 实现服务详情查看
  console.log('查看服务详情:', record as HbtServiceDto)
}

// 定时刷新
let refreshTimer: number | null = null

const startRefresh = async () => {
  try {
    await fetchServerInfo()
  } catch (error) {
    console.error('获取服务器信息失败:', error)
  }

  if (activeTab.value === 'processes') {
    try {
      await fetchProcessList()
    } catch (error) {
      console.error('获取进程列表失败:', error)
    }
  }
  if (activeTab.value === 'network') {
    try {
      await fetchNetworkInfo()
    } catch (error) {
      console.error('获取网络信息失败:', error)
    }
  }
  if (activeTab.value === 'services') {
    try {
      await fetchServiceList()
    } catch (error) {
      console.error('获取服务列表失败:', error)
    }
  }
}

// 生命周期钩子
onMounted(() => {
  startRefresh()
  refreshTimer = window.setInterval(startRefresh, 60000) // 改为每60秒刷新一次
})

onUnmounted(() => {
  if (refreshTimer) {
    clearInterval(refreshTimer)
  }
})

// 添加标签切换处理
const handleTabChange = (key: string) => {
  activeTab.value = key
  startRefresh() // 切换标签时刷新数据
}
</script>

<style lang="less" scoped>
.server-monitor {
  padding: 16px;

  .monitor-card {
    height: 280px;
    
    :deep(.ant-card-body) {
      padding: 16px;
      height: calc(100% - 57px);  // 57px是卡片头部的高度
    }

    :deep(.ant-card-loading-content) {
      height: 100%;
    }

    :deep(.ant-progress) {
      .ant-progress-text {
        font-size: 24px;
        font-weight: 500;
      }

      &.ant-progress-status-success .ant-progress-text {
        color: #52c41a;
      }
      &.ant-progress-status-exception .ant-progress-text {
        color: #ff4d4f;
      }
      &.ant-progress-status-active .ant-progress-text {
        color: #1890ff;
      }
    }
  }

  .card-content {
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    
    .ant-progress {
      margin-bottom: 16px;
    }
  }

  .center-content {
    text-align: center;
  }

  .detail-text {
    margin-top: 12px;
    text-align: center;
    color: rgba(0, 0, 0, 0.65);
    font-size: 14px;
    line-height: 1.5;
  }

  .uptime-info {
    p {
      margin-bottom: 12px;
      font-size: 14px;
      line-height: 1.5;
      &:last-child {
        margin-bottom: 0;
      }
    }
  }

  .mt-4 {
    margin-top: 16px;
  }

  :deep(.ant-progress-text) {
    font-size: 20px;
  }

  :deep(.ant-card-head-title) {
    display: flex;
    align-items: center;
    
    .anticon {
      margin-right: 8px;
      font-size: 16px;
    }
  }
}
</style> 