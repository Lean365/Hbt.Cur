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
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Persistence;

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
}