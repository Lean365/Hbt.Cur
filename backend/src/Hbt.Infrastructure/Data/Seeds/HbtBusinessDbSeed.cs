//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBusinessDbSeed.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 业务数据库种子数据初始化类 - 使用协调器模式
//===================================================================

using Hbt.Common.Exceptions;
using Hbt.Infrastructure.Data.Contexts;
using Hbt.Infrastructure.Data.Seeds.Biz;
using Hbt.Infrastructure.Data.Seeds.Biz.Dict;
using Hbt.Infrastructure.Data.Seeds.Biz.Translation;

namespace Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 业务数据库种子数据初始化类
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 统一管理业务数据库的种子数据初始化
/// 2. 使用协调器模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class HbtBusinessDbSeed
{
    private readonly HbtBusinessDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedLanguage _languageSeed;
    private readonly HbtDbSeedTranslationCoordinator _translationSeed;
    private readonly HbtDbSeedConfig _configSeed;
    private readonly HbtDbSeedDictCoordinator _dictSeed;
    private readonly HbtDbSeedHrm _hrmSeed;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtBusinessDbSeed(
        HbtBusinessDbContext context,
        IHbtLogger logger,
        HbtDbSeedLanguage languageSeed,
        HbtDbSeedTranslationCoordinator translationSeed,
        HbtDbSeedConfig configSeed,
        HbtDbSeedDictCoordinator dictSeed,
        HbtDbSeedHrm hrmSeed)
    {
        _context = context;
        _logger = logger;
        _languageSeed = languageSeed;
        _translationSeed = translationSeed;
        _configSeed = configSeed;
        _dictSeed = dictSeed;
        _hrmSeed = hrmSeed;
    }

    /// <summary>
    /// 初始化业务数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化业务数据库种子数据...");

            // 初始化语言数据
            var (languageInsertCount, languageUpdateCount) = await _languageSeed.InitializeLanguageAsync();
            _logger.Info($"语言数据初始化完成: 新增 {languageInsertCount} 条, 更新 {languageUpdateCount} 条");

            // 初始化翻译数据
            var translationResult = await _translationSeed.InitializeAllTranslationDataAsync();
            _logger.Info($"翻译数据初始化完成: 新增 {translationResult.GetTotalInsertCount()} 条, 更新 {translationResult.GetTotalUpdateCount()} 条");

            // 初始化配置数据
            var (configInsertCount, configUpdateCount) = await _configSeed.InitializeConfigAsync();
            _logger.Info($"配置数据初始化完成: 新增 {configInsertCount} 条, 更新 {configUpdateCount} 条");

            // 初始化字典数据
            var dictResult = await _dictSeed.InitializeAllDictDataAsync();
            _logger.Info($"字典数据初始化完成: 字典类型 {dictResult.GetTotalDictTypeCount()} 个, 字典数据 {dictResult.GetTotalDictDataCount()} 条");

            // 初始化人力资源数据
            await _hrmSeed.InitializeHrmDataAsync();
            _logger.Info("人力资源数据初始化完成");

            _logger.Info("业务数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"业务数据库种子数据初始化失败: {ex.Message}", ex);
            throw new HbtException("业务数据库种子数据初始化失败", ex);
        }
    }
} 