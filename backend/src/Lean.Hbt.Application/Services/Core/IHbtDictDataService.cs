//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtDictDataService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 字典数据服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface IHbtDictDataService
    {
        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        Task<HbtPagedResult<HbtDictDataDto>> GetListAsync(HbtDictDataQueryDto query);

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="id">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        Task<HbtDictDataDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        Task<long> CreateAsync(HbtDictDataCreateDto input);

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtDictDataUpdateDto input);

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long dictDataId);

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] dictDataIds);

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtDictDataQueryDto query, string sheetName = "HbtDictData");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtDictData");

        /// <summary>
        /// 更新字典数据状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtDictDataStatusDto input);

        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <returns>字典数据列表</returns>
        Task<List<HbtDictDataDto>> GetListAsync();

        /// <summary>
        /// 根据字典类型获取字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        Task<List<HbtDictDataDto>> GetListByDictTypeAsync(string dictType);
    }
}