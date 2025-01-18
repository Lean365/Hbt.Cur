//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : EnumExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:55
// 版本号 : V0.0.1
// 描述   : 枚举扩展方法
//===================================================================

using System.ComponentModel;
using System.Reflection;

namespace Lean.Hbt.Common.Extensions;

/// <summary>
/// 枚举扩展方法
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// 获取枚举的Description特性描述
    /// </summary>
    /// <param name="value">枚举值</param>
    /// <returns>描述</returns>
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        var field = value.GetType().GetField(value.ToString());
        if (field == null)
        {
            return value.ToString();
        }

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
} 