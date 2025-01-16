//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtServiceCollectionExtensions.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 基础设施服务扩展
//===================================================================

using Lean.Hbt.Infrastructure.Authentication;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Logging;
using Lean.Hbt.Infrastructure.Persistence;
using Lean.Hbt.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 基础设施服务扩展
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public static class HbtServiceCollectionExtensions
    {
        /// <summary>
        /// 添加基础设施服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置接口</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddHbtInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 添加数据库上下文
            services.AddScoped<HbtDbContext>();
            services.AddScoped<HbtDbSeed>();

            // 添加Redis缓存
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "HbtCache:";
            });
            services.AddScoped<IHbtRedisCache, HbtRedisCache>();

            // 添加JWT认证
            services.AddScoped<IHbtJwtHandler, HbtJwtHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["HbtJwt:SecretKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["HbtJwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["HbtJwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

            // 添加日志服务
            services.AddScoped<IHbtLogger>(provider => 
            {
                var type = provider.GetType();
                return new HbtNLogger(type.FullName ?? "Lean.Hbt");
            });

            // 添加Swagger服务
            services.AddHbtSwagger();

            return services;
        }
    }
} 