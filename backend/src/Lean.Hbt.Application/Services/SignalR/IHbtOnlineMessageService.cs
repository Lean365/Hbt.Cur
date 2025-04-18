//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtOnlineMessageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.SignalR;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.SignalR;

/// <summary>
/// 在线消息服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public interface IHbtOnlineMessageService
{
    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<HbtPagedResult<HbtOnlineMessageDto>> GetListAsync(HbtOnlineMessageQueryDto query);

    /// <summary>
    /// 导出在线消息数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<byte[]> ExportAsync(HbtOnlineMessageQueryDto query, string sheetName = "在线消息信息");

    /// <summary>
    /// 获取消息详情
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息详情</returns>
    Task<HbtOnlineMessageDto> GetMessageAsync(long id);

    /// <summary>
    /// 保存消息
    /// </summary>
    /// <param name="input">消息信息</param>
    /// <returns>消息ID</returns>
    Task<long> SaveMessageAsync(HbtOnlineMessageDto input);

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteMessageAsync(long id);

    /// <summary>
    /// 清理过期消息
    /// </summary>
    /// <param name="days">保留天数</param>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredMessagesAsync(int days = 7);

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAsReadAsync(long id);

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    Task<int> MarkAllAsReadAsync(long userId);

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAsUnreadAsync(long id);

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    Task<int> MarkAllAsUnreadAsync(long userId);
} 