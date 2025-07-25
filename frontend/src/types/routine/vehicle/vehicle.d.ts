/** 用车相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 车辆类型枚举 */
export enum HbtVehicleType {
  Sedan = 0,      // 轿车
  SUV = 1,        // SUV
  Business = 2,   // 商务车
  Van = 3         // 面包车
}

/** 车辆状态枚举 */
export enum HbtVehicleStatus {
  Idle = 0,       // 空闲
  InUse = 1,      // 使用中
  Maintenance = 2, // 维修中
  Scrapped = 3    // 已报废
}

/** 用车查询参数 */
export interface HbtVehicleQuery extends HbtPagedQuery {
  plateNumber?: string
  vehicleType?: HbtVehicleType
  status?: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
}

/** 用车数据传输对象 */
export interface HbtVehicle extends HbtBaseEntity {
  vehicleId: number
  plateNumber: string
  vehicleType: HbtVehicleType
  status: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车创建参数 */
export interface HbtVehicleCreate {
  plateNumber: string
  vehicleType: HbtVehicleType
  status: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车更新参数 */
export interface HbtVehicleUpdate extends HbtVehicleCreate {
  vehicleId: number
}

/** 用车删除参数 */
export interface HbtVehicleDelete {
  vehicleId: number
}

/** 用车批量删除参数 */
export interface HbtVehicleBatchDelete {
  vehicleIds: number[]
}

/** 用车导入参数 */
export interface HbtVehicleImport {
  plateNumber: string
  vehicleType: HbtVehicleType
  status: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车导出参数 */
export interface HbtVehicleExport {
  plateNumber: string
  vehicleType: HbtVehicleType
  status: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
  createTime: Date
}

/** 用车导入模板参数 */
export interface HbtVehicleTemplate {
  plateNumber: string
  vehicleType: HbtVehicleType
  status: HbtVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车分页结果 */
export type HbtVehiclePagedResult = HbtPagedResult<HbtVehicle>

/** 用车统计信息 */
export interface HbtVehicleStatistics {
  totalVehicles: number
  idleVehicles: number
  inUseVehicles: number
  maintenanceVehicles: number
  scrappedVehicles: number
  totalMileage: number
  averageMileage: number
} 