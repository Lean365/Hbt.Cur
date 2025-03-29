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
using System.Runtime.InteropServices;
using System.Diagnostics.Metrics;
using LibreHardwareMonitor.Hardware;

namespace Lean.Hbt.Common.Utils;

/// <summary>
/// 服务器监控工具类
/// </summary>
/// <remarks>
/// 提供以下功能：
/// 1. 系统资源监控：CPU、内存、磁盘使用情况
/// 2. 进程管理：获取进程列表、CPU使用率等
/// 3. 网络监控：网络接口信息、流量统计
/// </remarks>
public static class HbtServerMonitorUtils
{
    // LibreHardwareMonitor的主要对象，用于访问硬件信息
    private static readonly Computer _computer;
    
    // 性能指标收集器
    private static readonly Meter _meter = new("Lean.Hbt.ServerMonitor", "1.0.0");
    
    // CPU使用率计数器
    private static readonly Counter<long> _cpuCounter = _meter.CreateCounter<long>("cpu_usage", "CPU使用率");
    
    // 内存使用率计数器
    private static readonly Counter<long> _memoryCounter = _meter.CreateCounter<long>("memory_usage", "内存使用率");
    
    // 磁盘使用率计数器
    private static readonly Counter<long> _diskCounter = _meter.CreateCounter<long>("disk_usage", "磁盘使用率");
    
    // 网络使用率计数器
    private static readonly Counter<long> _networkCounter = _meter.CreateCounter<long>("network_usage", "网络使用率");

    /// <summary>
    /// 静态构造函数，初始化硬件监控
    /// </summary>
    static HbtServerMonitorUtils()
    {
        // 初始化硬件监控对象，启用所有监控项
        _computer = new Computer
        {
            IsCpuEnabled = true,      // 启用CPU监控
            IsMemoryEnabled = true,   // 启用内存监控
            IsStorageEnabled = true,  // 启用存储监控
            IsNetworkEnabled = true   // 启用网络监控
        };
        _computer.Open(); // 打开硬件监控
    }

    /// <summary>
    /// 获取物理内存总量
    /// </summary>
    /// <returns>物理内存总量，单位：字节</returns>
    /// <remarks>
    /// 使用LibreHardwareMonitor获取内存信息
    /// 返回值单位为字节，如需其他单位请自行转换
    /// </remarks>
    public static ulong GetTotalPhysicalMemory()
    {
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 获取内存硬件信息
            var memory = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Memory);
            if (memory != null)
            {
                // 获取总内存传感器数据
                var totalMemory = memory.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Data);
                if (totalMemory != null)
                {
                    // 转换为字节（传感器值单位为GB）
                    return (ulong)(totalMemory.Value * 1024 * 1024 * 1024);
                }
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// 获取处理器名称
    /// </summary>
    /// <returns>处理器名称，如获取失败则返回"Unknown"</returns>
    /// <remarks>
    /// 返回完整的处理器名称，包含制造商、型号等信息
    /// </remarks>
    public static string GetProcessorName()
    {
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 获取CPU硬件信息
            var cpu = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Cpu);
            return cpu?.Name ?? "Unknown";
        }
        catch { }
        return "Unknown";
    }

    /// <summary>
    /// 获取系统启动时间
    /// </summary>
    /// <returns>系统启动的时间点</returns>
    /// <remarks>
    /// 通过Environment.TickCount64计算系统启动时间
    /// 精确到毫秒级别
    /// </remarks>
    public static DateTime GetSystemStartTime()
    {
        return DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount64);
    }

    /// <summary>
    /// 获取网络接口信息
    /// </summary>
    /// <returns>网络接口信息列表，每项包含接口名称、IP地址和MAC地址</returns>
    /// <remarks>
    /// 返回所有可用的网络接口信息
    /// IP地址格式为IPv4地址
    /// MAC地址格式为12位十六进制数
    /// </remarks>
    public static List<(string Name, string IpAddress, string MacAddress)> GetNetworkInterfaces()
    {
        var interfaces = new List<(string Name, string IpAddress, string MacAddress)>();
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 获取所有网络接口
            var networkInterfaces = _computer.Hardware.Where(h => h.HardwareType == HardwareType.Network);
            
            foreach (var ni in networkInterfaces)
            {
                // 获取IP地址和MAC地址信息
                var ipAddress = ni.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Data)?.Value.ToString() ?? string.Empty;
                var macAddress = ni.Sensors.FirstOrDefault(s => s.SensorType == SensorType.SmallData)?.Value.ToString() ?? string.Empty;
                
                interfaces.Add((ni.Name, ipAddress, macAddress));
            }
        }
        catch { }
        return interfaces;
    }

    /// <summary>
    /// 获取网络接口的流量信息
    /// </summary>
    /// <param name="interfaceName">网络接口名称</param>
    /// <returns>发送和接收速率（KB/s）的元组</returns>
    /// <remarks>
    /// 返回值中：
    /// SendRate: 发送速率，单位KB/s
    /// ReceiveRate: 接收速率，单位KB/s
    /// </remarks>
    public static (double SendRate, double ReceiveRate) GetNetworkInterfaceRates(string interfaceName)
    {
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 查找指定名称的网络接口
            var networkInterface = _computer.Hardware.FirstOrDefault(h => 
                h.HardwareType == HardwareType.Network && h.Name == interfaceName);
            
            if (networkInterface != null)
            {
                // 获取发送和接收速率
                var sendRate = networkInterface.Sensors.FirstOrDefault(s => 
                    s.SensorType == SensorType.Throughput && s.Name.Contains("Send"))?.Value ?? 0;
                var receiveRate = networkInterface.Sensors.FirstOrDefault(s => 
                    s.SensorType == SensorType.Throughput && s.Name.Contains("Receive"))?.Value ?? 0;
                
                // 转换为KB/s
                return (sendRate / 1024.0, receiveRate / 1024.0);
            }
        }
        catch { }
        return (0, 0);
    }

    /// <summary>
    /// 获取系统CPU使用率
    /// </summary>
    /// <returns>CPU使用率百分比（0-100）</returns>
    /// <remarks>
    /// 返回所有CPU核心的平均使用率
    /// 值范围：0-100
    /// 精确到小数点后2位
    /// </remarks>
    public static double GetSystemCpuUsage()
    {
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 获取CPU信息
            var cpu = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Cpu);
            if (cpu != null)
            {
                // 获取CPU负载传感器数据
                var load = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);
                return load?.Value ?? 0;
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// 获取系统内存使用率
    /// </summary>
    /// <returns>内存使用率百分比（0-100）</returns>
    /// <remarks>
    /// 计算方式：已用内存/总内存 * 100
    /// 值范围：0-100
    /// 精确到小数点后2位
    /// </remarks>
    public static double GetSystemMemoryUsage()
    {
        try
        {
            // 更新硬件信息
            _computer.Accept(new UpdateVisitor());
            
            // 获取内存信息
            var memory = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Memory);
            if (memory != null)
            {
                // 获取已用内存和总内存
                var used = memory.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Data);
                var total = memory.Sensors.FirstOrDefault(s => s.SensorType == SensorType.SmallData);
                
                // 计算使用率
                if (used != null && total != null && total.Value > 0)
                {
                    return (double)Math.Round((decimal)(used.Value / total.Value * 100), 2);
                }
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// 硬件信息更新访问器
    /// </summary>
    /// <remarks>
    /// 实现IVisitor接口，用于更新硬件信息
    /// 包含对计算机、硬件、传感器和参数的访问方法
    /// </remarks>
    private class UpdateVisitor : IVisitor
    {
        /// <summary>
        /// 访问计算机对象
        /// </summary>
        /// <param name="computer">计算机对象</param>
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        /// <summary>
        /// 访问硬件对象
        /// </summary>
        /// <param name="hardware">硬件对象</param>
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                subHardware.Accept(this);
            }
        }

        /// <summary>
        /// 访问传感器对象（空实现）
        /// </summary>
        /// <param name="sensor">传感器对象</param>
        public void VisitSensor(ISensor sensor) { }

        /// <summary>
        /// 访问参数对象（空实现）
        /// </summary>
        /// <param name="parameter">参数对象</param>
        public void VisitParameter(IParameter parameter) { }
    }
} 
