//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedLanguage.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 语言数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 语言数据初始化类
/// </summary>
public class HbtDbSeedLanguage
{
    private readonly IHbtRepository<HbtLanguage> _languageRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedLanguage(IHbtRepository<HbtLanguage> languageRepository, IHbtLogger logger)
    {
        _languageRepository = languageRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化语言数据
    /// </summary>
    public async Task<(int, int)> InitializeLanguageAsync(long systemTenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultLanguages = new List<HbtLanguage>
        {
            new HbtLanguage
            {
                LangCode = "ar-SA",
                LangName = "العربية",
                LangIcon = "🇸🇦",
                OrderNum = 1,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "en-US",
                LangName = "English",
                LangIcon = "🇺🇸",
                OrderNum = 2,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "fr-FR",
                LangName = "Français",
                LangIcon = "🇫🇷",
                OrderNum = 3,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "ja-JP",
                LangName = "日本語",
                LangIcon = "🇯🇵",
                OrderNum = 4,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "ko-KR",
                LangName = "한국어",
                LangIcon = "🇰🇷",
                OrderNum = 5,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "ru-RU",
                LangName = "Русский",
                LangIcon = "🇷🇺",
                OrderNum = 6,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "es-ES",
                LangName = "Español",
                LangIcon = "🇪🇸",
                OrderNum = 7,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "zh-CN",
                LangName = "简体中文",
                LangIcon = "🇨🇳",
                OrderNum = 8,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            },
            new HbtLanguage
            {
                LangCode = "zh-TW",
                LangName = "繁體中文",
                LangIcon = "🇹🇼",
                OrderNum = 9,
                Status = 0,
                IsDefault = 0,
                IsBuiltin = 1,

            }
        };

        foreach (var language in defaultLanguages)
        {
            var existingLanguage = await _languageRepository.GetFirstAsync(l => l.LangCode == language.LangCode);
            if (existingLanguage == null)
            {

                language.CreateBy = "Hbt365";
                language.CreateTime = DateTime.Now;
                language.UpdateBy = "Hbt365";
                language.UpdateTime = DateTime.Now;

                await _languageRepository.CreateAsync(language);
                insertCount++;
                _logger.Info($"[创建] 语言 '{language.LangName}' 创建成功");
            }
            else
            {
                existingLanguage.LangCode = language.LangCode;
                existingLanguage.LangName = language.LangName;
                existingLanguage.IsBuiltin = language.IsBuiltin;
                existingLanguage.OrderNum = language.OrderNum;
                existingLanguage.Status = language.Status;

                existingLanguage.Remark = language.Remark;
                language.CreateBy = "Hbt365";
                language.CreateTime = DateTime.Now;
                language.UpdateBy = "Hbt365";
                language.UpdateTime = DateTime.Now;

                await _languageRepository.UpdateAsync(existingLanguage);
                updateCount++;
                _logger.Info($"[更新] 语言 '{existingLanguage.LangName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}