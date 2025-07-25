import { defineStore } from 'pinia'
import { getHbtTranslationsByModule, getHbtTranslationValue } from '@/api/routine/core/translation'
import type { HbtTranslation } from '@/types/routine/core/translation'

export const useTranslationStore = defineStore('translation', {
  state: () => ({
    translations: new Map<string, Map<string, string>>(), // module -> (key -> value)
    loading: false,
    initialized: false, // 是否已初始化
    commonModules: ['common', 'core', 'identity'] // 常用模块列表
  }),

  actions: {
    // 初始化翻译 - 启动时调用
    async initializeTranslations(langCode: string) {
      // 重置初始化状态，确保每次都能重新加载
      this.initialized = false
      this.translations.clear()
      
      try {
        this.loading = true
        
        // 并行加载常用模块的翻译
        const promises = this.commonModules.map(module => 
          this.loadModuleTranslations(module, langCode)
        )
        
        await Promise.all(promises)
        this.initialized = true
      } catch (error) {
        console.error('[Translation] 翻译初始化失败:', error)
        // 即使失败也要标记为已初始化，避免重复尝试
        this.initialized = true
      } finally {
        this.loading = false
      }
    },

    // 加载指定模块的翻译
    async loadModuleTranslations(module: string, langCode: string) {
      try {
        const response = await getHbtTranslationsByModule(langCode, module)
        
        if (response.data) {
          const moduleMap = new Map<string, string>()
          
          // 检查数据类型并确保是数组
          let translations: HbtTranslation[]
          
          // 如果response.data本身就是数组
          if (Array.isArray(response.data)) {
            translations = response.data
          } 
          // 如果response.data是对象，尝试从data字段获取数组
          else if (response.data && typeof response.data === 'object' && 'data' in response.data) {
            const dataField = (response.data as any).data
            if (Array.isArray(dataField)) {
              translations = dataField
            } else {
              return
            }
          } else {
            return
          }
          
          // 确保每个元素都有正确的结构
          translations.forEach((item) => {
            if (item && typeof item === 'object') {
              // 处理后端返回的字段名，转换为小驼峰
              const transKey = (item as any).TransKey || (item as any).transKey
              const transValue = (item as any).TransValue || (item as any).transValue
              
              if (transKey && transValue) {
                moduleMap.set(transKey, transValue)
              }
            }
          })
          
          this.translations.set(module, moduleMap)
        }
      } catch (error) {
        console.error(`[Translation] 加载模块 ${module} 翻译失败:`, error)
        // 不要抛出错误，让其他模块继续加载
      }
    },

    // 获取翻译值
    async getTranslation(key: string, langCode: string): Promise<string> {
      // 解析翻译键，格式：module.submodule.key
      const parts = key.split('.')
      if (parts.length < 2) {
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
        console.error('[Translation] 获取翻译失败:', error)
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
      this.initialized = false
    },

    // 检查模块是否已加载
    isModuleLoaded(module: string): boolean {
      return this.translations.has(module)
    },

    // 获取已加载的模块列表
    getLoadedModules(): string[] {
      return Array.from(this.translations.keys())
    },

    // 获取翻译状态信息
    getStatus() {
      return {
        initialized: this.initialized,
        loading: this.loading,
        loadedModules: this.getLoadedModules(),
        totalModules: this.commonModules.length
      }
    }
  }
}) 