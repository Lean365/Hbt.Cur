//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTemplateEngine.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 模板引擎实现
//===================================================================

using Lean.Hbt.Application.Services.Generator.CodeGenerator.Models;
using Lean.Hbt.Domain.IServices.Extensions;
using Scriban;
using Scriban.Runtime;

namespace Lean.Hbt.Application.Services.Generator.CodeGenerator.Templates;

/// <summary>
/// 模板引擎实现
/// </summary>
public class HbtTemplateEngine : IHbtTemplateEngine
{
    private readonly Dictionary<string, Template> _templateCache;
    private readonly HbtCodeGenerationConfig _config;
    private readonly IHbtLogger _logger;

    public HbtTemplateEngine(HbtCodeGenerationConfig config, IHbtLogger logger)
    {
        _templateCache = new Dictionary<string, Template>();
        _config = config;
        _logger = logger;
    }

    /// <summary>
    /// 渲染模板
    /// </summary>
    public async Task<string> RenderAsync(string template, HbtGeneratorModel model)
    {
        _logger.Debug($"开始渲染模板,模板长度:{template.Length},模型类型:{model.GetType().Name}");
        try
        {
            // 获取或编译模板
            var compiledTemplate = GetOrCompileTemplate(template);
            _logger.Debug($"模板编译成功,模板哈希值:{ComputeHash(template)}");

            // 创建模板上下文
            var context = CreateTemplateContext(model);
            _logger.Debug($"创建模板上下文成功,表名:{model.Table.TableName}");

            // 渲染模板
            var result = await compiledTemplate.RenderAsync(context);
            _logger.Debug($"模板渲染成功,结果长度:{result.Length}");

            // 格式化代码
            var language = GetLanguageType(model.Template.TemplateLanguage);
            var formattedResult = FormatCode(result, language);
            _logger.Info($"模板渲染和格式化完成,语言:{language},结果长度:{formattedResult.Length}");
            return formattedResult;
        }
        catch (Exception ex)
        {
            _logger.Error($"模板渲染失败,表名:{model.Table.TableName}", ex);
            throw new HbtTemplateException("模板渲染失败", ex);
        }
    }

    /// <summary>
    /// 渲染多个模板
    /// </summary>
    public async Task<Dictionary<string, string>> RenderManyAsync(Dictionary<string, string> templates, HbtGeneratorModel model)
    {
        _logger.Debug($"开始批量渲染模板,模板数量:{templates.Count},表名:{model.Table.TableName}");
        var results = new Dictionary<string, string>();
        try
        {
            foreach (var (key, template) in templates)
            {
                _logger.Debug($"开始渲染模板:{key}");
                results[key] = await RenderAsync(template, model);
                _logger.Debug($"模板:{key}渲染完成");
            }
            _logger.Info($"批量渲染模板完成,成功数量:{results.Count}");
            return results;
        }
        catch (Exception ex)
        {
            _logger.Error($"批量渲染模板失败,表名:{model.Table.TableName}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取或编译模板
    /// </summary>
    private Template GetOrCompileTemplate(string template)
    {
        var templateHash = ComputeHash(template);
        _logger.Debug($"获取或编译模板,模板哈希值:{templateHash}");

        if (!_templateCache.TryGetValue(templateHash, out var compiledTemplate))
        {
            _logger.Debug("模板未缓存,开始编译");
            compiledTemplate = Template.Parse(template);
            _templateCache[templateHash] = compiledTemplate;
            _logger.Debug("模板编译完成并已缓存");
        }
        else
        {
            _logger.Debug("使用缓存的模板");
        }

        return compiledTemplate;
    }

    /// <summary>
    /// 创建模板上下文
    /// </summary>
    private TemplateContext CreateTemplateContext(HbtGeneratorModel model)
    {
        var context = new TemplateContext();
        var scriptObject = new ScriptObject();

        // 添加模型到上下文
        scriptObject.Add("table", model.Table);
        scriptObject.Add("template", model.Template);
        scriptObject.Add("options", model.Options);
        scriptObject.Add("config", _config);

        // 字符串转换函数
        scriptObject.Add("pascal_case", new Func<string, string>(ToPascalCase));
        scriptObject.Add("camel_case", new Func<string, string>(ToCamelCase));
        scriptObject.Add("snake_case", new Func<string, string>(ToSnakeCase));
        scriptObject.Add("kebab_case", new Func<string, string>(ToKebabCase));

        // 类型转换函数
        scriptObject.Add("to_csharp_type", new Func<string, string>(DbTypeToCSharpType));
        scriptObject.Add("to_typescript_type", new Func<string, string>(DbTypeToTypeScriptType));
        scriptObject.Add("to_java_type", new Func<string, string>(DbTypeToJavaType));

        // 注释生成函数
        scriptObject.Add("xml_summary", new Func<string, string>(GenerateXmlSummary));
        scriptObject.Add("js_doc", new Func<string, string>(GenerateJsDoc));
        scriptObject.Add("java_doc", new Func<string, string>(GenerateJavaDoc));

        // 代码生成辅助函数
        scriptObject.Add("plural", new Func<string, string>(ToPlural));
        scriptObject.Add("singular", new Func<string, string>(ToSingular));
        scriptObject.Add("is_nullable", new Func<string, bool>(IsNullableType));
        scriptObject.Add("get_default_value", new Func<string, string>(GetDefaultValue));

        context.PushGlobal(scriptObject);
        return context;
    }

    /// <summary>
    /// 计算模板哈希值
    /// </summary>
    private string ComputeHash(string template)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(template);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// 获取语言类型
    /// </summary>
    private string GetLanguageType(int templateLanguage)
    {
        return templateLanguage switch
        {
            1 => "csharp",
            2 => "typescript",
            3 => "javascript",
            4 => "java",
            5 => "vue",
            6 => "html",
            7 => "css",
            8 => "sql",
            _ => "csharp"
        };
    }

    #region 字符串转换辅助方法

    private string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    private string ToCamelCase(string input)
    {
        var pascal = ToPascalCase(input);
        return pascal.Length > 0 ? char.ToLower(pascal[0]) + pascal.Substring(1) : pascal;
    }

    private string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    private string ToKebabCase(string input)
    {
        return ToSnakeCase(input).Replace('_', '-');
    }

    #endregion 字符串转换辅助方法

    #region 类型转换函数

    private string DbTypeToCSharpType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "int",
            "bigint" => "long",
            "varchar" => "string",
            "nvarchar" => "string",
            "datetime" => "DateTime",
            "bit" => "bool",
            "decimal" => "decimal",
            "float" => "float",
            "double" => "double",
            _ => "object"
        };
    }

    private string DbTypeToTypeScriptType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "number",
            "bigint" => "number",
            "varchar" => "string",
            "nvarchar" => "string",
            "datetime" => "Date",
            "bit" => "boolean",
            "decimal" => "number",
            "float" => "number",
            "double" => "number",
            _ => "any"
        };
    }

    private string DbTypeToJavaType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "Integer",
            "bigint" => "Long",
            "varchar" => "String",
            "nvarchar" => "String",
            "datetime" => "Date",
            "bit" => "Boolean",
            "decimal" => "BigDecimal",
            "float" => "Float",
            "double" => "Double",
            _ => "Object"
        };
    }

    #endregion 类型转换函数

    #region 注释生成函数

    private string GenerateXmlSummary(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/// <summary>
/// {description}
/// </summary>";
    }

    private string GenerateJsDoc(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/**
 * {description}
 */";
    }

    private string GenerateJavaDoc(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/**
 * {description}
 */";
    }

    #endregion 注释生成函数

    #region 代码生成辅助函数

    private string ToPlural(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return word.EndsWith("y") ? word[..^1] + "ies" :
               word.EndsWith("s") ? word :
               word + "s";
    }

    private string ToSingular(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return word.EndsWith("ies") ? word[..^3] + "y" :
               word.EndsWith("s") ? word[..^1] :
               word;
    }

    private bool IsNullableType(string type)
    {
        return type.EndsWith("?") || type == "string";
    }

    private string GetDefaultValue(string type)
    {
        return type.ToLower() switch
        {
            "int" => "0",
            "long" => "0L",
            "decimal" => "0M",
            "float" => "0F",
            "double" => "0D",
            "bool" => "false",
            "datetime" => "DateTime.MinValue",
            "string" => "string.Empty",
            _ => "null"
        };
    }

    #endregion 代码生成辅助函数

    #region 代码格式化

    private string FormatCode(string code, string language)
    {
        _logger.Debug($"开始格式化代码,语言:{language},代码长度:{code.Length}");
        try
        {
            var formattedCode = language.ToLower() switch
            {
                "csharp" => FormatCSharpCode(code),
                "typescript" => FormatTypeScriptCode(code),
                "javascript" => FormatJavaScriptCode(code),
                "java" => FormatJavaCode(code),
                "vue" => FormatVueCode(code),
                "html" => FormatHtmlCode(code),
                "css" => FormatCssCode(code),
                "sql" => FormatSqlCode(code),
                _ => code
            };

            _logger.Debug($"代码格式化完成,结果长度:{formattedCode.Length}");
            return formattedCode;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码格式化失败,语言:{language}", ex);
            return code;
        }
    }

    private string FormatCSharpCode(string code)
    {
        // 基本的C#代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 4)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("["))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    private string FormatTypeScriptCode(string code)
    {
        // TypeScript代码格式化
        return FormatJavaScriptCode(code); // TypeScript使用与JavaScript相同的格式化规则
    }

    private string FormatJavaScriptCode(string code)
    {
        // JavaScript代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("["))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    private string FormatJavaCode(string code)
    {
        // Java代码格式化
        return FormatCSharpCode(code); // Java使用与C#相同的格式化规则
    }

    private string FormatVueCode(string code)
    {
        // Vue代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();
        var inScript = false;
        var inStyle = false;
        var inTemplate = false;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 检测区域
            if (trimmedLine.StartsWith("<script"))
            {
                inScript = true;
                inStyle = false;
                inTemplate = false;
            }
            else if (trimmedLine.StartsWith("<style"))
            {
                inScript = false;
                inStyle = true;
                inTemplate = false;
            }
            else if (trimmedLine.StartsWith("<template"))
            {
                inScript = false;
                inStyle = false;
                inTemplate = true;
            }

            // 根据不同区域使用不同的缩进规则
            if (inScript || inStyle)
            {
                // 减少缩进级别
                if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]") || trimmedLine.StartsWith("</"))
                {
                    indentLevel = Math.Max(0, indentLevel - 1);
                }

                // 添加当前行
                if (!string.IsNullOrWhiteSpace(trimmedLine))
                {
                    result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
                }
                else
                {
                    result.AppendLine();
                }

                // 增加缩进级别
                if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("[") || trimmedLine.EndsWith(">"))
                {
                    indentLevel++;
                }
            }
            else
            {
                // template区域使用2空格缩进
                if (trimmedLine.StartsWith("</"))
                {
                    indentLevel = Math.Max(0, indentLevel - 1);
                }

                if (!string.IsNullOrWhiteSpace(trimmedLine))
                {
                    result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
                }
                else
                {
                    result.AppendLine();
                }

                if (trimmedLine.EndsWith(">") && !trimmedLine.StartsWith("</") && !trimmedLine.Contains("/>"))
                {
                    indentLevel++;
                }
            }
        }

        return result.ToString();
    }

    private string FormatHtmlCode(string code)
    {
        // HTML代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("</"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith(">") && !trimmedLine.StartsWith("</") && !trimmedLine.Contains("/>"))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    private string FormatCssCode(string code)
    {
        // CSS代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{"))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    private string FormatSqlCode(string code)
    {
        // SQL代码格式化
        var lines = code.Split('\n');
        var result = new System.Text.StringBuilder();
        var keywords = new[] { "SELECT", "FROM", "WHERE", "GROUP BY", "ORDER BY", "HAVING", "JOIN", "LEFT JOIN", "RIGHT JOIN", "INNER JOIN", "UPDATE", "DELETE", "INSERT", "VALUES" };

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                // 主要关键字新起一行
                var isKeywordLine = keywords.Any(k => trimmedLine.ToUpper().StartsWith(k));
                if (isKeywordLine)
                {
                    result.AppendLine();
                    result.AppendLine(trimmedLine);
                }
                else
                {
                    result.AppendLine($"    {trimmedLine}");
                }
            }
            else
            {
                result.AppendLine();
            }
        }

        return result.ToString();
    }

    #endregion 代码格式化
}

/// <summary>
/// 模板异常
/// </summary>
public class HbtTemplateException : Exception
{
    public HbtTemplateException(string message, Exception innerException = null)
        : base(message, innerException)
    {
    }
}