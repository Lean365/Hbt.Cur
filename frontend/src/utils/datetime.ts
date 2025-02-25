//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : datetime.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 日期时间工具函数
//===================================================================

import dayjs, { Dayjs } from 'dayjs'
import 'dayjs/locale/zh-cn'
import quarterOfYear from 'dayjs/plugin/quarterOfYear'

// 设置语言为中文
dayjs.locale('zh-cn')
// 注册季度插件
dayjs.extend(quarterOfYear)

/**
 * 获取日期的开始时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getStartOfDay = (date?: Date | string | number): Dayjs => {
  return dayjs(date).startOf('day')
}

/**
 * 获取日期的结束时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getEndOfDay = (date?: Date | string | number): Dayjs => {
  return dayjs(date).endOf('day')
}

/**
 * 获取本周的开始时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getStartOfWeek = (date?: Date | string | number): Dayjs => {
  return dayjs(date).startOf('week')
}

/**
 * 获取本周的结束时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getEndOfWeek = (date?: Date | string | number): Dayjs => {
  return dayjs(date).endOf('week')
}

/**
 * 获取本月的开始时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getStartOfMonth = (date?: Date | string | number): Dayjs => {
  return dayjs(date).startOf('month')
}

/**
 * 获取本月的结束时间
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getEndOfMonth = (date?: Date | string | number): Dayjs => {
  return dayjs(date).endOf('month')
}

/**
 * 计算两个日期之间的天数
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns 天数
 */
export const getDaysBetween = (start: Date | string | number, end: Date | string | number): number => {
  return dayjs(end).diff(dayjs(start), 'day')
}

/**
 * 判断是否为工作日（周一至周五）
 * @param date - 日期
 * @returns 是否为工作日
 */
export const isWorkday = (date?: Date | string | number | Dayjs): boolean => {
  const day = dayjs(date).day()
  return day !== 0 && day !== 6
}

/**
 * 判断是否为同一天
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一天
 */
export const isSameDay = (date1: Date | string | number, date2: Date | string | number): boolean => {
  return dayjs(date1).isSame(dayjs(date2), 'day')
}

/**
 * 判断日期是否在指定范围内
 * @param date - 要判断的日期
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns 是否在范围内
 */
export const isDateInRange = (
  date: Date | string | number,
  start: Date | string | number,
  end: Date | string | number
): boolean => {
  const d = dayjs(date)
  return d.isSameOrAfter(start) && d.isSameOrBefore(end)
}

/**
 * 添加工作日
 * @param date - 起始日期
 * @param days - 要添加的工作日天数
 * @returns Dayjs 对象
 */
export const addWorkdays = (date: Date | string | number, days: number): Dayjs => {
  let result = dayjs(date)
  let remainingDays = days
  
  while (remainingDays > 0) {
    result = result.add(1, 'day')
    if (isWorkday(result)) {
      remainingDays--
    }
  }
  
  return result
}

/**
 * 获取日期范围内的所有日期
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns Dayjs 对象数组
 */
export const getDatesBetween = (start: Dayjs, end: Dayjs): Dayjs[] => {
  const dates: Dayjs[] = []
  let current = start

  while (current.isBefore(end) || current.isSame(end, 'day')) {
    dates.push(current)
    current = current.add(1, 'day')
  }
  
  return dates
}

/**
 * 格式化日期时间
 * @param time 日期时间
 * @param pattern 格式化模式，默认为 YYYY-MM-DD HH:mm:ss
 * @returns 格式化后的日期时间字符串
 */
export function formatDateTime(time?: string | number | Date, pattern = 'YYYY-MM-DD HH:mm:ss'): string {
  if (!time) {
    return ''
  }
  return dayjs(time).format(pattern)
}

/**
 * 格式化日期
 * @param date 日期
 * @param pattern 格式化模式，默认为 YYYY-MM-DD
 * @returns 格式化后的日期字符串
 */
export function formatDate(date?: string | number | Date, pattern = 'YYYY-MM-DD'): string {
  if (!date) {
    return ''
  }
  return dayjs(date).format(pattern)
}

/**
 * 格式化时间
 * @param time 时间
 * @param pattern 格式化模式，默认为 HH:mm:ss
 * @returns 格式化后的时间字符串
 */
export function formatTime(time?: string | number | Date, pattern = 'HH:mm:ss'): string {
  if (!time) {
    return ''
  }
  return dayjs(time).format(pattern)
}

/**
 * 获取今天的开始时间
 * @returns Dayjs 对象
 */
export const getTodayStart = (): Dayjs => {
  return dayjs().startOf('day')
}

/**
 * 获取今天的结束时间
 * @returns Dayjs 对象
 */
export const getTodayEnd = (): Dayjs => {
  return dayjs().endOf('day')
}

/**
 * 获取昨天的开始时间
 * @returns Dayjs 对象
 */
export const getYesterdayStart = (): Dayjs => {
  return dayjs().subtract(1, 'day').startOf('day')
}

/**
 * 获取昨天的结束时间
 * @returns Dayjs 对象
 */
export const getYesterdayEnd = (): Dayjs => {
  return dayjs().subtract(1, 'day').endOf('day')
}

/**
 * 获取本周的开始时间
 * @returns Dayjs 对象
 */
export const getThisWeekStart = (): Dayjs => {
  return dayjs().startOf('week')
}

/**
 * 获取本周的结束时间
 * @returns Dayjs 对象
 */
export const getThisWeekEnd = (): Dayjs => {
  return dayjs().endOf('week')
}

/**
 * 获取上周的开始时间
 * @returns Dayjs 对象
 */
export const getLastWeekStart = (): Dayjs => {
  return dayjs().subtract(1, 'week').startOf('week')
}

/**
 * 获取上周的结束时间
 * @returns Dayjs 对象
 */
export const getLastWeekEnd = (): Dayjs => {
  return dayjs().subtract(1, 'week').endOf('week')
}

/**
 * 获取本月的开始时间
 * @returns Dayjs 对象
 */
export const getThisMonthStart = (): Dayjs => {
  return dayjs().startOf('month')
}

/**
 * 获取本月的结束时间
 * @returns Dayjs 对象
 */
export const getThisMonthEnd = (): Dayjs => {
  return dayjs().endOf('month')
}

/**
 * 获取上月的开始时间
 * @returns Dayjs 对象
 */
export const getLastMonthStart = (): Dayjs => {
  return dayjs().subtract(1, 'month').startOf('month')
}

/**
 * 获取上月的结束时间
 * @returns Dayjs 对象
 */
export const getLastMonthEnd = (): Dayjs => {
  return dayjs().subtract(1, 'month').endOf('month')
}

/**
 * 获取最近一周的开始时间
 * @returns Dayjs 对象
 */
export const getLastWeekRangeStart = (): Dayjs => {
  return dayjs().subtract(7, 'day').startOf('day')
}

/**
 * 获取最近一周的结束时间
 * @returns Dayjs 对象
 */
export const getLastWeekRangeEnd = (): Dayjs => {
  return dayjs().endOf('day')
}

/**
 * 获取最近一月的开始时间
 * @returns Dayjs 对象
 */
export const getLastMonthRangeStart = (): Dayjs => {
  return dayjs().subtract(30, 'day').startOf('day')
}

/**
 * 获取最近一月的结束时间
 * @returns Dayjs 对象
 */
export const getLastMonthRangeEnd = (): Dayjs => {
  return dayjs().endOf('day')
}

/**
 * 获取最近三月的开始时间
 * @returns Dayjs 对象
 */
export const getLastThreeMonthsStart = (): Dayjs => {
  return dayjs().subtract(90, 'day').startOf('day')
}

/**
 * 获取最近三月的结束时间
 * @returns Dayjs 对象
 */
export const getLastThreeMonthsEnd = (): Dayjs => {
  return dayjs().endOf('day')
}

/**
 * 获取本季度的开始时间
 * @returns Dayjs 对象
 */
export const getThisQuarterStart = (): Dayjs => {
  return dayjs().startOf('quarter')
}

/**
 * 获取本季度的结束时间
 * @returns Dayjs 对象
 */
export const getThisQuarterEnd = (): Dayjs => {
  return dayjs().endOf('quarter')
}

/**
 * 获取上季度的开始时间
 * @returns Dayjs 对象
 */
export const getLastQuarterStart = (): Dayjs => {
  return dayjs().subtract(1, 'quarter').startOf('quarter')
}

/**
 * 获取上季度的结束时间
 * @returns Dayjs 对象
 */
export const getLastQuarterEnd = (): Dayjs => {
  return dayjs().subtract(1, 'quarter').endOf('quarter')
}

/**
 * 获取本年的开始时间
 * @returns Dayjs 对象
 */
export const getThisYearStart = (): Dayjs => {
  return dayjs().startOf('year')
}

/**
 * 获取本年的结束时间
 * @returns Dayjs 对象
 */
export const getThisYearEnd = (): Dayjs => {
  return dayjs().endOf('year')
}

/**
 * 获取上年的开始时间
 * @returns Dayjs 对象
 */
export const getLastYearStart = (): Dayjs => {
  return dayjs().subtract(1, 'year').startOf('year')
}

/**
 * 获取上年的结束时间
 * @returns Dayjs 对象
 */
export const getLastYearEnd = (): Dayjs => {
  return dayjs().subtract(1, 'year').endOf('year')
}

/**
 * 获取指定日期范围
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns [开始时间, 结束时间]
 */
export const getDateRange = (start: Dayjs, end: Dayjs): [Dayjs, Dayjs] => {
  return [start, end]
}

/**
 * 获取两个日期之间的月数
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns 月数
 */
export const getMonthsBetween = (start: Dayjs, end: Dayjs): number => {
  return end.diff(start, 'month')
}

/**
 * 获取两个日期之间的年数
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns 年数
 */
export const getYearsBetween = (start: Dayjs, end: Dayjs): number => {
  return end.diff(start, 'year')
}

/**
 * 判断是否为同一月
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一月
 */
export const isSameMonth = (date1: Dayjs, date2: Dayjs): boolean => {
  return date1.isSame(date2, 'month')
}

/**
 * 判断是否为同一年
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一年
 */
export const isSameYear = (date1: Dayjs, date2: Dayjs): boolean => {
  return date1.isSame(date2, 'year')
}

/**
 * 判断是否为周末（周六日）
 * @param date - 日期
 * @returns 是否为周末
 */
export const isWeekend = (date: Dayjs): boolean => {
  return date.day() === 0 || date.day() === 6
}

/**
 * 获取日期所在月份的天数
 * @param date - 日期
 * @returns 天数
 */
export const getDaysInMonth = (date: Dayjs): number => {
  return date.daysInMonth()
}

/**
 * 获取日期所在季度的第一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getQuarterStart = (date: Dayjs): Dayjs => {
  return date.startOf('quarter')
}

/**
 * 获取日期所在季度的最后一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getQuarterEnd = (date: Dayjs): Dayjs => {
  return date.endOf('quarter')
}

/**
 * 获取日期所在周的第一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getWeekStart = (date: Dayjs): Dayjs => {
  return date.startOf('week')
}

/**
 * 获取日期所在周的最后一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getWeekEnd = (date: Dayjs): Dayjs => {
  return date.endOf('week')
}

/**
 * 获取日期所在月的第一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getMonthStart = (date: Dayjs): Dayjs => {
  return date.startOf('month')
}

/**
 * 获取日期所在月的最后一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getMonthEnd = (date: Dayjs): Dayjs => {
  return date.endOf('month')
}

/**
 * 获取日期所在年的第一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getYearStart = (date: Dayjs): Dayjs => {
  return date.startOf('year')
}

/**
 * 获取日期所在年的最后一天
 * @param date - 日期
 * @returns Dayjs 对象
 */
export const getYearEnd = (date: Dayjs): Dayjs => {
  return date.endOf('year')
}

/**
 * 解析日期字符串
 * @param dateStr - 日期字符串
 * @param format - 日期格式，可选
 * @returns Dayjs 对象
 */
export const parseDate = (dateStr: string, format?: string): Dayjs => {
  return format ? dayjs(dateStr, format) : dayjs(dateStr)
}

/**
 * 获取相对时间描述（例如：刚刚、1分钟前等）
 * @param date - 日期
 * @returns 相对时间描述
 */
export const getRelativeTime = (date: Dayjs | Date | string | number): string => {
  return dayjs(date).fromNow()
}

/**
 * 获取指定月份的所有日期
 * @param year - 年份
 * @param month - 月份（1-12）
 * @returns 日期数组
 */
export const getDatesInMonth = (year: number, month: number): Dayjs[] => {
  const start = dayjs().year(year).month(month - 1).startOf('month')
  const end = start.endOf('month')
  return getDatesBetween(start, end)
}

/**
 * 获取指定年份的所有月份的第一天
 * @param year - 年份
 * @returns 日期数组
 */
export const getMonthsInYear = (year: number): Dayjs[] => {
  const months: Dayjs[] = []
  for (let i = 0; i < 12; i++) {
    months.push(dayjs().year(year).month(i).startOf('month'))
  }
  return months
}

/**
 * 获取指定日期的农历信息
 * 注意：需要额外安装 dayjs/plugin/lunar 插件
 * @param date - 日期
 * @returns 农历日期字符串
 */
export const getLunarDate = (date: Dayjs): string => {
  // TODO: 实现农历转换
  return ''
}

/**
 * 获取指定日期是当年的第几周
 * @param date - 日期
 * @returns 周数
 */
export const getWeekOfYear = (date: Dayjs): number => {
  return date.week()
}

/**
 * 获取指定日期是当月的第几周
 * @param date - 日期
 * @returns 周数
 */
export const getWeekOfMonth = (date: Dayjs): number => {
  return date.week() - date.startOf('month').week() + 1
}

/**
 * 获取指定日期的星期名称
 * @param date - 日期
 * @returns 星期名称
 */
export const getWeekdayName = (date: Dayjs): string => {
  return date.format('dddd')
}

/**
 * 获取指定日期的月份名称
 * @param date - 日期
 * @returns 月份名称
 */
export const getMonthName = (date: Dayjs): string => {
  return date.format('MMMM')
}

export default {
  getTodayStart,
  getTodayEnd,
  getYesterdayStart,
  getYesterdayEnd,
  getThisWeekStart,
  getThisWeekEnd,
  getLastWeekStart,
  getLastWeekEnd,
  getThisMonthStart,
  getThisMonthEnd,
  getLastMonthStart,
  getLastMonthEnd,
  getLastWeekRangeStart,
  getLastWeekRangeEnd,
  getLastMonthRangeStart,
  getLastMonthRangeEnd,
  getLastThreeMonthsStart,
  getLastThreeMonthsEnd,
  getThisQuarterStart,
  getThisQuarterEnd,
  getLastQuarterStart,
  getLastQuarterEnd,
  getThisYearStart,
  getThisYearEnd,
  getLastYearStart,
  getLastYearEnd,
  getDateRange,
  getDaysBetween,
  getMonthsBetween,
  getYearsBetween,
  isSameDay,
  isSameMonth,
  isSameYear,
  isWorkday,
  isWeekend,
  getDaysInMonth,
  getQuarterStart,
  getQuarterEnd,
  getWeekStart,
  getWeekEnd,
  getMonthStart,
  getMonthEnd,
  getYearStart,
  getYearEnd,
  formatDateTime,
  formatDate,
  formatTime,
  parseDate,
  getRelativeTime,
  isDateInRange,
  getDatesBetween,
  getDatesInMonth,
  getMonthsInYear,
  getLunarDate,
  getWeekOfYear,
  getWeekOfMonth,
  getWeekdayName,
  getMonthName
} 