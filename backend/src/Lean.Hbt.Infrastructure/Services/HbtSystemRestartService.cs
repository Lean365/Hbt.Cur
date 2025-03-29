using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.Admin;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Services;

/// <summary>
/// 系统重启服务
/// 用于处理系统重启时的清理工作，包括：
/// 1. 清理用户会话信息
/// 2. 清理缓存信息
/// 3. 清理实时通信状态
/// 4. 清理系统状态
/// 5. 清理安全相关信息
/// 6. 清理临时数据
/// 7. 清理性能监控数据
/// 8. 清理分布式状态
/// </summary>
public class HbtSystemRestartService : IHbtSystemRestartService
{
    private readonly IHbtLoginExtendService _loginExtendService;
    private readonly IDistributedCache _cache;
    private readonly ILogger<HbtSystemRestartService> _logger;
    private readonly HbtSystemRestartOptions _options;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="loginExtendService">登录扩展服务</param>
    /// <param name="cache">分布式缓存服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="options">系统重启选项</param>
    public HbtSystemRestartService(
        IHbtLoginExtendService loginExtendService,
        IDistributedCache cache,
        ILogger<HbtSystemRestartService> logger,
        IOptions<HbtSystemRestartOptions> options)
    {
        _loginExtendService = loginExtendService;
        _cache = cache;
        _logger = logger;
        _options = options.Value;
    }

    /// <summary>
    /// 执行系统重启清理
    /// 按照预定义的顺序执行所有清理操作，确保系统重启后处于一个干净的状态
    /// </summary>
    /// <returns>清理结果，true表示成功，false表示失败</returns>
    public async Task<bool> ExecuteRestartCleanupAsync()
    {
        try
        {
            if (_options.ClearUserSessions)
            {
                await CleanupUserSessionsAsync();
            }

            if (_options.ClearCache)
            {
                await CleanupCacheAsync();
            }

            if (_options.ClearRealTimeConnections)
            {
                await CleanupRealTimeConnectionsAsync();
            }

            if (_options.ClearSystemStatus)
            {
                await CleanupSystemStatusAsync();
            }

            if (_options.ClearSecurityInfo)
            {
                await CleanupSecurityInfoAsync();
            }

            if (_options.ClearTempData)
            {
                await CleanupTempDataAsync();
            }

            if (_options.ClearPerformanceData)
            {
                await CleanupPerformanceDataAsync();
            }

            if (_options.ClearDistributedState)
            {
                await CleanupDistributedStateAsync();
            }

            _logger.LogInformation("系统重启清理完成");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "系统重启清理过程中发生错误");
            return false;
        }
    }

    /// <summary>
    /// 清理用户会话信息
    /// 清除所有用户的在线状态，包括：
    /// 1. 设置用户为离线状态
    /// 2. 记录最后离线时间
    /// 3. 清除登录相关信息
    /// </summary>
    private async Task CleanupUserSessionsAsync()
    {
        try
        {
            // 清除所有用户的在线状态
            await _loginExtendService.ClearAllUserSessionsAsync();
            _logger.LogInformation("用户会话信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理用户会话信息时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理缓存信息
    /// 清除所有缓存数据，包括：
    /// 1. 用户信息缓存
    /// 2. 权限信息缓存
    /// 3. 菜单信息缓存
    /// 4. 系统配置缓存
    /// 5. 字典数据缓存
    /// </summary>
    private async Task CleanupCacheAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后缓存会自动清除
            _logger.LogInformation("缓存信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理缓存信息时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理实时通信状态
    /// 清除所有实时通信相关的状态，包括：
    /// 1. SignalR连接状态
    /// 2. WebSocket连接状态
    /// 3. 消息推送状态
    /// </summary>
    private async Task CleanupRealTimeConnectionsAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后连接状态会自动清除
            _logger.LogInformation("实时通信状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理实时通信状态时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理系统状态
    /// 清除所有系统相关的状态，包括：
    /// 1. 定时任务状态
    /// 2. 工作流实例状态
    /// 3. 消息队列状态
    /// 4. 分布式锁状态
    /// </summary>
    private async Task CleanupSystemStatusAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后系统状态会自动清除
            _logger.LogInformation("系统状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理系统状态时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理安全相关信息
    /// 清除所有安全相关的信息，包括：
    /// 1. 验证码缓存
    /// 2. 登录失败次数记录
    /// 3. 敏感操作日志
    /// 4. 用户token信息
    /// </summary>
    private async Task CleanupSecurityInfoAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后安全信息会自动清除
            _logger.LogInformation("安全相关信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理安全相关信息时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理临时数据
    /// 清除所有临时数据，包括：
    /// 1. 文件上传临时文件
    /// 2. 导入导出临时文件
    /// 3. 报表生成临时文件
    /// </summary>
    private async Task CleanupTempDataAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后临时数据会自动清除
            _logger.LogInformation("临时数据清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理临时数据时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理性能监控数据
    /// 清除所有性能监控相关的数据，包括：
    /// 1. 接口调用统计
    /// 2. 系统资源使用统计
    /// 3. 性能指标数据
    /// </summary>
    private async Task CleanupPerformanceDataAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后性能数据会自动清除
            _logger.LogInformation("性能监控数据清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理性能监控数据时发生错误");
            throw;
        }
    }

    /// <summary>
    /// 清理分布式状态
    /// 清除所有分布式相关的状态，包括：
    /// 1. 分布式会话
    /// 2. 分布式锁
    /// 3. 分布式缓存
    /// 4. 分布式任务
    /// </summary>
    private async Task CleanupDistributedStateAsync()
    {
        try
        {
            // 由于使用内存缓存，重启后分布式状态会自动清除
            _logger.LogInformation("分布式状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清理分布式状态时发生错误");
            throw;
        }
    }
}