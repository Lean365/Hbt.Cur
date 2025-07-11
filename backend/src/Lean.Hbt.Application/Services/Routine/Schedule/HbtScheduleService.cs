//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtScheduleService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 日程服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Application.Services.Routine.Schedule
{
    /// <summary>
    /// 日程服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtScheduleService : HbtBaseService, IHbtScheduleService
    {
        private readonly IHbtRepository<HbtSchedule> _scheduleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="scheduleRepository">日程仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtScheduleService(
            IHbtLogger logger,
            IHbtRepository<HbtSchedule> scheduleRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _scheduleRepository = scheduleRepository;
        }

        /// <summary>
        /// 获取日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtScheduleDto>> GetListAsync(HbtScheduleQueryDto query)
        {
            _logger.Info("开始查询日程列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await _scheduleRepository.GetPagedListAsync(
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

            var dtoResult = new HbtPagedResult<HbtScheduleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtScheduleDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>返回日程详情</returns>
        public async Task<HbtScheduleDto> GetByIdAsync(long scheduleId)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new HbtException(L("Schedule.NotFound", scheduleId));

            return schedule.Adapt<HbtScheduleDto>();
        }

        /// <summary>
        /// 创建日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回日程ID</returns>
        public async Task<long> CreateAsync(HbtScheduleCreateDto input)
        {
            var schedule = input.Adapt<HbtSchedule>();
            var result = await _scheduleRepository.CreateAsync(schedule);
            return result;
        }

        /// <summary>
        /// 更新日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtScheduleUpdateDto input)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(input.ScheduleId);
            if (schedule == null)
                throw new HbtException(L("Schedule.NotFound", input.ScheduleId));

            input.Adapt(schedule);
            var result = await _scheduleRepository.UpdateAsync(schedule);
            return result > 0;
        }

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long scheduleId)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new HbtException(L("Schedule.NotFound", scheduleId));

            var result = await _scheduleRepository.DeleteAsync(schedule);
            return result > 0;
        }

        /// <summary>
        /// 批量删除日程
        /// </summary>
        /// <param name="scheduleIds">日程ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] scheduleIds)
        {
            if (scheduleIds == null || scheduleIds.Length == 0)
                throw new HbtException(L("Schedule.SelectToDelete"));

            var schedules = await _scheduleRepository.GetListAsync(x => scheduleIds.Contains(x.Id));
            if (!schedules.Any())
                throw new HbtException(L("Schedule.NotFound"));

            var result = await _scheduleRepository.DeleteAsync(schedules);
            return result > 0;
        }

        /// <summary>
        /// 导入日程数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "日程信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtScheduleImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var schedule = dto.Adapt<HbtSchedule>();
                    await _scheduleRepository.CreateAsync(schedule);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Schedule.ImportFailed", dto.Title), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtScheduleQueryDto query, string sheetName = "日程信息")
        {
            var predicate = QueryExpression(query);

            var schedules = await _scheduleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = schedules.Adapt<List<HbtScheduleExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "日程信息")
        {
            var template = new List<HbtScheduleTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtSchedule, bool>> QueryExpression(HbtScheduleQueryDto query)
        {
            var exp = Expressionable.Create<HbtSchedule>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Title))
                    exp = exp.And(x => x.Title.Contains(query.Title));

                if (query.ScheduleType.HasValue)
                    exp = exp.And(x => x.ScheduleType == query.ScheduleType.Value);

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (!string.IsNullOrEmpty(query.Location))
                    exp = exp.And(x => x.Location.Contains(query.Location));

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.StartTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.EndTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
} 