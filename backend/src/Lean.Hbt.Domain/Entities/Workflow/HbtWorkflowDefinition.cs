//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDefinition.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流定义实体类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

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
        /// 流程配置(JSON格式)
        /// 包含：节点布局、连线、条件等
        /// </summary>
        [SugarColumn(ColumnName = "workflow_config", ColumnDescription = "流程配置(JSON格式)", ColumnDataType = "text", IsNullable = false)]
        public string? WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "工作流状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 流程节点列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtWorkflowNode.WorkflowDefinitionId))]
        public List<HbtWorkflowNode>? WorkflowNodes { get; set; }
    }
}