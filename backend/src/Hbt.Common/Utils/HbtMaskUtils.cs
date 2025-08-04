using System.Text.RegularExpressions;

namespace Hbt.Common.Utils
{
    /// <summary>
    /// 数据脱敏工具类
    /// </summary>
    public static class HbtMaskUtils
    {
        /// <summary>
        /// 手机号码脱敏
        /// </summary>
        public static string MaskPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;
            return Regex.Replace(phoneNumber, @"(\d{3})\d{4}(\d{4})", "$1****$2");
        }

        /// <summary>
        /// 邮箱地址脱敏
        /// </summary>
        public static string MaskEmail(string? email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            return Regex.Replace(email, @"(.{2}).*(@.*)", "$1***$2");
        }

        /// <summary>
        /// 身份证号脱敏
        /// </summary>
        public static string MaskIdCard(string? idCard)
        {
            if (string.IsNullOrEmpty(idCard)) return string.Empty;
            return Regex.Replace(idCard, @"(\d{4}).*(\d{4})", "$1********$2");
        }

        /// <summary>
        /// 银行卡号脱敏
        /// </summary>
        public static string MaskBankCard(string? bankCard)
        {
            if (string.IsNullOrEmpty(bankCard)) return string.Empty;
            return Regex.Replace(bankCard, @"(\d{4}).*(\d{4})", "$1********$2");
        }

        /// <summary>
        /// 密码脱敏
        /// </summary>
        public static string MaskPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            return "********";
        }

        /// <summary>
        /// 地址脱敏
        /// </summary>
        public static string MaskAddress(string? address)
        {
            if (string.IsNullOrEmpty(address)) return string.Empty;
            if (address.Length <= 6) return "***";
            return address.Substring(0, 6) + "****";
        }

        /// <summary>
        /// 数据库连接字符串脱敏
        /// </summary>
        public static string MaskConnectionString(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return string.Empty;
            return Regex.Replace(
                connectionString,
                @"(Password|pwd)=[^;]*",
                "$1=********",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 自定义脱敏
        /// </summary>
        public static string MaskCustom(string? value, int prefixLength = 1, int suffixLength = 1)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length <= prefixLength + suffixLength) return value;
            
            var prefix = value.Substring(0, prefixLength);
            var suffix = value.Substring(value.Length - suffixLength);
            var maskLength = value.Length - prefixLength - suffixLength;
            return prefix + new string('*', maskLength) + suffix;
        }
    }
} 