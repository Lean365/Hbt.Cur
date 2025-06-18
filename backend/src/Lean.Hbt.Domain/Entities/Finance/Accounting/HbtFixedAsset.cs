#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFixedAsset.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 固定资产实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Finance.Accounting
{
    /// <summary>
    /// 固定资产实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_accounting_fixed_asset", "固定资产表")]
    [SugarIndex("ix_asset_code", nameof(AssetCode), OrderByType.Asc, true)]
    public class HbtFixedAsset : HbtBaseEntity
    {


        /// <summary>
        /// 资产编码
        /// </summary>
        [SugarColumn(ColumnName = "asset_code", ColumnDescription = "资产编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? AssetCode { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [SugarColumn(ColumnName = "asset_name", ColumnDescription = "资产名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? AssetName { get; set; }

        /// <summary>
        /// 资产类别
        /// </summary>
        [SugarColumn(ColumnName = "asset_category", ColumnDescription = "资产类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? AssetCategory { get; set; }

        /// <summary>
        /// 资产规格型号
        /// </summary>
        [SugarColumn(ColumnName = "asset_spec", ColumnDescription = "资产规格型号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetSpec { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "计量单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 原值
        /// </summary>
        [SugarColumn(ColumnName = "original_value", ColumnDescription = "原值", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// 累计折旧
        /// </summary>
        [SugarColumn(ColumnName = "accumulated_depreciation", ColumnDescription = "累计折旧", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal AccumulatedDepreciation { get; set; }

        /// <summary>
        /// 净值
        /// </summary>
        [SugarColumn(ColumnName = "net_value", ColumnDescription = "净值", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal NetValue { get; set; }

        /// <summary>
        /// 使用部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "使用部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 使用人ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "使用人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? UserId { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "存放地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Location { get; set; }

        /// <summary>
        /// 购置日期
        /// </summary>
        [SugarColumn(ColumnName = "purchase_date", ColumnDescription = "购置日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// 开始使用日期
        /// </summary>
        [SugarColumn(ColumnName = "start_use_date", ColumnDescription = "开始使用日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartUseDate { get; set; }

        /// <summary>
        /// 预计使用年限
        /// </summary>
        [SugarColumn(ColumnName = "expected_life", ColumnDescription = "预计使用年限", ColumnDataType = "int", IsNullable = false)]
        public int ExpectedLife { get; set; }

        /// <summary>
        /// 折旧方法
        /// </summary>
        [SugarColumn(ColumnName = "depreciation_method", ColumnDescription = "折旧方法", ColumnDataType = "int", IsNullable = false)]
        public int DepreciationMethod { get; set; }

        /// <summary>
        /// 折旧率
        /// </summary>
        [SugarColumn(ColumnName = "depreciation_rate", ColumnDescription = "折旧率", ColumnDataType = "decimal(18,4)", IsNullable = false)]
        public decimal DepreciationRate { get; set; }

        /// <summary>
        /// 资产状态(0=闲置 1=在用 2=维修 3=报废)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "资产状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }


    }
}