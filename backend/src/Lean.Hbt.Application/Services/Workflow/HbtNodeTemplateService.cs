//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeTemplateService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 节点模板服务实现 - 使用仓储工厂模式
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
    /// 节点模板服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供节点模板的增删改查服务
    /// 2. 支持节点模板的分页查询
    /// 3. 实现节点模板的状态管理
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtNodeTemplateService : HbtBaseService, IHbtNodeTemplateService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtNodeTemplate> NodeTemplateRepository => _repositoryFactory.GetWorkflowRepository<HbtNodeTemplate>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNodeTemplateService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取节点模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页后的节点模板列表</returns>
        public async Task<HbtPagedResult<HbtNodeTemplateDto>> GetListAsync(HbtNodeTemplateQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await NodeTemplateRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            var nodeTemplates = result.Rows.Adapt<List<HbtNodeTemplateDto>>();

            return new HbtPagedResult<HbtNodeTemplateDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = nodeTemplates
            };
        }

        /// <summary>
        /// 根据ID获取节点模板详情
        /// </summary>
        /// <param name="id">节点模板ID</param>
        /// <returns>节点模板详情DTO</returns>
        /// <exception cref="HbtException">当节点模板不存在时抛出异常</exception>
        public async Task<HbtNodeTemplateDto> GetByIdAsync(long id)
        {
            var nodeTemplate = await NodeTemplateRepository.GetByIdAsync(id);
            if (nodeTemplate == null)
                throw new HbtException(L("NodeTemplate.NotFound"));

            return nodeTemplate.Adapt<HbtNodeTemplateDto>();
        }

        /// <summary>
        /// 创建新的节点模板
        /// </summary>
        /// <param name="input">节点模板创建DTO</param>
        /// <returns>新创建的节点模板ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点模板创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtNodeTemplateCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var nodeTemplate = input.Adapt<HbtNodeTemplate>();

            var result = await NodeTemplateRepository.CreateAsync(nodeTemplate);
            if (result <= 0)
                throw new HbtException(L("NodeTemplate.Create.Failed"));

            _logger.Info(L("NodeTemplate.Created.Success", nodeTemplate.Id));
            return nodeTemplate.Id;
        }

        /// <summary>
        /// 更新节点模板信息
        /// </summary>
        /// <param name="input">节点模板更新DTO</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点模板不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtNodeTemplateUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var nodeTemplate = await NodeTemplateRepository.GetByIdAsync(input.NodeTemplateId);
            if (nodeTemplate == null)
                throw new HbtException(L("NodeTemplate.NotFound"));

            input.Adapt(nodeTemplate);
            var result = await NodeTemplateRepository.UpdateAsync(nodeTemplate);
            if (result <= 0)
                throw new HbtException(L("NodeTemplate.Update.Failed"));

            _logger.Info(L("NodeTemplate.Updated.Success", nodeTemplate.Id));
            return true;
        }

        /// <summary>
        /// 删除指定节点模板
        /// </summary>
        /// <param name="id">要删除的节点模板ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当节点模板不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var nodeTemplate = await NodeTemplateRepository.GetByIdAsync(id);
            if (nodeTemplate == null)
                throw new HbtException(L("NodeTemplate.NotFound"));

            var result = await NodeTemplateRepository.DeleteAsync(nodeTemplate);
            if (result <= 0)
                throw new HbtException(L("NodeTemplate.Delete.Failed"));

            _logger.Info(L("NodeTemplate.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除节点模板
        /// </summary>
        /// <param name="ids">要删除的节点模板ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtNodeTemplate>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await NodeTemplateRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("NodeTemplate.BatchDelete.Failed"));

            _logger.Info(L("NodeTemplate.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtNodeTemplate, bool>> QueryExpression(HbtNodeTemplateQueryDto query)
        {
            var exp = Expressionable.Create<HbtNodeTemplate>();

            if (!string.IsNullOrEmpty(query?.NodeName))
                exp = exp.And(x => x.NodeName.Contains(query.NodeName));

            if (query?.DefinitionId.HasValue == true)
                exp = exp.And(x => x.DefinitionId == query.DefinitionId.Value);

            if (query?.NodeType.HasValue == true)
                exp = exp.And(x => x.NodeType == query.NodeType.Value);

            if (query?.ParentNodeId.HasValue == true)
                exp = exp.And(x => x.ParentNodeId == query.ParentNodeId.Value);

            if (query?.IsEnabled.HasValue == true)
                exp = exp.And(x => x.IsEnabled == query.IsEnabled.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 导入节点模板数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importNodeTemplates = await HbtExcelHelper.ImportAsync<HbtNodeTemplateImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importNodeTemplates)
                {
                    try
                    {
                        var nodeTemplate = item.Adapt<HbtNodeTemplate>();
                        nodeTemplate.CreateTime = DateTime.Now;
                        nodeTemplate.CreateBy = _currentUser.UserName;

                        var result = await NodeTemplateRepository.CreateAsync(nodeTemplate);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入节点模板失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入节点模板数据失败", ex);
                throw new HbtException("导入节点模板数据失败");
            }
        }

        /// <summary>
        /// 导出节点模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtNodeTemplateQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var nodeTemplates = await NodeTemplateRepository.GetListAsync(exp);
            var exportList = nodeTemplates.Adapt<List<HbtNodeTemplateExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "节点模板数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtNodeTemplateImportDto>(sheetName);
        }
    }
} 