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
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Microsoft.AspNetCore.Routing;

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

    // 配置 Kestrel 服务器
    var serverConfig = builder.Configuration.GetSection("Server").Get<HbtServerConfig>() ?? new HbtServerConfig 
    { 
        UseHttps = false,
        HttpPort = 5249,
        HttpsPort = 7249,
        Init = new HbtInitOptions()
    };
    
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
                  .WithMethods(builder.Configuration.GetSection("Cors:Methods").Get<string[]>() ?? Array.Empty<string>())
                  .WithHeaders(
                      "Content-Type", 
                      "Authorization", 
                      "Accept", 
                      "X-Requested-With", 
                      "X-Tenant-Id", 
                      "Cache-Control", 
                      "Pragma",
                      "X-CSRF-Token",    // CSRF Token头
                      "X-XSRF-TOKEN"     // XSRF Token头
                  )
                  .AllowCredentials()
                  .SetIsOriginAllowed(_ => true) // 允许所有来源
                  .WithExposedHeaders("Set-Cookie"); // 允许前端访问 Set-Cookie 头
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

    // 添加领域服务
    builder.Services.AddDomainServices();

    // 添加应用服务
    builder.Services.AddApplicationServices();

    // 添加本地化服务
    builder.Services.AddHbtLocalization();

    // 配置 JWT 认证
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<HbtJwtOptions>();
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
        });

    // 配置 SignalR
    builder.Services.AddSignalR();
    builder.Services.AddScoped<IHbtSignalRUserService, HbtSignalRUserService>();
    builder.Services.AddScoped<IHbtSignalRMessageNotifier, HbtSignalRMessageNotifier>();

    // 配置缓存
    var cacheSettings = builder.Configuration.GetSection("Cache").Get<HbtCacheOptions>();
    if (cacheSettings?.Provider == CacheProviderType.Redis && 
        cacheSettings?.Redis?.Enabled == true && 
        !string.IsNullOrEmpty(cacheSettings.Redis.ConnectionString))
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheSettings.Redis.ConnectionString;
            options.InstanceName = cacheSettings.Redis.InstanceName;
        });

        // 注册 Redis 连接复用器（单例模式）
        builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
        {
            return ConnectionMultiplexer.Connect(cacheSettings.Redis.ConnectionString);
        });

        // 注册 Redis 缓存服务
        builder.Services.AddScoped<IHbtRedisCache, HbtRedisCache>();
    }
    else
    {
        // 如果未配置Redis或使用内存缓存，使用内存缓存作为备选
        builder.Services.AddDistributedMemoryCache();
        
        // 注册空的Redis实现
        builder.Services.AddScoped<IHbtRedisCache, HbtNullRedisCache>();
    }

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
        q.UseMicrosoftDependencyInjectionJobFactory();
        
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

    var app = builder.Build();

    // 根据配置初始化数据库和种子数据
    if (serverConfig.Init.InitDatabase || serverConfig.Init.InitSeedData)
    {
        using var scope = app.Services.CreateScope();
        
        if (serverConfig.Init.InitDatabase)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HbtDbContext>();
            await dbContext.InitializeAsync();
            logger.Info("数据库初始化完成");
        }

        if (serverConfig.Init.InitSeedData)
        {
            var dbSeed = scope.ServiceProvider.GetRequiredService<HbtDbSeed>();
            await dbSeed.InitializeAsync();
            logger.Info("种子数据初始化完成");
        }
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment() && serverConfig.Init.EnableSwagger)
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
    if (serverConfig.Init.EnableCors)
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
    
    // 9. 身份认证中间件（处理用户认证）
    app.UseAuthentication();
    
    // 10. 授权中间件（处理用户授权）
    app.UseAuthorization();
    
    // 11. 租户中间件（处理多租户隔离）
    app.UseHbtTenant();
    
    // 12. 权限验证中间件（处理细粒度的权限控制）
    app.UseHbtPerm();
    
    // 13. 本地化中间件（处理多语言支持）
    app.UseHbtLocalization();

    // 14. 注册所有控制器路由
    app.MapControllers();

    // 配置Excel帮助类（用于处理Excel导入导出功能）
    var excelOptions = app.Services.GetRequiredService<IOptions<HbtExcelOptions>>();
    HbtExcelHelper.Configure(excelOptions);

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