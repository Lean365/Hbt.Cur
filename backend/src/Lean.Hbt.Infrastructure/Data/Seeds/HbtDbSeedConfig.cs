//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedConfig.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 系统配置种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 系统配置种子数据初始化类
/// </summary>
public class HbtDbSeedConfig
{
    private readonly IHbtRepository<HbtConfig> _configRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configRepository">配置仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedConfig(IHbtRepository<HbtConfig> configRepository, IHbtLogger logger)
    {
        _configRepository = configRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化配置数据
    /// </summary>
    public async Task<(int, int)> InitializeConfigAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultConfigs = new List<HbtConfig>
        {
            // 缓存配置
            new HbtConfig { Id = 1, ConfigName = "缓存提供程序", ConfigKey = "Cache:Provider", ConfigValue = "Memory", ConfigBuiltin = 1, OrderNum = 50, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "缓存提供程序类型(Memory/Redis)" },
            new HbtConfig { Id = 2, ConfigName = "默认过期时间(分钟)", ConfigKey = "Cache:DefaultExpirationMinutes", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 51, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "缓存默认过期时间(分钟)" },
            new HbtConfig { Id = 3, ConfigName = "启用滑动过期", ConfigKey = "Cache:EnableSlidingExpiration", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 52, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用滑动过期" },
            new HbtConfig { Id = 4, ConfigName = "启用多级缓存", ConfigKey = "Cache:EnableMultiLevelCache", ConfigValue = "false", ConfigBuiltin = 1, OrderNum = 53, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用多级缓存" },
            new HbtConfig { Id = 5, ConfigName = "内存缓存大小限制", ConfigKey = "Cache:Memory:SizeLimit", ConfigValue = "104857600", ConfigBuiltin = 1, OrderNum = 54, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "内存缓存大小限制(字节)" },
            new HbtConfig { Id = 6, ConfigName = "内存缓存压缩阈值", ConfigKey = "Cache:Memory:CompactionThreshold", ConfigValue = "1048576", ConfigBuiltin = 1, OrderNum = 55, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "内存缓存压缩阈值(字节)" },
            new HbtConfig { Id = 7, ConfigName = "过期扫描频率", ConfigKey = "Cache:Memory:ExpirationScanFrequency", ConfigValue = "60", ConfigBuiltin = 1, OrderNum = 56, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "过期扫描频率(秒)" },
            new HbtConfig { Id = 8, ConfigName = "Redis实例名称", ConfigKey = "Cache:Redis:InstanceName", ConfigValue = "Lean.Hbt", ConfigBuiltin = 1, OrderNum = 57, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "Redis实例名称" },
            new HbtConfig { Id = 9, ConfigName = "Redis数据库", ConfigKey = "Cache:Redis:DefaultDatabase", ConfigValue = "0", ConfigBuiltin = 1, OrderNum = 58, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "Redis默认数据库编号" },
            new HbtConfig { Id = 10, ConfigName = "Redis启用压缩", ConfigKey = "Cache:Redis:EnableCompression", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 59, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用Redis数据压缩" },
            new HbtConfig { Id = 11, ConfigName = "Redis压缩阈值", ConfigKey = "Cache:Redis:CompressionThreshold", ConfigValue = "1024", ConfigBuiltin = 1, OrderNum = 60, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "Redis数据压缩阈值(字节)" },

            // OAuth配置
            new HbtConfig { Id = 12, ConfigName = "OAuth启用状态", ConfigKey = "Security:Oidentity:Enabled", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 70, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用OAuth认证" },
            new HbtConfig { Id = 13, ConfigName = "GitHub客户端ID", ConfigKey = "Security:Oidentity:Providers:GitHub:ClientId", ConfigValue = "", ConfigBuiltin = 1, OrderNum = 71, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth应用的客户端ID" },
            new HbtConfig { Id = 14, ConfigName = "GitHub客户端密钥", ConfigKey = "Security:Oidentity:Providers:GitHub:ClientSecret", ConfigValue = "", ConfigBuiltin = 1, OrderNum = 72, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth应用的客户端密钥" },
            new HbtConfig { Id = 15, ConfigName = "GitHub授权端点", ConfigKey = "Security:Oidentity:Providers:GitHub:AuthorizationEndpoint", ConfigValue = "https://github.com/login/oidentity/authorize", ConfigBuiltin = 1, OrderNum = 73, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth授权端点URL" },
            new HbtConfig { Id = 16, ConfigName = "GitHub令牌端点", ConfigKey = "Security:Oidentity:Providers:GitHub:TokenEndpoint", ConfigValue = "https://github.com/login/oidentity/access_token", ConfigBuiltin = 1, OrderNum = 74, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth令牌端点URL" },
            new HbtConfig { Id = 17, ConfigName = "GitHub用户信息端点", ConfigKey = "Security:Oidentity:Providers:GitHub:UserInfoEndpoint", ConfigValue = "https://api.github.com/user", ConfigBuiltin = 1, OrderNum = 75, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth用户信息端点URL" },
            new HbtConfig { Id = 18, ConfigName = "GitHub回调地址", ConfigKey = "Security:Oidentity:Providers:GitHub:RedirectUri", ConfigValue = "https://localhost:5001/oidentity/callback/github", ConfigBuiltin = 1, OrderNum = 76, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth回调地址" },
            new HbtConfig { Id = 19, ConfigName = "GitHub权限范围", ConfigKey = "Security:Oidentity:Providers:GitHub:Scope", ConfigValue = "read:user user:email", ConfigBuiltin = 1, OrderNum = 77, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "GitHub OAuth所需权限范围" },

            // 日志清理配置
            new HbtConfig { Id = 22, ConfigName = "日志清理启用状态", ConfigKey = "LogCleanup:Enabled", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 90, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用日志自动清理" },
            new HbtConfig { Id = 23, ConfigName = "日志保留天数", ConfigKey = "LogCleanup:RetentionDays", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 91, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "日志保留天数，超过该天数的日志将被清理" },
            new HbtConfig { Id = 24, ConfigName = "日志清理执行时间", ConfigKey = "LogCleanup:ExecutionTime", ConfigValue = "02:00:00", ConfigBuiltin = 1, OrderNum = 92, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "日志清理的执行时间（24小时制）" },
            new HbtConfig { Id = 25, ConfigName = "批次清理数量", ConfigKey = "LogCleanup:BatchSize", ConfigValue = "1000", ConfigBuiltin = 1, OrderNum = 93, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "每次清理的日志数量" },
            new HbtConfig { Id = 26, ConfigName = "日志类型", ConfigKey = "LogCleanup:LogTypes", ConfigValue = "Info,Debug,Warning", ConfigBuiltin = 1, OrderNum = 94, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "需要清理的日志类型，多个类型用逗号分隔" },

            // 日志归档配置
            new HbtConfig { Id = 27, ConfigName = "日志归档启用状态", ConfigKey = "LogArchive:Enabled", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 100, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用日志自动归档" },
            new HbtConfig { Id = 28, ConfigName = "归档触发天数", ConfigKey = "LogArchive:TriggerDays", ConfigValue = "90", ConfigBuiltin = 1, OrderNum = 101, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "超过多少天的日志将被归档" },
            new HbtConfig { Id = 29, ConfigName = "归档执行时间", ConfigKey = "LogArchive:ExecutionTime", ConfigValue = "03:00:00", ConfigBuiltin = 1, OrderNum = 102, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "日志归档的执行时间（24小时制）" },
            new HbtConfig { Id = 30, ConfigName = "归档批次大小", ConfigKey = "LogArchive:BatchSize", ConfigValue = "1000", ConfigBuiltin = 1, OrderNum = 103, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "每次归档的日志数量" },
            new HbtConfig { Id = 31, ConfigName = "归档存储路径", ConfigKey = "LogArchive:StoragePath", ConfigValue = "Archive/Logs", ConfigBuiltin = 1, OrderNum = 104, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "日志归档文件的存储路径" },
            new HbtConfig { Id = 32, ConfigName = "归档文件格式", ConfigKey = "LogArchive:FileFormat", ConfigValue = "json", ConfigBuiltin = 1, OrderNum = 105, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "归档文件的格式(json/csv)" },
            new HbtConfig { Id = 33, ConfigName = "归档压缩启用", ConfigKey = "LogArchive:Compression:Enabled", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 106, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用归档文件压缩" },
            new HbtConfig { Id = 34, ConfigName = "归档压缩格式", ConfigKey = "LogArchive:Compression:Format", ConfigValue = "gzip", ConfigBuiltin = 1, OrderNum = 107, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "归档文件的压缩格式(gzip/zip)" },

            // 安全配置 - 密码策略
            new HbtConfig { Id = 35, ConfigName = "密码最小长度", ConfigKey = "Security:Password:MinLength", ConfigValue = "8", ConfigBuiltin = 1, OrderNum = 110, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "密码最小长度要求" },
            new HbtConfig { Id = 36, ConfigName = "密码复杂度要求", ConfigKey = "Security:Password:RequireComplexity", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 111, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否要求密码包含大小写字母、数字和特殊字符" },
            new HbtConfig { Id = 37, ConfigName = "密码过期天数", ConfigKey = "Security:Password:ExpirationDays", ConfigValue = "90", ConfigBuiltin = 1, OrderNum = 112, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "密码过期天数，0表示永不过期" },
            new HbtConfig { Id = 38, ConfigName = "密码历史记录数", ConfigKey = "Security:Password:HistoryCount", ConfigValue = "3", ConfigBuiltin = 1, OrderNum = 113, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "记住多少个历史密码，防止重复使用" },

            // 安全配置 - 登录锁定
            new HbtConfig { Id = 39, ConfigName = "登录失败锁定次数", ConfigKey = "Security:Lockout:MaxFailedAttempts", ConfigValue = "5", ConfigBuiltin = 1, OrderNum = 114, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "允许的最大登录失败次数" },
            new HbtConfig { Id = 40, ConfigName = "锁定时间(分钟)", ConfigKey = "Security:Lockout:DurationMinutes", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 115, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "账户锁定持续时间(分钟)" },

            // 安全配置 - 会话管理
            new HbtConfig { Id = 41, ConfigName = "会话超时时间(分钟)", ConfigKey = "Security:Session:TimeoutMinutes", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 116, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "用户会话超时时间(分钟)" },
            new HbtConfig { Id = 42, ConfigName = "允许多端登录", ConfigKey = "Security:Session:AllowMultipleLogin", ConfigValue = "false", ConfigBuiltin = 1, OrderNum = 117, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否允许同一账户多个终端同时登录" },

            // 安全配置 - JWT
            new HbtConfig { Id = 43, ConfigName = "JWT密钥", ConfigKey = "Security:Jwt:SecretKey", ConfigValue = "your-secret-key-here", ConfigBuiltin = 1, OrderNum = 118, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "JWT令牌加密密钥" },
            new HbtConfig { Id = 44, ConfigName = "JWT过期时间(分钟)", ConfigKey = "Security:Jwt:ExpirationMinutes", ConfigValue = "120", ConfigBuiltin = 1, OrderNum = 119, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "JWT令牌过期时间(分钟)" },

            // 安全配置 - CORS
            new HbtConfig { Id = 45, ConfigName = "启用CORS", ConfigKey = "Security:Cors:Enabled", ConfigValue = "true", ConfigBuiltin = 1, OrderNum = 120, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用跨域资源共享" },
            new HbtConfig { Id = 46, ConfigName = "允许的来源", ConfigKey = "Security:Cors:AllowedOrigins", ConfigValue = "*", ConfigBuiltin = 1, OrderNum = 121, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "允许的跨域来源，多个用逗号分隔，*表示允许所有" },

            // 数据库配置
            new HbtConfig { Id = 47, ConfigName = "数据库类型", ConfigKey = "Database:Type", ConfigValue = "SqlServer", ConfigBuiltin = 1, OrderNum = 130, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库类型(SqlServer/MySql/PostgreSQL/Oracle/Sqlite)" },
            new HbtConfig { Id = 48, ConfigName = "数据库连接字符串", ConfigKey = "Database:ConnectionString", ConfigValue = "JA0UGkq0SN+3ZIyvN+W/NKcRz5hM9QIOEOia3zqQinTBjFEx8hSdAFTTV5Xapr2GZR0UXqlVmoKRfmL/3qgXtEmUCNBShOL/CGJcbZyZ8nPQJVvT7akNJd+J2/D2yY59", ConfigBuiltin = 1, OrderNum = 131, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库连接字符串(已加密)" },
            new HbtConfig { Id = 49, ConfigName = "最大连接池大小", ConfigKey = "Database:MaxPoolSize", ConfigValue = "100", ConfigBuiltin = 1, OrderNum = 132, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库最大连接池大小" },
            new HbtConfig { Id = 50, ConfigName = "最小连接池大小", ConfigKey = "Database:MinPoolSize", ConfigValue = "5", ConfigBuiltin = 1, OrderNum = 133, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库最小连接池大小" },
            new HbtConfig { Id = 51, ConfigName = "连接超时时间", ConfigKey = "Database:ConnectionTimeout", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 134, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库连接超时时间(秒)" },
            new HbtConfig { Id = 52, ConfigName = "命令超时时间", ConfigKey = "Database:CommandTimeout", ConfigValue = "30", ConfigBuiltin = 1, OrderNum = 135, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "数据库命令执行超时时间(秒)" },
            new HbtConfig { Id = 53, ConfigName = "启用读写分离", ConfigKey = "Database:EnableReadWriteSeparation", ConfigValue = "false", ConfigBuiltin = 1, OrderNum = 136, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用读写分离" },
            new HbtConfig { Id = 54, ConfigName = "只读连接字符串", ConfigKey = "Database:ReadOnlyConnectionString", ConfigValue = "", ConfigBuiltin = 1, OrderNum = 137, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "只读数据库连接字符串(读写分离时使用，需加密)" },

            // 验证码配置
            new HbtConfig { Id = 55, ConfigName = "验证码类型", ConfigKey = "Captcha:Type", ConfigValue = "Slider", ConfigBuiltin = 1, OrderNum = 140, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "验证码类型(Slider/Behavior)" },
            new HbtConfig { Id = 56, ConfigName = "滑块验证码宽度", ConfigKey = "Captcha:Slider:Width", ConfigValue = "300", ConfigBuiltin = 2, OrderNum = 141, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "滑块验证码背景图片宽度(像素)" },
            new HbtConfig { Id = 57, ConfigName = "滑块验证码高度", ConfigKey = "Captcha:Slider:Height", ConfigValue = "150", ConfigBuiltin = 2, OrderNum = 142, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "滑块验证码背景图片高度(像素)" },
            new HbtConfig { Id = 58, ConfigName = "滑块宽度", ConfigKey = "Captcha:Slider:SliderWidth", ConfigValue = "50", ConfigBuiltin = 2, OrderNum = 143, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "滑块宽度(像素)" },
            new HbtConfig { Id = 59, ConfigName = "滑块验证容差", ConfigKey = "Captcha:Slider:Tolerance", ConfigValue = "5", ConfigBuiltin = 2, OrderNum = 144, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "滑块验证允许的误差像素" },
            new HbtConfig { Id = 60, ConfigName = "滑块验证过期时间", ConfigKey = "Captcha:Slider:ExpirationMinutes", ConfigValue = "5", ConfigBuiltin = 2, OrderNum = 145, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "滑块验证码的过期时间(分钟)" },
            new HbtConfig { Id = 61, ConfigName = "行为验证分数阈值", ConfigKey = "Captcha:Behavior:ScoreThreshold", ConfigValue = "0.8", ConfigBuiltin = 2, OrderNum = 146, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "行为验证通过的最低分数" },
            new HbtConfig { Id = 62, ConfigName = "行为数据过期时间", ConfigKey = "Captcha:Behavior:DataExpirationMinutes", ConfigValue = "30", ConfigBuiltin = 2, OrderNum = 147, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "行为数据的缓存时间(分钟)" },
            new HbtConfig { Id = 63, ConfigName = "启用机器学习", ConfigKey = "Captcha:Behavior:EnableMachineLearning", ConfigValue = "false", ConfigBuiltin = 3, OrderNum = 148, Status = 0, CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now, Remark = "是否启用机器学习模型进行行为分析" }
        };

        foreach (var config in defaultConfigs)
        {
            var existingConfig = await _configRepository.GetInfoAsync(c => c.ConfigKey == config.ConfigKey);
            if (existingConfig == null)
            {
                await _configRepository.CreateAsync(config);
                insertCount++;
                _logger.Info($"[创建] 配置 '{config.ConfigName}' 创建成功");
            }
            else
            {
                // 更新所有字段
                existingConfig.ConfigName = config.ConfigName;
                existingConfig.ConfigKey = config.ConfigKey;
                existingConfig.ConfigValue = config.ConfigValue;
                existingConfig.ConfigBuiltin = config.ConfigBuiltin;
                existingConfig.OrderNum = config.OrderNum;
                existingConfig.Status = config.Status;
                existingConfig.TenantId = config.TenantId;
                existingConfig.Remark = config.Remark;
                existingConfig.CreateBy = config.CreateBy;
                existingConfig.CreateTime = config.CreateTime;
                existingConfig.UpdateBy = "system";
                existingConfig.UpdateTime = DateTime.Now;

                await _configRepository.UpdateAsync(existingConfig);
                updateCount++;
                _logger.Info($"[更新] 配置 '{existingConfig.ConfigName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}