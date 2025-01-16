using NLog;
using NLog.Web;
using Lean.Hbt.Infrastructure.Extensions;
using Lean.Hbt.Infrastructure.Swagger;
using Lean.Hbt.Infrastructure.Persistence;
using Lean.Hbt.WebApi.Middlewares;

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

    // 添加基础设施服务
    builder.Services.AddHbtInfrastructure(builder.Configuration);

    var app = builder.Build();

    // 初始化种子数据
    using (var scope = app.Services.CreateScope())
    {
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
