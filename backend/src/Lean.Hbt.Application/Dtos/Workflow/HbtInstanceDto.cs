//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流实例数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例DTO
    /// </summary>
    public class HbtInstanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceDto()
        {
            InstanceName = string.Empty;
            BusinessKey = string.Empty;
            FormData = string.Empty;
        }

        /// <summary>
        /// 实例ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceId { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string DefinitionName { get; set; } = string.Empty;

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 发起人姓名
        /// </summary>
        public string InitiatorName { get; set; } = string.Empty;

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string FormData { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

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
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 工作流实例查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtInstanceQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceQueryDto()
        {
            InstanceName = string.Empty;
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

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
    public class HbtInstanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceCreateDto()
        {
            InstanceName = string.Empty;
            BusinessKey = string.Empty;
            FormData = string.Empty;
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string FormData { get; set; }

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
    public class HbtInstanceUpdateDto : HbtInstanceCreateDto
    {
        /// <summary>
        /// 实例状态
        /// </summary>
        [AdaptMember("Id")]
        public int InstanceId { get; set; }
    }

    /// <summary>
    /// 工作流实例状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtInstanceStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceStatusDto()
        {
            CurrentNodeName = string.Empty;
            AvailableOperations = new List<string>();
            CurrentHandlers = new List<string>();
        }

        /// <summary>
        /// 实例ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceId { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }

        /// <summary>
        /// 可用操作列表
        /// </summary>
        public List<string> AvailableOperations { get; set; }

        /// <summary>
        /// 当前处理人列表
        /// </summary>
        public List<string> CurrentHandlers { get; set; }
    }

    /// <summary>
    /// 工作流实例导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtInstanceImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceImportDto()
        {
            InstanceName = string.Empty;
            BusinessKey = string.Empty;
            FormData = string.Empty;
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string FormData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流实例模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtInstanceTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTemplateDto()
        {
            InstanceName = string.Empty;
            BusinessKey = string.Empty;
            FormData = string.Empty;
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string FormData { get; set; }

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
    public class HbtInstanceExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceExportDto()
        {
            InstanceName = string.Empty;
            BusinessKey = string.Empty;
            FormData = string.Empty;
            InitiatorName = string.Empty;
            CurrentNodeName = string.Empty;
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        public string InitiatorName { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string FormData { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}