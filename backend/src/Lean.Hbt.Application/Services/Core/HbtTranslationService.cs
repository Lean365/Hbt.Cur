//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务实现类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Domain.Entities.Core;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 翻译服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtTranslationService : HbtBaseService, IHbtTranslationService
    {
        private readonly IHbtRepository<HbtTranslation> _translationRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtTranslationService(
            IHbtRepository<HbtTranslation> translationRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _translationRepository = translationRepository ?? throw new ArgumentNullException(nameof(translationRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtTranslation, bool>> KpTranslationQueryExpression(HbtTranslationQueryDto query)
        {
            return Expressionable.Create<HbtTranslation>()
                .AndIF(!string.IsNullOrEmpty(query.LangCode), x => x.LangCode == query.LangCode)
                .AndIF(!string.IsNullOrEmpty(query.TransKey), x => x.TransKey.Contains(query.TransKey))
                .AndIF(!string.IsNullOrEmpty(query.ModuleName), x => x.ModuleName == query.ModuleName)
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .ToExpression();
        }

        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        public async Task<HbtPagedResult<HbtTranslationDto>> GetListAsync(HbtTranslationQueryDto query)
        {
            query ??= new HbtTranslationQueryDto();

            var result = await _translationRepository.GetPagedListAsync(
                KpTranslationQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtTranslationDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtTranslationDto>>()
            };
        }

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>翻译详情</returns>
        public async Task<HbtTranslationDto> GetByIdAsync(long TranslationId)
        {
            var translation = await _translationRepository.GetByIdAsync(TranslationId);
            return translation == null ? throw new HbtException(L("Core.Translation.NotFound", TranslationId)) : translation.Adapt<HbtTranslationDto>();
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        public async Task<long> CreateAsync(HbtTranslationCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_translationRepository, "TransKey", input.TransKey);

            var translation = input.Adapt<HbtTranslation>();
            translation.Status = 0; // 0表示正常状态

            return await _translationRepository.CreateAsync(translation) > 0 ? translation.Id : throw new HbtException(L("Core.Translation.CreateFailed"));
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtTranslationUpdateDto input)
        {
            var translation = await _translationRepository.GetByIdAsync(input.TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", input.TranslationId));

            // 验证字段是否已存在
            if (translation.TransKey != input.TransKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_translationRepository, "TransKey", input.TransKey, input.TranslationId);

            input.Adapt(translation);
            return await _translationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long TranslationId)
        {
            var translation = await _translationRepository.GetByIdAsync(TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", TranslationId));

            return await _translationRepository.DeleteAsync(TranslationId) > 0;
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="TranslationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] TranslationIds)
        {
            if (TranslationIds == null || TranslationIds.Length == 0) return false;
            return await _translationRepository.DeleteRangeAsync(TranslationIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "翻译信息")
        {
            var translations = await HbtExcelHelper.ImportAsync<HbtTranslationDto>(fileStream, sheetName);
            if (translations == null || !translations.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var translation in translations)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_translationRepository, "TransKey", translation.TransKey);

                    await _translationRepository.CreateAsync(translation.Adapt<HbtTranslation>());
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.Translation.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtTranslationQueryDto query, string sheetName = "Translation")
        {
            var list = await _translationRepository.GetListAsync(KpTranslationQueryExpression(query));
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtTranslationDto>>(), sheetName, L("Core.Translation.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtTranslationTemplateDto>(sheetName);
        }

        /// <summary>
        /// 更新翻译状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtTranslationStatusDto input)
        {
            var translation = await _translationRepository.GetByIdAsync(input.TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", input.TranslationId));

            translation.Status = input.Status;
            return await _translationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        public async Task<string> GetTransValueAsync(string langCode, string transKey)
        {
            var translation = await _translationRepository.GetFirstAsync(x => x.LangCode == langCode && x.TransKey == transKey);
            return translation?.TransValue ?? string.Empty;
        }

        /// <summary>
        /// 获取指定模块的翻译列表
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>翻译列表</returns>
        public async Task<List<HbtTranslationDto>> GetListByModuleAsync(string langCode, string moduleName)
        {
            var list = await _translationRepository.GetListAsync(x => x.LangCode == langCode && x.ModuleName == moduleName);
            return list.Adapt<List<HbtTranslationDto>>();
        }

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据,包含分页信息</returns>
        public async Task<HbtPagedResult<Dictionary<string, string>>> GetTransposedDataAsync(HbtTranslationQueryDto query)
        {
            // 1. 构建查询条件
            var exp = KpTranslationQueryExpression(query);

            // 2. 获取所有语言代码
            var allTranslations = await _translationRepository.GetListAsync(x => true);
            var langCodes = allTranslations.Select(x => x.LangCode).Distinct().ToList();

            // 3. 获取分页数据
            var result = await _translationRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            // 4. 转置数据
            var transposedRows = new List<Dictionary<string, string>>();
            foreach (var row in result.Rows)
            {
                var dict = new Dictionary<string, string>
                {
                    { "TransKey", row.TransKey },
                    { "ModuleName", row.ModuleName }
                };

                foreach (var langCode in langCodes)
                {
                    var translation = allTranslations.FirstOrDefault(x => x.TransKey == row.TransKey && x.LangCode == langCode);
                    dict[langCode] = translation?.TransValue ?? string.Empty;
                }

                transposedRows.Add(dict);
            }

            return new HbtPagedResult<Dictionary<string, string>>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = transposedRows
            };
        }
    }
}