//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRelation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 关联关系数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;
using System.Threading.Tasks;

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
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtRepository<HbtRole> _roleRepository;
    private readonly IHbtRepository<HbtPost> _postRepository;
    private readonly IHbtRepository<HbtDept> _deptRepository;
    private readonly IHbtRepository<HbtMenu> _menuRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedRelation(
        IHbtRepository<HbtUserRole> userRoleRepository,
        IHbtRepository<HbtUserPost> userPostRepository,
        IHbtRepository<HbtUserDept> userDeptRepository,
        IHbtRepository<HbtRoleMenu> roleMenuRepository,
        IHbtRepository<HbtUser> userRepository,
        IHbtRepository<HbtRole> roleRepository,
        IHbtRepository<HbtPost> postRepository,
        IHbtRepository<HbtDept> deptRepository,
        IHbtRepository<HbtMenu> menuRepository,
        IHbtLogger logger)
    {
        _userRoleRepository = userRoleRepository;
        _userPostRepository = userPostRepository;
        _userDeptRepository = userDeptRepository;
        _roleMenuRepository = roleMenuRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _postRepository = postRepository;
        _deptRepository = deptRepository;
        _menuRepository = menuRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化关联关系数据
    /// </summary>
    public async Task<(int, int)> InitializeRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 1. 获取默认用户、角色、岗位、部门
        var adminUser = await _userRepository.GetInfoAsync(u => u.UserName == "admin");
        var adminRole = await _roleRepository.GetInfoAsync(r => r.RoleKey == "admin");
        var gmPost = await _postRepository.GetInfoAsync(p => p.PostCode == "GM");
        var rootDept = await _deptRepository.GetInfoAsync(d => d.ParentId == 0);

        if (adminUser == null || adminRole == null || gmPost == null || rootDept == null)
        {
            _logger.Error("未找到必要的基础数据，无法初始化关联关系");
            return (0, 0);
        }

        // 2. 初始化用户-角色关联
        var userRole = await _userRoleRepository.GetInfoAsync(ur => 
            ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id);

        if (userRole == null)
        {
            await _userRoleRepository.CreateAsync(new HbtUserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户角色关联 - 用户:{adminUser.UserName}, 角色:{adminRole.RoleName}");
        }

        // 3. 初始化用户-岗位关联
        var userPost = await _userPostRepository.GetInfoAsync(up => 
            up.UserId == adminUser.Id && up.PostId == gmPost.Id);

        if (userPost == null)
        {
            await _userPostRepository.CreateAsync(new HbtUserPost
            {
                UserId = adminUser.Id,
                PostId = gmPost.Id,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户岗位关联 - 用户:{adminUser.UserName}, 岗位:{gmPost.PostName}");
        }

        // 4. 初始化用户-部门关联
        var userDept = await _userDeptRepository.GetInfoAsync(ud => 
            ud.UserId == adminUser.Id && ud.DeptId == rootDept.Id);

        if (userDept == null)
        {
            await _userDeptRepository.CreateAsync(new HbtUserDept
            {
                UserId = adminUser.Id,
                DeptId = rootDept.Id,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            });
            insertCount++;
            _logger.Info($"[创建] 用户部门关联 - 用户:{adminUser.UserName}, 部门:{rootDept.DeptName}");
        }

        // 5. 初始化角色-菜单关联
        var allMenus = await _menuRepository.GetListAsync(m => m.IsDeleted == 0);
        foreach (var menu in allMenus)
        {
            var roleMenu = await _roleMenuRepository.GetInfoAsync(rm => 
                rm.RoleId == adminRole.Id && rm.MenuId == menu.Id);

            if (roleMenu == null)
            {
                await _roleMenuRepository.CreateAsync(new HbtRoleMenu
                {
                    RoleId = adminRole.Id,
                    MenuId = menu.Id,
                    CreateBy = "system",
                    CreateTime = DateTime.Now,
                    UpdateBy = "system",
                    UpdateTime = DateTime.Now
                });
                insertCount++;
                _logger.Info($"[创建] 角色菜单关联 - 角色:{adminRole.RoleName}, 菜单:{menu.MenuName}");
            }
        }

        return (insertCount, updateCount);
    }
} 