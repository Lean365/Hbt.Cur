//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedAuditMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 审计日志菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedAuditMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 审计日志菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 审计日志菜单数据初始化类
/// </summary>
public class HbtDbSeedAuditMenu
{
    /// <summary>
    /// 获取审计日志子菜单列表
    /// </summary>
    public static List<HbtMenu> GetAuditThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "操作日志",
                TransKey = "menu.audit.operlog",
                ParentId = parentId,
                OrderNum = 1,
                Path = "operlog",
                Component = "audit/operlog/index",
                MenuType = 1,
                Perms = "audit:operlog:list",
                Icon = "HistoryOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "登录日志",
                TransKey = "menu.audit.loginlog",
                ParentId = parentId,
                OrderNum = 2,
                Path = "loginlog",
                Component = "audit/loginlog/index",
                MenuType = 1,
                Perms = "audit:auditloginlog:list",
                Icon = "LoginOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "差异日志",
                TransKey = "menu.audit.sqldifflog",
                ParentId = parentId,
                OrderNum = 3,
                Path = "sqldifflog",
                Component = "audit/sqldifflog/index",
                MenuType = 1,
                Perms = "audit:sqldifflog:list",
                Icon = "DiffOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "异常日志",
                TransKey = "menu.audit.exceptionlog",
                ParentId = parentId,
                OrderNum = 4,
                Path = "exceptionlog",
                Component = "audit/exceptionlog/index",
                MenuType = 1,
                Perms = "audit:exceptionlog:list",
                Icon = "ExceptionOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
             new HbtMenu
            {
                MenuName = "任务日志",
                TransKey = "menu.audit.quartzlog",
                ParentId = parentId,
                OrderNum = 5,
                Path = "quartzlog",
                Component = "audit/quartzlog/index",
                MenuType = 1,
                Perms = "audit:quartzlog:list",
                Icon = "ExceptionOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "服务器监控",
                TransKey = "menu.audit.server",
                ParentId = parentId,
                OrderNum = 6,
                Path = "server",
                Component = "audit/server/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "audit:server:list",
                Icon = "DashboardOutlined",
                Remark = "服务器监控菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
} 