#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguage.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 语言实体
//===================================================================
using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Admin
{
    /// <summary>
    /// 语言实体
    /// </summary>
    [SugarTable("hbt_sys_language", "语言表")]
    public class HbtLanguage : HbtBaseEntity
    {
        /// <summary>
        /// 语言编码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        [SugarColumn(ColumnName = "lang_name", ColumnDescription = "语言名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        [SugarColumn(ColumnName = "lang_icon", ColumnDescription = "语言图标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;
    }
}