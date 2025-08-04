//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGeneratorMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGeneratorMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成菜单数据初始化类
//===================================================================

using Hbt.Domain.Entities.Identity;

namespace Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 代码生成菜单数据初始化类
/// </summary>
public class HbtDbSeedGeneratorMenu
{
    /// <summary>
    /// 获取代码生成子菜单列表
    /// </summary>
    public static List<HbtMenu> GetGeneratorThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "生成配置",
                TransKey = "menu.generator.config",
                ParentId = parentId,
                OrderNum = 1,
                Path = "config",
                Component = "generator/config/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:config:list",
                Icon = "SettingOutlined",
                Remark = "生成配置管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "生成模板",
                TransKey = "menu.generator.template",
                ParentId = parentId,
                OrderNum = 2,
                Path = "template",
                Component = "generator/template/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:template:list",
                Icon = "FileTextOutlined",
                Remark = "代码模板管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "库表管理",
                TransKey = "menu.generator.table",
                ParentId = parentId,
                OrderNum = 3,
                Path = "table",
                Component = "generator/table/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:table:list",
                Icon = "TableOutlined",
                Remark = "数据库表管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "库表定义",
                TransKey = "menu.generator.tableDefine",
                ParentId = parentId,
                OrderNum = 4,
                Path = "tableDefine",
                Component = "generator/tableDefine/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:tabledefine:list",
                Icon = "TableOutlined",
                Remark = "数据库表定义管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "API文档",
                TransKey = "menu.generator.api",
                ParentId = parentId,
                OrderNum = 5,
                Path = "api",
                Component = "generator/api/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:api:list",
                Icon = "ApiOutlined",
                Remark = "API文档管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}