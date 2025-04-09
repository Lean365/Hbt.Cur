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
 * 获取设备信息
 */
export async function getDeviceInfo(): Promise<DeviceInfo> {
  const browser = detectBrowser()
  const os = detectOS()
  const device = detectDevice()
  
  // 只收集基本设备信息
  const deviceInfo: DeviceInfo = {
    deviceId: `${os.type}-${browser.type}-${device.type}`,
    deviceType: device.type,
    deviceName: device.name,
    deviceModel: device.model,
    osType: os.type,
    osVersion: os.version,
    browserType: browser.type,
    browserVersion: browser.version,
    resolution: `${window.screen.width}x${window.screen.height}`,
    // 生成设备指纹
    deviceFingerprint: generateDeviceFingerprint()
  }
  
  return deviceInfo
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
  console.log('设备特征:', features)

  // 转换为特征字符串
  const featureString = Object.values(features).join('|')
  
  // 使用更健壮的哈希算法
  let hash = 0
  for (let i = 0; i < featureString.length; i++) {
    const char = featureString.charCodeAt(i)
    hash = ((hash << 5) - hash) + char
    hash = hash & hash // Convert to 32bit integer
  }
  
  // 添加时间戳和随机数增加唯一性
  const timestamp = Date.now().toString(36)
  const random = Math.random().toString(36).substr(2, 5)
  const fingerprint = `${Math.abs(hash).toString(36)}-${timestamp}-${random}`
  
  console.log('生成的指纹:', fingerprint)
  return fingerprint
} 