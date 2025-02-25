//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : numberformat.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 数字格式化工具函数
//===================================================================

/**
 * 格式化数字
 * @param value - 要格式化的数值
 * @param options - 格式化选项
 * @returns 格式化后的数字字符串
 */
export const formatNumber = (value: any, options?: { 
  precision?: number
  separator?: string
  prefix?: string
  suffix?: string 
}): string => {
  if (value === null || value === undefined || value === '') return ''
  
  const { precision = 2, separator = ',', prefix = '', suffix = '' } = options || {}
  
  let num = Number(value)
  if (isNaN(num)) return value.toString()
  
  num = Number(num.toFixed(precision))
  
  const parts = num.toString().split('.')
  parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, separator)
  
  return `${prefix}${parts.join('.')}${suffix}`
}

/**
 * 格式化百分比
 * @param value - 要格式化的数值
 * @param precision - 小数位数，默认为2
 * @returns 格式化后的百分比字符串
 */
export const formatPercent = (value: any, precision = 2): string => {
  if (value === null || value === undefined || value === '') return ''
  
  const num = Number(value)
  if (isNaN(num)) return value.toString()
  
  return formatNumber(num * 100, { precision, suffix: '%' })
}

/**
 * 格式化金额
 * @param value - 要格式化的金额
 * @param options - 格式化选项
 * @returns 格式化后的金额字符串
 */
export const formatMoney = (value: any, options?: {
  precision?: number
  separator?: string
  prefix?: string
}): string => {
  return formatNumber(value, { precision: 2, separator: ',', prefix: '¥', ...options })
} 