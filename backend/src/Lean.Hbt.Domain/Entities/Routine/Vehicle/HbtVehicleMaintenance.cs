//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtVehicleMaintenance.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 车辆维保管理实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆维保管理实体
    /// </summary>
    [SugarTable("hbt_routine_vehicle_maintenance", "车辆维保管理")]
    [SugarIndex("index_maintenance_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_maintenance_type", nameof(MaintenanceType), OrderByType.Asc)]
    [SugarIndex("index_maintenance_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_maintenance_plan_date", nameof(PlanDate), OrderByType.Asc)]
    public class HbtVehicleMaintenance : HbtBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 维保类型（0：日常保养，1：定期保养，2：维修，3：年检，4：保险，5：其他）
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_type", ColumnDescription = "维保类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MaintenanceType { get; set; }

        /// <summary>
        /// 维保标题
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_title", ColumnDescription = "维保标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MaintenanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 维保内容
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_content", ColumnDescription = "维保内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceContent { get; set; }

        /// <summary>
        /// 维保状态（0：计划中，1：进行中，2：已完成，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "维保状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 计划日期
        /// </summary>
        [SugarColumn(ColumnName = "plan_date", ColumnDescription = "计划日期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际完成时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 维保前里程
        /// </summary>
        [SugarColumn(ColumnName = "before_mileage", ColumnDescription = "维保前里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? BeforeMileage { get; set; }

        /// <summary>
        /// 维保后里程
        /// </summary>
        [SugarColumn(ColumnName = "after_mileage", ColumnDescription = "维保后里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? AfterMileage { get; set; }

        /// <summary>
        /// 维保周期（天）
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_cycle", ColumnDescription = "维保周期", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? MaintenanceCycle { get; set; }

        /// <summary>
        /// 下次维保日期
        /// </summary>
        [SugarColumn(ColumnName = "next_maintenance_date", ColumnDescription = "下次维保日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
        /// 维保费用
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_cost", ColumnDescription = "维保费用", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? MaintenanceCost { get; set; }

        /// <summary>
        /// 维保厂家
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_shop", ColumnDescription = "维保厂家", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceShop { get; set; }

        /// <summary>
        /// 维保厂家地址
        /// </summary>
        [SugarColumn(ColumnName = "shop_address", ColumnDescription = "维保厂家地址", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ShopAddress { get; set; }

        /// <summary>
        /// 维保厂家电话
        /// </summary>
        [SugarColumn(ColumnName = "shop_phone", ColumnDescription = "维保厂家电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ShopPhone { get; set; }

        /// <summary>
        /// 维保人员
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_person", ColumnDescription = "维保人员", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenancePerson { get; set; }

        /// <summary>
        /// 维保人员电话
        /// </summary>
        [SugarColumn(ColumnName = "person_phone", ColumnDescription = "维保人员电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? PersonPhone { get; set; }

        /// <summary>
        /// 维保项目
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_items", ColumnDescription = "维保项目", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceItems { get; set; }

        /// <summary>
        /// 更换配件
        /// </summary>
        [SugarColumn(ColumnName = "replaced_parts", ColumnDescription = "更换配件", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReplacedParts { get; set; }

        /// <summary>
        /// 维保结果
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_result", ColumnDescription = "维保结果", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceResult { get; set; }

        /// <summary>
        /// 维保质量评价（1-5星）
        /// </summary>
        [SugarColumn(ColumnName = "quality_rating", ColumnDescription = "维保质量评价", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? QualityRating { get; set; }

        /// <summary>
        /// 维保评价
        /// </summary>
        [SugarColumn(ColumnName = "quality_remarks", ColumnDescription = "维保评价", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QualityRemarks { get; set; }

        /// <summary>
        /// 维保单据号
        /// </summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "维保单据号", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// 维保单据图片
        /// </summary>
        [SugarColumn(ColumnName = "invoice_images", ColumnDescription = "维保单据图片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvoiceImages { get; set; }

        /// <summary>
        /// 维保前照片
        /// </summary>
        [SugarColumn(ColumnName = "before_images", ColumnDescription = "维保前照片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BeforeImages { get; set; }

        /// <summary>
        /// 维保后照片
        /// </summary>
        [SugarColumn(ColumnName = "after_images", ColumnDescription = "维保后照片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AfterImages { get; set; }

        /// <summary>
        /// 是否紧急维保（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急维保", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsUrgent { get; set; }

        /// <summary>
        /// 紧急原因
        /// </summary>
        [SugarColumn(ColumnName = "urgent_reason", ColumnDescription = "紧急原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? UrgentReason { get; set; }

        /// <summary>
        /// 维保负责人ID
        /// </summary>
        [SugarColumn(ColumnName = "responsible_user_id", ColumnDescription = "维保负责人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ResponsibleUserId { get; set; }

        /// <summary>
        /// 维保负责人姓名
        /// </summary>
        [SugarColumn(ColumnName = "responsible_user_name", ColumnDescription = "维保负责人姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ResponsibleUserName { get; set; }


    }
} 