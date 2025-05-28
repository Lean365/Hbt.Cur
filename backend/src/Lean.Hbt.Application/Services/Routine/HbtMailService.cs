//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMailService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 邮件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtMailService : HbtBaseService, IHbtMailService
    {
        private readonly IHbtRepository<HbtMail> _mailRepository;
        private readonly IHbtSignalRClient _signalRClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mailRepository">邮件仓储</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtMailService(
            IHbtLogger logger,
            IHbtRepository<HbtMail> mailRepository,
            IHbtSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _mailRepository = mailRepository;
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtMailDto>> GetListAsync(HbtMailQueryDto query)
        {
            var exp = Expressionable.Create<HbtMail>();

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailTo))
                exp.And(x => x.MailTo.Contains(query.MailTo));

            if (query?.MailStatus.HasValue == true)
                exp.And(x => x.MailStatus == query.MailStatus.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _mailRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtMailDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtMailDto>>()
            };
        }

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>返回邮件详情</returns>
        public async Task<HbtMailDto> GetByIdAsync(long mailId)
        {
            var mail = await _mailRepository.GetByIdAsync(mailId);
            if (mail == null)
                throw new HbtException(L("Mail.NotFound", mailId));

            return mail.Adapt<HbtMailDto>();
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回邮件ID</returns>
        public async Task<long> CreateAsync(HbtMailCreateDto input)
        {
            var mail = input.Adapt<HbtMail>();
            var result = await _mailRepository.CreateAsync(mail);
            return result;
        }

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtMailUpdateDto input)
        {
            var mail = await _mailRepository.GetByIdAsync(input.MailId);
            if (mail == null)
                throw new HbtException(L("Mail.NotFound", input.MailId));

            input.Adapt(mail);
            var result = await _mailRepository.UpdateAsync(mail);
            return result > 0;
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long mailId)
        {
            var mail = await _mailRepository.GetByIdAsync(mailId);
            if (mail == null)
                throw new HbtException(L("Mail.NotFound", mailId));

            var result = await _mailRepository.DeleteAsync(mail);
            return result > 0;
        }

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="mailIds">邮件ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] mailIds)
        {
            if (mailIds == null || mailIds.Length == 0)
                throw new HbtException(L("Mail.SelectToDelete"));

            Expression<Func<HbtMail, bool>> predicate = x => mailIds.Contains(x.Id);
            var result = await _mailRepository.DeleteAsync(predicate);
            return result > 0;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> SendAsync(HbtMailSendDto input)
        {
            try
            {
                // TODO: 实现邮件发送逻辑
                var mail = input.Adapt<HbtMail>();
                mail.MailStatus = 1; // 发送成功
                mail.MailSendTime = DateTime.Now;

                var result = await _mailRepository.CreateAsync(mail);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Mail.SendFailed", input.MailSubject), ex);
                return false;
            }
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>返回发送结果</returns>
        public async Task<(int success, int fail)> BatchSendAsync(List<HbtMailSendDto> inputs)
        {
            if (inputs == null || !inputs.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var input in inputs)
            {
                if (await SendAsync(input))
                    success++;
                else
                    fail++;
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtMailQueryDto query, string sheetName = "邮件信息")
        {
            var exp = Expressionable.Create<HbtMail>();

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailTo))
                exp.And(x => x.MailTo.Contains(query.MailTo));

            if (query?.MailStatus.HasValue == true)
                exp.And(x => x.MailStatus == query.MailStatus.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var mails = await _mailRepository.AsQueryable()
                .Where(exp.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            var exportDtos = mails.Adapt<List<HbtMailExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 标记邮件已读
        /// </summary>
        public async Task<bool> MarkAsReadAsync(long id)
        {
            var mail = await _mailRepository.GetByIdAsync(id);
            if (mail == null) return false;

            // 更新已读状态
            var readIds = (mail.MailReadIds?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()).ToList();
            var userId = _currentUser.UserId.ToString();
            if (!readIds.Contains(userId))
            {
                readIds.Add(userId);
                mail.MailReadIds = string.Join(",", readIds);
                mail.MailReadCount = readIds.Count;
                mail.MailLastReadTime = DateTime.Now;

                await _mailRepository.UpdateAsync(mail);

                // 发送已读通知
                var notification = new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailRead,
                    Title = L("Mail.Read"),
                    Content = L("Mail.ReadContent", mail.MailSubject),
                    Timestamp = DateTime.Now,
                    Data = mail
                };
                await _signalRClient.ReceiveMailStatus(notification);
            }

            return true;
        }

        /// <summary>
        /// 标记所有邮件为已读
        /// </summary>
        public async Task<int> MarkAllAsReadAsync(long userId)
        {
            // 查找所有未读邮件（MailReadIds为空或不包含当前用户ID的邮件）
            var unreadMails = await _mailRepository.GetListAsync(m =>
                string.IsNullOrEmpty(m.MailReadIds) ||
                !m.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).Contains(userId));

            if (!unreadMails.Any())
                return 0;

            // 遍历每个未读邮件
            foreach (var mail in unreadMails)
            {
                // 获取已读用户列表
                var readIds = string.IsNullOrEmpty(mail.MailReadIds)
                    ? new List<long>()
                    : mail.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).ToList();

                // 如果用户不在已读列表中，添加用户ID
                if (!readIds.Contains(userId))
                {
                    readIds.Add(userId);
                    mail.MailReadIds = string.Join(",", readIds);
                    mail.MailReadCount = readIds.Count;
                    mail.MailLastReadTime = DateTime.Now;
                }
            }

            // 批量更新邮件
            var result = await _mailRepository.UpdateRangeAsync(unreadMails);
            return result;
        }

        /// <summary>
        /// 标记邮件未读
        /// </summary>
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            var mail = await _mailRepository.GetByIdAsync(id);
            if (mail == null)
                return false;

            // 获取当前已读人ID列表
            var readIds = string.IsNullOrEmpty(mail.MailReadIds)
                ? new List<long>()
                : mail.MailReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            // 如果当前用户不在已读列表中，则返回true
            if (!readIds.Contains(_currentUser.UserId))
                return true;

            // 从已读列表中移除当前用户
            readIds.Remove(_currentUser.UserId);
            mail.MailReadIds = string.Join(",", readIds);
            mail.MailReadCount = readIds.Count;

            var result = await _mailRepository.UpdateAsync(mail);
            if (result > 0)
            {
                await _signalRClient.ReceiveMailStatus(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.MailUnread,
                    Title = L("Mail.Unread"),
                    Content = L("Mail.UnreadContent", mail.MailSubject),
                    Timestamp = DateTime.Now,
                    Data = mail
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 标记所有邮件为未读
        /// </summary>
        public async Task<int> MarkAllAsUnreadAsync(long userId)
        {
            // 查找所有已读邮件（MailReadIds包含当前用户ID的邮件）
            var readMails = await _mailRepository.GetListAsync(m =>
                !string.IsNullOrEmpty(m.MailReadIds) &&
                m.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).Contains(userId));

            if (!readMails.Any())
                return 0;

            // 遍历每个已读邮件
            foreach (var mail in readMails)
            {
                // 获取已读用户列表并移除当前用户
                var readIds = mail.MailReadIds.Split(',', StringSplitOptions.None)
                    .Select(id => long.Parse(id))
                    .ToList();
                readIds.Remove(userId);
                mail.MailReadIds = string.Join(",", readIds);
                mail.MailReadCount = readIds.Count;
            }

            // 批量更新邮件
            var result = await _mailRepository.UpdateRangeAsync(readMails);
            return result;
        }
    }
}