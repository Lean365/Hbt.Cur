#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 语言服务实现类
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 语言服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtLanguageService : HbtBaseService, IHbtLanguageService
    {
        private readonly IHbtRepository<HbtLanguage> _languageRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageRepository">语言仓储</param>
        /// <param name="logger">日志接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLanguageService(
            IHbtRepository<HbtLanguage> languageRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtLanguage, bool>> KpLanguageQueryExpression(HbtLanguageQueryDto query)
        {
            return Expressionable.Create<HbtLanguage>()
                .AndIF(!string.IsNullOrEmpty(query.LangCode), x => x.LangCode.Contains(query.LangCode!))
                .AndIF(!string.IsNullOrEmpty(query.LangName), x => x.LangName.Contains(query.LangName!))
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .AndIF(query.IsDefault != -1, x => x.IsDefault == query.IsDefault)
                .ToExpression();
        }

        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        public async Task<HbtPagedResult<HbtLanguageDto>> GetListAsync(HbtLanguageQueryDto? query)
        {
            query ??= new HbtLanguageQueryDto();

            var result = await _languageRepository.GetPagedListAsync(
                KpLanguageQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtLanguageDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtLanguageDto>>()
            };
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>语言详情</returns>
        public async Task<HbtLanguageDto> GetByIdAsync(long LanguageId)
        {
            var language = await _languageRepository.GetByIdAsync(LanguageId);
            return language == null ? throw new HbtException(L("Core.Language.NotFound", LanguageId)) : language.Adapt<HbtLanguageDto>();
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        public async Task<long> CreateAsync(HbtLanguageCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode);
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangIcon", input.LangIcon);

            var language = input.Adapt<HbtLanguage>();
            return await _languageRepository.CreateAsync(language) > 0 ? language.Id : throw new HbtException(L("Core.Language.CreateFailed"));
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtLanguageUpdateDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LanguageId)
                ?? throw new HbtException(L("Core.Language.NotFound", input.LanguageId));

            // 验证字段是否已存在
            if (language.LangCode != input.LangCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", input.LangCode, input.LanguageId);
            if (language.LangName != input.LangName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", input.LangName, input.LanguageId);
            if (language.LangIcon != input.LangIcon)
                await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangIcon", input.LangIcon, input.LanguageId);

            input.Adapt(language);
            return await _languageRepository.UpdateAsync(language) > 0;
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long LanguageId)
        {
            var language = await _languageRepository.GetByIdAsync(LanguageId)
                ?? throw new HbtException($"语言不存在: {LanguageId}");

            return await _languageRepository.DeleteAsync(LanguageId) > 0;
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="LanguageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] LanguageIds)
        {
            if (LanguageIds == null || LanguageIds.Length == 0) return false;
            return await _languageRepository.DeleteRangeAsync(LanguageIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtLanguage")
        {
            var languages = await HbtExcelHelper.ImportAsync<HbtLanguageDto>(fileStream, sheetName);
            if (languages == null || !languages.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var language in languages)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangCode", language.LangCode);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangName", language.LangName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_languageRepository, "LangIcon", language.LangIcon);

                    await _languageRepository.CreateAsync(language.Adapt<HbtLanguage>());
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.Language.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtLanguageQueryDto query, string sheetName = "HbtLanguage")
        {
            var list = await _languageRepository.GetListAsync(KpLanguageQueryExpression(query));
            var (fileName, content) = await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtLanguageDto>>(), sheetName, L("Core.Language.ExportTitle"));
            return (fileName, content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtLanguageTemplateDto>(sheetName);
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtLanguageStatusDto input)
        {
            var language = await _languageRepository.GetByIdAsync(input.LanguageId)
                ?? throw new HbtException(L("Core.Language.NotFound", input.LanguageId));

            language.Status = input.Status;
            return await _languageRepository.UpdateAsync(language) > 0;
        }
    }
}