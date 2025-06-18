#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMaterial.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 物料主数据实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Logistics.Material
{
    /// <summary>
    /// 物料主数据实体类（简写字段，参考SAP MARA）
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_logistics_material", "物料主数据表")]
    [SugarIndex("ix_matnr", nameof(Matnr), OrderByType.Asc, true)]
    public class HbtMaterial : HbtBaseEntity
    {
        /// <summary>集团（客户端）</summary>
        [SugarColumn(ColumnName = "mandt", ColumnDescription = "集团", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mandt { get; set; }

        /// <summary>物料号</summary>
        [SugarColumn(ColumnName = "matnr", ColumnDescription = "物料号", Length = 18, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Matnr { get; set; }

        /// <summary>物料类型</summary>
        [SugarColumn(ColumnName = "mtart", ColumnDescription = "物料类型", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Mtart { get; set; }

        /// <summary>物料组</summary>
        [SugarColumn(ColumnName = "matkl", ColumnDescription = "物料组", Length = 9, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Matkl { get; set; }

        /// <summary>物料描述</summary>
        [SugarColumn(ColumnName = "maktx", ColumnDescription = "物料描述", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Maktx { get; set; }

        /// <summary>基本计量单位</summary>
        [SugarColumn(ColumnName = "meins", ColumnDescription = "基本计量单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? Meins { get; set; }

        /// <summary>采购组</summary>
        [SugarColumn(ColumnName = "ekgrp", ColumnDescription = "采购组", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ekgrp { get; set; }

        /// <summary>采购价值代码</summary>
        [SugarColumn(ColumnName = "bwkey", ColumnDescription = "采购价值代码", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bwkey { get; set; }

        /// <summary>旧物料号</summary>
        [SugarColumn(ColumnName = "bismt", ColumnDescription = "旧物料号", Length = 18, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Bismt { get; set; }

        /// <summary>毛重</summary>
        [SugarColumn(ColumnName = "brgew", ColumnDescription = "毛重", ColumnDataType = "decimal(13,3)", IsNullable = true)]
        public decimal? Brgew { get; set; }

        /// <summary>净重</summary>
        [SugarColumn(ColumnName = "ntgew", ColumnDescription = "净重", ColumnDataType = "decimal(13,3)", IsNullable = true)]
        public decimal? Ntgew { get; set; }

        /// <summary>重量单位</summary>
        [SugarColumn(ColumnName = "gewei", ColumnDescription = "重量单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Gewei { get; set; }

        /// <summary>体积</summary>
        [SugarColumn(ColumnName = "volum", ColumnDescription = "体积", ColumnDataType = "decimal(13,3)", IsNullable = true)]
        public decimal? Volum { get; set; }

        /// <summary>体积单位</summary>
        [SugarColumn(ColumnName = "voleh", ColumnDescription = "体积单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Voleh { get; set; }

        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "ersda", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Ersda { get; set; }

        /// <summary>创建人</summary>
        [SugarColumn(ColumnName = "ernam", ColumnDescription = "创建人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Ernam { get; set; }

        /// <summary>上次更改日期</summary>
        [SugarColumn(ColumnName = "laeda", ColumnDescription = "上次更改日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? Laeda { get; set; }

        /// <summary>更改人</summary>
        [SugarColumn(ColumnName = "aenam", ColumnDescription = "更改人", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Aenam { get; set; }

        /// <summary>维护状态</summary>
        [SugarColumn(ColumnName = "pstat", ColumnDescription = "维护状态", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Pstat { get; set; }

        /// <summary>物料状态</summary>
        [SugarColumn(ColumnName = "mstae", ColumnDescription = "物料状态", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Mstae { get; set; }

        /// <summary>
        /// 物料状态描述
        /// </summary>
        [SugarColumn(ColumnName = "mstae_desc", ColumnDescription = "物料状态描述", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeDesc { get; set; }

        /// <summary>
        /// 物料状态日期
        /// </summary>
        [SugarColumn(ColumnName = "mstae_date", ColumnDescription = "物料状态日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? MstaeDate { get; set; }

        /// <summary>
        /// 物料状态时间
        /// </summary>
        [SugarColumn(ColumnName = "mstae_time", ColumnDescription = "物料状态时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? MstaeTime { get; set; }

        /// <summary>
        /// 物料状态用户
        /// </summary>
        [SugarColumn(ColumnName = "mstae_user", ColumnDescription = "物料状态用户", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeUser { get; set; }

        /// <summary>
        /// 物料状态系统
        /// </summary>
        [SugarColumn(ColumnName = "mstae_system", ColumnDescription = "物料状态系统", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeSystem { get; set; }

        /// <summary>
        /// 物料状态类型
        /// </summary>
        [SugarColumn(ColumnName = "mstae_type", ColumnDescription = "物料状态类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeType { get; set; }

        /// <summary>
        /// 物料状态原因
        /// </summary>
        [SugarColumn(ColumnName = "mstae_reason", ColumnDescription = "物料状态原因", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeReason { get; set; }

        /// <summary>
        /// 物料状态文本
        /// </summary>
        [SugarColumn(ColumnName = "mstae_text", ColumnDescription = "物料状态文本", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeText { get; set; }

        /// <summary>
        /// 物料状态标识
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag", ColumnDescription = "物料状态标识", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlag { get; set; }

        /// <summary>
        /// 物料状态标识描述
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_desc", ColumnDescription = "物料状态标识描述", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagDesc { get; set; }

        /// <summary>
        /// 物料状态标识日期
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_date", ColumnDescription = "物料状态标识日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? MstaeFlagDate { get; set; }

        /// <summary>
        /// 物料状态标识时间
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_time", ColumnDescription = "物料状态标识时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? MstaeFlagTime { get; set; }

        /// <summary>
        /// 物料状态标识用户
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_user", ColumnDescription = "物料状态标识用户", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagUser { get; set; }

        /// <summary>
        /// 物料状态标识系统
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_system", ColumnDescription = "物料状态标识系统", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagSystem { get; set; }

        /// <summary>
        /// 物料状态标识类型
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_type", ColumnDescription = "物料状态标识类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagType { get; set; }

        /// <summary>
        /// 物料状态标识原因
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_reason", ColumnDescription = "物料状态标识原因", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagReason { get; set; }

        /// <summary>
        /// 物料状态标识文本
        /// </summary>
        [SugarColumn(ColumnName = "mstae_flag_text", ColumnDescription = "物料状态标识文本", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MstaeFlagText { get; set; }
    }
} 