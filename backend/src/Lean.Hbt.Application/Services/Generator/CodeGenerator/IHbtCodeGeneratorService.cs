using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Generator;

namespace Lean.Hbt.Application.Services.Generator.CodeGenerator;

/// <summary>
/// 代码生成服务接口
/// </summary>
public interface IHbtCodeGeneratorService
{
    /// <summary>
    /// 获取当前数据库中的所有表
    /// </summary>
    /// <returns>表信息列表</returns>
    Task<List<HbtGenTable>> GetAllTablesFromDatabaseAsync();

    /// <summary>
    /// 从数据库同步到页面
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>同步结果</returns>
    Task<bool> SyncFromDatabaseAsync(HbtGenTable table);

    /// <summary>
    /// 从页面同步到数据库
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>同步结果</returns>
    Task<bool> SyncToDatabaseAsync(HbtGenTable table);

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>生成结果</returns>
    Task<bool> GenerateCodeAsync(HbtGenTable table);

    /// <summary>
    /// 预览代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>预览结果</returns>
    Task<Dictionary<string, string>> PreviewCodeAsync(HbtGenTable table);

    /// <summary>
    /// 下载代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>下载结果</returns>
    Task<byte[]> DownloadCodeAsync(HbtGenTable table);
}
