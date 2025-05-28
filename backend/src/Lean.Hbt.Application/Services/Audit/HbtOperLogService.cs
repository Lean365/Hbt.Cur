#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 操作日志服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Audit
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
        private readonly IHbtRepository<HbtOperLog> _operLogRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operLogRepository">操作日志仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOperLogService(
            IHbtRepository<HbtOperLog> operLogRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _operLogRepository = operLogRepository ?? throw new ArgumentNullException(nameof(operLogRepository));

        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtOperLog, bool>> KpOperLogQueryExpression(HbtOperLogQueryDto query)
        {
            return Expressionable.Create<HbtOperLog>()
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName!))
                .AndIF(!string.IsNullOrEmpty(query.OperationType), x => x.OperationType.Contains(query.OperationType!))
                .AndIF(!string.IsNullOrEmpty(query.TableName), x => x.TableName.Contains(query.TableName!))
                .AndIF(!string.IsNullOrEmpty(query.IpAddress), x => x.IpAddress.Contains(query.IpAddress!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取操作日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtOperLogDto>> GetListAsync(HbtOperLogQueryDto? query)
        {
            query ??= new HbtOperLogQueryDto();

            var result = await _operLogRepository.GetPagedListAsync(
                KpOperLogQueryExpression(query),
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
            var log = await _operLogRepository.GetByIdAsync(logId);
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
                var list = await _operLogRepository.GetListAsync(KpOperLogQueryExpression(query));
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
                var result = await _operLogRepository.DeleteAsync((Expression<Func<HbtOperLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.OperLog.ClearFailed", ex.Message), ex);
                throw new HbtException(L("Audit.OperLog.ClearFailed"));
            }
        }
    }
}