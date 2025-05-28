#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 异常日志服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Audit
{
    /// <summary>
    /// 异常日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtExceptionLogService : HbtBaseService, IHbtExceptionLogService
    {
        private readonly IHbtRepository<HbtExceptionLog> _exceptionLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionLogRepository">异常日志仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtExceptionLogService(
            IHbtRepository<HbtExceptionLog> exceptionLogRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _exceptionLogRepository = exceptionLogRepository ?? throw new ArgumentNullException(nameof(exceptionLogRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtExceptionLog, bool>> KpExceptionLogQueryExpression(HbtExceptionLogQueryDto query)
        {
            return Expressionable.Create<HbtExceptionLog>()
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName!))
                .AndIF(!string.IsNullOrEmpty(query.Method), x => x.Method.Contains(query.Method!))
                .AndIF(!string.IsNullOrEmpty(query.ExceptionType), x => x.ExceptionType.Contains(query.ExceptionType!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtExceptionLogDto>> GetListAsync(HbtExceptionLogQueryDto? query)
        {
            query ??= new HbtExceptionLogQueryDto();

            var result = await _exceptionLogRepository.GetPagedListAsync(
                KpExceptionLogQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtExceptionLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtExceptionLogDto>>()
            };
        }

        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回异常日志详情</returns>
        public async Task<HbtExceptionLogDto> GetByIdAsync(long logId)
        {
            var log = await _exceptionLogRepository.GetByIdAsync(logId);
            return log == null ? throw new HbtException(L("Audit.ExceptionLog.NotFound", logId)) : log.Adapt<HbtExceptionLogDto>();
        }

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtExceptionLogQueryDto query, string sheetName = "ExceptionLog")
        {
            try
            {
                var list = await _exceptionLogRepository.GetListAsync(KpExceptionLogQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtExceptionLogExportDto>>(), sheetName, L("Audit.ExceptionLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.ExceptionLog.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Audit.ExceptionLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                var result = await _exceptionLogRepository.DeleteAsync((Expression<Func<HbtExceptionLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.ExceptionLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.ExceptionLog.ClearFailed"));
            }
        }
    }
}