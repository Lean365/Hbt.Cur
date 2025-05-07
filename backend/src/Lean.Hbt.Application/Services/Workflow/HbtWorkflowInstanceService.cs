//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowInstanceService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例服务实现类
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流实例服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流实例的增删改查服务
    /// 2. 支持工作流实例的导入导出功能
    /// 3. 实现工作流实例的状态管理
    /// 4. 提供工作流实例的提交、撤回、终止等操作
    /// </remarks>
    public class HbtWorkflowInstanceService : HbtBaseService, IHbtWorkflowInstanceService
    {
        private readonly IHbtRepository<HbtWorkflowInstance> _instanceRepository;

        static HbtWorkflowInstanceService()
        {
            // 配置Mapster映射规则
            TypeAdapterConfig<HbtWorkflowInstance, HbtWorkflowInstanceDto>.NewConfig()
                .Map(dest => dest.Title, src => src.WorkflowTitle)
                .Map(dest => dest.CurrentNodeName, src => src.CurrentNode != null ? src.CurrentNode.NodeName : null);

            TypeAdapterConfig<HbtWorkflowInstanceDto, HbtWorkflowInstance>.NewConfig()
                .Map(dest => dest.WorkflowTitle, src => src.Title);

            TypeAdapterConfig<HbtWorkflowInstanceCreateDto, HbtWorkflowInstance>.NewConfig()
                .Map(dest => dest.WorkflowTitle, src => src.Title);

            TypeAdapterConfig<HbtWorkflowInstanceUpdateDto, HbtWorkflowInstance>.NewConfig()
                .Map(dest => dest.WorkflowTitle, src => src.Title);

            TypeAdapterConfig<HbtWorkflowInstance, HbtWorkflowInstanceExportDto>.NewConfig()
                .Map(dest => dest.Title, src => src.WorkflowTitle)
                .Map(dest => dest.CurrentNodeName, src => src.CurrentNode != null ? src.CurrentNode.NodeName : null)
                .Map(dest => dest.Status, src => src.Status.ToString());
        }

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="instanceRepository">工作流实例仓储接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowInstanceService(
            IHbtLogger logger,
            IHbtRepository<HbtWorkflowInstance> instanceRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _instanceRepository = instanceRepository;
        }

        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. WorkflowDefinitionId - 工作流定义ID
        /// 2. Title - 工作流标题（模糊查询）
        /// 3. InitiatorId - 发起人ID
        /// 4. Status - 实例状态
        /// 5. StartTime/EndTime - 时间范围
        /// 6. PageIndex - 页码
        /// 7. PageSize - 每页记录数</param>
        /// <returns>返回分页后的工作流实例列表</returns>
        public async Task<HbtPagedResult<HbtWorkflowInstanceDto>> GetListAsync(HbtWorkflowInstanceQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowInstance>();

            if (query?.WorkflowDefinitionId.HasValue == true)
                exp = exp.And(x => x.WorkflowDefinitionId == query.WorkflowDefinitionId.Value);

            if (!string.IsNullOrEmpty(query?.Title))
                exp = exp.And(x => x.WorkflowTitle.Contains(query.Title));

            if (query?.InitiatorId.HasValue == true)
                exp = exp.And(x => x.InitiatorId == query.InitiatorId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _instanceRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.Id, OrderByType.Asc);

            return new HbtPagedResult<HbtWorkflowInstanceDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtWorkflowInstanceDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流实例详情
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>工作流实例详情DTO</returns>
        /// <exception cref="HbtException">当工作流实例不存在时抛出异常</exception>
        public async Task<HbtWorkflowInstanceDto> GetByIdAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            return instance.Adapt<HbtWorkflowInstanceDto>();
        }

        /// <summary>
        /// 创建新的工作流实例
        /// </summary>
        /// <param name="input">工作流实例创建DTO，包含实例的基本信息</param>
        /// <returns>新创建的工作流实例ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流实例创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtWorkflowInstanceCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var instance = input.Adapt<HbtWorkflowInstance>();
            instance.Status = 0; // 0 表示草稿状态

            var result = await _instanceRepository.CreateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Create.Failed"));

            _logger.Info(L("WorkflowInstance.Created.Success", instance.Id));
            return instance.Id;
        }

        /// <summary>
        /// 更新工作流实例信息
        /// </summary>
        /// <param name="input">工作流实例更新DTO，包含需要更新的实例信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流实例不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtWorkflowInstanceUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var instance = await _instanceRepository.GetByIdAsync(input.WorkflowInstanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许更新
            if (instance.Status != 0) // 0 表示草稿状态
                throw new HbtException(L("WorkflowInstance.CannotUpdateNonDraft"));

            input.Adapt(instance);
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Update.Failed"));

            _logger.Info(L("WorkflowInstance.Updated.Success", instance.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流实例
        /// </summary>
        /// <param name="id">要删除的工作流实例ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当工作流实例不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许删除
            if (instance.Status != 0 && // 0 表示草稿状态
                instance.Status != 4) // 4 表示已终止状态
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            var result = await _instanceRepository.DeleteAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Delete.Failed"));

            _logger.Info(L("WorkflowInstance.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        /// <param name="ids">要删除的工作流实例ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            // 检查是否有活动状态的实例
            var activeInstances = await _instanceRepository.GetListAsync(x =>
                ids.Contains(x.Id) &&
                x.Status != 0 && // 0 表示草稿状态
                x.Status != 4); // 4 表示已终止状态

            if (activeInstances.Any())
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            var exp = Expressionable.Create<HbtWorkflowInstance>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await _instanceRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.BatchDelete.Failed"));

            _logger.Info(L("WorkflowInstance.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流实例数据
        /// </summary>
        /// <param name="instances">要导入的工作流实例列表</param>
        /// <returns>返回成功和失败的记录数</returns>
        /// <remarks>
        /// 导入过程中的异常会被记录但不会中断导入流程
        /// </remarks>
        public async Task<(int success, int fail)> ImportAsync(List<HbtWorkflowInstanceImportDto> instances)
        {
            if (instances == null || !instances.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var item in instances)
            {
                try
                {
                    var instance = item.Adapt<HbtWorkflowInstance>();
                    instance.Status = 0; // 0 表示草稿状态,导入时默认为草稿状态

                    var result = await _instanceRepository.CreateAsync(instance);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("WorkflowInstance.Import.Failed", item.Title), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出工作流实例数据
        /// </summary>
        /// <param name="query">导出查询条件</param>
        /// <returns>符合条件的工作流实例导出列表</returns>
        public async Task<List<HbtWorkflowInstanceExportDto>> ExportAsync(HbtWorkflowInstanceQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowInstance>();

            if (query?.WorkflowDefinitionId.HasValue == true)
                exp = exp.And(x => x.WorkflowDefinitionId == query.WorkflowDefinitionId.Value);

            if (!string.IsNullOrEmpty(query?.Title))
                exp = exp.And(x => x.WorkflowTitle.Contains(query.Title));

            if (query?.InitiatorId.HasValue == true)
                exp = exp.And(x => x.InitiatorId == query.InitiatorId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            var list = await _instanceRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtWorkflowInstanceExportDto>>();
        }

        /// <summary>
        /// 获取工作流实例导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        public Task<HbtWorkflowInstanceTemplateDto> GetTemplateAsync()
        {
            return Task.FromResult(new HbtWorkflowInstanceTemplateDto
            {
                Title = "示例工作流实例",
                Remark = "请填写备注信息"
            });
        }

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="HbtException">当工作流实例不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(HbtWorkflowInstanceStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var instance = await _instanceRepository.GetByIdAsync(input.WorkflowInstanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound", input.WorkflowInstanceId));

            instance.Status = input.Status;
            var result = await _instanceRepository.UpdateAsync(instance);

            return result > 0;
        }

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>提交是否成功</returns>
        /// <exception cref="HbtException">当工作流实例不存在或提交失败时抛出异常</exception>
        public async Task<bool> SubmitAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许提交
            if (instance.Status != 0) // 0 表示草稿状态
                throw new HbtException(L("WorkflowInstance.CannotSubmitNonDraft"));

            instance.Status = 1; // 1 表示运行中状态
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Submit.Failed"));

            _logger.Info(L("WorkflowInstance.Submitted.Success", id));
            return true;
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>撤回是否成功</returns>
        /// <exception cref="HbtException">当工作流实例不存在或撤回失败时抛出异常</exception>
        public async Task<bool> WithdrawAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许撤回
            if (instance.Status != 1) // 1 表示运行中状态
                throw new HbtException(L("WorkflowInstance.CannotWithdrawNonRunning"));

            instance.Status = 0; // 0 表示草稿状态
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Withdraw.Failed"));

            _logger.Info(L("WorkflowInstance.Withdrawn.Success", id));
            return true;
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>终止是否成功</returns>
        /// <exception cref="HbtException">当工作流实例不存在或终止失败时抛出异常</exception>
        public async Task<bool> TerminateAsync(long id, string reason)
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许终止
            if (instance.Status != 1 && // 1 表示运行中状态
                instance.Status != 2) // 2 表示已暂停状态
                throw new HbtException(L("WorkflowInstance.CannotTerminate"));

            instance.Status = 4; // 4 表示已终止状态
            instance.Remark = reason;

            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Terminate.Failed"));

            _logger.Info(L("WorkflowInstance.Terminated.Success", id));
            return true;
        }
    }
}