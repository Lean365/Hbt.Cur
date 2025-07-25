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
    /// 验证码类型（Slider、Behavior）
    /// </summary>
    public string Type { get; set; } = string.Empty;

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
    public int Width { get; set; }

    /// <summary>
    /// 背景图片高度
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 滑块宽度
    /// </summary>
    public int SliderWidth { get; set; }

    /// <summary>
    /// 验证容差(像素)
    /// </summary>
    public int Tolerance { get; set; }

    /// <summary>
    /// 缓存过期时间(分钟)
    /// </summary>
    public int ExpirationMinutes { get; set; }

    /// <summary>
    /// 背景图片配置
    /// </summary>
    public BackgroundImageOptions BackgroundImages { get; set; } = new();
}

/// <summary>
/// 背景图片配置选项
/// </summary>
public class BackgroundImageOptions
{
    /// <summary>
    /// 是否在系统启动时重新下载图片
    /// </summary>
    public bool RedownloadOnStartup { get; set; } = false;

    /// <summary>
    /// 最小图片数量
    /// </summary>
    public int MinCount { get; set; } = 5;

    /// <summary>
    /// 下载URL模板，使用 {width} 和 {height} 作为占位符
    /// </summary>
    public string DownloadUrl { get; set; } = "https://picsum.photos/{width}/{height}";

    /// <summary>
    /// 图片存储路径
    /// </summary>
    public string StoragePath { get; set; } = "slide/background";

    /// <summary>
    /// 图片文件扩展名
    /// </summary>
    public string FileExtension { get; set; } = ".png";

    /// <summary>
    /// 模板配置
    /// </summary>
    public SlideTemplateOptions Template { get; set; } = new();
}

/// <summary>
/// 滑块验证码模板配置
/// </summary>
public class SlideTemplateOptions
{
    /// <summary>
    /// 是否使用模板图片
    /// </summary>
    public bool UseTemplate { get; set; } = true;

    /// <summary>
    /// 模板目录路径
    /// </summary>
    public string TemplatePath { get; set; } = "slide/template";

    /// <summary>
    /// 图片组数量
    /// </summary>
    public int GroupCount { get; set; } = 5;
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