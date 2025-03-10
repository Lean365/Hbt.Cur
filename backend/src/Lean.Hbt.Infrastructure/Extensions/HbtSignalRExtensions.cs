//===================================================================
// 项目名 : Lean.Hbt.Infrastructure
// 文件名 : HbtSignalRExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : SignalR扩展类
//===================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Lean.Hbt.Infrastructure.SignalR;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// SignalR扩展类
    /// </summary>
    public static class HbtSignalRExtensions
    {
        /// <summary>
        /// 添加SignalR服务
        /// </summary>
        public static IServiceCollection AddHbtSignalR(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 102400000;
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