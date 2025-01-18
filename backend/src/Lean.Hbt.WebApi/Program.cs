using NLog;
using NLog.Web;
using Lean.Hbt.Infrastructure.Extensions;
using Lean.Hbt.Infrastructure.Swagger;
using Lean.Hbt.Infrastructure.Persistence;
using Lean.Hbt.WebApi.Middlewares;
using Microsoft.Extensions.Options;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Common.Helpers;

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

    // 添加控制器服务
    builder.Services.AddControllers();

    // 添加 Swagger 服务
    builder.Services.AddHbtSwagger();

    // 添加基础设施服务
    builder.Services.AddInfrastructure(builder.Configuration);

    // 添加认证和授权服务
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    // 添加配置
    builder.Services.Configure<HbtExcelOptions>(builder.Configuration.GetSection("Excel"));

    var app = builder.Build();

    // 初始化数据库和种子数据
    using (var scope = app.Services.CreateScope())
    {
        // 1. 初始化数据库和表结构
        var dbContext = scope.ServiceProvider.GetRequiredService<HbtDbContext>();
        await dbContext.InitializeAsync();

        // 2. 初始化种子数据
        var dbSeed = scope.ServiceProvider.GetRequiredService<HbtDbSeed>();
        await dbSeed.InitializeAsync();
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment())
    {
        app.UseHbtSwagger();
    }

    // 使用全局异常处理中间件
    app.UseHbtExceptionHandler();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // 配置Excel帮助类
    var excelOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<HbtExcelOptions>>();
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
