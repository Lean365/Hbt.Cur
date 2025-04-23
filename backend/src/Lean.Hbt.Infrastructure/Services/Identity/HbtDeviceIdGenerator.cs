using System;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Identity;

/// <summary>
/// 设备ID生成器实现
/// </summary>
public class HbtDeviceIdGenerator : IHbtDeviceIdGenerator
{
    /// <summary>
    /// 生成设备ID
    /// </summary>
    /// <param name="deviceInfoJson">设备信息JSON</param>
    /// <param name="userId">用户ID</param>
    /// <returns>设备ID</returns>
    public string GenerateDeviceId(string? deviceInfoJson, string userId)
    {
        // 提取设备特征
        var deviceFeatures = ExtractDeviceFeatures(deviceInfoJson);
        
        // 生成设备ID（使用完整的32位哈希值）
        return GenerateHash(
            string.Join(":", new[]
            {
                deviceFeatures.deviceType,
                deviceFeatures.deviceModel,
                deviceFeatures.osType,
                deviceFeatures.deviceFingerprint ?? "unknown",
                userId,
                // 添加硬件信息增加唯一性
                deviceFeatures.resolution ?? "unknown",
                deviceFeatures.browserInfo ?? "unknown"
            })
        );
    }

    /// <summary>
    /// 生成连接ID
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>连接ID</returns>
    public string GenerateConnectionId(string deviceId)
    {
        // 生成连接ID（32位）
        return GenerateHash(
            string.Join(":", new[]
            {
                deviceId,
                DateTime.UtcNow.Ticks.ToString("x16"),  // 16进制格式的时间戳
                Guid.NewGuid().ToString("N"),           // 无分隔符的GUID
                Random.Shared.Next().ToString("x8")     // 随机数
            })
        ).Substring(0, 32);
    }

    /// <summary>
    /// 生成设备ID和连接ID（兼容旧接口）
    /// </summary>
    /// <param name="deviceInfoJson">设备信息JSON</param>
    /// <param name="userId">用户ID</param>
    /// <returns>(设备ID, 连接ID)</returns>
    public (string deviceId, string connectionId) GenerateIds(string? deviceInfoJson, string userId)
    {
        var deviceId = GenerateDeviceId(deviceInfoJson, userId);
        var connectionId = GenerateConnectionId(deviceId);
        return (deviceId, connectionId);
    }

    /// <summary>
    /// 从JSON中提取设备特征
    /// </summary>
    private (string deviceType, string deviceModel, string osType, string? deviceFingerprint, string? resolution, string? browserInfo) 
        ExtractDeviceFeatures(string? deviceInfoJson)
    {
        if (string.IsNullOrEmpty(deviceInfoJson))
        {
            return ("unknown", "unknown", "unknown", null, null, null);
        }

        try
        {
            using var doc = JsonDocument.Parse(deviceInfoJson);
            var root = doc.RootElement;

            var deviceType = GetJsonValue(root, "DeviceType", "unknown");
            var deviceModel = GetJsonValue(root, "DeviceModel", "unknown");
            var osType = GetJsonValue(root, "OsType", "unknown");
            var deviceFingerprint = GetJsonValue(root, "DeviceFingerprint", null);
            var resolution = GetJsonValue(root, "Resolution", null);
            var browserType = GetJsonValue(root, "BrowserType", "unknown");
            var browserVersion = GetJsonValue(root, "BrowserVersion", "unknown");
            var browserInfo = $"{browserType}:{browserVersion}";

            return (deviceType, deviceModel, osType, deviceFingerprint, resolution, browserInfo);
        }
        catch
        {
            return ("unknown", "unknown", "unknown", null, null, null);
        }
    }

    /// <summary>
    /// 安全获取JSON值
    /// </summary>
    private string GetJsonValue(JsonElement element, string propertyName, string defaultValue)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            return property.GetString() ?? defaultValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// 生成SHA256哈希
    /// </summary>
    private string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
} 