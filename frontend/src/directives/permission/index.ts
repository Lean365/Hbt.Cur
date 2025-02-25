//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 权限验证指令
//===================================================================

import type { App, Directive } from 'vue'
import { useUserStore } from '@/stores/user'

/**
 * 权限验证指令
 * v-hasPermi="['identity:config:list']"
 */
export const hasPermi: Directive = {
  mounted(el: HTMLElement, binding) {
    const { value } = binding
    const permissions = useUserStore().permissions
    
    if (value && value instanceof Array && value.length > 0) {
      const hasPermission = value.some(permission => {
        return permissions.includes(permission)
      })
      
      if (!hasPermission) {
        el.style.display = 'none'
      }
    } else {
      throw new Error('请设置操作权限标签值')
    }
  }
}

/**
 * 角色验证指令
 * v-hasRole="['admin','editor']"
 */
export const hasRole: Directive = {
  mounted(el: HTMLElement, binding) {
    const { value } = binding
    const roles = useUserStore().roles
    
    if (value && value instanceof Array && value.length > 0) {
      const hasRole = roles.some(role => {
        return value.includes(role)
      })
      
      if (!hasRole) {
        el.style.display = 'none'
      }
    } else {
      throw new Error('请设置角色标签值')
    }
  }
}

// 注册所有指令
export function setupPermission(app: App) {
  app.directive('hasPermi', hasPermi)
  app.directive('hasRole', hasRole)
} 