#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtWorkflowTransitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流转换服务接口
    /// </summary>
    public interface IHbtWorkflowTransitionService
    {
        /// <summary>
        /// 获取工作流转换列表
        /// </summary>
        /// <param name="workflowDefinitionId">工作流定义ID</param>
        /// <returns>工作流转换列表</returns>
        Task<List<HbtWorkflowTransitionDto>> GetListAsync(long workflowDefinitionId);

        /// <summary>
        /// 获取工作流转换详情
        /// </summary>
        /// <param name="id">转换ID</param>
        /// <returns>工作流转换详情</returns>
        Task<HbtWorkflowTransitionDto> GetAsync(long id);

        /// <summary>
        /// 创建工作流转换
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>工作流转换ID</returns>
        Task<long> CreateAsync(HbtWorkflowTransitionDto input);

        /// <summary>
        /// 更新工作流转换
        /// </summary>
        /// <param name="id">转换ID</param>
        /// <param name="input">更新参数</param>
        Task UpdateAsync(long id, HbtWorkflowTransitionDto input);

        /// <summary>
        /// 删除工作流转换
        /// </summary>
        /// <param name="id">转换ID</param>
        Task DeleteAsync(long id);
    }
} 