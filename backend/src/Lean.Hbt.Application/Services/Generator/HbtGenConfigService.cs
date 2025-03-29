#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfigService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成配置服务实现
//===================================================================

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成配置服务实现
/// 用于管理代码生成的基础配置信息
/// </summary>
public class HbtGenConfigService : IHbtGenConfigService
{
    private readonly IHbtRepository<HbtGenConfig> _configRepository;
    private readonly ILogger<HbtGenConfigService> _logger;
    private readonly IHbtCurrentUser _currentUser;
    private readonly IHbtLocalizationService _localization;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configRepository">配置仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtGenConfigService(
        IHbtRepository<HbtGenConfig> configRepository,
        ILogger<HbtGenConfigService> logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization)
    {
        _configRepository = configRepository;
        _logger = logger;
        _currentUser = currentUser;
        _localization = localization;
    }

    #region 基础操作

    /// <summary>
    /// 根据ID获取配置信息
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>配置信息</returns>
    public async Task<HbtGenConfigDto?> GetByIdAsync(long id)
    {
        var config = await _configRepository.GetByIdAsync(id);
        if (config == null)
        {
            return null;
        }

        return config.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 获取分页配置列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenConfigDto>> GetListAsync(HbtGenConfigQueryDto input)
    {
        var exp = Expressionable.Create<HbtGenConfig>();

        // 添加查询条件
        if (!string.IsNullOrEmpty(input.TableName))
        {
            exp = exp.And(x => x.TableName.Contains(input.TableName));
        }

        var result = await _configRepository.GetPagedListAsync(
            exp.ToExpression(),
            input.PageIndex,
            input.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        return new HbtPagedResult<HbtGenConfigDto>
        {
            Rows = result.Rows.Adapt<List<HbtGenConfigDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = input.PageIndex,
            PageSize = input.PageSize
        };
    }

    /// <summary>
    /// 创建配置信息
    /// </summary>
    /// <param name="input">配置信息</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenConfigDto> CreateAsync(HbtGenConfigCreateDto input)
    {
        // 验证字段是否已存在
        try
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", input.TableName);
        }
        catch (HbtException ex)
        {
            _logger.LogWarning($"{_localization.L("GenConfig.Create.Failed")}: {ex.Message}");
            throw;
        }

        var config = input.Adapt<HbtGenConfig>();
        await _configRepository.CreateAsync(config);
        return config.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 更新配置信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的配置信息</returns>
    public async Task<HbtGenConfigDto> UpdateAsync(HbtGenConfigUpdateDto input)
    {
        var config = await _configRepository.GetByIdAsync(input.Id);
        if (config == null)
        {
            throw new HbtException(_localization.L("GenConfig.NotFound"));
        }

        // 如果修改了表名,需要验证新表名是否已存在
        if (config.TableName != input.TableName)
        {
            try
            {
                await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", input.TableName);
            }
            catch (HbtException ex)
            {
                _logger.LogWarning($"{_localization.L("GenConfig.Update.Failed")}: {ex.Message}");
                throw;
            }
        }

        // 更新配置信息
        input.Adapt(config);
        await _configRepository.UpdateAsync(config);
        return config.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var config = await _configRepository.GetInfoAsync(x => x.Id == id);
        if (config == null)
            throw new HbtException(_localization.L("GenConfig.NotFound"));

        var result = await _configRepository.DeleteAsync(config);
        if (result <= 0)
            throw new HbtException(_localization.L("GenConfig.Delete.Failed"));

        _logger.LogInformation(_localization.L("GenConfig.Deleted.Success", id));
        return true;
    }

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            throw new ArgumentNullException(nameof(ids));

        var entities = await _configRepository.GetListAsync(x => ids.Contains(x.Id));
        if (!entities.Any())
            throw new HbtException(_localization.L("GenConfig.NotFound"));

        var result = await _configRepository.DeleteRangeAsync(entities);
        if (result <= 0)
            throw new HbtException(_localization.L("GenConfig.BatchDelete.Failed"));

        _logger.LogInformation(_localization.L("GenConfig.BatchDeleted.Success", string.Join(",", ids)));
        return true;
    }

    #endregion 基础操作

    #region 配置操作

    /// <summary>
    /// 导入配置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入成功和失败的数量</returns>
    public async Task<(int success, int fail)> ImportConfigsAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        var configs = await HbtExcelHelper.ImportAsync<HbtGenConfigImportDto>(fileStream, sheetName);
        if (configs == null || !configs.Any())
            return (0, 0);

        int success = 0;
        int fail = 0;

        foreach (var config in configs)
        {
            try
            {
                if (string.IsNullOrEmpty(config.TableName))
                {
                    _logger.LogWarning(_localization.L("GenConfig.Import.Empty"));
                    fail++;
                    continue;
                }

                // 验证字段是否已存在
                try
                {
                    await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", config.TableName);
                }
                catch (HbtException ex)
                {
                    _logger.LogWarning($"{_localization.L("GenConfig.Import.Failed")}: {ex.Message}");
                    fail++;
                    continue;
                }

                // 创建配置
                var newConfig = config.Adapt<HbtGenConfig>();
                await _configRepository.CreateAsync(newConfig);
                success++;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_localization.L("GenConfig.Import.Failed")}: {ex.Message}");
                fail++;
            }
        }

        _logger.LogInformation(_localization.L("GenConfig.Imported.Success", success, fail));
        return (success, fail);
    }

    /// <summary>
    /// 导出配置
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<byte[]> ExportConfigsAsync(HbtGenConfigQueryDto query, string sheetName = "Sheet1")
    {
        var exp = Expressionable.Create<HbtGenConfig>();

        if (!string.IsNullOrEmpty(query?.TableName))
            exp.And(x => x.TableName.Contains(query.TableName));

        var configs = await _configRepository.GetListAsync(exp.ToExpression());
        var exportData = configs.Adapt<List<HbtGenConfigExportDto>>();

        return await HbtExcelHelper.ExportAsync(exportData, sheetName);
    }

    /// <summary>
    /// 获取配置模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtGenConfigImportDto>(sheetName);
    }

    #endregion 配置操作
}