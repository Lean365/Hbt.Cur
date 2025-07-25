#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginDevLog.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录设备日志实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 登录设备日志实体
    /// </summary>
    [SugarTable("hbt_audit_login_dev_log", "登录设备")]
    [SugarIndex("ix_device_user", nameof(UserId), OrderByType.Asc, nameof(DeviceId), OrderByType.Asc, true)]
    public class HbtLoginDevLog : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        [SugarColumn(ColumnName = "device_id", ColumnDescription = "设备标识", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型(0:PC,1:手机,2:Pad,3:其他)
        /// </summary>
        [SugarColumn(ColumnName = "device_type", ColumnDescription = "设备类型", ColumnDataType = "int", IsNullable = false)]
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "device_name", ColumnDescription = "设备名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [SugarColumn(ColumnName = "device_model", ColumnDescription = "设备型号", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceModel { get; set; }

        /// <summary>
        /// 设备令牌
        /// </summary>
        [SugarColumn(ColumnName = "device_token", ColumnDescription = "设备令牌", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceToken { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        [SugarColumn(ColumnName = "device_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false)]
        public int DeviceStatus { get; set; }


        /// <summary>
        /// 最后在线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_online_time", ColumnDescription = "最后在线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_offline_time", ColumnDescription = "最后离线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        [SugarColumn(ColumnName = "today_online_periods", ColumnDescription = "今日在线时段", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TodayOnlinePeriods { get; set; }
    }
}