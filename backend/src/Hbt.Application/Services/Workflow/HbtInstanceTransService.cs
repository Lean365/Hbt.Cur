//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceTransService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史服务实现
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
/// 工作流实例流转历史服务实现
/// </summary>
public class HbtInstanceTransService : HbtBaseService, IHbtInstanceTransService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流实例流转历史仓储
    /// </summary>
    private IHbtRepository<HbtInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceTrans>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public HbtInstanceTransService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取流转历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页流转历史列表</returns>
    public async Task<HbtPagedResult<HbtInstanceTransDto>> GetListAsync(HbtInstanceTransQueryDto query)
    {
        try
        {
            var result = await TransRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtInstanceTransDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtInstanceTransDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("InstanceTrans.GetListFailed"), ex);
            throw new HbtException(L("InstanceTrans.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>流转历史信息，如果不存在则返回null</returns>
    public async Task<HbtInstanceTransDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<HbtInstanceTransDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取流转历史失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建流转历史
    /// </summary>
    /// <param name="dto">流转历史创建信息</param>
    /// <returns>新创建的流转历史ID</returns>
    public async Task<long> CreateAsync(HbtInstanceTransCreateDto dto)
    {
        try
        {
            var entity = dto.Adapt<HbtInstanceTrans>();

            var id = await TransRepository.CreateAsync(entity);
            _logger.Info($"创建流转历史成功，ID: {id}, 实例ID: {dto.InstanceId}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建流转历史失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await TransRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除流转历史成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除流转历史失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除实例流转
    /// </summary>
    /// <param name="ids">实例流转ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await TransRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 获取工作流实例的流转历史
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转历史列表</returns>
    public async Task<List<HbtInstanceTransDto>> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var list = await TransRepository.GetListAsync(x => x.InstanceId == instanceId);
            return list.Adapt<List<HbtInstanceTransDto>>();
        }
        catch (Exception ex)
        {
            _logger.Error($"获取工作流实例流转历史失败: {ex.Message}", ex);
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
        return await HbtExcelHelper.GenerateTemplateAsync<HbtInstanceTransTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入流转历史数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var transList = await HbtExcelHelper.ImportAsync<HbtInstanceTransImportDto>(fileStream, sheetName);
            if (!transList.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var trans in transList)
            {
                try
                {
                    if (trans.InstanceId <= 0)
                    {
                        _logger.Warn("导入流转历史失败: 工作流实例ID不能为空");
                        fail++;
                        continue;
                    }

                    var entity = trans.Adapt<HbtInstanceTrans>();

                    var result = await TransRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入流转历史失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入流转历史数据失败", ex);
            throw new HbtException("导入流转历史数据失败");
        }
    }

    /// <summary>
    /// 导出流转历史数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceTransQueryDto query, string sheetName = "Sheet1")
    {
        var list = await TransRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<HbtInstanceTransExportDto>>();
        return await HbtExcelHelper.ExportAsync(exportList, sheetName, "流转历史数据");
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<HbtInstanceTrans, bool>> QueryExpression(HbtInstanceTransQueryDto query)
    {
        return Expressionable.Create<HbtInstanceTrans>()
            .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
            .AndIF(!string.IsNullOrEmpty(query.StartNodeId), x => x.StartNodeId.Contains(query.StartNodeId))
            .AndIF(!string.IsNullOrEmpty(query.StartNodeName), x => x.StartNodeName.Contains(query.StartNodeName))
            .AndIF(query.StartNodeType.HasValue, x => x.StartNodeType == query.StartNodeType.Value)
            .AndIF(!string.IsNullOrEmpty(query.ToNodeId), x => x.ToNodeId.Contains(query.ToNodeId))
            .AndIF(!string.IsNullOrEmpty(query.ToNodeName), x => x.ToNodeName.Contains(query.ToNodeName))
            .AndIF(query.ToNodeType.HasValue, x => x.ToNodeType == query.ToNodeType.Value)
            .AndIF(query.TransState.HasValue, x => x.TransState == query.TransState.Value)
            .AndIF(query.IsFinish.HasValue, x => x.IsFinish == query.IsFinish.Value)
            .AndIF(!string.IsNullOrEmpty(query.CreateBy), x => x.CreateBy == query.CreateBy)
            .ToExpression();
    }
} 