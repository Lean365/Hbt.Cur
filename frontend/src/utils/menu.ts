import type { Menu } from '@/types/identity/menu'
import type { MenuProps } from 'ant-design-vue'
import * as Icons from '@ant-design/icons-vue'
import { h } from 'vue'
import { HbtMenuType } from '@/types/base'
import i18n from '@/locales'

/**
 * 获取图标组件
 * @param iconName 图标名称
 * @returns 图标渲染函数
 */
function getIcon(iconName: string | undefined) {
  if (!iconName) {
    return undefined
  }

  // 尝试直接使用图标名称
  const IconComponent = (Icons as any)[iconName]
  if (IconComponent) {
    return () => h(IconComponent)
  }

  // 如果没找到，尝试添加 Outlined 后缀
  const outlinedIconName = iconName.endsWith('Outlined') ? iconName : `${iconName}Outlined`
  const OutlinedIconComponent = (Icons as any)[outlinedIconName]
  if (OutlinedIconComponent) {
    return () => h(OutlinedIconComponent)
  }

  console.warn(`[菜单图标] 图标未找到:`, {
    原始图标: iconName,
    带后缀图标: outlinedIconName
  })
  return () => h(Icons.MenuOutlined)
}

/**
 * 将后端菜单数据转换为Ant Design Vue菜单格式
 * @param menus 后端菜单数据
 * @returns Ant Design Vue菜单数据
 */
export function transformMenu(menus: Menu[]): MenuProps['items'] {
  return menus.map(menu => {
    // 跳过按钮类型的菜单
    if (menu.menuType === HbtMenuType.Button) {
      return null
    }

    console.log('[菜单转换] 处理菜单项:', {
      路径: menu.path,
      名称: menu.menuName,
      类型: menu.menuType,
      图标: menu.icon,
      子菜单数量: menu.children?.length,
      原始数据: menu
    })
    
    // 处理路径，确保以/开头
    const path = menu.path?.startsWith('/') ? menu.path : `/${menu.path || ''}`
    
    // 创建菜单项
    const menuItem: any = {
      key: menu.menuType === HbtMenuType.Directory ? `dir_${menu.menuId}` : path,
      label: menu.transKey ? i18n.global.t(menu.transKey) : menu.menuName,
      icon: getIcon(menu.icon),
      disabled: menu.disabled || false,
      selectable: menu.menuType !== HbtMenuType.Directory
    }

    // 如果有子菜单，递归处理
    if (menu.children?.length) {
      menuItem.children = transformMenu(menu.children)
    }

    return menuItem
  }).filter(Boolean) as MenuProps['items']
}

/**
 * 从菜单中提取权限标识
 * @param menus 菜单数据
 * @returns 权限标识列表
 */
export function extractPermissions(menus: Menu[]): string[] {
  const perms: string[] = []
  
  const extract = (items: Menu[]) => {
    items.forEach(item => {
      if (item.perms) {
        perms.push(...item.perms.split(',').map((p: string) => p.trim()))
      }
      if (item.children?.length) {
        extract(item.children)
      }
    })
  }
  
  extract(menus)
  return [...new Set(perms)] // 去重
} 