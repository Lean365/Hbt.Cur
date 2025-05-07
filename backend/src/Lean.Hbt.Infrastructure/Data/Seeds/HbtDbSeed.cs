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
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
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
    private readonly HbtDbSeedOADictType _oaDictTypeSeed;
    private readonly HbtDbSeedOADictData _oaDictDataSeed;
    private readonly HbtDbSeedCsDictType _csDictTypeSeed;
    private readonly HbtDbSeedCsDictData _csDictDataSeed;
    private readonly HbtDbSeedEquipmentDictType _equipmentDictTypeSeed;
    private readonly HbtDbSeedEquipmentDictData _equipmentDictDataSeed;
    private readonly HbtDbSeedFinanceDictType _financeDictTypeSeed;
    private readonly HbtDbSeedFinanceDictData _financeDictDataSeed;
    private readonly HbtDbSeedHrDictType _hrDictTypeSeed;
    private readonly HbtDbSeedHrDictData _hrDictDataSeed;
    private readonly HbtDbSeedIndDictType _indDictTypeSeed;
    private readonly HbtDbSeedIndDictData _indDictDataSeed;
    private readonly HbtDbSeedMaterialDictType _materialDictTypeSeed;
    private readonly HbtDbSeedMaterialDictData _materialDictDataSeed;
    private readonly HbtDbSeedNatureDictType _natureDictTypeSeed;
    private readonly HbtDbSeedNatureDictData _natureDictDataSeed;

        private readonly HbtDbSeedFileDictType _fileDictTypeSeed;
    private readonly HbtDbSeedFileDictData _fileDictDataSeed;
    private readonly HbtCoreSeedTranslation _coreSeedTranslation;
    private readonly HbtGeneratorSeedTranslation _generatorSeedTranslation;
    private readonly HbtIdentitySeedTranslation _identitySeedTranslation;
    private readonly HbtLogsSeedTranslation _logsSeedTranslation;
    private readonly HbtRoutineSeedTranslation _routineSeedTranslation;
    private readonly HbtSignalRSeedTranslation _signalRSeedTranslation;
    private readonly HbtWorkflowSeedTranslation _workflowSeedTranslation;

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
        HbtDbSeedPost postSeed,
        HbtDbSeedOADictType oaDictTypeSeed,
        HbtDbSeedOADictData oaDictDataSeed,
        HbtDbSeedCsDictType csDictTypeSeed,
        HbtDbSeedCsDictData csDictDataSeed,
        HbtDbSeedEquipmentDictType equipmentDictTypeSeed,
        HbtDbSeedEquipmentDictData equipmentDictDataSeed,
        HbtDbSeedFinanceDictType financeDictTypeSeed,
        HbtDbSeedFinanceDictData financeDictDataSeed,
        HbtDbSeedHrDictType hrDictTypeSeed,
        HbtDbSeedHrDictData hrDictDataSeed,
        HbtDbSeedIndDictType indDictTypeSeed,
        HbtDbSeedIndDictData indDictDataSeed,
        HbtDbSeedMaterialDictType materialDictTypeSeed,
        HbtDbSeedMaterialDictData materialDictDataSeed,
        HbtDbSeedNatureDictType natureDictTypeSeed,
        HbtDbSeedNatureDictData natureDictDataSeed,
        HbtDbSeedFileDictType fileDictTypeSeed,
        HbtDbSeedFileDictData fileDictDataSeed,
        HbtCoreSeedTranslation coreSeedTranslation,
        HbtGeneratorSeedTranslation generatorSeedTranslation,
        HbtIdentitySeedTranslation identitySeedTranslation,
        HbtLogsSeedTranslation logsSeedTranslation,
        HbtRoutineSeedTranslation routineSeedTranslation,
        HbtSignalRSeedTranslation signalRSeedTranslation,
        HbtWorkflowSeedTranslation workflowSeedTranslation)
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
        _oaDictTypeSeed = oaDictTypeSeed;
        _oaDictDataSeed = oaDictDataSeed;
        _csDictTypeSeed = csDictTypeSeed;
        _csDictDataSeed = csDictDataSeed;
        _equipmentDictTypeSeed = equipmentDictTypeSeed;
        _equipmentDictDataSeed = equipmentDictDataSeed;
        _financeDictTypeSeed = financeDictTypeSeed;
        _financeDictDataSeed = financeDictDataSeed;
        _hrDictTypeSeed = hrDictTypeSeed;
        _hrDictDataSeed = hrDictDataSeed;
        _indDictTypeSeed = indDictTypeSeed;
        _indDictDataSeed = indDictDataSeed;
        _materialDictTypeSeed = materialDictTypeSeed;
        _materialDictDataSeed = materialDictDataSeed;
        _natureDictTypeSeed = natureDictTypeSeed;
        _natureDictDataSeed = natureDictDataSeed;
        _fileDictTypeSeed = fileDictTypeSeed;
        _fileDictDataSeed = fileDictDataSeed;
        _coreSeedTranslation = coreSeedTranslation;
        _generatorSeedTranslation = generatorSeedTranslation;
        _identitySeedTranslation = identitySeedTranslation;
        _logsSeedTranslation = logsSeedTranslation;
        _routineSeedTranslation = routineSeedTranslation;
        _signalRSeedTranslation = signalRSeedTranslation;
        _workflowSeedTranslation = workflowSeedTranslation;
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

    // 12. 初始化OA字典类型数据
    var (oaDictTypeInsertCount, oaDictTypeUpdateCount) = await _oaDictTypeSeed.InitializeOADictTypeAsync();
    _logger.Info($"OA字典类型数据初始化完成: 新增 {oaDictTypeInsertCount} 条, 更新 {oaDictTypeUpdateCount} 条");

    // 13. 初始化OA字典数据
    var (oaDictDataInsertCount, oaDictDataUpdateCount) = await _oaDictDataSeed.InitializeOADictDataAsync();
    _logger.Info($"OA字典数据初始化完成: 新增 {oaDictDataInsertCount} 条, 更新 {oaDictDataUpdateCount} 条");

    // 14. 初始化客户服务字典类型数据
    var (csDictTypeInsertCount, csDictTypeUpdateCount) = await _csDictTypeSeed.InitializeCsDictTypeAsync();
    _logger.Info($"客户服务字典类型数据初始化完成: 新增 {csDictTypeInsertCount} 条, 更新 {csDictTypeUpdateCount} 条");

    // 15. 初始化客户服务字典数据
    var (csDictDataInsertCount, csDictDataUpdateCount) = await _csDictDataSeed.InitializeCsDictDataAsync();
    _logger.Info($"客户服务字典数据初始化完成: 新增 {csDictDataInsertCount} 条, 更新 {csDictDataUpdateCount} 条");

    // 16. 初始化设备字典类型数据
    var (equipmentDictTypeInsertCount, equipmentDictTypeUpdateCount) = await _equipmentDictTypeSeed.InitializeEquipmentDictTypeAsync();
    _logger.Info($"设备字典类型数据初始化完成: 新增 {equipmentDictTypeInsertCount} 条, 更新 {equipmentDictTypeUpdateCount} 条");

    // 17. 初始化设备字典数据
    var (equipmentDictDataInsertCount, equipmentDictDataUpdateCount) = await _equipmentDictDataSeed.InitializeEquipmentDictDataAsync();
    _logger.Info($"设备字典数据初始化完成: 新增 {equipmentDictDataInsertCount} 条, 更新 {equipmentDictDataUpdateCount} 条");

    // 18. 初始化财务字典类型数据
    var (financeDictTypeInsertCount, financeDictTypeUpdateCount) = await _financeDictTypeSeed.InitializeFinanceDictTypeAsync();
    _logger.Info($"财务字典类型数据初始化完成: 新增 {financeDictTypeInsertCount} 条, 更新 {financeDictTypeUpdateCount} 条");

    // 19. 初始化财务字典数据
    var (financeDictDataInsertCount, financeDictDataUpdateCount) = await _financeDictDataSeed.InitializeFinanceDictDataAsync();
    _logger.Info($"财务字典数据初始化完成: 新增 {financeDictDataInsertCount} 条, 更新 {financeDictDataUpdateCount} 条");

    // 20. 初始化人力资源字典类型数据
    var (hrDictTypeInsertCount, hrDictTypeUpdateCount) = await _hrDictTypeSeed.InitializeHrDictTypeAsync();
    _logger.Info($"人力资源字典类型数据初始化完成: 新增 {hrDictTypeInsertCount} 条, 更新 {hrDictTypeUpdateCount} 条");

    // 21. 初始化人力资源字典数据
    var (hrDictDataInsertCount, hrDictDataUpdateCount) = await _hrDictDataSeed.InitializeHrDictDataAsync();
    _logger.Info($"人力资源字典数据初始化完成: 新增 {hrDictDataInsertCount} 条, 更新 {hrDictDataUpdateCount} 条");

    // 22. 初始化工业字典类型数据
    var (indDictTypeInsertCount, indDictTypeUpdateCount) = await _indDictTypeSeed.InitializeIndDictTypeAsync();
    _logger.Info($"工业字典类型数据初始化完成: 新增 {indDictTypeInsertCount} 条, 更新 {indDictTypeUpdateCount} 条");

    // 23. 初始化工业字典数据
    var (indDictDataInsertCount, indDictDataUpdateCount) = await _indDictDataSeed.InitializeIndDictDataAsync();
    _logger.Info($"工业字典数据初始化完成: 新增 {indDictDataInsertCount} 条, 更新 {indDictDataUpdateCount} 条");

    // 24. 初始化物料字典类型数据
    var (materialDictTypeInsertCount, materialDictTypeUpdateCount) = await _materialDictTypeSeed.InitializeMaterialDictTypeAsync();
    _logger.Info($"物料字典类型数据初始化完成: 新增 {materialDictTypeInsertCount} 条, 更新 {materialDictTypeUpdateCount} 条");

    // 25. 初始化物料字典数据
    var (materialDictDataInsertCount, materialDictDataUpdateCount) = await _materialDictDataSeed.InitializeMaterialDictDataAsync();
    _logger.Info($"物料字典数据初始化完成: 新增 {materialDictDataInsertCount} 条, 更新 {materialDictDataUpdateCount} 条");

    // 26. 初始化自然字典类型数据
    var (natureDictTypeInsertCount, natureDictTypeUpdateCount) = await _natureDictTypeSeed.InitializeNatureDictTypeAsync();
    _logger.Info($"自然字典类型数据初始化完成: 新增 {natureDictTypeInsertCount} 条, 更新 {natureDictTypeUpdateCount} 条");

    // 27. 初始化自然字典数据
    var (natureDictDataInsertCount, natureDictDataUpdateCount) = await _natureDictDataSeed.InitializeNatureDictDataAsync();
    _logger.Info($"自然字典数据初始化完成: 新增 {natureDictDataInsertCount} 条, 更新 {natureDictDataUpdateCount} 条");

    // 28. 初始化文件字典类型数据
    var (fileDictTypeInsertCount, fileDictTypeUpdateCount) = await _fileDictTypeSeed.InitializeFileDictTypeAsync();
    _logger.Info($"文件字典类型数据初始化完成: 新增 {fileDictTypeInsertCount} 条, 更新 {fileDictTypeUpdateCount} 条");

    // 29. 初始化文件字典数据
    var (fileDictDataInsertCount, fileDictDataUpdateCount) = await _fileDictDataSeed.InitializeFileDictDataAsync();
    _logger.Info($"文件字典数据初始化完成: 新增 {fileDictDataInsertCount} 条, 更新 {fileDictDataUpdateCount} 条");

    // 30. 初始化关联关系数据
    var (relationInsertCount, relationUpdateCount) = await _relationSeed.InitializeRelationsAsync();
    _logger.Info($"关联关系数据初始化完成: 新增 {relationInsertCount} 条, 更新 {relationUpdateCount} 条");

    // 31. 初始化核心模块翻译数据
    var (coreTransInsertCount, coreTransUpdateCount) = await _coreSeedTranslation.InitializeCoreTranslationAsync();
    _logger.Info($"核心模块翻译数据初始化完成: 新增 {coreTransInsertCount} 条, 更新 {coreTransUpdateCount} 条");

    // 32. 初始化代码生成器翻译数据
    var (generatorTransInsertCount, generatorTransUpdateCount) = await _generatorSeedTranslation.InitializeGeneratorTranslationAsync();
    _logger.Info($"代码生成器翻译数据初始化完成: 新增 {generatorTransInsertCount} 条, 更新 {generatorTransUpdateCount} 条");

    // 33. 初始化身份认证翻译数据
    var (identityTransInsertCount, identityTransUpdateCount) = await _identitySeedTranslation.InitializeIdentityTranslationAsync();
    _logger.Info($"身份认证翻译数据初始化完成: 新增 {identityTransInsertCount} 条, 更新 {identityTransUpdateCount} 条");

    // 34. 初始化日志翻译数据
    var (logsTransInsertCount, logsTransUpdateCount) = await _logsSeedTranslation.InitializeLogsTranslationAsync();
    _logger.Info($"日志翻译数据初始化完成: 新增 {logsTransInsertCount} 条, 更新 {logsTransUpdateCount} 条");

    // 35. 初始化日常办公翻译数据
    var (routineTransInsertCount, routineTransUpdateCount) = await _routineSeedTranslation.InitializeRoutineTranslationAsync();
    _logger.Info($"日常办公翻译数据初始化完成: 新增 {routineTransInsertCount} 条, 更新 {routineTransUpdateCount} 条");

    // 36. 初始化SignalR翻译数据
    var (signalRTransInsertCount, signalRTransUpdateCount) = await _signalRSeedTranslation.InitializeSignalRTranslationAsync();
    _logger.Info($"SignalR翻译数据初始化完成: 新增 {signalRTransInsertCount} 条, 更新 {signalRTransUpdateCount} 条");

    // 37. 初始化工作流翻译数据
    var (workflowTransInsertCount, workflowTransUpdateCount) = await _workflowSeedTranslation.InitializeWorkflowTranslationAsync();
    _logger.Info($"工作流翻译数据初始化完成: 新增 {workflowTransInsertCount} 条, 更新 {workflowTransUpdateCount} 条");

    _logger.Info("数据库种子数据初始化完成。");
        
    }
}