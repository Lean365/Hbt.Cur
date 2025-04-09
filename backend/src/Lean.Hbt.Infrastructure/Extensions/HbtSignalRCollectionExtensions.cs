//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR扩展类
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Infrastructure.SignalR;
using Lean.Hbt.Infrastructure.SignalR.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Lean.Hbt.Common.Options;
using Microsoft.AspNetCore.Builder;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// SignalR扩展类
    /// </summary>
    public static class HbtSignalRCollectionExtensions
    {
        /// <summary>
        /// 添加SignalR服务
        /// </summary>
        public static IServiceCollection AddHbtSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            // 配置SignalR日志
            services.AddLogging(logging =>
            {
                logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);
                logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);
            });

            // 配置SignalR
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 1024 * 1024 * 10; // 10MB
                options.HandshakeTimeout = TimeSpan.FromSeconds(15);
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            })
            .AddHubOptions<HbtSignalRHub>(options =>
            {
                // Hub 特定的配置可以放在这里
            })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.PayloadSerializerOptions.WriteIndented = false;
            });

            // 注册 SignalR 服务
            services.AddScoped<IHbtSignalRUserService, HbtSignalRUserService>();
            services.AddScoped<IHbtSignalRHub, HbtSignalRHub>();
            services.AddScoped<IHbtSignalRClient>(sp =>
            {
                var hubContext = sp.GetRequiredService<IHubContext<HbtSignalRHub, IHbtSignalRClient>>();
                return hubContext.Clients.All;
            });

            // 配置 SignalR 缓存选项
            services.Configure<HbtSignalRCacheOptions>(
                configuration.GetSection("SignalRCache"));

            // 配置 JWT 认证中的 SignalR 相关事件
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/signalr/hbthub"))
                        {
                            var accessToken = context.Request.Query["access_token"].ToString();
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<HbtSignalRHub>>();
                                logger.LogInformation("SignalR 连接认证: Path={Path}, Token={Token}...", 
                                    path, 
                                    accessToken.Substring(0, Math.Min(20, accessToken.Length)));
                            }
                            else
                            {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<HbtSignalRHub>>();
                                logger.LogWarning("SignalR 连接认证失败: 未提供 access_token 参数");
                            }
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<HbtSignalRHub>>();
                        logger.LogError(context.Exception, "JWT认证失败: {Message}", context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<HbtSignalRHub>>();
                        logger.LogInformation("JWT认证成功: UserId={UserId}, UserName={UserName}", 
                            context.Principal?.FindFirst("uid")?.Value,
                            context.Principal?.FindFirst("unm")?.Value);
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        /// <summary>
        /// 使用SignalR
        /// </summary>
        public static IApplicationBuilder UseHbtSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HbtSignalRHub>("/signalr/hbthub");
            });

            return app;
        }
    }
}
