//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRepositoryExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 20:30
// 版本号 : V0.0.1
// 描述   : SqlSugar通用仓储扩展方法
//===================================================================

using System.Linq.Expressions;
using SqlSugar;

namespace Lean.Hbt.Domain.Repositories;

/// <summary>
/// SqlSugar通用仓储扩展方法
/// </summary>
public static class HbtRepositoryExtensions
{
    /// <summary>
    /// 获取查询对象
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <returns>查询对象</returns>
    public static ISugarQueryable<T> AsQueryable<T>(this IHbtRepository<T> repository) where T : class, new()
    {
        return repository.Context.Queryable<T>();
    }

    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="id">主键ID</param>
    /// <returns>实体</returns>
    public static async Task<T> GetByIdAsync<T>(this IHbtRepository<T> repository, object id) where T : class, new()
    {
        return await repository.Context.Queryable<T>().InSingleAsync(id);
    }

    /// <summary>
    /// 插入实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    public static async Task<bool> InsertAsync<T>(this IHbtRepository<T> repository, T entity) where T : class, new()
    {
        return await repository.Context.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    public static async Task<bool> UpdateAsync<T>(this IHbtRepository<T> repository, T entity) where T : class, new()
    {
        return await repository.Context.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据ID删除实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="id">主键ID</param>
    /// <returns>是否成功</returns>
    public static async Task<bool> DeleteByIdAsync<T>(this IHbtRepository<T> repository, object id) where T : class, new()
    {
        return await repository.Context.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="repository">仓储接口</param>
    /// <param name="expression">条件表达式</param>
    /// <returns>是否成功</returns>
    public static async Task<bool> DeleteAsync<T>(this IHbtRepository<T> repository, Expression<Func<T, bool>> expression) where T : class, new()
    {
        return await repository.Context.Deleteable<T>().Where(expression).ExecuteCommandAsync() > 0;
    }
} 