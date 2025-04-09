//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedUser.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 用户数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Common.Utils;
using Microsoft.Extensions.Configuration;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 用户数据初始化类
/// </summary>
public class HbtDbSeedUser
{
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtLogger _logger;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="configuration">配置</param>
    public HbtDbSeedUser(
        IHbtRepository<HbtUser> userRepository, 
        IHbtLogger logger,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    public async Task<(int, int)> InitializeUserAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 从配置中获取默认密码和错误限制
        var defaultPassword = _configuration.GetValue<string>("Security:PasswordPolicy:DefaultPassword")!;
        var defaultErrorLimit = _configuration.GetValue<int>("Security:PasswordPolicy:DefaultErrorLimit");

        var defaultUsers = new List<HbtUser>
        {
            // 系统管理员
            new HbtUser
            {
                Id = nextId++,
                UserName = "admin",
                NickName = "系统管理员",
                EnglishName = "System Administrator",
                UserType = 0, // 系统用户
                Email = "admin@lean.com",
                PhoneNumber = "13800000001",
                Gender = 0,
                Avatar = "/avatar/default.gif",
                Status = 0,
                TenantId = 0,
                LastPasswordChangeTime = DateTime.Now,
                IsLock = 0,
                ErrorLimit = 0,
                LoginCount = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 开发管理员
            new HbtUser
            {
                Id = nextId++,
                UserName = "dev_admin",
                NickName = "开发管理员",
                EnglishName = "Development Administrator",
                UserType = 2, // 管理员
                Email = "dev_admin@lean.com",
                PhoneNumber = "13800000002",
                Gender = 0,
                Avatar = "/avatar/default.gif",
                Status = 0,
                TenantId = 0,
                LastPasswordChangeTime = DateTime.Now,
                IsLock = 0,
                ErrorLimit = 0,
                LoginCount = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 开发工程师
            new HbtUser
            {
                Id = nextId++,
                UserName = "developer",
                NickName = "开发工程师",
                EnglishName = "Developer",
                UserType = 1, // 普通用户
                Email = "developer@lean.com",
                PhoneNumber = "13800000003",
                Gender = 0,
                Avatar = "/avatar/default.gif",
                Status = 0,
                TenantId = 0,
                LastPasswordChangeTime = DateTime.Now,
                IsLock = 0,
                ErrorLimit = 0,
                LoginCount = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 测试工程师
            new HbtUser
            {
                Id = nextId++,
                UserName = "tester",
                NickName = "测试工程师",
                EnglishName = "Test Engineer",
                UserType = 1, // 普通用户
                Email = "tester@lean.com",
                PhoneNumber = "13800000004",
                Gender = 0,
                Avatar = "/avatar/default.gif",
                Status = 0,
                TenantId = 0,
                LastPasswordChangeTime = DateTime.Now,
                IsLock = 0,
                ErrorLimit = 0,
                LoginCount = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 普通用户
            new HbtUser
            {
                Id = nextId++,
                UserName = "user",
                NickName = "普通用户",
                EnglishName = "Normal User",
                UserType = 1, // 普通用户
                Email = "user@lean.com",
                PhoneNumber = "13800000005",
                Gender = 0,
                Avatar = "/avatar/default.gif",
                Status = 0,
                TenantId = 0,
                LastPasswordChangeTime = DateTime.Now,
                IsLock = 0,
                ErrorLimit = 0,
                LoginCount = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var user in defaultUsers)
        {
            var existingUser = await _userRepository.GetInfoAsync(u => u.UserName == user.UserName);
            if (existingUser == null)
            {
                // 为每个新用户生成独特的密码哈希和盐值
                var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(defaultPassword);
                user.Password = hash;
                user.Salt = salt;
                user.Iterations = iterations;
                
                await _userRepository.CreateAsync(user);
                insertCount++;
                _logger.Info($"[创建] 用户 '{user.NickName}' 创建成功");
            }
            else
            {
                existingUser.NickName = user.NickName;
                existingUser.EnglishName = user.EnglishName;
                existingUser.UserType = user.UserType;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Gender = user.Gender;
                existingUser.Avatar = user.Avatar;
                existingUser.Status = user.Status;
                existingUser.TenantId = user.TenantId;
                existingUser.LastPasswordChangeTime = user.LastPasswordChangeTime;
                existingUser.UpdateBy = "system";
                existingUser.UpdateTime = DateTime.Now;

                await _userRepository.UpdateAsync(existingUser);
                updateCount++;
                _logger.Info($"[更新] 用户 '{existingUser.NickName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}