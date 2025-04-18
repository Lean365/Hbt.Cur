//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCodeGenerationConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成配置
//===================================================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Lean.Hbt.Application.Services.Generator.CodeGenerator.Models;

/// <summary>
/// 代码生成配置
/// </summary>
public class HbtCodeGenerationConfig
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="webHostEnvironment"> Web主机环境 </param>
    public HbtCodeGenerationConfig(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    /// 包名称
    /// </summary>
    public string? PackageName { get; set; }

    /// <summary>
    /// 基础命名空间
    /// </summary>
    public string? BaseNamespace { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string? GenPath { get; set; }

    /// <summary>
    /// 默认命名空间
    /// </summary>
    public string? DefaultNamespace { get; set; }

    /// <summary>
    /// 输出目录
    /// </summary>
    public string? OutputDirectory { get; set; }

    /// <summary>
    /// 模板列表
    /// </summary>
    public List<HbtGenTemplate>? Templates { get; set; } = new();

    /// <summary>
    /// 模板路径配置
    /// </summary>
    public HbtTemplatePathConfig? TemplatePaths { get; set; }

    /// <summary>
    /// 命名规则
    /// </summary>
    public HbtNamingRules? NamingRules { get; set; } = new();

    /// <summary>
    /// 代码风格
    /// </summary>
    public HbtCodeStyle? CodeStyle { get; set; } = new();

    /// <summary>
    /// 文件路径规则
    /// </summary>
    public HbtFilePathRules? FilePathRules { get; set; } = new();

    /// <summary>
    /// 从配置加载
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <param name="webHostEnvironment">Web主机环境</param>
    /// <returns>代码生成配置</returns>
    public static HbtCodeGenerationConfig FromConfiguration(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        var config = new HbtCodeGenerationConfig(webHostEnvironment);
        configuration.GetSection("CodeGeneration").Bind(config);
        return config;
    }
}

/// <summary>
/// 模板路径配置
/// </summary>
public class HbtTemplatePathConfig
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="webHostEnvironment"> Web主机环境 </param>
    public HbtTemplatePathConfig(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #region 后端模板路径

    /// <summary>
    /// 实体模板路径
    /// </summary>
    public string? EntityTemplate { get; set; }

    /// <summary>
    /// DTO模板路径
    /// </summary>
    public string? DtoTemplate { get; set; }

    /// <summary>
    /// 服务模板路径
    /// </summary>
    public string? ServiceTemplate { get; set; }

    /// <summary>
    /// 控制器模板路径
    /// </summary>
    public string? ControllerTemplate { get; set; }

    #endregion 后端模板路径

    #region 前端模板路径

    /// <summary>
    /// API模板路径
    /// </summary>
    public string? ApiTemplate { get; set; }

    /// <summary>
    /// 国际化模板路径
    /// </summary>
    public string? LocalesTemplate { get; set; }

    /// <summary>
    /// 类型定义模板路径
    /// </summary>
    public string? TypesTemplate { get; set; }

    /// <summary>
    /// 视图模板路径
    /// </summary>
    public string? ViewsTemplate { get; set; }

    #endregion 前端模板路径

    /// <summary>
    /// 获取所有模板路径
    /// </summary>
    public Dictionary<string, string> GetAllTemplatePaths()
    {
        return new Dictionary<string, string>
        {
            // 后端模板
            { "Entity", Path.Combine(_webHostEnvironment.WebRootPath, EntityTemplate) },
            { "Dto", Path.Combine(_webHostEnvironment.WebRootPath, DtoTemplate) },
            { "Service", Path.Combine(_webHostEnvironment.WebRootPath, ServiceTemplate) },
            { "Controller", Path.Combine(_webHostEnvironment.WebRootPath, ControllerTemplate) },

            // 前端模板
            { "Api", Path.Combine(_webHostEnvironment.WebRootPath, ApiTemplate) },
            { "Locales", Path.Combine(_webHostEnvironment.WebRootPath, LocalesTemplate) },
            { "Types", Path.Combine(_webHostEnvironment.WebRootPath, TypesTemplate) },
            { "Views", Path.Combine(_webHostEnvironment.WebRootPath, ViewsTemplate) }
        };
    }
}

/// <summary>
/// 命名规则
/// </summary>
public class HbtNamingRules
{
    /// <summary>
    /// 控制器命名模式
    /// </summary>
    public string ControllerPattern { get; set; } = "{Name}Controller";

    /// <summary>
    /// 服务接口命名模式
    /// </summary>
    public string ServiceInterfacePattern { get; set; } = "I{Name}Service";

    /// <summary>
    /// 服务实现命名模式
    /// </summary>
    public string ServiceImplementationPattern { get; set; } = "{Name}Service";

    /// <summary>
    /// 仓储接口命名模式
    /// </summary>
    public string RepositoryInterfacePattern { get; set; } = "I{Name}Repository";

    /// <summary>
    /// 仓储实现命名模式
    /// </summary>
    public string RepositoryImplementationPattern { get; set; } = "{Name}Repository";

    /// <summary>
    /// DTO命名模式
    /// </summary>
    public string DtoPattern { get; set; } = "{Name}Dto";
}

/// <summary>
/// 代码风格
/// </summary>
public class HbtCodeStyle
{
    /// <summary>
    /// 缩进风格
    /// </summary>
    public string IndentStyle { get; set; } = "space";

    /// <summary>
    /// 缩进大小
    /// </summary>
    public int IndentSize { get; set; } = 4;

    /// <summary>
    /// 换行符
    /// </summary>
    public string NewLine { get; set; } = "\n";

    /// <summary>
    /// 是否使用var
    /// </summary>
    public bool UseVar { get; set; } = true;

    /// <summary>
    /// 是否使用表达式体成员
    /// </summary>
    public bool UseExpressionBodiedMembers { get; set; } = true;
}

/// <summary>
/// 文件路径规则
/// </summary>
public class HbtFilePathRules
{
    /// <summary>
    /// 控制器路径模式
    /// </summary>
    public string ControllerPathPattern { get; set; } = "Controllers/{Module}/{Name}Controller.cs";

    /// <summary>
    /// 服务路径模式
    /// </summary>
    public string ServicePathPattern { get; set; } = "Services/{Module}/{Name}Service.cs";

    /// <summary>
    /// 仓储路径模式
    /// </summary>
    public string RepositoryPathPattern { get; set; } = "Repositories/{Module}/{Name}Repository.cs";

    /// <summary>
    /// DTO路径模式
    /// </summary>
    public string DtoPathPattern { get; set; } = "Dtos/{Module}/{Name}Dto.cs";

    /// <summary>
    /// 实体路径模式
    /// </summary>
    public string EntityPathPattern { get; set; } = "Entities/{Module}/{Name}.cs";
}