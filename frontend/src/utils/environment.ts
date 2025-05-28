// 扩展Window接口
declare global {
  interface Window {
    __wxjs_environment?: string;
  }
}

import type { HbtSignalREnvironment } from '@/types/identity/auth'

/**
 * 生成环境指纹
 * 基于多个环境特征生成唯一标识
 */
function generateEnvironmentFingerprint(): string {
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
  
  return fingerprint
}

/**
 * 获取登录类型
 * @returns 登录类型（0=其他 1=密码 2=短信 3=邮箱 4=微信 5=QQ 6=钉钉）
 */
export const getLoginType = (): number => {
  // 从 URL 参数中获取登录类型
  const urlParams = new URLSearchParams(window.location.search);
  const loginType = urlParams.get('loginType');
  
  // 从 localStorage 中获取上次的登录类型
  const lastLoginType = localStorage.getItem('lastLoginType');
  
  // 如果 URL 中有指定登录类型，优先使用
  if (loginType) {
    const type = parseInt(loginType);
    if (!isNaN(type) && type >= 0 && type <= 6) {
      localStorage.setItem('lastLoginType', type.toString());
      return type;
    }
  }
  
  // 如果有上次的登录类型记录，使用上次的类型
  if (lastLoginType) {
    const type = parseInt(lastLoginType);
    if (!isNaN(type) && type >= 0 && type <= 6) {
      return type;
    }
  }
  
  // 默认返回密码登录
  return 1;
};

/**
 * 获取登录来源
 * @returns 登录来源（0=Web 1=App 2=小程序 3=其他）
 */
export const getLoginSource = (): number => {
  // 判断当前环境
  if (typeof window === 'undefined') return 3; // 服务端渲染
  
  // 判断是否为移动设备
  const isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
  
  // 判断是否为小程序环境
  const isMiniProgram = window.__wxjs_environment === 'miniprogram';
  
  if (isMiniProgram) return 2; // 小程序
  if (isMobile) return 1; // App
  return 0; // Web
};

/**
 * 获取登录提供者
 * @returns 登录提供者（0=系统 1=微信 2=钉钉 3=企业微信）
 */
export const getLoginProvider = (): number => {
  // 从 URL 参数中获取登录提供者
  const urlParams = new URLSearchParams(window.location.search);
  const provider = urlParams.get('provider');
  
  // 从 localStorage 中获取上次的登录提供者
  const lastProvider = localStorage.getItem('lastLoginProvider');
  
  // 如果 URL 中有指定提供者，优先使用
  if (provider) {
    const providerId = parseInt(provider);
    if (!isNaN(providerId) && providerId >= 0 && providerId <= 3) {
      localStorage.setItem('lastLoginProvider', providerId.toString());
      return providerId;
    }
  }
  
  // 如果有上次的提供者记录，使用上次的提供者
  if (lastProvider) {
    const providerId = parseInt(lastProvider);
    if (!isNaN(providerId) && providerId >= 0 && providerId <= 3) {
      return providerId;
    }
  }
  
  // 检查是否为微信环境
  const isWechat = /MicroMessenger/i.test(navigator.userAgent);
  if (isWechat) {
    return 1; // 微信
  }
  
  // 检查是否为钉钉环境
  const isDingTalk = /DingTalk/i.test(navigator.userAgent);
  if (isDingTalk) {
    return 2; // 钉钉
  }
  
  // 检查是否为企业微信环境
  const isWxWork = /wxwork/i.test(navigator.userAgent);
  if (isWxWork) {
    return 3; // 企业微信
  }
  
  // 默认返回系统
  return 0;
};

/**
 * 获取提供者Key
 * @returns 提供者Key
 */
export const getProviderKey = (): string => {
  const provider = getLoginProvider();
  
  // 从 localStorage 中获取提供者 Key
  const providerKey = localStorage.getItem(`providerKey_${provider}`);
  
  // 如果已有缓存的 Key，直接返回
  if (providerKey) {
    return providerKey;
  }
  
  // 根据不同的提供者返回对应的 Key
  switch (provider) {
    case 1: // 微信
      return process.env.VUE_APP_WECHAT_APPID || '';
    case 2: // 钉钉
      return process.env.VUE_APP_DINGTALK_APPKEY || '';
    case 3: // 企业微信
      return process.env.VUE_APP_WXWORK_CORPID || '';
    default: // 系统
      return '';
  }
};

/**
 * 获取提供者显示名称
 * @returns 提供者显示名称
 */
export const getProviderDisplayName = (): string => {
  const provider = getLoginProvider();
  
  // 从 localStorage 中获取提供者显示名称
  const displayName = localStorage.getItem(`providerDisplayName_${provider}`);
  
  // 如果已有缓存的显示名称，直接返回
  if (displayName) {
    return displayName;
  }
  
  // 根据不同的提供者返回对应的显示名称
  switch (provider) {
    case 1: // 微信
      return '微信登录';
    case 2: // 钉钉
      return '钉钉登录';
    case 3: // 企业微信
      return '企业微信登录';
    default: // 系统
      return '账号密码登录';
  }
};

/**
 * 获取完整环境信息
 */
export const getEnvironmentInfo = async (): Promise<HbtSignalREnvironment> => {
  const fingerprint = generateEnvironmentFingerprint();
  const now = new Date().toISOString();

  return {
    tenantId: 1,
    userId: 0,
    deviceId: 0,
    environmentId: fingerprint,
    loginType: getLoginType(),
    loginSource: getLoginSource(),
    loginStatus:0,
    loginProvider: getLoginProvider(),
    providerKey: getProviderKey(),
    providerDisplayName: getProviderDisplayName(),
    networkType: await getNetworkType(),
    timeZone: getTimeZone(),
    language: getLanguage(),
    isVpn: await checkIsVpn(),
    isProxy: await checkIsProxy(),
    status: 0,
    firstLoginTime: now,
    firstLoginIp: '',
    firstLoginLocation: '',
    firstLoginDeviceId: '',
    firstLoginDeviceType: 0,
    firstLoginBrowser: 0,
    firstLoginOs: 0,
    lastLoginTime: now,
    lastLoginIp: '',
    lastLoginLocation: '',
    lastLoginDeviceId: '',
    lastLoginDeviceType: 0,
    lastLoginBrowser: 0,
    lastLoginOs: 0,
    lastOfflineTime: '',
    todayOnlinePeriods: 0,
    loginCount: 0,
    continuousLoginDays: 0,
    environmentFingerprint: fingerprint
  };
};

// 环境信息相关工具

/**
 * 获取网络类型
 */
export const getNetworkType = async (): Promise<number> => {
  if (!('connection' in navigator)) return 4;
  const connection = (navigator as any).connection;
  if (connection.type === 'wifi') return 0;
  if (connection.type === 'cellular') {
    const operator = connection.effectiveType;
    if (operator?.includes('chinamobile')) return 1;
    if (operator?.includes('chinatelecom')) return 2;
    if (operator?.includes('chinaunicom')) return 3;
  }
  return 4;
};

/**
 * 获取时区
 */
export const getTimeZone = (): string => {
  return Intl.DateTimeFormat().resolvedOptions().timeZone;
};

/**
 * 获取浏览器语言
 */
export const getLanguage = (): string => {
  return navigator.language || 'zh-CN';
};

/**
 * 检查是否使用VPN
 */
export const checkIsVpn = async (): Promise<number> => {
  try {
    // 1. WebRTC检测
    const webrtcResult = await checkWebRTC();
    if (webrtcResult === 1) return 1;

    // 2. DNS泄露检测
    const dnsResult = await checkDNSLeak();
    if (dnsResult === 1) return 1;

    // 3. WebGL指纹检测
    const webglResult = checkWebGLFingerprint();
    if (webglResult === 1) return 1;

    return 0;
  } catch (error) {
    console.warn('[VPN检查] 检查失败:', error);
    return 0;
  }
};

/**
 * WebRTC检测
 */
const checkWebRTC = async (): Promise<number> => {
  try {
    const pc = new RTCPeerConnection();
    pc.createDataChannel('');
    const offer = await pc.createOffer();
    await pc.setLocalDescription(offer);
    
    // 检查是否有多个候选IP地址
    const candidates = pc.localDescription?.sdp?.match(/candidate:.+/g) || [];
    const ipAddresses = new Set();
    
    candidates.forEach(candidate => {
      const match = candidate.match(/candidate:.+ (\d+\.\d+\.\d+\.\d+)/);
      if (match) {
        ipAddresses.add(match[1]);
      }
    });
    
    // 如果发现多个不同的IP地址，可能是VPN
    return ipAddresses.size > 1 ? 1 : 0;
  } catch (error) {
    console.warn('[VPN检查] WebRTC检查失败:', error);
    return 0;
  }
};

/**
 * DNS泄露检测
 */
const checkDNSLeak = async (): Promise<number> => {
  try {
    // 创建一个隐藏的iframe来加载一个不存在的域名
    const iframe = document.createElement('iframe');
    iframe.style.display = 'none';
    document.body.appendChild(iframe);
    
    // 记录开始时间
    const startTime = performance.now();
    
    // 尝试加载一个不存在的域名
    iframe.src = 'https://nonexistent-domain-' + Math.random().toString(36).substring(7) + '.com';
    
    // 等待一段时间后检查是否有DNS解析
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 如果DNS解析时间异常，可能是VPN
    const dnsTime = performance.now() - startTime;
    document.body.removeChild(iframe);
    
    // 如果DNS解析时间超过500ms，可能是VPN
    return dnsTime > 500 ? 1 : 0;
  } catch (error) {
    console.warn('[VPN检查] DNS检查失败:', error);
    return 0;
  }
};

/**
 * WebGL指纹检测
 */
const checkWebGLFingerprint = (): number => {
  try {
    const canvas = document.createElement('canvas');
    const gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl') as WebGLRenderingContext;
    
    if (!gl) return 0;
    
    // 获取WebGL渲染器信息
    const debugInfo = gl.getExtension('WEBGL_debug_renderer_info');
    if (!debugInfo) return 0;
    
    const renderer = gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL);
    const vendor = gl.getParameter(debugInfo.UNMASKED_VENDOR_WEBGL);
    
    // 检查是否使用了虚拟化技术
    const virtualizationKeywords = [
      'vmware',
      'virtualbox',
      'qemu',
      'xen',
      'kvm',
      'virtual',
      'vpn',
      'proxy'
    ];
    
    const combinedInfo = (renderer + ' ' + vendor).toLowerCase();
    return virtualizationKeywords.some(keyword => combinedInfo.includes(keyword)) ? 1 : 0;
  } catch (error) {
    console.warn('[VPN检查] WebGL检查失败:', error);
    return 0;
  }
};

/**
 * 检查是否使用代理
 */
export const checkIsProxy = async (): Promise<number> => {
  try {
    const pc = new RTCPeerConnection();
    pc.createDataChannel('');
    const offer = await pc.createOffer();
    await pc.setLocalDescription(offer);
    const candidates = pc.localDescription?.sdp?.match(/candidate:.+/g) || [];
    return candidates.length > 1 ? 1 : 0;
  } catch {
    return 0;
  }
}; 