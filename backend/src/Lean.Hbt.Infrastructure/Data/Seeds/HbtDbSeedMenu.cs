//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 菜单数据初始化主类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 菜单数据初始化主类
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
        var menuNameToId = new Dictionary<string, long>();

        _logger.Info("开始初始化菜单数据...");

        // 1. 初始化顶级菜单
        var (topInsertCount, topUpdateCount) = await InitializeTopMenusAsync(menuNameToId);

        // 2. 初始化二级目录
        var (secondLevelInsertCount, secondLevelUpdateCount) = await InitializeSecondLevelMenusAsync(menuNameToId);

        // 3. 初始化二级子菜单
        var (secondInsertCount, secondUpdateCount) = await InitializeSecondMenusAsync(menuNameToId);

        // 4. 初始化三级目录
        var (thirdLevelInsertCount, thirdLevelUpdateCount) = await InitializeThirdLevelMenusAsync(menuNameToId);

        // 5. 初始化三级子菜单
        var (thirdSubInsertCount, thirdSubUpdateCount) = await InitializeThirdMenusAsync(menuNameToId);

        // 6. 初始化四级目录
        var (fourthLevelInsertCount, fourthLevelUpdateCount) = await InitializeFourthLevelMenusAsync(menuNameToId);

        // 7. 初始化四级子菜单
        var (fourthSubInsertCount, fourthSubUpdateCount) = await InitializeFourthMenusAsync(menuNameToId);

        // 8. 初始化五级目录
        var (fifthLevelInsertCount, fifthLevelUpdateCount) = await InitializeFifthLevelMenusAsync(menuNameToId);

        // 9. 初始化五级子菜单
        var (fifthSubInsertCount, fifthSubUpdateCount) = await InitializeFifthMenusAsync(menuNameToId);

        // 10. 初始化六级菜单
        var (sixthInsertCount, sixthUpdateCount) = await InitializeSixthMenusAsync(menuNameToId);

        // 11. 初始化按钮
        var (btnInsertCount, btnUpdateCount) = await InitializeButtonsAsync(menuNameToId);

        var totalInsertCount = topInsertCount + secondLevelInsertCount + secondInsertCount + thirdLevelInsertCount + thirdSubInsertCount + fourthLevelInsertCount + fourthSubInsertCount + fifthLevelInsertCount + fifthSubInsertCount + sixthInsertCount + btnInsertCount;
        var totalUpdateCount = topUpdateCount + secondLevelUpdateCount + secondUpdateCount + thirdLevelUpdateCount + thirdSubUpdateCount + fourthLevelUpdateCount + fourthSubUpdateCount + fifthLevelUpdateCount + fifthSubUpdateCount + sixthUpdateCount + btnUpdateCount;

        _logger.Info($"菜单数据初始化完成。新增: {totalInsertCount}, 更新: {totalUpdateCount}");

        return (totalInsertCount, totalUpdateCount);
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
                Remark = "身份认证目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "核心功能",
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
                Remark = "核心功能目录",
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
                Remark = "审计日志目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtMenu
            {
                MenuName = "实时通信",
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
                Remark = "实时通信目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var menu in topMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, true);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化二级目录
    /// </summary>
    private async Task<(int, int)> InitializeSecondLevelMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 1. 日常办公二级目录
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 初始化日常办公的二级目录（MenuType = 0）
            var routineSubMenus = HbtDbSeedRoutineMenu.GetRoutineSecondLevelMenus(routineMenu.Id);
            foreach (var menu in routineSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;

                if (isNew) insertCount++;
                else updateCount++;
            }
        }

        // 2. 财务管理二级目录
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 初始化管理会计、控制会计、全面预算目录
            var financeSubMenus = HbtDbSeedFinanceMenu.GetFinanceSecondLevelMenus(financeMenu.Id);
            foreach (var menu in financeSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;

                if (isNew) insertCount++;
                else updateCount++;
            }
        }

        // 3. 后勤管理二级目录
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 初始化后勤管理的二级目录（MenuType = 0）
            var logisticsSubMenus = HbtDbSeedLogisticsMenu.GetLogisticsSecondLevelMenus(logisticsMenu.Id);
            foreach (var menu in logisticsSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 4. 工作流程二级目录
        var workflowMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            var workflowSubMenus = HbtDbSeedWorkflowMenu.GetWorkflowThirdMenus(workflowMenu.Id);
            foreach (var menu in workflowSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 5. 身份认证二级目录
        var identityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            var identitySubMenus = HbtDbSeedIdentityMenu.GetIdentityThirdMenus(identityMenu.Id);
            foreach (var menu in identitySubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 6. 核心功能二级目录
        var adminMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "核心功能" && m.ParentId == 0);
        if (adminMenu != null)
        {
            var adminSubMenus = HbtDbSeedCoreMenu.GetCoreThirdMenus(adminMenu.Id);
            foreach (var menu in adminSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 7. 代码生成二级目录
        var generatorMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            var generatorSubMenus = HbtDbSeedGeneratorMenu.GetGeneratorThirdMenus(generatorMenu.Id);
            foreach (var menu in generatorSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 8. 审计日志二级目录
        var auditMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            var auditSubMenus = HbtDbSeedAuditMenu.GetAuditThirdMenus(auditMenu.Id);
            foreach (var menu in auditSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        // 9. 实时通信二级目录
        var realtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            var realtimeSubMenus = HbtDbSeedSignalRMenu.GetSignalrThirdMenus(realtimeMenu.Id);
            foreach (var menu in realtimeSubMenus)
            {
                menu.CreateBy = "Hbt365";
                menu.CreateTime = DateTime.Now;
                menu.UpdateBy = "Hbt365";
                menu.UpdateTime = DateTime.Now;
                var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                menuNameToId[menu.MenuName] = savedMenu.Id;
                if (isNew) insertCount++; else updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化二级子菜单
    /// </summary>
    private async Task<(int, int)> InitializeSecondMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var subMenus = new List<HbtMenu>();

        // 1. 日常办公二级子菜单
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 日常办公的二级子菜单处理
            // 这里可以添加日常办公的二级子菜单逻辑
        }

        // 2. 财务管理二级子菜单
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 财务管理的二级子菜单处理
            // 这里可以添加财务管理的二级子菜单逻辑
        }

        // 3. 后勤管理二级子菜单
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 后勤管理的二级子菜单处理
            // 这里可以添加后勤管理的二级子菜单逻辑
        }

        // 4. 工作流程二级子菜单
        var workflowMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的二级子菜单处理
            // 这里可以添加工作流程的二级子菜单逻辑
        }

        // 5. 身份认证二级子菜单
        var identityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的二级子菜单处理
            // 这里可以添加身份认证的二级子菜单逻辑
        }

        // 6. 核心功能二级子菜单
        var adminMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "核心功能" && m.ParentId == 0);
        if (adminMenu != null)
        {
            // 核心功能的二级子菜单处理
            // 这里可以添加核心功能的二级子菜单逻辑
        }

        // 7. 代码生成二级子菜单
        var generatorMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的二级子菜单处理
            // 这里可以添加代码生成的二级子菜单逻辑
        }

        // 8. 审计日志二级子菜单
        var auditMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的二级子菜单处理
            // 这里可以添加审计日志的二级子菜单逻辑
        }

        // 9. 实时通信二级子菜单
        var realtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            // 实时通信的二级子菜单处理
            // 这里可以添加实时通信的二级子菜单逻辑
        }

        // 处理所有二级子菜单
        foreach (var menu in subMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化三级目录
    /// </summary>
    private async Task<(int, int)> InitializeThirdLevelMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var thirdLevelMenus = new List<HbtMenu>();

        // 1. 日常办公下的三级目录
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 办公用品下的三级目录
            var officeSuppliesMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
            if (officeSuppliesMenu != null)
            {
                var officeSuppliesSubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesThirdLevelMenus(officeSuppliesMenu.Id);
                thirdLevelMenus.AddRange(officeSuppliesSubMenus);
            }

            // 文件管理下的三级目录
            var fileMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenu != null)
            {
                var fileSubMenus = HbtDbSeedRoutineMenu.GetRoutineFileThirdLevelMenus(fileMenu.Id);
                thirdLevelMenus.AddRange(fileSubMenus);
            }

            // 费用管理下的三级目录
            var expenseMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "费用管理" && m.ParentId == routineMenu.Id);
            if (expenseMenu != null)
            {
                var expenseSubMenus = HbtDbSeedRoutineMenu.GetRoutineExpenseThirdLevelMenus(expenseMenu.Id);
                thirdLevelMenus.AddRange(expenseSubMenus);
            }

            // 公告通知下的三级目录
            var noticeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
            if (noticeMenu != null)
            {
                var noticeSubMenus = HbtDbSeedRoutineMenu.GetRoutineNoticeThirdLevelMenus(noticeMenu.Id);
                thirdLevelMenus.AddRange(noticeSubMenus);
            }

            // 医务管理下的三级目录
            var medicalMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
            if (medicalMenu != null)
            {
                var medicalSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicalThirdLevelMenus(medicalMenu.Id);
                thirdLevelMenus.AddRange(medicalSubMenus);
            }

            // 人事考勤下的三级目录
            var hrMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "人事考勤" && m.ParentId == routineMenu.Id);
            if (hrMenu != null)
            {
                var hrSubMenus = HbtDbSeedRoutineMenu.GetRoutineHrThirdLevelMenus(hrMenu.Id);
                thirdLevelMenus.AddRange(hrSubMenus);
            }

            // 图书管理下的三级目录
            var bookMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "图书管理" && m.ParentId == routineMenu.Id);
            if (bookMenu != null)
            {
                var bookSubMenus = HbtDbSeedRoutineMenu.GetRoutineBookThirdLevelMenus(bookMenu.Id);
                thirdLevelMenus.AddRange(bookSubMenus);
            }
        }

        // 2. 财务管理下的三级目录
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 全面预算下的三级目录
            var budgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var budgetSubMenus = HbtDbSeedFinanceMenu.GetBudgetThirdLevelMenus(budgetMenu.Id);
                thirdLevelMenus.AddRange(budgetSubMenus);
            }
        }
        else
        {
            _logger.Info("未找到财务管理目录");
        }

        // 3. 后勤管理下的三级目录
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 物料管理下的三级目录
            var materialMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenu.Id);
            if (materialMenu != null)
            {
                var materialSubMenus = HbtDbSeedLogisticsMenu.GetMaterialThirdLevelMenus(materialMenu.Id);
                thirdLevelMenus.AddRange(materialSubMenus);
            }

            // 生产管理下的三级目录
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var productionSubMenus = HbtDbSeedLogisticsMenu.GetProductionThirdLevelMenus(productionMenu.Id);
                thirdLevelMenus.AddRange(productionSubMenus);
            }

            // 销售管理下的三级目录
            var salesMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
            if (salesMenu != null)
            {
                var salesSubMenus = HbtDbSeedLogisticsMenu.GetSalesThirdMenus(salesMenu.Id);
                thirdLevelMenus.AddRange(salesSubMenus);
            }

            // 质量管理下的三级目录
            var qualityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
            if (qualityMenu != null)
            {
                var qualitySubMenus = HbtDbSeedLogisticsMenu.GetQualityThirdMenus(qualityMenu.Id);
                thirdLevelMenus.AddRange(qualitySubMenus);
            }

            // 设备管理下的三级目录
            var equipmentMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
            if (equipmentMenu != null)
            {
                var equipmentSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentThirdMenus(equipmentMenu.Id);
                thirdLevelMenus.AddRange(equipmentSubMenus);
            }

            // 客诉管理下的三级目录
            var complaintMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客诉管理" && m.ParentId == logisticsMenu.Id);
            if (complaintMenu != null)
            {
                var complaintSubMenus = HbtDbSeedLogisticsMenu.GetComplaintThirdMenus(complaintMenu.Id);
                thirdLevelMenus.AddRange(complaintSubMenus);
            }

            // 客服管理下的三级目录
            var serviceMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
            if (serviceMenu != null)
            {
                var serviceSubMenus = HbtDbSeedLogisticsMenu.GetServiceThirdMenus(serviceMenu.Id);
                thirdLevelMenus.AddRange(serviceSubMenus);
            }

            // 项目管理下的三级目录
            var projectMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "项目管理" && m.ParentId == logisticsMenu.Id);
            if (projectMenu != null)
            {
                var projectSubMenus = HbtDbSeedLogisticsMenu.GetProjectThirdMenus(projectMenu.Id);
                thirdLevelMenus.AddRange(projectSubMenus);
            }
        }

        // 4. 工作流程下的三级目录
        var workflowMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的三级目录处理
            // 这里可以添加工作流程的三级目录逻辑
        }

        // 5. 身份认证下的三级目录
        var identityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的三级目录处理
            // 这里可以添加身份认证的三级目录逻辑
        }

        // 6. 核心功能下的三级目录
        var adminMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "核心功能" && m.ParentId == 0);
        if (adminMenu != null)
        {
            // 核心功能的三级目录处理
            // 这里可以添加核心功能的三级目录逻辑
        }

        // 7. 代码生成下的三级目录
        var generatorMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的三级目录处理
            // 这里可以添加代码生成的三级目录逻辑
        }

        // 8. 审计日志下的三级目录
        var auditMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的三级目录处理
            // 这里可以添加审计日志的三级目录逻辑
        }

        // 9. 实时通信下的三级目录
        var realtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            // 实时通信的三级目录处理
            // 这里可以添加实时通信的三级目录逻辑
        }

        // 处理所有三级目录菜单
        foreach (var menu in thirdLevelMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化三级子菜单
    /// </summary>
    private async Task<(int, int)> InitializeThirdMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 1. 日常办公下的三级子菜单
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 1. 日程管理下的三级菜单
            var scheduleMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日程管理" && m.ParentId == routineMenu.Id);
            if (scheduleMenu != null)
            {
                var scheduleSubMenus = HbtDbSeedRoutineMenu.GetRoutineScheduleThirdMenus(scheduleMenu.Id);
                foreach (var menu in scheduleSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 2. 用车管理下的三级菜单
            var carMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "用车管理" && m.ParentId == routineMenu.Id);
            if (carMenu != null)
            {
                var carSubMenus = HbtDbSeedRoutineMenu.GetRoutineCarThirdMenus(carMenu.Id);
                foreach (var menu in carSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 3. 邮件管理下的三级菜单
            var emailMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "邮件管理" && m.ParentId == routineMenu.Id);
            if (emailMenu != null)
            {
                var emailSubMenus = HbtDbSeedRoutineMenu.GetRoutineEmailThirdMenus(emailMenu.Id);
                foreach (var menu in emailSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 4. 会议管理下的三级菜单
            var meetingMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "会议管理" && m.ParentId == routineMenu.Id);
            if (meetingMenu != null)
            {
                var meetingSubMenus = HbtDbSeedRoutineMenu.GetRoutineMeetingThirdMenus(meetingMenu.Id);
                foreach (var menu in meetingSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }
        }

        // 2. 财务管理下的三级子菜单
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 管理会计下的三级菜单
            var managementMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == financeMenu.Id);
            if (managementMenu != null)
            {
                var managementSubMenus = HbtDbSeedFinanceMenu.GetFinanceManagementThirdMenus(managementMenu.Id);
                foreach (var menu in managementSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 控制会计下的三级菜单
            var controlMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == financeMenu.Id);
            if (controlMenu != null)
            {
                var controlSubMenus = HbtDbSeedFinanceMenu.GetFinanceControlThirdMenus(controlMenu.Id);
                foreach (var menu in controlSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }
        }

        // 3. 后勤管理下的三级子菜单
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 生产管理下的三级菜单
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var productionSubMenus = HbtDbSeedLogisticsMenu.GetProductionThirdMenus(productionMenu.Id);
                foreach (var menu in productionSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 销售管理下的三级菜单
            var salesMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
            if (salesMenu != null)
            {
                var salesSubMenus = HbtDbSeedLogisticsMenu.GetSalesThirdMenus(salesMenu.Id);
                foreach (var menu in salesSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 质量管理下的三级菜单
            var qualityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
            if (qualityMenu != null)
            {
                var qualitySubMenus = HbtDbSeedLogisticsMenu.GetQualityThirdMenus(qualityMenu.Id);
                foreach (var menu in qualitySubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 设备管理下的三级菜单
            var equipmentMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
            if (equipmentMenu != null)
            {
                var equipmentSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentThirdMenus(equipmentMenu.Id);
                foreach (var menu in equipmentSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 客服管理下的三级菜单
            var serviceMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
            if (serviceMenu != null)
            {
                var serviceSubMenus = HbtDbSeedLogisticsMenu.GetServiceThirdMenus(serviceMenu.Id);
                foreach (var menu in serviceSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 客诉管理下的三级菜单
            var complaintMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "客诉管理" && m.ParentId == logisticsMenu.Id);
            if (complaintMenu != null)
            {
                var complaintSubMenus = HbtDbSeedLogisticsMenu.GetComplaintThirdMenus(complaintMenu.Id);
                foreach (var menu in complaintSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }

            // 项目管理下的三级菜单
            var projectMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "项目管理" && m.ParentId == logisticsMenu.Id);
            if (projectMenu != null)
            {
                var projectSubMenus = HbtDbSeedLogisticsMenu.GetProjectThirdMenus(projectMenu.Id);
                foreach (var menu in projectSubMenus)
                {
                    menu.CreateBy = "Hbt365";
                    menu.CreateTime = DateTime.Now;
                    menu.UpdateBy = "Hbt365";
                    menu.UpdateTime = DateTime.Now;
                    var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
                    menuNameToId[menu.MenuName] = savedMenu.Id;
                    if (isNew) insertCount++; else updateCount++;
                }
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化四级子菜单
    /// </summary>
    private async Task<(int, int)> InitializeFourthMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var fourthLevelMenus = new List<HbtMenu>();

        // 1. 日常办公下的四级子菜单
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 图书管理下的四级子菜单
            var bookMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "图书管理" && m.ParentId == routineMenu.Id);
            if (bookMenu != null)
            {
                // 图书库存下的四级子菜单
                var bookInventoryMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "库存" && m.ParentId == bookMenu.Id);
                if (bookInventoryMenu != null)
                {
                    var bookInventorySubMenus = HbtDbSeedRoutineMenu.GetRoutineBookInventoryFourthMenus(bookInventoryMenu.Id);
                    fourthLevelMenus.AddRange(bookInventorySubMenus);
                }

                // 图书领用下的四级子菜单
                var bookUsageMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == bookMenu.Id);
                if (bookUsageMenu != null)
                {
                    var bookUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineBookUsageFourthMenus(bookUsageMenu.Id);
                    fourthLevelMenus.AddRange(bookUsageSubMenus);
                }
            }

            // 办公用品下的四级子菜单
            var officeSuppliesMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
            if (officeSuppliesMenu != null)
            {
                // 办公用品库存下的四级子菜单
                var officeSuppliesInventoryMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "库存" && m.ParentId == officeSuppliesMenu.Id);
                if (officeSuppliesInventoryMenu != null)
                {
                    var officeSuppliesInventorySubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesInventoryFourthMenus(officeSuppliesInventoryMenu.Id);
                    fourthLevelMenus.AddRange(officeSuppliesInventorySubMenus);
                }

                // 办公用品领用下的四级子菜单
                var officeSuppliesUsageMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == officeSuppliesMenu.Id);
                if (officeSuppliesUsageMenu != null)
                {
                    var officeSuppliesUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesUsageFourthMenus(officeSuppliesUsageMenu.Id);
                    fourthLevelMenus.AddRange(officeSuppliesUsageSubMenus);
                }
            }

            // 文件管理下的四级子菜单
            var fileMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenu != null)
            {
                // 日常文件下的四级子菜单
                var dailyFileMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常" && m.ParentId == fileMenu.Id);
                if (dailyFileMenu != null)
                {
                    var dailyFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineDailyFileFourthMenus(dailyFileMenu.Id);
                    fourthLevelMenus.AddRange(dailyFileSubMenus);
                }

                // ISO文件下的四级子菜单
                var isoFileMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "ISO" && m.ParentId == fileMenu.Id);
                if (isoFileMenu != null)
                {
                    var isoFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineIsoFileFourthMenus(isoFileMenu.Id);
                    fourthLevelMenus.AddRange(isoFileSubMenus);
                }

                // 公文文件下的四级子菜单
                var documentFileMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "公文" && m.ParentId == fileMenu.Id);
                if (documentFileMenu != null)
                {
                    var documentFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineDocumentFileFourthMenus(documentFileMenu.Id);
                    fourthLevelMenus.AddRange(documentFileSubMenus);
                }
            }

            // 费用管理下的四级子菜单
            var expenseMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "费用管理" && m.ParentId == routineMenu.Id);
            if (expenseMenu != null)
            {
                // 日常费用下的四级子菜单
                var dailyExpenseMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常费用" && m.ParentId == expenseMenu.Id);
                if (dailyExpenseMenu != null)
                {
                    var dailyExpenseSubMenus = HbtDbSeedRoutineMenu.GetRoutineDailyExpenseFourthMenus(dailyExpenseMenu.Id);
                    fourthLevelMenus.AddRange(dailyExpenseSubMenus);
                }

                // 出差费用下的四级子菜单
                var travelExpenseMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "出差费用" && m.ParentId == expenseMenu.Id);
                if (travelExpenseMenu != null)
                {
                    var travelExpenseSubMenus = HbtDbSeedRoutineMenu.GetRoutineTravelExpenseFourthMenus(travelExpenseMenu.Id);
                    fourthLevelMenus.AddRange(travelExpenseSubMenus);
                }
            }

            // 公告通知下的四级子菜单
            var noticeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
            if (noticeMenu != null)
            {
                // 消息下的四级子菜单
                var messageMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "消息" && m.ParentId == noticeMenu.Id);
                if (messageMenu != null)
                {
                    var messageSubMenus = HbtDbSeedRoutineMenu.GetRoutineMessageFourthMenus(messageMenu.Id);
                    fourthLevelMenus.AddRange(messageSubMenus);
                }

                // 公告下的四级子菜单
                var announcementMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "公告" && m.ParentId == noticeMenu.Id);
                if (announcementMenu != null)
                {
                    var announcementSubMenus = HbtDbSeedRoutineMenu.GetRoutineAnnouncementFourthMenus(announcementMenu.Id);
                    fourthLevelMenus.AddRange(announcementSubMenus);
                }

                // 通知下的四级子菜单
                var notificationMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "通知" && m.ParentId == noticeMenu.Id);
                if (notificationMenu != null)
                {
                    var notificationSubMenus = HbtDbSeedRoutineMenu.GetRoutineNotificationFourthMenus(notificationMenu.Id);
                    fourthLevelMenus.AddRange(notificationSubMenus);
                }
            }

            // 医务管理下的四级子菜单
            var medicalMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
            if (medicalMenu != null)
            {
                // 药品下的四级子菜单
                var medicineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "药品" && m.ParentId == medicalMenu.Id);
                if (medicineMenu != null)
                {
                    var medicineSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicineFourthMenus(medicineMenu.Id);
                    fourthLevelMenus.AddRange(medicineSubMenus);
                }

                // 医务领用下的四级子菜单
                var medicalUsageMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == medicalMenu.Id);
                if (medicalUsageMenu != null)
                {
                    var medicalUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicalUsageFourthMenus(medicalUsageMenu.Id);
                    fourthLevelMenus.AddRange(medicalUsageSubMenus);
                }
            }

            // 人事考勤下的四级子菜单
            var hrMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "人事考勤" && m.ParentId == routineMenu.Id);
            if (hrMenu != null)
            {
                // 招聘下的四级子菜单
                var recruitmentMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "招聘" && m.ParentId == hrMenu.Id);
                if (recruitmentMenu != null)
                {
                    var recruitmentSubMenus = HbtDbSeedRoutineMenu.GetRoutineRecruitmentFourthMenus(recruitmentMenu.Id);
                    fourthLevelMenus.AddRange(recruitmentSubMenus);
                }

                // 调岗下的四级子菜单
                var transferMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "调岗" && m.ParentId == hrMenu.Id);
                if (transferMenu != null)
                {
                    var transferSubMenus = HbtDbSeedRoutineMenu.GetRoutineTransferFourthMenus(transferMenu.Id);
                    fourthLevelMenus.AddRange(transferSubMenus);
                }

                // 加班下的四级子菜单
                var overtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "加班" && m.ParentId == hrMenu.Id);
                if (overtimeMenu != null)
                {
                    var overtimeSubMenus = HbtDbSeedRoutineMenu.GetRoutineOvertimeFourthMenus(overtimeMenu.Id);
                    fourthLevelMenus.AddRange(overtimeSubMenus);
                }

                // 请假下的四级子菜单
                var leaveMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "请假" && m.ParentId == hrMenu.Id);
                if (leaveMenu != null)
                {
                    var leaveSubMenus = HbtDbSeedRoutineMenu.GetRoutineLeaveFourthMenus(leaveMenu.Id);
                    fourthLevelMenus.AddRange(leaveSubMenus);
                }

                // 出差下的四级子菜单
                var tripMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "出差" && m.ParentId == hrMenu.Id);
                if (tripMenu != null)
                {
                    var tripSubMenus = HbtDbSeedRoutineMenu.GetRoutineBusinessTripFourthMenus(tripMenu.Id);
                    fourthLevelMenus.AddRange(tripSubMenus);
                }
            }
        }

        // 2. 财务管理下的四级子菜单
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 全面预算
            var financeBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (financeBudgetMenu != null)
            {
                // 预算控制下的四级菜单
                var financeBudgetControlMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "预算控制" && m.ParentId == financeBudgetMenu.Id);
                if (financeBudgetControlMenu != null)
                {
                    var budgetControlMenus = HbtDbSeedFinanceMenu.GetBudgetControlFourthMenus(financeBudgetControlMenu.Id);
                    fourthLevelMenus.AddRange(budgetControlMenus);
                }
            }
        }

        // 3. 后勤管理下的四级子菜单
        var logisticsMenuForFourth = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenuForFourth != null)
        {
            // 物料管理下的四级子菜单
            var materialMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (materialMenu != null)
            {
                // 物料目录下的四级子菜单
                var materialSubMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "物料目录" && m.ParentId == materialMenu.Id);
                if (materialSubMenu != null)
                {
                    var materialMaterialSubMenus = HbtDbSeedLogisticsMenu.GetMaterialMaterialFourthMenus(materialSubMenu.Id);
                    fourthLevelMenus.AddRange(materialMaterialSubMenus);
                }

                // 采购目录下的四级子菜单
                var purchaseSubMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "采购目录" && m.ParentId == materialMenu.Id);
                if (purchaseSubMenu != null)
                {
                    var materialPurchaseSubMenus = HbtDbSeedLogisticsMenu.GetMaterialPurchaseFourthMenus(purchaseSubMenu.Id);
                    fourthLevelMenus.AddRange(materialPurchaseSubMenus);
                }
            }

            // 生产管理下的四级子菜单
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (productionMenu != null)
            {
                // 设计变更下的四级子菜单
                var productionChangeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (productionChangeMenu != null)
                {
                    var productionChangeSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeFourthMenus(productionChangeMenu.Id);
                    fourthLevelMenus.AddRange(productionChangeSubMenus);

                    // 设变录入下的四级子菜单
                    var productionChangeInputMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == productionChangeMenu.Id);
                    if (productionChangeInputMenu != null)
                    {
                        var productionChangeInputSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(productionChangeInputMenu.Id);
                        fourthLevelMenus.AddRange(productionChangeInputSubMenus);
                    }
                }
            }
        }

        // 处理所有四级子菜单
        foreach (var menu in fourthLevelMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化四级目录
    /// </summary>
    private async Task<(int, int)> InitializeFourthLevelMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var fourthLevelMenus = new List<HbtMenu>();

        // 1. 日常办公下的四级目录
        var routineMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 日常办公的四级目录处理
            // 这里可以添加日常办公的四级目录逻辑
        }

        // 2. 财务管理下的四级目录
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            var budgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var formulationMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
                if (formulationMenu != null)
                {
                    var formulationSubMenus = HbtDbSeedFinanceMenu.GetFormulationFourthLevelMenus(formulationMenu.Id);
                    fourthLevelMenus.AddRange(formulationSubMenus);
                }
            }
        }
        else
        {
            _logger.Info("未找到财务管理目录");
        }

        // 3. 后勤管理下的四级目录
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 生产管理
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                // 设计变更下的四级目录
                var changeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (changeMenu != null)
                {
                    var changeSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeFourthLevelMenus(changeMenu.Id);
                    fourthLevelMenus.AddRange(changeSubMenus);
                }

                // 制造管理下的四级目录
                var ophMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (ophMenu != null)
                {
                    var ophSubMenus = HbtDbSeedLogisticsMenu.GetProductionOphFourthLevelMenus(ophMenu.Id);
                    fourthLevelMenus.AddRange(ophSubMenus);
                }
            }
        }

        // 4. 工作流程下的四级目录
        var workflowMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的四级目录处理
            // 这里可以添加工作流程的四级目录逻辑
        }

        // 5. 身份认证下的四级目录
        var identityMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的四级目录处理
            // 这里可以添加身份认证的四级目录逻辑
        }

        // 6. 核心功能下的四级目录
        var adminMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "核心功能" && m.ParentId == 0);
        if (adminMenu != null)
        {
            // 核心功能的四级目录处理
            // 这里可以添加核心功能的四级目录逻辑
        }

        // 7. 代码生成下的四级目录
        var generatorMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的四级目录处理
            // 这里可以添加代码生成的四级目录逻辑
        }

        // 8. 审计日志下的四级目录
        var auditMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的四级目录处理
            // 这里可以添加审计日志的四级目录逻辑
        }

        // 9. 实时通信下的四级目录
        var realtimeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
        if (realtimeMenu != null)
        {
            // 实时通信的四级目录处理
            // 这里可以添加实时通信的四级目录逻辑
        }

        // 处理所有四级目录菜单
        foreach (var menu in fourthLevelMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化五级目录
    /// </summary>
    private async Task<(int, int)> InitializeFifthLevelMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var fifthLevelMenus = new List<HbtMenu>();

        // 财务管理模块没有五级目录，此方法留空或移除相关逻辑

        // 处理所有五级目录菜单
        foreach (var menu in fifthLevelMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化五级子菜单
    /// </summary>
    private async Task<(int, int)> InitializeFifthMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;
        var fifthMenus = new List<HbtMenu>();

        // 财务管理
        var financeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务管理" && m.ParentId == 0);
        if (financeMenu != null)
        {
            var budgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var formulationMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
                if (formulationMenu != null)
                {
                    // 销售预算下的五级菜单
                    var salesBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "销售预算" && m.ParentId == formulationMenu.Id);
                    if (salesBudgetMenu != null)
                    {
                        var salesBudgetSubMenus = HbtDbSeedFinanceMenu.GetSalesBudgetFifthMenus(salesBudgetMenu.Id);
                        fifthMenus.AddRange(salesBudgetSubMenus);
                    }

                    // 生产预算下的五级菜单
                    var productionBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产预算" && m.ParentId == formulationMenu.Id);
                    if (productionBudgetMenu != null)
                    {
                        var productionBudgetSubMenus = HbtDbSeedFinanceMenu.GetProductionBudgetFifthMenus(productionBudgetMenu.Id);
                        fifthMenus.AddRange(productionBudgetSubMenus);
                    }

                    // 成本预算下的五级菜单
                    var costBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "成本预算" && m.ParentId == formulationMenu.Id);
                    if (costBudgetMenu != null)
                    {
                        var costBudgetSubMenus = HbtDbSeedFinanceMenu.GetCostBudgetFifthMenus(costBudgetMenu.Id);
                        fifthMenus.AddRange(costBudgetSubMenus);
                    }

                    // 费用预算下的五级菜单
                    var expenseBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "费用预算" && m.ParentId == formulationMenu.Id);
                    if (expenseBudgetMenu != null)
                    {
                        var expenseBudgetSubMenus = HbtDbSeedFinanceMenu.GetExpenseBudgetFifthMenus(expenseBudgetMenu.Id);
                        fifthMenus.AddRange(expenseBudgetSubMenus);
                    }

                    // 财务预算下的五级菜单
                    var financialBudgetMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "财务预算" && m.ParentId == formulationMenu.Id);
                    if (financialBudgetMenu != null)
                    {
                        var financialBudgetSubMenus = HbtDbSeedFinanceMenu.GetFinancialBudgetFifthMenus(financialBudgetMenu.Id);
                        fifthMenus.AddRange(financialBudgetSubMenus);
                    }
                }
            }
        }

        // 后勤管理
        var logisticsMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                // 设计变更 -> 设变录入
                var changeMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (changeMenu != null)
                {
                    var inputMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == changeMenu.Id);
                    if (inputMenu != null)
                    {
                        var inputSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(inputMenu.Id);
                        fifthMenus.AddRange(inputSubMenus);
                    }
                }

                var ophMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (ophMenu != null)
                {
                    // 制一课下的五级菜单
                    var workshop1Menu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制一课" && m.ParentId == ophMenu.Id);
                    if (workshop1Menu != null)
                    {
                        var workshop1SubMenus = HbtDbSeedLogisticsMenu.GetProductionOphWorkshop1FifthMenus(workshop1Menu.Id);
                        fifthMenus.AddRange(workshop1SubMenus);
                    }

                    // 制二课下的五级菜单
                    var workshop2Menu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制二课" && m.ParentId == ophMenu.Id);
                    if (workshop2Menu != null)
                    {
                        var workshop2SubMenus = HbtDbSeedLogisticsMenu.GetProductionOphWorkshop2FifthMenus(workshop2Menu.Id);
                        fifthMenus.AddRange(workshop2SubMenus);
                    }
                }
            }
        }

        // 后勤管理
        var logisticsMenuForFifth = await _menuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenuForFifth != null)
        {
            var productionMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenuForFifth.Id);
            if (productionMenu != null)
            {
                var productionOphMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (productionOphMenu != null)
                {
                    // 制一课下的五级菜单
                    var workshop1Menu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制一课" && m.ParentId == productionOphMenu.Id);
                    if (workshop1Menu != null)
                    {
                        var workshop1SubMenus = HbtDbSeedLogisticsMenu.GetProductionOphWorkshop1FifthMenus(workshop1Menu.Id);
                        fifthMenus.AddRange(workshop1SubMenus);
                    }

                    // 制二课下的五级菜单
                    var workshop2Menu = await _menuRepository.GetFirstAsync(m => m.MenuName == "制二课" && m.ParentId == productionOphMenu.Id);
                    if (workshop2Menu != null)
                    {
                        var workshop2SubMenus = HbtDbSeedLogisticsMenu.GetProductionOphWorkshop2FifthMenus(workshop2Menu.Id);
                        fifthMenus.AddRange(workshop2SubMenus);
                    }
                }
            }
        }

        // 处理所有五级菜单
        foreach (var menu in fifthMenus)
        {
            menu.CreateBy = "Hbt365";
            menu.CreateTime = DateTime.Now;
            menu.UpdateBy = "Hbt365";
            menu.UpdateTime = DateTime.Now;
            var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
            menuNameToId[menu.MenuName] = savedMenu.Id;

            if (isNew) insertCount++;
            else updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化六级菜单
    /// </summary>
    private async Task<(int, int)> InitializeSixthMenusAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 财务管理模块没有六级菜单，此方法留空或移除相关逻辑

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
                    button.CreateBy = "Hbt365";
                    button.CreateTime = DateTime.Now;
                    button.UpdateBy = "Hbt365";
                    button.UpdateTime = DateTime.Now;
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
            
            // 创建后重新查询以获取正确的ID
            var createdMenu = await _menuRepository.GetFirstAsync(m => m.MenuName == menu.MenuName && m.ParentId == menu.ParentId);
            return (true, createdMenu ?? menu);
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
        // 修复：只有当新的MenuType是目录时，才强制更新
        if (menu.MenuType == 0)
        {
            existingMenu.MenuType = 0;
        }
        existingMenu.Visible = menu.Visible;
        existingMenu.Status = menu.Status;
        existingMenu.Perms = menu.Perms;
        existingMenu.Icon = menu.Icon;

        existingMenu.Remark = menu.Remark;
        existingMenu.UpdateBy = "Hbt365";
        existingMenu.UpdateTime = DateTime.Now;

        await _menuRepository.UpdateAsync(existingMenu);
        _logger.Info($"[更新] {(isTopLevel ? "顶级" : "")}菜单 '{existingMenu.MenuName}' (ParentId: {existingMenu.ParentId}) 更新成功");
        return (false, existingMenu);
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

        // 认证通用按钮
        var buttonIdNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消", "授权", "分配", "重置密码", "变更密码" };
        var buttonIdPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke", "authorize", "allocate", "resetpwd", "changepwd" };

        // 代码生成按钮
        var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成代码", "预览代码", "下载代码", "同步数据库", "导入", "导出", "导入模板", "字段", "表", "数据库","初始化" };
        var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export", "template", "columns", "tables", "databases", "initialize" };

        // 工作流按钮
        var buttonFlowNames = new[] { "查询", "新增", "修改", "删除", "发布","开始", "停用", "挂起", "恢复", "转办", "委托", "退回", "终止", "导入", "导出","导入模板", "打印" };
        var buttonFlowPerms = new[] { "query", "create", "update", "delete", "publish", "start", "stop", "suspend", "resume", "transfer", "delegate", "reject", "terminate", "import", "export", "template", "print" };

        // 日常办公按钮
        var buttonRoutineNames = new[] {
            "查询", "新增", "修改", "删除", "详情",
            // 文档操作
            "保存草稿", "删除草稿", "发送", "撤回",
            // 阅读操作
            "已读", "未读", "传阅", "签收", "催办",
            // 其他操作
            "打印", "导出", "归档", "导入模板","导入","开始","结束","暂停","恢复","终止","运行","停止"
        };
        var buttonRoutinePerms = new[] {
            "query", "create", "update", "delete", "detail",
            // 文档权限
            "draft", "deletedraft", "send", "withdraw",
            // 阅读权限
            "read", "unread", "circulate", "sign", "urge",
            // 其他权限
            "print", "export", "archive", "template", "import", "start", "end", "pause", "resume", "terminate", "run", "stop"
        };

        // 从菜单的权限标识中获取菜单标识
        var menuPerm = menu.Perms?.Split(':').Length > 1 
            ? menu.Perms.Split(':')[1].ToLower() 
            : string.Empty;

        string[] names;
        string[] perms;

        // 根据模块前缀选择对应的按钮配置
        switch (modulePrefix.ToLower())
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
                Perms = $"{modulePrefix.ToLower()}:{menuPerm}:{perms[i].ToLower()}",
                Icon = string.Empty,

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