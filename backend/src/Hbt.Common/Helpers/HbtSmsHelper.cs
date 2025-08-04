//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSmsHelper.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 短信验证码帮助类
//===================================================================

using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace Hbt.Cur.Common.Helpers;

/// <summary>
/// 短信验证码帮助类
/// </summary>
/// <remarks>
/// 提供短信验证码生成、验证、频率限制等功能
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public static class HbtSmsHelper
{
    /// <summary>
    /// 默认验证码长度
    /// </summary>
    private const int DefaultCodeLength = 6;

    /// <summary>
    /// 默认验证码有效期（分钟）
    /// </summary>
    private const int DefaultExpirationMinutes = 5;

    /// <summary>
    /// 默认每小时发送次数限制
    /// </summary>
    private const int DefaultHourlyLimit = 10;

    /// <summary>
    /// 默认每天发送次数限制
    /// </summary>
    private const int DefaultDailyLimit = 50;

    /// <summary>
    /// 手机号正则表达式
    /// </summary>
    private static readonly Regex PhoneRegex = new(@"^1[3-9]\d{9}$", RegexOptions.Compiled);

    /// <summary>
    /// 生成数字验证码
    /// </summary>
    /// <param name="length">验证码长度（默认6位）</param>
    /// <returns>数字验证码</returns>
    public static string GenerateNumericCode(int length = DefaultCodeLength)
    {
        if (length <= 0 || length > 10)
            throw new ArgumentException("验证码长度必须在1-10之间", nameof(length));

        var random = new Random();
        var code = new StringBuilder(length);

        // 确保第一位不为0
        code.Append(random.Next(1, 10));

        // 生成其余位
        for (int i = 1; i < length; i++)
        {
            code.Append(random.Next(0, 10));
        }

        return code.ToString();
    }

    /// <summary>
    /// 生成字母数字混合验证码
    /// </summary>
    /// <param name="length">验证码长度（默认6位）</param>
    /// <param name="includeUppercase">是否包含大写字母</param>
    /// <param name="includeLowercase">是否包含小写字母</param>
    /// <param name="includeNumbers">是否包含数字</param>
    /// <returns>混合验证码</returns>
    public static string GenerateAlphanumericCode(int length = DefaultCodeLength, 
        bool includeUppercase = true, bool includeLowercase = false, bool includeNumbers = true)
    {
        if (length <= 0 || length > 20)
            throw new ArgumentException("验证码长度必须在1-20之间", nameof(length));

        if (!includeUppercase && !includeLowercase && !includeNumbers)
            throw new ArgumentException("至少需要包含一种字符类型");

        var chars = new List<char>();

        if (includeUppercase)
            chars.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        if (includeLowercase)
            chars.AddRange("abcdefghijklmnopqrstuvwxyz");

        if (includeNumbers)
            chars.AddRange("0123456789");

        var random = new Random();
        var code = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            code.Append(chars[random.Next(chars.Count)]);
        }

        return code.ToString();
    }

    /// <summary>
    /// 生成加密验证码
    /// </summary>
    /// <param name="length">验证码长度（默认6位）</param>
    /// <returns>加密验证码</returns>
    public static string GenerateSecureCode(int length = DefaultCodeLength)
    {
        if (length <= 0 || length > 10)
            throw new ArgumentException("验证码长度必须在1-10之间", nameof(length));

        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);

        var code = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            code.Append(bytes[i] % 10);
        }

        return code.ToString();
    }

    /// <summary>
    /// 验证手机号格式
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <returns>是否有效</returns>
    public static bool IsValidPhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        return PhoneRegex.IsMatch(phone);
    }

    /// <summary>
    /// 验证验证码格式
    /// </summary>
    /// <param name="code">验证码</param>
    /// <param name="length">期望长度</param>
    /// <returns>是否有效</returns>
    public static bool IsValidCode(string code, int length = DefaultCodeLength)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        if (code.Length != length)
            return false;

        // 检查是否全为数字
        return code.All(char.IsDigit);
    }

    /// <summary>
    /// 生成验证码哈希
    /// </summary>
    /// <param name="code">验证码</param>
    /// <param name="salt">盐值</param>
    /// <returns>哈希值</returns>
    public static string GenerateCodeHash(string code, string salt)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentException("验证码不能为空", nameof(code));

        if (string.IsNullOrEmpty(salt))
            throw new ArgumentException("盐值不能为空", nameof(salt));

        using var sha256 = SHA256.Create();
        var combined = $"{code}{salt}";
        var bytes = Encoding.UTF8.GetBytes(combined);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// 验证验证码哈希
    /// </summary>
    /// <param name="code">验证码</param>
    /// <param name="salt">盐值</param>
    /// <param name="hash">哈希值</param>
    /// <returns>是否匹配</returns>
    public static bool VerifyCodeHash(string code, string salt, string hash)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hash))
            return false;

        var expectedHash = GenerateCodeHash(code, salt);
        return string.Equals(expectedHash, hash, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 生成盐值
    /// </summary>
    /// <param name="length">盐值长度（默认16）</param>
    /// <returns>盐值</returns>
    public static string GenerateSalt(int length = 16)
    {
        if (length <= 0 || length > 64)
            throw new ArgumentException("盐值长度必须在1-64之间", nameof(length));

        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 检查发送频率限制
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="currentTime">当前时间</param>
    /// <param name="lastSendTime">上次发送时间</param>
    /// <param name="hourlyCount">每小时发送次数</param>
    /// <param name="dailyCount">每天发送次数</param>
    /// <param name="hourlyLimit">每小时限制（默认10次）</param>
    /// <param name="dailyLimit">每天限制（默认50次）</param>
    /// <param name="minIntervalSeconds">最小间隔秒数（默认60秒）</param>
    /// <returns>频率检查结果</returns>
    public static SmsFrequencyCheckResult CheckFrequencyLimit(string phone, DateTime currentTime, 
        DateTime? lastSendTime, int hourlyCount, int dailyCount, 
        int hourlyLimit = DefaultHourlyLimit, int dailyLimit = DefaultDailyLimit, 
        int minIntervalSeconds = 60)
    {
        if (!IsValidPhoneNumber(phone))
            return new SmsFrequencyCheckResult
            {
                CanSend = false,
                Reason = "无效的手机号格式"
            };

        // 检查最小间隔
        if (lastSendTime.HasValue)
        {
            var timeSpan = currentTime - lastSendTime.Value;
            if (timeSpan.TotalSeconds < minIntervalSeconds)
            {
                return new SmsFrequencyCheckResult
                {
                    CanSend = false,
                    Reason = $"发送间隔过短，需要等待 {minIntervalSeconds - (int)timeSpan.TotalSeconds} 秒",
                    RemainingSeconds = minIntervalSeconds - (int)timeSpan.TotalSeconds
                };
            }
        }

        // 检查每小时限制
        if (hourlyCount >= hourlyLimit)
        {
            return new SmsFrequencyCheckResult
            {
                CanSend = false,
                Reason = $"每小时发送次数已达上限 {hourlyLimit} 次",
                RemainingAttempts = 0
            };
        }

        // 检查每天限制
        if (dailyCount >= dailyLimit)
        {
            return new SmsFrequencyCheckResult
            {
                CanSend = false,
                Reason = $"每天发送次数已达上限 {dailyLimit} 次",
                RemainingAttempts = 0
            };
        }

        return new SmsFrequencyCheckResult
        {
            CanSend = true,
            Reason = "可以发送",
            RemainingAttempts = Math.Min(hourlyLimit - hourlyCount, dailyLimit - dailyCount)
        };
    }

    /// <summary>
    /// 生成短信模板内容
    /// </summary>
    /// <param name="template">模板</param>
    /// <param name="code">验证码</param>
    /// <param name="expirationMinutes">有效期（分钟）</param>
    /// <param name="companyName">公司名称</param>
    /// <returns>短信内容</returns>
    public static string GenerateSmsContent(string template, string code, int expirationMinutes = DefaultExpirationMinutes, string companyName = "Lean.Hbt")
    {
        if (string.IsNullOrEmpty(template))
            template = "您的验证码是：{code}，有效期{expiration}分钟，请勿泄露给他人。【{company}】";

        return template
            .Replace("{code}", code)
            .Replace("{expiration}", expirationMinutes.ToString())
            .Replace("{company}", companyName);
    }

    /// <summary>
    /// 生成验证码缓存键
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="codeType">验证码类型</param>
    /// <returns>缓存键</returns>
    public static string GenerateCacheKey(string phone, string codeType = "login")
    {
        return $"sms_code:{phone}:{codeType}";
    }

    /// <summary>
    /// 生成发送次数缓存键
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="codeType">验证码类型</param>
    /// <param name="period">周期（hourly/daily）</param>
    /// <returns>缓存键</returns>
    public static string GenerateCountCacheKey(string phone, string codeType = "login", string period = "hourly")
    {
        return $"sms_count:{phone}:{codeType}:{period}";
    }

    /// <summary>
    /// 获取手机号运营商信息
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <returns>运营商信息</returns>
    public static SmsCarrierInfo GetCarrierInfo(string phone)
    {
        if (!IsValidPhoneNumber(phone))
            return new SmsCarrierInfo { IsValid = false };

        var prefix = phone.Substring(0, 3);
        
        // 中国移动
        if (new[] { "134", "135", "136", "137", "138", "139", "147", "150", "151", "152", "157", "158", "159", "178", "182", "183", "184", "187", "188", "198" }.Contains(prefix))
        {
            return new SmsCarrierInfo { IsValid = true, Carrier = "中国移动", Prefix = prefix };
        }
        
        // 中国联通
        if (new[] { "130", "131", "132", "145", "155", "156", "166", "175", "176", "185", "186" }.Contains(prefix))
        {
            return new SmsCarrierInfo { IsValid = true, Carrier = "中国联通", Prefix = prefix };
        }
        
        // 中国电信
        if (new[] { "133", "149", "153", "173", "177", "180", "181", "189", "199" }.Contains(prefix))
        {
            return new SmsCarrierInfo { IsValid = true, Carrier = "中国电信", Prefix = prefix };
        }

        return new SmsCarrierInfo { IsValid = true, Carrier = "未知运营商", Prefix = prefix };
    }

    /// <summary>
    /// 生成测试验证码（仅用于开发环境）
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="length">验证码长度</param>
    /// <returns>测试验证码</returns>
    public static string GenerateTestCode(string phone, int length = DefaultCodeLength)
    {
        if (!IsValidPhoneNumber(phone))
            throw new ArgumentException("无效的手机号格式", nameof(phone));

        // 使用手机号后4位作为种子，确保同一手机号总是生成相同的测试码
        var lastFourDigits = phone.Substring(phone.Length - 4);
        var seed = int.Parse(lastFourDigits);
        var random = new Random(seed);

        var code = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            code.Append(random.Next(0, 10));
        }

        return code.ToString();
    }
}

/// <summary>
/// 短信频率检查结果
/// </summary>
public class SmsFrequencyCheckResult
{
    /// <summary>
    /// 是否可以发送
    /// </summary>
    public bool CanSend { get; set; }

    /// <summary>
    /// 原因说明
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// 剩余尝试次数
    /// </summary>
    public int RemainingAttempts { get; set; }

    /// <summary>
    /// 剩余等待秒数
    /// </summary>
    public int RemainingSeconds { get; set; }
}

/// <summary>
/// 短信运营商信息
/// </summary>
public class SmsCarrierInfo
{
    /// <summary>
    /// 是否有效
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 运营商名称
    /// </summary>
    public string Carrier { get; set; } = string.Empty;

    /// <summary>
    /// 号段前缀
    /// </summary>
    public string Prefix { get; set; } = string.Empty;
} 