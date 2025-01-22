#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
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
    [SugarTable("hbt_sys_translation", "翻译表")]
    public class HbtTranslation : HbtBaseEntity
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        [SugarColumn(ColumnName = "trans_value", ColumnDescription = "翻译值", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false)]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;
    }
}