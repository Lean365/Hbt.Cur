#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowActivity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动实体
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Domain.Entities;
using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow.Engine
{
    /// <summary>
    /// 工作流活动实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    [SugarTable("hbt_wf_activity", "工作流活动表")]
    [SugarIndex("ix_workflow_activity_definition", nameof(WorkflowDefinitionId), OrderByType.Asc)]
    public class HbtWorkflowActivity : HbtBaseEntity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        [SugarColumn(ColumnName = "name", ColumnDescription = "活动名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 活动类型
        /// </summary>
        [SugarColumn(ColumnName = "type", ColumnDescription = "活动类型", ColumnDataType = "int", IsNullable = false)]
        public HbtWorkflowActivityType Type { get; set; }

        /// <summary>
        /// 活动配置(JSON)
        /// </summary>
        [SugarColumn(ColumnName = "configuration", ColumnDescription = "活动配置(JSON)", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string? Configuration { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowDefinitionId))]
        public virtual HbtWorkflowDefinition? WorkflowDefinition { get; set; }
    }
} 
