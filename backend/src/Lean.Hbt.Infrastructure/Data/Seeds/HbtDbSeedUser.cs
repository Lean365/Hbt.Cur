//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedUser.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 用户数据初始化类
//===================================================================

using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;

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
    /// 初始化系统预设用户（admin、dev_admin、developer、tester、user）
    /// </summary>
    private async Task<(int, int)> InitializeSystemUsersAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;
        var defaultPassword = _configuration.GetValue<string>("Security:PasswordPolicy:DefaultPassword")!;
        var systemUserNames = new[] { "admin", "dev_admin", "developer", "tester", "user" };
        var existingUsers = await _userRepository.GetListAsync(u => systemUserNames.Contains(u.UserName));
        var existingUserDict = existingUsers.ToDictionary(u => u.UserName, u => u);
        var existingPhones = existingUsers.Select(u => u.PhoneNumber).ToHashSet();
        var db = _userRepository.SqlSugarClient;
        var now = DateTime.Now;
        var systemUsers = new List<HbtUser>
        {
            new HbtUser { Id = nextId++, UserName = "admin", NickName = "超级管理员", EnglishName = "System Administrator", UserType = 2, Email = "admin@hbt365.net", PhoneNumber = "13800000001", Gender = 0, Avatar = "/avatar/default.gif", Status = 0, TenantId = 0, LastPasswordChangeTime = now, IsLock = 0, ErrorLimit = 0, LoginCount = 0, CreateBy = "Hbt365", CreateTime = now, UpdateBy = "Hbt365", UpdateTime = now },
            new HbtUser { Id = nextId++, UserName = "dev_admin", NickName = "开发管理员", EnglishName = "Development Administrator", UserType = 2, Email = "dev_admin@lean.com", PhoneNumber = "13800000002", Gender = 0, Avatar = "/avatar/default.gif", Status = 0, TenantId = 0, LastPasswordChangeTime = now, IsLock = 0, ErrorLimit = 0, LoginCount = 0, CreateBy = "Hbt365", CreateTime = now, UpdateBy = "Hbt365", UpdateTime = now },
            new HbtUser { Id = nextId++, UserName = "developer", NickName = "开发工程师", EnglishName = "Developer", UserType = 1, Email = "developer@lean.com", PhoneNumber = "13800000003", Gender = 0, Avatar = "/avatar/default.gif", Status = 0, TenantId = 0, LastPasswordChangeTime = now, IsLock = 0, ErrorLimit = 0, LoginCount = 0, CreateBy = "Hbt365", CreateTime = now, UpdateBy = "Hbt365", UpdateTime = now },
            new HbtUser { Id = nextId++, UserName = "tester", NickName = "测试工程师", EnglishName = "Test Engineer", UserType = 1, Email = "tester@lean.com", PhoneNumber = "13800000004", Gender = 0, Avatar = "/avatar/default.gif", Status = 0, TenantId = 0, LastPasswordChangeTime = now, IsLock = 0, ErrorLimit = 0, LoginCount = 0, CreateBy = "Hbt365", CreateTime = now, UpdateBy = "Hbt365", UpdateTime = now },
            new HbtUser { Id = nextId++, UserName = "user", NickName = "普通用户", EnglishName = "Normal User", UserType = 1, Email = "user@lean.com", PhoneNumber = "13800000005", Gender = 0, Avatar = "/avatar/default.gif", Status = 0, TenantId = 0, LastPasswordChangeTime = now, IsLock = 0, ErrorLimit = 0, LoginCount = 0, CreateBy = "Hbt365", CreateTime = now, UpdateBy = "Hbt365", UpdateTime = now }
        };
        var systemToInsert = new List<HbtUser>();
        var systemToUpdate = new List<HbtUser>();
        foreach (var user in systemUsers)
        {
            if (existingUserDict.TryGetValue(user.UserName, out var existingUser))
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
                existingUser.UpdateBy = "Hbt365";
                existingUser.UpdateTime = now;
                systemToUpdate.Add(existingUser);
            }
            else if (!existingPhones.Contains(user.PhoneNumber))
            {
                var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(defaultPassword);
                user.Password = hash;
                user.Salt = salt;
                user.Iterations = iterations;
                systemToInsert.Add(user);
            }
        }
        if (systemToInsert.Count > 0)
        {
            db.Fastest<HbtUser>().BulkCopy(systemToInsert);
            insertCount += systemToInsert.Count;
            _logger.Info($"[系统用户] {systemToInsert.Count} 条用户数据插入成功");
        }
        if (systemToUpdate.Count > 0)
        {
            await db.Updateable(systemToUpdate).ExecuteCommandAsync();
            updateCount += systemToUpdate.Count;
            _logger.Info($"[系统用户] {systemToUpdate.Count} 条用户数据更新成功");
        }
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化批量普通用户（user1~userN）
    /// </summary>
    private async Task<(int, int)> InitializeBatchUsersAsync(int totalCount = 1000)
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 100; // 避免和系统用户ID冲突
        var defaultPassword = _configuration.GetValue<string>("Security:PasswordPolicy:DefaultPassword")!;
        var db = _userRepository.SqlSugarClient;
        var now = DateTime.Now;
        var batchSize = 1000;
        var totalBatches = (int)Math.Ceiling((double)totalCount / batchSize);
        var existingUsers = await _userRepository.GetListAsync(u => u.UserName.StartsWith("user") && u.UserName != "user");
        var existingUserDict = existingUsers.ToDictionary(u => u.UserName, u => u);
        var existingPhones = existingUsers.Select(u => u.PhoneNumber).ToHashSet();
        for (int batch = 0; batch < totalBatches; batch++)
        {
            var startIndex = batch * batchSize + 1;
            var endIndex = Math.Min((batch + 1) * batchSize, totalCount);
            var toInsert = new List<HbtUser>(batchSize);
            var toUpdate = new List<HbtUser>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                var userName = $"user{i}";
                var phoneNumber = $"{13810000000 + i:D11}";
                if (existingUserDict.TryGetValue(userName, out var existingUser))
                {
                    existingUser.NickName = $"普通用户{i}";
                    existingUser.EnglishName = $"Normal User {i}";
                    existingUser.UserType = 1;
                    existingUser.Email = $"user{i}@lean.com";
                    existingUser.PhoneNumber = phoneNumber;
                    existingUser.Gender = 0;
                    existingUser.Avatar = "/avatar/default.gif";
                    existingUser.Status = 0;
                    existingUser.TenantId = 0;
                    existingUser.LastPasswordChangeTime = now;
                    existingUser.UpdateBy = "Hbt365";
                    existingUser.UpdateTime = now;
                    toUpdate.Add(existingUser);
                }
                else if (!existingPhones.Contains(phoneNumber))
                {
                    var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(defaultPassword);
                    toInsert.Add(new HbtUser
                    {
                        Id = nextId++,
                        UserName = userName,
                        NickName = $"普通用户{i}",
                        EnglishName = $"Normal User {i}",
                        UserType = 1,
                        Email = $"user{i}@lean.com",
                        PhoneNumber = phoneNumber,
                        Gender = 0,
                        Avatar = "/avatar/default.gif",
                        Status = 0,
                        TenantId = 0,
                        LastPasswordChangeTime = now,
                        IsLock = 0,
                        ErrorLimit = 0,
                        LoginCount = 0,
                        CreateBy = "Hbt365",
                        CreateTime = now,
                        UpdateBy = "Hbt365",
                        UpdateTime = now,
                        Password = hash,
                        Salt = salt,
                        Iterations = iterations
                    });
                }
            }
            if (toInsert.Count > 0)
            {
                db.Fastest<HbtUser>().PageSize(batchSize).BulkCopy(toInsert);
                insertCount += toInsert.Count;
                _logger.Info($"[批量插入] {toInsert.Count} 条用户数据插入成功");
            }
            if (toUpdate.Count > 0)
            {
                await db.Updateable(toUpdate).ExecuteCommandAsync();
                updateCount += toUpdate.Count;
                _logger.Info($"[批量更新] {toUpdate.Count} 条用户数据更新成功");
            }
            _logger.Info($"[批次完成] 第 {batch + 1}/{totalBatches} 批处理完成");
        }
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户数据（入口）
    /// </summary>
    public async Task<(int, int)> InitializeUserAsync()
    {
        var (sysInsert, sysUpdate) = await InitializeSystemUsersAsync();
        var (batchInsert, batchUpdate) = await InitializeBatchUsersAsync();
        return (sysInsert + batchInsert, sysUpdate + batchUpdate);
    }
}