//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : usePermission.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 权限检查hook
//===================================================================

import { useUserStore } from '@/stores/user'

/**
 * 权限检查hook
 */
export function usePermission() {
  const userStore = useUserStore()

  /**
   * 检查是否有权限
   * @param permission 权限标识
   * @returns 是否有权限
   */
  const hasPermission = (permission: string | string[]): boolean => {
    if (!permission) return true

    const permissions = userStore.permissions || []
    if (Array.isArray(permission)) {
      return permission.some(p => permissions.includes(p))
    }
    return permissions.includes(permission)
  }

  return {
    hasPermission
  }
} 