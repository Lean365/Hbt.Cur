//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTenant.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 租户数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

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
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now
        };

        var existingTenant = await _tenantRepository.FirstOrDefaultAsync(t => t.TenantCode == defaultTenant.TenantCode);
        if (existingTenant == null)
        {
            await _tenantRepository.InsertAsync(defaultTenant);
            insertCount++;
            _logger.Info($"[创建] 租户 '{defaultTenant.TenantName}' 创建成功");
        }
        else
        {
            existingTenant.TenantName = defaultTenant.TenantName;
            existingTenant.ContactPerson = defaultTenant.ContactPerson;
            existingTenant.ContactPhone = defaultTenant.ContactPhone;
            existingTenant.ContactEmail = defaultTenant.ContactEmail;
            existingTenant.Address = defaultTenant.Address;
            existingTenant.Domain = defaultTenant.Domain;
            existingTenant.LogoUrl = defaultTenant.LogoUrl;
            existingTenant.DbConnection = defaultTenant.DbConnection;
            existingTenant.Theme = defaultTenant.Theme;
            existingTenant.LicenseStartTime = defaultTenant.LicenseStartTime;
            existingTenant.LicenseEndTime = defaultTenant.LicenseEndTime;
            existingTenant.MaxUserCount = defaultTenant.MaxUserCount;
            existingTenant.Status = defaultTenant.Status;
            existingTenant.Remark = defaultTenant.Remark;
            existingTenant.CreateBy = defaultTenant.CreateBy;
            existingTenant.CreateTime = defaultTenant.CreateTime;
            existingTenant.UpdateBy = "system";
            existingTenant.UpdateTime = DateTime.Now;

            await _tenantRepository.UpdateAsync(existingTenant);
            updateCount++;
            _logger.Info($"[更新] 租户 '{existingTenant.TenantName}' 更新成功");
        }

        return (insertCount, updateCount);
    }
} 