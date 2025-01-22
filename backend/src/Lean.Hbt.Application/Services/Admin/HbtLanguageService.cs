//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言服务实现类
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Mapster;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 语言服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtLanguageService : IHbtLanguageService
    {
        private readonly IHbtRepository<HbtLanguage> _languageRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageRepository">语言仓储</param>
        /// <param name="logger">日志接口</param>
        public HbtLanguageService(
            IHbtRepository<HbtLanguage> languageRepository,
            IHbtLogger logger)
        {
            _languageRepository = languageRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtLanguageDto>> GetPagedListAsync(HbtLanguageQueryDto query)
        {
            var exp = Expressionable.Create<HbtLanguage>();

            if (!string.IsNullOrEmpty(query.LangCode))
                exp.And(x => x.LangCode.Contains(query.LangCode));

            if (!string.IsNullOrEmpty(query.LangName))
                exp.And(x => x.LangName.Contains(query.LangName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var (list, total) = await _languageRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);
            return new HbtPagedResult<HbtLanguageDto>
            {
                Rows = list.Adapt<List<HbtLanguageDto>>(),
                TotalNum = total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        public async Task<HbtLanguageDto> GetAsync(long languageId)
        {
            var language = await _languageRepository.GetByIdAsync(languageId);
            if (language == null)
                throw new HbtException("语言不存在");

            return language.Adapt<HbtLanguageDto>();
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        public async Task<long> InsertAsync(HbtLanguageCreateDto input)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode);
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName);

            var language = input.Adapt<HbtLanguage>();
            var result = await _languageRepository.InsertAsync(language);
            return result > 0 ? language.Id : 0;
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        public async Task<bool> UpdateAsync(HbtLanguageUpdateDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LanguageId);
            if (language == null)
                throw new HbtException("语言不存在");

            if (language.LangCode != input.LangCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode, input.LanguageId);

            if (language.LangName != input.LangName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName, input.LanguageId);

            language.LangCode = input.LangCode;
            language.LangName = input.LangName;
            language.OrderNum = input.OrderNum;
            language.Status = input.Status;

            return await _languageRepository.UpdateAsync(language) > 0;
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        public async Task<bool> DeleteAsync(long languageId)
        {
            var language = await _languageRepository.GetByIdAsync(languageId);
            if (language == null)
                throw new HbtException("语言不存在");

            return await _languageRepository.DeleteAsync(language) > 0;
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] languageIds)
        {
            if (languageIds == null || languageIds.Length == 0)
                return false;

            var entities = await _languageRepository.GetListAsync(x => languageIds.Contains(x.Id));
            return await _languageRepository.DeleteRangeAsync(entities) > 0;
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(List<HbtLanguageImportDto> languages)
        {
            if (languages == null || !languages.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var item in languages)
            {
                try
                {
                    var language = item.Adapt<HbtLanguage>();
                    await _languageRepository.InsertAsync(language);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error($"导入语言失败：{ex.Message}", ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        public async Task<List<HbtLanguageExportDto>> ExportAsync(HbtLanguageQueryDto query)
        {
            var exp = Expressionable.Create<HbtLanguage>();

            if (!string.IsNullOrEmpty(query.LangCode))
                exp.And(x => x.LangCode.Contains(query.LangCode));

            if (!string.IsNullOrEmpty(query.LangName))
                exp.And(x => x.LangName.Contains(query.LangName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _languageRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtLanguageExportDto>>();
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        public async Task<HbtLanguageTemplateDto> GetTemplateAsync()
        {
            return await Task.FromResult(new HbtLanguageTemplateDto());
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(HbtLanguageStatusDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LanguageId);
            if (language == null)
                throw new HbtException("语言不存在");

            language.Status = input.Status;
            return await _languageRepository.UpdateAsync(language) > 0;
        }
    }
} 