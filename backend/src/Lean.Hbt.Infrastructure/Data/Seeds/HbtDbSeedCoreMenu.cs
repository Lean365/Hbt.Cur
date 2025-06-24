//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCoreMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 核心功能菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 核心功能菜单数据初始化类
/// </summary>
public class HbtDbSeedCoreMenu
{
    /// <summary>
    /// 获取系统管理子菜单列表
    /// </summary>
    public static List<HbtMenu> GetCoreThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "系统配置",
                TransKey = "menu.core.config",
                ParentId = parentId,
                OrderNum = 1,
                Path = "configs",
                Component = "core/configs/index",
                MenuType = 1,
                Perms = "core:config:list",
                Icon = "ToolOutlined",
                Remark = "系统配置菜单"
            },
            new HbtMenu
            {
                MenuName = "语言管理",
                TransKey = "menu.core.language",
                ParentId = parentId,
                OrderNum = 2,
                Path = "language",
                Component = "core/language/index",
                MenuType = 1,
                Perms = "core:language:list",
                Icon = "TranslationOutlined",
                Remark = "语言管理菜单"
            },
            new HbtMenu
            {
                MenuName = "字典管理",
                TransKey = "menu.core.dict",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dict",
                Component = "core/dict/index",
                MenuType = 1,
                Perms = "core:dict:list",
                Icon = "BookOutlined",
                Remark = "字典管理菜单"
            }
        };
    }
} 