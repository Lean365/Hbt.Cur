//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerDto.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器信息DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Audit;

/// <summary>
/// 服务器信息DTO
/// </summary>
public class HbtServerMonitorDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public HbtServerMonitorDto()
    {
        OsName = "Unknown";
        OsArchitecture = "Unknown";
        OsVersion = "Unknown";
        ProcessorName = "Unknown";
        ProcessorCount = 0;
        SystemStartTime = DateTime.Now;
        SystemUptime = 0;
        DotNetRuntimeVersion = string.Empty;
        ClrVersion = string.Empty;
        DotNetRuntimeDirectory = string.Empty;
    }

    /// <summary>
    /// CPU使用率
    /// </summary>
    public double CpuUsage { get; set; }

    /// <summary>
    /// 总内存(GB)
    /// </summary>
    public double TotalMemory { get; set; }

    /// <summary>
    /// 已用内存(GB)
    /// </summary>
    public double UsedMemory { get; set; }

    /// <summary>
    /// 内存使用率
    /// </summary>
    public double MemoryUsage { get; set; }

    /// <summary>
    /// 总磁盘空间(GB)
    /// </summary>
    public double TotalDiskSpace { get; set; }

    /// <summary>
    /// 已用磁盘空间(GB)
    /// </summary>
    public double UsedDiskSpace { get; set; }

    /// <summary>
    /// 磁盘使用率
    /// </summary>
    public double DiskUsage { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [Required]
    public required string OsName { get; set; }

    /// <summary>
    /// 系统架构
    /// </summary>
    [Required]
    public required string OsArchitecture { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    [Required]
    public required string OsVersion { get; set; }

    /// <summary>
    /// 处理器信息
    /// </summary>
    [Required]
    public required string ProcessorName { get; set; }

    /// <summary>
    /// 处理器核心数
    /// </summary>
    public int ProcessorCount { get; set; }

    /// <summary>
    /// 系统启动时间
    /// </summary>
    public DateTime SystemStartTime { get; set; }

    /// <summary>
    /// 系统运行时间(天)
    /// </summary>
    public double SystemUptime { get; set; }

    /// <summary>
    /// .NET运行时版本
    /// </summary>
    [Required]
    public required string DotNetRuntimeVersion { get; set; }

    /// <summary>
    /// CLR版本
    /// </summary>
    [Required]
    public required string ClrVersion { get; set; }

    /// <summary>
    /// .NET运行时目录
    /// </summary>
    [Required]
    public required string DotNetRuntimeDirectory { get; set; }
}

/// <summary>
/// 进程信息DTO
/// </summary>
public class HbtProcessDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public HbtProcessDto()
    {
        ProcessName = string.Empty;
        Description = string.Empty;
        CpuUsage = 0;
        MemoryUsage = 0;
        StartTime = DateTime.Now;
        RunningTime = 0;
    }

    /// <summary>
    /// 进程ID
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// 进程名称
    /// </summary>
    [Required]
    public required string ProcessName { get; set; }

    /// <summary>
    /// 进程描述
    /// </summary>
    [Required]
    public required string Description { get; set; }

    /// <summary>
    /// CPU使用率
    /// </summary>
    public double CpuUsage { get; set; }

    /// <summary>
    /// 内存使用(MB)
    /// </summary>
    public double MemoryUsage { get; set; }

    /// <summary>
    /// 启动时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 运行时间(分钟)
    /// </summary>
    public double RunningTime { get; set; }
}

/// <summary>
/// 网络信息DTO
/// </summary>
public class HbtNetworkDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public HbtNetworkDto()
    {
        AdapterName = string.Empty;
        MacAddress = string.Empty;
        IpAddress = string.Empty;
        IpLocation = string.Empty;
        BytesSent = 0;
        BytesReceived = 0;
        SendRate = 0;
        ReceiveRate = 0;
    }

    /// <summary>
    /// 网卡名称
    /// </summary>
    [Required]
    public required string AdapterName { get; set; }

    /// <summary>
    /// MAC地址
    /// </summary>
    [Required]
    public required string MacAddress { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [Required]
    public required string IpAddress { get; set; }

    /// <summary>
    /// IP地址位置信息
    /// </summary>
    [Required]
    public required string IpLocation { get; set; }

    /// <summary>
    /// 发送字节数
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// 接收字节数
    /// </summary>
    public long BytesReceived { get; set; }

    /// <summary>
    /// 发送速率(KB/s)
    /// </summary>
    public double SendRate { get; set; }

    /// <summary>
    /// 接收速率(KB/s)
    /// </summary>
    public double ReceiveRate { get; set; }
}

/// <summary>
/// 系统服务信息DTO
/// </summary>
public class HbtServiceDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public HbtServiceDto()
    {
        ServiceName = string.Empty;
        DisplayName = string.Empty;
        ServiceType = string.Empty;
        Status = 0;
        StartType = string.Empty;
        Account = string.Empty;
    }

    /// <summary>
    /// 服务名称
    /// </summary>
    [Required]
    public required string ServiceName { get; set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    [Required]
    public required string DisplayName { get; set; }

    /// <summary>
    /// 服务类型
    /// </summary>
    [Required]
    public required string ServiceType { get; set; }

    /// <summary>
    /// 服务状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 启动类型
    /// </summary>
    [Required]
    public required string StartType { get; set; }

    /// <summary>
    /// 运行账户
    /// </summary>
    [Required]
    public required string Account { get; set; }
} 