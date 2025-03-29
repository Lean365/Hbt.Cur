using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Routine;

namespace Lean.Hbt.Domain.Repositories
{
    /// <summary>
    /// 通知仓储接口
    /// </summary>
    public interface INoticeRepository
    {
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns>通知实体</returns>
        Task<HbtNotice?> GetAsync(long id);

        /// <summary>
        /// 获取通知列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>通知列表</returns>
        Task<List<HbtNotice>> GetListAsync(Expression<Func<HbtNotice, bool>> predicate);

        /// <summary>
        /// 更新通知
        /// </summary>
        /// <param name="notice">通知实体</param>
        /// <returns>更新结果</returns>
        Task<bool> UpdateAsync(HbtNotice notice);

        /// <summary>
        /// 批量更新通知
        /// </summary>
        /// <param name="notices">通知列表</param>
        /// <returns>更新结果</returns>
        Task<bool> UpdateManyAsync(List<HbtNotice> notices);
    }
} 