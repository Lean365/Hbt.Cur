#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎实现 - 基于简化后的实体结构
//===================================================================

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Hbt.Common.Helpers;
using SqlSugar;

namespace Hbt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流引擎实现
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// 功能说明:
    /// 1. 提供工作流实例的启动、暂停、恢复、终止等操作
    /// 2. 支持工作流审批和流转
    /// 3. 实现工作流变量的管理
    /// 4. 提供工作流状态查询和历史记录
    /// 基于简化后的实体结构实现
    /// </remarks>
    public class HbtWorkflowEngine : IHbtWorkflowEngine
    {

        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly IHbtDbContext _dbContext;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly IHbtCurrentUser _currentUser;

        /// <summary>
        /// 获取实例仓储
        /// </summary>
        private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();

        /// <summary>
        /// 获取方案仓储
        /// </summary>
        private IHbtRepository<HbtScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<HbtScheme>();

        /// <summary>
        /// 获取操作记录仓储
        /// </summary>
        private IHbtRepository<HbtInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceOper>();

        /// <summary>
        /// 获取流转历史仓储
        /// </summary>
        private IHbtRepository<HbtInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceTrans>();



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        public HbtWorkflowEngine(
            IHbtDbContext dbContext,
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtCurrentUser currentUser)
        {
            _dbContext = dbContext;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="dto">工作流启动信息</param>
        /// <returns>新创建的工作流实例ID</returns>
        /// <exception cref="InvalidOperationException">当工作流方案不存在、未发布或配置无效时抛出</exception>
        public async Task<long> StartAsync(HbtWorkflowStartDto dto)
        {
            _dbContext.BeginTran();
            try
            {
                // 获取工作流方案
                var scheme = await SchemeRepository.GetByIdAsync(dto.SchemeId);
                if (scheme == null)
                {
                    throw new InvalidOperationException("工作流方案不存在");
                }

                if (scheme.Status != 1) // 1 表示已发布
                {
                    throw new InvalidOperationException("工作流方案未发布，无法启动");
                }

                // 解析工作流配置
                var workflowConfig = JsonSerializer.Deserialize<WorkflowConfigModel>(scheme.SchemeConfig);
                if (workflowConfig == null || !workflowConfig.Nodes.Any())
                {
                    throw new InvalidOperationException("工作流配置无效");
                }

                // 获取开始节点
                var startNode = workflowConfig.Nodes.FirstOrDefault(x => x.Type == "start");
                if (startNode == null)
                {
                    throw new InvalidOperationException("工作流配置中未找到开始节点");
                }

                // 创建工作流实例
                var instance = new HbtInstance
                {
                    SchemeId = dto.SchemeId,
                    InstanceTitle = dto.InstanceTitle,
                    BusinessKey = dto.BusinessKey ?? Guid.NewGuid().ToString(),
                    InitiatorId = dto.InitiatorId,
                    CurrentNodeId = startNode.Id,
                    CurrentNodeName = startNode.Name,
                    Status = 1, // 1 表示运行中
                    StartTime = DateTime.Now,
                    Variables = dto.Variables
                };

                await InstanceRepository.CreateAsync(instance);

                // 记录工作流启动历史
                await CreateTransRecordAsync(instance.Id, "", startNode.Id, startNode.Name, 1, "工作流启动");

                // 记录开始节点操作
                await CreateOperRecordAsync(instance.Id, startNode.Id, startNode.Name, 1, dto.InitiatorId, "工作流启动", null);

                _dbContext.CommitTran();
                _logger.Info($"工作流实例启动成功，ID: {instance.Id}, 方案: {scheme.SchemeName}");
                return instance.Id;
            }
            catch (Exception ex)
            {
                _dbContext.RollbackTran();
                _logger.Error($"工作流实例启动失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">暂停原因</param>
        /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许暂停时抛出</exception>
        public async Task SuspendAsync(long instanceId, string reason = "手动暂停")
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1) // 1 表示运行中
            {
                throw new InvalidOperationException("只有运行中的工作流实例才能暂停");
            }

            instance.Status = 3; // 3 表示已暂停
            await InstanceRepository.UpdateAsync(instance);

            // 记录工作流暂停历史
            await CreateTransRecordAsync(instanceId, instance.CurrentNodeId ?? "", "", "", 3, reason);
            await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 6, _currentUser.UserId, reason, null);
        }

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许恢复时抛出</exception>
        public async Task ResumeAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 3) // 3 表示已暂停
            {
                throw new InvalidOperationException("只有已暂停的工作流实例才能恢复");
            }

            instance.Status = 1; // 1 表示运行中
            await InstanceRepository.UpdateAsync(instance);

            // 记录工作流恢复历史
            await CreateTransRecordAsync(instanceId, "", instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 1, "工作流恢复");
            await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 7, _currentUser.UserId, "工作流恢复", null);
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许终止时抛出</exception>
        public async Task TerminateAsync(long instanceId, string reason)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1 && instance.Status != 3) // 1 表示运行中, 3 表示已暂停
            {
                throw new InvalidOperationException("只有运行中或已暂停的工作流实例才能终止");
            }

            instance.Status = 4; // 4 表示已终止
            instance.EndTime = DateTime.Now;
            await InstanceRepository.UpdateAsync(instance);

            // 记录工作流终止历史
            await CreateTransRecordAsync(instanceId, instance.CurrentNodeId ?? "", "", "", 4, reason);
            await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 8, _currentUser.UserId, reason, null);
        }

        /// <summary>
        /// 审批工作流
        /// </summary>
        /// <param name="dto">审批信息</param>
        /// <returns>审批是否成功</returns>
        /// <exception cref="InvalidOperationException">当工作流实例不存在、状态不允许审批或操作类型不支持时抛出</exception>
        public async Task<bool> ApproveAsync(HbtWorkflowApproveDto dto)
        {
            _dbContext.BeginTran();
            try
            {
                var instance = await InstanceRepository.GetByIdAsync(dto.InstanceId);
                if (instance == null)
                {
                    throw new InvalidOperationException("工作流实例不存在");
                }

                if (instance.Status != 1) // 1 表示运行中
                {
                    throw new InvalidOperationException("只有运行中的工作流实例才能审批");
                }

                if (instance.CurrentNodeId != dto.NodeId)
                {
                    throw new InvalidOperationException("当前节点不匹配");
                }

                // 获取工作流方案
                var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
                if (scheme == null)
                {
                    throw new InvalidOperationException("工作流方案不存在");
                }

                // 解析工作流配置
                var workflowConfig = JsonSerializer.Deserialize<WorkflowConfigModel>(scheme.SchemeConfig);
                if (workflowConfig == null)
                {
                    throw new InvalidOperationException("工作流配置无效");
                }

                // 记录操作
                await CreateOperRecordAsync(dto.InstanceId, dto.NodeId, instance.CurrentNodeName ?? "", dto.OperType, _currentUser.UserId, dto.OperOpinion, dto.OperData);

                // 根据操作类型处理
                switch (dto.OperType)
                {
                    case 1: // 同意
                        await ProcessApproveAsync(instance, workflowConfig, dto);
                        break;
                    case 2: // 拒绝
                        await ProcessRejectAsync(instance, dto);
                        break;
                    case 3: // 退回
                        await ProcessReturnAsync(instance, workflowConfig, dto);
                        break;
                    case 4: // 转办
                        await ProcessTransferAsync(instance, dto);
                        break;
                    case 5: // 委托
                        await ProcessDelegateAsync(instance, dto);
                        break;
                    default:
                        throw new InvalidOperationException("不支持的操作类型");
                }

                _dbContext.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _dbContext.RollbackTran();
                _logger.Error($"工作流审批失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态信息</returns>
        /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
        public async Task<HbtInstanceStatusDto> GetStatusAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            return new HbtInstanceStatusDto
            {
                InstanceId = instance.Id,
                Status = instance.Status
            };
        }

        /// <summary>
        /// 获取可用的流转选项
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用的流转选项列表</returns>
        /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
        public async Task<List<HbtTransitionDto>> GetAvailableTransitionsAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1) // 1 表示运行中
            {
                return new List<HbtTransitionDto>();
            }

            // 获取工作流方案
            var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
            if (scheme == null)
            {
                return new List<HbtTransitionDto>();
            }

            // 解析工作流配置
            var workflowConfig = JsonSerializer.Deserialize<WorkflowConfigModel>(scheme.SchemeConfig);
            if (workflowConfig == null)
            {
                return new List<HbtTransitionDto>();
            }

            // 获取当前节点的可用转换
            var transitions = new List<HbtTransitionDto>();
            var currentEdges = workflowConfig.Edges.Where(e => e.Source == instance.CurrentNodeId).ToList();

            foreach (var edge in currentEdges)
            {
                var targetNode = workflowConfig.Nodes.FirstOrDefault(n => n.Id == edge.Target);
                if (targetNode != null)
                {
                    transitions.Add(new HbtTransitionDto
                    {
                        InstanceTransId = edge.Id,
                        InstanceId = instance.Id,
                        StartNodeId = instance.CurrentNodeId ?? "",
                        StartNodeName = instance.CurrentNodeName ?? "",
                        ToNodeId = targetNode.Id,
                        ToNodeName = targetNode.Name,
                        TransState = 1,
                        IsFinish = 0,
                        TransTime = DateTime.Now
                    });
                }
            }

            return transitions;
        }

        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>当前节点信息，如果不存在则返回null</returns>
        public async Task<HbtNodeDto?> GetCurrentNodeAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null || string.IsNullOrEmpty(instance.CurrentNodeId))
            {
                return null;
            }

            // 获取工作流方案
            var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
            if (scheme == null)
            {
                return null;
            }

            // 解析工作流配置
            var workflowConfig = JsonSerializer.Deserialize<WorkflowConfigModel>(scheme.SchemeConfig);
            if (workflowConfig == null)
            {
                return null;
            }

            // 查找当前节点
            var currentNode = workflowConfig.Nodes.FirstOrDefault(n => n.Id == instance.CurrentNodeId);
            if (currentNode == null)
            {
                return null;
            }

            return new HbtNodeDto
            {
                NodeId = currentNode.Id,
                NodeName = currentNode.Name,
                NodeType = currentNode.Type,
                NodeConfig = currentNode.Config?.ToString(),
                ApproverType = currentNode.ApproverType ?? 1,
                ApproverConfig = currentNode.ApproverConfig?.ToString()
            };
        }

        /// <summary>
        /// 获取工作流变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流变量字典</returns>
        /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
        public async Task<Dictionary<string, object>> GetVariablesAsync(long instanceId)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (string.IsNullOrEmpty(instance.Variables))
            {
                return new Dictionary<string, object>();
            }

            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(instance.Variables) ?? new Dictionary<string, object>();
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// 设置工作流变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="variables">变量字典</param>
        /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
        public async Task SetVariablesAsync(long instanceId, Dictionary<string, object> variables)
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            instance.Variables = JsonSerializer.Serialize(variables);
            await InstanceRepository.UpdateAsync(instance);
        }

        /// <summary>
        /// 获取工作流历史记录
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>流转历史记录列表</returns>
        public async Task<List<HbtInstanceTransDto>> GetHistoryAsync(long instanceId)
        {
            var list = await TransRepository.GetListAsync(x => x.InstanceId == instanceId);
            return list.Adapt<List<HbtInstanceTransDto>>();
        }

        /// <summary>
        /// 获取工作流操作记录
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作记录列表</returns>
        public async Task<List<HbtInstanceOperDto>> GetOperationsAsync(long instanceId)
        {
            var list = await OperRepository.GetListAsync(x => x.InstanceId == instanceId);
            return list.Adapt<List<HbtInstanceOperDto>>();
        }

        /// <summary>
        /// 获取转换历史列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转换历史列表</returns>
        public async Task<HbtPagedResult<HbtTransitionDto>> GetTransitionListAsync(HbtTransitionQueryDto query)
        {
            var list = await TransRepository.GetListAsync(x => 
                (query.InstanceId == null || x.InstanceId == query.InstanceId) &&
                (string.IsNullOrEmpty(query.StartNodeId) || x.StartNodeId.Contains(query.StartNodeId)) &&
                (string.IsNullOrEmpty(query.ToNodeId) || x.ToNodeId.Contains(query.ToNodeId)) &&
                (query.TransState == null || x.TransState == query.TransState) &&
                (query.IsFinish == null || x.IsFinish == query.IsFinish)
            );
            
            var total = list.Count;
            var pagedList = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();
            
            var dtoList = pagedList.Adapt<List<HbtTransitionDto>>();
            
            return new HbtPagedResult<HbtTransitionDto>
            {
                TotalNum = total,
                Rows = dtoList
            };
        }

        /// <summary>
        /// 获取转换历史详情
        /// </summary>
        /// <param name="transitionId">转换ID</param>
        /// <returns>转换历史详情</returns>
        public async Task<HbtTransitionDto?> GetTransitionAsync(string transitionId)
        {
            var trans = await TransRepository.GetFirstAsync(x => x.Id.ToString() == transitionId);
            return trans?.Adapt<HbtTransitionDto>();
        }

        #region 私有方法

        /// <summary>
        /// 处理同意操作
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="workflowConfig">工作流配置</param>
        /// <param name="dto">审批信息</param>
        /// <returns>异步任务</returns>
        private async Task ProcessApproveAsync(HbtInstance instance, WorkflowConfigModel workflowConfig, HbtWorkflowApproveDto dto)
        {
            // 获取当前节点的出边
            var edges = workflowConfig.Edges.Where(e => e.Source == instance.CurrentNodeId).ToList();
            if (!edges.Any())
            {
                // 没有出边，说明是结束节点
                instance.Status = 2; // 2 表示已完成
                instance.EndTime = DateTime.Now;
                instance.CurrentNodeId = null;
                instance.CurrentNodeName = null;
                await InstanceRepository.UpdateAsync(instance);

                await CreateTransRecordAsync(instance.Id, instance.CurrentNodeId ?? "", "", "", 2, "工作流完成");
                return;
            }

            // 获取下一个节点（这里简化处理，取第一个出边）
            var nextEdge = edges.First();
            var nextNode = workflowConfig.Nodes.FirstOrDefault(n => n.Id == nextEdge.Target);
            if (nextNode == null)
            {
                throw new InvalidOperationException("无法找到下一个节点");
            }

            // 更新实例状态
            instance.CurrentNodeId = nextNode.Id;
            instance.CurrentNodeName = nextNode.Name;
            await InstanceRepository.UpdateAsync(instance);

            // 记录流转历史
            await CreateTransRecordAsync(instance.Id, dto.NodeId, nextNode.Id, nextNode.Name, 1, "节点流转");

            // 如果是结束节点，完成工作流
            if (nextNode.Type == "end")
            {
                instance.Status = 2; // 2 表示已完成
                instance.EndTime = DateTime.Now;
                await InstanceRepository.UpdateAsync(instance);
            }
        }

        /// <summary>
        /// 处理拒绝操作
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="dto">审批信息</param>
        /// <returns>异步任务</returns>
        private async Task ProcessRejectAsync(HbtInstance instance, HbtWorkflowApproveDto dto)
        {
            instance.Status = 4; // 4 表示已终止
            instance.EndTime = DateTime.Now;
            await InstanceRepository.UpdateAsync(instance);

            await CreateTransRecordAsync(instance.Id, dto.NodeId, "", "", 4, "工作流被拒绝");
        }

        /// <summary>
        /// 处理退回操作
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="workflowConfig">工作流配置</param>
        /// <param name="dto">审批信息</param>
        /// <returns>异步任务</returns>
        private async Task ProcessReturnAsync(HbtInstance instance, WorkflowConfigModel workflowConfig, HbtWorkflowApproveDto dto)
        {
            // 这里简化处理，退回到上一个节点
            // 实际应用中需要根据业务逻辑确定退回目标
            var edges = workflowConfig.Edges.Where(e => e.Target == instance.CurrentNodeId).ToList();
            if (edges.Any())
            {
                var prevEdge = edges.First();
                var prevNode = workflowConfig.Nodes.FirstOrDefault(n => n.Id == prevEdge.Source);
                if (prevNode != null)
                {
                    instance.CurrentNodeId = prevNode.Id;
                    instance.CurrentNodeName = prevNode.Name;
                    await InstanceRepository.UpdateAsync(instance);

                    await CreateTransRecordAsync(instance.Id, dto.NodeId, prevNode.Id, prevNode.Name, 3, "节点退回");
                }
            }
        }

        /// <summary>
        /// 处理转办操作
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="dto">审批信息</param>
        /// <returns>异步任务</returns>
        private async Task ProcessTransferAsync(HbtInstance instance, HbtWorkflowApproveDto dto)
        {
            // 转办操作，当前节点保持不变，但操作人变为目标用户
            // 这里需要根据具体业务逻辑实现
            await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 5, $"转办给用户{dto.TargetUserId}");
        }

        /// <summary>
        /// 处理委托操作
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="dto">审批信息</param>
        /// <returns>异步任务</returns>
        private async Task ProcessDelegateAsync(HbtInstance instance, HbtWorkflowApproveDto dto)
        {
            // 委托操作，当前节点保持不变，但操作人变为目标用户
            // 这里需要根据具体业务逻辑实现
            await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 6, $"委托给用户{dto.TargetUserId}");
        }

        /// <summary>
        /// 创建流转历史记录
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="fromNodeId">源节点ID</param>
        /// <param name="toNodeId">目标节点ID</param>
        /// <param name="toNodeName">目标节点名称</param>
        /// <param name="transType">流转类型</param>
        /// <param name="remark">备注</param>
        /// <returns>异步任务</returns>
        private async Task CreateTransRecordAsync(long instanceId, string fromNodeId, string toNodeId, string toNodeName, int transType, string remark)
        {
            var trans = new HbtInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = fromNodeId,
                StartNodeType = 1, // 默认类型
                StartNodeName = fromNodeId, // 简化处理
                ToNodeId = toNodeId,
                ToNodeType = 1, // 默认类型
                ToNodeName = toNodeName,
                TransState = 1, // 1 表示成功
                IsFinish = string.IsNullOrEmpty(toNodeId) ? 1 :0, // 如果没有目标节点，表示完成
                TransTime = DateTime.Now
            };

            await TransRepository.CreateAsync(trans);
        }

        /// <summary>
        /// 创建操作记录
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="operType">操作类型</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operOpinion">操作意见</param>
        /// <param name="operData">操作数据</param>
        /// <returns>异步任务</returns>
        private async Task CreateOperRecordAsync(long instanceId, string nodeId, string nodeName, int operType, long operatorId, string operOpinion, string? operData)
        {
            var oper = new HbtInstanceOper
            {
                InstanceId = instanceId,
                NodeId = nodeId,
                NodeName = nodeName,
                OperType = operType,
                OperatorId = operatorId,
                OperatorName = _currentUser.UserName,
                OperOpinion = operOpinion,
                OperData = operData
            };

            await OperRepository.CreateAsync(oper);
        }

        #endregion

        #region 内部类

        /// <summary>
        /// 工作流配置模型
        /// </summary>
        private class WorkflowConfigModel
        {
            /// <summary>
            /// 版本号
            /// </summary>
            public string Version { get; set; } = string.Empty;

            /// <summary>
            /// 节点列表
            /// </summary>
            public List<WorkflowNode> Nodes { get; set; } = new();

            /// <summary>
            /// 边列表
            /// </summary>
            public List<WorkflowEdge> Edges { get; set; } = new();
        }

        /// <summary>
        /// 工作流节点
        /// </summary>
        private class WorkflowNode
        {
            /// <summary>
            /// 节点ID
            /// </summary>
            public string Id { get; set; } = string.Empty;

            /// <summary>
            /// 节点名称
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// 节点类型
            /// </summary>
            public string Type { get; set; } = string.Empty;

            /// <summary>
            /// 节点配置
            /// </summary>
            public object? Config { get; set; }

            /// <summary>
            /// 审批人类型
            /// </summary>
            public int? ApproverType { get; set; }

            /// <summary>
            /// 审批人配置
            /// </summary>
            public object? ApproverConfig { get; set; }
        }

        /// <summary>
        /// 工作流边
        /// </summary>
        private class WorkflowEdge
        {
            /// <summary>
            /// 边ID
            /// </summary>
            public string Id { get; set; } = string.Empty;

            /// <summary>
            /// 源节点ID
            /// </summary>
            public string Source { get; set; } = string.Empty;

            /// <summary>
            /// 目标节点ID
            /// </summary>
            public string Target { get; set; } = string.Empty;

            /// <summary>
            /// 边标签
            /// </summary>
            public string? Label { get; set; }

            /// <summary>
            /// 边类型
            /// </summary>
            public string Type { get; set; } = "manual";

            /// <summary>
            /// 转换条件
            /// </summary>
            public string? Condition { get; set; }
        }

        #endregion
    }
}