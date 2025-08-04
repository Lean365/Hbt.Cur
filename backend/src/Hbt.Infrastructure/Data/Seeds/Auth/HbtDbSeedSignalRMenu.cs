//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSignalRMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 实时通信菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSignalRMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 实时通信菜单数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 实时通信菜单数据初始化类
/// </summary>
public class HbtDbSeedSignalRMenu
{
    /// <summary>
    /// 获取实时监控子菜单
    /// </summary>
    public static List<HbtMenu> GetSignalrThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "在线用户",
                TransKey = "menu.signalr.online",
                ParentId = parentId,
                OrderNum = 2,
                Path = "online",
                Component = "signalr/online/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "signalr:online:list",
                Icon = "TeamOutlined",
                Remark = "在线用户菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "在线消息",
                TransKey = "menu.signalr.message",
                ParentId = parentId,
                OrderNum = 3,
                Path = "message",
                Component = "signalr/message/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "signalr:message:list",
                Icon = "MessageOutlined",
                Remark = "在线消息菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
} 