//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDefinitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流定义服务实现类
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
    /// </remarks>
    public class HbtDefinitionService : HbtBaseService, IHbtDefinitionService
    {
        private readonly IHbtRepository<HbtDefinition> _definitionRepository;


        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="definitionRepository">工作流定义仓储接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDefinitionService(
            IHbtLogger logger,
            IHbtRepository<HbtDefinition> definitionRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization
                     ) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _definitionRepository = definitionRepository;

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

            var result = await _definitionRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtDefinitionDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtDefinitionDto>>()
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
            var definition = await _definitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            return definition.Adapt<HbtDefinitionDto>();
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
            var exists = await _definitionRepository.GetFirstAsync(x => x.WorkflowName == input.WorkflowName);
            if (exists != null)
                throw new HbtException(L("WorkflowDefinition.NameExists"));

            var definition = input.Adapt<HbtDefinition>();
            definition.WorkflowVersion = "A"; // 新建定义默认版本为A
            definition.Status = 0; // 0 表示草稿状态

            var result = await _definitionRepository.CreateAsync(definition);
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

            var definition = await _definitionRepository.GetByIdAsync(input.DefinitionId);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            // 检查名称是否已被其他定义使用
            var exists = await _definitionRepository.GetFirstAsync(x => x.WorkflowName == input.WorkflowName && x.Id != input.DefinitionId);
            if (exists != null)
                throw new HbtException(L("WorkflowDefinition.NameExists"));

            input.Adapt(definition);
            var result = await _definitionRepository.UpdateAsync(definition);
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
            var definition = await _definitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            // 检查工作流定义是否处于活动状态
            if (definition.Status == 1) // 1 表示已发布状态
                throw new HbtException(L("WorkflowDefinition.CannotDeleteActive"));

            var result = await _definitionRepository.DeleteAsync(definition);
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
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            // 检查是否有活动状态的定义
            var activeDefinitions = await _definitionRepository.GetListAsync(x => ids.Contains(x.Id) && x.Status == 1); // 1 表示已发布状态
            if (activeDefinitions.Any())
                throw new HbtException(L("WorkflowDefinition.CannotDeleteActive"));

            var exp = Expressionable.Create<HbtDefinition>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await _definitionRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.BatchDelete.Failed"));

            _logger.Info(L("WorkflowDefinition.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流定义数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(Stream stream, string sheetName = "Sheet1")
        {
            try
            {
                var importDefinitions = await HbtExcelHelper.ImportAsync<HbtDefinitionImportDto>(stream, sheetName);
                var success = 0;
                var fail = 0;

                foreach (var item in importDefinitions)
                {
                    try
                    {
                        var definition = item.Adapt<HbtDefinition>();
                        var insertResult = await _definitionRepository.CreateAsync(definition);
                        if (insertResult > 0)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("WorkflowDefinition.Import.Failed"), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowDefinition.Import.Failed"), ex);
                throw new HbtException(L("WorkflowDefinition.Import.Failed"), ex);
            }
        }

        /// <summary>
        /// 导出工作流定义数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtDefinitionQueryDto query, string sheetName = "Sheet1")
        {
            try
            {
                var data = await GetListAsync(query);
                var result = await HbtExcelHelper.ExportAsync(data.Rows, sheetName);
                return (result.fileName, result.content);
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowDefinition.Export.Failed"), ex);
                throw new HbtException(L("WorkflowDefinition.Export.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDefinitionImportDto>(sheetName);
        }

        /// <summary>
        /// 更新工作流定义状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="HbtException">当工作流定义不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(HbtDefinitionStatusDto input)
        {
            var definition = await _definitionRepository.GetByIdAsync(input.DefinitionId);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            definition.Status = input.Status;
            var result = await _definitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.UpdateStatus.Failed"));

            _logger.Info(L("WorkflowDefinition.UpdatedStatus.Success", definition.Id));
            return true;
        }

        /// <summary>
        /// 升级工作流定义版本
        /// </summary>
        /// <param name="id">工作流定义ID</param>
        /// <returns>升级后的版本号</returns>
        /// <exception cref="HbtException">当工作流定义不存在或升级失败时抛出异常</exception>
        public async Task<string> UpgradeVersionAsync(long id)
        {
            var definition = await _definitionRepository.GetByIdAsync(id);
            if (definition == null)
                throw new HbtException(L("WorkflowDefinition.NotFound"));

            // 获取当前版本
            var currentVersion = definition.WorkflowVersion ?? "A";
            
            // 升级版本号
            var newVersion = GetNextVersion(currentVersion);
            
            // 更新版本号
            definition.WorkflowVersion = newVersion;
            definition.UpdateBy = _currentUser.UserName;
            definition.UpdateTime = DateTime.Now;

            var result = await _definitionRepository.UpdateAsync(definition);
            if (result <= 0)
                throw new HbtException(L("WorkflowDefinition.UpgradeVersion.Failed"));

            _logger.Info(L("WorkflowDefinition.UpgradeVersion.Success", definition.Id, newVersion));
            return newVersion;
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

            // 确保版本号是单个字符
            if (currentVersion.Length > 1)
                currentVersion = currentVersion[0].ToString();

            // 获取当前字符的ASCII码
            int ascii = currentVersion[0];
            
            // 检查是否是有效的字母
            if (ascii < 65 || ascii > 90) // A-Z的ASCII码范围是65-90
                return "A";

            // 获取下一个字母
            char nextChar = (char)(ascii + 1);
            
            // 如果超出Z，则从A重新开始
            if (nextChar > 'Z')
                nextChar = 'A';

            return nextChar.ToString();
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
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
    }
}