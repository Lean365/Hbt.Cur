#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTemplateService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成模板服务实现
//===================================================================

namespace Lean.Hbt.Application.Services.Generator;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

/// <summary>
/// 代码生成模板服务实现
/// </summary>
public class HbtGenTemplateService : HbtBaseService, IHbtGenTemplateService
{
    private readonly IHbtRepository<HbtGenTemplate> _templateRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="templateRepository">模板仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtGenTemplateService(
        IHbtRepository<HbtGenTemplate> templateRepository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _templateRepository = templateRepository;
    }

    #region 基础操作

    /// <summary>
    /// 根据ID获取模板信息
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>模板信息</returns>
    public async Task<HbtGenTemplateDto?> GetByIdAsync(long id)
    {
        var template = await _templateRepository.GetByIdAsync(id);
        if (template == null)
        {
            return null;
        }

        return template.Adapt<HbtGenTemplateDto>();
    }

    /// <summary>
    /// 获取分页模板列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenTemplateDto>> GetListAsync(HbtGenTemplateQueryDto input)
    {
        var exp = Expressionable.Create<HbtGenTemplate>();

        // 构建查询条件
        if (!string.IsNullOrEmpty(input.TemplateName))
            exp.And(x => x.TemplateName.Contains(input.TemplateName));
        if (input.TemplateType.HasValue)
            exp.And(x => x.TemplateType == input.TemplateType.Value);
        if (input.Status.HasValue)
            exp.And(x => x.Status == input.Status.Value);

        // 执行分页查询
        var result = await _templateRepository.GetPagedListAsync(
            exp.ToExpression(),
            input.PageIndex,
            input.PageSize,
            x => x.Id,
            OrderByType.Asc);

        // 返回分页结果
        return new HbtPagedResult<HbtGenTemplateDto>
        {
            Rows = result.Rows.Adapt<List<HbtGenTemplateDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = result.PageIndex,
            PageSize = result.PageSize
        };
    }

    /// <summary>
    /// 创建模板信息
    /// </summary>
    /// <param name="input">模板信息</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenTemplateDto> CreateAsync(HbtGenTemplateCreateDto input)
    {
        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_templateRepository, "TemplateName", input.TemplateName);

        var template = input.Adapt<HbtGenTemplate>();
        var result = await _templateRepository.CreateAsync(template);
        if (result <= 0)
        {
            throw new HbtException(L("GenTemplate.CreateFailed"));
        }

        return template.Adapt<HbtGenTemplateDto>();
    }

    /// <summary>
    /// 更新模板信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的模板信息</returns>
    public async Task<HbtGenTemplateDto> UpdateAsync(HbtGenTemplateUpdateDto input)
    {
        var template = await _templateRepository.GetByIdAsync(input.Id)
            ?? throw new HbtException(L("GenTemplate.NotFound", input.Id));

        // 验证字段是否已存在
        if (template.TemplateName != input.TemplateName)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_templateRepository, "TemplateName", input.TemplateName, input.Id);
        }

        input.Adapt(template);
        var result = await _templateRepository.UpdateAsync(template);
        if (result <= 0)
        {
            throw new HbtException(L("GenTemplate.UpdateFailed"));
        }

        return template.Adapt<HbtGenTemplateDto>();
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var template = await _templateRepository.GetByIdAsync(id);
        if (template == null)
        {
            throw new HbtException($"模板[{id}]不存在");
        }

        return await _templateRepository.DeleteAsync(template) > 0;
    }

    /// <summary>
    /// 批量删除模板
    /// </summary>
    /// <param name="ids">模板ID集合</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || !ids.Any())
            throw new HbtException(L("GenTemplate.BatchDelete.Empty"));

        return await _templateRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 更新模板状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(HbtGenTemplateStatusDto input)
    {
        var template = await _templateRepository.GetByIdAsync(input.TemplateId)
            ?? throw new HbtException(L("GenTemplate.NotFound", input.TemplateId));

        template.Status = input.Status;
        template.UpdateBy = _currentUser.UserName;
        template.UpdateTime = DateTime.Now;

        return await _templateRepository.UpdateAsync(template) > 0;
    }

    #endregion 基础操作

    #region 模板操作

    /// <summary>
    /// 导入模板
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入成功和失败的数量</returns>
    public async Task<(int success, int fail)> ImportTemplatesAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var templates = await HbtExcelHelper.ImportAsync<HbtGenTemplate>(fileStream, sheetName);
            if (templates == null || !templates.Any())
            {
                return (0, 0);
            }

            var success = 0;
            var fail = 0;

            foreach (var template in templates)
            {
                try
                {
                    // 验证模板名称是否已存在
                    var existTemplate = await _templateRepository.GetFirstAsync(x => x.TemplateName == template.TemplateName);
                    if (existTemplate != null)
                    {
                        fail++;
                        continue;
                    }

                    template.CreateBy = _currentUser.UserName;
                    template.CreateTime = DateTime.Now;
                    template.UpdateBy = _currentUser.UserName;
                    template.UpdateTime = DateTime.Now;

                    var result = await _templateRepository.CreateAsync(template);
                    if (result > 0)
                    {
                        success++;
                    }
                    else
                    {
                        fail++;
                    }
                }
                catch (Exception)
                {
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            throw new HbtException("导入模板失败");
        }
    }

    /// <summary>
    /// 导出模板
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<(string fileName, byte[] content)> ExportTemplatesAsync(HbtGenTemplateQueryDto query, string sheetName = "Sheet1")
    {
        try
        {
            var data = await GetListAsync(new HbtGenTemplateQueryDto());
            return await HbtExcelHelper.ExportAsync(data.Rows, sheetName);
        }
        catch (Exception ex)
        {
            _logger.Error(L("GenTemplate.Export.Failed"), ex);
            throw new HbtException(L("GenTemplate.Export.Failed"), ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtGenTemplate>(sheetName);
    }

    #endregion 模板操作
}