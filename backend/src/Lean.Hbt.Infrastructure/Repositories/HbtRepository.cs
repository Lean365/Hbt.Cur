//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtRepository.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 14:20
// 版本号 : V0.0.1
// 描述    : SqlSugar仓储实现
//===================================================================

using SqlSugar;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Repositories
{
    /// <summary>
    /// SqlSugar仓储实现
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtRepository<T> : IHbtRepository<T> where T : class, new()
    {
        protected readonly ISqlSugarClient _db;
        protected readonly SimpleClient<T> _client;

        public HbtRepository(ISqlSugarClient db) 
        {
            _db = db;
            _client = _db.GetSimpleClient<T>();
        }

        /// <summary>
        /// 获取 SimpleClient 实例
        /// </summary>
        public SimpleClient<T> Entities => _client;

        /// <summary>
        /// 获取 SqlSugarClient 实例
        /// </summary>
        public ISqlSugarClient Context => _db;
    }
} 