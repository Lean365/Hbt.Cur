//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGeneratorDbSeed.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 代码生成数据库种子数据初始化类
//===================================================================

using Hbt.Common.Exceptions;
using Hbt.Infrastructure.Data.Contexts;
using Hbt.Infrastructure.Data.Seeds.Biz.Dict;
using Hbt.Infrastructure.Data.Seeds.Biz.Translation;
using Hbt.Infrastructure.Data.Seeds.Generator;

namespace Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成数据库种子数据初始化类
/// </summary>
public class HbtGeneratorDbSeed
{
    private readonly HbtGeneratorDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedGenConfig _genConfigSeed;
    private readonly HbtDbSeedGenTemplate _genTemplateSeed;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGeneratorDbSeed(
        HbtGeneratorDbContext context,
        IHbtLogger logger,

        HbtDbSeedGenConfig genConfigSeed,
        HbtDbSeedGenTemplate genTemplateSeed)
    {
        _context = context;
        _logger = logger;

        _genConfigSeed = genConfigSeed;
        _genTemplateSeed = genTemplateSeed;
    }

    /// <summary>
    /// 初始化代码生成数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化代码生成数据库种子数据...");


            // 初始化生成器配置数据
            var (genConfigInsertCount, genConfigUpdateCount) = await _genConfigSeed.InitializeGenConfigAsync();
            _logger.Info($"生成器配置数据初始化完成: 新增 {genConfigInsertCount} 条, 更新 {genConfigUpdateCount} 条");

            // 初始化生成器模板数据
            var (genTemplateInsertCount, genTemplateUpdateCount) = await _genTemplateSeed.InitializeGenTemplateAsync();
            _logger.Info($"生成器模板数据初始化完成: 新增 {genTemplateInsertCount} 条, 更新 {genTemplateUpdateCount} 条");


        }
        catch (Exception ex)
        {
            _logger.Error($"代码生成数据库种子数据初始化失败: {ex.Message}", ex);
            throw new HbtException("代码生成数据库种子数据初始化失败", ex);
        }
    }
} 