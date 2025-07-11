//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtParallelBranchService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流并行分支服务实现 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common;
using Lean.Hbt.Common.Extensions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using System.IO;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流并行分支服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流并行分支的增删改查服务
    /// 2. 支持并行分支的分页查询
    /// 3. 实现并行分支的状态管理
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtParallelBranchService : HbtBaseService, IHbtParallelBranchService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtParallelBranch> ParallelBranchRepository => _repositoryFactory.GetWorkflowRepository<HbtParallelBranch>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtParallelBranchService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取并行分支分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页后的并行分支列表</returns>
        public async Task<HbtPagedResult<HbtParallelBranchDto>> GetListAsync(HbtParallelBranchQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await ParallelBranchRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            var parallelBranches = result.Rows.Adapt<List<HbtParallelBranchDto>>();

            return new HbtPagedResult<HbtParallelBranchDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = parallelBranches
            };
        }

        /// <summary>
        /// 根据ID获取并行分支详情
        /// </summary>
        /// <param name="id">并行分支ID</param>
        /// <returns>并行分支详情DTO</returns>
        /// <exception cref="HbtException">当并行分支不存在时抛出异常</exception>
        public async Task<HbtParallelBranchDto> GetByIdAsync(long id)
        {
            var parallelBranch = await ParallelBranchRepository.GetByIdAsync(id);
            if (parallelBranch == null)
                throw new HbtException(L("ParallelBranch.NotFound"));

            return parallelBranch.Adapt<HbtParallelBranchDto>();
        }

        /// <summary>
        /// 创建新的并行分支
        /// </summary>
        /// <param name="input">并行分支创建DTO</param>
        /// <returns>新创建的并行分支ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当并行分支创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtParallelBranchCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var parallelBranch = input.Adapt<HbtParallelBranch>();

            var result = await ParallelBranchRepository.CreateAsync(parallelBranch);
            if (result <= 0)
                throw new HbtException(L("ParallelBranch.Create.Failed"));

            _logger.Info(L("ParallelBranch.Created.Success", parallelBranch.Id));
            return parallelBranch.Id;
        }

        /// <summary>
        /// 更新并行分支信息
        /// </summary>
        /// <param name="input">并行分支更新DTO</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当并行分支不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtParallelBranchUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var parallelBranch = await ParallelBranchRepository.GetByIdAsync(input.ParallelBranchId);
            if (parallelBranch == null)
                throw new HbtException(L("ParallelBranch.NotFound"));

            input.Adapt(parallelBranch);
            var result = await ParallelBranchRepository.UpdateAsync(parallelBranch);
            if (result <= 0)
                throw new HbtException(L("ParallelBranch.Update.Failed"));

            _logger.Info(L("ParallelBranch.Updated.Success", parallelBranch.Id));
            return true;
        }

        /// <summary>
        /// 删除指定并行分支
        /// </summary>
        /// <param name="id">要删除的并行分支ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当并行分支不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var parallelBranch = await ParallelBranchRepository.GetByIdAsync(id);
            if (parallelBranch == null)
                throw new HbtException(L("ParallelBranch.NotFound"));

            var result = await ParallelBranchRepository.DeleteAsync(parallelBranch);
            if (result <= 0)
                throw new HbtException(L("ParallelBranch.Delete.Failed"));

            _logger.Info(L("ParallelBranch.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除并行分支
        /// </summary>
        /// <param name="ids">要删除的并行分支ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtParallelBranch>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await ParallelBranchRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("ParallelBranch.BatchDelete.Failed"));

            _logger.Info(L("ParallelBranch.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtParallelBranch, bool>> QueryExpression(HbtParallelBranchQueryDto query)
        {
            var exp = Expressionable.Create<HbtParallelBranch>();

            if (query?.InstanceId.HasValue == true)
                exp = exp.And(x => x.InstanceId == query.InstanceId.Value);

            if (query?.ParallelNodeId.HasValue == true)
                exp = exp.And(x => x.ParallelNodeId == query.ParallelNodeId.Value);

            if (query?.BranchTransitionId.HasValue == true)
                exp = exp.And(x => x.BranchTransitionId == query.BranchTransitionId.Value);

            if (query?.IsCompleted.HasValue == true)
                exp = exp.And(x => x.IsCompleted == query.IsCompleted.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 导入并行分支数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importParallelBranches = await HbtExcelHelper.ImportAsync<HbtParallelBranchImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importParallelBranches)
                {
                    try
                    {
                        var parallelBranch = item.Adapt<HbtParallelBranch>();
                        parallelBranch.CreateTime = DateTime.Now;
                        parallelBranch.CreateBy = _currentUser.UserName;

                        var result = await ParallelBranchRepository.CreateAsync(parallelBranch);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入并行分支失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入并行分支数据失败", ex);
                throw new HbtException("导入并行分支数据失败");
            }
        }

        /// <summary>
        /// 导出并行分支数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtParallelBranchQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var parallelBranches = await ParallelBranchRepository.GetListAsync(exp);
            var exportList = parallelBranches.Adapt<List<HbtParallelBranchExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "并行分支数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtParallelBranchImportDto>(sheetName);
        }
    }
} 