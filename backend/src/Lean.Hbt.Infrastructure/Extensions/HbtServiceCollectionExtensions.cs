//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:30
// 版本号 : V.0.0.1
// 描述    : 服务集合扩展
//===================================================================

using System.Text;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Infrastructure.Authentication;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Data.Seeds;
using Lean.Hbt.Infrastructure.Logging;
using Lean.Hbt.Infrastructure.Security;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Infrastructure.SignalR;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Application.Services.Identity;
using Lean.Hbt.Application.Services.Audit;
using Lean.Hbt.Application.Services.RealTime;
using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Executors;
using Lean.Hbt.Application.Services.Workflow.Engine.Expressions;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using Lean.Hbt.Infrastructure.Repositories;
using Lean.Hbt.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DbType = SqlSugar.DbType;
using StackExchange.Redis;

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
            
            // 会话管理服务
            services.AddScoped<IHbtIdentitySessionManager, HbtSessionManager>();
            services.AddScoped<IHbtSignalRSessionManager, HbtSessionManager>();
            
            // 验证服务
            services.AddScoped<IHbtCaptchaService, HbtCaptchaService>();
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
            
            // 缓存服务 - 所有缓存相关服务使用 Scoped 生命周期
            services.AddScoped<IHbtMemoryCache, HbtMemoryCache>();
            services.AddScoped<HbtCacheConfigManager>();
            // Redis缓存服务在 AddInfrastructure 中根据配置注册
            
            return services;
        }

        /// <summary>
        /// 添加日志服务
        /// </summary>
        private static IServiceCollection AddLogServices(this IServiceCollection services)
        {
            // 添加日志记录器
            services.AddScoped<IHbtLogger, HbtNLogger>();

            // 添加日志服务
            services.AddScoped<IHbtLogCleanupService, HbtLogCleanupService>();   // 日志清理服务
            services.AddScoped<IHbtLogArchiveService, HbtLogArchiveService>();   // 日志归档服务
            
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
            services.AddScoped<IHbtLoginService, HbtLoginService>();         // 登录服务
            services.AddScoped<IHbtLoginExtendService, HbtLoginExtendService>(); // 登录扩展服务
            services.AddScoped<IHbtDeviceExtendService, HbtDeviceExtendService>(); // 设备扩展服务
            services.AddScoped<IHbtJwtHandler, HbtJwtHandler>();            // JWT令牌处理
            
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
            services.AddScoped<IHbtSysConfigService, HbtSysConfigService>();     // 系统配置
            
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
            services.AddScoped<IHbtAuditsLog, HbtAuditsLog>();                  // 审计日志记录器
            
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
            
            // 配置数据库
            var connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentException("数据库连接字符串不能为空，请检查配置文件中的 ConnectionStrings:Default");

            var connectionConfig = new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,  // 根据连接字符串判断是SQL Server
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            };

            // 配置 SqlSugar
            services.AddSingleton<SqlSugarScope>(sp =>
            {
                var sqlSugar = new SqlSugarScope(connectionConfig);
                
                // 配置Aop
                sqlSugar.Aop.OnLogExecuting = (sql, parameters) =>
                {
                    using (var scope = sp.CreateScope())
                    {
                        var logger = scope.ServiceProvider.GetService<IHbtLogger>();
                        if (logger != null)
                        {
                            logger.Debug($"SQL: {sql}");
                            if (parameters?.Any() == true)
                            {
                                logger.Debug($"Parameters: {string.Join(", ", parameters.Select(p => $"{p.ParameterName}={p.Value}"))}");
                            }
                        }
                    }
                };

                sqlSugar.Aop.OnError = (ex) =>
                {
                    using (var scope = sp.CreateScope())
                    {
                        var logger = scope.ServiceProvider.GetService<IHbtLogger>();
                        logger?.Error($"SQL执行错误: {ex.Message}", ex);
                    }
                };

                return sqlSugar;
            });

            // 同时注册 ISqlSugarClient 接口
            services.AddSingleton<ISqlSugarClient>(sp => sp.GetRequiredService<SqlSugarScope>());

            services.Configure<ConnectionConfig>(options => 
            {
                options.ConnectionString = connectionConfig.ConnectionString;
                options.DbType = connectionConfig.DbType;
                options.IsAutoCloseConnection = connectionConfig.IsAutoCloseConnection;
                options.InitKeyType = connectionConfig.InitKeyType;
            });

            services.AddScoped<HbtDbContext>();
            services.AddScoped<IHbtDbContext, HbtDbContext>();
            services.AddScoped<HbtDbSeed>();

            // 配置 JWT 认证
            var jwtSettings = configuration.GetSection("Jwt").Get<HbtJwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });

            // 配置 SignalR
            services.AddSignalR();
            services.AddScoped<IHbtSignalRUserService, HbtSignalRUserService>();
            services.AddScoped<IHbtSignalRMessageNotifier, HbtSignalRMessageNotifier>();

            // 配置缓存
            var cacheSettings = configuration.GetSection("Cache").Get<HbtCacheOptions>();
            if (cacheSettings?.Provider == CacheProviderType.Redis && 
                cacheSettings?.Redis?.Enabled == true && 
                !string.IsNullOrEmpty(cacheSettings.Redis.ConnectionString))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheSettings.Redis.ConnectionString;
                    options.InstanceName = cacheSettings.Redis.InstanceName;
                });

                // 注册 ConnectionMultiplexer
                services.AddSingleton<ConnectionMultiplexer>(sp =>
                {
                    return ConnectionMultiplexer.Connect(cacheSettings.Redis.ConnectionString);
                });

                services.Configure<HbtCacheOptions>(configuration.GetSection("Cache"));

                // 注册 Redis 缓存服务
                services.AddScoped<IHbtRedisCache, HbtRedisCache>();
            }
            else
            {
                // 如果未配置Redis或使用内存缓存，使用内存缓存作为备选
                services.AddDistributedMemoryCache();
                
                // 注册空的Redis实现
                services.AddScoped<IHbtRedisCache, HbtNullRedisCache>();
            }

            return services;
        }
    }
}