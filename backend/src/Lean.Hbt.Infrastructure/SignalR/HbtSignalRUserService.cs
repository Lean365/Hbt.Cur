//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSignalRUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-24 10:00
// 版本号 : V1.0.0
// 描述    : SignalR用户服务实现
//===================================================================

using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR 用户服务实现
    /// </summary>
    public class HbtSignalRUserService : IHbtSignalRUserService
    {
        private readonly IHubContext<HbtSignalRHub> _hubContext;
        private readonly ILogger<HbtSignalRUserService> _logger;
        private readonly IHbtRepository<HbtOnlineUser> _repository;

        public HbtSignalRUserService(
            IHubContext<HbtSignalRHub> hubContext,
            ILogger<HbtSignalRUserService> logger,
            IHbtRepository<HbtOnlineUser> repository)
        {
            _hubContext = hubContext;
            _logger = logger;
            _repository = repository;
        }

        public async Task SaveOnlineUserAsync(HbtOnlineUser user)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == user.ConnectionId);
                
                var existingUser = await _repository.GetInfoAsync(exp.ToExpression());
                if (existingUser != null)
                {
                    await _repository.UpdateAsync(user);
                }
                else
                {
                    await _repository.CreateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存在线用户时发生错误: ConnectionId={ConnectionId}, UserId={UserId}", user.ConnectionId, user.UserId);
                throw;
            }
        }

        public async Task<bool> DeleteOnlineUserAsync(string connectionId, string deleteBy)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);
                
                var user = await _repository.GetInfoAsync(exp.ToExpression());
                if (user != null)
                {
                    user.DeleteBy = deleteBy;
                    user.DeleteTime = DateTime.Now;
                    user.OnlineStatus = 1; // 设置为离线状态
                    await _repository.UpdateAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除在线用户时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        public async Task<List<string>> GetConnectionIdsAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                
                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户连接ID列表时发生错误: UserId={UserId}", userId);
                throw;
            }
        }

        public async Task<List<string>> GetGroupConnectionIdsAsync(long tenantId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.TenantId == tenantId && u.OnlineStatus == 0);
                
                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.ConnectionId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取租户组连接ID列表时发生错误: TenantId={TenantId}", tenantId);
                throw;
            }
        }

        public async Task UpdateUserLastActiveTimeAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.ConnectionId == connectionId);
                
                var user = await _repository.GetInfoAsync(exp.ToExpression());
                if (user != null)
                {
                    user.LastActivity = DateTime.Now;
                    await _repository.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新用户最后活动时间时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        public async Task<List<long>> GetMailReceivers(long mailId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.OnlineStatus == 0);
                
                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.UserId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取邮件接收者列表时发生错误: MailId={MailId}", mailId);
                throw;
            }
        }

        public async Task<List<long>> GetNoticeReceivers(long noticeId)
        {
            try
            {
                var exp = Expressionable.Create<HbtOnlineUser>();
                exp.And(u => u.OnlineStatus == 0);
                
                var users = await _repository.GetListAsync(exp.ToExpression());
                return users.Select(u => u.UserId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取通知接收者列表时发生错误: NoticeId={NoticeId}", noticeId);
                throw;
            }
        }

        public async Task DisconnectUserAsync(string connectionId)
        {
            try
            {
                _logger.LogInformation("断开用户连接: ConnectionId={ConnectionId}", connectionId);
                await _hubContext.Clients.Client(connectionId).SendAsync("ForceOffline", "您已被管理员强制下线");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "断开用户连接时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        public async Task SendMessageAsync(string connectionId, string method, object[] args)
        {
            try
            {
                await _hubContext.Clients.Client(connectionId).SendAsync(method, args);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "发送消息时发生错误: ConnectionId={ConnectionId}, Method={Method}", connectionId, method);
                throw;
            }
        }
    }
} 