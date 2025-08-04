//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 工作流程菜单数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 工作流程菜单数据初始化类
/// </summary>
public class HbtDbSeedWorkflowMenu
{
    /// <summary>
    /// 获取工作流程二级菜单列表
    /// </summary>
    /// <param name="parentId">父菜单ID</param>
    /// <returns>工作流程二级菜单列表</returns>
    public static List<HbtMenu> GetWorkflowSecondLevelMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "流程引擎", TransKey = "menu.workflow.engine._self", ParentId = parentId, OrderNum = 1, Path = "engine", Component = "", MenuType = 0, Perms = "workflow:engine", Icon = "SettingOutlined", Remark = "流程引擎菜单" },
            new HbtMenu { MenuName = "流程管理", TransKey = "menu.workflow.manage._self", ParentId = parentId, OrderNum = 2, Path = "manage", Component = "", MenuType = 0, Perms = "workflow:manage", Icon = "DeploymentUnitOutlined", Remark = "流程管理菜单" }
        };
    }

    /// <summary>
    /// 获取流程引擎三级菜单列表
    /// </summary>
    /// <param name="parentId">父菜单ID</param>
    /// <returns>流程引擎三级菜单列表</returns>
    public static List<HbtMenu> GetWorkflowEngineThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "流程监控", TransKey = "menu.workflow.engine.monitor", ParentId = parentId, OrderNum = 1, Path = "monitor", Component = "workflow/engine/monitor/index", MenuType = 1, Perms = "workflow:engine:monitor:list", Icon = "MonitorOutlined", Remark = "流程监控菜单" },
            new HbtMenu { MenuName = "待办任务", TransKey = "menu.workflow.engine.todo", ParentId = parentId, OrderNum = 2, Path = "todo", Component = "workflow/engine/todo/index", MenuType = 1, Perms = "workflow:engine:todo:list", Icon = "ClockCircleOutlined", Remark = "待办任务菜单" },
            new HbtMenu { MenuName = "已办任务", TransKey = "menu.workflow.engine.done", ParentId = parentId, OrderNum = 3, Path = "done", Component = "workflow/engine/done/index", MenuType = 1, Perms = "workflow:engine:done:list", Icon = "CheckCircleOutlined", Remark = "已办任务菜单" },
            new HbtMenu { MenuName = "流程审批", TransKey = "menu.workflow.engine.signoff", ParentId = parentId, OrderNum = 4, Path = "signoff", Component = "workflow/engine/signoff/index", MenuType = 1, Perms = "workflow:engine:signoff:list", Icon = "AuditOutlined", Remark = "流程审批菜单" },
            new HbtMenu { MenuName = "流程启动", TransKey = "menu.workflow.engine.execution", ParentId = parentId, OrderNum = 5, Path = "execution", Component = "workflow/engine/execution/index", MenuType = 1, Perms = "workflow:engine:execution:list", Icon = "PlayCircleOutlined", Remark = "流程启动菜单" },
            new HbtMenu { MenuName = "流程设计器", TransKey = "menu.workflow.engine.designer", ParentId = parentId, OrderNum = 6, Path = "designer", Component = "workflow/engine/designer/index", MenuType = 1, Perms = "workflow:engine:designer:list", Icon = "EditOutlined", Remark = "流程设计器菜单" }
        };
    }

    /// <summary>
    /// 获取流程管理三级菜单列表
    /// </summary>
    /// <param name="parentId">父菜单ID</param>
    /// <returns>流程管理三级菜单列表</returns>
    public static List<HbtMenu> GetWorkflowManageThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "表单管理", TransKey = "menu.workflow.manage.form", ParentId = parentId, OrderNum = 1, Path = "form", Component = "workflow/manage/form/index", MenuType = 1, Perms = "workflow:manage:form:list", Icon = "FormOutlined", Remark = "表单管理菜单" },
            new HbtMenu { MenuName = "流程方案", TransKey = "menu.workflow.manage.scheme", ParentId = parentId, OrderNum = 2, Path = "scheme", Component = "workflow/manage/scheme/index", MenuType = 1, Perms = "workflow:manage:scheme:list", Icon = "DeploymentUnitOutlined", Remark = "流程方案菜单" },
            new HbtMenu { MenuName = "流程实例", TransKey = "menu.workflow.manage.instance", ParentId = parentId, OrderNum = 3, Path = "instance", Component = "workflow/manage/instance/index", MenuType = 1, Perms = "workflow:manage:instance:list", Icon = "ApartmentOutlined", Remark = "流程实例菜单" },
            new HbtMenu { MenuName = "实例操作", TransKey = "menu.workflow.manage.oper", ParentId = parentId, OrderNum = 4, Path = "oper", Component = "workflow/manage/oper/index", MenuType = 1, Perms = "workflow:manage:oper:list", Icon = "NodeIndexOutlined", Remark = "实例操作菜单" },
            new HbtMenu { MenuName = "实例流转", TransKey = "menu.workflow.manage.trans", ParentId = parentId, OrderNum = 5, Path = "trans", Component = "workflow/manage/trans/index", MenuType = 1, Perms = "workflow:manage:trans:list", Icon = "CheckSquareOutlined", Remark = "实例流转菜单" }
        };
    }
}