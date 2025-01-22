//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtValidateUtils.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 14:30
// 版本号 : V0.0.1
// 描述   : 通用验证工具类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Domain.Utils
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
                throw new HbtException($"{fieldName}已存在: {fieldValue}");
        }
    }
}