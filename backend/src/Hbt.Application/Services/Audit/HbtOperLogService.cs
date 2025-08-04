#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 操作日志服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Hbt.Application.Services.Audit
{
    /// <summary>
    /// 操作日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogService : HbtBaseService, IHbtOperLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private IHbtRepository<HbtOperLog> OperLogRepository => _repositoryFactory.GetAuthRepository<HbtOperLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">操作日志仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOperLogService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));

        }

        /// <summary>
        /// 获取操作日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtOperLogDto>> GetListAsync(HbtOperLogQueryDto? query)
        {
            query ??= new HbtOperLogQueryDto();

            var result = await OperLogRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtOperLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtOperLogDto>>()
            };
        }

        /// <summary>
        /// 获取操作日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回操作日志详情</returns>
        public async Task<HbtOperLogDto> GetByIdAsync(long logId)
        {
            var log = await OperLogRepository.GetByIdAsync(logId);
            return log == null ? throw new HbtException(L("Audit.OperLog.NotFound", logId)) : log.Adapt<HbtOperLogDto>();
        }

        /// <summary>
        /// 导出操作日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtOperLogQueryDto query, string sheetName = "OperLog")
        {
            try
            {
                var list = await OperLogRepository.GetListAsync(QueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtOperLogExportDto>>(), sheetName, L("Audit.OperLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.OperLog.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Audit.OperLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                var result = await OperLogRepository.DeleteAsync((Expression<Func<HbtOperLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.OperLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.OperLog.ClearFailed"));
            }
        }
        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtOperLog, bool>> QueryExpression(HbtOperLogQueryDto query)
        {
            return Expressionable.Create<HbtOperLog>()
                .AndIF(!string.IsNullOrEmpty(query.OperModule), x => x.OperModule.Contains(query.OperModule!))
                .AndIF(!string.IsNullOrEmpty(query.OperType), x => x.OperType.Contains(query.OperType!))
                .AndIF(!string.IsNullOrEmpty(query.OperTableName), x => x.OperTableName.Contains(query.OperTableName!))
                .AndIF(!string.IsNullOrEmpty(query.OperIpAddress), x => x.OperIpAddress.Contains(query.OperIpAddress!))
                .AndIF(query.OperStatus.HasValue, x => x.OperStatus == query.OperStatus.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}