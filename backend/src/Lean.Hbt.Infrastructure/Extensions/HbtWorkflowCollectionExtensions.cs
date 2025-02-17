//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流服务扩展方法
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Lean.Hbt.Application.Services.Workflow.Engine.Cache;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Executors;
using Lean.Hbt.Application.Services.Workflow.Engine.Expressions;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;
using Lean.Hbt.Application.Services.Workflow;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 工作流服务扩展方法
    /// </summary>
    public static class HbtWorkflowCollectionExtensions
    {
        /// <summary>
        /// 添加工作流缓存服务
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的缓存服务，包括：
        /// 1. 内存缓存服务
        /// 2. 工作流缓存服务
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddWorkflowCacheServices(this IServiceCollection services)
        {
            // 注册内存缓存
            services.AddMemoryCache();

            // 注册工作流缓存服务
            services.AddScoped<IWorkflowCache, WorkflowMemoryCache>();

            return services;
        }

        /// <summary>
        /// 添加工作流服务
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的所有服务,包括:
        /// 1. 工作流引擎
        /// 2. 工作流定义服务
        /// 3. 工作流实例服务
        /// 4. 工作流节点服务
        /// 5. 工作流任务服务
        /// 6. 工作流历史服务
        /// 7. 工作流活动服务
        /// 8. 各类节点执行器
        /// 9. 表达式引擎
        /// 10. 审批人解析器
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddWorkflowServices(this IServiceCollection services)
        {
            // 添加工作流缓存服务
            services.AddWorkflowCacheServices();

            // 注册工作流引擎,负责工作流的整体调度和执行
            services.AddScoped<IHbtWorkflowEngine, HbtWorkflowEngine>();

            // 注册工作流核心服务
            services.AddScoped<IHbtWorkflowDefinitionService, HbtWorkflowDefinitionService>(); // 工作流定义管理
            services.AddScoped<IHbtWorkflowInstanceService, HbtWorkflowInstanceService>();     // 工作流实例管理
            services.AddScoped<IHbtWorkflowNodeService, HbtWorkflowNodeService>();            // 工作流节点管理
            services.AddScoped<IHbtWorkflowTransitionService, HbtWorkflowTransitionService>(); // 工作流流转管理
            services.AddScoped<IHbtWorkflowTaskService, HbtWorkflowTaskService>();            // 工作流任务管理
            services.AddScoped<IHbtWorkflowHistoryService, HbtWorkflowHistoryService>();      // 工作流历史记录
            services.AddScoped<IHbtWorkflowActivityService, HbtWorkflowActivityService>();     // 工作流活动管理

            // 注册工作流节点执行器,处理不同类型节点的具体执行逻辑
            services.AddScoped<IWorkflowNodeExecutor, StartNodeExecutor>();      // 开始节点执行器
            services.AddScoped<IWorkflowNodeExecutor, EndNodeExecutor>();        // 结束节点执行器
            services.AddScoped<IWorkflowNodeExecutor, ApprovalNodeExecutor>();   // 审批节点执行器
            services.AddScoped<IWorkflowNodeExecutor, BranchNodeExecutor>();     // 分支节点执行器
            services.AddScoped<IWorkflowNodeExecutor, ParallelNodeExecutor>();   // 并行节点执行器
            services.AddScoped<IWorkflowNodeExecutor, JoinNodeExecutor>();       // 合并节点执行器

            // 注册工作流表达式引擎,用于解析和执行条件表达式
            services.AddScoped<IWorkflowExpressionEngine, WorkflowExpressionEngine>();

            // 注册工作流审批人解析器,用于动态解析审批人
            services.AddScoped<IWorkflowApproverResolver, WorkflowApproverResolver>();

            return services;
        }
    }
} 