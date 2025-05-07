using Lean.Hbt.Infrastructure.Data.Seeds;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<HbtDbSeedTenant>();
        services.AddScoped<HbtDbSeedRole>();
        services.AddScoped<HbtDbSeedUser>();
        services.AddScoped<HbtDbSeedMenu>();
        services.AddScoped<HbtDbSeedLanguage>();
        services.AddScoped<HbtDbSeedDept>();
        services.AddScoped<HbtDbSeedPost>();
        services.AddScoped<HbtDbSeedRelation>();
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
        services.AddScoped<HbtDbSeedProductionDictType>();
        services.AddScoped<HbtDbSeedProductionDictData>();
        services.AddScoped<HbtDbSeedPurchaseDictType>();
        services.AddScoped<HbtDbSeedPurchaseDictData>();
        services.AddScoped<HbtDbSeedQualityDictType>();
        services.AddScoped<HbtDbSeedQualityDictData>();
        services.AddScoped<HbtDbSeedSalesDictType>();
        services.AddScoped<HbtDbSeedSalesDictData>();
        services.AddScoped<HbtDbSeedUnDictType>();
        services.AddScoped<HbtDbSeedUnDictData>();
        services.AddScoped<HbtCoreSeedTranslation>();
        services.AddScoped<HbtGeneratorSeedTranslation>();
        services.AddScoped<HbtIdentitySeedTranslation>();
        services.AddScoped<HbtLogsSeedTranslation>();
        services.AddScoped<HbtRoutineSeedTranslation>();
        services.AddScoped<HbtSignalRSeedTranslation>();
        services.AddScoped<HbtWorkflowSeedTranslation>();

        // 最后注册主种子服务
        services.AddScoped<HbtDbSeed>();

        return services;
    }
} 