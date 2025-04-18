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

/// <summary>
/// 代码生成模板服务实现
/// </summary>
public class HbtGenTemplateService : IHbtGenTemplateService
{
    private readonly IHbtRepository<HbtGenTemplate> _templateRepository;
    private readonly ILogger<HbtGenTemplateService> _logger;
    private readonly IHbtCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="templateRepository">模板仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    public HbtGenTemplateService(
        IHbtRepository<HbtGenTemplate> templateRepository,
        ILogger<HbtGenTemplateService> logger,
        IHbtCurrentUser currentUser)
    {
        _templateRepository = templateRepository;
        _logger = logger;
        _currentUser = currentUser;
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
        // 验证模板名称是否已存在
        var existTemplate = await _templateRepository.GetFirstAsync(x => x.TemplateName == input.TemplateName);
        if (existTemplate != null)
        {
            throw new HbtException($"模板名称[{input.TemplateName}]已存在");
        }

        var template = input.Adapt<HbtGenTemplate>();
        template.CreateBy = _currentUser.UserName;
        template.CreateTime = DateTime.Now;
        template.UpdateBy = _currentUser.UserName;
        template.UpdateTime = DateTime.Now;

        var result = await _templateRepository.CreateAsync(template);
        if (result <= 0)
        {
            throw new HbtException("创建模板失败");
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
        var template = await _templateRepository.GetByIdAsync(input.Id);
        if (template == null)
        {
            throw new HbtException($"模板[{input.Id}]不存在");
        }

        // 验证模板名称是否已存在
        if (template.TemplateName != input.TemplateName)
        {
            var existTemplate = await _templateRepository.GetFirstAsync(x => x.TemplateName == input.TemplateName);
            if (existTemplate != null)
            {
                throw new HbtException($"模板名称[{input.TemplateName}]已存在");
            }
        }

        template.TemplateName = input.TemplateName;
        template.TemplateType = input.TemplateType;
        template.TemplateContent = input.TemplateContent;
        template.Status = input.Status;
        template.Remark = input.Remark;
        template.UpdateBy = _currentUser.UserName;
        template.UpdateTime = DateTime.Now;

        var result = await _templateRepository.UpdateAsync(template);
        if (result <= 0)
        {
            throw new HbtException("更新模板失败");
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
        if (ids == null || ids.Length == 0)
        {
            throw new HbtException("请选择要删除的模板");
        }

        var templates = await _templateRepository.GetListAsync(x => ids.Contains(x.Id));
        if (templates?.Count > 0)
        {
            return await _templateRepository.DeleteRangeAsync(templates) > 0;
        }

        return false;
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
            _logger.LogError(ex, "导入模板失败");
            throw new HbtException("导入模板失败");
        }
    }

    /// <summary>
    /// 导出模板
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<byte[]> ExportTemplatesAsync(HbtGenTemplateQueryDto query, string sheetName = "Sheet1")
    {
        try
        {
            var exp = Expressionable.Create<HbtGenTemplate>();

            // 构建查询条件
            if (!string.IsNullOrEmpty(query.TemplateName))
                exp.And(x => x.TemplateName.Contains(query.TemplateName));
            if (query.TemplateType.HasValue)
                exp.And(x => x.TemplateType == query.TemplateType.Value);
            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var templates = await _templateRepository.GetListAsync(exp.ToExpression());
            if (templates == null || !templates.Any())
            {
                return Array.Empty<byte>();
            }

            return await HbtExcelHelper.ExportAsync(templates, sheetName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "导出模板失败");
            throw new HbtException("导出模板失败");
        }
    }

    /// <summary>
    /// 获取模板文件
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
    {
        try
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtGenTemplate>(sheetName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取模板文件失败");
            throw new HbtException("获取模板文件失败");
        }
    }

    #endregion 模板操作
}