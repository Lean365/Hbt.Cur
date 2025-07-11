//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtVariableDto.cs
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
    public class HbtVariableDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableDto()
        {
            VariableId = 0;
            InstanceId = 0;
            VariableName = string.Empty;
            VariableType = string.Empty;
            VariableValue = string.Empty;
            Scope = 0;
            NodeId = null;
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
        public long VariableId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 变量类型(string/int/decimal/datetime/bool)
        /// </summary>
        public string VariableType { get; set; }

        /// <summary>
        /// 变量值(JSON格式)
        /// </summary>
        public string VariableValue { get; set; }

        /// <summary>
        /// 变量作用域(0:全局 1:节点)
        /// </summary>
        public int Scope { get; set; }

        /// <summary>
        /// 节点ID(作用域为节点时必填)
        /// </summary>
        public long? NodeId { get; set; }

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

        /// <summary>
        /// 工作流实例
        /// </summary>
        public HbtInstanceDto? WorkflowInstance { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public HbtNodeDto? Node { get; set; }
    }

    /// <summary>
    /// 工作流变量查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtVariableQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// 变量作用域(0:全局 1:节点)
        /// </summary>
        public int? Scope { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public long? NodeId { get; set; }

        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 创建时间结束
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工作流变量创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtVariableCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableCreateDto()
        {
            InstanceId = 0;
            VariableName = string.Empty;
            VariableType = string.Empty;
            VariableValue = string.Empty;
            Scope = 0;
            NodeId = null;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 变量类型(string/int/decimal/datetime/bool)
        /// </summary>
        public string VariableType { get; set; }

        /// <summary>
        /// 变量值(JSON格式)
        /// </summary>
        public string VariableValue { get; set; }

        /// <summary>
        /// 变量作用域(0:全局 1:节点)
        /// </summary>
        public int Scope { get; set; }

        /// <summary>
        /// 节点ID(作用域为节点时必填)
        /// </summary>
        public long? NodeId { get; set; }

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
    public class HbtVariableUpdateDto : HbtVariableCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableUpdateDto()
        {
            VariableId = 0;
        }

        /// <summary>
        /// 变量ID
        /// </summary>
        [AdaptMember("Id")]
        public long VariableId { get; set; }
    }

    /// <summary>
    /// 工作流变量导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtVariableImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableImportDto()
        {
            InstanceId = 0;
            VariableName = string.Empty;
            VariableType = string.Empty;
            VariableValue = string.Empty;
            Scope = 0;
            NodeId = null;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量类型(string/int/decimal/datetime/bool)
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// 变量值(JSON格式)
        /// </summary>
        public string? VariableValue { get; set; }

        /// <summary>
        /// 变量作用域(0:全局 1:节点)
        /// </summary>
        public int? Scope { get; set; }

        /// <summary>
        /// 节点ID(作用域为节点时必填)
        /// </summary>
        public long? NodeId { get; set; }

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
    public class HbtVariableExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableExportDto()
        {
            VariableName = string.Empty;
            VariableValue = string.Empty;
            VariableTypeName = string.Empty;
            ScopeName = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
        }

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
        public string? ScopeName { get; set; }

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
    public class HbtVariableTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtVariableTemplateDto()
        {
            VariableName = string.Empty;
            VariableType = string.Empty;
            Scope = 0;
            Remark = string.Empty;
        }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string? VariableName { get; set; }

        /// <summary>
        /// 变量类型(string/int/decimal/datetime/bool)
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// 变量作用域(0:全局 1:节点)
        /// </summary>
        public int? Scope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}