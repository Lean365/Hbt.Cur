#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuditLogEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 审计日志实体
//===================================================================

using System.Text.RegularExpressions;
using Lean.Hbt.Common.Utils;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Audit
{
    /// <summary>
    /// 审计日志实体
    /// </summary>
    [SugarTable("hbt_mon_audit_log", "审计日志表")]
    [SugarIndex("ix_tenant_audit", nameof(TenantId), OrderByType.Asc)]
    public class HbtAuditLog : HbtBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public HbtLogLevel LogLevel { get; set; } = HbtLogLevel.Info;

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 模块
        /// </summary>
        [SugarColumn(ColumnName = "module", ColumnDescription = "模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string Module { get; set; } = string.Empty;

        /// <summary>
        /// 操作
        /// </summary>
        [SugarColumn(ColumnName = "operation", ColumnDescription = "操作", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string Operation { get; set; } = string.Empty;

        /// <summary>
        /// 方法
        /// </summary>
        [SugarColumn(ColumnName = "method", ColumnDescription = "方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        [SugarColumn(ColumnName = "parameters", ColumnDescription = "参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Parameters { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "结果", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        [SugarColumn(ColumnName = "elapsed", ColumnDescription = "耗时(毫秒)", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
        public long Elapsed { get; set; } = 0;

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置请求URL
        /// </summary>
        [SugarColumn(ColumnName = "request_url", Length = 255, IsNullable = false, DefaultValue = "")]
        public string RequestUrl { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置请求方法
        /// </summary>
        [SugarColumn(ColumnName = "request_method", Length = 10, IsNullable = false, DefaultValue = "")]
        public string RequestMethod { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置请求参数
        /// </summary>
        [SugarColumn(ColumnName = "request_params", ColumnDataType = "text", IsNullable = true)]
        public string? RequestParams { get; set; }

        /// <summary>
        /// 获取或设置响应结果
        /// </summary>
        [SugarColumn(ColumnName = "response_result", ColumnDataType = "text", IsNullable = true)]
        public string? ResponseResult { get; set; }

        /// <summary>
        /// 获取或设置执行时间(毫秒)
        /// </summary>
        [SugarColumn(ColumnName = "execution_time", IsNullable = false, DefaultValue = "0")]
        public long ExecutionTime { get; set; } = 0;

        /// <summary>
        /// 脱敏请求参数
        /// </summary>
        public string GetMaskedRequestParams()
        {
            if (string.IsNullOrEmpty(RequestParams)) return string.Empty;

            var maskedParams = RequestParams;

            // 密码脱敏
            maskedParams = Regex.Replace(maskedParams, @"""password""\s*:\s*""[^""]*""", @"""password"":""********""");

            // 手机号脱敏
            maskedParams = Regex.Replace(maskedParams, @"""phoneNumber""\s*:\s*""([^""]*)""", m =>
                $@"""phoneNumber"":""{HbtMaskUtils.MaskPhoneNumber(m.Groups[1].Value)}""");

            // 邮箱脱敏
            maskedParams = Regex.Replace(maskedParams, @"""email""\s*:\s*""([^""]*)""", m =>
                $@"""email"":""{HbtMaskUtils.MaskEmail(m.Groups[1].Value)}""");

            // 身份证号脱敏
            maskedParams = Regex.Replace(maskedParams, @"""idCard""\s*:\s*""([^""]*)""", m =>
                $@"""idCard"":""{HbtMaskUtils.MaskIdCard(m.Groups[1].Value)}""");

            // 银行卡号脱敏
            maskedParams = Regex.Replace(maskedParams, @"""bankCard""\s*:\s*""([^""]*)""", m =>
                $@"""bankCard"":""{HbtMaskUtils.MaskBankCard(m.Groups[1].Value)}""");

            // 地址脱敏
            maskedParams = Regex.Replace(maskedParams, @"""address""\s*:\s*""([^""]*)""", m =>
                $@"""address"":""{HbtMaskUtils.MaskAddress(m.Groups[1].Value)}""");

            return maskedParams;
        }

        /// <summary>
        /// 脱敏响应结果
        /// </summary>
        public string GetMaskedResponseResult()
        {
            if (string.IsNullOrEmpty(ResponseResult)) return string.Empty;

            var maskedResult = ResponseResult;

            // 密码脱敏
            maskedResult = Regex.Replace(maskedResult, @"""password""\s*:\s*""[^""]*""", @"""password"":""********""");

            // 手机号脱敏
            maskedResult = Regex.Replace(maskedResult, @"""phoneNumber""\s*:\s*""([^""]*)""", m =>
                $@"""phoneNumber"":""{HbtMaskUtils.MaskPhoneNumber(m.Groups[1].Value)}""");

            // 邮箱脱敏
            maskedResult = Regex.Replace(maskedResult, @"""email""\s*:\s*""([^""]*)""", m =>
                $@"""email"":""{HbtMaskUtils.MaskEmail(m.Groups[1].Value)}""");

            // 身份证号脱敏
            maskedResult = Regex.Replace(maskedResult, @"""idCard""\s*:\s*""([^""]*)""", m =>
                $@"""idCard"":""{HbtMaskUtils.MaskIdCard(m.Groups[1].Value)}""");

            // 银行卡号脱敏
            maskedResult = Regex.Replace(maskedResult, @"""bankCard""\s*:\s*""([^""]*)""", m =>
                $@"""bankCard"":""{HbtMaskUtils.MaskBankCard(m.Groups[1].Value)}""");

            // 地址脱敏
            maskedResult = Regex.Replace(maskedResult, @"""address""\s*:\s*""([^""]*)""", m =>
                $@"""address"":""{HbtMaskUtils.MaskAddress(m.Groups[1].Value)}""");

            return maskedResult;
        }
    }
}