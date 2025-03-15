using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Text.Json;
using System.Linq;
using Lean.Hbt.Common.Helpers;
using Point = SixLabors.ImageSharp.Point;

namespace Lean.Hbt.Infrastructure.Security;

/// <summary>
/// 验证码服务实现
/// </summary>
public class HbtCaptchaService : IHbtCaptchaService
{
    private readonly IDistributedCache _cache;
    private readonly HbtCaptchaOptions _options;
    private readonly string _sliderCachePrefix = "slider:";
    private readonly string _behaviorCachePrefix = "behavior:";
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _backgroundImagesPath;
    private readonly string _templatePath;
    private readonly ILogger<HbtCaptchaService> _logger;
    private bool _templatesValid = false;

    /// <summary>
    /// 初始化验证码服务
    /// </summary>
    public HbtCaptchaService(
        IDistributedCache cache,
        IOptions<HbtCaptchaOptions> options,
        IWebHostEnvironment webHostEnvironment,
        ILogger<HbtCaptchaService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("开始构造验证码服务...");
            
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

        _templatePath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.Template.TemplatePath);
        _backgroundImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.StoragePath);
        
        _logger.LogInformation("验证码服务构造完成");
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    public async Task<(string bgImage, string sliderImage, string token)> GenerateSliderAsync()
    {
        // 随机选择一组模板
        var random = new Random();
        var groupIndex = random.Next(1, _options.Slider.BackgroundImages.Template.GroupCount + 1);
        var groupPath = Path.Combine(_templatePath, groupIndex.ToString());

        // 构建模板文件路径
        var holePath = Path.Combine(groupPath, "hole.png");
        var sliderPath = Path.Combine(groupPath, "slider.png");

        if (!File.Exists(holePath) || !File.Exists(sliderPath))
        {
            throw new InvalidOperationException("验证码模板文件不存在");
        }

        // 加载模板图片
        using var holeImage = await Image.LoadAsync<Rgba32>(holePath);
        using var sliderImage = await Image.LoadAsync<Rgba32>(sliderPath);

        // 随机选择一张背景图片
        var backgroundFiles = Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}");
        if (backgroundFiles.Length == 0)
        {
            throw new Exception("没有可用的背景图片");
        }

        var selectedBackground = backgroundFiles[random.Next(backgroundFiles.Length)];
        using var bgImage = await Image.LoadAsync<Rgba32>(selectedBackground);

        // 调整背景图片大小
        var targetWidth = _options.Slider.Width;  // 350px
        var targetHeight = _options.Slider.Height; // 150px
        bgImage.Mutate(x => x.Resize(targetWidth, targetHeight));

        // 使用模板图片的原始尺寸
        var sliderSize = 48; // 滑块的实际尺寸

        // 计算有效的X坐标范围（确保滑块不会超出背景图片）
        var minX = sliderSize * 2; // 留出左边距，避免太靠左
        var maxX = targetWidth - sliderSize * 3; // 留出右边距
        
        // 生成随机的X坐标，Y坐标固定在中间位置
        var xPos = random.Next(minX, maxX);
        var yPos = (targetHeight - sliderSize) / 2;

        // 创建一个新的背景图副本，用于应用挖空效果
        using var processedBgImage = bgImage.Clone();
        
        // 在背景图上应用挖空效果
        processedBgImage.Mutate(x => x.DrawImage(holeImage, new Point(xPos, yPos), 0.8f));

        // 创建滑块图片（保持原始大小）
        using var finalSliderImage = new Image<Rgba32>(sliderSize, sliderSize);
        finalSliderImage.Mutate(x => 
        {
            x.Clear(Color.Transparent);
            x.DrawImage(sliderImage, new Point(0, 0), 1f);
        });

        // 生成验证token和缓存数据
        var token = GenerateToken();
        var cacheData = new SliderCacheData
        {
            X = xPos,
            Y = yPos,
            CreatedAt = DateTime.UtcNow
        };

        // 缓存验证数据
        await _cache.SetStringAsync(
            $"{_sliderCachePrefix}{token}",
            JsonSerializer.Serialize(cacheData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Slider.ExpirationMinutes)
            }
        );

        return (
            ImageToBase64(processedBgImage),
            ImageToBase64(finalSliderImage),
            token
        );
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    public async Task<bool> ValidateSliderAsync(string token, int xOffset)
    {
        _logger.LogInformation("验证Token详情 - 接收到的Token: {Token}, 时间: {Time}", token, DateTime.Now);
        
        var cacheKey = $"{_sliderCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            _logger.LogWarning("验证失败：Token无效或已过期 - Token: {Token}, 缓存Key: {CacheKey}", token, cacheKey);
            return false;
        }

        var cacheData = JsonSerializer.Deserialize<SliderCacheData>(cacheValue);
        if (cacheData == null)
        {
            _logger.LogWarning("验证失败：缓存数据无效 - Token: {Token}", token);
            return false;
        }

        // 验证是否过期
        var timeSinceCreation = DateTime.UtcNow - cacheData.CreatedAt;
        if (timeSinceCreation.TotalMinutes > _options.Slider.ExpirationMinutes)
        {
            _logger.LogWarning("验证失败：验证码已过期 - Token: {Token}, 创建时间: {CreatedAt}, 当前时间: {Now}, 已过时间: {TimeSinceCreation:g}", 
                token, 
                cacheData.CreatedAt, 
                DateTime.UtcNow,
                timeSinceCreation);
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 验证是否已被使用
        if (cacheData.IsVerified)
        {
            _logger.LogWarning("验证失败：验证码已被使用 - Token: {Token}", token);
            return false;
        }

        // 验证偏移量是否在容差范围内
        var difference = Math.Abs(xOffset - cacheData.X);
        var isValid = difference <= _options.Slider.Tolerance;
        
        _logger.LogInformation(
            "验证详情 - 期望位置: {ExpectedX}, 实际位置: {ActualX}, 差值: {Difference}, 容差: {Tolerance}, 结果: {Result}",
            cacheData.X,
            xOffset,
            difference,
            _options.Slider.Tolerance,
            isValid ? "验证通过" : "验证失败"
        );
        
        return isValid;
    }

    /// <summary>
    /// 标记验证码已使用
    /// </summary>
    public async Task MarkAsUsedAsync(string token)
    {
        var cacheKey = $"{_sliderCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cacheValue))
        {
            var cacheData = JsonSerializer.Deserialize<SliderCacheData>(cacheValue);
            if (cacheData != null)
            {
                cacheData.IsVerified = true;
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(cacheData),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Slider.ExpirationMinutes)
                    }
                );
                _logger.LogInformation("验证码已标记为已使用 - Token: {Token}", token);
            }
        }
    }

    /// <summary>
    /// 收集行为数据
    /// </summary>
    public async Task<string> CollectBehaviorDataAsync(string userId, BehaviorData data)
    {
        var token = GenerateToken();
        var cacheData = new BehaviorCacheData
        {
            UserId = userId,
            Data = data,
            CreatedAt = DateTime.UtcNow,
            Score = CalculateBehaviorScore(data)
        };

        await _cache.SetStringAsync(
            $"{_behaviorCachePrefix}{token}",
            JsonSerializer.Serialize(cacheData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Behavior.DataExpirationMinutes)
            }
        );

        return token;
    }

    /// <summary>
    /// 验证行为特征
    /// </summary>
    public async Task<bool> ValidateBehaviorAsync(string token)
    {
        var cacheKey = $"{_behaviorCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            return false;
        }

        var cacheData = JsonSerializer.Deserialize<BehaviorCacheData>(cacheValue);
        if (cacheData == null)
        {
            return false;
        }

        // 验证是否过期
        if ((DateTime.UtcNow - cacheData.CreatedAt).TotalMinutes > _options.Behavior.DataExpirationMinutes)
        {
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 验证行为分数是否达到阈值
        var isValid = cacheData.Score >= _options.Behavior.ScoreThreshold;
        
        // 验证完成后删除缓存
        await _cache.RemoveAsync(cacheKey);
        
        return isValid;
    }

    #region 私有方法

    /// <summary>
    /// 生成验证token
    /// </summary>
    private static string GenerateToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }

    /// <summary>
    /// 计算行为分数
    /// </summary>
    private double CalculateBehaviorScore(BehaviorData data)
    {
        if (_options.Behavior.EnableMachineLearning && !string.IsNullOrEmpty(_options.Behavior.ModelPath))
        {
            // TODO: 使用机器学习模型计算分数
            return 0.9;
        }

        var score = 0.0;

        // 1. 检查操作时长是否合理
        if (data.Duration >= 500 && data.Duration <= 5000)
        {
            score += 0.3;
        }

        // 2. 分析鼠标轨迹
        if (data.MouseTrack.Count >= 10)
        {
            score += 0.3;
            
            // 检查轨迹是否平滑
            var smoothScore = CalculateTrackSmoothness(data.MouseTrack);
            score += smoothScore * 0.2;
        }

        // 3. 分析按键间隔
        if (data.KeyIntervals.Count >= 2)
        {
            score += 0.2;
        }

        return Math.Min(score, 1.0);
    }

    /// <summary>
    /// 计算轨迹平滑度
    /// </summary>
    private double CalculateTrackSmoothness(List<Domain.IServices.Security.Point> track)
    {
        if (track.Count < 3)
        {
            return 0;
        }

        var smoothCount = 0;
        for (int i = 1; i < track.Count - 1; i++)
        {
            var prev = track[i - 1];
            var curr = track[i];
            var next = track[i + 1];

            // 计算两个向量的夹角
            var v1x = curr.X - prev.X;
            var v1y = curr.Y - prev.Y;
            var v2x = next.X - curr.X;
            var v2y = next.Y - curr.Y;

            var cos = (v1x * v2x + v1y * v2y) / 
                     (Math.Sqrt(v1x * v1x + v1y * v1y) * Math.Sqrt(v2x * v2x + v2y * v2y));

            // 夹角小于45度认为是平滑的
            if (cos > 0.7)
            {
                smoothCount++;
            }
        }

        return (double)smoothCount / (track.Count - 2);
    }

    /// <summary>
    /// 将图片转换为Base64字符串
    /// </summary>
    private static string ImageToBase64<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
    {
        using var ms = new MemoryStream();
        image.SaveAsPng(ms);
        return $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
    }

    #endregion

    #region 私有类

    private class SliderCacheData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
    }

    private class BehaviorCacheData
    {
        public string UserId { get; set; } = string.Empty;
        public BehaviorData Data { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public double Score { get; set; }
    }

    #endregion
} 