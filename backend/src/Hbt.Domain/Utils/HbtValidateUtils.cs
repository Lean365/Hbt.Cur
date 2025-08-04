//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtValidateUtils.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 14:30
// 版本号 : V0.0.1
// 描述   : 通用验证工具类
//===================================================================

using System.Linq.Expressions;
using Hbt.Common.Exceptions;
using Hbt.Domain.Repositories;
using Hbt.Domain.IServices.Extensions;
using SqlSugar;

namespace Hbt.Domain.Utils
{
    /// <summary>
    /// 通用验证工具类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public static class HbtValidateUtils
    {
        /// <summary>
        /// 验证字段是否已存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="repository">仓储接口</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>验证任务</returns>
        public static async Task ValidateFieldExistsAsync<T>(IHbtRepository<T> repository, string fieldName, string fieldValue, long? excludeId = null) where T : class, new()
        {
            if (string.IsNullOrEmpty(fieldValue))
                return;

            var exp = Expressionable.Create<T>();
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(fieldValue);
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
            exp.And(lambda);

            if (excludeId.HasValue)
            {
                var idProperty = Expression.Property(parameter, "Id");
                var idConstant = Expression.Constant(excludeId.Value);
                var notEquals = Expression.NotEqual(idProperty, idConstant);
                var idLambda = Expression.Lambda<Func<T, bool>>(notEquals, parameter);
                exp.And(idLambda);
            }

            if (await repository.AsQueryable().AnyAsync(exp.ToExpression()))
                throw HbtException.ValidationError($"Common.FieldExists:{fieldName}={fieldValue}");
        }

        /// <summary>
        /// 验证多个字段组合是否已存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="repository">仓储接口</param>
        /// <param name="fieldValues">字段名和值的字典</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>验证任务</returns>
        public static async Task ValidateFieldsExistsAsync<T>(IHbtRepository<T> repository, Dictionary<string, string> fieldValues, long? excludeId = null) where T : class, new()
        {
            if (fieldValues == null || !fieldValues.Any())
                return;

            var exp = Expressionable.Create<T>();
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var field in fieldValues)
            {
                if (string.IsNullOrEmpty(field.Value))
                    continue;

                var property = Expression.Property(parameter, field.Key);
                var propertyType = property.Type;
                
                // 根据属性类型转换值
                object convertedValue;
                if (propertyType == typeof(long))
                {
                    convertedValue = long.Parse(field.Value);
                }
                else if (propertyType == typeof(int))
                {
                    convertedValue = int.Parse(field.Value);
                }
                else if (propertyType == typeof(decimal))
                {
                    convertedValue = decimal.Parse(field.Value);
                }
                else if (propertyType == typeof(double))
                {
                    convertedValue = double.Parse(field.Value);
                }
                else if (propertyType == typeof(float))
                {
                    convertedValue = float.Parse(field.Value);
                }
                else if (propertyType == typeof(bool))
                {
                    convertedValue = bool.Parse(field.Value);
                }
                else if (propertyType == typeof(DateTime))
                {
                    convertedValue = DateTime.Parse(field.Value);
                }
                else
                {
                    convertedValue = field.Value;
                }

                var constant = Expression.Constant(convertedValue, propertyType);
                var equals = Expression.Equal(property, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
                exp.And(lambda);
            }

            if (excludeId.HasValue)
            {
                var idProperty = Expression.Property(parameter, "Id");
                var idConstant = Expression.Constant(excludeId.Value);
                var notEquals = Expression.NotEqual(idProperty, idConstant);
                var idLambda = Expression.Lambda<Func<T, bool>>(notEquals, parameter);
                exp.And(idLambda);
            }

            if (await repository.AsQueryable().AnyAsync(exp.ToExpression()))
            {
                var fieldNames = string.Join(",", fieldValues.Keys);
                var fieldValuesStr = string.Join(",", fieldValues.Values);
                throw HbtException.ValidationError($"Common.FieldExists:{fieldNames}={fieldValuesStr}");
            }
        }
    }
}