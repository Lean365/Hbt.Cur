import { createRouter, createWebHistory, type Router, type RouteRecordRaw } from 'vue-router'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import type { HbtMenu } from '@/types/identity/menu'
import { HbtMenuType } from '@/types/identity/menu'
import { message } from 'ant-design-vue'

import { clearAutoLogout } from '@/utils/autoLogout'

// 使用相对路径导入所有 Vue 组件
const modules = import.meta.glob('../views/**/*.vue')

// 页面组件加载函数
const loadView = (view: string) => {
  try {
    // 规范化组件路径
    const normalizedPath = view.replace(/^\/+|\/+$/g, '') // 移除开头和结尾的所有斜杠
    
    // 如果路径为空，直接返回404组件
    if (!normalizedPath) {
      return () => import('@/views/error/404.vue')
    }

    // 使用后端返回的组件路径
    const modulePath = `../views/${normalizedPath}.vue`
    const availableModules = Object.keys(modules)

    const component = () => {
      return new Promise((resolve, reject) => {
        try {
          if (modules[modulePath]) {
            modules[modulePath]()
              .then((module: any) => {
                resolve(module.default || module)
              })
              .catch((error: Error) => {
                console.error('[路由] 组件加载失败:', error.message)
                message.error(`组件 ${view} 加载失败: ${error.message}`)
                import('@/views/error/404.vue').then(resolve)
              })
          } else {
            console.error('[路由] 组件不存在:', modulePath)
            message.error(`组件 ${view} 不存在，请检查组件路径配置`)
            import('@/views/error/404.vue').then(resolve)
          }
        } catch (error) {
          const err = error as Error
          console.error('[路由] 组件导入错误:', err.message)
          message.error(`组件导入错误: ${err.message}`)
          import('@/views/error/404.vue').then(resolve)
        }
      })
    }

    return component
  } catch (error: unknown) {
    const err = error as Error
    console.error('[路由] 组件加载配置失败:', err.message)
    message.error(`组件配置失败: ${err.message}`)
    return () => import('@/views/error/404.vue')
  }
}

// 基础路由
export const constantRoutes: RouteRecordRaw[] = [
  {
    path: '/redirect',
    component: () => import('@/layouts/index.vue'),
    children: [
      {
        path: ':path(.*)*',
        name: 'Redirect',
        component: () => import('@/components/Navigation/HbtPageRedirect/index.vue'),
        meta: {
          title: 'redirect',
          requiresAuth: true,
          hidden: true
        }
      }
    ]
  },
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
    component: () => import('@/layouts/index.vue'),
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
        path: 'identity/user/profile',
        name: 'UserProfile',
        component: () => import('@/views/identity/user/components/UserProfile.vue'),
        meta: {
          title: 'identity.user.profile',
          icon: 'ProfileOutlined',
          requiresAuth: true,
          transKey: 'menu.identity.user.profile',
          hidden: true
        }
      },
      {
        path: 'identity/user/change-password',
        name: 'ChangePwdForm',
        component: () => import('@/views/identity/user/components/ChangePwdForm.vue'),
        meta: {
          title: '修改密码',
          icon: 'lock',
          requiresAuth: true,
          transKey: 'identity.user.changePassword',
          hidden: true
        }
      },
      {
        path: 'components',
        name: 'Components',
        redirect: '/components/icons',
        meta: {
          title: 'menu.components.title',
          icon: 'AppstoreOutlined',
          requiresAuth: true,
          transKey: 'menu.components.title'
        },
        children: [
          {
            path: 'icons',
            name: 'Icons',
            component: () => import('@/components/Base/HbtIcon/index.vue'),
            meta: {
              title: 'menu.components.icons',
              icon: 'StarOutlined',
              requiresAuth: true,
              transKey: 'menu.components.icons'
            }
          }
        ]
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
      },
      {
        path: '/notification',
        name: 'Notification',
        redirect: '/notification/center',
        meta: {
          title: 'menu.notification.title',
          icon: 'BellOutlined',
          requiresAuth: true,
          transKey: 'menu.notification.title'
        },
        children: [
          {
            path: 'center',
            name: 'NotificationCenter',
            component: () => import('@/components/Base/HbtNotificationCenter/index.vue'),
            meta: {
              title: 'menu.notification.center',
              icon: 'NotificationOutlined',
              requiresAuth: true,
              transKey: 'menu.notification.center'
            }
          }
        ]
      },
      {
        path: '/404',
        name: 'NotFound',
        component: () => import('@/views/error/404.vue'),
        meta: {
          title: '404',
          hidden: true,
          requiresAuth: false
        }
      }
    ]
  }
]



// 创建路由实例
const router = createRouter({
  history: createWebHistory(),
  routes: constantRoutes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } else {
      return { top: 0 }
    }
  }
})

// 动态路由注册
export async function registerDynamicRoutes(router: Router, menus?: HbtMenu[]) {
  try {
    let menuData = menus
    if (!menuData) {
      const menuStore = useMenuStore()
      const userStore = useUserStore()
      
      // 加载菜单数据
      menuData = await menuStore.reloadMenus(router)
    }
    
    if (!menuData || menuData.length === 0) {
      return false
    }
    
    // 过滤路由权限
    const userStore = useUserStore()
    const filteredMenus = filterAsyncRoutes<HbtMenu>(menuData, userStore.permissions)

    // 清理现有路由
    router.getRoutes()
      .filter(route => route.name?.toString().startsWith('HbtMenu_'))
      .forEach(route => {
        if (route.name) {
          router.removeRoute(route.name)
        }
      })
    
    // 注册所有路由
    const processMenus = (menus: HbtMenu[]) => {
      // 创建动态路由数组
      const dynamicRoutes: RouteRecordRaw[] = []

      // 处理菜单项
      const processMenuItem = (menu: HbtMenu, parentPath: string = ''): RouteRecordRaw => {
        const routeName = `HbtMenu_${menu.menuId}`
        
        // 处理路径：确保所有路径都以/开头
        let routePath = ''
        if (parentPath) {
          // 子菜单：父路径 + 当前路径
          const currentPath = menu.path.startsWith('/') ? menu.path.slice(1) : menu.path
          routePath = `${parentPath}/${currentPath}`.replace(/\/+/g, '/')
        } else {
          // 顶级菜单：确保路径以/开头
          routePath = menu.path.startsWith('/') ? menu.path : `/${menu.path}`
        }
        
        const route: RouteRecordRaw = {
          path: routePath,
          name: routeName,
          component: menu.menuType === HbtMenuType.Directory 
            ? (parentPath ? () => import('@/components/Navigation/HbtMenuWrapper/index.vue') : () => import('@/layouts/index.vue'))
            : loadView(menu.component || ''),
          meta: {
            title: menu.menuName || '',
            transKey: menu.transKey, // 添加 transKey 字段
            icon: menu.icon,
            hidden: menu.visible === 1,
            keepAlive: menu.isCache === 1,
            permission: menu.perms,
            requiresAuth: true,
            menuId: menu.menuId,
            orderNum: menu.orderNum,
            hideBreadcrumb: menu.menuType === HbtMenuType.Directory // 目录类型隐藏面包屑
          },
          children: [] as RouteRecordRaw[],
          redirect: undefined
        }

        if (menu.children?.length) {
          // 递归处理子菜单，传递当前完整路径作为父路径
          route.children = menu.children.map(child => processMenuItem(child, routePath))
          // 设置重定向到第一个子菜单
          if (route.children.length > 0) {
            route.redirect = `${routePath}/${route.children[0].path}`
          }
        }

        return route
      }

      // 处理所有顶级菜单
      menus.forEach((menu, index) => {
        if (menu.menuType === HbtMenuType.Directory) {
          const route = processMenuItem(menu)
          dynamicRoutes.push(route)
        } else if (menu.menuType === HbtMenuType.Menu) {
          // 处理顶级页面菜单
          const route = processMenuItem(menu)
          dynamicRoutes.push(route)
        }
      })

      // 批量注册所有动态路由作为顶级路由
      dynamicRoutes.forEach(route => {
        try {
          // 直接添加为顶级路由
          router.addRoute(route)
        } catch (error) {
          console.error('[路由] 注册路由失败:', route.path, error)
        }
      })

      return true
    }

    // 处理所有菜单
    processMenus(filteredMenus)

    // 添加通配符路由
    router.addRoute({
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: () => import('@/views/error/404.vue'),
      meta: {
        title: '404',
        hidden: true,
        requiresAuth: false
      }
    })

    return true
  } catch (error) {
    console.error('[路由] 动态路由注册失败:', error)
    return false
  }
}

// 路由守卫
router.beforeEach(async (to, from, next) => {
  const token = getToken()
  const userStore = useUserStore()
  const menuStore = useMenuStore()

  // 不需要登录的页面直接放行
  if (to.meta.requiresAuth === false) {
    clearAutoLogout() // 清理自动登出
    next()
    return
  }

  // 未登录时跳转到登录页
  if (!token) {
    clearAutoLogout() // 清理自动登出
    if (to.path !== '/login') {
      next({ path: '/login', query: { redirect: to.fullPath } })
    } else {
      next()
    }
    return
  }

  // 已登录时访问登录页，跳转到首页
  if (to.path === '/login') {
    next({ path: '/' })
    return
  }

  try {
    // 初始化用户信息
    if (!userStore.userInfo) {
      // 登录时强制刷新用户信息，刷新页面时使用缓存
      const isLogin = from.path === '/login'
      const userInfo = await userStore.getUserInfo(isLogin)
      
      // 只有在登录时获取用户信息失败才跳转到登录页
      if (!userInfo && isLogin) {
        console.warn('[路由] 登录时获取用户信息失败，跳转到登录页')
        next({ path: '/login', query: { redirect: to.fullPath } })
        return
      }
    }

    // 自动登出功能已在 App.vue 中初始化，这里不需要重复初始化
    // initAutoLogout(userStore)

    // 检查是否需要初始化菜单和动态路由
    const needInitRoutes =
      !menuStore.rawMenuList?.length ||
      router.getRoutes().filter(r => r.name?.toString().startsWith('HbtMenu_')).length === 0

    if (needInitRoutes) {
      // 登录时强制刷新菜单，刷新页面时使用缓存
      const isLogin = from.path === '/login'
      const menus = await menuStore.reloadMenus(router, isLogin)
      
      // 只有在登录时菜单加载失败才跳转到登录页
      if (!menus && isLogin) {
        next({ path: '/login', query: { redirect: to.fullPath } })
        return
      }
      
      if (!menus || menus.length === 0) {
        next()
        return
      }
      
      // 等待路由注册完成
      let retryCount = 0
      const maxRetries = 10
      const retryInterval = 50
      
      while (retryCount < maxRetries) {
        // 检查目标路由是否已注册
        const matchedRoute = router.resolve(to.fullPath)
        if (matchedRoute.matched.length > 0) {
          break
        }
        
        await new Promise(resolve => setTimeout(resolve, retryInterval))
        retryCount++
      }

      // 再次检查路由是否存在
      const finalCheck = router.resolve(to.fullPath)
      if (finalCheck.matched.length === 0) {
        // 导航到404页面
        next({ path: '/404', replace: true })
        return
      }
    }
    next()
  } catch (error) {
    console.error('[路由守卫] 错误:', error)
    // 发生错误时，清除token并跳转到登录页
    removeToken()
    next({ path: '/login', query: { redirect: to.fullPath } })
  }
})

/**
 * 过滤路由数据
 */
function filterAsyncRoutes<T extends { meta?: { permission?: string }, children?: T[], path?: string }>(routes: T[], permissions: string[]): T[] {
  return routes.filter(route => {
    // 检查路由权限
    if (route.meta?.permission && typeof route.meta.permission === 'string') {
      const hasPermission = permissions.includes(route.meta.permission)
      return hasPermission
    }
    
    // 如果有子路由，递归过滤
    if (route.children) {
      route.children = filterAsyncRoutes(route.children, permissions)
      return route.children.length > 0
    }
    
    return true
  })
}

// 路由工具函数
export const findRouteByPath = (path: string): RouteRecordRaw | undefined => {
  // 标准化路径
  const normalizedPath = path.replace(/\/+/g, '/')

  // 在路由表中查找匹配的路由
  const matchedRoute = router.resolve(normalizedPath)
  if (matchedRoute.matched.length) {
    return matchedRoute.matched[matchedRoute.matched.length - 1]
  }

  return undefined
}

// 处理路由导航
export const handleRouteNavigation = async (path: string): Promise<boolean> => {
  try {
    // 确保路径以斜杠开头
    const normalizedPath = path.startsWith('/') ? path : `/${path}`

    // 获取目标路由信息
    const matchedRoute = router.resolve(normalizedPath)

    if (!matchedRoute.matched.length) {
      // 导航到404页面
      await router.push({ path: '/404', replace: true })
      return false
    }

    // 执行导航
    await router.push({ path: normalizedPath, replace: true })
    return true
  } catch (error) {
    // 导航失败时跳转到404页面
    try {
      await router.push({ path: '/404', replace: true })
    } catch (fallbackError) {
      console.error('[路由] 404页面导航失败:', fallbackError)
    }
    
    return false
  }
}

export default router