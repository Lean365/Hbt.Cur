//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGeneratorSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 代码生成器本地化资源种子
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成器本地化资源种子
/// </summary>
public class HbtGeneratorSeedTranslation
{
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtGeneratorSeedTranslation(IHbtRepository<HbtTranslation> translationRepository, IHbtLogger logger)
    {
        _translationRepository = translationRepository;
        _logger = logger;
    }

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = "Generator",
            Status = 0,
            TransBuiltin = 1,
            TenantId = 0,
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
    }

    /// <summary>
    /// 初始化代码生成器本地化资源
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeGeneratorTranslationAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var translations = new List<HbtTranslation>
        {
            // 代码生成器模块
            CreateTranslation("zh-CN", "generator.module.name", "代码生成器"),
            CreateTranslation("en-US", "generator.module.name", "Code Generator"),
            CreateTranslation("zh-CN", "generator.module.description", "快速生成代码的工具"),
            CreateTranslation("en-US", "generator.module.description", "A tool for quickly generating code"),

            // 代码生成器页面
            CreateTranslation("zh-CN", "generator.page.title", "代码生成"),
            CreateTranslation("en-US", "generator.page.title", "Code Generation"),
            CreateTranslation("zh-CN", "generator.page.description", "选择模板并生成代码"),
            CreateTranslation("en-US", "generator.page.description", "Select template and generate code"),

            // 代码生成器操作
            CreateTranslation("zh-CN", "generator.action.generate", "生成代码"),
            CreateTranslation("en-US", "generator.action.generate", "Generate Code"),
            CreateTranslation("zh-CN", "generator.action.preview", "预览代码"),
            CreateTranslation("en-US", "generator.action.preview", "Preview Code"),
            CreateTranslation("zh-CN", "generator.action.download", "下载代码"),
            CreateTranslation("en-US", "generator.action.download", "Download Code"),

            // 代码生成器模板
            CreateTranslation("zh-CN", "generator.template.entity", "实体类模板"),
            CreateTranslation("en-US", "generator.template.entity", "Entity Template"),
            CreateTranslation("zh-CN", "generator.template.repository", "仓储类模板"),
            CreateTranslation("en-US", "generator.template.repository", "Repository Template"),
            CreateTranslation("zh-CN", "generator.template.service", "服务类模板"),
            CreateTranslation("en-US", "generator.template.service", "Service Template"),
            CreateTranslation("zh-CN", "generator.template.controller", "控制器模板"),
            CreateTranslation("en-US", "generator.template.controller", "Controller Template"),

            // 代码生成器配置
            CreateTranslation("zh-CN", "generator.config.namespace", "命名空间"),
            CreateTranslation("en-US", "generator.config.namespace", "Namespace"),
            CreateTranslation("zh-CN", "generator.config.author", "作者"),
            CreateTranslation("en-US", "generator.config.author", "Author"),
            CreateTranslation("zh-CN", "generator.config.date", "创建日期"),
            CreateTranslation("en-US", "generator.config.date", "Create Date"),

            // 代码生成器状态
            CreateTranslation("zh-CN", "generator.status.generating", "正在生成"),
            CreateTranslation("en-US", "generator.status.generating", "Generating"),
            CreateTranslation("zh-CN", "generator.status.success", "生成成功"),
            CreateTranslation("en-US", "generator.status.success", "Generation Successful"),
            CreateTranslation("zh-CN", "generator.status.failed", "生成失败"),
            CreateTranslation("en-US", "generator.status.failed", "Generation Failed")
        };

        foreach (var translation in translations)
        {
            var existingTranslation = await _translationRepository.GetFirstAsync(x => 
                x.LangCode == translation.LangCode && 
                x.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                await _translationRepository.CreateAsync(translation);
                insertCount++;
            }
            else
            {
                existingTranslation.TransValue = translation.TransValue;
                existingTranslation.UpdateBy = translation.UpdateBy;
                existingTranslation.UpdateTime = translation.UpdateTime;
                await _translationRepository.UpdateAsync(existingTranslation);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
} 