/**
 * ===================================================================
 * 项目名称: Lean.Hbt
 * 文件名称: useDictData.ts
 * 创建日期: 2024-03-20
 * 描述: 字典数据管理Hook，提供字典数据的加载、获取和转换功能
 * 
 * 字典缓存清除时机：
 * 1. 用户登出时 (stores/user.ts中的logout方法)
 * 2. 切换租户时 (在租户切换相关代码中)
 * 3. 字典数据变更时 (在字典管理页面的增删改操作后)
 * 4. 手动调用reloadDictData时 (需要刷新字典数据时)
 * =================================================================== 
 */

import { ref, onMounted } from 'vue'
import { getHbtDictDataByType } from '@/api/admin/hbtDictData'
import { getHbtDictTypeByType } from '@/api/admin/hbtDictType'
import type { HbtDictData } from '@/types/admin/hbtDictData'
import type { HbtDictType } from '@/types/admin/hbtDictType'
import type { ApiResult } from '@/types/base'
import { useI18n } from 'vue-i18n'

// 全局字典缓存
const globalDictCache = new Map<string, DictOption[]>()
const loadingPromises = new Map<string, Promise<void>>()

// 全局字典类型缓存
const globalDictTypeCache = new Map<string, HbtDictType>()

// 重试配置
const MAX_RETRIES = 3
const RETRY_DELAY = 1000

/**
 * 延迟指定时间
 * @param ms 延迟毫秒数
 */
const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms))

/**
 * 带重试的请求包装器
 * @param fn 请求函数
 * @param retries 重试次数
 * @returns 请求结果
 */
async function withRetry<T>(fn: () => Promise<T>, retries = MAX_RETRIES): Promise<T> {
  try {
    return await fn()
  } catch (error) {
    if (retries > 0) {
      console.log(`请求失败，${retries}秒后重试...`)
      await delay(RETRY_DELAY)
      return withRetry(fn, retries - 1)
    }
    throw error
  }
}

export interface DictOption {
  label: string
  value: number
  cssClass?: string
  listClass?: string
  isDefault?: number
  extLabel?: string
  extValue?: string
  transKey?: string
  disabled?: boolean
}

export interface UseDictDataReturn {
  loading: Ref<boolean>
  dictDataMap: Ref<Map<string, DictOption[]>>
  getDictOptions: (type: string) => DictOption[]
  getDictLabel: (type: string, value: number | string) => string
  getDictTransKey: (type: string, value: number | string) => string
  getDictClass: (type: string, value: number) => string | undefined
  getDictExtLabel: (type: string, value: number) => string | undefined
  getDictExtValue: (type: string, value: number) => string | undefined
  reloadDictData: () => Promise<void>
  loadAllDictData: () => Promise<void>
}

// 清除全局字典缓存的函数
export function clearDictCache(): void {
  //console.log('[字典Hook] 清除全局字典缓存')
  globalDictCache.clear()
  loadingPromises.clear()
  globalDictTypeCache.clear()
}

export function useDictData(dictTypes?: string[]): UseDictDataReturn {
  const { t } = useI18n()
  const loading = ref(false)
  const dictDataMap = ref<Map<string, DictOption[]>>(new Map())
  const validTypes = Array.from(new Set(dictTypes?.filter(type => type && typeof type === 'string') || []))

  // 加载字典类型
  const loadDictType = async (type: string) => {
    if (globalDictTypeCache.has(type)) {
      return globalDictTypeCache.get(type)!
    }

    try {
      const response = await withRetry(() => getHbtDictTypeByType(type))
      if (response?.data) {
        const dictType = (response as unknown as { data: HbtDictType }).data
        globalDictTypeCache.set(type, dictType)
        return dictType
      }
    } catch (error) {
      console.error(`加载字典类型[${type}]失败:`, error)
      throw error
    }
  }

  // 加载字典数据
  const loadDictData = async (type: string) => {
    // 如果已经有加载中的Promise，直接返回
    if (loadingPromises.has(type)) {
      console.log(`[字典Hook] 字典[${type}]正在加载中，复用Promise`)
      return loadingPromises.get(type)
    }

    // 如果已经有缓存，直接返回
    if (globalDictCache.has(type)) {
      console.log(`[字典Hook] 字典[${type}]命中缓存`)
      return Promise.resolve()
    }

    const loadPromise = (async () => {
      let retryCount = 0
      const maxRetries = 3
      let lastError: any = null
      
      while (retryCount < maxRetries) {
        try {
          console.log(`[字典Hook] 开始加载字典[${type}]${retryCount > 0 ? `，第${retryCount}次重试` : ''}`)
          const response = await getHbtDictDataByType(type)
          if (response?.data) {
            const dictDataList = (response as unknown as { data: HbtDictData[] }).data
            const options = dictDataList.map(item => ({
              label: item.dictLabel,
              value: Number(item.dictValue),
              cssClass: item.cssClass,
              listClass: item.listClass,
              isDefault: item.isDefault,
              extLabel: item.extLabel,
              extValue: item.extValue,
              transKey: item.transKey,
              disabled: item.status === 1
            }))
            globalDictCache.set(type, options)
            console.log(`[字典Hook] 字典[${type}]加载成功，共${options.length}条数据`)
            return
          }
        } catch (error) {
          lastError = error
          retryCount++
          if (retryCount === maxRetries) {
            console.error(`[字典Hook] 字典[${type}]加载失败，已重试${maxRetries}次，将抛出最后一次错误`)
            throw lastError
          }
          // 只在开发环境输出警告日志
          if (process.env.NODE_ENV === 'development') {
            console.warn(`[字典Hook] 字典[${type}]加载失败，${retryCount}秒后重试...`)
          }
          await delay(retryCount * 1000)
        }
      }
    })()

    loadingPromises.set(type, loadPromise)
    return loadPromise.finally(() => {
      loadingPromises.delete(type)
    })
  }

  /**
   * 加载所有字典数据
   */
  const loadAllDictData = async () => {
    if (!validTypes.length) return

    try {
      loading.value = true
      await Promise.all(validTypes.map(type => loadDictData(type)))
    } finally {
      loading.value = false
    }
  }

  onMounted(() => {
    if (validTypes.length > 0) {
      loadAllDictData()
    }
  })

  // 清除当前hook的缓存
  const clearCache = () => {
    dictDataMap.value = new Map()
  }

  const getDictOptions = (dictType: string): DictOption[] => {
    // 如果本地没有但全局有，从全局获取
    if (!dictDataMap.value.has(dictType) && globalDictCache.has(dictType)) {
      dictDataMap.value.set(dictType, globalDictCache.get(dictType)!)
    }
    return dictDataMap.value.get(dictType) || []
  }

  const getDictLabel = (type: string, value: number | string): string => {
    const option = getDictOptions(type).find(item => item.value === Number(value))
    return option?.label || String(value)
  }

  const getDictTransKey = (type: string, value: number | string): string => {
    const option = getDictOptions(type).find(item => item.value === Number(value))
    return option?.transKey || String(value)
  }

  // 样式类映射
  const CLASS_MAP: Record<number, string> = {
    // 基础状态
    0: 'default',   // 默认
    1: 'primary',   // 主要
    2: 'success',   // 成功
    3: 'info',      // 信息
    4: 'warning',   // 警告
    5: 'error',     // 错误
    6: 'disabled',  // 禁用

    // 流程状态
    10: 'process-draft',      // 草稿
    11: 'process-pending',    // 待处理
    12: 'process-running',    // 进行中
    13: 'process-completed',  // 已完成
    14: 'process-rejected',   // 已驳回
    15: 'process-canceled',   // 已取消
    16: 'process-suspended',  // 已暂停
    17: 'process-terminated', // 已终止
    18: 'process-expired',    // 已过期
    19: 'process-archived',   // 已归档

    // 邮件状态
    20: 'mail-unread',       // 未读
    21: 'mail-read',         // 已读
    22: 'mail-replied',      // 已回复
    23: 'mail-forwarded',    // 已转发
    24: 'mail-starred',      // 已标星
    25: 'mail-spam',         // 垃圾邮件
    26: 'mail-deleted',      // 已删除
    27: 'mail-draft',        // 草稿
    28: 'mail-sent',         // 已发送
    29: 'mail-failed',       // 发送失败

    // 通知状态
    30: 'notify-unread',     // 未读通知
    31: 'notify-read',       // 已读通知
    32: 'notify-urgent',     // 紧急通知
    33: 'notify-important',  // 重要通知
    34: 'notify-normal',     // 普通通知
    35: 'notify-system',     // 系统通知
    36: 'notify-business',   // 业务通知
    37: 'notify-expired',    // 已过期
    38: 'notify-processing', // 处理中
    39: 'notify-done',       // 已处理

    // 审批状态
    40: 'audit-pending',     // 待审批
    41: 'audit-approved',    // 已通过
    42: 'audit-rejected',    // 已拒绝
    43: 'audit-reviewing',   // 审核中
    44: 'audit-withdrawn',   // 已撤回
    45: 'audit-transferred', // 已转交
    46: 'audit-countersign', // 会签中
    47: 'audit-returned',    // 已退回
    48: 'audit-suspended',   // 已暂停
    49: 'audit-terminated',   // 已终止

    // 定时任务状态
    50: 'task-normal',      // 正常
    51: 'task-paused',      // 暂停
    52: 'task-running',     // 运行中
    53: 'task-error',       // 错误
    54: 'task-blocked',     // 阻塞
    55: 'task-expired',     // 过期
    56: 'task-timeout',     // 超时
    57: 'task-waiting',     // 等待中
    58: 'task-disabled',    // 已禁用
    59: 'task-deleted'      // 已删除
  } as const

  const getDictClass = (dictType: string, value: number): string | undefined => {
    const option = getDictOptions(dictType).find(item => item.value === value)
    if (!option) return undefined
    
    // 优先使用 listClass，如果没有则使用 cssClass
    const classValue = Number(option.listClass ?? option.cssClass)
    if (isNaN(classValue)) return 'default'
    
    return CLASS_MAP[classValue] || 'default'
  }

  const getDictExtLabel = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.extLabel
  }

  const getDictExtValue = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.extValue
  }

  return {
    loading,
    dictDataMap,
    getDictOptions,
    getDictLabel,
    getDictTransKey,
    getDictClass,
    getDictExtLabel,
    getDictExtValue,
    reloadDictData: loadAllDictData,
    loadAllDictData
  }
} 