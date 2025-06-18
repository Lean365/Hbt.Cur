//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流节点数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流节点基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNodeDto()
        {
            NodeId = 0;
            DefinitionId = 0;
            NodeName = string.Empty;
            NodeType = 0;
            ParentNodeId = null;
            NodeConfig = string.Empty;
            OrderNum = 0;
            Remark = string.Empty;
            CreateBy = string.Empty;
            CreateTime = DateTime.Now;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        [AdaptMember("Id")]
        public long NodeId { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置
        /// </summary>
        public string NodeConfig { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

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
    /// 工作流节点查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long? DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int? NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

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
    /// 工作流节点创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeCreateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置
        /// </summary>
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流节点更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeUpdateDto:HbtNodeCreateDto
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [AdaptMember("Id")]
        public long NodeId { get; set; }


    }

    /// <summary>
    /// 工作流节点导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeImportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string? NodeType { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点配置
        /// </summary>
        public string? NodeConfig { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流节点导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeExportDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string? NodeTypeName { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        public int OrderNum { get; set; }

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
    /// 工作流节点模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtNodeTemplateDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string? NodeTypeName { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public long? ParentNodeId { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        public int OrderNum { get; set; }
    }
}