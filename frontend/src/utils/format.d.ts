declare module '@/utils/format' {
  export function formatDateTime(date: Date | string, format?: string): string
  export function formatFileSize(bytes: number): string
  export function formatDuration(minutes: number): string
} 