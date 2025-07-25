//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtInstanceOperService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow;

/// <summary>
/// 工作流实例操作记录服务接口
/// </summary>
public interface IHbtInstanceOperService
{
    /// <summary>
    /// 获取操作记录列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtInstanceOperDto>> GetListAsync(HbtInstanceOperQueryDto query);

    /// <summary>
    /// 根据ID获取操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>操作记录</returns>
    Task<HbtInstanceOperDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建工作流审批操作
    /// </summary>
    /// <param name="dto">审批DTO</param>
    /// <returns>操作记录ID</returns>
    Task<long> CreateApproveAsync(HbtInstanceApproveDto dto);

    /// <summary>
    /// 创建操作记录
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>操作记录ID</returns>
    Task<long> CreateAsync(HbtInstanceOperCreateDto dto);

    /// <summary>
    /// 删除操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除实例操作
    /// </summary>
    /// <param name="ids">实例操作ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 获取我的操作记录列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtInstanceOperDto>> GetMyOperationsAsync(long userId, HbtInstanceOperQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导入操作记录数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出操作记录数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceOperQueryDto query, string sheetName = "Sheet1");
} 