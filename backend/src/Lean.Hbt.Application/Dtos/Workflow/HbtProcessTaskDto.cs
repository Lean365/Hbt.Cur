//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTaskDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流任务数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{

    /// <summary>
    /// 工作流任务基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtProcessTaskDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtProcessTaskDto()
        {
            WorkflowTaskId = 0;
            InstanceId = 0;
            WorkflowNodeId = 0;
            TaskType = 0;
            AssigneeId = 0;
            AssigneeName = string.Empty;
            Status = 0;
            TaskResult = 0;
            Comment = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
            UpdateBy = string.Empty;
            UpdateTime = null;
        }

        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

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
        public int Status { get; set; }

        /// <summary>
        /// 任务结果
        /// </summary>
        public int TaskResult { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

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
    public class HbtProcessTaskQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long? WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int? TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long? AssigneeId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int? Status { get; set; }

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
    public class HbtTaskCreateDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long WorkflowNodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public long AssigneeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtProcessTaskUpdateDto
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public int? Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtProcessTaskImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtProcessTaskImportDto()
        {
            TaskType = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

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
    public class HbtProcessTaskExportDto
    {
        /// <summary>
        /// 处理人名称
        /// </summary>
        public string? AssigneeName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string? TaskTypeName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string? Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

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
    public class HbtProcessTaskTemplateDto
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public string? TaskType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流任务状态DTO
    /// </summary>
    public class HbtProcessTaskStatusDto
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public long WorkflowTaskId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string? Result { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string? Comment { get; set; }
    }
}