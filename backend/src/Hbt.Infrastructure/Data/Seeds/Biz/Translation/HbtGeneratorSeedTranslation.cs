//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGeneratorSeedTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 代码生成器本地化资源数据提供类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 代码生成器本地化资源数据提供类
/// </summary>
public class HbtGeneratorSeedTranslation
{
    /// <summary>
    /// 获取代码生成器翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetGeneratorTranslations()
    {
        return new List<HbtTranslation>
        {
            // 代码生成器模块
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.module.name", TransValue = "代码生成器", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.module.name", TransValue = "Code Generator", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.module.description", TransValue = "快速生成代码的工具", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.module.description", TransValue = "A tool for quickly generating code", ModuleName = "Backend", Status = 0 },

            // 代码生成器页面
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.page.title", TransValue = "代码生成", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.page.title", TransValue = "Code Generation", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.page.description", TransValue = "选择模板并生成代码", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.page.description", TransValue = "Select template and generate code", ModuleName = "Backend", Status = 0 },

            // 代码生成器操作
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.action.generate", TransValue = "生成代码", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.action.generate", TransValue = "Generate Code", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.action.preview", TransValue = "预览代码", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.action.preview", TransValue = "Preview Code", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.action.download", TransValue = "下载代码", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.action.download", TransValue = "Download Code", ModuleName = "Backend", Status = 0 },

            // 代码生成器模板
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.template.entity", TransValue = "实体类模板", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.template.entity", TransValue = "Entity Template", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.template.repository", TransValue = "仓储类模板", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.template.repository", TransValue = "Repository Template", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.template.service", TransValue = "服务类模板", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.template.service", TransValue = "Service Template", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.template.controller", TransValue = "控制器模板", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.template.controller", TransValue = "Controller Template", ModuleName = "Backend", Status = 0 },

            // 代码生成器配置
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.config.namespace", TransValue = "命名空间", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.config.namespace", TransValue = "Namespace", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.config.author", TransValue = "作者", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.config.author", TransValue = "Author", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.config.date", TransValue = "创建日期", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.config.date", TransValue = "Create Date", ModuleName = "Backend", Status = 0 },

            // 代码生成器状态
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.status.generating", TransValue = "正在生成", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.status.generating", TransValue = "Generating", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.status.success", TransValue = "生成成功", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.status.success", TransValue = "Generation Successful", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "generator.status.failed", TransValue = "生成失败", ModuleName = "Backend", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "generator.status.failed", TransValue = "Generation Failed", ModuleName = "Backend", Status = 0 }
        };
    }
}