<template>
  <div class="page-tabs-wrapper">
    <a-tabs
      v-model:activeKey="activeKey"
      type="editable-card"
      :hide-add="true"
      :animated="false"
      class="page-tabs"
      @edit="onEdit"
      @change="onChange"
    >
      <a-tab-pane
        v-for="pane in panes"
        :key="pane.key"
        :tab="pane.tab"
        :closable="pane.closable"
      >
        <template #tab>
          <span class="tab-item">
            <component :is="pane.icon" v-if="pane.icon" />
            <span>{{ pane.title }}</span>
          </span>
        </template>
      </a-tab-pane>
    </a-tabs>
    
    <a-dropdown :trigger="['click']" class="tabs-actions">
      <a-button type="text">
        <template #icon><down-outlined /></template>
      </a-button>
      <template #overlay>
        <a-menu @click="handleTabAction">
          <a-menu-item key="closeOthers">
            {{ t('tabs.closeOthers') }}
          </a-menu-item>
          <a-menu-item key="closeRight">
            {{ t('tabs.closeRight') }}
          </a-menu-item>
          <a-menu-item key="closeAll">
            {{ t('tabs.closeAll') }}
          </a-menu-item>
        </a-menu>
      </template>
    </a-dropdown>
  </div>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
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
  MenuOutlined,
  DownOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()

interface TabPane {
  key: string
  title: string
  tab?: any
  icon?: any
  closable: boolean
}

// 路由映射配置
interface RouteMapItem {
  title: string;
  icon?: any;
  children?: {
    [key: string]: {
      title: string;
      icon?: any;
    };
  };
}

interface RouteMap {
  [key: string]: RouteMapItem;
}

const routeMap: RouteMap = {
  dashboard: {
    title: t('dashboard.title'),
    icon: DashboardOutlined,
    children: {
      analysis: { 
        title: t('dashboard.analysis'),
        icon: BarChartOutlined
      },
      monitor: { 
        title: t('dashboard.monitor'),
        icon: MonitorOutlined
      },
      workplace: { 
        title: t('dashboard.workplace'),
        icon: DesktopOutlined
      }
    }
  },
  identity: {
    title: t('identity.title'),
    icon: TeamOutlined,
    children: {
      user: { 
        title: t('identity.user.title'),
        icon: UserOutlined
      },
      role: { 
        title: t('identity.role.title'),
        icon: SafetyOutlined
      },
      menu: { 
        title: t('identity.menu.title'),
        icon: MenuOutlined
      }
    }
  }
}

const activeKey = ref(route.path)
const panes = ref<TabPane[]>([
  {
    title: t('home.title'),
    key: '/',
    icon: HomeOutlined,
    closable: false
  }
])

// 监听路由变化
watch(
  () => route.path,
  (newPath) => {
    activeKey.value = newPath
    const paths = newPath.split('/').filter(Boolean)
    
    // 如果标签不存在，添加新标签
    if (!panes.value.find(pane => pane.key === newPath)) {
      if (paths.length >= 2) {
        const [parentPath, childPath] = paths
        const parentMenu = routeMap[parentPath]
        if (parentMenu?.children?.[childPath]) {
          const menuInfo = parentMenu.children[childPath]
          panes.value.push({
            title: menuInfo.title,
            key: newPath,
            icon: menuInfo.icon,
            closable: true
          })
        }
      } else if (paths.length === 1) {
        const menuInfo = routeMap[paths[0]]
        if (menuInfo) {
          panes.value.push({
            title: menuInfo.title,
            key: newPath,
            icon: menuInfo.icon,
            closable: true
          })
        }
      }
    }
  },
  { immediate: true }
)

// 标签切换
const onChange = (key: string) => {
  router.push(key)
}

// 关闭标签
const onEdit = (targetKey: string | MouseEvent, action: 'add' | 'remove') => {
  if (action === 'remove' && typeof targetKey === 'string') {
    closeTab(targetKey)
  }
}

// 关闭指定标签
const closeTab = (targetKey: string) => {
  const targetIndex = panes.value.findIndex(pane => pane.key === targetKey)
  if (targetIndex === -1) return
  
  panes.value = panes.value.filter(pane => pane.key !== targetKey)
  
  // 如果关闭的是当前标签，跳转到前一个标签
  if (targetKey === activeKey.value) {
    const newActiveKey = panes.value[targetIndex - 1]?.key || panes.value[0].key
    router.push(newActiveKey)
  }
}

// 处理标签操作
const handleTabAction = ({ key }: { key: string }) => {
  const currentIndex = panes.value.findIndex(pane => pane.key === activeKey.value)
  
  switch (key) {
    case 'closeOthers':
      panes.value = panes.value.filter(
        pane => !pane.closable || pane.key === activeKey.value
      )
      break
    case 'closeRight':
      panes.value = panes.value.filter(
        (pane, index) => !pane.closable || index <= currentIndex
      )
      break
    case 'closeAll':
      panes.value = panes.value.filter(pane => !pane.closable)
      if (activeKey.value !== '/') {
        router.push('/')
      }
      break
  }
}
</script>

<style lang="less" scoped>
.page-tabs-wrapper {
  position: relative;
  height: 40px;
  background: var(--ant-component-background);
  border-top: 1px solid var(--ant-border-color-split);
  
  .page-tabs {
    height: 40px;
    padding-right: 40px;
    
    :deep(.ant-tabs-nav) {
      margin: 0;
      
      &::before {
        display: none;
      }
      
      .ant-tabs-tab {
        margin: 0 !important;
        padding: 8px 16px;
        background: transparent;
        border: none;
        transition: all 0.3s;
        
        &:hover {
          color: var(--ant-primary-color);
        }
        
        &.ant-tabs-tab-active {
          .ant-tabs-tab-btn {
            color: var(--ant-primary-color);
          }
        }
        
        .ant-tabs-tab-remove {
          margin: 0;
          padding: 0;
          
          &:hover {
            color: var(--ant-primary-color);
          }
        }
      }
    }
  }
  
  .tab-item {
    display: inline-flex;
    align-items: center;
    
    .anticon {
      margin-right: 8px;
      font-size: 14px;
    }
  }
  
  .tabs-actions {
    position: absolute;
    right: 0;
    top: 0;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-left: 1px solid var(--ant-border-color-split);
    
    .ant-btn {
      width: 40px;
      height: 40px;
      
      &:hover {
        color: var(--ant-primary-color);
      }
    }
  }
}
</style> 