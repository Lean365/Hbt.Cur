#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : WorkflowExpressionEngine.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表达式引擎实现
//===================================================================

using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Lean.Hbt.Application.Services.Workflow.Engine.Expressions
{
    /// <summary>
    /// 工作流表达式引擎实现
    /// </summary>
    public class WorkflowExpressionEngine : IWorkflowExpressionEngine
    {
        private readonly IHbtLogger _logger;
        private readonly Dictionary<string, Func<object[], Task<object>>> _customFunctions;
        private readonly ScriptOptions _scriptOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowExpressionEngine(IHbtLogger logger)
        {
            _logger = logger;
            _customFunctions = new Dictionary<string, Func<object[], Task<object>>>();
            _scriptOptions = ScriptOptions.Default
                .AddImports("System", "System.Linq", "System.Collections.Generic")
                .AddReferences(typeof(System.Linq.Enumerable).Assembly);

            // 注册内置函数
            RegisterBuiltInFunctions();
        }

        /// <summary>
        /// 注册自定义函数
        /// </summary>
        public void RegisterFunction(string name, Func<object[], Task<object>> function)
        {
            _customFunctions[name] = function;
            _logger.Info($"已注册自定义函数: {name}");
        }

        /// <summary>
        /// 解析并执行表达式
        /// </summary>
        public async Task<bool> EvaluateAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                _logger.Info($"开始执行表达式: {expression}");

                // 替换自定义函数调用
                expression = await ReplaceCustomFunctionCallsAsync(expression);

                // 创建全局变量
                var globals = new Dictionary<string, object>();
                if (variables != null)
                {
                    foreach (var kvp in variables)
                    {
                        globals[kvp.Key] = kvp.Value;
                    }
                }

                // 执行表达式
                var result = await CSharpScript.EvaluateAsync<bool>(
                    expression,
                    _scriptOptions,
                    globals);

                _logger.Info($"表达式执行完成: {expression} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"表达式执行失败: {expression}", ex);
                throw new WorkflowExpressionException($"表达式执行失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证表达式语法
        /// </summary>
        public bool Validate(string expression)
        {
            try
            {
                _logger.Info($"开始验证表达式: {expression}");

                // 编译表达式（不执行）
                var script = CSharpScript.Create<bool>(
                    expression,
                    _scriptOptions);

                script.Compile();

                _logger.Info($"表达式验证通过: {expression}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"表达式验证失败: {expression}", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取表达式中使用的变量名列表
        /// </summary>
        public IEnumerable<string> GetVariableNames(string expression)
        {
            var variablePattern = @"\b[a-zA-Z_][a-zA-Z0-9_]*\b";
            var matches = Regex.Matches(expression, variablePattern);

            // 排除C#关键字和自定义函数名
            var keywords = new HashSet<string> { "true", "false", "null", "var", "new" };
            return matches
                .Select(m => m.Value)
                .Where(v => !keywords.Contains(v) && !_customFunctions.ContainsKey(v))
                .Distinct();
        }

        private void RegisterBuiltInFunctions()
        {
            // 数学函数
            RegisterFunction("Sum", async args =>
                args.Select(a => Convert.ToDouble(a)).Sum());

            RegisterFunction("Average", async args =>
                args.Select(a => Convert.ToDouble(a)).Average());

            // 字符串函数
            RegisterFunction("Concat", async args =>
                string.Concat(args.Select(a => a?.ToString())));

            RegisterFunction("Contains", async args =>
                args[0]?.ToString()?.Contains(args[1]?.ToString() ?? "") ?? false);

            // 日期函数
            RegisterFunction("DaysBetween", async args =>
            {
                if (args.Length != 2) return 0;
                var date1 = Convert.ToDateTime(args[0]);
                var date2 = Convert.ToDateTime(args[1]);
                return (date2 - date1).TotalDays;
            });

            // 集合函数
            RegisterFunction("Count", async args =>
            {
                if (args[0] is IEnumerable<object> list)
                    return list.Count();
                return 0;
            });

            // 类型转换函数
            RegisterFunction("ToNumber", async args =>
                Convert.ToDouble(args[0]));

            RegisterFunction("ToString", async args =>
                args[0]?.ToString() ?? "");

            RegisterFunction("ToDate", async args =>
                Convert.ToDateTime(args[0]));

            // 条件函数
            RegisterFunction("If", async args =>
                Convert.ToBoolean(args[0]) ? args[1] : args[2]);

            RegisterFunction("IsNull", async args =>
                args[0] == null);

            RegisterFunction("IsEmpty", async args =>
                string.IsNullOrEmpty(args[0]?.ToString()));
        }

        private async Task<string> ReplaceCustomFunctionCallsAsync(string expression)
        {
            foreach (var func in _customFunctions)
            {
                var pattern = $@"\b{func.Key}\s*\((.*?)\)";
                expression = await ReplaceAsync(expression, pattern, async match =>
                {
                    var argsString = match.Groups[1].Value;
                    var args = ParseArguments(argsString);
                    var result = await func.Value(args.ToArray());
                    return result?.ToString() ?? "null";
                });
            }
            return expression;
        }

        private object[] ParseArguments(string argsString)
        {
            if (string.IsNullOrWhiteSpace(argsString))
                return Array.Empty<object>();

            var args = new List<object>();
            var currentArg = "";
            var inString = false;
            var bracketCount = 0;

            foreach (var c in argsString)
            {
                if (c == '"')
                {
                    inString = !inString;
                    currentArg += c;
                }
                else if (c == '(')
                {
                    bracketCount++;
                    currentArg += c;
                }
                else if (c == ')')
                {
                    bracketCount--;
                    currentArg += c;
                }
                else if (c == ',' && !inString && bracketCount == 0)
                {
                    args.Add(currentArg.Trim());
                    currentArg = "";
                }
                else
                {
                    currentArg += c;
                }
            }

            if (!string.IsNullOrEmpty(currentArg))
                args.Add(currentArg.Trim());

            return args.ToArray();
        }

        private async Task<string> ReplaceAsync(string input, string pattern, Func<Match, Task<string>> replacementFunc)
        {
            var regex = new Regex(pattern);
            var matches = regex.Matches(input);
            var result = input;

            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var replacement = await replacementFunc(match);
                result = result.Substring(0, match.Index) + replacement + result.Substring(match.Index + match.Length);
            }

            return result;
        }
    }

    /// <summary>
    /// 工作流表达式异常
    /// </summary>
    public class WorkflowExpressionException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public WorkflowExpressionException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WorkflowExpressionException(string message, Exception innerException) : base(message, innerException) { }
    }
}