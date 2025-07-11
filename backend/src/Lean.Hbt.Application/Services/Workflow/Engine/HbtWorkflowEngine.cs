#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtEngine.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流引擎实现
//===================================================================

using Lean.Hbt.Application.Services.Workflow.Engine.Executors;
using Lean.Hbt.Application.Services.Workflow.Engine.Expressions;
using Lean.Hbt.Domain.Data;

namespace Lean.Hbt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流引擎实现
    /// </summary>
    public class HbtWorkflowEngine : IHbtWorkflowEngine
    {
        private readonly IHbtDbContext _dbContext;
        private readonly IHbtRepository<HbtInstance> _instanceRepository;
        private readonly IHbtRepository<HbtNode> _nodeRepository;
        private readonly IHbtRepository<HbtNodeTemplate> _nodeTemplateRepository;
        private readonly IHbtRepository<HbtTransition> _transitionRepository;
        private readonly IHbtRepository<HbtVariable> _variableRepository;
        private readonly IHbtRepository<HbtDefinition> _definitionRepository;
        private readonly IEnumerable<IHbtNodeExecutor> _nodeExecutors;
        private readonly IHbtRepository<HbtParallelBranch> _parallelBranchRepository;
        private readonly IHbtExpressionEngine _expressionEngine;
        private readonly IHbtRepository<HbtActivity> _activityRepository;
        private readonly IHbtRepository<HbtHistory> _historyRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowEngine(
            IHbtDbContext dbContext,
            IHbtRepository<HbtInstance> instanceRepository,
            IHbtRepository<HbtNode> nodeRepository,
            IHbtRepository<HbtNodeTemplate> nodeTemplateRepository,
            IHbtRepository<HbtTransition> transitionRepository,
            IHbtRepository<HbtVariable> variableRepository,
            IHbtRepository<HbtDefinition> definitionRepository,
            IEnumerable<IHbtNodeExecutor> nodeExecutors,
            IHbtRepository<HbtParallelBranch> parallelBranchRepository,
            IHbtExpressionEngine expressionEngine,
            IHbtRepository<HbtActivity> activityRepository,
            IHbtRepository<HbtHistory> historyRepository,
            IHbtLogger logger)
        {
            _dbContext = dbContext;
            _instanceRepository = instanceRepository;
            _nodeRepository = nodeRepository;
            _nodeTemplateRepository = nodeTemplateRepository;
            _transitionRepository = transitionRepository;
            _variableRepository = variableRepository;
            _definitionRepository = definitionRepository;
            _nodeExecutors = nodeExecutors;
            _parallelBranchRepository = parallelBranchRepository;
            _expressionEngine = expressionEngine;
            _activityRepository = activityRepository;
            _historyRepository = historyRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<long> StartAsync(long definitionId, string title, long initiatorId, string formData, Dictionary<string, object>? variables = null)
        {
            _dbContext.BeginTran();
            try
            {
                // 获取工作流定义
                var definition = await _definitionRepository.GetByIdAsync(definitionId);
                if (definition == null)
                {
                    throw new InvalidOperationException("工作流定义不存在");
                }

                if (definition.Status != 1) // 1 表示已发布
                {
                    throw new InvalidOperationException("工作流定义未发布，无法启动");
                }

                // 创建工作流实例
                var instance = new HbtInstance
                {
                    DefinitionId = definitionId,
                    InstanceName = title,
                    BusinessKey = Guid.NewGuid().ToString(), // 生成业务键
                    InitiatorId = initiatorId,
                    FormData = formData,
                    Status = 1, // 1 表示运行中
                    StartTime = DateTime.Now
                };

                await _instanceRepository.CreateAsync(instance);

                // 从workflowConfig创建节点
                var nodes = await CreateNodesFromWorkflowConfigAsync(definition, instance.Id);
                if (!nodes.Any())
                {
                    throw new InvalidOperationException("无法从工作流配置创建节点");
                }

                // 获取开始节点
                var startNode = nodes.FirstOrDefault(x => {
                    var nodeTemplate = _nodeTemplateRepository.GetByIdAsync(x.NodeTemplateId).Result;
                    return nodeTemplate?.NodeType == 1; // 1 表示开始节点
                });
                if (startNode == null)
                {
                    throw new InvalidOperationException("工作流定义中未找到开始节点");
                }

                // 更新实例的当前节点
                instance.CurrentNodeId = startNode.Id;
                await _instanceRepository.UpdateAsync(instance);

                // 保存变量
                if (variables != null)
                {
                    await SaveVariablesAsync(instance.Id, variables);
                }

                // 记录工作流启动历史
                await CreateHistoryRecordAsync(instance.Id, startNode.Id, 1, initiatorId, 1, "工作流启动");

                // 执行开始节点
                var result = await ExecuteNodeAsync(instance.Id, startNode.Id);
                if (!result.Success)
                {
                    throw new InvalidOperationException($"执行开始节点失败: {result.ErrorMessage}");
                }

                _dbContext.CommitTran();
                return instance.Id;
            }
            catch (Exception)
            {
                _dbContext.RollbackTran();
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SuspendAsync(long instanceId)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1) // 1 表示运行中
            {
                throw new InvalidOperationException("只有运行中的工作流实例才能暂停");
            }

            instance.Status = 3; // 3 表示已挂起
            await _instanceRepository.UpdateAsync(instance);

            // 记录工作流暂停历史
            var currentNodeId = instance.CurrentNodeId ?? 0;
            await CreateHistoryRecordAsync(instanceId, currentNodeId, 6, instance.InitiatorId, null, "工作流暂停");
        }

        /// <inheritdoc/>
        public async Task ResumeAsync(long instanceId)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 3) // 3 表示已挂起
            {
                throw new InvalidOperationException("只有已暂停的工作流实例才能恢复");
            }

            instance.Status = 1; // 1 表示运行中
            await _instanceRepository.UpdateAsync(instance);

            // 记录工作流恢复历史
            var currentNodeId = instance.CurrentNodeId ?? 0;
            await CreateHistoryRecordAsync(instanceId, currentNodeId, 7, instance.InitiatorId, null, "工作流恢复");
        }

        /// <inheritdoc/>
        public async Task TerminateAsync(long instanceId, string reason)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1 && instance.Status != 3) // 1 表示运行中, 3 表示已挂起
            {
                throw new InvalidOperationException("只有运行中或已暂停的工作流实例才能终止");
            }

            instance.Status = 4; // 4 表示已终止
            instance.EndTime = DateTime.Now;
            instance.Remark = reason;
            await _instanceRepository.UpdateAsync(instance);

            // 记录工作流手动终止历史
            var currentNodeId = instance.CurrentNodeId ?? 0;
            await CreateHistoryRecordAsync(instanceId, currentNodeId, 5, instance.InitiatorId, 2, $"工作流手动终止: {reason}");
        }

        /// <inheritdoc/>
        public async Task<HbtNodeResult> ExecuteNodeAsync(long instanceId, long nodeId, Dictionary<string, object>? variables = null)
        {
            // 获取实例
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            // 检查实例状态
            if (instance.Status != 1) // 1 表示运行中
            {
                throw new InvalidOperationException("工作流实例状态不正确");
            }

            // 获取节点
            var node = await _nodeRepository.GetByIdAsync(nodeId);
            if (node == null)
            {
                throw new InvalidOperationException("工作流节点不存在");
            }

            // 保存变量
            if (variables != null)
            {
                await SaveVariablesAsync(instanceId, variables, nodeId);
            }

            // 查找合适的执行器
            var nodeTemplate = await _nodeTemplateRepository.GetByIdAsync(node.NodeTemplateId);
            if (nodeTemplate == null)
            {
                throw new InvalidOperationException("节点模板不存在");
            }
            
            var executor = _nodeExecutors.FirstOrDefault(x => x.CanHandle(nodeTemplate.NodeType));
            if (executor == null)
            {
                throw new InvalidOperationException($"未找到节点类型 {nodeTemplate.NodeType} 的执行器");
            }

            // 记录节点开始执行历史
            await CreateHistoryRecordAsync(instanceId, nodeId, 2, instance.InitiatorId, null, $"开始执行节点: {nodeTemplate.NodeName}");

            // 执行节点
            var result = await executor.ExecuteAsync(instance, node, variables);

            // 记录节点执行结果历史
            var operationResult = result.Success ? 1 : 2; // 1=成功, 2=失败
            var operationComment = result.Success ? $"节点执行成功: {nodeTemplate.NodeName}" : $"节点执行失败: {nodeTemplate.NodeName} - {result.ErrorMessage}";
            await CreateHistoryRecordAsync(instanceId, nodeId, 2, instance.InitiatorId, operationResult, operationComment);

            // 如果执行成功且有输出变量，保存输出变量
            if (result.Success && result.OutputVariables != null)
            {
                await SaveVariablesAsync(instanceId, result.OutputVariables, nodeId);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<HbtTransitionResult> ExecuteTransitionAsync(long instanceId, long transitionId, Dictionary<string, object>? variables = null)
        {
            try
            {
                _dbContext.BeginTran();

                // 获取实例
                var instance = await _instanceRepository.GetByIdAsync(instanceId);
                if (instance == null)
                {
                    throw new InvalidOperationException("工作流实例不存在");
                }

                // 检查实例状态
                if (instance.Status != 1) // 1 表示运行中
                {
                    throw new InvalidOperationException("工作流实例状态不正确");
                }

                // 获取转换
                var transition = await _transitionRepository.GetByIdAsync(transitionId);
                if (transition == null)
                {
                    throw new InvalidOperationException("工作流转换不存在");
                }

                // 检查转换是否属于当前实例的工作流定义
                if (transition.DefinitionId != instance.DefinitionId)
                {
                    throw new InvalidOperationException("转换不属于当前工作流实例");
                }

                // 获取源活动和目标活动
                var sourceActivity = await _activityRepository.GetByIdAsync(transition.SourceActivityId);
                var targetActivity = await _activityRepository.GetByIdAsync(transition.TargetActivityId);
                if (sourceActivity == null || targetActivity == null)
                {
                    throw new InvalidOperationException("转换的源活动或目标活动不存在");
                }

                // 查找对应的节点
                var sourceNode = await _nodeRepository.GetFirstAsync(x => x.NodeTemplateId == sourceActivity.NodeTemplateId && x.InstanceId == instanceId);
                var targetNode = await _nodeRepository.GetFirstAsync(x => x.NodeTemplateId == targetActivity.NodeTemplateId && x.InstanceId == instanceId);
                if (sourceNode == null || targetNode == null)
                {
                    throw new InvalidOperationException("转换的源节点或目标节点不存在");
                }

                // 检查源节点是否为当前节点
                if (sourceNode.Id != instance.CurrentNodeId)
                {
                    throw new InvalidOperationException("转换的源节点不是当前节点");
                }

                // 记录转换开始历史
                await CreateHistoryRecordAsync(instanceId, sourceNode.Id, 8, instance.InitiatorId, null, $"开始执行转换: {sourceActivity.Name} -> {targetActivity.Name}");

                // 更新实例的当前节点
                instance.CurrentNodeId = targetNode.Id;
                await _instanceRepository.UpdateAsync(instance);

                // 保存变量
                if (variables != null)
                {
                    await SaveVariablesAsync(instanceId, variables, targetNode.Id);
                }

                // 记录转换完成历史
                await CreateHistoryRecordAsync(instanceId, targetNode.Id, 8, instance.InitiatorId, 1, $"转换执行完成: {sourceActivity.Name} -> {targetActivity.Name}");

                // 执行目标节点
                var result = await ExecuteNodeAsync(instanceId, targetNode.Id);
                if (!result.Success)
                {
                    throw new InvalidOperationException($"执行目标节点失败: {result.ErrorMessage}");
                }

                _dbContext.CommitTran();
                return new HbtTransitionResult { Success = true };
            }
            catch (Exception)
            {
                _dbContext.RollbackTran();
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<HbtInstanceStatusDto> GetStatusAsync(long instanceId)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            var currentNode = await _nodeRepository.GetByIdAsync(instance.CurrentNodeId);
            
            // 获取节点名称，通过NodeTemplate关联获取
            string currentNodeName = string.Empty;
            if (currentNode != null)
            {
                var nodeTemplate = await _nodeTemplateRepository.GetByIdAsync(currentNode.NodeTemplateId);
                currentNodeName = nodeTemplate?.NodeName ?? string.Empty;
            }

            return new HbtInstanceStatusDto
            {
                InstanceId = instance.Id,
                Status = instance.Status,
                CurrentNodeId = instance.CurrentNodeId ?? 0,
                CurrentNodeName = currentNodeName,
                AvailableOperations = GetAvailableOperations(instance),
            };
        }

        /// <inheritdoc/>
        public async Task<List<HbtTransitionDto>> GetAvailableTransitionsAsync(long instanceId)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                throw new InvalidOperationException("工作流实例不存在");
            }

            if (instance.Status != 1)
            {
                return new List<HbtTransitionDto>();
            }

            // 获取当前节点
            var currentNode = await _nodeRepository.GetByIdAsync(instance.CurrentNodeId);
            if (currentNode == null)
            {
                return new List<HbtTransitionDto>();
            }

            // 通过当前节点的NodeTemplateId查找相关的转换
            // 需要先找到与当前节点模板关联的活动
            var activities = await _activityRepository.GetListAsync(x => x.NodeTemplateId == currentNode.NodeTemplateId);
            if (!activities.Any())
            {
                return new List<HbtTransitionDto>();
            }

            // 通过活动ID查找转换
            var activityIds = activities.Select(x => x.Id).ToList();
            var transitions = await _transitionRepository.GetListAsync(x => activityIds.Contains(x.SourceActivityId));
            return transitions.Adapt<List<HbtTransitionDto>>();
        }

        /// <inheritdoc/>
        public async Task<Dictionary<string, object>> GetVariablesAsync(long instanceId, long? nodeId = null)
        {
            var variables = new Dictionary<string, object>();

            // 获取实例级变量
            var instanceVariables = await _variableRepository.GetListAsync(x =>
                x.InstanceId == instanceId &&
                x.Scope == 1); // 1 表示全局范围

            foreach (var variable in instanceVariables)
            {
                variables[variable.VariableName] = variable.VariableValue;
            }

            // 获取节点级变量
            if (nodeId.HasValue)
            {
                var nodeVariables = await _variableRepository.GetListAsync(x =>
                    x.InstanceId == instanceId &&
                    x.NodeId == nodeId &&
                    x.Scope == 2); // 2 表示节点范围

                foreach (var variable in nodeVariables)
                {
                    variables[variable.VariableName] = variable.VariableValue;
                }
            }

            return variables;
        }

        /// <inheritdoc/>
        public async Task SetVariablesAsync(long instanceId, Dictionary<string, object> variables, long? nodeId = null)
        {
            foreach (var kvp in variables)
            {
                var variable = new HbtVariable
                {
                    InstanceId = instanceId,
                    NodeId = nodeId,
                    VariableName = kvp.Key,
                    VariableValue = kvp.Value.ToString() ?? string.Empty,
                    Scope = nodeId.HasValue ? 2 : 1
                };

                await _variableRepository.CreateAsync(variable);
            }
        }

        private async Task SaveVariablesAsync(long instanceId, Dictionary<string, object> variables, long? nodeId = null)
        {
            foreach (var kvp in variables)
            {
                var variable = new HbtVariable
                {
                    InstanceId = instanceId,
                    NodeId = nodeId,
                    VariableName = kvp.Key,
                    VariableValue = kvp.Value.ToString() ?? string.Empty,
                    Scope = nodeId.HasValue ? 2 : 1
                };

                await _variableRepository.CreateAsync(variable);
            }
        }

        private List<string> GetAvailableOperations(HbtInstance instance)
        {
            var operations = new List<string>();

            switch (instance.Status)
            {
                case 1: // 1 表示运行中
                    operations.Add("suspend");
                    operations.Add("terminate");
                    break;

                case 3: // 3 表示已挂起
                    operations.Add("resume");
                    operations.Add("terminate");
                    break;

                case 0: // 0 表示草稿
                    operations.Add("submit");
                    operations.Add("delete");
                    break;
            }

            return operations;
        }

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        private async Task UpdateInstanceStatusAsync(long instanceId, int status)
        {
            var instance = await _instanceRepository.GetByIdAsync(instanceId);
            if (instance != null)
            {
                instance.Status = status;
                await _instanceRepository.UpdateAsync(instance);
            }
        }

        /// <summary>
        /// 从工作流配置创建节点
        /// </summary>
        private async Task<List<HbtNode>> CreateNodesFromWorkflowConfigAsync(HbtDefinition definition, long instanceId)
        {
            var nodes = new List<HbtNode>();
            
            if (string.IsNullOrEmpty(definition.WorkflowConfig))
            {
                throw new InvalidOperationException("工作流配置为空");
            }

            try
            {
                // 解析workflowConfig
                var config = System.Text.Json.JsonSerializer.Deserialize<WorkflowConfigModel>(definition.WorkflowConfig);
                if (config?.Nodes == null || !config.Nodes.Any())
                {
                    throw new InvalidOperationException("工作流配置中未找到节点信息");
                }

                // 创建节点映射，用于处理父子关系
                var nodeMap = new Dictionary<string, HbtNode>();

                // 第一遍：创建所有节点
                foreach (var nodeData in config.Nodes)
                {
                    // 根据配置创建或查找对应的节点模板
                    var nodeTemplateId = await GetOrCreateNodeTemplateFromConfigAsync(definition.Id, nodeData);
                    
                    var node = new HbtNode
                    {
                        InstanceId = instanceId,
                        NodeTemplateId = nodeTemplateId,
                        Status = 0, // 0 表示未开始
                        CreateTime = DateTime.Now
                    };

                    await _nodeRepository.CreateAsync(node);
                    nodeMap[nodeData.Id] = node;
                    nodes.Add(node);
                }

                // 第二遍：处理父子关系
                foreach (var nodeData in config.Nodes)
                {
                    if (nodeMap.TryGetValue(nodeData.Id, out HbtNode? node) && node != null)
                    {
                        // 根据边的信息确定父子关系
                        var parentNodeId = GetParentNodeIdFromEdges(config.Edges, nodeData.Id.ToString());
                        if (!string.IsNullOrEmpty(parentNodeId))
                        {
                            HbtNode? parentNode = null;
                            if (nodeMap.TryGetValue(parentNodeId, out parentNode) && parentNode != null)
                            {
                                node.ParentNodeId = parentNode.Id;
                                await _nodeRepository.UpdateAsync(node);
                            }
                        }
                    }
                }

                return nodes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"解析工作流配置失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 根据配置获取或创建节点模板
        /// </summary>
        private async Task<long> GetOrCreateNodeTemplateFromConfigAsync(long definitionId, dynamic nodeData)
        {
            try
            {
                var nodeName = GetNodeNameFromConfig(nodeData);
                var nodeType = GetNodeTypeFromConfig(nodeData);
                
                // 查找是否已存在相同的节点模板
                // 将动态变量提取到表达式树外部
                var nodeNameValue = nodeName;
                var nodeTypeValue = nodeType;
                var definitionIdValue = definitionId;
                
                // 先获取所有模板，然后在内存中过滤
                var allTemplates = await _nodeTemplateRepository.GetListAsync(x => x.DefinitionId == definitionIdValue);
                var existingTemplate = allTemplates.FirstOrDefault(x => 
                    x.NodeName == nodeNameValue && 
                    x.NodeType == nodeTypeValue);
                
                if (existingTemplate != null)
                {
                    return existingTemplate.Id;
                }
                
                // 创建新的节点模板
                var nodeConfigJson = System.Text.Json.JsonSerializer.Serialize(nodeData);
                var nodeTemplate = new HbtNodeTemplate
                {
                    NodeName = nodeName,
                    NodeType = nodeType,
                    DefinitionId = definitionId,
                    NodeConfig = nodeConfigJson,
                    OrderNum = 1,
                    IsEnabled = true
                };
                
                await _nodeTemplateRepository.CreateAsync(nodeTemplate);
                return nodeTemplate.Id;
            }
            catch
            {
                // 如果出错，返回默认节点模板ID
                return 1;
            }
        }

        /// <summary>
        /// 从节点配置中获取节点名称
        /// </summary>
        private string GetNodeNameFromConfig(dynamic nodeData)
        {
            try
            {
                // 尝试从不同位置获取节点名称
                if (nodeData.attrs?.label?.text != null)
                    return nodeData.attrs.label.text.ToString();
                
                if (nodeData.data?.name != null)
                    return nodeData.data.name.ToString();
                
                if (nodeData.data?.label != null)
                    return nodeData.data.label.ToString();
                
                // 根据节点类型返回默认名称
                var nodeType = GetNodeTypeFromConfig(nodeData);
                return nodeType switch
                {
                    1 => "开始节点",
                    2 => "任务节点", 
                    3 => "网关节点",
                    4 => "并行节点",
                    5 => "结束节点",
                    _ => "未知节点"
                };
            }
            catch
            {
                return "节点";
            }
        }

        /// <summary>
        /// 从节点配置中获取节点类型
        /// </summary>
        private int GetNodeTypeFromConfig(dynamic nodeData)
        {
            try
            {
                // 根据节点形状和属性判断类型
                var shape = nodeData.shape?.ToString()?.ToLower();
                var fill = nodeData.attrs?.body?.fill?.ToString()?.ToLower();
                
                // 开始节点：圆形，绿色填充
                if (shape == "circle" && fill == "#52c41a")
                    return 1;
                
                // 结束节点：圆形，红色填充
                if (shape == "circle" && fill == "#ff4d4f")
                    return 5;
                
                // 网关节点：多边形
                if (shape == "polygon")
                    return 3;
                
                // 任务节点：矩形
                if (shape == "rect")
                    return 2;
                
                // 默认返回任务节点
                return 2;
            }
            catch
            {
                return 2; // 默认任务节点
            }
        }

        /// <summary>
        /// 从边的信息中获取父节点ID
        /// </summary>
        private string GetParentNodeIdFromEdges(dynamic edges, string nodeId)
        {
            try
            {
                if (edges == null) return null;
                
                foreach (var edge in edges)
                {
                    if (edge.target?.cell == nodeId)
                    {
                        return edge.source?.cell?.ToString();
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 工作流配置模型
        /// </summary>
        private class WorkflowConfigModel
        {
            public string Version { get; set; } = string.Empty;
            public string Timestamp { get; set; } = string.Empty;
            public List<dynamic> Nodes { get; set; } = new();
            public List<dynamic> Edges { get; set; } = new();
            public dynamic Metadata { get; set; }
        }

        /// <summary>
        /// 创建历史记录
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operationResult">操作结果</param>
        /// <param name="operationComment">操作意见</param>
        private async Task CreateHistoryRecordAsync(long instanceId, long nodeId, int operationType, long operatorId, int? operationResult = null, string? operationComment = null)
        {
            try
            {
                var history = new HbtHistory
                {
                    InstanceId = instanceId,
                    NodeId = nodeId,
                    OperationType = operationType,
                    OperationResult = operationResult,
                    OperationComment = operationComment ?? string.Empty,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                };

                await _historyRepository.CreateAsync(history);
                _logger.Info($"创建历史记录成功: 实例ID={instanceId}, 节点ID={nodeId}, 操作类型={operationType}");
            }
            catch (Exception ex)
            {
                _logger.Error($"创建历史记录失败: {ex.Message}", ex);
                // 历史记录创建失败不应该影响主流程，所以只记录日志，不抛出异常
            }
        }
    }
}