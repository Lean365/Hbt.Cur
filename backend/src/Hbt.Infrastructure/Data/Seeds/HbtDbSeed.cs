//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeed.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 数据库种子数据初始化类
//===================================================================

using Hbt.Common.Exceptions;
using Hbt.Infrastructure.Data.Contexts;
using Hbt.Infrastructure.Data.Seeds.Auth;
using Hbt.Infrastructure.Data.Seeds.Biz;
using Hbt.Infrastructure.Data.Seeds.Biz.Dict;
using Hbt.Infrastructure.Data.Seeds.Biz.Translation;
using Hbt.Infrastructure.Data.Seeds.Generator;
using Hbt.Infrastructure.Data.Seeds.Workflow;

namespace Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 数据库种子数据初始化类
/// </summary>
public class HbtDbSeed
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedIdentityTenant _tenantSeed;
    private readonly HbtDbSeedIdentityUser _userSeed;
    private readonly HbtDbSeedMenu _menuSeed;
    private readonly HbtDbSeedIdentityRole _roleSeed;
    private readonly HbtDbSeedIdentityDept _deptSeed;
    private readonly HbtDbSeedIdentityPost _postSeed;
    private readonly HbtDbSeedLanguage _languageSeed;
    private readonly HbtDbSeedTranslationCoordinator _translationSeed;
    private readonly HbtDbSeedConfig _configSeed;
    private readonly HbtDbSeedDictCoordinator _dictSeed;
    private readonly HbtDbSeedGenConfig _genConfigSeed;
    private readonly HbtDbSeedGenTemplate _genTemplateSeed;
    private readonly HbtDbSeedWorkflowCoordinator _workflowCoordinator;
    private readonly HbtDbSeedHrm _hrmSeed;
    private readonly HbtDbSeedIdentityRelation _relationSeed;
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeed(
        HbtDbContext context,
        IHbtLogger logger,
        HbtDbSeedIdentityTenant tenantSeed,
        HbtDbSeedIdentityUser userSeed,
        HbtDbSeedMenu menuSeed,
        HbtDbSeedIdentityRole roleSeed,
        HbtDbSeedIdentityDept deptSeed,
        HbtDbSeedIdentityPost postSeed,
        HbtDbSeedLanguage languageSeed,
        HbtDbSeedTranslationCoordinator translationSeed,
        HbtDbSeedConfig configSeed,
        HbtDbSeedDictCoordinator dictSeed,
        HbtDbSeedGenConfig genConfigSeed,
        HbtDbSeedGenTemplate genTemplateSeed,
        HbtDbSeedWorkflowCoordinator workflowCoordinator,
        HbtDbSeedHrm hrmSeed,
        HbtDbSeedIdentityRelation relationSeed)
    {
        _context = context;
        _logger = logger;
        _tenantSeed = tenantSeed;
        _userSeed = userSeed;
        _menuSeed = menuSeed;
        _roleSeed = roleSeed;
        _deptSeed = deptSeed;
        _postSeed = postSeed;
        _languageSeed = languageSeed;
        _translationSeed = translationSeed;
        _configSeed = configSeed;
        _dictSeed = dictSeed;
        _genConfigSeed = genConfigSeed;
        _genTemplateSeed = genTemplateSeed;
        _workflowCoordinator = workflowCoordinator;
        _hrmSeed = hrmSeed;
        _relationSeed = relationSeed;
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化种子数据...");

            // 初始化租户数据
            var (tenantInsertCount, tenantUpdateCount) = await _tenantSeed.InitializeTenantAsync();
            _logger.Info($"租户数据初始化完成: 新增 {tenantInsertCount} 条, 更新 {tenantUpdateCount} 条");

            // 初始化用户数据
            var (userInsertCount, userUpdateCount) = await _userSeed.InitializeUserAsync();
            _logger.Info($"用户数据初始化完成: 新增 {userInsertCount} 条, 更新 {userUpdateCount} 条");

            // 初始化菜单数据
            var (menuInsertCount, menuUpdateCount) = await _menuSeed.InitializeMenuAsync();
            _logger.Info($"菜单数据初始化完成: 新增 {menuInsertCount} 条, 更新 {menuUpdateCount} 条");

            // 初始化角色数据
            var (roleInsertCount, roleUpdateCount) = await _roleSeed.InitializeRoleAsync();
            _logger.Info($"角色数据初始化完成: 新增 {roleInsertCount} 条, 更新 {roleUpdateCount} 条");

            // 初始化部门数据
            var (deptInsertCount, deptUpdateCount) = await _deptSeed.InitializeDeptAsync();
            _logger.Info($"部门数据初始化完成: 新增 {deptInsertCount} 条, 更新 {deptUpdateCount} 条");

            // 初始化岗位数据
            var (postInsertCount, postUpdateCount) = await _postSeed.InitializePostAsync();
            _logger.Info($"岗位数据初始化完成: 新增 {postInsertCount} 条, 更新 {postUpdateCount} 条");

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


            // 使用工作流协调器初始化所有工作流相关数据
            await _workflowCoordinator.InitializeLeaveWorkflowAsync();
            _logger.Info("工作流数据初始化完成");

            // 初始化生成器配置数据
            var (genConfigInsertCount, genConfigUpdateCount) = await _genConfigSeed.InitializeGenConfigAsync();
            _logger.Info($"生成器配置数据初始化完成: 新增 {genConfigInsertCount} 条, 更新 {genConfigUpdateCount} 条");

            // 初始化生成器模板数据
            var (genTemplateInsertCount, genTemplateUpdateCount) = await _genTemplateSeed.InitializeGenTemplateAsync();
            _logger.Info($"生成器模板数据初始化完成: 新增 {genTemplateInsertCount} 条, 更新 {genTemplateUpdateCount} 条");

            // 初始化人力资源数据
            await _hrmSeed.InitializeHrmDataAsync();
            _logger.Info("人力资源数据初始化完成");

            // 初始化关系数据（放在最后）
            var (relationInsertCount, relationUpdateCount) = await _relationSeed.InitializeRelationsAsync();
            _logger.Info($"关系数据初始化完成: 新增 {relationInsertCount} 条, 更新 {relationUpdateCount} 条");

            _logger.Info("种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"种子数据初始化失败: {ex.Message}", ex);
            throw new HbtException("种子数据初始化失败", ex);
        }
    }
}
