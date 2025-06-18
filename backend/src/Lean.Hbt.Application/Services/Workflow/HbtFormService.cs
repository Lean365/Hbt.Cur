//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表单服务实现
//===================================================================
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流表单服务实现类
    /// </summary>
    public class HbtFormService : HbtBaseService, IHbtFormService
    {
        private readonly IHbtRepository<HbtForm> _formRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="formRepository">表单仓储接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtFormService(
            IHbtLogger logger,
            IHbtRepository<HbtForm> formRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _formRepository = formRepository;
        }

        /// <summary>
        /// 获取工作流表单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        public async Task<HbtPagedResult<HbtFormDto>> GetListAsync(HbtFormQueryDto query)
        {
            var exp = QueryExpression(query);
            var result = await _formRepository.GetPagedListAsync(exp, query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.CreateTime, OrderByType.Desc);

            return new HbtPagedResult<HbtFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtFormDto>>()
            };
        }

        /// <summary>
        /// 获取工作流表单详情
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <returns>表单详情</returns>
        public async Task<HbtFormDto> GetByIdAsync(long id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            return form.Adapt<HbtFormDto>();
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

            await HbtValidateUtils.ValidateFieldExistsAsync(_formRepository, "FormName", input.FormName);

            var form = input.Adapt<HbtForm>();
            var result = await _formRepository.CreateAsync(form);
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

            var form = await _formRepository.GetByIdAsync(input.FormId);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            if (form.FormName != input.FormName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_formRepository, "FormName", input.FormName);

            input.Adapt(form);
            var result = await _formRepository.UpdateAsync(form);
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
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            var result = await _formRepository.DeleteAsync(form);
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

            var result = await _formRepository.DeleteAsync(exp.ToExpression());
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
                        var insertResult = await _formRepository.CreateAsync(form);
                        if (insertResult > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("WorkflowForm.Import.Failed", item.FormName), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowForm.Import.Failed"), ex);
                throw new HbtException(L("WorkflowForm.Import.Failed"), ex);
            }
        }

        /// <summary>
        /// 导出工作流表单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtFormQueryDto query, string sheetName = "Sheet1")
        {
            try
            {
                var exp = QueryExpression(query);
                var list = await _formRepository.GetListAsync(exp);
                var exportList = list.Adapt<List<HbtFormExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流表单数据");
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowForm.Export.Failed"), ex);
                throw new HbtException(L("WorkflowForm.Export.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取工作流表单导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.GenerateTemplateAsync<HbtFormImportDto>(sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowForm.Template.Failed"), ex);
                throw new HbtException(L("WorkflowForm.Template.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取指定工作流定义下的所有表单
        /// </summary>
        /// <param name="definitionId">工作流定义ID</param>
        /// <returns>表单列表</returns>
        public async Task<List<HbtFormDto>> GetFormsByWorkflowDefinitionAsync(long definitionId)
        {
            var exp = Expressionable.Create<HbtForm>();
            exp = exp.And(x => x.DefinitionId == definitionId);

            var list = await _formRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtFormDto>>();
        }

        /// <summary>
        /// 修改表单状态
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="status">新状态</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ChangeStatusAsync(long id, int status)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new HbtException(L("WorkflowForm.NotFound"));

            form.Status = status;
            var result = await _formRepository.UpdateAsync(form);
            if (result <= 0)
                throw new HbtException(L("WorkflowForm.ChangeStatus.Failed"));

            _logger.Info(L("WorkflowForm.ChangedStatus.Success", id));
            return true;
        }

        /// <summary>
        /// 获取表单选项列表
        /// </summary>
        /// <returns>表单选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var forms = await _formRepository.AsQueryable()
                .Where(f => f.Status == 5) // 只获取已批准状态的表单
                .OrderBy(f => f.Id)
                .Select(f => new HbtSelectOption
                {
                    Label = f.FormName,
                    Value = f.Id,
                })
                .ToListAsync();
            return forms;
        }

        private Expression<Func<HbtForm, bool>> QueryExpression(HbtFormQueryDto query)
        {
            var exp = Expressionable.Create<HbtForm>();

            if (!string.IsNullOrEmpty(query?.FormName))
                exp = exp.And(x => x.FormName.Contains(query.FormName));

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }
    }
}