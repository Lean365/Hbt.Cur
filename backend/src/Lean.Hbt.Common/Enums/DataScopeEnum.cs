//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : DataScopeEnum.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 12:15
// 版本号 : V0.0.1
// 描述    : 数据范围枚举定义
//===================================================================

namespace Lean.Hbt.Common.Enums
{
    /// <summary>
    /// 数据范围枚举
    /// </summary>
    public enum DataScope
    {
        /// <summary>
        /// 全部数据权限
        /// </summary>
        All = 1,

        /// <summary>
        /// 自定数据权限
        /// </summary>
        Custom = 2,

        /// <summary>
        /// 本部门数据权限
        /// </summary>
        Department = 3,

        /// <summary>
        /// 本部门及以下数据权限
        /// </summary>
        DepartmentAndBelow = 4,

        /// <summary>
        /// 仅本人数据权限
        /// </summary>
        Self = 5
    }
} 