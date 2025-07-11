#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtActivityService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流活动服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Repositories;
using Mapster;
using System.Threading.Tasks;
using SqlSugar;
using Lean.Hbt.Application.Services;
using System.Linq;
using System;

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
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtActivityService : HbtBaseService, IHbtActivityService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtActivity> ActivityRepository => _repositoryFactory.GetWorkflowRepository<HbtActivity>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtActivityService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取指定工作流定义下的所有活动列表
        /// </summary>
        /// <param name="DefinitionId">工作流定义ID</param>
        /// <returns>工作流活动DTO列表</returns>
        /// <remarks>
        /// 该方法使用分页查询获取所有活动，但设置了较大的页面大小以获取全部数据
        /// 查询结果按活动ID升序排序
        /// </remarks>
        public async Task<List<HbtActivityDto>> GetListAsync(long DefinitionId)
        {
            // 创建查询表达式
            var exp = Expressionable.Create<HbtActivity>();
            exp = exp.And(x => x.DefinitionId == DefinitionId);

            // 使用分页查询获取数据，设置较大的页面大小以获取全部数据
            var result = await ActivityRepository.GetPagedListAsync(exp.ToExpression(), 1, int.MaxValue, x => x.Id, OrderByType.Asc);
            return result.Rows.Adapt<List<HbtActivityDto>>();
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
        public async Task<HbtActivityDto> GetByIdAsync(long id)
        {
            var activity = await ActivityRepository.GetByIdAsync(id);
            return activity.Adapt<HbtActivityDto>();
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
        public async Task<long> CreateAsync(HbtActivityDto input)
        {
            var activity = input.Adapt<HbtActivity>();
            await ActivityRepository.CreateAsync(activity);
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
        public async Task UpdateAsync(long id, HbtActivityDto input)
        {
            var activity = await ActivityRepository.GetByIdAsync(id);
            input.Adapt(activity);
            await ActivityRepository.UpdateAsync(activity);
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
            await ActivityRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtActivityImportDto>(sheetName);
        }

        /// <summary>
        /// 导入工作流活动数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var activities = await HbtExcelHelper.ImportAsync<HbtActivityImportDto>(fileStream, sheetName);
                if (!activities.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var activity in activities)
                {
                    try
                    {
                        var entity = activity.Adapt<HbtActivity>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;

                        var result = await ActivityRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流活动失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流活动数据失败", ex);
                throw new HbtException("导入工作流活动数据失败");
            }
        }

        /// <summary>
        /// 导出工作流活动数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtActivityQueryDto query, string sheetName = "Sheet1")
        {
            // 创建查询表达式
            var exp = Expressionable.Create<HbtActivity>();
            if (query?.DefinitionId > 0)
                exp = exp.And(x => x.DefinitionId == query.DefinitionId);
            if (!string.IsNullOrEmpty(query?.ActivityName))
                exp = exp.And(x => x.Name.Contains(query.ActivityName));
            if (query?.ActivityType.HasValue == true)
                exp = exp.And(x => x.ActivityType == query.ActivityType.Value);

            // 获取所有数据用于导出
            var activities = await ActivityRepository.GetListAsync(exp.ToExpression());
            var dtos = activities.Adapt<List<HbtActivityExportDto>>();

            return await HbtExcelHelper.ExportAsync(dtos, sheetName, "工作流活动数据");
        }
    }
} 
