namespace Lean.Hbt.Common.Options;

/// <summary>
/// 验证码配置选项
/// </summary>
public class HbtCaptchaOptions
{
    /// <summary>
    /// 登录失败阈值
    /// </summary>
    public int LoginFailThreshold { get; set; } = 3;

    /// <summary>
    /// 登录失败记录过期时间(分钟)
    /// </summary>
    public int LoginFailExpireMinutes { get; set; } = 30;

    /// <summary>
    /// 验证码类型
    /// </summary>
    public string Type { get; set; } = "Slider";

    /// <summary>
    /// 滑块验证码选项
    /// </summary>
    public SliderOptions Slider { get; set; } = new();

    /// <summary>
    /// 行为验证选项
    /// </summary>
    public BehaviorOptions Behavior { get; set; } = new();
}

/// <summary>
/// 滑块验证码选项
/// </summary>
public class SliderOptions
{
    /// <summary>
    /// 背景图片宽度
    /// </summary>
    public int Width { get; set; } = 300;

    /// <summary>
    /// 背景图片高度
    /// </summary>
    public int Height { get; set; } = 150;

    /// <summary>
    /// 滑块宽度
    /// </summary>
    public int SliderWidth { get; set; } = 50;

    /// <summary>
    /// 验证容差(像素)
    /// </summary>
    public int Tolerance { get; set; } = 5;

    /// <summary>
    /// 缓存过期时间(分钟)
    /// </summary>
    public int ExpirationMinutes { get; set; } = 5;
}

/// <summary>
/// 行为验证选项
/// </summary>
public class BehaviorOptions
{
    /// <summary>
    /// 验证通过的最低分数
    /// </summary>
    public double ScoreThreshold { get; set; } = 0.8;

    /// <summary>
    /// 行为数据缓存时间(分钟)
    /// </summary>
    public int DataExpirationMinutes { get; set; } = 30;

    /// <summary>
    /// 是否启用机器学习模型
    /// </summary>
    public bool EnableMachineLearning { get; set; } = false;

    /// <summary>
    /// 机器学习模型路径
    /// </summary>
    public string? ModelPath { get; set; }
} 