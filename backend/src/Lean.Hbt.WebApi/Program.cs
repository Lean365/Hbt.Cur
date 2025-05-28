using System.Text;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Caching;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.IServices.SignalR;
using Lean.Hbt.Infrastructure.Caching;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Data.Seeds;
using Lean.Hbt.Infrastructure.Extensions;
using Lean.Hbt.Infrastructure.Jobs;
using Lean.Hbt.Infrastructure.Security;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.Infrastructure.Services.Identity;
using Lean.Hbt.Infrastructure.SignalR.Cache;
using Lean.Hbt.WebApi.Middlewares;
using Lean.Hbt.WebApi.Extensions;
using Lean.Hbt.Application.Services.Audit;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using Quartz;
using SqlSugar;


var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup()
                      .LoadConfigurationFromFile("nlog.config")
                      .GetCurrentClassLogger();
try
{
    logger.Info("正在初始化应用程序...");

    // 添加NLog支持
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // 注册NLog.ILogger
    builder.Services.AddSingleton<NLog.ILogger>(sp => LogManager.GetCurrentClassLogger());

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
        options.MaxRequestBodySize = null; // 移除请求体大小限制
    });

    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
        options.Limits.MaxRequestHeadersTotalSize = 32768; // 32KB
        options.Limits.MaxRequestLineSize = 8192; // 8KB
    });

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
        options.Limits.MaxRequestHeadersTotalSize = 32768; // 32KB
        options.Limits.MaxRequestLineSize = 8192; // 8KB
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
    var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
    var corsMethods = builder.Configuration.GetSection("Cors:Methods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" };
    var corsHeaders = builder.Configuration.GetSection("Cors:Headers").Get<string[]>() ?? new[] { "*" };
    var allowCredentials = builder.Configuration.GetSection("Cors:AllowCredentials").Get<bool>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("HbtPolicy", builder =>
        {
            builder
                .WithOrigins(corsOrigins)
                .WithMethods(corsMethods)
                .WithHeaders(corsHeaders)
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition", "X-Device-Id", "X-Device-Name", "X-Device-Type", "X-Device-Model",
                    "X-OS-Type", "X-OS-Version", "X-Browser-Type", "X-Browser-Version",
                    "X-Resolution", "X-Location", "X-Device-Token");
        });
    });

    // 添加控制器服务
    builder.Services.AddControllers(options =>
    {
        options.EnableEndpointRouting = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // 配置文件上传限制
    builder.Services.Configure<FormOptions>(options =>
    {
        var fileUploadConfig = builder.Configuration.GetSection("FileUpload");
        options.MultipartBodyLengthLimit = fileUploadConfig.GetValue<int>("MultipartBodyLengthLimit", 268435456); // 256MB
        options.MultipartHeadersLengthLimit = fileUploadConfig.GetValue<int>("MultipartHeadersLengthLimit", 32768); // 32KB
        options.MultipartBoundaryLengthLimit = fileUploadConfig.GetValue<int>("MultipartBoundaryLengthLimit", 128); // 128 bytes
        options.ValueLengthLimit = fileUploadConfig.GetValue<int>("ValueLengthLimit", 10485760); // 10MB
        options.KeyLengthLimit = fileUploadConfig.GetValue<int>("KeyLengthLimit", 1024); // 1KB
        options.BufferBody = true;
        options.MemoryBufferThreshold = 1048576; // 1MB
    });

    // 添加 Antiforgery 服务
    builder.Services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-CSRF-Token";
        options.Cookie.Name = "XSRF-TOKEN";
        options.Cookie.HttpOnly = false;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

    // 添加 HttpClient 服务
    builder.Services.AddHttpClient();

    // 添加 Swagger 服务
    builder.Services.AddHbtSwagger();

    // 配置安全选项
    builder.Services.Configure<HbtSecurityOptions>(builder.Configuration.GetSection("Security"));

    // 配置密码策略选项
    builder.Services.Configure<HbtPasswordPolicyOptions>(builder.Configuration.GetSection("Security:PasswordPolicy"));

    // 配置验证码选项
    builder.Services.Configure<HbtCaptchaOptions>(builder.Configuration.GetSection("Captcha"));

    // 注册当前用户服务
    builder.Services.AddScoped<IHbtCurrentUser, HbtCurrentUser>();

    // 注册当前租户服务
    builder.Services.AddScoped<IHbtCurrentTenant, HbtCurrentTenant>();

    // 添加基础设施服务
    builder.Services.AddInfrastructure(builder.Configuration);

    // 添加领域服务
    builder.Services.AddDomainServices();

    // 添加应用服务
    builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                NameClaimType = "unm",
                RoleClaimType = "rol",
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ClockSkew = TimeSpan.Zero,
                ValidateTokenReplay = true,
                RequireAudience = true,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };

            // 添加自定义的声明类型映射
            options.MapInboundClaims = false;

            // 添加 JWT 事件处理
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<IHbtLogger>();
                    var claims = context.Principal?.Claims.ToList();
                    logger.Info("JWT认证成功，所有声明: {@Claims}", claims?.Select(c => new { c.Type, c.Value }));
                    
                    var tenantId = context.Principal?.FindFirst("tid")?.Value;
                    var userName = context.Principal?.FindFirst("unm")?.Value;
                    
                    if (string.IsNullOrEmpty(tenantId))
                    {
                        logger.Warn("JWT认证失败: 未找到租户ID声明");
                        context.Fail("未找到租户ID声明");
                        return Task.CompletedTask;
                    }

                    logger.Info("JWT认证成功: UserId={UserId}, UserName={UserName}, TenantId={TenantId}",
                        context.Principal?.FindFirst("uid")?.Value,
                        userName,
                        tenantId);
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<IHbtLogger>();
                    logger.Error("JWT认证失败: {Message}, Exception: {Exception}", 
                        context.Exception.Message,
                        context.Exception);
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<IHbtLogger>();
                    logger.Info("收到JWT认证请求: Path={Path}, Token={Token}",
                        context.Request.Path,
                        context.Token?.Substring(0, Math.Min(20, context.Token?.Length ?? 0)));
                    return Task.CompletedTask;
                }
            };
        });

    // 配置SignalR
    builder.Services.AddHbtSignalR(builder.Configuration);

    builder.Services.AddMemoryCache();

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
    builder.Services.AddHbtSeeds();

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
    builder.Services.AddScoped<IHbtRestartService, HbtRestartService>();

    // 注册系统重启配置选项
    builder.Services.Configure<HbtRestartOptions>(
        builder.Configuration.GetSection("SystemRestart"));

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddMvc()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();

    // 注册租户审计日志服务
    builder.Services.AddScoped<IHbtTenantLogService, HbtTenantLogService>();

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
        var systemRestartService = scope.ServiceProvider.GetRequiredService<IHbtRestartService>();
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

    // 5. 开启访问静态文件/wwwroot目录文件
    app.UseStaticFiles();
    logger.Info("静态文件中间件已启用");

    // 6. CSRF防护中间件（防止跨站请求伪造攻击）
    app.UseHbtCsrf();
    logger.Info("CSRF防护已启用");

    // 7. HTTPS重定向中间件（如果启用HTTPS）
    if (serverConfig.UseHttps)
    {
        app.UseHttpsRedirection();
        logger.Info("已启用HTTPS重定向");
    }

    // 8. 速率限制中间件（控制请求频率，防止DoS攻击）
    app.UseHbtRateLimit();

    // 9. 区分大小写路由中间件（确保路由匹配时区分大小写）
    app.UseHbtCaseSensitiveRoute();

    // 10. 添加路由中间件
    app.UseRouting();

    // 11. 添加认证中间件
    app.UseAuthentication();
    app.UseAuthorization();

    // 12. 添加租户中间件
    app.Use((RequestDelegate next) => async (HttpContext context) =>
    {
        using var scope = app.Services.CreateScope();
        var middleware = ActivatorUtilities.CreateInstance<HbtTenantMiddleware>(
            scope.ServiceProvider,
            next);
        await middleware.InvokeAsync(context);
    });

    // 13. 添加权限中间件
    app.UseHbtPerm();

    // 14. 添加本地化中间件
    app.UseHbtLocalization();

    // 15. 添加租户审计日志中间件
    app.Use((RequestDelegate next) => async (HttpContext context) =>
    {
        using var scope = app.Services.CreateScope();
        var middleware = ActivatorUtilities.CreateInstance<HbtTenantLogMiddleware>(
            scope.ServiceProvider,
            next);
        await middleware.InvokeAsync(context);
    });

    // 16. 注册所有控制器路由和SignalR Hub
    app.MapControllers();
    app.UseHbtSignalR();

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