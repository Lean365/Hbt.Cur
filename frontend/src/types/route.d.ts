import type { RouteRecordRaw } from 'vue-router'

interface HbtRouteMeta {
  title?: string
  icon?: string
  hidden?: boolean
  keepAlive?: boolean
  permission?: string
  requiresAuth?: boolean
  menuId?: number
  orderNum?: number
}

export interface HbtRouteRecordRaw {
  path: string
  name?: string | symbol
  redirect?: string
  component?: RouteRecordRaw['component']
  meta?: HbtRouteMeta
  children?: HbtRouteRecordRaw[]
}

declare module 'vue-router' {
  interface RouteMeta {
    title?: string
    icon?: string
    hidden?: boolean
    keepAlive?: boolean
    permission?: string
    requiresAuth?: boolean
    menuId?: number
    orderNum?: number
  }
} 