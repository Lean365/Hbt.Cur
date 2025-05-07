//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务服务实现类
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流任务服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流任务的增删改查服务
    /// 2. 支持工作流任务的导入导出功能
    /// 3. 实现工作流任务的状态管理
    /// 4. 提供任务的完成、转办、退回、撤销等操作
    /// </remarks>
    public class HbtWorkflowTaskService : HbtBaseService, IHbtWorkflowTaskService
    {
        private readonly IHbtRepository<HbtWorkflowTask> _taskRepository;

        static HbtWorkflowTaskService()
        {
            // 配置Mapster映射规则
            TypeAdapterConfig<HbtWorkflowTask, HbtWorkflowTaskDto>.NewConfig()
                .Map(dest => dest.AssigneeName, src => src.Node != null ? src.Node.NodeName : null)
                .Map(dest => dest.AssigneeId, src => src.AssigneeId)
                .Map(dest => dest.TaskResult, src => src.Result)
                .Map(dest => dest.Comment, src => src.Comment);

            TypeAdapterConfig<HbtWorkflowTask, HbtWorkflowTaskExportDto>.NewConfig()
                .Map(dest => dest.AssigneeName, src => src.Node != null ? src.Node.NodeName : null)
                .Map(dest => dest.TaskTypeName, src => src.TaskType.ToString())
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.Result, src => src.Result)
                .Map(dest => dest.Comment, src => src.Comment);

            TypeAdapterConfig<HbtWorkflowTaskCreateDto, HbtWorkflowTask>.NewConfig()
                .Map(dest => dest.AssigneeId, src => src.AssigneeId)
                .Map(dest => dest.NodeId, src => src.WorkflowNodeId);

            TypeAdapterConfig<HbtWorkflowTaskUpdateDto, HbtWorkflowTask>.NewConfig()
                .Map(dest => dest.Result, src => src.Result)
                .Map(dest => dest.Comment, src => src.Comment);
        }

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="taskRepository">工作流任务仓储接口</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowTaskService(
            IHbtRepository<HbtWorkflowTask> taskRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. WorkflowInstanceId - 工作流实例ID
        /// 2. WorkflowNodeId - 工作流节点ID
        /// 3. TaskType - 任务类型
        /// 4. AssigneeId - 处理人ID
        /// 5. Status - 任务状态
        /// 6. StartTime/EndTime - 时间范围
        /// 7. PageIndex - 页码
        /// 8. PageSize - 每页记录数</param>
        /// <returns>返回分页后的工作流任务列表</returns>
        public async Task<HbtPagedResult<HbtWorkflowTaskDto>> GetListAsync(HbtWorkflowTaskQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowTask>();

            if (query?.WorkflowInstanceId.HasValue == true)
                exp = exp.And(x => x.WorkflowInstanceId == query.WorkflowInstanceId.Value);

            if (query?.WorkflowNodeId.HasValue == true)
                exp = exp.And(x => x.NodeId == query.WorkflowNodeId.Value);

            if (query?.TaskType.HasValue == true)
                exp = exp.And(x => x.TaskType == (int)query.TaskType.Value);

            if (query?.AssigneeId.HasValue == true)
                exp = exp.And(x => x.AssigneeId == query.AssigneeId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == (int)query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            var pageIndex = query?.PageIndex ?? 1;
            var pageSize = query?.PageSize ?? 10;

            var result = await _taskRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtWorkflowTaskDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtWorkflowTaskDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流任务详情
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <returns>工作流任务详情DTO</returns>
        /// <exception cref="HbtException">当工作流任务不存在时抛出异常</exception>
        public async Task<HbtWorkflowTaskDto> GetByIdAsync(long id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            return task.Adapt<HbtWorkflowTaskDto>();
        }

        /// <summary>
        /// 创建新的工作流任务
        /// </summary>
        /// <param name="input">工作流任务创建DTO，包含任务的基本信息</param>
        /// <returns>新创建的工作流任务ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流任务创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtWorkflowTaskCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var task = input.Adapt<HbtWorkflowTask>();
            task.Status = 0; // 0 表示待处理状态
            task.CreateTime = DateTime.Now;

            var result = await _taskRepository.CreateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Create.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Created.Success", task.Id));
            return task.Id;
        }

        /// <summary>
        /// 更新工作流任务信息
        /// </summary>
        /// <param name="input">工作流任务更新DTO，包含需要更新的任务信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流任务不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtWorkflowTaskUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var task = await _taskRepository.GetByIdAsync(input.WorkflowTaskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许更新
            if (task.Status != 0) // 0 表示待处理状态
                throw new HbtException(_localization.L("WorkflowTask.CannotUpdateNonPending"));

            input.Adapt(task);
            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Update.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Updated.Success", task.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流任务
        /// </summary>
        /// <param name="id">要删除的工作流任务ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许删除
            if (task.Status != 0) // 0 表示待处理状态
                throw new HbtException(_localization.L("WorkflowTask.CannotDeleteNonPending"));

            var result = await _taskRepository.DeleteAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Delete.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流任务
        /// </summary>
        /// <param name="ids">要删除的工作流任务ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            // 检查是否有非待处理状态的任务
            var activeTasks = await _taskRepository.GetListAsync(x =>
                ids.Contains(x.Id) &&
                x.Status != 0); // 0 表示待处理状态

            if (activeTasks.Any())
                throw new HbtException(_localization.L("WorkflowTask.CannotDeleteNonPending"));

            var exp = Expressionable.Create<HbtWorkflowTask>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await _taskRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.BatchDelete.Failed"));

            _logger.Info(_localization.L("WorkflowTask.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流任务数据
        /// </summary>
        /// <param name="tasks">要导入的工作流任务列表</param>
        /// <returns>返回成功和失败的记录数</returns>
        /// <remarks>
        /// 导入过程中的异常会被记录但不会中断导入流程
        /// </remarks>
        public async Task<(int success, int fail)> ImportAsync(List<HbtWorkflowTaskImportDto> tasks)
        {
            if (tasks == null || !tasks.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var item in tasks)
            {
                try
                {
                    var task = item.Adapt<HbtWorkflowTask>();
                    task.Status = 0; // 0 表示待处理状态,导入时默认为待处理状态
                    task.CreateTime = DateTime.Now;

                    var result = await _taskRepository.CreateAsync(task);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Error(_localization.L("WorkflowTask.Import.Failed", item.TaskType), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        /// <param name="query">导出查询条件</param>
        /// <returns>符合条件的工作流任务导出列表</returns>
        public async Task<List<HbtWorkflowTaskExportDto>> ExportAsync(HbtWorkflowTaskQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowTask>();

            if (query?.WorkflowInstanceId.HasValue == true)
                exp = exp.And(x => x.WorkflowInstanceId == query.WorkflowInstanceId.Value);

            if (query?.WorkflowNodeId.HasValue == true)
                exp = exp.And(x => x.NodeId == query.WorkflowNodeId.Value);

            if (query?.TaskType.HasValue == true)
                exp = exp.And(x => x.TaskType == query.TaskType.Value);

            if (query?.AssigneeId.HasValue == true)
                exp = exp.And(x => x.AssigneeId == query.AssigneeId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            var list = await _taskRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtWorkflowTaskExportDto>>();
        }

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        public Task<HbtWorkflowTaskTemplateDto> GetTemplateAsync()
        {
            return Task.FromResult(new HbtWorkflowTaskTemplateDto
            {
                TaskType = "审批任务",
                Remark = "请填写备注信息"
            });
        }

        /// <summary>
        /// 更新工作流任务状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(long taskId, int status)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound", taskId));

            task.Status = status;
            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.UpdateStatus.Failed"));

            _logger.Info(_localization.L("WorkflowTask.UpdatedStatus.Success", task.Id));
            return true;
        }

        /// <summary>
        /// 完成工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="result">处理结果</param>
        /// <param name="comment">处理意见</param>
        /// <returns>完成是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或完成失败时抛出异常</exception>
        public async Task<bool> CompleteAsync(long id, string result, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许完成
            if (task.Status != 0) // 0 表示待处理状态
                throw new HbtException(_localization.L("WorkflowTask.CannotCompleteNonPending"));

            task.Status = 3; // 3 表示已完成状态
            task.Result = result;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;

            var updateResult = await _taskRepository.UpdateAsync(task);
            if (updateResult <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Complete.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Completed.Success", id));
            return true;
        }

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="assigneeId">新处理人ID</param>
        /// <param name="comment">转办说明</param>
        /// <returns>转办是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或转办失败时抛出异常</exception>
        public async Task<bool> TransferAsync(long id, long assigneeId, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许转办
            if (task.Status != 1) // 1 表示处理中状态
                throw new HbtException(_localization.L("WorkflowTask.CannotTransferNonProcessing"));

            task.Status = 3; // 3 表示已转办状态
            task.AssigneeId = assigneeId;
            task.Comment = comment;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Transfer.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Transferred.Success", id));
            return true;
        }

        /// <summary>
        /// 退回工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">退回说明</param>
        /// <returns>退回是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或退回失败时抛出异常</exception>
        public async Task<bool> RejectAsync(long id, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            if (task.Status != 1) // 1 表示处理中状态
                throw new HbtException(_localization.L("WorkflowTask.CannotRejectNonProcessing"));

            task.Status = 2; // 2 表示已完成状态
            task.Result = 1.ToString(); // 1 表示已拒绝
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Reject.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Rejected.Success", id));
            return true;
        }

        /// <summary>
        /// 撤销工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">撤销说明</param>
        /// <returns>撤销是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或撤销失败时抛出异常</exception>
        public async Task<bool> CancelAsync(long id, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许撤销
            if (task.Status != 1) // 1 表示处理中状态
                throw new HbtException(_localization.L("WorkflowTask.CannotCancelNonProcessing"));

            task.Status = 2; // 2 表示已完成状态
            task.Result = null; // 撤销时不设置处理结果
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Cancel.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Cancelled.Success", id));
            return true;
        }

        /// <summary>
        /// 审批通过工作流任务
        /// </summary>
        /// <param name="taskId">工作流任务ID</param>
        /// <param name="comment">审批意见</param>
        /// <returns>审批是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或审批失败时抛出异常</exception>
        public async Task<bool> ApproveTaskAsync(long taskId, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            if (task.Status != 1) // 1 表示处理中状态
                throw new HbtException(_localization.L("WorkflowTask.CannotApproveNonProcessing"));

            task.Status = 2;
            task.Result = 1.ToString();
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Approve.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Approved.Success", taskId));
            return true;
        }

        /// <summary>
        /// 驳回工作流任务
        /// </summary>
        /// <param name="taskId">工作流任务ID</param>
        /// <param name="comment">驳回意见</param>
        /// <returns>驳回是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或驳回失败时抛出异常</exception>
        public async Task<bool> RejectTaskAsync(long taskId, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            if (task.Status != 1) // 1 表示处理中状态
                throw new HbtException(_localization.L("WorkflowTask.CannotRejectNonProcessing"));

            task.Status = 2;
            task.Result = 0.ToString();
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Reject.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Rejected.Success", taskId));
            return true;
        }

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        /// <param name="taskId">工作流任务ID</param>
        /// <param name="assigneeId">新处理人ID</param>
        /// <param name="comment">转办说明</param>
        /// <returns>转办是否成功</returns>
        /// <exception cref="HbtException">当工作流任务不存在或转办失败时抛出异常</exception>
        public async Task<bool> TransferTaskAsync(long taskId, long assigneeId, string comment)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            if (task.Status != 1)
                throw new HbtException(_localization.L("WorkflowTask.CannotTransferNonProcessing"));

            task.Status = 3;
            task.Result = 2.ToString();
            task.AssigneeId = assigneeId;
            task.Comment = comment;

            var result = await _taskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Transfer.Failed"));

            _logger.Info(_localization.L("WorkflowTask.Transferred.Success", taskId));
            return true;
        }

        /// <summary>
        /// 获取指定处理人的工作流任务列表
        /// </summary>
        /// <param name="assigneeId">处理人ID</param>
        /// <returns>工作流任务列表</returns>
        public async Task<List<HbtWorkflowTaskDto>> GetTasksByAssigneeAsync(long assigneeId)
        {
            var tasks = await _taskRepository.GetListAsync(x => x.AssigneeId == assigneeId);
            return tasks.Adapt<List<HbtWorkflowTaskDto>>();
        }
    }
}