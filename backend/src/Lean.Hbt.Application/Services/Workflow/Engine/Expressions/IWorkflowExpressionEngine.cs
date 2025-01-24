#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IWorkflowExpressionEngine.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表达式引擎接口
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Expressions
{
    /// <summary>
    /// 工作流表达式引擎接口
    /// </summary>
    public interface IWorkflowExpressionEngine
    {
        /// <summary>
        /// 解析并执行表达式
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>表达式执行结果</returns>
        Task<bool> EvaluateAsync(string expression, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 验证表达式语法
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>是否有效</returns>
        bool Validate(string expression);

        /// <summary>
        /// 获取表达式中使用的变量名列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>变量名列表</returns>
        IEnumerable<string> GetVariableNames(string expression);
    }
} 