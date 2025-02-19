//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedUser.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 用户数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 用户数据初始化类
/// </summary>
public class HbtDbSeedUser
{
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedUser(IHbtRepository<HbtUser> userRepository, IHbtLogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    public async Task<(int, int)> InitializeUserAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 使用现有的 HbtPasswordUtils 创建密码哈希
        var (hash, salt, iterations) = HbtPasswordUtils.CreateHash("123456");

        var adminUser = new HbtUser
        {
            UserName = "admin",
            NickName = "超级管理员",
            EnglishName = "Administrator",
            UserType = HbtUserType.Admin,
            Password = hash,        // 使用哈希后的密码
            Salt = salt,           // 使用生成的盐值
            Email = "admin@lean365.com",
            PhoneNumber = "13800138000",
            Gender = HbtGender.Unknown,
            Avatar = "/avatar/default.png",
            Status = HbtStatus.Normal,
            TenantId = 0,
            LastPasswordChangeTime = DateTime.Now,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now
        };

        var existingUser = await _userRepository.FirstOrDefaultAsync(u => u.UserName == adminUser.UserName);
        if (existingUser == null)
        {
            await _userRepository.InsertAsync(adminUser);
            insertCount++;
            _logger.Info($"[创建] 用户 '{adminUser.UserName}' 创建成功");
        }
        else
        {
            // 更新所有非敏感字段，保持密码和盐值不变
            existingUser.NickName = adminUser.NickName;
            existingUser.EnglishName = adminUser.EnglishName;
            existingUser.UserType = adminUser.UserType;
            existingUser.Email = adminUser.Email;
            existingUser.PhoneNumber = adminUser.PhoneNumber;
            existingUser.Gender = adminUser.Gender;
            existingUser.Avatar = adminUser.Avatar;
            existingUser.Status = adminUser.Status;
            existingUser.TenantId = adminUser.TenantId;
            existingUser.LastPasswordChangeTime = existingUser.LastPasswordChangeTime; // 保持原有值
            existingUser.Remark = adminUser.Remark;
            existingUser.CreateBy = adminUser.CreateBy;
            existingUser.CreateTime = adminUser.CreateTime;
            existingUser.UpdateBy = "system";
            existingUser.UpdateTime = DateTime.Now;

            await _userRepository.UpdateAsync(existingUser);
            updateCount++;
            _logger.Info($"[更新] 用户 '{existingUser.UserName}' 更新成功");
        }

        return (insertCount, updateCount);
    }
} 