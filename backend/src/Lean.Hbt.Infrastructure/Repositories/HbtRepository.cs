//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRepository.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 14:20
// 版本号 : V0.0.1
// 描述    : SqlSugar仓储实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Infrastructure.Repositories
{
    /// <summary>
    /// SqlSugar通用仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtRepository<TEntity> : IHbtRepository<TEntity> where TEntity : class, new()
    {
        private readonly SqlSugarScope _db;
        private readonly SimpleClient<TEntity> _entities;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">SqlSugar客户端</param>
        public HbtRepository(SqlSugarScope db)
        {
            _db = db;
            _entities = _db.GetSimpleClient<TEntity>();
        }

        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        public ISqlSugarClient SqlSugarClient => _db;

        /// <summary>
        /// 获取SimpleClient对象
        /// </summary>
        public SimpleClient<TEntity> SimpleClient => _entities;

        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <returns>返回ISugarQueryable查询对象</returns>
        public ISugarQueryable<TEntity> AsQueryable()
        {
            return _db.Queryable<TEntity>();
        }

        #region 查询操作

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>返回实体对象,如果未找到返回null</returns>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _entities.GetByIdAsync(id);
        }

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        /// <returns>返回实体列表</returns>
        public async Task<List<TEntity>> GetListAsync()
        {
            return await _entities.GetListAsync();
        }

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">查询条件表达式</param>
        /// <returns>返回符合条件的实体列表</returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _entities.GetListAsync(condition);
        }

        /// <summary>
        /// 分页查询所有数据
        /// </summary>
        /// <param name="pageIndex">页码(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>返回分页结果(list:当前页数据,total:总记录数)</returns>
        public async Task<(List<TEntity> list, long total)> GetPagedListAsync(int pageIndex, int pageSize)
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<TEntity>()
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return (list, totalCount);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="condition">查询条件表达式</param>
        /// <param name="pageIndex">页码(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>返回分页结果(list:当前页数据,total:总记录数)</returns>
        public async Task<(List<TEntity> list, long total)> GetPagedListAsync(Expression<Func<TEntity, bool>> condition, int pageIndex, int pageSize)
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<TEntity>()
                .Where(condition)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return (list, totalCount);
        }

        /// <summary>
        /// 根据条件获取第一个实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体</returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await SimpleClient.AsQueryable().FirstAsync(condition);
        }

        #endregion 查询操作

        #region 新增操作

        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> InsertRangeAsync(List<TEntity> entities)
        {
            return await _db.Insertable(entities).ExecuteCommandAsync();
        }

        #endregion 新增操作

        #region 更新操作

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            return await _db.Updateable(entities).ExecuteCommandAsync();
        }

        #endregion 更新操作

        #region 删除操作

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteAsync(object id)
        {
            return await _db.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await _db.Deleteable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <param name="ids">主键值列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteRangeAsync(List<object> ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteRangeAsync(List<TEntity> entities)
        {
            return await _db.Deleteable(entities).ExecuteCommandAsync();
        }

        #endregion 删除操作
    }
}