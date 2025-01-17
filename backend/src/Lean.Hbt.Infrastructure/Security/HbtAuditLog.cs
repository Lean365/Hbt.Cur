//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtAuditLog.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 审计日志实现
//===================================================================

using Lean.Hbt.Infrastructure.Logging;
using Newtonsoft.Json;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// 审计日志实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtAuditLog : IHbtAuditLog
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly HbtDbContext _context;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="context">数据库上下文</param>
        public HbtAuditLog(IHbtLogger logger, HbtDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="module">模块</param>
        /// <param name="operation">操作</param>
        /// <param name="details">详细信息</param>
        /// <returns>异步任务</returns>
        public async Task LogOperationAsync(string userId, string userName, string module, string operation, object details = null)
        {
            var log = new HbtAuditLogEntity
            {
                UserId = userId,
                UserName = userName,
                Module = module,
                Operation = operation,
                Details = details != null ? JsonConvert.SerializeObject(details) : null,
                IpAddress = GetClientIpAddress(),
                UserAgent = GetUserAgent(),
                CreatedTime = DateTime.Now
            };

            try
            {
                var repo = _context.GetRepository<HbtAuditLogEntity>();
                await repo.InsertAsync(log);
                _logger.Info($"记录操作日志成功: {userName} 在 {module} 模块执行了 {operation} 操作");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "记录操作日志失败");
                throw;
            }
        }

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        /// <returns>异步任务</returns>
        public async Task LogLoginAsync(string userId, string userName, bool success, string message = null)
        {
            var log = new HbtLoginLogEntity
            {
                UserId = userId,
                UserName = userName,
                Success = success,
                Message = message,
                IpAddress = GetClientIpAddress(),
                UserAgent = GetUserAgent(),
                CreatedTime = DateTime.Now
            };

            try
            {
                var repo = _context.GetRepository<HbtLoginLogEntity>();
                await repo.InsertAsync(log);
                _logger.Info($"记录登录日志成功: {userName} 登录{(success ? "成功" : "失败")} - {message}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "记录登录日志失败");
                throw;
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        private string GetClientIpAddress()
        {
            // TODO: 从HttpContext获取客户端IP地址
            return "127.0.0.1";
        }

        /// <summary>
        /// 获取用户代理
        /// </summary>
        /// <returns>用户代理</returns>
        private string GetUserAgent()
        {
            // TODO: 从HttpContext获取用户代理
            return "Unknown";
        }
    }

    /// <summary>
    /// 审计日志实体
    /// </summary>
    public class HbtAuditLogEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }

    /// <summary>
    /// 登录日志实体
    /// </summary>
    public class HbtLoginLogEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
} 