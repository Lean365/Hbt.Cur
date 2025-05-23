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
    const response = await fetch('https://api.ipify.org?format=json');
    const data = await response.json();
    const ip = data.ip;
    
    // 检查是否为私有IP地址
    const isPrivateIP = /^(10\.|172\.(1[6-9]|2[0-9]|3[0-1])\.|192\.168\.)/.test(ip);
    if (isPrivateIP) return 0;

    // 检查是否为已知的VPN服务提供商IP
    const vpnIPRanges = [
      // NordVPN
      /^185\.(65\.|66\.|67\.|68\.|69\.|70\.|71\.|72\.|73\.|74\.|75\.|76\.|77\.|78\.|79\.|80\.|81\.|82\.|83\.|84\.|85\.|86\.|87\.|88\.|89\.|90\.|91\.|92\.|93\.|94\.|95\.|96\.|97\.|98\.|99\.|100\.|101\.|102\.|103\.|104\.|105\.|106\.|107\.|108\.|109\.|110\.|111\.|112\.|113\.|114\.|115\.|116\.|117\.|118\.|119\.|120\.|121\.|122\.|123\.|124\.|125\.|126\.|127\.|128\.|129\.|130\.|131\.|132\.|133\.|134\.|135\.|136\.|137\.|138\.|139\.|140\.|141\.|142\.|143\.|144\.|145\.|146\.|147\.|148\.|149\.|150\.|151\.|152\.|153\.|154\.|155\.|156\.|157\.|158\.|159\.|160\.|161\.|162\.|163\.|164\.|165\.|166\.|167\.|168\.|169\.|170\.|171\.|172\.|173\.|174\.|175\.|176\.|177\.|178\.|179\.|180\.|181\.|182\.|183\.|184\.|185\.|186\.|187\.|188\.|189\.|190\.|191\.|192\.|193\.|194\.|195\.|196\.|197\.|198\.|199\.|200\.|201\.|202\.|203\.|204\.|205\.|206\.|207\.|208\.|209\.|210\.|211\.|212\.|213\.|214\.|215\.|216\.|217\.|218\.|219\.|220\.|221\.|222\.|223\.|224\.|225\.|226\.|227\.|228\.|229\.|230\.|231\.|232\.|233\.|234\.|235\.|236\.|237\.|238\.|239\.|240\.|241\.|242\.|243\.|244\.|245\.|246\.|247\.|248\.|249\.|250\.|251\.|252\.|253\.|254\.|255\.)/,
      // ExpressVPN
      /^45\.(67\.|68\.|69\.|70\.|71\.|72\.|73\.|74\.|75\.|76\.|77\.|78\.|79\.|80\.|81\.|82\.|83\.|84\.|85\.|86\.|87\.|88\.|89\.|90\.|91\.|92\.|93\.|94\.|95\.|96\.|97\.|98\.|99\.|100\.|101\.|102\.|103\.|104\.|105\.|106\.|107\.|108\.|109\.|110\.|111\.|112\.|113\.|114\.|115\.|116\.|117\.|118\.|119\.|120\.|121\.|122\.|123\.|124\.|125\.|126\.|127\.|128\.|129\.|130\.|131\.|132\.|133\.|134\.|135\.|136\.|137\.|138\.|139\.|140\.|141\.|142\.|143\.|144\.|145\.|146\.|147\.|148\.|149\.|150\.|151\.|152\.|153\.|154\.|155\.|156\.|157\.|158\.|159\.|160\.|161\.|162\.|163\.|164\.|165\.|166\.|167\.|168\.|169\.|170\.|171\.|172\.|173\.|174\.|175\.|176\.|177\.|178\.|179\.|180\.|181\.|182\.|183\.|184\.|185\.|186\.|187\.|188\.|189\.|190\.|191\.|192\.|193\.|194\.|195\.|196\.|197\.|198\.|199\.|200\.|201\.|202\.|203\.|204\.|205\.|206\.|207\.|208\.|209\.|210\.|211\.|212\.|213\.|214\.|215\.|216\.|217\.|218\.|219\.|220\.|221\.|222\.|223\.|224\.|225\.|226\.|227\.|228\.|229\.|230\.|231\.|232\.|233\.|234\.|235\.|236\.|237\.|238\.|239\.|240\.|241\.|242\.|243\.|244\.|245\.|246\.|247\.|248\.|249\.|250\.|251\.|252\.|253\.|254\.|255\.)/,
      // Surfshark
      /^103\.(149\.|150\.|151\.|152\.|153\.|154\.|155\.|156\.|157\.|158\.|159\.|160\.|161\.|162\.|163\.|164\.|165\.|166\.|167\.|168\.|169\.|170\.|171\.|172\.|173\.|174\.|175\.|176\.|177\.|178\.|179\.|180\.|181\.|182\.|183\.|184\.|185\.|186\.|187\.|188\.|189\.|190\.|191\.|192\.|193\.|194\.|195\.|196\.|197\.|198\.|199\.|200\.|201\.|202\.|203\.|204\.|205\.|206\.|207\.|208\.|209\.|210\.|211\.|212\.|213\.|214\.|215\.|216\.|217\.|218\.|219\.|220\.|221\.|222\.|223\.|224\.|225\.|226\.|227\.|228\.|229\.|230\.|231\.|232\.|233\.|234\.|235\.|236\.|237\.|238\.|239\.|240\.|241\.|242\.|243\.|244\.|245\.|246\.|247\.|248\.|249\.|250\.|251\.|252\.|253\.|254\.|255\.)/,
      // ProtonVPN
      /^185\.(159\.|160\.|161\.|162\.|163\.|164\.|165\.|166\.|167\.|168\.|169\.|170\.|171\.|172\.|173\.|174\.|175\.|176\.|177\.|178\.|179\.|180\.|181\.|182\.|183\.|184\.|185\.|186\.|187\.|188\.|189\.|190\.|191\.|192\.|193\.|194\.|195\.|196\.|197\.|198\.|199\.|200\.|201\.|202\.|203\.|204\.|205\.|206\.|207\.|208\.|209\.|210\.|211\.|212\.|213\.|214\.|215\.|216\.|217\.|218\.|219\.|220\.|221\.|222\.|223\.|224\.|225\.|226\.|227\.|228\.|229\.|230\.|231\.|232\.|233\.|234\.|235\.|236\.|237\.|238\.|239\.|240\.|241\.|242\.|243\.|244\.|245\.|246\.|247\.|248\.|249\.|250\.|251\.|252\.|253\.|254\.|255\.)/,
      // 数据中心IP范围
      /^103\.(21\.|22\.|23\.|24\.|25\.|26\.|27\.|28\.|29\.|30\.|31\.|32\.|33\.|34\.|35\.|36\.|37\.|38\.|39\.|40\.|41\.|42\.|43\.|44\.|45\.|46\.|47\.|48\.|49\.|50\.|51\.|52\.|53\.|54\.|55\.|56\.|57\.|58\.|59\.|60\.|61\.|62\.|63\.|64\.|65\.|66\.|67\.|68\.|69\.|70\.|71\.|72\.|73\.|74\.|75\.|76\.|77\.|78\.|79\.|80\.|81\.|82\.|83\.|84\.|85\.|86\.|87\.|88\.|89\.|90\.|91\.|92\.|93\.|94\.|95\.|96\.|97\.|98\.|99\.|100\.|101\.|102\.|103\.|104\.|105\.|106\.|107\.|108\.|109\.|110\.|111\.|112\.|113\.|114\.|115\.|116\.|117\.|118\.|119\.|120\.|121\.|122\.|123\.|124\.|125\.|126\.|127\.|128\.|129\.|130\.|131\.|132\.|133\.|134\.|135\.|136\.|137\.|138\.|139\.|140\.|141\.|142\.|143\.|144\.|145\.|146\.|147\.|148\.|149\.|150\.|151\.|152\.|153\.|154\.|155\.|156\.|157\.|158\.|159\.|160\.|161\.|162\.|163\.|164\.|165\.|166\.|167\.|168\.|169\.|170\.|171\.|172\.|173\.|174\.|175\.|176\.|177\.|178\.|179\.|180\.|181\.|182\.|183\.|184\.|185\.|186\.|187\.|188\.|189\.|190\.|191\.|192\.|193\.|194\.|195\.|196\.|197\.|198\.|199\.|200\.|201\.|202\.|203\.|204\.|205\.|206\.|207\.|208\.|209\.|210\.|211\.|212\.|213\.|214\.|215\.|216\.|217\.|218\.|219\.|220\.|221\.|222\.|223\.|224\.|225\.|226\.|227\.|228\.|229\.|230\.|231\.|232\.|233\.|234\.|235\.|236\.|237\.|238\.|239\.|240\.|241\.|242\.|243\.|244\.|245\.|246\.|247\.|248\.|249\.|250\.|251\.|252\.|253\.|254\.|255\.)/
    ];

    // 检查IP是否匹配任何VPN范围
    const isVpnIP = vpnIPRanges.some(range => range.test(ip));
    
    // 检查是否为数据中心IP
    const isDatacenterIP = /^(103\.|185\.|45\.)/.test(ip);
    
    // 如果IP来自数据中心或匹配VPN范围，则认为是VPN
    return (isVpnIP || isDatacenterIP) ? 1 : 0;
  } catch {
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