//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtWorkflowInstanceService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流实例服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtWorkflowInstanceService
    {
        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtWorkflowInstanceDto>> GetListAsync(HbtWorkflowInstanceQueryDto query);

        /// <summary>
        /// 获取工作流实例详情
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>工作流实例详情</returns>
        Task<HbtWorkflowInstanceDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流实例
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流实例ID</returns>
        Task<long> CreateAsync(HbtWorkflowInstanceCreateDto input);

        /// <summary>
        /// 更新工作流实例
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtWorkflowInstanceUpdateDto input);

        /// <summary>
        /// 删除工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        /// <param name="ids">工作流实例ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流实例
        /// </summary>
        /// <param name="instances">工作流实例导入列表</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(List<HbtWorkflowInstanceImportDto> instances);

        /// <summary>
        /// 导出工作流实例
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        Task<List<HbtWorkflowInstanceExportDto>> ExportAsync(HbtWorkflowInstanceQueryDto query);

        /// <summary>
        /// 获取工作流实例导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        Task<HbtWorkflowInstanceTemplateDto> GetTemplateAsync();

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtWorkflowInstanceStatusDto input);

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> SubmitAsync(long id);

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> WithdrawAsync(long id);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>是否成功</returns>
        Task<bool> TerminateAsync(long id, string reason);
    }
} 