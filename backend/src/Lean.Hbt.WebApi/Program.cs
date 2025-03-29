using Lean.Hbt.Common.Utils;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Data.Seeds;
using Lean.Hbt.Infrastructure.Extensions;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Infrastructure.Security;
using Lean.Hbt.Infrastructure.Swagger;
using Lean.Hbt.WebApi.Extensions;
using Lean.Hbt.WebApi.Middlewares;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StackExchange.Redis;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Infrastructure.SignalR;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.SignalR.Cache;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Microsoft.AspNetCore.Routing;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Identity;
using Lean.Hbt.Infrastructure.Identity;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Antiforgery;
using Lean.Hbt.Infrastructure.Jobs;
using Microsoft.AspNetCore.SignalR;
using SqlSugar;

var logger = LogManager.Setup()
                      .LoadConfigurationFromFile("nlog.config")
                      .GetCurrentClassLogger();
try
{
    logger.Info("正在初始化应用程序...");

    var builder = WebApplication.CreateBuilder(args);

    // 添加NLog支持
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    // 配置SignalR日志
    builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", Microsoft.Extensions.Logging.LogLevel.Debug);
    builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", Microsoft.Extensions.Logging.LogLevel.Debug);

    // 配置 Kestrel 服务器
    var serverConfig = builder.Configuration.GetSection("Server").Get<HbtServerConfig>() 
        ?? throw new InvalidOperationException("服务器配置节点不能为空");

    if (serverConfig.HttpPort <= 0)
    {
        throw new InvalidOperationException("HTTP端口配置无效");
    }

    if (serverConfig.UseHttps && serverConfig.HttpsPort <= 0)
    {
        throw new InvalidOperationException("HTTPS端口配置无效");
    }

    // 配置数据库
    var dbConfig = builder.Configuration.GetSection("Database").Get<HbtDbOptions>() ?? throw new InvalidOperationException("数据库配置节点不能为空");

    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.AllowSynchronousIO = true;
        options.MaxRequestBodySize = 30000000;
    });

    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
    });

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MaxRequestHeadersTotalSize = 1024 * 1024; // 设置为1MB
        options.Limits.MaxRequestHeaderCount = 100;
        options.Limits.MaxRequestBodySize = 1024 * 1024 * 100; // 设置为100MB
    });

    // 配置服务器URL
    if (serverConfig.UseHttps)
    {
        builder.WebHost.UseUrls(
            $"https://localhost:{serverConfig.HttpsPort}",
            $"http://localhost:{serverConfig.HttpPort}"
        );
        logger.Info($"已启用HTTPS，监听端口: {serverConfig.HttpsPort}");
        logger.Info($"HTTP请求将被重定向到HTTPS");
    }
    else
    {
        builder.WebHost.UseUrls($"http://localhost:{serverConfig.HttpPort}");
        logger.Info($"仅使用HTTP，监听端口: {serverConfig.HttpPort}");
    }

    // 配置 JSON 序列化
    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore,
        DateFormatString = "yyyy-MM-dd HH:mm:ss"
    };

    // 添加CORS服务
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("HbtPolicy", policy =>
        {
            policy.WithOrigins(builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>())
                  .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                  .WithHeaders(
                      "Content-Type", 
                      "Authorization", 
                      "Accept", 
                      "X-Requested-With", 
                      "X-Tenant-Id", 
                      "Cache-Control", 
                      "Pragma",
                      "X-CSRF-Token",// CSRF Token头
                      "X-XSRF-TOKEN", // XSRF Token头
                      "x-signalr-user-agent",// SignalR用户代理头
                      "X-Device-Info" // 设备信息头
                  )
                  .AllowCredentials()
                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .WithExposedHeaders("X-CSRF-Token", "X-XSRF-TOKEN")
                  .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
        });
    });

    // 添加控制器服务
    builder.Services.AddControllers(options => {
        options.EnableEndpointRouting = true;
    });

    // 添加 HttpClient 服务
    builder.Services.AddHttpClient();

    // 添加 Swagger 服务
    builder.Services.AddHbtSwagger();

    // 配置安全选项
    builder.Services.Configure<HbtSecurityOptions>(builder.Configuration.GetSection("Security"));
    
    // 配置验证码选项
    builder.Services.Configure<HbtCaptchaOptions>(builder.Configuration.GetSection("Captcha"));

    // 添加基础设施服务
    builder.Services.AddInfrastructure(builder.Configuration);

    // 注册当前用户服务
    builder.Services.AddScoped<IHbtCurrentUser, HbtCurrentUser>();

    // 添加领域服务
    builder.Services.AddDomainServices();

    // 添加应用服务
    builder.Services.AddApplicationServices();

    // 添加本地化服务
    builder.Services.AddHbtLocalization();

    // 配置 JWT 认证
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<HbtJwtOptions>() ?? throw new InvalidOperationException("JWT配置节点不能为空");
    builder.Services.Configure<HbtJwtOptions>(builder.Configuration.GetSection("Jwt"));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };

            // 配置SignalR的JWT认证
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/signalr"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

    // 配置 SignalR
    builder.Services.AddSignalR();
    builder.Services.AddMemoryCache();
    builder.Services.AddScoped<IHbtSignalRUserService, HbtSignalRUserService>();
    builder.Services.AddScoped<IHbtSignalRClient>(sp => 
    {
        var hubContext = sp.GetRequiredService<IHubContext<HbtSignalRHub, IHbtSignalRClient>>();
        return hubContext.Clients.All;
    });
    builder.Services.AddScoped<IHbtSignalRHub, HbtSignalRHub>();

    // 配置单点登录
    builder.Services.Configure<HbtSingleSignOnOptions>(builder.Configuration.GetSection("Security:SingleSignOn"));
    builder.Services.AddScoped<IHbtSingleSignOnService, HbtSingleSignOnService>();

    // 根据配置选择SignalR缓存实现
    var cacheSettings = builder.Configuration.GetSection("Cache").Get<HbtCacheOptions>();
    
    // 注册缓存配置管理器（改为Scoped生命周期）
    builder.Services.AddScoped<HbtCacheConfigManager>();
    
    // 注册内存缓存
    builder.Services.AddScoped<IHbtMemoryCache, HbtMemoryCache>();
    
    if (cacheSettings?.Provider == CacheProviderType.Redis && 
        cacheSettings?.Redis?.Enabled == true && 
        !string.IsNullOrEmpty(cacheSettings.Redis.ConnectionString))
    {
        // 配置Redis连接
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheSettings.Redis.ConnectionString;
            options.InstanceName = cacheSettings.Redis.InstanceName;
        });
        builder.Services.AddScoped<IHbtRedisCache, HbtRedisCache>();
        builder.Services.AddScoped<IHbtSignalRCacheService, HbtSignalRRedisCache>();
    }
    else
    {
        builder.Services.AddScoped<IHbtRedisCache, HbtNullRedisCache>();
        builder.Services.AddScoped<IHbtSignalRCacheService, HbtSignalRMemoryCache>();
    }
    
    // 注册缓存工厂
    builder.Services.AddScoped<IHbtCacheFactory, HbtCacheFactory>();

    // 注册HttpContext访问器
    builder.Services.AddHttpContextAccessor();

    // 注册种子数据服务
    builder.Services.AddScoped<HbtDbSeedTenant>();
    builder.Services.AddScoped<HbtDbSeedRole>();
    builder.Services.AddScoped<HbtDbSeedUser>();
    builder.Services.AddScoped<HbtDbSeedMenu>();
    builder.Services.AddScoped<HbtDbSeedLanguage>();
    builder.Services.AddScoped<HbtDbSeedDept>();
    builder.Services.AddScoped<HbtDbSeedPost>();
    builder.Services.AddScoped<HbtDbSeedRelation>();
    builder.Services.AddScoped<HbtDbSeedConfig>();
    builder.Services.AddScoped<HbtDbSeedDictType>();
    builder.Services.AddScoped<HbtDbSeedDictData>();
    builder.Services.AddScoped<HbtDbSeedTranslation>();
    builder.Services.AddScoped<HbtDbSeedOADictType>();
    builder.Services.AddScoped<HbtDbSeedOADictData>();
    builder.Services.AddScoped<HbtDbSeedCsDictType>();
    builder.Services.AddScoped<HbtDbSeedCsDictData>();
    builder.Services.AddScoped<HbtDbSeedEquipmentDictType>();
    builder.Services.AddScoped<HbtDbSeedEquipmentDictData>();
    builder.Services.AddScoped<HbtDbSeedFinanceDictType>();
    builder.Services.AddScoped<HbtDbSeedFinanceDictData>();
    builder.Services.AddScoped<HbtDbSeedHrDictType>();
    builder.Services.AddScoped<HbtDbSeedHrDictData>();
    builder.Services.AddScoped<HbtDbSeedIndDictType>();
    builder.Services.AddScoped<HbtDbSeedIndDictData>();
    builder.Services.AddScoped<HbtDbSeedMaterialDictType>();
    builder.Services.AddScoped<HbtDbSeedMaterialDictData>();
    builder.Services.AddScoped<HbtDbSeedNatureDictType>();
    builder.Services.AddScoped<HbtDbSeedNatureDictData>();
    builder.Services.AddScoped<HbtDbSeedProductionDictType>();
    builder.Services.AddScoped<HbtDbSeedProductionDictData>();
    builder.Services.AddScoped<HbtDbSeedPurchaseDictType>();
    builder.Services.AddScoped<HbtDbSeedPurchaseDictData>();
    builder.Services.AddScoped<HbtDbSeedQualityDictType>();
    builder.Services.AddScoped<HbtDbSeedQualityDictData>();
    builder.Services.AddScoped<HbtDbSeedSalesDictType>();
    builder.Services.AddScoped<HbtDbSeedSalesDictData>();
    builder.Services.AddScoped<HbtDbSeedUnDictType>();
    builder.Services.AddScoped<HbtDbSeedUnDictData>();
    builder.Services.AddScoped<HbtDbSeed>();

    // 添加后台服务
    builder.Services.AddHostedService<HbtLoginPolicyInitializer>();
    // 添加验证码初始化服务
    builder.Services.AddHostedService<HbtCaptchaInitializer>();

    // 注册邮件配置
    builder.Services.Configure<HbtMailOption>(builder.Configuration.GetSection(HbtMailOption.Position));

    // 注册定时任务配置
    builder.Services.Configure<HbtQuartzOption>(builder.Configuration.GetSection(HbtQuartzOption.Position));

    // 配置Quartz服务
    builder.Services.AddQuartz(q =>
    {
        // 从配置中获取Quartz设置
        var quartzOptions = builder.Configuration.GetSection(HbtQuartzOption.Position).Get<HbtQuartzOption>();
        if (quartzOptions != null)
        {
            // 配置Quartz调度器
            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();
            q.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = quartzOptions.ThreadPool.MaxConcurrency;
            });

            // 添加在线用户清理任务
            var jobKey = new JobKey("OnlineUserCleanupJob");
            q.AddJob<HbtOnlineUserCleanupJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("OnlineUserCleanupTrigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1) // 每分钟执行一次
                    .RepeatForever())
            );
        }
    });

    // 添加Quartz托管服务
    builder.Services.AddQuartzHostedService(options =>
    {
        options.WaitForJobsToComplete = true;
    });

    // 注册IScheduler服务
    builder.Services.AddSingleton(provider =>
    {
        var schedulerFactory = provider.GetRequiredService<ISchedulerFactory>();
        return schedulerFactory.GetScheduler().GetAwaiter().GetResult();
    });

    // 初始化邮件服务
    builder.Services.AddTransient(provider =>
    {
        HbtMailHelper.Initialize(provider);
        return provider;
    });

    // 初始化定时任务
    builder.Services.AddTransient(async provider =>
    {
        var options = provider.GetRequiredService<IOptions<HbtQuartzOption>>();
        await HbtQuartzHelper.InitializeAsync(options.Value);
        return provider;
    });

    // 注册系统重启服务
    builder.Services.AddScoped<IHbtSystemRestartService, HbtSystemRestartService>();

    // 注册系统重启配置选项
    builder.Services.Configure<HbtSystemRestartOptions>(
        builder.Configuration.GetSection("SystemRestart"));

    builder.Services.Configure<HbtSignalRCacheOptions>(
        builder.Configuration.GetSection("SignalRCache"));

    builder.Services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-CSRF-TOKEN";
        options.Cookie.Name = "XSRF-TOKEN";
        options.Cookie.HttpOnly = false; // 允许JavaScript访问
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Path = "/";
    });

    var app = builder.Build();

    // 初始化IP位置查询工具
    HbtIpLocationUtils.SetWebHostEnvironment(app.Environment);

    // 初始化数据库和种子数据
    if (dbConfig.Init.InitDatabase || dbConfig.Init.InitSeedData)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HbtDbContext>();

        if (dbConfig.Init.InitDatabase)
        {
            // 检查数据库配置
            var connectionConfigOptions = scope.ServiceProvider.GetRequiredService<IOptions<ConnectionConfig>>();
            if (string.IsNullOrEmpty(connectionConfigOptions.Value.ConnectionString))
            {
                throw new InvalidOperationException("数据库连接字符串不能为空");
            }

            await dbContext.InitializeAsync();
            logger.Info("数据库初始化完成");
        }

        if (dbConfig.Init.InitSeedData)
        {
            var dbSeed = scope.ServiceProvider.GetRequiredService<HbtDbSeed>();
            await dbSeed.InitializeAsync();
            logger.Info("种子数据初始化完成");
        }

        // 执行系统重启清理
        var systemRestartService = scope.ServiceProvider.GetRequiredService<IHbtSystemRestartService>();
        var result = await systemRestartService.ExecuteRestartCleanupAsync();
        if (result)
        {
            logger.Info("系统重启清理完成");
        }
        else
        {
            logger.Warn("系统重启清理失败");
        }
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment() && serverConfig.EnableSwagger)
    {
        app.UseHbtSwagger();
        logger.Info("Swagger已启用");
    }

    // 1. 异常处理中间件（必须放在最前面，以捕获后续中间件中的所有异常）
    app.UseHbtExceptionHandler();
    
    // 2. 会话安全中间件（处理会话相关的安全机制）
    app.UseHbtSessionSecurity();
    
    // 3. SQL注入防护中间件（防止SQL注入攻击）
    app.UseHbtSqlInjection();

    // 4. 跨域资源共享中间件（如果启用）
    if (serverConfig.EnableCors)
    {
        app.UseCors("HbtPolicy");
        logger.Info("CORS已启用");
    }

    // 5. CSRF防护中间件（防止跨站请求伪造攻击）
    app.UseHbtCsrf();

    // 6. HTTPS重定向中间件（如果启用HTTPS）
    if (serverConfig.UseHttps)
    {
        app.UseHttpsRedirection();
        logger.Info("已启用HTTPS重定向");
    }

    // 7. 速率限制中间件（控制请求频率，防止DoS攻击）
    app.UseHbtRateLimit();
    
    // 8. 区分大小写路由中间件（确保路由匹配时区分大小写）
    app.UseHbtCaseSensitiveRoute();

    // 9. 添加路由中间件
    app.UseRouting();
    
    // 10. 添加认证中间件
    app.UseAuthentication();
    app.UseAuthorization();

    // 11. 添加权限中间件
    app.UseHbtPerm();

    // 12. 添加本地化中间件
    app.UseHbtLocalization();

    // 13. 注册所有控制器路由和SignalR Hub
    app.MapControllers();
    app.MapHub<HbtSignalRHub>("/signalr/hbthub");

    // 配置Excel帮助类（用于处理Excel导入导出功能）
    var excelOptions = app.Services.GetRequiredService<IOptions<HbtExcelOptions>>();
    HbtExcelHelper.Configure(excelOptions);

    app.UseAntiforgery();

    // 添加CSRF中间件
    app.Use(async (context, next) =>
    {
        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
        
        // 只在Cookie不存在时生成新的CSRF Token
        if (HttpMethods.IsGet(context.Request.Method) && 
            !context.Request.Cookies.ContainsKey("XSRF-TOKEN"))
        {
            var tokens = antiforgery.GetAndStoreTokens(context);
            context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, 
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddHours(1)
                });
        }
        
        await next(context);
    });

    logger.Info("应用程序启动成功");
    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "应用程序启动失败");
    throw;
}
finally
{
    LogManager.Shutdown();
}