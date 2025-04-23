//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOnlineMessageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.SignalR;
using Lean.Hbt.Application.Dtos.SignalR;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;

namespace Lean.Hbt.Application.Services.SignalR;

/// <summary>
/// 在线消息服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtOnlineMessageService : HbtBaseService, IHbtOnlineMessageService
{
    private readonly IHbtRepository<HbtOnlineMessage> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOnlineMessageService(
        IHbtLogger logger,
        IHbtRepository<HbtOnlineMessage> repository,
        IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    public async Task<HbtPagedResult<HbtOnlineMessageDto>> GetListAsync(HbtOnlineMessageQueryDto query)
    {
        try
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
                x => x.Id,
                OrderByType.Desc);

            // 3.转换数据
            return new HbtPagedResult<HbtOnlineMessageDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtOnlineMessageDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.GetListFailed"), ex);
            throw new HbtException(L("Message.GetListFailed"));
        }
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
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtOnlineMessageQueryDto query, string sheetName = "Message")
    {
        try
        {
            var list = await _repository.GetListAsync(KpMessageQueryExpression(query));
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtOnlineMessageExportDto>>(), sheetName, L("Message.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.ExportFailed"), ex);
            throw new HbtException(L("Message.ExportFailed"));
        }
    }

    /// <summary>
    /// 获取消息详情
    /// </summary>
    public async Task<HbtOnlineMessageDto> GetMessageAsync(long id)
    {
        try
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
                throw new HbtException(L("Message.NotFound", id));

            return message.Adapt<HbtOnlineMessageDto>();
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.GetFailed", id), ex);
            throw new HbtException(L("Message.GetFailed", id));
        }
    }

    /// <summary>
    /// 保存消息
    /// </summary>
    public async Task<long> SaveMessageAsync(HbtOnlineMessageDto input)
    {
        try
        {
            var message = input.Adapt<HbtOnlineMessage>();
            message.CreateTime = DateTime.Now;
            var result = await _repository.CreateAsync(message);
            if (result <= 0)
                throw new HbtException(L("Message.SaveFailed"));

            return message.Id;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.SaveFailed"), ex);
            throw new HbtException(L("Message.SaveFailed"));
        }
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    public async Task<bool> DeleteMessageAsync(long id)
    {
        try
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
                throw new HbtException(L("Message.NotFound", id));

            var result = await _repository.DeleteAsync(id);
            if (result <= 0)
                throw new HbtException(L("Message.DeleteFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.DeleteFailed", id), ex);
            throw new HbtException(L("Message.DeleteFailed", id));
        }
    }

    /// <summary>
    /// 清理过期消息
    /// </summary>
    public async Task<int> CleanupExpiredMessagesAsync(int days = 7)
    {
        try
        {
            var expiredTime = DateTime.Now.AddDays(-days);
            var exp = Expressionable.Create<HbtOnlineMessage>();
            exp.And(m => m.CreateTime < expiredTime);

            return await _repository.DeleteAsync(exp.ToExpression());
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.CleanupFailed"), ex);
            throw new HbtException(L("Message.CleanupFailed"));
        }
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    public async Task<bool> MarkAsReadAsync(long id)
    {
        try
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
                throw new HbtException(L("Message.NotFound", id));

            message.IsRead = 1;
            message.ReadTime = DateTime.Now;
            var result = await _repository.UpdateAsync(message);
            if (result <= 0)
                throw new HbtException(L("Message.MarkReadFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkReadFailed", id), ex);
            throw new HbtException(L("Message.MarkReadFailed", id));
        }
    }

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    public async Task<int> MarkAllAsReadAsync(long userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkAllReadFailed", userId), ex);
            throw new HbtException(L("Message.MarkAllReadFailed", userId));
        }
    }

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    public async Task<bool> MarkAsUnreadAsync(long id)
    {
        try
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
                throw new HbtException(L("Message.NotFound", id));

            message.IsRead = 0;
            message.ReadTime = null;
            var result = await _repository.UpdateAsync(message);
            if (result <= 0)
                throw new HbtException(L("Message.MarkUnreadFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkUnreadFailed", id), ex);
            throw new HbtException(L("Message.MarkUnreadFailed", id));
        }
    }

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    public async Task<int> MarkAllAsUnreadAsync(long userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkAllUnreadFailed", userId), ex);
            throw new HbtException(L("Message.MarkAllUnreadFailed", userId));
        }
    }

    private Expression<Func<HbtOnlineMessage, bool>> KpMessageQueryExpression(HbtOnlineMessageQueryDto query)
    {
        return Expressionable.Create<HbtOnlineMessage>()
            .AndIF(query.SenderId.HasValue, x => x.SenderId == query.SenderId.Value)
            .AndIF(query.ReceiverId.HasValue, x => x.ReceiverId == query.ReceiverId.Value)
            .AndIF(query.MessageType.HasValue, x => x.MessageType == query.MessageType.Value)
            .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
            .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
            .AndIF(query.IsRead.HasValue, x => x.IsRead == query.IsRead.Value)
            .ToExpression();
    }
}