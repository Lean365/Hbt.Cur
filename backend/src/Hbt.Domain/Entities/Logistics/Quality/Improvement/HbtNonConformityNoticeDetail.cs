#nullable enable

using SqlSugar;

namespace Hbt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 不合格通知书明细表
    /// </summary>
    [SugarTable("hbt_logistics_quality_nonconformity_notice_detail", "不合格通知书明细表")]
    public class HbtNonConformityNoticeDetail : HbtBaseEntity
    {
        /// <summary>
        /// 通知书编号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "notice_no", ColumnDescription = "通知书编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NoticeNo { get; set; } = string.Empty;

        /// <summary>
        /// 缺陷代码
        /// </summary>
        [SugarColumn(ColumnName = "defect_code", ColumnDescription = "缺陷代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectCode { get; set; }

        /// <summary>
        /// 缺陷名称
        /// </summary>
        [SugarColumn(ColumnName = "defect_name", ColumnDescription = "缺陷名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectName { get; set; }

        /// <summary>
        /// 缺陷数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_quantity", ColumnDescription = "缺陷数量", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? DefectQuantity { get; set; }

        /// <summary>
        /// 缺陷描述
        /// </summary>
        [SugarColumn(ColumnName = "defect_description", ColumnDescription = "缺陷描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        [SugarColumn(ColumnName = "action", ColumnDescription = "处理措施", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Action { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }
    }
} 