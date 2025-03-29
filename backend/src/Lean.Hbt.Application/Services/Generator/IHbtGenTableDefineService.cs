#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtGenTableDefineService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表定义服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表定义服务接口
/// 用于自定义数据表结构,当数据库中没有对应的表时,通过此服务来定义表结构
/// 主要功能:
/// 1. 自定义数据表结构
/// 2. 从数据库初始化表结构
/// 3. 将自定义表结构同步到代码生成所需的表中
/// </summary>
public interface IHbtGenTableDefineService
{
    #region 基础操作

    /// <summary>
    /// 根据ID获取表定义信息
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义信息</returns>
    Task<HbtGenTableDefineDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取分页表定义列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtGenTableDefineDto>> GetListAsync(HbtGenTableDefineQueryDto input);

    /// <summary>
    /// 创建代码生成表定义
    /// </summary>
    /// <param name="input">代码生成表定义信息</param>
    /// <returns>创建结果</returns>
    Task<HbtGenTableDefineDto> CreateAsync(HbtGenTableDefineCreateDto input);

    /// <summary>
    /// 更新表定义信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表定义信息</returns>
    Task<HbtGenTableDefineDto> UpdateAsync(HbtGenTableDefineUpdateDto input);

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除表定义
    /// </summary>
    /// <param name="ids">表定义ID数组</param>
    /// <returns>是否删除成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    #endregion

    #region 导入导出操作

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表定义列表</returns>
    Task<List<HbtGenTableDefineDto>> ImportTablesAsync(HbtGenTableDefineImportDto input);

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <returns>导出的表定义列表</returns>
    Task<List<HbtGenTableDefineExportDto>> ExportTablesAsync();

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <returns>模板结果</returns>
    Task<HbtGenTableDefineTemplateDto> GetTemplateAsync();

    #endregion

    #region 特殊操作

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    Task<List<HbtGenTableDefineDto>> InitializeTablesAsync(HbtGenTableDefineCreateDto input);

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否同步成功</returns>
    Task<bool> SyncTableAsync(long id);

    #endregion
}