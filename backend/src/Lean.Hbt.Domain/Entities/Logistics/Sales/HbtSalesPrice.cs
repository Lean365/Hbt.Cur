#nullable enable

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.Logistics.Material;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSalesPrice.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 销售价格实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售价格
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_logistics_sales_price", "销售价格表")]
    [SugarIndex("ix_sales_price_customer_material", nameof(Kunnr), OrderByType.Asc, nameof(Matnr), OrderByType.Asc, true)]
    public class HbtSalesPrice : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }
        /// <summary>条件记录号</summary>
        [SugarColumn(ColumnName = "knumh", ColumnDescription = "条件记录号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Knumh { get; set; }
        /// <summary>条件类型</summary>
        [SugarColumn(ColumnName = "kschl", ColumnDescription = "条件类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Kschl { get; set; }
        /// <summary>条件表</summary>
        [SugarColumn(ColumnName = "kotabnr", ColumnDescription = "条件表", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Kotabnr { get; set; }
        /// <summary>有效从</summary>
        [SugarColumn(ColumnName = "datab", ColumnDescription = "有效从", ColumnDataType = "date", IsNullable = false)]
        public DateTime? Datab { get; set; }
        /// <summary>有效至</summary>
        [SugarColumn(ColumnName = "datbi", ColumnDescription = "有效至", ColumnDataType = "date", IsNullable = false)]
        public DateTime? Datbi { get; set; }
        /// <summary>客户</summary>
        [SugarColumn(ColumnName = "kunnr", ColumnDescription = "客户", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kunnr { get; set; }
        /// <summary>物料</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matnr { get; set; }
        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Werks { get; set; }
        /// <summary>销售组织</summary>
        [SugarColumn(ColumnName = "vkorg", ColumnDescription = "销售组织", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vkorg { get; set; }
        /// <summary>分销渠道</summary>
        [SugarColumn(ColumnName = "vtweg", ColumnDescription = "分销渠道", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Vtweg { get; set; }
        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "kpein", ColumnDescription = "价格单位", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Kpein { get; set; }
        /// <summary>条件金额</summary>
        [SugarColumn(ColumnName = "kbetr", ColumnDescription = "条件金额", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? Kbetr { get; set; }
        /// <summary>货币</summary>
        [SugarColumn(ColumnName = "konwa", ColumnDescription = "货币", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Konwa { get; set; }
        /// <summary>条件单位</summary>
        [SugarColumn(ColumnName = "kmein", ColumnDescription = "条件单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kmein { get; set; }

    }
} 