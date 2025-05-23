//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGenConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成配置种子数据
//===================================================================

using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.IServices.Extensions;
using System.Text.Json;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成配置种子数据
/// </summary>
public class HbtDbSeedGenConfig
{
    private readonly IHbtRepository<HbtGenConfig> _genConfigRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedGenConfig(IHbtRepository<HbtGenConfig> genConfigRepository, IHbtLogger logger)
    {
        _genConfigRepository = genConfigRepository;
        _logger = logger;
    }

    /// <summary>
    /// 获取当前项目路径
    /// </summary>
    private static string GetCurrentProjectPath()
    {
        // 获取当前执行程序所在目录
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        // 获取项目根目录（backend/src/Lean.Hbt）
        var projectPath = Path.GetFullPath(Path.Combine(baseDirectory, "../../../"));
        return projectPath;
    }

    /// <summary>
    /// 初始化代码生成配置数据
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeGenConfigAsync(long tenantId)
    {
        var seedData = GetSeedData();
        var insertCount = 0;
        var updateCount = 0;

        foreach (var config in seedData)
        {
            var existingConfig = await _genConfigRepository.GetFirstAsync(x => x.GenConfigName == config.GenConfigName);

            if (existingConfig == null)
            {
                config.TenantId = tenantId;
                config.CreateBy = "Hbt365";
                config.CreateTime = DateTime.Now;
                config.UpdateBy = "Hbt365";
                config.UpdateTime = DateTime.Now;
                await _genConfigRepository.CreateAsync(config);
                insertCount++;
                _logger.Info($"[创建] 代码生成配置 '{config.GenConfigName}' 创建成功");
            }
            else
            {
                existingConfig.GenConfigName = config.GenConfigName;
                existingConfig.Author = config.Author;
                existingConfig.ModuleName = config.ModuleName;
                existingConfig.PackageName = config.PackageName;
                existingConfig.BusinessName = config.BusinessName;
                existingConfig.FunctionName = config.FunctionName;
                existingConfig.GenType = config.GenType;
                existingConfig.GenTemplateType = config.GenTemplateType;
                existingConfig.GenPath = config.GenPath;
                existingConfig.Options = config.Options;
                existingConfig.Status = config.Status;
                existingConfig.TenantId = tenantId;
                existingConfig.UpdateBy = "Hbt365";
                existingConfig.UpdateTime = DateTime.Now;

                await _genConfigRepository.UpdateAsync(existingConfig);
                updateCount++;
                _logger.Info($"[更新] 代码生成配置 '{existingConfig.GenConfigName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取种子数据
    /// </summary>
    private static IEnumerable<HbtGenConfig> GetSeedData()
    {
        var projectPath = GetCurrentProjectPath();
        
        return new List<HbtGenConfig>
        {
            new HbtGenConfig
            {
                GenConfigName = "Default",
                Author = "Lean365",
                ModuleName = "Core",
                PackageName = "Lean.Hbt",
                BusinessName = "Default",
                FunctionName = "Default",
                GenType = 0,
                GenTemplateType = 0, // 使用wwwroot/Generator/*.scriban模板
                GenPath = "src/Lean.Hbt.WebApi/src",
                Options = JsonSerializer.Serialize(new Dictionary<string, object>
                {
                    { "isSqlDiff", 1 },
                    { "isSnowflakeId", 1 },
                    { "isRepository", 0 },
                    { "crudGroup", new[] { 1, 2, 3, 4, 5, 6, 7, 8 } }
                }),
                Status = 0,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenConfig
            {
                GenConfigName = "CustomTemplate",
                Author = "Lean365",
                ModuleName = "Core",
                PackageName = "Lean.Hbt",
                BusinessName = "Custom",
                FunctionName = "Custom",
                GenType = 2,
                GenTemplateType = 1, // 使用HbtGenTemplate表中的模板
                GenPath = "src/Lean.Hbt.WebApi",
                Options = JsonSerializer.Serialize(new Dictionary<string, object>
                {
                    { "isSqlDiff", 1 },
                    { "isSnowflakeId", 1 },
                    { "isRepository", 1 },
                    { "crudGroup", new[] { 1, 2, 3, 4, 5, 6, 7, 8 } }
                }),
                Status = 0,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}
