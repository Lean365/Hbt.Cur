//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtDictTypeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Application.Dtos.Admin;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 字典类型服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface IHbtDictTypeService
    {
        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        Task<HbtPagedResult<HbtDictTypeDto>> GetPagedListAsync(HbtDictTypeQueryDto query);

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        Task<HbtDictTypeDto> GetAsync(long dictTypeId);

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        Task<long> InsertAsync(HbtDictTypeCreateDto input);

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtDictTypeUpdateDto input);

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long dictTypeId);

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] dictTypeIds);

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        /// <param name="dictTypes">字典类型数据列表</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(List<HbtDictTypeImportDto> dictTypes);

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        Task<List<HbtDictTypeExportDto>> ExportAsync(HbtDictTypeQueryDto query);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        Task<HbtDictTypeTemplateDto> GetTemplateAsync();

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtDictTypeStatusDto input);
    }
} 