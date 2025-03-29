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
 * 获取Canvas指纹
 */
function getCanvasFingerprint(): string {
  try {
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');
    if (!ctx) return '';
    
    // 绘制一些图形和文字
    canvas.width = 200;
    canvas.height = 50;
    ctx.textBaseline = "top";
    ctx.font = "14px 'Arial'";
    ctx.textBaseline = "alphabetic";
    ctx.fillStyle = "#f60";
    ctx.fillRect(125,1,62,20);
    ctx.fillStyle = "#069";
    ctx.fillText("Hello, world!", 2, 15);
    ctx.fillStyle = "rgba(102, 204, 0, 0.7)";
    ctx.fillText("Hello, world!", 4, 17);
    
    return canvas.toDataURL();
  } catch {
    return '';
  }
}

/**
 * 获取音频指纹
 */
function getAudioFingerprint(): string {
  try {
    const audioContext = new (window.AudioContext || (window as any).webkitAudioContext)();
    const oscillator = audioContext.createOscillator();
    const analyser = audioContext.createAnalyser();
    const gainNode = audioContext.createGain();
    const scriptProcessor = audioContext.createScriptProcessor(4096, 1, 1);
    
    return [
      audioContext.sampleRate,
      analyser.fftSize,
      oscillator.type,
      gainNode.gain.value,
      scriptProcessor.bufferSize
    ].join('_');
  } catch {
    return '';
  }
}

/**
 * 获取WebGL参数
 */
function getWebGLParameters(): string {
  try {
    const canvas = document.createElement('canvas');
    const gl = canvas.getContext('webgl');
    if (!gl) return '';
    
    const params = [
      gl.getParameter(gl.RED_BITS),
      gl.getParameter(gl.GREEN_BITS),
      gl.getParameter(gl.BLUE_BITS),
      gl.getParameter(gl.ALPHA_BITS),
      gl.getParameter(gl.DEPTH_BITS),
      gl.getParameter(gl.STENCIL_BITS),
      gl.getParameter(gl.MAX_VERTEX_ATTRIBS),
      gl.getParameter(gl.MAX_VERTEX_UNIFORM_VECTORS),
      gl.getParameter(gl.MAX_VARYING_VECTORS),
      gl.getParameter(gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS),
      gl.getParameter(gl.MAX_VERTEX_TEXTURE_IMAGE_UNITS),
      gl.getParameter(gl.MAX_TEXTURE_IMAGE_UNITS),
      gl.getParameter(gl.MAX_FRAGMENT_UNIFORM_VECTORS),
      gl.getParameter(gl.ALIASED_LINE_WIDTH_RANGE),
      gl.getParameter(gl.ALIASED_POINT_SIZE_RANGE),
      gl.getParameter(gl.MAX_VIEWPORT_DIMS)
    ];
    
    return params.join('_');
  } catch {
    return '';
  }
}

/**
 * 获取已安装字体列表
 */
function getInstalledFonts(): string {
  const baseFonts = ['monospace', 'sans-serif', 'serif'];
  const fontList = [
    'Arial', 'Arial Black', 'Arial Narrow', 'Calibri', 'Cambria', 'Cambria Math', 'Comic Sans MS', 
    'Consolas', 'Courier', 'Courier New', 'Georgia', 'Helvetica', 'Impact', 'Lucida Console', 
    'Lucida Sans Unicode', 'Microsoft Sans Serif', 'MS Gothic', 'MS PGothic', 'MS Sans Serif', 
    'MS Serif', 'Palatino Linotype', 'Segoe Print', 'Segoe Script', 'Segoe UI', 'Segoe UI Light', 
    'Segoe UI Semibold', 'Segoe UI Symbol', 'Tahoma', 'Times', 'Times New Roman', 'Trebuchet MS', 
    'Verdana', 'Wingdings', 'Wingdings 2', 'Wingdings 3'
  ];

  const testString = 'mmmmmmmmmmlli';
  const testSize = '72px';
  const h = document.getElementsByTagName('body')[0];
  const s = document.createElement('span');
  s.style.fontSize = testSize;
  s.innerHTML = testString;
  const defaultWidth: {[key: string]: number} = {};
  const defaultHeight: {[key: string]: number} = {};

  for (const baseFont of baseFonts) {
    s.style.fontFamily = baseFont;
    h.appendChild(s);
    defaultWidth[baseFont] = s.offsetWidth;
    defaultHeight[baseFont] = s.offsetHeight;
    h.removeChild(s);
  }

  const detectedFonts = new Set<string>();
  for (const font of fontList) {
    let isDetected = false;
    for (const baseFont of baseFonts) {
      s.style.fontFamily = font + ',' + baseFont;
      h.appendChild(s);
      const matched = (s.offsetWidth !== defaultWidth[baseFont] || s.offsetHeight !== defaultHeight[baseFont]);
      h.removeChild(s);
      if (matched) {
        isDetected = true;
        break;
      }
    }
    if (isDetected) {
      detectedFonts.add(font);
    }
  }
  
  return Array.from(detectedFonts).join(',');
}

/**
 * 获取设备信息
 */
export function getDeviceInfo(): DeviceInfo {
  const browser = detectBrowser()
  const os = detectOS()
  const device = detectDevice()
  
  // 收集设备特征
  const deviceFeatures = [
    navigator.hardwareConcurrency?.toString() || '',
    getWebGLRenderer(),
    `${screen.width}x${screen.height}`,
    screen.colorDepth?.toString() || '',
    Intl.DateTimeFormat().resolvedOptions().timeZone || '',
    navigator.vendor || '',
    navigator.language || '',
    navigator.languages?.join(',') || '',
    navigator.platform || '',
    navigator.userAgent || '',
    getCanvasFingerprint(),
    getAudioFingerprint(),
    getWebGLParameters(),
    getInstalledFonts()
  ].join('|');

  const deviceFingerprint = btoa(deviceFeatures);  // Base64编码设备特征
  
  // 添加日志输出
  console.log('设备特征:', {
    hardwareConcurrency: navigator.hardwareConcurrency?.toString() || '',
    webGLRenderer: getWebGLRenderer(),
    resolution: `${screen.width}x${screen.height}`,
    colorDepth: screen.colorDepth?.toString() || '',
    timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone || '',
    vendor: navigator.vendor || '',
    language: navigator.language || '',
    languages: navigator.languages?.join(',') || '',
    platform: navigator.platform || '',
    userAgent: navigator.userAgent || '',
    canvasFingerprint: getCanvasFingerprint().length,
    audioFingerprint: getAudioFingerprint(),
    webGLParameters: getWebGLParameters().length,
    installedFonts: getInstalledFonts().length
  });
  //console.log('设备指纹:', deviceFingerprint);
  
  const deviceInfo: DeviceInfo = {
    deviceId: deviceFingerprint,  // 使用设备指纹作为临时ID
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
    webGLRenderer: getWebGLRenderer(),
    deviceFingerprint: deviceFingerprint
  }
  
  return deviceInfo
} 