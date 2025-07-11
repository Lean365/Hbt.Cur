<template>
  <div class="workflow-manager">
    <div class="page-header">
      <h1>工作流管理</h1>
      <p>管理工作流定义、实例和流程</p>
    </div>

    <!-- 统计卡片 -->
    <a-row :gutter="16" style="margin-bottom: 24px;">
      <a-col :span="4">
        <a-card>
          <a-statistic
            title="工作流定义"
            :value="statistics.definitionCount"
            :value-style="{ color: '#3f8600' }"
          >
            <template #prefix>
              <FileTextOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="4">
        <a-card>
          <a-statistic
            title="流程实例"
            :value="statistics.instanceCount"
            :value-style="{ color: '#1890ff' }"
          >
            <template #prefix>
              <PlayCircleOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="4">
        <a-card>
          <a-statistic
            title="运行中实例"
            :value="statistics.runningCount"
            :value-style="{ color: '#722ed1' }"
          >
            <template #prefix>
              <SyncOutlined spin />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="4">
        <a-card>
          <a-statistic
            title="已完成实例"
            :value="statistics.endedCount"
            :value-style="{ color: '#52c41a' }"
          >
            <template #prefix>
              <CheckCircleOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="4">
        <a-card>
          <a-statistic
            title="待处理任务"
            :value="statistics.taskCount"
            :value-style="{ color: '#fa8c16' }"
          >
            <template #prefix>
              <ClockCircleOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
    </a-row>

    <!-- 快速操作 -->
    <a-row :gutter="16" style="margin-bottom: 24px;">
      <a-col :span="24">
        <a-card title="快速操作">
          <a-space>
            <a-button type="primary" @click="navigateTo('/workflow/definition')">
              <PlusOutlined />
              创建工作流定义
            </a-button>
            <a-button @click="navigateTo('/workflow/instance')">
              <UnorderedListOutlined />
              查看流程实例
            </a-button>
            <a-button @click="navigateTo('/workflow/task')">
              <CheckSquareOutlined />
              待办任务
            </a-button>
            <a-button @click="navigateTo('/workflow/my')">
              <UserOutlined />
              我的流程
            </a-button>
          </a-space>
        </a-card>
      </a-col>
    </a-row>

    <!-- 图表区域 -->
    <a-row :gutter="16" style="margin-bottom: 24px;">
      <a-col :span="12">
        <a-card title="状态分布" :loading="chartLoading">
          <div ref="statusChartRef" style="height: 300px;"></div>
        </a-card>
      </a-col>
      <a-col :span="12">
        <a-card title="最近活动" :loading="activitiesLoading">
          <a-list
            :data-source="recentActivities"
            :pagination="false"
            size="small"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>{{ item.title }}</template>
                  <template #description>
                    <div>
                      <span>{{ item.description }}</span>
                      <span style="margin-left: 8px; color: #999;">
                        {{ formatDateTime(item.createTime, 'yyyy-MM-dd HH:mm') }}
                      </span>
                    </div>
                  </template>
                </a-list-item-meta>
              </a-list-item>
            </template>
            <template #default>
              <a-empty description="暂无活动" />
            </template>
          </a-list>
          <template #extra>
            <a-button type="link" @click="navigateTo('/workflow/instance')">
              查看全部
            </a-button>
          </template>
        </a-card>
      </a-col>
    </a-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, nextTick, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import {
  FileTextOutlined,
  PlayCircleOutlined,
  SyncOutlined,
  ClockCircleOutlined,
  PlusOutlined,
  UnorderedListOutlined,
  CheckSquareOutlined,
  UserOutlined,
  CheckCircleOutlined
} from '@ant-design/icons-vue'
import * as echarts from 'echarts'
import { getWorkflowDashboardStats, getWorkflowRecentActivities } from '@/api/workflow/workflow'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()

// 统计数据
const statistics = ref({
  definitionCount: 0,
  instanceCount: 0,
  runningCount: 0,
  taskCount: 0,
  endedCount: 0
})
const statsLoading = ref(false)

// 最近活动
const recentActivities = ref<any[]>([])
const activitiesLoading = ref(false)

// 图表引用
const statusChartRef = ref<HTMLElement>()
const chartLoading = ref(false)
let statusChart: echarts.ECharts | null = null

// 获取统计数据
const fetchStatistics = async () => {
  statsLoading.value = true
  try {
    const response = await getWorkflowDashboardStats()
    if (response.data.code === 200) {
      statistics.value = response.data.data
    }
  } catch (error) {
    console.error('获取统计数据失败:', error)
  } finally {
    statsLoading.value = false
  }
}

// 获取最近活动
const fetchRecentActivities = async () => {
  activitiesLoading.value = true
  try {
    const response = await getWorkflowRecentActivities()
    if (response.data.code === 200) {
      recentActivities.value = response.data.data || []
    }
  } catch (error) {
    console.error('获取最近活动失败:', error)
  } finally {
    activitiesLoading.value = false
  }
}

// 初始化图表
const initCharts = async () => {
  await nextTick()
  if (!statusChartRef.value) return

  chartLoading.value = true
  try {
    statusChart = echarts.init(statusChartRef.value)
    
    // 模拟状态分布数据
    const option = {
      tooltip: {
        trigger: 'item',
        formatter: '{a} <br/>{b}: {c} ({d}%)'
      },
      legend: {
        orient: 'vertical',
        left: 'left'
      },
      series: [
        {
          name: '实例状态',
          type: 'pie',
          radius: ['40%', '70%'],
          avoidLabelOverlap: false,
          label: {
            show: false,
            position: 'center'
          },
          emphasis: {
            label: {
              show: true,
              fontSize: '18',
              fontWeight: 'bold'
            }
          },
          labelLine: {
            show: false
          },
          data: [
            { value: statistics.value.runningCount, name: '运行中', itemStyle: { color: '#1890ff' } },
            { value: statistics.value.endedCount, name: '已完成', itemStyle: { color: '#52c41a' } },
            { value: statistics.value.instanceCount - statistics.value.runningCount - statistics.value.endedCount, name: '其他状态', itemStyle: { color: '#d9d9d9' } }
          ]
        }
      ]
    }
    
    statusChart.setOption(option)
  } catch (error) {
    console.error('初始化图表失败:', error)
  } finally {
    chartLoading.value = false
  }
}

// 导航到指定页面
const navigateTo = (path: string) => {
  router.push(path)
}

// 页面加载
onMounted(() => {
  fetchStatistics()
  fetchRecentActivities()
  initCharts()
})
</script>

<style scoped>
.workflow-manager {
  padding: 24px;
}

.page-header {
  margin-bottom: 24px;
}

.page-header h1 {
  margin: 0 0 8px 0;
  font-size: 24px;
  font-weight: 600;
  color: #262626;
}

.page-header p {
  margin: 0;
  color: #8c8c8c;
  font-size: 14px;
}
</style> 