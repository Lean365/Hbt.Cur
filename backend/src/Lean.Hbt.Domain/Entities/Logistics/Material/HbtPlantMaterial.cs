#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPlantMaterial.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 工厂物料实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 工厂物料实体类（字段参照SAP标准简写）
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_logistics_plant_material", "工厂物料表")]
    [SugarIndex("ix_matnr_werks", nameof(Mandt), OrderByType.Asc, nameof(Matnr), OrderByType.Asc, nameof(Werks), OrderByType.Asc, true)]
    public class HbtPlantMaterial : HbtBaseEntity
    {
        /// <summary>集团</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }

        /// <summary>物料号</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料号", Length = 36, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Matnr { get; set; }

        /// <summary>工厂</summary>
        [SugarColumn(ColumnName = "werks", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Werks { get; set; }

        /// <summary>维护状态</summary>
        [SugarColumn(ColumnName = "pstat", ColumnDescription = "维护状态", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstat { get; set; }

        /// <summary>在工厂级标记要删除的物料</summary>
        [SugarColumn(ColumnName = "lvorm", ColumnDescription = "在工厂级标记要删除的物料", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lvorm { get; set; }

        /// <summary>评估类别</summary>
        [SugarColumn(ColumnName = "bwtty", ColumnDescription = "评估类别", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bwtty { get; set; }

        /// <summary>批量管理标识(内部)</summary>
        [SugarColumn(ColumnName = "xchar", ColumnDescription = "批量管理标识(内部)", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Xchar { get; set; }

        /// <summary>工厂特定的物料状态</summary>
        [SugarColumn(ColumnName = "mmsta", ColumnDescription = "工厂特定的物料状态", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mmsta { get; set; }

        /// <summary>工厂特定物料状态有效的起始日期</summary>
        [SugarColumn(ColumnName = "mmstd", ColumnDescription = "工厂特定物料状态有效的起始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Mmstd { get; set; }

        /// <summary>ABC标识</summary>
        [SugarColumn(ColumnName = "maabc", ColumnDescription = "ABC标识", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Maabc { get; set; }

        /// <summary>标志：关键部件</summary>
        [SugarColumn(ColumnName = "kzkri", ColumnDescription = "标志：关键部件", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kzkri { get; set; }

        /// <summary>采购组</summary>
        [SugarColumn(ColumnName = "ekgrp", ColumnDescription = "采购组", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ekgrp { get; set; }

        /// <summary>发货单位</summary>
        [SugarColumn(ColumnName = "ausme", ColumnDescription = "发货单位", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ausme { get; set; }

        /// <summary>物料: MRP 参数文件</summary>
        [SugarColumn(ColumnName = "dispr", ColumnDescription = "物料: MRP 参数文件", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Dispr { get; set; }

        /// <summary>物料需求计划类型</summary>
        [SugarColumn(ColumnName = "dismm", ColumnDescription = "物料需求计划类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Dismm { get; set; }

        /// <summary>MRP 控制者（物料计划人）</summary>
        [SugarColumn(ColumnName = "dispo", ColumnDescription = "MRP 控制者（物料计划人）", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Dispo { get; set; }

        /// <summary>标识: MRP控制者是买方(未激活的)</summary>
        [SugarColumn(ColumnName = "kzdie", ColumnDescription = "标识: MRP控制者是买方(未激活的)", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Kzdie { get; set; }

        /// <summary>计划的天数内交货</summary>
        [SugarColumn(ColumnName = "plifz", ColumnDescription = "计划的天数内交货", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Plifz { get; set; }

        /// <summary>以天计的收货处理时间</summary>
        [SugarColumn(ColumnName = "webaz", ColumnDescription = "以天计的收货处理时间", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Webaz { get; set; }

        /// <summary>期间标识</summary>
        [SugarColumn(ColumnName = "perkz", ColumnDescription = "期间标识", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Perkz { get; set; }

        /// <summary>装配报废百分比</summary>
        [SugarColumn(ColumnName = "ausss", ColumnDescription = "装配报废百分比", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? Ausss { get; set; }

        /// <summary>批量 (物料计划)</summary>
        [SugarColumn(ColumnName = "disls", ColumnDescription = "批量 (物料计划)", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Disls { get; set; }

        /// <summary>采购类型</summary>
        [SugarColumn(ColumnName = "beskz", ColumnDescription = "采购类型", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Beskz { get; set; }

        /// <summary>特殊采购类型</summary>
        [SugarColumn(ColumnName = "sobsl", ColumnDescription = "特殊采购类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Sobsl { get; set; }
    }
} 