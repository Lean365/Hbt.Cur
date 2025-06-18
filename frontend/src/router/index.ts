import { createRouter, createWebHistory, type Router, type RouteRecordRaw } from 'vue-router'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import type { HbtMenu } from '@/types/identity/menu'
import { HbtMenuType } from '@/types/identity/menu'
import { message } from 'ant-design-vue'
import i18n from '@/locales'
import { filterAsyncRoutes } from '@/utils/route'
import type { HbtRouteRecordRaw } from '@/types/route'
import { initAutoLogout, clearAutoLogout } from '@/utils/autoLogout'

interface HbtRouteMeta {
  title: string
  icon?: string
  hidden?: boolean
  keepAlive?: boolean
  permission?: string
  requiresAuth: boolean
  menuId?: number
  orderNum?: number
  transKey?: string
}

interface HbtRouteRecord {
  path: string
  name: string
  component?: (() => Promise<unknown>) | undefined
  redirect?: string | undefined
  meta: HbtRouteMeta
  children?: HbtRouteRecord[]
}

// 使用相对路径导入所有 Vue 组件
const modules = import.meta.glob('../views/**/*.vue')

// 页面组件加载函数
const loadView = (view: string) => {
  try {
    // 规范化组件路径
    const normalizedPath = view.replace(/^\/+|\/+$/g, '') // 移除开头和结尾的所有斜杠
    
    // 如果路径为空，直接返回404组件
    if (!normalizedPath) {
      console.warn('[路由] 组件路径为空，返回404页面')
      return () => import('@/views/error/404.vue')
    }

    // 使用后端返回的组件路径
    const modulePath = `../views/${normalizedPath}.vue`
    const availableModules = Object.keys(modules)

    console.log('[路由] 组件加载配置:', {
      原始路径: view,
      规范化路径: normalizedPath,
      模块路径: modulePath,
      是否存在: availableModules.includes(modulePath),
      可用模块列表: availableModules
    })

    const component = () => {
      return new Promise((resolve, reject) => {
        try {
          if (modules[modulePath]) {
            modules[modulePath]()
              .then((module: any) => {
                console.log('[路由] 组件加载成功:', {
                  路径: view,
                  模块路径: modulePath,
                  组件名称: module.default?.name || '未命名组件',
                  组件类型: module.default?.type || '未知类型',
                  异步组件: module.default?.__asyncLoader ? '是' : '否'
                })
                resolve(module.default || module)
              })
              .catch((error: Error) => {
                console.error('[路由] 组件加载失败:', {
                  路径: view,
                  模块路径: modulePath,
                  错误: error.message,
                  错误堆栈: error.stack,
                  尝试加载的完整路径: modulePath
                })
                message.error(`组件 ${view} 加载失败: ${error.message}`)
                import('@/views/error/404.vue').then(resolve)
              })
          } else {
            console.error('[路由] 组件不存在:', {
              查找路径: modulePath,
              原始路径: view,
              规范化路径: normalizedPath,
              可用组件完整列表: availableModules.join('\n'),
              尝试加载的完整路径: modulePath
            })
            message.error(`组件 ${view} 不存在`)
            import('@/views/error/404.vue').then(resolve)
          }
        } catch (error) {
          const err = error as Error
          console.error('[路由] 组件导入错误:', {
            路径: view,
            模块路径: modulePath,
            错误: err.message,
            错误堆栈: err.stack,
            尝试加载的完整路径: modulePath
          })
          message.error(`组件导入错误: ${err.message}`)
          import('@/views/error/404.vue').then(resolve)
        }
      })
    }

    return component
  } catch (error: unknown) {
    const err = error as Error
    console.error('[路由] 组件加载配置失败:', {
      组件路径: view,
      错误信息: err.message,
      错误堆栈: err.stack
    })
    message.error(`组件配置失败: ${err.message}`)
    return () => import('@/views/error/404.vue')
  }
}

// 基础路由
export const constantRoutes: RouteRecordRaw[] = [
  {
    path: '/redirect',
    component: () => import('@/layouts/BasicLayout/index.vue'),
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
    component: () => import('@/layouts/BasicLayout/index.vue'),
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
      }
    ]
  }
]

// 动态路由根节点
const dynamicRouteRoot: RouteRecordRaw = {
  path: '/',
  name: 'DynamicRoot',
  component: () => import('@/layouts/BasicLayout/index.vue'),
  children: []
}

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
export async function registerDynamicRoutes(router: Router) {
  try {
    console.log('[路由守卫] 加载菜单数据')
    const menuStore = useMenuStore()
    const userStore = useUserStore()
    
    // 加载菜单数据
    const menus = await menuStore.reloadMenus(router)
    console.log('[路由守卫] 菜单加载完成:', {
      菜单数量: menus?.length || 0,
      顶级菜单: menus || []
    })
    
    if (!menus || menus.length === 0) {
      console.warn('[路由] 菜单列表为空，跳过注册')
      return false
    }
    
    // 过滤路由权限
    const filteredMenus = filterAsyncRoutes<HbtMenu>(menus, userStore.permissions)
    console.log('[路由] 过滤后的菜单:', filteredMenus)

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
      // 创建根路由
      const rootRoute: RouteRecordRaw = {
        path: '/',
        name: 'Layout',
        component: () => import('@/layouts/BasicLayout/index.vue'),
        redirect: '/home',
        children: [] as RouteRecordRaw[]
      }

      // 处理菜单项
      const processMenuItem = (menu: HbtMenu): RouteRecordRaw => {
        const routeName = `HbtMenu_${menu.menuId}`
        const routePath = menu.path.startsWith('/') ? menu.path.slice(1) : menu.path
        
        const route: RouteRecordRaw = {
          path: routePath,
          name: routeName,
          component: menu.menuType === HbtMenuType.Directory 
            ? () => import('@/layouts/BasicLayout/index.vue')
            : loadView(menu.component || ''),
          meta: {
            title: menu.menuName || '',
            icon: menu.icon,
            hidden: menu.visible === 1,
            keepAlive: menu.isCache === 1,
            permission: menu.perms,
            requiresAuth: true,
            menuId: menu.menuId,
            orderNum: menu.orderNum
          },
          children: [] as RouteRecordRaw[],
          redirect: undefined
        }

        if (menu.children?.length) {
          route.children = menu.children.map(child => processMenuItem(child))
          route.redirect = `/${routePath}/${menu.children[0].path}`
        }

        return route
      }

      // 处理所有顶级菜单
      menus.forEach(menu => {
        if (menu.menuType === HbtMenuType.Directory) {
          const route = processMenuItem(menu)
          rootRoute.children?.push(route)
          
          // console.log('[路由] 注册目录路由:', {
          //   路径: route.path,
          //   名称: route.name,
          //   组件: route.component ? '已配置' : '未配置',
          //   子路由数量: route.children?.length || 0,
          //   重定向: route.redirect || '无'
          // })
        }
      })

      // 注册根路由
      router.addRoute(rootRoute)

      // 验证路由注册状态
      //console.log('[路由] 验证路由注册状态 ============================')
      //const routes = router.getRoutes()
      
      // console.log('[路由] 已注册的路由:', routes.map(route => ({
      //   名称: route.name,
      //   路径: route.path,
      //   完整路径: route.path,
      //   组件: route.components?.default?.name || '未知组件',
      //   子路由数量: route.children?.length || 0,
      //   子路由: route.children?.map(child => ({
      //     路径: child.path,
      //     名称: child.name,
      //     完整路径: `${route.path}/${child.path}`.replace(/\/+/g, '/')
      //   }))
      // })))

      // 特别检查 /admin/configs 路由
      // const adminConfigsRoute = router.resolve('/admin/configs')
      // console.log('[路由] 检查 /admin/configs 路由:', {
      //   是否存在: adminConfigsRoute.matched.length > 0,
      //   匹配路由: adminConfigsRoute.matched,
      //   完整路径: adminConfigsRoute.fullPath,
      //   路由详情: adminConfigsRoute.matched.map(r => ({
      //     名称: r.name,
      //     路径: r.path,
      //     完整路径: r.path,
      //     组件: r.components?.default?.name || '未知组件'
      //   }))
      // })

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

    console.log('[路由] 动态路由注册完成')
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

    // 初始化自动登出
    initAutoLogout(userStore)

    // 检查是否需要初始化菜单和动态路由
    const needInitRoutes =
      !menuStore.rawMenuList?.length ||
      router.getRoutes().filter(r => r.name?.toString().startsWith('HbtMenu_')).length === 0

    if (needInitRoutes) {
      console.log('[路由] 需要初始化路由')
      // 登录时强制刷新菜单，刷新页面时使用缓存
      const isLogin = from.path === '/login'
      const menus = await menuStore.reloadMenus(router, isLogin)
      
      // 只有在登录时菜单加载失败才跳转到登录页
      if (!menus && isLogin) {
        console.warn('[路由] 登录时菜单加载失败，跳转到登录页')
        next({ path: '/login', query: { redirect: to.fullPath } })
        return
      }
      
      if (!menus || menus.length === 0) {
        console.warn('[路由] 菜单列表为空，跳过注册')
        next()
        return
      }
      
      // 等待路由注册完成
      let retryCount = 0
      const maxRetries = 10  // 增加重试次数
      const retryInterval = 50  // 缩短重试间隔
      
      while (retryCount < maxRetries) {
        // 检查目标路由是否已注册
        const matchedRoute = router.resolve(to.fullPath)
        if (matchedRoute.matched.length > 0) {
          console.log('[路由] 目标路由已注册:', {
            路径: to.fullPath,
            匹配路由: matchedRoute.matched.map(r => ({
              名称: r.name,
              路径: r.path,
              完整路径: r.path
            }))
          })
          break
        }
        
        console.log(`[路由] 等待路由注册完成 (${retryCount + 1}/${maxRetries})`, {
          目标路径: to.fullPath,
          当前路由表: router.getRoutes().map(r => ({
            名称: r.name,
            路径: r.path
          }))
        })
        
        await new Promise(resolve => setTimeout(resolve, retryInterval))
        retryCount++
      }

      // 再次检查路由是否存在
      const finalCheck = router.resolve(to.fullPath)
      if (finalCheck.matched.length === 0) {
        console.warn('[路由] 路由注册超时，目标路由未找到:', {
          路径: to.fullPath,
          可用路由: router.getRoutes().map(r => ({
            名称: r.name,
            路径: r.path,
            完整路径: r.path
          }))
        })
        // 如果是刷新页面，尝试重定向到父路由
        if (from.path === '/') {
          const parentPath = to.path.split('/').slice(0, -1).join('/')
          if (parentPath) {
            console.log('[路由] 尝试重定向到父路由:', parentPath)
            next({ path: parentPath })
            return
          }
        }
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

// 路由工具函数
export const findRouteByPath = (path: string): RouteRecordRaw | undefined => {
  console.log('[路由] 开始查找路由:', {
    查找路径: path,
    当前路由表: router.getRoutes()
  })

  // 标准化路径
  const normalizedPath = path.replace(/\/+/g, '/')

  // 在路由表中查找匹配的路由
  const matchedRoute = router.resolve(normalizedPath)
  if (matchedRoute.matched.length) {
    console.log('[路由] 找到匹配的路由:', matchedRoute.matched[matchedRoute.matched.length - 1])
    return matchedRoute.matched[matchedRoute.matched.length - 1]
  }

  console.log('[路由] 未找到匹配的路由:', normalizedPath)
  return undefined
}

// 处理路由导航
export const handleRouteNavigation = async (path: string): Promise<boolean> => {
  try {
    // 确保路径以斜杠开头
    const normalizedPath = path.startsWith('/') ? path : `/${path}`

    // 获取目标路由信息
    const matchedRoute = router.resolve(normalizedPath)
    const targetRoute = matchedRoute.matched[matchedRoute.matched.length - 1]

    console.log('[路由] 准备导航:', {
      原始路径: path,
      标准路径: normalizedPath,
      当前路径: router.currentRoute.value.path,
      目标路由: targetRoute ? {
        名称: targetRoute.name,
        路径: targetRoute.path,
        组件: targetRoute.components?.default?.name || '未知组件',
        元数据: targetRoute.meta
      } : '未找到路由'
    })

    if (!matchedRoute.matched.length) {
      console.error('[路由] 路由不存在:', normalizedPath)
      return false
    }

    // 执行导航
    await router.push({ path: normalizedPath, replace: true })
    console.log('[路由] 导航成功:', normalizedPath)
    return true
  } catch (error) {
    console.error('[路由] 导航失败:', {
      路径: path,
      错误: error
    })
    return false
  }
}

export default router