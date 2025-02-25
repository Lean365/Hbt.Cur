//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dict.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据接口
//===================================================================

import request from '@/utils/request'
import type { DictQuery, DictResponse } from '@/types/components/dict'

/**
 * 获取字典数据
 * @param dictType 字典类型
 * @returns 字典数据列表
 */
export function getDictData(dictType: string) {
  return request<DictResponse>({
    url: '/system/dict/data/type/' + dictType,
    method: 'get'
  })
}

/**
 * 根据条件查询字典数据
 * @param query 查询条件
 * @returns 字典数据列表
 */
export function getDictDataList(query: DictQuery) {
  return request<DictResponse>({
    url: '/system/dict/data/list',
    method: 'get',
    params: query
  })
} 