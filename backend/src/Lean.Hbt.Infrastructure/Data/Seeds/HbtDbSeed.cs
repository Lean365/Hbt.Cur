//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeed.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 数据库种子数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 数据库种子数据初始化类
/// </summary>
public class HbtDbSeed
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedTenant _tenantSeed;
    private readonly HbtDbSeedRole _roleSeed;
    private readonly HbtDbSeedUser _userSeed;
    private readonly HbtDbSeedMenu _menuSeed;
    private readonly HbtDbSeedLanguage _languageSeed;
    private readonly HbtDbSeedRelation _relationSeed;
    private readonly HbtDbSeedTranslation _translationSeed;
    private readonly HbtDbSeedConfig _configSeed;
    private readonly HbtDbSeedDictType _dictTypeSeed;
    private readonly HbtDbSeedDictData _dictDataSeed;
    private readonly HbtDbSeedDept _deptSeed;
    private readonly HbtDbSeedPost _postSeed;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeed(
        HbtDbContext context,
        IHbtLogger logger,
        HbtDbSeedTenant tenantSeed,
        HbtDbSeedRole roleSeed,
        HbtDbSeedUser userSeed,
        HbtDbSeedMenu menuSeed,
        HbtDbSeedLanguage languageSeed,
        HbtDbSeedRelation relationSeed,
        HbtDbSeedTranslation translationSeed,
        HbtDbSeedConfig configSeed,
        HbtDbSeedDictType dictTypeSeed,
        HbtDbSeedDictData dictDataSeed,
        HbtDbSeedDept deptSeed,
        HbtDbSeedPost postSeed)
    {
        _context = context;
        _logger = logger;
        _tenantSeed = tenantSeed;
        _roleSeed = roleSeed;
        _userSeed = userSeed;
        _menuSeed = menuSeed;
        _languageSeed = languageSeed;
        _relationSeed = relationSeed;
        _translationSeed = translationSeed;
        _configSeed = configSeed;
        _dictTypeSeed = dictTypeSeed;
        _dictDataSeed = dictDataSeed;
        _deptSeed = deptSeed;
        _postSeed = postSeed;
    }

    /// <summary>
    /// 初始化所有种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        _logger.Info("开始初始化数据库种子数据...");

        // 1. 初始化租户数据
        var (tenantInsertCount, tenantUpdateCount) = await _tenantSeed.InitializeTenantAsync();
        _logger.Info($"租户数据初始化完成: 新增 {tenantInsertCount} 条, 更新 {tenantUpdateCount} 条");

        // 2. 初始化角色数据
        var (roleInsertCount, roleUpdateCount) = await _roleSeed.InitializeRoleAsync();
        _logger.Info($"角色数据初始化完成: 新增 {roleInsertCount} 条, 更新 {roleUpdateCount} 条");

        // 3. 初始化用户数据
        var (userInsertCount, userUpdateCount) = await _userSeed.InitializeUserAsync();
        _logger.Info($"用户数据初始化完成: 新增 {userInsertCount} 条, 更新 {userUpdateCount} 条");

        // 4. 初始化部门数据
        var (deptInsertCount, deptUpdateCount) = await _deptSeed.InitializeDeptAsync();
        _logger.Info($"部门数据初始化完成: 新增 {deptInsertCount} 条, 更新 {deptUpdateCount} 条");

        // 5. 初始化岗位数据
        var (postInsertCount, postUpdateCount) = await _postSeed.InitializePostAsync();
        _logger.Info($"岗位数据初始化完成: 新增 {postInsertCount} 条, 更新 {postUpdateCount} 条");

        // 6. 初始化菜单数据
        var (menuInsertCount, menuUpdateCount) = await _menuSeed.InitializeMenuAsync();
        _logger.Info($"菜单数据初始化完成: 新增 {menuInsertCount} 条, 更新 {menuUpdateCount} 条");

        // 7. 初始化语言数据
        var (langInsertCount, langUpdateCount) = await _languageSeed.InitializeLanguageAsync();
        _logger.Info($"语言数据初始化完成: 新增 {langInsertCount} 条, 更新 {langUpdateCount} 条");

        // 8. 初始化翻译数据
        var (transInsertCount, transUpdateCount) = await _translationSeed.InitializeTranslationAsync();
        _logger.Info($"翻译数据初始化完成: 新增 {transInsertCount} 条, 更新 {transUpdateCount} 条");

        // 9. 初始化配置数据
        var (configInsertCount, configUpdateCount) = await _configSeed.InitializeConfigAsync();
        _logger.Info($"配置数据初始化完成: 新增 {configInsertCount} 条, 更新 {configUpdateCount} 条");

        // 10. 初始化字典类型数据
        var (dictTypeInsertCount, dictTypeUpdateCount) = await _dictTypeSeed.InitializeDictTypeAsync();
        _logger.Info($"字典类型数据初始化完成: 新增 {dictTypeInsertCount} 条, 更新 {dictTypeUpdateCount} 条");

        // 11. 初始化字典数据
        var (dictDataInsertCount, dictDataUpdateCount) = await _dictDataSeed.InitializeDictDataAsync();
        _logger.Info($"字典数据初始化完成: 新增 {dictDataInsertCount} 条, 更新 {dictDataUpdateCount} 条");

        // 12. 初始化关联关系数据
        var (relationInsertCount, relationUpdateCount) = await _relationSeed.InitializeRelationsAsync();
        _logger.Info($"关联关系数据初始化完成: 新增 {relationInsertCount} 条, 更新 {relationUpdateCount} 条");

        _logger.Info("数据库种子数据初始化完成。");
    }
}