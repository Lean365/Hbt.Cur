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
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 通知服务实现
    /// </summary>
    public class HbtNoticeService : IHbtNoticeService
    {
        private readonly ILogger<HbtNoticeService> _logger;
        private readonly IHbtRepository<HbtNotice> _noticeRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="noticeRepository">通知仓储</param>
        public HbtNoticeService(
            ILogger<HbtNoticeService> logger,
            IHbtRepository<HbtNotice> noticeRepository)
        {
            _logger = logger;
            _noticeRepository = noticeRepository;
        }

        /// <summary>
        /// 获取通知分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtNoticeDto>> GetPagedListAsync(HbtNoticeQueryDto query)
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

            var result = await _noticeRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

            return new HbtPagedResult<HbtNoticeDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.list.Adapt<List<HbtNoticeDto>>()
            };
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        public async Task<HbtNoticeDto> GetAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            return notice.Adapt<HbtNoticeDto>();
        }

        /// <summary>
        /// 创建通知
        /// </summary>
        public async Task<long> CreateAsync(HbtNoticeCreateDto input)
        {
            var notice = input.Adapt<HbtNotice>();
            notice.CreateTime = DateTime.Now;

            var result = await _noticeRepository.InsertAsync(notice);
            if (result <= 0)
                throw new HbtException("创建通知失败");

            return notice.Id;
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        public async Task<bool> UpdateAsync(long noticeId, HbtNoticeDto input)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            input.Adapt(notice);
            var result = await _noticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        public async Task<bool> DeleteAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            var result = await _noticeRepository.DeleteAsync(noticeId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除通知
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] noticeIds)
        {
            foreach (var noticeId in noticeIds)
            {
                await DeleteAsync(noticeId);
            }
            return true;
        }

        /// <summary>
        /// 导出通知数据
        /// </summary>
        public async Task<byte[]> ExportAsync(HbtNoticeQueryDto query, string sheetName = "通知数据")
        {
            var exp = Expressionable.Create<HbtNotice>();

            if (!string.IsNullOrEmpty(query.NoticeTitle))
                exp.And(x => x.NoticeTitle.Contains(query.NoticeTitle));

            if (query.NoticeType.HasValue)
                exp.And(x => x.NoticeType == query.NoticeType.Value);

            if (query.NoticeStatus.HasValue)
                exp.And(x => x.NoticeStatus == query.NoticeStatus.Value);

            var list = await _noticeRepository.GetListAsync(exp.ToExpression());
            var result = list.Adapt<List<HbtNoticeExportDto>>();

            return await HbtExcelHelper.ExportAsync(result, sheetName);
        }

        /// <summary>
        /// 发布通知
        /// </summary>
        public async Task<bool> PublishAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            notice.NoticeStatus = 1; // 已发布
            notice.NoticePublishTime = DateTime.Now;
            
            var result = await _noticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 关闭通知
        /// </summary>
        public async Task<bool> CloseAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            notice.NoticeStatus = 2; // 已关闭
            
            var result = await _noticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        public async Task<bool> MarkAsReadAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            notice.NoticeReadCount++;
            // TODO: 添加当前用户ID到已读列表
            notice.NoticeLastReceiptTime = DateTime.Now;
            
            var result = await _noticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 确认通知
        /// </summary>
        public async Task<bool> ConfirmAsync(long noticeId)
        {
            var notice = await _noticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new HbtException($"通知不存在: {noticeId}");

            if (!notice.NoticeRequireConfirm)
                throw new HbtException($"该通知不需要确认");

            notice.NoticeConfirmCount++;
            // TODO: 添加当前用户ID到已确认列表
            notice.NoticeLastReceiptTime = DateTime.Now;
            
            var result = await _noticeRepository.UpdateAsync(notice);
            return result > 0;
        }
    }
} 