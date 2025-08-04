//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLocalIpUtils.cs
// 创建者 : Claude
// 创建时间: 2024-12-01 14:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : 本机内网地址获取工具类
//===================================================================

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Hbt.Cur.Common.Utils;

/// <summary>
/// 本机内网地址获取工具类
/// </summary>
/// <remarks>
/// 提供获取本机真实内网IP地址的功能：
/// 1. 获取所有网络接口的内网IP
/// 2. 过滤掉回环地址和无效地址
/// 3. 优先返回活跃的内网IP地址
/// </remarks>
public static class HbtLocalIpUtils
{
    /// <summary>
    /// 获取本机内网IP地址列表
    /// </summary>
    /// <returns>内网IP地址列表</returns>
    public static List<string> GetLocalIpAddresses()
    {
        var localIps = new List<string>();
        
        try
        {
            // 获取所有网络接口
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (var networkInterface in networkInterfaces)
            {
                // 只处理活跃的以太网和无线网卡
                if (networkInterface.OperationalStatus != OperationalStatus.Up ||
                    (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ethernet &&
                     networkInterface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211))
                {
                    continue;
                }
                
                // 获取IP属性
                var ipProperties = networkInterface.GetIPProperties();
                
                // 获取单播地址
                foreach (var unicastAddress in ipProperties.UnicastAddresses)
                {
                    var ip = unicastAddress.Address;
                    
                    // 只处理IPv4地址
                    if (ip.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    
                    // 过滤掉回环地址
                    if (IPAddress.IsLoopback(ip))
                        continue;
                    
                    // 检查是否为内网地址
                    if (IsPrivateIp(ip))
                    {
                        localIps.Add(ip.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // 记录错误但不抛出异常
            System.Diagnostics.Debug.WriteLine($"获取本机内网IP失败: {ex.Message}");
        }
        
        return localIps;
    }
    
    /// <summary>
    /// 获取本机首选内网IP地址
    /// </summary>
    /// <returns>首选内网IP地址，如果没有则返回空字符串</returns>
    public static string GetPreferredLocalIpAddress()
    {
        var localIps = GetLocalIpAddresses();
        
        if (localIps.Count == 0)
            return string.Empty;
        
        // 优先返回192.168.x.x地址
        var preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("192.168."));
        if (!string.IsNullOrEmpty(preferredIp))
            return preferredIp;
        
        // 其次返回10.x.x.x地址
        preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("10."));
        if (!string.IsNullOrEmpty(preferredIp))
            return preferredIp;
        
        // 最后返回172.16-31.x.x地址
        preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("172."));
        if (!string.IsNullOrEmpty(preferredIp))
            return preferredIp;
        
        // 如果都没有，返回第一个
        return localIps.First();
    }
    
    /// <summary>
    /// 判断是否为内网IP地址
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <returns>是否为内网IP</returns>
    private static bool IsPrivateIp(IPAddress ip)
    {
        var bytes = ip.GetAddressBytes();
        
        // 10.0.0.0 - 10.255.255.255
        if (bytes[0] == 10)
            return true;
        
        // 172.16.0.0 - 172.31.255.255
        if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31)
            return true;
        
        // 192.168.0.0 - 192.168.255.255
        if (bytes[0] == 192 && bytes[1] == 168)
            return true;
        
        return false;
    }
    
    /// <summary>
    /// 获取本机主机名
    /// </summary>
    /// <returns>主机名</returns>
    public static string GetHostName()
    {
        try
        {
            return Dns.GetHostName();
        }
        catch
        {
            return "Unknown";
        }
    }
    
    /// <summary>
    /// 获取本机网络信息
    /// </summary>
    /// <returns>网络信息字典</returns>
    public static Dictionary<string, object> GetLocalNetworkInfo()
    {
        var networkInfo = new Dictionary<string, object>
        {
            ["HostName"] = GetHostName(),
            ["LocalIps"] = GetLocalIpAddresses(),
            ["PreferredIp"] = GetPreferredLocalIpAddress()
        };
        
        return networkInfo;
    }
} 