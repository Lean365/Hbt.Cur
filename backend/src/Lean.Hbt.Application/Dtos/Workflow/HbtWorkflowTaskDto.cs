//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowTaskDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务数据传输对象
//===================================================================

using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流任务状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskStatusDto
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public HbtWorkflowTaskStatus Status { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public HbtWorkflowTaskType TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long AssigneeId { get; set; }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public string AssigneeName { get; set; }

        /// <summary>
        /// 可用操作列表
        /// </summary>
        public List<string> AvailableOperations { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// 任务截止时间
        /// </summary>
        public DateTime? DueTime { get; set; }

        /// <summary>
        /// 任务提醒时间
        /// </summary>
        public DateTime? ReminderTime { get; set; }
    }

    /// <summary>
    /// 工作流任务基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskDto
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public HbtWorkflowTaskType TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long AssigneeId { get; set; }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public string AssigneeName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public HbtWorkflowTaskStatus Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 工作流任务查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long? WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public HbtWorkflowTaskType? TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long? AssigneeId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public HbtWorkflowTaskStatus? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工作流任务创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskCreateDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public HbtWorkflowTaskType TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long AssigneeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskUpdateDto
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public HbtWorkflowTaskStatus Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public HbtWorkflowTaskResult? Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskImportDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long AssigneeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskExportDto
    {
        /// <summary>
        /// 处理人名称
        /// </summary>
        public string AssigneeName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskTypeName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 工作流任务模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowTaskTemplateDto
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}