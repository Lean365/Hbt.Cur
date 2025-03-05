/**
 * ===================================================================
 * 项目名称: Lean.Hbt
 * 文件名称: useDictData.ts
 * 创建日期: 2024-03-20
 * 描述: 字典数据管理Hook，提供字典数据的加载、获取和转换功能
 * =================================================================== 
 */

import { ref, onMounted } from 'vue'
import { getHbtDictDataList } from '@/api/admin/hbtDictData'
import { getHbtDictType, executeDictSql } from '@/api/admin/hbtDictType'
import type { HbtDictData, HbtDictDataQuery } from '@/types/admin/hbtDictData'
import type { HbtDictType } from '@/types/admin/hbtDictType'
import type { ApiResult, PageResult } from '@/types/base'
import type { AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'

/**
 * 字典选项接口定义
 * @interface DictOption
 * @property {string} label - 显示标签
 * @property {number} value - 选项值
 * @property {string} [cssClass] - CSS类名
 * @property {string} [listClass] - 列表项CSS类名
 * @property {number} [isDefault] - 是否默认选项
 * @property {string} [extLabel] - 扩展标签
 * @property {string} [extValue] - 扩展值
 * @property {string} [transKey] - 国际化翻译键值
 */
export interface DictOption {
  label: string
  value: number
  cssClass?: string
  listClass?: string
  isDefault?: number
  extLabel?: string
  extValue?: string
  transKey?: string
}

/**
 * 字典数据Hook
 * @param {string[]} dictTypes - 字典类型数组
 * @returns {Object} 返回字典数据相关的方法和状态
 */
export function useDictData(dictTypes: string[]) {
  const { t } = useI18n()
  // 存储字典数据的Map，key为字典类型，value为选项数组
  const dictDataMap = ref<Record<string, DictOption[]>>({})
  // 加载状态标志
  const loading = ref(false)

  /**
   * 处理SQL执行结果，转换为标准选项格式
   * @param rows SQL执行结果行数据
   * @returns 标准选项数组
   */
  const convertSqlResultToOptions = (rows: any[]): DictOption[] => {
    if (!rows || !rows.length) return []
    
    // 获取第一行数据的所有字段
    const fields = Object.keys(rows[0])
    
    // 尝试识别标签和值字段
    const labelField = fields.find(f => 
      f.toLowerCase().includes('name') || 
      f.toLowerCase().includes('label') || 
      f.toLowerCase().includes('text')
    ) || fields[1] || fields[0]
    
    const valueField = fields.find(f => 
      f.toLowerCase().includes('id') || 
      f.toLowerCase().includes('value') || 
      f.toLowerCase().includes('key') ||
      f.toLowerCase().includes('code')
    ) || fields[0]
    
    // 尝试识别其他扩展字段
    const extLabelField = fields.find(f => f.toLowerCase().includes('ext_label'))
    const extValueField = fields.find(f => f.toLowerCase().includes('ext_value'))
    const transKeyField = fields.find(f => f.toLowerCase().includes('trans_key'))
    const cssClassField = fields.find(f => f.toLowerCase().includes('css_class'))
    const listClassField = fields.find(f => f.toLowerCase().includes('list_class'))
    const isDefaultField = fields.find(f => f.toLowerCase().includes('is_default'))
    
    // 转换为标准选项格式
    return rows.map(row => ({
      label: String(row[labelField]),
      value: Number(row[valueField]),
      extLabel: extLabelField ? row[extLabelField] : undefined,
      extValue: extValueField ? row[extValueField] : undefined,
      transKey: transKeyField ? row[transKeyField] : undefined,
      cssClass: cssClassField ? row[cssClassField] : undefined,
      listClass: listClassField ? row[listClassField] : undefined,
      isDefault: isDefaultField ? Number(row[isDefaultField]) : undefined
    }))
  }

  /**
   * 加载字典数据
   * 根据字典类型数组加载对应的字典数据
   * 系统字典(dictCategory=0)：从字典数据表获取
   * SQL字典(dictCategory=1)：直接处理SQL脚本获取数据
   */
  const loadDictData = async () => {
    try {
      loading.value = true
      
      // 先获取所有字典类型的信息
      const typePromises = dictTypes.map(async dictType => {
        try {
          const response = await getHbtDictType(Number(dictType))
          return response.data.data
        } catch (error) {
          console.error(`获取字典类型[${dictType}]失败:`, error)
          return null
        }
      })

      const dictTypeInfos = await Promise.all(typePromises)
      
      // 分别处理系统字典和SQL字典
      for (let i = 0; i < dictTypes.length; i++) {
        const dictType = dictTypes[i]
        const typeInfo = dictTypeInfos[i]
        
        if (!typeInfo) {
          console.error(`字典类型[${dictType}]不存在`)
          continue
        }

        try {
          if (typeInfo.dictCategory === 1) { // SQL类型字典
            // 直接处理SQL脚本
            if (typeInfo.sqlScript) {
              try {
                // 执行SQL脚本
                const response = await executeDictSql(typeInfo.dictTypeId)
                if (response.data.code === 200 && Array.isArray(response.data.data)) {
                  // 转换SQL执行结果为标准选项格式
                  dictDataMap.value[dictType] = convertSqlResultToOptions(response.data.data)
                  console.log(`SQL字典[${dictType}]加载成功, 数据:`, dictDataMap.value[dictType])
                } else {
                  console.error(`SQL字典[${dictType}]执行失败:`, response.data.msg)
                  dictDataMap.value[dictType] = []
                }
              } catch (error) {
                console.error(`SQL字典[${dictType}]执行出错:`, error)
                dictDataMap.value[dictType] = []
              }
            } else {
              console.warn(`SQL字典[${dictType}]未配置SQL脚本`)
              dictDataMap.value[dictType] = []
            }
          } else { // 系统字典
            // 从字典数据表获取
            const response = await getHbtDictDataList({
              dictTypeId: Number(dictType),
              status: 1,
              pageNum: 1,
              pageSize: 100
            } as HbtDictDataQuery)

            const pageResult = response.data.data
            const dictDataList = pageResult.rows || []
            
            // 转换为标准的选项格式
            dictDataMap.value[dictType] = dictDataList.map((item: HbtDictData) => ({
              label: item.dictLabel,
              value: Number(item.dictValue),
              cssClass: item.cssClass,
              listClass: item.listClass,
              isDefault: item.isDefault,
              extLabel: item.extLabel,
              extValue: item.extValue,
              transKey: item.transKey
            }))
          }
        } catch (error) {
          console.error(`处理字典[${dictType}]失败:`, error)
          dictDataMap.value[dictType] = []
        }
      }
    } catch (error) {
      console.error('加载字典数据失败:', error)
      message.error(t('common.message.loadFailed'))
    } finally {
      loading.value = false
    }
  }

  /**
   * 获取指定字典类型的选项列表
   * @param {string} dictType - 字典类型
   * @returns {DictOption[]} 返回选项数组
   */
  const getDictOptions = (dictType: string) => {
    return dictDataMap.value[dictType] || []
  }

  /**
   * 根据值获取对应的标签
   * @param {string} dictType - 字典类型
   * @param {number} value - 选项值
   * @returns {string | undefined} 返回对应的标签
   */
  const getDictLabel = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    return options.find(item => item.value === value)?.label
  }

  /**
   * 根据值获取对应的样式类
   * @param {string} dictType - 字典类型
   * @param {number} value - 选项值
   * @returns {string | undefined} 返回对应的样式类
   */
  const getDictClass = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    const option = options.find(item => item.value === value)
    return option?.listClass || option?.cssClass
  }

  /**
   * 根据值获取对应的扩展标签
   * @param {string} dictType - 字典类型
   * @param {number} value - 选项值
   * @returns {string | undefined} 返回对应的扩展标签
   */
  const getDictExtLabel = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    return options.find(item => item.value === value)?.extLabel
  }

  /**
   * 根据值获取对应的扩展值
   * @param {string} dictType - 字典类型
   * @param {number} value - 选项值
   * @returns {string | undefined} 返回对应的扩展值
   */
  const getDictExtValue = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    return options.find(item => item.value === value)?.extValue
  }

  /**
   * 根据值获取对应的翻译键值
   * @param {string} dictType - 字典类型
   * @param {number} value - 选项值
   * @returns {string | undefined} 返回对应的翻译键值
   */
  const getDictTransKey = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    return options.find(item => item.value === value)?.transKey
  }

  /**
   * 重新加载字典数据
   * @returns {Promise} 返回加载Promise
   */
  const reloadDictData = () => {
    return loadDictData()
  }

  // 组件挂载时自动加载字典数据
  onMounted(() => {
    loadDictData()
  })

  // 返回Hook的方法和状态
  return {
    dictDataMap,      // 字典数据Map
    loading,          // 加载状态
    getDictOptions,   // 获取选项列表方法
    getDictLabel,     // 获取标签方法
    getDictClass,     // 获取样式类方法
    getDictExtLabel,  // 获取扩展标签方法
    getDictExtValue,  // 获取扩展值方法
    getDictTransKey,  // 获取翻译键值方法
    reloadDictData    // 重新加载方法
  }
} 