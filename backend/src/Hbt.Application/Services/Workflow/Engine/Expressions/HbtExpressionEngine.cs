#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExpressionEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流表达式引擎实现
//===================================================================

using Hbt.Domain.IServices;
using System.Text.RegularExpressions;

namespace Hbt.Application.Services.Workflow.Engine.Expressions
{
    /// <summary>
    /// 工作流表达式引擎实现
    /// </summary>
    public class HbtExpressionEngine : IHbtExpressionEngine
    {
        private readonly IHbtLogger _logger;

        public HbtExpressionEngine(IHbtLogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> EvaluateAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrEmpty(expression))
                    return true;

                // 替换变量
                var processedExpression = ReplaceVariables(expression, variables);

                // 简化实现：支持基本的比较表达式
                return EvaluateSimpleExpression(processedExpression);
            }
            catch (Exception ex)
            {
                _logger.Error($"表达式执行失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <inheritdoc/>
        public bool Validate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return true;

            try
            {
                // 基本语法验证
                var operators = new[] { "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!", "(", ")" };
                var validChars = new[] { ' ', '(', ')', '&', '|', '!', '=', '>', '<', '.', '_', '-' };

                // 检查是否包含有效的操作符
                bool hasValidOperator = operators.Any(op => expression.Contains(op));
                if (!hasValidOperator)
                    return false;

                // 检查括号匹配
                int openBrackets = expression.Count(c => c == '(');
                int closeBrackets = expression.Count(c => c == ')');
                if (openBrackets != closeBrackets)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetVariableNames(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return Enumerable.Empty<string>();

            try
            {
                // 使用正则表达式提取变量名
                var pattern = @"\$\{([^}]+)\}";
                var matches = Regex.Matches(expression, pattern);
                return matches.Select(m => m.Groups[1].Value).Distinct();
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        /// <inheritdoc/>
        public async Task<object> EvaluateMathAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrEmpty(expression))
                    return 0;

                // 替换变量
                var processedExpression = ReplaceVariables(expression, variables);

                // 简化实现：使用DataTable.Compute方法
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(processedExpression, "");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"数学表达式执行失败: {ex.Message}", ex);
                return 0;
            }
        }

        /// <inheritdoc/>
        public async Task<string> EvaluateStringAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrEmpty(expression))
                    return string.Empty;

                // 替换变量
                var processedExpression = ReplaceVariables(expression, variables);

                // 支持字符串拼接
                return ProcessStringExpression(processedExpression);
            }
            catch (Exception ex)
            {
                _logger.Error($"字符串表达式执行失败: {ex.Message}", ex);
                return string.Empty;
            }
        }

        #region 私有方法

        /// <summary>
        /// 替换表达式中的变量
        /// </summary>
        private string ReplaceVariables(string expression, Dictionary<string, object>? variables)
        {
            if (variables == null || !variables.Any())
                return expression;

            var result = expression;
            foreach (var variable in variables)
            {
                var placeholder = $"${{{variable.Key}}}";
                var value = variable.Value?.ToString() ?? "";
                result = result.Replace(placeholder, value);
            }

            return result;
        }

        /// <summary>
        /// 执行简单表达式
        /// </summary>
        private bool EvaluateSimpleExpression(string expression)
        {
            try
            {
                // 简化实现：支持基本的比较表达式
                if (expression.Contains("=="))
                {
                    var parts = expression.Split("==", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                        return parts[0].Trim() == parts[1].Trim();
                }
                else if (expression.Contains("!="))
                {
                    var parts = expression.Split("!=", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                        return parts[0].Trim() != parts[1].Trim();
                }
                else if (expression.Contains(">"))
                {
                    var parts = expression.Split(">", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && double.TryParse(parts[0].Trim(), out double left) && double.TryParse(parts[1].Trim(), out double right))
                        return left > right;
                }
                else if (expression.Contains("<"))
                {
                    var parts = expression.Split("<", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && double.TryParse(parts[0].Trim(), out double left) && double.TryParse(parts[1].Trim(), out double right))
                        return left < right;
                }
                else if (expression.Contains(">="))
                {
                    var parts = expression.Split(">=", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && double.TryParse(parts[0].Trim(), out double left) && double.TryParse(parts[1].Trim(), out double right))
                        return left >= right;
                }
                else if (expression.Contains("<="))
                {
                    var parts = expression.Split("<=", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && double.TryParse(parts[0].Trim(), out double left) && double.TryParse(parts[1].Trim(), out double right))
                        return left <= right;
                }

                // 如果没有操作符，直接返回true
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 处理字符串表达式
        /// </summary>
        private string ProcessStringExpression(string expression)
        {
            try
            {
                // 支持字符串拼接
                if (expression.Contains("+"))
                {
                    var parts = expression.Split('+');
                    return string.Join("", parts.Select(p => p.Trim()));
                }

                return expression;
            }
            catch
            {
                return expression;
            }
        }

        #endregion
    }
}