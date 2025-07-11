//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例服务实现类 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System.IO;
using Lean.Hbt.Common.Enums;
using System.Linq.Expressions;

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
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtInstanceService : HbtBaseService, IHbtInstanceService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtInstanceService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页后的工作流实例列表</returns>
        public async Task<HbtPagedResult<HbtInstanceDto>> GetListAsync(HbtInstanceQueryDto query)
        {
            query ??= new HbtInstanceQueryDto();

            var exp = QueryExpression(query);

            var result = await InstanceRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtInstanceDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtInstanceDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流实例详情
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>工作流实例详情DTO</returns>
        public async Task<HbtInstanceDto> GetByIdAsync(long id)
        {
            var instance = await InstanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            return instance.Adapt<HbtInstanceDto>();
        }

        /// <summary>
        /// 创建新的工作流实例
        /// </summary>
        /// <param name="input">工作流实例创建DTO</param>
        /// <returns>新创建的工作流实例ID</returns>
        public async Task<long> CreateAsync(HbtInstanceCreateDto input)
        {
            var instance = input.Adapt<HbtInstance>();
            instance.Status = 0; // 0 表示未开始状态

            var result = await InstanceRepository.CreateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Create.Failed"));

            _logger.Info(L("WorkflowInstance.Created.Success", instance.Id));
            return instance.Id;
        }

        /// <summary>
        /// 更新工作流实例信息
        /// </summary>
        /// <param name="input">工作流实例更新DTO</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateAsync(HbtInstanceUpdateDto input)
        {
            var instance = await InstanceRepository.GetByIdAsync(input.InstanceId)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许更新
            if (instance.Status != 0) // 0 表示未开始状态
                throw new HbtException(L("WorkflowInstance.CannotUpdateNonDraft"));

            input.Adapt(instance);
            var result = await InstanceRepository.UpdateAsync(instance);
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
        public async Task<bool> DeleteAsync(long id)
        {
            var instance = await InstanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许删除
            if (instance.Status != 0 && // 0 表示未开始状态
                instance.Status != 3) // 3 表示已终止状态
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            return await InstanceRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        /// <param name="ids">要删除的工作流实例ID数组</param>
        /// <returns>删除是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0) return false;

            // 检查是否有活动状态的实例
            var activeInstances = await InstanceRepository.GetListAsync(x =>
                ids.Contains(x.Id) &&
                x.Status != 0 && // 0 表示未开始状态
                x.Status != 3); // 3 表示已终止状态

            if (activeInstances.Any())
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            return await InstanceRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入工作流实例数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回成功和失败的记录数</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtInstance")
        {
            var instances = await HbtExcelHelper.ImportAsync<HbtInstanceDto>(fileStream, sheetName);
            if (instances == null || !instances.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var instance in instances)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(InstanceRepository, "InstanceName", instance.InstanceName);

                    var entity = instance.Adapt<HbtInstance>();
                    entity.Status = 0; // 0 表示未开始状态
                    entity.CreateBy = _currentUser.UserName;
                    entity.CreateTime = DateTime.Now;

                    await InstanceRepository.CreateAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入工作流实例失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出工作流实例数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceQueryDto query, string sheetName = "Instance")
        {
            var exp = QueryExpression(query);
            var instances = await InstanceRepository.GetListAsync(exp);
            var exportList = instances.Adapt<List<HbtInstanceExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流实例数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Instance")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtInstanceImportDto>(sheetName);
        }

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        /// <param name="input">状态更新DTO</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtInstanceStatusDto input)
        {
            var instance = await InstanceRepository.GetByIdAsync(input.InstanceId)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            instance.Status = input.Status;
            instance.UpdateTime = DateTime.Now;
            instance.UpdateBy = _currentUser.UserName;

            var result = await InstanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.UpdateStatus.Failed"));

            _logger.Info(L("WorkflowInstance.StatusUpdated.Success", instance.Id, input.Status));
            return true;
        }

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns>提交是否成功</returns>
        public async Task<bool> SubmitAsync(long id)
        {
            var instance = await InstanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态
            if (instance.Status != 0) // 0 表示未开始状态
                throw new HbtException(L("WorkflowInstance.CannotSubmitNonDraft"));

            // 更新实例状态
            instance.Status = 1; // 1 表示进行中状态
            instance.UpdateTime = DateTime.Now;
            instance.UpdateBy = _currentUser.UserName;

            await InstanceRepository.UpdateAsync(instance);

            _logger.Info(L("WorkflowInstance.Submitted.Success", id));
            return true;
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns>撤回是否成功</returns>
        public async Task<bool> WithdrawAsync(long id)
        {
            var instance = await InstanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.CannotWithdrawNonSubmitted"));

            // 检查实例状态
            if (instance.Status != 1) // 1 表示进行中状态
                throw new HbtException(L("WorkflowInstance.CannotWithdrawNonSubmitted"));

            // 更新实例状态
            instance.Status = 0; // 0 表示未开始状态
            instance.UpdateTime = DateTime.Now;
            instance.UpdateBy = _currentUser.UserName;

            await InstanceRepository.UpdateAsync(instance);

            _logger.Info(L("WorkflowInstance.Withdrawn.Success", id));
            return true;
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>终止是否成功</returns>
        public async Task<bool> TerminateAsync(long id, string reason)
        {
            var instance = await InstanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态
            if (instance.Status == 3) // 3 表示已终止状态
                throw new HbtException(L("WorkflowInstance.AlreadyTerminated"));

            // 更新实例状态
            instance.Status = 3; // 3 表示已终止状态
            instance.UpdateTime = DateTime.Now;
            instance.UpdateBy = _currentUser.UserName;

            await InstanceRepository.UpdateAsync(instance);

            _logger.Info(L("WorkflowInstance.Terminated.Success", id));
            return true;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtInstance, bool>> QueryExpression(HbtInstanceQueryDto query)
        {
            var exp = Expressionable.Create<HbtInstance>();

            if (!string.IsNullOrEmpty(query?.InstanceName))
                exp = exp.And(x => x.InstanceName.Contains(query.InstanceName));

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取工作流实例选项列表
        /// </summary>
        /// <returns>工作流实例选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var instances = await InstanceRepository.GetListAsync(x => x.Status == 1); // 1 表示进行中状态
            return instances.Select(x => new HbtSelectOption
            {
                Value = x.Id.ToString(),
                Label = x.InstanceName
            }).ToList();
        }

        /// <summary>
        /// 获取当前用户的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">数量限制</param>
        /// <returns>工作流实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetCurrentUserInstancesAsync(int? status = null, int limit = 20)
        {
            var userName = _currentUser.UserName;
            var query = InstanceRepository.GetListAsync(x => 
                x.CreateBy == userName && 
                (status == null || x.Status == status));

            var instances = await query;
            return instances.Take(limit).Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 获取当前用户发起的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">数量限制</param>
        /// <returns>工作流实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetCurrentUserInitiatedInstancesAsync(int? status = null, int limit = 20)
        {
            var userName = _currentUser.UserName;
            var query = InstanceRepository.GetListAsync(x => 
                x.CreateBy == userName && 
                (status == null || x.Status == status));

            var instances = await query;
            return instances.Take(limit).Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 获取当前用户参与的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">数量限制</param>
        /// <returns>工作流实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetCurrentUserParticipatedInstancesAsync(int? status = null, int limit = 20)
        {
            var userId = _currentUser.UserId;
            
            // 获取用户参与的任务
            var taskRepository = _repositoryFactory.GetWorkflowRepository<HbtProcessTask>();
            var userTasks = await taskRepository.GetListAsync(x => 
                x.AssigneeId == userId && 
                (status == null || x.Status == status));

            // 获取相关的实例ID
            var instanceIds = userTasks.Select(x => x.InstanceId).Distinct().ToList();
            
            if (!instanceIds.Any())
                return new List<HbtInstanceDto>();

            // 获取实例详情
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            return instances.Take(limit).Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="input">启动参数</param>
        /// <returns>工作流实例ID</returns>
        public async Task<long> StartWorkflowAsync(HbtWorkflowStartDto input)
        {
            // 创建新的工作流实例
            var instance = new HbtInstance
            {
                InstanceName = input.Title,
                DefinitionId = input.DefinitionId,
                Status = 1, // 1 表示进行中状态
                CreateBy = _currentUser.UserName,
                CreateTime = DateTime.Now,
                UpdateBy = _currentUser.UserName,
                UpdateTime = DateTime.Now
            };

            var instanceId = await InstanceRepository.CreateAsync(instance);
            _logger.Info($"工作流实例启动成功，实例ID: {instanceId}");
            return instanceId;
        }

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        public async Task<bool> SuspendWorkflowAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            instance.Status = 4; // 4 表示已挂起状态
            instance.UpdateBy = _currentUser.UserName;
            instance.UpdateTime = DateTime.Now;

            var result = await InstanceRepository.UpdateAsync(instance);
            _logger.Info($"工作流实例暂停成功，实例ID: {instanceId}");
            return result > 0;
        }

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        public async Task<bool> ResumeWorkflowAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            instance.Status = 1; // 1 表示进行中状态
            instance.UpdateBy = _currentUser.UserName;
            instance.UpdateTime = DateTime.Now;

            var result = await InstanceRepository.UpdateAsync(instance);
            _logger.Info($"工作流实例恢复成功，实例ID: {instanceId}");
            return result > 0;
        }

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态</returns>
        public async Task<object> GetWorkflowStatusAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            return new
            {
                InstanceId = instance.Id,
                Status = instance.Status,
                StatusName = GetStatusName(instance.Status),
                LastUpdateTime = instance.UpdateTime
            };
        }

        /// <summary>
        /// 获取可用转换
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用转换列表</returns>
        public async Task<List<object>> GetAvailableTransitionsAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 根据当前状态返回可用的转换
            var transitions = new List<object>();
            
            switch (instance.Status)
            {
                case 0: // 未开始状态
                    transitions.Add(new { Name = "提交", Action = "submit" });
                    break;
                case 1: // 进行中状态
                    transitions.Add(new { Name = "暂停", Action = "suspend" });
                    transitions.Add(new { Name = "终止", Action = "terminate" });
                    break;
                case 4: // 已挂起状态
                    transitions.Add(new { Name = "恢复", Action = "resume" });
                    transitions.Add(new { Name = "终止", Action = "terminate" });
                    break;
            }

            return transitions;
        }

        /// <summary>
        /// 执行工作流转换
        /// </summary>
        /// <param name="input">转换执行参数</param>
        /// <returns>转换结果</returns>
        public async Task<bool> ExecuteTransitionAsync(HbtTransitionExecuteDto input)
        {
            var instance = await InstanceRepository.GetByIdAsync(input.InstanceId);
            if (instance == null)
                throw new HbtException(L("WorkflowInstance.NotFound"));

            // 根据转换ID执行相应的操作
            // 这里简化处理，实际应该根据转换定义来执行
            switch (input.TransitionId)
            {
                case 1: // 假设转换ID 1 是提交
                    return await SubmitAsync(input.InstanceId);
                case 2: // 假设转换ID 2 是暂停
                    return await SuspendWorkflowAsync(input.InstanceId);
                case 3: // 假设转换ID 3 是恢复
                    return await ResumeWorkflowAsync(input.InstanceId);
                case 4: // 假设转换ID 4 是终止
                    return await TerminateAsync(input.InstanceId, "用户终止");
                default:
                    throw new HbtException($"不支持的转换ID: {input.TransitionId}");
            }
        }

        /// <summary>
        /// 获取工作流仪表盘统计数据
        /// </summary>
        /// <returns>仪表盘统计数据</returns>
        public async Task<object> GetDashboardStatsAsync()
        {
            var totalInstances = await InstanceRepository.GetCountAsync(x => true);
            var runningInstances = await InstanceRepository.GetCountAsync(x => x.Status == 1);
            var completedInstances = await InstanceRepository.GetCountAsync(x => x.Status == 2);
            var terminatedInstances = await InstanceRepository.GetCountAsync(x => x.Status == 3);
            var suspendedInstances = await InstanceRepository.GetCountAsync(x => x.Status == 4);

            return new
            {
                TotalInstances = totalInstances,
                RunningInstances = runningInstances,
                CompletedInstances = completedInstances,
                TerminatedInstances = terminatedInstances,
                SuspendedInstances = suspendedInstances
            };
        }

        /// <summary>
        /// 获取最近活动
        /// </summary>
        /// <returns>最近活动列表</returns>
        public async Task<List<object>> GetRecentActivitiesAsync()
        {
            var recentInstances = await InstanceRepository.GetListAsync(x => true);
            var sortedInstances = recentInstances.OrderByDescending(x => x.UpdateTime).Take(10);
            
            return sortedInstances.Select(x => new
            {
                InstanceId = x.Id,
                InstanceName = x.InstanceName,
                Status = x.Status,
                StatusName = GetStatusName(x.Status),
                UpdateTime = x.UpdateTime,
                UpdateBy = x.UpdateBy
            }).Cast<object>().ToList();
        }

        /// <summary>
        /// 获取用户流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态筛选</param>
        /// <returns>用户流程实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetUserWorkflowsAsync(long userId, int? status = null)
        {
            var userName = await GetUserNameByIdAsync(userId);
            var query = InstanceRepository.GetListAsync(x => 
                x.CreateBy == userName && 
                (status == null || x.Status == status));

            var instances = await query;
            return instances.Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 获取用户发起的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户发起的流程实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetUserInitiatedWorkflowsAsync(long userId)
        {
            var userName = await GetUserNameByIdAsync(userId);
            var instances = await InstanceRepository.GetListAsync(x => x.CreateBy == userName);
            return instances.Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 获取用户参与的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户参与的流程实例列表</returns>
        public async Task<List<HbtInstanceDto>> GetUserParticipatedWorkflowsAsync(long userId)
        {
            // 获取用户参与的任务
            var taskRepository = _repositoryFactory.GetWorkflowRepository<HbtProcessTask>();
            var userTasks = await taskRepository.GetListAsync(x => x.AssigneeId == userId);

            // 获取相关的实例ID
            var instanceIds = userTasks.Select(x => x.InstanceId).Distinct().ToList();
            
            if (!instanceIds.Any())
                return new List<HbtInstanceDto>();

            // 获取实例详情
            var instances = await InstanceRepository.GetListAsync(x => instanceIds.Contains(x.Id));
            return instances.Adapt<List<HbtInstanceDto>>();
        }

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户名</returns>
        private async Task<string> GetUserNameByIdAsync(long userId)
        {
            var userRepository = _repositoryFactory.GetAuthRepository<HbtUser>();
            var user = await userRepository.GetByIdAsync(userId);
            return user?.UserName ?? "Unknown";
        }

        /// <summary>
        /// 获取状态名称
        /// </summary>
        /// <param name="status">状态码</param>
        /// <returns>状态名称</returns>
        private string GetStatusName(int status)
        {
            return status switch
            {
                0 => "未开始",
                1 => "进行中",
                2 => "已完成",
                3 => "已终止",
                4 => "已挂起",
                _ => "未知状态"
            };
        }
    }
}