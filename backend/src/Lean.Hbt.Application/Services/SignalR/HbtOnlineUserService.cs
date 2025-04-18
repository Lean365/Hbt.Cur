//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.SignalR;
using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Domain.IServices.SignalR;

namespace Lean.Hbt.Application.Services.SignalR;

/// <summary>
/// 在线用户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserService : IHbtOnlineUserService
{
    private readonly IHbtRepository<HbtOnlineUser> _repository;
    private readonly IHbtSignalRUserService _signalRUserService;
    private readonly ILogger<HbtOnlineUserService> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserService(
        IHbtRepository<HbtOnlineUser> repository,
        IHbtSignalRUserService signalRUserService,
        ILogger<HbtOnlineUserService> logger)
    {
        _repository = repository;
        _signalRUserService = signalRUserService;
        _logger = logger;
    }

    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    public async Task<HbtPagedResult<HbtOnlineUserDto>> GetListAsync(HbtOnlineUserQueryDto query)
    {
        try
        {
            _logger.LogInformation("开始查询在线用户列表，参数：{@Query}", query);

            // 1.构建查询条件
            var exp = Expressionable.Create<HbtOnlineUser>();

            // 租户ID是必须的
            exp = exp.And(u => u.TenantId == query.TenantId);

            if (query.UserId.HasValue)
                exp = exp.And(u => u.UserId == query.UserId.Value);

            if (query.StartTime.HasValue)
                exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

            if (query.OnlineStatus.HasValue)
                exp = exp.And(u => u.OnlineStatus == query.OnlineStatus.Value);

            _logger.LogInformation("查询条件构建完成");

            // 2.查询数据
            var result = await _repository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.LastActivity,
                OrderByType.Desc);

            _logger.LogInformation("查询到数据：总数={Total}, 当前页={PageIndex}, 每页={PageSize}",
                result.TotalNum, query.PageIndex, query.PageSize);

            // 3.转换数据
            return new HbtPagedResult<HbtOnlineUserDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Select(x => x.Adapt<HbtOnlineUserDto>()).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询在线用户列表时发生错误");
            throw new Exception("查询在线用户列表失败", ex);
        }
    }

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<byte[]> ExportAsync(HbtOnlineUserQueryDto query, string sheetName = "在线用户信息")
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineUser>();

        // 租户ID是必须的

        if (query.UserId.HasValue)
            exp = exp.And(u => u.UserId == query.UserId.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

        // 2.查询数据
        var users = await _repository.GetListAsync(exp.ToExpression());

        // 3.转换数据
        var dtos = users.Adapt<List<HbtOnlineUserDto>>();

        // 4.导出Excel
        return await HbtExcelHelper.ExportAsync(dtos, sheetName);
    }

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    public async Task<List<string>> GetConnectionIdsAsync(long userId)
    {
        var users = await _repository.GetListAsync(u => u.UserId == userId);
        return users.Select(u => u.ConnectionId).ToList();
    }

    /// <summary>
    /// 获取用户组连接ID列表
    /// </summary>
    public async Task<List<string>> GetGroupConnectionIdsAsync(long groupId)
    {
        var users = await _repository.GetListAsync(u => u.GroupId == groupId);
        return users.Select(u => u.ConnectionId).ToList();
    }

    /// <summary>
    /// 保存在线用户
    /// </summary>
    public async Task<bool> SaveOnlineUserAsync(HbtOnlineUserDto input)
    {
        var user = input.Adapt<HbtOnlineUser>();
        user.LastActivity = DateTime.Now;
        var result = await _repository.CreateAsync(user);
        return result > 0;
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

            var user = await _repository.GetFirstAsync(exp.ToExpression());
            if (user != null)
            {
                _logger.LogInformation("正在强制用户下线: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, user.UserId);

                // 1. 发送强制下线消息
                try
                {
                    await _signalRUserService.SendMessageAsync(connectionId, "ForceOffline", new object[] { "您已被管理员强制下线" });
                    _logger.LogInformation("已发送强制下线消息: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "发送强制下线消息失败: ConnectionId={ConnectionId}", connectionId);
                }

                // 2. 更新用户状态
                try
                {
                    user.DeleteBy = deleteBy;
                    user.DeleteTime = DateTime.Now;
                    user.OnlineStatus = 1; // 设置为离线状态
                    user.IsDeleted = 1;//管理员强制下线，删除标记
                    user.Remark = "被" + deleteBy + "要求强制下线";
                    await _repository.UpdateAsync(user);
                    _logger.LogInformation("已更新用户状态为离线: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "更新用户状态失败: ConnectionId={ConnectionId}", connectionId);
                }

                // 3. 断开 SignalR 连接
                try
                {
                    await _signalRUserService.DisconnectUserAsync(connectionId);
                    _logger.LogInformation("已断开用户连接: ConnectionId={ConnectionId}", connectionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "断开用户连接失败: ConnectionId={ConnectionId}", connectionId);
                }

                _logger.LogInformation("用户强制下线成功: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, user.UserId);
                return true;
            }
            _logger.LogWarning("未找到要强制下线的用户: ConnectionId={ConnectionId}", connectionId);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "强制用户下线失败: ConnectionId={ConnectionId}", connectionId);
            throw;
        }
    }

    /// <summary>
    /// 清理过期用户
    /// </summary>
    public async Task<int> CleanupExpiredUsersAsync(int minutes = 20)
    {
        var expiredTime = DateTime.Now.AddMinutes(-minutes);
        var exp = Expressionable.Create<HbtOnlineUser>();
        exp.And(u => u.LastActivity < expiredTime);

        return await _repository.DeleteAsync(exp.ToExpression());
    }

    /// <summary>
    /// 根据连接ID获取在线用户信息
    /// </summary>
    public async Task<HbtOnlineUserDto> GetUserByConnectionIdAsync(string connectionId)
    {
        var exp = Expressionable.Create<HbtOnlineUser>();
        exp = exp.And(u => u.ConnectionId == connectionId);

        var user = await _repository.GetFirstAsync(exp.ToExpression());
        return user?.Adapt<HbtOnlineUserDto>();
    }

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    public async Task<HbtOnlineUserDto> GetFirstAsync(Expression<Func<HbtOnlineUserDto, bool>> predicate)
    {
        // 由于DTO和实体字段相同，直接使用相同的lambda表达式
        var exp = predicate.Compile();
        var users = await _repository.GetListAsync();
        var dtos = users.Select(x => x.Adapt<HbtOnlineUserDto>());
        return dtos.FirstOrDefault(exp);
    }

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    public async Task<bool> UpdateAsync(HbtOnlineUserDto entity)
    {
        var user = entity.Adapt<HbtOnlineUser>();
        return await _repository.UpdateAsync(user) > 0;
    }

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    public async Task<List<HbtOnlineUserDto>> GetAllAsync()
    {
        var users = await _repository.GetListAsync();
        return users.Select(x => x.Adapt<HbtOnlineUserDto>()).ToList();
    }

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    public async Task<int> UpdateHeartbeatAsync()
    {
        var users = await _repository.GetListAsync();
        if (!users.Any()) return 0;

        foreach (var user in users)
        {
            user.LastHeartbeat = DateTime.Now;
            user.LastActivity = DateTime.Now;
            user.UpdateTime = DateTime.Now;
        }

        return await _repository.UpdateRangeAsync(users);
    }
}