//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowInstanceDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流实例数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceStatusDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 可用操作列表
        /// </summary>
        public List<string>? AvailableOperations { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public int StatusDescription { get; set; }

        /// <summary>
        /// 当前处理人列表
        /// </summary>
        public List<string>? CurrentHandlers { get; set; }
    }

    /// <summary>
    /// 工作流实例基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowInstanceDto()
        {
            WorkflowInstanceId = 0;
            WorkflowDefinitionId = 0;
            Title = string.Empty;
            InitiatorId = 0;
            InitiatorName = string.Empty;
            CurrentNodeId = 0;
            CurrentNodeName = string.Empty;
            FormData = string.Empty;
            Status = 0;
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
            Modifier = string.Empty;
            ModifyTime = null;
        }

        /// <summary>
        /// 实例ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        public string InitiatorName { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string FormData { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }

    /// <summary>
    /// 工作流实例查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>

        public long? WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long? InitiatorId { get; set; }

        /// <summary>
        /// 实例状态
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
    /// 工作流实例创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceCreateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>

        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流实例更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceUpdateDto
    {
        /// <summary>
        /// 实例ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流实例导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceImportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>

        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流实例导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceExportDto
    {
        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        public string? InitiatorName { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

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
    /// 工作流实例模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowInstanceTemplateDto
    {
        /// <summary>
        /// 工作流标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}