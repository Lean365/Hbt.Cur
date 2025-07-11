#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTransitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流转换服务实现
    /// </summary>
    /// <remarks>
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtTransitionService : HbtBaseService, IHbtTransitionService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtTransition> TransitionRepository => _repositoryFactory.GetWorkflowRepository<HbtTransition>();
        private IHbtRepository<HbtActivity> ActivityRepository => _repositoryFactory.GetWorkflowRepository<HbtActivity>();
        private IHbtRepository<HbtDefinition> DefinitionRepository => _repositoryFactory.GetWorkflowRepository<HbtDefinition>();
        private IHbtRepository<HbtParallelBranch> ParallelBranchRepository => _repositoryFactory.GetWorkflowRepository<HbtParallelBranch>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtTransitionService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <inheritdoc/>
        public async Task<List<HbtTransitionDto>> GetListAsync(long DefinitionId)
        {
            var transitions = await TransitionRepository.GetListAsync(x => x.DefinitionId == DefinitionId);
            var transitionDtos = transitions.Adapt<List<HbtTransitionDto>>();

            // 获取所有相关的ID
            var sourceActivityIds = transitionDtos.Select(t => t.SourceActivityId).Distinct().ToList();
            var targetActivityIds = transitionDtos.Select(t => t.TargetActivityId).Distinct().ToList();

            // 批量查询关联数据
            var sourceActivities = await ActivityRepository.GetListAsync(x => sourceActivityIds.Contains(x.Id));
            var targetActivities = await ActivityRepository.GetListAsync(x => targetActivityIds.Contains(x.Id));
            var definition = await DefinitionRepository.GetByIdAsync(DefinitionId);

            // 填充名称信息和关联对象
            foreach (var transition in transitionDtos)
            {
                // 填充源活动名称
                var sourceActivity = sourceActivities.FirstOrDefault(a => a.Id == transition.SourceActivityId);
                transition.SourceActivityName = sourceActivity?.Name ?? $"活动{transition.SourceActivityId}";
                transition.SourceActivity = sourceActivity?.Adapt<HbtActivityDto>();

                // 填充目标活动名称
                var targetActivity = targetActivities.FirstOrDefault(a => a.Id == transition.TargetActivityId);
                transition.TargetActivityName = targetActivity?.Name ?? $"活动{transition.TargetActivityId}";
                transition.TargetActivity = targetActivity?.Adapt<HbtActivityDto>();

                // 填充定义名称和关联对象
                transition.DefinitionName = definition?.WorkflowName ?? $"定义{DefinitionId}";
                transition.WorkflowDefinition = definition?.Adapt<HbtDefinitionDto>();

                // 获取并行分支
                var parallelBranches = await ParallelBranchRepository.GetListAsync(x => x.BranchTransitionId == transition.TransitionId);
                transition.ParallelBranches = parallelBranches.Adapt<List<HbtParallelBranchDto>>();
            }

            return transitionDtos;
        }

        /// <inheritdoc/>
        public async Task<HbtTransitionDto> GetByIdAsync(long id)
        {
            var transition = await TransitionRepository.GetByIdAsync(id);
            var transitionDto = transition.Adapt<HbtTransitionDto>();

            // 获取关联数据
            var sourceActivity = await ActivityRepository.GetByIdAsync(transition.SourceActivityId);
            var targetActivity = await ActivityRepository.GetByIdAsync(transition.TargetActivityId);
            var definition = await DefinitionRepository.GetByIdAsync(transition.DefinitionId);

            // 填充名称信息
            transitionDto.SourceActivityName = sourceActivity?.Name ?? $"活动{transition.SourceActivityId}";
            transitionDto.TargetActivityName = targetActivity?.Name ?? $"活动{transition.TargetActivityId}";
            transitionDto.DefinitionName = definition?.WorkflowName ?? $"定义{transition.DefinitionId}";

            // 填充关联对象
            transitionDto.SourceActivity = sourceActivity?.Adapt<HbtActivityDto>();
            transitionDto.TargetActivity = targetActivity?.Adapt<HbtActivityDto>();
            transitionDto.WorkflowDefinition = definition?.Adapt<HbtDefinitionDto>();

            // 获取并行分支
            var parallelBranches = await ParallelBranchRepository.GetListAsync(x => x.BranchTransitionId == transition.Id);
            transitionDto.ParallelBranches = parallelBranches.Adapt<List<HbtParallelBranchDto>>();

            return transitionDto;
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(HbtTransitionDto input)
        {
            var transition = input.Adapt<HbtTransition>();
            await TransitionRepository.CreateAsync(transition);
            return transition.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(long id, HbtTransitionDto input)
        {
            var transition = await TransitionRepository.GetByIdAsync(id);
            input.Adapt(transition);
            await TransitionRepository.UpdateAsync(transition);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(long id)
        {
            await TransitionRepository.DeleteAsync(id);
        }

        /// <inheritdoc/>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importTransitions = await HbtExcelHelper.ImportAsync<HbtTransitionImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importTransitions)
                {
                    try
                    {
                        var transition = item.Adapt<HbtTransition>();
                        transition.CreateTime = DateTime.Now;
                        transition.CreateBy = _currentUser.UserName;

                        var result = await TransitionRepository.CreateAsync(transition);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流转换失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流转换数据失败", ex);
                throw new HbtException("导入工作流转换数据失败");
            }
        }

        /// <inheritdoc/>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtTransitionQueryDto query, string sheetName = "Sheet1")
        {
            var exp = Expressionable.Create<HbtTransition>();
            if (query != null && query.DefinitionId > 0)
                exp = exp.And(x => x.DefinitionId == query.DefinitionId);

            var list = await TransitionRepository.GetListAsync(exp.ToExpression());
            var exportList = list.Adapt<List<HbtTransitionExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流转换数据");
        }

        /// <inheritdoc/>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtTransitionImportDto>(sheetName);
        }
    }
}
