using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Lean.Hbt.Common.Options;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Entities.Admin;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Text.Json;

namespace Lean.Hbt.Infrastructure.Security;

/// <summary>
/// 验证码服务实现
/// </summary>
public class HbtCaptchaService : IHbtCaptchaService
{
    private readonly IDistributedCache _cache;
    private readonly HbtCaptchaOptions _options;
    private readonly IHbtRepository<HbtConfig> _repository;
    private readonly string _sliderCachePrefix = "slider:";
    private readonly string _behaviorCachePrefix = "behavior:";

    /// <summary>
    /// 初始化验证码服务
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="options">验证码配置选项</param>
    /// <param name="repository">系统配置仓储</param>
    public HbtCaptchaService(
        IDistributedCache cache,
        IOptions<HbtCaptchaOptions> options,
        IHbtRepository<HbtConfig> repository)
    {
        _cache = cache;
        _options = options.Value;
        _repository = repository;
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    public async Task<(string bgImage, string sliderImage, string token)> GenerateSliderAsync()
    {
        // 1. 生成随机背景图
        using var bgImage = new Image<Rgba32>(_options.Slider.Width, _options.Slider.Height);
        var random = new Random();
        for (int x = 0; x < bgImage.Width; x++)
        {
            for (int y = 0; y < bgImage.Height; y++)
            {
                bgImage[x, y] = new Rgba32(
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256)
                );
            }
        }

        // 2. 生成滑块
        var sliderX = random.Next(_options.Slider.SliderWidth, bgImage.Width - _options.Slider.SliderWidth * 2);
        var sliderY = random.Next(_options.Slider.SliderWidth, bgImage.Height - _options.Slider.SliderWidth);
        
        using var sliderImage = new Image<Rgba32>(_options.Slider.SliderWidth, _options.Slider.SliderWidth);
        var cropRect = new SixLabors.ImageSharp.Rectangle(sliderX, sliderY, _options.Slider.SliderWidth, _options.Slider.SliderWidth);
        using var croppedImage = bgImage.Clone(x => x.Crop(cropRect));
        croppedImage.Mutate(x => x.Resize(_options.Slider.SliderWidth, _options.Slider.SliderWidth));
        croppedImage.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < accessor.Width; x++)
                {
                    sliderImage[x, y] = row[x];
                }
            }
        });

        // 3. 在背景图上绘制滑块区域
        bgImage.Mutate(ctx => ctx.Draw(
            SixLabors.ImageSharp.Drawing.Processing.Pens.Solid(SixLabors.ImageSharp.Color.White, 2),
            new SixLabors.ImageSharp.RectangleF(sliderX, sliderY, _options.Slider.SliderWidth, _options.Slider.SliderWidth)
        ));

        // 4. 生成验证token
        var token = GenerateToken();
        var cacheData = new SliderCacheData
        {
            X = sliderX,
            Y = sliderY,
            CreatedAt = DateTime.UtcNow
        };

        // 5. 缓存验证数据
        await _cache.SetStringAsync(
            $"{_sliderCachePrefix}{token}",
            JsonSerializer.Serialize(cacheData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Slider.ExpirationMinutes)
            }
        );

        // 6. 转换图片为Base64
        return (
            ImageToBase64(bgImage),
            ImageToBase64(sliderImage),
            token
        );
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    public async Task<bool> ValidateSliderAsync(string token, int xOffset)
    {
        var cacheKey = $"{_sliderCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            return false;
        }

        var cacheData = JsonSerializer.Deserialize<SliderCacheData>(cacheValue);
        if (cacheData == null)
        {
            return false;
        }

        // 验证是否过期
        if ((DateTime.UtcNow - cacheData.CreatedAt).TotalMinutes > _options.Slider.ExpirationMinutes)
        {
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 验证偏移量是否在容差范围内
        var isValid = Math.Abs(xOffset - cacheData.X) <= _options.Slider.Tolerance;
        
        // 验证完成后删除缓存
        await _cache.RemoveAsync(cacheKey);
        
        return isValid;
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
    /// 生成随机token
    /// </summary>
    private string GenerateToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 图片转Base64
    /// </summary>
    private string ImageToBase64<T>(Image<T> image) where T : unmanaged, IPixel<T>
    {
        using var ms = new MemoryStream();
        image.SaveAsJpeg(ms);
        return $"data:image/jpeg;base64,{Convert.ToBase64String(ms.ToArray())}";
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

    #endregion

    #region 私有类

    private class SliderCacheData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime CreatedAt { get; set; }
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