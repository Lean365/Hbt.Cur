import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { getToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import type { Menu } from '@/types/identity/menu'
import { MenuType } from '@/types/identity/menu'
import { message } from 'ant-design-vue'
import i18n from '@/locales'

// 页面组件加载函数
const loadView = (view: string) => {
  // console.log('[路由] 开始加载组件:', {
  //   组件路径: view,
  //   完整路径: `/src/views/${view}.vue`,
  //   当前路由: router.currentRoute.value.path,
  //   可用模块: Object.keys(import.meta.glob('/src/views/**/*.vue'))
  // })

  try {
    // 规范化组件路径
    const normalizedPath = view.replace(/\//g, '_').replace(/\./g, '_')

    // console.log('[路由] 组件加载配置:', {
    //   原始路径: view,
    //   规范化路径: normalizedPath,
    //   加载方式: '动态导入',
    //   当前路由状态: {
    //     路径: router.currentRoute.value.path,
    //     参数: router.currentRoute.value.params,
    //     查询: router.currentRoute.value.query
    //   }
    // })

    const component = () => {
      console.log('[路由] 执行组件加载:', view)

      // 使用 Vite 的动态导入
      return new Promise((resolve, reject) => {
        try {
          // 构建导入表达式
          const modules = import.meta.glob('/src/views/**/*.vue')
          const modulePath = `/src/views/${view}.vue`

          // console.log('[路由] 组件加载详情:', {
          //   路径: modulePath,
          //   模块列表: Object.keys(modules),
          //   是否存在: modules[modulePath] !== undefined
          // })

          if (modules[modulePath]) {
            modules[modulePath]()
              .then((module: any) => {
                // console.log('[路由] 组件加载成功:', {
                //   路径: view,
                //   模块: module,
                //   组件名称: module.default?.name,
                //   组件定义: Object.keys(module.default || {})
                // })
                resolve(module.default || module)
              })
              .catch((error: Error) => {
                // console.error('[路由] 组件加载失败:', {
                //   路径: view,
                //   错误: error,
                //   错误堆栈: error.stack,
                //   错误类型: error.name,
                //   错误信息: error.message
                // })
                message.error(`组件 ${view} 加载失败: ${error.message}`)
                import('@/views/error/404.vue').then(resolve)
              })
          } else {
            console.error('[路由] 组件不存在:', {
              查找路径: modulePath,
              可用组件: Object.keys(modules)
            })
            message.error(`组件 ${view} 不存在`)
            import('@/views/error/404.vue').then(resolve)
          }
        } catch (error) {
          const err = error as Error
          console.error('[路由] 组件导入错误:', {
            路径: view,
            错误: err,
            错误堆栈: err.stack,
            错误类型: err.name,
            错误消息: err.message
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
      错误信息: err,
      错误堆栈: err.stack,
      错误类型: err.name,
      错误消息: err.message
    })
    message.error(`组件配置失败: ${err.message}`)
    return () => import('@/views/error/404.vue')
  }
}

// 基础路由
export const constantRoutes: RouteRecordRaw[] = [
  {
    path: '/redirect',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: ':path(.*)*',
        name: 'Redirect',
        component: () => import('@/components/Navigation/PageRedirect.vue'),
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
    path: '/404',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: '',
        name: '404',
        component: () => import('@/views/error/404.vue'),
        meta: {
          title: 'error.404.title',
          requiresAuth: true,
          hidden: true,
          ignoreError: true,
          transKey: 'error.404.title'
        }
      }
    ]
  },
  {
    path: '/403',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: '',
        name: '403',
        component: () => import('@/views/error/403.vue'),
        meta: {
          title: 'error.403.title',
          requiresAuth: false,
          hidden: true,
          ignoreError: true,
          transKey: 'error.403.title'
        }
      }
    ]
  },
  {
    path: '/500',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: '',
        name: '500',
        component: () => import('@/views/error/500.vue'),
        meta: {
          title: 'error.500.title',
          requiresAuth: true,
          hidden: true,
          ignoreError: true,
          transKey: 'error.500.title'
        }
      }
    ]
  },
  {
    path: '/401',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: '',
        name: '401',
        component: () => import('@/views/error/401.vue'),
        meta: {
          title: 'error.401.title',
          requiresAuth: false,
          hidden: true,
          ignoreError: true,
          transKey: 'error.401.title'
        }
      }
    ]
  },
  {
    path: '/503',
    component: () => import('@/layouts/BasicLayout.vue'),
    children: [
      {
        path: '',
        name: '503',
        component: () => import('@/views/error/503.vue'),
        meta: {
          title: 'error.503.title',
          requiresAuth: false,
          hidden: true,
          ignoreError: true,
          transKey: 'error.503.title'
        }
      }
    ]
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

// 动态路由根节点
const dynamicRouteRoot: RouteRecordRaw = {
  path: '/',
  name: 'DynamicRoot',
  component: () => import('@/layouts/BasicLayout.vue'),
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

// 注册动态路由
export const registerDynamicRoutes = async (menus: Menu[]): Promise<boolean> => {
  try {
    if (!menus?.length) {
      console.warn('[路由] 菜单列表为空，跳过注册')
      return false
    }

    // console.log('[路由] 开始注册动态路由:', {
    //   菜单数量: menus.length,
    //   菜单列表: menus.map(m => ({
    //     ID: m.menuId,
    //     名称: m.menuName,
    //     路径: m.path,
    //     类型: m.menuType,
    //     父ID: m.parentId,
    //     组件: m.component
    //   }))
    // })

    // 移除现有的动态路由和通配符路由
    router.getRoutes().forEach(route => {
      const routeName = route.name?.toString()
      if (routeName && (routeName.startsWith('HbtMenu_') || routeName === 'NotFound')) {
        router.removeRoute(routeName)
      }
    })

    // 构建路由配置
    const buildRouteConfig = (menu: Menu): RouteRecordRaw => {
      try {
        // 生成唯一的路由名称
        const routeName = `HbtMenu_${menu.path.replace(/\//g, '_')}`

        // 处理路径
        let routePath = menu.path
        if (!menu.parentId) {
          // 顶层路由必须以/开头
          routePath = routePath.startsWith('/') ? routePath : `/${routePath}`
        } else {
          // 子路由不能以/开头
          routePath = routePath.startsWith('/') ? routePath.slice(1) : routePath
        }

        // console.log('[路由] 构建路由配置:', {
        //   菜单路径: menu.path,
        //   路由名称: routeName,
        //   处理后路径: routePath,
        //   菜单类型: menu.menuType,
        //   组件路径: menu.component,
        //   是否顶层: !menu.parentId
        // })

        // 创建路由配置
        const route: RouteRecordRaw = {
          path: routePath,
          name: routeName,
          component:
            menu.menuType === MenuType.DIRECTORY
              ? () => import('@/layouts/BasicLayout.vue')
              : menu.component
                ? loadView(menu.component)
                : () => import('@/views/error/404.vue'),
          children: [], // 初始化为空数组
          meta: {
            title: menu.transKey ? i18n.global.t(menu.transKey) : menu.menuName,
            icon: menu.icon,
            menuId: menu.menuId,
            menuType: menu.menuType,
            requiresAuth: true,
            transKey: menu.transKey
          }
        }

        // 处理子菜单
        if (menu.children?.length) {
          const childRoutes = menu.children
            .filter(child => child.menuType !== MenuType.BUTTON)
            .map(child => buildRouteConfig(child))
          route.children = childRoutes
        }

        return route
      } catch (error) {
        console.error('[路由] 构建路由配置失败:', {
          菜单: menu,
          错误: error
        })
        throw error
      }
    }

    // 注册路由
    const registeredRoutes: RouteRecordRaw[] = []

    // 注册动态路由
    for (const menu of menus) {
      if (!menu.parentId) {
        // 只处理顶层菜单
        try {
          const route = buildRouteConfig(menu)
          // console.log('[路由] 注册顶层路由:', {
          //   路由名称: route.name,
          //   路由路径: route.path,
          //   子路由数量: route.children?.length || 0,
          //   完整配置: route
          // })

          // 添加到根路由
          router.addRoute(route)
          registeredRoutes.push(route)
        } catch (error) {
          console.error('[路由] 注册动态路由失败:', error)
          return false
        }
      }
    }

    // 打印路由表
    console.log('[路由] 动态路由注册完成:', {
      注册数量: registeredRoutes.length,
      路由表: router.getRoutes().map(r => ({
        路径: r.path,
        名称: r.name,
        类型: r.name?.toString().startsWith('HbtMenu_') ? '动态路由' : '静态路由',
        子路由: r.children?.map(c => ({
          路径: c.path,
          名称: c.name
        }))
      }))
    })

    // 最后添加通配符路由
    router.addRoute({
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      redirect: '/404'
    })

    return true
  } catch (error) {
    console.error('[路由] 注册动态路由失败:', error)
    return false
  }
}

// 路由守卫
router.beforeEach(async (to, from, next) => {
  const token = getToken()
  const userStore = useUserStore()
  const menuStore = useMenuStore()

  console.log('[路由守卫] 开始处理路由:', {
    目标路由: to.path,
    来源路由: from.path,
    认证状态: {
      token: !!token,
      用户信息: !!userStore.user,
      菜单: !!menuStore.rawMenuList?.length,
      菜单数量: menuStore.rawMenuList?.length || 0,
      动态路由数量: router.getRoutes().filter(r => r.name?.toString().startsWith('HbtMenu_')).length
    }
  })

  // 登录页面直接放行
  if (to.path === '/login') {
    next()
    return
  }

  // 未登录时统一跳转到登录页
  if (!token) {
    next({ path: '/login', query: { redirect: to.fullPath } })
    return
  }

  try {
    // 初始化用户信息
    if (!userStore.user) {
      await userStore.getUserInfo()
    }

    // 检查是否需要初始化菜单和动态路由
    const needInitRoutes =
      !menuStore.rawMenuList?.length ||
      router.getRoutes().filter(r => r.name?.toString().startsWith('HbtMenu_')).length === 0

    if (needInitRoutes) {
      console.log('[路由守卫] 开始初始化动态路由')
      try {
        // 重新加载菜单
        await menuStore.reloadMenus()
        
        // 注册动态路由
        const success = await registerDynamicRoutes(menuStore.rawMenuList || [])
        if (!success) {
          throw new Error('动态路由注册失败')
        }

        console.log('[路由守卫] 动态路由注册完成，重新导航:', to.fullPath)
        // 使用 replace 模式重新导航，保留完整的路由信息
        next({ path: to.fullPath, replace: true })
        return
      } catch (error) {
        console.error('[路由守卫] 初始化动态路由失败:', error)
        userStore.logout()
        next({ path: '/login' })
        return
      }
    }

    // 路由存在，直接放行
    next()
  } catch (error) {
    console.error('[路由守卫] 错误:', error)
    userStore.logout()
    next({ path: '/login' })
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

    // 打印当前所有路由表
    // console.log(
    //   '[路由] 当前路由表:',
    //   router.getRoutes().map(r => ({
    //     路径: r.path,
    //     名称: r.name,
    //     完整路径: r.path + (r.children?.length ? '/' + r.children.map(c => c.path).join('/') : ''),
    //     子路由: r.children?.map(c => ({
    //       路径: c.path,
    //       名称: c.name,
    //       完整路径: `${r.path}/${c.path}`.replace(/\/+/g, '/')
    //     }))
    //   }))
    // )

    console.log('[路由] 准备导航:', {
      原始路径: path,
      标准路径: normalizedPath,
      当前路径: router.currentRoute.value.path
    })

    // 检查路由是否存在
    const matchedRoute = router.resolve(normalizedPath)

    // 打印匹配过程
    console.log('[路由] 匹配过程:', {
      待匹配路径: normalizedPath,
      父级匹配: matchedRoute.matched.map(m => ({
        路径: m.path,
        名称: m.name,
        完整路径: m.path + (m.children?.length ? '/' + m.children.map(c => c.path).join('/') : '')
      })),
      路由参数: matchedRoute.params,
      查询参数: matchedRoute.query,
      完整路径: matchedRoute.fullPath
    })

    console.log('[路由] 路由匹配结果:', {
      路径: normalizedPath,
      匹配结果: matchedRoute.matched.map(m => ({
        路径: m.path,
        名称: m.name,
        完整路径: m.path + (m.children?.length ? '/' + m.children.map(c => c.path).join('/') : '')
      }))
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
