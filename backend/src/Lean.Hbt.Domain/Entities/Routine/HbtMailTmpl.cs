//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtMailTmpl.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 邮件模板实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 邮件模板实体
    /// </summary>
    [SugarTable("hbt_rou_mail_tmpl", "邮件模板")]
    [SugarIndex("index_tmpl_code", nameof(TmplCode), OrderByType.Asc, true)]
    [SugarIndex("index_tmpl_name", nameof(TmplName), OrderByType.Asc)]
    [SugarIndex("index_tmpl_status", nameof(TmplStatus), OrderByType.Asc)]
    public class HbtMailTmpl : HbtBaseEntity
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_name", ColumnDescription = "模板名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TmplName { get; set; } = string.Empty;

        /// <summary>
        /// 模板编码
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_code", ColumnDescription = "模板编码", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TmplCode { get; set; } = string.Empty;

        /// <summary>
        /// 主题
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_subject", ColumnDescription = "主题", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TmplSubject { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_body", ColumnDescription = "内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string TmplBody { get; set; } = string.Empty;

        /// <summary>
        /// 是否HTML
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_is_html", ColumnDescription = "是否HTML", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_parameters", ColumnDescription = "参数列表", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        [SugarColumn(ColumnName = "tmpl_status", ColumnDescription = "状态（0停用 1启用）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int TmplStatus { get; set; }
    }
}