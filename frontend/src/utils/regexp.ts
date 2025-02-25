//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : regexp.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 正则表达式工具
//===================================================================

/**
 * 正则表达式模式
 */
export const RegExpPatterns = {
  // 用户名：4-16位字母、数字、下划线
  USERNAME: /^[a-zA-Z0-9_]{4,16}$/,
  
  // 密码：8-16位，必须包含字母和数字
  PASSWORD: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$/,
  
  // 手机号码（中国大陆）
  MOBILE_CN: /^1[3-9]\d{9}$/,
  
  // 固定电话（中国大陆）
  TELEPHONE_CN: /^(?:(?:\d{3}-)?\d{8}|(?:\d{4}-)?\d{7,8})$/,
  
  // 邮箱
  EMAIL: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
  
  // 身份证号（中国大陆）
  ID_CARD_CN: /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/,
  
  // URL
  URL: /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/,
  
  // IPv4地址
  IPV4: /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/,
  
  // 日期 YYYY-MM-DD
  DATE: /^\d{4}-(?:0[1-9]|1[0-2])-(?:0[1-9]|[12]\d|3[01])$/,
  
  // 时间 HH:mm:ss
  TIME: /^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$/,
  
  // 整数
  INTEGER: /^-?\d+$/,
  
  // 正整数
  POSITIVE_INTEGER: /^[1-9]\d*$/,
  
  // 负整数
  NEGATIVE_INTEGER: /^-[1-9]\d*$/,
  
  // 数字（整数或小数）
  NUMBER: /^-?\d*\.?\d+$/,
  
  // 中文字符
  CHINESE: /^[\u4e00-\u9fa5]+$/,
  
  // 邮政编码（中国）
  POSTAL_CODE_CN: /^[1-9]\d{5}$/,
  
  // 车牌号（中国）
  LICENSE_PLATE_CN: /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z][A-HJ-NP-Z0-9]{4,5}[A-HJ-NP-Z0-9挂学警港澳]$/,
  
  // 16进制颜色
  HEX_COLOR: /^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/,
  
  // Base64
  BASE64: /^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$/
}

/**
 * 验证函数
 */
export const RegExpValidators = {
  /**
   * 验证用户名
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidUsername: (value: string): boolean => RegExpPatterns.USERNAME.test(value),
  
  /**
   * 验证密码
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidPassword: (value: string): boolean => RegExpPatterns.PASSWORD.test(value),
  
  /**
   * 验证手机号码（中国大陆）
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidMobileCN: (value: string): boolean => RegExpPatterns.MOBILE_CN.test(value),
  
  /**
   * 验证邮箱
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidEmail: (value: string): boolean => RegExpPatterns.EMAIL.test(value),
  
  /**
   * 验证身份证号（中国大陆）
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidIDCardCN: (value: string): boolean => RegExpPatterns.ID_CARD_CN.test(value),
  
  /**
   * 验证URL
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidURL: (value: string): boolean => RegExpPatterns.URL.test(value),
  
  /**
   * 验证IPv4地址
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidIPv4: (value: string): boolean => RegExpPatterns.IPV4.test(value),
  
  /**
   * 验证日期格式 YYYY-MM-DD
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidDate: (value: string): boolean => RegExpPatterns.DATE.test(value),
  
  /**
   * 验证时间格式 HH:mm:ss
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidTime: (value: string): boolean => RegExpPatterns.TIME.test(value),
  
  /**
   * 验证是否为整数
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isInteger: (value: string): boolean => RegExpPatterns.INTEGER.test(value),
  
  /**
   * 验证是否为正整数
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isPositiveInteger: (value: string): boolean => RegExpPatterns.POSITIVE_INTEGER.test(value),
  
  /**
   * 验证是否为数字（整数或小数）
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isNumber: (value: string): boolean => RegExpPatterns.NUMBER.test(value),
  
  /**
   * 验证是否为中文字符
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isChinese: (value: string): boolean => RegExpPatterns.CHINESE.test(value),
  
  /**
   * 验证是否为邮政编码（中国）
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidPostalCodeCN: (value: string): boolean => RegExpPatterns.POSTAL_CODE_CN.test(value),
  
  /**
   * 验证是否为车牌号（中国）
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidLicensePlateCN: (value: string): boolean => RegExpPatterns.LICENSE_PLATE_CN.test(value),
  
  /**
   * 验证是否为16进制颜色值
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidHexColor: (value: string): boolean => RegExpPatterns.HEX_COLOR.test(value),
  
  /**
   * 验证是否为Base64编码
   * @param value - 要验证的值
   * @returns 是否通过验证
   */
  isValidBase64: (value: string): boolean => RegExpPatterns.BASE64.test(value)
} 