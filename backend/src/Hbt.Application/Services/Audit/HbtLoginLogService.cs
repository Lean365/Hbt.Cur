#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 登录日志服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hbt.Cur.Common.Models;
using Hbt.Cur.Domain.Entities.Audit;
using Hbt.Cur.Application.Dtos.Audit;
using Hbt.Cur.Common.Exceptions;
using Hbt.Cur.Common.Utils;
using Hbt.Cur.Domain.Repositories;
using SqlSugar;
using Mapster;
using Hbt.Cur.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

namespace Hbt.Cur.Application.Services.Audit
{
    /// <summary>
    /// 登录日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-20
    /// </remarks>
    public class HbtLoginLogService : HbtBaseService, IHbtLoginLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private IHbtRepository<HbtLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<HbtLoginLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginLogService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));

        }
        /// <summary>
        /// 获取登录日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtLoginLogDto>> GetListAsync(HbtLoginLogQueryDto? query)
        {
            query ??= new HbtLoginLogQueryDto();

            try
            {
                var result = await LoginLogRepository.GetPagedListAsync(
                    QueryExpression(query),
                    query.PageIndex,
                    query.PageSize,
                    x => x.CreateTime,
                    OrderByType.Desc);

                return new HbtPagedResult<HbtLoginLogDto>
                {
                    TotalNum = result.TotalNum,
                    PageIndex = query.PageIndex,
                    PageSize = query.PageSize,
                    Rows = result.Rows.Adapt<List<HbtLoginLogDto>>()
                };
            }
            catch (Exception ex)
            {
                _logger.Error("获取登录日志分页列表失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取登录日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回登录日志详情</returns>
        public async Task<HbtLoginLogDto> GetByIdAsync(long logId)
        {
            try
            {
                var log = await LoginLogRepository.GetByIdAsync(logId);
                return log == null ? throw new HbtException(L("Audit.LoginLog.NotFound", logId)) : log.Adapt<HbtLoginLogDto>();
            }
            catch (Exception ex)
            {
                _logger.Error("获取登录日志详情失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 导出登录日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtLoginLogQueryDto query, string sheetName = "LoginLog")
        {
            try
            {
                var list = await LoginLogRepository.GetListAsync(QueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtLoginLogExportDto>>(), sheetName, L("Audit.LoginLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.LoginLog.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Audit.LoginLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空登录日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                var result = await LoginLogRepository.DeleteAsync((Expression<Func<HbtLoginLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("删除登录日志失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>任务</returns>
        public async Task UnlockUserAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtLoginLog>();
                exp.And(x => x.UserId == userId);
                
                await LoginLogRepository.DeleteAsync(exp.ToExpression());
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.LoginLog.UnlockFailed", userId, ex.Message), ex);
                throw new HbtException(L("Audit.LoginLog.UnlockFailed", userId));
            }
        }
        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtLoginLog, bool>> QueryExpression(HbtLoginLogQueryDto query)
        {
            return Expressionable.Create<HbtLoginLog>()
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName!))
                .AndIF(!string.IsNullOrEmpty(query.IpAddress), x => x.IpAddress.Contains(query.IpAddress!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
} 