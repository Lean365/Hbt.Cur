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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "黑冰台代码生成管理系统",
                    Version = "v1",
                    Description = "基于.NET 8 + Vue 3的代码生成管理系统",
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
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                // 对Action的名称进行排序，如果有多个，就可以看到效果了
                c.OrderActionsBy(o => o.RelativePath);
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "黑冰台代码生成管理系统 V1");
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
} 