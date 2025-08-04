//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedConfig.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 系统配置种子数据初始化类 - 使用仓储工厂模式
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;
using Hbt.Cur.Domain.Repositories;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 系统配置种子数据初始化类
/// </summary>
public class HbtDbSeedConfig
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtConfig> ConfigRepository => _repositoryFactory.GetBusinessRepository<HbtConfig>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedConfig(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
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
            new HbtConfig { ConfigName = "缓存提供程序", ConfigKey = "Cache:Provider", ConfigValue = "Memory", IsBuiltin = 1, OrderNum = 50, Status = 0, Remark = "缓存提供程序类型(Memory/Redis)" },
            new HbtConfig { ConfigName = "默认过期时间(分钟)", ConfigKey = "Cache:DefaultExpirationMinutes", ConfigValue = "30", IsBuiltin = 1, OrderNum = 51, Status = 0, Remark = "缓存默认过期时间(分钟)" },
            new HbtConfig { ConfigName = "启用滑动过期", ConfigKey = "Cache:EnableSlidingExpiration", ConfigValue = "true", IsBuiltin = 1, OrderNum = 52, Status = 0, Remark = "是否启用滑动过期" },
            new HbtConfig { ConfigName = "启用多级缓存", ConfigKey = "Cache:EnableMultiLevelCache", ConfigValue = "false", IsBuiltin = 1, OrderNum = 53, Status = 0, Remark = "是否启用多级缓存" },
            new HbtConfig { ConfigName = "内存缓存大小限制", ConfigKey = "Cache:Memory:SizeLimit", ConfigValue = "104857600", IsBuiltin = 1, OrderNum = 54, Status = 0, Remark = "内存缓存大小限制(字节)" },
            new HbtConfig { ConfigName = "内存缓存压缩阈值", ConfigKey = "Cache:Memory:CompactionThreshold", ConfigValue = "1048576", IsBuiltin = 1, OrderNum = 55, Status = 0, Remark = "内存缓存压缩阈值(字节)" },
            new HbtConfig { ConfigName = "过期扫描频率", ConfigKey = "Cache:Memory:ExpirationScanFrequency", ConfigValue = "60", IsBuiltin = 1, OrderNum = 56, Status = 0, Remark = "过期扫描频率(秒)" },
            new HbtConfig { ConfigName = "Redis实例名称", ConfigKey = "Cache:Redis:InstanceName", ConfigValue = "Lean.Hbt", IsBuiltin = 1, OrderNum = 57, Status = 0, Remark = "Redis实例名称" },
            new HbtConfig { ConfigName = "Redis数据库", ConfigKey = "Cache:Redis:DefaultDatabase", ConfigValue = "0", IsBuiltin = 1, OrderNum = 58, Status = 0, Remark = "Redis默认数据库编号" },
            new HbtConfig { ConfigName = "Redis启用压缩", ConfigKey = "Cache:Redis:EnableCompression", ConfigValue = "true", IsBuiltin = 1, OrderNum = 59, Status = 0, Remark = "是否启用Redis数据压缩" },
            new HbtConfig { ConfigName = "Redis压缩阈值", ConfigKey = "Cache:Redis:CompressionThreshold", ConfigValue = "1024", IsBuiltin = 1, OrderNum = 60, Status = 0, Remark = "Redis数据压缩阈值(字节)" },

            // OAuth配置
            new HbtConfig { ConfigName = "OAuth启用状态", ConfigKey = "Security:Oidentity:Enabled", ConfigValue = "true", IsBuiltin = 1, OrderNum = 70, Status = 0, Remark = "是否启用OAuth认证" },
            new HbtConfig { ConfigName = "GitHub客户端ID", ConfigKey = "Security:Oidentity:Providers:GitHub:ClientId", ConfigValue = "", IsBuiltin = 1, OrderNum = 71, Status = 0, Remark = "GitHub OAuth应用的客户端ID" },
            new HbtConfig { ConfigName = "GitHub客户端密钥", ConfigKey = "Security:Oidentity:Providers:GitHub:ClientSecret", ConfigValue = "", IsBuiltin = 1, OrderNum = 72, Status = 0, Remark = "GitHub OAuth应用的客户端密钥" },
            new HbtConfig { ConfigName = "GitHub授权端点", ConfigKey = "Security:Oidentity:Providers:GitHub:AuthorizationEndpoint", ConfigValue = "https://github.com/login/oidentity/authorize", IsBuiltin = 1, OrderNum = 73, Status = 0, Remark = "GitHub OAuth授权端点URL" },
            new HbtConfig { ConfigName = "GitHub令牌端点", ConfigKey = "Security:Oidentity:Providers:GitHub:TokenEndpoint", ConfigValue = "https://github.com/login/oidentity/access_token", IsBuiltin = 1, OrderNum = 74, Status = 0, Remark = "GitHub OAuth令牌端点URL" },
            new HbtConfig { ConfigName = "GitHub用户信息端点", ConfigKey = "Security:Oidentity:Providers:GitHub:UserInfoEndpoint", ConfigValue = "https://api.github.com/user", IsBuiltin = 1, OrderNum = 75, Status = 0, Remark = "GitHub OAuth用户信息端点URL" },
            new HbtConfig { ConfigName = "GitHub回调地址", ConfigKey = "Security:Oidentity:Providers:GitHub:RedirectUri", ConfigValue = "https://localhost:5001/oidentity/callback/github", IsBuiltin = 1, OrderNum = 76, Status = 0, Remark = "GitHub OAuth回调地址" },
            new HbtConfig { ConfigName = "GitHub权限范围", ConfigKey = "Security:Oidentity:Providers:GitHub:Scope", ConfigValue = "read:user user:email", IsBuiltin = 1, OrderNum = 77, Status = 0, Remark = "GitHub OAuth所需权限范围" },

            // 日志清理配置
            new HbtConfig { ConfigName = "日志清理启用状态", ConfigKey = "LogCleanup:Enabled", ConfigValue = "true", IsBuiltin = 1, OrderNum = 90, Status = 0, Remark = "是否启用日志自动清理" },
            new HbtConfig { ConfigName = "日志保留天数", ConfigKey = "LogCleanup:RetentionDays", ConfigValue = "30", IsBuiltin = 1, OrderNum = 91, Status = 0, Remark = "日志保留天数，超过该天数的日志将被清理" },
            new HbtConfig { ConfigName = "日志清理执行时间", ConfigKey = "LogCleanup:ExecutionTime", ConfigValue = "02:00:00", IsBuiltin = 1, OrderNum = 92, Status = 0, Remark = "日志清理的执行时间（24小时制）" },
            new HbtConfig { ConfigName = "批次清理数量", ConfigKey = "LogCleanup:BatchSize", ConfigValue = "1000", IsBuiltin = 1, OrderNum = 93, Status = 0, Remark = "每次清理的日志数量" },
            new HbtConfig { ConfigName = "日志类型", ConfigKey = "LogCleanup:LogTypes", ConfigValue = "Info,Debug,Warning", IsBuiltin = 1, OrderNum = 94, Status = 0, Remark = "需要清理的日志类型，多个类型用逗号分隔" },

            // 日志归档配置
            new HbtConfig { ConfigName = "日志归档启用状态", ConfigKey = "LogArchive:Enabled", ConfigValue = "true", IsBuiltin = 1, OrderNum = 100, Status = 0, Remark = "是否启用日志自动归档" },
            new HbtConfig { ConfigName = "归档触发天数", ConfigKey = "LogArchive:TriggerDays", ConfigValue = "90", IsBuiltin = 1, OrderNum = 101, Status = 0, Remark = "超过多少天的日志将被归档" },
            new HbtConfig { ConfigName = "归档执行时间", ConfigKey = "LogArchive:ExecutionTime", ConfigValue = "03:00:00", IsBuiltin = 1, OrderNum = 102, Status = 0, Remark = "日志归档的执行时间（24小时制）" },
            new HbtConfig { ConfigName = "归档批次大小", ConfigKey = "LogArchive:BatchSize", ConfigValue = "1000", IsBuiltin = 1, OrderNum = 103, Status = 0, Remark = "每次归档的日志数量" },
            new HbtConfig { ConfigName = "归档存储路径", ConfigKey = "LogArchive:StoragePath", ConfigValue = "Archive/Logs", IsBuiltin = 1, OrderNum = 104, Status = 0, Remark = "日志归档文件的存储路径" },
            new HbtConfig { ConfigName = "归档文件格式", ConfigKey = "LogArchive:FileFormat", ConfigValue = "json", IsBuiltin = 1, OrderNum = 105, Status = 0, Remark = "归档文件的格式(json/csv)" },
            new HbtConfig { ConfigName = "归档压缩启用", ConfigKey = "LogArchive:Compression:Enabled", ConfigValue = "true", IsBuiltin = 1, OrderNum = 106, Status = 0, Remark = "是否启用归档文件压缩" },
            new HbtConfig { ConfigName = "归档压缩格式", ConfigKey = "LogArchive:Compression:Format", ConfigValue = "gzip", IsBuiltin = 1, OrderNum = 107, Status = 0, Remark = "归档文件的压缩格式(gzip/zip)" },

            // 安全配置 - 密码策略
            new HbtConfig { ConfigName = "密码最小长度", ConfigKey = "Security:Password:MinLength", ConfigValue = "8", IsBuiltin = 1, OrderNum = 110, Status = 0, Remark = "密码最小长度要求" },
            new HbtConfig { ConfigName = "密码复杂度要求", ConfigKey = "Security:Password:RequireComplexity", ConfigValue = "true", IsBuiltin = 1, OrderNum = 111, Status = 0, Remark = "是否要求密码包含大小写字母、数字和特殊字符" },
            new HbtConfig { ConfigName = "密码过期天数", ConfigKey = "Security:Password:ExpirationDays", ConfigValue = "90", IsBuiltin = 1, OrderNum = 112, Status = 0, Remark = "密码过期天数，0表示永不过期" },
            new HbtConfig { ConfigName = "密码历史记录数", ConfigKey = "Security:Password:HistoryCount", ConfigValue = "3", IsBuiltin = 1, OrderNum = 113, Status = 0, Remark = "记住多少个历史密码，防止重复使用" },

            // 安全配置 - 登录锁定
            new HbtConfig { ConfigName = "登录失败锁定次数", ConfigKey = "Security:Lockout:MaxFailedAttempts", ConfigValue = "5", IsBuiltin = 1, OrderNum = 114, Status = 0, Remark = "允许的最大登录失败次数" },
            new HbtConfig { ConfigName = "锁定时间(分钟)", ConfigKey = "Security:Lockout:DurationMinutes", ConfigValue = "30", IsBuiltin = 1, OrderNum = 115, Status = 0, Remark = "账户锁定持续时间(分钟)" },

            // 安全配置 - 会话管理
            new HbtConfig { ConfigName = "会话超时时间(分钟)", ConfigKey = "Security:Session:TimeoutMinutes", ConfigValue = "30", IsBuiltin = 1, OrderNum = 116, Status = 0, Remark = "用户会话超时时间(分钟)" },
            new HbtConfig { ConfigName = "允许多端登录", ConfigKey = "Security:Session:AllowMultipleLogin", ConfigValue = "false", IsBuiltin = 1, OrderNum = 117, Status = 0, Remark = "是否允许同一账户多个终端同时登录" },

            // 安全配置 - JWT
            new HbtConfig { ConfigName = "JWT密钥", ConfigKey = "Security:Jwt:SecretKey", ConfigValue = "your-secret-key-here", IsBuiltin = 1, OrderNum = 118, Status = 0, Remark = "JWT令牌加密密钥" },
            new HbtConfig { ConfigName = "JWT过期时间(分钟)", ConfigKey = "Security:Jwt:ExpirationMinutes", ConfigValue = "120", IsBuiltin = 1, OrderNum = 119, Status = 0, Remark = "JWT令牌过期时间(分钟)" },

            // 安全配置 - CORS
            new HbtConfig { ConfigName = "启用CORS", ConfigKey = "Security:Cors:Enabled", ConfigValue = "true", IsBuiltin = 1, OrderNum = 120, Status = 0, Remark = "是否启用跨域资源共享" },
            new HbtConfig { ConfigName = "允许的来源", ConfigKey = "Security:Cors:AllowedOrigins", ConfigValue = "*", IsBuiltin = 1, OrderNum = 121, Status = 0, Remark = "允许的跨域来源，多个用逗号分隔，*表示允许所有" },

            // 数据库配置
            new HbtConfig { ConfigName = "数据库类型", ConfigKey = "Database:Type", ConfigValue = "SqlServer", IsBuiltin = 1, OrderNum = 130, Status = 0, Remark = "数据库类型(SqlServer/MySql/PostgreSQL/Oracle/Sqlite)" },
            new HbtConfig { ConfigName = "数据库连接字符串", ConfigKey = "Database:ConnectionString", ConfigValue = "JA0UGkq0SN+3ZIyvN+W/NKcRz5hM9QIOEOia3zqQinTBjFEx8hSdAFTTV5Xapr2GZR0UXqlVmoKRfmL/3qgXtEmUCNBShOL/CGJcbZyZ8nPQJVvT7akNJd+J2/D2yY59", IsBuiltin = 1, OrderNum = 131, Status = 0, Remark = "数据库连接字符串(已加密)" },
            new HbtConfig { ConfigName = "最大连接池大小", ConfigKey = "Database:MaxPoolSize", ConfigValue = "100", IsBuiltin = 1, OrderNum = 132, Status = 0, Remark = "数据库最大连接池大小" },
            new HbtConfig { ConfigName = "最小连接池大小", ConfigKey = "Database:MinPoolSize", ConfigValue = "5", IsBuiltin = 1, OrderNum = 133, Status = 0, Remark = "数据库最小连接池大小" },
            new HbtConfig { ConfigName = "连接超时时间", ConfigKey = "Database:ConnectionTimeout", ConfigValue = "30", IsBuiltin = 1, OrderNum = 134, Status = 0, Remark = "数据库连接超时时间(秒)" },
            new HbtConfig { ConfigName = "命令超时时间", ConfigKey = "Database:CommandTimeout", ConfigValue = "30", IsBuiltin = 1, OrderNum = 135, Status = 0, Remark = "数据库命令执行超时时间(秒)" },
            new HbtConfig { ConfigName = "启用读写分离", ConfigKey = "Database:EnableReadWriteSeparation", ConfigValue = "false", IsBuiltin = 1, OrderNum = 136, Status = 0, Remark = "是否启用读写分离" },
            new HbtConfig { ConfigName = "只读连接字符串", ConfigKey = "Database:ReadOnlyConnectionString", ConfigValue = "", IsBuiltin = 1, OrderNum = 137, Status = 0, Remark = "只读数据库连接字符串(读写分离时使用，需加密)" },

            // 验证码配置
            new HbtConfig { ConfigName = "验证码类型", ConfigKey = "Captcha:Type", ConfigValue = "Slider", IsBuiltin = 1, OrderNum = 140, Status = 0, Remark = "验证码类型(Slider/Behavior)" },
            new HbtConfig { ConfigName = "滑块验证码宽度", ConfigKey = "Captcha:Slider:Width", ConfigValue = "300", IsBuiltin = 2, OrderNum = 141, Status = 0, Remark = "滑块验证码背景图片宽度(像素)" },
            new HbtConfig { ConfigName = "滑块验证码高度", ConfigKey = "Captcha:Slider:Height", ConfigValue = "150", IsBuiltin = 2, OrderNum = 142, Status = 0, Remark = "滑块验证码背景图片高度(像素)" },
            new HbtConfig { ConfigName = "滑块宽度", ConfigKey = "Captcha:Slider:SliderWidth", ConfigValue = "50", IsBuiltin = 2, OrderNum = 143, Status = 0, Remark = "滑块宽度(像素)" },
            new HbtConfig { ConfigName = "滑块验证容差", ConfigKey = "Captcha:Slider:Tolerance", ConfigValue = "5", IsBuiltin = 2, OrderNum = 144, Status = 0, Remark = "滑块验证允许的误差像素" },
            new HbtConfig { ConfigName = "滑块验证过期时间", ConfigKey = "Captcha:Slider:ExpirationMinutes", ConfigValue = "5", IsBuiltin = 2, OrderNum = 145, Status = 0, Remark = "滑块验证码的过期时间(分钟)" },
            new HbtConfig { ConfigName = "行为验证分数阈值", ConfigKey = "Captcha:Behavior:ScoreThreshold", ConfigValue = "0.8", IsBuiltin = 2, OrderNum = 146, Status = 0, Remark = "行为验证通过的最低分数" },
            new HbtConfig { ConfigName = "行为数据过期时间", ConfigKey = "Captcha:Behavior:DataExpirationMinutes", ConfigValue = "30", IsBuiltin = 2, OrderNum = 147, Status = 0, Remark = "行为数据的缓存时间(分钟)" },
            new HbtConfig { ConfigName = "启用机器学习", ConfigKey = "Captcha:Behavior:EnableMachineLearning", ConfigValue = "false", IsBuiltin = 3, OrderNum = 148, Status = 0, Remark = "是否启用机器学习模型进行行为分析" }
        };

        foreach (var config in defaultConfigs)
        {
            var existingConfig = await ConfigRepository.GetFirstAsync(c => c.ConfigKey == config.ConfigKey);
            if (existingConfig == null)
            {
                // 统一处理审计字段和租户
                config.CreateBy = "Hbt365";
                config.CreateTime = DateTime.Now;
                config.UpdateBy = "Hbt365";
                config.UpdateTime = DateTime.Now;

                await ConfigRepository.CreateAsync(config);
                insertCount++;
                _logger.Info($"[创建] 配置 '{config.ConfigName}' 创建成功");
            }
            else
            {
                existingConfig.ConfigName = config.ConfigName;
                existingConfig.ConfigKey = config.ConfigKey;
                existingConfig.ConfigValue = config.ConfigValue;
                existingConfig.IsBuiltin = config.IsBuiltin;
                existingConfig.OrderNum = config.OrderNum;
                existingConfig.Status = config.Status;
                existingConfig.Remark = config.Remark;
                existingConfig.UpdateBy = "Hbt365";
                existingConfig.UpdateTime = DateTime.Now;

                await ConfigRepository.UpdateAsync(existingConfig);
                updateCount++;
                _logger.Info($"[更新] 配置 '{existingConfig.ConfigName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}