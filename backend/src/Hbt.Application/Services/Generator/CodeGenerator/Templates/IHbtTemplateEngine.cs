using Hbt.Cur.Application.Services.Generator.CodeGenerator.Models;

namespace Hbt.Cur.Application.Services.Generator.CodeGenerator.Templates;

/// <summary>
/// 模板引擎接口
/// </summary>
public interface IHbtTemplateEngine
{
    /// <summary>
    /// 渲染模板
    /// </summary>
    Task<string> RenderAsync(string template, HbtGeneratorModel model);

    /// <summary>
    /// 渲染多个模板
    /// </summary>
    Task<Dictionary<string, string>> RenderManyAsync(Dictionary<string, string> templates, HbtGeneratorModel model);
} 