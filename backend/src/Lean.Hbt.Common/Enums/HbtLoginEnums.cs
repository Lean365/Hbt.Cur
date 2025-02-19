#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginEnums.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录相关枚举
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 登录类型枚举
    /// </summary>
    public enum HbtLoginType
    {
        /// <summary>
        /// 普通登录
        /// </summary>
        Normal = 0,

        /// <summary>
        /// OAuth登录
        /// </summary>
        OAuth = 1
    }

    /// <summary>
    /// 登录来源枚举
    /// </summary>
    public enum HbtLoginSource
    {
        /// <summary>
        /// 网页
        /// </summary>
        Web = 0,

        /// <summary>
        /// APP
        /// </summary>
        App = 1,

        /// <summary>
        /// 小程序
        /// </summary>
        MiniProgram = 2
    }

    /// <summary>
    /// 登录状态枚举
    /// </summary>
    public enum HbtLoginStatus
    {
        /// <summary>
        /// 离线
        /// </summary>
        Offline = 0,

        /// <summary>
        /// 在线
        /// </summary>
        Online = 1,

        /// <summary>
        /// 登录失败
        /// </summary>
        Failed = 2,

        /// <summary>
        /// 账号锁定
        /// </summary>
        Locked = 3,

        /// <summary>
        /// 账号禁用
        /// </summary>
        Disabled = 4,

        /// <summary>
        /// 会话过期
        /// </summary>
        Expired = 5
    }

    /// <summary>
    /// 设备类型枚举
    /// </summary>
    public enum HbtDeviceType
    {
        /// <summary>
        /// PC
        /// </summary>
        PC = 0,

        /// <summary>
        /// Android
        /// </summary>
        Android = 1,

        /// <summary>
        /// iOS
        /// </summary>
        iOS = 2
    }

    /// <summary>
    /// 浏览器类型枚举
    /// </summary>
    public enum HbtBrowserType
    {
        /// <summary>
        /// Chrome
        /// </summary>
        Chrome = 0,

        /// <summary>
        /// Firefox
        /// </summary>
        Firefox = 1,

        /// <summary>
        /// Safari
        /// </summary>
        Safari = 2,

        /// <summary>
        /// Edge
        /// </summary>
        Edge = 3,

        /// <summary>
        /// IE
        /// </summary>
        IE = 4,

        /// <summary>
        /// Opera
        /// </summary>
        Opera = 5,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 99
    }

    /// <summary>
    /// 操作系统类型枚举
    /// </summary>
    public enum HbtOsType
    {
        /// <summary>
        /// Windows
        /// </summary>
        Windows = 0,

        /// <summary>
        /// Linux
        /// </summary>
        Linux = 1,

        /// <summary>
        /// MacOS
        /// </summary>
        MacOS = 2,

        /// <summary>
        /// Android
        /// </summary>
        Android = 3,

        /// <summary>
        /// iOS
        /// </summary>
        iOS = 4,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 99
    }
} 