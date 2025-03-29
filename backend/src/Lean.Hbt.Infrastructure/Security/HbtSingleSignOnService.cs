using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 单点登录服务实现
    /// </summary>
    /// <remarks>
    /// 提供单点登录的核心功能，包括：
    /// 1. 用户登录限制检查
    /// 2. 多设备登录管理
    /// 3. 会话踢出处理
    /// 4. 用户限制豁免判断
    /// </remarks>
    public class HbtSingleSignOnService : IHbtSingleSignOnService
    {
        private readonly ILogger<HbtSingleSignOnService> _logger;
        private readonly IHbtSignalRUserService _signalRUserService;
        private readonly IHbtSignalRClient _signalRClient;
        private readonly HbtSingleSignOnOptions _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRUserService">SignalR用户服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="options">单点登录配置选项</param>
        public HbtSingleSignOnService(
            ILogger<HbtSingleSignOnService> logger,
            IHbtSignalRUserService signalRUserService,
            IHbtSignalRClient signalRClient,
            IOptions<HbtSingleSignOnOptions> options)
        {
            _logger = logger;
            _signalRUserService = signalRUserService;
            _signalRClient = signalRClient;
            _options = options.Value;
        }

        /// <summary>
        /// 检查用户是否可以登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <returns>true: 允许登录, false: 禁止登录</returns>
        /// <remarks>
        /// 判断逻辑：
        /// 1. 如果单点登录未启用，允许登录
        /// 2. 如果用户在豁免名单中，允许登录
        /// 3. 如果用户没有活跃会话，允许登录
        /// </remarks>
        public async Task<bool> CanUserLoginAsync(long userId, string userName)
        {
            if (!_options.Enabled || !await IsUserRestrictedAsync(userId, userName))
            {
                return true;
            }

            var activeSessionCount = await GetUserActiveSessionCountAsync(userId);
            return activeSessionCount == 0;
        }

        /// <summary>
        /// 处理用户登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">当前连接ID</param>
        /// <returns>处理结果</returns>
        /// <remarks>
        /// 处理流程：
        /// 1. 检查是否启用单点登录和用户限制
        /// 2. 如果配置为踢出旧会话：
        ///    - 获取用户所有连接
        ///    - 通知并关闭旧连接
        ///    - 记录踢出日志
        /// </remarks>
        public async Task HandleUserLoginAsync(string userId, string connectionId)
        {
            // 如果未启用单点登录，直接返回
            if (!_options.Enabled)
            {
                _logger.LogInformation("单点登录未启用，允许多点登录");
                return;
            }

            // 检查用户是否属于豁免角色
            if (await IsUserExemptAsync(userId))
            {
                _logger.LogInformation($"用户 {userId} 属于豁免角色，允许多点登录");
                return;
            }

            // 获取用户现有会话
            var existingConnections = await _signalRUserService.GetConnectionIdsAsync(long.Parse(userId));
            if (existingConnections?.Any() == true)
            {
                // 如果配置了踢出旧会话
                if (_options.KickoutOldSession)
                {
                    foreach (var oldConnection in existingConnections)
                    {
                        // 如果配置了通知
                        if (_options.NotifyKickout)
                        {
                            await _signalRClient.ForceOffline(_options.KickoutMessage);
                        }

                        // 删除旧连接
                        await _signalRUserService.DeleteOnlineUserAsync(oldConnection, userId);
                        _logger.LogInformation($"已踢出用户 {userId} 的旧会话 {oldConnection}");
                    }
                }
            }
        }

        /// <summary>
        /// 处理用户登出
        /// </summary>
        public async Task HandleUserLogoutAsync(string userId, string connectionId)
        {
            await _signalRUserService.DeleteOnlineUserAsync(connectionId, userId);
            _logger.LogInformation($"用户 {userId} 已登出，连接 {connectionId} 已删除");
        }

        /// <summary>
        /// 处理用户登出
        /// </summary>
        public async Task HandleLogoutAsync(string userId, string connectionId)
        {
            await HandleUserLogoutAsync(userId, connectionId);
        }

        /// <summary>
        /// 获取用户当前活跃会话数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>活跃会话数量</returns>
        /// <remarks>
        /// 通过SignalR用户服务获取用户的所有连接数量
        /// </remarks>
        public async Task<int> GetUserActiveSessionCountAsync(long userId)
        {
            var connections = await _signalRUserService.GetConnectionIdsAsync(userId);
            return connections.Count;
        }

        /// <summary>
        /// 检查用户是否受单点登录限制
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <returns>true: 受限制, false: 不受限制</returns>
        /// <remarks>
        /// 判断逻辑：
        /// 1. 检查用户是否在豁免用户列表中
        /// 2. TODO: 检查用户角色是否在豁免角色列表中
        /// </remarks>
        public async Task<bool> IsUserRestrictedAsync(long userId, string userName)
        {
            // 检查用户是否在排除列表中
            if (_options.ExcludeUsers.Contains(userName))
            {
                return false;
            }

            // TODO: 检查用户角色是否在排除列表中
            // 这里需要注入用户服务来获取用户角色
            // var userRoles = await _userService.GetUserRolesAsync(userId);
            // if (userRoles.Any(role => _options.ExcludeRoles.Contains(role)))
            // {
            //     return false;
            // }

            return true;
        }

        /// <summary>
        /// 处理新的登录
        /// </summary>
        public async Task HandleNewLoginAsync(string userId, string userName, string connectionId)
        {
            // 如果未启用单点登录，直接返回
            if (!_options.Enabled)
            {
                _logger.LogInformation("单点登录未启用，允许多点登录");
                return;
            }

            // 检查用户是否属于豁免角色或豁免用户
            if (_options.ExcludeUsers.Contains(userName))
            {
                _logger.LogInformation($"用户 {userName} 在豁免名单中，允许多点登录");
                return;
            }

            // 获取用户现有会话
            var existingConnections = await _signalRUserService.GetConnectionIdsAsync(long.Parse(userId));
            if (existingConnections?.Any() == true)
            {
                // 如果配置了踢出旧会话
                if (_options.KickoutOldSession)
                {
                    foreach (var oldConnection in existingConnections)
                    {
                        // 如果配置了通知
                        if (_options.NotifyKickout)
                        {
                            await _signalRClient.ForceOffline(_options.KickoutMessage);
                        }

                        // 删除旧连接
                        await _signalRUserService.DeleteOnlineUserAsync(oldConnection, userId);
                        _logger.LogInformation($"已踢出用户 {userName}({userId}) 的旧会话 {oldConnection}");
                    }
                }
            }
        }

        /// <summary>
        /// 检查用户是否已在其他设备登录
        /// </summary>
        public async Task<bool> IsUserLoggedInElsewhereAsync(string userId, string connectionId)
        {
            if (!_options.Enabled)
            {
                return false;
            }

            var connections = await _signalRUserService.GetConnectionIdsAsync(long.Parse(userId));
            return connections.Any(c => c != connectionId);
        }

        /// <summary>
        /// 获取用户的活跃连接数
        /// </summary>
        public async Task<int> GetActiveConnectionCountAsync(string userId)
        {
            var connections = await _signalRUserService.GetConnectionIdsAsync(long.Parse(userId));
            return connections.Count;
        }

        /// <summary>
        /// 检查用户是否属于豁免角色
        /// </summary>
        private async Task<bool> IsUserExemptAsync(string userId)
        {
            // TODO: 实现角色检查逻辑
            // 这里需要调用您的用户服务来获取用户角色
            // 然后检查是否在豁免列表中
            return false;
        }
    }
} 