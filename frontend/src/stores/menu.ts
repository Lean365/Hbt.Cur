import { defineStore } from 'pinia'
import { ref, h } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import type { Menu } from '@/types/identity/menu'
import type { ApiResult } from '@/types/base'
import { getCurrentUserMenus } from '@/api/identity/menu'
import { message } from 'ant-design-vue'
import {
  SettingOutlined,
  ToolOutlined,
  GlobalOutlined,
  TranslationOutlined,
  DatabaseOutlined,
  TableOutlined,
  SafetyOutlined,
  ApartmentOutlined,
  UserOutlined,
  MenuOutlined,
  TeamOutlined,
  SafetyCertificateOutlined,
  IdcardOutlined,
  MonitorOutlined,
  DesktopOutlined,
  DashboardOutlined,
  BarChartOutlined,
  LoginOutlined,
  AuditOutlined,
  HistoryOutlined,
  DiffOutlined,
  ExceptionOutlined,
  CloudServerOutlined,
  UserSwitchOutlined,
  MessageOutlined,
  ClockCircleOutlined
} from '@ant-design/icons-vue'

export const useMenuStore = defineStore('menu', () => {
  // 状态
  const menuList = ref<MenuProps['items']>([])
  const permissions = ref<string[]>([])
  const isLoading = ref(false)

  // 设置菜单列表
  const setMenuList = (list: MenuProps['items']) => {
    menuList.value = list
  }

  // 设置权限列表
  const setPermissions = (perms: string[]) => {
    permissions.value = perms
  }

  // 图标映射
  const iconMap: Record<string, any> = {
    'system': () => h(SettingOutlined),
    'config': () => h(ToolOutlined),
    'language': () => h(GlobalOutlined),
    'translation': () => h(TranslationOutlined),
    'dict-type': () => h(DatabaseOutlined),
    'dict-data': () => h(TableOutlined),
    'auth': () => h(SafetyOutlined),
    'tenant': () => h(ApartmentOutlined),
    'user': () => h(UserOutlined),
    'menu': () => h(MenuOutlined),
    'dept': () => h(TeamOutlined),
    'role': () => h(SafetyCertificateOutlined),
    'post': () => h(IdcardOutlined),
    'monitor': () => h(MonitorOutlined),
    'online': () => h(DesktopOutlined),
    'dashboard': () => h(DashboardOutlined),
    'analysis': () => h(BarChartOutlined),
    'workplace': () => h(DesktopOutlined),
    // 新增日志相关图标
    'login-log': () => h(LoginOutlined),
    'audit-log': () => h(AuditOutlined),
    'operation-log': () => h(HistoryOutlined),
    'diff-log': () => h(DiffOutlined),
    'exception-log': () => h(ExceptionOutlined),
    'service-monitor': () => h(CloudServerOutlined),
    'online-user': () => h(UserSwitchOutlined),
    'online-message': () => h(MessageOutlined),
    'realtime-online': () => h(ClockCircleOutlined)
  }

  // 将后端菜单数据转换为ant-design-vue菜单格式
  const transformMenu = (menus: Menu[]): MenuProps['items'] => {
    console.log('[菜单转换] 开始转换菜单数据:', menus)
    
    const transformedMenus = menus.map(menu => {
      console.log('[菜单转换] 处理菜单项:', {
        id: menu.id,
        name: menu.menuName,
        type: menu.menuType,
        icon: menu.icon,
        path: menu.path,
        component: menu.component
      })
      
      // 处理图标
      let icon = undefined
      if (menu.icon) {
        const iconRenderer = iconMap[menu.icon.toLowerCase()]
        if (iconRenderer) {
          icon = iconRenderer
        } else {
          console.warn('[菜单转换] 未找到图标组件:', menu.icon)
        }
      }

      // 处理路径
      let key = menu.path || ''
      if (!key && menu.menuType === 0) {
        // 如果是目录且没有路径，使用component作为key
        key = menu.component || menu.menuName.toLowerCase()
      } else if (!key) {
        // 如果是菜单且没有路径，使用component作为key
        key = menu.component || String(menu.id)
      }

      // 确保key以/开头，但不重复添加
      key = key.startsWith('/') ? key : `/${key}`

      // 如果是子菜单，需要加上父级路径
      if (menu.parentId !== 0) {
        const parentMenu = menus.find(m => m.id === menu.parentId)
        if (parentMenu && parentMenu.path) {
          const parentPath = parentMenu.path.startsWith('/') ? parentMenu.path : `/${parentMenu.path}`
          key = `${parentPath}/${key.replace(/^\//, '')}`
        }
      }

      console.log('[菜单转换] 处理后的路径:', key)
      
      // 根据菜单类型创建不同的菜单项
      if (menu.menuType === 0) {
        // 目录类型，创建SubMenu
        const submenu = {
          key,
          label: menu.menuName,
          icon,
          children: menu.children && menu.children.length > 0 ? transformMenu(menu.children) : undefined
        }
        console.log('[菜单转换] 创建目录:', submenu)
        return submenu
      } else {
        // 菜单类型，创建MenuItem
        const menuItem = {
          key,
          label: menu.menuName,
          icon
        }
        console.log('[菜单转换] 创建菜单项:', menuItem)
        return menuItem
      }
    })
    
    console.log('[菜单转换] 转换后的菜单列表:', JSON.stringify(transformedMenus, null, 2))
    return transformedMenus
  }

  // 从菜单中提取权限标识
  const extractPermissions = (menus: Menu[]): string[] => {
    const perms: string[] = []
    const extract = (items: Menu[]) => {
      items.forEach(item => {
        if (item.perms) {
          perms.push(...item.perms.split(',').map(p => p.trim()))
        }
        if (item.children && item.children.length > 0) {
          extract(item.children)
        }
      })
    }
    extract(menus)
    return [...new Set(perms)] // 去重
  }

  // 加载用户菜单和权限
  const loadUserMenus = async () => {
    try {
      console.log('[菜单加载] 开始加载用户菜单')
      isLoading.value = true
      
      const response = await getCurrentUserMenus()
      console.log('[菜单加载] API响应:', response)
      
      if (!response) {
        console.error('[菜单加载] API响应无效')
        setMenuList([])
        setPermissions([])
        return true
      }

      // response 是API的标准响应体 { code, msg, data }
      const { code, data, msg } = response as unknown as ApiResult<Menu[]>
      console.log('[菜单加载] 响应数据:', { code, msg, dataLength: data?.length })

      if (code === 200 && data) {
        console.log('[菜单加载] 原始菜单数据:', JSON.stringify(data, null, 2))
        
        const transformedMenus = transformMenu(data)
        console.log('[菜单加载] 转换后的菜单数据:', JSON.stringify(transformedMenus, null, 2))
        
        const extractedPermissions = extractPermissions(data)
        console.log('[菜单加载] 提取的权限数据:', extractedPermissions)
        
        setMenuList(transformedMenus)
        setPermissions(extractedPermissions)
        return true
      }

      console.warn('[菜单加载] 加载失败:', msg)
      // 如果获取失败，设置空菜单
      setMenuList([])
      setPermissions([])
      return true // 返回true让用户可以继续访问
    } catch (error) {
      console.error('[菜单加载] 发生错误:', error)
      message.error('获取用户菜单失败')
      // 出错时也设置空菜单
      setMenuList([])
      setPermissions([])
      return true // 返回true让用户可以继续访问
    } finally {
      isLoading.value = false
      console.log('[菜单加载] 加载完成')
    }
  }

  // 重置状态
  const resetState = () => {
    menuList.value = []
    permissions.value = []
    isLoading.value = false
  }

  return {
    menuList,
    permissions,
    isLoading,
    setMenuList,
    setPermissions,
    loadUserMenus,
    resetState
  }
}) 