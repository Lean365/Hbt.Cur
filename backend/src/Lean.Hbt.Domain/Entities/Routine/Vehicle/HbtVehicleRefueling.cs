//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtVehicleRefueling.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 车辆加油管理实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆加油管理实体
    /// </summary>
    [SugarTable("hbt_routine_vehicle_refueling", "车辆加油管理")]
    [SugarIndex("index_refueling_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_refueling_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_refueling_station_id", nameof(StationId), OrderByType.Asc)]
    [SugarIndex("index_refueling_refuel_time", nameof(RefuelTime), OrderByType.Asc)]
    [SugarIndex("index_refueling_status", nameof(Status), OrderByType.Asc)]
    public class HbtVehicleRefueling : HbtBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 加油人ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "加油人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 加油站ID
        /// </summary>
        [SugarColumn(ColumnName = "station_id", ColumnDescription = "加油站ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? StationId { get; set; }

        /// <summary>
        /// 加油站名称
        /// </summary>
        [SugarColumn(ColumnName = "station_name", ColumnDescription = "加油站名称", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? StationName { get; set; }

        /// <summary>
        /// 加油站地址
        /// </summary>
        [SugarColumn(ColumnName = "station_address", ColumnDescription = "加油站地址", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? StationAddress { get; set; }

        /// <summary>
        /// 加油时间
        /// </summary>
        [SugarColumn(ColumnName = "refuel_time", ColumnDescription = "加油时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime RefuelTime { get; set; }

        /// <summary>
        /// 加油状态（0：计划中，1：进行中，2：已完成，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "加油状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 加油类型（0：92号汽油，1：95号汽油，2：98号汽油，3：0号柴油，4：-10号柴油，5：-20号柴油，6：其他）
        /// </summary>
        [SugarColumn(ColumnName = "fuel_type", ColumnDescription = "加油类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FuelType { get; set; }

        /// <summary>
        /// 加油前里程
        /// </summary>
        [SugarColumn(ColumnName = "before_mileage", ColumnDescription = "加油前里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? BeforeMileage { get; set; }

        /// <summary>
        /// 加油后里程
        /// </summary>
        [SugarColumn(ColumnName = "after_mileage", ColumnDescription = "加油后里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? AfterMileage { get; set; }

        /// <summary>
        /// 加油前油量（升）
        /// </summary>
        [SugarColumn(ColumnName = "before_fuel_level", ColumnDescription = "加油前油量", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? BeforeFuelLevel { get; set; }

        /// <summary>
        /// 加油后油量（升）
        /// </summary>
        [SugarColumn(ColumnName = "after_fuel_level", ColumnDescription = "加油后油量", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? AfterFuelLevel { get; set; }

        /// <summary>
        /// 加油量（升）
        /// </summary>
        [SugarColumn(ColumnName = "refuel_amount", ColumnDescription = "加油量", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal RefuelAmount { get; set; }

        /// <summary>
        /// 单价（元/升）
        /// </summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 总金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "总金额", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 优惠金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "优惠金额", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 实付金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "actual_amount", ColumnDescription = "实付金额", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 支付方式（0：现金，1：银行卡，2：微信，3：支付宝，4：油卡，5：其他）
        /// </summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "支付方式", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PaymentMethod { get; set; }

        /// <summary>
        /// 支付状态（0：未支付，1：已支付，2：已退款）
        /// </summary>
        [SugarColumn(ColumnName = "payment_status", ColumnDescription = "支付状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PaymentStatus { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [SugarColumn(ColumnName = "payment_time", ColumnDescription = "支付时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        [SugarColumn(ColumnName = "transaction_no", ColumnDescription = "交易流水号", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TransactionNo { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [SugarColumn(ColumnName = "invoice_no", ColumnDescription = "发票号码", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvoiceNo { get; set; }

        /// <summary>
        /// 发票金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "invoice_amount", ColumnDescription = "发票金额", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? InvoiceAmount { get; set; }

        /// <summary>
        /// 发票状态（0：未开票，1：已开票，2：已作废）
        /// </summary>
        [SugarColumn(ColumnName = "invoice_status", ColumnDescription = "发票状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int InvoiceStatus { get; set; }

        /// <summary>
        /// 开票时间
        /// </summary>
        [SugarColumn(ColumnName = "invoice_time", ColumnDescription = "开票时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? InvoiceTime { get; set; }

        /// <summary>
        /// 发票图片
        /// </summary>
        [SugarColumn(ColumnName = "invoice_images", ColumnDescription = "发票图片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvoiceImages { get; set; }

        /// <summary>
        /// 加油枪号
        /// </summary>
        [SugarColumn(ColumnName = "pump_no", ColumnDescription = "加油枪号", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? PumpNo { get; set; }

        /// <summary>
        /// 加油员
        /// </summary>
        [SugarColumn(ColumnName = "attendant", ColumnDescription = "加油员", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Attendant { get; set; }

        /// <summary>
        /// 加油员工号
        /// </summary>
        [SugarColumn(ColumnName = "attendant_no", ColumnDescription = "加油员工号", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AttendantNo { get; set; }

        /// <summary>
        /// 是否满油（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_full_tank", ColumnDescription = "是否满油", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsFullTank { get; set; }

        /// <summary>
        /// 油箱容量（升）
        /// </summary>
        [SugarColumn(ColumnName = "tank_capacity", ColumnDescription = "油箱容量", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? TankCapacity { get; set; }

        /// <summary>
        /// 油耗（升/百公里）
        /// </summary>
        [SugarColumn(ColumnName = "fuel_consumption", ColumnDescription = "油耗", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? FuelConsumption { get; set; }

        /// <summary>
        /// 预计续航里程（公里）
        /// </summary>
        [SugarColumn(ColumnName = "estimated_range", ColumnDescription = "预计续航里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? EstimatedRange { get; set; }

        /// <summary>
        /// 加油原因（0：正常加油，1：紧急加油，2：长途加油，3：其他）
        /// </summary>
        [SugarColumn(ColumnName = "refuel_reason", ColumnDescription = "加油原因（0：正常加油，1：紧急加油，2：长途加油，3：其他）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RefuelReason { get; set; }

        /// <summary>
        /// 加油备注
        /// </summary>
        [SugarColumn(ColumnName = "refuel_remarks", ColumnDescription = "加油备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RefuelRemarks { get; set; }

        /// <summary>
        /// 天气情况
        /// </summary>
        [SugarColumn(ColumnName = "weather", ColumnDescription = "天气情况", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Weather { get; set; }

        /// <summary>
        /// 温度（摄氏度）
        /// </summary>
        [SugarColumn(ColumnName = "temperature", ColumnDescription = "温度（摄氏度）", IsNullable = true, ColumnDataType = "decimal(5,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? Temperature { get; set; }

        /// <summary>
        /// GPS坐标
        /// </summary>
        [SugarColumn(ColumnName = "gps_coordinates", ColumnDescription = "GPS坐标", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? GpsCoordinates { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "地理位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Location { get; set; }

        /// <summary>
        /// 是否异常加油（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_abnormal", ColumnDescription = "是否异常加油（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsAbnormal { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        [SugarColumn(ColumnName = "abnormal_reason", ColumnDescription = "异常原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AbnormalReason { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [SugarColumn(ColumnName = "approval_time", ColumnDescription = "审批时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalRemarks { get; set; }


    }
} 