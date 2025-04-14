//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineMessageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Domain.Entities.RealTime;

namespace Lean.Hbt.Application.Services.RealTime;

/// <summary>
/// 在线消息服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageService : IHbtOnlineMessageService
{
    private readonly IHbtRepository<HbtOnlineMessage> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageService(IHbtRepository<HbtOnlineMessage> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    public async Task<HbtPagedResult<HbtOnlineMessageDto>> GetListAsync(HbtOnlineMessageQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineMessage>();


        if (query.SenderId.HasValue)
            exp = exp.And(m => m.SenderId == query.SenderId.Value);

        if (query.ReceiverId.HasValue)
            exp = exp.And(m => m.ReceiverId == query.ReceiverId.Value);

        if (query.MessageType.HasValue)
            exp = exp.And(m => m.MessageType == query.MessageType.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(m => m.CreateTime >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(m => m.CreateTime <= query.EndTime.Value);

        if (query.IsRead.HasValue)
            exp = exp.And(m => m.IsRead == query.IsRead.Value);

        // 2.查询数据
        var result = await _repository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        // 3.转换并返回
        return new HbtPagedResult<HbtOnlineMessageDto>
        {
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.Rows.Adapt<List<HbtOnlineMessageDto>>()
        };
    }

    /// <summary>
    /// 获取在线消息导出数据
    /// </summary>
    public async Task<List<HbtOnlineMessageExportDto>> GetExportDataAsync(HbtOnlineMessageQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineMessage>();



        if (query.SenderId.HasValue)
            exp = exp.And(m => m.SenderId == query.SenderId.Value);

        if (query.ReceiverId.HasValue)
            exp = exp.And(m => m.ReceiverId == query.ReceiverId.Value);

        if (query.MessageType.HasValue)
            exp = exp.And(m => m.MessageType == query.MessageType.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(m => m.CreateTime >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(m => m.CreateTime <= query.EndTime.Value);

        // 2.查询数据
        var messages = await _repository.GetListAsync(exp.ToExpression());

        // 3.转换并返回
        return messages.Adapt<List<HbtOnlineMessageExportDto>>();
    }

    /// <summary>
    /// 导出在线消息数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<byte[]> ExportAsync(HbtOnlineMessageQueryDto query, string sheetName = "在线消息信息")
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineMessage>();



        if (query.SenderId.HasValue)
            exp = exp.And(m => m.SenderId == query.SenderId.Value);

        if (query.ReceiverId.HasValue)
            exp = exp.And(m => m.ReceiverId == query.ReceiverId.Value);

        if (query.MessageType.HasValue)
            exp = exp.And(m => m.MessageType == query.MessageType.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(m => m.CreateTime >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(m => m.CreateTime <= query.EndTime.Value);

        // 2.查询数据
        var messages = await _repository.GetListAsync(exp.ToExpression());

        // 3.转换数据
        var dtos = messages.Adapt<List<HbtOnlineMessageDto>>();

        // 4.导出Excel
        return await HbtExcelHelper.ExportAsync(dtos, sheetName);
    }

    /// <summary>
    /// 获取消息详情
    /// </summary>
    public async Task<HbtOnlineMessageDto> GetMessageAsync(long id)
    {
        var message = await _repository.GetByIdAsync(id);
        return message.Adapt<HbtOnlineMessageDto>();
    }

    /// <summary>
    /// 保存消息
    /// </summary>
    public async Task<long> SaveMessageAsync(HbtOnlineMessageDto input)
    {
        var message = input.Adapt<HbtOnlineMessage>();
        message.CreateTime = DateTime.Now;
        var result = await _repository.CreateAsync(message);
        return result > 0 ? message.Id : 0;
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    public async Task<bool> DeleteMessageAsync(long id)
    {
        var result = await _repository.DeleteAsync(id);
        return result > 0;
    }

    /// <summary>
    /// 清理过期消息
    /// </summary>
    public async Task<int> CleanupExpiredMessagesAsync(int days = 7)
    {
        var expiredTime = DateTime.Now.AddDays(-days);
        var exp = Expressionable.Create<HbtOnlineMessage>();
        exp.And(m => m.CreateTime < expiredTime);

        return await _repository.DeleteAsync(exp.ToExpression());
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    public async Task<bool> MarkAsReadAsync(long id)
    {
        var message = await _repository.GetByIdAsync(id);
        if (message == null)
            return false;

        message.IsRead = 1;
        message.ReadTime = DateTime.Now;
        var result = await _repository.UpdateAsync(message);
        return result > 0;
    }

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    public async Task<int> MarkAllAsReadAsync(long userId)
    {
        var exp = Expressionable.Create<HbtOnlineMessage>();
        exp.And(m => m.ReceiverId == userId && m.IsRead == 0);

        var messages = await _repository.GetListAsync(exp.ToExpression());
        if (!messages.Any())
            return 0;

        foreach (var message in messages)
        {
            message.IsRead = 1;
            message.ReadTime = DateTime.Now;
        }

        return await _repository.UpdateRangeAsync(messages);
    }

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    public async Task<bool> MarkAsUnreadAsync(long id)
    {
        var message = await _repository.GetByIdAsync(id);
        if (message == null)
            return false;

        message.IsRead = 0;
        message.ReadTime = null;
        var result = await _repository.UpdateAsync(message);
        return result > 0;
    }

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    public async Task<int> MarkAllAsUnreadAsync(long userId)
    {
        var exp = Expressionable.Create<HbtOnlineMessage>();
        exp.And(m => m.ReceiverId == userId && m.IsRead == 1);

        var messages = await _repository.GetListAsync(exp.ToExpression());
        if (!messages.Any())
            return 0;

        foreach (var message in messages)
        {
            message.IsRead = 0;
            message.ReadTime = null;
        }

        return await _repository.UpdateRangeAsync(messages);
    }
}