//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 登录日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            LoginMessage = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long LoginLogId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public HbtLoginType LoginType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public HbtLoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public int LoginSource { get; set; }

        /// <summary>
        /// 是否成功（0失败 1成功）
        /// </summary>
        public int LoginSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string? LoginMessage { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public HbtSignalRDevice? DeviceInfo { get; set; }

        /// <summary>
        /// 登录设备ID
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 登录设备日志
        /// </summary>
        public HbtLoginDevLogDto? LoginDevLog { get; set; }

        /// <summary>
        /// 环境信息
        /// </summary>
        public HbtSignalREnvironment? EnvironmentInfo { get; set; }

        /// <summary>
        /// 登录环境日志ID
        /// </summary>
        public string? EnvironmentId { get; set; }

        /// <summary>
        /// 登录环境日志信息
        /// </summary>
        public HbtLoginEnvLogDto? LoginEnvLog { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

    }

    /// <summary>
    /// 登录日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogQueryDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public int? LoginSuccess { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public HbtLoginType? LoginType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public HbtLoginStatus? LoginStatus { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public HbtDeviceType? DeviceType { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public HbtOsType? OsType { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public HbtBrowserType? BrowserType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 登录日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginLogExportDto()
        {
            UserName = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            LoginMessage = string.Empty;
            CreateTime = DateTime.Now;
            DeviceName = string.Empty;
            OsVersion = string.Empty;
            BrowserVersion = string.Empty;
            Location = string.Empty;
            LoginType = string.Empty;
            LoginStatus = string.Empty;
            DeviceType = string.Empty;
            OsType = string.Empty;
            BrowserType = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public string LoginStatus { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public string BrowserType { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public int LoginSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string? LoginMessage { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}