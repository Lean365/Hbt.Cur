import { useTranslationStore } from '@/stores/translation'
import { useI18n } from 'vue-i18n'

/**
 * 获取翻译值
 * @param key 翻译键，格式：module.submodule.key
 * @returns 翻译值
 */
export async function t(key: string): Promise<string> {
  const translationStore = useTranslationStore()
  const { locale } = useI18n()
  
  // 解析模块名
  const module = key.split('.')[0]
  
  // 确保模块翻译已加载
  await translationStore.loadModuleTranslations(module, locale.value)
  
  // 获取翻译
  return await translationStore.getTranslation(key, locale.value)
}

/**
 * 同步获取翻译值（从缓存中）
 * @param key 翻译键，格式：module.submodule.key
 * @returns 翻译值
 */
export function tSync(key: string): string {
  const translationStore = useTranslationStore()
  const { locale } = useI18n()
  
  // 解析模块名
  const module = key.split('.')[0]
  const moduleMap = translationStore.translations.get(module)
  
  // 从缓存中获取
  return moduleMap?.get(key) || key
} 