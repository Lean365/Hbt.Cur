//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGeneratorCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-04-25
// 版本号 : V0.0.1
// 描述    : 代码生成服务集合扩展
//===================================================================

using Hbt.Cur.Application.Services.Generator;
using Hbt.Cur.Application.Services.Generator.CodeGenerator;
using Hbt.Cur.Application.Services.Generator.CodeGenerator.Templates;
using Hbt.Cur.Application.Services.Generator.CodeGenerator.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Hbt.Cur.Infrastructure.Extensions
{
    /// <summary>
    /// 代码生成服务集合扩展
    /// </summary>
    public static class HbtGeneratorCollectionExtensions
    {
        /// <summary>
        /// 添加代码生成服务
        /// </summary>
        /// <remarks>
        /// 注册代码生成相关的所有服务，包括：
        /// 1. 代码生成表服务 - 管理代码生成表
        /// 2. 代码生成表定义服务 - 管理表定义
        /// 3. 代码生成配置服务 - 管理生成配置
        /// 4. 代码生成器服务 - 负责代码生成
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <param name="webHostEnvironment">Web主机环境</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddGeneratorServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            // 注册代码生成配置
            var config = new HbtCodeGenerationConfig(webHostEnvironment);
            var templatePathConfig = new HbtTemplatePathConfig(webHostEnvironment);
            configuration.GetSection("CodeGeneration:TemplatePaths").Bind(templatePathConfig);
            config.TemplatePaths = templatePathConfig;
            
            configuration.GetSection("CodeGeneration").Bind(config, options => options.BindNonPublicProperties = true);
            services.AddSingleton(config);

            // 注册代码生成表服务
            services.AddScoped<IHbtGenTableService, HbtGenTableService>();
            services.AddScoped<IHbtGenTableDefineService, HbtGenTableDefineService>();
            services.AddScoped<IHbtGenConfigService, HbtGenConfigService>();
            services.AddScoped<IHbtCodeGeneratorService, HbtCodeGeneratorService>();
            services.AddScoped<IHbtTemplateEngine, HbtTemplateEngine>();
            services.AddScoped<IHbtGenTemplateService, HbtGenTemplateService>();

            return services;
        }
    }
} 