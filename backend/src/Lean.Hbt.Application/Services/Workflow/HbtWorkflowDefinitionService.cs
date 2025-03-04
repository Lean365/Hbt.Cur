//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDefinitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义服务实现类
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定义服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流定义的增删改查服务
    /// 2. 支持工作流定义的导入导出功能
    /// 3. 实现工作流定义的版本管理
    /// 4. 提供工作流定义的启用/禁用功能
    /// </remarks>
    public class HbtWorkflowDefinitionService : IHbtWorkflowDefinitionService
    {
        private readonly IHbtRepository<HbtWorkflowDefinition> _definitionRepository;
        private readonly IHbtLogger _logger;
        private readonly IHbtLocalizationService _localization;

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="definitionRepository">工作流定义仓储接口</param>
        /// <param name="logger">日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowDefinitionService(
            IHbtRepository<HbtWorkflowDefinition> definitionRepository,
            IHbtLogger logger,
            IHbtLocalizationService localization)
        {
            _definitionRepository = definitionRepository;
            _logger = logger;
            _localization = localization;
        }

        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. WorkflowName - 工作流定义名称（模糊查询）
        /// 2. WorkflowCategory - 工作流定义分类
        /// 3. WorkflowStatus - 工作流定义状态
        /// 4. PageIndex - 页码
        /// 5. PageSize - 每页记录数</param>
        /// <returns>返回分页后的工作流定义列表</returns>
        public async Task<HbtPagedResult<HbtWorkflowDefinitionDto>> GetPagedListAsync(HbtWorkflowDefinitionQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowDefinition>();

            if (!string.IsNullOrEmpty(query?.WorkflowName))
                exp = exp.And(x => x.WorkflowName.Contains(query.WorkflowName));

            if (!string.IsNullOrEmpty(query?.WorkflowCategory))
                exp = exp.And(x => x.WorkflowCategory == query.WorkflowCategory);

            if (query?.WorkflowStatus.HasValue == true)
                exp = exp.And(x => x.Status == query.WorkflowStatus.Value);

            var result = await _definitionRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtWorkflowDefinitionDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtWorkflowDefinitionDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流定义详情
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>工作流定义详情DTO</returns>
        /// <exception cref="HbtException">当工作流定义不存在时抛出异常</exception>
        public async Task<HbtWorkflowDefinitionDto> GetAsync(long id)
        {
            var definition = await _definitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(_localization.L("WorkflowDefinition.NotFound"));

            return definition.Adapt<HbtWorkflowDefinitionDto>();
        }

        /// <summary>
        /// 创建新的工作流定义
        /// </summary>
        /// <param name="input">工作流定义创建DTO，包含定义的基本信息</param>
        /// <returns>新创建的工作流定义ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流定义创建失败时抛出异常</exception>
        public async Task<long> InsertAsync(HbtWorkflowDefinitionCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // 检查名称是否已存在
            var exists = await _definitionRepository.FirstOrDefaultAsync(x => x.WorkflowName == input.WorkflowName);
            if (exists != null)
                throw new HbtException(_localization.L("WorkflowDefinition.NameExists"));

            var definition = input.Adapt<HbtWorkflowDefinition>();
            definition.WorkflowVersion = 1; // 新建定义默认版本为1
            definition.Status = Common.Enums.HbtWorkflowStatus.Draft; // 新建定义默认为草稿状态

            var result = await _definitionRepository.InsertAsync(definition);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowDefinition.Create.Failed"));

            _logger.Info(_localization.L("WorkflowDefinition.Created.Success", definition.Id));
            return definition.Id;
        }

        /// <summary>
        /// 更新工作流定义信息
        /// </summary>
        /// <param name="input">工作流定义更新DTO，包含需要更新的定义信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流定义不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtWorkflowDefinitionUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var definition = await _definitionRepository.GetByIdAsync(input.WorkflowActivityId);
            if (definition == null)
                throw new HbtException(_localization.L("WorkflowDefinition.NotFound"));

            // 检查名称是否已被其他定义使用
            var exists = await _definitionRepository.FirstOrDefaultAsync(x => x.WorkflowName == input.WorkflowName && x.Id != input.WorkflowActivityId);
            if (exists != null)
                throw new HbtException(_localization.L("WorkflowDefinition.NameExists"));

            input.Adapt(definition);
            var result = await _definitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowDefinition.Update.Failed"));

            _logger.Info(_localization.L("WorkflowDefinition.Updated.Success", definition.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流定义
        /// </summary>
        /// <param name="id">要删除的工作流定义ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当工作流定义不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var definition = await _definitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(_localization.L("WorkflowDefinition.NotFound"));

            // 检查工作流定义是否处于活动状态
            if (definition.Status == Common.Enums.HbtWorkflowStatus.Published)
                throw new HbtException(_localization.L("WorkflowDefinition.CannotDeleteActive"));

            var result = await _definitionRepository.DeleteAsync(definition);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowDefinition.Delete.Failed"));

            _logger.Info(_localization.L("WorkflowDefinition.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        /// <param name="ids">要删除的工作流定义ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            // 检查是否有活动状态的定义
            var activeDefinitions = await _definitionRepository.GetListAsync(x => ids.Contains(x.Id) && x.Status == Common.Enums.HbtWorkflowStatus.Published);
            if (activeDefinitions.Any())
                throw new HbtException(_localization.L("WorkflowDefinition.CannotDeleteActive"));

            var exp = Expressionable.Create<HbtWorkflowDefinition>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await _definitionRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowDefinition.BatchDelete.Failed"));

            _logger.Info(_localization.L("WorkflowDefinition.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流定义
        /// </summary>
        public async Task<List<HbtWorkflowDefinitionDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importDefinitions = await HbtExcelHelper.ImportAsync<HbtWorkflowDefinitionImportDto>(fileStream, sheetName);
                var result = new List<HbtWorkflowDefinitionDto>();

                foreach (var item in importDefinitions)
                {
                    try
                    {
                        var definition = item.Adapt<HbtWorkflowDefinition>();
                        var insertResult = await _definitionRepository.InsertAsync(definition);
                        if (insertResult > 0)
                        {
                            result.Add(definition.Adapt<HbtWorkflowDefinitionDto>());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(_localization.L("WorkflowDefinition.Import.Failed"), ex);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowDefinition.Import.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowDefinition.Import.Failed"), ex);
            }
        }

        /// <summary>
        /// 导出工作流定义
        /// </summary>
        public async Task<byte[]> ExportAsync(IEnumerable<HbtWorkflowDefinitionDto> data, string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.ExportAsync(data, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowDefinition.Export.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowDefinition.Export.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取工作流定义导入模板
        /// </summary>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.GenerateTemplateAsync<HbtWorkflowDefinitionImportDto>(sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowDefinition.Template.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowDefinition.Template.Failed"), ex);
            }
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="HbtException">当工作流定义不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(HbtWorkflowDefinitionStatusDto input)
        {
            var definition = await _definitionRepository.GetByIdAsync(input.WorkflowDefinitionId);
            if (definition == null)
                throw new HbtException(_localization.L("WorkflowDefinition.NotFound"));

            definition.Status = input.Status;
            var result = await _definitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowDefinition.UpdateStatus.Failed"));

            _logger.Info(_localization.L("WorkflowDefinition.UpdatedStatus.Success", definition.Id));
            return true;
        }
    }
}