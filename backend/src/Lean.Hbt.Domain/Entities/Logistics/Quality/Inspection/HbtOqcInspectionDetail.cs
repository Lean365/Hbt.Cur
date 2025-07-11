#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Logistics.Quality.Inspection
{
    /// <summary>
    /// 出货检验明细（OQC）实体
    /// </summary>
    [SugarTable("hbt_logistics_quality_oqc_inspection_detail", "出货检验明细(OQC)表")]
    public class HbtOqcInspectionDetail : HbtBaseEntity
    {
        /// <summary>
        /// 检验单号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "inspection_no", ColumnDescription = "检验单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string InspectionNo { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目
        /// </summary>
        [SugarColumn(ColumnName = "item", ColumnDescription = "检验项目", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Item { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        [SugarColumn(ColumnName = "standard", ColumnDescription = "检验标准", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Standard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "检验结果", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        [SugarColumn(ColumnName = "defect_desc", ColumnDescription = "不良描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectDesc { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        [SugarColumn(ColumnName = "action", ColumnDescription = "处理措施", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Action { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }
    }
} 