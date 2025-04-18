namespace Lean.Hbt.Application.Dtos.Security;

/// <summary>
/// 滑块验证码生成结果
/// </summary>
public class SliderCaptchaDto
{
    /// <summary>
    /// 背景图片(Base64)
    /// </summary>
    public string? BackgroundImage { get; set; }

    /// <summary>
    /// 滑块图片(Base64)
    /// </summary>
    public string? SliderImage { get; set; }

    /// <summary>
    /// 验证令牌
    /// </summary>
    public string? Token { get; set; }
}

/// <summary>
/// 滑块验证请求
/// </summary>
public class SliderValidateDto
{
    /// <summary>
    /// 验证令牌
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// X轴偏移量
    /// </summary>
    public int XOffset { get; set; }
}

/// <summary>
/// 行为数据点
/// </summary>
public class BehaviorPointDto
{
    /// <summary>
    /// X坐标
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y坐标
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}

/// <summary>
/// 行为数据
/// </summary>
public class BehaviorDataDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// 鼠标轨迹
    /// </summary>
    public List<BehaviorPointDto> MouseTrack { get; set; } = new();

    /// <summary>
    /// 按键间隔(毫秒)
    /// </summary>
    public List<int> KeyIntervals { get; set; } = new();

    /// <summary>
    /// 操作时长(毫秒)
    /// </summary>
    public int Duration { get; set; }
}

/// <summary>
/// 验证结果
/// </summary>
public class CaptchaResultDto
{
    /// <summary>
    /// 是否通过验证
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? Message { get; set; }
}