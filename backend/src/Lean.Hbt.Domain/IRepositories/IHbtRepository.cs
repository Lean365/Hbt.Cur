//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtRepository.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 通用仓储接口
//===================================================================

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlSugar;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Domain.IRepositories
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IHbtRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        SqlSugarClient SqlSugarClient { get; }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体</returns>
        Task<TEntity> GetByIdAsync(dynamic id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页获取实体列表
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>是否存在</returns>
        Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>是否成功</returns>
        Task<bool> InsertRangeAsync(List<TEntity> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateRangeAsync(List<TEntity> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(dynamic id);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ids">实体ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteRangeAsync(dynamic[] ids);
    }
} 