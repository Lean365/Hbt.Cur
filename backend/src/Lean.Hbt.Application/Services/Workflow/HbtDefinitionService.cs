//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefinitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义服务实现类 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common;
using Lean.Hbt.Common.Extensions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流定义服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流定义的增删改查服务
    /// 2. 支持工作流定义的导入导出功能
    /// 3. 实现工作流定义的版本管理
    /// 4. 提供工作流定义的启用/禁用功能
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtDefinitionService : HbtBaseService, IHbtDefinitionService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtDefinition> DefinitionRepository => _repositoryFactory.GetWorkflowRepository<HbtDefinition>();
        private IHbtRepository<HbtForm> FormRepository => _repositoryFactory.GetWorkflowRepository<HbtForm>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDefinitionService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization
                     ) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流定义分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. WorkflowName - 工作流定义名称（模糊查询）
        /// 2. WorkflowCategory - 工作流定义分类
        /// 3. WorkflowStatus - 工作流定义状态
        /// 4. PageIndex - 页码
        /// 5. PageSize - 每页记录数</param>
        /// <returns>返回分页后的工作流定义列表</returns>
        public async Task<HbtPagedResult<HbtDefinitionDto>> GetListAsync(HbtDefinitionQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await DefinitionRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            var definitions = result.Rows.Adapt<List<HbtDefinitionDto>>();
            
            // 填充表单名称
            foreach (var definition in definitions)
            {
                if (definition.FormId > 0)
                {
                    var form = await FormRepository.GetByIdAsync(definition.FormId);
                    definition.FormName = form?.FormName;
                }
            }

            return new HbtPagedResult<HbtDefinitionDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = definitions
            };
        }

        /// <summary>
        /// 根据ID获取工作流定义详情
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>工作流定义详情DTO</returns>
        /// <exception cref="HbtException">当工作流定义不存在时抛出异常</exception>
        public async Task<HbtDefinitionDto> GetByIdAsync(long id)
        {
            var definition = await DefinitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            var definitionDto = definition.Adapt<HbtDefinitionDto>();
            
            // 填充表单名称
            if (definitionDto.FormId > 0)
            {
                var form = await FormRepository.GetByIdAsync(definitionDto.FormId);
                definitionDto.FormName = form?.FormName;
            }

            return definitionDto;
        }

        /// <summary>
        /// 创建新的工作流定义
        /// </summary>
        /// <param name="input">工作流定义创建DTO，包含定义的基本信息</param>
        /// <returns>新创建的工作流定义ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流定义创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtDefinitionCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // 检查名称是否已存在
            var exists = await DefinitionRepository.GetFirstAsync(x => x.WorkflowName == input.WorkflowName);
            if (exists != null)
                throw new HbtException(L("WorkflowDefinition.NameExists"));

            var definition = input.Adapt<HbtDefinition>();
            definition.WorkflowVersion = "A"; // 新建定义默认版本为A
            definition.Status = 0; // 0 表示草稿状态

            var result = await DefinitionRepository.CreateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.Create.Failed"));

            _logger.Info(L("WorkflowDefinition.Created.Success", definition.Id));
            return definition.Id;
        }

        /// <summary>
        /// 更新工作流定义信息
        /// </summary>
        /// <param name="input">工作流定义更新DTO，包含需要更新的定义信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流定义不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtDefinitionUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var definition = await DefinitionRepository.GetByIdAsync(input.DefinitionId);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            // 检查名称是否已被其他定义使用
            var exists = await DefinitionRepository.GetFirstAsync(x => x.WorkflowName == input.WorkflowName && x.Id != input.DefinitionId);
            if (exists != null)
                throw new HbtException(L("WorkflowDefinition.NameExists"));

            input.Adapt(definition);
            var result = await DefinitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.Update.Failed"));

            _logger.Info(L("WorkflowDefinition.Updated.Success", definition.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流定义
        /// </summary>
        /// <param name="id">要删除的工作流定义ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当工作流定义不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var definition = await DefinitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            var result = await DefinitionRepository.DeleteAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.Delete.Failed"));

            _logger.Info(L("WorkflowDefinition.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流定义
        /// </summary>
        /// <param name="ids">要删除的工作流定义ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtDefinition>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await DefinitionRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.BatchDelete.Failed"));

            _logger.Info(L("WorkflowDefinition.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流定义数据
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream stream, string sheetName = "Sheet1")
        {
            try
            {
                var importDefinitions = await HbtExcelHelper.ImportAsync<HbtDefinitionImportDto>(stream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importDefinitions)
                {
                    try
                    {
                        var definition = item.Adapt<HbtDefinition>();
                        definition.CreateTime = DateTime.Now;
                        definition.CreateBy = _currentUser.UserName;

                        var result = await DefinitionRepository.CreateAsync(definition);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流定义失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流定义数据失败", ex);
                throw new HbtException("导入工作流定义数据失败");
            }
        }

        /// <summary>
        /// 导出工作流定义数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtDefinitionQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var definitions = await DefinitionRepository.GetListAsync(exp);
            var exportList = definitions.Adapt<List<HbtDefinitionExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流定义数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDefinitionImportDto>(sheetName);
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        /// <param name="input">状态更新DTO</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtDefinitionStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var definition = await DefinitionRepository.GetByIdAsync(input.DefinitionId);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            definition.Status = input.Status;
            definition.UpdateTime = DateTime.Now;
            definition.UpdateBy = _currentUser.UserName;

            var result = await DefinitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.UpdateStatus.Failed"));

            _logger.Info(L("WorkflowDefinition.StatusUpdated.Success", definition.Id, input.Status));
            return true;
        }

        /// <summary>
        /// 升级工作流定义版本
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>新版本号</returns>
        public async Task<string> UpgradeVersionAsync(long id)
        {
            var definition = await DefinitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            var newVersion = GetNextVersion(definition.WorkflowVersion);
            definition.WorkflowVersion = newVersion;
            definition.UpdateTime = DateTime.Now;
            definition.UpdateBy = _currentUser.UserName;

            var result = await DefinitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.UpgradeVersion.Failed"));

            _logger.Info(L("WorkflowDefinition.VersionUpgraded.Success", definition.Id, newVersion));
            return newVersion;
        }

        /// <summary>
        /// 获取工作流定义选项列表
        /// </summary>
        /// <param name="includeDisabled">是否包含禁用的定义</param>
        /// <returns>选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync(bool includeDisabled = false)
        {
            var exp = Expressionable.Create<HbtDefinition>();
            if (!includeDisabled)
                exp = exp.And(x => x.Status == 1); // 只获取启用的定义

            var definitions = await DefinitionRepository.GetListAsync(exp.ToExpression());
            
            return definitions.Select(x => new HbtSelectOption
            {
                Value = x.Id.ToString(),
                Label = $"{x.WorkflowName} (v{x.WorkflowVersion})"
            }).ToList();
        }

        /// <summary>
        /// 获取下一个版本号
        /// </summary>
        /// <param name="currentVersion">当前版本号</param>
        /// <returns>下一个版本号</returns>
        private string GetNextVersion(string currentVersion)
        {
            if (string.IsNullOrEmpty(currentVersion))
                return "A";

            // 简单的版本升级逻辑：A -> B -> C -> ... -> Z -> AA -> AB -> ...
            if (currentVersion.Length == 1)
            {
                var nextChar = (char)(currentVersion[0] + 1);
                if (nextChar > 'Z')
                    return "AA";
                return nextChar.ToString();
            }
            else
            {
                // 处理多字符版本号
                var lastChar = currentVersion[currentVersion.Length - 1];
                var nextChar = (char)(lastChar + 1);
                if (nextChar > 'Z')
                {
                    // 需要进位
                    var prefix = currentVersion.Substring(0, currentVersion.Length - 1);
                    var nextPrefix = GetNextVersion(prefix);
                    return nextPrefix + "A";
                }
                else
                {
                    return currentVersion.Substring(0, currentVersion.Length - 1) + nextChar;
                }
            }
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtDefinition, bool>> QueryExpression(HbtDefinitionQueryDto query)
        {
            var exp = Expressionable.Create<HbtDefinition>();

            if (!string.IsNullOrEmpty(query?.WorkflowName))
                exp = exp.And(x => x.WorkflowName.Contains(query.WorkflowName));

            if (!string.IsNullOrEmpty(query?.WorkflowCategory))
                exp = exp.And(x => x.WorkflowCategory == query.WorkflowCategory);

            if (query?.WorkflowStatus.HasValue == true)
                exp = exp.And(x => x.Status == query.WorkflowStatus.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取当前用户的工作流定义
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户的工作流定义列表</returns>
        public async Task<List<HbtDefinitionDto>> GetCurrentUserDefinitionsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtDefinition>();
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var definitions = await DefinitionRepository.GetListAsync(exp.ToExpression());
            var result = definitions.Take(limit).Adapt<List<HbtDefinitionDto>>();
            
            // 填充表单名称
            foreach (var definition in result)
            {
                if (definition.FormId > 0)
                {
                    var form = await FormRepository.GetByIdAsync(definition.FormId);
                    definition.FormName = form?.FormName;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户创建的工作流定义
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户创建的工作流定义列表</returns>
        public async Task<List<HbtDefinitionDto>> GetCurrentUserCreatedDefinitionsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtDefinition>();
            exp = exp.And(x => x.CreateBy == _currentUser.UserName);
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var definitions = await DefinitionRepository.GetListAsync(exp.ToExpression());
            var result = definitions.Take(limit).Adapt<List<HbtDefinitionDto>>();
            
            // 填充表单名称
            foreach (var definition in result)
            {
                if (definition.FormId > 0)
                {
                    var form = await FormRepository.GetByIdAsync(definition.FormId);
                    definition.FormName = form?.FormName;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户可访问的工作流定义
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户可访问的工作流定义列表</returns>
        public async Task<List<HbtDefinitionDto>> GetCurrentUserAccessibleDefinitionsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtDefinition>();
            exp = exp.And(x => x.Status == 1); // 只获取启用的定义
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var definitions = await DefinitionRepository.GetListAsync(exp.ToExpression());
            var result = definitions.Take(limit).Adapt<List<HbtDefinitionDto>>();
            
            // 填充表单名称
            foreach (var definition in result)
            {
                if (definition.FormId > 0)
                {
                    var form = await FormRepository.GetByIdAsync(definition.FormId);
                    definition.FormName = form?.FormName;
                }
            }

            return result;
        }
    }
}