//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRelation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 关联关系数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 关联关系数据初始化类
/// </summary>
public class HbtDbSeedRelation
{
    private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
    private readonly IHbtRepository<HbtUserPost> _userPostRepository;
    private readonly IHbtRepository<HbtUserDept> _userDeptRepository;
    private readonly IHbtRepository<HbtRoleMenu> _roleMenuRepository;
    private readonly IHbtRepository<HbtRoleDept> _roleDeptRepository;
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtRepository<HbtRole> _roleRepository;
    private readonly IHbtRepository<HbtPost> _postRepository;
    private readonly IHbtRepository<HbtDept> _deptRepository;
    private readonly IHbtRepository<HbtMenu> _menuRepository;
    private readonly IHbtRepository<HbtUserTenant> _userTenantRepository;
    private readonly IHbtRepository<HbtTenant> _tenantRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedRelation(
        IHbtRepository<HbtUserRole> userRoleRepository,
        IHbtRepository<HbtUserPost> userPostRepository,
        IHbtRepository<HbtUserDept> userDeptRepository,
        IHbtRepository<HbtRoleMenu> roleMenuRepository,
        IHbtRepository<HbtRoleDept> roleDeptRepository,
        IHbtRepository<HbtUser> userRepository,
        IHbtRepository<HbtRole> roleRepository,
        IHbtRepository<HbtPost> postRepository,
        IHbtRepository<HbtDept> deptRepository,
        IHbtRepository<HbtMenu> menuRepository,
        IHbtRepository<HbtUserTenant> userTenantRepository,
        IHbtRepository<HbtTenant> tenantRepository,
        IHbtLogger logger)
    {
        _userRoleRepository = userRoleRepository;
        _userPostRepository = userPostRepository;
        _userDeptRepository = userDeptRepository;
        _roleMenuRepository = roleMenuRepository;
        _roleDeptRepository = roleDeptRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _postRepository = postRepository;
        _deptRepository = deptRepository;
        _menuRepository = menuRepository;
        _userTenantRepository = userTenantRepository;
        _tenantRepository = tenantRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化所有关联关系数据
    /// </summary>
    public async Task<(int, int)> InitializeRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取默认租户
        var defaultTenant = await _tenantRepository.GetFirstAsync(t => t.IsDefault == 1 && t.IsDeleted == 0);
        if (defaultTenant == null)
        {
            _logger.Error("未找到默认租户，无法初始化用户租户关联关系");
            return (0, 0);
        }

        // 初始化用户租户关联关系
        var (userTenantInsert, userTenantUpdate) = await InitializeUserTenantRelationsAsync(defaultTenant.Id);
        insertCount += userTenantInsert;
        updateCount += userTenantUpdate;

        // 初始化管理员关联关系
        var (adminInsert, adminUpdate) = await InitializeAdminRelationsAsync();
        insertCount += adminInsert;
        updateCount += adminUpdate;

        // 初始化所有用户关联关系
        var (userInsertCount, userUpdateCount) = await InitializeUserRelationsAsync();
        insertCount += userInsertCount;
        updateCount += userUpdateCount;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化管理员关联关系数据
    /// </summary>
    private async Task<(int, int)> InitializeAdminRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 1. 获取默认用户、角色、岗位、部门
        var adminUser = await _userRepository.GetFirstAsync(u => u.UserName == "admin");
        var adminRole = await _roleRepository.GetFirstAsync(r => r.RoleKey == "system_admin");
        var gmPost = await _postRepository.GetFirstAsync(p => p.PostCode == "GM");
        var rootDept = await _deptRepository.GetFirstAsync(d => d.ParentId == 0);

        if (adminUser == null || adminRole == null || gmPost == null || rootDept == null)
        {
            _logger.Error("未找到必要的基础数据，无法初始化管理员关联关系");
            return (0, 0);
        }

        // 2. 初始化用户-角色关联
        var userRole = await _userRoleRepository.GetFirstAsync(ur =>
            ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id);

        if (userRole == null)
        {
            await _userRoleRepository.CreateAsync(new HbtUserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户角色关联 - 用户:{adminUser.UserName}, 角色:{adminRole.RoleName}");
        }
        else
        {
            userRole.UpdateBy = "Hbt365";
            userRole.UpdateTime = DateTime.Now;
            await _userRoleRepository.UpdateAsync(userRole);
            updateCount++;
            _logger.Info($"[更新] 用户角色关联 - 用户:{adminUser.UserName}, 角色:{adminRole.RoleName}");
        }

        // 3. 初始化用户-岗位关联
        var userPost = await _userPostRepository.GetFirstAsync(up =>
            up.UserId == adminUser.Id && up.PostId == gmPost.Id);

        if (userPost == null)
        {
            await _userPostRepository.CreateAsync(new HbtUserPost
            {
                UserId = adminUser.Id,
                PostId = gmPost.Id,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户岗位关联 - 用户:{adminUser.UserName}, 岗位:{gmPost.PostName}");
        }
        else
        {
            userPost.UpdateBy = "Hbt365";
            userPost.UpdateTime = DateTime.Now;
            await _userPostRepository.UpdateAsync(userPost);
            updateCount++;
            _logger.Info($"[更新] 用户岗位关联 - 用户:{adminUser.UserName}, 岗位:{gmPost.PostName}");
        }

        // 4. 初始化用户-部门关联
        var userDept = await _userDeptRepository.GetFirstAsync(ud =>
            ud.UserId == adminUser.Id && ud.DeptId == rootDept.Id);

        if (userDept == null)
        {
            await _userDeptRepository.CreateAsync(new HbtUserDept
            {
                UserId = adminUser.Id,
                DeptId = rootDept.Id,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户部门关联 - 用户:{adminUser.UserName}, 部门:{rootDept.DeptName}");
        }
        else
        {
            userDept.UpdateBy = "Hbt365";
            userDept.UpdateTime = DateTime.Now;
            await _userDeptRepository.UpdateAsync(userDept);
            updateCount++;
            _logger.Info($"[更新] 用户部门关联 - 用户:{adminUser.UserName}, 部门:{rootDept.DeptName}");
        }

        // 5. 初始化角色-菜单关联
        var allMenus = await _menuRepository.GetListAsync(m => m.IsDeleted == 0);
        foreach (var menu in allMenus)
        {
            var roleMenu = await _roleMenuRepository.GetFirstAsync(rm =>
                rm.RoleId == adminRole.Id && rm.MenuId == menu.Id);

            if (roleMenu == null)
            {
                await _roleMenuRepository.CreateAsync(new HbtRoleMenu
                {
                    RoleId = adminRole.Id,
                    MenuId = menu.Id,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                });
                insertCount++;
                _logger.Info($"[创建] 角色菜单关联 - 角色:{adminRole.RoleName}, 菜单:{menu.MenuName}");
            }
            else
            {
                roleMenu.UpdateBy = "Hbt365";
                roleMenu.UpdateTime = DateTime.Now;
                await _roleMenuRepository.UpdateAsync(roleMenu);
                updateCount++;
                _logger.Info($"[更新] 角色菜单关联 - 角色:{adminRole.RoleName}, 菜单:{menu.MenuName}");
            }
        }

        // 6. 初始化角色-部门关联
        var allDepts = await _deptRepository.GetListAsync(d => d.IsDeleted == 0);
        foreach (var dept in allDepts)
        {
            var roleDept = await _roleDeptRepository.GetFirstAsync(rd =>
                rd.RoleId == adminRole.Id && rd.DeptId == dept.Id);

            if (roleDept == null)
            {
                await _roleDeptRepository.CreateAsync(new HbtRoleDept
                {
                    RoleId = adminRole.Id,
                    DeptId = dept.Id,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                });
                insertCount++;
                _logger.Info($"[创建] 角色部门关联 - 角色:{adminRole.RoleName}, 部门:{dept.DeptName}");
            }
            else
            {
                roleDept.UpdateBy = "Hbt365";
                roleDept.UpdateTime = DateTime.Now;
                await _roleDeptRepository.UpdateAsync(roleDept);
                updateCount++;
                _logger.Info($"[更新] 角色部门关联 - 角色:{adminRole.RoleName}, 部门:{dept.DeptName}");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化其他用户关联关系数据
    /// </summary>
    private async Task<(int, int)> InitializeUserRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 获取所有用户、角色、部门、岗位和菜单
        var userTenants = await _userTenantRepository.GetListAsync(ut => ut.IsDeleted == 0 );
        var userIds = userTenants.Select(ut => ut.UserId).ToList();
        var allUsers = (await _userRepository.GetListAsync(u => u.IsDeleted == 0 && u.UserName != "admin" && userIds.Contains(u.Id))).Take(100).ToList();
        var allRoles = await _roleRepository.GetListAsync(r => r.IsDeleted == 0);
        var allDepts = await _deptRepository.GetListAsync(d => d.IsDeleted == 0);
        var allPosts = await _postRepository.GetListAsync(p => p.IsDeleted == 0);
        var allMenus = await _menuRepository.GetListAsync(m => m.IsDeleted == 0);

        // 1. 创建角色-部门关联（根据角色类型分配部门）
        foreach (var role in allRoles)
        {
            var depts = allDepts.Where(d =>
            {
                // 系统管理员可以管理所有部门
                if (role.RoleKey == "system_admin") return true;

                // 安全管理员管理安全相关部门
                if (role.RoleKey == "security_admin")
                    return d.DeptName.Contains("安全") || d.DeptName.Contains("IT");

                // 业务管理员管理业务相关部门
                if (role.RoleKey == "biz_manager")
                    return d.DeptName.Contains("业务") || d.DeptName.Contains("管理");

                // 人事管理员管理人事相关部门
                if (role.RoleKey == "hr_manager")
                    return d.DeptName.Contains("人事") || d.DeptName.Contains("人力资源");

                // 财务管理员管理财务相关部门
                if (role.RoleKey == "fin_manager")
                    return d.DeptName.Contains("财务") || d.DeptName.Contains("会计");

                // 生产管理员管理生产相关部门
                if (role.RoleKey == "prod_manager")
                    return d.DeptName.Contains("生产") || d.DeptName.Contains("制造");

                // 品质管理员管理品质相关部门
                if (role.RoleKey == "qc_manager")
                    return d.DeptName.Contains("品质") || d.DeptName.Contains("质量");

                // 仓库管理员管理仓库相关部门
                if (role.RoleKey == "wh_manager")
                    return d.DeptName.Contains("仓库") || d.DeptName.Contains("仓储");

                // 库存管理员管理库存相关部门
                if (role.RoleKey == "inv_manager")
                    return d.DeptName.Contains("库存") || d.DeptName.Contains("仓储");

                // 采购管理员管理采购相关部门
                if (role.RoleKey == "purchase_manager")
                    return d.DeptName.Contains("采购") || d.DeptName.Contains("购买");

                // 供应商管理员管理供应商相关部门
                if (role.RoleKey == "supplier_manager")
                    return d.DeptName.Contains("供应商") || d.DeptName.Contains("采购");

                // 销售管理员管理销售相关部门
                if (role.RoleKey == "sales_manager")
                    return d.DeptName.Contains("销售") || d.DeptName.Contains("营销");

                // 客户管理员管理客户相关部门
                if (role.RoleKey == "customer_manager")
                    return d.DeptName.Contains("客户") || d.DeptName.Contains("销售");

                // 其他角色根据角色名称匹配对应的部门
                return d.DeptName.Contains(role.RoleName.Replace("管理员", ""));
            }).ToList();

            foreach (var dept in depts)
            {
                var roleDept = new HbtRoleDept
                {
                    RoleId = role.Id,
                    DeptId = dept.Id,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                };

                var existingRoleDept = await _roleDeptRepository.GetFirstAsync(rd =>
                    rd.RoleId == roleDept.RoleId && rd.DeptId == roleDept.DeptId);

                if (existingRoleDept == null)
                {
                    await _roleDeptRepository.CreateAsync(roleDept);
                    insertCount++;
                    _logger.Info($"[创建] 角色部门关联 - 角色:{role.RoleName}, 部门:{dept.DeptName}");
                }
            }
        }

        // 2. 创建角色-菜单关联（根据角色类型分配菜单）
        foreach (var role in allRoles)
        {
            var menus = allMenus.Where(m =>
            {
                // 系统管理员拥有所有菜单
                if (role.RoleKey == "system_admin") return true;

                // 安全管理员拥有安全相关菜单
                if (role.RoleKey == "security_admin")
                    return m.MenuName.Contains("安全") || m.MenuName.Contains("权限");

                // 业务管理员拥有业务相关菜单
                if (role.RoleKey == "biz_manager")
                    return m.MenuName.Contains("业务") || m.MenuName.Contains("管理");

                // 人事管理员拥有人事相关菜单
                if (role.RoleKey == "hr_manager")
                    return m.MenuName.Contains("人事") || m.MenuName.Contains("员工");

                // 财务管理员拥有财务相关菜单
                if (role.RoleKey == "fin_manager")
                    return m.MenuName.Contains("财务") || m.MenuName.Contains("会计");

                // 生产管理员拥有生产相关菜单
                if (role.RoleKey == "prod_manager")
                    return m.MenuName.Contains("生产") || m.MenuName.Contains("制造");

                // 品质管理员拥有品质相关菜单
                if (role.RoleKey == "qc_manager")
                    return m.MenuName.Contains("品质") || m.MenuName.Contains("质量");

                // 仓库管理员拥有仓库相关菜单
                if (role.RoleKey == "wh_manager")
                    return m.MenuName.Contains("仓库") || m.MenuName.Contains("仓储");

                // 库存管理员拥有库存相关菜单
                if (role.RoleKey == "inv_manager")
                    return m.MenuName.Contains("库存") || m.MenuName.Contains("仓储");

                // 采购管理员拥有采购相关菜单
                if (role.RoleKey == "purchase_manager")
                    return m.MenuName.Contains("采购") || m.MenuName.Contains("购买");

                // 供应商管理员拥有供应商相关菜单
                if (role.RoleKey == "supplier_manager")
                    return m.MenuName.Contains("供应商") || m.MenuName.Contains("采购");

                // 销售管理员拥有销售相关菜单
                if (role.RoleKey == "sales_manager")
                    return m.MenuName.Contains("销售") || m.MenuName.Contains("营销");

                // 客户管理员拥有客户相关菜单
                if (role.RoleKey == "customer_manager")
                    return m.MenuName.Contains("客户") || m.MenuName.Contains("销售");

                // 其他角色根据角色名称匹配对应的菜单
                return m.MenuName.Contains(role.RoleName.Replace("管理员", ""));
            }).ToList();

            foreach (var menu in menus)
            {
                var roleMenu = new HbtRoleMenu
                {
                    RoleId = role.Id,
                    MenuId = menu.Id,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                };

                var existingRoleMenu = await _roleMenuRepository.GetFirstAsync(rm =>
                    rm.RoleId == roleMenu.RoleId && rm.MenuId == roleMenu.MenuId);

                if (existingRoleMenu == null)
                {
                    await _roleMenuRepository.CreateAsync(roleMenu);
                    insertCount++;
                    _logger.Info($"[创建] 角色菜单关联 - 角色:{role.RoleName}, 菜单:{menu.MenuName}");
                }
            }
        }

        // 3. 创建用户关联关系
        foreach (var user in allUsers)
        {
            // 3.1 创建用户-角色关联（一个用户一个角色）
            var role = allRoles.FirstOrDefault(r => r.RoleKey == "system_admin"); // 默认关联系统管理员角色
            if (role != null)
            {
                var existingUserRole = await _userRoleRepository.GetFirstAsync(ur =>
                    ur.UserId == user.Id);

                if (existingUserRole == null)
                {
                    var userRole = new HbtUserRole
                    {
                        Id = nextId++,
                        UserId = user.Id,
                        RoleId = role.Id,
                        CreateBy = "Hbt365",
                        CreateTime = DateTime.Now,
                        UpdateBy = "Hbt365",
                        UpdateTime = DateTime.Now
                    };

                    await _userRoleRepository.CreateAsync(userRole);
                    insertCount++;
                    _logger.Info($"[创建] 用户角色关联 - 用户:{user.UserName}, 角色:{role.RoleName}");
                }
                else
                {
                    existingUserRole.RoleId = role.Id;
                    existingUserRole.UpdateBy = "Hbt365";
                    existingUserRole.UpdateTime = DateTime.Now;
                    await _userRoleRepository.UpdateAsync(existingUserRole);
                    updateCount++;
                    _logger.Info($"[更新] 用户角色关联 - 用户:{user.UserName}, 角色:{role.RoleName}");
                }

                // 3.2 创建用户-部门关联（一个用户一个部门，根据角色分配）
                var depts = allDepts.Where(d =>
                {
                    // 系统管理员可以管理所有部门
                    if (role.RoleKey == "system_admin") return true;

                    // 安全管理员管理安全相关部门
                    if (role.RoleKey == "security_admin")
                        return d.DeptName.Contains("安全") || d.DeptName.Contains("IT");

                    // 业务管理员管理业务相关部门
                    if (role.RoleKey == "biz_manager")
                        return d.DeptName.Contains("业务") || d.DeptName.Contains("管理");

                    // 人事管理员管理人事相关部门
                    if (role.RoleKey == "hr_manager")
                        return d.DeptName.Contains("人事") || d.DeptName.Contains("人力资源");

                    // 财务管理员管理财务相关部门
                    if (role.RoleKey == "fin_manager")
                        return d.DeptName.Contains("财务") || d.DeptName.Contains("会计");

                    // 生产管理员管理生产相关部门
                    if (role.RoleKey == "prod_manager")
                        return d.DeptName.Contains("生产") || d.DeptName.Contains("制造");

                    // 品质管理员管理品质相关部门
                    if (role.RoleKey == "qc_manager")
                        return d.DeptName.Contains("品质") || d.DeptName.Contains("质量");

                    // 仓库管理员管理仓库相关部门
                    if (role.RoleKey == "wh_manager")
                        return d.DeptName.Contains("仓库") || d.DeptName.Contains("仓储");

                    // 库存管理员管理库存相关部门
                    if (role.RoleKey == "inv_manager")
                        return d.DeptName.Contains("库存") || d.DeptName.Contains("仓储");

                    // 采购管理员管理采购相关部门
                    if (role.RoleKey == "purchase_manager")
                        return d.DeptName.Contains("采购") || d.DeptName.Contains("购买");

                    // 供应商管理员管理供应商相关部门
                    if (role.RoleKey == "supplier_manager")
                        return d.DeptName.Contains("供应商") || d.DeptName.Contains("采购");

                    // 销售管理员管理销售相关部门
                    if (role.RoleKey == "sales_manager")
                        return d.DeptName.Contains("销售") || d.DeptName.Contains("营销");

                    // 客户管理员管理客户相关部门
                    if (role.RoleKey == "customer_manager")
                        return d.DeptName.Contains("客户") || d.DeptName.Contains("销售");

                    // 其他角色根据角色名称匹配对应的部门
                    return d.DeptName.Contains(role.RoleName.Replace("管理员", ""));
                }).ToList();

                var dept = depts.FirstOrDefault();
                if (dept != null)
                {
                    var existingUserDept = await _userDeptRepository.GetFirstAsync(ud =>
                        ud.UserId == user.Id);

                    if (existingUserDept == null)
                    {
                        var userDept = new HbtUserDept
                        {
                            Id = nextId++,
                            UserId = user.Id,
                            DeptId = dept.Id,
                            CreateBy = "Hbt365",
                            CreateTime = DateTime.Now,
                            UpdateBy = "Hbt365",
                            UpdateTime = DateTime.Now
                        };

                        await _userDeptRepository.CreateAsync(userDept);
                        insertCount++;
                        _logger.Info($"[创建] 用户部门关联 - 用户:{user.UserName}, 部门:{dept.DeptName}");
                    }
                    else
                    {
                        existingUserDept.DeptId = dept.Id;
                        existingUserDept.UpdateBy = "Hbt365";
                        existingUserDept.UpdateTime = DateTime.Now;
                        await _userDeptRepository.UpdateAsync(existingUserDept);
                        updateCount++;
                        _logger.Info($"[更新] 用户部门关联 - 用户:{user.UserName}, 部门:{dept.DeptName}");
                    }
                }
            }

            // 3.3 创建用户-岗位关联（一个用户一个岗位）
            var post = allPosts.FirstOrDefault(p => p.PostName.Contains("管理")); // 默认关联管理岗位
            if (post != null)
            {
                var existingUserPost = await _userPostRepository.GetFirstAsync(up =>
                    up.UserId == user.Id);

                if (existingUserPost == null)
                {
                    var userPost = new HbtUserPost
                    {
                        Id = nextId++,
                        UserId = user.Id,
                        PostId = post.Id,
                        CreateBy = "Hbt365",
                        CreateTime = DateTime.Now,
                        UpdateBy = "Hbt365",
                        UpdateTime = DateTime.Now
                    };

                    await _userPostRepository.CreateAsync(userPost);
                    insertCount++;
                    _logger.Info($"[创建] 用户岗位关联 - 用户:{user.UserName}, 岗位:{post.PostName}");
                }
                else
                {
                    existingUserPost.PostId = post.Id;
                    existingUserPost.UpdateBy = "Hbt365";
                    existingUserPost.UpdateTime = DateTime.Now;
                    await _userPostRepository.UpdateAsync(existingUserPost);
                    updateCount++;
                    _logger.Info($"[更新] 用户岗位关联 - 用户:{user.UserName}, 岗位:{post.PostName}");
                }
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户租户关联数据
    /// </summary>
    private async Task<(int, int)> InitializeUserTenantRelationsAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有用户和租户
        var users = await _userRepository.GetListAsync(u => u.IsDeleted == 0);
        var allTenants = await _tenantRepository.GetListAsync(t => t.IsDeleted == 0);
        var existingRelations = await _userTenantRepository.GetListAsync(ut => ut.IsDeleted == 0);

        foreach (var user in users)
        {
            if (user.UserName == "admin")
            {
                // admin用户关联到所有租户
                foreach (var tenant in allTenants)
                {
                    var existingRelation = existingRelations.FirstOrDefault(ut => 
                        ut.UserId == user.Id && ut.TenantId == tenant.Id);
                    
                    if (existingRelation == null)
                    {
                        var userTenantRelation = new HbtUserTenant
                        {
                            UserId = user.Id,
                            TenantId = tenant.Id,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        await _userTenantRepository.CreateAsync(userTenantRelation);
                        insertCount++;
                    }
                    else
                    {
                        existingRelation.UpdateTime = DateTime.Now;
                        await _userTenantRepository.UpdateAsync(existingRelation);
                        updateCount++;
                    }
                }
            }
            else
            {
                // 其他用户只关联到指定租户
                var existingRelation = existingRelations.FirstOrDefault(ut => 
                    ut.UserId == user.Id && ut.TenantId == tenantId);
                
                if (existingRelation == null)
                {
                    var userTenantRelation = new HbtUserTenant
                    {
                        UserId = user.Id,
                        TenantId = tenantId,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                    await _userTenantRepository.CreateAsync(userTenantRelation);
                    insertCount++;
                }
                else
                {
                    existingRelation.UpdateTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(existingRelation);
                    updateCount++;
                }
            }
        }

        return (insertCount, updateCount);
    }
}