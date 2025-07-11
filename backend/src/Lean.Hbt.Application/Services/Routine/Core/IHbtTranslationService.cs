//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTranslationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using System.IO;
using Lean.Hbt.Application.Dtos.Routine.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 翻译服务接口
    /// </summary>
    public interface IHbtTranslationService
    {
        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        Task<HbtPagedResult<HbtTranslationDto>> GetListAsync(HbtTranslationQueryDto query);

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>翻译详情</returns>
        Task<HbtTranslationDto> GetByIdAsync(long TranslationId);

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        Task<long> CreateAsync(HbtTranslationCreateDto input);

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtTranslationUpdateDto input);

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long TranslationId);

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="TranslationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] TranslationIds);

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "翻译信息");

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtTranslationQueryDto query, string sheetName = "翻译信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "翻译信息");

        /// <summary>
        /// 更新翻译状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtTranslationStatusDto input);

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        Task<string> GetTransValueAsync(string langCode, string transKey);

        /// <summary>
        /// 获取指定模块的翻译列表
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>翻译列表</returns>
        Task<List<HbtTranslationDto>> GetListByModuleAsync(string langCode, string moduleName);

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据,包含分页信息</returns>
        Task<HbtPagedResult<HbtTransposedDto>> GetTransposedDataAsync(HbtTransposedQueryDto query);

        /// <summary>
        /// 获取转置后的翻译详情
        /// </summary>
        /// <param name="transKey">翻译键</param>
        /// <returns>转置后的翻译详情</returns>
        Task<HbtTransposedDto> GetTransposedDetailAsync(string transKey);

        /// <summary>
        /// 创建转置翻译数据
        /// </summary>
        /// <param name="input">转置数据创建对象</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateTransposedAsync(HbtTransposedDto input);

        /// <summary>
        /// 更新转置翻译数据
        /// </summary>
        /// <param name="input">转置数据更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateTransposedAsync(HbtTransposedDto input);
    }
} 