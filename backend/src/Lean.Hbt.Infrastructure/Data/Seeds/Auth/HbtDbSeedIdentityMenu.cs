//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedIdentityMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 身份认证菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 身份认证菜单数据初始化类
/// </summary>
public class HbtDbSeedIdentityMenu
{
    /// <summary>
    /// 获取身份认证子菜单
    /// </summary>
    public static List<HbtMenu> GetIdentityThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "租户管理",
                TransKey = "menu.identity.tenant",
                ParentId = parentId,
                OrderNum = 1,
                Path = "tenant",
                Component = "identity/tenant/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:tenant:list",
                Icon = "ShopOutlined",
                Remark = "租户管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "用户管理",
                TransKey = "menu.identity.user",
                ParentId = parentId,
                OrderNum = 2,
                Path = "user",
                Component = "identity/user/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:user:list",
                Icon = "UserOutlined",
                Remark = "用户管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "OAuth管理",
                TransKey = "menu.identity.oauth",
                ParentId = parentId,
                OrderNum = 3,
                Path = "oauth",
                Component = "identity/oauth/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:oauth:list",
                Icon = "ApiOutlined",
                Remark = "OAuth第三方登录管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "菜单管理",
                TransKey = "menu.identity.menu",
                ParentId = parentId,
                OrderNum = 4,
                Path = "menu",
                Component = "identity/menu/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:menu:list",
                Icon = "MenuOutlined",
                Remark = "菜单管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "部门管理",
                TransKey = "menu.identity.dept",
                ParentId = parentId,
                OrderNum = 5,
                Path = "dept",
                Component = "identity/dept/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:dept:list",
                Icon = "ApartmentOutlined",
                Remark = "部门管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "角色管理",
                TransKey = "menu.identity.role",
                ParentId = parentId,
                OrderNum = 6,
                Path = "role",
                Component = "identity/role/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:role:list",
                Icon = "TeamOutlined",
                Remark = "角色管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "岗位管理",
                TransKey = "menu.identity.post",
                ParentId = parentId,
                OrderNum = 7,
                Path = "post",
                Component = "identity/post/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "identity:post:list",
                Icon = "IdcardOutlined",
                Remark = "岗位管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
} 