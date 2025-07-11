//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 工作流程菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 工作流程菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 工作流程菜单数据初始化类
/// </summary>
public class HbtDbSeedWorkflowMenu
{
    /// <summary>
    /// 获取工作流程子菜单列表
    /// </summary>
    public static List<HbtMenu> GetWorkflowThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "流程总览",
                TransKey = "menu.workflow.overview",
                ParentId = parentId,
                OrderNum = 1,
                Path = "overview",
                Component = "workflow/manager/index",
                MenuType = 1,
                Perms = "workflow:overview:list",
                Icon = "AppstoreOutlined",
                Remark = "流程总览菜单"
            },
            new HbtMenu
            {
                MenuName = "我的流程",
                TransKey = "menu.workflow.my",
                ParentId = parentId,
                OrderNum = 2,
                Path = "my",
                Component = "workflow/manager/my",
                MenuType = 1,
                Perms = "workflow:my:list",
                Icon = "UserOutlined",
                Remark = "我的流程菜单"
            },
            new HbtMenu
            {
                MenuName = "表单管理",
                TransKey = "menu.workflow.form",
                ParentId = parentId,
                OrderNum = 3,
                Path = "form",
                Component = "workflow/form/index",
                MenuType = 1,
                Perms = "workflow:form:list",
                Icon = "FormOutlined",
                Remark = "表单管理菜单"
            },
            new HbtMenu
            {
                MenuName = "流程定义",
                TransKey = "menu.workflow.definition",
                ParentId = parentId,
                OrderNum = 4,
                Path = "definition",
                Component = "workflow/definition/index",
                MenuType = 1,
                Perms = "workflow:definition:list",
                Icon = "DeploymentUnitOutlined",
                Remark = "流程定义菜单"
            },
            new HbtMenu
            {
                MenuName = "流程实例",
                TransKey = "menu.workflow.instance",
                ParentId = parentId,
                OrderNum = 5,
                Path = "instance",
                Component = "workflow/instance/index",
                MenuType = 1,
                Perms = "workflow:instance:list",
                Icon = "ApartmentOutlined",
                Remark = "流程实例菜单"
            },
            new HbtMenu
            {
                MenuName = "工作任务",
                TransKey = "menu.workflow.task",
                ParentId = parentId,
                OrderNum = 6,
                Path = "task",
                Component = "workflow/task/index",
                MenuType = 1,
                Perms = "workflow:task:list",
                Icon = "CheckSquareOutlined",
                Remark = "工作任务菜单"
            },
            new HbtMenu
            {
                MenuName = "流程节点",
                TransKey = "menu.workflow.node",
                ParentId = parentId,
                OrderNum = 7,
                Path = "node",
                Component = "workflow/node/index",
                MenuType = 1,
                Perms = "workflow:node:list",
                Icon = "NodeIndexOutlined",
                Remark = "流程节点菜单"
            },
            new HbtMenu
            {
                MenuName = "流程变量",
                TransKey = "menu.workflow.variable",
                ParentId = parentId,
                OrderNum = 8,
                Path = "variable",
                Component = "workflow/variable/index",
                MenuType = 1,
                Perms = "workflow:variable:list",
                Icon = "FieldBinaryOutlined",
                Remark = "流程变量菜单"
            },
            new HbtMenu
            {
                MenuName = "流程历史",
                TransKey = "menu.workflow.history",
                ParentId = parentId,
                OrderNum = 9,
                Path = "history",
                Component = "workflow/history/index",
                MenuType = 1,
                Perms = "workflow:history:list",
                Icon = "HistoryOutlined",
                Remark = "流程历史菜单"
            }
        };
    }
}