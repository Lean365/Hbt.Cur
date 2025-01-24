//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowHistory.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流历史记录实体类
//===================================================================

using System;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流历史记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_history", "工作流历史记录表")]
    public class HbtWorkflowHistory : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", ColumnDataType = "bigint", IsNullable = false)]
        public long NodeId { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:转交 4:退回 5:终止 6:挂起 7:恢复)
        /// </summary>
        [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型(1:提交 2:审批 3:转交 4:退回 5:终止 6:挂起 7:恢复)", ColumnDataType = "tinyint", IsNullable = false)]
        public int OperationType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [SugarColumn(ColumnName = "operator_id", ColumnDescription = "操作人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作人名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OperatorName { get; set; }

        /// <summary>
        /// 操作结果(1:同意 2:拒绝 3:转交 4:退回)
        /// </summary>
        [SugarColumn(ColumnName = "operation_result", ColumnDescription = "操作结果(1:同意 2:拒绝 3:转交 4:退回)", ColumnDataType = "tinyint", IsNullable = true)]
        public int? OperationResult { get; set; }

        /// <summary>
        /// 操作意见
        /// </summary>
        [SugarColumn(ColumnName = "operation_comment", ColumnDescription = "操作意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string OperationComment { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [SugarColumn(ColumnName = "operation_time", ColumnDescription = "操作时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkflowInstanceId))]
        public HbtWorkflowInstance WorkflowInstance { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NodeId))]
        public HbtWorkflowNode Node { get; set; }
    }
} 