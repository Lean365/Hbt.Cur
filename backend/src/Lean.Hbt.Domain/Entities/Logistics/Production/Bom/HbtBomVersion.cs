#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// BOM版本表（基于HbtBomHead、HbtBomDetail、HbtBomItem）
    /// </summary>
    [SugarTable("hbt_logistics_bom_version", "BOM版本表")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_head_id", nameof(BomHeadId), OrderByType.Asc, false)]
    public class HbtBomVersion : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>BOM主表Id（关联HbtBomHead）</summary>
        [SugarColumn(ColumnName = "bom_head_id", ColumnDescription = "BOM主表Id", IsNullable = false)]
        public long BomHeadId { get; set; }

        /// <summary>版本号</summary>
        [SugarColumn(ColumnName = "version_no", ColumnDescription = "版本号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VersionNo { get; set; } = string.Empty;

        /// <summary>版本类型(1=主版本 2=次版本 3=修订版本)</summary>
        [SugarColumn(ColumnName = "version_type", ColumnDescription = "版本类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int VersionType { get; set; } = 3;

        /// <summary>生效时间</summary>
        [SugarColumn(ColumnName = "effective_time", ColumnDescription = "生效时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime EffectiveTime { get; set; }

        /// <summary>失效时间</summary>
        [SugarColumn(ColumnName = "expire_time", ColumnDescription = "失效时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpireTime { get; set; }

        /// <summary>变更单号</summary>
        [SugarColumn(ColumnName = "change_order_no", ColumnDescription = "变更单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeOrderNo { get; set; }

        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "created_by", ColumnDescription = "创建人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>创建时间</summary>
        [SugarColumn(ColumnName = "created_time", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime CreatedTime { get; set; }
    }
} 