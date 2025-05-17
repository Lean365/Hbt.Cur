#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译实体
//===================================================================
using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Entities.Core
{
    /// <summary>
    /// 翻译实体
    /// </summary>
    [SugarTable("hbt_core_translation", "翻译表")]
    [SugarIndex("ix_tenant_translation", nameof(TenantId), OrderByType.Asc)]
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
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
       public long TenantId { get; set; }

        /// <summary>
        /// 租户导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 翻译内置（0否 1是）
        /// </summary>
        [SugarColumn(ColumnName = "trans_builtin", ColumnDescription = "翻译内置（0否 1是）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TransBuiltin { get; set; } = 0;
    }
}