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

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 数据库种子数据初始化
/// </summary>
public class HbtDbSeed
{
    private readonly HbtDbContext _context;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeed(HbtDbContext context, IHbtLogger logger)
    {
        _context = context;
        _logger = logger;
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
}