//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务实现类
//===================================================================

using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Domain.Entities.Routine.Core;
using Lean.Hbt.Application.Dtos.Routine.Core;

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
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取翻译仓储
        /// </summary>
        private IHbtRepository<HbtTranslation> TranslationRepository => _repositoryFactory.GetBusinessRepository<HbtTranslation>();

        /// <summary>
        /// 获取语言仓储
        /// </summary>
        private IHbtRepository<HbtLanguage> LanguageRepository => _repositoryFactory.GetBusinessRepository<HbtLanguage>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtTranslationService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtTranslation, bool>> KpTranslationQueryExpression(HbtTranslationQueryDto query)
        {
            return Expressionable.Create<HbtTranslation>()
                .AndIF(!string.IsNullOrEmpty(query.LangCode), x => x.LangCode == query.LangCode)
                .AndIF(!string.IsNullOrEmpty(query.TransKey), x => x.TransKey.Contains(query.TransKey))
                .AndIF(!string.IsNullOrEmpty(query.TransValue), x => x.TransValue.Contains(query.TransValue))
                .AndIF(!string.IsNullOrEmpty(query.ModuleName) && query.ModuleName != "-1", x => x.ModuleName == query.ModuleName)
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

            var result = await TranslationRepository.GetPagedListAsync(
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
            var translation = await TranslationRepository.GetByIdAsync(TranslationId);
            return translation == null ? throw new HbtException(L("Core.Translation.NotFound", TranslationId)) : translation.Adapt<HbtTranslationDto>();
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        public async Task<long> CreateAsync(HbtTranslationCreateDto input)
        {
            // 验证字段组合是否已存在
            var fieldValues = new Dictionary<string, string>
            {
                { "LangCode", input.LangCode },
                { "TransKey", input.TransKey }
            };
            await HbtValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

            var translation = input.Adapt<HbtTranslation>();
            translation.Status = 0; // 0表示正常状态

            return await TranslationRepository.CreateAsync(translation) > 0 ? translation.Id : throw new HbtException(L("Core.Translation.CreateFailed"));
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtTranslationUpdateDto input)
        {
            var translation = await TranslationRepository.GetByIdAsync(input.TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", input.TranslationId));

            // 验证字段组合是否已存在
            if (translation.LangCode != input.LangCode || translation.TransKey != input.TransKey)
            {
                var fieldValues = new Dictionary<string, string>
                {
                    { "LangCode", input.LangCode },
                    { "TransKey", input.TransKey }
                };
                await HbtValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues, input.TranslationId);
            }

            input.Adapt(translation);
            return await TranslationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long TranslationId)
        {
            var translation = await TranslationRepository.GetByIdAsync(TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", TranslationId));

            return await TranslationRepository.DeleteAsync(TranslationId) > 0;
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="TranslationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] TranslationIds)
        {
            if (TranslationIds == null || TranslationIds.Length == 0) return false;
            return await TranslationRepository.DeleteRangeAsync(TranslationIds.Cast<object>().ToList()) > 0;
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
                    // 验证字段组合是否已存在
                    var fieldValues = new Dictionary<string, string>
                    {
                        { "LangCode", translation.LangCode },
                        { "TransKey", translation.TransKey }
                    };
                    await HbtValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

                    await TranslationRepository.CreateAsync(translation.Adapt<HbtTranslation>());
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
            var list = await TranslationRepository.GetListAsync(KpTranslationQueryExpression(query));
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
            var translation = await TranslationRepository.GetByIdAsync(input.TranslationId)
                ?? throw new HbtException(L("Core.Translation.NotFound", input.TranslationId));

            translation.Status = input.Status;
            return await TranslationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        public async Task<string> GetTransValueAsync(string langCode, string transKey)
        {
            var translation = await TranslationRepository.GetFirstAsync(x => x.LangCode == langCode && x.TransKey == transKey);
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
            var list = await TranslationRepository.GetListAsync(x => x.LangCode == langCode && x.ModuleName == moduleName);
            return list.Adapt<List<HbtTranslationDto>>();
        }

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据,包含分页信息</returns>
        public async Task<HbtPagedResult<HbtTransposedDto>> GetTransposedDataAsync(HbtTransposedQueryDto query)
        {
            // 1. 获取所有启用的语言代码
            var languages = await LanguageRepository.GetListAsync(x => x.Status == 0);
            var langCodes = languages.Select(x => x.LangCode).ToList();
            _logger.Info($"获取到的语言代码列表: {string.Join(", ", langCodes)}");

            // 2. 构建查询条件
            var exp = Expressionable.Create<HbtTranslation>()
                .AndIF(!string.IsNullOrEmpty(query.TransKey), x => x.TransKey.Contains(query.TransKey))
                .AndIF(!string.IsNullOrEmpty(query.TransValue), x => x.TransValue.Contains(query.TransValue))
                .AndIF(!string.IsNullOrEmpty(query.ModuleName) && query.ModuleName != "-1", x => x.ModuleName == query.ModuleName)
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .ToExpression();

            // 3. 获取所有翻译键并分页
            var allTranslations = await TranslationRepository.GetListAsync(exp);
            _logger.Info($"查询到的翻译数据数量: {allTranslations.Count}");
            var transKeys = allTranslations.Select(x => x.TransKey).Distinct().ToList();
            _logger.Info($"翻译键数量: {transKeys.Count}");
            var totalNum = transKeys.Count;
            var pagedTransKeys = transKeys
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            // 4. 转置数据
            var transposedRows = new List<HbtTransposedDto>();
            foreach (var transKey in pagedTransKeys)
            {
                var firstTranslation = allTranslations.FirstOrDefault(x => x.TransKey == transKey);
                if (firstTranslation == null) continue;

                var dto = new HbtTransposedDto
                {
                    TransKey = transKey,
                    ModuleName = firstTranslation.ModuleName,
                    Translations = new Dictionary<string, HbtTranslationLangDto>()
                };

                // 为每个语言代码添加翻译值
                foreach (var langCode in langCodes)
                {
                    var translation = allTranslations.FirstOrDefault(x => x.TransKey == transKey && x.LangCode == langCode);
                    dto.Translations[langCode] = new HbtTranslationLangDto
                    {
                        TranslationId = translation?.Id ?? 0,
                        LangCode = langCode,
                        TransValue = translation?.TransValue ?? string.Empty,
                        Status = translation?.Status ?? 0
                    };
                }

                transposedRows.Add(dto);
            }

            return new HbtPagedResult<HbtTransposedDto>
            {
                TotalNum = totalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = transposedRows
            };
        }

        /// <summary>
        /// 获取转置后的翻译详情
        /// </summary>
        /// <param name="transKey">翻译键</param>
        /// <returns>转置后的翻译详情</returns>
        public async Task<HbtTransposedDto> GetTransposedDetailAsync(string transKey)
        {
            if (string.IsNullOrEmpty(transKey))
            {
                throw new HbtException(L("Core.Translation.TransKeyRequired"));
            }

            // 1. 获取所有启用的语言代码
            var languages = await LanguageRepository.GetListAsync(x => x.Status == 0);
            var langCodes = languages.Select(x => x.LangCode).ToList();

            // 2. 获取指定翻译键的所有翻译
            var translations = await TranslationRepository.GetListAsync(x => x.TransKey == transKey);
            if (!translations.Any())
            {
                throw new HbtException(L("Core.Translation.NotFound", transKey));
            }

            // 3. 构建转置详情
            var firstTranslation = translations.First();
            var detail = new HbtTransposedDto
            {
                TransKey = transKey,
                ModuleName = firstTranslation.ModuleName,
                OrderNum = firstTranslation.OrderNum,
                Status = firstTranslation.Status,
                Remark = firstTranslation.Remark,
                CreateBy = firstTranslation.CreateBy,
                CreateTime = firstTranslation.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdateBy = firstTranslation.UpdateBy,
                UpdateTime = firstTranslation.UpdateTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                Translations = new Dictionary<string, HbtTranslationLangDto>()
            };

            // 4. 添加各语言的翻译信息
            foreach (var langCode in langCodes)
            {
                var translation = translations.FirstOrDefault(x => x.LangCode == langCode);
                detail.Translations[langCode] = new HbtTranslationLangDto
                {
                    TranslationId = translation?.Id ?? 0,
                    LangCode = langCode,
                    TransValue = translation?.TransValue ?? string.Empty,
                    Status = translation?.Status ?? 0
                };
            }

            return detail;
        }

        /// <summary>
        /// 创建转置翻译数据
        /// </summary>
        /// <param name="input">转置数据创建对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> CreateTransposedAsync(HbtTransposedDto input)
        {
            if (input == null || string.IsNullOrEmpty(input.TransKey) || string.IsNullOrEmpty(input.ModuleName))
            {
                throw new HbtException(L("Core.Translation.InvalidInput"));
            }

            try
            {
                // 获取所有启用的语言
                var languages = await LanguageRepository.GetListAsync(x => x.Status == 0);
                var langCodes = languages.Select(x => x.LangCode).ToList();

                // 验证每个语言的翻译数据
                foreach (var langCode in langCodes)
                {
                    if (input.Translations.TryGetValue(langCode, out var translation))
                    {
                        // 验证字段组合是否已存在
                        var fieldValues = new Dictionary<string, string>
                        {
                            { "LangCode", langCode },
                            { "TransKey", input.TransKey }
                        };
                        await HbtValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

                        // 创建翻译记录，不管 TransValue 是否有值
                        var translationEntity = new HbtTranslation
                        {
                            LangCode = langCode,
                            TransKey = input.TransKey,
                            TransValue = translation.TransValue ?? string.Empty,
                            ModuleName = input.ModuleName,
                            Status = input.Status,
                            Remark = input.Remark,
                            CreateBy = _currentUser.UserName,
                            CreateTime = DateTime.Now
                        };

                        await TranslationRepository.CreateAsync(translationEntity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"创建转置翻译数据失败: {ex.Message}", ex);
                throw new HbtException(L("Core.Translation.CreateFailed"));
            }
        }

        /// <summary>
        /// 更新转置翻译数据
        /// </summary>
        /// <param name="input">转置数据更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateTransposedAsync(HbtTransposedDto input)
        {
            if (input == null || string.IsNullOrEmpty(input.TransKey) || string.IsNullOrEmpty(input.ModuleName))
            {
                throw new HbtException(L("Core.Translation.InvalidInput"));
            }

            try
            {
                // 获取所有启用的语言
                var languages = await LanguageRepository.GetListAsync(x => x.Status == 0);

                // 处理每个语言的翻译
                foreach (var language in languages)
                {
                    var langCode = language.LangCode;
                    if (input.Translations.TryGetValue(langCode, out var translation))
                    {
                        // 根据 LangCode 和 TransKey 查询已存在的翻译记录
                        var existingTranslation = await TranslationRepository.GetFirstAsync(x => 
                            x.LangCode == langCode && x.TransKey == input.TransKey);

                        if (existingTranslation != null)
                        {
                            // 更新已存在的记录
                            existingTranslation.TransValue = translation.TransValue ?? string.Empty;
                            existingTranslation.Status = input.Status;
                            existingTranslation.Remark = input.Remark;
                            existingTranslation.UpdateBy = _currentUser.UserName;
                            existingTranslation.UpdateTime = DateTime.Now;
                            await TranslationRepository.UpdateAsync(existingTranslation);
                        }
                        else
                        {
                            // 创建不存在的记录
                            var newTranslation = new HbtTranslation
                            {
                                LangCode = langCode,
                                TransKey = input.TransKey,
                                TransValue = translation.TransValue ?? string.Empty,
                                ModuleName = input.ModuleName,
                                Status = input.Status,
                                Remark = input.Remark,
                                CreateBy = _currentUser.UserName,
                                CreateTime = DateTime.Now
                            };

                            await TranslationRepository.CreateAsync(newTranslation);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新转置翻译数据失败: {ex.Message}", ex);
                throw new HbtException(L("Core.Translation.UpdateFailed"));
            }
        }
    }
}