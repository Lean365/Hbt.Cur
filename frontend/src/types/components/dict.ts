//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dict.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典组件类型定义
//===================================================================

// 字典选项类型
export interface DictOption {
  label: string
  value: string | number
  disabled?: boolean
}

// 字典数据类型
export interface DictData {
  dictId: number
  dictLabel: string
  dictValue: string | number
  dictType: string
  status: number
  remark?: string
}

// 字典查询参数
export interface DictQuery {
  dictType: string
  status?: number
}

// 字典响应数据
export interface DictResponse {
  code: number
  msg: string
  data: DictData[]
} 