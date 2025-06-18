#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTransitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换服务实现
//===================================================================

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Common.Utils;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流转换服务实现
    /// </summary>
    public class HbtTransitionService : HbtBaseService, IHbtTransitionService
    {
        private readonly IHbtRepository<HbtTransition> _transitionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transitionRepository">工作流转换仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtTransitionService(
            IHbtRepository<HbtTransition> transitionRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _transitionRepository = transitionRepository;
        }

        /// <inheritdoc/>
        public async Task<List<HbtTransitionDto>> GetListAsync(long DefinitionId)
        {
            var transitions = await _transitionRepository.GetListAsync(x => x.DefinitionId == DefinitionId);
            return transitions.Adapt<List<HbtTransitionDto>>();
        }

        /// <inheritdoc/>
        public async Task<HbtTransitionDto> GetByIdAsync(long id)
        {
            var transition = await _transitionRepository.GetByIdAsync(id);
            return transition.Adapt<HbtTransitionDto>();
        }

        /// <inheritdoc/>
        public async Task<long> CreateAsync(HbtTransitionDto input)
        {
            var transition = input.Adapt<HbtTransition>();
            await _transitionRepository.CreateAsync(transition);
            return transition.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(long id, HbtTransitionDto input)
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
                        var insertResult = await _transitionRepository.CreateAsync(transition);
                        if (insertResult > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(_localization.L("WorkflowTransition.Import.Failed"), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowTransition.Import.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowTransition.Import.Failed"), ex);
            }
        }

        /// <inheritdoc/>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtTransitionQueryDto query, string sheetName = "Sheet1")
        {
            try
            {
                var exp = Expressionable.Create<HbtTransition>();
                if (query != null && query.DefinitionId > 0)
                    exp = exp.And(x => x.DefinitionId == query.DefinitionId);

                var list = await _transitionRepository.GetListAsync(exp.ToExpression());
                var exportList = list.Adapt<List<HbtTransitionExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流转换数据");
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowTransition.Export.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowTransition.Export.Failed"), ex);
            }
        }

        /// <inheritdoc/>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtTransitionImportDto>(sheetName);
        }
    }
}
