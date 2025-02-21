//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 菜单数据初始化类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Enums;
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
                MenuName = "系统管理",
                TransKey = "menu.admin._self",
                ParentId = 0,
                OrderNum = 1,
                Path = "/admin",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "SettingOutlined",
                TenantId = 0,
                Remark = "系统管理目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "身份认证",
                TransKey = "menu.identity._self",
                ParentId = 0,
                OrderNum = 2,
                Path = "/identity",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "UserOutlined",
                TenantId = 0,
                Remark = "身份认证目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "审计日志",
                TransKey = "menu.audit._self",
                ParentId = 0,
                OrderNum = 3,
                Path = "/audit",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "AuditOutlined",
                TenantId = 0,
                Remark = "审计日志目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "工作流程",
                TransKey = "menu.workflow._self",
                ParentId = 0,
                OrderNum = 4,
                Path = "/workflow",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "DeploymentUnitOutlined",
                TenantId = 0,
                Remark = "工作流程目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "实时监控",
                TransKey = "menu.realtime._self",
                ParentId = 0,
                OrderNum = 5,
                Path = "/realtime",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "DashboardOutlined",
                TenantId = 0,
                Remark = "实时监控目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "安全管理",
                TransKey = "menu.security._self",
                ParentId = 0,
                OrderNum = 6,
                Path = "/security",
                Component = "",
                QueryParams = null,
                IsFrame = HbtYesNo.No,
                IsCache = HbtYesNo.No,
                MenuType = HbtMenuType.Directory,
                Visible = HbtVisible.Show,
                Status = HbtStatus.Normal,
                Perms = "",
                Icon = "SafetyCertificateOutlined",
                TenantId = 0,
                Remark = "安全管理目录",
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
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

        // 系统管理子菜单
        var adminMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "系统管理" && m.ParentId == 0);
        if (adminMenu != null)
        {
            var adminSubMenus = GetSystemManagementSubMenus(adminMenu.Id);
            subMenus.AddRange(adminSubMenus);
        }

        // 身份认证子菜单
        var identityMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            var identitySubMenus = GetIdentitySubMenus(identityMenu.Id);
            subMenus.AddRange(identitySubMenus);
        }

        // 审计日志子菜单
        var auditMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            var auditSubMenus = GetAuditSubMenus(auditMenu.Id);
            subMenus.AddRange(auditSubMenus);
        }

        // 工作流程子菜单
        var workflowMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            var workflowSubMenus = GetWorkflowSubMenus(workflowMenu.Id);
            subMenus.AddRange(workflowSubMenus);
        }

        // 实时监控子菜单
        var realtimeMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "实时监控" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            var realtimeSubMenus = GetRealtimeSubMenus(realtimeMenu.Id);
            subMenus.AddRange(realtimeSubMenus);
        }

        // 安全管理子菜单
        var securityMenu = await _menuRepository.FirstOrDefaultAsync(m => m.MenuName == "安全管理" && m.ParentId == 0);
        if (securityMenu != null)
        {
            var securitySubMenus = GetSecuritySubMenus(securityMenu.Id);
            subMenus.AddRange(securitySubMenus);
        }

        // 保存所有子菜单
        foreach (var menu in subMenus)
        {
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;

            _logger.Info($"[{(isNew ? "创建" : "更新")}] 子菜单 '{menu.MenuName}' (ParentId: {menu.ParentId}) {(isNew ? "创建" : "更新")}成功");
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
        var menus = await _menuRepository.GetListAsync(m => m.MenuType == HbtMenuType.Menu);

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

        var existingMenu = await _menuRepository.FirstOrDefaultAsync(query);

        if (existingMenu == null)
        {
            await _menuRepository.InsertAsync(menu);
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
        existingMenu.IsFrame = menu.IsFrame;
        existingMenu.IsCache = menu.IsCache;
        existingMenu.MenuType = menu.MenuType;
        existingMenu.Visible = menu.Visible;
        existingMenu.Status = menu.Status;
        existingMenu.Perms = menu.Perms;
        existingMenu.Icon = menu.Icon;
        existingMenu.TenantId = menu.TenantId;
        existingMenu.Remark = menu.Remark;
        existingMenu.UpdateBy = "admin";
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
                Perms = "admin:translation:list",
                Icon = "GlobalOutlined",
                Remark = "翻译管理菜单"
            }
        };
    }

    /// <summary>
    /// 获取身份认证子菜单列表
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
                MenuType = HbtMenuType.Menu,
                Perms = "identity:user:list",
                Icon = "UserOutlined",
                Remark = "用户管理菜单"
            },
            new HbtMenu
            {
                MenuName = "角色管理",
                TransKey = "menu.identity.role",
                ParentId = parentId,
                OrderNum = 2,
                Path = "role",
                Component = "identity/role/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:role:list",
                Icon = "TeamOutlined",
                Remark = "角色管理菜单"
            },
            new HbtMenu
            {
                MenuName = "部门管理",
                TransKey = "menu.identity.dept",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dept",
                Component = "identity/dept/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:dept:list",
                Icon = "ApartmentOutlined",
                Remark = "部门管理菜单"
            },
            new HbtMenu
            {
                MenuName = "岗位管理",
                TransKey = "menu.identity.post",
                ParentId = parentId,
                OrderNum = 4,
                Path = "post",
                Component = "identity/post/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:post:list",
                Icon = "IdcardOutlined",
                Remark = "岗位管理菜单"
            },
            new HbtMenu
            {
                MenuName = "菜单管理",
                TransKey = "menu.identity.menu",
                ParentId = parentId,
                OrderNum = 5,
                Path = "menu",
                Component = "identity/menu/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:menu:list",
                Icon = "MenuOutlined",
                Remark = "菜单管理菜单"
            },
            new HbtMenu
            {
                MenuName = "租户管理",
                TransKey = "menu.identity.tenant",
                ParentId = parentId,
                OrderNum = 6,
                Path = "tenant",
                Component = "identity/tenant/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:tenant:list",
                Icon = "ClusterOutlined",
                Remark = "租户管理菜单"
            },
            new HbtMenu
            {
                MenuName = "认证管理",
                TransKey = "menu.identity.auth",
                ParentId = parentId,
                OrderNum = 7,
                Path = "auth",
                Component = "identity/auth/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:auth:list",
                Icon = "KeyOutlined",
                Remark = "认证管理菜单"
            },
            new HbtMenu
            {
                MenuName = "OAuth管理",
                TransKey = "menu.identity.oauth",
                ParentId = parentId,
                OrderNum = 8,
                Path = "oauth",
                Component = "identity/oauth/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:oauth:list",
                Icon = "LinkOutlined",
                Remark = "OAuth管理菜单"
            },
            new HbtMenu
            {
                MenuName = "登录策略",
                TransKey = "menu.identity.loginpolicy",
                ParentId = parentId,
                OrderNum = 9,
                Path = "loginpolicy",
                Component = "identity/loginpolicy/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:loginpolicy:list",
                Icon = "SecurityScanOutlined",
                Remark = "登录策略菜单"
            },
            new HbtMenu
            {
                MenuName = "登录扩展",
                TransKey = "menu.identity.loginextend",
                ParentId = parentId,
                OrderNum = 10,
                Path = "loginextend",
                Component = "identity/loginextend/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:loginextend:list",
                Icon = "LoginOutlined",
                Remark = "登录扩展菜单"
            },
            new HbtMenu
            {
                MenuName = "设备扩展",
                TransKey = "menu.identity.deviceextend",
                ParentId = parentId,
                OrderNum = 11,
                Path = "deviceextend",
                Component = "identity/deviceextend/index",
                MenuType = HbtMenuType.Menu,
                Perms = "identity:deviceextend:list",
                Icon = "MobileOutlined",
                Remark = "设备扩展菜单"
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
                MenuType = HbtMenuType.Menu,
                Perms = "audit:operlog:list",
                Icon = "HistoryOutlined",
                CreateBy = "admin",
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
                MenuType = HbtMenuType.Menu,
                Perms = "audit:loginlog:list",
                Icon = "LoginOutlined",
                CreateBy = "admin",
                CreateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "数据变更日志",
                TransKey = "menu.audit.dbdifflog",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dbdifflog",
                Component = "audit/dbdifflog/index",
                MenuType = HbtMenuType.Menu,
                Perms = "audit:dbdifflog:list",
                Icon = "DiffOutlined",
                CreateBy = "admin",
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
                MenuType = HbtMenuType.Menu,
                Perms = "audit:exceptionlog:list",
                Icon = "ExceptionOutlined",
                CreateBy = "admin",
                CreateTime = DateTime.Now
            }
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
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
                MenuType = HbtMenuType.Menu,
                Perms = "workflow:history:list",
                Icon = "HistoryOutlined",
                Remark = "流程历史菜单"
            }
        };
    }

    /// <summary>
    /// 获取实时监控子菜单列表
    /// </summary>
    private List<HbtMenu> GetRealtimeSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "在线用户",
                TransKey = "menu.realtime.online-user",
                ParentId = parentId,
                OrderNum = 1,
                Path = "online-user",
                Component = "realtime/online-user/index",
                MenuType = HbtMenuType.Menu,
                Perms = "realtime:online-user:list",
                Icon = "TeamOutlined",
                Remark = "在线用户菜单"
            },
            new HbtMenu
            {
                MenuName = "在线消息",
                TransKey = "menu.realtime.online-message",
                ParentId = parentId,
                OrderNum = 2,
                Path = "online-message",
                Component = "realtime/online-message/index",
                MenuType = HbtMenuType.Menu,
                Perms = "realtime:online-message:list",
                Icon = "MessageOutlined",
                Remark = "在线消息菜单"
            }
        };
    }

    /// <summary>
    /// 获取安全管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetSecuritySubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "验证码",
                TransKey = "menu.security.captcha",
                ParentId = parentId,
                OrderNum = 1,
                Path = "captcha",
                Component = "security/captcha/index",
                MenuType = HbtMenuType.Menu,
                Perms = "security:captcha:list",
                Icon = "SafetyCertificateOutlined",
                Remark = "验证码菜单"
            }
        };
    }

    /// <summary>
    /// 获取菜单按钮列表
    /// </summary>
    private List<HbtMenu> GetMenuButtons(HbtMenu menu, string modulePrefix)
    {
        var buttons = new List<HbtMenu>();
        var buttonNames = new[] { "查询", "新增", "修改", "删除", "预览", "导入", "导出" };
        var buttonPerms = new[] { "query", "add", "edit", "delete", "preview", "import", "export" };

        for (int i = 0; i < buttonNames.Length; i++)
        {
            buttons.Add(new HbtMenu
            {
                MenuName = buttonNames[i],
                TransKey = "button." + buttonPerms[i],
                ParentId = menu.Id,
                OrderNum = i + 1,
                Path = string.Empty,
                Component = string.Empty,
                MenuType = HbtMenuType.Button,
                Perms = $"{modulePrefix}:{buttonPerms[i]}",
                Icon = string.Empty,
                TenantId = 0,
                CreateBy = "admin",
                CreateTime = DateTime.Now,
                UpdateBy = "admin",
                UpdateTime = DateTime.Now,
                Remark = $"{menu.MenuName}{buttonNames[i]}按钮"
            });
        }

        return buttons;
    }
}