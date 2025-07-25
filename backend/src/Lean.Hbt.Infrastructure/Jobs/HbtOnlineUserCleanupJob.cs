//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineUserCleanupJob.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户清理任务 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Domain.Repositories;
using Quartz;

namespace Lean.Hbt.Infrastructure.Jobs
{
    /// <summary>
    /// 在线用户清理任务
    /// </summary>
    /// <remarks>
    /// 该任务每分钟执行一次，用于清理超时未发送心跳的用户。
    /// 主要功能：
    /// 1. 检测超过5分钟未更新心跳的用户
    /// 2. 将这些用户标记为离线状态
    /// 3. 更新用户的最后活动时间
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    [DisallowConcurrentExecution] // 禁止并发执行
    public class HbtOnlineUserCleanupJob : IJob
    {
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        /// <summary>
        /// 获取在线用户仓储
        /// </summary>
        private IHbtRepository<HbtOnlineUser> Repository => _repositoryFactory.GetAuthRepository<HbtOnlineUser>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志记录器</param>
        public HbtOnlineUserCleanupJob(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
        }

        /// <summary>
        /// 执行清理任务
        /// </summary>
        /// <param name="context">任务执行上下文</param>
        /// <returns>异步任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行在线用户清理任务");

                // 获取超过5分钟未更新心跳的用户
                var timeout = DateTime.Now.AddMinutes(-5);
                var timeoutUsers = await Repository.GetListAsync(
                    u => u.OnlineStatus == 0 && u.LastHeartbeat < timeout
                );

                if (timeoutUsers?.Any() == true)
                {
                    _logger.Info("发现{Count}个超时用户，开始批量更新", timeoutUsers.Count);

                    // 开启事务
                    try
                    {
                        var now = DateTime.Now;
                        foreach (var user in timeoutUsers)
                        {
                            user.OnlineStatus = 1;
                            user.LastActivity = now;
                            user.UpdateBy = "Hbt365";
                            user.UpdateTime = now;
                        }
                        await Repository.UpdateRangeAsync(timeoutUsers);

                        _logger.Info("成功清理{Count}个超时用户", timeoutUsers.Count);

                        // 记录每个用户的详细信息
                        foreach (var user in timeoutUsers)
                        {
                            _logger.Info(
                                "用户{UserId}被标记为离线 - 最后心跳时间:{LastHeartbeat}, 连接ID:{ConnectionId}",
                                user.UserId,
                                user.LastHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"),
                                user.ConnectionId ?? "未知");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("批量更新用户状态时发生错误", ex.Message);
                        throw;
                    }
                }
                else
                {
                    _logger.Info("没有发现需要清理的超时用户");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("执行在线用户清理任务时发生错误", ex.Message);
                throw;
            }
        }
    }
}