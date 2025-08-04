//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtScheme.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义实体类
//===================================================================

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Workflow
{
    /// <summary>
    /// 工作流定义实体类
    /// </summary>
    [SugarTable("hbt_workflow_scheme", "工作流定义表")]
    [SugarIndex("ix_workflow_scheme_key", nameof(SchemeKey), OrderByType.Asc)]
    [SugarIndex("ix_workflow_scheme_category", nameof(SchemeCategory), OrderByType.Asc)]
    [SugarIndex("ix_workflow_scheme_status", nameof(Status), OrderByType.Asc)]
    public class HbtScheme : HbtBaseEntity
    {

        /// <summary>
        /// 流程定义键
        /// </summary>
        [SugarColumn(ColumnName = "scheme_key", ColumnDescription = "流程键", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        [SugarColumn(ColumnName = "scheme_name", ColumnDescription = "流程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他)
        /// </summary>
        [SugarColumn(ColumnName = "scheme_category", ColumnDescription = "流程分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SchemeCategory { get; set; }

        /// <summary>
        /// 流程定义版本
        /// </summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "流程版本", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0")]
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 流程定义配置(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "scheme_config", ColumnDescription = "流程配置", ColumnDataType = "text", IsNullable = false)]
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 表单定义ID
        /// </summary>
        [SugarColumn(ColumnName = "form_id", ColumnDescription = "表单ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? FormId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? Description { get; set; }
    }
} 