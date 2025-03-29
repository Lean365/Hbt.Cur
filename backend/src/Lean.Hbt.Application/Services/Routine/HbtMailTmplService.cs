//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMailTmplService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件模板服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 邮件模板服务实现
    /// </summary>
    public class HbtMailTmplService : IHbtMailTmplService
    {
        private readonly ILogger<HbtMailTmplService> _logger;
        private readonly IHbtRepository<HbtMailTmpl> _tmplRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="tmplRepository">模板仓储</param>
        public HbtMailTmplService(
            ILogger<HbtMailTmplService> logger,
            IHbtRepository<HbtMailTmpl> tmplRepository)
        {
            _logger = logger;
            _tmplRepository = tmplRepository;
        }

        /// <summary>
        /// 获取邮件模板分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtMailTmplDto>> GetListAsync(HbtMailTmplQueryDto query)
        {
            var exp = Expressionable.Create<HbtMailTmpl>();

            if (!string.IsNullOrEmpty(query.TmplName))
                exp.And(x => x.TmplName.Contains(query.TmplName));

            if (!string.IsNullOrEmpty(query.TmplCode))
                exp.And(x => x.TmplCode.Contains(query.TmplCode));

            if (query.TmplStatus.HasValue)
                exp.And(x => x.TmplStatus == query.TmplStatus.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _tmplRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtMailTmplDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtMailTmplDto>>()
            };
        }

        /// <summary>
        /// 获取邮件模板详情
        /// </summary>
        public async Task<HbtMailTmplDto> GetByIdAsync(long tmplId)
        {
            var tmpl = await _tmplRepository.GetByIdAsync(tmplId);
            if (tmpl == null)
                throw new HbtException($"邮件模板不存在: {tmplId}");

            return tmpl.Adapt<HbtMailTmplDto>();
        }

        /// <summary>
        /// 创建邮件模板
        /// </summary>
        public async Task<long> CreateAsync(HbtMailTmplCreateDto input)
        {
            var tmpl = input.Adapt<HbtMailTmpl>();
            tmpl.CreateTime = DateTime.Now;

            // 验证模板编码是否已存在
            var existingTmpl = await _tmplRepository.GetInfoAsync(x => x.TmplCode == input.TmplCode);
            if (existingTmpl != null)
                throw new HbtException($"模板编码已存在: {input.TmplCode}");

            var result = await _tmplRepository.CreateAsync(tmpl);
            if (result <= 0)
                throw new HbtException("创建邮件模板失败");

            return tmpl.Id;
        }

        /// <summary>
        /// 更新邮件模板
        /// </summary>
        public async Task<bool> UpdateAsync(long tmplId, HbtMailTmplDto input)
        {
            var tmpl = await _tmplRepository.GetByIdAsync(tmplId);
            if (tmpl == null)
                throw new HbtException($"邮件模板不存在: {tmplId}");

            // 验证模板编码是否已存在
            var existingTmpl = await _tmplRepository.GetInfoAsync(x => x.TmplCode == input.TmplCode && x.Id != tmplId);
            if (existingTmpl != null)
                throw new HbtException($"模板编码已存在: {input.TmplCode}");

            input.Adapt(tmpl);
            var result = await _tmplRepository.UpdateAsync(tmpl);
            return result > 0;
        }

        /// <summary>
        /// 删除邮件模板
        /// </summary>
        public async Task<bool> DeleteAsync(long tmplId)
        {
            var tmpl = await _tmplRepository.GetByIdAsync(tmplId);
            if (tmpl == null)
                throw new HbtException($"邮件模板不存在: {tmplId}");

            var result = await _tmplRepository.DeleteAsync(tmplId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除邮件模板
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] tmplIds)
        {
            foreach (var tmplId in tmplIds)
            {
                await DeleteAsync(tmplId);
            }
            return true;
        }

        /// <summary>
        /// 导出邮件模板数据
        /// </summary>
        public async Task<byte[]> ExportAsync(HbtMailTmplQueryDto query, string sheetName = "邮件模板数据")
        {
            var exp = Expressionable.Create<HbtMailTmpl>();

            if (!string.IsNullOrEmpty(query.TmplName))
                exp.And(x => x.TmplName.Contains(query.TmplName));

            if (!string.IsNullOrEmpty(query.TmplCode))
                exp.And(x => x.TmplCode.Contains(query.TmplCode));

            if (query.TmplStatus.HasValue)
                exp.And(x => x.TmplStatus == query.TmplStatus.Value);

            var list = await _tmplRepository.GetListAsync(exp.ToExpression());
            var result = list.Adapt<List<HbtMailTmplExportDto>>();

            return await HbtExcelHelper.ExportAsync(result, sheetName);
        }
    }
} 