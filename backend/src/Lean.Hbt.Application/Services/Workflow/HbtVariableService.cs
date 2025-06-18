//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtVariableService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流变量服务实现类
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
    /// </remarks>
    public class HbtVariableService : HbtBaseService, IHbtVariableService
    {
        private readonly IHbtRepository<HbtVariable> _variableRepository;

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="variableRepository">工作流变量仓储接口</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtVariableService(
            IHbtRepository<HbtVariable> variableRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _variableRepository = variableRepository;
        }

        /// <summary>
        /// 获取工作流变量分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtVariableDto>> GetListAsync(HbtVariableQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await _variableRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtVariableDto>
            {
                Rows = result.Rows.Adapt<List<HbtVariableDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
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
            var variable = await _variableRepository.GetByIdAsync(id);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            return variable.Adapt<HbtVariableDto>();
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
            await HbtValidateUtils.ValidateFieldExistsAsync(_variableRepository, "VariableName", input.VariableName);

            var variable = input.Adapt<HbtVariable>();

            var result = await _variableRepository.CreateAsync(variable);
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

            var variable = await _variableRepository.GetByIdAsync(input.WorkflowVariableId);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            input.Adapt(variable);
            var result = await _variableRepository.UpdateAsync(variable);
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
            var variable = await _variableRepository.GetByIdAsync(id);
            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            var result = await _variableRepository.DeleteAsync(variable);
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

            var result = await _variableRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.BatchDelete.Failed"));

            _logger.Info(L("WorkflowVariable.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 获取工作流实例的所有变量
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <returns>变量列表</returns>
        public async Task<List<HbtVariableDto>> GetVariablesByWorkflowInstanceAsync(long InstanceId)
        {
            var variables = await _variableRepository.GetListAsync(x => x.InstanceId == InstanceId);
            return variables.Adapt<List<HbtVariableDto>>();
        }

        /// <summary>
        /// 获取工作流节点的所有变量
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>变量列表</returns>
        public async Task<List<HbtVariableDto>> GetVariablesByWorkflowNodeAsync(long workflowNodeId)
        {
            var variables = await _variableRepository.GetListAsync(x => x.NodeId == workflowNodeId);
            return variables.Adapt<List<HbtVariableDto>>();
        }

        /// <summary>
        /// 获取工作流变量值
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量值</returns>
        /// <exception cref="HbtException">当变量不存在时抛出异常</exception>
        public async Task<string> GetVariableValueAsync(long InstanceId, string variableName)
        {
            var variable = await _variableRepository.SqlSugarClient.Queryable<HbtVariable>()
                .FirstAsync(x => x.InstanceId == InstanceId &&
                               x.VariableName == variableName);

            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            return variable.VariableValue;
        }

        /// <summary>
        /// 设置工作流变量值
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量值</param>
        /// <returns>是否成功</returns>
        /// <exception cref="HbtException">当变量不存在或更新失败时抛出异常</exception>
        public async Task<bool> SetVariableValueAsync(long InstanceId, string variableName, string variableValue)
        {
            var variable = await _variableRepository.SqlSugarClient.Queryable<HbtVariable>()
                .FirstAsync(x => x.InstanceId == InstanceId &&
                               x.VariableName == variableName);

            if (variable == null)
                throw new HbtException(L("WorkflowVariable.NotFound"));

            variable.VariableValue = variableValue;

            var result = await _variableRepository.UpdateAsync(variable);
            if (result <= 0)
                throw new HbtException(L("WorkflowVariable.UpdateValue.Failed"));

            _logger.Info(L("WorkflowVariable.UpdatedValue.Success", variable.Id));
            return true;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        private Expression<Func<HbtVariable, bool>> QueryExpression(HbtVariableQueryDto query)
        {
            var exp = Expressionable.Create<HbtVariable>();

            if (!string.IsNullOrEmpty(query?.VariableName))
                exp = exp.And(x => x.VariableName.Contains(query.VariableName));

            if (query?.VariableType.HasValue == true)
                exp = exp.And(x => x.VariableType == query.VariableType.Value.ToString());

            if (query?.VariableScope.HasValue == true)
                exp = exp.And(x => x.Scope == query.VariableScope.Value);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtVariableImportDto>(sheetName);
        }

        /// <summary>
        /// 导入工作流变量数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var variables = await HbtExcelHelper.ImportAsync<HbtVariableImportDto>(fileStream, sheetName);
                if (!variables.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var variable in variables)
                {
                    try
                    {
                        var entity = variable.Adapt<HbtVariable>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;

                        var result = await _variableRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入变量失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入变量数据失败", ex);
                throw new HbtException("导入变量数据失败");
            }
        }

        /// <summary>
        /// 导出工作流变量数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件或zip文件</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtVariableQueryDto query, string sheetName = "Sheet1")
        {
            var list = await _variableRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<HbtVariableExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "变量数据");
        }
    }
}