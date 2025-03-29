//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言服务实现类
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
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
        private readonly IHbtLocalizationService _localization;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageRepository">语言仓储</param>
        /// <param name="logger">日志接口</param>
        /// <param name="localization">本地化服务</param>
        public HbtLanguageService(
            IHbtRepository<HbtLanguage> languageRepository,
            IHbtLogger logger,
            IHbtLocalizationService localization)
        {
            _languageRepository = languageRepository;
            _logger = logger;
            _localization = localization;
        }

        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtLanguageDto>> GetListAsync(HbtLanguageQueryDto query)
        {
            var exp = Expressionable.Create<HbtLanguage>();

            if (!string.IsNullOrEmpty(query.LangCode))
                exp.And(x => x.LangCode.Contains(query.LangCode));

            if (!string.IsNullOrEmpty(query.LangName))
                exp.And(x => x.LangName.Contains(query.LangName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _languageRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtLanguageDto>
            {
                Rows = result.Rows.Adapt<List<HbtLanguageDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10
            };
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        public async Task<HbtLanguageDto> GetByIdAsync(long LangId)
        {
            var language = await _languageRepository.GetByIdAsync(LangId);
            if (language == null)
                throw new HbtException("语言不存在");

            return language.Adapt<HbtLanguageDto>();
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        public async Task<long> CreateAsync(HbtLanguageCreateDto input)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode);
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName);

            var language = input.Adapt<HbtLanguage>();
            var result = await _languageRepository.CreateAsync(language);
            return result > 0 ? language.Id : 0;
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        public async Task<bool> UpdateAsync(HbtLanguageUpdateDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LangId);
            if (language == null)
                throw new HbtException("语言不存在");

            if (language.LangCode != input.LangCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode, input.LangId);

            if (language.LangName != input.LangName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName, input.LangId);

            language.LangCode = input.LangCode;
            language.LangName = input.LangName;
            language.OrderNum = input.OrderNum;
            language.Status = input.Status;

            return await _languageRepository.UpdateAsync(language) > 0;
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        public async Task<bool> DeleteAsync(long LangId)
        {
            var language = await _languageRepository.GetByIdAsync(LangId);
            if (language == null)
                throw new HbtException("语言不存在");

            return await _languageRepository.DeleteAsync(language) > 0;
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] LangIds)
        {
            if (LangIds == null || LangIds.Length == 0)
                return false;

            var entities = await _languageRepository.GetListAsync(x => LangIds.Contains(x.Id));
            return await _languageRepository.DeleteRangeAsync(entities) > 0;
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var success = 0;
            var fail = 0;

            try
            {
                // 1.从Excel导入数据
                var languages = await HbtExcelHelper.ImportAsync<HbtLanguageDto>(fileStream, sheetName);
                if (languages == null || !languages.Any())
                    return (0, 0);

                // 2.保存数据
                foreach (var language in languages)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(language.LangCode) || string.IsNullOrEmpty(language.LangName))
                        {
                            _logger.Warn(await _localization.GetLocalizedStringAsync("Language:ImportFailedEmptyFields"));
                            fail++;
                            continue;
                        }

                        // 验证字段是否已存在
                        try
                        {
                            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", language.LangCode);
                        }
                        catch (HbtException ex)
                        {
                            _logger.Warn($"{await _localization.GetLocalizedStringAsync("Language:ImportFailed")}: {ex.Message}");
                            fail++;
                            continue;
                        }

                        // 创建语言
                        var newLanguage = language.Adapt<HbtLanguage>();
                        await _languageRepository.CreateAsync(newLanguage);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"{await _localization.GetLocalizedStringAsync("Language:ImportFailed")}: {ex.Message}", ex);
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(await _localization.GetLocalizedStringAsync("Language.Import.Failed"), ex);
                return (0, 0);
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtLanguageQueryDto query, string sheetName)
        {
            var exp = Expressionable.Create<HbtLanguage>();

            if (!string.IsNullOrEmpty(query?.LangCode))
                exp.And(x => x.LangCode.Contains(query.LangCode));

            if (!string.IsNullOrEmpty(query?.LangName))
                exp.And(x => x.LangName.Contains(query.LangName));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _languageRepository.GetListAsync(exp.ToExpression());
            var dtos = list.Adapt<List<HbtLanguageDto>>();

            try
            {
                return await HbtExcelHelper.ExportAsync(dtos, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(await _localization.GetLocalizedStringAsync("Language.Export.Failed"), ex);
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName)
        {
            var template = new List<HbtLanguageDto>
            {
                new HbtLanguageDto
                {
                    LangCode = "zh-CN",
                    LangName = await _localization.GetLocalizedStringAsync("Language.Template.Chinese"),
                    Status = 0
                }
            };

            try
            {
                return await HbtExcelHelper.ExportAsync(template, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(await _localization.GetLocalizedStringAsync("Language.Template.Failed"), ex);
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(HbtLanguageStatusDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LangId);
            if (language == null)
                throw new HbtException("语言不存在");

            language.Status = input.Status;
            return await _languageRepository.UpdateAsync(language) > 0;
        }
    }
}