//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtRepository.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 14:10
// 版本号 : V0.0.1
// 描述    : SqlSugar通用仓储接口
//===================================================================

using System.Linq.Expressions;
using SqlSugar;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Repositories
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface IHbtRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        ISqlSugarClient SqlSugarClient { get; }

        /// <summary>
        /// 获取SimpleClient对象
        /// </summary>
        SimpleClient<TEntity> SimpleClient { get; }

        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <returns>查询对象</returns>
        ISugarQueryable<TEntity> AsQueryable();

        #region 查询操作

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>实体</returns>
        Task<TEntity?> GetByIdAsync(object id);

        /// <summary>
        /// 获取第一个符合条件的实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体</returns>
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? condition = null);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="orderByType">排序类型</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? condition = null,
            int pageIndex = 1,
            int pageSize = 20,
            Expression<Func<TEntity, object>>? orderByExpression = null,
            OrderByType orderByType = OrderByType.Desc);

        /// <summary>
        /// 获取分页列表(支持多个排序条件)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderByExpressions">排序表达式列表</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? condition = null,
            int pageIndex = 1,
            int pageSize = 20,
            List<(Expression<Func<TEntity, object>> Expression, OrderByType Type)>? orderByExpressions = null);

        #endregion

        #region 新增操作

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> CreateRangeAsync(List<TEntity> entities);

        #endregion 新增操作

        #region 更新操作

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> UpdateRangeAsync(List<TEntity> entities);

        #endregion 更新操作

        #region 删除操作

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteAsync(object id);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteRangeAsync(List<object> ids);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteRangeAsync(List<TEntity> entities);

        #endregion 删除操作

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        Task<List<string>> GetUserRolesAsync(long userId);

        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        Task<List<string>> GetUserPermissionsAsync(long userId);

        /// <summary>
        /// 获取用户租户列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>租户ID列表</returns>
        Task<List<long>> GetUserTenantsAsync(long userId);

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>岗位ID列表</returns>
        Task<List<long>> GetUserPostsAsync(long userId);

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>部门ID列表</returns>
        Task<List<long>> GetUserDeptsAsync(long userId);

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>菜单ID列表</returns>
        Task<List<long>> GetRoleMenusAsync(long roleId);

    }
}