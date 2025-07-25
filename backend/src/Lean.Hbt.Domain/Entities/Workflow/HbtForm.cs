//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtForm.cs 
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单实体类（整合定义和实例）
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Workflow
{
    /// <summary>
    /// 表单实体类（整合定义和实例）
    /// </summary>
    [SugarTable("hbt_workflow_form", "流程表单")]
    [SugarIndex("ix_form_key", nameof(FormKey), OrderByType.Asc)]
    [SugarIndex("ix_form_type", nameof(FormType), OrderByType.Asc)]
    [SugarIndex("ix_form_category", nameof(FormCategory), OrderByType.Asc)]
    [SugarIndex("ix_form_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_form_instance", nameof(InstanceId), OrderByType.Asc)]
    public class HbtForm : HbtBaseEntity
    {
        /// <summary>
        /// 表单键
        /// </summary>
        [SugarColumn(ColumnName = "form_key", ColumnDescription = "表单键", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        [SugarColumn(ColumnName = "form_name", ColumnDescription = "表单名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        [SugarColumn(ColumnName = "form_category", ColumnDescription = "表单分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        [SugarColumn(ColumnName = "form_type", ColumnDescription = "表单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int FormType { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "表单版本", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0")]
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "form_config", ColumnDescription = "表单配置", ColumnDataType = "text", IsNullable = false)]
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? InstanceId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "form_data", ColumnDescription = "表单数据", ColumnDataType = "text", IsNullable = true)]
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已作废)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? Description { get; set; }

        /// <summary>
        /// 工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
        public HbtInstance? WorkflowInstance { get; set; }
    }
} 