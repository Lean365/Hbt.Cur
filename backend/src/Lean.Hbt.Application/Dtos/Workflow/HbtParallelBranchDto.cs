#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtParallelBranchDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流并行分支DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流并行分支DTO
    /// </summary>
    public class HbtParallelBranchDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtParallelBranchDto()
        {
            ParallelBranchId = 0;
            InstanceId = 0;
            ParallelNodeId = 0;
            BranchTransitionId = 0;
            BranchName = string.Empty;
            IsCompleted = 0;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long ParallelBranchId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 工作流实例名称
        /// </summary>
        public string InstanceName { get; set; } = string.Empty;

        /// <summary>
        /// 并行节点ID
        /// </summary>
        public long ParallelNodeId { get; set; }

        /// <summary>
        /// 并行节点名称
        /// </summary>
        public string ParallelNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 分支转换ID
        /// </summary>
        public long BranchTransitionId { get; set; }

        /// <summary>
        /// 分支转换名称
        /// </summary>
        public string BranchTransitionName { get; set; } = string.Empty;

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 关联的节点模板名称
        /// </summary>
        public string? NodeTemplateName { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// 是否完成(0:未完成 1:已完成)
        /// </summary>
        public int IsCompleted { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        public HbtInstanceDto? WorkflowInstance { get; set; }

        /// <summary>
        /// 并行节点
        /// </summary>
        public HbtNodeDto? ParallelNode { get; set; }

        /// <summary>
        /// 分支转换
        /// </summary>
        public HbtTransitionDto? BranchTransition { get; set; }

        /// <summary>
        /// 关联的节点模板
        /// </summary>
        public HbtNodeTemplateDto? NodeTemplate { get; set; }

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
    /// 工作流并行分支查询DTO
    /// </summary>
    public class HbtParallelBranchQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 并行节点ID
        /// </summary>
        public long? ParallelNodeId { get; set; }

        /// <summary>
        /// 分支转换ID
        /// </summary>
        public long? BranchTransitionId { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int? IsCompleted { get; set; }
    }

    /// <summary>
    /// 工作流并行分支创建DTO
    /// </summary>
    public class HbtParallelBranchCreateDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 并行节点ID
        /// </summary>
        public long ParallelNodeId { get; set; }

        /// <summary>
        /// 分支转换ID
        /// </summary>
        public long BranchTransitionId { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流并行分支更新DTO
    /// </summary>
    public class HbtParallelBranchUpdateDto : HbtParallelBranchCreateDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long ParallelBranchId { get; set; }

        /// <summary>
        /// 是否完成(0:未完成 1:已完成)
        /// </summary>
        public int? IsCompleted { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
    }

    /// <summary>
    /// 工作流并行分支删除DTO
    /// </summary>
    public class HbtParallelBranchDeleteDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        public long ParallelBranchId { get; set; }
    }

    /// <summary>
    /// 工作流并行分支导入DTO
    /// </summary>
    public class HbtParallelBranchImportDto
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 并行节点ID
        /// </summary>
        public long ParallelNodeId { get; set; }

        /// <summary>
        /// 分支转换ID
        /// </summary>
        public long BranchTransitionId { get; set; }

        /// <summary>
        /// 关联的节点模板ID
        /// </summary>
        public long? NodeTemplateId { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流并行分支导出DTO
    /// </summary>
    public class HbtParallelBranchExportDto
    {
        /// <summary>
        /// 工作流实例名称
        /// </summary>
        public string InstanceName { get; set; } = string.Empty;

        /// <summary>
        /// 并行节点名称
        /// </summary>
        public string ParallelNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 分支转换名称
        /// </summary>
        public string BranchTransitionName { get; set; } = string.Empty;

        /// <summary>
        /// 关联的节点模板名称
        /// </summary>
        public string? NodeTemplateName { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public string IsCompletedName { get; set; } = string.Empty;

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

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
} 