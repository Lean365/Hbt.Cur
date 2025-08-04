//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFormService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单服务实现
//===================================================================

using Hbt.Cur.Application.Dtos.Workflow;
using Hbt.Cur.Domain.Entities.Workflow;
using Hbt.Cur.Domain.Repositories;
using Hbt.Cur.Domain.IServices;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using Mapster;
using Hbt.Cur.Common.Exceptions;

namespace Hbt.Cur.Application.Services.Workflow;

/// <summary>
/// 表单服务实现
/// </summary>
public class HbtFormService : HbtBaseService, IHbtFormService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;

    private IHbtRepository<HbtForm> FormRepository => _repositoryFactory.GetWorkflowRepository<HbtForm>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public HbtFormService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取表单定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页表单定义列表</returns>
    public async Task<HbtPagedResult<HbtFormDto>> GetListAsync(HbtFormQueryDto query)
    {
        try
        {
            var result = await FormRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtFormDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("Form.GetListFailed"), ex);
            throw new HbtException(L("Form.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>表单定义信息，如果不存在则返回null</returns>
    public async Task<HbtFormDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<HbtFormDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 根据键获取表单定义
    /// </summary>
    /// <param name="formKey">表单键</param>
    /// <returns>表单定义信息，如果不存在则返回null</returns>
    public async Task<HbtFormDto?> GetByKeyAsync(string formKey)
    {
        try
        {
            var entity = await FormRepository.GetFirstAsync(x => x.FormKey == formKey);
            if (entity == null)
                return null;

            return entity.Adapt<HbtFormDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据键获取表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建表单定义
    /// </summary>
    /// <param name="dto">表单定义创建信息</param>
    /// <returns>新创建的表单定义ID</returns>
    public async Task<long> CreateAsync(HbtFormCreateDto dto)
    {
        try
        {
            // 检查键是否已存在
            var existing = await FormRepository.GetFirstAsync(x => x.FormKey == dto.FormKey);
            if (existing != null)
            {
                throw new InvalidOperationException($"表单键 '{dto.FormKey}' 已存在");
            }

            var entity = dto.Adapt<HbtForm>();

            var id = await FormRepository.CreateAsync(entity);
            _logger.Info($"创建表单定义成功，ID: {id}, 键: {dto.FormKey}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 更新表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="dto">表单定义更新信息</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateAsync(long id, HbtFormUpdateDto dto)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            dto.Adapt(entity);

            var result = await FormRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新表单定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await FormRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除表单定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除表单定义
    /// </summary>
    /// <param name="ids">表单定义ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await FormRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 更新表单状态
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="status">新状态值</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, int status)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Status = status;

            var result = await FormRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新表单状态成功，ID: {id}, 状态: {status}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新表单状态失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取我的表单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页表单定义列表</returns>
    public async Task<HbtPagedResult<HbtFormDto>> GetMyFormsAsync(long userId, HbtFormQueryDto query)
    {
        try
        {
            var expression = Expressionable.Create<HbtForm>()
                .AndIF(!string.IsNullOrEmpty(query.FormKey), x => x.FormKey.Contains(query.FormKey))
                .AndIF(!string.IsNullOrEmpty(query.FormName), x => x.FormName.Contains(query.FormName))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
                .And(x => x.CreateBy == _currentUser.UserName)
                .ToExpression();

            var result = await FormRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtFormDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的表单列表失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtFormTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入表单数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var forms = await HbtExcelHelper.ImportAsync<HbtFormImportDto>(fileStream, sheetName);
            if (!forms.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var form in forms)
            {
                try
                {
                    if (string.IsNullOrEmpty(form.FormKey))
                    {
                        _logger.Warn("导入表单失败: 表单键不能为空");
                        fail++;
                        continue;
                    }

                    // 校验表单键是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormKey", form.FormKey);

                    var entity = form.Adapt<HbtForm>();

                    var result = await FormRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入表单失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入表单数据失败", ex);
            throw new HbtException("导入表单数据失败");
        }
    }

    /// <summary>
    /// 导出表单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtFormQueryDto query, string sheetName = "Sheet1")
    {
        var list = await FormRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<HbtFormExportDto>>();
        return await HbtExcelHelper.ExportAsync(exportList, sheetName, "表单数据");
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<HbtForm, bool>> QueryExpression(HbtFormQueryDto query)
    {
        return Expressionable.Create<HbtForm>()
            .AndIF(!string.IsNullOrEmpty(query.FormKey), x => x.FormKey.Contains(query.FormKey))
            .AndIF(!string.IsNullOrEmpty(query.FormName), x => x.FormName.Contains(query.FormName))
            .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
            .ToExpression();
    }
} 