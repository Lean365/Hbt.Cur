//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceOper.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录实体类
//===================================================================

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流实例操作记录实体类
    /// </summary>
    [SugarTable("hbt_workflow_instance_oper", "实例操作")]
    [SugarIndex("ix_workflow_instance_oper_instance", nameof(InstanceId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_oper_operator", nameof(OperatorId), OrderByType.Asc)]
    [SugarIndex("ix_workflow_instance_oper_time", nameof(CreateTime), OrderByType.Desc)]
    public class HbtInstanceOper : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(ColumnName = "node_name", ColumnDescription = "节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? NodeName { get; set; }

        /// <summary>
        /// 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)
        /// </summary>
        [SugarColumn(ColumnName = "oper_type", ColumnDescription = "操作类型", ColumnDataType = "int", IsNullable = false)]
        public int OperType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [SugarColumn(ColumnName = "operator_id", ColumnDescription = "操作人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作人姓名", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作意见
        /// </summary>
        [SugarColumn(ColumnName = "oper_opinion", ColumnDescription = "操作意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? OperOpinion { get; set; }

        /// <summary>
        /// 操作数据(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "oper_data", ColumnDescription = "操作数据", ColumnDataType = "text", IsNullable = true)]
        public string? OperData { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
        public HbtInstance? WorkflowInstance { get; set; }
    }
} 