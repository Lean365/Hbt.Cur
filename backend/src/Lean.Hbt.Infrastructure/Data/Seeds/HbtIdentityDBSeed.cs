//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIdentityDBSeed.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 认证数据库种子数据初始化类
//===================================================================

using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Infrastructure.Data.Contexts;
using Lean.Hbt.Infrastructure.Data.Seeds.Auth;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 认证数据库种子数据初始化类
/// </summary>
public class HbtIdentityDBSeed
{
    private readonly HbtIdentityDBContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedIdentityTenant _tenantSeed;
    private readonly HbtDbSeedIdentityRole _roleSeed;
    private readonly HbtDbSeedIdentityUser _userSeed;
    private readonly HbtDbSeedMenu _menuSeed;
    private readonly HbtDbSeedIdentityDept _deptSeed;
    private readonly HbtDbSeedIdentityPost _postSeed;
    private readonly HbtDbSeedIdentityRelation _relationSeed;


    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtIdentityDBSeed(
        HbtIdentityDBContext context,
        IHbtLogger logger,
        HbtDbSeedIdentityTenant tenantSeed,
        HbtDbSeedIdentityRole roleSeed,
        HbtDbSeedIdentityUser userSeed,
        HbtDbSeedMenu menuSeed,
        HbtDbSeedIdentityDept deptSeed,
        HbtDbSeedIdentityPost postSeed,
        HbtDbSeedIdentityRelation relationSeed)
    {
        _context = context;
        _logger = logger;
        _tenantSeed = tenantSeed;
        _roleSeed = roleSeed;
        _userSeed = userSeed;
        _menuSeed = menuSeed;
        _deptSeed = deptSeed;
        _postSeed = postSeed;
        _relationSeed = relationSeed;

    }

    /// <summary>
    /// 初始化认证数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化认证数据库种子数据...");

            // 初始化租户数据
            var (tenantInsertCount, tenantUpdateCount) = await _tenantSeed.InitializeTenantAsync();
            _logger.Info($"租户数据初始化完成: 新增 {tenantInsertCount} 条, 更新 {tenantUpdateCount} 条");

            // 初始化角色数据
            var (roleInsertCount, roleUpdateCount) = await _roleSeed.InitializeRoleAsync();
            _logger.Info($"角色数据初始化完成: 新增 {roleInsertCount} 条, 更新 {roleUpdateCount} 条");

            // 初始化用户数据
            var (userInsertCount, userUpdateCount) = await _userSeed.InitializeUserAsync();
            _logger.Info($"用户数据初始化完成: 新增 {userInsertCount} 条, 更新 {userUpdateCount} 条");

            // 初始化菜单数据
            var (menuInsertCount, menuUpdateCount) = await _menuSeed.InitializeMenuAsync();
            _logger.Info($"菜单数据初始化完成: 新增 {menuInsertCount} 条, 更新 {menuUpdateCount} 条");

            // 初始化部门数据
            var (deptInsertCount, deptUpdateCount) = await _deptSeed.InitializeDeptAsync();
            _logger.Info($"部门数据初始化完成: 新增 {deptInsertCount} 条, 更新 {deptUpdateCount} 条");

            // 初始化岗位数据
            var (postInsertCount, postUpdateCount) = await _postSeed.InitializePostAsync();
            _logger.Info($"岗位数据初始化完成: 新增 {postInsertCount} 条, 更新 {postUpdateCount} 条");

            // 初始化关系数据（放在最后）
            var (relationInsertCount, relationUpdateCount) = await _relationSeed.InitializeRelationsAsync();
            _logger.Info($"关系数据初始化完成: 新增 {relationInsertCount} 条, 更新 {relationUpdateCount} 条");

            _logger.Info("认证数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"认证数据库种子数据初始化失败: {ex.Message}", ex);
            throw new HbtException("认证数据库种子数据初始化失败", ex);
        }
    }
}