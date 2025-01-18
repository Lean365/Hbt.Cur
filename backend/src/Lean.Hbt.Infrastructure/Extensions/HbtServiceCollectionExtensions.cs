//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServiceCollectionExtensions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:30
// 版本号 : V.0.0.1
// 描述    : 服务集合扩展
//===================================================================

using System.Text;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Authentication;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Logging;
using Lean.Hbt.Infrastructure.Persistence;
using Lean.Hbt.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Lean.Hbt.Infrastructure.Extensions
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class HbtServiceCollectionExtensions
    {
        /// <summary>
        /// 添加基础设施服务
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 1.配置选项
            var connectionConfig = new ConnectionConfig
            {
                ConnectionString = configuration.GetConnectionString("Default"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            services.Configure<ConnectionConfig>(options => 
            {
                options.ConnectionString = connectionConfig.ConnectionString;
                options.DbType = connectionConfig.DbType;
                options.IsAutoCloseConnection = connectionConfig.IsAutoCloseConnection;
            });
            services.Configure<HbtSecurityOptions>(configuration.GetSection("Security"));
            services.Configure<HbtRedisOptions>(configuration.GetSection("Redis"));

            // 2.认证服务
            var jwtOptions = configuration.GetSection("Security:Jwt").Get<HbtJwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.SecretKey ?? string.Empty))
                };
            });

            // 3.基础设施服务
            services.AddSingleton<IHbtLogger, HbtNLogger>();
            
            // 添加Redis分布式缓存
            var redisOptions = configuration.GetSection("Redis").Get<HbtRedisOptions>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisOptions?.ConnectionString;
                options.InstanceName = redisOptions?.InstanceName;
            });
            
            services.AddSingleton<IHbtRedisCache, HbtRedisCache>();
            services.AddScoped<HbtDbContext>();
            services.AddScoped<HbtDbSeed>();
            services.AddScoped<IHbtAuditLog, HbtAuditLog>();
            services.AddScoped<IHbtLoginPolicy, HbtLoginPolicy>();
            services.AddScoped<IHbtPasswordPolicy, HbtPasswordPolicy>();
            services.AddScoped<IHbtSessionManager, HbtSessionManager>();
            services.AddScoped<HbtJwtHandler>();

            return services;
        }
    }
}