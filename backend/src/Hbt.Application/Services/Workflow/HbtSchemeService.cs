//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSchemeService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义服务实现
//===================================================================

using Hbt.Application.Dtos.Workflow;
using Hbt.Domain.Entities.Workflow;
using Hbt.Domain.Repositories;
using Hbt.Domain.IServices;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using Mapster;
using Hbt.Common.Exceptions;

namespace Hbt.Application.Services.Workflow;

/// <summary>
/// 工作流定义服务实现
/// </summary>
public class HbtSchemeService : HbtBaseService, IHbtSchemeService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流定义仓储
    /// </summary>
    private IHbtRepository<HbtScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<HbtScheme>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public HbtSchemeService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页工作流定义列表</returns>
    public async Task<HbtPagedResult<HbtSchemeDto>> GetListAsync(HbtSchemeQueryDto query)
    {
        try
        {
            var result = await SchemeRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtSchemeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtSchemeDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("Scheme.GetListFailed"), ex);
            throw new HbtException(L("Scheme.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>工作流定义信息，如果不存在则返回null</returns>
    public async Task<HbtSchemeDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await SchemeRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<HbtSchemeDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取工作流定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 根据键获取工作流定义
    /// </summary>
    /// <param name="schemeKey">工作流定义键</param>
    /// <returns>工作流定义信息，如果不存在则返回null</returns>
    public async Task<HbtSchemeDto?> GetByKeyAsync(string schemeKey)
    {
        try
        {
            var entity = await SchemeRepository.GetFirstAsync(x => x.SchemeKey == schemeKey);
            if (entity == null)
                return null;

            return entity.Adapt<HbtSchemeDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据键获取工作流定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建工作流定义
    /// </summary>
    /// <param name="dto">工作流定义创建信息</param>
    /// <returns>新创建的工作流定义ID</returns>
    public async Task<long> CreateAsync(HbtSchemeCreateDto dto)
    {
        try
        {
            // 检查键是否已存在
            var existing = await SchemeRepository.GetFirstAsync(x => x.SchemeKey == dto.SchemeKey);
            if (existing != null)
            {
                throw new InvalidOperationException($"工作流定义键 '{dto.SchemeKey}' 已存在");
            }

            var entity = dto.Adapt<HbtScheme>();

            var id = await SchemeRepository.CreateAsync(entity);
            _logger.Info($"创建工作流定义成功，ID: {id}, 键: {dto.SchemeKey}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建工作流定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 更新工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="dto">工作流定义更新信息</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateAsync(long id, HbtSchemeUpdateDto dto)
    {
        try
        {
            var entity = await SchemeRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            dto.Adapt(entity);

            var result = await SchemeRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新工作流定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新工作流定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await SchemeRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            // 检查是否有运行中的实例
            // TODO: 这里需要检查实例表，暂时跳过

            var result = await SchemeRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除工作流定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除工作流定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除工作流定义
    /// </summary>
    /// <param name="ids">工作流定义ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await SchemeRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await SchemeRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }


    /// <summary>
    /// 更新工作流定义状态
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="status">新状态值</param>
    /// <param name="reason">状态变更原因</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, int status, string? reason = null)
    {
        try
        {
            var entity = await SchemeRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Status = status;

            var result = await SchemeRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新工作流定义状态成功，ID: {id}, 状态: {status}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新工作流定义状态失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取我的工作流定义列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页工作流定义列表</returns>
    public async Task<HbtPagedResult<HbtSchemeDto>> GetMySchemesAsync(long userId, HbtSchemeQueryDto query)
    {
        try
        {
            var expression = QueryExpression(query, x => x.CreateBy == _currentUser.UserName);

            var result = await SchemeRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtSchemeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtSchemeDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的工作流定义列表失败: {ex.Message}", ex);
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
        return await HbtExcelHelper.GenerateTemplateAsync<HbtSchemeTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入工作流定义数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var schemes = await HbtExcelHelper.ImportAsync<HbtSchemeImportDto>(fileStream, sheetName);
            if (!schemes.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var scheme in schemes)
            {
                try
                {
                    if (string.IsNullOrEmpty(scheme.SchemeKey))
                    {
                        _logger.Warn("导入工作流定义失败: 工作流定义键不能为空");
                        fail++;
                        continue;
                    }

                    // 校验工作流定义键是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(SchemeRepository, "SchemeKey", scheme.SchemeKey);

                    var entity = scheme.Adapt<HbtScheme>();

                    var result = await SchemeRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入工作流定义失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入工作流定义数据失败", ex);
            throw new HbtException("导入工作流定义数据失败");
        }
    }

    /// <summary>
    /// 导出工作流定义数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtSchemeQueryDto query, string sheetName = "Sheet1")
    {
        var list = await SchemeRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<HbtSchemeExportDto>>();
        return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流定义数据");
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="additionalCondition">附加查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<HbtScheme, bool>> QueryExpression(HbtSchemeQueryDto query, Expression<Func<HbtScheme, bool>>? additionalCondition = null)
    {
        var expression = Expressionable.Create<HbtScheme>()
            .AndIF(!string.IsNullOrEmpty(query.SchemeKey), x => x.SchemeKey.Contains(query.SchemeKey))
            .AndIF(!string.IsNullOrEmpty(query.SchemeName), x => x.SchemeName.Contains(query.SchemeName))
            .AndIF(query.SchemeCategory.HasValue && query.SchemeCategory.Value != -1, x => x.SchemeCategory == query.SchemeCategory.Value)
            .AndIF(query.Status.HasValue && query.Status.Value != -1, x => x.Status == query.Status.Value);

        if (additionalCondition != null)
        {
            expression = expression.And(additionalCondition);
        }

        return expression.ToExpression();
    }
} 