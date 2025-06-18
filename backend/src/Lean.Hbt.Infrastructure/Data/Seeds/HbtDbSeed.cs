//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeed.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 数据库种子数据初始化类
//===================================================================

using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Infrastructure.Data.Contexts;

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
    private readonly HbtDbSeedGeneratorDictType _generatorDictTypeSeed;
    private readonly HbtDbSeedGeneratorDictData _generatorDictDataSeed;
    private readonly HbtCoreSeedTranslation _coreSeedTranslation;
    private readonly HbtGeneratorSeedTranslation _generatorSeedTranslation;
    private readonly HbtIdentitySeedTranslation _identitySeedTranslation;
    private readonly HbtLogsSeedTranslation _logsSeedTranslation;
    private readonly HbtRoutineSeedTranslation _routineSeedTranslation;
    private readonly HbtSignalRSeedTranslation _signalRSeedTranslation;
    private readonly HbtDbSeedGenConfig _genConfigSeed;
    private readonly HbtDbSeedGenTemplate _genTemplateSeed;
    private readonly HbtDbSeedWorkflowDictType _workflowDictTypeSeed;
    private readonly HbtDbSeedWorkflowDictData _workflowDictDataSeed;
    private readonly HbtWorkflowSeedTranslation _workflowSeedTranslation;
    private readonly HbtDbSeedDefinition _definitionSeed;
    private readonly HbtDbSeedInstance _instanceSeed;
    private readonly HbtDbSeedNode _nodeSeed;
    private readonly HbtDbSeedTask _taskSeed;
    private readonly HbtDbSeedVariable _variableSeed;
    private readonly HbtDbSeedForm _formSeed;

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
        HbtDbSeedGeneratorDictType generatorDictTypeSeed,
        HbtDbSeedGeneratorDictData generatorDictDataSeed,
        HbtCoreSeedTranslation coreSeedTranslation,
        HbtGeneratorSeedTranslation generatorSeedTranslation,
        HbtIdentitySeedTranslation identitySeedTranslation,
        HbtLogsSeedTranslation logsSeedTranslation,
        HbtRoutineSeedTranslation routineSeedTranslation,
        HbtSignalRSeedTranslation signalRSeedTranslation,
        HbtDbSeedGenConfig genConfigSeed,
        HbtDbSeedGenTemplate genTemplateSeed,
        HbtDbSeedWorkflowDictType workflowDictTypeSeed,
        HbtDbSeedWorkflowDictData workflowDictDataSeed,
        HbtWorkflowSeedTranslation workflowSeedTranslation,
        HbtDbSeedDefinition definitionSeed,
        HbtDbSeedInstance instanceSeed,
        HbtDbSeedNode nodeSeed,
        HbtDbSeedTask taskSeed,
        HbtDbSeedVariable variableSeed,
        HbtDbSeedForm formSeed)
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
        _generatorDictTypeSeed = generatorDictTypeSeed;
        _generatorDictDataSeed = generatorDictDataSeed;
        _coreSeedTranslation = coreSeedTranslation;
        _generatorSeedTranslation = generatorSeedTranslation;
        _identitySeedTranslation = identitySeedTranslation;
        _logsSeedTranslation = logsSeedTranslation;
        _routineSeedTranslation = routineSeedTranslation;
        _signalRSeedTranslation = signalRSeedTranslation;
        _genConfigSeed = genConfigSeed;
        _genTemplateSeed = genTemplateSeed;
        _workflowDictTypeSeed = workflowDictTypeSeed;
        _workflowDictDataSeed = workflowDictDataSeed;
        _workflowSeedTranslation = workflowSeedTranslation;
        _definitionSeed = definitionSeed;
        _instanceSeed = instanceSeed;
        _nodeSeed = nodeSeed;
        _taskSeed = taskSeed;
        _variableSeed = variableSeed;
        _formSeed = formSeed;
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

            // 初始化角色数据
            var (roleInsertCount, roleUpdateCount) = await _roleSeed.InitializeRoleAsync();
            _logger.Info($"角色数据初始化完成: 新增 {roleInsertCount} 条, 更新 {roleUpdateCount} 条");

            // 初始化用户数据
            var (userInsertCount, userUpdateCount) = await _userSeed.InitializeUserAsync();
            _logger.Info($"用户数据初始化完成: 新增 {userInsertCount} 条, 更新 {userUpdateCount} 条");

            // 初始化菜单数据
            var (menuInsertCount, menuUpdateCount) = await _menuSeed.InitializeMenuAsync();
            _logger.Info($"菜单数据初始化完成: 新增 {menuInsertCount} 条, 更新 {menuUpdateCount} 条");

            // 初始化语言数据
            var (languageInsertCount, languageUpdateCount) = await _languageSeed.InitializeLanguageAsync();
            _logger.Info($"语言数据初始化完成: 新增 {languageInsertCount} 条, 更新 {languageUpdateCount} 条");

            // 初始化翻译数据
            var (translationInsertCount, translationUpdateCount) = await _translationSeed.InitializeTranslationAsync();
            _logger.Info($"翻译数据初始化完成: 新增 {translationInsertCount} 条, 更新 {translationUpdateCount} 条");

            // 初始化配置数据
            var (configInsertCount, configUpdateCount) = await _configSeed.InitializeConfigAsync();
            _logger.Info($"配置数据初始化完成: 新增 {configInsertCount} 条, 更新 {configUpdateCount} 条");

            // 初始化字典类型数据
            var (dictTypeInsertCount, dictTypeUpdateCount) = await _dictTypeSeed.InitializeDictTypeAsync();
            _logger.Info($"字典类型数据初始化完成: 新增 {dictTypeInsertCount} 条, 更新 {dictTypeUpdateCount} 条");

            // 初始化字典数据
            var (dictDataInsertCount, dictDataUpdateCount) = await _dictDataSeed.InitializeDictDataAsync();
            _logger.Info($"字典数据初始化完成: 新增 {dictDataInsertCount} 条, 更新 {dictDataUpdateCount} 条");

            // 初始化部门数据
            var (deptInsertCount, deptUpdateCount) = await _deptSeed.InitializeDeptAsync();
            _logger.Info($"部门数据初始化完成: 新增 {deptInsertCount} 条, 更新 {deptUpdateCount} 条");

            // 初始化岗位数据
            var (postInsertCount, postUpdateCount) = await _postSeed.InitializePostAsync();
            _logger.Info($"岗位数据初始化完成: 新增 {postInsertCount} 条, 更新 {postUpdateCount} 条");

            // 初始化OA字典类型数据
            var (oaDictTypeInsertCount, oaDictTypeUpdateCount) = await _oaDictTypeSeed.InitializeOADictTypeAsync();
            _logger.Info($"OA字典类型数据初始化完成: 新增 {oaDictTypeInsertCount} 条, 更新 {oaDictTypeUpdateCount} 条");

            // 初始化OA字典数据
            var (oaDictDataInsertCount, oaDictDataUpdateCount) = await _oaDictDataSeed.InitializeOADictDataAsync();
            _logger.Info($"OA字典数据初始化完成: 新增 {oaDictDataInsertCount} 条, 更新 {oaDictDataUpdateCount} 条");

            // 初始化客户服务字典类型数据
            var (csDictTypeInsertCount, csDictTypeUpdateCount) = await _csDictTypeSeed.InitializeCsDictTypeAsync();
            _logger.Info($"客户服务字典类型数据初始化完成: 新增 {csDictTypeInsertCount} 条, 更新 {csDictTypeUpdateCount} 条");

            // 初始化客户服务字典数据
            var (csDictDataInsertCount, csDictDataUpdateCount) = await _csDictDataSeed.InitializeCsDictDataAsync();
            _logger.Info($"客户服务字典数据初始化完成: 新增 {csDictDataInsertCount} 条, 更新 {csDictDataUpdateCount} 条");

            // 初始化设备字典类型数据
            var (equipmentDictTypeInsertCount, equipmentDictTypeUpdateCount) = await _equipmentDictTypeSeed.InitializeEquipmentDictTypeAsync();
            _logger.Info($"设备字典类型数据初始化完成: 新增 {equipmentDictTypeInsertCount} 条, 更新 {equipmentDictTypeUpdateCount} 条");

            // 初始化设备字典数据
            var (equipmentDictDataInsertCount, equipmentDictDataUpdateCount) = await _equipmentDictDataSeed.InitializeEquipmentDictDataAsync();
            _logger.Info($"设备字典数据初始化完成: 新增 {equipmentDictDataInsertCount} 条, 更新 {equipmentDictDataUpdateCount} 条");

            // 初始化财务字典类型数据
            var (financeDictTypeInsertCount, financeDictTypeUpdateCount) = await _financeDictTypeSeed.InitializeFinanceDictTypeAsync();
            _logger.Info($"财务字典类型数据初始化完成: 新增 {financeDictTypeInsertCount} 条, 更新 {financeDictTypeUpdateCount} 条");

            // 初始化财务字典数据
            var (financeDictDataInsertCount, financeDictDataUpdateCount) = await _financeDictDataSeed.InitializeFinanceDictDataAsync();
            _logger.Info($"财务字典数据初始化完成: 新增 {financeDictDataInsertCount} 条, 更新 {financeDictDataUpdateCount} 条");

            // 初始化人力资源字典类型数据
            var (hrDictTypeInsertCount, hrDictTypeUpdateCount) = await _hrDictTypeSeed.InitializeHrDictTypeAsync();
            _logger.Info($"人力资源字典类型数据初始化完成: 新增 {hrDictTypeInsertCount} 条, 更新 {hrDictTypeUpdateCount} 条");

            // 初始化人力资源字典数据
            var (hrDictDataInsertCount, hrDictDataUpdateCount) = await _hrDictDataSeed.InitializeHrDictDataAsync();
            _logger.Info($"人力资源字典数据初始化完成: 新增 {hrDictDataInsertCount} 条, 更新 {hrDictDataUpdateCount} 条");

            // 初始化工业字典类型数据
            var (indDictTypeInsertCount, indDictTypeUpdateCount) = await _indDictTypeSeed.InitializeIndDictTypeAsync();
            _logger.Info($"工业字典类型数据初始化完成: 新增 {indDictTypeInsertCount} 条, 更新 {indDictTypeUpdateCount} 条");

            // 初始化工业字典数据
            var (indDictDataInsertCount, indDictDataUpdateCount) = await _indDictDataSeed.InitializeIndDictDataAsync();
            _logger.Info($"工业字典数据初始化完成: 新增 {indDictDataInsertCount} 条, 更新 {indDictDataUpdateCount} 条");

            // 初始化物料字典类型数据
            var (materialDictTypeInsertCount, materialDictTypeUpdateCount) = await _materialDictTypeSeed.InitializeMaterialDictTypeAsync();
            _logger.Info($"物料字典类型数据初始化完成: 新增 {materialDictTypeInsertCount} 条, 更新 {materialDictTypeUpdateCount} 条");

            // 初始化物料字典数据
            var (materialDictDataInsertCount, materialDictDataUpdateCount) = await _materialDictDataSeed.InitializeMaterialDictDataAsync();
            _logger.Info($"物料字典数据初始化完成: 新增 {materialDictDataInsertCount} 条, 更新 {materialDictDataUpdateCount} 条");

            // 初始化自然字典类型数据
            var (natureDictTypeInsertCount, natureDictTypeUpdateCount) = await _natureDictTypeSeed.InitializeNatureDictTypeAsync();
            _logger.Info($"自然字典类型数据初始化完成: 新增 {natureDictTypeInsertCount} 条, 更新 {natureDictTypeUpdateCount} 条");

            // 初始化自然字典数据
            var (natureDictDataInsertCount, natureDictDataUpdateCount) = await _natureDictDataSeed.InitializeNatureDictDataAsync();
            _logger.Info($"自然字典数据初始化完成: 新增 {natureDictDataInsertCount} 条, 更新 {natureDictDataUpdateCount} 条");

            // 初始化文件字典类型数据
            var (fileDictTypeInsertCount, fileDictTypeUpdateCount) = await _fileDictTypeSeed.InitializeFileDictTypeAsync();
            _logger.Info($"文件字典类型数据初始化完成: 新增 {fileDictTypeInsertCount} 条, 更新 {fileDictTypeUpdateCount} 条");

            // 初始化文件字典数据
            var (fileDictDataInsertCount, fileDictDataUpdateCount) = await _fileDictDataSeed.InitializeFileDictDataAsync();
            _logger.Info($"文件字典数据初始化完成: 新增 {fileDictDataInsertCount} 条, 更新 {fileDictDataUpdateCount} 条");

            // 初始化生成器字典类型数据
            var (generatorDictTypeInsertCount, generatorDictTypeUpdateCount) = await _generatorDictTypeSeed.InitializeGeneratorDictTypeAsync();
            _logger.Info($"生成器字典类型数据初始化完成: 新增 {generatorDictTypeInsertCount} 条, 更新 {generatorDictTypeUpdateCount} 条");

            // 初始化生成器字典数据
            var (generatorDictDataInsertCount, generatorDictDataUpdateCount) = await _generatorDictDataSeed.InitializeGeneratorDictDataAsync();
            _logger.Info($"生成器字典数据初始化完成: 新增 {generatorDictDataInsertCount} 条, 更新 {generatorDictDataUpdateCount} 条");

            // 初始化工作流字典类型数据
            var (workflowDictTypeInsertCount, workflowDictTypeUpdateCount) = await _workflowDictTypeSeed.InitializeWorkflowDictTypeAsync();
            _logger.Info($"工作流字典类型数据初始化完成: 新增 {workflowDictTypeInsertCount} 条, 更新 {workflowDictTypeUpdateCount} 条");

            // 初始化工作流字典数据
            var (workflowDictDataInsertCount, workflowDictDataUpdateCount) = await _workflowDictDataSeed.InitializeWorkflowDictDataAsync();
            _logger.Info($"工作流字典数据初始化完成: 新增 {workflowDictDataInsertCount} 条, 更新 {workflowDictDataUpdateCount} 条");

            // 初始化表单数据
            var (formInsertCount, formUpdateCount) = await _formSeed.InitializeFormAsync();
            _logger.Info($"表单数据初始化完成: 新增 {formInsertCount} 条, 更新 {formUpdateCount} 条");

            // 初始化工作流定义数据
            var (definitionInsertCount, definitionUpdateCount) = await _definitionSeed.InitializeDefinitionAsync();
            _logger.Info($"工作流定义数据初始化完成: 新增 {definitionInsertCount} 条, 更新 {definitionUpdateCount} 条");

            // 初始化工作流实例数据
            var (instanceInsertCount, instanceUpdateCount) = await _instanceSeed.InitializeInstanceAsync();
            _logger.Info($"工作流实例数据初始化完成: 新增 {instanceInsertCount} 条, 更新 {instanceUpdateCount} 条");

            // 初始化工作流节点数据
            var (nodeInsertCount, nodeUpdateCount) = await _nodeSeed.InitializeNodeAsync();
            _logger.Info($"工作流节点数据初始化完成: 新增 {nodeInsertCount} 条, 更新 {nodeUpdateCount} 条");

            // 初始化工作流任务数据
            var (taskInsertCount, taskUpdateCount) = await _taskSeed.InitializeTaskAsync();
            _logger.Info($"工作流任务数据初始化完成: 新增 {taskInsertCount} 条, 更新 {taskUpdateCount} 条");

            // 初始化工作流变量数据
            var (variableInsertCount, variableUpdateCount) = await _variableSeed.InitializeVariableAsync();
            _logger.Info($"工作流变量数据初始化完成: 新增 {variableInsertCount} 条, 更新 {variableUpdateCount} 条");

            // 初始化生成器配置数据
            var (genConfigInsertCount, genConfigUpdateCount) = await _genConfigSeed.InitializeGenConfigAsync();
            _logger.Info($"生成器配置数据初始化完成: 新增 {genConfigInsertCount} 条, 更新 {genConfigUpdateCount} 条");

            // 初始化生成器模板数据
            var (genTemplateInsertCount, genTemplateUpdateCount) = await _genTemplateSeed.InitializeGenTemplateAsync();
            _logger.Info($"生成器模板数据初始化完成: 新增 {genTemplateInsertCount} 条, 更新 {genTemplateUpdateCount} 条");

            // 初始化核心翻译数据
            var (coreTranslationInsertCount, coreTranslationUpdateCount) = await _coreSeedTranslation.InitializeCoreTranslationAsync();
            _logger.Info($"核心翻译数据初始化完成: 新增 {coreTranslationInsertCount} 条, 更新 {coreTranslationUpdateCount} 条");

            // 初始化生成器翻译数据
            var (generatorTranslationInsertCount, generatorTranslationUpdateCount) = await _generatorSeedTranslation.InitializeGeneratorTranslationAsync();
            _logger.Info($"生成器翻译数据初始化完成: 新增 {generatorTranslationInsertCount} 条, 更新 {generatorTranslationUpdateCount} 条");

            // 初始化身份翻译数据
            var (identityTranslationInsertCount, identityTranslationUpdateCount) = await _identitySeedTranslation.InitializeIdentityTranslationAsync();
            _logger.Info($"身份翻译数据初始化完成: 新增 {identityTranslationInsertCount} 条, 更新 {identityTranslationUpdateCount} 条");

            // 初始化日志翻译数据
            var (logsTranslationInsertCount, logsTranslationUpdateCount) = await _logsSeedTranslation.InitializeLogsTranslationAsync();
            _logger.Info($"日志翻译数据初始化完成: 新增 {logsTranslationInsertCount} 条, 更新 {logsTranslationUpdateCount} 条");

            // 初始化例行翻译数据
            var (routineTranslationInsertCount, routineTranslationUpdateCount) = await _routineSeedTranslation.InitializeRoutineTranslationAsync();
            _logger.Info($"例行翻译数据初始化完成: 新增 {routineTranslationInsertCount} 条, 更新 {routineTranslationUpdateCount} 条");

            // 初始化信号R翻译数据
            var (signalRTranslationInsertCount, signalRTranslationUpdateCount) = await _signalRSeedTranslation.InitializeSignalRTranslationAsync();
            _logger.Info($"信号R翻译数据初始化完成: 新增 {signalRTranslationInsertCount} 条, 更新 {signalRTranslationUpdateCount} 条");

            // 初始化工作流翻译数据
            var (workflowTranslationInsertCount, workflowTranslationUpdateCount) = await _workflowSeedTranslation.InitializeWorkflowTranslationAsync();
            _logger.Info($"工作流翻译数据初始化完成: 新增 {workflowTranslationInsertCount} 条, 更新 {workflowTranslationUpdateCount} 条");

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
