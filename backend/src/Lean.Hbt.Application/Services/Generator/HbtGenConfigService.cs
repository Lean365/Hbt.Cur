//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 生成配置服务实现类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 生成配置服务实现类
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtGenConfigService : HbtBaseService, IHbtGenConfigService
{
    private readonly IHbtRepository<HbtGenConfig> _configRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigService(
        IHbtRepository<HbtGenConfig> configRepository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
    {
        _configRepository = configRepository ?? throw new ArgumentNullException(nameof(configRepository));
    }

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<HbtGenConfig, bool>> HbtGenConfigQueryExpression(HbtGenConfigQueryDto query)
    {
        return Expressionable.Create<HbtGenConfig>()
            .AndIF(!string.IsNullOrEmpty(query.TableName), x => x.TableName.Contains(query.TableName!))
            .AndIF(!string.IsNullOrEmpty(query.ModuleName), x => x.ModuleName.Contains(query.ModuleName!))
            .AndIF(!string.IsNullOrEmpty(query.BusinessName), x => x.BusinessName.Contains(query.BusinessName!))
            .AndIF(query.GenType.HasValue, x => x.GenType == query.GenType.Value)
            .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .ToExpression();
    }

    /// <summary>
    /// 获取配置分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>配置分页列表</returns>
    public async Task<HbtPagedResult<HbtGenConfigDto>> GetListAsync(HbtGenConfigQueryDto query)
    {
        query ??= new HbtGenConfigQueryDto();

        var result = await _configRepository.GetPagedListAsync(
            HbtGenConfigQueryExpression(query),
            query.PageIndex,
            query.PageSize,
            x => x.Id,
            OrderByType.Asc);

        return new HbtPagedResult<HbtGenConfigDto>
        {
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.Rows.Adapt<List<HbtGenConfigDto>>()
        };
    }

    /// <summary>
    /// 获取配置详情
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>配置详情</returns>
    public async Task<HbtGenConfigDto?> GetByIdAsync(long id)
    {
        var config = await _configRepository.GetByIdAsync(id);
        return config?.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 创建配置
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>配置信息</returns>
    public async Task<HbtGenConfigDto> CreateAsync(HbtGenConfigCreateDto input)
    {
        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", input.TableName);

        var config = input.Adapt<HbtGenConfig>();
        var result = await _configRepository.CreateAsync(config);
        if (result <= 0)
            throw new HbtException(L("Generator.Config.CreateFailed"));

        return config.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>更新后的配置信息</returns>
    public async Task<HbtGenConfigDto> UpdateAsync(HbtGenConfigUpdateDto input)
    {
        var config = await _configRepository.GetByIdAsync(input.ConfigId)
            ?? throw new HbtException(L("Generator.Config.NotFound", input.ConfigId));

        // 验证字段是否已存在
        if (config.TableName != input.TableName)
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", input.TableName, input.ConfigId);

        input.Adapt(config);
        var result = await _configRepository.UpdateAsync(config);
        if (result <= 0)
            throw new HbtException(L("Generator.Config.UpdateFailed"));

        return config.Adapt<HbtGenConfigDto>();
    }

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var config = await _configRepository.GetByIdAsync(id)
            ?? throw new HbtException(L("Generator.Config.NotFound", id));

        return await _configRepository.DeleteAsync(id) > 0;
    }

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0) return false;
        return await _configRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 导入配置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportConfigsAsync(Stream fileStream, string sheetName = "HbtGenConfig")
    {
        var configs = await HbtExcelHelper.ImportAsync<HbtGenConfigDto>(fileStream, sheetName);
        if (configs == null || !configs.Any()) return (0, 0);

        var (success, fail) = (0, 0);
        foreach (var config in configs)
        {
            try
            {
                // 验证字段是否已存在
                await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "TableName", config.TableName);

                await _configRepository.CreateAsync(config.Adapt<HbtGenConfig>());
                success++;
            }
            catch
            {
                fail++;
            }
        }

        return (success, fail);
    }

    /// <summary>
    /// 导出配置
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导出的Excel文件内容</returns>
    public async Task<(string fileName, byte[] content)> ExportConfigsAsync(HbtGenConfigQueryDto query, string sheetName = "Config")
    {
        try
        {
            var list = await _configRepository.GetListAsync(HbtGenConfigQueryExpression(query));
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtGenConfigDto>>(), sheetName, L("Generator.Config.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.Config.ExportFailed", ex.Message), ex);
            throw new HbtException(L("Generator.Config.ExportFailed"));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtGenConfigDto>(sheetName);
    }
}
