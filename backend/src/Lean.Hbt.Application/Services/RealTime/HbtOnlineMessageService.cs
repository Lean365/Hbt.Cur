//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtOnlineMessageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V1.0.0
// 描述    : 在线消息服务实现
//===================================================================

using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Application.Dtos.RealTime;
using Lean.Hbt.Common.Models;
using Mapster;
using SqlSugar;
using SqlSugar.Extensions;
using System.Linq.Expressions;

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
    public async Task<HbtPagedResult<HbtOnlineMessageDto>> GetPagedListAsync(HbtOnlineMessageQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineMessage>();
        
        if (query.TenantId.HasValue)
            exp = exp.And(m => m.TenantId == query.TenantId.Value);
            
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
        var result = await _repository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize);

        // 3.转换并返回
        return new HbtPagedResult<HbtOnlineMessageDto>
        {
            TotalNum = result.total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.list.Adapt<List<HbtOnlineMessageDto>>()
        };
    }

    /// <summary>
    /// 获取在线消息导出数据
    /// </summary>
    public async Task<List<HbtOnlineMessageExportDto>> GetExportDataAsync(HbtOnlineMessageQueryDto query)
    {
        // 1.构建查询条件
        var exp = Expressionable.Create<HbtOnlineMessage>();
        
        if (query.TenantId.HasValue)
            exp = exp.And(m => m.TenantId == query.TenantId.Value);
            
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
        var result = await _repository.InsertAsync(message);
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
} 