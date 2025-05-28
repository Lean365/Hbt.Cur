//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowInstance.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流实例实体类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流实例实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_instance", "工作流实例表")]
    public class HbtWorkflowInstance : HbtBaseEntity
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 流程标题
        /// </summary>
        [SugarColumn(ColumnName = "workflow_title", ColumnDescription = "流程标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? WorkflowTitle { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [SugarColumn(ColumnName = "initiator_id", ColumnDescription = "发起人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "form_data", ColumnDescription = "表单数据(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string? FormData { get; set; }

        /// <summary>
        /// 实例状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "实例状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EndTime { get; set; }


        /// <summary>
        /// 工作流定义
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowDefinitionId))]
        public HbtWorkflowDefinition? WorkflowDefinition { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CurrentNodeId))]
        public HbtWorkflowNode? CurrentNode { get; set; }
    }
}