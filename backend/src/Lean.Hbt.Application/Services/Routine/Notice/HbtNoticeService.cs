//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNoticeService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 通知服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Common.Enums;
using SqlSugar;
using Mapster;

namespace Lean.Hbt.Application.Services.Routine.Notice
{
    /// <summary>
    /// 通知服务实现
    /// </summary>
    public class HbtNoticeService : HbtBaseService, IHbtNoticeService
    {
        private readonly IHbtRepository<HbtNotice> _repository;
        private readonly IHbtSignalRClient _signalRClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repository">通知仓储</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNoticeService(
            IHbtLogger logger,
            IHbtRepository<HbtNotice> repository,
            IHbtSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repository = repository;
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取通知分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtNoticeDto>> GetListAsync(HbtNoticeQueryDto query)
        {
            var exp = Expressionable.Create<HbtNotice>();

            if (!string.IsNullOrEmpty(query.NoticeTitle))
                exp.And(x => x.NoticeTitle.Contains(query.NoticeTitle));

            if (query.NoticeType.HasValue)
                exp.And(x => x.NoticeType == query.NoticeType.Value);

            if (query.NoticeStatus.HasValue)
                exp.And(x => x.NoticeStatus == query.NoticeStatus.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _repository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtNoticeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtNoticeDto>>()
            };
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        public async Task<HbtNoticeDto> GetByIdAsync(long noticeId)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            return notice.Adapt<HbtNoticeDto>();
        }

        /// <summary>
        /// 创建通知
        /// </summary>
        public async Task<long> CreateAsync(HbtNoticeCreateDto input)
        {
            var notice = input.Adapt<HbtNotice>();
            notice.CreateTime = DateTime.Now;

            var result = await _repository.CreateAsync(notice);
            if (result <= 0)
                throw new HbtException(L("Notice.CreateFailed"));

            return notice.Id;
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        public async Task<bool> UpdateAsync(long noticeId, HbtNoticeDto input)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            input.Adapt(notice);
            var result = await _repository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        public async Task<bool> DeleteAsync(long noticeId)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            var result = await _repository.DeleteAsync(noticeId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除通知
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] noticeIds)
        {
            if (noticeIds == null || noticeIds.Length == 0)
                throw new HbtException(L("Notice.SelectToDelete"));

            foreach (var noticeId in noticeIds)
            {
                await DeleteAsync(noticeId);
            }
            return true;
        }

        /// <summary>
        /// 导出通知数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtNoticeQueryDto query, string sheetName = "Notice")
        {
            try
            {
                var list = await _repository.GetListAsync(KpNoticeQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtNoticeExportDto>>(), sheetName, L("Notice.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Notice.ExportFailed"), ex);
                throw new HbtException(L("Notice.ExportFailed"));
            }
        }

        /// <summary>
        /// 发布通知
        /// </summary>
        public async Task<bool> PublishAsync(long noticeId)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            notice.NoticeStatus = 1; // 已发布
            notice.NoticePublishTime = DateTime.Now;

            var result = await _repository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 关闭通知
        /// </summary>
        public async Task<bool> CloseAsync(long noticeId)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            notice.NoticeStatus = 2; // 已关闭

            var result = await _repository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        public async Task<bool> MarkAsReadAsync(long id)
        {
            var notice = await _repository.GetByIdAsync(id);
            if (notice == null) return false;

            // 更新已读状态
            var readIds = (notice.NoticeReadIds?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()).ToList();
            var userId = _currentUser.UserId.ToString();
            if (!readIds.Contains(userId))
            {
                readIds.Add(userId);
                notice.NoticeReadIds = string.Join(",", readIds);
                notice.NoticeReadCount = readIds.Count;
                notice.NoticeLastReceiptTime = DateTime.Now;

                await _repository.UpdateAsync(notice);

                // 发送已读通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.Notification,
                    Title = L("Notice.ReadTitle"),
                    Content = L("Notice.ReadContent", notice.NoticeTitle),
                    Timestamp = DateTime.Now,
                    Data = notice
                };
                await _signalRClient.ReceiveNoticeStatus(notification);
            }

            return true;
        }

        /// <summary>
        /// 确认通知
        /// </summary>
        public async Task<bool> ConfirmAsync(long noticeId)
        {
            var notice = await _repository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException(L("Notice.NotFound", noticeId));

            if (!notice.NoticeRequireConfirm)
                throw new HbtException(L("Notice.NoConfirmRequired"));

            // 获取当前已确认人ID列表
            var confirmIds = string.IsNullOrEmpty(notice.NoticeConfirmIds)
                ? new List<long>()
                : notice.NoticeConfirmIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .ToList();

            // 如果当前用户已在已确认列表中，抛出异常
            if (confirmIds.Contains(_currentUser.UserId))
                throw new HbtException(L("Notice.AlreadyConfirmed"));

            // 添加当前用户到已确认列表
            confirmIds.Add(_currentUser.UserId);
            notice.NoticeConfirmIds = string.Join(",", confirmIds);
            notice.NoticeConfirmCount = confirmIds.Count;
            notice.NoticeLastReceiptTime = DateTime.Now;

            var result = await _repository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 标记所有通知已读
        /// </summary>
        public async Task<int> MarkAllAsReadAsync(long userId)
        {
            var unreadNotices = await _repository.GetListAsync(n =>
                string.IsNullOrEmpty(n.NoticeReadIds) ||
                !n.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .Contains(userId));

            if (!unreadNotices.Any())
                return 0;

            foreach (var notice in unreadNotices)
            {
                var readIds = string.IsNullOrEmpty(notice.NoticeReadIds)
                    ? new List<long>()
                    : notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => long.Parse(id))
                        .ToList();

                if (!readIds.Contains(userId))
                {
                    readIds.Add(userId);
                    notice.NoticeReadIds = string.Join(",", readIds);
                    notice.NoticeReadCount = readIds.Count;
                    notice.NoticeLastReceiptTime = DateTime.Now;
                }
            }

            var result = await _repository.UpdateRangeAsync(unreadNotices);
            return result;
        }

        /// <summary>
        /// 标记通知未读
        /// </summary>
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            var notice = await _repository.GetByIdAsync(id);
            if (notice == null)
                return false;

            // 获取当前已读人ID列表
            var readIds = string.IsNullOrEmpty(notice.NoticeReadIds)
                ? new List<long>()
                : notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            // 如果当前用户不在已读列表中，则返回true
            if (!readIds.Contains(_currentUser.UserId))
                return true;

            // 从已读列表中移除当前用户
            readIds.Remove(_currentUser.UserId);
            notice.NoticeReadIds = string.Join(",", readIds);
            notice.NoticeReadCount = readIds.Count;

            var result = await _repository.UpdateAsync(notice);
            if (result > 0)
            {
                // 发送实时通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.Notification,
                    Title = L("Notice.UnreadTitle"),
                    Content = L("Notice.UnreadContent", notice.NoticeTitle),
                    Timestamp = DateTime.Now,
                    Data = notice
                };

                await _signalRClient.ReceiveNoticeStatus(notification);
            }

            return result > 0;
        }

        /// <summary>
        /// 标记所有通知未读
        /// </summary>
        public async Task<int> MarkAllAsUnreadAsync(long userId)
        {
            var readNotices = await _repository.GetListAsync(n =>
                !string.IsNullOrEmpty(n.NoticeReadIds) &&
                n.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .Contains(userId));

            if (!readNotices.Any())
                return 0;

            foreach (var notice in readNotices)
            {
                var readIds = notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .ToList();
                readIds.Remove(userId);
                notice.NoticeReadIds = string.Join(",", readIds);
                notice.NoticeReadCount = readIds.Count;
            }

            var result = await _repository.UpdateRangeAsync(readNotices);
            return result;
        }

        private Expression<Func<HbtNotice, bool>> KpNoticeQueryExpression(HbtNoticeQueryDto query)
        {
            return Expressionable.Create<HbtNotice>()
                .AndIF(!string.IsNullOrEmpty(query.NoticeTitle), x => x.NoticeTitle.Contains(query.NoticeTitle))
                .AndIF(query.NoticeType.HasValue, x => x.NoticeType == query.NoticeType.Value)
                .AndIF(query.NoticeStatus.HasValue, x => x.NoticeStatus == query.NoticeStatus.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}