//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtInstanceTransService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow;

/// <summary>
/// 工作流实例流转历史服务接口
/// </summary>
public interface IHbtInstanceTransService
{
    /// <summary>
    /// 获取流转历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtInstanceTransDto>> GetListAsync(HbtInstanceTransQueryDto query);

    /// <summary>
    /// 根据ID获取流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>流转历史</returns>
    Task<HbtInstanceTransDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建流转历史
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>流转历史ID</returns>
    Task<long> CreateAsync(HbtInstanceTransCreateDto dto);

    /// <summary>
    /// 删除流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除实例流转
    /// </summary>
    /// <param name="ids">实例流转ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 获取工作流实例的流转历史
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转历史列表</returns>
    Task<List<HbtInstanceTransDto>> GetByInstanceIdAsync(long instanceId);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导入流转历史数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出流转历史数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceTransQueryDto query, string sheetName = "Sheet1");
} 