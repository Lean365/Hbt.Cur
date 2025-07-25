//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtFormService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow;

/// <summary>
/// 表单服务接口
/// </summary>
public interface IHbtFormService
{
    /// <summary>
    /// 获取表单定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtFormDto>> GetListAsync(HbtFormQueryDto query);

    /// <summary>
    /// 根据ID获取表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>表单定义</returns>
    Task<HbtFormDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据键获取表单定义
    /// </summary>
    /// <param name="formKey">表单键</param>
    /// <returns>表单定义</returns>
    Task<HbtFormDto?> GetByKeyAsync(string formKey);

    /// <summary>
    /// 创建表单定义
    /// </summary>
    /// <param name="dto">表单定义创建DTO</param>
    /// <returns>表单定义ID</returns>
    Task<long> CreateAsync(HbtFormCreateDto dto);

    /// <summary>
    /// 更新表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="dto">表单定义更新DTO</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(long id, HbtFormUpdateDto dto);

    /// <summary>
    /// 删除表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除表单定义
    /// </summary>
    /// <param name="ids">表单定义ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 更新表单状态
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="status">新状态</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, int status);

    /// <summary>
    /// 获取我的表单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtFormDto>> GetMyFormsAsync(long userId, HbtFormQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导入表单数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出表单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtFormQueryDto query, string sheetName = "Sheet1");
} 