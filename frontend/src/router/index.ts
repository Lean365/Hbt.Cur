import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { getToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'

// 基础路由
export const constantRoutes: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/login/index.vue'),
    meta: {
      title: '登录',
      requiresAuth: false
    }
  },
  {
    path: '/',
    component: () => import('@/layouts/BasicLayout.vue'),
    redirect: '/home',
    children: [
      {
        path: 'home',
        name: 'Home',
        component: () => import('@/views/home/index.vue'),
        meta: {
          title: '首页',
          icon: 'HomeOutlined',
          requiresAuth: true
        }
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        redirect: 'workplace',
        meta: { 
          title: 'dashboard.title', 
          icon: 'DashboardOutlined', 
          requiresAuth: true 
        },
        children: [
          {
            path: 'workplace',
            name: 'Workplace',
            component: () => import('@/views/dashboard/workplace/index.vue'),
            meta: { 
              title: 'dashboard.workplace', 
              icon: 'DesktopOutlined', 
              requiresAuth: true 
            }
          },
          {
            path: 'analysis',
            name: 'Analysis',
            component: () => import('@/views/dashboard/analysis/index.vue'),
            meta: { 
              title: 'dashboard.analysis', 
              icon: 'BarChartOutlined', 
              requiresAuth: true 
            }
          },
          {
            path: 'monitor',
            name: 'Monitor',
            component: () => import('@/views/dashboard/monitor/index.vue'),
            meta: { 
              title: 'dashboard.monitor', 
              icon: 'MonitorOutlined', 
              requiresAuth: true 
            }
          }
        ]
      }
    ]
  }
]

// 创建路由实例
const router = createRouter({
  history: createWebHistory(),
  routes: constantRoutes
})

// 路由守卫
router.beforeEach(async (to, from, next) => {
  // 设置页面标题
  const title = to.meta.title
  if (typeof title === 'string') {
    document.title = `${title} - Lean.Hbt`
  }

  const token = getToken()
  const userStore = useUserStore()
  const menuStore = useMenuStore()

  // 不需要登录的页面直接放行
  if (to.meta.requiresAuth === false) {
    next()
    return
  }

  // 未登录时跳转到登录页
  if (!token) {
    next({
      name: 'Login',
      query: { redirect: to.fullPath }
    })
    return
  }

  try {
    // 如果没有用户信息，获取用户信息
    if (!userStore.user) {
      await userStore.getUserInfo()
    }

    // 如果没有菜单，加载菜单
    if (!menuStore.menuList?.length) {
      // 设置加载状态，防止重复加载
      if (menuStore.isLoading) {
        next()
        return
      }

      try {
        menuStore.isLoading = true
        const success = await menuStore.loadUserMenus()
        menuStore.isLoading = false
        
        if (!success) {
          // 如果加载菜单失败，但用户已登录，仍然允许访问
          if (userStore.user) {
            next()
            return
          }
          
          // 如果没有用户信息，则跳转到登录页
          userStore.logout()
          next({
            name: 'Login',
            query: { redirect: to.fullPath }
          })
          return
        }
      } catch (error) {
        menuStore.isLoading = false
        console.error('加载菜单失败:', error)
        // 如果用户已登录，仍然允许访问
        if (userStore.user) {
          next()
          return
        }
        
        // 如果没有用户信息，则跳转到登录页
        userStore.logout()
        next({
          name: 'Login',
          query: { redirect: to.fullPath }
        })
        return
      }
    }

    next()
  } catch (error) {
    console.error('路由守卫错误:', error)
    // 发生错误时，清除token并跳转到登录页
    userStore.logout()
    next({
      name: 'Login',
      query: { redirect: to.fullPath }
    })
  }
})

export default router 