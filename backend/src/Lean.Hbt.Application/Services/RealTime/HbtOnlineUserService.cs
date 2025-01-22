//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtOnlineUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线用户服务实现
//===================================================================

using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Common.Models;
using Mapster;
using SqlSugar;
using SqlSugar.Extensions;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.RealTime;

/// <summary>
/// 在线用户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineUserService : IHbtOnlineUserAppService
{
    private readonly IHbtRepository<HbtOnlineUser> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineUserService(IHbtRepository<HbtOnlineUser> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    public async Task<HbtPagedResult<HbtOnlineUserDto>> GetPagedListAsync(HbtOnlineUserQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineUser>();
        
        if (query.TenantId.HasValue)
            exp = exp.And(u => u.TenantId == query.TenantId.Value);
            
        if (query.UserId.HasValue)
            exp = exp.And(u => u.UserId == query.UserId.Value);

        // 2.查询数据
        var result = await _repository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize);

        // 3.转换并返回
        return new HbtPagedResult<HbtOnlineUserDto>
        {
            TotalNum = result.total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.list.Adapt<List<HbtOnlineUserDto>>()
        };
    }

    /// <summary>
    /// 获取在线用户导出数据
    /// </summary>
    public async Task<List<HbtOnlineUserExportDto>> GetExportDataAsync(HbtOnlineUserQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineUser>();
        
        if (query.TenantId.HasValue)
            exp = exp.And(u => u.TenantId == query.TenantId.Value);
            
        if (query.UserId.HasValue)
            exp = exp.And(u => u.UserId == query.UserId.Value);

        // 2.查询数据
        var users = await _repository.GetListAsync(exp.ToExpression());

        // 3.转换并返回
        return users.Adapt<List<HbtOnlineUserExportDto>>();
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
        var result = await _repository.InsertAsync(user);
        return result > 0;
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    public async Task<bool> DeleteOnlineUserAsync(string connectionId)
    {
        var exp = Expressionable.Create<HbtOnlineUser>();
        exp.And(u => u.ConnectionId == connectionId);
        var result = await _repository.DeleteAsync(exp.ToExpression());
        return result > 0;
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
} 