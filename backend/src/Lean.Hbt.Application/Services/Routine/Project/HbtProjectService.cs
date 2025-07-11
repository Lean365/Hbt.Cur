//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 项目服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Application.Services.Routine.Project
{
    /// <summary>
    /// 项目服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtProjectService : HbtBaseService, IHbtProjectService
    {
        private readonly IHbtRepository<HbtProject> _projectRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="projectRepository">项目仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtProjectService(
            IHbtLogger logger,
            IHbtRepository<HbtProject> projectRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtProjectDto>> GetListAsync(HbtProjectQueryDto query)
        {
            _logger.Info("开始查询项目列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await _projectRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new HbtPagedResult<HbtProjectDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtProjectDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>返回项目详情</returns>
        public async Task<HbtProjectDto> GetByIdAsync(long projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new HbtException(L("Project.NotFound", projectId));

            return project.Adapt<HbtProjectDto>();
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回项目ID</returns>
        public async Task<long> CreateAsync(HbtProjectCreateDto input)
        {
            var project = input.Adapt<HbtProject>();
            var result = await _projectRepository.CreateAsync(project);
            return result;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtProjectUpdateDto input)
        {
            var project = await _projectRepository.GetByIdAsync(input.ProjectId);
            if (project == null)
                throw new HbtException(L("Project.NotFound", input.ProjectId));

            input.Adapt(project);
            var result = await _projectRepository.UpdateAsync(project);
            return result > 0;
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new HbtException(L("Project.NotFound", projectId));

            var result = await _projectRepository.DeleteAsync(project);
            return result > 0;
        }

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="projectIds">项目ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] projectIds)
        {
            if (projectIds == null || projectIds.Length == 0)
                throw new HbtException(L("Project.SelectToDelete"));

            var projects = await _projectRepository.GetListAsync(x => projectIds.Contains(x.Id));
            if (!projects.Any())
                throw new HbtException(L("Project.NotFound"));

            var result = await _projectRepository.DeleteAsync(projects);
            return result > 0;
        }

        /// <summary>
        /// 导入项目数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "项目信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtProjectImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var project = dto.Adapt<HbtProject>();
                    await _projectRepository.CreateAsync(project);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Project.ImportFailed", dto.ProjectName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtProjectQueryDto query, string sheetName = "项目信息")
        {
            var predicate = QueryExpression(query);

            var projects = await _projectRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = projects.Adapt<List<HbtProjectExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "项目信息")
        {
            var template = new List<HbtProjectTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtProject, bool>> QueryExpression(HbtProjectQueryDto query)
        {
            var exp = Expressionable.Create<HbtProject>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ProjectCode))
                    exp = exp.And(x => x.ProjectCode.Contains(query.ProjectCode));

                if (!string.IsNullOrEmpty(query.ProjectName))
                    exp = exp.And(x => x.ProjectName.Contains(query.ProjectName));

                if (query.ProjectType.HasValue)
                    exp = exp.And(x => x.ProjectType == query.ProjectType.Value);

                if (!string.IsNullOrEmpty(query.ProjectCategory))
                    exp = exp.And(x => x.ProjectCategory.Contains(query.ProjectCategory));

                if (query.ProjectStatus.HasValue)
                    exp = exp.And(x => x.ProjectStatus == query.ProjectStatus.Value);

                if (query.ProjectPhase.HasValue)
                    exp = exp.And(x => x.ProjectPhase == query.ProjectPhase.Value);

                if (!string.IsNullOrEmpty(query.ProjectManager))
                    exp = exp.And(x => x.ProjectManager.Contains(query.ProjectManager));

                if (!string.IsNullOrEmpty(query.CustomerName))
                    exp = exp.And(x => x.CustomerName.Contains(query.CustomerName));

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.CreateTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
} 