//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtSwaggerSetup.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : Swagger配置
//===================================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Lean.Hbt.Infrastructure.Swagger
{
    /// <summary>
    /// Swagger配置
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public static class HbtSwaggerSetup
    {
        /// <summary>
        /// 添加Swagger服务
        /// </summary>
        /// <param name="services">服务集合</param>
        public static void AddHbtSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // 系统管理接口文档
                c.SwaggerDoc("system", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 系统管理", 
                    Version = "v1",
                    Description = "系统配置、字典管理、多语言等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

                // 身份认证接口文档
                c.SwaggerDoc("identity", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 身份认证", 
                    Version = "v1",
                    Description = "用户、角色、权限等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

                // 审计日志接口文档
                c.SwaggerDoc("audit", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 审计日志", 
                    Version = "v1",
                    Description = "审计日志、操作日志、异常日志等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

                // 实时通讯接口文档
                c.SwaggerDoc("realtime", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 实时通讯", 
                    Version = "v1",
                    Description = "在线用户、即时消息等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

                // 代码生成接口文档
                c.SwaggerDoc("generate", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 代码生成", 
                    Version = "v1",
                    Description = "代码生成、模板管理等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

                // 工作流程接口文档
                c.SwaggerDoc("workflow", new OpenApiInfo 
                { 
                    Title = "黑冰台代码生成管理系统 - 工作流程", 
                    Version = "v1",
                    Description = "工作流程、流程设计等",
                    Contact = new OpenApiContact
                    {
                        Name = "Lean365",
                        Email = "support@lean365.com",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/Lean365/Lean.Hbt/blob/master/LICENSE")
                    }
                });

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

                // 添加XML注释
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlFile = $"{currentAssembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                // 添加 WebApi 项目的 XML 文档
                var webApiXmlFile = "Lean.Hbt.WebApi.xml";
                var webApiXmlPath = Path.Combine(AppContext.BaseDirectory, webApiXmlFile);
                if (File.Exists(webApiXmlPath))
                {
                    c.IncludeXmlComments(webApiXmlPath, true);
                }

                // 添加 Application 项目的 XML 文档
                var applicationXmlFile = "Lean.Hbt.Application.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
                if (File.Exists(applicationXmlPath))
                {
                    c.IncludeXmlComments(applicationXmlPath, true);
                }

                // 添加 Domain 项目的 XML 文档
                var domainXmlFile = "Lean.Hbt.Domain.xml";
                var domainXmlPath = Path.Combine(AppContext.BaseDirectory, domainXmlFile);
                if (File.Exists(domainXmlPath))
                {
                    c.IncludeXmlComments(domainXmlPath, true);
                }

                // 添加API操作的中文描述
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor.RouteValues;
                    var actionName = controllerAction["action"];
                    var controllerName = controllerAction["controller"];
                    return $"{controllerName}_{actionName}";
                });

                // 自定义API操作的展示名称
                c.DocumentFilter<SwaggerOperationFilter>();

                // 对Action的名称进行排序，如果有多个，就可以看到效果了
                c.OrderActionsBy(o => o.RelativePath);

                // 根据ApiModule特性分组
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
                    
                    var controllerType = methodInfo.DeclaringType;
                    var moduleAttr = controllerType.GetCustomAttribute<ApiModuleAttribute>();
                    
                    // 如果控制器没有标记模块,归入默认模块
                    var moduleName = (moduleAttr?.Name ?? "system").ToLower();
                    return docName == moduleName;
                });
            });
        }

        /// <summary>
        /// 使用Swagger中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public static void UseHbtSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/system/swagger.json", "系统管理");
                c.SwaggerEndpoint("/swagger/identity/swagger.json", "身份认证");
                c.SwaggerEndpoint("/swagger/audit/swagger.json", "审计日志");
                c.SwaggerEndpoint("/swagger/realtime/swagger.json", "实时通讯");
                 c.SwaggerEndpoint("/swagger/generate/swagger.json", "代码生成");
                  c.SwaggerEndpoint("/swagger/workflow/swagger.json", "工作流程");
                c.RoutePrefix = "swagger";
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
                c.DefaultModelExpandDepth(1);
                c.EnableDeepLinking();
                c.DisplayRequestDuration();
                c.EnableFilter();
                c.ShowExtensions();
            });
        }
    }

    /// <summary>
    /// Swagger文档过滤器
    /// </summary>
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 应用文档过滤器
        /// </summary>
        /// <param name="swaggerDoc">Swagger文档</param>
        /// <param name="context">文档过滤器上下文</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // 设置分组的展示顺序
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "系统管理", Description = "系统配置、字典管理、多语言等" },
                new OpenApiTag { Name = "身份认证", Description = "用户、角色、权限等" },
                new OpenApiTag { Name = "审计日志", Description = "审计日志、操作日志、异常日志等" },
                new OpenApiTag { Name = "实时通讯", Description = "在线用户、即时消息等" },
                new OpenApiTag { Name = "代码生成", Description = "代码生成、模板管理等" },
                new OpenApiTag { Name = "工作流程", Description = "工作流程、流程设计等" },
                new OpenApiTag { Name = "其他", Description = "未分类的接口" }
            };

            // 如果是代码生成或工作流程模块，但没有任何接口，添加一个空的路径
            if (swaggerDoc.Paths.Count == 0)
            {
                if (swaggerDoc.Info.Title.Contains("代码生成") || swaggerDoc.Info.Title.Contains("工作流程"))
                {
                    swaggerDoc.Paths.Add("/", new OpenApiPathItem
                    {
                        Description = "模块正在开发中..."
                    });
                }
            }
        }
    }

    /// <summary>
    /// Swagger操作过滤器
    /// </summary>
    public class SwaggerOperationFilter : IDocumentFilter
    {
        /// <summary>
        /// 应用操作过滤器
        /// </summary>
        /// <param name="swaggerDoc">Swagger文档</param>
        /// <param name="context">文档过滤器上下文</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths)
            {
                // GET 操作
                if (path.Value.Operations.ContainsKey(OperationType.Get))
                {
                    path.Value.Operations[OperationType.Get].Summary = "获取数据";
                    path.Value.Operations[OperationType.Get].Description = "获取指定条件的数据";
                }

                // POST 操作
                if (path.Value.Operations.ContainsKey(OperationType.Post))
                {
                    path.Value.Operations[OperationType.Post].Summary = "新增数据";
                    path.Value.Operations[OperationType.Post].Description = "新增一条数据";
                }

                // PUT 操作
                if (path.Value.Operations.ContainsKey(OperationType.Put))
                {
                    path.Value.Operations[OperationType.Put].Summary = "更新数据";
                    path.Value.Operations[OperationType.Put].Description = "更新指定数据";
                }

                // DELETE 操作
                if (path.Value.Operations.ContainsKey(OperationType.Delete))
                {
                    path.Value.Operations[OperationType.Delete].Summary = "删除数据";
                    path.Value.Operations[OperationType.Delete].Description = "删除指定数据";
                }
            }
        }
    }
} 