#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLogCleanupService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 日志清理服务接口
//===================================================================




#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLogCleanupService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 日志清理服务接口
//===================================================================

using Hbt.Cur.Common.Options;

namespace Hbt.Cur.Domain.IServices.Extensions
{
    /// <summary>
    /// 日志清理服务接口
    /// </summary>
    public interface IHbtLogCleanupService
    {
        /// <summary>
        /// 获取日志清理配置
        /// </summary>
        Task<HbtLogCleanupOptions> GetConfigAsync();

        /// <summary>
        /// 执行日志清理
        /// </summary>
        Task CleanupAsync();
    }
}