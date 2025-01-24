#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowActivityService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow.Engine;
using Lean.Hbt.Domain.Repositories;
using Mapster;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流活动服务实现
    /// </summary>
    public class HbtWorkflowActivityService : IHbtWorkflowActivityService
    {
        private readonly IHbtRepository<HbtWorkflowActivity> _activityRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activityRepository">工作流活动仓储</param>
        public HbtWorkflowActivityService(IHbtRepository<HbtWorkflowActivity> activityRepository)
        {
            _activityRepository = activityRepository;
        }

        /// <inheritdoc/>
        public async Task<List<HbtWorkflowActivityDto>> GetListAsync(long workflowDefinitionId)
        {
            var activities = await _activityRepository.GetListAsync(x => x.WorkflowDefinitionId == workflowDefinitionId);
            return activities.Adapt<List<HbtWorkflowActivityDto>>();
        }

        /// <inheritdoc/>
        public async Task<HbtWorkflowActivityDto> GetAsync(long id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            return activity.Adapt<HbtWorkflowActivityDto>();
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(HbtWorkflowActivityDto input)
        {
            var activity = input.Adapt<HbtWorkflowActivity>();
            await _activityRepository.InsertAsync(activity);
            return activity.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(long id, HbtWorkflowActivityDto input)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            input.Adapt(activity);
            await _activityRepository.UpdateAsync(activity);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(long id)
        {
            await _activityRepository.DeleteAsync(id);
        }
    }
} 
