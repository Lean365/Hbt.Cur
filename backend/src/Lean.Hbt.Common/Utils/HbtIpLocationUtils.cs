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

namespace Lean.Hbt.Common.Utils;

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
    private static readonly Searcher? _ip2RegionSearcher;
    private static IWebHostEnvironment? _webHostEnvironment;

    /// <summary>
    /// 静态构造函数，初始化IP2Region搜索器
    /// </summary>
    static HbtIpLocationUtils()
    {
        try
        {
            var dbPath = Path.Combine(_webHostEnvironment?.WebRootPath ?? AppDomain.CurrentDomain.BaseDirectory, 
                                    "IpRegion", "ip2region.xdb");
            _ip2RegionSearcher = new Searcher(CachePolicy.File, dbPath);
        }
        catch (Exception ex)
        {
            // 记录日志
            Console.WriteLine($"初始化IP2Region失败: {ex.Message}");
            _ip2RegionSearcher = null;
        }
    }

    /// <summary>
    /// 设置Web环境
    /// </summary>
    /// <param name="webHostEnvironment">Web主机环境</param>
    public static void SetWebHostEnvironment(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
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
    public static string GetIpLocation(string ipAddress)
    {
        try
        {
            if (string.IsNullOrEmpty(ipAddress))
                return "未知IP";

            // 检查是否是合法的IP地址
            if (!IPAddress.TryParse(ipAddress, out var ip))
                return "无效IP";

            // 检查是否是内网IP
            if (IsInternalIp(ip))
                return "内网IP";

            // 使用IP2Region查询
            if (_ip2RegionSearcher != null)
            {
                var location = _ip2RegionSearcher.Search(ipAddress);
                return string.IsNullOrEmpty(location) ? "未知位置" : location;
            }

            return "查询服务未初始化";
        }
        catch
        {
            return "查询失败";
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