//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMeetingService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 会议服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Hbt.Cur.Common.Models;
using Hbt.Cur.Domain.Entities.Routine;
using Hbt.Cur.Application.Dtos.Routine;
using Hbt.Cur.Common.Exceptions;
using Hbt.Cur.Common.Helpers;
using Hbt.Cur.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Hbt.Cur.Common.Utils;
using Hbt.Cur.Domain.Utils;
using Hbt.Cur.Common.Constants;
using Hbt.Cur.Domain.IServices.SignalR;
using Hbt.Cur.Common.Enums;

namespace Hbt.Cur.Application.Services.Routine.Metting
{
    /// <summary>
    /// 会议服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtMeetingService : HbtBaseService, IHbtMeetingService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private readonly IHbtSignalRClient _signalRClient;

        /// <summary>
        /// 获取会议仓储
        /// </summary>
        private IHbtRepository<HbtMeeting> MeetingRepository => _repositoryFactory.GetBusinessRepository<HbtMeeting>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtMeetingService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取会议分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtMeetingDto>> GetListAsync(HbtMeetingQueryDto query)
        {
            _logger.Info("开始查询会议列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await MeetingRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new HbtPagedResult<HbtMeetingDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtMeetingDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取会议详情
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>返回会议详情</returns>
        public async Task<HbtMeetingDto> GetByIdAsync(long meetingId)
        {
            var meeting = await MeetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
                throw new HbtException(L("Meeting.NotFound", meetingId));

            return meeting.Adapt<HbtMeetingDto>();
        }

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回会议ID</returns>
        public async Task<long> CreateAsync(HbtMeetingCreateDto input)
        {
            var meeting = input.Adapt<HbtMeeting>();
            var result = await MeetingRepository.CreateAsync(meeting);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.System,
                    Title = L("Meeting.Created"),
                    Content = L("Meeting.CreatedContent", meeting.Title),
                    Timestamp = DateTime.Now,
                    Data = meeting
                });
            }
            
            return result;
        }

        /// <summary>
        /// 更新会议
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtMeetingUpdateDto input)
        {
            var meeting = await MeetingRepository.GetByIdAsync(input.MeetingId);
            if (meeting == null)
                throw new HbtException(L("Meeting.NotFound", input.MeetingId));

            input.Adapt(meeting);
            var result = await MeetingRepository.UpdateAsync(meeting);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.System,
                    Title = L("Meeting.Updated"),
                    Content = L("Meeting.UpdatedContent", meeting.Title),
                    Timestamp = DateTime.Now,
                    Data = meeting
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long meetingId)
        {
            var meeting = await MeetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
                throw new HbtException(L("Meeting.NotFound", meetingId));

            var result = await MeetingRepository.DeleteAsync(meeting);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.System,
                    Title = L("Meeting.Deleted"),
                    Content = L("Meeting.DeletedContent", meeting.Title),
                    Timestamp = DateTime.Now,
                    Data = meeting
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除会议
        /// </summary>
        /// <param name="meetingIds">会议ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] meetingIds)
        {
            if (meetingIds == null || meetingIds.Length == 0)
                throw new HbtException(L("Meeting.SelectToDelete"));

            var meetings = await MeetingRepository.GetListAsync(x => meetingIds.Contains(x.Id));
            if (!meetings.Any())
                throw new HbtException(L("Meeting.NotFound"));

            var result = await MeetingRepository.DeleteAsync(meetings);
            return result > 0;
        }

        /// <summary>
        /// 导入会议数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "会议信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtMeetingImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var meeting = dto.Adapt<HbtMeeting>();
                    await MeetingRepository.CreateAsync(meeting);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Meeting.ImportFailed", dto.Title), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出会议数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtMeetingQueryDto query, string sheetName = "会议信息")
        {
            var predicate = QueryExpression(query);

            var meetings = await MeetingRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = meetings.Adapt<List<HbtMeetingExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "会议信息")
        {
            var template = new List<HbtMeetingTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtMeeting, bool>> QueryExpression(HbtMeetingQueryDto query)
        {
            var exp = Expressionable.Create<HbtMeeting>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Title))
                    exp = exp.And(x => x.Title.Contains(query.Title));

                if (query.MeetingType.HasValue)
                    exp = exp.And(x => x.MeetingType == query.MeetingType.Value);

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (!string.IsNullOrEmpty(query.Location))
                    exp = exp.And(x => x.Location.Contains(query.Location));

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.StartTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.EndTime <= query.EndTime.Value);

                if (!string.IsNullOrEmpty(query.Host))
                    exp = exp.And(x => x.Host == query.Host);

                if (!string.IsNullOrEmpty(query.Participant))
                    exp = exp.And(x => x.Host == query.Participant || 
                                     (x.Participants != null && x.Participants.Contains(query.Participant)));
            }

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取指定用户主持的会议状态统计
        /// </summary>
        /// <param name="host">主持人</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetHostedMeetingStatisticsAsync(string host)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));

            var stats = await MeetingRepository.AsQueryable()
                .Where(x => x.Host == host)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }

        /// <summary>
        /// 获取指定用户参与的会议状态统计
        /// </summary>
        /// <param name="participant">参与者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetParticipatedMeetingStatisticsAsync(string participant)
        {
            if (string.IsNullOrEmpty(participant))
                throw new ArgumentNullException(nameof(participant));

            var stats = await MeetingRepository.AsQueryable()
                .Where(x => x.Host == participant || 
                           (x.Participants != null && x.Participants.Contains(participant)))
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
} 