//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtVariableService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流变量服务实现类 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common;
using Lean.Hbt.Common.Extensions;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流变量服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流变量的增删改查服务
    /// 2. 支持工作流变量的导入导出功能
    /// 3. 实现工作流变量的作用域管理
    /// 4. 提供变量值的获取和设置功能
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtVariableService : HbtBaseService, IHbtVariableService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtVariable> VariableRepository => _repositoryFactory.GetWorkflowRepository<HbtVariable>();
        private IHbtRepository<HbtInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<HbtInstance>();
        private IHbtRepository<HbtNode> NodeRepository => _repositoryFactory.GetWorkflowRepository<HbtNode>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtVariableService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流变量分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtVariableDto>> GetListAsync(HbtVariableQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await VariableRepository.GetPagedListAsync(
                exp,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            var variables = result.Rows.Adapt<List<HbtVariableDto>>();

            // 填充关联对象
            foreach (var variable in variables)
            {
                if (variable.InstanceId > 0)
                {
                    var instance = await InstanceRepository.GetByIdAsync(variable.InstanceId);
                    variable.WorkflowInstance = instance?.Adapt<HbtInstanceDto>();
                }
                if (variable.NodeId.HasValue)
                {
                    var node = await NodeRepository.GetByIdAsync(variable.NodeId.Value);
                    variable.Node = node?.Adapt<HbtNodeDto>();
                }
            }

            return new HbtPagedResult<HbtVariableDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = variables
            };
        }

        /// <summary>
        /// 根据ID获取工作流变量详情
        /// </summary>
        /// <param name="id">工作流变量ID</param>
        /// <returns>工作流变量详情DTO</returns>
        /// <exception cref="HbtException">当工作流变量不存在时抛出异常</exception>
        public async Task<HbtVariableDto> GetByIdAsync(long id)
        {
            var variable = await VariableRepository.GetByIdAsync(id);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            var dto = variable.Adapt<HbtVariableDto>();

            // 填充关联对象
            if (variable.InstanceId > 0)
            {
                var instance = await InstanceRepository.GetByIdAsync(variable.InstanceId);
                dto.WorkflowInstance = instance?.Adapt<HbtInstanceDto>();
            }
            if (variable.NodeId.HasValue)
            {
                var node = await NodeRepository.GetByIdAsync(variable.NodeId.Value);
                dto.Node = node?.Adapt<HbtNodeDto>();
            }
            return dto;
        }

        /// <summary>
        /// 创建新的工作流变量
        /// </summary>
        /// <param name="input">工作流变量创建DTO，包含变量的基本信息</param>
        /// <returns>新创建的工作流变量ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流变量创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtVariableCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // 检查变量名称是否已存在
            var exists = await VariableRepository.GetFirstAsync(x => x.VariableName == input.VariableName && x.InstanceId == input.InstanceId);
            if (exists != null)
                throw new HbtException(L("WorkflowVariable.NameExists"));

            var variable = input.Adapt<HbtVariable>();

            var result = await VariableRepository.CreateAsync(variable);
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.Create.Failed"));

            _logger.Info(L("WorkflowVariable.Created.Success", variable.Id));
            return variable.Id;
        }

        /// <summary>
        /// 更新工作流变量信息
        /// </summary>
        /// <param name="input">工作流变量更新DTO，包含需要更新的变量信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当工作流变量不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtVariableUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var variable = await VariableRepository.GetByIdAsync(input.VariableId);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            input.Adapt(variable);
            var result = await VariableRepository.UpdateAsync(variable);
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.Update.Failed"));

            _logger.Info(L("WorkflowVariable.Updated.Success", variable.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流变量
        /// </summary>
        /// <param name="id">要删除的工作流变量ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当工作流变量不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var variable = await VariableRepository.GetByIdAsync(id);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            var result = await VariableRepository.DeleteAsync(variable);
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.Delete.Failed"));

            _logger.Info(L("WorkflowVariable.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流变量
        /// </summary>
        /// <param name="ids">要删除的工作流变量ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtVariable>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await VariableRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.BatchDelete.Failed"));

            _logger.Info(L("WorkflowVariable.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 获取工作流实例的所有变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>变量列表</returns>
        public async Task<List<HbtVariableDto>> GetVariablesByWorkflowInstanceAsync(long instanceId)
        {
            var variables = await VariableRepository.GetListAsync(x => x.InstanceId == instanceId);
            return variables.Adapt<List<HbtVariableDto>>();
        }

        /// <summary>
        /// 获取工作流节点的所有变量
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>变量列表</returns>
        public async Task<List<HbtVariableDto>> GetVariablesByWorkflowNodeAsync(long workflowNodeId)
        {
            var variables = await VariableRepository.GetListAsync(x => x.NodeId == workflowNodeId);
            return variables.Adapt<List<HbtVariableDto>>();
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量值</returns>
        public async Task<string> GetVariableValueAsync(long instanceId, string variableName)
        {
            var variable = await VariableRepository.GetFirstAsync(x => 
                x.InstanceId == instanceId && 
                x.VariableName == variableName);

            return variable?.VariableValue ?? string.Empty;
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量值</param>
        /// <returns>设置是否成功</returns>
        public async Task<bool> SetVariableValueAsync(long instanceId, string variableName, string variableValue)
        {
            var variable = await VariableRepository.GetFirstAsync(x => 
                x.InstanceId == instanceId && 
                x.VariableName == variableName);

            if (variable == null)
            {
                // 创建新变量
                variable = new HbtVariable
                {
                    InstanceId = instanceId,
                    VariableName = variableName,
                    VariableValue = variableValue,
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = _currentUser.UserName,
                    UpdateTime = DateTime.Now
                };
                await VariableRepository.CreateAsync(variable);
            }
            else
            {
                // 更新现有变量
                variable.VariableValue = variableValue;
                variable.UpdateBy = _currentUser.UserName;
                variable.UpdateTime = DateTime.Now;
                await VariableRepository.UpdateAsync(variable);
            }

            return true;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtVariable, bool>> QueryExpression(HbtVariableQueryDto query)
        {
            var exp = Expressionable.Create<HbtVariable>();

            if (!string.IsNullOrEmpty(query?.VariableName))
                exp = exp.And(x => x.VariableName.Contains(query.VariableName));

            if (query?.InstanceId.HasValue == true)
                exp = exp.And(x => x.InstanceId == query.InstanceId.Value);

            if (query?.NodeId.HasValue == true)
                exp = exp.And(x => x.NodeId == query.NodeId.Value);

            if (!string.IsNullOrEmpty(query?.VariableType))
                exp = exp.And(x => x.VariableType == query.VariableType);

            if (query?.Scope.HasValue == true)
                exp = exp.And(x => x.Scope == query.Scope.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 导入工作流变量数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importVariables = await HbtExcelHelper.ImportAsync<HbtVariableImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importVariables)
                {
                    try
                    {
                        var variable = item.Adapt<HbtVariable>();
                        variable.CreateTime = DateTime.Now;
                        variable.CreateBy = _currentUser.UserName;

                        var result = await VariableRepository.CreateAsync(variable);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流变量失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流变量数据失败", ex);
                throw new HbtException("导入工作流变量数据失败");
            }
        }

        /// <summary>
        /// 导出工作流变量数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtVariableQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var variables = await VariableRepository.GetListAsync(exp);
            var exportList = variables.Adapt<List<HbtVariableExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流变量数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtVariableImportDto>(sheetName);
        }
    }
}