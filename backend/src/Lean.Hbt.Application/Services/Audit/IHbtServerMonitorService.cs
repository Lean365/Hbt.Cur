//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtServerMonitorService.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Audit;

namespace Lean.Hbt.Application.Services.Audit;

/// <summary>
/// 服务器监控服务接口
/// </summary>
public interface IHbtServerMonitorService
{
    /// <summary>
    /// 获取服务器基本信息
    /// </summary>
    /// <returns>服务器基本信息</returns>
    Task<HbtServerMonitorDto> GetServerInfoAsync();

    /// <summary>
    /// 获取进程列表
    /// </summary>
    /// <returns>进程列表</returns>
    Task<List<HbtProcessDto>> GetProcessListAsync();

    /// <summary>
    /// 获取网络信息
    /// </summary>
    /// <returns>网络信息列表</returns>
    Task<List<HbtNetworkDto>> GetNetworkInfoAsync();

    /// <summary>
    /// 获取系统服务列表
    /// </summary>
    /// <returns>系统服务列表</returns>
    Task<List<HbtServiceDto>> GetServiceListAsync();
} 