//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNoticeService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 通知服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine.Notice
{
    /// <summary>
    /// 通知服务接口
    /// </summary>
    public interface IHbtNoticeService
    {
        /// <summary>
        /// 获取通知分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>通知分页列表</returns>
        Task<HbtPagedResult<HbtNoticeDto>> GetListAsync(HbtNoticeQueryDto query);

        /// <summary>
        /// 获取通知详情
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>通知详情</returns>
        Task<HbtNoticeDto> GetByIdAsync(long noticeId);

        /// <summary>
        /// 创建通知
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>通知ID</returns>
        Task<long> CreateAsync(HbtNoticeCreateDto input);

        /// <summary>
        /// 更新通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(long noticeId, HbtNoticeDto input);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long noticeId);

        /// <summary>
        /// 批量删除通知
        /// </summary>
        /// <param name="noticeIds">通知ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] noticeIds);

        /// <summary>
        /// 导出通知数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtNoticeQueryDto query, string sheetName = "通知数据");

        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        Task<bool> PublishAsync(long noticeId);

        /// <summary>
        /// 关闭通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <returns>是否成功</returns>
        Task<bool> CloseAsync(long noticeId);

        /// <summary>
        /// 标记通知为已读
        /// </summary>
        /// <param name="id">通知ID</param>
        Task<bool> MarkAsReadAsync(long id);

        /// <summary>
        /// 标记所有通知为已读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsReadAsync(long userId);

        /// <summary>
        /// 标记通知为未读
        /// </summary>
        /// <param name="id">通知ID</param>
        Task<bool> MarkAsUnreadAsync(long id);

        /// <summary>
        /// 标记所有通知为未读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsUnreadAsync(long userId);

        /// <summary>
        /// 确认通知
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        Task<bool> ConfirmAsync(long noticeId);
    }
} 