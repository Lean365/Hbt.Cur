//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSwaggerSetup.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 14:30
// 版本号 : V1.0.0
// 描述    : Swagger配置类
//===================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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
                        Description = $"黑冰台 {module.Value} API文档",
                        Contact = new OpenApiContact
                        {
                            Name = "Lean365",
                            Email = "support@lean365.com",
                            Url = new Uri("https://www.lean365.com")
                        }
                    });
                }

                // 添加JWT认证
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
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

                // 根据API模块特性对API进行分组
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actionDescriptor)
                    {
                        var moduleAttr = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<ApiModuleAttribute>();
                        return moduleAttr?.Code == docName;
                    }
                    return false;
                });

                // 添加XML注释
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                // 添加WebApi层的XML注释
                var webApiXmlFile = "Lean.Hbt.WebApi.xml";
                var webApiXmlPath = Path.Combine(AppContext.BaseDirectory, webApiXmlFile);
                if (File.Exists(webApiXmlPath))
                {
                    c.IncludeXmlComments(webApiXmlPath);
                }
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // 为每个API模块添加一个SwaggerEndpoint
                foreach (var module in ApiModules)
                {
                    c.SwaggerEndpoint($"/swagger/{module.Key}/swagger.json", $"Lean.Hbt {module.Value} API");
                }

                c.RoutePrefix = "swagger";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });

            return app;
        }
    }
} 