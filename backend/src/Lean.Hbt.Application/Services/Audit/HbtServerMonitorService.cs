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
public class HbtServerMonitorService : IHbtServerMonitorService, IDisposable
{
#if WINDOWS
    private readonly PerformanceCounter? _cpuCounter;
    private readonly PerformanceCounter? _memCounter;
#endif
    private readonly Process _process;
    private readonly DriveInfo[] _drives;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtServerMonitorService()
    {
#if WINDOWS
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try 
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _memCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            }
            catch (Exception ex)
            {
                // 如果性能计数器初始化失败，记录错误但不中断服务
                _cpuCounter = null;
                _memCounter = null;
            }
        }
#endif
        _process = Process.GetCurrentProcess();
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
            double cpuUsage = 0;
            double memoryUsage = 0;
            
#if WINDOWS
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && _cpuCounter != null && _memCounter != null)
            {
                try
                {
                    cpuUsage = _cpuCounter.NextValue();
                    memoryUsage = _memCounter.NextValue();
                }
                catch
                {
                    // 如果读取性能计数器失败，使用替代方法
                    cpuUsage = GetCpuUsageAlternative();
                    memoryUsage = GetMemoryUsageAlternative();
                }
            }
            else
#endif
            {
                // 非Windows平台或性能计数器不可用时的替代实现
                cpuUsage = GetCpuUsageAlternative();
                memoryUsage = GetMemoryUsageAlternative();
            }

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
    /// 获取CPU使用率的替代实现
    /// </summary>
    private double GetCpuUsageAlternative()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Linux平台下读取/proc/stat获取CPU使用率
                var cpuUsage = File.ReadAllText("/proc/stat")
                    .Split('\n')
                    .FirstOrDefault(l => l.StartsWith("cpu "))
                    ?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Take(7)
                    .Select(long.Parse)
                    .ToArray();

                if (cpuUsage != null)
                {
                    var total = cpuUsage.Sum();
                    var idle = cpuUsage[3];
                    return Math.Round((1 - (double)idle / total) * 100, 2);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS平台下使用top命令获取CPU使用率
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "top",
                        Arguments = "-l 1 -n 0",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // 解析top命令输出，查找CPU usage行
                var cpuLine = output.Split('\n')
                    .FirstOrDefault(l => l.Contains("CPU usage:"));
                
                if (cpuLine != null)
                {
                    var parts = cpuLine.Split(':')[1].Trim().Split(',');
                    var user = double.Parse(parts[0].Trim().Replace("%", ""));
                    var sys = double.Parse(parts[1].Trim().Replace("%", ""));
                    return Math.Round(user + sys, 2);
                }
            }
            
            // 其他平台或读取失败时返回进程CPU时间作为估算值
            return Math.Round(_process.TotalProcessorTime.TotalMilliseconds / 
                (Environment.ProcessorCount * DateTime.Now.Subtract(_process.StartTime).TotalMilliseconds) * 100, 2);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 获取内存使用率的替代实现
    /// </summary>
    private double GetMemoryUsageAlternative()
    {
        try
        {
            var totalMemory = HbtServerMonitorUtils.GetTotalPhysicalMemory();
            if (totalMemory <= 0) return 0;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Linux平台下读取/proc/meminfo获取内存使用情况
                var memInfo = File.ReadAllText("/proc/meminfo");
                var memTotal = GetMemInfoValue(memInfo, "MemTotal:");
                var memAvailable = GetMemInfoValue(memInfo, "MemAvailable:");
                
                if (memTotal > 0)
                {
                    return Math.Round(((double)(memTotal - memAvailable) / memTotal) * 100, 2);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS平台下使用vm_stat命令获取内存使用情况
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "vm_stat",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // 解析vm_stat输出
                var lines = output.Split('\n');
                var pageSize = 4096; // 默认页大小
                long freePages = 0, activePages = 0, inactivePages = 0, wiredPages = 0;

                foreach (var line in lines)
                {
                    if (line.Contains("Pages free:"))
                        freePages = ParseVmStatValue(line);
                    else if (line.Contains("Pages active:"))
                        activePages = ParseVmStatValue(line);
                    else if (line.Contains("Pages inactive:"))
                        inactivePages = ParseVmStatValue(line);
                    else if (line.Contains("Pages wired down:"))
                        wiredPages = ParseVmStatValue(line);
                }

                var totalPages = freePages + activePages + inactivePages + wiredPages;
                var usedPages = activePages + wiredPages;

                if (totalPages > 0)
                {
                    return Math.Round((double)usedPages / totalPages * 100, 2);
                }
            }

            // 其他平台或读取失败时返回进程内存占用作为估算值
            return Math.Round((double)_process.WorkingSet64 / totalMemory * 100, 2);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 解析vm_stat命令输出中的数值
    /// </summary>
    private long ParseVmStatValue(string line)
    {
        try
        {
            return long.Parse(line.Split(':')[1].Trim().Replace(".", ""));
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 从/proc/meminfo中获取特定值
    /// </summary>
    private long GetMemInfoValue(string memInfo, string key)
    {
        try
        {
            return long.Parse(memInfo
                .Split('\n')
                .FirstOrDefault(l => l.StartsWith(key))?
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1] ?? "0");
        }
        catch
        {
            return 0;
        }
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
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
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
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linux下使用systemctl命令获取服务列表
                    using var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "systemctl",
                            Arguments = "list-units --type=service --all",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output.Split('\n')
                        .Skip(1) // 跳过标题行
                        .Where(line => !string.IsNullOrWhiteSpace(line) && line.Contains(".service"))
                        .Select(line =>
                        {
                            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            return new HbtServiceDto
                            {
                                ServiceName = parts[0].Replace(".service", ""),
                                DisplayName = parts[0],
                                ServiceType = "systemd",
                                Status = parts.Length > 3 ? (parts[3] == "active" ? 0 : 1) : 1,
                                StartType = "undefined",
                                Account = "system"
                            };
                        }).ToList();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // macOS下使用launchctl命令获取服务列表
                    using var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "launchctl",
                            Arguments = "list",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output.Split('\n')
                        .Skip(1) // 跳过标题行
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line =>
                        {
                            var parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            return new HbtServiceDto
                            {
                                ServiceName = parts.Length > 2 ? parts[2] : "unknown",
                                DisplayName = parts.Length > 2 ? parts[2] : "unknown",
                                ServiceType = "launchd",
                                Status = parts.Length > 0 && !string.IsNullOrEmpty(parts[0]) ? 0 : 1,
                                StartType = "undefined",
                                Account = "system"
                            };
                        }).ToList();
                }

                return new List<HbtServiceDto>();
            }
            catch (Exception ex)
            {
                return new List<HbtServiceDto>();
            }
        });
    }

    public void Dispose()
    {
#if WINDOWS
        _cpuCounter?.Dispose();
        _memCounter?.Dispose();
#endif
        _process.Dispose();
    }
}