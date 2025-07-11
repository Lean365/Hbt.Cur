<template>
  <div class="workflow-manager-my">
    <div class="page-header">
      <h1>我的流程</h1>
      <p>查看和管理我发起和参与的流程</p>
    </div>

    <!-- 用户统计卡片 -->
    <a-row :gutter="16" style="margin-bottom: 24px;">
      <a-col :span="6">
        <a-card>
          <a-statistic
            title="我发起的流程"
            :value="userStatistics.initiatedCount"
            :value-style="{ color: '#3f8600' }"
          >
            <template #prefix>
              <UserOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <a-statistic
            title="我参与的流程"
            :value="userStatistics.participatedCount"
            :value-style="{ color: '#1890ff' }"
          >
            <template #prefix>
              <TeamOutlined />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <a-statistic
            title="运行中的流程"
            :value="userStatistics.runningCount"
            :value-style="{ color: '#722ed1' }"
          >
            <template #prefix>
              <SyncOutlined spin />
            </template>
          </a-statistic>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <a-statistic
            title="待处理任务"
            :value="userStatistics.taskCount"
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
              创建工作流
            </a-button>
            <a-button @click="navigateTo('/workflow/overview')">
              <AppstoreOutlined />
              全部流程
            </a-button>
            <a-button @click="navigateTo('/workflow/task')">
              <CheckSquareOutlined />
              我的待办
            </a-button>
            <a-button @click="refreshData">
              <ReloadOutlined />
              刷新数据
            </a-button>
          </a-space>
        </a-card>
      </a-col>
    </a-row>

    <!-- 用户流程列表 -->
    <a-row :gutter="16" style="margin-bottom: 24px;">
      <a-col :span="12">
        <a-card title="我发起的流程" :loading="loadingUserWorkflows">
          <template #extra>
            <a-button type="link" @click="navigateTo('/workflow/instance')">
              查看全部
            </a-button>
          </template>
          <a-list
            :data-source="userInitiatedWorkflows"
            :pagination="false"
            size="small"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>
                    <a @click="navigateTo(`/workflow/instance/detail/${item.id}`)">
                      {{ item.instanceName }}
                    </a>
                  </template>
                  <template #description>
                    <div>
                      <a-tag :color="getStatusColor(item.status)">
                        {{ getStatusText(item.status) }}
                      </a-tag>
                      <span style="margin-left: 8px; color: #999;">
                        {{ formatDateTime(item.createTime, 'yyyy-MM-dd HH:mm') }}
                      </span>
                    </div>
                  </template>
                </a-list-item-meta>
              </a-list-item>
            </template>
            <template #default>
              <a-empty description="暂无发起的流程" />
            </template>
          </a-list>
        </a-card>
      </a-col>
      
      <a-col :span="12">
        <a-card title="我参与的流程" :loading="loadingUserWorkflows">
          <template #extra>
            <a-button type="link" @click="navigateTo('/workflow/instance')">
              查看全部
            </a-button>
          </template>
          <a-list
            :data-source="userParticipatedWorkflows"
            :pagination="false"
            size="small"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>
                    <a @click="navigateTo(`/workflow/instance/detail/${item.id}`)">
                      {{ item.instanceName }}
                    </a>
                  </template>
                  <template #description>
                    <div>
                      <a-tag :color="getStatusColor(item.status)">
                        {{ getStatusText(item.status) }}
                      </a-tag>
                      <span style="margin-left: 8px; color: #999;">
                        {{ formatDateTime(item.createTime, 'yyyy-MM-dd HH:mm') }}
                      </span>
                    </div>
                  </template>
                </a-list-item-meta>
              </a-list-item>
            </template>
            <template #default>
              <a-empty description="暂无参与的流程" />
            </template>
          </a-list>
        </a-card>
      </a-col>
    </a-row>

    <!-- 图表区域 -->
    <a-row :gutter="16">
      <a-col :span="12">
        <a-card title="我的流程状态分布" :loading="chartLoading">
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
  UserOutlined,
  TeamOutlined,
  SyncOutlined,
  ClockCircleOutlined,
  PlusOutlined,
  AppstoreOutlined,
  CheckSquareOutlined,
  ReloadOutlined
} from '@ant-design/icons-vue'
import * as echarts from 'echarts'
import { getUserInitiatedWorkflows, getUserParticipatedWorkflows, getWorkflowRecentActivities } from '@/api/workflow/workflow'
import { useUserStore } from '@/stores/user'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()
const currentUser = computed(() => userStore.userInfo)

// 用户统计数据
const userStatistics = ref({
  initiatedCount: 0,
  participatedCount: 0,
  runningCount: 0,
  taskCount: 0
})

// 用户流程数据
const userInitiatedWorkflows = ref<any[]>([])
const userParticipatedWorkflows = ref<any[]>([])
const loadingUserWorkflows = ref(false)

// 最近活动
const recentActivities = ref<any[]>([])
const activitiesLoading = ref(false)

// 图表引用
const statusChartRef = ref<HTMLElement>()
const chartLoading = ref(false)
let statusChart: echarts.ECharts | null = null

// 获取用户流程数据
const loadUserWorkflows = async () => {
  if (!currentUser.value?.userId) return
  
  loadingUserWorkflows.value = true
  try {
    const [initiatedRes, participatedRes] = await Promise.all([
      getUserInitiatedWorkflows(currentUser.value.userId),
      getUserParticipatedWorkflows(currentUser.value.userId)
    ])
    
    if (initiatedRes.data.code === 200) {
      userInitiatedWorkflows.value = initiatedRes.data.data || []
      userStatistics.value.initiatedCount = userInitiatedWorkflows.value.length
    }
    
    if (participatedRes.data.code === 200) {
      userParticipatedWorkflows.value = participatedRes.data.data || []
      userStatistics.value.participatedCount = userParticipatedWorkflows.value.length
    }

    // 计算运行中的流程数量
    const allWorkflows = [...userInitiatedWorkflows.value, ...userParticipatedWorkflows.value]
    userStatistics.value.runningCount = allWorkflows.filter(w => w.status === 1).length
    userStatistics.value.taskCount = Math.floor(Math.random() * 10) // 模拟任务数量
  } catch (error) {
    console.error('获取用户流程失败:', error)
  } finally {
    loadingUserWorkflows.value = false
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
          name: '我的流程状态',
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
            { value: userStatistics.value.runningCount, name: '运行中', itemStyle: { color: '#1890ff' } },
            { value: userStatistics.value.initiatedCount - userStatistics.value.runningCount, name: '其他状态', itemStyle: { color: '#d9d9d9' } }
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

// 获取状态颜色
const getStatusColor = (status: number) => {
  switch (status) {
    case 0: return 'default' // 草稿
    case 1: return 'processing' // 运行中
    case 2: return 'warning' // 暂停
    case 3: return 'success' // 完成
    case 4: return 'error' // 终止
    default: return 'default'
  }
}

// 获取状态文本
const getStatusText = (status: number) => {
  switch (status) {
    case 0: return '草稿'
    case 1: return '运行中'
    case 2: return '暂停'
    case 3: return '完成'
    case 4: return '终止'
    default: return '未知'
  }
}

// 导航到指定页面
const navigateTo = (path: string) => {
  router.push(path)
}

// 刷新数据
const refreshData = async () => {
  await loadUserWorkflows()
  await fetchRecentActivities()
  await initCharts()
}

// 页面加载
onMounted(() => {
  loadUserWorkflows()
  fetchRecentActivities()
  initCharts()
})
</script>

<style scoped>
.workflow-manager-my {
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