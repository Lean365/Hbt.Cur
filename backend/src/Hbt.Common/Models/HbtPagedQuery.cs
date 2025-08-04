//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtPagedQuery.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 21:35
// 版本号 : V.0.0.1
// 描述    : 分页查询基类
//===================================================================

using Newtonsoft.Json;

namespace Hbt.Common.Models
{
    /// <summary>
    /// 分页查询基类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public abstract class HbtPagedQuery
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序列
        /// </summary>
        public string? OrderByColumn { get; set; }

        /// <summary>
        /// 排序方向(desc/asc)
        /// </summary>
        public string? OrderType { get; set; }
    }
} 