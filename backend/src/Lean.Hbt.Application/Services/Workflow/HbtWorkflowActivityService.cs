#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowActivityService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow.Engine;
using Lean.Hbt.Domain.Repositories;
using Mapster;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SqlSugar;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Application.Services;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流活动服务实现类
    /// </summary>
    /// <remarks>
    /// 该服务提供工作流活动的增删改查功能，包括：
    /// 1. 获取指定工作流定义下的所有活动
    /// 2. 获取单个活动详情
    /// 3. 创建新的工作流活动
    /// 4. 更新现有工作流活动
    /// 5. 删除工作流活动
    /// </remarks>
    public class HbtWorkflowActivityService : HbtBaseService, IHbtWorkflowActivityService
    {
        /// <summary>
        /// 工作流活动仓储接口
        /// </summary>
        private readonly IHbtRepository<HbtWorkflowActivity> _activityRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="activityRepository">工作流活动仓储接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowActivityService(
            IHbtLogger logger,
            IHbtRepository<HbtWorkflowActivity> activityRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _activityRepository = activityRepository;
        }

        /// <summary>
        /// 获取指定工作流定义下的所有活动列表
        /// </summary>
        /// <param name="workflowDefinitionId">工作流定义ID</param>
        /// <returns>工作流活动DTO列表</returns>
        /// <remarks>
        /// 该方法使用分页查询获取所有活动，但设置了较大的页面大小以获取全部数据
        /// 查询结果按活动ID升序排序
        /// </remarks>
        public async Task<List<HbtWorkflowActivityDto>> GetListAsync(long workflowDefinitionId)
        {
            // 创建查询表达式
            var exp = Expressionable.Create<HbtWorkflowActivity>();
            exp = exp.And(x => x.WorkflowDefinitionId == workflowDefinitionId);

            // 使用分页查询获取数据，设置较大的页面大小以获取全部数据
            var result = await _activityRepository.GetPagedListAsync(exp.ToExpression(), 1, int.MaxValue, x => x.Id, OrderByType.Asc);
            return result.Rows.Adapt<List<HbtWorkflowActivityDto>>();
        }

        /// <summary>
        /// 根据ID获取工作流活动详情
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <returns>工作流活动DTO</returns>
        /// <remarks>
        /// 通过活动ID查询单个活动的详细信息
        /// 使用Mapster将实体映射为DTO返回
        /// </remarks>
        public async Task<HbtWorkflowActivityDto> GetByIdAsync(long id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            return activity.Adapt<HbtWorkflowActivityDto>();
        }

        /// <summary>
        /// 创建新的工作流活动
        /// </summary>
        /// <param name="input">工作流活动创建DTO</param>
        /// <returns>新创建的活动ID</returns>
        /// <remarks>
        /// 将输入DTO转换为实体对象
        /// 调用仓储接口创建新活动
        /// 返回新创建活动的ID
        /// </remarks>
        public async Task<long> CreateAsync(HbtWorkflowActivityDto input)
        {
            var activity = input.Adapt<HbtWorkflowActivity>();
            await _activityRepository.CreateAsync(activity);
            return activity.Id;
        }

        /// <summary>
        /// 更新工作流活动
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="input">工作流活动更新DTO</param>
        /// <remarks>
        /// 先获取现有活动
        /// 使用输入DTO更新活动信息
        /// 调用仓储接口保存更改
        /// </remarks>
        public async Task UpdateAsync(long id, HbtWorkflowActivityDto input)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            input.Adapt(activity);
            await _activityRepository.UpdateAsync(activity);
        }

        /// <summary>
        /// 删除工作流活动
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <remarks>
        /// 根据ID删除指定的工作流活动
        /// </remarks>
        public async Task DeleteAsync(long id)
        {
            await _activityRepository.DeleteAsync(id);
        }
    }
} 
