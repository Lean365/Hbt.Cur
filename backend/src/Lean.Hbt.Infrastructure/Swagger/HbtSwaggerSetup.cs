//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSwaggerSetup.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 14:30
// 版本号 : V1.0.0
// 描述    : Swagger配置类
//===================================================================


using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Lean.Hbt.Infrastructure.Swagger
{
    /// <summary>
    /// Swagger配置类
    /// </summary>
    public static class HbtSwaggerSetup
    {
        /// <summary>
        /// API模块列表
        /// </summary>
        private static readonly Dictionary<string, string> ApiModules = new()
        {
            { "identity", "身份认证" },
            { "admin", "系统管理" },
            { "workflow", "工作流" },
            { "audit", "审计日志" },
            { "realtime", "实时通信" }
        };

        /// <summary>
        /// 添加Swagger服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddHbtSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // 为每个API模块创建一个SwaggerDoc
                foreach (var module in ApiModules)
                {
                    c.SwaggerDoc(module.Key, new OpenApiInfo
                    {
                        Title = $"Lean.Hbt {module.Value} API",
                        Version = "v1",
                        Description = $"黑冰台 {module.Value} API文档\n\n" +
                                    "## API 使用说明\n" +
                                    "1. 所有请求需要在 Header 中携带 Authorization Token\n" +
                                    "2. 返回格式统一为 ApiResult<T>\n" +
                                    "3. 支持多语言,请在 Header 中设置 Accept-Language\n" +
                                    "4. 分页查询统一使用 PageRequest 对象\n" +
                                    "5. 文件上传请使用 multipart/form-data",
                        Contact = new OpenApiContact
                        {
                            Name = "👨‍💻 Lean365",
                            Email = "support@lean365.com",
                            Url = new Uri("https://www.lean365.com")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "📄 MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                }

                // 添加JWT认证
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\n\n" +
                                "Enter 'Bearer' [space] and then your token in the text input below.\n\n" +
                                "Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // 对所有API进行分组
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actionDescriptor)
                    {
                        var moduleAttr = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<ApiModuleAttribute>();
                        return moduleAttr?.Code == docName;
                    }
                    return false;
                });

                // 添加操作过滤器
                c.OperationFilter<SwaggerOperationFilter>();

                // 自定义架构ID
                c.CustomSchemaIds(type => type.FullName);

                // 添加XML注释
                var xmlFiles = new[] 
                {
                    "Lean.Hbt.Infrastructure.xml",
                    "Lean.Hbt.WebApi.xml",
                    "Lean.Hbt.Application.xml",
                    "Lean.Hbt.Domain.xml"
                };

                foreach (var xmlFile in xmlFiles)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath, true);
                    }
                }

                // 使用完整的类型名称
                c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

                // 配置枚举处理
                c.SchemaFilter<EnumSchemaFilter>();
            });

            return services;
        }

        /// <summary>
        /// 使用Swagger中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <returns>应用程序构建器</returns>
        public static IApplicationBuilder UseHbtSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            
            app.UseSwaggerUI(c =>
            {
                // 为每个API模块添加一个SwaggerEndpoint
                foreach (var module in ApiModules)
                {
                    c.SwaggerEndpoint($"/swagger/{module.Key}/swagger.json", $"Lean.Hbt {module.Value} API");
                }

                // 自定义样式 - 暂时注释掉，因为文件不存在
                // c.InjectStylesheet("/swagger-ui/custom.css");
                // c.InjectJavascript("/swagger-ui/custom.js");
                
                // 配置选项
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "Lean.Hbt API Documentation";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                c.DefaultModelsExpandDepth(2);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });

            return app;
        }
    }
} 