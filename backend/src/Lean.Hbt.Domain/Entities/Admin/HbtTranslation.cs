#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 翻译实体
//===================================================================
using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Admin
{
    /// <summary>
    /// 翻译实体
    /// </summary>
    [SugarTable("hbt_translation", "翻译表")]
    public class HbtTranslation : HbtBaseEntity
    {
        /// <summary>
        /// 语言编码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        [SugarColumn(ColumnName = "trans_value", ColumnDescription = "翻译值", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块
        /// </summary>
        [SugarColumn(ColumnName = "module", ColumnDescription = "模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Module { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;
    }
}