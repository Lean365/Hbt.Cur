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
using Lean.Hbt.Domain.IServices.Extensions;

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

        // 2. 初始化次级菜单
        var (secondInsertCount, secondUpdateCount) = await InitializeSecondMenusAsync(menuNameToId);
        insertCount += secondInsertCount;
        updateCount += secondUpdateCount;

        // 3. 初始化子菜单
        var (subInsertCount, subUpdateCount) = await InitializeSubMenusAsync(menuNameToId);
        insertCount += subInsertCount;
        updateCount += subUpdateCount;

        // 4. 初始化按钮
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
            TransKey = "menu.core._self",
            ParentId = 0,
            OrderNum = 6,
            Path = "core",
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
    /// 初始化次级菜单
    /// </summary>
    private async Task<(int, int)> InitializeSecondMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取财务管理菜单
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 初始化管理会计和控制会计目录
            var financeSubMenus = GetFinanceSubMenus(financeMenu.Id);
            foreach (var menu in financeSubMenus)
            {
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;

                if (isNew) insertCount++;
                else updateCount++;
            }
        }

        // 获取后勤管理菜单
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 初始化后勤管理的次级目录
            var logisticsSubMenus = GetLogisticsSubMenus(logisticsMenu.Id);
            foreach (var menu in logisticsSubMenus)
            {
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;

                if (isNew) insertCount++;
                else updateCount++;
            }
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
        // 获取管理会计目录
        var managementMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == topMenuDict["财务管理"]);
        if (managementMenu != null)
        {
            var managementSubMenus = GetManagementSubMenus(managementMenu.Id);
            subMenus.AddRange(managementSubMenus);
        }

        // 获取控制会计目录
        var controlMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == topMenuDict["财务管理"]);
        if (controlMenu != null)
        {
            var controlSubMenus = GetControlSubMenus(controlMenu.Id);
            subMenus.AddRange(controlSubMenus);
        }

        // 3. 后勤管理子菜单
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 获取物料管理子菜单
            var materialMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenu.Id);
            if (materialMenu != null)
            {
                var materialSubMenus = GetMaterialSubMenus(materialMenu.Id);
                subMenus.AddRange(materialSubMenus);
            }

            // 获取生产管理子菜单
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var productionSubMenus = GetProductionSubMenus(productionMenu.Id);
                subMenus.AddRange(productionSubMenus);
            }

            // 获取销售管理子菜单
            var salesMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
            if (salesMenu != null)
            {
                var salesSubMenus = GetSalesSubMenus(salesMenu.Id);
                subMenus.AddRange(salesSubMenus);
            }

            // 获取质量管理子菜单
            var qualityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
            if (qualityMenu != null)
            {
                var qualitySubMenus = GetQualitySubMenus(qualityMenu.Id);
                subMenus.AddRange(qualitySubMenus);
            }

            // 获取设备管理子菜单
            var equipmentMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
            if (equipmentMenu != null)
            {
                var equipmentSubMenus = GetEquipmentSubMenus(equipmentMenu.Id);
                subMenus.AddRange(equipmentSubMenus);
            }

            // 获取客诉管理子菜单
            var complaintMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客诉管理" && m.ParentId == logisticsMenu.Id);
            if (complaintMenu != null)
            {
                var complaintSubMenus = GetComplaintSubMenus(complaintMenu.Id);
                subMenus.AddRange(complaintSubMenus);
            }

            // 获取客服管理子菜单
            var serviceMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
            if (serviceMenu != null)
            {
                var serviceSubMenus = GetServiceSubMenus(serviceMenu.Id);
                subMenus.AddRange(serviceSubMenus);
            }

            // 获取项目管理子菜单
            var projectMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "项目管理" && m.ParentId == logisticsMenu.Id);
            if (projectMenu != null)
            {
                var projectSubMenus = GetProjectSubMenus(projectMenu.Id);
                subMenus.AddRange(projectSubMenus);
            }
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
                Remark = "字典类型菜单"
            },

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
                MenuName = "服务器监控",
                TransKey = "menu.signalr.server",
                ParentId = parentId,
                OrderNum = 6,
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
            TransKey = "menu.routine.quartz",
            ParentId = parentId,
            OrderNum = 5,
            Path = "quartz",
            Component = "routine/quartz/index",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 1,
            Visible = 0,
            Status = 0,
            Perms = "routine:quartz:list",
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
        var menus = new List<HbtMenu>();
        
        // 1. 管理会计目录
        var managementMenu = new HbtMenu
        {
            MenuName = "管理会计",
            TransKey = "menu.finance.accounting._self",
            ParentId = parentId,
            OrderNum = 1,
            Path = "accounting",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "FundOutlined",
            TenantId = 0,
            Remark = "管理会计子目录",  // 修改为子目录
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        menus.Add(managementMenu);

        // 2. 控制会计目录
        var controlMenu = new HbtMenu
        {
            MenuName = "控制会计",
            TransKey = "menu.finance.controlling._self",
            ParentId = parentId,
            OrderNum = 2,
            Path = "controlling",
            Component = "",
            QueryParams = null,
            IsExternal = 0,
            IsCache = 0,
            MenuType = 0,
            Visible = 0,
            Status = 0,
            Perms = "",
            Icon = "ControlOutlined",
            TenantId = 0,
            Remark = "控制会计子目录",  // 已经是子目录
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        menus.Add(controlMenu);

        return menus;
    }

    /// <summary>
    /// 获取管理会计子菜单列表
    /// </summary>
    private List<HbtMenu> GetManagementSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "公司科目",
                TransKey = "menu.finance.accounting.companyaccounts",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "companyaccounts",
                Component = "finance/accounting/companyaccounts/index",
                MenuType = 1,
                Perms = "finance:accounting:companyaccounts:list",
                Icon = "BankOutlined",
                Remark = "公司科目管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "会计科目",
                TransKey = "menu.finance.accounting.glaccount",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 2,
                Path = "glaccount",
                Component = "finance/accounting/glaccount/index",
                MenuType = 1,
                Perms = "finance:accounting:glaccount:list",
                Icon = "AccountBookOutlined",
                Remark = "会计科目管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "总账",
                TransKey = "menu.finance.accounting.generalledger",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 3,
                Path = "generalledger",
                Component = "finance/accounting/generalledger/index",
                MenuType = 1,
                Perms = "finance:accounting:generalledger:list",
                Icon = "BookOutlined",
                Remark = "总账管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "应付",
                TransKey = "menu.finance.accounting.payable",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 4,
                Path = "payable",
                Component = "finance/accounting/payable/index",
                MenuType = 1,
                Perms = "finance:accounting:payable:list",
                Icon = "MoneyCollectOutlined",
                Remark = "应付管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "应收",
                TransKey = "menu.finance.accounting.receivable",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 5,
                Path = "receivable",
                Component = "finance/accounting/receivable/index",
                MenuType = 1,
                Perms = "finance:accounting:receivable:list",
                Icon = "AccountBookOutlined",
                Remark = "应收管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "固定资产",
                TransKey = "menu.finance.accounting.asset",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 6,
                Path = "asset",
                Component = "finance/accounting/asset/index",
                MenuType = 1,
                Perms = "finance:accounting:asset:list",
                Icon = "PropertySafetyOutlined",
                Remark = "固定资产管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "银行",
                TransKey = "menu.finance.accounting.bank",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 7,
                Path = "bank",
                Component = "finance/accounting/bank/index",
                MenuType = 1,
                Perms = "finance:accounting:bank:list",
                Icon = "BankOutlined",
                Remark = "银行管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取控制会计子菜单列表
    /// </summary>
    private List<HbtMenu> GetControlSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "成本要素",
                TransKey = "menu.finance.controlling.costelement",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "costelement",
                Component = "finance/controlling/costelement/index",
                MenuType = 1,
                Perms = "finance:controlling:costelement:list",
                Icon = "FundOutlined",
                Remark = "成本要素管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "成本中心",
                TransKey = "menu.finance.controlling.costcenter",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 2,
                Path = "costcenter",
                Component = "finance/controlling/costcenter/index",
                MenuType = 1,
                Perms = "finance:controlling:costcenter:list",
                Icon = "ClusterOutlined",
                Remark = "成本中心管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "利润中心",
                TransKey = "menu.finance.controlling.profitcenter",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 3,
                Path = "profitcenter",
                Component = "finance/controlling/profitcenter/index",
                MenuType = 1,
                Perms = "finance:controlling:profitcenter:list",
                Icon = "RiseOutlined",
                Remark = "利润中心管理",
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
                MenuName = "物料管理",
                TransKey = "menu.logistics.material._self",
                ParentId = parentId,
                OrderNum = 1,
                Path = "material",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "InboxOutlined",
                TenantId = 0,
                Remark = "物料管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "生产管理",
                TransKey = "menu.logistics.production._self",
                ParentId = parentId,
                OrderNum = 2,
                Path = "production",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "ExperimentOutlined",
                TenantId = 0,
                Remark = "生产管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "销售管理",
                TransKey = "menu.logistics.sales._self",
                ParentId = parentId,
                OrderNum = 3,
                Path = "sales",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "ShoppingCartOutlined",
                TenantId = 0,
                Remark = "销售管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "质量管理",
                TransKey = "menu.logistics.quality._self",
                ParentId = parentId,
                OrderNum = 4,
                Path = "quality",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "SafetyCertificateOutlined",
                TenantId = 0,
                Remark = "质量管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "设备管理",
                TransKey = "menu.logistics.equipment._self",
                ParentId = parentId,
                OrderNum = 5,
                Path = "equipment",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "ToolOutlined",
                TenantId = 0,
                Remark = "设备管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "客服管理",
                TransKey = "menu.logistics.service._self",
                ParentId = parentId,
                OrderNum = 6,
                Path = "service",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "CustomerServiceOutlined",
                TenantId = 0,
                Remark = "客服管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "客诉管理",
                TransKey = "menu.logistics.complaint._self",
                ParentId = parentId,
                OrderNum = 7,
                Path = "complaint",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "WarningOutlined",
                TenantId = 0,
                Remark = "客诉管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "项目管理",
                TransKey = "menu.logistics.project._self",
                ParentId = parentId,
                OrderNum = 8,
                Path = "project",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "ProjectOutlined",
                TenantId = 0,
                Remark = "项目管理目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取物料管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetMaterialSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "物料信息",
                TransKey = "menu.logistics.material.info",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "info",
                Component = "logistics/material/info/index",
                MenuType = 1,
                Perms = "logistics:material:info:list",
                Icon = "InfoCircleOutlined",
                Remark = "物料信息管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "工厂物料",
                TransKey = "menu.logistics.material.factory",
                ParentId = parentId,
                OrderNum = 2,
                Path = "factory",
                Component = "logistics/material/factory/index",
                MenuType = 1,
                Perms = "logistics:material:factory:list",
                Icon = "ClusterOutlined",
                Remark = "工厂物料管理"
            },
            new HbtMenu
            {
                MenuName = "供应商",
                TransKey = "menu.logistics.material.vendor",
                ParentId = parentId,
                OrderNum = 3,
                Path = "vendor",
                Component = "logistics/material/vendor/index",
                MenuType = 1,
                Perms = "logistics:material:vendor:list",
                Icon = "ShopOutlined",
                Remark = "供应商管理"
            },
            new HbtMenu
            {
                MenuName = "供应商",
                TransKey = "menu.logistics.material.supplier",
                ParentId = parentId,
                OrderNum = 4,
                Path = "supplier",
                Component = "logistics/material/supplier/index",
                MenuType = 1,
                Perms = "logistics:material:supplier:list",
                Icon = "ShopOutlined",
                Remark = "供应商管理"
            },
            new HbtMenu
            {
                MenuName = "采购价格",
                TransKey = "menu.logistics.material.price",
                ParentId = parentId,
                OrderNum = 5,
                Path = "price",
                Component = "logistics/material/price/index",
                MenuType = 1,
                Perms = "logistics:material:price:list",
                Icon = "MoneyCollectOutlined",
                Remark = "采购价格管理"
            },
            new HbtMenu
            {
                MenuName = "采购申请",
                TransKey = "menu.logistics.material.requisition",
                ParentId = parentId,
                OrderNum = 6,
                Path = "requisition",
                Component = "logistics/material/requisition/index",
                MenuType = 1,
                Perms = "logistics:material:requisition:list",
                Icon = "FileAddOutlined",
                Remark = "采购申请管理"
            },
            new HbtMenu
            {
                MenuName = "采购订单",
                TransKey = "menu.logistics.material.order",
                ParentId = parentId,
                OrderNum = 7,
                Path = "order",
                Component = "logistics/material/order/index",
                MenuType = 1,
                Perms = "logistics:material:order:list",
                Icon = "ShoppingOutlined",
                Remark = "采购订单管理"
            }
        };
    }

    /// <summary>
    /// 获取生产管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetProductionSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "物料清单",
                TransKey = "menu.logistics.production.bom",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "bom",
                Component = "logistics/production/bom/index",
                MenuType = 1,
                Perms = "logistics:production:bom:list",
                Icon = "UnorderedListOutlined",
                Remark = "物料清单管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "设计变更",
                TransKey = "menu.logistics.production.change",
                ParentId = parentId,
                OrderNum = 2,
                Path = "change",
                Component = "logistics/production/change/index",
                MenuType = 1,
                Perms = "logistics:production:change:list",
                Icon = "SwapOutlined",
                Remark = "设计变更管理"
            },
            new HbtMenu
            {
                MenuName = "工作中心",
                TransKey = "menu.logistics.production.workcenter",
                ParentId = parentId,
                OrderNum = 3,
                Path = "workcenter",
                Component = "logistics/production/workcenter/index",
                MenuType = 1,
                Perms = "logistics:production:workcenter:list",
                Icon = "ClusterOutlined",
                Remark = "工作中心管理"
            },
            new HbtMenu
            {
                MenuName = "生产订单",
                TransKey = "menu.logistics.production.order",
                ParentId = parentId,
                OrderNum = 4,
                Path = "order",
                Component = "logistics/production/order/index",
                MenuType = 1,
                Perms = "logistics:production:order:list",
                Icon = "FileTextOutlined",
                Remark = "生产订单管理"
            },
            new HbtMenu
            {
                MenuName = "看板",
                TransKey = "menu.logistics.production.kanban",
                ParentId = parentId,
                OrderNum = 5,
                Path = "kanban",
                Component = "logistics/production/kanban/index",
                MenuType = 1,
                Perms = "logistics:production:kanban:list",
                Icon = "DashboardOutlined",
                Remark = "看板管理"
            }
        };
    }

    /// <summary>
    /// 获取销售管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetSalesSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "客户",
                TransKey = "menu.logistics.sales.customer",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "customer",
                Component = "logistics/sales/customer/index",
                MenuType = 1,
                Perms = "logistics:sales:customer:list",
                Icon = "UserOutlined",
                Remark = "客户管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "客户",
                TransKey = "menu.logistics.sales.client",
                ParentId = parentId,
                OrderNum = 2,
                Path = "client",
                Component = "logistics/sales/client/index",
                MenuType = 1,
                Perms = "logistics:sales:client:list",
                Icon = "TeamOutlined",
                Remark = "客户管理"
            },
            new HbtMenu
            {
                MenuName = "销售价格",
                TransKey = "menu.logistics.sales.price",
                ParentId = parentId,
                OrderNum = 3,
                Path = "price",
                Component = "logistics/sales/price/index",
                MenuType = 1,
                Perms = "logistics:sales:price:list",
                Icon = "MoneyCollectOutlined",
                Remark = "销售价格管理"
            },
            new HbtMenu
            {
                MenuName = "销售订单",
                TransKey = "menu.logistics.sales.order",
                ParentId = parentId,
                OrderNum = 4,
                Path = "order",
                Component = "logistics/sales/order/index",
                MenuType = 1,
                Perms = "logistics:sales:order:list",
                Icon = "ShoppingCartOutlined",
                Remark = "销售订单管理"
            }
        };
    }

    /// <summary>
    /// 获取质量管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetQualitySubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "检验项目",
                TransKey = "menu.logistics.quality.item",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "item",
                Component = "logistics/quality/item/index",
                MenuType = 1,
                Perms = "logistics:quality:item:list",
                Icon = "CheckSquareOutlined",
                Remark = "检验项目管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "收货检验",
                TransKey = "menu.logistics.quality.receiving",
                ParentId = parentId,
                OrderNum = 2,
                Path = "receiving",
                Component = "logistics/quality/receiving/index",
                MenuType = 1,
                Perms = "logistics:quality:receiving:list",
                Icon = "InboxOutlined",
                Remark = "收货检验管理"
            },
            new HbtMenu
            {
                MenuName = "过程检验",
                TransKey = "menu.logistics.quality.process",
                ParentId = parentId,
                OrderNum = 3,
                Path = "process",
                Component = "logistics/quality/process/index",
                MenuType = 1,
                Perms = "logistics:quality:process:list",
                Icon = "ExperimentOutlined",
                Remark = "过程检验管理"
            },
            new HbtMenu
            {
                MenuName = "入库检验",
                TransKey = "menu.logistics.quality.storage",
                ParentId = parentId,
                OrderNum = 4,
                Path = "storage",
                Component = "logistics/quality/storage/index",
                MenuType = 1,
                Perms = "logistics:quality:storage:list",
                Icon = "DatabaseOutlined",
                Remark = "入库检验管理"
            },
            new HbtMenu
            {
                MenuName = "退货检验",
                TransKey = "menu.logistics.quality.return",
                ParentId = parentId,
                OrderNum = 5,
                Path = "return",
                Component = "logistics/quality/return/index",
                MenuType = 1,
                Perms = "logistics:quality:return:list",
                Icon = "RollbackOutlined",
                Remark = "退货检验管理"
            }
        };
    }

    /// <summary>
    /// 获取设备管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetEquipmentSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "设备数据",
                TransKey = "menu.logistics.equipment.data",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "data",
                Component = "logistics/equipment/data/index",
                MenuType = 1,
                Perms = "logistics:equipment:data:list",
                Icon = "DatabaseOutlined",
                Remark = "设备数据管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "功能位置",
                TransKey = "menu.logistics.equipment.location",
                ParentId = parentId,
                OrderNum = 2,
                Path = "location",
                Component = "logistics/equipment/location/index",
                MenuType = 1,
                Perms = "logistics:equipment:location:list",
                Icon = "EnvironmentOutlined",
                Remark = "功能位置管理"
            },
            new HbtMenu
            {
                MenuName = "物料关联",
                TransKey = "menu.logistics.equipment.material",
                ParentId = parentId,
                OrderNum = 3,
                Path = "material",
                Component = "logistics/equipment/material/index",
                MenuType = 1,
                Perms = "logistics:equipment:material:list",
                Icon = "LinkOutlined",
                Remark = "物料关联管理"
            },
            new HbtMenu
            {
                MenuName = "工单",
                TransKey = "menu.logistics.equipment.workorder",
                ParentId = parentId,
                OrderNum = 4,
                Path = "workorder",
                Component = "logistics/equipment/workorder/index",
                MenuType = 1,
                Perms = "logistics:equipment:workorder:list",
                Icon = "FileTextOutlined",
                Remark = "工单管理"
            }
        };
    }

    /// <summary>
    /// 获取客服管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetServiceSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "服务项目",
                TransKey = "menu.logistics.service.item",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "item",
                Component = "logistics/service/item/index",
                MenuType = 1,
                Perms = "logistics:service:item:list",
                Icon = "AppstoreOutlined",
                Remark = "服务项目管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "服务合同",
                TransKey = "menu.logistics.service.contract",
                ParentId = parentId,
                OrderNum = 2,
                Path = "contract",
                Component = "logistics/service/contract/index",
                MenuType = 1,
                Perms = "logistics:service:contract:list",
                Icon = "FileTextOutlined",
                Remark = "服务合同管理"
            },
            new HbtMenu
            {
                MenuName = "服务请求",
                TransKey = "menu.logistics.service.request",
                ParentId = parentId,
                OrderNum = 3,
                Path = "request",
                Component = "logistics/service/request/index",
                MenuType = 1,
                Perms = "logistics:service:request:list",
                Icon = "QuestionCircleOutlined",
                Remark = "服务请求管理"
            },
            new HbtMenu
            {
                MenuName = "服务工单",
                TransKey = "menu.logistics.service.workorder",
                ParentId = parentId,
                OrderNum = 4,
                Path = "workorder",
                Component = "logistics/service/workorder/index",
                MenuType = 1,
                Perms = "logistics:service:workorder:list",
                Icon = "FileTextOutlined",
                Remark = "服务工单管理"
            },
            new HbtMenu
            {
                MenuName = "工时记录",
                TransKey = "menu.logistics.service.timesheet",
                ParentId = parentId,
                OrderNum = 5,
                Path = "timesheet",
                Component = "logistics/service/timesheet/index",
                MenuType = 1,
                Perms = "logistics:service:timesheet:list",
                Icon = "FieldTimeOutlined",
                Remark = "工时记录管理"
            },
            new HbtMenu
            {
                MenuName = "物料消耗",
                TransKey = "menu.logistics.service.consumption",
                ParentId = parentId,
                OrderNum = 6,
                Path = "consumption",
                Component = "logistics/service/consumption/index",
                MenuType = 1,
                Perms = "logistics:service:consumption:list",
                Icon = "InboxOutlined",
                Remark = "物料消耗管理"
            },
            new HbtMenu
            {
                MenuName = "外协服务",
                TransKey = "menu.logistics.service.outsourcing",
                ParentId = parentId,
                OrderNum = 7,
                Path = "outsourcing",
                Component = "logistics/service/outsourcing/index",
                MenuType = 1,
                Perms = "logistics:service:outsourcing:list",
                Icon = "TeamOutlined",
                Remark = "外协服务管理"
            }
        };
    }

    /// <summary>
    /// 获取客诉管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetComplaintSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "质量通知单",
                TransKey = "menu.logistics.complaint.notice",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "notice",
                Component = "logistics/complaint/notice/index",
                MenuType = 1,
                Perms = "logistics:complaint:notice:list",
                Icon = "NotificationOutlined",
                Remark = "质量通知单管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "客户主数据标记",
                TransKey = "menu.logistics.complaint.mark",
                ParentId = parentId,
                OrderNum = 2,
                Path = "mark",
                Component = "logistics/complaint/mark/index",
                MenuType = 1,
                Perms = "logistics:complaint:mark:list",
                Icon = "TagOutlined",
                Remark = "客户主数据标记管理"
            },
            new HbtMenu
            {
                MenuName = "原因分析",
                TransKey = "menu.logistics.complaint.analysis",
                ParentId = parentId,
                OrderNum = 3,
                Path = "analysis",
                Component = "logistics/complaint/analysis/index",
                MenuType = 1,
                Perms = "logistics:complaint:analysis:list",
                Icon = "FundOutlined",
                Remark = "原因分析管理"
            },
            new HbtMenu
            {
                MenuName = "纠正措施",
                TransKey = "menu.logistics.complaint.corrective",
                ParentId = parentId,
                OrderNum = 4,
                Path = "corrective",
                Component = "logistics/complaint/corrective/index",
                MenuType = 1,
                Perms = "logistics:complaint:corrective:list",
                Icon = "ToolOutlined",
                Remark = "纠正措施管理"
            },
            new HbtMenu
            {
                MenuName = "退换货执行",
                TransKey = "menu.logistics.complaint.return",
                ParentId = parentId,
                OrderNum = 5,
                Path = "return",
                Component = "logistics/complaint/return/index",
                MenuType = 1,
                Perms = "logistics:complaint:return:list",
                Icon = "RollbackOutlined",
                Remark = "退换货执行管理"
            }
        };
    }

    /// <summary>
    /// 获取项目管理子菜单列表
    /// </summary>
    private List<HbtMenu> GetProjectSubMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu
            {
                MenuName = "定义项目",
                TransKey = "menu.logistics.project.define",
                ParentId = parentId,  // 使用传入的父级ID
                OrderNum = 1,
                Path = "define",
                Component = "logistics/project/define/index",
                MenuType = 1,
                Perms = "logistics:project:define:list",
                Icon = "FileTextOutlined",
                Remark = "项目定义管理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "成本计划",
                TransKey = "menu.logistics.project.cost",
                ParentId = parentId,
                OrderNum = 2,
                Path = "cost",
                Component = "logistics/project/cost/index",
                MenuType = 1,
                Perms = "logistics:project:cost:list",
                Icon = "AccountBookOutlined",
                Remark = "成本计划管理"
            },
            new HbtMenu
            {
                MenuName = "资源计划",
                TransKey = "menu.logistics.project.resource",
                ParentId = parentId,
                OrderNum = 3,
                Path = "resource",
                Component = "logistics/project/resource/index",
                MenuType = 1,
                Perms = "logistics:project:resource:list",
                Icon = "ClusterOutlined",
                Remark = "资源计划管理"
            },
            new HbtMenu
            {
                MenuName = "进度计划",
                TransKey = "menu.logistics.project.schedule",
                ParentId = parentId,
                OrderNum = 4,
                Path = "schedule",
                Component = "logistics/project/schedule/index",
                MenuType = 1,
                Perms = "logistics:project:schedule:list",
                Icon = "FieldTimeOutlined",
                Remark = "进度计划管理"
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

        // 通用按钮
        var buttonIdNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消" ,"授权","分配","重置密码","变更密码"};
        var buttonIdPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke" ,"authorize","allocate","resetpwd","changepwd"};

        // 代码生成按钮
        var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成代码", "预览代码", "下载代码", "同步数据库", "导入", "导出", "字段", "表", "数据库" };
        var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export","columns","tables","databases" };

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
            case "identity":
                names = buttonIdNames;
                perms = buttonIdPerms;
                break;

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