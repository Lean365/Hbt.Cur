import { ref, onMounted } from 'vue'
import { getHbtDictDataList } from '@/api/admin/hbtDictData'
import type { HbtDictData, HbtDictDataQuery } from '@/types/admin/hbtDictData'
import type { ApiResult, PageResult } from '@/types/base'
import type { AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'

export interface DictOption {
  label: string
  value: number
  cssClass?: string
  listClass?: string
  isDefault?: number
}

export function useDictData(dictTypes: string[]) {
  const { t } = useI18n()
  const dictDataMap = ref<Record<string, DictOption[]>>({})
  const loading = ref(false)

  // 加载字典数据
  const loadDictData = async () => {
    try {
      loading.value = true
      const promises = dictTypes.map(dictType =>
        getHbtDictDataList({
          dictTypeId: Number(dictType), // 转换为数字类型
          status: 1,
          pageNum: 1,
          pageSize: 100
        } as HbtDictDataQuery)
      )

      const results = await Promise.all(promises)
      
      results.forEach((res: AxiosResponse<ApiResult<PageResult<HbtDictData>>>, index) => {
        const dictType = dictTypes[index]
        const pageResult = res.data.data
        const dictDataList = pageResult.rows || []
        
        dictDataMap.value[dictType] = dictDataList.map((item: HbtDictData) => ({
          label: item.dictLabel,
          value: Number(item.dictValue),
          cssClass: item.cssClass,
          listClass: item.listClass,
          isDefault: item.isDefault
        }))
      })
    } catch (error) {
      console.error('加载字典数据失败:', error)
      message.error(t('common.message.loadFailed'))
    } finally {
      loading.value = false
    }
  }

  // 获取指定字典类型的选项
  const getDictOptions = (dictType: string) => {
    return dictDataMap.value[dictType] || []
  }

  // 根据值获取标签
  const getDictLabel = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    return options.find(item => item.value === value)?.label
  }

  // 根据值获取样式类
  const getDictClass = (dictType: string, value: number) => {
    const options = getDictOptions(dictType)
    const option = options.find(item => item.value === value)
    return option?.listClass || option?.cssClass
  }

  // 重新加载字典数据
  const reloadDictData = () => {
    return loadDictData()
  }

  onMounted(() => {
    loadDictData()
  })

  return {
    dictDataMap,
    loading,
    getDictOptions,
    getDictLabel,
    getDictClass,
    reloadDictData
  }
} 