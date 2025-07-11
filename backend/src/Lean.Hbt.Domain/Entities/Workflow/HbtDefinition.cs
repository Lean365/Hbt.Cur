#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefinition.cs
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
    [SugarIndex("ix_workflow_category", nameof(WorkflowCategory), OrderByType.Asc)]
    [SugarIndex("ix_workflow_status", nameof(Status), OrderByType.Asc)]
    public class HbtDefinition : HbtBaseEntity
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
        /// 工作流版本
        /// </summary>
        [SugarColumn(ColumnName = "workflow_version", ColumnDescription = "流程版本",Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? WorkflowVersion { get; set; }

        /// <summary>
        /// 关联表单ID
        /// </summary>
        [SugarColumn(ColumnName = "form_id", ColumnDescription = "关联表单ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? FormId { get; set; }

        /// <summary>
        /// 工作流配置(JSON格式)
        /// 包含：节点定义、连线定义、变量定义等
        /// </summary>
        [SugarColumn(ColumnName = "workflow_config", ColumnDescription = "工作流配置", ColumnDataType = "text", IsNullable = false)]
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 状态(0=草稿 1=已发布 2=已停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        [SugarColumn(ColumnName = "workflow_desc", ColumnDescription = "流程描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkflowDesc { get; set; }

        /// <summary>
        /// 流程节点模板列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtNodeTemplate.DefinitionId))]
        public List<HbtNodeTemplate>? NodeTemplates { get; set; }

        /// <summary>
        /// 关联表单列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtForm.DefinitionId))]
        public List<HbtForm>? Forms { get; set; }

        /// <summary>
        /// 工作流实例列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtInstance.DefinitionId))]
        public List<HbtInstance>? WorkflowInstances { get; set; }

        /// <summary>
        /// 工作流活动列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtActivity.DefinitionId))]
        public List<HbtActivity>? WorkflowActivities { get; set; }

        /// <summary>
        /// 工作流转换列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtTransition.DefinitionId))]
        public List<HbtTransition>? WorkflowTransitions { get; set; }
    }
}