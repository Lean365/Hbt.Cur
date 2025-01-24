#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : WorkflowServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流服务扩展方法
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Lean.Hbt.Application.Services.Workflow.Engine.Cache;

namespace Lean.Hbt.Application.Extensions
{
    /// <summary>
    /// 工作流服务扩展方法
    /// </summary>
    public static class WorkflowServiceCollectionExtensions
    {
        /// <summary>
        /// 添加工作流服务
        /// </summary>
        public static IServiceCollection AddWorkflowServices(this IServiceCollection services)
        {
            // 注册内存缓存
            services.AddMemoryCache();

            // 注册工作流缓存服务
            services.AddScoped<IWorkflowCache, WorkflowMemoryCache>();

            return services;
        }
    }
} 