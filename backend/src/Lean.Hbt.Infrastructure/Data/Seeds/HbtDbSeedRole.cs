//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRole.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 角色数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 角色数据初始化类
/// </summary>
public class HbtDbSeedRole
{
    private readonly IHbtRepository<HbtRole> _roleRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedRole(IHbtRepository<HbtRole> roleRepository, IHbtLogger logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化角色数据
    /// </summary>
    public async Task<(int, int)> InitializeRoleAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var adminRole = new HbtRole
        {
            RoleName = "超级管理员",
            RoleKey = "admin",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now
        };

        var existingRole = await _roleRepository.FirstOrDefaultAsync(r => r.RoleKey == adminRole.RoleKey);
        if (existingRole == null)
        {
            await _roleRepository.InsertAsync(adminRole);
            insertCount++;
            _logger.Info($"[创建] 角色 '{adminRole.RoleName}' 创建成功");
        }
        else
        {
            existingRole.RoleName = adminRole.RoleName;
            existingRole.RoleKey = adminRole.RoleKey;
            existingRole.OrderNum = adminRole.OrderNum;
            existingRole.DataScope = adminRole.DataScope;
            existingRole.Status = adminRole.Status;
            existingRole.TenantId = adminRole.TenantId;
            existingRole.Remark = adminRole.Remark;
            existingRole.CreateBy = adminRole.CreateBy;
            existingRole.CreateTime = adminRole.CreateTime;
            existingRole.UpdateBy = "system";
            existingRole.UpdateTime = DateTime.Now;

            await _roleRepository.UpdateAsync(existingRole);
            updateCount++;
            _logger.Info($"[更新] 角色 '{existingRole.RoleName}' 更新成功");
        }

        return (insertCount, updateCount);
    }
} 