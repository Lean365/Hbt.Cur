//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表单服务实现 - 使用仓储工厂模式
//===================================================================
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流表单服务实现类
    /// </summary>
    /// <remarks>
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtFormService : HbtBaseService, IHbtFormService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtForm> FormRepository => _repositoryFactory.GetWorkflowRepository<HbtForm>();
        private IHbtRepository<HbtDefinition> DefinitionRepository => _repositoryFactory.GetWorkflowRepository<HbtDefinition>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtFormService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流表单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        public async Task<HbtPagedResult<HbtFormDto>> GetListAsync(HbtFormQueryDto query)
        {
            var exp = QueryExpression(query);
            var result = await FormRepository.GetPagedListAsync(exp, query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.CreateTime, OrderByType.Desc);

            var forms = result.Rows.Adapt<List<HbtFormDto>>();
            
            // 填充工作流定义名称
            foreach (var form in forms)
            {
                if (form.DefinitionId.HasValue && form.DefinitionId.Value > 0)
                {
                    var definition = await DefinitionRepository.GetByIdAsync(form.DefinitionId.Value);
                    form.DefinitionName = definition?.WorkflowName;
                }
            }

            return new HbtPagedResult<HbtFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = forms
            };
        }

        /// <summary>
        /// 获取工作流表单详情
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <returns>表单详情</returns>
        public async Task<HbtFormDto> GetByIdAsync(long id)
        {
            var form = await FormRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            var formDto = form.Adapt<HbtFormDto>();
            
            // 填充工作流定义名称
            if (formDto.DefinitionId.HasValue && formDto.DefinitionId.Value > 0)
            {
                var definition = await DefinitionRepository.GetByIdAsync(formDto.DefinitionId.Value);
                formDto.DefinitionName = definition?.WorkflowName;
            }

            return formDto;
        }

        /// <summary>
        /// 创建工作流表单
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的表单ID</returns>
        public async Task<long> CreateAsync(HbtFormCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            await HbtValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", input.FormName);

            var form = input.Adapt<HbtForm>();
            var result = await FormRepository.CreateAsync(form);
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.Create.Failed"));

            _logger.Info(L("WorkflowForm.Created.Success", form.Id));
            return form.Id;
        }

        /// <summary>
        /// 更新工作流表单
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtFormUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var form = await FormRepository.GetByIdAsync(input.FormId);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            if (form.FormName != input.FormName)
                await HbtValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", input.FormName);

            input.Adapt(form);
            var result = await FormRepository.UpdateAsync(form);
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.Update.Failed"));

            _logger.Info(L("WorkflowForm.Updated.Success", form.Id));
            return true;
        }

        /// <summary>
        /// 删除工作流表单
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var form = await FormRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            var result = await FormRepository.DeleteAsync(form);
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.Delete.Failed"));

            _logger.Info(L("WorkflowForm.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流表单
        /// </summary>
        /// <param name="ids">表单ID数组</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await FormRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.BatchDelete.Failed"));

            _logger.Info(L("WorkflowForm.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流表单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importForms = await HbtExcelHelper.ImportAsync<HbtFormImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importForms)
                {
                    try
                    {
                        var form = item.Adapt<HbtForm>();
                        form.CreateTime = DateTime.Now;
                        form.CreateBy = _currentUser.UserName;

                        var result = await FormRepository.CreateAsync(form);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流表单失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流表单数据失败", ex);
                throw new HbtException("导入工作流表单数据失败");
            }
        }

        /// <summary>
        /// 导出工作流表单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtFormQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var forms = await FormRepository.GetListAsync(exp);
            var exportList = forms.Adapt<List<HbtFormExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流表单数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtFormImportDto>(sheetName);
        }

        /// <summary>
        /// 根据工作流定义获取表单列表
        /// </summary>
        /// <param name="definitionId">工作流定义ID</param>
        /// <returns>表单列表</returns>
        public async Task<List<HbtFormDto>> GetFormsByWorkflowDefinitionAsync(long definitionId)
        {
            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => x.DefinitionId == definitionId);

            var forms = await FormRepository.GetListAsync(exp.ToExpression());
            return forms.Adapt<List<HbtFormDto>>();
        }

        /// <summary>
        /// 更改表单状态
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="status">新状态</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ChangeStatusAsync(long id, int status)
        {
            var form = await FormRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            form.Status = status;
            form.UpdateTime = DateTime.Now;
            form.UpdateBy = _currentUser.UserName;

            var result = await FormRepository.UpdateAsync(form);
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.ChangeStatus.Failed"));

            _logger.Info(L("WorkflowForm.StatusChanged.Success", id, status));
            return true;
        }

        /// <summary>
        /// 获取表单选项列表
        /// </summary>
        /// <returns>选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => x.Status == 1); // 只获取启用的表单

            var forms = await FormRepository.GetListAsync(exp.ToExpression());
            
            return forms.Select(x => new HbtSelectOption
            {
                Value = x.Id.ToString(),
                Label = x.FormName
            }).ToList();
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtForm, bool>> QueryExpression(HbtFormQueryDto query)
        {
            var exp = Expressionable.Create<HbtForm>();

            if (!string.IsNullOrEmpty(query?.FormName))
                exp = exp.And(x => x.FormName.Contains(query.FormName));

            if (query?.DefinitionId.HasValue == true)
                exp = exp.And(x => x.DefinitionId == query.DefinitionId.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取当前用户的表单
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户的表单列表</returns>
        public async Task<List<HbtFormDto>> GetCurrentUserFormsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtForm>();
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var forms = await FormRepository.GetListAsync(exp.ToExpression());
            var result = forms.Take(limit).Adapt<List<HbtFormDto>>();
            
            // 填充工作流定义名称
            foreach (var form in result)
            {
                if (form.DefinitionId.HasValue && form.DefinitionId.Value > 0)
                {
                    var definition = await DefinitionRepository.GetByIdAsync(form.DefinitionId.Value);
                    form.DefinitionName = definition?.WorkflowName;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户创建的表单
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户创建的表单列表</returns>
        public async Task<List<HbtFormDto>> GetCurrentUserCreatedFormsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => x.CreateBy == _currentUser.UserName);
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var forms = await FormRepository.GetListAsync(exp.ToExpression());
            var result = forms.Take(limit).Adapt<List<HbtFormDto>>();
            
            // 填充工作流定义名称
            foreach (var form in result)
            {
                if (form.DefinitionId.HasValue && form.DefinitionId.Value > 0)
                {
                    var definition = await DefinitionRepository.GetByIdAsync(form.DefinitionId.Value);
                    form.DefinitionName = definition?.WorkflowName;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户可访问的表单
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户可访问的表单列表</returns>
        public async Task<List<HbtFormDto>> GetCurrentUserAccessibleFormsAsync(int? status = null, int limit = 20)
        {
            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => x.Status == 1); // 只获取启用的表单
            
            if (status.HasValue)
                exp = exp.And(x => x.Status == status.Value);

            var forms = await FormRepository.GetListAsync(exp.ToExpression());
            var result = forms.Take(limit).Adapt<List<HbtFormDto>>();
            
            // 填充工作流定义名称
            foreach (var form in result)
            {
                if (form.DefinitionId.HasValue && form.DefinitionId.Value > 0)
                {
                    var definition = await DefinitionRepository.GetByIdAsync(form.DefinitionId.Value);
                    form.DefinitionName = definition?.WorkflowName;
                }
            }

            return result;
        }
    }
}