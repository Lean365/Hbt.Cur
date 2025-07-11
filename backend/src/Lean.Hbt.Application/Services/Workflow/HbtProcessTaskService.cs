//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务服务实现类 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Enums;

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
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtProcessTaskService : HbtBaseService, IHbtProcessTaskService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtProcessTask> TaskRepository => _repositoryFactory.GetWorkflowRepository<HbtProcessTask>();
        private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();
        private IHbtRepository<HbtNode> NodeRepository => _repositoryFactory.GetWorkflowRepository<HbtNode>();
        private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();
        private IHbtRepository<HbtHistory> HistoryRepository => _repositoryFactory.GetWorkflowRepository<HbtHistory>();

        // 统一的任务状态常量
        private const int TASK_STATUS_PENDING = 0;      // 待处理
        private const int TASK_STATUS_PROCESSING = 1;   // 处理中
        private const int TASK_STATUS_APPROVED = 2;     // 已同意
        private const int TASK_STATUS_REJECTED = 3;     // 已拒绝
        private const int TASK_STATUS_RETURNED = 4;     // 已退回
        private const int TASK_STATUS_TRANSFERRED = 5;  // 已转办
        private const int TASK_STATUS_CANCELLED = 6;    // 已取消

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtProcessTaskService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. InstanceId - 工作流实例ID
        /// 2. NodeId - 工作流节点ID
        /// 3. TaskType - 任务类型
        /// 4. AssigneeId - 处理人ID
        /// 5. Status - 任务状态
        /// 6. StartTime/EndTime - 时间范围
        /// 7. PageIndex - 页码
        /// 8. PageSize - 每页记录数</param>
        /// <returns>返回分页后的工作流任务列表</returns>
        public async Task<HbtPagedResult<HbtProcessTaskDto>> GetListAsync(HbtProcessTaskQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await TaskRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            // 获取任务列表
            var tasks = result.Rows.Adapt<List<HbtProcessTaskDto>>();

            // 获取所有相关的ID
            var instanceIds = tasks.Select(t => t.InstanceId).Distinct().ToList();
            var nodeIds = tasks.Select(t => t.NodeId).Distinct().ToList();
            var assigneeIds = tasks.Select(t => t.AssigneeId).Distinct().Where(id => id > 0).ToList();

            // 批量查询关联数据
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            var nodes = await NodeRepository.GetListAsync(x => nodeIds.Contains(x.Id));
            var users = await UserRepository.GetListAsync(x => assigneeIds.Contains(x.Id));

            // 填充名称信息
            foreach (var task in tasks)
            {
                // 填充实例名称
                var instance = instances.FirstOrDefault(i => i.Id == task.InstanceId);
                task.InstanceName = instance?.InstanceName ?? $"实例{task.InstanceId}";

                // 填充节点名称
                var node = nodes.FirstOrDefault(n => n.Id == task.NodeId);
                task.NodeName = node?.NodeTemplate?.NodeName ?? $"节点{task.NodeId}";

                // 填充处理人名称
                var user = users.FirstOrDefault(u => u.Id == task.AssigneeId);
                task.AssigneeName = user?.UserName ?? $"用户{task.AssigneeId}";
            }

            return new HbtPagedResult<HbtProcessTaskDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = tasks
            };
        }

        /// <summary>
        /// 根据ID获取工作流任务详情
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <returns>工作流任务详情DTO</returns>
        /// <exception cref="HbtException">当工作流任务不存在时抛出异常</exception>
        public async Task<HbtProcessTaskDto> GetByIdAsync(long id)
        {
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            var taskDto = task.Adapt<HbtProcessTaskDto>();

            // 获取关联数据
            var instance = await InstanceRepository.GetByIdAsync(task.InstanceId);
            var node = await NodeRepository.GetByIdAsync(task.NodeId);
            var user = await UserRepository.GetByIdAsync(task.AssigneeId);

            // 填充名称信息
            taskDto.InstanceName = instance?.InstanceName ?? $"实例{task.InstanceId}";
            taskDto.NodeName = node?.NodeTemplate?.NodeName ?? $"节点{task.NodeId}";
            taskDto.AssigneeName = user?.UserName ?? $"用户{task.AssigneeId}";

            // 填充关联对象
            taskDto.WorkflowInstance = instance?.Adapt<HbtInstanceDto>();
            taskDto.Node = node?.Adapt<HbtNodeDto>();

            return taskDto;
        }

        /// <summary>
        /// 创建新的工作流任务
        /// </summary>
        /// <param name="input">工作流任务创建DTO，包含任务的基本信息</param>
        /// <returns>新创建的工作流任务ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流任务创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtTaskCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // 验证字段是否存在
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, input.InstanceId.ToString(), nameof(HbtProcessTask.InstanceId));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, input.NodeId.ToString(), nameof(HbtProcessTask.NodeId));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, input.TaskType.ToString(), nameof(HbtProcessTask.TaskType));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, input.AssigneeId.ToString(), nameof(HbtProcessTask.AssigneeId));

            var task = input.Adapt<HbtProcessTask>();

            var result = await TaskRepository.CreateAsync(task);
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
        public async Task<bool> UpdateAsync(HbtProcessTaskUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var task = await TaskRepository.GetByIdAsync(input.ProcessTaskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许更新
            if (task.Status != 0) // 0 表示待处理状态
                throw new HbtException(_localization.L("WorkflowTask.CannotUpdateNonPending"));

            // 验证字段是否存在
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, task.InstanceId.ToString(), nameof(HbtProcessTask.InstanceId));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, task.NodeId.ToString(), nameof(HbtProcessTask.NodeId));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, task.TaskType.ToString(), nameof(HbtProcessTask.TaskType));
            await HbtValidateUtils.ValidateFieldExistsAsync<HbtProcessTask>(TaskRepository, task.AssigneeId.ToString(), nameof(HbtProcessTask.AssigneeId));

            input.Adapt(task);
            var result = await TaskRepository.UpdateAsync(task);
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
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态是否允许删除
            if (task.Status != 0) // 0 表示待处理状态
                throw new HbtException(_localization.L("WorkflowTask.CannotDeleteNonPending"));

            var result = await TaskRepository.DeleteAsync(task);
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
            var activeTasks = await TaskRepository.GetListAsync(x =>
                ids.Contains(x.Id) &&
                x.Status != 0); // 0 表示待处理状态

            if (activeTasks.Any())
                throw new HbtException(_localization.L("WorkflowTask.CannotDeleteNonPending"));

            var exp = Expressionable.Create<HbtProcessTask>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await TaskRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.BatchDelete.Failed"));

            _logger.Info(_localization.L("WorkflowTask.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtProcessTaskImportDto>(sheetName);
        }

        /// <summary>
        /// 导入工作流任务数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importTasks = await HbtExcelHelper.ImportAsync<HbtProcessTaskImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importTasks)
                {
                    try
                    {
                        var task = item.Adapt<HbtProcessTask>();
                        task.CreateTime = DateTime.Now;
                        task.CreateBy = _currentUser.UserName;

                        var result = await TaskRepository.CreateAsync(task);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流任务失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流任务数据失败", ex);
                throw new HbtException("导入工作流任务数据失败");
            }
        }

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtProcessTaskQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var tasks = await TaskRepository.GetListAsync(exp);
            var exportList = tasks.Adapt<List<HbtProcessTaskExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流任务数据");
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="status">新状态</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateStatusAsync(long taskId, int status)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            task.Status = status;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.UpdateStatus.Failed"));

            _logger.Info(_localization.L("WorkflowTask.StatusUpdated.Success", taskId, status));
            return true;
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="comment">完成意见</param>
        /// <returns>完成是否成功</returns>
        public async Task<bool> CompleteAsync(long id, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotCompleteNonPending"));

            task.Status = TASK_STATUS_APPROVED;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Complete.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 7, _currentUser.UserId, 1, comment);

            _logger.Info(_localization.L("WorkflowTask.Completed.Success", id));
            return true;
        }

        /// <summary>
        /// 转办任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="assigneeId">新的处理人ID</param>
        /// <param name="comment">转办意见</param>
        /// <returns>转办是否成功</returns>
        public async Task<bool> TransferAsync(long id, long assigneeId, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotTransferNonPending"));

            task.AssigneeId = assigneeId;
            task.Status = TASK_STATUS_TRANSFERRED;
            task.Comment = comment;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Transfer.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 8, _currentUser.UserId, 1, comment);

            _logger.Info(_localization.L("WorkflowTask.Transferred.Success", id));
            return true;
        }

        /// <summary>
        /// 转办任务（重载方法）
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="assigneeId">新的处理人ID</param>
        /// <param name="comment">转办意见</param>
        /// <returns>转办是否成功</returns>
        public async Task<bool> TransferTaskAsync(long taskId, long assigneeId, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotTransferNonPending"));

            // 验证新处理人是否存在
            var newAssignee = await UserRepository.GetByIdAsync(assigneeId);
            if (newAssignee == null)
                throw new HbtException(_localization.L("WorkflowTask.AssigneeNotFound"));

            task.AssigneeId = assigneeId;
            task.Status = TASK_STATUS_TRANSFERRED;
            task.Comment = comment;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Transfer.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 8, _currentUser.UserId, 1, comment);

            _logger.Info(_localization.L("WorkflowTask.Transferred.Success", taskId));
            return true;
        }

        /// <summary>
        /// 拒绝任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="comment">拒绝意见</param>
        /// <returns>拒绝是否成功</returns>
        public async Task<bool> RejectAsync(long id, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotRejectNonPending"));

            task.Status = TASK_STATUS_REJECTED;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Reject.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 9, _currentUser.UserId, 0, comment);

            _logger.Info(_localization.L("WorkflowTask.Rejected.Success", id));
            return true;
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="comment">取消原因</param>
        /// <returns>取消是否成功</returns>
        public async Task<bool> CancelAsync(long id, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotCancelNonPending"));

            task.Status = TASK_STATUS_CANCELLED;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Cancel.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 10, _currentUser.UserId, 0, comment);

            _logger.Info(_localization.L("WorkflowTask.Cancelled.Success", id));
            return true;
        }

        /// <summary>
        /// 同意任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="comment">同意意见</param>
        /// <returns>同意是否成功</returns>
        public async Task<bool> ApproveTaskAsync(long taskId, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotApproveNonPending"));

            task.Status = TASK_STATUS_APPROVED;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Approve.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 11, _currentUser.UserId, 1, comment);

            _logger.Info(_localization.L("WorkflowTask.Approved.Success", taskId));
            return true;
        }

        /// <summary>
        /// 拒绝任务（重载方法）
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="comment">拒绝意见</param>
        /// <returns>拒绝是否成功</returns>
        public async Task<bool> RejectTaskAsync(long taskId, string comment)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new HbtException(_localization.L("WorkflowTask.NotFound"));

            // 检查任务状态
            if (task.Status != TASK_STATUS_PENDING && task.Status != TASK_STATUS_PROCESSING)
                throw new HbtException(_localization.L("WorkflowTask.CannotRejectNonPending"));

            task.Status = TASK_STATUS_REJECTED;
            task.Comment = comment;
            task.CompleteTime = DateTime.Now;
            task.UpdateTime = DateTime.Now;
            task.UpdateBy = _currentUser.UserName;

            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowTask.Reject.Failed"));

            // 记录历史
            await CreateHistoryRecordAsync(task.InstanceId, task.NodeId, 12, _currentUser.UserId, 0, comment);

            _logger.Info(_localization.L("WorkflowTask.Rejected.Success", taskId));
            return true;
        }

        /// <summary>
        /// 获取处理人的任务列表
        /// </summary>
        /// <param name="assigneeId">处理人ID</param>
        /// <returns>任务列表</returns>
        public async Task<List<HbtProcessTaskDto>> GetTasksByAssigneeAsync(long assigneeId)
        {
            var tasks = await TaskRepository.GetListAsync(x => x.AssigneeId == assigneeId);
            var taskDtos = tasks.Adapt<List<HbtProcessTaskDto>>();

            // 获取所有相关的ID
            var instanceIds = taskDtos.Select(t => t.InstanceId).Distinct().ToList();
            var nodeIds = taskDtos.Select(t => t.NodeId).Distinct().ToList();

            // 批量查询关联数据
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            var nodes = await NodeRepository.GetListAsync(x => nodeIds.Contains(x.Id));

            // 填充名称信息
            foreach (var task in taskDtos)
            {
                var instance = instances.FirstOrDefault(i => i.Id == task.InstanceId);
                task.InstanceName = instance?.InstanceName ?? $"实例{task.InstanceId}";

                var node = nodes.FirstOrDefault(n => n.Id == task.NodeId);
                task.NodeName = node?.NodeTemplate?.NodeName ?? $"节点{task.NodeId}";
            }

            return taskDtos;
        }

        /// <summary>
        /// 获取用户任务状态统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>任务状态统计</returns>
        public async Task<HbtTaskStatusStatsDto> GetUserTaskStatusStatsAsync(long userId)
        {
            var pendingCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_PENDING);
            var processingCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_PROCESSING);
            var approvedCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_APPROVED);
            var overdueCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_PENDING && x.DueTime < DateTime.Now);

            return new HbtTaskStatusStatsDto
            {
                PendingCount = pendingCount,
                ProcessingCount = processingCount,
                ApprovedCount = approvedCount,
                OverdueCount = overdueCount
            };
        }

        /// <summary>
        /// 获取用户任务结果统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>任务结果统计</returns>
        public async Task<HbtTaskResultStatsDto> GetUserTaskResultStatsAsync(long userId)
        {
            var approvedCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_APPROVED);
            var rejectedCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_REJECTED);
            var transferredCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_TRANSFERRED);
            var returnedCount = await TaskRepository.GetCountAsync(x => x.AssigneeId == userId && x.Status == TASK_STATUS_RETURNED);

            return new HbtTaskResultStatsDto
            {
                ApprovedCount = approvedCount,
                RejectedCount = rejectedCount,
                TransferredCount = transferredCount,
                ReturnedCount = returnedCount
            };
        }

        /// <summary>
        /// 获取用户待办列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>待办任务列表</returns>
        public async Task<List<HbtProcessTaskDto>> GetUserTodoListAsync(long userId, int? status = null, int limit = 5)
        {
            var exp = Expressionable.Create<HbtProcessTask>();
            exp = exp.And(x => x.AssigneeId == userId);

            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);
            else
                exp = exp.And(x => x.Status == TASK_STATUS_PENDING || x.Status == TASK_STATUS_PROCESSING);

            var result = await TaskRepository.GetPagedListAsync(exp.ToExpression(), 1, limit, x => x.CreateTime, OrderByType.Desc);
            var tasks = result.Rows;
            var taskDtos = tasks.Adapt<List<HbtProcessTaskDto>>();

            // 获取所有相关的ID
            var instanceIds = taskDtos.Select(t => t.InstanceId).Distinct().ToList();
            var nodeIds = taskDtos.Select(t => t.NodeId).Distinct().ToList();

            // 批量查询关联数据
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            var nodes = await NodeRepository.GetListAsync(x => nodeIds.Contains(x.Id));

            // 填充名称信息
            foreach (var task in taskDtos)
            {
                var instance = instances.FirstOrDefault(i => i.Id == task.InstanceId);
                task.InstanceName = instance?.InstanceName ?? $"实例{task.InstanceId}";

                var node = nodes.FirstOrDefault(n => n.Id == task.NodeId);
                task.NodeName = node?.NodeTemplate?.NodeName ?? $"节点{task.NodeId}";
            }

            return taskDtos;
        }

        /// <summary>
        /// 获取用户催办列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="urgeType">催办类型</param>
        /// <param name="limit">限制数量</param>
        /// <returns>催办任务列表</returns>
        public async Task<List<HbtProcessTaskDto>> GetUserUrgeListAsync(long userId, string urgeType = "overdue", int limit = 5)
        {
            var exp = Expressionable.Create<HbtProcessTask>();
            exp = exp.And(x => x.AssigneeId == userId);

            switch (urgeType.ToLower())
            {
                case "overdue":
                    exp = exp.And(x => x.Status == TASK_STATUS_PENDING && x.DueTime < DateTime.Now);
                    break;
                case "urgent":
                    exp = exp.And(x => x.Status == TASK_STATUS_PENDING && x.DueTime <= DateTime.Now.AddDays(1));
                    break;
                case "normal":
                    exp = exp.And(x => x.Status == TASK_STATUS_PENDING && x.DueTime > DateTime.Now.AddDays(1));
                    break;
                default:
                    exp = exp.And(x => x.Status == TASK_STATUS_PENDING);
                    break;
            }

            var result = await TaskRepository.GetPagedListAsync(exp.ToExpression(), 1, limit, x => x.DueTime, OrderByType.Asc);
            var tasks = result.Rows;
            var taskDtos = tasks.Adapt<List<HbtProcessTaskDto>>();

            // 获取所有相关的ID
            var instanceIds = taskDtos.Select(t => t.InstanceId).Distinct().ToList();
            var nodeIds = taskDtos.Select(t => t.NodeId).Distinct().ToList();

            // 批量查询关联数据
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            var nodes = await NodeRepository.GetListAsync(x => nodeIds.Contains(x.Id));

            // 填充名称信息
            foreach (var task in taskDtos)
            {
                var instance = instances.FirstOrDefault(i => i.Id == task.InstanceId);
                task.InstanceName = instance?.InstanceName ?? $"实例{task.InstanceId}";

                var node = nodes.FirstOrDefault(n => n.Id == task.NodeId);
                task.NodeName = node?.NodeTemplate?.NodeName ?? $"节点{task.NodeId}";
            }

            return taskDtos;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtProcessTask, bool>> QueryExpression(HbtProcessTaskQueryDto query)
        {
            var exp = Expressionable.Create<HbtProcessTask>();

            if (query?.InstanceId.HasValue == true)
                exp = exp.And(x => x.InstanceId == query.InstanceId.Value);

            if (query?.NodeId.HasValue == true)
                exp = exp.And(x => x.NodeId == query.NodeId.Value);

            if (query?.TaskType.HasValue == true)
                exp = exp.And(x => x.TaskType == query.TaskType.Value);

            if (query?.AssigneeId.HasValue == true)
                exp = exp.And(x => x.AssigneeId == query.AssigneeId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 创建历史记录
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operationResult">操作结果</param>
        /// <param name="operationComment">操作注释</param>
        private async Task CreateHistoryRecordAsync(long instanceId, long nodeId, int operationType, long operatorId, int? operationResult = null, string? operationComment = null)
        {
            var history = new HbtHistory
            {
                InstanceId = instanceId,
                NodeId = nodeId,
                OperationType = operationType,
                OperationResult = operationResult,
                OperationComment = operationComment ?? string.Empty,
                CreateBy = operatorId.ToString(),
                CreateTime = DateTime.Now,
                UpdateBy = operatorId.ToString(),
                UpdateTime = DateTime.Now
            };

            await HistoryRepository.CreateAsync(history);
        }
    }
}