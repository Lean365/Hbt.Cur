//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtRepository.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 14:10
// 版本号 : V0.0.1
// 描述    : SqlSugar通用仓储接口
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Repositories
{
    /// <summary>
    /// SqlSugar通用仓储接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface IHbtRepository<T> where T : class, new()
    {
        /// <summary>
        /// 获取 SimpleClient 实例
        /// </summary>
        SimpleClient<T> Entities { get; }

        /// <summary>
        /// 获取 SqlSugarClient 实例
        /// </summary>
        ISqlSugarClient Context { get; }
    }
} 