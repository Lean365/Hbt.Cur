namespace Lean.Hbt.Domain.IServices.Extensions;

/// <summary>
/// 设备ID生成器接口
/// </summary>
public interface IHbtDeviceIdGenerator
{
    /// <summary>
    /// 生成设备ID
    /// </summary>
    /// <param name="deviceInfoJson">设备信息JSON</param>
    /// <param name="userId">用户ID</param>
    /// <returns>设备ID</returns>
    string GenerateDeviceId(string? deviceInfoJson, string userId);

    /// <summary>
    /// 生成连接ID
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>连接ID</returns>
    string GenerateConnectionId(string deviceId);

    /// <summary>
    /// 生成设备ID和连接ID（兼容旧接口）
    /// </summary>
    /// <param name="deviceInfoJson">设备信息JSON</param>
    /// <param name="userId">用户ID</param>
    /// <returns>(设备ID, 连接ID)</returns>
    (string deviceId, string connectionId) GenerateIds(string? deviceInfoJson, string userId);
}