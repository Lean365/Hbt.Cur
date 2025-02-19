import dayjs from 'dayjs'

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