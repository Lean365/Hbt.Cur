using Lean.Hbt.Common.Constants;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户切换服务
/// </summary>
public class HbtTenantSwitchService
{
    private readonly ILogger<HbtTenantSwitchService> _logger;
    private readonly IHbtCurrentUser _currentUser;
    private readonly IHbtCurrentTenant _currentTenant;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtRepository<HbtUserTenant> _userTenantRepository;
    private readonly IHbtRepository<HbtTenant> _tenantRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantSwitchService(
        ILogger<HbtTenantSwitchService> logger,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtJwtHandler jwtHandler,
        IHbtRepository<HbtUserTenant> userTenantRepository,
        IHbtRepository<HbtTenant> tenantRepository)
    {
        _logger = logger;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _jwtHandler = jwtHandler;
        _userTenantRepository = userTenantRepository;
        _tenantRepository = tenantRepository;
    }

    /// <summary>
    /// 切换租户
    /// </summary>
    public async Task<string> SwitchTenantAsync(long tenantId)
    {
        try
        {
            // 1. 验证租户是否存在且有效
            var tenant = await _tenantRepository.GetByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new HbtException("租户不存在", HbtConstants.ErrorCodes.NotFound);
            }

            if (tenant.Status != 0)
            {
                throw new HbtException("租户已停用", HbtConstants.ErrorCodes.TenantDisabled);
            }

            // 2. 验证用户是否有权限访问该租户
            var userTenant = await _userTenantRepository.GetFirstAsync(ut =>
                ut.UserId == _currentUser.UserId &&
                ut.TenantId == tenantId);

            if (userTenant == null)
            {
                throw new HbtException("用户不属于该租户", HbtConstants.ErrorCodes.NoTenantAccess);
            }

            if (userTenant.Status != 0)
            {
                throw new HbtException("用户在该租户中已停用", HbtConstants.ErrorCodes.UserDisabled);
            }

            // 3. 生成新的访问令牌
            var token = await _jwtHandler.GenerateAccessTokenAsync(
                new HbtUser 
                { 
                    Id = _currentUser.UserId,
                    UserName = _currentUser.UserName,
                    UserType = _currentUser.UserType
                },
                new HbtTenant 
                { 
                    Id = tenantId,
                    TenantName = tenant.TenantName
                },
                _currentUser.Roles,
                _currentUser.Permissions);

            _logger.LogInformation("用户 {UserId} 成功切换到租户 {TenantId}", _currentUser.UserId, tenantId);

            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "切换租户失败: UserId={UserId}, TenantId={TenantId}", _currentUser.UserId, tenantId);
            throw;
        }
    }

    /// <summary>
    /// 获取用户可访问的租户列表
    /// </summary>
    public async Task<List<HbtTenant>> GetAccessibleTenantsAsync()
    {
        try
        {
            var userTenants = await _userTenantRepository.GetListAsync(ut =>
                ut.UserId == _currentUser.UserId &&
                ut.Status == 0);

            if (userTenants == null || !userTenants.Any())
            {
                return new List<HbtTenant>();
            }

            var tenantIds = userTenants.Select(ut => ut.TenantId).ToList();
            var tenants = await _tenantRepository.GetListAsync(t =>
                tenantIds.Contains(t.Id) &&
                t.Status == 0);

            return tenants ?? new List<HbtTenant>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取用户可访问租户列表失败: UserId={UserId}", _currentUser.UserId);
            throw;
        }
    }
}