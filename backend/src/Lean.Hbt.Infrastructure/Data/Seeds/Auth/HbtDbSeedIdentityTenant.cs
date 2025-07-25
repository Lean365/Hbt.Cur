//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTenant.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 租户数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 租户数据初始化类 - 支持多库模式
/// </summary>
public class HbtDbSeedIdentityTenant
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedIdentityTenant(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
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
                TenantName = "TCJ",
                TenantCode = "tcj",
                ConfigId = "tcj",
                ContactUser = "TCJ公司",
                ContactPhone = "13800138002",
                ContactEmail = "tcj@lean365.com",
                Address = "TCJ公司",
                LicenseType = "Professional",
                LicenseKey = "HBT-TCJ-2024-PROFESSIONAL-001",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
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
                ConfigId = "tca",
                ContactUser = "TCA公司",
                ContactPhone = "13800138003",
                ContactEmail = "tca@lean365.com",
                Address = "TCA公司",
                LicenseType = "Professional",
                LicenseKey = "HBT-TCA-2024-PROFESSIONAL-001",
                ExpireTime = DateTime.Now.AddYears(1),
                Status = 0,
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
                ConfigId = "tms",
                ContactUser = "TMS公司",
                ContactPhone = "13800138004",
                ContactEmail = "tms@lean365.com",
                Address = "TMS公司",
                LicenseType = "Standard",
                LicenseKey = "HBT-TMS-2024-STANDARD-001",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
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
                ConfigId = "dta",
                ContactUser = "DTA公司",
                ContactPhone = "13800138005",
                ContactEmail = "dta@lean365.com",
                Address = "DTA公司",
                LicenseType = "Standard",
                LicenseKey = "HBT-DTA-2024-STANDARD-001",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
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
                ConfigId = "tac",
                ContactUser = "TAC公司",
                ContactPhone = "13800138006",
                ContactEmail = "tac@lean365.com",
                Address = "TAC公司",
                LicenseType = "Standard",
                LicenseKey = "HBT-TAC-2024-STANDARD-001",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
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
                ConfigId = "tsz",
                ContactUser = "TSZ公司",
                ContactPhone = "13800138007",
                ContactEmail = "tsz@lean365.com",
                Address = "TSZ公司",
                LicenseType = "Standard",
                LicenseKey = "HBT-TSZ-2024-STANDARD-001",
                ExpireTime = DateTime.Now.AddMonths(3),
                Status = 0,
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

        // 获取认证数据库的租户仓储
        var tenantRepository = _repositoryFactory.GetAuthRepository<HbtTenant>();

        foreach (var tenant in defaultTenants)
        {
            var existingTenant = await tenantRepository.GetFirstAsync(t => t.TenantCode == tenant.TenantCode);
            if (existingTenant == null)
            {
                await tenantRepository.CreateAsync(tenant);
                insertCount++;
                _logger.Info($"[创建] 租户 '{tenant.TenantName}' 创建成功");
            }
            else
            {
                existingTenant.TenantName = tenant.TenantName;
                existingTenant.ConfigId = tenant.ConfigId;
                existingTenant.ContactUser = tenant.ContactUser;
                existingTenant.ContactPhone = tenant.ContactPhone;
                existingTenant.ContactEmail = tenant.ContactEmail;
                existingTenant.Address = tenant.Address;
                existingTenant.LicenseType = tenant.LicenseType;
                existingTenant.LicenseKey = tenant.LicenseKey;
                existingTenant.ExpireTime = tenant.ExpireTime;
                existingTenant.Status = tenant.Status;
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

                await tenantRepository.UpdateAsync(existingTenant);
                updateCount++;
                _logger.Info($"[更新] 租户 '{existingTenant.TenantName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}