//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowCoordinator.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 工作流种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Workflow;

/// <summary>
/// 工作流种子数据协调器
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 基于简化工作流实体架构
/// </remarks>
public class HbtDbSeedWorkflowCoordinator
{
    // 工作流实例状态常量
    private const int INSTANCE_STATUS_DRAFT = 0;         // 草稿
    private const int INSTANCE_STATUS_RUNNING = 1;       // 运行中
    private const int INSTANCE_STATUS_COMPLETED = 2;     // 已完成
    private const int INSTANCE_STATUS_SUSPENDED = 3;     // 已暂停
    private const int INSTANCE_STATUS_TERMINATED = 4;    // 已终止

    // 表单状态常量
    private const int FORM_STATUS_DRAFT = 0;             // 草稿
    private const int FORM_STATUS_PUBLISHED = 1;         // 已发布
    private const int FORM_STATUS_DISABLED = 2;          // 已作废

    // 流程定义状态常量
    private const int SCHEME_STATUS_DRAFT = 0;           // 草稿
    private const int SCHEME_STATUS_PUBLISHED = 1;       // 已发布
    private const int SCHEME_STATUS_DISABLED = 2;        // 已停用

    // 操作类型常量
    private const int OPERATION_TYPE_SUBMIT = 1;         // 提交
    private const int OPERATION_TYPE_APPROVE = 2;        // 审批
    private const int OPERATION_TYPE_REJECT = 3;         // 驳回
    private const int OPERATION_TYPE_TRANSFER = 4;       // 转办
    private const int OPERATION_TYPE_TERMINATE = 5;      // 终止
    private const int OPERATION_TYPE_WITHDRAW = 6;       // 撤回

    // 流转类型常量
    private const int TRANSITION_TYPE_ENTER = 1;         // 进入
    private const int TRANSITION_TYPE_LEAVE = 2;         // 离开
    private const int TRANSITION_TYPE_EXECUTE = 3;       // 执行

    // 流转结果常量
    private const int TRANSITION_RESULT_SUCCESS = 1;     // 成功
    private const int TRANSITION_RESULT_FAILED = 2;      // 失败
    private const int TRANSITION_RESULT_SKIPPED = 3;     // 跳过

    // 节点类型常量
    private const int NODE_TYPE_START = 1;               // 开始节点
    private const int NODE_TYPE_USER_TASK = 2;           // 用户任务
    private const int NODE_TYPE_GATEWAY = 3;             // 网关
    private const int NODE_TYPE_END = 4;                 // 结束节点

    // 流转状态常量
    private const int TRANS_STATE_SUCCESS = 1;           // 成功
    private const int TRANS_STATE_FAILED = 2;            // 失败
    private const int TRANS_STATE_SKIPPED = 3;           // 跳过

    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtForm> FormRepository => _repositoryFactory.GetWorkflowRepository<HbtForm>();
    private IHbtRepository<HbtScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<HbtScheme>();
    private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();
    private IHbtRepository<HbtInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceTrans>();
    private IHbtRepository<HbtInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceOper>();

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

            // 1. 插入请假申请表单定义
            var formId = await InsertLeaveFormDefinitionAsync();
            _logger.Info($"请假申请表单定义创建完成，FormId: {formId}");

            // 2. 插入请假流程定义
            var schemeId = await InsertLeaveSchemeAsync(formId);
            _logger.Info($"请假流程定义创建完成，SchemeId: {schemeId}");

            // 3. 插入流程实例
            var instanceId = await InsertLeaveInstanceAsync(schemeId);
            _logger.Info($"请假流程实例创建完成，InstanceId: {instanceId}");

            // 4. 插入表单实例
            await InsertLeaveFormInstanceAsync(instanceId);
            _logger.Info("请假表单实例创建完成");

            // 5. 插入流转历史
            await InsertLeaveTransitionsAsync(instanceId);
            _logger.Info("请假流转历史创建完成");

            // 6. 插入操作记录
            await InsertLeaveOperationsAsync(instanceId);
            _logger.Info("请假操作记录创建完成");

            _logger.Info("员工请假流程全链路数据初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化员工请假流程失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 插入请假申请表单定义
    /// </summary>
    private async Task<long> InsertLeaveFormDefinitionAsync()
    {
        var form = new HbtForm
        {
            FormKey = "leave_form",
            FormName = "请假申请表单",
            Version = "1.0",
            FormConfig = @"{
                ""rule"": [
                    {
                        ""type"": ""input"",
                        ""field"": ""LeaveNo"",
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
                        ""field"": ""EmployeeId"",
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
                        ""field"": ""LeaveTypeId"",
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
                        ""field"": ""StartTime"",
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
                        ""field"": ""EndTime"",
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
                        ""field"": ""LeaveDays"",
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
                        ""field"": ""LeaveReason"",
                        ""title"": ""请假原因"",
                        ""props"": {
                            ""placeholder"": ""请输入请假原因"",
                            ""rows"": 4,
                            ""maxLength"": 500,
                            ""showCount"": true
                        },
                        ""validate"": [
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
            Status = FORM_STATUS_PUBLISHED,
            Description = "员工请假申请表单，字段与HbtLeave实体完全一致，用于工作流审批后自动创建请假记录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };

        var existingForm = await FormRepository.GetFirstAsync(f => f.FormKey == form.FormKey);
        if (existingForm != null)
        {
            // 存在就更新
            existingForm.FormName = form.FormName;
            existingForm.FormConfig = form.FormConfig;
            existingForm.Status = form.Status;
            existingForm.Description = form.Description;
            existingForm.UpdateBy = form.UpdateBy;
            existingForm.UpdateTime = form.UpdateTime;
            await FormRepository.UpdateAsync(existingForm);
            _logger.Info($"请假申请表单定义已更新: FormKey={form.FormKey}");
            return existingForm.Id;
        }

        // 不存在就新增
        var formId = await FormRepository.CreateAsync(form);
        _logger.Info($"请假申请表单定义已创建: FormKey={form.FormKey}, FormId={formId}");
        return formId;
    }

    /// <summary>
    /// 插入请假流程定义
    /// </summary>
    private async Task<long> InsertLeaveSchemeAsync(long formId)
    {
        var scheme = new HbtScheme
        {
            SchemeKey = "leave_workflow",
            SchemeName = "员工请假流程",
            Version = "1.0",
            FormId = formId,
            SchemeConfig = @"{
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
                    {""source"": ""condition"", ""target"": ""generalManager"", ""condition"": ""LeaveTypeId == 1 || LeaveTypeId == 2""},
                    {""source"": ""condition"", ""target"": ""adminProcess"", ""condition"": ""LeaveTypeId == 3 || LeaveTypeId == 4""},
                    {""source"": ""generalManager"", ""target"": ""adminProcess""},
                    {""source"": ""adminProcess"", ""target"": ""end""}
                ]
            }",
            Status = SCHEME_STATUS_PUBLISHED,
            Description = "员工请假审批流程定义 - 根据请假类型决定审批路径（事假/病假需要总经理审批，年假/调休直接总务处理）",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };

        var existingScheme = await SchemeRepository.GetFirstAsync(d => d.SchemeKey == scheme.SchemeKey);
        if (existingScheme != null)
        {
            // 存在就更新
            existingScheme.SchemeName = scheme.SchemeName;
            existingScheme.SchemeConfig = scheme.SchemeConfig;
            existingScheme.Status = scheme.Status;
            existingScheme.Description = scheme.Description;
            existingScheme.UpdateBy = scheme.UpdateBy;
            existingScheme.UpdateTime = scheme.UpdateTime;
            await SchemeRepository.UpdateAsync(existingScheme);
            _logger.Info($"请假流程定义已更新: SchemeKey={scheme.SchemeKey}");
            return existingScheme.Id;
        }

        // 不存在就新增
        var schemeId = await SchemeRepository.CreateAsync(scheme);
        _logger.Info($"请假流程定义已创建: SchemeKey={scheme.SchemeKey}, SchemeId={schemeId}");
        return schemeId;
    }

    /// <summary>
    /// 插入请假流程实例
    /// </summary>
    private async Task<long> InsertLeaveInstanceAsync(long schemeId)
    {
        var instance = new HbtInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "张三请假申请",
            BusinessKey = $"LEAVE_{DateTime.Now:yyyyMMddHHmmss}",
            Status = INSTANCE_STATUS_RUNNING,
            StartTime = DateTime.Now.AddDays(-2),
            EndTime = null,
            InitiatorId = 1,
            CurrentNodeId = "deptManager",
            CurrentNodeName = "部门经理审批",
            Variables = @"{
                ""LeaveNo"": ""LEAVE20240120001"",
                ""EmployeeId"": 1,
                ""LeaveTypeId"": 1,
                ""StartTime"": ""2024-01-20 09:00:00"",
                ""EndTime"": ""2024-01-22 18:00:00"",
                ""LeaveDays"": 2.5,
                ""LeaveReason"": ""感冒发烧，需要休息调养"",
                ""Status"": 1
            }",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now.AddDays(-2),
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now.AddDays(-2)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            _logger.Info($"请假流程实例已更新: BusinessKey={instance.BusinessKey}");
            return existingInstance.Id;
        }

        // 不存在就新增
        var instanceId = await InstanceRepository.CreateAsync(instance);
        _logger.Info($"请假流程实例已创建: BusinessKey={instance.BusinessKey}, InstanceId={instanceId}");
        return instanceId;
    }

    /// <summary>
    /// 插入请假表单实例
    /// </summary>
    private async Task InsertLeaveFormInstanceAsync(long instanceId)
    {
        var formInstance = new HbtForm
        {
            FormKey = "leave_form",
            FormName = "请假申请表单",
            Version = "1.0",
            InstanceId = instanceId,
            FormData = @"{
                ""LeaveNo"": ""LEAVE20240120001"",
                ""EmployeeId"": 1,
                ""LeaveTypeId"": 1,
                ""StartTime"": ""2024-01-20 09:00:00"",
                ""EndTime"": ""2024-01-22 18:00:00"",
                ""LeaveDays"": 2.5,
                ""LeaveReason"": ""感冒发烧，需要休息调养""
            }",
            Status = FORM_STATUS_PUBLISHED,
            Description = "张三的请假申请表单实例",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now.AddDays(-2),
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now.AddDays(-2)
        };

        var existingForm = await FormRepository.GetFirstAsync(x =>
            x.InstanceId == instanceId && x.FormKey == formInstance.FormKey);

        if (existingForm != null)
        {
            // 存在就更新
            existingForm.FormData = formInstance.FormData;
            existingForm.UpdateBy = formInstance.UpdateBy;
            existingForm.UpdateTime = formInstance.UpdateTime;
            await FormRepository.UpdateAsync(existingForm);
            _logger.Info($"请假表单实例已更新: InstanceId={instanceId}");
        }
        else
        {
            // 不存在就新增
            await FormRepository.CreateAsync(formInstance);
            _logger.Info($"请假表单实例已创建: InstanceId={instanceId}");
        }
    }

    /// <summary>
    /// 插入请假流转历史
    /// </summary>
    private async Task InsertLeaveTransitionsAsync(long instanceId)
    {
        var transitions = new[]
        {
            new HbtInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "start",
                StartNodeType = NODE_TYPE_START,
                StartNodeName = "开始",
                ToNodeId = "apply",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "员工申请",
                TransState = TRANS_STATE_SUCCESS,
                IsFinish = 0,
                TransTime = DateTime.Now.AddDays(-2),
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new HbtInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "apply",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "员工申请",
                ToNodeId = "deptManager",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "部门经理审批",
                TransState = TRANS_STATE_SUCCESS,
                IsFinish = 0,
                TransTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            },
            new HbtInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "deptManager",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "部门经理审批",
                ToNodeId = "condition",
                ToNodeType = NODE_TYPE_GATEWAY,
                ToNodeName = "请假类型判断",
                TransState = TRANS_STATE_SUCCESS,
                IsFinish = 0,
                TransTime = DateTime.Now.AddDays(-2).AddMinutes(3),
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(3),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(3)
            }
        };

        foreach (var transition in transitions)
        {
            var existing = await TransRepository.GetFirstAsync(x =>
                x.InstanceId == transition.InstanceId &&
                x.StartNodeId == transition.StartNodeId &&
                x.ToNodeId == transition.ToNodeId &&
                x.TransTime == transition.TransTime);

            if (existing != null)
            {
                // 存在就更新
                existing.TransState = transition.TransState;
                existing.IsFinish = transition.IsFinish;
                existing.UpdateBy = transition.UpdateBy;
                existing.UpdateTime = transition.UpdateTime;
                await TransRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                await TransRepository.CreateAsync(transition);
            }
        }
        _logger.Info($"请假流转历史处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 插入请假操作记录
    /// </summary>
    private async Task InsertLeaveOperationsAsync(long instanceId)
    {
        var operations = new[]
        {
            new HbtInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "apply",
                NodeName = "员工申请",
                OperType = OPERATION_TYPE_SUBMIT,
                OperatorId = 1,
                OperatorName = "张三",
                OperOpinion = "请假事由：感冒发烧，需要休息调养",
                OperData = "{\"LeaveNo\":\"LEAVE20240120001\",\"EmployeeId\":1,\"LeaveTypeId\":1,\"StartTime\":\"2024-01-20 09:00:00\",\"EndTime\":\"2024-01-22 18:00:00\",\"LeaveDays\":2.5,\"LeaveReason\":\"感冒发烧，需要休息调养\"}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            }
        };

        foreach (var operation in operations)
        {
            var existing = await OperRepository.GetFirstAsync(x =>
                x.InstanceId == operation.InstanceId &&
                x.NodeId == operation.NodeId &&
                x.OperType == operation.OperType &&
                x.OperatorId == operation.OperatorId);

            if (existing != null)
            {
                // 存在就更新
                existing.OperOpinion = operation.OperOpinion;
                existing.OperData = operation.OperData;
                existing.UpdateBy = operation.UpdateBy;
                existing.UpdateTime = operation.UpdateTime;
                await OperRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                await OperRepository.CreateAsync(operation);
            }
        }
        _logger.Info($"请假操作记录处理完成: InstanceId={instanceId}");
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
            var scheme = await SchemeRepository.GetFirstAsync(d => d.SchemeKey == "leave_workflow");
            if (scheme == null)
            {
                _logger.Warn("未找到员工请假流程定义，请先运行 InitializeLeaveWorkflowAsync");
                return;
            }

            // 创建不同状态的实例
            await CreateInstanceInDraftStatusAsync(scheme.Id);
            await CreateInstanceInRunningStatusAsync(scheme.Id);
            await CreateInstanceInCompletedStatusAsync(scheme.Id);
            await CreateInstanceInSuspendedStatusAsync(scheme.Id);

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
    private async Task CreateInstanceInDraftStatusAsync(long schemeId)
    {
        var instance = new HbtInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "李四的请假申请（草稿）",
            BusinessKey = "LEAVE-2024-002",
            Status = INSTANCE_STATUS_DRAFT,
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = null,
            InitiatorId = 2,
            CurrentNodeId = null,
            CurrentNodeName = null,
            Variables = "{\"LeaveNo\":\"LEAVE20240305001\",\"EmployeeId\":2,\"LeaveTypeId\":1,\"StartTime\":\"2024-03-05 09:00:00\",\"EndTime\":\"2024-03-06 18:00:00\",\"LeaveDays\":2.0,\"LeaveReason\":\"家中有事\",\"Status\":1}",
            CreateBy = "lisi",
            CreateTime = DateTime.Now.AddDays(-1),
            UpdateBy = "lisi",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            _logger.Info($"草稿状态实例已更新: {instance.BusinessKey}");
        }
        else
        {
            // 不存在就新增
            await InstanceRepository.CreateAsync(instance);
            _logger.Info($"草稿状态实例已创建: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 创建运行中状态的实例
    /// </summary>
    private async Task CreateInstanceInRunningStatusAsync(long schemeId)
    {
        var instance = new HbtInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "王五的请假申请（运行中）",
            BusinessKey = "LEAVE-2024-003",
            Status = INSTANCE_STATUS_RUNNING,
            StartTime = DateTime.Now.AddHours(-2),
            EndTime = null,
            InitiatorId = 3,
            CurrentNodeId = "deptManager",
            CurrentNodeName = "部门经理审批",
            Variables = "{\"LeaveNo\":\"LEAVE20240310001\",\"EmployeeId\":3,\"LeaveTypeId\":3,\"StartTime\":\"2024-03-10 09:00:00\",\"EndTime\":\"2024-03-14 18:00:00\",\"LeaveDays\":5.0,\"LeaveReason\":\"年假休息\",\"Status\":1}",
            CreateBy = "wangwu",
            CreateTime = DateTime.Now.AddHours(-2),
            UpdateBy = "wangwu",
            UpdateTime = DateTime.Now.AddHours(-2)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            _logger.Info($"运行中状态实例已更新: {instance.BusinessKey}");
        }
        else
        {
            // 不存在就新增
            await InstanceRepository.CreateAsync(instance);
            _logger.Info($"运行中状态实例已创建: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 创建已完成状态的实例
    /// </summary>
    private async Task CreateInstanceInCompletedStatusAsync(long schemeId)
    {
        var instance = new HbtInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "赵六的请假申请（已完成）",
            BusinessKey = "LEAVE-2024-004",
            Status = INSTANCE_STATUS_COMPLETED,
            StartTime = DateTime.Now.AddDays(-5),
            EndTime = DateTime.Now.AddDays(-3),
            InitiatorId = 4,
            CurrentNodeId = "end",
            CurrentNodeName = "结束",
            Variables = "{\"LeaveNo\":\"LEAVE20240315001\",\"EmployeeId\":4,\"LeaveTypeId\":4,\"StartTime\":\"2024-03-15 09:00:00\",\"EndTime\":\"2024-03-15 18:00:00\",\"LeaveDays\":1.0,\"LeaveReason\":\"调休\",\"Status\":2}",
            CreateBy = "zhaoliu",
            CreateTime = DateTime.Now.AddDays(-5),
            UpdateBy = "zhaoliu",
            UpdateTime = DateTime.Now.AddDays(-3)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            _logger.Info($"已完成状态实例已更新: {instance.BusinessKey}");
        }
        else
        {
            // 不存在就新增
            await InstanceRepository.CreateAsync(instance);
            _logger.Info($"已完成状态实例已创建: {instance.BusinessKey}");
        }
    }

    /// <summary>
    /// 创建已挂起状态的实例
    /// </summary>
    private async Task CreateInstanceInSuspendedStatusAsync(long schemeId)
    {
        var instance = new HbtInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "孙七的请假申请（已挂起）",
            BusinessKey = "LEAVE-2024-005",
            Status = INSTANCE_STATUS_SUSPENDED,
            StartTime = DateTime.Now.AddDays(-3),
            EndTime = null,
            InitiatorId = 5,
            CurrentNodeId = "generalManager",
            CurrentNodeName = "总经理审批",
            Variables = "{\"LeaveNo\":\"LEAVE20240320001\",\"EmployeeId\":5,\"LeaveTypeId\":2,\"StartTime\":\"2024-03-20 09:00:00\",\"EndTime\":\"2024-03-25 18:00:00\",\"LeaveDays\":6.0,\"LeaveReason\":\"病假\",\"Status\":1}",
            CreateBy = "sunqi",
            CreateTime = DateTime.Now.AddDays(-3),
            UpdateBy = "sunqi",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            _logger.Info($"已挂起状态实例已更新: {instance.BusinessKey}");
        }
        else
        {
            // 不存在就新增
            await InstanceRepository.CreateAsync(instance);
            _logger.Info($"已挂起状态实例已创建: {instance.BusinessKey}");
        }
    }
}