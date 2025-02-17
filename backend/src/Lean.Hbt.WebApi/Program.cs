using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Data.Seeds;
using Lean.Hbt.Infrastructure.Extensions;
using Lean.Hbt.Infrastructure.Services;
using Lean.Hbt.WebApi.Extensions;
using Lean.Hbt.WebApi.Middlewares;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

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
    var serverConfig = builder.Configuration.GetSection("Server").Get<HbtServerConfig>();
    
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
                  .WithHeaders("Content-Type", "Authorization", "Accept", "X-Requested-With")
                  .AllowCredentials()
                  .SetIsOriginAllowed(origin => true);
        });
    });

    // 添加控制器服务
    builder.Services.AddControllers();

    // 添加 Swagger 服务
    builder.Services.AddHbtSwagger();

    // 添加基础设施服务
    builder.Services.AddInfrastructure(builder.Configuration);

    // 添加领域服务
    builder.Services.AddDomainServices();

    // 添加应用服务
    builder.Services.AddApplicationServices();

    // 添加本地化服务
    builder.Services.AddHbtLocalization();

    // 添加后台服务
    builder.Services.AddHostedService<HbtLoginPolicyInitializer>();

    var app = builder.Build();

    // 根据配置初始化数据库和种子数据
    if (serverConfig.Init.InitDatabase || serverConfig.Init.InitSeedData)
    {
        using (var scope = app.Services.CreateScope())
        {
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
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment() && serverConfig.Init.EnableSwagger)
    {
        app.UseHbtSwagger();
        logger.Info("Swagger已启用");
    }

    if (serverConfig.Init.EnableCors)
    {
        app.UseCors("HbtPolicy");
        logger.Info("CORS已启用");
    }

    app.UseHbtExceptionHandler();
    app.UseHbtSessionSecurity();
    app.UseHbtSqlInjection();
    app.UseHbtCsrf();

    if (serverConfig.UseHttps)
    {
        app.UseHttpsRedirection();
        logger.Info("已启用HTTPS重定向");
    }

    app.UseHbtRateLimit();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHbtTenant();
    app.UseHbtPerm();
    app.UseHbtLocalization();

    app.MapControllers();

    // 配置Excel帮助类
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