#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTransitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using Mapster;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流转换服务实现
    /// </summary>
    public class HbtWorkflowTransitionService : IHbtWorkflowTransitionService
    {
        private readonly IHbtRepository<HbtWorkflowTransition> _transitionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transitionRepository">工作流转换仓储</param>
        public HbtWorkflowTransitionService(IHbtRepository<HbtWorkflowTransition> transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        /// <inheritdoc/>
        public async Task<List<HbtWorkflowTransitionDto>> GetListAsync(long workflowDefinitionId)
        {
            var transitions = await _transitionRepository.GetListAsync(x => x.WorkflowDefinitionId == workflowDefinitionId);
            return transitions.Adapt<List<HbtWorkflowTransitionDto>>();
        }

        /// <inheritdoc/>
        public async Task<HbtWorkflowTransitionDto> GetByIdAsync(long id)
        {
            var transition = await _transitionRepository.GetByIdAsync(id);
            return transition.Adapt<HbtWorkflowTransitionDto>();
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(HbtWorkflowTransitionDto input)
        {
            var transition = input.Adapt<HbtWorkflowTransition>();
            await _transitionRepository.CreateAsync(transition);
            return transition.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(long id, HbtWorkflowTransitionDto input)
        {
            var transition = await _transitionRepository.GetByIdAsync(id);
            input.Adapt(transition);
            await _transitionRepository.UpdateAsync(transition);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(long id)
        {
            await _transitionRepository.DeleteAsync(id);
        }
    }
}
