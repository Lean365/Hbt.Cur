//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 菜单数据初始化类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 菜单数据初始化类
/// </summary>
public class HbtDbSeedMenu
{
    private readonly IHbtRepository<HbtMenu> _menuRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedMenu(IHbtRepository<HbtMenu> menuRepository, IHbtLogger logger)
    {
        _menuRepository = menuRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化菜单数据
    /// </summary>
    public async Task<(int, int)> InitializeMenuAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        var menuNameToId = new Dictionary<string, long>();

        // 1. 初始化顶级菜单
        var (topInsertCount, topUpdateCount) = await InitializeTopMenusAsync(menuNameToId);
        insertCount += topInsertCount;
        updateCount += topUpdateCount;

        // 2. 初始化子菜单
        var (subInsertCount, subUpdateCount) = await InitializeSubMenusAsync(menuNameToId);
        insertCount += subInsertCount;
        updateCount += subUpdateCount;

        // 3. 初始化按钮
        var (btnInsertCount, btnUpdateCount) = await InitializeButtonsAsync(menuNameToId);
        insertCount += btnInsertCount;
        updateCount += btnUpdateCount;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化顶级菜单
    /// </summary>
    private async Task<(int, int)> InitializeTopMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var topMenus = new List<HbtMenu>
    {
        new HbtMenu
        {
            MenuName = "日常办公",
            TransKey = "menu.routine._self",
            ParentId = 0,
            OrderNum = 1,
            Path = "routine",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "FileTextOutlined",
            TenantId = 0,
            Remark = "日常办公目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "财务管理",
            TransKey = "menu.finance._self",
            ParentId = 0,
            OrderNum = 2,
            Path = "finance",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "AccountBookOutlined",
            TenantId = 0,
            Remark = "财务管理目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "后勤管理",
            TransKey = "menu.logistics._self",
            ParentId = 0,
            OrderNum = 3,
            Path = "logistics",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "ClusterOutlined",
            TenantId = 0,
            Remark = "后勤管理目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "工作流程",
            TransKey = "menu.workflow._self",
            ParentId = 0,
            OrderNum = 4,
            Path = "workflow",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "DeploymentUnitOutlined",
            TenantId = 0,
            Remark = "工作流程目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "身份认证",
            TransKey = "menu.identity._self",
            ParentId = 0,
            OrderNum = 5,
            Path = "identity",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "UserOutlined",
            TenantId = 0,
            Remark = "身份认证目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "系统管理",
            TransKey = "menu.admin._self",
            ParentId = 0,
            OrderNum = 6,
            Path = "Hbt365",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "SettingOutlined",
            TenantId = 0,
            Remark = "系统管理目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "代码生成",
            TransKey = "menu.generator._self",
            ParentId = 0,
            OrderNum = 7,
            Path = "generator",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "CodeOutlined",
            TenantId = 0,
            Remark = "代码生成目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "审计日志",
            TransKey = "menu.audit._self",
            ParentId = 0,
            OrderNum = 8,
            Path = "audit",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "AuditOutlined",
            TenantId = 0,
            Remark = "审计日志目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "实时监控",
            TransKey = "menu.signalr._self",
            ParentId = 0,
            OrderNum = 9,
            Path = "signalr",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "DashboardOutlined",
            TenantId = 0,
            Remark = "实时监控目录",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        }
    };

        foreach (var menu in topMenus)
        {
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, true);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化子菜单
    /// </summary>
    private async Task<(int, int)> InitializeSubMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var subMenus = new List<HbtMenu>();

        // 从数据库读取所有顶级菜单
        var topMenus = await _menuRepository.GetListAsync(m => m.ParentId == 0);
        var topMenuDict = topMenus.ToDictionary(m => m.MenuName, m => m.Id);

        // 1. 日常办公子菜单
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            var routineSubMenus = GetRoutineSubMenus(routineMenu.Id);
            subMenus.AddRange(routineSubMenus);
        }

        // 2. 财务管理子菜单
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            var financeSubMenus = GetFinanceSubMenus(financeMenu.Id);
            subMenus.AddRange(financeSubMenus);
        }

        // 3. 后勤管理子菜单
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            var logisticsSubMenus = GetLogisticsSubMenus(logisticsMenu.Id);
            subMenus.AddRange(logisticsSubMenus);
        }

        // 4. 工作流程子菜单
        var workflowMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            var workflowSubMenus = GetWorkflowSubMenus(workflowMenu.Id);
            subMenus.AddRange(workflowSubMenus);
        }

        // 5. 身份认证子菜单
        var identityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            var identitySubMenus = GetIdentitySubMenus(identityMenu.Id);
            subMenus.AddRange(identitySubMenus);
        }

        // 6. 系统管理子菜单
        var adminMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "系统管理" && m.ParentId == 0);
        if (adminMenu != null)
        {
            var adminSubMenus = GetSystemManagementSubMenus(adminMenu.Id);
            subMenus.AddRange(adminSubMenus);
        }

        // 7. 代码生成子菜单
        var generatorMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            var generatorSubMenus = GetGeneratorSubMenus(generatorMenu.Id);
            subMenus.AddRange(generatorSubMenus);
        }

        // 8. 审计日志子菜单
        var auditMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            var auditSubMenus = GetAuditSubMenus(auditMenu.Id);
            subMenus.AddRange(auditSubMenus);
        }

        // 9. 实时监控子菜单
        var realtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "实时监控" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            var realtimeSubMenus = GetRealtimeSubMenus(realtimeMenu.Id);
            subMenus.AddRange(realtimeSubMenus);
        }

        foreach (var menu in subMenus)
        {
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化按钮
    /// </summary>
    private async Task<(int, int)> InitializeButtonsAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有菜单类型的菜单
        var menus = await _menuRepository.GetListAsync(m => m.MenuType == 1);

        foreach (var menu in menus)
        {
            if (!string.IsNullOrEmpty(menu.Perms))
            {
                var modulePrefix = menu.Perms.Split(':')[0];
                var buttons = GetMenuButtons(menu, modulePrefix);

                foreach (var button in buttons)
                {
                    button.ParentId = menu.Id; // 使用数据库中的菜单ID
                    var (isNew, savedButton) = await CreateOrUpdateMenuAsync(button, false);

                    if (isNew) insertCount++;
                    else updateCount++;

                    _logger.Info($"[{(isNew ? "创建" : "更新")}] 按钮 '{button.MenuName}' (ParentId: {button.ParentId}) {(isNew ? "创建" : "更新")}成功");
                }
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新菜单
    /// </summary>
    private async Task<(bool isNew, HbtMenu menu)> CreateOrUpdateMenuAsync(HbtMenu menu, bool isTopLevel)
    {
        Expression<Func<HbtMenu, bool>> query;
        if (isTopLevel)
        {
            query = m => m.MenuName == menu.MenuName && m.ParentId == 0;
        }
        else
        {
            query = m => m.MenuName == menu.MenuName && m.ParentId == menu.ParentId;
        }

        var existingMenu = await _menuRepository.GetFirstAsync(query);

        if (existingMenu == null)
        {
            await _menuRepository.CreateAsync(menu);
            _logger.Info($"[创建] {(isTopLevel ? "顶级" : "")}菜单 '{menu.MenuName}' (ParentId: {menu.ParentId}) 创建成功");
            return (true, menu);
        }

        // 更新菜单属性，保持ParentId不变
        existingMenu.MenuName = menu.MenuName;
        existingMenu.TransKey = menu.TransKey;
        existingMenu.OrderNum = menu.OrderNum;
        existingMenu.Path = menu.Path;
        existingMenu.Component = menu.Component;
        existingMenu.QueryParams = menu.QueryParams;
        existingMenu.IsExternal = menu.IsExternal;
        existingMenu.IsCache = menu.IsCache;
        existingMenu.MenuType = menu.MenuType;
        existingMenu.Visible = menu.Visible;
        existingMenu.Status = menu.Status;
        existingMenu.Perms = menu.Perms;
        existingMenu.Icon = menu.Icon;
        existingMenu.TenantId = menu.TenantId;
        existingMenu.Remark = menu.Remark;
        existingMenu.UpdateBy = "Hbt365";
        existingMenu.UpdateTime = DateTime.Now;

        await _menuRepository.UpdateAsync(existingMenu);
        _logger.Info($"[更新] {(isTopLevel ? "顶级" : "")}菜单 '{existingMenu.MenuName}' (ParentId: {existingMenu.ParentId}) 更新成功");
        return (false, existingMenu);
    }

    /// <summary>
    /// 获取系统管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetSystemManagementSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "系统配置",
                TransKey = "menu.admin.config",
                ParentId = parentId,
                OrderNum = 1,
                Path = "config",
                Component = "admin/config/index",
                MenuType = 1,
                Perms = "admin:config:list",
                Icon = "ToolOutlined",
                Remark = "系统配置菜单"
            },
            new HbtMenu
            {
                MenuName = "语言管理",
                TransKey = "menu.admin.language",
                ParentId = parentId,
                OrderNum = 2,
                Path = "language",
                Component = "admin/language/index",
                MenuType = 1,
                Perms = "admin:language:list",
                Icon = "TranslationOutlined",
                Remark = "语言管理菜单"
            },
            new HbtMenu
            {
                MenuName = "字典类型",
                TransKey = "menu.admin.dicttype",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dicttype",
                Component = "admin/dicttype/index",
                MenuType = 1,
                Perms = "admin:dicttype:list",
                Icon = "BookOutlined",
                Remark = "字典类型菜单"
            },
            new HbtMenu
            {
                MenuName = "字典数据",
                TransKey = "menu.admin.dictdata",
                ParentId = parentId,
                OrderNum = 4,
                Path = "dictdata",
                Component = "admin/dictdata/index",
                MenuType = 1,
                Perms = "admin:dictdata:list",
                Icon = "DatabaseOutlined",
                Remark = "字典数据菜单"
            },
            new HbtMenu
            {
                MenuName = "翻译管理",
                TransKey = "menu.admin.translation",
                ParentId = parentId,
                OrderNum = 5,
                Path = "translation",
                Component = "admin/translation/index",
                MenuType = 1,
                Perms = "admin:translation:list",
                Icon = "GlobalOutlined",
                Remark = "翻译管理菜单"
            }
        };
    }

    /// <summary>
    /// 获取身份认证子菜单
    /// </summary>
    private List<HbtMenu> GetIdentitySubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "用户管理",
                TransKey = "menu.identity.user",
                ParentId = parentId,
                OrderNum = 1,
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
                TenantId = 0,
                Remark = "用户管理菜单",
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
                OrderNum = 2,
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
                TenantId = 0,
                Remark = "角色管理菜单",
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
                OrderNum = 3,
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
                TenantId = 0,
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
                OrderNum = 4,
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
                TenantId = 0,
                Remark = "部门管理菜单",
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
                OrderNum = 5,
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
                TenantId = 0,
                Remark = "岗位管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "租户管理",
                TransKey = "menu.identity.tenant",
                ParentId = parentId,
                OrderNum = 6,
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
                TenantId = 0,
                Remark = "租户管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取审计日志子菜单列表
    /// </summary>
    private List<HbtMenu> GetAuditSubMenus(long parentId)
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
                TransKey = "menu.audit.dbdifflog",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dbdifflog",
                Component = "audit/dbdifflog/index",
                MenuType = 1,
                Perms = "audit:difflog:list",
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
                MenuName = "审计日志",
                TransKey = "menu.audit.auditlog",
                ParentId = parentId,
                OrderNum = 6,
                Path = "auditlog",
                Component = "audit/auditlog/index",
                MenuType = 1,
                Perms = "audit:log:list",
                Icon = "AuditOutlined",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "服务器监控",
                TransKey = "menu.signalr.server",
                ParentId = parentId,
                OrderNum = 1,
                Path = "server",
                Component = "signalr/server/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "signalr:server:list",
                Icon = "DashboardOutlined",
                TenantId = 0,
                Remark = "服务器监控菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
        };
    }

    /// <summary>
    /// 获取工作流程子菜单列表
    /// </summary>
    private List<HbtMenu> GetWorkflowSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "流程定义",
                TransKey = "menu.workflow.definition",
                ParentId = parentId,
                OrderNum = 1,
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
                OrderNum = 2,
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
                OrderNum = 3,
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
                OrderNum = 4,
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
                OrderNum = 5,
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
                OrderNum = 6,
                Path = "history",
                Component = "workflow/history/index",
                MenuType = 1,
                Perms = "workflow:history:list",
                Icon = "HistoryOutlined",
                Remark = "流程历史菜单"
            }
        };
    }

    /// <summary>
    /// 获取实时监控子菜单
    /// </summary>
    private List<HbtMenu> GetRealtimeSubMenus(long parentId)
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
                TenantId = 0,
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
                TenantId = 0,
                Remark = "在线消息菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取代码生成子菜单列表
    /// </summary>
    private List<HbtMenu> GetGeneratorSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "数据库表",
                TransKey = "menu.generator.table",
                ParentId = parentId,
                OrderNum = 1,
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
                TenantId = 0,
                Remark = "数据库表管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "数据库表定义",
                TransKey = "menu.generator.tableDefine",
                ParentId = parentId,
                OrderNum = 2,
                Path = "tableDefine",
                Component = "generator/tableDefine/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                Status = 0,
                Perms = "generator:tableDefine:list",
                Icon = "TableOutlined",
                TenantId = 0,
                Remark = "数据库表定义管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "代码模板",
                TransKey = "menu.generator.template",
                ParentId = parentId,
                OrderNum = 3,
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
                TenantId = 0,
                Remark = "代码模板管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "生成配置",
                TransKey = "menu.generator.config",
                ParentId = parentId,
                OrderNum = 4,
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
                TenantId = 0,
                Remark = "生成配置管理菜单",
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
                TenantId = 0,
                Remark = "API文档管理菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取日常办公子菜单列表
    /// </summary>
    private List<HbtMenu> GetRoutineSubMenus(long parentId)
    {
        return new List<HbtMenu>
    {
        new HbtMenu
        {
            MenuName = "文件管理",
            TransKey = "menu.routine.file",
            ParentId = parentId,
            OrderNum = 1,
            Path = "file",
            Component = "routine/file/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:file:list",
            Icon = "FileOutlined",
            TenantId = 0,
            Remark = "文件管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "邮件管理",
            TransKey = "menu.routine.mail",
            ParentId = parentId,
            OrderNum = 2,
            Path = "mail",
            Component = "routine/mail/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:mail:list",
            Icon = "MailOutlined",
            TenantId = 0,
            Remark = "邮件管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "邮件模板",
            TransKey = "menu.routine.mailTmpl",
            ParentId = parentId,
            OrderNum = 3,
            Path = "mailTmpl",
            Component = "routine/mailTmpl/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:mailTmpl:list",
            Icon = "FileTextOutlined",
            TenantId = 0,
            Remark = "邮件模板菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "通知公告",
            TransKey = "menu.routine.notice",
            ParentId = parentId,
            OrderNum = 4,
            Path = "notice",
            Component = "routine/notice/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:notice:list",
            Icon = "NotificationOutlined",
            TenantId = 0,
            Remark = "通知公告菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "工作任务",
            TransKey = "menu.routine.quartztask",
            ParentId = parentId,
            OrderNum = 5,
            Path = "quartztask",
            Component = "routine/quartztask/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:quartztask:list",
            Icon = "CheckSquareOutlined",
            TenantId = 0,
            Remark = "工作任务菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "日程管理",
            TransKey = "menu.routine.schedule",
            ParentId = parentId,
            OrderNum = 6,
            Path = "schedule",
            Component = "routine/schedule/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:schedule:list",
            Icon = "CalendarOutlined",
            TenantId = 0,
            Remark = "日程管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        }
    };
    }

    /// <summary>
    /// 获取财务管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetFinanceSubMenus(long parentId)
    {
        return new List<HbtMenu>
    {
        new HbtMenu
        {
            MenuName = "管理会计",
            TransKey = "menu.finance.management",
            ParentId = parentId,
            OrderNum = 1,
            Path = "management",
            Component = "finance/management/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "finance:management:list",
            Icon = "FundOutlined",
            TenantId = 0,
            Remark = "管理会计菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "财务会计",
            TransKey = "menu.finance.financial",
            ParentId = parentId,
            OrderNum = 2,
            Path = "financial",
            Component = "finance/financial/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "finance:financial:list",
            Icon = "AccountBookOutlined",
            TenantId = 0,
            Remark = "财务会计菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        }
    };
    }

    /// <summary>
    /// 获取后勤管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetLogisticsSubMenus(long parentId)
    {
        return new List<HbtMenu>
    {
        new HbtMenu
        {
            MenuName = "销售管理",
            TransKey = "menu.logistics.sales",
            ParentId = parentId,
            OrderNum = 1,
            Path = "sales",
            Component = "logistics/sales/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:sales:list",
            Icon = "ShoppingCartOutlined",
            TenantId = 0,
            Remark = "销售管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "生产管理",
            TransKey = "menu.logistics.production",
            ParentId = parentId,
            OrderNum = 2,
            Path = "production",
            Component = "logistics/production/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:production:list",
            Icon = "ExperimentOutlined",
            TenantId = 0,
            Remark = "生产管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "物料管理",
            TransKey = "menu.logistics.material",
            ParentId = parentId,
            OrderNum = 3,
            Path = "material",
            Component = "logistics/material/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:material:list",
            Icon = "InboxOutlined",
            TenantId = 0,
            Remark = "物料管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "质量管理",
            TransKey = "menu.logistics.quality",
            ParentId = parentId,
            OrderNum = 4,
            Path = "quality",
            Component = "logistics/quality/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:quality:list",
            Icon = "SafetyCertificateOutlined",
            TenantId = 0,
            Remark = "质量管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "客服管理",
            TransKey = "menu.logistics.service",
            ParentId = parentId,
            OrderNum = 5,
            Path = "service",
            Component = "logistics/service/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:service:list",
            Icon = "CustomerServiceOutlined",
            TenantId = 0,
            Remark = "客服管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "项目管理",
            TransKey = "menu.logistics.project",
            ParentId = parentId,
            OrderNum = 6,
            Path = "project",
            Component = "logistics/project/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:project:list",
            Icon = "ProjectOutlined",
            TenantId = 0,
            Remark = "项目管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        },
        new HbtMenu
        {
            MenuName = "设备管理",
            TransKey = "menu.logistics.equipment",
            ParentId = parentId,
            OrderNum = 7,
            Path = "equipment",
            Component = "logistics/equipment/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "logistics:equipment:list",
            Icon = "ToolOutlined",
            TenantId = 0,
            Remark = "设备管理菜单",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        }
    };
    }

    /// <summary>
    /// 获取菜单按钮列表
    /// </summary>
    private List<HbtMenu> GetMenuButtons(HbtMenu menu, string modulePrefix)
    {
        var buttons = new List<HbtMenu>();

        // 通用按钮
        var buttonNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消" };
        var buttonPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke" };

        // 代码生成按钮
        var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成代码", "预览代码", "下载代码", "同步数据库" };
        var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync" };

        // 工作流按钮
        var buttonFlowNames = new[] { "查询", "新增", "修改", "删除", "发布", "停用", "挂起", "恢复", "转办", "委托", "退回", "终止", "导入", "导出", "打印" };
        var buttonFlowPerms = new[] { "query", "create", "update", "delete", "publish", "stop", "suspend", "resume", "transfer", "delegate", "reject", "terminate", "import", "export", "print" };

        // 日常办公按钮
        var buttonRoutineNames = new[] {
            "查询", "新增", "修改", "删除", "详情",
            // 文档操作
            "保存草稿", "删除草稿", "发送", "撤回",
            // 阅读操作
            "已读", "未读", "传阅", "签收", "催办",
            // 其他操作
            "打印", "导出", "归档"
        };
        var buttonRoutinePerms = new[] {
            "query", "create", "update", "delete", "detail",
            // 文档权限
            "draft", "deleteDraft", "send", "withdraw",
            // 阅读权限
            "read", "unread", "circulate", "sign", "urge",
            // 其他权限
            "print", "export", "archive"
        };

        // 从菜单的权限标识中获取菜单标识
        var menuPerm = menu.Perms.Split(':')[1];

        string[] names;
        string[] perms;

        // 根据模块前缀选择对应的按钮配置
        switch (modulePrefix)
        {
            case "generator":
                names = buttonGenNames;
                perms = buttonGenPerms;
                break;

            case "workflow":
                names = buttonFlowNames;
                perms = buttonFlowPerms;
                break;

            case "routine":
                names = buttonRoutineNames;
                perms = buttonRoutinePerms;
                break;

            default:
                names = buttonNames;
                perms = buttonPerms;
                break;
        }

        for (int i = 0; i < names.Length; i++)
        {
            buttons.Add(new HbtMenu
            {
                MenuName = names[i],
                TransKey = "button." + perms[i],
                ParentId = menu.Id,
                OrderNum = i + 1,
                Path = string.Empty,
                Component = string.Empty,
                MenuType = 2,
                Perms = $"{modulePrefix}:{menuPerm}:{perms[i]}", // 使用三级结构
                Icon = string.Empty,
                TenantId = 0,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now,
                Remark = $"{menu.MenuName}{names[i]}按钮"
            });
        }

        return buttons;
    }
}