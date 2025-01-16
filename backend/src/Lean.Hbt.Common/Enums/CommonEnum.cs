//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : CommonEnum.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 12:10
// 版本号 : V0.0.1
// 描述    : 通用枚举定义
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 通用状态枚举
    /// </summary>
    public enum CommonStatus
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
    public enum Gender
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
    public enum MenuType
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
    public enum VisibleStatus
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
    public enum YesNo
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
} 