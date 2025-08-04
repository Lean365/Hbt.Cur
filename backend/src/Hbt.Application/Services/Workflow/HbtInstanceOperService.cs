//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceOperService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录服务实现
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
/// 工作流实例操作记录服务实现
/// </summary>
public class HbtInstanceOperService : HbtBaseService, IHbtInstanceOperService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流实例操作记录仓储
    /// </summary>
    private IHbtRepository<HbtInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<HbtInstanceOper>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public HbtInstanceOperService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取操作记录列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页操作记录列表</returns>
    public async Task<HbtPagedResult<HbtInstanceOperDto>> GetListAsync(HbtInstanceOperQueryDto query)
    {
        try
        {
            var result = await OperRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtInstanceOperDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtInstanceOperDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("InstanceOper.GetListFailed"), ex);
            throw new HbtException(L("InstanceOper.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>操作记录信息，如果不存在则返回null</returns>
    public async Task<HbtInstanceOperDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<HbtInstanceOperDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建工作流审批操作
    /// </summary>
    /// <param name="dto">审批操作信息</param>
    /// <returns>新创建的操作记录ID</returns>
    public async Task<long> CreateApproveAsync(HbtInstanceApproveDto dto)
    {
        try
        {
            // 使用Adapt进行对象映射
            var entity = dto.Adapt<HbtInstanceOper>();
            
            // 设置当前用户信息
            entity.OperatorId = _currentUser.UserId;
            entity.OperatorName = _currentUser.UserName;
            
            // 如果没有节点名称，使用节点ID作为默认名称
            if (string.IsNullOrEmpty(entity.NodeName))
            {
                entity.NodeName = dto.NodeId;
            }
            
            // 确保操作类型正确映射
            entity.OperType = dto.OperType;

            var id = await OperRepository.CreateAsync(entity);
            _logger.Info($"创建工作流审批操作成功，ID: {id}, 实例ID: {dto.InstanceId}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建工作流审批操作失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建操作记录
    /// </summary>
    /// <param name="dto">操作记录创建信息</param>
    /// <returns>新创建的操作记录ID</returns>
    public async Task<long> CreateAsync(HbtInstanceOperCreateDto dto)
    {
        try
        {
            var entity = dto.Adapt<HbtInstanceOper>();

            var id = await OperRepository.CreateAsync(entity);
            _logger.Info($"创建操作记录成功，ID: {id}, 实例ID: {dto.InstanceId}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await OperRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除操作记录成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除实例操作
    /// </summary>
    /// <param name="ids">实例操作ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await OperRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 获取我的操作记录列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页操作记录列表</returns>
    public async Task<HbtPagedResult<HbtInstanceOperDto>> GetMyOperationsAsync(long userId, HbtInstanceOperQueryDto query)
    {
        try
        {
            var expression = Expressionable.Create<HbtInstanceOper>()
                .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
                .AndIF(!string.IsNullOrEmpty(query.NodeId), x => x.NodeId.Contains(query.NodeId))
                .AndIF(!string.IsNullOrEmpty(query.NodeName), x => x.NodeName.Contains(query.NodeName))
                .AndIF(query.OperType.HasValue, x => x.OperType == query.OperType.Value)
                .AndIF(query.OperatorId.HasValue, x => x.OperatorId == query.OperatorId.Value)
                .AndIF(!string.IsNullOrEmpty(query.OperatorName), x => x.OperatorName.Contains(query.OperatorName))
                .And(x => x.OperatorId == userId)
                .ToExpression();

            var result = await OperRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new HbtPagedResult<HbtInstanceOperDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtInstanceOperDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的操作记录列表失败: {ex.Message}", ex);
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
        return await HbtExcelHelper.GenerateTemplateAsync<HbtInstanceOperTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入操作记录数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var operations = await HbtExcelHelper.ImportAsync<HbtInstanceOperImportDto>(fileStream, sheetName);
            if (!operations.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var operation in operations)
            {
                try
                {
                    if (operation.InstanceId <= 0)
                    {
                        _logger.Warn("导入操作记录失败: 实例ID不能为空");
                        fail++;
                        continue;
                    }

                    var entity = operation.Adapt<HbtInstanceOper>();

                    var result = await OperRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入操作记录失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入操作记录数据失败", ex);
            throw new HbtException("导入操作记录数据失败");
        }
    }

    /// <summary>
    /// 导出操作记录数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceOperQueryDto query, string sheetName = "Sheet1")
    {
        var list = await OperRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<HbtInstanceOperExportDto>>();
        return await HbtExcelHelper.ExportAsync(exportList, sheetName, "操作记录数据");
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<HbtInstanceOper, bool>> QueryExpression(HbtInstanceOperQueryDto query)
    {
        return Expressionable.Create<HbtInstanceOper>()
            .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
            .AndIF(!string.IsNullOrEmpty(query.NodeId), x => x.NodeId.Contains(query.NodeId))
            .AndIF(!string.IsNullOrEmpty(query.NodeName), x => x.NodeName.Contains(query.NodeName))
            .AndIF(query.OperType.HasValue && query.OperType.Value != -1, x => x.OperType == query.OperType.Value)
            .AndIF(query.OperatorId.HasValue, x => x.OperatorId == query.OperatorId.Value)
            .AndIF(!string.IsNullOrEmpty(query.OperatorName), x => x.OperatorName.Contains(query.OperatorName))
            .ToExpression();
    }
} 