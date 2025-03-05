//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerMonitorService.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : 服务器监控服务实现
//===================================================================

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Lean.Hbt.Application.Dtos.Audit;
using Lean.Hbt.Common.Utils;

namespace Lean.Hbt.Application.Services.Audit;

/// <summary>
/// 服务器监控服务实现
/// </summary>
public class HbtServerMonitorService : IHbtServerMonitorService
{
    private readonly PerformanceCounter _cpuCounter;
    private readonly PerformanceCounter _memCounter;
    private readonly DriveInfo[] _drives;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtServerMonitorService()
    {
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        _memCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        _drives = DriveInfo.GetDrives();
    }

    /// <summary>
    /// 获取服务器基本信息
    /// </summary>
    /// <returns>服务器基本信息</returns>
    public async Task<HbtServerMonitorDto> GetServerInfoAsync()
    {
        return await Task.Run(() =>
        {
            var cpuUsage = _cpuCounter.NextValue();
            var memoryUsage = _memCounter.NextValue();
            var totalMemory = HbtServerMonitorUtils.GetTotalPhysicalMemory();
            var usedMemory = totalMemory * (memoryUsage / 100);

            var totalDiskSpace = _drives.Sum(d => d.TotalSize);
            var usedDiskSpace = _drives.Sum(d => d.TotalSize - d.AvailableFreeSpace);
            var diskUsage = (double)usedDiskSpace / totalDiskSpace * 100;

            var dotNetInfo = HbtServerMonitorUtils.GetDotNetRuntimeInfo();

            return new HbtServerMonitorDto
            {
                CpuUsage = Math.Round(cpuUsage, 2),
                TotalMemory = Math.Round(totalMemory / 1024.0 / 1024.0 / 1024.0, 2), // Convert to GB
                UsedMemory = Math.Round(usedMemory / 1024.0 / 1024.0 / 1024.0, 2), // Convert to GB
                MemoryUsage = Math.Round(memoryUsage, 2),
                TotalDiskSpace = Math.Round(totalDiskSpace / 1024.0 / 1024.0 / 1024.0, 2), // Convert to GB
                UsedDiskSpace = Math.Round(usedDiskSpace / 1024.0 / 1024.0 / 1024.0, 2), // Convert to GB
                DiskUsage = Math.Round(diskUsage, 2),
                OsName = RuntimeInformation.OSDescription,
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                OsVersion = Environment.OSVersion.ToString(),
                ProcessorName = HbtServerMonitorUtils.GetProcessorName(),
                ProcessorCount = Environment.ProcessorCount,
                SystemStartTime = HbtServerMonitorUtils.GetSystemStartTime(),
                SystemUptime = Math.Round((DateTime.Now - HbtServerMonitorUtils.GetSystemStartTime()).TotalDays, 2),
                DotNetRuntimeVersion = dotNetInfo.RuntimeVersion,
                ClrVersion = dotNetInfo.ClrVersion,
                DotNetRuntimeDirectory = dotNetInfo.RuntimeDirectory
            };
        });
    }

    /// <summary>
    /// 获取进程列表
    /// </summary>
    /// <returns>进程列表</returns>
    public async Task<List<HbtProcessDto>> GetProcessListAsync()
    {
        return await Task.Run(() =>
        {
            var processes = Process.GetProcesses();
            return processes.Select(p => new HbtProcessDto
            {
                ProcessId = p.Id,
                ProcessName = p.ProcessName,
                Description = HbtServerMonitorUtils.GetProcessDescription(p),
                CpuUsage = HbtServerMonitorUtils.GetProcessCpuUsage(p),
                MemoryUsage = Math.Round(p.WorkingSet64 / 1024.0 / 1024.0, 2), // Convert to MB
                StartTime = p.StartTime,
                RunningTime = Math.Round((DateTime.Now - p.StartTime).TotalMinutes, 2)
            }).ToList();
        });
    }

    /// <summary>
    /// 获取网络信息
    /// </summary>
    /// <returns>网络信息列表</returns>
    public async Task<List<HbtNetworkDto>> GetNetworkInfoAsync()
    {
        return await Task.Run(() =>
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            return networkInterfaces.Select(ni =>
            {
                var stats = ni.GetIPv4Statistics();
                var rates = HbtServerMonitorUtils.GetNetworkInterfaceRates(ni);
                return new HbtNetworkDto
                {
                    AdapterName = ni.Name,
                    MacAddress = ni.GetPhysicalAddress().ToString(),
                    IpAddress = HbtServerMonitorUtils.GetNetworkInterfaceIpAddress(ni),
                    IpLocation = HbtIpLocationUtils.GetIpLocation(HbtServerMonitorUtils.GetNetworkInterfaceIpAddress(ni)),
                    BytesSent = stats.BytesSent,
                    BytesReceived = stats.BytesReceived,
                    SendRate = rates.SendRate,
                    ReceiveRate = rates.ReceiveRate
                };
            }).ToList();
        });
    }

    /// <summary>
    /// 获取系统服务列表
    /// </summary>
    /// <returns>系统服务列表</returns>
    public async Task<List<HbtServiceDto>> GetServiceListAsync()
    {
        return await Task.Run(() =>
        {
            var services = ServiceController.GetServices();
            return services.Select(s => new HbtServiceDto
            {
                ServiceName = s.ServiceName,
                DisplayName = s.DisplayName,
                ServiceType = s.ServiceType.ToString(),
                Status = 0,
                StartType = HbtServerMonitorUtils.GetServiceStartType(s),
                Account = HbtServerMonitorUtils.GetServiceAccount(s)
            }).ToList();
        });
    }
}