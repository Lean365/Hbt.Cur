//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtForm.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-22 11:50
// 版本号 : V.0.0.1
// 描述    : 工作流表单实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流表单实体类
    /// </summary>
    [SugarTable("hbt_workflow_form", "工作流表单表")]
    public class HbtForm : HbtBaseEntity
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        [SugarColumn(ColumnName = "form_name", ColumnDescription = "表单名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单描述
        /// </summary>
        [SugarColumn(ColumnName = "form_desc", ColumnDescription = "表单描述", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FormDesc { get; set; }

        /// <summary>
        /// 表单分类(0=系统，1=人事，2=财务，3=日常，4=后勤，5=IT，6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "form_category", ColumnDescription = "表单分类", ColumnDataType = "int", IsNullable = false)]
        public int FormCategory { get; set; } = 0;

        /// <summary>
        /// 表单配置(JSON)
        /// </summary>
        [SugarColumn(ColumnName = "form_config", ColumnDescription = "表单配置(JSON)", ColumnDataType = "text", IsNullable = false)]
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 关联的工作流定义ID
        /// </summary>
        [SugarColumn(ColumnName = "definition_id", ColumnDescription = "工作流定义ID", ColumnDataType = "bigint", IsNullable = true, DefaultValue = "0")]
        public long? DefinitionId { get; set; }

        /// <summary>
        /// 表单版本(v1.0.0-archived=已归档 v1.0.0-draft=草稿 v1.0.0-published=已发布 v1.0.0-deprecated=已弃用,v1.0.0-pending=待发布,v1.0.0-rejected=已拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "form_version", ColumnDescription = "表单版本", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FormVersion { get; set; } = string.Empty;

        /// <summary>
        /// 状态(0=草稿 (Draft)​ 1=待提交 (Pending Submission)​ 2=待提交 (Pending Submission)​​	​ 3=​已提交 (Submitted)​4=审批中 (Under Review/Approval)​​​ 5=已批准 (Approved)​​ 6=已驳回 (Rejected)​​7=​已完成 (Completed)​8=已取消 (Cancelled)​​​9=​已关闭 (Closed)​)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 