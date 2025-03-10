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

// 清除全局字典缓存的函数
export function clearDictCache(): void {
  console.log('[字典Hook] 清除全局字典缓存')
  globalDictCache.clear()
}

export function useDictData(dictTypes: string[]) {
  const { t } = useI18n()
  const loading = ref(false)
  const dictDataMap = ref(globalDictCache)
  const validTypes = Array.from(new Set(dictTypes?.filter(type => type && typeof type === 'string') || []))

  const loadDictType = async (dictType: string): Promise<HbtDictType | null> => {
    try {
      console.log(`[字典Hook] 开始请求字典类型[${dictType}]...`)
      const { data: apiResult } = await getHbtDictTypeByType(dictType)
      console.log(`[字典Hook]  { data: apiResult } 字典类型[${dictType}]响应:`, apiResult)
      
      if (!apiResult || apiResult.code !== 200 || !apiResult.data) {
        console.warn(`[字典Hook] 字典类型[${dictType}]响应无效:`, apiResult)
        return null
      }
      
      const dictTypeData = apiResult.data as HbtDictType
      return dictTypeData
    } catch (error) {
      console.error(`[字典Hook] 字典类型[${dictType}]加载出错:`, error)
      return null
    }
  }

  const loadDictData = async (dictType: string): Promise<void> => {
    try {
      console.log(`[字典Hook] 开始加载字典数据[${dictType}]`)
      const { data: apiResult } = await getHbtDictDataByType(dictType)
      console.log(`[字典Hook] 字典数据[${dictType}]响应:`, apiResult)
      
      if (!apiResult || apiResult.code !== 200 || !apiResult.data || !Array.isArray(apiResult.data)) {
        console.warn(`[字典Hook] 字典数据[${dictType}]响应格式无效:`, apiResult)
        return
      }
      
      const options = apiResult.data.map(item => ({
        label: item.dictLabel || '',
        value: Number(item.dictValue) || 0,
        cssClass: item.cssClass || '',
        listClass: item.listClass || '',
        isDefault: Number(item.isDefault) || 0,
        extLabel: item.extLabel || '',
        extValue: item.extValue || '',
        transKey: item.transKey || '',
        disabled: false
      }))
      dictDataMap.value.set(dictType, options)
      console.log(`[字典Hook] 字典数据[${dictType}]加载完成，数据条数:`, options.length)
    } catch (error) {
      console.error(`[字典Hook] 字典数据[${dictType}]加载出错:`, error)
    }
  }

  const loadAllDictData = async () => {
    if (loading.value) {
      console.log('[字典Hook] 正在加载中，跳过重复加载')
      return
    }

    try {
      loading.value = true
      console.log('[字典Hook] 开始加载字典数据:', validTypes)

      for (const dictType of validTypes) {
        const typeInfo = await loadDictType(dictType)
        if (typeInfo) {
          await loadDictData(dictType)
        }
      }
    } finally {
      loading.value = false
      console.log('[字典Hook] 字典加载完成')
    }
  }

  onMounted(() => {
    if (validTypes.length > 0) {
      loadAllDictData()
    }
  })

  const getDictOptions = (dictType: string): DictOption[] => {
    return dictDataMap.value.get(dictType) || []
  }

  const getDictLabel = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.label
  }

  const getDictClass = (dictType: string, value: number): string | undefined => {
    const option = getDictOptions(dictType).find(item => item.value === value)
    return option?.listClass || option?.cssClass
  }

  const getDictExtLabel = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.extLabel
  }

  const getDictExtValue = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.extValue
  }

  const getDictTransKey = (dictType: string, value: number): string | undefined => {
    return getDictOptions(dictType).find(item => item.value === value)?.transKey
  }

  return {
    loading,
    getDictOptions,
    getDictLabel,
    getDictClass,
    getDictExtLabel,
    getDictExtValue,
    getDictTransKey,
    reloadDictData: loadAllDictData
  }
} 