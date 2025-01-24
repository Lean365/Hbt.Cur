#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtWorkflowActivityService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流活动服务接口
    /// </summary>
    public interface IHbtWorkflowActivityService
    {
        /// <summary>
        /// 获取工作流活动列表
        /// </summary>
        /// <param name="workflowDefinitionId">工作流定义ID</param>
        /// <returns>工作流活动列表</returns>
        Task<List<HbtWorkflowActivityDto>> GetListAsync(long workflowDefinitionId);

        /// <summary>
        /// 获取工作流活动详情
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <returns>工作流活动详情</returns>
        Task<HbtWorkflowActivityDto> GetAsync(long id);

        /// <summary>
        /// 创建工作流活动
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>工作流活动ID</returns>
        Task<long> CreateAsync(HbtWorkflowActivityDto input);

        /// <summary>
        /// 更新工作流活动
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="input">更新参数</param>
        Task UpdateAsync(long id, HbtWorkflowActivityDto input);

        /// <summary>
        /// 删除工作流活动
        /// </summary>
        /// <param name="id">活动ID</param>
        Task DeleteAsync(long id);
    }
} 