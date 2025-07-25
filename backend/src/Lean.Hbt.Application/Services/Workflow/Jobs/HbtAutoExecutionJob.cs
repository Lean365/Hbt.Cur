#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAutoExecutionJob.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流自动执行任务
//===================================================================

using System.Text.Json;
using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Options;
using Quartz;

namespace Lean.Hbt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流自动执行任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class HbtAutoExecutionJob : IJob
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        private readonly IHbtRepositoryFactory _repositoryFactory;
        
        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly IHbtLogger _logger;
        
        /// <summary>
        /// 工作流引擎
        /// </summary>
        private readonly IHbtWorkflowEngine _workflowEngine;
        
        /// <summary>
        /// 工作流配置选项
        /// </summary>
        private readonly HbtWorkflowOptions _workflowOptions;

        /// <summary>
        /// 获取工作流实例仓储
        /// </summary>
        private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();

        /// <summary>
        /// 获取工作流定义仓储
        /// </summary>
        private IHbtRepository<HbtScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<HbtScheme>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory"></param>
        /// <param name="logger"></param>
        /// <param name="workflowEngine"></param>
        /// <param name="workflowOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowEngine">工作流引擎</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        public HbtAutoExecutionJob(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtWorkflowEngine workflowEngine,
            IOptions<HbtWorkflowOptions> workflowOptions)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
            _workflowEngine = workflowEngine;
            _workflowOptions = workflowOptions.Value;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context">任务上下文</param>
        /// <returns>异步任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行工作流自动执行任务");

                if (!_workflowOptions.AutoExecution.Enabled)
                {
                    _logger.Info("自动执行功能已禁用");
                    return;
                }

                // 查找需要自动执行的工作流实例
                var autoExecuteInstances = await InstanceRepository.GetListAsync(
                    i => i.Status == 1 && // 1 表示运行中
                         i.StartTime.HasValue && 
                         i.StartTime.Value.AddMinutes(_workflowOptions.AutoExecution.IntervalMinutes) <= DateTime.Now); // 超过配置的间隔时间

                if (!autoExecuteInstances.Any())
                {
                    _logger.Info("没有需要自动执行的工作流实例");
                    return;
                }

                _logger.Info($"找到 {autoExecuteInstances.Count} 个需要自动执行的工作流实例");

                foreach (var instance in autoExecuteInstances)
                {
                    await ProcessAutoExecuteInstanceAsync(instance);
                }

                _logger.Info("工作流自动执行任务执行完成");
            }
            catch (Exception ex)
            {
                _logger.Error("工作流自动执行任务执行失败", ex);
                throw; // 重新抛出异常，让Quartz处理
            }
        }

        /// <summary>
        /// 处理自动执行的工作流实例
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <returns>异步任务</returns>
        private async Task ProcessAutoExecuteInstanceAsync(HbtInstance instance)
        {
            try
            {
                _logger.Info($"开始自动执行工作流实例[{instance.Id}]");

                // 检查当前节点是否支持自动执行
                if (string.IsNullOrEmpty(instance.CurrentNodeId))
                {
                    _logger.Error($"工作流实例[{instance.Id}]当前节点ID为空");
                    return;
                }

                // 获取当前节点配置
                var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
                if (scheme == null)
                {
                    _logger.Error($"未找到工作流实例[{instance.Id}]对应的流程定义");
                    return;
                }

                // 解析工作流定义
                var workflowDefinition = JsonSerializer.Deserialize<WorkflowDefinition>(scheme.SchemeConfig);
                if (workflowDefinition == null)
                {
                    _logger.Error($"工作流实例[{instance.Id}]的流程定义解析失败");
                    return;
                }

                // 查找当前节点
                var currentNode = workflowDefinition.Nodes?.FirstOrDefault(n => n.Id == instance.CurrentNodeId);
                if (currentNode == null)
                {
                    _logger.Error($"工作流实例[{instance.Id}]未找到当前节点[{instance.CurrentNodeId}]");
                    return;
                }

                // 检查节点是否支持自动执行
                if (currentNode.AutoExecute != true)
                {
                    _logger.Info($"工作流实例[{instance.Id}]的当前节点[{instance.CurrentNodeId}]不支持自动执行");
                    return;
                }

                // 执行自动审批
                var approveDto = new HbtWorkflowApproveDto
                {
                    InstanceId = instance.Id,
                    NodeId = instance.CurrentNodeId,
                    OperType = 1, // 1 表示同意
                    OperOpinion = "自动执行",
                    OperData = JsonSerializer.Serialize(new Dictionary<string, object>())
                };
                
                var approveResult = await _workflowEngine.ApproveAsync(approveDto);

                if (approveResult)
                {
                    _logger.Info($"工作流实例[{instance.Id}]自动执行成功");
                }
                else
                {
                    _logger.Error($"工作流实例[{instance.Id}]自动执行失败");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"处理自动执行工作流实例[{instance.Id}]失败", ex);
            }
        }
    }

    /// <summary>
    /// 工作流定义
    /// </summary>
    public class WorkflowDefinition
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public string? Id { get; set; }
        
        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<WorkflowNode>? Nodes { get; set; }
        
        /// <summary>
        /// 工作流转换列表
        /// </summary>
        public List<WorkflowTransition>? Transitions { get; set; }
    }

    /// <summary>
    /// 工作流节点
    /// </summary>
    public class WorkflowNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string? Id { get; set; }
        
        /// <summary>
        /// 节点名称
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// 节点类型
        /// </summary>
        public string? Type { get; set; }
        
        /// <summary>
        /// 是否支持自动执行
        /// </summary>
        public bool? AutoExecute { get; set; }
        
        /// <summary>
        /// 节点配置
        /// </summary>
        public string? Config { get; set; }
    }

    /// <summary>
    /// 工作流转换
    /// </summary>
    public class WorkflowTransition
    {
        /// <summary>
        /// 转换ID
        /// </summary>
        public string? Id { get; set; }
        
        /// <summary>
        /// 起始节点ID
        /// </summary>
        public string? FromNodeId { get; set; }
        
        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string? ToNodeId { get; set; }
        
        /// <summary>
        /// 转换条件
        /// </summary>
        public string? Condition { get; set; }
    }
}