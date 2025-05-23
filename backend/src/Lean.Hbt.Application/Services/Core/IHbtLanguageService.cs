//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLanguageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 语言服务接口
    /// </summary>
    public interface IHbtLanguageService
    {
        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        Task<HbtPagedResult<HbtLanguageDto>> GetListAsync(HbtLanguageQueryDto query);

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>语言详情</returns>
        Task<HbtLanguageDto> GetByIdAsync(long LanguageId);

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        Task<long> CreateAsync(HbtLanguageCreateDto input);

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtLanguageUpdateDto input);

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long LanguageId);

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="LanguageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] LanguageIds);

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtLanguageQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtLanguageStatusDto input);
    }
}