//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ApiModuleAttribute.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述   : API模块特性
//===================================================================

using System;

namespace Lean.Hbt.Infrastructure.Swagger
{
    /// <summary>
    /// API模块特性
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="description">模块描述</param>
        public ApiModuleAttribute(string name, string description = null)
        {
            Name = name;
            Description = description;
        }
    }
} 