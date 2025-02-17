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
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>实体</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果</returns>
        Task<(List<TEntity> list, long total)> GetPagedListAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果</returns>
        Task<(List<TEntity> list, long total)> GetPagedListAsync(Expression<Func<TEntity, bool>> condition, int pageIndex, int pageSize);

        /// <summary>
        /// 根据条件获取第一个实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition);

        #endregion 查询操作

        #region 新增操作

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> InsertRangeAsync(List<TEntity> entities);

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
    }
}