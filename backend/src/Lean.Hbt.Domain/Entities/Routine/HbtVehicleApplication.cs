//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtVehicleApplication.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 用车申请实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 用车申请实体
    /// </summary>
    [SugarTable("hbt_routine_vehicle_application", "用车申请")]
    [SugarIndex("index_vehicle_app_vehicle", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_vehicle_app_user", nameof(ApplicantId), OrderByType.Asc)]
    [SugarIndex("index_vehicle_app_status", nameof(Status), OrderByType.Asc)]
    public class HbtVehicleApplication : HbtBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ApplicantName { get; set; } = string.Empty;

        /// <summary>
        /// 申请状态（0：待审批，1：已批准，2：已拒绝，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "申请状态（0：待审批，1：已批准，2：已拒绝，3：已取消）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 用车事由
        /// </summary>
        [SugarColumn(ColumnName = "reason", ColumnDescription = "用车事由", Length = 500, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 出发地点
        /// </summary>
        [SugarColumn(ColumnName = "start_location", ColumnDescription = "出发地点", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string StartLocation { get; set; } = string.Empty;

        /// <summary>
        /// 目的地
        /// </summary>
        [SugarColumn(ColumnName = "end_location", ColumnDescription = "目的地", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string EndLocation { get; set; } = string.Empty;

        /// <summary>
        /// 出发时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "出发时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 预计返回时间
        /// </summary>
        [SugarColumn(ColumnName = "expected_end_time", ColumnDescription = "预计返回时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ExpectedEndTime { get; set; }

        /// <summary>
        /// 实际返回时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际返回时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 同行人数
        /// </summary>
        [SugarColumn(ColumnName = "passenger_count", ColumnDescription = "同行人数", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PassengerCount { get; set; }

        /// <summary>
        /// 同行人员
        /// </summary>
        [SugarColumn(ColumnName = "passengers", ColumnDescription = "同行人员", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Passengers { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        [SugarColumn(ColumnName = "approver_name", ColumnDescription = "审批人姓名", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproverName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [SugarColumn(ColumnName = "approve_time", ColumnDescription = "审批时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approve_comment", ColumnDescription = "审批意见", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproveComment { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

    }
}