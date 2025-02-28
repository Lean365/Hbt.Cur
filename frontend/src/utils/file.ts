//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : file.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 文件工具函数
//===================================================================

/**
 * 下载文件
 * @param data 文件数据
 * @param filename 文件名
 */
export function downloadFile(data: Blob, filename?: string) {
  const blob = new Blob([data])
  const url = window.URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = filename || '导出文件.xlsx'
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  window.URL.revokeObjectURL(url)
} 