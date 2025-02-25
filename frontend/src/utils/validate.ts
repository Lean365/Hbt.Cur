//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : validate.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 验证工具函数
//===================================================================

import { RegExpPatterns } from './regexp'

/**
 * 判断是否为 HTTP(S) URL
 * @param path - 要验证的路径
 * @returns 是否为 HTTP(S) URL
 */
export const isHttp = (path: string): boolean => {
  return /^https?:\/\//.test(path)
}

/**
 * 判断是否为数组
 * @param arg - 要验证的值
 * @returns 是否为数组
 */
export const isArray = (arg: any): boolean => {
  if (typeof Array.isArray === 'undefined') {
    return Object.prototype.toString.call(arg) === '[object Array]'
  }
  return Array.isArray(arg)
}

/**
 * 判断是否为字符串
 * @param str - 要验证的值
 * @returns 是否为字符串
 */
export const isString = (str: any): boolean => {
  return typeof str === 'string' || str instanceof String
}

/**
 * 验证邮箱格式
 * @param email - 要验证的邮箱
 * @returns 是否为有效邮箱
 */
export const validEmail = (email: string): boolean => {
  return RegExpPatterns.EMAIL.test(email)
}

/**
 * 验证是否全为字母
 * @param str - 要验证的字符串
 * @returns 是否全为字母
 */
export const validAlphabets = (str: string): boolean => {
  return /^[A-Za-z]+$/.test(str)
}

/**
 * 验证是否全为大写字母
 * @param str - 要验证的字符串
 * @returns 是否全为大写字母
 */
export const validUpperCase = (str: string): boolean => {
  return /^[A-Z]+$/.test(str)
}

/**
 * 验证是否全为小写字母
 * @param str - 要验证的字符串
 * @returns 是否全为小写字母
 */
export const validLowerCase = (str: string): boolean => {
  return /^[a-z]+$/.test(str)
}

/**
 * 验证URL格式
 * @param url - 要验证的URL
 * @returns 是否为有效URL
 */
export const validURL = (url: string): boolean => {
  return RegExpPatterns.URL.test(url)
}

/**
 * 验证用户名格式
 * @param username - 要验证的用户名
 * @returns 是否为有效用户名
 */
export const validUsername = (username: string): boolean => {
  return RegExpPatterns.USERNAME.test(username)
}

/**
 * 判断是否为外部链接
 * @param path - 要验证的路径
 * @returns 是否为外部链接
 */
export const isExternal = (path: string): boolean => {
  return /^(https?:|mailto:|tel:)/.test(path)
}

/**
 * 判断是否为链接
 * @param path - 要验证的路径
 * @returns 是否为链接
 */
export const isLink = (path: string): boolean => {
  return /^(http|https|ftp|mailto|tel):/.test(path)
}

/**
 * 判断是否为空
 * @param value - 要验证的值
 * @returns 是否为空
 */
export const isEmpty = (value: any): boolean => {
  if (value === null || value === undefined) {
    return true
  }
  if (typeof value === 'string' && value.trim() === '') {
    return true
  }
  if (Array.isArray(value) && value.length === 0) {
    return true
  }
  if (typeof value === 'object' && Object.keys(value).length === 0) {
    return true
  }
  return false
}

/**
 * 判断是否为对象
 * @param obj - 要验证的值
 * @returns 是否为对象
 */
export const isObject = (obj: any): boolean => {
  return Object.prototype.toString.call(obj) === '[object Object]'
}

/**
 * 判断是否为数字
 * @param value - 要验证的值
 * @returns 是否为数字
 */
export const isNumber = (value: any): boolean => {
  return typeof value === 'number' && !isNaN(value)
}

/**
 * 判断是否为布尔值
 * @param value - 要验证的值
 * @returns 是否为布尔值
 */
export const isBoolean = (value: any): boolean => {
  return typeof value === 'boolean'
}

/**
 * 判断是否为函数
 * @param value - 要验证的值
 * @returns 是否为函数
 */
export const isFunction = (value: any): boolean => {
  return typeof value === 'function'
}

/**
 * 判断是否为日期对象
 * @param value - 要验证的值
 * @returns 是否为日期对象
 */
export const isDate = (value: any): boolean => {
  return value instanceof Date && !isNaN(value.getTime())
}

/**
 * 判断是否为Promise
 * @param value - 要验证的值
 * @returns 是否为Promise
 */
export const isPromise = (value: any): boolean => {
  return value && typeof value.then === 'function'
}

/**
 * 判断是否为 undefined
 * @param value - 要验证的值
 * @returns 是否为 undefined
 */
export const isUndefined = (value: any): boolean => {
  return typeof value === 'undefined'
}

/**
 * 判断是否为 null
 * @param value - 要验证的值
 * @returns 是否为 null
 */
export const isNull = (value: any): boolean => {
  return value === null
}

/**
 * 判断是否为 null 或 undefined
 * @param value - 要验证的值
 * @returns 是否为 null 或 undefined
 */
export const isNullOrUndefined = (value: any): boolean => {
  return isNull(value) || isUndefined(value)
}

/**
 * 判断是否为有限数值
 * @param value - 要验证的值
 * @returns 是否为有限数值
 */
export const isFinite = (value: any): boolean => {
  return typeof value === 'number' && Number.isFinite(value)
}

/**
 * 判断是否为整数
 * @param value - 要验证的值
 * @returns 是否为整数
 */
export const isInteger = (value: any): boolean => {
  return typeof value === 'number' && Number.isInteger(value)
}

/**
 * 判断是否为安全整数
 * @param value - 要验证的值
 * @returns 是否为安全整数
 */
export const isSafeInteger = (value: any): boolean => {
  return typeof value === 'number' && Number.isSafeInteger(value)
}

/**
 * 判断是否为正则表达式
 * @param value - 要验证的值
 * @returns 是否为正则表达式
 */
export const isRegExp = (value: any): boolean => {
  return Object.prototype.toString.call(value) === '[object RegExp]'
}

/**
 * 判断是否为错误对象
 * @param value - 要验证的值
 * @returns 是否为错误对象
 */
export const isError = (value: any): boolean => {
  return value instanceof Error
}

/**
 * 判断是否为类数组对象
 * @param value - 要验证的值
 * @returns 是否为类数组对象
 */
export const isArrayLike = (value: any): boolean => {
  return value !== null && typeof value !== 'function' && isFinite(value.length)
}

/**
 * 判断是否为纯对象（使用对象字面量或 Object.create(null) 创建）
 * @param value - 要验证的值
 * @returns 是否为纯对象
 */
export const isPlainObject = (value: any): boolean => {
  if (!isObject(value)) return false
  const proto = Object.getPrototypeOf(value)
  return proto === null || proto === Object.prototype
}

/**
 * 判断是否为空对象
 * @param value - 要验证的值
 * @returns 是否为空对象
 */
export const isEmptyObject = (value: any): boolean => {
  return isObject(value) && Object.keys(value).length === 0
}

/**
 * 判断是否为有效的 JSON 字符串
 * @param value - 要验证的值
 * @returns 是否为有效的 JSON 字符串
 */
export const isValidJSON = (value: string): boolean => {
  try {
    JSON.parse(value)
    return true
  } catch {
    return false
  }
}

/**
 * 判断是否为有效的手机号（中国大陆）
 * @param value - 要验证的值
 * @returns 是否为有效的手机号
 */
export const isValidMobile = (value: string): boolean => {
  return RegExpPatterns.MOBILE_CN.test(value)
}

/**
 * 判断是否为有效的固定电话（中国大陆）
 * @param value - 要验证的值
 * @returns 是否为有效的固定电话
 */
export const isValidTelephone = (value: string): boolean => {
  return RegExpPatterns.TELEPHONE_CN.test(value)
}

/**
 * 判断是否为有效的身份证号（中国大陆）
 * @param value - 要验证的值
 * @returns 是否为有效的身份证号
 */
export const isValidIDCard = (value: string): boolean => {
  return RegExpPatterns.ID_CARD_CN.test(value)
}

/**
 * 判断是否为有效的邮政编码（中国大陆）
 * @param value - 要验证的值
 * @returns 是否为有效的邮政编码
 */
export const isValidPostalCode = (value: string): boolean => {
  return RegExpPatterns.POSTAL_CODE_CN.test(value)
}

/**
 * 判断是否为有效的车牌号（中国大陆）
 * @param value - 要验证的值
 * @returns 是否为有效的车牌号
 */
export const isValidLicensePlate = (value: string): boolean => {
  return RegExpPatterns.LICENSE_PLATE_CN.test(value)
}

/**
 * 判断字符串是否为空白字符
 * @param value - 要验证的值
 * @returns 是否为空白字符
 */
export const isWhitespace = (value: string): boolean => {
  return /^\s*$/.test(value)
}

/**
 * 判断是否为有效的十六进制颜色值
 * @param value - 要验证的值
 * @returns 是否为有效的十六进制颜色值
 */
export const isValidHexColor = (value: string): boolean => {
  return RegExpPatterns.HEX_COLOR.test(value)
}

/**
 * 判断是否为有效的 Base64 编码
 * @param value - 要验证的值
 * @returns 是否为有效的 Base64 编码
 */
export const isValidBase64 = (value: string): boolean => {
  return RegExpPatterns.BASE64.test(value)
}

/**
 * 判断是否为有效的 IP 地址（IPv4）
 * @param value - 要验证的值
 * @returns 是否为有效的 IP 地址
 */
export const isValidIPv4 = (value: string): boolean => {
  return RegExpPatterns.IPV4.test(value)
}

/**
 * 判断是否为有效的 MAC 地址
 * @param value - 要验证的值
 * @returns 是否为有效的 MAC 地址
 */
export const isValidMAC = (value: string): boolean => {
  return /^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$/.test(value)
}

/**
 * 判断是否为有效的经度
 * @param value - 要验证的值
 * @returns 是否为有效的经度
 */
export const isValidLongitude = (value: number): boolean => {
  return isFinite(value) && value >= -180 && value <= 180
}

/**
 * 判断是否为有效的纬度
 * @param value - 要验证的值
 * @returns 是否为有效的纬度
 */
export const isValidLatitude = (value: number): boolean => {
  return isFinite(value) && value >= -90 && value <= 90
}

/**
 * 判断是否为有效的端口号
 * @param value - 要验证的值
 * @returns 是否为有效的端口号
 */
export const isValidPort = (value: number): boolean => {
  return isInteger(value) && value >= 0 && value <= 65535
}

/**
 * 判断是否为有效的版本号（Semver）
 * @param value - 要验证的值
 * @returns 是否为有效的版本号
 */
export const isValidVersion = (value: string): boolean => {
  return /^\d+\.\d+\.\d+(?:-[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*)?(?:\+[0-9A-Za-z-]+)?$/.test(value)
} 