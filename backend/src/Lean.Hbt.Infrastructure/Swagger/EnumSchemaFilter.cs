using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lean.Hbt.Infrastructure.Swagger
{
    /// <summary>
    /// 枚举架构过滤器
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        private readonly ILogger<EnumSchemaFilter> _logger;

        public EnumSchemaFilter(ILogger<EnumSchemaFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 应用过滤器
        /// </summary>
        /// <param name="schema">OpenAPI架构</param>
        /// <param name="context">架构过滤器上下文</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            try
            {
                if (context.Type.IsEnum)
                {
                    schema.Enum.Clear();
                    schema.Type = "string";
                    schema.Format = null;

                    var enumValues = Enum.GetValues(context.Type);
                    foreach (var value in enumValues)
                    {
                        var name = Enum.GetName(context.Type, value);
                        var desc = GetEnumDescription(context.Type, name);
                        schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString($"{value} = {desc}"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理枚举架构过滤器时发生错误");
            }
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetEnumDescription(Type enumType, string name)
        {
            var field = enumType.GetField(name);
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return attribute?.Description ?? name;
        }
    }
}