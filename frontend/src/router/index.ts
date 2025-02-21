import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { getToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import Layout from '@/layouts/BasicLayout.vue'
import type { Menu } from '@/types/identity/menu'
import { HbtMenuType } from '@/types/base'
import { message } from 'ant-design-vue'
import i18n from '@/locales'

// 基础路由名称
const BASIC_ROUTES = ['Login', 'Home']

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
        redirect: '/dashboard/workplace',
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
      },
      {
        path: '/about',
        redirect: '/about/index',
        meta: { title: '关于', icon: 'InfoCircleOutlined' },
        children: [
          {
            path: 'index',
            name: 'About',
            component: () => import('@/views/about/index.vue'),
            meta: { title: '关于系统', icon: 'InfoCircleOutlined' }
          },
          {
            path: 'terms',
            name: 'Terms',
            component: () => import('@/views/about/terms.vue'),
            meta: { title: '使用条款', icon: 'FileTextOutlined' }
          },
          {
            path: 'privacy',
            name: 'Privacy',
            component: () => import('@/views/about/privacy.vue'),
            meta: { title: '隐私政策', icon: 'SafetyOutlined' }
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

// 将菜单转换为路由配置
const menuToRoute = (menu: Menu, parentPath: string = ''): RouteRecordRaw | null => {
  // 跳过按钮类型的菜单
  if (menu.type === HbtMenuType.Button) {
    return null
  }

  // 处理路径
  const path = menu.path || ''
  // 如果是根级菜单，确保以/开头
  const routePath = parentPath ? path : (path.startsWith('/') ? path : `/${path}`)
  // 组合完整路径（用于组件导入和子菜单）
  const fullPath = parentPath ? `${parentPath}/${path}` : routePath

  console.log('[路由] 处理菜单:', {
    menuName: menu.name,
    type: menu.type,
    path: path,
    routePath: routePath,
    fullPath: fullPath,
    component: menu.component,
    parentPath: parentPath
  })

  // 基础路由配置
  const route: Partial<RouteRecordRaw> = {
    path: routePath,
    name: menu.name,
    meta: {
      title: menu.transKey ? i18n.global.t(menu.transKey) : menu.name,
      icon: menu.icon,
      requiresAuth: true,
      perms: menu.permission
    }
  }

  // 处理组件
  if (menu.type === HbtMenuType.Directory) {
    // 目录类型使用Layout组件
    route.component = Layout
    // 目录类型添加重定向到第一个子菜单
    if (menu.children?.length) {
      const firstChild = menu.children[0]
      route.redirect = `${fullPath}/${firstChild.path}`
    }
  } else if (menu.component) {
    // 菜单类型使用指定的组件
    try {
      route.component = () => import(`@/views/${menu.component}.vue`)
      console.log('[路由] 加载组件:', menu.component)
    } catch (error) {
      console.error(`[路由] 组件加载失败: ${menu.component}`, error)
      route.component = () => import('@/views/error/404.vue')
    }
  } else {
    console.warn('[路由] 菜单缺少组件路径:', menu.name)
    route.component = () => import('@/views/error/404.vue')
  }

  // 如果有子菜单，递归处理
  if (menu.children?.length) {
    route.children = menu.children
      .map(child => menuToRoute(child, fullPath))
      .filter((route): route is RouteRecordRaw => route !== null)
  }

  return route as RouteRecordRaw
}

// 注册动态路由
export const registerDynamicRoutes = async (menus: Menu[]): Promise<boolean> => {
  try {
    console.log('[路由] 开始注册动态路由:', menus)

    // 移除所有非基础路由
    router.getRoutes().forEach(route => {
      if (route.name && !BASIC_ROUTES.includes(route.name.toString())) {
        router.removeRoute(route.name)
      }
    })

    // 递归处理菜单
    const processMenus = async (items: Menu[]) => {
      for (const menu of items) {
        const route = menuToRoute(menu, '')
        if (route) {
          // 将路由添加到根路由下
          router.addRoute('/', route)
          console.log('[路由] 注册路由:', {
            path: route.path,
            name: route.name,
            children: route.children?.length
          })
        }
      }
    }

    // 处理所有菜单
    await processMenus(menus)

    // 打印最终路由
    console.log('[路由] 动态路由注册完成:', router.getRoutes())
    return true
  } catch (error) {
    console.error('[路由] 注册动态路由失败:', error)
    return false
  }
}

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

    // 如果没有菜单，加载菜单并注册动态路由
    if (!menuStore.rawMenuList?.length) {
      if (menuStore.isLoading) {
        next()
        return
      }

      try {
        menuStore.isLoading = true
        const success = await menuStore.loadUserMenus()
        
        if (success) {
          // 注册动态路由
          const registered = await registerDynamicRoutes(menuStore.rawMenuList)
          
          if (registered) {
            // 如果当前路由不存在，重定向到目标路由
            if (to.name === undefined) {
              // 等待路由注册完成
              await router.isReady()
              // 重新导航到目标路由
              next({ ...to, replace: true })
              return
            }
          } else {
            console.error('[路由守卫] 动态路由注册失败')
            message.error('加载菜单失败，请重试')
            next(false)
            return
          }
        }
        
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
        console.error('[路由守卫] 加载菜单失败:', error)
        message.error('加载菜单失败，请重试')
        next(false)
        return
      }
    }

    next()
  } catch (error) {
    console.error('[路由守卫] 错误:', error)
    next(false)
  }
})

export default router 