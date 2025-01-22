//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCommonEnum.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 通用枚举定义
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 通用状态枚举
    /// </summary>
    public enum HbtStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 停用
        /// </summary>
        Disabled = 1
    }

    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum HbtGender
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 男
        /// </summary>
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        Female = 2
    }

    /// <summary>
    /// 菜单类型枚举
    /// </summary>
    public enum HbtMenuType
    {
        /// <summary>
        /// 目录
        /// </summary>
        Directory = 0,

        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 1,

        /// <summary>
        /// 按钮
        /// </summary>
        Button = 2
    }

    /// <summary>
    /// 显示状态枚举
    /// </summary>
    public enum HbtVisible
    {
        /// <summary>
        /// 显示
        /// </summary>
        Show = 0,

        /// <summary>
        /// 隐藏
        /// </summary>
        Hide = 1
    }

    /// <summary>
    /// 是否枚举
    /// </summary>
    public enum HbtYesNo
    {
        /// <summary>
        /// 否
        /// </summary>
        No = 0,

        /// <summary>
        /// 是
        /// </summary>
        Yes = 1
    }

    /// <summary>
    /// 用户类型枚举
    /// </summary>
    public enum HbtUserType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 0,

        /// <summary>
        /// 普通用户
        /// </summary>
        User = 1,

        /// <summary>
        /// OAuth用户
        /// </summary>
        OAuth = 2
    }
}