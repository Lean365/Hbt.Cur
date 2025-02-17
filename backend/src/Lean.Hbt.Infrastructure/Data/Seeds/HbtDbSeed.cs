//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeed.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:35
// 版本号 : V0.0.1
// 描述   : 数据库种子数据初始化
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 数据库种子数据初始化
/// </summary>
public class HbtDbSeed
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly IHbtRepository<HbtSysConfig> _repository;
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly IHbtRepository<HbtLanguage> _languageRepository;
    private readonly IHbtRepository<HbtMenu> _menuRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="repository">系统配置仓储</param>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="menuRepository">菜单仓储</param>
    public HbtDbSeed(HbtDbContext context, IHbtLogger logger, IHbtRepository<HbtSysConfig> repository, 
        IHbtRepository<HbtTranslation> translationRepository, IHbtRepository<HbtLanguage> languageRepository,
        IHbtRepository<HbtMenu> menuRepository)
    {
        _context = context;
        _logger = logger;
        _repository = repository;
        _translationRepository = translationRepository;
        _languageRepository = languageRepository;
        _menuRepository = menuRepository;
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("[初始化] 开始初始化种子数据...");

            // 1.初始化租户
            var tenantCount = await InitializeTenantAsync();
            _logger.Info($"[初始化] 租户数据 - 新增: {tenantCount.Item1}, 更新: {tenantCount.Item2}");

            // 2.初始化角色
            var roleCount = await InitializeRoleAsync();
            _logger.Info($"[初始化] 角色数据 - 新增: {roleCount.Item1}, 更新: {roleCount.Item2}");

            // 3.初始化用户
            var userCount = await InitializeUserAsync();
            _logger.Info($"[初始化] 用户数据 - 新增: {userCount.Item1}, 更新: {userCount.Item2}");

            // 4.初始化部门
            var deptCount = await InitializeDeptAsync();
            _logger.Info($"[初始化] 部门数据 - 新增: {deptCount.Item1}, 更新: {deptCount.Item2}");

            // 5.初始化岗位
            var postCount = await InitializePostAsync();
            _logger.Info($"[初始化] 岗位数据 - 新增: {postCount.Item1}, 更新: {postCount.Item2}");

            // 6.初始化系统配置
            var configCount = await InitializeSysConfigAsync();
            _logger.Info($"[初始化] 系统配置数据 - 新增: {configCount.Item1}, 更新: {configCount.Item2}");

            // 7.初始化验证码配置
            var captchaCount = await InitializeCaptchaConfigAsync();
            _logger.Info($"[初始化] 验证码配置数据 - 新增: {captchaCount.Item1}, 更新: {captchaCount.Item2}");

            // 8.初始化语言配置
            var languageCount = await InitializeLanguageAsync();
            _logger.Info($"[初始化] 语言配置数据 - 新增: {languageCount.Item1}, 更新: {languageCount.Item2}");

            // 9.初始化翻译数据
            var translationCount = await InitializeTranslationAsync();
            _logger.Info($"[初始化] 翻译数据 - 新增: {translationCount.Item1}, 更新: {translationCount.Item2}");

            // 10.初始化菜单数据
            var menuCount = await InitializeMenuAsync();
            _logger.Info($"[初始化] 菜单数据 - 新增: {menuCount.Item1}, 更新: {menuCount.Item2}");

            _logger.Info("[初始化] 种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error("[初始化] 种子数据初始化失败", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化租户
    /// </summary>
    private async Task<(int, int)> InitializeTenantAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtTenant>();
        var defaultTenant = new HbtTenant
        {
            TenantName = "默认租户",
            TenantCode = "default",
            ContactPerson = "管理员",
            ContactPhone = "13800138000",
            ContactEmail = "admin@lean365.com",
            Address = "默认地址",
            Domain = "localhost",
            LogoUrl = "/logo.png",
            DbConnection = "Server=localhost;Database=LeanHbt_Dev;Trusted_Connection=True;MultipleActiveResultSets=true",
            Theme = "default",
            LicenseStartTime = DateTime.Now,
            LicenseEndTime = DateTime.Now.AddYears(1),
            MaxUserCount = 100,
            Status = HbtStatus.Normal,
            CreateTime = DateTime.Now,
            CreateBy = "system"
        };

        if (await repo.AnyAsync())
        {
            // 更新默认租户
            var existingTenant = await repo.FirstAsync();
            existingTenant.TenantName = defaultTenant.TenantName;
            existingTenant.TenantCode = defaultTenant.TenantCode;
            existingTenant.ContactPerson = defaultTenant.ContactPerson;
            existingTenant.ContactPhone = defaultTenant.ContactPhone;
            existingTenant.ContactEmail = defaultTenant.ContactEmail;
            existingTenant.Address = defaultTenant.Address;
            existingTenant.Domain = defaultTenant.Domain;
            existingTenant.DbConnection = defaultTenant.DbConnection;
            existingTenant.LicenseStartTime = defaultTenant.LicenseStartTime;
            existingTenant.LicenseEndTime = defaultTenant.LicenseEndTime;
            existingTenant.MaxUserCount = defaultTenant.MaxUserCount;
            existingTenant.Status = defaultTenant.Status;
            existingTenant.UpdateTime = DateTime.Now;
            existingTenant.UpdateBy = "system";
            await _context.Client.Updateable(existingTenant).ExecuteCommandAsync();
            updateCount++;
            _logger.Info($"[更新] 租户 '{defaultTenant.TenantName}' 更新成功");
        }
        else
        {
            // 创建默认租户
            await _context.Client.Insertable(defaultTenant).ExecuteCommandAsync();
            insertCount++;
            _logger.Info($"[创建] 租户 '{defaultTenant.TenantName}' 创建成功");
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化角色
    /// </summary>
    private async Task<(int, int)> InitializeRoleAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtRole>();
        var adminRole = new HbtRole
        {
            RoleName = "超级管理员",
            RoleKey = "admin",
            Status = HbtStatus.Normal,
            CreateTime = DateTime.Now,
            CreateBy = "system"
        };

        if (await repo.AnyAsync())
        {
            // 更新管理员角色
            var existingRole = await repo.FirstAsync(r => r.RoleKey == adminRole.RoleKey);
            if (existingRole != null)
            {
                existingRole.RoleName = adminRole.RoleName;
                existingRole.Status = adminRole.Status;
                existingRole.UpdateTime = DateTime.Now;
                existingRole.UpdateBy = "system";
                await _context.Client.Updateable(existingRole).ExecuteCommandAsync();
                updateCount++;
                _logger.Info($"[更新] 角色 '{adminRole.RoleName}' 更新成功");
            }
        }
        else
        {
            // 创建管理员角色
            await _context.Client.Insertable(adminRole).ExecuteCommandAsync();
            insertCount++;
            _logger.Info($"[创建] 角色 '{adminRole.RoleName}' 创建成功");
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户
    /// </summary>
    private async Task<(int, int)> InitializeUserAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtUser>();

        // 使用正确的密码哈希方法
        var (hash, salt, iterations) = HbtPasswordUtils.CreateHash("123456");

        var adminUser = new HbtUser
        {
            UserName = "admin",
            NickName = "超级管理员",
            EnglishName = "Administrator",
            UserType = HbtUserType.Admin,
            Password = hash,
            Salt = salt,

            Email = "admin@lean365.com",
            PhoneNumber = "13800138000",
            Gender = HbtGender.Unknown,
            Avatar = "/avatar/default.png",
            Status = HbtStatus.Normal,
            LastPasswordChangeTime = DateTime.Now,
            CreateTime = DateTime.Now,
            CreateBy = "system",
            TenantId = 0  // 使用默认租户ID
        };

        if (await repo.AnyAsync())
        {
            // 更新管理员用户
            var existingUser = await repo.FirstAsync(u => u.UserName == adminUser.UserName);
            if (existingUser != null)
            {
                existingUser.NickName = adminUser.NickName;
                existingUser.EnglishName = adminUser.EnglishName;
                existingUser.UserType = adminUser.UserType;
                existingUser.Email = adminUser.Email;
                existingUser.PhoneNumber = adminUser.PhoneNumber;
                existingUser.Gender = adminUser.Gender;
                existingUser.Avatar = adminUser.Avatar;
                existingUser.Status = adminUser.Status;
                existingUser.UpdateTime = DateTime.Now;
                existingUser.UpdateBy = "system";
                await _context.Client.Updateable(existingUser).ExecuteCommandAsync();
                updateCount++;
                _logger.Info($"[更新] 用户 '{adminUser.UserName}' 更新成功");
            }
        }
        else
        {
            // 创建管理员用户
            await _context.Client.Insertable(adminUser).ExecuteCommandAsync();
            insertCount++;
            _logger.Info($"[创建] 用户 '{adminUser.UserName}' 创建成功");
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化部门
    /// </summary>
    private async Task<(int, int)> InitializeDeptAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtDept>();
        if (await repo.AnyAsync())
            return (0, 0);

        var dept = new HbtDept
        {
            DeptName = "总公司",
            OrderNum = 1,
            Status = HbtStatus.Normal,
            CreateTime = DateTime.Now,
            CreateBy = "system"
        };

        await _context.Client.Insertable(dept).ExecuteCommandAsync();
        insertCount++;
        _logger.Info($"[创建] 部门 '{dept.DeptName}' 创建成功");

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化岗位
    /// </summary>
    private async Task<(int, int)> InitializePostAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtPost>();
        if (await repo.AnyAsync())
            return (0, 0);

        var post = new HbtPost
        {
            PostCode = "CEO",
            PostName = "总经理",
            OrderNum = 1,
            Status = HbtStatus.Normal,
            CreateTime = DateTime.Now,
            CreateBy = "system"
        };

        await _context.Client.Insertable(post).ExecuteCommandAsync();
        insertCount++;
        _logger.Info($"[创建] 岗位 '{post.PostName}' 创建成功");

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化系统配置
    /// </summary>
    private async Task<(int, int)> InitializeSysConfigAsync()
    {
        var insertCount = 0;
        var updateCount = 0;
        var repo = _context.Client.Queryable<HbtSysConfig>();

        var configs = new List<HbtSysConfig>
        {
            // 缓存配置
            new HbtSysConfig
            {
                ConfigName = "缓存提供程序",
                ConfigKey = "Cache:Provider",
                ConfigValue = "Memory",
                ConfigType = 1,
                OrderNum = 50,
                Status = HbtStatus.Normal,
                Remark = "缓存提供程序类型(Memory/Redis)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "默认过期时间(分钟)",
                ConfigKey = "Cache:DefaultExpirationMinutes",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 51,
                Status = HbtStatus.Normal,
                Remark = "缓存默认过期时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "启用滑动过期",
                ConfigKey = "Cache:EnableSlidingExpiration",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 52,
                Status = HbtStatus.Normal,
                Remark = "是否启用滑动过期",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "启用多级缓存",
                ConfigKey = "Cache:EnableMultiLevelCache",
                ConfigValue = "false",
                ConfigType = 1,
                OrderNum = 53,
                Status = HbtStatus.Normal,
                Remark = "是否启用多级缓存",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "内存缓存大小限制",
                ConfigKey = "Cache:Memory:SizeLimit",
                ConfigValue = "104857600",
                ConfigType = 1,
                OrderNum = 54,
                Status = HbtStatus.Normal,
                Remark = "内存缓存大小限制(字节)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "内存缓存压缩阈值",
                ConfigKey = "Cache:Memory:CompactionThreshold",
                ConfigValue = "1048576",
                ConfigType = 1,
                OrderNum = 55,
                Status = HbtStatus.Normal,
                Remark = "内存缓存压缩阈值(字节)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "过期扫描频率",
                ConfigKey = "Cache:Memory:ExpirationScanFrequency",
                ConfigValue = "60",
                ConfigType = 1,
                OrderNum = 56,
                Status = HbtStatus.Normal,
                Remark = "过期扫描频率(秒)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "Redis实例名称",
                ConfigKey = "Cache:Redis:InstanceName",
                ConfigValue = "Lean.Hbt",
                ConfigType = 1,
                OrderNum = 57,
                Status = HbtStatus.Normal,
                Remark = "Redis实例名称",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "Redis数据库",
                ConfigKey = "Cache:Redis:DefaultDatabase",
                ConfigValue = "0",
                ConfigType = 1,
                OrderNum = 58,
                Status = HbtStatus.Normal,
                Remark = "Redis默认数据库编号",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "Redis启用压缩",
                ConfigKey = "Cache:Redis:EnableCompression",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 59,
                Status = HbtStatus.Normal,
                Remark = "是否启用Redis数据压缩",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "Redis压缩阈值",
                ConfigKey = "Cache:Redis:CompressionThreshold",
                ConfigValue = "1024",
                ConfigType = 1,
                OrderNum = 60,
                Status = HbtStatus.Normal,
                Remark = "Redis数据压缩阈值(字节)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // OAuth配置
            new HbtSysConfig
            {
                ConfigName = "OAuth启用状态",
                ConfigKey = "Security:OAuth:Enabled",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 70,
                Status = HbtStatus.Normal,
                Remark = "是否启用OAuth认证",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // GitHub配置
            new HbtSysConfig
            {
                ConfigName = "GitHub客户端ID",
                ConfigKey = "Security:OAuth:Providers:GitHub:ClientId",
                ConfigValue = "",
                ConfigType = 1,
                OrderNum = 71,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth应用的客户端ID",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub客户端密钥",
                ConfigKey = "Security:OAuth:Providers:GitHub:ClientSecret",
                ConfigValue = "",
                ConfigType = 1,
                OrderNum = 72,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth应用的客户端密钥",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub授权端点",
                ConfigKey = "Security:OAuth:Providers:GitHub:AuthorizationEndpoint",
                ConfigValue = "https://github.com/login/oauth/authorize",
                ConfigType = 1,
                OrderNum = 73,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth授权端点URL",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub令牌端点",
                ConfigKey = "Security:OAuth:Providers:GitHub:TokenEndpoint",
                ConfigValue = "https://github.com/login/oauth/access_token",
                ConfigType = 1,
                OrderNum = 74,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth令牌端点URL",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub用户信息端点",
                ConfigKey = "Security:OAuth:Providers:GitHub:UserInfoEndpoint",
                ConfigValue = "https://api.github.com/user",
                ConfigType = 1,
                OrderNum = 75,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth用户信息端点URL",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub回调地址",
                ConfigKey = "Security:OAuth:Providers:GitHub:RedirectUri",
                ConfigValue = "https://localhost:5001/oauth/callback/github",
                ConfigType = 1,
                OrderNum = 76,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth回调地址",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "GitHub权限范围",
                ConfigKey = "Security:OAuth:Providers:GitHub:Scope",
                ConfigValue = "read:user user:email",
                ConfigType = 1,
                OrderNum = 77,
                Status = HbtStatus.Normal,
                Remark = "GitHub OAuth所需权限范围",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // Google配置
            new HbtSysConfig
            {
                ConfigName = "Google客户端ID",
                ConfigKey = "Security:OAuth:Providers:Google:ClientId",
                ConfigValue = "",
                ConfigType = 1,
                OrderNum = 78,
                Status = HbtStatus.Normal,
                Remark = "Google OAuth应用的客户端ID",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "Google客户端密钥",
                ConfigKey = "Security:OAuth:Providers:Google:ClientSecret",
                ConfigValue = "",
                ConfigType = 1,
                OrderNum = 79,
                Status = HbtStatus.Normal,
                Remark = "Google OAuth应用的客户端密钥",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 日志清理配置
            new HbtSysConfig
            {
                ConfigName = "日志清理启用状态",
                ConfigKey = "LogCleanup:Enabled",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 90,
                Status = HbtStatus.Normal,
                Remark = "是否启用日志自动清理",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "日志保留天数",
                ConfigKey = "LogCleanup:RetentionDays",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 91,
                Status = HbtStatus.Normal,
                Remark = "日志保留天数，超过该天数的日志将被清理",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "日志清理执行时间",
                ConfigKey = "LogCleanup:ExecutionTime",
                ConfigValue = "02:00:00",
                ConfigType = 1,
                OrderNum = 92,
                Status = HbtStatus.Normal,
                Remark = "日志清理的执行时间（24小时制）",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "批次清理数量",
                ConfigKey = "LogCleanup:BatchSize",
                ConfigValue = "1000",
                ConfigType = 1,
                OrderNum = 93,
                Status = HbtStatus.Normal,
                Remark = "每次清理的日志数量",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "日志类型",
                ConfigKey = "LogCleanup:LogTypes",
                ConfigValue = "Info,Debug,Warning",
                ConfigType = 1,
                OrderNum = 94,
                Status = HbtStatus.Normal,
                Remark = "需要清理的日志类型，多个类型用逗号分隔",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 日志归档配置
            new HbtSysConfig
            {
                ConfigName = "日志归档启用状态",
                ConfigKey = "LogArchive:Enabled",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 100,
                Status = HbtStatus.Normal,
                Remark = "是否启用日志自动归档",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档触发天数",
                ConfigKey = "LogArchive:TriggerDays",
                ConfigValue = "90",
                ConfigType = 1,
                OrderNum = 101,
                Status = HbtStatus.Normal,
                Remark = "超过多少天的日志将被归档",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档执行时间",
                ConfigKey = "LogArchive:ExecutionTime",
                ConfigValue = "03:00:00",
                ConfigType = 1,
                OrderNum = 102,
                Status = HbtStatus.Normal,
                Remark = "日志归档的执行时间（24小时制）",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档批次大小",
                ConfigKey = "LogArchive:BatchSize",
                ConfigValue = "1000",
                ConfigType = 1,
                OrderNum = 103,
                Status = HbtStatus.Normal,
                Remark = "每次归档的日志数量",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档存储路径",
                ConfigKey = "LogArchive:StoragePath",
                ConfigValue = "Archive/Logs",
                ConfigType = 1,
                OrderNum = 104,
                Status = HbtStatus.Normal,
                Remark = "日志归档文件的存储路径",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档文件格式",
                ConfigKey = "LogArchive:FileFormat",
                ConfigValue = "json",
                ConfigType = 1,
                OrderNum = 105,
                Status = HbtStatus.Normal,
                Remark = "归档文件的格式(json/csv)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档压缩启用",
                ConfigKey = "LogArchive:Compression:Enabled",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 106,
                Status = HbtStatus.Normal,
                Remark = "是否启用归档文件压缩",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "归档压缩格式",
                ConfigKey = "LogArchive:Compression:Format",
                ConfigValue = "gzip",
                ConfigType = 1,
                OrderNum = 107,
                Status = HbtStatus.Normal,
                Remark = "归档文件的压缩格式(gzip/zip)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 安全配置
            new HbtSysConfig
            {
                ConfigName = "密码最小长度",
                ConfigKey = "Security:Password:MinLength",
                ConfigValue = "8",
                ConfigType = 1,
                OrderNum = 110,
                Status = HbtStatus.Normal,
                Remark = "密码最小长度要求",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "密码复杂度要求",
                ConfigKey = "Security:Password:RequireComplexity",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 111,
                Status = HbtStatus.Normal,
                Remark = "是否要求密码包含大小写字母、数字和特殊字符",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "密码过期天数",
                ConfigKey = "Security:Password:ExpirationDays",
                ConfigValue = "90",
                ConfigType = 1,
                OrderNum = 112,
                Status = HbtStatus.Normal,
                Remark = "密码过期天数，0表示永不过期",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "密码历史记录数",
                ConfigKey = "Security:Password:HistoryCount",
                ConfigValue = "3",
                ConfigType = 1,
                OrderNum = 113,
                Status = HbtStatus.Normal,
                Remark = "记住多少个历史密码，防止重复使用",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "登录失败锁定次数",
                ConfigKey = "Security:Lockout:MaxFailedAttempts",
                ConfigValue = "5",
                ConfigType = 1,
                OrderNum = 114,
                Status = HbtStatus.Normal,
                Remark = "允许的最大登录失败次数",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "锁定时间(分钟)",
                ConfigKey = "Security:Lockout:DurationMinutes",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 115,
                Status = HbtStatus.Normal,
                Remark = "账户锁定持续时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "会话超时时间(分钟)",
                ConfigKey = "Security:Session:TimeoutMinutes",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 116,
                Status = HbtStatus.Normal,
                Remark = "用户会话超时时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "允许多端登录",
                ConfigKey = "Security:Session:AllowMultipleLogin",
                ConfigValue = "false",
                ConfigType = 1,
                OrderNum = 117,
                Status = HbtStatus.Normal,
                Remark = "是否允许同一账户多个终端同时登录",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "JWT密钥",
                ConfigKey = "Security:Jwt:SecretKey",
                ConfigValue = "your-secret-key-here",
                ConfigType = 1,
                OrderNum = 118,
                Status = HbtStatus.Normal,
                Remark = "JWT令牌加密密钥",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "JWT过期时间(分钟)",
                ConfigKey = "Security:Jwt:ExpirationMinutes",
                ConfigValue = "120",
                ConfigType = 1,
                OrderNum = 119,
                Status = HbtStatus.Normal,
                Remark = "JWT令牌过期时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "启用CORS",
                ConfigKey = "Security:Cors:Enabled",
                ConfigValue = "true",
                ConfigType = 1,
                OrderNum = 120,
                Status = HbtStatus.Normal,
                Remark = "是否启用跨域资源共享",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "允许的来源",
                ConfigKey = "Security:Cors:AllowedOrigins",
                ConfigValue = "*",
                ConfigType = 1,
                OrderNum = 121,
                Status = HbtStatus.Normal,
                Remark = "允许的跨域来源，多个用逗号分隔，*表示允许所有",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 数据库配置
            new HbtSysConfig
            {
                ConfigName = "数据库类型",
                ConfigKey = "Database:Type",
                ConfigValue = "SqlServer",
                ConfigType = 1,
                OrderNum = 130,
                Status = HbtStatus.Normal,
                Remark = "数据库类型(SqlServer/MySql/PostgreSQL/Oracle/Sqlite)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "数据库连接字符串",
                ConfigKey = "Database:ConnectionString",
                ConfigValue = HbtEncryptUtils.AesEncrypt("Server=localhost;Database=Lean.Hbt;Trusted_Connection=True;MultipleActiveResultSets=true"),
                ConfigType = 1,
                OrderNum = 131,
                Status = HbtStatus.Normal,
                Remark = "数据库连接字符串(已加密)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "最大连接池大小",
                ConfigKey = "Database:MaxPoolSize",
                ConfigValue = "100",
                ConfigType = 1,
                OrderNum = 132,
                Status = HbtStatus.Normal,
                Remark = "数据库最大连接池大小",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "最小连接池大小",
                ConfigKey = "Database:MinPoolSize",
                ConfigValue = "5",
                ConfigType = 1,
                OrderNum = 133,
                Status = HbtStatus.Normal,
                Remark = "数据库最小连接池大小",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "连接超时时间",
                ConfigKey = "Database:ConnectionTimeout",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 134,
                Status = HbtStatus.Normal,
                Remark = "数据库连接超时时间(秒)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "命令超时时间",
                ConfigKey = "Database:CommandTimeout",
                ConfigValue = "30",
                ConfigType = 1,
                OrderNum = 135,
                Status = HbtStatus.Normal,
                Remark = "数据库命令执行超时时间(秒)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "启用读写分离",
                ConfigKey = "Database:EnableReadWriteSeparation",
                ConfigValue = "false",
                ConfigType = 1,
                OrderNum = 136,
                Status = HbtStatus.Normal,
                Remark = "是否启用读写分离",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new HbtSysConfig
            {
                ConfigName = "只读连接字符串",
                ConfigKey = "Database:ReadOnlyConnectionString",
                ConfigValue = "",  // 为空时不启用读写分离
                ConfigType = 1,
                OrderNum = 137,
                Status = HbtStatus.Normal,
                Remark = "只读数据库连接字符串(读写分离时使用，需加密)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            }
        };

        foreach (var config in configs)
        {
            var existingConfig = await repo.FirstAsync(c => c.ConfigKey == config.ConfigKey);
            if (existingConfig != null)
            {
                existingConfig.ConfigName = config.ConfigName;
                existingConfig.ConfigValue = config.ConfigValue;
                existingConfig.ConfigType = config.ConfigType;
                existingConfig.OrderNum = config.OrderNum;
                existingConfig.Status = config.Status;
                existingConfig.Remark = config.Remark;
                existingConfig.UpdateTime = DateTime.Now;
                existingConfig.UpdateBy = "system";
                await _context.Client.Updateable(existingConfig).ExecuteCommandAsync();
                updateCount++;
                _logger.Info($"[更新] 系统配置 '{config.ConfigName}' 更新成功");
            }
            else
            {
                await _context.Client.Insertable(config).ExecuteCommandAsync();
                insertCount++;
                _logger.Info($"[创建] 系统配置 '{config.ConfigName}' 创建成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化验证码配置
    /// </summary>
    private async Task<(int, int)> InitializeCaptchaConfigAsync()
    {
        var insertCount = 0;
        var updateCount = 0;

        var captchaConfigs = new List<HbtSysConfig>
        {
            new()
            {
                ConfigName = "验证码类型",
                ConfigKey = "Captcha:Type",
                ConfigValue = "Slider",
                ConfigType = 1,
                OrderNum = 140,
                Status = HbtStatus.Normal,
                Remark = "验证码类型(Slider/Behavior)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "滑块验证码宽度",
                ConfigKey = "Captcha:Slider:Width",
                ConfigValue = "300",
                ConfigType = 2,
                OrderNum = 141,
                Status = HbtStatus.Normal,
                Remark = "滑块验证码背景图片宽度(像素)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "滑块验证码高度",
                ConfigKey = "Captcha:Slider:Height",
                ConfigValue = "150",
                ConfigType = 2,
                OrderNum = 142,
                Status = HbtStatus.Normal,
                Remark = "滑块验证码背景图片高度(像素)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "滑块宽度",
                ConfigKey = "Captcha:Slider:SliderWidth",
                ConfigValue = "50",
                ConfigType = 2,
                OrderNum = 143,
                Status = HbtStatus.Normal,
                Remark = "滑块宽度(像素)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "滑块验证容差",
                ConfigKey = "Captcha:Slider:Tolerance",
                ConfigValue = "5",
                ConfigType = 2,
                OrderNum = 144,
                Status = HbtStatus.Normal,
                Remark = "滑块验证允许的误差像素",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "滑块验证过期时间",
                ConfigKey = "Captcha:Slider:ExpirationMinutes",
                ConfigValue = "5",
                ConfigType = 2,
                OrderNum = 145,
                Status = HbtStatus.Normal,
                Remark = "滑块验证码的过期时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "行为验证分数阈值",
                ConfigKey = "Captcha:Behavior:ScoreThreshold",
                ConfigValue = "0.8",
                ConfigType = 2,
                OrderNum = 146,
                Status = HbtStatus.Normal,
                Remark = "行为验证通过的最低分数",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "行为数据过期时间",
                ConfigKey = "Captcha:Behavior:DataExpirationMinutes",
                ConfigValue = "30",
                ConfigType = 2,
                OrderNum = 147,
                Status = HbtStatus.Normal,
                Remark = "行为数据的缓存时间(分钟)",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                ConfigName = "启用机器学习",
                ConfigKey = "Captcha:Behavior:EnableMachineLearning",
                ConfigValue = "false",
                ConfigType = 3,
                OrderNum = 148,
                Status = HbtStatus.Normal,
                Remark = "是否启用机器学习模型进行行为分析",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            }
        };

        foreach (var config in captchaConfigs)
        {
            var existingConfig = await _repository.FirstOrDefaultAsync(x => x.ConfigKey == config.ConfigKey);
            if (existingConfig == null)
            {
                await _repository.InsertAsync(config);
                insertCount++;
            }
            else
            {
                existingConfig.ConfigValue = config.ConfigValue;
                existingConfig.Remark = config.Remark;
                existingConfig.UpdateTime = DateTime.Now;
                existingConfig.UpdateBy = "system";
                await _repository.UpdateAsync(existingConfig);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化语言配置
    /// </summary>
    private async Task<(int, int)> InitializeLanguageAsync()
    {
        var insertCount = 0;
        var updateCount = 0;

        var languages = new List<HbtLanguage>
        {
            // 阿拉伯语 (联合国官方语言)
            new()
            {
                LangCode = "ar-SA",
                LangName = "العربية",
                LangIcon = "🇸🇦",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            
            // 英语 (联合国官方语言)
            new()
            {
                LangCode = "en-US",
                LangName = "English",
                LangIcon = "🇺🇸",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            
            // 法语 (联合国官方语言)
            new()
            {
                LangCode = "fr-FR",
                LangName = "Français",
                LangIcon = "🇫🇷",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 日语 (东亚语系)
            new()
            {
                LangCode = "ja-JP",
                LangName = "日本語",
                LangIcon = "🇯🇵",
                OrderNum = 4,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 韩语 (东亚语系)
            new()
            {
                LangCode = "ko-KR",
                LangName = "한국어",
                LangIcon = "🇰🇷",
                OrderNum = 5,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 俄语 (联合国官方语言)
            new()
            {
                LangCode = "ru-RU",
                LangName = "Русский",
                LangIcon = "🇷🇺",
                OrderNum = 6,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 西班牙语 (联合国官方语言)
            new()
            {
                LangCode = "es-ES",
                LangName = "Español",
                LangIcon = "🇪🇸",
                OrderNum = 7,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 简体中文 (默认语言，联合国官方语言)
            new()
            {
                LangCode = "zh-CN",
                LangName = "简体中文",
                LangIcon = "🇨🇳",
                OrderNum = 8,
                Status = HbtStatus.Normal,
                IsDefault = true,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 繁体中文 (东亚语系)
            new()
            {
                LangCode = "zh-TW",
                LangName = "繁體中文",
                LangIcon = "🇹🇼",
                OrderNum = 9,
                Status = HbtStatus.Normal,
                IsDefault = false,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            }
        };

        foreach (var lang in languages)
        {
            var existingLang = await _languageRepository.FirstOrDefaultAsync(x => x.LangCode == lang.LangCode);
            if (existingLang == null)
            {
                await _languageRepository.InsertAsync(lang);
                insertCount++;
                _logger.Info($"[创建] 语言 '{lang.LangName}' 创建成功");
            }
            else
            {
                existingLang.LangName = lang.LangName;
                existingLang.LangIcon = lang.LangIcon;
                existingLang.OrderNum = lang.OrderNum;
                existingLang.Status = lang.Status;
                existingLang.IsDefault = lang.IsDefault;
                existingLang.UpdateTime = DateTime.Now;
                existingLang.UpdateBy = "system";
                await _languageRepository.UpdateAsync(existingLang);
                updateCount++;
                _logger.Info($"[更新] 语言 '{lang.LangName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化翻译数据
    /// </summary>
    private async Task<(int, int)> InitializeTranslationAsync()
    {
        var insertCount = 0;
        var updateCount = 0;

        var translations = new List<HbtTranslation>
        {
            // Common模块 - 中文
            new() { LangCode = "zh-CN", TransKey = "Common.OperationSuccess", TransValue = "操作成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.OperationFailed", TransValue = "操作失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.SaveSuccess", TransValue = "保存成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.SaveFailed", TransValue = "保存失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.DeleteSuccess", TransValue = "删除成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.DeleteFailed", TransValue = "删除失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.UpdateSuccess", TransValue = "更新成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.UpdateFailed", TransValue = "更新失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.QuerySuccess", TransValue = "查询成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.QueryFailed", TransValue = "查询失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.SubmitSuccess", TransValue = "提交成功", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.SubmitFailed", TransValue = "提交失败", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.InvalidRequest", TransValue = "无效的请求", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.DataNotFound", TransValue = "数据不存在", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.DataExists", TransValue = "数据已存在", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Common.ServerError", TransValue = "服务器错误", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            
            // Common模块 - 英文
            new() { LangCode = "en-US", TransKey = "Common.OperationSuccess", TransValue = "Operation successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.OperationFailed", TransValue = "Operation failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.SaveSuccess", TransValue = "Save successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.SaveFailed", TransValue = "Save failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.DeleteSuccess", TransValue = "Delete successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.DeleteFailed", TransValue = "Delete failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.UpdateSuccess", TransValue = "Update successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.UpdateFailed", TransValue = "Update failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.QuerySuccess", TransValue = "Query successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.QueryFailed", TransValue = "Query failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.SubmitSuccess", TransValue = "Submit successful", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.SubmitFailed", TransValue = "Submit failed", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.InvalidRequest", TransValue = "Invalid request", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.DataNotFound", TransValue = "Data not found", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.DataExists", TransValue = "Data already exists", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Common.ServerError", TransValue = "Server error", ModuleName = "Common", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },

            // System模块 - 中文
            new() { LangCode = "zh-CN", TransKey = "System.Login", TransValue = "登录", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.Logout", TransValue = "退出登录", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.Username", TransValue = "用户名", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.Password", TransValue = "密码", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.RememberMe", TransValue = "记住我", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.ForgotPassword", TransValue = "忘记密码", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.LoginFailed", TransValue = "登录失败", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.AccountLocked", TransValue = "账号已锁定", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.AccountDisabled", TransValue = "账号已禁用", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "System.PasswordExpired", TransValue = "密码已过期", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            
            // System模块 - 英文
            new() { LangCode = "en-US", TransKey = "System.Login", TransValue = "Login", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.Logout", TransValue = "Logout", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.Username", TransValue = "Username", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.Password", TransValue = "Password", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.RememberMe", TransValue = "Remember me", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.ForgotPassword", TransValue = "Forgot password", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.LoginFailed", TransValue = "Login failed", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.AccountLocked", TransValue = "Account locked", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.AccountDisabled", TransValue = "Account disabled", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "System.PasswordExpired", TransValue = "Password expired", ModuleName = "System", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },

            // User模块 - 中文
            new() { LangCode = "zh-CN", TransKey = "User.UserManagement", TransValue = "用户管理", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.UserList", TransValue = "用户列表", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.AddUser", TransValue = "添加用户", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.EditUser", TransValue = "编辑用户", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.DeleteUser", TransValue = "删除用户", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.ResetPassword", TransValue = "重置密码", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.AssignRoles", TransValue = "分配角色", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.UserProfile", TransValue = "用户资料", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "User.ChangePassword", TransValue = "修改密码", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            
            // User模块 - 英文
            new() { LangCode = "en-US", TransKey = "User.UserManagement", TransValue = "User Management", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.UserList", TransValue = "User List", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.AddUser", TransValue = "Add User", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.EditUser", TransValue = "Edit User", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.DeleteUser", TransValue = "Delete User", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.ResetPassword", TransValue = "Reset Password", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.AssignRoles", TransValue = "Assign Roles", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.UserProfile", TransValue = "User Profile", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "User.ChangePassword", TransValue = "Change Password", ModuleName = "User", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },

            // Role模块 - 中文
            new() { LangCode = "zh-CN", TransKey = "Role.RoleManagement", TransValue = "角色管理", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Role.RoleList", TransValue = "角色列表", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Role.AddRole", TransValue = "添加角色", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Role.EditRole", TransValue = "编辑角色", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Role.DeleteRole", TransValue = "删除角色", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "zh-CN", TransKey = "Role.AssignPermissions", TransValue = "分配权限", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            
            // Role模块 - 英文
            new() { LangCode = "en-US", TransKey = "Role.RoleManagement", TransValue = "Role Management", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Role.RoleList", TransValue = "Role List", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Role.AddRole", TransValue = "Add Role", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Role.EditRole", TransValue = "Edit Role", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Role.DeleteRole", TransValue = "Delete Role", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },
            new() { LangCode = "en-US", TransKey = "Role.AssignPermissions", TransValue = "Assign Permissions", ModuleName = "Role", Status = HbtStatus.Normal, CreateTime = DateTime.Now, CreateBy = "system" },

            // Post模块 - 中文
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Management", TransValue = "岗位管理", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.List", TransValue = "岗位列表", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Code", TransValue = "岗位编码", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Name", TransValue = "岗位名称", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.OrderNum", TransValue = "显示顺序", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Status", TransValue = "状态", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Remark", TransValue = "备注", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.AddSuccess", TransValue = "添加岗位成功", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.UpdateSuccess", TransValue = "更新岗位成功", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.DeleteSuccess", TransValue = "删除岗位成功", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.NotExists", TransValue = "岗位不存在", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.CodeExists", TransValue = "岗位编码已存在", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.NameExists", TransValue = "岗位名称已存在", ModuleName = "Post", Status = HbtStatus.Normal },

            // Post模块 - 英文
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Management", TransValue = "Post Management", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.List", TransValue = "Post List", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Code", TransValue = "Post Code", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Name", TransValue = "Post Name", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.OrderNum", TransValue = "Display Order", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Status", TransValue = "Status", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Remark", TransValue = "Remark", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.AddSuccess", TransValue = "Add post successfully", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.UpdateSuccess", TransValue = "Update post successfully", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.DeleteSuccess", TransValue = "Delete post successfully", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.NotExists", TransValue = "Post does not exist", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.CodeExists", TransValue = "Post code already exists", ModuleName = "Post", Status = HbtStatus.Normal },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.NameExists", TransValue = "Post name already exists", ModuleName = "Post", Status = HbtStatus.Normal }
        };

        foreach (var trans in translations)
        {
            var existingTrans = await _translationRepository.FirstOrDefaultAsync(x => 
                x.LangCode == trans.LangCode && 
                x.TransKey == trans.TransKey);
            
            if (existingTrans == null)
            {
                await _translationRepository.InsertAsync(trans);
                insertCount++;
                _logger.Info($"[创建] 翻译 '{trans.TransKey}' ({trans.LangCode}) 创建成功");
            }
            else
            {
                existingTrans.TransValue = trans.TransValue;
                existingTrans.ModuleName = trans.ModuleName;
                existingTrans.Status = trans.Status;
                existingTrans.UpdateTime = DateTime.Now;
                existingTrans.UpdateBy = "system";
                await _translationRepository.UpdateAsync(existingTrans);
                updateCount++;
                _logger.Info($"[更新] 翻译 '{trans.TransKey}' ({trans.LangCode}) 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化菜单数据
    /// </summary>
    private async Task<(int, int)> InitializeMenuAsync()
    {
        var insertCount = 0;
        var updateCount = 0;

        var menus = new List<HbtMenu>
        {
            // 系统管理
            new()
            {
                MenuName = "系统管理",
                ParentId = 0,
                OrderNum = 1,
                Path = "system",
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 目录不需要缓存
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system",
                Icon = "system",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 系统管理子菜单
            new()
            {
                MenuName = "系统配置",
                ParentId = 1,
                OrderNum = 1,
                Path = "config",
                Component = "system/config/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.Yes, // 菜单需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system:config:list",
                Icon = "config",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "语言管理",
                ParentId = 1,
                OrderNum = 2,
                Path = "language",
                Component = "system/language/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system:language:list",
                Icon = "language",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "翻译管理",
                ParentId = 1,
                OrderNum = 3,
                Path = "translation",
                Component = "system/translation/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system:translation:list",
                Icon = "translation",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "字典类型",
                ParentId = 1,
                OrderNum = 4,
                Path = "dict-type",
                Component = "system/dict-type/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system:dict-type:list",
                Icon = "dict-type",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "数据字典",
                ParentId = 1,
                OrderNum = 5,
                Path = "dict-data",
                Component = "system/dict-data/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "system:dict-data:list",
                Icon = "dict-data",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 认证管理
            new()
            {
                MenuName = "认证管理",
                ParentId = 0,
                OrderNum = 2,
                Path = "auth",
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 目录不需要缓存
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth",
                Icon = "auth",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 认证管理子菜单
            new()
            {
                MenuName = "租户管理",
                ParentId = 7,
                OrderNum = 1,
                Path = "tenant",
                Component = "auth/tenant/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:tenant:list",
                Icon = "tenant",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "用户管理",
                ParentId = 7,
                OrderNum = 2,
                Path = "user",
                Component = "auth/user/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:user:list",
                Icon = "user",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "菜单管理",
                ParentId = 7,
                OrderNum = 3,
                Path = "menu",
                Component = "auth/menu/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:menu:list",
                Icon = "menu",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "部门管理",
                ParentId = 7,
                OrderNum = 4,
                Path = "dept",
                Component = "auth/dept/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:dept:list",
                Icon = "dept",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "角色管理",
                ParentId = 7,
                OrderNum = 5,
                Path = "role",
                Component = "auth/role/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:role:list",
                Icon = "role",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "岗位管理",
                ParentId = 7,
                OrderNum = 6,
                Path = "post",
                Component = "auth/post/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "auth:post:list",
                Icon = "post",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 系统监控
            new()
            {
                MenuName = "系统监控",
                ParentId = 0,
                OrderNum = 3,
                Path = "monitor",
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 目录不需要缓存
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor",
                Icon = "monitor",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 系统监控子菜单
            new()
            {
                MenuName = "登录日志",
                ParentId = 13,
                OrderNum = 1,
                Path = "loginlog",
                Component = "monitor/loginlog/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:loginlog:list",
                Icon = "loginlog",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "审计日志",
                ParentId = 13,
                OrderNum = 2,
                Path = "auditlog",
                Component = "monitor/auditlog/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:auditlog:list",
                Icon = "auditlog",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "操作日志",
                ParentId = 13,
                OrderNum = 3,
                Path = "operlog",
                Component = "monitor/operlog/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:operlog:list",
                Icon = "operlog",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "差异日志",
                ParentId = 13,
                OrderNum = 4,
                Path = "difflog",
                Component = "monitor/difflog/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:difflog:list",
                Icon = "difflog",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "异常日志",
                ParentId = 13,
                OrderNum = 5,
                Path = "exceptionlog",
                Component = "monitor/exceptionlog/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:exceptionlog:list",
                Icon = "exceptionlog",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "服务监控",
                ParentId = 13,
                OrderNum = 6,
                Path = "server",
                Component = "monitor/server/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "monitor:server:list",
                Icon = "server",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },

            // 实时在线
            new()
            {
                MenuName = "实时在线",
                ParentId = 0,
                OrderNum = 4,
                Path = "online",
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 目录不需要缓存
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "online",
                Icon = "online",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            // 实时在线子菜单
            new()
            {
                MenuName = "在线用户",
                ParentId = 19,
                OrderNum = 1,
                Path = "user",
                Component = "online/user/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "online:user:list",
                Icon = "online-user",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            },
            new()
            {
                MenuName = "在线消息",
                ParentId = 19,
                OrderNum = 2,
                Path = "message",
                Component = "online/message/index",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No, // 按钮不需要缓存
                MenuType = HbtMenuType.Menu,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "online:message:list",
                Icon = "online-message",
                CreateTime = DateTime.Now,
                CreateBy = "system"
            }
        };

        // 先插入目录和菜单
        foreach (var menu in menus)
        {
            var existingMenu = await _menuRepository.FirstOrDefaultAsync(x => 
                x.MenuName == menu.MenuName && 
                x.ParentId == menu.ParentId);
            
            if (existingMenu == null)
            {
                await _menuRepository.InsertAsync(menu);
                insertCount++;
                _logger.Info($"[创建] 菜单 '{menu.MenuName}' 创建成功");
            }
            else
            {
                existingMenu.Path = menu.Path;
                existingMenu.Component = menu.Component;
                existingMenu.QueryParams = menu.QueryParams;
                existingMenu.IsFrame = menu.IsFrame;
                existingMenu.IsCache = menu.IsCache;
                existingMenu.MenuType = menu.MenuType;
                existingMenu.Visible = menu.Visible;
                existingMenu.Status = menu.Status;
                existingMenu.Perms = menu.Perms;
                existingMenu.Icon = menu.Icon;
                existingMenu.OrderNum = menu.OrderNum;
                existingMenu.UpdateTime = DateTime.Now;
                existingMenu.UpdateBy = "system";
                await _menuRepository.UpdateAsync(existingMenu);
                updateCount++;
                _logger.Info($"[更新] 菜单 '{menu.MenuName}' 更新成功");
            }
        }

        // 为每个菜单添加按钮
        foreach (var menu in menus.Where(m => m.MenuType == HbtMenuType.Menu))
        {
            // 获取实际的菜单ID
            var parentMenu = await _menuRepository.FirstOrDefaultAsync(x => 
                x.MenuName == menu.MenuName && 
                x.MenuType == HbtMenuType.Menu);
            
            if (parentMenu == null)
            {
                _logger.Error($"未找到菜单 '{menu.MenuName}'，无法创建按钮");
                continue;
            }

            var menuButtons = new List<HbtMenu>();

            // 查询按钮
            menuButtons.Add(new HbtMenu
            {
                MenuName = $"{menu.MenuName}查询",
                ParentId = parentMenu.Id,
                OrderNum = 1,
                Path = null,
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Button,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = $"{menu.Perms}:query",
                Icon = null,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            });

            // 新增按钮
            menuButtons.Add(new HbtMenu
            {
                MenuName = $"{menu.MenuName}新增",
                ParentId = parentMenu.Id,
                OrderNum = 2,
                Path = null,
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Button,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = $"{menu.Perms}:add",
                Icon = null,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            });

            // 修改按钮
            menuButtons.Add(new HbtMenu
            {
                MenuName = $"{menu.MenuName}修改",
                ParentId = parentMenu.Id,
                OrderNum = 3,
                Path = null,
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Button,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = $"{menu.Perms}:edit",
                Icon = null,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            });

            // 删除按钮
            menuButtons.Add(new HbtMenu
            {
                MenuName = $"{menu.MenuName}删除",
                ParentId = parentMenu.Id,
                OrderNum = 4,
                Path = null,
                Component = null,
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Button,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = $"{menu.Perms}:remove",
                Icon = null,
                CreateTime = DateTime.Now,
                CreateBy = "system"
            });

            // 导出按钮 (排除服务监控和在线消息)
            if (menu.MenuName != "服务监控" && menu.MenuName != "在线消息")
            {
                menuButtons.Add(new HbtMenu
                {
                    MenuName = $"{menu.MenuName}导出",
                    ParentId = parentMenu.Id,
                    OrderNum = 5,
                    Path = null,
                    Component = null,
                    QueryParams = null,
                    IsFrame = HbtYesNo.No,
                    IsCache = HbtYesNo.No,
                    MenuType = HbtMenuType.Button,
                    Visible = HbtVisible.Show,
                    Status = HbtStatus.Normal,
                    Perms = $"{menu.Perms}:export",
                    Icon = null,
                    CreateTime = DateTime.Now,
                    CreateBy = "system"
                });
            }

            // 导入按钮 (仅适用于部分菜单)
            if (new[] { "租户管理", "用户管理", "部门管理", "岗位管理", "字典类型", "数据字典" }.Contains(menu.MenuName))
            {
                menuButtons.Add(new HbtMenu
                {
                    MenuName = $"{menu.MenuName}导入",
                    ParentId = parentMenu.Id,
                    OrderNum = 6,
                    Path = null,
                    Component = null,
                    QueryParams = null,
                    IsFrame = HbtYesNo.No,
                    IsCache = HbtYesNo.No,
                    MenuType = HbtMenuType.Button,
                    Visible = HbtVisible.Show,
                    Status = HbtStatus.Normal,
                    Perms = $"{menu.Perms}:import",
                    Icon = null,
                    CreateTime = DateTime.Now,
                    CreateBy = "system"
                });
            }

            // 插入按钮
            foreach (var button in menuButtons)
            {
                var existingButton = await _menuRepository.FirstOrDefaultAsync(x => 
                    x.MenuName == button.MenuName && 
                    x.ParentId == button.ParentId);
                
                if (existingButton == null)
                {
                    await _menuRepository.InsertAsync(button);
                    insertCount++;
                    _logger.Info($"[创建] 按钮 '{button.MenuName}' 创建成功");
                }
                else
                {
                    existingButton.Perms = button.Perms;
                    existingButton.OrderNum = button.OrderNum;
                    existingButton.UpdateTime = DateTime.Now;
                    existingButton.UpdateBy = "system";
                    await _menuRepository.UpdateAsync(existingButton);
                    updateCount++;
                    _logger.Info($"[更新] 按钮 '{button.MenuName}' 更新成功");
                }
            }
        }

        return (insertCount, updateCount);
    }
}