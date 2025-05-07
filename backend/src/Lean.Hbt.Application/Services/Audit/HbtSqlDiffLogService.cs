#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlDiffLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 差异日志服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 差异日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSqlDiffLogService : HbtBaseService, IHbtSqlDiffLogService
    {
        private readonly IHbtRepository<HbtSqlDiffLog> _sqlDiffLogRepository;

        private Expression<Func<HbtSqlDiffLog, bool>> KpSqlDiffLogQueryExpression(HbtSqlDiffLogQueryDto query)
        {
            return Expressionable.Create<HbtSqlDiffLog>()
                .AndIF(!string.IsNullOrEmpty(query.TableName), x => x.TableName.Contains(query.TableName))
                .AndIF(!string.IsNullOrEmpty(query.DiffType), x => x.DiffType.Contains(query.DiffType))
                .AndIF(!string.IsNullOrEmpty(query.BusinessName), x => x.BusinessName.Contains(query.BusinessName))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="sqlDiffLogRepository">差异日志仓储</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtSqlDiffLogService(
            IHbtRepository<HbtSqlDiffLog> sqlDiffLogRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _sqlDiffLogRepository = sqlDiffLogRepository ?? throw new ArgumentNullException(nameof(sqlDiffLogRepository));
        }

        /// <summary>
        /// 获取SQL差异日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtSqlDiffLogDto>> GetListAsync(HbtSqlDiffLogQueryDto? query)
        {
            query ??= new HbtSqlDiffLogQueryDto();

            var result = await _sqlDiffLogRepository.GetPagedListAsync(
                KpSqlDiffLogQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtSqlDiffLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtSqlDiffLogDto>>()
            };
        }

        /// <summary>
        /// 获取SQL差异日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回SQL差异日志详情</returns>
        public async Task<HbtSqlDiffLogDto> GetByIdAsync(long logId)
        {
            var log = await _sqlDiffLogRepository.GetByIdAsync(logId);
            return log == null ? throw new HbtException(L("Audit.SqlDiffLog.NotFound", logId)) : log.Adapt<HbtSqlDiffLogDto>();
        }

        /// <summary>
        /// 导出SQL差异日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtSqlDiffLogQueryDto query, string sheetName = "SqlDiffLog")
        {
            try
            {
                var list = await _sqlDiffLogRepository.GetListAsync(KpSqlDiffLogQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtSqlDiffLogExportDto>>(), sheetName, L("Audit.SqlDiffLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.SqlDiffLog.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Audit.SqlDiffLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空SQL差异日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                var result = await _sqlDiffLogRepository.DeleteAsync((Expression<Func<HbtSqlDiffLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.SqlDiffLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.SqlDiffLog.ClearFailed"));
            }
        }
    }
}