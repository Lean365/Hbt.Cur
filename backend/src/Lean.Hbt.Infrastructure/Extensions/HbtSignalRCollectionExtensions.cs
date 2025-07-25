//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
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
using Microsoft.AspNetCore.Http.Connections;

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
            // 获取SignalR配置
            var signalRConfig = configuration.GetSection("SignalR").Get<SignalRConfig>();

            // 配置SignalR日志
            services.AddLogging(logging =>
            {
                logging.AddFilter("Microsoft.AspNetCore.SignalR", 
                    signalRConfig.EnableDetailedErrors ? LogLevel.Debug : LogLevel.Information);
                logging.AddFilter("Microsoft.AspNetCore.Http.Connections", 
                    signalRConfig.EnableDetailedErrors ? LogLevel.Debug : LogLevel.Information);
            });

            // 配置SignalR
            services.AddSignalR(options =>
            {
                // 基本设置
                options.EnableDetailedErrors = signalRConfig.EnableDetailedErrors;
                options.MaximumReceiveMessageSize = signalRConfig.MaximumReceiveMessageSize;
                options.HandshakeTimeout = TimeSpan.FromSeconds(signalRConfig.HandshakeTimeout);
                options.KeepAliveInterval = TimeSpan.FromSeconds(signalRConfig.KeepAliveInterval);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(signalRConfig.ClientTimeoutInterval);
                options.StreamBufferCapacity = signalRConfig.StreamBufferCapacity;
            })
            .AddHubOptions<HbtSignalRHub>(options =>
            {
                options.EnableDetailedErrors = signalRConfig.EnableDetailedErrors;
                options.MaximumReceiveMessageSize = signalRConfig.MaximumReceiveMessageSize;
                options.HandshakeTimeout = TimeSpan.FromSeconds(signalRConfig.HandshakeTimeout);
                options.KeepAliveInterval = TimeSpan.FromSeconds(signalRConfig.KeepAliveInterval);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(signalRConfig.ClientTimeoutInterval);
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
                            // 从请求头中获取 token
                            var accessToken = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
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
                                logger.LogWarning("SignalR 连接认证失败: 未提供 Authorization 头");
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
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            var signalRConfig = configuration.GetSection("SignalR").Get<SignalRConfig>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HbtSignalRHub>("/signalr/hbthub", options =>
                {
                    options.CloseOnAuthenticationExpiration = signalRConfig.Authentication.RequireAuthentication;
                    options.ApplicationMaxBufferSize = signalRConfig.MaximumReceiveMessageSize;
                    options.TransportMaxBufferSize = signalRConfig.MaximumReceiveMessageSize;
                });
            });

            return app;
        }
    }
}
