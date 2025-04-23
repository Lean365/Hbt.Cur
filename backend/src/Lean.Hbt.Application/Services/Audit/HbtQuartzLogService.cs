//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 定时任务日志服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Domain.Entities.Audit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 定时任务日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtQuartzLogService : HbtBaseService, IHbtQuartzLogService
    {
        private readonly IHbtRepository<HbtQuartzLog> _quartzLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtQuartzLogService(
            IHbtRepository<HbtQuartzLog> quartzLogRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _quartzLogRepository = quartzLogRepository ?? throw new ArgumentNullException(nameof(quartzLogRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtQuartzLog, bool>> KpQuartzLogQueryExpression(HbtQuartzLogQueryDto query)
        {
            return Expressionable.Create<HbtQuartzLog>()
                .AndIF(!string.IsNullOrEmpty(query.LogTaskName), x => x.LogTaskName.Contains(query.LogTaskName!))
                .AndIF(!string.IsNullOrEmpty(query.LogGroupName), x => x.LogGroupName.Contains(query.LogGroupName!))
                .AndIF(query.LogStatus.HasValue, x => x.LogStatus == query.LogStatus.Value)
                .AndIF(query.BeginTime.HasValue, x => x.CreateTime >= query.BeginTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取定时任务日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtQuartzLogDto>> GetPagedAsync(HbtQuartzLogQueryDto? query)
        {
            query ??= new HbtQuartzLogQueryDto();

            var result = await _quartzLogRepository.GetPagedListAsync(
                KpQuartzLogQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtQuartzLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtQuartzLogDto>>()
            };
        }

        /// <summary>
        /// 获取定时任务日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回定时任务日志详情</returns>
        public async Task<HbtQuartzLogDto> GetByIdAsync(long logId)
        {
            var log = await _quartzLogRepository.GetByIdAsync(logId);
            return log == null ? throw new HbtException(L("Audit.QuartzLog.NotFound", logId)) : log.Adapt<HbtQuartzLogDto>();
        }

        /// <summary>
        /// 导出定时任务日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtQuartzLogQueryDto query, string sheetName = "QuartzLog")
        {
            try
            {
                var list = await _quartzLogRepository.GetListAsync(KpQuartzLogQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtQuartzLogExportDto>>(), sheetName, L("Audit.QuartzLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.QuartzLog.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Audit.QuartzLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空定时任务日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                var result = await _quartzLogRepository.DeleteAsync((Expression<Func<HbtQuartzLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.QuartzLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.QuartzLog.ClearFailed"));
            }
        }
    }
}