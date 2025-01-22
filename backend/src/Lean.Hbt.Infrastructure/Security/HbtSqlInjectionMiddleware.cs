//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSqlInjectionMiddleware.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
// 描述    : SQL注入防护中间件
//===================================================================

using System.Text.RegularExpressions;
using Lean.Hbt.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Security
{
    /// <summary>
    /// SQL注入防护中间件
    /// </summary>
    public class HbtSqlInjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HbtSecurityOptions _options;

        private static readonly Regex SqlInjectionPattern = new(
            @"(\b(select|insert|update|delete|drop|truncate|exec|declare|union|create|alter)\b)|([""'])",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public HbtSqlInjectionMiddleware(
            RequestDelegate next,
            IOptions<HbtSecurityOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// 调用中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // 1. 检查请求方法
            if (context.Request.Method != "GET")
            {
                // 2. 读取请求体
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // 3. 检查请求参数
                if (ContainsSqlInjection(body) ||
                    ContainsSqlInjection(context.Request.QueryString.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
                    return;
                }
            }
            else
            {
                // 检查查询字符串
                if (ContainsSqlInjection(context.Request.QueryString.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
                    return;
                }
            }

            await _next(context);
        }

        /// <summary>
        /// 检查是否包含SQL注入攻击
        /// </summary>
        private bool ContainsSqlInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // 1. 检查SQL关键字
            if (SqlInjectionPattern.IsMatch(input))
                return true;

            // 2. 检查特殊字符
            if (input.Contains(';') || input.Contains("--") || input.Contains("/*") || input.Contains("*/"))
                return true;

            // 3. 检查Unicode编码
            if (input.Contains("\\u") || input.Contains("%u"))
                return true;

            return false;
        }
    }
}