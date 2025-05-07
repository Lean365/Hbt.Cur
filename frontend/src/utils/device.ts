import type { DeviceInfo } from '@/types/identity/auth'
import { HbtDeviceType, HbtOsType, HbtBrowserType } from '@/types/identity/deviceExtend'

/**
 * 检测浏览器类型
 */
function detectBrowser(): { type: HbtBrowserType; version: string } {
  const ua = navigator.userAgent.toLowerCase()
  
  if (ua.includes('edg/')) {
    return { type: HbtBrowserType.Edge, version: 'Edge' }
  }
  if (ua.includes('chrome/')) {
    return { type: HbtBrowserType.Chrome, version: 'Chrome' }
  }
  if (ua.includes('firefox/')) {
    return { type: HbtBrowserType.Firefox, version: 'Firefox' }
  }
  if (ua.includes('safari/') && !ua.includes('chrome/')) {
    return { type: HbtBrowserType.Safari, version: 'Safari' }
  }
  if (ua.includes('opr/')) {
    return { type: HbtBrowserType.Opera, version: 'Opera' }
  }
  
  return { type: HbtBrowserType.Other, version: 'Other' }
}

/**
 * 检测操作系统类型
 */
function detectOS(): { type: HbtOsType; version: string } {
  const ua = navigator.userAgent
  
  if (ua.includes('Windows')) {
    return { type: HbtOsType.Windows, version: 'Windows' }
  }
  if (ua.includes('Mac OS X')) {
    return { type: HbtOsType.MacOS, version: 'macOS' }
  }
  if (ua.includes('Linux')) {
    return { type: HbtOsType.Linux, version: 'Linux' }
  }
  if (ua.includes('iPhone') || ua.includes('iPad')) {
    return { type: HbtOsType.iOS, version: 'iOS' }
  }
  if (ua.includes('Android')) {
    return { type: HbtOsType.Android, version: 'Android' }
  }
  
  return { type: HbtOsType.Other, version: 'Other' }
}

/**
 * 检测设备类型
 */
function detectDevice(): { type: HbtDeviceType; name: string; model: string } {
  const ua = navigator.userAgent
  
  if (/Mobile|Android|iPhone|iPad|iPod/i.test(ua)) {
    if (/iPad|Tablet/i.test(ua)) {
      return { 
        type: HbtDeviceType.Tablet,
        name: 'Tablet',
        model: 'Tablet'
      }
    }
    return { 
      type: HbtDeviceType.Mobile,
      name: 'Mobile',
      model: 'Mobile'
    }
  }
  
  return { 
    type: HbtDeviceType.PC,
    name: 'PC',
    model: 'PC'
  }
}

/**
 * 生成设备指纹
 * 基于多个设备特征生成唯一标识
 */
function generateDeviceFingerprint(): string {
  // 收集基本特征
  const features = {
    userAgent: navigator.userAgent,
    platform: navigator.platform,
    language: navigator.language,
    screenWidth: window.screen.width,
    screenHeight: window.screen.height,
    colorDepth: window.screen.colorDepth,
    hardwareConcurrency: navigator.hardwareConcurrency || 'unknown',
    maxTouchPoints: navigator.maxTouchPoints || '0',
    devicePixelRatio: window.devicePixelRatio || '1',
    vendor: navigator.vendor || 'unknown',
    product: navigator.product || 'unknown',
    appVersion: navigator.appVersion || 'unknown',
    timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone || 'unknown',
    screenOrientation: window.screen.orientation?.type || 'unknown',
    touchSupport: 'ontouchstart' in window ? 'touch' : 'no-touch',
    webglSupport: 'WebGLRenderingContext' in window ? 'webgl' : 'no-webgl'
  }

  // 输出特征值
  // console.log('设备特征:', features)

  // 转换为特征字符串
  const featureString = Object.values(features).join('|')
  
  // 使用更健壮的哈希算法
  let hash = 0
  for (let i = 0; i < featureString.length; i++) {
    const char = featureString.charCodeAt(i)
    hash = ((hash << 5) - hash) + char
    hash = hash & hash // Convert to 32bit integer
  }
  
  // 使用固定的时间戳和随机数
  const fingerprint = `${Math.abs(hash).toString(36)}-${Date.now().toString(36)}`
  
  console.log('生成的指纹:', fingerprint)
  return fingerprint
}

// 缓存设备信息
let cachedDeviceInfo: DeviceInfo | null = null

/**
 * 获取设备信息
 */
export async function getDeviceInfo(): Promise<DeviceInfo> {
  // 如果已经缓存了设备信息，直接返回
  if (cachedDeviceInfo) {
    console.log('[Device] 使用缓存的设备信息')
    return cachedDeviceInfo
  }
  
  console.log('[Device] 开始收集设备信息')
  
  // 生成设备指纹
  const fingerprint = generateDeviceFingerprint()
  
  // 检测设备类型
  const deviceType = detectDeviceType()
  
  // 检测操作系统类型
  const osType = detectOsType()
  
  // 检测浏览器类型
  const browserType = detectBrowserType()
  
  // 收集完整的设备信息
  const deviceInfo: DeviceInfo = {
    deviceId: fingerprint,
    deviceType,
    deviceName: navigator.platform,
    deviceModel: navigator.userAgent,
    osType,
    osVersion: navigator.platform,
    browserType,
    browserVersion: navigator.userAgent,
    resolution: `${window.screen.width}x${window.screen.height}`,
    processorCores: String(navigator.hardwareConcurrency || 'unknown'),
    platformVendor: navigator.vendor || 'unknown',
    hardwareConcurrency: String(navigator.hardwareConcurrency || 'unknown'),
    systemLanguage: navigator.language,
    timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone,
    screenColorDepth: String(window.screen.colorDepth),
    deviceMemory: String((navigator as any).deviceMemory || 'unknown'),
    webGLRenderer: getWebGLRenderer(),
    deviceFingerprint: fingerprint
  }
  
  // 缓存设备信息
  cachedDeviceInfo = deviceInfo
  
  console.log('[Device] 设备信息收集完成:', deviceInfo)
  return deviceInfo
}

/**
 * 检测设备类型
 */
function detectDeviceType(): number {
  const ua = navigator.userAgent.toLowerCase()
  if (/mobile|android|iphone|ipad|ipod/.test(ua)) {
    return 2 // 手机
  } else if (/tablet|ipad/.test(ua)) {
    return 3 // 平板
  }
  return 1 // PC
}

/**
 * 检测操作系统类型
 */
function detectOsType(): number {
  const platform = navigator.platform.toLowerCase()
  if (platform.includes('win')) {
    return 1 // Windows
  } else if (platform.includes('mac')) {
    return 2 // MacOS
  } else if (platform.includes('linux')) {
    return 3 // Linux
  } else if (platform.includes('android')) {
    return 4 // Android
  } else if (platform.includes('iphone') || platform.includes('ipad')) {
    return 5 // iOS
  }
  return 0 // 未知
}

/**
 * 检测浏览器类型
 */
function detectBrowserType(): number {
  const ua = navigator.userAgent.toLowerCase()
  if (ua.includes('chrome')) {
    return 1 // Chrome
  } else if (ua.includes('firefox')) {
    return 2 // Firefox
  } else if (ua.includes('safari') && !ua.includes('chrome')) {
    return 3 // Safari
  } else if (ua.includes('edge') || ua.includes('edg')) {
    return 4 // Edge
  }
  return 0 // 未知
}

/**
 * 获取WebGL渲染器信息
 */
function getWebGLRenderer(): string {
  try {
    const canvas = document.createElement('canvas')
    const gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl')
    if (gl && gl instanceof WebGLRenderingContext) {
      const debugInfo = gl.getExtension('WEBGL_debug_renderer_info')
      if (debugInfo) {
        return gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL) || 'unknown'
      }
    }
  } catch (e) {
    console.error('获取WebGL渲染器信息失败:', e)
  }
  return 'unknown'
} 