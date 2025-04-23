import type { RouteRecordRaw } from 'vue-router'
import type { Menu } from '@/types/identity/menu'
import { useRouter } from 'vue-router'
import { Router } from 'vue-router'

const LAYOUT = () => import('@/layouts/default/index.vue')

/**
 * 格式化菜单数据为路由格式
 */
export function formatMenuToRoutes(menus: Menu[]): RouteRecordRaw[] {
  return menus.map(menu => {
    console.log('[路由] 处理菜单项:', {
      菜单ID: menu.menuId,
      名称: menu.menuName,
      路径: menu.path,
      组件: menu.component,
      类型: menu.menuType,
      状态: menu.status
    })

    // 构造基础路由对象
    const route = {
      path: `/${menu.path.replace(/^\/+/, '')}`,  // 确保路径以单个/开头
      name: `HbtMenu_${menu.menuId}`,  // 仅使用menuId作为唯一标识
      redirect: '',  // 默认重定向为空
      component: () => import('@/layouts/BasicLayout/index.vue'),  // 父路由使用基础布局
      children: [] as RouteRecordRaw[],  // 初始化空的子路由数组
      meta: {
        title: menu.menuName || '',
        icon: menu.icon,
        hidden: menu.hidden,
        keepAlive: menu.keepAlive,
        permission: menu.permission,
        requiresAuth: true,
        menuId: menu.menuId,
        orderNum: menu.orderNum,
        menuType: menu.menuType
      }
    } as RouteRecordRaw

    // 处理子路由
    if (menu.children && menu.children.length > 0) {
      console.log('[路由] 处理子菜单:', {
        父菜单: menu.menuName,
        父路径: route.path,
        子菜单数量: menu.children.length,
        子菜单: menu.children.map(child => ({
          名称: child.menuName,
          路径: child.path,
          组件: child.component
        }))
      })

      // 处理子路由
      route.children = menu.children.map(child => {
        // 确保子路由路径不以斜杠开头
        const childPath = child.path.replace(/^\/+/, '')
        
        // 构造子路由
        const childRoute = {
          path: childPath,
          name: `HbtMenu_${child.menuId}`,
          component: child.component 
            ? () => {
                // 使用相对路径，因为动态导入不支持别名
                const componentPath = `../views/${child.component}.vue`
                console.log('[路由] 加载子路由组件:', {
                  菜单名称: child.menuName,
                  组件路径: componentPath,
                  原始组件: child.component,
                  完整路径: `${route.path}/${childPath}`
                })
                return import(/* @vite-ignore */ componentPath)
              }
            : () => import('@/views/error/404.vue'),  // 静态导入可以使用别名
          children: [] as RouteRecordRaw[],  // 初始化空的子路由数组
          meta: {
            title: child.menuName || '',
            icon: child.icon,
            hidden: child.hidden,
            keepAlive: child.keepAlive,
            permission: child.permission,
            requiresAuth: true,
            menuId: child.menuId,
            orderNum: child.orderNum,
            menuType: child.menuType,
            parentId: menu.menuId  // 添加父菜单ID
          }
        } as RouteRecordRaw

        // 如果子路由还有子路由，递归处理
        if (child.children?.length) {
          childRoute.children = child.children.map(grandChild => ({
            path: grandChild.path.replace(/^\/+/, ''),
            name: `HbtMenu_${grandChild.menuId}`,
            component: grandChild.component 
              ? () => import(/* @vite-ignore */ `../views/${grandChild.component}.vue`)
              : () => import('@/views/error/404.vue'),
            children: [] as RouteRecordRaw[],
            meta: {
              title: grandChild.menuName || '',
              icon: grandChild.icon,
              hidden: grandChild.hidden,
              keepAlive: grandChild.keepAlive,
              permission: grandChild.permission,
              requiresAuth: true,
              menuId: grandChild.menuId,
              orderNum: grandChild.orderNum,
              menuType: grandChild.menuType,
              parentId: child.menuId
            }
          } as RouteRecordRaw))
        }

        return childRoute
      })

      // 设置重定向到第一个子路由
      const firstChild = menu.children[0]
      const firstChildPath = firstChild.path.replace(/^\/+/, '')
      route.redirect = `${route.path}/${firstChildPath}`
      
      console.log('[路由] 设置重定向:', {
        从: route.path,
        到: route.redirect,
        第一个子路由: {
          名称: firstChild.menuName,
          路径: firstChildPath,
          组件: firstChild.component,
          完整路径: `${route.path}/${firstChildPath}`
        }
      })
    }

    return route
  })
}

/**
 * 过滤路由数据
 */
export function filterAsyncRoutes<T extends { meta?: { permission?: string }, children?: T[], path?: string }>(routes: T[], permissions: string[]): T[] {
  console.log('[路由] 开始过滤路由:', {
    路由数量: routes.length,
    权限列表: permissions
  })

  return routes.filter(route => {
    // 检查路由权限
    if (route.meta?.permission && typeof route.meta.permission === 'string') {
      const hasPermission = permissions.includes(route.meta.permission)
      console.log('[路由] 权限检查:', {
        路径: route.path,
        权限: route.meta.permission,
        结果: hasPermission
      })
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

export const registerDynamicRoutes = async (menus: Menu[], router: Router) => {
  try {
    console.log('[路由] 开始注册动态路由:', {
      菜单数量: menus.length,
      当前路由: router.getRoutes()
    })

    // 先清理已存在的动态路由
    router.getRoutes()
      .filter(route => route.name?.toString().startsWith('HbtMenu_'))
      .forEach(route => {
        if (route.name) {
          router.removeRoute(route.name)
        }
      })

    const routes = formatMenuToRoutes(menus)
    routes.forEach(route => {
      try {
        router.addRoute(route)
        console.log('[路由] 添加路由成功:', {
          路径: route.path,
          名称: route.name,
          组件: route.component?.name,
          子路由: route.children?.map(child => ({
            路径: child.path,
            名称: child.name,
            完整路径: `${route.path}/${child.path}`
          }))
        })
      } catch (error) {
        console.error('[路由] 添加路由失败:', {
          路径: route.path,
          名称: route.name,
          错误: error
        })
      }
    })

    // 验证路由注册状态
    const registeredRoutes = router.getRoutes()
    console.log('[路由] 已注册的路由:', registeredRoutes.map(route => ({
      名称: route.name,
      路径: route.path,
      子路由: route.children?.map(child => ({
        路径: child.path,
        名称: child.name,
        完整路径: `${route.path}/${child.path}`
      }))
    })))

    console.log('[路由] 动态路由注册完成')
  } catch (error) {
    console.error('[路由] 注册动态路由失败:', error)
  }
} 