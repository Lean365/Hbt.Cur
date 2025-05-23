#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置实体
//===================================================================
namespace Lean.Hbt.Domain.Entities.Core
{
    /// <summary>
    /// 系统配置实体
    /// </summary>
    [SugarTable("hbt_core_config", "系统配置表")]
    [SugarIndex("ix_tenant_config", nameof(TenantId), OrderByType.Asc)]
    [SugarIndex("ix_name_config", nameof(ConfigName), OrderByType.Asc)]
    public class HbtConfig : HbtBaseEntity
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [SugarColumn(ColumnName = "config_name", ColumnDescription = "配置名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [SugarColumn(ColumnName = "config_key", ColumnDescription = "配置键名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        [SugarColumn(ColumnName = "config_value", ColumnDescription = "配置键值", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? ConfigValue { get; set; }


        /// <summary>
        /// 系统内置（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_builtin", ColumnDescription = "内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsBuiltin { get; set; } = 1;

        /// <summary>
        /// 是否加密（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_encrypted", ColumnDescription = "加密", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsEncrypted { get; set; } = 1;
        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }
    }
}