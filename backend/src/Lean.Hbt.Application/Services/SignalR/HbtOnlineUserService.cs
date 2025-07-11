//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户服务实现
//===================================================================

using Lean.Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.SignalR;

/// <summary>
/// 在线用户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserService : HbtBaseService, IHbtOnlineUserService
{
    private readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtSignalRUserService _signalRUserService;

    private IHbtRepository<HbtOnlineUser> Repository => _repositoryFactory.GetAuthRepository<HbtOnlineUser>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="signalRUserService">SignalR用户服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtOnlineUserService(
        IHbtLogger logger,
        IHbtRepositoryFactory repositoryFactory,
        IHttpContextAccessor httpContextAccessor,
        IHbtSignalRUserService signalRUserService,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _signalRUserService = signalRUserService;
    }



    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    public async Task<HbtPagedResult<HbtOnlineUserDto>> GetListAsync(HbtOnlineUserQueryDto query)
    {
        try
        {
            // 1.构建查询条件
            var exp = QueryExpression(query);

            // 2.查询数据
            var result = await Repository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            // 3.转换数据
            return new HbtPagedResult<HbtOnlineUserDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtOnlineUserDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.GetListFailed"), ex);
            throw new HbtException(L("User.GetListFailed"));
        }
    }

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtOnlineUserQueryDto query, string sheetName = "User")
    {
        try
        {
            var exp = Expressionable.Create<HbtOnlineUser>();

            if (query.UserId.HasValue)
                exp = exp.And(u => u.UserId == query.UserId.Value);

            if (query.StartTime.HasValue)
                exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

            if (query.OnlineStatus.HasValue)
                exp = exp.And(u => u.OnlineStatus == query.OnlineStatus.Value);

            var list = await Repository.GetListAsync(exp.ToExpression());
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtOnlineUserExportDto>>(), sheetName, L("User.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.ExportFailed"), ex);
            throw new HbtException(L("User.ExportFailed"));
        }
    }

    /// <summary>
    /// 获取用户详情
    /// </summary>
    public async Task<HbtOnlineUserDto> GetUserAsync(long id)
    {
        try
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
                throw new HbtException(L("User.NotFound", id));

            return user.Adapt<HbtOnlineUserDto>();
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.GetFailed", id), ex);
            throw new HbtException(L("User.GetFailed", id));
        }
    }

    /// <summary>
    /// 保存用户信息
    /// </summary>
    public async Task<long> SaveUserAsync(HbtOnlineUserDto input)
    {
        try
        {
            var user = input.Adapt<HbtOnlineUser>();
            user.LastActivity = DateTime.Now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var result = await Repository.CreateAsync(user);
            if (result <= 0)
                throw new HbtException(L("User.SaveFailed"));

            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.SaveFailed"), ex);
            throw new HbtException(L("User.SaveFailed"));
        }
    }

    /// <summary>
    /// 删除用户信息
    /// </summary>
    public async Task<bool> DeleteUserAsync(long id)
    {
        try
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
                throw new HbtException(L("User.NotFound", id));

            var result = await Repository.DeleteAsync(id);
            if (result <= 0)
                throw new HbtException(L("User.DeleteFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.DeleteFailed", id), ex);
            throw new HbtException(L("User.DeleteFailed", id));
        }
    }

    /// <summary>
    /// 清理过期用户
    /// </summary>
    public async Task<int> CleanupExpiredUsersAsync(int minutes = 30)
    {
        try
        {
            var expiredTime = DateTime.Now.AddMinutes(-minutes);
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.LastActivity < expiredTime && u.OnlineStatus == 0);

            return await Repository.DeleteAsync(exp.ToExpression());
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.CleanupFailed"), ex);
            throw new HbtException(L("User.CleanupFailed"));
        }
    }

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    public async Task<bool> UpdateLastActivityTimeAsync(long id)
    {
        try
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
                throw new HbtException(L("User.NotFound", id));

            user.LastActivity = DateTime.Now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var result = await Repository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("User.UpdateFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.UpdateFailed", id), ex);
            throw new HbtException(L("User.UpdateFailed", id));
        }
    }

    /// <summary>
    /// 强制用户下线
    /// </summary>
    public async Task<bool> ForceOfflineAsync(long id)
    {
        try
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
                throw new HbtException(L("User.NotFound", id));

            user.LastActivity = DateTime.Now;
            user.OnlineStatus = 1; // 设置为离线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var result = await Repository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("User.ForceOfflineFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.ForceOfflineFailed", id), ex);
            throw new HbtException(L("User.ForceOfflineFailed", id));
        }
    }

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    public async Task<List<string>> GetConnectionIdsAsync(long userId)
    {
        var users = await Repository.GetListAsync(u => u.UserId == userId);
        return users.Select(u => u.ConnectionId).ToList();
    }

    /// <summary>
    /// 获取用户组连接ID列表
    /// </summary>
    public async Task<List<string>> GetGroupConnectionIdsAsync(long groupId)
    {
        var users = await Repository.GetListAsync(u => u.GroupId == groupId);
        return users.Select(u => u.ConnectionId).ToList();
    }

    /// <summary>
    /// 根据连接ID获取在线用户信息
    /// </summary>
    public async Task<HbtOnlineUserDto> GetUserByConnectionIdAsync(string connectionId)
    {
        var exp = Expressionable.Create<HbtOnlineUser>();
        exp = exp.And(u => u.ConnectionId == connectionId);

        var user = await Repository.GetFirstAsync(exp.ToExpression());
        return user?.Adapt<HbtOnlineUserDto>();
    }

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    public async Task<HbtOnlineUserDto> GetFirstAsync(Expression<Func<HbtOnlineUserDto, bool>> predicate)
    {
        // 由于DTO和实体字段相同，直接使用相同的lambda表达式
        var exp = predicate.Compile();
        var users = await Repository.GetListAsync();
        var dtos = users.Select(x => x.Adapt<HbtOnlineUserDto>());
        return dtos.FirstOrDefault(exp);
    }

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    public async Task<bool> UpdateAsync(HbtOnlineUserDto entity)
    {
        var user = entity.Adapt<HbtOnlineUser>();
        user.LastActivity = DateTime.Now;
        user.OnlineStatus = 0; // 设置为在线状态
        user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        return await Repository.UpdateAsync(user) > 0;
    }

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    public async Task<List<HbtOnlineUserDto>> GetAllAsync()
    {
        var users = await Repository.GetListAsync();
        return users.Select(x => x.Adapt<HbtOnlineUserDto>()).ToList();
    }

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    public async Task<int> UpdateHeartbeatAsync()
    {
        var users = await Repository.GetListAsync();
        if (!users.Any()) return 0;

        foreach (var user in users)
        {
            user.LastHeartbeat = DateTime.Now;
            user.LastActivity = DateTime.Now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            user.UpdateTime = DateTime.Now;
        }

        return await Repository.UpdateRangeAsync(users);
    }

    /// <summary>
    /// 保存在线用户
    /// </summary>
    public async Task<bool> SaveOnlineUserAsync(HbtOnlineUserDto input)
    {
        try
        {
            var user = input.Adapt<HbtOnlineUser>();
            user.LastActivity = DateTime.Now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var result = await Repository.CreateAsync(user);
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.SaveFailed"), ex);
            throw new HbtException(L("User.SaveFailed"));
        }
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    public async Task<bool> DeleteOnlineUserAsync(string connectionId, string deleteBy)
    {
        try
        {
            var exp = Expressionable.Create<HbtOnlineUser>();
            exp.And(u => u.ConnectionId == connectionId);

            var user = await Repository.GetFirstAsync(exp.ToExpression());
            if (user != null)
            {
                _logger.Info("正在强制用户下线: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, user.UserId);

                // 1. 发送强制下线消息
                try
                {
                    await _signalRUserService.SendMessageAsync(connectionId, "ForceOffline", new object[] { "您已被管理员强制下线" });
                    _logger.Info("已发送强制下线消息: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.Error("发送强制下线消息失败: ConnectionId={ConnectionId}", ex, connectionId);
                }

                // 2. 更新用户状态
                try
                {
                    user.DeleteBy = deleteBy;
                    user.DeleteTime = DateTime.Now;
                    user.OnlineStatus = 1; // 设置为离线状态
                    user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                    user.IsDeleted = 1;//管理员强制下线，删除标记
                    user.Remark = "被" + deleteBy + "要求强制下线";
                    await Repository.UpdateAsync(user);
                    _logger.Info("已更新用户状态为离线: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.Error("更新用户状态失败: ConnectionId={ConnectionId}", ex, connectionId);
                }

                // 3. 断开 SignalR 连接
                try
                {
                    await _signalRUserService.DisconnectUserAsync(connectionId);
                    _logger.Info("已断开用户连接: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.Error("断开用户连接失败: ConnectionId={ConnectionId}", ex, connectionId);
                }

                _logger.Info("用户强制下线成功: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, user.UserId);
                return true;
            }
            _logger.Warn("未找到要强制下线的用户: ConnectionId={ConnectionId}", connectionId);
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error("强制用户下线失败: ConnectionId={ConnectionId}", ex, connectionId);
            throw;
        }
    }
    /// <summary>
    /// 构建在线用户查询表达式
    /// </summary>
    private Expression<Func<HbtOnlineUser, bool>> QueryExpression(HbtOnlineUserQueryDto query)
    {
        var exp = Expressionable.Create<HbtOnlineUser>();

        if (query.UserId.HasValue)
            exp = exp.And(u => u.UserId == query.UserId.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

        if (query.OnlineStatus.HasValue)
            exp = exp.And(u => u.OnlineStatus == query.OnlineStatus.Value);

        return exp.ToExpression();
    }
}