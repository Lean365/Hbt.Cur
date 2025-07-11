#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtQuartzLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 定时任务日志服务实现
//===================================================================

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
        private readonly IHbtRepositoryFactory _repositoryFactory;
        private IHbtRepository<HbtQuartzLog> QuartzLogRepository => _repositoryFactory.GetAuthRepository<HbtQuartzLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">定时任务日志仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtQuartzLogService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取定时任务日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtQuartzLogDto>> GetPagedAsync(HbtQuartzLogQueryDto? query)
        {
            query ??= new HbtQuartzLogQueryDto();

            var result = await QuartzLogRepository.GetPagedListAsync(
                QueryExpression(query),
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
            var log = await QuartzLogRepository.GetByIdAsync(logId);
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
                var list = await QuartzLogRepository.GetListAsync(QueryExpression(query));
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
                var result = await QuartzLogRepository.DeleteAsync((Expression<Func<HbtQuartzLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.QuartzLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.QuartzLog.ClearFailed"));
            }

        }
        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtQuartzLog, bool>> QueryExpression(HbtQuartzLogQueryDto query)
        {
            return Expressionable.Create<HbtQuartzLog>()
                .AndIF(!string.IsNullOrEmpty(query.QuartzName), x => x.QuartzName.Contains(query.QuartzName!))
                .AndIF(!string.IsNullOrEmpty(query.QuartzGroupName), x => x.QuartzGroupName.Contains(query.QuartzGroupName!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.BeginTime.HasValue, x => x.CreateTime >= query.BeginTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}