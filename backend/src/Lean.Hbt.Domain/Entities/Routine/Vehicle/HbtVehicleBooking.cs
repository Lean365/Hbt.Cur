//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtVehicleBooking.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 车辆预约管理实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆预约管理实体
    /// </summary>
    [SugarTable("hbt_routine_vehicle_booking", "车辆预约管理")]
    [SugarIndex("index_booking_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_booking_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_booking_start_time", nameof(StartTime), OrderByType.Asc)]
    [SugarIndex("index_booking_status", nameof(Status), OrderByType.Asc)]
    public class HbtVehicleBooking : HbtBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 预约人ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "预约人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 预约标题
        /// </summary>
        [SugarColumn(ColumnName = "booking_title", ColumnDescription = "预约标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string BookingTitle { get; set; } = string.Empty;

        /// <summary>
        /// 预约内容
        /// </summary>
        [SugarColumn(ColumnName = "booking_content", ColumnDescription = "预约内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BookingContent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 预约状态（0：待审批，1：已批准，2：已拒绝，3：已取消，4：已完成）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "预约状态（0：待审批，1：已批准，2：已拒绝，3：已取消，4：已完成）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 预约类型（0：公务用车，1：私人用车，2：紧急用车，3：其他）
        /// </summary>
        [SugarColumn(ColumnName = "booking_type", ColumnDescription = "预约类型（0：公务用车，1：私人用车，2：紧急用车，3：其他）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int BookingType { get; set; }

        /// <summary>
        /// 用车目的
        /// </summary>
        [SugarColumn(ColumnName = "purpose", ColumnDescription = "用车目的", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Purpose { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        [SugarColumn(ColumnName = "destination", ColumnDescription = "目的地", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Destination { get; set; }

        /// <summary>
        /// 预计里程
        /// </summary>
        [SugarColumn(ColumnName = "estimated_mileage", ColumnDescription = "预计里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? EstimatedMileage { get; set; }

        /// <summary>
        /// 乘客数量
        /// </summary>
        [SugarColumn(ColumnName = "passenger_count", ColumnDescription = "乘客数量", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PassengerCount { get; set; }

        /// <summary>
        /// 乘客信息
        /// </summary>
        [SugarColumn(ColumnName = "passengers", ColumnDescription = "乘客信息", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Passengers { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// 是否需要司机（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_driver", ColumnDescription = "是否需要司机（0：否，1：是）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedDriver { get; set; }

        /// <summary>
        /// 司机ID
        /// </summary>
        [SugarColumn(ColumnName = "driver_id", ColumnDescription = "司机ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? DriverId { get; set; }

        /// <summary>
        /// 司机姓名
        /// </summary>
        [SugarColumn(ColumnName = "driver_name", ColumnDescription = "司机姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DriverName { get; set; }

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

        /// <summary>
        /// 取消原因
        /// </summary>
        [SugarColumn(ColumnName = "cancel_reason", ColumnDescription = "取消原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? CancelReason { get; set; }

        /// <summary>
        /// 取消时间
        /// </summary>
        [SugarColumn(ColumnName = "cancel_time", ColumnDescription = "取消时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 取消人ID
        /// </summary>
        [SugarColumn(ColumnName = "cancel_user_id", ColumnDescription = "取消人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? CancelUserId { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 实际里程
        /// </summary>
        [SugarColumn(ColumnName = "actual_mileage", ColumnDescription = "实际里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? ActualMileage { get; set; }

        /// <summary>
        /// 油费
        /// </summary>
        [SugarColumn(ColumnName = "fuel_cost", ColumnDescription = "油费", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? FuelCost { get; set; }

        /// <summary>
        /// 停车费
        /// </summary>
        [SugarColumn(ColumnName = "parking_fee", ColumnDescription = "停车费", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? ParkingFee { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        [SugarColumn(ColumnName = "other_cost", ColumnDescription = "其他费用", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? OtherCost { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        [SugarColumn(ColumnName = "total_cost", ColumnDescription = "总费用", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? TotalCost { get; set; }


    }
} 