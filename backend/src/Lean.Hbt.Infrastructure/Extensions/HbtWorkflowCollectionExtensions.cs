//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流服务扩展方法
//===================================================================

using Lean.Hbt.Application.Services.Workflow;
using Lean.Hbt.Application.Services.Workflow.Engine;
using Lean.Hbt.Application.Services.Workflow.Engine.Cache;
using Lean.Hbt.Application.Services.Workflow.Engine.Executors;
using Lean.Hbt.Application.Services.Workflow.Engine.Expressions;
using Lean.Hbt.Application.Services.Workflow.Engine.Resolvers;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 工作流服务扩展方法
    /// </summary>
    public static class HbtCollectionExtensions
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
            // 注册工作流缓存服务
            services.AddScoped<IHbtWorkflowCache, HbtWorkflowMemoryCache>();
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
            services.AddScoped<IHbtDefinitionService, HbtDefinitionService>(); // 工作流定义管理
            services.AddScoped<IHbtInstanceService, HbtInstanceService>();     // 工作流实例管理
            services.AddScoped<IHbtNodeService, HbtNodeService>();            // 工作流节点管理
            services.AddScoped<IHbtTransitionService, HbtTransitionService>(); // 工作流流转管理
            services.AddScoped<IHbtProcessTaskService, HbtProcessTaskService>();// 工作流任务管理
            services.AddScoped<IHbtHistoryService, HbtHistoryService>();      // 工作流历史记录
            services.AddScoped<IHbtActivityService, HbtActivityService>();     // 工作流活动管理
            services.AddScoped<IHbtVariableService, HbtVariableService>();     // 工作流变量管理
            services.AddScoped<IHbtFormService, HbtFormService>();            // 工作流表单管理

            // 注册工作流节点执行器,处理不同类型节点的具体执行逻辑
            services.AddScoped<IHbtNodeExecutor, HbtStartNodeExecutor>();      // 开始节点执行器
            services.AddScoped<IHbtNodeExecutor, HbtEndNodeExecutor>();        // 结束节点执行器
            services.AddScoped<IHbtNodeExecutor, HbtApprovalNodeExecutor>();   // 审批节点执行器
            services.AddScoped<IHbtNodeExecutor, HbtBranchNodeExecutor>();     // 分支节点执行器
            services.AddScoped<IHbtNodeExecutor, HbtParallelNodeExecutor>();   // 并行节点执行器
            services.AddScoped<IHbtNodeExecutor, HbtJoinNodeExecutor>();       // 合并节点执行器

            // 注册工作流表达式引擎,用于解析和执行条件表达式
            services.AddScoped<IHbtExpressionEngine, HbtExpressionEngine>();

            // 注册工作流审批人解析器,用于动态解析审批人
            services.AddScoped<IHbtApproverResolver, HbtApproverResolver>();

            return services;
        }
    }
}