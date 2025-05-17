#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典类型实体
//===================================================================
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Core
{
    /// <summary>
    /// 字典类型实体
    /// </summary>
    [SugarTable("hbt_core_dict_type", "字典类型表")]
    [SugarIndex("ix_tenant_dict_type", nameof(TenantId), OrderByType.Asc)]
    public class HbtDictType : HbtBaseEntity
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [SugarColumn(ColumnName = "dict_name", ColumnDescription = "字典名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        [SugarColumn(ColumnName = "dict_category", ColumnDescription = "字典类别（0系统 1SQL）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DictCategory { get; set; } = 0;

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        [SugarColumn(ColumnName = "dict_builtin", ColumnDescription = "字典内置（0否 1是）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DictBuiltin { get; set; } = 0;

        /// <summary>
        /// SQL脚本
        /// </summary>
        [SugarColumn(ColumnName = "sql_script", ColumnDescription = "SQL脚本", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
       public long TenantId { get; set; }
    }
}