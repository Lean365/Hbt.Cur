//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerMonitorUtils.cs
// 创建者 : Claude
// 创建时间: 2024-02-18 11:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : 服务器监控工具类，提供系统资源监控、进程管理、网络监控等功能
//===================================================================

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Net;

namespace Lean.Hbt.Common.Utils;

/// <summary>
/// 服务器监控工具类
/// </summary>
/// <remarks>
/// 提供以下功能：
/// 1. 系统资源监控：CPU、内存、磁盘使用情况
/// 2. 进程管理：获取进程列表、CPU使用率等
/// 3. 网络监控：网络接口信息、流量统计
/// 4. 系统服务管理：服务状态、启动类型等
/// </remarks>
public static class HbtServerMonitorUtils
{
    /// <summary>
    /// Windows内存状态结构体
    /// </summary>
    /// <remarks>
    /// 用于与Windows API交互，获取系统内存信息
    /// 参考：https://learn.microsoft.com/windows/win32/api/sysinfoapi/ns-sysinfoapi-memorystatusex
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;                    // 结构体大小
        public uint dwMemoryLoad;               // 内存使用百分比
        public ulong ullTotalPhys;              // 物理内存总量
        public ulong ullAvailPhys;              // 可用物理内存
        public ulong ullTotalPageFile;          // 页面文件总量
        public ulong ullAvailPageFile;          // 可用页面文件
        public ulong ullTotalVirtual;           // 虚拟内存总量
        public ulong ullAvailVirtual;           // 可用虚拟内存
        public ulong ullAvailExtendedVirtual;   // 保留，始终为0
    }

    /// <summary>
    /// Windows API：获取系统内存状态
    /// </summary>
    /// <param name="lpBuffer">内存状态结构体引用</param>
    /// <returns>操作是否成功</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    /// <summary>
    /// 获取物理内存总量(字节)
    /// </summary>
    /// <returns>物理内存总量，单位：字节</returns>
    /// <remarks>
    /// 支持Windows和Linux系统
    /// - Windows：通过GlobalMemoryStatusEx API获取
    /// - Linux：通过读取/proc/meminfo文件获取
    /// </remarks>
    public static ulong GetTotalPhysicalMemory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetWindowsMemoryInfo();
        }
        return GetUnixMemoryInfo();
    }

    /// <summary>
    /// 获取Windows系统内存信息
    /// </summary>
    /// <returns>物理内存总量，单位：字节</returns>
    private static ulong GetWindowsMemoryInfo()
    {
        var memStatus = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX)) };
        if (!GlobalMemoryStatusEx(ref memStatus))
        {
            return 0;
        }
        return memStatus.ullTotalPhys;
    }

    /// <summary>
    /// 获取Unix/Linux系统内存信息
    /// </summary>
    /// <returns>物理内存总量，单位：字节</returns>
    private static ulong GetUnixMemoryInfo()
    {
        try
        {
            var info = File.ReadAllText("/proc/meminfo");
            var lines = info.Split('\n');
            var totalLine = lines.FirstOrDefault(l => l.StartsWith("MemTotal:"));
            if (totalLine != null)
            {
                var parts = totalLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (ulong.TryParse(parts[1], out ulong totalKb))
                {
                    return totalKb * 1024; // 将KB转换为字节
                }
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// 获取处理器名称
    /// </summary>
    /// <returns>处理器名称</returns>
    /// <remarks>
    /// 在Windows系统下，从注册表读取处理器信息
    /// 在其他系统下返回"Unknown"
    /// </remarks>
    public static string GetProcessorName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            return key?.GetValue("ProcessorNameString")?.ToString() ?? "Unknown";
        }
        return "Unknown";
    }

    /// <summary>
    /// 获取系统启动时间
    /// </summary>
    /// <returns>系统启动的时间点</returns>
    /// <remarks>
    /// 通过Environment.TickCount64获取系统运行时间（毫秒）
    /// 然后用当前时间减去运行时间得到启动时间
    /// </remarks>
    public static DateTime GetSystemStartTime()
    {
        return DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount64);
    }

    /// <summary>
    /// 获取进程描述信息
    /// </summary>
    /// <param name="process">进程对象</param>
    /// <returns>进程的描述信息</returns>
    /// <remarks>
    /// 从进程的主模块文件版本信息中获取描述
    /// 如果无法获取则返回空字符串
    /// </remarks>
    public static string GetProcessDescription(Process process)
    {
        try
        {
            return process.MainModule?.FileVersionInfo.FileDescription ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 获取进程的CPU使用率
    /// </summary>
    /// <param name="process">进程对象</param>
    /// <returns>CPU使用率百分比</returns>
    /// <remarks>
    /// 计算方法：
    /// 1. 获取进程当前的处理器时间
    /// 2. 等待100ms
    /// 3. 再次获取处理器时间
    /// 4. 计算时间差除以(处理器核心数 * 采样时间)得到使用率
    /// </remarks>
    public static double GetProcessCpuUsage(Process process)
    {
        try
        {
            TimeSpan totalProcessorTime = process.TotalProcessorTime;
            Thread.Sleep(100); // 等待100ms以计算使用率
            double cpuUsage = (process.TotalProcessorTime - totalProcessorTime).TotalMilliseconds / (Environment.ProcessorCount * 100.0);
            return Math.Round(cpuUsage, 2);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 获取网络接口的IPv4地址
    /// </summary>
    /// <param name="ni">网络接口对象</param>
    /// <returns>IPv4地址字符串</returns>
    /// <remarks>
    /// 从网络接口的单播地址列表中
    /// 查找第一个IPv4地址并返回
    /// </remarks>
    public static string GetNetworkInterfaceIpAddress(NetworkInterface ni)
    {
        var ipProps = ni.GetIPProperties();
        var ipv4Address = ipProps.UnicastAddresses
            .FirstOrDefault(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        return ipv4Address?.Address.ToString() ?? string.Empty;
    }

    /// <summary>
    /// 获取Windows服务的启动类型
    /// </summary>
    /// <param name="sc">服务控制器对象</param>
    /// <returns>服务启动类型：Automatic(自动)、Manual(手动)、Disabled(禁用)</returns>
    /// <remarks>
    /// 从注册表中读取服务的Start值：
    /// 2 = 自动
    /// 3 = 手动
    /// 4 = 禁用
    /// </remarks>
    public static string GetServiceStartType(ServiceController sc)
    {
        if (!OperatingSystem.IsWindows())
            return "Unknown";

        try
        {
            using var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{sc.ServiceName}");
            var startType = key?.GetValue("Start");
            return startType switch
            {
                2 => "Automatic",
                3 => "Manual", 
                4 => "Disabled",
                _ => "Unknown"
            };
        }
        catch
        {
            return "Unknown";
        }
    }

    /// <summary>
    /// 获取Windows服务的运行账户
    /// </summary>
    /// <param name="sc">服务控制器对象</param>
    /// <returns>服务运行账户名称</returns>
    /// <remarks>
    /// 从注册表中读取服务的ObjectName值
    /// 该值表示服务运行时使用的账户
    /// </remarks>
    public static string GetServiceAccount(ServiceController sc)
    {
        if (!OperatingSystem.IsWindows())
            return "Unknown";

        try
        {
            using var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{sc.ServiceName}");
            return key?.GetValue("ObjectName")?.ToString() ?? "Unknown";
        }
        catch
        {
            return "Unknown";
        }
    }

    /// <summary>
    /// 网络接口流量信息缓存
    /// </summary>
    private static readonly Dictionary<string, (long BytesSent, long BytesReceived, DateTime Timestamp)> _networkStatsCache = new();

    /// <summary>
    /// 获取网络接口的实时流量信息
    /// </summary>
    /// <param name="ni">网络接口对象</param>
    /// <returns>发送和接收速率（KB/s）</returns>
    /// <remarks>
    /// 计算方法：
    /// 1. 获取当前流量数据
    /// 2. 与上次数据比较计算差值
    /// 3. 除以时间间隔得到速率
    /// 首次调用时返回0
    /// </remarks>
    public static (double SendRate, double ReceiveRate) GetNetworkInterfaceRates(NetworkInterface ni)
    {
        var stats = ni.GetIPv4Statistics();
        var now = DateTime.Now;

        // 如果是首次获取该接口的数据
        if (!_networkStatsCache.TryGetValue(ni.Id, out var lastStats))
        {
            _networkStatsCache[ni.Id] = (stats.BytesSent, stats.BytesReceived, now);
            return (0, 0);
        }

        // 计算时间间隔（秒）
        var interval = (now - lastStats.Timestamp).TotalSeconds;
        if (interval <= 0) return (0, 0);

        // 计算速率（KB/s）
        var sendRate = (stats.BytesSent - lastStats.BytesSent) / interval / 1024.0;
        var receiveRate = (stats.BytesReceived - lastStats.BytesReceived) / interval / 1024.0;

        // 更新缓存
        _networkStatsCache[ni.Id] = (stats.BytesSent, stats.BytesReceived, now);

        return (Math.Round(sendRate, 2), Math.Round(receiveRate, 2));
    }

    /// <summary>
    /// 获取.NET运行时版本信息
    /// </summary>
    /// <returns>.NET运行时版本信息</returns>
    /// <remarks>
    /// 获取当前服务器运行的.NET版本信息，包括：
    /// 1. .NET运行时版本
    /// 2. CLR版本
    /// 3. 运行时目录
    /// </remarks>
    public static (string RuntimeVersion, string ClrVersion, string RuntimeDirectory) GetDotNetRuntimeInfo()
    {
        var runtimeVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
        var clrVersion = Environment.Version.ToString();
        var runtimeDirectory = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        
        return (runtimeVersion, clrVersion, runtimeDirectory);
    }
} 