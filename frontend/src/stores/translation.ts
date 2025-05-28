import { defineStore } from 'pinia'
import { getHbtTranslationsByModule, getHbtTranslationValue } from '@/api/core/translation'
import type { HbtTranslation } from '@/types/core/translation'
import type { HbtApiResponse } from '@/types/common'

export const useTranslationStore = defineStore('translation', {
  state: () => ({
    translations: new Map<string, Map<string, string>>(), // module -> (key -> value)
    loading: false
  }),

  actions: {
    // 加载指定模块的翻译
    async loadModuleTranslations(module: string, langCode: string) {
      try {
        this.loading = true
        const response = await getHbtTranslationsByModule(langCode, module)
        if (response.data) {
          const moduleMap = new Map<string, string>()
          const translations = (response.data as unknown) as HbtTranslation[]
          translations.forEach((item) => {
            moduleMap.set(item.transKey, item.transValue)
          })
          this.translations.set(module, moduleMap)
        }
      } catch (error) {
        console.error('加载翻译失败:', error)
      } finally {
        this.loading = false
      }
    },

    // 获取翻译值
    async getTranslation(key: string, langCode: string): Promise<string> {
      // 解析翻译键，格式：module.submodule.key
      const parts = key.split('.')
      if (parts.length < 2) {
        console.warn('无效的翻译键格式:', key)
        return key
      }

      const module = parts[0]
      const transKey = key

      // 先检查缓存
      const moduleMap = this.translations.get(module)
      if (moduleMap?.has(transKey)) {
        return moduleMap.get(transKey)!
      }

      // 如果缓存中没有，从服务器获取
      try {
        const response = await getHbtTranslationValue(langCode, transKey)
        if (response.data) {
          const value = (response.data as unknown) as string
          // 更新缓存
          if (!moduleMap) {
            this.translations.set(module, new Map([[transKey, value]]))
          } else {
            moduleMap.set(transKey, value)
          }
          return value
        }
      } catch (error) {
        console.error('获取翻译失败:', error)
      }
      return key // 如果获取失败，返回键名
    },

    // 清除指定模块的翻译缓存
    clearModuleCache(module: string) {
      this.translations.delete(module)
    },

    // 清除所有翻译缓存
    clearAllCache() {
      this.translations.clear()
    }
  }
}) 