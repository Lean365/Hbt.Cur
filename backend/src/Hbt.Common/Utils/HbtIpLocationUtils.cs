//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIpLocationUtils.cs
// 创建者 : Claude
// 创建时间: 2024-02-18 14:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : IP位置查询工具类
//===================================================================

using System.Net;
using IP2Region.Net.XDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace Hbt.Cur.Common.Utils;

/// <summary>
/// IP位置查询工具类
/// </summary>
/// <remarks>
/// 提供IP地址位置查询功能：
/// 1. 支持IPv4地址查询
/// 2. 支持内网IP识别
/// 3. 使用IP2Region数据库
/// </remarks>
public static class HbtIpLocationUtils
{
    private static Searcher? _searcher;
    private static readonly Regex _ipv4Regex = new(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled);
    private static readonly Regex _ipv6Regex = new(@"^(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$|^::1$|^::$", RegexOptions.Compiled);
    private static IWebHostEnvironment? _webHostEnvironment;

    /// <summary>
    /// 设置Web环境
    /// </summary>
    /// <param name="webHostEnvironment">Web主机环境</param>
    public static void SetWebHostEnvironment(IWebHostEnvironment webHostEnvironment)
    {
        try
        {
            _webHostEnvironment = webHostEnvironment;
            var dbPath = Path.Combine(_webHostEnvironment.WebRootPath, "IpRegion", "ip2region.xdb");
            Debug.WriteLine($"数据库文件路径: {dbPath}");
            
            if (!File.Exists(dbPath))
            {
                var error = $"IP2Region数据库文件不存在: {dbPath}";
                Debug.WriteLine(error);
                throw new FileNotFoundException(error);
            }
            
            _searcher = new Searcher(CachePolicy.File, dbPath);
        }
        catch (Exception ex)
        {
            var error = $"初始化IP2Region失败: {ex.GetType().Name} - {ex.Message}\n{ex.StackTrace}";
            Debug.WriteLine(error);
            throw new InvalidOperationException(error, ex);
        }
    }

    /// <summary>
    /// 获取IP地址的位置信息
    /// </summary>
    /// <param name="ipAddress">IP地址</param>
    /// <returns>位置信息，格式：国家|区域|省份|城市|ISP</returns>
    /// <remarks>
    /// 使用IP2Region库查询IP地址的地理位置信息
    /// 如果是内网IP或查询失败则返回特定说明
    /// </remarks>
    public static async Task<string> GetLocationAsync(string ipAddress)
    {
        if (_searcher == null)
        {
            throw new InvalidOperationException("IP2Region未初始化，请先调用SetWebHostEnvironment方法");
        }

        if (string.IsNullOrEmpty(ipAddress))
            return "Unknown Location";

        // 检查是否是本地回环地址
        if (ipAddress == "::1" || ipAddress == "127.0.0.1")
            return "本地";

        // 检查IPv4和IPv6格式
        if (!_ipv4Regex.IsMatch(ipAddress) && !_ipv6Regex.IsMatch(ipAddress))
            return "Unknown Location";

        try
        {
            return await Task.Run(() => _searcher.Search(ipAddress));
        }
        catch
        {
            return "Unknown Location";
        }
    }

    /// <summary>
    /// 判断是否是内网IP
    /// </summary>
    private static bool IsInternalIp(IPAddress ip)
    {
        if (IPAddress.IsLoopback(ip)) return true;
        
        byte[] bytes = ip.GetAddressBytes();
        return bytes[0] switch
        {
            10 => true,
            172 => bytes[1] >= 16 && bytes[1] <= 31,
            192 => bytes[1] == 168,
            _ => false
        };
    }
} 