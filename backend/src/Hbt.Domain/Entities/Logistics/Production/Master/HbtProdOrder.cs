#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProdOrder.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 生产工单实体类 (基于SAP PP生产计划)
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hbt.Domain.Entities.Logistics.Production
{
    /// <summary>
    /// 生产工单实体类 (基于SAP PP生产计划)
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 生产计划模块
    /// </remarks>
    [SugarTable("hbt_logistics_prod_order", "生产工单表")]
    [SugarIndex("ix_prod_order_code", nameof(ProdOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    public class HbtProdOrder : HbtBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// ZDTA=製造指図：DTA通常生産
        /// ZDTB=製造指図：DTA改造改修
        /// ZDTC=製造指図：DTA開発試作
        /// ZDTD=製造指図：DTA通常生産 PCBA
        /// ZDTE=製造指図：DTA改造改修 PCBA
        /// ZDTF=製造指図：DTA開発試作 PCBA
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_type", ColumnDescription = "生产工单类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "ZDTA")]
        public string ProdOrderType { get; set; } = "ZDTA";
        
        /// <summary>
        /// 生产工单号
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_quantity", ColumnDescription = "生产工单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProdOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        [SugarColumn(ColumnName = "produced_quantity", ColumnDescription = "已生产数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProducedQuantity { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 生产工单状态(0=创建 1=下达 2=生产中 3=完成 4=关闭 5=取消)
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_status", ColumnDescription = "生产工单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProdOrderStatus { get; set; } = 0;

        /// <summary>
        /// 生产工单优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_priority", ColumnDescription = "生产工单优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ProdOrderPriority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        [SugarColumn(ColumnName = "production_batch", ColumnDescription = "生产批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionBatch { get; set; }

        /// <summary>
        /// 生产主管
        /// </summary>
        [SugarColumn(ColumnName = "production_supervisor", ColumnDescription = "生产主管", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionSupervisor { get; set; }

        /// <summary>
        /// 状态(0=正常  1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
    }
} 