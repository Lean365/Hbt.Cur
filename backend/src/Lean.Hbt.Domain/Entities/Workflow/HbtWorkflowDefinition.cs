#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDefinition.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流定义实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流定义实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_workflow_definition", "工作流定义表")]
    [SugarIndex("ix_workflow_name", nameof(WorkflowName), OrderByType.Asc, true)]
    public class HbtWorkflowDefinition : HbtBaseEntity
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        [SugarColumn(ColumnName = "workflow_name", ColumnDescription = "流程名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 流程分类(字典)
        /// </summary>
        [SugarColumn(ColumnName = "workflow_category", ColumnDescription = "流程分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? WorkflowCategory { get; set; }

        /// <summary>
        /// 流程版本
        /// </summary>
        [SugarColumn(ColumnName = "workflow_version", ColumnDescription = "流程版本", ColumnDataType = "int", IsNullable = false)]
        public int WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置(JSON格式)
        /// 包含：表单字段、验证规则、UI配置等
        /// </summary>
        [SugarColumn(ColumnName = "form_config", ColumnDescription = "表单配置(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string? FormConfig { get; set; }

        /// <summary>
        /// 工作流配置(JSON格式)
        /// 包含：节点定义、连线定义、变量定义等
        /// </summary>
        [SugarColumn(ColumnName = "workflow_config", ColumnDescription = "工作流配置(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 状态(0=草稿 1=已发布 2=已停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }

        /// <summary>
        /// 流程节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtWorkflowNode.WorkflowDefinitionId))]
        public List<HbtWorkflowNode>? WorkflowNodes { get; set; }
    }
}