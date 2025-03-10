//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : SwaggerOperationFilter.cs
// 创建者 : Lean365
// 创建时间: 2024-01-24 14:30
// 版本号 : V1.0.0
// 描述    : Swagger操作过滤器
//===================================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Infrastructure.Swagger
{
    /// <summary>
    /// Swagger操作过滤器
    /// </summary>
    public class SwaggerOperationFilter : IOperationFilter
    {
        private readonly ILogger<SwaggerOperationFilter> _logger;
        private static readonly HashSet<Type> _processedTypes = new HashSet<Type>();

        public SwaggerOperationFilter(ILogger<SwaggerOperationFilter> logger)
        {
            _logger = logger;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            try
            {
                var fileParameters = context.ApiDescription.ParameterDescriptions
                    .Where(p => p.Type == typeof(IFormFile))
                    .ToList();

                if (fileParameters.Any())
                {
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["multipart/form-data"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = fileParameters.ToDictionary(
                                        p => p.Name,
                                        _ => new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    )
                                }
                            }
                        }
                    };

                    // 移除已处理的文件参数
                    foreach (var fileParam in fileParameters)
                    {
                        var param = operation.Parameters.FirstOrDefault(p => p.Name == fileParam.Name);
                        if (param != null)
                        {
                            operation.Parameters.Remove(param);
                        }
                    }
                }

                // 处理查询参数
                var queryParameters = context.ApiDescription.ParameterDescriptions
                    .Where(p => p.Source?.Id == "Query")
                    .ToList();

                foreach (var param in queryParameters)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = param.Name,
                        In = ParameterLocation.Query,
                        Schema = context.SchemaGenerator.GenerateSchema(param.Type, context.SchemaRepository),
                        Required = param.IsRequired,
                        Description = GetParameterDescription(param)
                    });
                }

                // 处理认证
                var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes<AuthorizeAttribute>(true)
                    .Union(context.MethodInfo.GetCustomAttributes<AuthorizeAttribute>(true));

                if (authAttributes.Any())
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "未经授权" });
                    operation.Responses.Add("403", new OpenApiResponse { Description = "访问被拒绝" });
                }

                // 添加操作属性
                HandleOperationAttributes(operation, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理 Swagger 操作过滤器时发生错误");
            }
        }

        private void HandleOperationAttributes(OpenApiOperation operation, OperationFilterContext context)
        {
            var attribute = context.MethodInfo.GetCustomAttribute<SwaggerOperationAttribute>();
            if (attribute != null)
            {
                if (!string.IsNullOrEmpty(attribute.Summary))
                    operation.Summary = attribute.Summary;
                
                if (!string.IsNullOrEmpty(attribute.Description))
                    operation.Description = attribute.Description;
            }
        }

        private string GetParameterDescription(ApiParameterDescription parameter)
        {
            if (parameter.ModelMetadata?.Description != null)
            {
                return parameter.ModelMetadata.Description;
            }
            return string.Empty;
        }

        private string CreateExample(Type type)
        {
            if (type == null) return "null";
            
            if (!_processedTypes.Add(type))
            {
                return "{}";
            }

            try
            {
                string result;
                if (type == typeof(string)) result = "string";
                else if (type == typeof(int) || type == typeof(long)) result = "0";
                else if (type == typeof(double) || type == typeof(decimal)) result = "0.0";
                else if (type == typeof(bool)) result = "false";
                else if (type == typeof(DateTime)) result = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                else if (type == typeof(Guid)) result = "00000000-0000-0000-0000-000000000000";
                else if (type.IsGenericType)
                {
                    var genericType = type.GetGenericTypeDefinition();
                    if (genericType == typeof(Nullable<>))
                        result = CreateExample(type.GetGenericArguments()[0]);
                    else if (genericType == typeof(List<>))
                        result = $"[{CreateExample(type.GetGenericArguments()[0])}]";
                    else
                        result = "{}";
                }
                else if (type.IsClass && type != typeof(object))
                {
                    var example = new Dictionary<string, string>();
                    foreach (var prop in type.GetProperties())
                    {
                        if (prop.CanRead && prop.GetMethod?.IsPublic == true)
                        {
                            example[prop.Name] = CreateExample(prop.PropertyType);
                        }
                    }
                    result = System.Text.Json.JsonSerializer.Serialize(example);
                }
                else
                {
                    result = "null";
                }

                return result;
            }
            finally
            {
                _processedTypes.Remove(type);
            }
        }
    }
} 