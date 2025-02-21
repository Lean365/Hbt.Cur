import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw, RouteRecordSingleViewWithChildren } from 'vue-router'
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
      title: 'menu.login',
      requiresAuth: false,
      transKey: 'menu.login'
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
          title: 'menu.home',
          icon: 'HomeOutlined',
          requiresAuth: true,
          transKey: 'menu.home'
        }
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        redirect: '/dashboard/workplace',
        meta: { 
          title: 'menu.dashboard.title', 
          icon: 'DashboardOutlined', 
          requiresAuth: true,
          transKey: 'menu.dashboard.title'
        },
        children: [
          {
            path: 'workplace',
            name: 'Workplace',
            component: () => import('@/views/dashboard/workplace/index.vue'),
            meta: { 
              title: 'menu.dashboard.workplace', 
              icon: 'DesktopOutlined', 
              requiresAuth: true,
              transKey: 'menu.dashboard.workplace'
            }
          },
          {
            path: 'analysis',
            name: 'Analysis',
            component: () => import('@/views/dashboard/analysis/index.vue'),
            meta: { 
              title: 'menu.dashboard.analysis', 
              icon: 'BarChartOutlined', 
              requiresAuth: true,
              transKey: 'menu.dashboard.analysis'
            }
          },
          {
            path: 'monitor',
            name: 'Monitor',
            component: () => import('@/views/dashboard/monitor/index.vue'),
            meta: { 
              title: 'menu.dashboard.monitor', 
              icon: 'MonitorOutlined', 
              requiresAuth: true,
              transKey: 'menu.dashboard.monitor'
            }
          }
        ]
      },
      {
        path: '/about',
        redirect: '/about/index',
        meta: { 
          title: 'menu.about.title', 
          icon: 'InfoCircleOutlined',
          transKey: 'menu.about.title'
        },
        children: [
          {
            path: 'index',
            name: 'About',
            component: () => import('@/views/about/index.vue'),
            meta: { 
              title: 'menu.about.index', 
              icon: 'InfoCircleOutlined',
              transKey: 'menu.about.index'
            }
          },
          {
            path: 'terms',
            name: 'Terms',
            component: () => import('@/views/about/terms.vue'),
            meta: { 
              title: 'menu.about.terms', 
              icon: 'FileTextOutlined',
              transKey: 'menu.about.terms'
            }
          },
          {
            path: 'privacy',
            name: 'Privacy',
            component: () => import('@/views/about/privacy.vue'),
            meta: { 
              title: 'menu.about.privacy', 
              icon: 'SafetyOutlined',
              transKey: 'menu.about.privacy'
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

// 菜单转换为路由
const menuToRoute = (menu: Menu, parentPath: string): RouteRecordRaw | null => {
  try {
    // 处理路径
    let routePath = menu.path || ''
    if (routePath.startsWith('/')) {
      routePath = routePath.slice(1)
    }

    // 构建完整路径
    const fullPath = parentPath
      ? `${parentPath}/${routePath}`.replace(/\/+/g, '/')
      : `/${routePath}`.replace(/\/+/g, '/')

    console.log('[路由] 处理菜单:', {
      菜单ID: menu.menuId,
      菜单名称: menu.menuName,
      菜单类型: menu.menuType,
      原始路径: menu.path,
      处理后路径: routePath,
      父级路径: parentPath,
      完整路径: fullPath,
      组件: menu.component
    })

    // 基础路由配置
    const route: RouteRecordRaw = {
      path: routePath,
      name: `menu_${menu.menuId}`,
      redirect: undefined,
      component: undefined,
      children: [],
      meta: {
        title: menu.transKey ? i18n.global.t(menu.transKey) : menu.menuName,
        icon: menu.icon,
        perms: menu.perms,
        isCache: menu.isCache,
        transKey: menu.transKey // 保存翻译键，以便后续切换语言时使用
      }
    }

    // 处理目录类型菜单
    if (menu.menuType === HbtMenuType.Directory) {
      route.component = Layout
      
      // 如果有子菜单，设置重定向到第一个子菜单
      if (menu.children?.length) {
        const firstChild = menu.children[0]
        const childPath = firstChild.path.startsWith('/') ? firstChild.path.slice(1) : firstChild.path
        route.redirect = `${fullPath}/${childPath}`.replace(/\/+/g, '/')
        console.log('[路由] 设置重定向:', {
          从: fullPath,
          到: route.redirect
        })
      }
    } else {
      // 处理菜单类型
      if (menu.component) {
        try {
          const componentPath = `@/views/${menu.component}.vue`
          console.log('[路由] 加载组件:', {
            菜单路径: menu.path,
            组件路径: menu.component,
            完整路径: componentPath
          })
          route.component = () => import(componentPath)
        } catch (error) {
          console.error('[路由] 加载组件失败:', error)
          route.component = () => import('@/views/error/404.vue')
        }
      }
    }

    // 处理子菜单
    if (menu.children?.length) {
      route.children = []
      for (const child of menu.children) {
        const childRoute = menuToRoute(child, fullPath)
        if (childRoute) {
          route.children.push(childRoute)
        }
      }
    }

    return route
  } catch (error) {
    console.error('[路由] 转换菜单失败:', error)
    return null
  }
}

// 注册动态路由
export const registerDynamicRoutes = async (menus: Menu[]): Promise<boolean> => {
  try {
    console.log('[路由] 开始注册动态路由:', {
      菜单数量: menus.length,
      菜单列表: menus.map(m => ({
        ID: m.menuId,
        名称: m.menuName,
        路径: m.path,
        类型: m.menuType,
        组件: m.component
      }))
    })

    // 移除所有非基础路由
    const existingRoutes = router.getRoutes()
    console.log('[路由] 现有路由:', existingRoutes.map(r => ({
      路径: r.path,
      名称: r.name,
      完整路径: r.path
    })))

    existingRoutes.forEach(route => {
      if (route.name && !BASIC_ROUTES.includes(route.name.toString())) {
        router.removeRoute(route.name)
        console.log('[路由] 移除路由:', {
          路径: route.path,
          名称: route.name
        })
      }
    })

    // 递归处理菜单
    const processMenus = async (items: Menu[]) => {
      for (const menu of items) {
        const route = menuToRoute(menu, '')
        if (route) {
          // 将路由添加为根路由的子路由
          router.addRoute('/', route)
          console.log('[路由] 注册路由:', {
            路径: route.path,
            名称: route.name,
            组件: route.component ? '已设置' : '未设置',
            重定向: route.redirect,
            子路由数量: route.children?.length || 0,
            完整路由: route
          })
        }
      }
    }

    // 处理所有菜单
    await processMenus(menus)

    // 打印最终路由配置
    console.log('[路由] 最终路由配置:', router.getRoutes().map(r => ({
      路径: r.path,
      名称: r.name,
      组件: r.components?.default ? '已设置' : '未设置',
      子路由: r.children?.map(c => ({
        路径: c.path,
        名称: c.name,
        组件: c.components?.default ? '已设置' : '未设置'
      }))
    })))

    return true
  } catch (error) {
    console.error('[路由] 注册动态路由失败:', error)
    return false
  }
}

// 路由守卫
router.beforeEach(async (to, from, next) => {
  // 设置页面标题
  const title = to.meta.transKey ? i18n.global.t(to.meta.transKey as string) : to.meta.title
  if (typeof title === 'string') {
    document.title = `${title} - ${i18n.global.t('app.name')}`
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
              next({ path: to.fullPath, replace: true })
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