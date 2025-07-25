using Lean.Hbt.Infrastructure.Data.Seeds;
using Lean.Hbt.Infrastructure.Data.Seeds.Auth;
using Lean.Hbt.Infrastructure.Data.Seeds.Biz;
using Lean.Hbt.Infrastructure.Data.Seeds.Biz.Dict;
using Lean.Hbt.Infrastructure.Data.Seeds.Biz.Translation;
using Lean.Hbt.Infrastructure.Data.Seeds.Generator;
using Lean.Hbt.Infrastructure.Data.Seeds.Workflow;

namespace Lean.Hbt.Infrastructure.Extensions;

/// <summary>
/// 种子数据服务集合扩展
/// </summary>
public static class HbtSeedsCollectionExtensions
{
    /// <summary>
    /// 添加种子数据服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddHbtSeeds(this IServiceCollection services)
    {
        // 注册种子数据服务
        services.AddScoped<HbtDbSeedIdentityTenant>();
        services.AddScoped<HbtDbSeedIdentityRole>();
        services.AddScoped<HbtDbSeedIdentityUser>();
        services.AddScoped<HbtDbSeedMenu>();
        services.AddScoped<HbtDbSeedLanguage>();
        services.AddScoped<HbtDbSeedIdentityDept>();
        services.AddScoped<HbtDbSeedIdentityPost>();
        services.AddScoped<HbtDbSeedConfig>();
        services.AddScoped<HbtDbSeedDictType>();
        services.AddScoped<HbtDbSeedDictData>();
        services.AddScoped<HbtDbSeedTranslation>();
        services.AddScoped<HbtDbSeedOADictType>();
        services.AddScoped<HbtDbSeedOADictData>();
        services.AddScoped<HbtDbSeedCsDictType>();
        services.AddScoped<HbtDbSeedCsDictData>();
        services.AddScoped<HbtDbSeedEquipmentDictType>();
        services.AddScoped<HbtDbSeedEquipmentDictData>();
        services.AddScoped<HbtDbSeedFinanceDictType>();
        services.AddScoped<HbtDbSeedFinanceDictData>();
        services.AddScoped<HbtDbSeedHrDictType>();
        services.AddScoped<HbtDbSeedHrDictData>();
        services.AddScoped<HbtDbSeedIndDictType>();
        services.AddScoped<HbtDbSeedIndDictData>();
        services.AddScoped<HbtDbSeedMaterialDictType>();
        services.AddScoped<HbtDbSeedMaterialDictData>();
        services.AddScoped<HbtDbSeedNatureDictType>();
        services.AddScoped<HbtDbSeedNatureDictData>();
        services.AddScoped<HbtDbSeedFileDictType>();
        services.AddScoped<HbtDbSeedFileDictData>();
        services.AddScoped<HbtDbSeedGeneratorDictType>();
        services.AddScoped<HbtDbSeedGeneratorDictData>();
        services.AddScoped<HbtCoreSeedTranslation>();
        services.AddScoped<HbtGeneratorSeedTranslation>();
        services.AddScoped<HbtIdentitySeedTranslation>();
        services.AddScoped<HbtLogsSeedTranslation>();
        services.AddScoped<HbtRoutineSeedTranslation>();
        services.AddScoped<HbtSignalRSeedTranslation>();
        services.AddScoped<HbtWorkflowSeedTranslation>();
        services.AddScoped<HbtDbSeedGenConfig>();
        services.AddScoped<HbtDbSeedGenTemplate>();
        services.AddScoped<HbtDbSeedWorkflowDictType>();
        services.AddScoped<HbtDbSeedWorkflowDictData>();
        services.AddScoped<HbtDbSeedIdentityRelation>();
        services.AddScoped<HbtDbSeedWorkflowCoordinator>();
        services.AddScoped<HbtDbSeedTranslationCoordinator>();
        services.AddScoped<HbtDbSeedDictCoordinator>();

        // HRM种子数据服务

        services.AddScoped<HbtDbSeedHrmDepartment>();
        services.AddScoped<HbtDbSeedHrmPosition>();
        services.AddScoped<HbtDbSeedHrmEmployee>();
        services.AddScoped<HbtDbSeedHrm>();

        // 注册多库种子数据服务
        services.AddScoped<HbtIdentityDBSeed>();
        services.AddScoped<HbtGeneratorDbSeed>();
        services.AddScoped<HbtWorkflowDbSeed>();
        services.AddScoped<HbtBusinessDbSeed>();

        // 最后注册主种子服务
        services.AddScoped<HbtDbSeed>();

        return services;
    }
}