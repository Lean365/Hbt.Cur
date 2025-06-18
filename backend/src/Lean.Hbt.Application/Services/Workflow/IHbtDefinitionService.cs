//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtDefinitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义服务接口
//===================================================================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定义服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtDefinitionService
    {
        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtDefinitionDto>> GetListAsync(HbtDefinitionQueryDto query);

        /// <summary>
        /// 获取工作流定义详情
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>工作流定义详情</returns>
        Task<HbtDefinitionDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流定义
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流定义ID</returns>
        Task<long> CreateAsync(HbtDefinitionCreateDto input);

        /// <summary>
        /// 更新工作流定义
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtDefinitionUpdateDto input);

        /// <summary>
        /// 删除工作流定义
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        /// <param name="ids">工作流定义ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流任务数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtDefinitionQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtDefinitionStatusDto input);

        /// <summary>
        /// 升级工作流定义版本
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>升级后的版本号</returns>
        Task<string> UpgradeVersionAsync(long id);
    }
} 