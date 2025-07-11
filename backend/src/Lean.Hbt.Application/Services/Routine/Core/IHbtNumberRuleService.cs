//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNumberRuleService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 单号规则服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 单号规则服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtNumberRuleService
    {
        /// <summary>
        /// 获取单号规则分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>单号规则分页列表</returns>
        Task<HbtPagedResult<HbtNumberRuleDto>> GetListAsync(HbtNumberRuleQueryDto query);

        /// <summary>
        /// 获取单号规则详情
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>单号规则详情</returns>
        Task<HbtNumberRuleDto> GetByIdAsync(long numberRuleId);

        /// <summary>
        /// 创建单号规则
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>单号规则ID</returns>
        Task<long> CreateAsync(HbtNumberRuleCreateDto input);

        /// <summary>
        /// 更新单号规则
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtNumberRuleUpdateDto input);

        /// <summary>
        /// 删除单号规则
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long numberRuleId);

        /// <summary>
        /// 批量删除单号规则
        /// </summary>
        /// <param name="numberRuleIds">单号规则ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] numberRuleIds);

        /// <summary>
        /// 导入单号规则数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "单号规则信息");

        /// <summary>
        /// 导出单号规则数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtNumberRuleQueryDto query, string sheetName = "单号规则信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "单号规则信息");


    }
} 