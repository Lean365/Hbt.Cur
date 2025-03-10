import type { DeviceInfo } from '@/types/identity/auth'
import { HbtDeviceType, HbtOsType, HbtBrowserType } from '@/types/identity/deviceExtend'

/**
 * 检测浏览器类型
 */
function detectBrowser(): { type: HbtBrowserType; version: string } {
  const ua = navigator.userAgent.toLowerCase()
  
  if (ua.includes('edg/')) {
    return { type: HbtBrowserType.Edge, version: ua.match(/edg\/(\d+\.\d+)/)![1] }
  }
  if (ua.includes('chrome/')) {
    return { type: HbtBrowserType.Chrome, version: ua.match(/chrome\/(\d+\.\d+)/)![1] }
  }
  if (ua.includes('firefox/')) {
    return { type: HbtBrowserType.Firefox, version: ua.match(/firefox\/(\d+\.\d+)/)![1] }
  }
  if (ua.includes('safari/') && !ua.includes('chrome/')) {
    return { type: HbtBrowserType.Safari, version: ua.match(/version\/(\d+\.\d+)/)![1] }
  }
  if (ua.includes('opr/')) {
    return { type: HbtBrowserType.Opera, version: ua.match(/opr\/(\d+\.\d+)/)![1] }
  }
  
  return { type: HbtBrowserType.Other, version: 'Unknown' }
}

/**
 * 检测操作系统类型
 */
function detectOS(): { type: HbtOsType; version: string } {
  const ua = navigator.userAgent
  
  if (ua.includes('Windows')) {
    const version = ua.match(/Windows NT (\d+\.\d+)/)
    return { 
      type: HbtOsType.Windows, 
      version: version ? `Windows ${version[1]}` : 'Windows'
    }
  }
  if (ua.includes('Mac OS X')) {
    const version = ua.match(/Mac OS X (\d+[._]\d+)/)
    return { 
      type: HbtOsType.MacOS, 
      version: version ? `macOS ${version[1].replace('_', '.')}` : 'macOS'
    }
  }
  if (ua.includes('Linux')) {
    return { type: HbtOsType.Linux, version: 'Linux' }
  }
  if (ua.includes('iPhone') || ua.includes('iPad')) {
    const version = ua.match(/OS (\d+[._]\d+)/)
    return { 
      type: HbtOsType.iOS, 
      version: version ? `iOS ${version[1].replace('_', '.')}` : 'iOS'
    }
  }
  if (ua.includes('Android')) {
    const version = ua.match(/Android (\d+\.\d+)/)
    return { 
      type: HbtOsType.Android, 
      version: version ? `Android ${version[1]}` : 'Android'
    }
  }
  
  return { type: HbtOsType.Other, version: 'Unknown' }
}

/**
 * 检测设备类型
 */
function detectDevice(): { type: HbtDeviceType; name: string; model: string } {
  const ua = navigator.userAgent
  
  // 检测移动设备
  if (/Mobile|Android|iPhone|iPad|iPod/i.test(ua)) {
    if (/iPad|Tablet/i.test(ua)) {
      return { 
        type: HbtDeviceType.Tablet,
        name: 'Tablet',
        model: ua
      }
    }
    return { 
      type: HbtDeviceType.Mobile,
      name: 'Mobile',
      model: ua
    }
  }
  
  // 默认为PC
  return { 
    type: HbtDeviceType.PC,
    name: 'PC',
    model: ua
  }
}

/**
 * 获取WebGL渲染器信息
 */
function getWebGLRenderer(): string {
  try {
    const canvas = document.createElement('canvas');
    const gl = canvas.getContext('webgl') as WebGLRenderingContext;
    if (!gl) return '';
    
    const debugInfo = gl.getExtension('WEBGL_debug_renderer_info');
    if (!debugInfo) return '';
    
    return gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL);
  } catch {
    return '';
  }
}

/**
 * 获取设备信息
 */
export function getDeviceInfo(): DeviceInfo {
  const browser = detectBrowser()
  const os = detectOS()
  const device = detectDevice()
  
  // 收集设备特征
  const deviceInfo: DeviceInfo = {
    deviceId: '',  // 由后端生成
    deviceType: device.type,
    deviceName: device.name,
    deviceModel: device.model,
    osType: os.type,
    osVersion: os.version,
    browserType: browser.type,
    browserVersion: browser.version,
    resolution: `${screen.width}x${screen.height}`,
    processorCores: navigator.hardwareConcurrency?.toString() || '',
    platformVendor: navigator.vendor || '',
    hardwareConcurrency: navigator.hardwareConcurrency?.toString() || '',
    systemLanguage: navigator.language || '',
    timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone || '',
    screenColorDepth: screen.colorDepth?.toString() || '',
    deviceMemory: (navigator as any).deviceMemory?.toString() || '',
    webGLRenderer: getWebGLRenderer()
  }

  // 生成设备指纹
  const deviceFeatures = [
    deviceInfo.processorCores,
    deviceInfo.hardwareConcurrency,
    deviceInfo.webGLRenderer,
    deviceInfo.resolution,
    deviceInfo.screenColorDepth,
    deviceInfo.timeZone,
    deviceInfo.platformVendor
  ].join('|');

  deviceInfo.deviceFingerprint = btoa(deviceFeatures);  // Base64编码设备特征
  
  return deviceInfo
} 