//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtInstanceService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例服务接口
//===================================================================

using Hbt.Application.Dtos.Workflow;

namespace Hbt.Application.Services.Workflow;

/// <summary>
/// 工作流实例服务接口
/// </summary>
public interface IHbtInstanceService
{
    /// <summary>
    /// 获取工作流实例列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtInstanceDto>> GetListAsync(HbtInstanceQueryDto query);

    /// <summary>
    /// 根据ID获取工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>工作流实例</returns>
    Task<HbtInstanceDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据业务键获取工作流实例
    /// </summary>
    /// <param name="businessKey">业务键</param>
    /// <returns>工作流实例</returns>
    Task<HbtInstanceDto?> GetByBusinessKeyAsync(string businessKey);

    /// <summary>
    /// 创建工作流实例
    /// </summary>
    /// <param name="dto">工作流实例创建DTO</param>
    /// <returns>工作流实例ID</returns>
    Task<long> CreateAsync(HbtInstanceCreateDto dto);

    /// <summary>
    /// 更新工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="dto">工作流实例更新DTO</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(long id, HbtInstanceUpdateDto dto);

    /// <summary>
    /// 删除工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除工作流实例
    /// </summary>
    /// <param name="ids">工作流实例ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 更新工作流实例状态
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, int status, string? reason = null);

    /// <summary>
    /// 设置工作流实例变量
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="variables">变量字典</param>
    /// <returns>是否成功</returns>
    Task<bool> SetVariablesAsync(long id, Dictionary<string, object> variables);

    /// <summary>
    /// 获取我的工作流实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtInstanceDto>> GetMyInstancesAsync(long userId, HbtInstanceQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导入工作流实例数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出工作流实例数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceQueryDto query, string sheetName = "Sheet1");
} 