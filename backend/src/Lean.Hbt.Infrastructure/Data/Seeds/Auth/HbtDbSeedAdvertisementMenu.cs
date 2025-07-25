#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedAdvertisementMenu.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 广告菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 广告菜单数据初始化类
/// </summary>
public class HbtDbSeedAdvertisementMenu
{
    /// <summary>
    /// 获取广告管理二级菜单列表
    /// </summary>
    /// <param name="parentId">父菜单ID</param>
    /// <returns>广告管理二级菜单列表</returns>
    public static List<HbtMenu> GetAdvertisementSecondMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu 
            { 
                MenuName = "横幅广告", 
                TransKey = "menu.advertisement.banner", 
                ParentId = parentId, 
                OrderNum = 1, 
                Path = "banner", 
                Component = "advertisement/banner/index", 
                MenuType = 1, 
                Perms = "advertisement:banner", 
                Icon = "PictureOutlined", 
                Remark = "横幅广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "弹窗广告", 
                TransKey = "menu.advertisement.popup", 
                ParentId = parentId, 
                OrderNum = 2, 
                Path = "popup", 
                Component = "advertisement/popup/index", 
                MenuType = 1, 
                Perms = "advertisement:popup", 
                Icon = "NotificationOutlined", 
                Remark = "弹窗广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "插屏广告", 
                TransKey = "menu.advertisement.interstitial", 
                ParentId = parentId, 
                OrderNum = 3, 
                Path = "interstitial", 
                Component = "advertisement/interstitial/index", 
                MenuType = 1, 
                Perms = "advertisement:interstitial", 
                Icon = "FullscreenOutlined", 
                Remark = "插屏广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "浮动广告", 
                TransKey = "menu.advertisement.floating", 
                ParentId = parentId, 
                OrderNum = 4, 
                Path = "floating", 
                Component = "advertisement/floating/index", 
                MenuType = 1, 
                Perms = "advertisement:floating", 
                Icon = "FloatOutlined", 
                Remark = "浮动广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "悬浮广告", 
                TransKey = "menu.advertisement.overlay", 
                ParentId = parentId, 
                OrderNum = 5, 
                Path = "overlay", 
                Component = "advertisement/overlay/index", 
                MenuType = 1, 
                Perms = "advertisement:overlay", 
                Icon = "CoverOutlined", 
                Remark = "覆盖广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "角标广告", 
                TransKey = "menu.advertisement.corner", 
                ParentId = parentId, 
                OrderNum = 6, 
                Path = "corner", 
                Component = "advertisement/corner/index", 
                MenuType = 1, 
                Perms = "advertisement:corner", 
                Icon = "CornerOutlined", 
                Remark = "角落广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "开屏广告", 
                TransKey = "menu.advertisement.splash", 
                ParentId = parentId, 
                OrderNum = 7, 
                Path = "splash", 
                Component = "advertisement/splash/index", 
                MenuType = 1, 
                Perms = "advertisement:splash", 
                Icon = "SplashOutlined", 
                Remark = "开屏广告管理页面" 
            },
            new HbtMenu 
            { 
                MenuName = "广告计费", 
                TransKey = "menu.advertisement.billing", 
                ParentId = parentId, 
                OrderNum = 8, 
                Path = "billing", 
                Component = "advertisement/billing/index", 
                MenuType = 1, 
                Perms = "advertisement:billing", 
                Icon = "AccountBookOutlined", 
                Remark = "广告计费管理页面" 
            }
        };
    }
} 