<template>
  <div class="breadcrumb-wrapper">
    <a-breadcrumb>
      <!-- 首页 -->
      <a-breadcrumb-item>
        <router-link to="/">
          <home-outlined />
          <span>{{ t('home.title') }}</span>
        </router-link>
      </a-breadcrumb-item>
      
      <!-- 动态面包屑 -->
      <template v-for="item in breadcrumbItems" :key="item.path">
        <a-breadcrumb-item>
          <router-link 
            v-if="item.path !== route.path" 
            :to="item.path"
          >
            <component :is="getIcon(item.icon)" v-if="item.icon" />
            <span>{{ t(item.title) }}</span>
          </router-link>
          <template v-else>
            <component :is="getIcon(item.icon)" v-if="item.icon" />
            <span>{{ t(item.title) }}</span>
          </template>
        </a-breadcrumb-item>
      </template>
    </a-breadcrumb>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import {
  HomeOutlined,
  DashboardOutlined,
  BarChartOutlined,
  MonitorOutlined,
  DesktopOutlined,
  TeamOutlined,
  UserOutlined,
  SafetyOutlined,
  MenuOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()

// 图标映射
const iconMap = {
  HomeOutlined,
  DashboardOutlined,
  BarChartOutlined,
  MonitorOutlined,
  DesktopOutlined,
  TeamOutlined,
  UserOutlined,
  SafetyOutlined,
  MenuOutlined
}

// 获取图标组件
const getIcon = (iconName?: string) => {
  if (!iconName) return null
  return iconMap[iconName as keyof typeof iconMap]
}

// 面包屑项接口定义
interface BreadcrumbItem {
  title: string | undefined
  icon?: string
  path: string
}

// 计算面包屑项
const breadcrumbItems = computed<BreadcrumbItem[]>(() => {
  const items: BreadcrumbItem[] = []
  // 过滤掉 Layout 和 Home 路由，只保留实际的导航路径
  const matched = route.matched.filter(item => 
    item.name !== 'Layout' && 
    item.name !== 'Home' && 
    item.meta?.title
  )
  
  // 构建面包屑路径
  let fullPath = ''
  matched.forEach((item) => {
    // 构建累积路径
    const pathSegment = item.path.replace(/^\/+/, '') // 移除开头的斜杠
    fullPath = fullPath ? `${fullPath}/${pathSegment}` : `/${pathSegment}`
    
    items.push({
      title: item.meta?.title as string | undefined,
      icon: item.meta?.icon as string | undefined,
      path: fullPath
    })
  })
  
  return items
})
</script>

<style lang="less" scoped>
.breadcrumb-wrapper {
  margin: 16px 24px;
  
  :deep(.ant-breadcrumb) {
    line-height: 1;
    
    .anticon {
      margin-right: 4px;
      font-size: 14px;
      vertical-align: -0.125em;
    }
    
    a {
      display: inline-flex;
      align-items: center;
      color: var(--ant-text-color-secondary);
      transition: color 0.3s;
      
      &:hover {
        color: var(--ant-primary-color);
      }
    }

    .ant-breadcrumb-link {
      display: inline-flex;
      align-items: center;
      line-height: 1;
    }

    span {
      line-height: 1;
    }
  }
}
</style>