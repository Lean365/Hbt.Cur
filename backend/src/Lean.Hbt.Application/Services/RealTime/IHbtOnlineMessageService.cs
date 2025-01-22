//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtOnlineMessageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.RealTime;

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
    Task<HbtPagedResult<HbtOnlineMessageDto>> GetPagedListAsync(HbtOnlineMessageQueryDto query);

    /// <summary>
    /// 获取在线消息导出数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出数据</returns>
    Task<List<HbtOnlineMessageExportDto>> GetExportDataAsync(HbtOnlineMessageQueryDto query);

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
} 