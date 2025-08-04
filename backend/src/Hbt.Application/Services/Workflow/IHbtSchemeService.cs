//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSchemeService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义服务接口
//===================================================================

using Hbt.Application.Dtos.Workflow;

namespace Hbt.Application.Services.Workflow;

/// <summary>
/// 工作流定义服务接口
/// </summary>
public interface IHbtSchemeService
{
    /// <summary>
    /// 获取工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtSchemeDto>> GetListAsync(HbtSchemeQueryDto query);

    /// <summary>
    /// 根据ID获取工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>工作流定义</returns>
    Task<HbtSchemeDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据键获取工作流定义
    /// </summary>
    /// <param name="schemeKey">工作流定义键</param>
    /// <returns>工作流定义</returns>
    Task<HbtSchemeDto?> GetByKeyAsync(string schemeKey);

    /// <summary>
    /// 创建工作流定义
    /// </summary>
    /// <param name="dto">工作流定义创建DTO</param>
    /// <returns>工作流定义ID</returns>
    Task<long> CreateAsync(HbtSchemeCreateDto dto);

    /// <summary>
    /// 更新工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="dto">工作流定义更新DTO</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(long id, HbtSchemeUpdateDto dto);

    /// <summary>
    /// 删除工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除工作流定义
    /// </summary>
    /// <param name="ids">工作流定义ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 更新工作流定义状态
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, int status, string? reason = null);

    /// <summary>
    /// 获取我的工作流定义列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtSchemeDto>> GetMySchemesAsync(long userId, HbtSchemeQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导入工作流定义数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出工作流定义数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtSchemeQueryDto query, string sheetName = "Sheet1");
} 