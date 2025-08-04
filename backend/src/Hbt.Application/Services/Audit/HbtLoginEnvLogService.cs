#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginEnvLogService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录环境日志信息服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Hbt.Application.Services.Audit
{
    /// <summary>
    /// 登录环境日志服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtLoginEnvLogService : HbtBaseService, IHbtLoginEnvLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private IHbtRepository<HbtLoginEnvLog> LoginExtendRepository => _repositoryFactory.GetAuthRepository<HbtLoginEnvLog>();

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLoginEnvLogService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// 获取登录环境日志信息分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtLoginEnvLogDto>> GetListAsync(HbtLoginEnvLogQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await LoginExtendRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.LastLoginTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtLoginEnvLogDto>
            {
                Rows = result.Rows.Adapt<List<HbtLoginEnvLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取登录环境日志详情
        /// </summary>
        /// <param name="loginExtendId">登录环境日志ID</param>
        /// <returns>返回登录环境日志详细信息</returns>
        /// <exception cref="HbtException">当登录环境日志不存在时抛出异常</exception>
        public async Task<HbtLoginEnvLogDto> GetByIdAsync(long loginExtendId)
        {
            var loginExtend = await LoginExtendRepository.GetByIdAsync(loginExtendId);
            if (loginExtend == null)
                throw new HbtException(L("Identity.LoginExtend.NotFound", loginExtendId));

            return loginExtend.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 创建登录环境日志
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>登录环境日志ID</returns>
        public async Task<long> CreateAsync(HbtLoginEnvLogCreateDto input)
        {
            // 验证用户ID是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(LoginExtendRepository, "UserId", input.UserId.ToString());

            var loginExtend = input.Adapt<HbtLoginEnvLog>();
            return await LoginExtendRepository.CreateAsync(loginExtend) > 0 ? loginExtend.Id : throw new HbtException(L("Identity.LoginExtend.CreateFailed"));
        }

        /// <summary>
        /// 更新登录环境日志
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtLoginEnvLogUpdateDto input)
        {
            var loginExtend = await LoginExtendRepository.GetByIdAsync(input.Id)
                ?? throw new HbtException(L("Identity.LoginExtend.NotFound", input.Id));

            input.Adapt(loginExtend);
            return await LoginExtendRepository.UpdateAsync(loginExtend) > 0;
        }

        /// <summary>
        /// 更新登录在线时段
        /// </summary>
        public async Task<HbtLoginEnvLogDto> UpdateOnlinePeriodAsync(HbtLoginEnvLogOnlinePeriodUpdateDto request)
        {
            var loginExtend = await LoginExtendRepository.GetFirstAsync(
                x => x.UserId == request.UserId);
            if (loginExtend == null)
            {
                throw new InvalidOperationException($"用户{request.UserId}的登录环境日志信息不存在");
            }

            loginExtend.TodayOnlinePeriods = request.OnlinePeriod;
            await LoginExtendRepository.UpdateAsync(loginExtend);

            return loginExtend.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 导出登录环境日志数据
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtLoginEnvLogDto> data, string sheetName = "HbtLoginEnvLog")
        {
            return await HbtExcelHelper.ExportAsync(data, sheetName, "登录环境日志数据");
        }

        /// <summary>
        /// 更新登录环境日志状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtLoginEnvLogStatusDto input)
        {
            var loginExtend = await LoginExtendRepository.GetByIdAsync(input.LoginId)
                ?? throw new HbtException(L("Identity.LoginExtend.NotFound", input.LoginId));

            input.Adapt(loginExtend);
            return await LoginExtendRepository.UpdateAsync(loginExtend) > 0;
        }

        /// <summary>
        /// 删除登录环境日志
        /// </summary>
        /// <param name="loginExtendId">登录环境日志ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long loginExtendId)
        {
            var loginExtend = await LoginExtendRepository.GetByIdAsync(loginExtendId)
                ?? throw new HbtException(L("Identity.LoginExtend.NotFound", loginExtendId));

            return await LoginExtendRepository.DeleteAsync(loginExtendId) > 0;
        }

        /// <summary>
        /// 批量删除登录环境日志
        /// </summary>
        /// <param name="loginExtendIds">登录环境日志ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] loginExtendIds)
        {
            if (loginExtendIds == null || loginExtendIds.Length == 0)
                throw new HbtException(L("Identity.LoginExtend.SelectRequired"));

            return await LoginExtendRepository.DeleteRangeAsync(loginExtendIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        public async Task<HbtLoginEnvLogDto> UpdateLoginInfoAsync(HbtLoginEnvLogUpdateDto request)
        {
            var now = DateTime.Now;
            var loginExtend = await LoginExtendRepository.GetFirstAsync(x =>
                x.UserId == request.UserId &&
                x.Id == request.Id);

            if (loginExtend == null)
            {
                loginExtend = new HbtLoginEnvLog
                {
                    UserId = request.UserId,
                    Id = request.Id,
                    LoginType = request.LoginType,
                    LoginStatus = 1, // 1表示在线
                    LastLoginTime = now
                };

                await LoginExtendRepository.CreateAsync(loginExtend);
            }
            else
            {
                loginExtend.LoginType = request.LoginType;
                loginExtend.LoginStatus = 1; // 1表示在线
                loginExtend.LastLoginTime = now;

                await LoginExtendRepository.UpdateAsync(loginExtend);
            }

            return loginExtend.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 更新登录离线信息
        /// </summary>
        public async Task<HbtLoginEnvLogDto> UpdateOfflineInfoAsync(long userId)
        {
            var loginExtend = await LoginExtendRepository.GetFirstAsync(x => x.UserId == userId);
            if (loginExtend == null)
            {
                throw new InvalidOperationException($"用户{userId}的登录环境日志信息不存在");
            }

            loginExtend.LoginStatus = 0; // 0表示离线
            loginExtend.LastOfflineTime = DateTime.Now;

            await LoginExtendRepository.UpdateAsync(loginExtend);
            return loginExtend.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 获取用户的登录环境日志信息
        /// </summary>
        public async Task<HbtLoginEnvLogDto?> GetByUserIdAndLoginExtendIdAsync(long userId, long loginExtendId)
        {
            var loginExtend = await LoginExtendRepository.GetFirstAsync(
                x => x.UserId == userId && x.Id == loginExtendId);
            return loginExtend?.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 获取用户的所有登录环境日志信息
        /// </summary>
        public async Task<HbtLoginEnvLogDto?> GetByUserIdAsync(long userId)
        {
            var loginExtend = await LoginExtendRepository.GetFirstAsync(x => x.UserId == userId);
            return loginExtend?.Adapt<HbtLoginEnvLogDto>();
        }

        /// <summary>
        /// 清除所有用户会话
        /// </summary>
        public async Task<bool> ClearAllUserSessionsAsync()
        {
            var loginExtends = await LoginExtendRepository.GetListAsync(x => x.LoginStatus == 1); // 1表示在线
            foreach (var loginExtend in loginExtends)
            {
                loginExtend.LoginStatus = 0; // 0表示离线
                loginExtend.LastOfflineTime = DateTime.Now;
            }
            return await LoginExtendRepository.UpdateRangeAsync(loginExtends) > 0;
        }

        /// <summary>
        /// 构建登录环境日志信息查询条件
        /// </summary>
        private Expression<Func<HbtLoginEnvLog, bool>> QueryExpression(HbtLoginEnvLogQueryDto query)
        {
            var exp = Expressionable.Create<HbtLoginEnvLog>();

            if (query.UserId.HasValue)
                exp.And(x => x.UserId == query.UserId.Value);

            if (query.LoginType.HasValue)
                exp.And(x => x.LoginType == query.LoginType.Value);

            if (query.LoginStatus.HasValue)
                exp.And(x => x.LoginStatus == query.LoginStatus.Value);

            if (query.LastLoginTimeStart.HasValue)
                exp.And(x => x.LastLoginTime >= query.LastLoginTimeStart.Value);

            if (query.LastLoginTimeEnd.HasValue)
                exp.And(x => x.LastLoginTime <= query.LastLoginTimeEnd.Value);

            return exp.ToExpression();
        }
    }
}