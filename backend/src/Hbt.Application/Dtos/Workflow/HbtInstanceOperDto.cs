//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtInstanceOperDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工作流实例操作记录数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例操作记录基础DTO（与HbtInstanceOper实体字段严格对应）
    /// </summary>
    public class HbtInstanceOperDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperDto()
        {
            OperatorName = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceOperId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        public string? OperData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

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
    /// 工作流实例操作记录查询DTO
    /// </summary>
    public class HbtInstanceOperQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperQueryDto()
        {
            NodeId = string.Empty;
            NodeName = string.Empty;
            OperatorName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int? OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long? OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatorName { get; set; }
    }

    /// <summary>
    /// 工作流实例操作记录创建DTO
    /// </summary>
    public class HbtInstanceOperCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperCreateDto()
        {
            OperatorName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        public string? OperData { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流审批DTO
    /// </summary>
    public class HbtInstanceApproveDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceApproveDto()
        {
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 操作类型(2:审批 3:驳回 4:转办)
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// 操作意见
        /// </summary>
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        public string? OperData { get; set; }
    }

    /// <summary>
    /// 工作流实例操作记录模板DTO
    /// </summary>
    public class HbtInstanceOperTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperTemplateDto()
        {
            OperatorName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        public string? OperData { get; set; }
    }

    /// <summary>
    /// 工作流实例操作记录导入DTO
    /// </summary>
    public class HbtInstanceOperImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperImportDto()
        {
            OperatorName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)
        /// </summary>
        public int OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        public string? OperData { get; set; }
    }

    /// <summary>
    /// 工作流实例操作记录导出DTO
    /// </summary>
    public class HbtInstanceOperExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceOperExportDto()
        {
            NodeId = string.Empty;
            NodeName = string.Empty;
            OperType = string.Empty;
            OperatorName = string.Empty;
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
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperType { get; set; } = string.Empty;

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        public string OperOpinion { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 