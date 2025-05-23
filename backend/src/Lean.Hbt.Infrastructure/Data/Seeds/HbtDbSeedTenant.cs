//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTenant.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 租户数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

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
                DbConnection = "Server=localhost;Database=LeanHbt_System;User ID=sa;Password=System26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "localhost",
                LogoUrl = "/system.png",
                Theme = "system",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(99),
                MaxUserCount = 999999,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "系统租户",
                IsDeleted = 0
            },
            new HbtTenant
            {
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
                DbConnection = "Server=localhost;Database=LeanHbt_Dev;User ID=sa;Password=Dev26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "localhost",
                LogoUrl = "/dev.png",
                Theme = "dev",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 100,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "默认租户",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantName = "TCJ",
                TenantCode = "tcj",
                ContactUser = "TCJ公司",
                ContactPhone = "13800138002",
                ContactEmail = "tcj@lean365.com",
                Address = "TCJ公司",
                License = "Professional",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tcj;User ID=sa;Password=Tcj26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "tcj.localhost",
                LogoUrl = "/tcj.png",
                Theme = "tcj",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 50,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "TCJ公司",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantName = "TCA",
                TenantCode = "tca",
                ContactUser = "TCA公司",
                ContactPhone = "13800138003",
                ContactEmail = "tca@lean365.com",
                Address = "TCA公司",
                License = "Professional",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tca;User ID=sa;Password=Tca26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "tca.localhost",
                LogoUrl = "/tca.png",
                Theme = "tca",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddYears(1),
                MaxUserCount = 50,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "TCA公司",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantName = "TMS",
                TenantCode = "tms",
                ContactUser = "TMS公司",
                ContactPhone = "13800138004",
                ContactEmail = "tms@lean365.com",
                Address = "TMS公司",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tms;User ID=sa;Password=Tms26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "tms.localhost",
                LogoUrl = "/tms.png",
                Theme = "tms",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "TMS公司",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantName = "DTA",
                TenantCode = "dta",
                ContactUser = "DTA公司",
                ContactPhone = "13800138005",
                ContactEmail = "dta@lean365.com",
                Address = "DTA公司",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tdz;User ID=sa;Password=Dta26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "dta.localhost",
                LogoUrl = "/dta.png",
                Theme = "dta",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "DTA公司",
                IsDeleted = 0
            },
            new HbtTenant
            {
                TenantName = "TAC",
                TenantCode = "tac",
                ContactUser = "TAC公司",
                ContactPhone = "13800138006",
                ContactEmail = "tac@lean365.com",
                Address = "TAC公司",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tac;User ID=sa;Password=Tac26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "tac.localhost",
                LogoUrl = "/tac.png",
                Theme = "tac",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "TAC公司",
                IsDeleted = 0
            },
             new HbtTenant
            {
                TenantName = "TSZ",
                TenantCode = "tsz",
                ContactUser = "TSZ公司",
                ContactPhone = "13800138007",
                ContactEmail = "tsz@lean365.com",
                Address = "TSZ公司",
                License = "Standard",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
                IsDefault = 0,
                DbConnection = "Server=localhost;Database=LeanHbt_Tsz;User ID=sa;Password=Tsz26901333.;Trusted_Connection=True;MultipleActiveResultSets=true",
                Domain = "tsz.localhost",
                LogoUrl = "/tsz.png",
                Theme = "tsz",
                LicenseStartTime = DateTime.Now,
                LicenseEndTime = DateTime.Now.AddMonths(3),
                MaxUserCount = 20,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = "TSZ公司",
                IsDeleted = 0
            },
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
                existingTenant.UpdateBy = "Hbt365";
                existingTenant.UpdateTime = DateTime.Now;

                await _tenantRepository.UpdateAsync(existingTenant);
                updateCount++;
                _logger.Info($"[更新] 租户 '{existingTenant.TenantName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取系统租户
    /// </summary>
    /// <returns>系统租户信息</returns>
    public async Task<HbtTenant?> GetSystemTenantAsync()
    {
        return await _tenantRepository.GetFirstAsync(t => t.TenantCode == "system" && t.IsDeleted == 0);
    }
}