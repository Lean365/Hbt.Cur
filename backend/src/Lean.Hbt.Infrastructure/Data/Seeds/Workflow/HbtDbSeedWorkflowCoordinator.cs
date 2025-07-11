//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowCoordinator.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Workflow;

/// <summary>
/// 工作流种子数据协调器
/// </summary>
/// <remarks>
/// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
/// </remarks>
public class HbtDbSeedWorkflowCoordinator
{
    // 工作流实例状态常量
    private const int INSTANCE_STATUS_NOT_STARTED = 0;    // 未开始
    private const int INSTANCE_STATUS_PROCESSING = 1;     // 进行中
    private const int INSTANCE_STATUS_COMPLETED = 2;      // 已完成
    private const int INSTANCE_STATUS_TERMINATED = 3;     // 已终止
    private const int INSTANCE_STATUS_SUSPENDED = 4;      // 已挂起

    // 工作流节点状态常量
    private const int NODE_STATUS_NOT_STARTED = 0;      // 未开始
    private const int NODE_STATUS_PROCESSING = 1;       // 进行中
    private const int NODE_STATUS_COMPLETED = 2;        // 已完成
    private const int NODE_STATUS_CANCELLED = 3;        // 已取消
    private const int NODE_STATUS_SKIPPED = 4;          // 已跳过

    // 工作流任务状态常量
    private const int TASK_STATUS_PENDING = 0;          // 待处理
    private const int TASK_STATUS_PROCESSING = 1;       // 处理中
    private const int TASK_STATUS_APPROVED = 2;         // 已同意
    private const int TASK_STATUS_REJECTED = 3;         // 已拒绝
    private const int TASK_STATUS_RETURNED = 4;         // 已退回
    private const int TASK_STATUS_TRANSFERRED = 5;      // 已转办
    private const int TASK_STATUS_CANCELLED = 6;        // 已取消

    // 工作流定义状态常量
    private const int DEFINITION_STATUS_DISABLED = 0;   // 禁用
    private const int DEFINITION_STATUS_ENABLED = 1;    // 启用

    // 节点类型常量
    private const int NODE_TYPE_START = 1;              // 开始节点
    private const int NODE_TYPE_USER_TASK = 2;          // 用户任务节点
    private const int NODE_TYPE_GATEWAY = 3;            // 网关节点（条件判断）
    private const int NODE_TYPE_APPROVAL = 4;           // 审批节点（兼容旧版本）
    private const int NODE_TYPE_END = 5;                // 结束节点

    // 任务类型常量
    private const int TASK_TYPE_APPROVAL = 1;           // 审批任务

    private readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtForm> FormRepository => _repositoryFactory.GetWorkflowRepository<HbtForm>();
    private IHbtRepository<HbtDefinition> DefinitionRepository => _repositoryFactory.GetWorkflowRepository<HbtDefinition>();
    private IHbtRepository<HbtNodeTemplate> NodeTemplateRepository => _repositoryFactory.GetWorkflowRepository<HbtNodeTemplate>();
    private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();
    private IHbtRepository<HbtNode> NodeRepository => _repositoryFactory.GetWorkflowRepository<HbtNode>();
    private IHbtRepository<HbtProcessTask> TaskRepository => _repositoryFactory.GetWorkflowRepository<HbtProcessTask>();
    private IHbtRepository<HbtVariable> VariableRepository => _repositoryFactory.GetWorkflowRepository<HbtVariable>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedWorkflowCoordinator(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化员工请假流程全链路数据
    /// </summary>
    public async Task InitializeLeaveWorkflowAsync()
    {
        try
        {
            _logger.Info("开始初始化员工请假流程全链路数据...");

            // 1. 插入请假申请表单
            var formId = await InsertLeaveFormAsync();
            _logger.Info($"请假申请表单创建完成，FormId: {formId}");

            // 2. 插入请假流程定义
            var definitionId = await InsertLeaveDefinitionAsync(formId);
            _logger.Info($"请假流程定义创建完成，DefinitionId: {definitionId}");

            // 3. 插入节点模板
            var (startId, applyId, deptManagerId, conditionId, generalManagerId, adminProcessId, endId) = await InsertLeaveNodeTemplatesAsync(definitionId);
            _logger.Info($"节点模板创建完成，开始: {startId}, 员工申请: {applyId}, 部门经理审批: {deptManagerId}, 员工类型判断: {conditionId}, 总经理审批: {generalManagerId}, 总务处理: {adminProcessId}, 结束: {endId}");

            // 4. 插入流程实例
            var instanceId = await InsertLeaveInstanceAsync(definitionId);
            _logger.Info($"请假流程实例创建完成，InstanceId: {instanceId}");

            // 5. 插入流程节点
            var (startNodeId, applyNodeId, deptManagerNodeId, conditionNodeId, generalManagerNodeId, adminProcessNodeId, endNodeId) = await InsertLeaveNodesAsync(instanceId, startId, applyId, deptManagerId, conditionId, generalManagerId, adminProcessId, endId);
            _logger.Info($"流程节点创建完成，开始: {startNodeId}, 员工申请: {applyNodeId}, 部门经理审批: {deptManagerNodeId}, 员工类型判断: {conditionNodeId}, 总经理审批: {generalManagerNodeId}, 总务处理: {adminProcessNodeId}, 结束: {endNodeId}");

            // 6. 插入审批任务
            await InsertLeaveTaskAsync(instanceId, deptManagerNodeId);
            _logger.Info("审批任务创建完成");

            // 7. 插入流程变量
            await InsertLeaveVariablesAsync(instanceId);
            _logger.Info("流程变量创建完成");

            // 8. 更新实例当前节点
            await UpdateInstanceCurrentNodeAsync(instanceId, deptManagerNodeId);
            _logger.Info($"实例当前节点更新完成: {deptManagerNodeId}");

            // 9. 验证状态一致性
            await ValidateStatusConsistencyAsync(instanceId);
            _logger.Info("状态一致性验证完成");

            _logger.Info("员工请假流程全链路数据初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化员工请假流程失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 插入请假申请表单
    /// </summary>
    private async Task<long> InsertLeaveFormAsync()
    {
        var form = new HbtForm
        {
            FormName = "请假申请表单",
            FormDesc = "员工请假申请表单，与HbtLeave实体完全一致",
            FormConfig = @"{
                ""rule"": [
                    {
                        ""type"": ""input"",
                        ""field"": ""leaveNo"",
                        ""title"": ""请假编号"",
                        ""props"": {
                            ""type"": ""text"",
                            ""placeholder"": ""请输入请假编号"",
                            ""maxLength"": 20
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入请假编号"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""employeeId"",
                        ""title"": ""员工"",
                        ""props"": {
                            ""placeholder"": ""请选择员工"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择员工"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""leaveTypeId"",
                        ""title"": ""请假类型"",
                        ""props"": {
                            ""placeholder"": ""请选择请假类型"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择请假类型"" }
                        ]
                    },
                    {
                        ""type"": ""datePicker"",
                        ""field"": ""startTime"",
                        ""title"": ""请假开始时间"",
                        ""props"": {
                            ""type"": ""datetime"",
                            ""placeholder"": ""请选择开始时间"",
                            ""showTime"": true,
                            ""format"": ""YYYY-MM-DD HH:mm:ss""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择开始时间"" }
                        ]
                    },
                    {
                        ""type"": ""datePicker"",
                        ""field"": ""endTime"",
                        ""title"": ""请假结束时间"",
                        ""props"": {
                            ""type"": ""datetime"",
                            ""placeholder"": ""请选择结束时间"",
                            ""showTime"": true,
                            ""format"": ""YYYY-MM-DD HH:mm:ss""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择结束时间"" }
                        ]
                    },
                    {
                        ""type"": ""number"",
                        ""field"": ""leaveDays"",
                        ""title"": ""请假天数"",
                        ""props"": {
                            ""placeholder"": ""请输入请假天数"",
                            ""min"": 0,
                            ""max"": 365,
                            ""precision"": 2,
                            ""step"": 0.5
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入请假天数"" },
                            { ""type"": ""number"", ""min"": 0, ""message"": ""请假天数不能小于0"" }
                        ]
                    },
                    {
                        ""type"": ""textarea"",
                        ""field"": ""leaveReason"",
                        ""title"": ""请假原因"",
                        ""props"": {
                            ""placeholder"": ""请输入请假原因"",
                            ""rows"": 4,
                            ""maxLength"": 500,
                            ""showCount"": true
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入请假原因"" },
                            { ""max"": 500, ""message"": ""请假原因不能超过500个字符"" }
                        ]
                    }
                ],
                ""option"": {
                    ""submitText"": ""提交申请"",
                    ""resetText"": ""重置表单"",
                    ""labelWidth"": 120,
                    ""labelPosition"": ""right"",
                    ""size"": ""default"",
                    ""disabled"": false,
                    ""hideRequiredMark"": false,
                    ""showMessage"": true,
                    ""inlineMessage"": false,
                    ""statusIcon"": false,
                    ""validateOnRuleChange"": true
                }
            }",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };

        var existingForm = await FormRepository.GetFirstAsync(f => f.FormName == form.FormName);
        if (existingForm != null)
        {
            return existingForm.Id;
        }

        var formId = await FormRepository.CreateAsync(form);
        return formId;
    }

    /// <summary>
    /// 插入请假流程定义
    /// </summary>
    private async Task<long> InsertLeaveDefinitionAsync(long formId)
    {
        var definition = new HbtDefinition
        {
            WorkflowName = "员工请假流程",
            WorkflowCategory = "leave",
            WorkflowVersion = "A",
            FormId = formId,
            WorkflowConfig = @"{
                ""nodes"": [
                    {""id"": ""start"", ""type"": ""start"", ""name"": ""开始""},
                    {""id"": ""apply"", ""type"": ""userTask"", ""name"": ""员工申请""},
                    {""id"": ""deptManager"", ""type"": ""userTask"", ""name"": ""部门经理审批""},
                    {""id"": ""condition"", ""type"": ""gateway"", ""name"": ""请假类型判断""},
                    {""id"": ""generalManager"", ""type"": ""userTask"", ""name"": ""总经理审批""},
                    {""id"": ""adminProcess"", ""type"": ""userTask"", ""name"": ""总务处理""},
                    {""id"": ""end"", ""type"": ""end"", ""name"": ""结束""}
                ],
                ""edges"": [
                    {""source"": ""start"", ""target"": ""apply""},
                    {""source"": ""apply"", ""target"": ""deptManager""},
                    {""source"": ""deptManager"", ""target"": ""condition""},
                    {""source"": ""condition"", ""target"": ""generalManager"", ""condition"": ""leaveTypeId == 1 || leaveTypeId == 2""},
                    {""source"": ""condition"", ""target"": ""adminProcess"", ""condition"": ""leaveTypeId == 3 || leaveTypeId == 4""},
                    {""source"": ""generalManager"", ""target"": ""adminProcess""},
                    {""source"": ""adminProcess"", ""target"": ""end""}
                ]
            }",
            Status = DEFINITION_STATUS_ENABLED, // 启用 - 流程定义已启用，可以使用
            Remark = "员工请假审批流程定义 - 根据请假类型决定审批路径（事假/病假需要总经理审批，年假/调休直接总务处理）",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };

        var existingDefinition = await DefinitionRepository.GetFirstAsync(d => d.WorkflowName == definition.WorkflowName);
        if (existingDefinition != null)
        {
            return existingDefinition.Id;
        }

        var definitionId = await DefinitionRepository.CreateAsync(definition);
        return definitionId;
    }

    /// <summary>
    /// 插入请假流程节点模板
    /// </summary>
    private async Task<(long startId, long applyId, long deptManagerId, long conditionId, long generalManagerId, long adminProcessId, long endId)> InsertLeaveNodeTemplatesAsync(long definitionId)
    {
        var templates = new[]
        {
            new HbtNodeTemplate
            {
                NodeName = "开始",
                NodeType = NODE_TYPE_START,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"start\",\"name\":\"开始节点\"}",
                OrderNum = 1,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "员工申请",
                NodeType = NODE_TYPE_USER_TASK,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"userTask\",\"name\":\"员工申请\",\"assigneeType\":\"initiator\",\"formKey\":\"leave_apply_form\"}",
                OrderNum = 2,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "部门经理审批",
                NodeType = NODE_TYPE_USER_TASK,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"userTask\",\"name\":\"部门经理审批\",\"assigneeType\":\"role\",\"assigneeValue\":\"dept_manager\",\"formKey\":\"leave_approval_form\"}",
                OrderNum = 3,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "请假类型判断",
                NodeType = NODE_TYPE_GATEWAY,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"gateway\",\"name\":\"请假类型判断\",\"conditionType\":\"expression\",\"conditionExpression\":\"leaveTypeId\"}",
                OrderNum = 4,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "总经理审批",
                NodeType = NODE_TYPE_USER_TASK,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"userTask\",\"name\":\"总经理审批\",\"assigneeType\":\"role\",\"assigneeValue\":\"general_manager\",\"formKey\":\"leave_approval_form\"}",
                OrderNum = 5,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "总务处理",
                NodeType = NODE_TYPE_USER_TASK,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"userTask\",\"name\":\"总务处理\",\"assigneeType\":\"role\",\"assigneeValue\":\"admin_staff\",\"formKey\":\"leave_admin_form\"}",
                OrderNum = 6,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNodeTemplate
            {
                NodeName = "结束",
                NodeType = NODE_TYPE_END,
                DefinitionId = definitionId,
                NodeConfig = "{\"type\":\"end\",\"name\":\"结束节点\"}",
                OrderNum = 7,
                IsEnabled = true,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        var startId = await InsertNodeTemplateAsync(templates[0]);
        var applyId = await InsertNodeTemplateAsync(templates[1]);
        var deptManagerId = await InsertNodeTemplateAsync(templates[2]);
        var conditionId = await InsertNodeTemplateAsync(templates[3]);
        var generalManagerId = await InsertNodeTemplateAsync(templates[4]);
        var adminProcessId = await InsertNodeTemplateAsync(templates[5]);
        var endId = await InsertNodeTemplateAsync(templates[6]);

        return (startId, applyId, deptManagerId, conditionId, generalManagerId, adminProcessId, endId);
    }

    /// <summary>
    /// 插入单个节点模板
    /// </summary>
    private async Task<long> InsertNodeTemplateAsync(HbtNodeTemplate template)
    {
        var existing = await NodeTemplateRepository.GetFirstAsync(x =>
            x.DefinitionId == template.DefinitionId &&
            x.NodeName == template.NodeName &&
            x.NodeType == template.NodeType);

        if (existing != null)
        {
            return existing.Id;
        }

        var templateId = await NodeTemplateRepository.CreateAsync(template);
        return templateId;
    }

    /// <summary>
    /// 插入请假流程实例
    /// </summary>
    private async Task<long> InsertLeaveInstanceAsync(long definitionId)
    {
        var instance = new HbtInstance
        {
            DefinitionId = definitionId,
            InstanceName = "员工请假申请",
            BusinessKey = $"LEAVE_{DateTime.Now:yyyyMMddHHmmss}",
            Status = INSTANCE_STATUS_PROCESSING, // 进行中 - 流程实例正在执行中
            StartTime = DateTime.Now.AddDays(-2),
            EndTime = null, // 还未结束
            Priority = 2, // 中等优先级
            InitiatorId = 1, // 假设发起人ID为1
            FormData = @"{
                ""leaveNo"": ""LEAVE20240120001"",
                ""employeeId"": 1,
                ""leaveTypeId"": 1,
                ""startTime"": ""2024-01-20 09:00:00"",
                ""endTime"": ""2024-01-22 18:00:00"",
                ""leaveDays"": 2.5,
                ""leaveReason"": ""感冒发烧，需要休息调养""
            }",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now.AddDays(-2),
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now.AddDays(-2)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            return existingInstance.Id;
        }

        var instanceId = await InstanceRepository.CreateAsync(instance);
        return instanceId;
    }

    /// <summary>
    /// 插入请假流程节点
    /// </summary>
    private async Task<(long startId, long applyId, long deptManagerId, long conditionId, long generalManagerId, long adminProcessId, long endId)> InsertLeaveNodesAsync(long instanceId, long startTemplateId, long applyTemplateId, long deptManagerTemplateId, long conditionTemplateId, long generalManagerTemplateId, long adminProcessTemplateId, long endTemplateId)
    {
        var nodes = new[]
        {
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = startTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED, // 已完成 - 开始节点已经执行完成
                StartTime = DateTime.Now.AddDays(-2),
                EndTime = DateTime.Now.AddDays(-2).AddMinutes(1),
                NodeResult = "{\"result\":\"success\",\"message\":\"开始节点执行成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(1)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = applyTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED, // 已完成 - 员工申请已完成
                StartTime = DateTime.Now.AddDays(-2).AddMinutes(1),
                EndTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                NodeResult = "{\"result\":\"success\",\"message\":\"员工申请提交成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(1),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = deptManagerTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_PROCESSING, // 进行中 - 部门经理审批正在等待处理
                StartTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                EndTime = null,
                NodeResult = null, // 还未有结果
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = conditionTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED, // 未开始 - 条件判断节点还未开始
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = generalManagerTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED, // 未开始 - 总经理审批还未开始
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = adminProcessTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED, // 未开始 - 总务处理还未开始
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = endTemplateId,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED, // 未开始 - 结束节点还未开始
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            }
        };

        var startId = await InsertNodeAsync(nodes[0]);
        var applyId = await InsertNodeAsync(nodes[1]);
        var deptManagerId = await InsertNodeAsync(nodes[2]);
        var conditionId = await InsertNodeAsync(nodes[3]);
        var generalManagerId = await InsertNodeAsync(nodes[4]);
        var adminProcessId = await InsertNodeAsync(nodes[5]);
        var endId = await InsertNodeAsync(nodes[6]);

        return (startId, applyId, deptManagerId, conditionId, generalManagerId, adminProcessId, endId);
    }

    /// <summary>
    /// 插入单个节点
    /// </summary>
    private async Task<long> InsertNodeAsync(HbtNode node)
    {
        var existing = await NodeRepository.GetFirstAsync(x =>
            x.InstanceId == node.InstanceId &&
            x.NodeTemplateId == node.NodeTemplateId);

        if (existing != null)
        {
            return existing.Id;
        }

        var nodeId = await NodeRepository.CreateAsync(node);
        return nodeId;
    }

    /// <summary>
    /// 插入请假审批任务
    /// </summary>
    private async Task InsertLeaveTaskAsync(long instanceId, long approvalNodeId)
    {
        var task = new HbtProcessTask
        {
            TaskName = "部门经理审批",
            InstanceId = instanceId,
            NodeId = approvalNodeId,
            TaskType = TASK_TYPE_APPROVAL, // 审批任务
            Status = TASK_STATUS_PROCESSING, // 处理中 - 任务已分配，等待处理
            AssigneeId = 1, // 部门经理用户ID
            Priority = 1, // 普通优先级
            Comment = null, // 还未有处理意见
            CompleteTime = null, // 还未完成
            DueTime = DateTime.Now.AddDays(3), // 3天后到期
            ReminderTime = DateTime.Now.AddDays(1), // 1天后提醒
            Remark = "张三的请假申请需要审批",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };

        var existingTask = await TaskRepository.GetFirstAsync(x =>
            x.InstanceId == task.InstanceId &&
            x.NodeId == task.NodeId &&
            x.AssigneeId == task.AssigneeId);

        if (existingTask == null)
        {
            await TaskRepository.CreateAsync(task);
        }
    }

    /// <summary>
    /// 插入请假流程变量
    /// </summary>
    private async Task InsertLeaveVariablesAsync(long instanceId)
    {
        var variables = new[]
        {
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "leaveNo",
                VariableValue = "LEAVE20240120001",
                VariableType = "string",
                Remark = "请假编号",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "employeeId",
                VariableValue = "1",
                VariableType = "long",
                Remark = "员工ID",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "leaveTypeId",
                VariableValue = "1",
                VariableType = "long",
                Remark = "请假类型ID",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "startTime",
                VariableValue = "2024-01-20 09:00:00",
                VariableType = "datetime",
                Remark = "请假开始时间",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "endTime",
                VariableValue = "2024-01-22 18:00:00",
                VariableType = "datetime",
                Remark = "请假结束时间",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "leaveDays",
                VariableValue = "2.5",
                VariableType = "decimal",
                Remark = "请假天数",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "leaveReason",
                VariableValue = "感冒发烧，需要休息调养",
                VariableType = "string",
                Remark = "请假原因",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = instanceId,
                VariableName = "status",
                VariableValue = "1",
                VariableType = "int",
                Remark = "请假状态(1=待审批 2=已批准 3=已拒绝 4=已取消)",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var variable in variables)
        {
            var existing = await VariableRepository.GetFirstAsync(x =>
                x.InstanceId == variable.InstanceId &&
                x.VariableName == variable.VariableName);

            if (existing == null)
            {
                await VariableRepository.CreateAsync(variable);
            }
        }
    }

    /// <summary>
    /// 更新实例当前节点
    /// </summary>
    private async Task UpdateInstanceCurrentNodeAsync(long instanceId, long currentNodeId)
    {
        var instance = await InstanceRepository.GetByIdAsync(instanceId);
        if (instance != null)
        {
            instance.CurrentNodeId = currentNodeId;
            await InstanceRepository.UpdateAsync(instance);
        }
    }

    /// <summary>
    /// 验证状态一致性
    /// </summary>
    private async Task ValidateStatusConsistencyAsync(long instanceId)
    {
        try
        {
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance == null)
            {
                _logger.Warn($"实例 {instanceId} 不存在，跳过状态验证");
                return;
            }

            // 验证实例状态
            if (instance.Status != INSTANCE_STATUS_PROCESSING)
            {
                _logger.Warn($"实例 {instanceId} 状态为 {instance.Status}，期望为 {INSTANCE_STATUS_PROCESSING}");
            }

            // 验证节点状态
            var nodes = await NodeRepository.GetListAsync(x => x.InstanceId == instance.Id);
            foreach (var node in nodes)
            {
                if (node.Status != NODE_STATUS_COMPLETED && 
                    node.Status != NODE_STATUS_PROCESSING && 
                    node.Status != NODE_STATUS_NOT_STARTED)
                {
                    _logger.Warn($"节点 {node.Id} 状态为 {node.Status}，不在预期范围内");
                }
            }

            // 验证任务状态
            var tasks = await TaskRepository.GetListAsync(x => x.InstanceId == instance.Id);
            foreach (var task in tasks)
            {
                if (task.Status != TASK_STATUS_PROCESSING && 
                    task.Status != TASK_STATUS_PENDING)
                {
                    _logger.Warn($"任务 {task.Id} 状态为 {task.Status}，不在预期范围内");
                }
            }

            _logger.Info($"实例 {instanceId} 状态验证完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"验证实例 {instanceId} 状态时发生错误: {ex.Message}", ex);
            // 不抛出异常，只记录警告，避免阻止初始化流程
        }
    }

    /// <summary>
    /// 创建多个流程实例用于演示不同状态
    /// </summary>
    public async Task CreateMultipleWorkflowInstancesAsync()
    {
        try
        {
            _logger.Info("开始创建多个工作流实例用于演示...");

            // 获取已存在的流程定义
            var definition = await DefinitionRepository.GetFirstAsync(d => d.WorkflowName == "员工请假流程");
            if (definition == null)
            {
                _logger.Warn("未找到员工请假流程定义，请先运行 InitializeLeaveWorkflowAsync");
                return;
            }

            // 创建不同状态的实例
            await CreateInstanceInDraftStatusAsync(definition.Id);
            await CreateInstanceInRunningStatusAsync(definition.Id);
            await CreateInstanceInCompletedStatusAsync(definition.Id);
            await CreateInstanceInSuspendedStatusAsync(definition.Id);

            _logger.Info("多个工作流实例创建完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"创建多个工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建草稿状态的实例
    /// </summary>
    private async Task CreateInstanceInDraftStatusAsync(long definitionId)
    {
        var instance = new HbtInstance
        {
            InstanceName = "李四的请假申请（草稿）",
            DefinitionId = definitionId,
            CurrentNodeId = null,
            InitiatorId = 2,
            BusinessKey = "LEAVE-2024-002",
            FormData = "{\"reason\":\"家中有事\",\"startTime\":\"2024-03-05 09:00:00\",\"endTime\":\"2024-03-06 18:00:00\",\"type\":1}",
            Status = INSTANCE_STATUS_NOT_STARTED, // 未开始状态
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = null,
            Priority = 1, // 低优先级
            Remark = "李四的请假申请实例 - 事假2天（草稿）",
            CreateBy = "lisi",
            CreateTime = DateTime.Now.AddDays(-1),
            UpdateBy = "lisi",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance == null)
        {
            await InstanceRepository.CreateAsync(instance);
            _logger.Info($"草稿状态实例创建完成: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 创建运行中状态的实例
    /// </summary>
    private async Task CreateInstanceInRunningStatusAsync(long definitionId)
    {
        var instance = new HbtInstance
        {
            InstanceName = "王五的请假申请（运行中）",
            DefinitionId = definitionId,
            CurrentNodeId = null, // 稍后更新
            InitiatorId = 3,
            BusinessKey = "LEAVE-2024-003",
            FormData = "{\"reason\":\"年假休息\",\"startTime\":\"2024-03-10 09:00:00\",\"endTime\":\"2024-03-14 18:00:00\",\"type\":3}",
            Status = INSTANCE_STATUS_PROCESSING, // 进行中状态
            StartTime = DateTime.Now.AddHours(-2),
            EndTime = null,
            Priority = 2, // 中优先级
            Remark = "王五的请假申请实例 - 年假5天（运行中）",
            CreateBy = "wangwu",
            CreateTime = DateTime.Now.AddHours(-2),
            UpdateBy = "wangwu",
            UpdateTime = DateTime.Now.AddHours(-2)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance == null)
        {
            var instanceId = await InstanceRepository.CreateAsync(instance);
            
            // 创建对应的节点和任务
            await CreateNodesAndTasksForRunningInstanceAsync(instanceId, definitionId);
            
            _logger.Info($"运行中状态实例创建完成: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 为运行中实例创建节点和任务
    /// </summary>
    private async Task CreateNodesAndTasksForRunningInstanceAsync(long instanceId, long definitionId)
    {
        // 获取节点模板 - 使用完整的流程节点
        var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == definitionId);
        var startTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_START);
        var applyTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "员工申请");
        var deptManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "部门经理审批");
        var conditionTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "请假类型判断");
        var generalManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总经理审批");
        var adminProcessTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总务处理");
        var endTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_END);

        if (startTemplate != null && applyTemplate != null && deptManagerTemplate != null && 
            conditionTemplate != null && generalManagerTemplate != null && adminProcessTemplate != null && endTemplate != null)
        {
            // 创建完整的节点链
            var startNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = startTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddHours(-2),
                EndTime = DateTime.Now.AddHours(-2).AddMinutes(1),
                NodeResult = "{\"result\":\"success\",\"message\":\"开始节点执行成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2).AddMinutes(1)
            };

            var applyNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = applyTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddHours(-2).AddMinutes(1),
                EndTime = DateTime.Now.AddHours(-2).AddMinutes(2),
                NodeResult = "{\"result\":\"success\",\"message\":\"员工申请提交成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2).AddMinutes(1),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2).AddMinutes(2)
            };

            var deptManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = deptManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_PROCESSING, // 当前正在处理
                StartTime = DateTime.Now.AddHours(-2).AddMinutes(2),
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2).AddMinutes(2)
            };

            var conditionNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = conditionTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2)
            };

            var generalManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = generalManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2)
            };

            var adminProcessNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = adminProcessTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2)
            };

            var endNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = endTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddHours(-2)
            };

            // 创建所有节点
            var startNodeId = await NodeRepository.CreateAsync(startNode);
            var applyNodeId = await NodeRepository.CreateAsync(applyNode);
            var deptManagerNodeId = await NodeRepository.CreateAsync(deptManagerNode);
            var conditionNodeId = await NodeRepository.CreateAsync(conditionNode);
            var generalManagerNodeId = await NodeRepository.CreateAsync(generalManagerNode);
            var adminProcessNodeId = await NodeRepository.CreateAsync(adminProcessNode);
            var endNodeId = await NodeRepository.CreateAsync(endNode);

            // 创建当前审批任务
            var task = new HbtProcessTask
            {
                TaskName = "部门经理审批",
                InstanceId = instanceId,
                NodeId = deptManagerNodeId,
                TaskType = TASK_TYPE_APPROVAL,
                Status = TASK_STATUS_PROCESSING,
                AssigneeId = 1,
                Priority = 1,
                Comment = null,
                CompleteTime = null,
                DueTime = DateTime.Now.AddDays(3),
                ReminderTime = DateTime.Now.AddDays(1),
                Remark = "王五的请假申请需要审批",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            };

            await TaskRepository.CreateAsync(task);

            // 更新实例当前节点
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance != null)
            {
                instance.CurrentNodeId = deptManagerNodeId;
                await InstanceRepository.UpdateAsync(instance);
            }
        }
    }

    /// <summary>
    /// 创建已完成状态的实例
    /// </summary>
    private async Task CreateInstanceInCompletedStatusAsync(long definitionId)
    {
        var instance = new HbtInstance
        {
            InstanceName = "赵六的请假申请（已完成）",
            DefinitionId = definitionId,
            CurrentNodeId = null, // 稍后更新
            InitiatorId = 4,
            BusinessKey = "LEAVE-2024-004",
            FormData = "{\"reason\":\"调休\",\"startTime\":\"2024-03-15 09:00:00\",\"endTime\":\"2024-03-15 18:00:00\",\"type\":4}",
            Status = INSTANCE_STATUS_COMPLETED, // 已完成状态
            StartTime = DateTime.Now.AddDays(-5),
            EndTime = DateTime.Now.AddDays(-3),
            Priority = 1, // 低优先级
            Remark = "赵六的请假申请实例 - 调休1天（已完成）",
            CreateBy = "zhaoliu",
            CreateTime = DateTime.Now.AddDays(-5),
            UpdateBy = "zhaoliu",
            UpdateTime = DateTime.Now.AddDays(-3)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance == null)
        {
            var instanceId = await InstanceRepository.CreateAsync(instance);
            
            // 为已完成实例创建节点和任务
            await CreateNodesAndTasksForCompletedInstanceAsync(instanceId, definitionId);
            
            _logger.Info($"已完成状态实例创建完成: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 为已完成实例创建节点和任务
    /// </summary>
    private async Task CreateNodesAndTasksForCompletedInstanceAsync(long instanceId, long definitionId)
    {
        // 获取节点模板 - 使用完整的流程节点
        var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == definitionId);
        var startTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_START);
        var applyTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "员工申请");
        var deptManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "部门经理审批");
        var conditionTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "请假类型判断");
        var generalManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总经理审批");
        var adminProcessTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总务处理");
        var endTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_END);

        if (startTemplate != null && applyTemplate != null && deptManagerTemplate != null && 
            conditionTemplate != null && generalManagerTemplate != null && adminProcessTemplate != null && endTemplate != null)
        {
            // 创建完整的节点链 - 模拟普通员工流程（跳过总经理审批）
            var startNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = startTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-5),
                EndTime = DateTime.Now.AddDays(-5).AddMinutes(1),
                NodeResult = "{\"result\":\"success\",\"message\":\"开始节点执行成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-5).AddMinutes(1)
            };

            var applyNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = applyTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-5).AddMinutes(1),
                EndTime = DateTime.Now.AddDays(-5).AddMinutes(2),
                NodeResult = "{\"result\":\"success\",\"message\":\"员工申请提交成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5).AddMinutes(1),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-5).AddMinutes(2)
            };

            var deptManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = deptManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-5).AddMinutes(2),
                EndTime = DateTime.Now.AddDays(-4).AddHours(2),
                NodeResult = "{\"result\":\"approved\",\"message\":\"部门经理审批通过\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-4).AddHours(2)
            };

            var conditionNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = conditionTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-4).AddHours(2),
                EndTime = DateTime.Now.AddDays(-4).AddHours(2).AddMinutes(1),
                NodeResult = "{\"result\":\"employee\",\"message\":\"普通员工，跳过总经理审批\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-4).AddHours(2).AddMinutes(1)
            };

            var generalManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = generalManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_SKIPPED, // 跳过
                StartTime = null,
                EndTime = null,
                NodeResult = "{\"result\":\"skipped\",\"message\":\"普通员工跳过总经理审批\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-4).AddHours(2).AddMinutes(1)
            };

            var adminProcessNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = adminProcessTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-4).AddHours(2).AddMinutes(1),
                EndTime = DateTime.Now.AddDays(-3).AddHours(1),
                NodeResult = "{\"result\":\"success\",\"message\":\"总务处理完成\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3).AddHours(1)
            };

            var endNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = endTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-3).AddHours(1),
                EndTime = DateTime.Now.AddDays(-3),
                NodeResult = "{\"result\":\"success\",\"message\":\"流程结束\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3)
            };

            // 创建所有节点
            var startNodeId = await NodeRepository.CreateAsync(startNode);
            var applyNodeId = await NodeRepository.CreateAsync(applyNode);
            var deptManagerNodeId = await NodeRepository.CreateAsync(deptManagerNode);
            var conditionNodeId = await NodeRepository.CreateAsync(conditionNode);
            var generalManagerNodeId = await NodeRepository.CreateAsync(generalManagerNode);
            var adminProcessNodeId = await NodeRepository.CreateAsync(adminProcessNode);
            var endNodeId = await NodeRepository.CreateAsync(endNode);

            // 创建任务
            var task = new HbtProcessTask
            {
                TaskName = "部门经理审批",
                InstanceId = instanceId,
                NodeId = deptManagerNodeId,
                TaskType = TASK_TYPE_APPROVAL,
                Status = TASK_STATUS_APPROVED,
                AssigneeId = 1,
                Priority = 1,
                Comment = "同意调休申请",
                CompleteTime = DateTime.Now.AddDays(-4).AddHours(2),
                DueTime = DateTime.Now.AddDays(-2),
                ReminderTime = DateTime.Now.AddDays(-3),
                Remark = "赵六的请假申请已审批通过",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-4).AddHours(2)
            };

            await TaskRepository.CreateAsync(task);

            // 更新实例当前节点
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance != null)
            {
                instance.CurrentNodeId = endNodeId;
                await InstanceRepository.UpdateAsync(instance);
            }
        }
    }

    /// <summary>
    /// 创建已挂起状态的实例
    /// </summary>
    private async Task CreateInstanceInSuspendedStatusAsync(long definitionId)
    {
        var instance = new HbtInstance
        {
            InstanceName = "孙七的请假申请（已挂起）",
            DefinitionId = definitionId,
            CurrentNodeId = null, // 稍后更新
            InitiatorId = 5,
            BusinessKey = "LEAVE-2024-005",
            FormData = "{\"reason\":\"病假\",\"startTime\":\"2024-03-20 09:00:00\",\"endTime\":\"2024-03-25 18:00:00\",\"type\":2}",
            Status = INSTANCE_STATUS_SUSPENDED, // 已挂起状态
            StartTime = DateTime.Now.AddDays(-3),
            EndTime = null,
            Priority = 3, // 高优先级
            Remark = "孙七的请假申请实例 - 病假6天（已挂起）",
            CreateBy = "sunqi",
            CreateTime = DateTime.Now.AddDays(-3),
            UpdateBy = "sunqi",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance == null)
        {
            var instanceId = await InstanceRepository.CreateAsync(instance);
            
            // 为已挂起实例创建节点和任务
            await CreateNodesAndTasksForSuspendedInstanceAsync(instanceId, definitionId);
            
            _logger.Info($"已挂起状态实例创建完成: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 为已挂起实例创建节点和任务
    /// </summary>
    private async Task CreateNodesAndTasksForSuspendedInstanceAsync(long instanceId, long definitionId)
    {
        // 获取节点模板 - 使用完整的流程节点
        var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == definitionId);
        var startTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_START);
        var applyTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "员工申请");
        var deptManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "部门经理审批");
        var conditionTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "请假类型判断");
        var generalManagerTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总经理审批");
        var adminProcessTemplate = nodeTemplates.FirstOrDefault(x => x.NodeName == "总务处理");
        var endTemplate = nodeTemplates.FirstOrDefault(x => x.NodeType == NODE_TYPE_END);

        if (startTemplate != null && applyTemplate != null && deptManagerTemplate != null && 
            conditionTemplate != null && generalManagerTemplate != null && adminProcessTemplate != null && endTemplate != null)
        {
            // 创建完整的节点链 - 模拟管理职员工流程（需要总经理审批）
            var startNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = startTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-3),
                EndTime = DateTime.Now.AddDays(-3).AddMinutes(1),
                NodeResult = "{\"result\":\"success\",\"message\":\"开始节点执行成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(1)
            };

            var applyNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = applyTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-3).AddMinutes(1),
                EndTime = DateTime.Now.AddDays(-3).AddMinutes(2),
                NodeResult = "{\"result\":\"success\",\"message\":\"员工申请提交成功\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(1),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(2)
            };

            var deptManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = deptManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED, // 已完成但实例已挂起
                StartTime = DateTime.Now.AddDays(-3).AddMinutes(2),
                EndTime = DateTime.Now.AddDays(-1).AddHours(1),
                NodeResult = "{\"result\":\"approved\",\"message\":\"部门经理审批通过\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-1).AddHours(1)
            };

            var conditionNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = conditionTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_COMPLETED,
                StartTime = DateTime.Now.AddDays(-1).AddHours(1),
                EndTime = DateTime.Now.AddDays(-1).AddHours(1).AddMinutes(1),
                NodeResult = "{\"result\":\"management\",\"message\":\"管理职员工，需要总经理审批\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-1).AddHours(1).AddMinutes(1)
            };

            var generalManagerNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = generalManagerTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_PROCESSING, // 进行中但实例已挂起
                StartTime = DateTime.Now.AddDays(-1).AddHours(1).AddMinutes(1),
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-1).AddHours(1).AddMinutes(1)
            };

            var adminProcessNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = adminProcessTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3)
            };

            var endNode = new HbtNode
            {
                InstanceId = instanceId,
                NodeTemplateId = endTemplate.Id,
                ParentNodeId = null,
                Status = NODE_STATUS_NOT_STARTED,
                StartTime = null,
                EndTime = null,
                NodeResult = null,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-3)
            };

            // 创建所有节点
            var startNodeId = await NodeRepository.CreateAsync(startNode);
            var applyNodeId = await NodeRepository.CreateAsync(applyNode);
            var deptManagerNodeId = await NodeRepository.CreateAsync(deptManagerNode);
            var conditionNodeId = await NodeRepository.CreateAsync(conditionNode);
            var generalManagerNodeId = await NodeRepository.CreateAsync(generalManagerNode);
            var adminProcessNodeId = await NodeRepository.CreateAsync(adminProcessNode);
            var endNodeId = await NodeRepository.CreateAsync(endNode);

            // 创建任务
            var task = new HbtProcessTask
            {
                TaskName = "总经理审批",
                InstanceId = instanceId,
                NodeId = generalManagerNodeId,
                TaskType = TASK_TYPE_APPROVAL,
                Status = TASK_STATUS_PROCESSING, // 处理中但实例已挂起
                AssigneeId = 2, // 总经理用户ID
                Priority = 3, // 高优先级
                Comment = null,
                CompleteTime = null,
                DueTime = DateTime.Now.AddDays(2),
                ReminderTime = DateTime.Now.AddDays(1),
                Remark = "孙七的请假申请需要总经理审批（已挂起）",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-1)
            };

            await TaskRepository.CreateAsync(task);

            // 更新实例当前节点
            var instance = await InstanceRepository.GetByIdAsync(instanceId);
            if (instance != null)
            {
                instance.CurrentNodeId = generalManagerNodeId;
                await InstanceRepository.UpdateAsync(instance);
            }
        }
    }
} 