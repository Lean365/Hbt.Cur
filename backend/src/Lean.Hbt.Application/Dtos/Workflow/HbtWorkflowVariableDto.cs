//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowVariableDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流变量数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流变量基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowVariableDto()
        {
            WorkflowVariableId = 0;
            WorkflowInstanceId = 0;
            VariableName = string.Empty;
            VariableValue = string.Empty;
            VariableType = string.Empty;
            VariableScope = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
            UpdateBy = string.Empty;
            UpdateTime = null;
        }

        /// <summary>
        /// 变量ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowVariableId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long? WorkflowNodeId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string VariableValue { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string VariableType { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public string VariableScope { get; set; }

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
    /// 工作流变量查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableQueryDto : HbtPagedQuery
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
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public int? VariableType { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public int? VariableScope { get; set; }
    }

    /// <summary>
    /// 工作流变量创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableCreateDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long? WorkflowNodeId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string VariableValue { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string VariableType { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public string VariableScope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流变量更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableUpdateDto
    {
        /// <summary>
        /// 变量ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowVariableId { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string VariableValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流变量导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableImportDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 工作流节点ID
        /// </summary>
        public long? WorkflowNodeId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string? VariableValue { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public string? VariableScope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流变量导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableExportDto
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string? VariableValue { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string? VariableTypeName { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public string? VariableScopeName { get; set; }

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
    /// 工作流变量模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowVariableTemplateDto
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        public string? VariableScope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}