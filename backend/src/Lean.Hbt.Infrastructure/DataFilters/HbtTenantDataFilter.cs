/*
 * 文件名：HbtTenantDataFilter.cs
 * 描述：多租户数据过滤器实现类，用于在数据查询时自动过滤租户数据
 * 功能：
 * 1. 实现IDataFilter接口，提供租户数据过滤功能
 * 2. 通过当前租户上下文获取租户ID
 * 3. 为实体查询添加租户ID过滤条件
 * 
 * 作者：Lean.Hbt
 * 创建时间：2024
 */

using System;
using System.Linq.Expressions;
using SqlSugar;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Interfaces;

namespace Lean.Hbt.Infrastructure.DataFilters
{
    /// <summary>
    /// 多租户数据过滤器
    /// </summary>
    /// <typeparam name="TEntity">实体类型，必须实现IHbtTenant接口</typeparam>
    public class HbtTenantDataFilter<TEntity> : IDataFilter<TEntity> where TEntity : class, IHbtTenant
    {
        /// <summary>
        /// 当前租户上下文
        /// </summary>
        private readonly IHbtCurrentTenant _currentTenant;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentTenant">当前租户上下文</param>
        public HbtTenantDataFilter(IHbtCurrentTenant currentTenant)
        {
            _currentTenant = currentTenant;
        }

        /// <summary>
        /// 获取过滤器是否启用
        /// </summary>
        public bool IsEnabled => true;

        /// <summary>
        /// 获取租户数据过滤表达式
        /// </summary>
        /// <returns>返回一个表达式，用于过滤指定租户的数据</returns>
        public Expression<Func<TEntity, bool>> GetFilter()
        {
            var tenantId = _currentTenant.TenantId;
            return e => e.TenantId == tenantId;
        }
    }
} 