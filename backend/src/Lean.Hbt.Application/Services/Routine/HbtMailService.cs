//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMailService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 邮件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtMailService : IHbtMailService
    {
        private readonly ILogger<HbtMailService> _logger;
        private readonly IHbtRepository<HbtMail> _mailRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="mailRepository">邮件仓储</param>
        public HbtMailService(
            ILogger<HbtMailService> logger,
            IHbtRepository<HbtMail> mailRepository)
        {
            _logger = logger;
            _mailRepository = mailRepository;
        }

        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtMailDto>> GetPagedListAsync(HbtMailQueryDto query)
        {
            var exp = Expressionable.Create<HbtMail>();

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailToEmail))
                exp.And(x => x.MailToEmail.Contains(query.MailToEmail));

            if (query?.MailStatus.HasValue == true)
                exp.And(x => x.MailStatus == query.MailStatus.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _mailRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtMailDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtMailDto>>()
            };
        }

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>返回邮件详情</returns>
        public async Task<HbtMailDto> GetAsync(long mailId)
        {
            var mail = await _mailRepository.GetByIdAsync(mailId);
            if (mail == null)
                throw new HbtException($"邮件不存在: {mailId}");

            return mail.Adapt<HbtMailDto>();
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回邮件ID</returns>
        public async Task<long> InsertAsync(HbtMailCreateDto input)
        {
            var mail = input.Adapt<HbtMail>();
            var result = await _mailRepository.InsertAsync(mail);
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
                throw new HbtException($"邮件不存在: {input.MailId}");

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
                throw new HbtException($"邮件不存在: {mailId}");

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
                throw new HbtException("请选择要删除的邮件");

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
                
                var result = await _mailRepository.InsertAsync(mail);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"发送邮件失败: {input.MailSubject}");
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
        public async Task<byte[]> ExportAsync(HbtMailQueryDto query, string sheetName = "邮件信息")
        {
            var exp = Expressionable.Create<HbtMail>();

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailToEmail))
                exp.And(x => x.MailToEmail.Contains(query.MailToEmail));

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
    }
} 