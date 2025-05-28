using System;
using System.Linq.Expressions;

namespace Lean.Hbt.Domain.Interfaces
{
    /// <summary>
    /// 数据过滤器接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IDataFilter<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取过滤器是否启用
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// 获取过滤表达式
        /// </summary>
        /// <returns>过滤表达式</returns>
        Expression<Func<TEntity, bool>> GetFilter();
    }
} 