//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLanguageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.Application.Services.Admin
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
        Task<HbtPagedResult<HbtLanguageDto>> GetPagedListAsync(HbtLanguageQueryDto query);

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="LangId">语言ID</param>
        /// <returns>语言详情</returns>
        Task<HbtLanguageDto> GetAsync(long LangId);

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        Task<long> InsertAsync(HbtLanguageCreateDto input);

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtLanguageUpdateDto input);

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="LangId">语言ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long LangId);

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="LangIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] LangIds);

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
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> ExportAsync(HbtLanguageQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtLanguageStatusDto input);
    }
} 