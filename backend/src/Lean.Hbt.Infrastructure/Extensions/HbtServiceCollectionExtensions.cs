//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:30
// 版本号 : V.0.0.1
// 描述    : 服务集合扩展
//===================================================================

using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Application.Services.SignalR;
using Lean.Hbt.Application.Services.Routine;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Audit;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Infrastructure.Authentication;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Identity;
using Lean.Hbt.Infrastructure.Logging;
using Lean.Hbt.Infrastructure.Security;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Infrastructure.Services.Identity;
using Lean.Hbt.Infrastructure.Services.Local;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 服务集合扩展类
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册系统所需的所有服务，包括：
    /// 1. 领域层服务 - 包含核心业务逻辑
    /// 2. 应用层服务 - 处理用户交互和业务流程
    /// 3. 基础设施服务 - 提供技术支持和底层实现
    /// 4. 工作流服务 - 处理业务流程自动化
    /// </remarks>
    public static class HbtServiceCollectionExtensions
    {
        /// <summary>
        /// 获取当前用户名
        /// </summary>
        /// <returns>当前用户名，如果未登录则返回null</returns>
        private static string? GetCurrentUserName()
        {
            var httpContextAccessor = new HttpContextAccessor();
            return httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        /// <summary>
        /// 添加领域层服务
        /// </summary>
        /// <remarks>
        /// 注册领域层相关的所有服务，包括：
        /// 1. 仓储服务 - 数据访问抽象
        /// 2. 安全服务 - 登录和密码策略
        /// 3. 会话服务 - 用户会话管理
        /// 4. 验证服务 - 验证码和OAuth认证
        /// 5. 日志服务 - 系统日志管理
        /// 6. 本地化服务 - 多语言支持
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // 添加仓储服务 - 用于数据访问
            services.AddScoped(typeof(IHbtRepository<>), typeof(HbtRepository<>));

            // 添加领域服务
            services.AddSecurityServices();     // 安全服务
            services.AddCacheServices();        // 缓存服务
            services.AddLogServices();          // 日志服务
            services.AddLocalizationServices(); // 本地化服务

            // 添加服务器监控服务
            services.AddScoped<IHbtServerMonitorService, HbtServerMonitorService>();

            return services;
        }

        /// <summary>
        /// 添加应用层服务
        /// </summary>
        /// <remarks>
        /// 注册应用层相关的所有服务，包括：
        /// 1. 身份认证服务 - 用户认证和授权
        /// 2. 审计日志服务 - 系统操作记录
        /// 3. 系统管理服务 - 系统配置和维护
        /// 4. 实时通信服务 - 即时消息和通知
        /// 5. 工作流服务 - 业务流程管理
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddIdentityServices();    // 身份认证服务
            services.AddAdminServices();       // 管理服务
            services.AddAuditServices();       // 审计服务
            services.AddRealTimeServices();    // 实时服务
            services.AddWorkflowServices();    // 工作流服务
            services.AddRoutineServices();     // 常规业务服务

            return services;
        }

        /// <summary>
        /// 添加安全服务
        /// </summary>
        private static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            // 安全策略服务
            services.AddScoped<IHbtLoginPolicy, HbtLoginPolicy>();
            services.AddScoped<IHbtPasswordPolicy, HbtPasswordPolicy>();

            // 注册会话管理
            services.AddScoped<IHbtIdentitySessionManager, HbtSessionManager>();
            services.AddScoped<IHbtSingleSignOnService, HbtSingleSignOnService>();

            // 验证服务
            services.AddSingleton<IHbtCaptchaService, HbtCaptchaService>();
            services.AddScoped<IHbtOAuthService, HbtOAuthService>();

            return services;
        }

        /// <summary>
        /// 添加缓存服务
        /// </summary>
        private static IServiceCollection AddCacheServices(this IServiceCollection services)
        {
            // 内存缓存
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            // 缓存服务
            services.AddScoped<IHbtMemoryCache, HbtMemoryCache>();
            services.AddScoped<HbtCacheConfigManager>();

            return services;
        }

        /// <summary>
        /// 添加日志服务
        /// </summary>
        private static IServiceCollection AddLogServices(this IServiceCollection services)
        {
            // 添加日志记录器 - 只在这里注册一次
            services.AddScoped<IHbtLogger, HbtNLogger>();
            services.AddScoped<IHbtLogCleanupService, HbtLogCleanupService>();
            services.AddScoped<IHbtLogArchiveService, HbtLogArchiveService>();

            return services;
        }

        /// <summary>
        /// 添加本地化服务
        /// </summary>
        private static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            // 所有本地化相关服务使用 Scoped 生命周期
            services.AddScoped<IHbtTranslationCache, HbtTranslationCache>();     // 翻译缓存服务
            services.AddScoped<IHbtLocalizationService, HbtLocalizationService>();  // 本地化服务
            services.AddScoped<IHbtTranslationService, HbtTranslationService>();    // 翻译服务
            services.AddScoped<IHbtLanguageService, HbtLanguageService>();          // 语言服务

            return services;
        }

        /// <summary>
        /// 添加身份认证服务
        /// </summary>
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            // 核心身份认证服务
            services.AddScoped<IHbtAuthService, HbtAuthService>();         // 登录服务
            services.AddScoped<IHbtLoginExtendService, HbtLoginExtendService>(); // 登录扩展服务
            services.AddScoped<IHbtDeviceExtendService, HbtDeviceExtendService>(); // 设备扩展服务
            services.AddScoped<IHbtJwtHandler, HbtJwtHandler>();            // JWT令牌处理
            services.AddScoped<IHbtDeviceIdGenerator, HbtDeviceIdGenerator>(); // 设备ID生成器

            // 用户和权限管理
            services.AddScoped<IHbtUserService, HbtUserService>();          // 用户管理
            services.AddScoped<IHbtRoleService, HbtRoleService>();          // 角色管理
            services.AddScoped<IHbtDeptService, HbtDeptService>();          // 部门管理
            services.AddScoped<IHbtPostService, HbtPostService>();          // 岗位管理
            services.AddScoped<IHbtMenuService, HbtMenuService>();          // 菜单管理

            // 租户管理
            services.AddScoped<IHbtTenantService, HbtTenantService>();      // 租户服务

            return services;
        }

        /// <summary>
        /// 添加管理服务
        /// </summary>
        private static IServiceCollection AddAdminServices(this IServiceCollection services)
        {
            // 系统服务
            services.AddScoped<IHbtConfigService, HbtConfigService>();     // 系统配置

            // 字典和类型服务
            services.AddScoped<IHbtDictDataService, HbtDictDataService>();       // 字典数据服务
            services.AddScoped<IHbtDictTypeService, HbtDictTypeService>();       // 字典类型服务

            return services;
        }

        /// <summary>
        /// 添加审计服务
        /// </summary>
        public static IServiceCollection AddAuditServices(this IServiceCollection services)
        {
            // 审计日志记录器
            services.AddScoped<IHbtLogManager, HbtLogManager>();                  // 基础日志管理器
            services.AddScoped<IHbtAuditLogManager, HbtLogManager>();            // 审计日志管理器
            services.AddScoped<IHbtDbDiffLogManager, HbtLogManager>();           // 数据库差异日志管理器
            services.AddScoped<IHbtOperLogManager, HbtLogManager>();             // 操作日志管理器
            services.AddScoped<IHbtExceptionLogManager, HbtLogManager>();        // 异常日志管理器

            // 审计日志服务
            services.AddScoped<IHbtAuditsLogService, HbtAuditLogService>();     // 审计日志服务
            services.AddScoped<IHbtLoginLogService, HbtLoginLogService>();       // 登录日志服务
            services.AddScoped<IHbtOperLogService, HbtOperLogService>();         // 操作日志服务
            services.AddScoped<IHbtExceptionLogService, HbtExceptionLogService>(); // 异常日志服务
            services.AddScoped<IHbtDbDiffLogService, HbtDbDiffLogService>();     // 数据库差异日志服务

            return services;
        }

        /// <summary>
        /// 添加实时服务
        /// </summary>
        public static IServiceCollection AddRealTimeServices(this IServiceCollection services)
        {
            services.AddScoped<IHbtOnlineUserService, HbtOnlineUserService>();   // 在线用户服务
            services.AddScoped<IHbtOnlineMessageService, HbtOnlineMessageService>();   // 在线消息服务
            return services;
        }

        /// <summary>
        /// 添加常规业务服务
        /// </summary>
        /// <remarks>
        /// 注册常规业务相关的所有服务，包括：
        /// 1. 任务服务 - 定时任务和日志
        /// 2. 邮件服务 - 邮件发送和模板
        /// 3. 文件服务 - 文件上传和管理
        /// 4. 通知服务 - 系统通知管理
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddRoutineServices(this IServiceCollection services)
        {
            // 任务相关服务
            services.AddScoped<IHbtQuartzService, HbtQuartzService>();  // 定时任务服务
            services.AddScoped<IHbtQuartzLogService, HbtQuartzLogService>();    // 任务日志服务

            // 邮件相关服务
            services.AddScoped<IHbtMailService, HbtMailService>();              // 邮件服务
            services.AddScoped<IHbtMailTmplService, HbtMailTmplService>();      // 邮件模板服务

            // 文件服务
            services.AddScoped<IHbtFileService, HbtFileService>();              // 文件服务

            // 通知服务
            services.AddScoped<IHbtNoticeService, HbtNoticeService>();          // 通知服务

            return services;
        }

        /// <summary>
        /// 添加基础设施服务
        /// </summary>
        /// <remarks>
        /// 注册基础设施层相关的所有服务，包括：
        /// 1. 数据库上下文 - 数据访问
        /// 2. 认证服务 - JWT认证
        /// 3. 缓存服务 - Redis和内存缓存
        /// 4. 消息服务 - SignalR实时通信
        /// 5. 日志服务 - 系统日志记录
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 添加 HttpClient 工厂
            services.AddHttpClient();

            // 配置数据库 - 只在这里注册一次
            var connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentException("数据库连接字符串不能为空");

            var dbConfig = configuration.GetSection("Database").Get<HbtDbOptions>()
                ?? throw new ArgumentException("数据库配置节点不能为空");

            // 注册数据库配置
            services.Configure<ConnectionConfig>(options =>
            {
                options.ConnectionString = connectionString;
                options.DbType = dbConfig.DbType;
                options.IsAutoCloseConnection = true;
                options.InitKeyType = InitKeyType.Attribute;
            });

            // 配置 SqlSugar
            services.AddSingleton<SqlSugarScope>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionConfig>>();
                var scope = new SqlSugarScope(options.Value);

                // 添加 SQL 执行日志
                var logger = sp.GetRequiredService<ILogger<SqlSugarScope>>();
                scope.Aop.OnLogExecuting = (sql, parameters) =>
                {
                    logger.LogInformation("SQL执行: {SQL}", sql);
                    if (parameters?.Any() == true)
                    {
                        logger.LogInformation("参数: {@Parameters}", parameters.ToDictionary(p => p.ParameterName, p => p.Value));
                    }
                };

                // 添加 SQL 执行结果日志
                scope.Aop.OnLogExecuted = (sql, parameters) =>
                {
                    logger.LogInformation("SQL执行完成: {SQL}", sql);
                    if (parameters?.Any() == true)
                    {
                        logger.LogInformation("参数: {@Parameters}", parameters.ToDictionary(p => p.ParameterName, p => p.Value));
                    }
                };

                return scope;
            });
            services.AddSingleton<ISqlSugarClient>(sp => sp.GetRequiredService<SqlSugarScope>());

            // 数据库上下文 - 只注册一次
            services.AddScoped<HbtDbContext>();
            services.AddScoped<IHbtDbContext, HbtDbContext>();

            // 其他基础设施服务
            services.AddScoped<IHbtCurrentUser, HbtCurrentUser>();
            services.AddScoped<IHbtCurrentTenant, HbtCurrentTenant>();

            return services;
        }
    }
}