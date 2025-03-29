import dayjs from 'dayjs'

export function formatDateTime(date: Date | string, format = 'YYYY-MM-DD HH:mm:ss'): string {
  return dayjs(date).format(format)
}

export function formatFileSize(bytes: number): string {
  if (bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return `${parseFloat((bytes / Math.pow(k, i)).toFixed(2))} ${sizes[i]}`
}

export function formatDuration(minutes: number): string {
  const days = Math.floor(minutes / (24 * 60))
  const hours = Math.floor((minutes % (24 * 60)) / 60)
  const mins = Math.floor(minutes % 60)
  
  const parts = []
  if (days > 0) parts.push(`${days}天`)
  if (hours > 0) parts.push(`${hours}小时`)
  if (mins > 0) parts.push(`${mins}分钟`)
  
  return parts.length > 0 ? parts.join(' ') : '0分钟'
} 