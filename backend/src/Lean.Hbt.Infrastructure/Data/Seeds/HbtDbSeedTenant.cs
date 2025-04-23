//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTenant.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 租户数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 租户数据初始化类
/// </summary>
public class HbtDbSeedTenant
{
    private readonly IHbtRepository<HbtTenant> _tenantRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tenantRepository">租户仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedTenant(IHbtRepository<HbtTenant> tenantRepository, IHbtLogger logger)
    {
        _tenantRepository = tenantRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化租户数据
    /// </summary>
    public async Task<(int, int)> InitializeTenantAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultTenants = new List<HbtTenant>
        {
            new HbtTenant
            {
                TenantId = 0,
                TenantName = "系统租户",
                TenantCode = "system",
                ContactUser = "系统管理员",
                ContactPhone = "13800138000",
                ContactEmail = "admin@lean365.com",
                Address = "系统默认地址",
                License = "Enterprise",
                ExpireTime = DateTime.Now.AddYears(99),
                Status = 0,
                IsDefault = 1,
                DbConnection = "Server=localhost;Database=LeanHbt_System;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(99),
                MaxUserCount = 999999,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "系统租户",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantId = 1,
                TenantName = "默认租户",
                TenantCode = "default",
                ContactUser = "管理员",
                ContactPhone = "13800138001",
                ContactEmail = "admin@lean365.com",
                Address = "默认地址",
                License = "Enterprise",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Dev;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 100,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "默认租户",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantId = 2,
                TenantName = "测试租户A",
                TenantCode = "test_a",
                ContactUser = "测试管理员A",
                ContactPhone = "13800138002",
                ContactEmail = "test_a@lean365.com",
                Address = "测试地址A",
                License = "Professional",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_TestA;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "test-a.localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 50,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "测试租户A",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantId = 3,
                TenantName = "测试租户B",
                TenantCode = "test_b",
                ContactUser = "测试管理员B",
                ContactPhone = "13800138003",
                ContactEmail = "test_b@lean365.com",
                Address = "测试地址B",
                License = "Professional",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_TestB;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "test-b.localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 50,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "测试租户B",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantId = 4,
                TenantName = "演示租户A",
                TenantCode = "demo_a",
                ContactUser = "演示管理员A",
                ContactPhone = "13800138004",
                ContactEmail = "demo_a@lean365.com",
                Address = "演示地址A",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_DemoA;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "demo-a.localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "演示租户A",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantId = 5,
                TenantName = "演示租户B",
                TenantCode = "demo_b",
                ContactUser = "演示管理员B",
                ContactPhone = "13800138005",
                ContactEmail = "demo_b@lean365.com",
                Address = "演示地址B",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_DemoB;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "demo-b.localhost",
                LogoUrl = "/logo.png",
                Theme = "default",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now,
                Remark = "演示租户B",
                IsDeleted = 0
            }
        };

        foreach (var tenant in defaultTenants)
        {
            var existingTenant = await _tenantRepository.GetFirstAsync(t => t.TenantCode == tenant.TenantCode);
            if (existingTenant == null)
            {
                await _tenantRepository.CreateAsync(tenant);
                insertCount++;
                _logger.Info($"[创建] 租户 '{tenant.TenantName}' 创建成功");
            }
            else
            {
                existingTenant.TenantName = tenant.TenantName;
                existingTenant.ContactUser = tenant.ContactUser;
                existingTenant.ContactPhone = tenant.ContactPhone;
                existingTenant.ContactEmail = tenant.ContactEmail;
                existingTenant.Address = tenant.Address;
                existingTenant.License = tenant.License;
                existingTenant.ExpireTime = tenant.ExpireTime;
                existingTenant.Status = tenant.Status;
                existingTenant.IsDefault = tenant.IsDefault;
                existingTenant.DbConnection = tenant.DbConnection;
                existingTenant.Domain = tenant.Domain;
                existingTenant.LogoUrl = tenant.LogoUrl;
                existingTenant.Theme = tenant.Theme;
                existingTenant.LicenseStartTime = tenant.LicenseStartTime;
                existingTenant.LicenseEndTime = tenant.LicenseEndTime;
                existingTenant.MaxUserCount = tenant.MaxUserCount;
                existingTenant.Remark = tenant.Remark;
                existingTenant.IsDeleted = tenant.IsDeleted;
                existingTenant.UpdateBy = "system";
                existingTenant.UpdateTime = DateTime.Now;

                await _tenantRepository.UpdateAsync(existingTenant);
                updateCount++;
                _logger.Info($"[更新] 租户 '{existingTenant.TenantName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}