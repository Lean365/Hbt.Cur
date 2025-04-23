//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExceptionMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 全局异常处理中间件
//===================================================================

using Newtonsoft.Json;

namespace Lean.Hbt.WebApi.Middlewares
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public class HbtExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        /// <param name="logger">日志记录器</param>
        public HbtExceptionMiddleware(RequestDelegate next,
            IHbtLogger logger
                )
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>异步任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = exception switch
            {
                HbtException hbtEx => new HbtApiResult
                {
                    Code = hbtEx.Code,
                    Msg = hbtEx.Message,
                    Data = null
                },
                _ => new HbtApiResult
                {
                    Code = 500,
                    Msg = "服务器内部错误",
                    Data = null
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (response.Code == 500)
            {
                _logger.Error("未处理的异常");
            }
            else
            {
                _logger.Warn("业务异常: {Message}", response.Msg);
            }

            var settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, settings));
        }
    }

    /// <summary>
    /// 中间件扩展方法
    /// </summary>
    public static class HbtExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 使用全局异常处理中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <returns>应用程序构建器</returns>
        public static IApplicationBuilder UseHbtExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HbtExceptionMiddleware>();
        }
    }
}