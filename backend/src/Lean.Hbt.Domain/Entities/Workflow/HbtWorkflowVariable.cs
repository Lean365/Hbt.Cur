//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowVariable.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流变量实体类
//===================================================================

using System;
using SqlSugar;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流变量实体类
    /// </summary>
    [SugarTable("hbt_wf_variable", "工作流变量表")]
    public class HbtWorkflowVariable : HbtBaseEntity
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "workflow_instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        [SugarColumn(ColumnName = "variable_name", ColumnDescription = "变量名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VariableName { get; set; }

        /// <summary>
        /// 变量类型(string/int/decimal/datetime/bool)
        /// </summary>
        [SugarColumn(ColumnName = "variable_type", ColumnDescription = "变量类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VariableType { get; set; }

        /// <summary>
        /// 变量值(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "variable_value", ColumnDescription = "变量值(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string VariableValue { get; set; }

        /// <summary>
        /// 变量作用域
        /// </summary>
        [SugarColumn(ColumnName = "scope", ColumnDescription = "变量作用域", ColumnDataType = "int", IsNullable = false)]
        public int Scope { get; set; }

        /// <summary>
        /// 节点ID(作用域为节点时必填)
        /// </summary>
        [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? NodeId { get; set; }

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