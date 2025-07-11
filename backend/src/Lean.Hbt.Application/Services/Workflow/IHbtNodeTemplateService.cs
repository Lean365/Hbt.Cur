//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNodeTemplateService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 节点模板服务接口
//===================================================================
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 节点模板服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtNodeTemplateService
    {
        /// <summary>
        /// 获取节点模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtNodeTemplateDto>> GetListAsync(HbtNodeTemplateQueryDto query);

        /// <summary>
        /// 获取节点模板详情
        /// </summary>
        /// <param name="id">节点模板ID</param>
        /// <returns>节点模板详情</returns>
        Task<HbtNodeTemplateDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建节点模板
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的节点模板ID</returns>
        Task<long> CreateAsync(HbtNodeTemplateCreateDto input);

        /// <summary>
        /// 更新节点模板
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtNodeTemplateUpdateDto input);

        /// <summary>
        /// 删除节点模板
        /// </summary>
        /// <param name="id">节点模板ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除节点模板
        /// </summary>
        /// <param name="ids">节点模板ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入节点模板数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出节点模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtNodeTemplateQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取节点模板导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");
    }
} 