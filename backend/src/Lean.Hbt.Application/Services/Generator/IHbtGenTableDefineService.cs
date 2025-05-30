using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Common.Models;

#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtGenTableDefineService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表定义服务接口
//===================================================================

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
    #region 表定义基础操作

    /// <summary>
    /// 根据ID获取表定义信息
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义信息</returns>
    Task<HbtGenTableDefineDto?> GetTableByIdAsync(long id);

    /// <summary>
    /// 获取分页表定义列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtGenTableDefineDto>> GetTableListAsync(HbtGenTableDefineQueryDto input);

    /// <summary>
    /// 创建代码生成表定义
    /// </summary>
    /// <param name="input">代码生成表定义信息</param>
    /// <returns>创建结果</returns>
    Task<HbtGenTableDefineDto> CreateTableAsync(HbtGenTableDefineCreateDto input);

    /// <summary>
    /// 更新表定义信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表定义信息</returns>
    Task<HbtGenTableDefineDto> UpdateTableAsync(HbtGenTableDefineUpdateDto input);

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteTableAsync(long id);

    /// <summary>
    /// 批量删除表定义
    /// </summary>
    /// <param name="ids">表定义ID数组</param>
    /// <returns>是否删除成功</returns>
    Task<bool> BatchDeleteTableAsync(long[] ids);

    #endregion

    #region 表定义导入导出操作

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量，失败数量）</returns>
    Task<(int success, int fail)> ImportTableAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    Task<(string fileName, byte[] content)> GetTableTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    Task<(string fileName, byte[] content)> ExportTableAsync(HbtGenTableDefineQueryDto query, string sheetName = "Sheet1");

    #endregion

    #region 表定义特殊操作

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    Task<List<HbtGenTableDefineDto>> InitializeTableListAsync(HbtGenTableDefineInitializeDto input);

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否同步成功</returns>
    Task<bool> SyncTableAsync(long id);

    #endregion

    #region 列定义操作

    /// <summary>
    /// 获取列定义列表
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>列定义列表</returns>
    Task<HbtPagedResult<HbtGenColumnDefineDto>> GetColumnListAsync(HbtGenColumnDefineQueryDto query);

    /// <summary>
    /// 创建列定义
    /// </summary>
    /// <param name="input">列定义信息</param>
    /// <returns>创建结果</returns>
    Task<HbtGenColumnDefineDto> CreateColumnAsync(HbtGenColumnDefineCreateDto input);

    /// <summary>
    /// 更新列定义
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的列定义信息</returns>
    Task<HbtGenColumnDefineDto> UpdateColumnAsync(HbtGenColumnDefineUpdateDto input);

    /// <summary>
    /// 删除列定义
    /// </summary>
    /// <param name="id">列定义ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteColumnAsync(long id);

    /// <summary>
    /// 批量删除列定义
    /// </summary>
    /// <param name="ids">列定义ID数组</param>
    /// <returns>是否删除成功</returns>
    Task<bool> BatchDeleteColumnsAsync(long[] ids);

    /// <summary>
    /// 导入列定义
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量，失败数量）</returns>
    Task<(int success, int fail)> ImportColumnAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 获取列定义模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    Task<(string fileName, byte[] content)> GetColumnTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 导出列定义
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    Task<(string fileName, byte[] content)> ExportColumnAsync(long tableId, string sheetName = "Sheet1");

    #endregion
}