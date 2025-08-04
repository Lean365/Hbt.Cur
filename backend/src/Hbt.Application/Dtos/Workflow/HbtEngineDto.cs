//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtEngineDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎相关DTO类
//===================================================================

namespace Hbt.Cur.Application.Dtos.Workflow;

/// <summary>
/// 工作流转换DTO
/// </summary>
public class HbtTransitionDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTransitionDto()
    {
        InstanceTransId = string.Empty;
        StartNodeId = string.Empty;
        StartNodeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
    }

    /// <summary>
    /// 转换ID
    /// </summary>
    public string InstanceTransId { get; set; } = string.Empty;

    /// <summary>
    /// 实例ID
    /// </summary>
    public long InstanceId { get; set; }

    /// <summary>
    /// 开始节点ID
    /// </summary>
    public string StartNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 开始节点类型
    /// </summary>
    public int StartNodeType { get; set; }

    /// <summary>
    /// 开始节点名称
    /// </summary>
    public string StartNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点类型
    /// </summary>
    public int ToNodeType { get; set; }

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 转化状态
    /// </summary>
    public int TransState { get; set; }

    /// <summary>
    /// 是否完成
    /// </summary>
    public int IsFinish { get; set; }

    /// <summary>
    /// 转化时间
    /// </summary>
    public DateTime TransTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 工作流节点DTO
/// </summary>
public class HbtNodeDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtNodeDto()
    {
        NodeId = string.Empty;
        NodeName = string.Empty;
        NodeType = string.Empty;
    }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// 节点配置(JSON格式)
    /// </summary>
    public string? NodeConfig { get; set; }

    /// <summary>
    /// 审批人类型(1:指定用户 2:角色 3:部门 4:动态)
    /// </summary>
    public int ApproverType { get; set; }

    /// <summary>
    /// 审批人配置(JSON格式)
    /// </summary>
    public string? ApproverConfig { get; set; }
}

/// <summary>
/// 工作流审批DTO
/// </summary>
public class HbtWorkflowApproveDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtWorkflowApproveDto()
    {
        NodeId = string.Empty;
        OperOpinion = string.Empty;
    }

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型(1:同意 2:拒绝 3:退回 4:转办 5:委托)
    /// </summary>
    public int OperType { get; set; }

    /// <summary>
    /// 操作意见
    /// </summary>
    public string OperOpinion { get; set; } = string.Empty;

    /// <summary>
    /// 操作数据(JSON格式)
    /// </summary>
    public string? OperData { get; set; }

    /// <summary>
    /// 目标用户ID(转办/委托时使用)
    /// </summary>
    public long? TargetUserId { get; set; }
}

/// <summary>
/// 工作流启动DTO
/// </summary>
public class HbtWorkflowStartDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtWorkflowStartDto()
    {
        InstanceTitle = string.Empty;
    }

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public long SchemeId { get; set; }

    /// <summary>
    /// 实例标题
    /// </summary>
    public string InstanceTitle { get; set; } = string.Empty;

    /// <summary>
    /// 业务键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 发起人ID
    /// </summary>
    public long InitiatorId { get; set; }

    /// <summary>
    /// 流程变量(JSON格式)
    /// </summary>
    public string? Variables { get; set; }
}

/// <summary>
/// 转换查询DTO
/// </summary>
public class HbtTransitionQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 实例ID
    /// </summary>
    public long? InstanceId { get; set; }

    /// <summary>
    /// 开始节点ID
    /// </summary>
    public string? StartNodeId { get; set; }

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string? ToNodeId { get; set; }

    /// <summary>
    /// 转化状态
    /// </summary>
    public int? TransState { get; set; }

    /// <summary>
    /// 是否完成
    /// </summary>
    public int? IsFinish { get; set; }
}

/// <summary>
/// 转换创建DTO
/// </summary>
public class HbtTransitionCreateDto
{
    /// <summary>
    /// 实例ID
    /// </summary>
    public long InstanceId { get; set; }

    /// <summary>
    /// 开始节点ID
    /// </summary>
    public string StartNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 开始节点类型
    /// </summary>
    public int StartNodeType { get; set; }

    /// <summary>
    /// 开始节点名称
    /// </summary>
    public string StartNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点类型
    /// </summary>
    public int ToNodeType { get; set; }

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 转化状态
    /// </summary>
    public int TransState { get; set; }

    /// <summary>
    /// 是否完成
    /// </summary>
    public int IsFinish { get; set; }

    /// <summary>
    /// 转化时间
    /// </summary>
    public DateTime TransTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 转换更新DTO
/// </summary>
public class HbtTransitionUpdateDto : HbtTransitionCreateDto
{
    /// <summary>
    /// 转换ID
    /// </summary>
    public string InstanceTransId { get; set; } = string.Empty;
} 