//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 菜单数据初始化主类 - 使用仓储工厂模式
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Infrastructure.Data.Seeds.Biz;
using Lean.Hbt.Infrastructure.Data.Seeds.Auth;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 菜单数据初始化主类
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class HbtDbSeedMenu
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtMenu> MenuRepository => _repositoryFactory.GetAuthRepository<HbtMenu>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedMenu(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                MenuName = "财务核算",
                TransKey = "menu.accounting._self",
                ParentId = 0,
                OrderNum = 2,
                Path = "accounting",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "AccountBookOutlined",
                Remark = "财务核算目录",
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
                MenuName = "人力资源",
                TransKey = "menu.human._self",
                ParentId = 0,
                OrderNum = 4,
                Path = "hrm",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "TeamOutlined",
                Remark = "人力资源目录",
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
                OrderNum = 5,
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
                OrderNum = 6,
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
            },
            new HbtMenu
            {
                MenuName = "广告管理",
                TransKey = "menu.advertisement._self",
                ParentId = 0,
                OrderNum = 10,
                Path = "advertisement",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                Status = 0,
                Perms = "",
                Icon = "PictureOutlined",
                Remark = "广告管理目录",
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
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

        // 2. 财务核算二级目录
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
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
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
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
        var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            var workflowSubMenus = HbtDbSeedWorkflowMenu.GetWorkflowSecondLevelMenus(workflowMenu.Id);
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

        // 5. 人力资源二级目录
        var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
        if (hrmMenu != null)
        {
            var hrmSubMenus = HbtDbSeedHrmMenu.GetHrmSecondMenus(hrmMenu.Id);
            foreach (var menu in hrmSubMenus)
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

        // 6. 身份认证二级目录
        var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
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

        // 7. 代码生成二级目录
        var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
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
        var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
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
        var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
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

        // 10. 广告管理二级目录
        var advertisementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "广告管理" && m.ParentId == 0);
        if (advertisementMenu != null)
        {
            var advertisementSubMenus = HbtDbSeedAdvertisementMenu.GetAdvertisementSecondMenus(advertisementMenu.Id);
            foreach (var menu in advertisementSubMenus)
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 日常办公的二级子菜单处理
            // 这里可以添加日常办公的二级子菜单逻辑
        }

        // 2. 财务核算二级子菜单
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 财务核算的二级子菜单处理
            // 这里可以添加财务核算的二级子菜单逻辑
        }

        // 3. 后勤管理二级子菜单
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 后勤管理的二级子菜单处理
            // 这里可以添加后勤管理的二级子菜单逻辑
        }

        // 4. 工作流程二级子菜单
        var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的二级子菜单处理
            // 这里可以添加工作流程的二级子菜单逻辑
        }

        // 5. 人力资源二级子菜单
        var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
        if (hrmMenu != null)
        {
            // 人力资源的二级子菜单处理
            // 这里可以添加人力资源的二级子菜单逻辑
        }

        // 6. 身份认证二级子菜单
        var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的二级子菜单处理
            // 这里可以添加身份认证的二级子菜单逻辑
        }

        // 7. 代码生成二级子菜单
        var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的二级子菜单处理
            // 这里可以添加代码生成的二级子菜单逻辑
        }

        // 8. 审计日志二级子菜单
        var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的二级子菜单处理
            // 这里可以添加审计日志的二级子菜单逻辑
        }

        // 9. 实时通信二级子菜单
        var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 新闻管理下的三级菜单
            var newsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "新闻管理" && m.ParentId == routineMenu.Id);
            if (newsMenu != null)
            {
                var newsSubMenus = HbtDbSeedRoutineMenu.GetRoutineNewsThirdMenus(newsMenu.Id);
                thirdLevelMenus.AddRange(newsSubMenus);
            }

            // 办公用品下的三级目录
            var officeSuppliesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
            if (officeSuppliesMenu != null)
            {
                var officeSuppliesSubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesThirdLevelMenus(officeSuppliesMenu.Id);
                thirdLevelMenus.AddRange(officeSuppliesSubMenus);
            }

            // 文件管理下的三级目录
            var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenu != null)
            {
                var fileSubMenus = HbtDbSeedRoutineMenu.GetRoutineFileThirdLevelMenus(fileMenu.Id);
                thirdLevelMenus.AddRange(fileSubMenus);
            }



            // 公告通知下的三级目录
            var noticeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
            if (noticeMenu != null)
            {
                var noticeSubMenus = HbtDbSeedRoutineMenu.GetRoutineNoticeThirdLevelMenus(noticeMenu.Id);
                thirdLevelMenus.AddRange(noticeSubMenus);
            }

            // 医务管理下的三级目录
            var medicalMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
            if (medicalMenu != null)
            {
                var medicalSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicalThirdLevelMenus(medicalMenu.Id);
                thirdLevelMenus.AddRange(medicalSubMenus);
            }



            // 图书管理下的三级目录
            var bookMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "图书管理" && m.ParentId == routineMenu.Id);
            if (bookMenu != null)
            {
                var bookSubMenus = HbtDbSeedRoutineMenu.GetRoutineBookThirdLevelMenus(bookMenu.Id);
                thirdLevelMenus.AddRange(bookSubMenus);
            }
        }

        // 2. 财务核算下的三级目录
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 管理会计下的三级目录
            var managementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == financeMenu.Id);
            if (managementMenu != null)
            {
                var managementSubMenus = HbtDbSeedFinanceMenu.GetFinanceManagementThirdMenus(managementMenu.Id);
                thirdLevelMenus.AddRange(managementSubMenus);
            }

            // 控制会计下的三级目录
            var controlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == financeMenu.Id);
            if (controlMenu != null)
            {
                var controlSubMenus = HbtDbSeedFinanceMenu.GetFinanceControlThirdMenus(controlMenu.Id);
                thirdLevelMenus.AddRange(controlSubMenus);
            }

            // 全面预算下的三级目录
            var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var budgetSubMenus = HbtDbSeedFinanceMenu.GetBudgetThirdLevelMenus(budgetMenu.Id);
                thirdLevelMenus.AddRange(budgetSubMenus);
            }
        }
        else
        {
            _logger.Info("未找到财务核算目录");
        }

        // 3. 后勤管理下的三级目录
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 物料管理下的三级目录
            var materialMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenu.Id);
            if (materialMenu != null)
            {
                var materialSubMenus = HbtDbSeedLogisticsMenu.GetMaterialThirdLevelMenus(materialMenu.Id);
                thirdLevelMenus.AddRange(materialSubMenus);
            }

            // 生产管理下的三级目录
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var productionSubMenus = HbtDbSeedLogisticsMenu.GetProductionThirdLevelMenus(productionMenu.Id);
                thirdLevelMenus.AddRange(productionSubMenus);
            }

            // 质量管理下的三级目录
            var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
            if (qualityMenu != null)
            {
                var qualitySubMenus = HbtDbSeedLogisticsMenu.GetQualityThirdLevelMenus(qualityMenu.Id);
                thirdLevelMenus.AddRange(qualitySubMenus);
            }

            // 销售管理下的三级目录
            var salesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
            if (salesMenu != null)
            {
                var salesSubMenus = HbtDbSeedLogisticsMenu.GetSalesThirdMenus(salesMenu.Id);
                thirdLevelMenus.AddRange(salesSubMenus);
            }

            // 设备管理下的三级目录
            var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
            if (equipmentMenu != null)
            {
                var equipmentSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentThirdLevelMenus(equipmentMenu.Id);
                thirdLevelMenus.AddRange(equipmentSubMenus);
            }

            // 客服管理下的三级目录
            var serviceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
            if (serviceMenu != null)
            {
                var serviceSubMenus = HbtDbSeedLogisticsMenu.GetServiceThirdLevelMenus(serviceMenu.Id);
                thirdLevelMenus.AddRange(serviceSubMenus);
            }

            // 项目管理下的三级目录 - 已移除，项目管理功能已整合到其他模块
        }

        // 4. 工作流程下的三级目录
        var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的三级目录处理
            // 这里可以添加工作流程的三级目录逻辑
        }

        // 5. 人力资源下的三级目录
        var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
        if (hrmMenu != null)
        {
            // 考勤管理下的三级目录
            var attendanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "考勤管理" && m.ParentId == hrmMenu.Id);
            if (attendanceMenu != null)
            {
                var attendanceSubMenus = HbtDbSeedHrmMenu.GetHrmAttendanceThirdMenus(attendanceMenu.Id);
                thirdLevelMenus.AddRange(attendanceSubMenus);
            }

            // 福利管理下的三级目录
            var benefitMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "福利管理" && m.ParentId == hrmMenu.Id);
            if (benefitMenu != null)
            {
                var benefitSubMenus = HbtDbSeedHrmMenu.GetHrmBenefitThirdMenus(benefitMenu.Id);
                thirdLevelMenus.AddRange(benefitSubMenus);
            }

            // 员工管理下的三级目录
            var employeeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "员工管理" && m.ParentId == hrmMenu.Id);
            if (employeeMenu != null)
            {
                var employeeSubMenus = HbtDbSeedHrmMenu.GetHrmEmployeeThirdMenus(employeeMenu.Id);
                thirdLevelMenus.AddRange(employeeSubMenus);
            }

            // 请假管理下的三级目录
            var leaveMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "请假管理" && m.ParentId == hrmMenu.Id);
            if (leaveMenu != null)
            {
                var leaveSubMenus = HbtDbSeedHrmMenu.GetHrmLeaveThirdMenus(leaveMenu.Id);
                thirdLevelMenus.AddRange(leaveSubMenus);
            }

            // 组织管理下的三级目录
            var organizationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "组织管理" && m.ParentId == hrmMenu.Id);
            if (organizationMenu != null)
            {
                var organizationSubMenus = HbtDbSeedHrmMenu.GetHrmOrganizationThirdMenus(organizationMenu.Id);
                thirdLevelMenus.AddRange(organizationSubMenus);
            }

            // 绩效管理下的三级目录
            var performanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "绩效管理" && m.ParentId == hrmMenu.Id);
            if (performanceMenu != null)
            {
                var performanceSubMenus = HbtDbSeedHrmMenu.GetHrmPerformanceThirdMenus(performanceMenu.Id);
                thirdLevelMenus.AddRange(performanceSubMenus);
            }

            // 招聘管理下的三级目录
            var recruitmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "招聘管理" && m.ParentId == hrmMenu.Id);
            if (recruitmentMenu != null)
            {
                var recruitmentSubMenus = HbtDbSeedHrmMenu.GetHrmRecruitmentThirdMenus(recruitmentMenu.Id);
                thirdLevelMenus.AddRange(recruitmentSubMenus);
            }

            // 薪酬管理下的三级目录
            var salaryMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "薪酬管理" && m.ParentId == hrmMenu.Id);
            if (salaryMenu != null)
            {
                var salarySubMenus = HbtDbSeedHrmMenu.GetHrmSalaryThirdMenus(salaryMenu.Id);
                thirdLevelMenus.AddRange(salarySubMenus);
            }

            // 培训管理下的三级目录
            var trainingMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "培训管理" && m.ParentId == hrmMenu.Id);
            if (trainingMenu != null)
            {
                var trainingSubMenus = HbtDbSeedHrmMenu.GetHrmTrainingThirdMenus(trainingMenu.Id);
                thirdLevelMenus.AddRange(trainingSubMenus);
            }

            // 报表统计下的三级目录
            var reportMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "报表统计" && m.ParentId == hrmMenu.Id);
            if (reportMenu != null)
            {
                var reportSubMenus = HbtDbSeedHrmMenu.GetHrmReportThirdMenus(reportMenu.Id);
                thirdLevelMenus.AddRange(reportSubMenus);
            }
        }

        // 6. 身份认证下的三级目录
        var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的三级目录处理
            // 这里可以添加身份认证的三级目录逻辑
        }

        // 7. 代码生成下的三级目录
        var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的三级目录处理
            // 这里可以添加代码生成的三级目录逻辑
        }

        // 8. 审计日志下的三级目录
        var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的三级目录处理
            // 这里可以添加审计日志的三级目录逻辑
        }

        // 9. 实时通信下的三级目录
        var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 1. 基础服务下的三级菜单
            var coreMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "基础服务" && m.ParentId == routineMenu.Id);
            if (coreMenu != null)
            {
                var coreSubMenus = HbtDbSeedRoutineMenu.GetRoutineCoreThirdMenus(coreMenu.Id);
                foreach (var menu in coreSubMenus)
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

            // 2. 日程管理下的三级菜单
            var scheduleMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日程管理" && m.ParentId == routineMenu.Id);
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

            // 3. 用车管理下的三级菜单
            var carMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "用车管理" && m.ParentId == routineMenu.Id);
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



            // 4. 邮件管理下的三级菜单
            var emailMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "邮件管理" && m.ParentId == routineMenu.Id);
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

            // 5. 会议管理下的三级菜单
            var meetingMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "会议管理" && m.ParentId == routineMenu.Id);
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

            // 6. 合同管理下的三级菜单
            var contractMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "合同管理" && m.ParentId == routineMenu.Id);
            if (contractMenu != null)
            {
                var contractSubMenus = HbtDbSeedRoutineMenu.GetRoutineContractThirdMenus(contractMenu.Id);
                foreach (var menu in contractSubMenus)
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

            // 7. 项目管理下的三级菜单
            var projectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "项目管理" && m.ParentId == routineMenu.Id);
            if (projectMenu != null)
            {
                var projectSubMenus = HbtDbSeedRoutineMenu.GetRoutineProjectThirdMenus(projectMenu.Id);
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

            // 8. 任务管理下的三级菜单
            var quartzMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "任务管理" && m.ParentId == routineMenu.Id);
            if (quartzMenu != null)
            {
                var quartzSubMenus = HbtDbSeedRoutineMenu.GetRoutineQuartzThirdMenus(quartzMenu.Id);
                foreach (var menu in quartzSubMenus)
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

        // 2. 财务核算下的三级子菜单
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 管理会计下的三级菜单
            var managementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == financeMenu.Id);
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
            var controlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == financeMenu.Id);
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
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 生产管理下的三级菜单
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var productionSubMenus = HbtDbSeedLogisticsMenu.GetProductionThirdLevelMenus(productionMenu.Id);
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
            var salesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
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
            var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
            if (qualityMenu != null)
            {
                var qualitySubMenus = HbtDbSeedLogisticsMenu.GetQualityThirdLevelMenus(qualityMenu.Id);
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

            // 设备管理下的三级目录
            var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
            if (equipmentMenu != null)
            {
                var equipmentSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentThirdLevelMenus(equipmentMenu.Id);
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

            // 文件管理下的三级菜单
            var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenu != null)
            {
                var fileSubMenus = HbtDbSeedRoutineMenu.GetRoutineFileThirdMenus(fileMenu.Id);
                foreach (var menu in fileSubMenus)
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

            // 项目管理下的三级菜单 - 已移除，项目管理功能已整合到其他模块
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 图书管理下的四级子菜单
            var bookMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "图书管理" && m.ParentId == routineMenu.Id);
            if (bookMenu != null)
            {
                // 图书库存下的四级子菜单
                var bookInventoryMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "库存" && m.ParentId == bookMenu.Id);
                if (bookInventoryMenu != null)
                {
                    var bookInventorySubMenus = HbtDbSeedRoutineMenu.GetRoutineBookInventoryFourthMenus(bookInventoryMenu.Id);
                    fourthLevelMenus.AddRange(bookInventorySubMenus);
                }

                // 图书领用下的四级子菜单
                var bookUsageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == bookMenu.Id);
                if (bookUsageMenu != null)
                {
                    var bookUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineBookUsageFourthMenus(bookUsageMenu.Id);
                    fourthLevelMenus.AddRange(bookUsageSubMenus);
                }
            }

            // 办公用品下的四级子菜单
            var officeSuppliesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
            if (officeSuppliesMenu != null)
            {
                // 办公用品库存下的四级子菜单
                var officeSuppliesInventoryMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "库存" && m.ParentId == officeSuppliesMenu.Id);
                if (officeSuppliesInventoryMenu != null)
                {
                    var officeSuppliesInventorySubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesInventoryFourthMenus(officeSuppliesInventoryMenu.Id);
                    fourthLevelMenus.AddRange(officeSuppliesInventorySubMenus);
                }

                // 办公用品领用下的四级子菜单
                var officeSuppliesUsageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == officeSuppliesMenu.Id);
                if (officeSuppliesUsageMenu != null)
                {
                    var officeSuppliesUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineOfficeSuppliesUsageFourthMenus(officeSuppliesUsageMenu.Id);
                    fourthLevelMenus.AddRange(officeSuppliesUsageSubMenus);
                }
            }

            // 文件管理下的四级子菜单
            var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenu != null)
            {
                // 规章制度下的四级子菜单
                var regulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "规章制度" && m.ParentId == fileMenu.Id);
                if (regulationMenu != null)
                {
                    var regulationSubMenus = HbtDbSeedRoutineMenu.GetRoutineRegulationFourthMenus(regulationMenu.Id);
                    fourthLevelMenus.AddRange(regulationSubMenus);
                }

                // ISO文件下的四级子菜单
                var isoFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "ISO文件" && m.ParentId == fileMenu.Id);
                if (isoFileMenu != null)
                {
                    var isoFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineIsoFileFourthMenus(isoFileMenu.Id);
                    fourthLevelMenus.AddRange(isoFileSubMenus);
                }

                // 公文文件下的四级子菜单
                var documentFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公文文件" && m.ParentId == fileMenu.Id);
                if (documentFileMenu != null)
                {
                    var documentFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineDocumentFileFourthMenus(documentFileMenu.Id);
                    fourthLevelMenus.AddRange(documentFileSubMenus);
                }
            }



            // 公告通知下的四级子菜单
            var noticeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
            if (noticeMenu != null)
            {
                // 消息下的四级子菜单
                var messageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "消息" && m.ParentId == noticeMenu.Id);
                if (messageMenu != null)
                {
                    var messageSubMenus = HbtDbSeedRoutineMenu.GetRoutineMessageFourthMenus(messageMenu.Id);
                    fourthLevelMenus.AddRange(messageSubMenus);
                }

                // 公告下的四级子菜单
                var announcementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告" && m.ParentId == noticeMenu.Id);
                if (announcementMenu != null)
                {
                    var announcementSubMenus = HbtDbSeedRoutineMenu.GetRoutineAnnouncementFourthMenus(announcementMenu.Id);
                    fourthLevelMenus.AddRange(announcementSubMenus);
                }

                // 通知下的四级子菜单
                var notificationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "通知" && m.ParentId == noticeMenu.Id);
                if (notificationMenu != null)
                {
                    var notificationSubMenus = HbtDbSeedRoutineMenu.GetRoutineNotificationFourthMenus(notificationMenu.Id);
                    fourthLevelMenus.AddRange(notificationSubMenus);
                }
            }

            // 医务管理下的四级子菜单
            var medicalMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
            if (medicalMenu != null)
            {
                // 药品下的四级子菜单
                var medicineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "药品" && m.ParentId == medicalMenu.Id);
                if (medicineMenu != null)
                {
                    var medicineSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicineFourthMenus(medicineMenu.Id);
                    fourthLevelMenus.AddRange(medicineSubMenus);
                }

                // 医务领用下的四级子菜单
                var medicalUsageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == medicalMenu.Id);
                if (medicalUsageMenu != null)
                {
                    var medicalUsageSubMenus = HbtDbSeedRoutineMenu.GetRoutineMedicalUsageFourthMenus(medicalUsageMenu.Id);
                    fourthLevelMenus.AddRange(medicalUsageSubMenus);
                }
            }



            // 文件管理下的四级子菜单 - 新增菜单
            var fileMenuForFourth = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
            if (fileMenuForFourth != null)
            {
                // 规章制度下的四级子菜单
                var regulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "规章制度" && m.ParentId == fileMenuForFourth.Id);
                if (regulationMenu != null)
                {
                    var regulationSubMenus = HbtDbSeedRoutineMenu.GetRoutineRegulationFourthMenus(regulationMenu.Id);
                    fourthLevelMenus.AddRange(regulationSubMenus);
                }

                // ISO文件下的四级子菜单
                var isoFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "ISO文件" && m.ParentId == fileMenuForFourth.Id);
                if (isoFileMenu != null)
                {
                    var isoFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineIsoFileFourthMenus(isoFileMenu.Id);
                    fourthLevelMenus.AddRange(isoFileSubMenus);
                }

                // 公文文件下的四级子菜单
                var documentFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公文文件" && m.ParentId == fileMenuForFourth.Id);
                if (documentFileMenu != null)
                {
                    var documentFileSubMenus = HbtDbSeedRoutineMenu.GetRoutineDocumentFileFourthMenus(documentFileMenu.Id);
                    fourthLevelMenus.AddRange(documentFileSubMenus);
                }
            }








        }

        // 2. 财务核算下的四级子菜单
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            // 全面预算
            var financeBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (financeBudgetMenu != null)
            {
                // 预算编制下的四级目录
                var financeBudgetFormulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == financeBudgetMenu.Id);
                if (financeBudgetFormulationMenu != null)
                {
                    var formulationMenus = HbtDbSeedFinanceMenu.GetFormulationFourthLevelMenus(financeBudgetFormulationMenu.Id);
                    fourthLevelMenus.AddRange(formulationMenus);
                }

                // 预算控制下的四级菜单
                var financeBudgetControlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算控制" && m.ParentId == financeBudgetMenu.Id);
                if (financeBudgetControlMenu != null)
                {
                    var budgetControlMenus = HbtDbSeedFinanceMenu.GetBudgetControlFourthMenus(financeBudgetControlMenu.Id);
                    fourthLevelMenus.AddRange(budgetControlMenus);
                }
            }
        }

        // 3. 后勤管理下的四级子菜单
        var logisticsMenuForFourth = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenuForFourth != null)
        {
            // 物料管理下的四级子菜单
            var materialMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (materialMenu != null)
            {
                // 物料信息下的四级子菜单
                var materialSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料信息" && m.ParentId == materialMenu.Id);
                if (materialSubMenu != null)
                {
                    var materialMaterialSubMenus = HbtDbSeedLogisticsMenu.GetMaterialMaterialFourthMenus(materialSubMenu.Id);
                    fourthLevelMenus.AddRange(materialMaterialSubMenus);
                }

                // 采购管理下的四级子菜单
                var purchaseSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "采购管理" && m.ParentId == materialMenu.Id);
                if (purchaseSubMenu != null)
                {
                    var materialPurchaseSubMenus = HbtDbSeedLogisticsMenu.GetMaterialPurchaseFourthMenus(purchaseSubMenu.Id);
                    fourthLevelMenus.AddRange(materialPurchaseSubMenus);
                }

                // 样品管理下的四级子菜单
                var sampleSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "样品管理" && m.ParentId == materialMenu.Id);
                if (sampleSubMenu != null)
                {
                    var materialSampleSubMenus = HbtDbSeedLogisticsMenu.GetMaterialSampleFourthMenus(sampleSubMenu.Id);
                    fourthLevelMenus.AddRange(materialSampleSubMenus);
                }

                // 图纸管理下的四级子菜单
                var drawingSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "图纸管理" && m.ParentId == materialMenu.Id);
                if (drawingSubMenu != null)
                {
                    var materialDrawingSubMenus = HbtDbSeedLogisticsMenu.GetMaterialDrawingFourthMenus(drawingSubMenu.Id);
                    fourthLevelMenus.AddRange(materialDrawingSubMenus);
                }

                // 客供品管理下的四级子菜单
                var customerSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客供品管理" && m.ParentId == materialMenu.Id);
                if (customerSubMenu != null)
                {
                    var materialCustomerSubMenus = HbtDbSeedLogisticsMenu.GetMaterialCustomerFourthMenus(customerSubMenu.Id);
                    fourthLevelMenus.AddRange(materialCustomerSubMenus);
                }
            }

            // 生产管理下的四级子菜单
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (productionMenu != null)
            {
                // 基础数据下的四级子菜单
                var productionBasicMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "基础数据" && m.ParentId == productionMenu.Id);
                if (productionBasicMenu != null)
                {
                    var productionBasicSubMenus = HbtDbSeedLogisticsMenu.GetProductionBasicFourthMenus(productionBasicMenu.Id);
                    fourthLevelMenus.AddRange(productionBasicSubMenus);
                }

                // 设计变更下的四级子菜单
                var productionChangeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (productionChangeMenu != null)
                {
                    var productionChangeSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeFourthMenus(productionChangeMenu.Id);
                    fourthLevelMenus.AddRange(productionChangeSubMenus);

                    // 设变录入下的四级子菜单
                    var productionChangeInputMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == productionChangeMenu.Id);
                    if (productionChangeInputMenu != null)
                    {
                        var productionChangeInputSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(productionChangeInputMenu.Id);
                        fourthLevelMenus.AddRange(productionChangeInputSubMenus);
                    }
                }

                // SOP管理下的四级子菜单
                var productionSopMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "SOP管理" && m.ParentId == productionMenu.Id);
                if (productionSopMenu != null)
                {
                    var productionSopSubMenus = HbtDbSeedLogisticsMenu.GetProductionSopFourthMenus(productionSopMenu.Id);
                    fourthLevelMenus.AddRange(productionSopSubMenus);
                }

                // 技联管理下的四级子菜单
                var productionTechcontactMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "技联管理" && m.ParentId == productionMenu.Id);
                if (productionTechcontactMenu != null)
                {
                    var productionTechcontactSubMenus = HbtDbSeedLogisticsMenu.GetProductionTechcontactFourthMenus(productionTechcontactMenu.Id);
                    fourthLevelMenus.AddRange(productionTechcontactSubMenus);
                }
            }

            // 质量管理下的四级子菜单
            var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (qualityMenu != null)
            {
                // 基础数据下的四级子菜单
                var qualityBasicMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "基础数据" && m.ParentId == qualityMenu.Id);
                if (qualityBasicMenu != null)
                {
                    var qualityBasicSubMenus = HbtDbSeedLogisticsMenu.GetQualityBasicFourthMenus(qualityBasicMenu.Id);
                    fourthLevelMenus.AddRange(qualityBasicSubMenus);
                }

                // 质量检验下的四级子菜单
                var qualityInspectionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量检验" && m.ParentId == qualityMenu.Id);
                if (qualityInspectionMenu != null)
                {
                    var qualityInspectionSubMenus = HbtDbSeedLogisticsMenu.GetQualityInspectionFourthMenus(qualityInspectionMenu.Id);
                    fourthLevelMenus.AddRange(qualityInspectionSubMenus);
                }

                // 质量追溯下的四级子菜单
                var qualityTraceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量追溯" && m.ParentId == qualityMenu.Id);
                if (qualityTraceMenu != null)
                {
                    var qualityTraceSubMenus = HbtDbSeedLogisticsMenu.GetQualityTraceFourthMenus(qualityTraceMenu.Id);
                    fourthLevelMenus.AddRange(qualityTraceSubMenus);
                }

                // 质量成本下的四级子菜单
                var qualityCostMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量成本" && m.ParentId == qualityMenu.Id);
                if (qualityCostMenu != null)
                {
                    var qualityCostSubMenus = HbtDbSeedLogisticsMenu.GetQualityCostFourthMenus(qualityCostMenu.Id);
                    fourthLevelMenus.AddRange(qualityCostSubMenus);
                }

                // 检验计划下的四级子菜单
                var qualityPlanMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "检验计划" && m.ParentId == qualityMenu.Id);
                if (qualityPlanMenu != null)
                {
                    var qualityPlanSubMenus = HbtDbSeedLogisticsMenu.GetQualityPlanFourthMenus(qualityPlanMenu.Id);
                    fourthLevelMenus.AddRange(qualityPlanSubMenus);
                }
            }



            // 设备管理下的四级子菜单
            var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenuForFourth.Id);
            if (equipmentMenu != null)
            {
                // 设备数据下的四级子菜单
                var equipmentMasterMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备数据" && m.ParentId == equipmentMenu.Id);
                if (equipmentMasterMenu != null)
                {
                    var equipmentMasterSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentMasterFourthMenus(equipmentMasterMenu.Id);
                    fourthLevelMenus.AddRange(equipmentMasterSubMenus);
                }

                // 维保管理下的四级子菜单
                var equipmentMaintenanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "维保管理" && m.ParentId == equipmentMenu.Id);
                if (equipmentMaintenanceMenu != null)
                {
                    var equipmentMaintenanceSubMenus = HbtDbSeedLogisticsMenu.GetEquipmentMaintenanceFourthMenus(equipmentMaintenanceMenu.Id);
                    fourthLevelMenus.AddRange(equipmentMaintenanceSubMenus);
                }
            }
        }

        // 4. 工作流程下的四级子菜单
        var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 流程引擎下的四级子菜单
            var engineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "流程引擎" && m.ParentId == workflowMenu.Id);
            if (engineMenu != null)
            {
                var engineSubMenus = HbtDbSeedWorkflowMenu.GetWorkflowEngineThirdMenus(engineMenu.Id);
                fourthLevelMenus.AddRange(engineSubMenus);
            }

            // 流程管理下的四级子菜单
            var manageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "流程管理" && m.ParentId == workflowMenu.Id);
            if (manageMenu != null)
            {
                var manageSubMenus = HbtDbSeedWorkflowMenu.GetWorkflowManageThirdMenus(manageMenu.Id);
                fourthLevelMenus.AddRange(manageSubMenus);
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
        var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常办公" && m.ParentId == 0);
        if (routineMenu != null)
        {
            // 日常办公的四级目录处理
            // 这里可以添加日常办公的四级目录逻辑
        }

        // 2. 财务核算下的四级目录
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var formulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
                if (formulationMenu != null)
                {
                    var formulationSubMenus = HbtDbSeedFinanceMenu.GetFormulationFourthLevelMenus(formulationMenu.Id);
                    fourthLevelMenus.AddRange(formulationSubMenus);
                }
            }
        }
        else
        {
            _logger.Info("未找到财务核算目录");
        }

        // 3. 后勤管理下的四级目录
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            // 生产管理
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                // 设计变更下的四级目录
                var changeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (changeMenu != null)
                {
                    var changeSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeFourthLevelMenus(changeMenu.Id);
                    fourthLevelMenus.AddRange(changeSubMenus);
                }

                // 制造管理下的四级目录
                var ophMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (ophMenu != null)
                {
                    var ophSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputFourthLevelMenus(ophMenu.Id);
                    fourthLevelMenus.AddRange(ophSubMenus);
                }

                // SOP管理下的四级目录
                var sopMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "SOP管理" && m.ParentId == productionMenu.Id);
                if (sopMenu != null)
                {
                    var sopSubMenus = HbtDbSeedLogisticsMenu.GetProductionSopFourthMenus(sopMenu.Id);
                    fourthLevelMenus.AddRange(sopSubMenus);
                }

                // 技联管理下的四级目录
                var techcontactMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "技联管理" && m.ParentId == productionMenu.Id);
                if (techcontactMenu != null)
                {
                    var techcontactSubMenus = HbtDbSeedLogisticsMenu.GetProductionTechcontactFourthMenus(techcontactMenu.Id);
                    fourthLevelMenus.AddRange(techcontactSubMenus);
                }
            }

            // 客服管理
            var serviceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
            if (serviceMenu != null)
            {
                // 服务管理下的四级目录
                var serviceServiceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客户服务" && m.ParentId == serviceMenu.Id);
                if (serviceServiceMenu != null)
                {
                    var serviceServiceSubMenus = HbtDbSeedLogisticsMenu.GetServiceCustomerServiceFourthMenus(serviceServiceMenu.Id);
                    fourthLevelMenus.AddRange(serviceServiceSubMenus);
                }

                // 客诉管理下的四级目录
                var serviceComplaintMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客诉管理" && m.ParentId == serviceMenu.Id);
                if (serviceComplaintMenu != null)
                {
                    var serviceComplaintSubMenus = HbtDbSeedLogisticsMenu.GetServiceCustomerComplaintFourthMenus(serviceComplaintMenu.Id);
                    fourthLevelMenus.AddRange(serviceComplaintSubMenus);
                }
            }
        }

        // 4. 工作流程下的四级目录
        var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
        if (workflowMenu != null)
        {
            // 工作流程的四级目录处理
            // 这里可以添加工作流程的四级目录逻辑
        }

        // 5. 身份认证下的四级目录
        var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
        if (identityMenu != null)
        {
            // 身份认证的四级目录处理
            // 这里可以添加身份认证的四级目录逻辑
        }

        // 6. 代码生成下的四级目录
        var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
        if (generatorMenu != null)
        {
            // 代码生成的四级目录处理
            // 这里可以添加代码生成的四级目录逻辑
        }

        // 7. 审计日志下的四级目录
        var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审计日志" && m.ParentId == 0);
        if (auditMenu != null)
        {
            // 审计日志的四级目录处理
            // 这里可以添加审计日志的四级目录逻辑
        }

        // 8. 实时通信下的四级目录
        var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
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

        // 财务核算模块没有五级目录，此方法留空或移除相关逻辑

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

        // 财务核算
        var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
        if (financeMenu != null)
        {
            var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
            if (budgetMenu != null)
            {
                var formulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
                if (formulationMenu != null)
                {
                    // 销售预算下的五级菜单
                    var salesBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售预算" && m.ParentId == formulationMenu.Id);
                    if (salesBudgetMenu != null)
                    {
                        var salesBudgetSubMenus = HbtDbSeedFinanceMenu.GetSalesBudgetFifthMenus(salesBudgetMenu.Id);
                        fifthMenus.AddRange(salesBudgetSubMenus);
                    }

                    // 生产预算下的五级菜单
                    var productionBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产预算" && m.ParentId == formulationMenu.Id);
                    if (productionBudgetMenu != null)
                    {
                        var productionBudgetSubMenus = HbtDbSeedFinanceMenu.GetProductionBudgetFifthMenus(productionBudgetMenu.Id);
                        fifthMenus.AddRange(productionBudgetSubMenus);
                    }

                    // 成本预算下的五级菜单
                    var costBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "成本预算" && m.ParentId == formulationMenu.Id);
                    if (costBudgetMenu != null)
                    {
                        var costBudgetSubMenus = HbtDbSeedFinanceMenu.GetCostBudgetFifthMenus(costBudgetMenu.Id);
                        fifthMenus.AddRange(costBudgetSubMenus);
                    }

                    // 费用预算下的五级菜单
                    var expenseBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "费用预算" && m.ParentId == formulationMenu.Id);
                    if (expenseBudgetMenu != null)
                    {
                        var expenseBudgetSubMenus = HbtDbSeedFinanceMenu.GetExpenseBudgetFifthMenus(expenseBudgetMenu.Id);
                        fifthMenus.AddRange(expenseBudgetSubMenus);
                    }

                    // 财务预算下的五级菜单
                    var financialBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务预算" && m.ParentId == formulationMenu.Id);
                    if (financialBudgetMenu != null)
                    {
                        var financialBudgetSubMenus = HbtDbSeedFinanceMenu.GetFinancialBudgetFifthMenus(financialBudgetMenu.Id);
                        fifthMenus.AddRange(financialBudgetSubMenus);
                    }
                }
            }
        }

        // 后勤管理
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                // 设计变更 -> 设变录入
                var changeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
                if (changeMenu != null)
                {
                    var inputMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == changeMenu.Id);
                    if (inputMenu != null)
                    {
                        var inputSubMenus = HbtDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(inputMenu.Id);
                        fifthMenus.AddRange(inputSubMenus);
                    }
                }

                var ophMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (ophMenu != null)
                {
                    // 制一课下的五级菜单
                    var workshop1Menu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制一课" && m.ParentId == ophMenu.Id);
                    if (workshop1Menu != null)
                    {
                        var workshop1SubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop1FifthLevelMenus(workshop1Menu.Id);
                        fifthMenus.AddRange(workshop1SubMenus);
                    }

                    // 制二课下的五级菜单
                    var workshop2Menu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制二课" && m.ParentId == ophMenu.Id);
                    if (workshop2Menu != null)
                    {
                        var workshop2SubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop2FifthLevelMenus(workshop2Menu.Id);
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
        var sixthMenus = new List<HbtMenu>();

        // 后勤管理
        var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
        if (logisticsMenu != null)
        {
            var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
            if (productionMenu != null)
            {
                var ophMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制造管理" && m.ParentId == productionMenu.Id);
                if (ophMenu != null)
                {
                    // 制一课下的六级菜单
                    var workshop1Menu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制一课" && m.ParentId == ophMenu.Id);
                    if (workshop1Menu != null)
                    {
                        // 制一课OPH下的六级菜单
                        var workshop1OphMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "OPH" && m.ParentId == workshop1Menu.Id);
                        if (workshop1OphMenu != null)
                        {
                            var workshop1OphSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop1OutputSixthMenus(workshop1OphMenu.Id);
                            sixthMenus.AddRange(workshop1OphSubMenus);
                        }

                        // 制一课不良下的六级菜单
                        var workshop1DefectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "不良" && m.ParentId == workshop1Menu.Id);
                        if (workshop1DefectMenu != null)
                        {
                            var workshop1DefectSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop1DefectSixthMenus(workshop1DefectMenu.Id);
                            sixthMenus.AddRange(workshop1DefectSubMenus);
                        }

                        // 制一课工数下的六级菜单
                        var workshop1WorktimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工数" && m.ParentId == workshop1Menu.Id);
                        if (workshop1WorktimeMenu != null)
                        {
                            var workshop1WorktimeSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop1WorktimeSixthMenus(workshop1WorktimeMenu.Id);
                            sixthMenus.AddRange(workshop1WorktimeSubMenus);
                        }
                    }

                    // 制二课下的六级菜单
                    var workshop2Menu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制二课" && m.ParentId == ophMenu.Id);
                    if (workshop2Menu != null)
                    {
                        // 制二课OPH下的六级菜单
                        var workshop2OphMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "OPH" && m.ParentId == workshop2Menu.Id);
                        if (workshop2OphMenu != null)
                        {
                            var workshop2OphSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop2OutputSixthMenus(workshop2OphMenu.Id);
                            sixthMenus.AddRange(workshop2OphSubMenus);
                        }

                        // 制二课不良下的六级菜单
                        var workshop2DefectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "不良" && m.ParentId == workshop2Menu.Id);
                        if (workshop2DefectMenu != null)
                        {
                            var workshop2DefectSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop2DefectSixthMenus(workshop2DefectMenu.Id);
                            sixthMenus.AddRange(workshop2DefectSubMenus);
                        }

                        // 制二课工数下的六级菜单
                        var workshop2WorktimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工数" && m.ParentId == workshop2Menu.Id);
                        if (workshop2WorktimeMenu != null)
                        {
                            var workshop2WorktimeSubMenus = HbtDbSeedLogisticsMenu.GetProductionOutputWorkshop2WorktimeSixthMenus(workshop2WorktimeMenu.Id);
                            sixthMenus.AddRange(workshop2WorktimeSubMenus);
                        }
                    }
                }
            }
        }

        // 处理所有六级菜单
        foreach (var menu in sixthMenus)
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
    /// 初始化按钮
    /// </summary>
    private async Task<(int, int)> InitializeButtonsAsync(Dictionary<string, long> menuNameToId)
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有菜单类型的菜单
        var menus = await MenuRepository.GetListAsync(m => m.MenuType == 1);

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

        var existingMenu = await MenuRepository.GetFirstAsync(query);

        if (existingMenu == null)
        {
            await MenuRepository.CreateAsync(menu);
            _logger.Info($"[创建] {(isTopLevel ? "顶级" : "")}菜单 '{menu.MenuName}' (ParentId: {menu.ParentId}) 创建成功");

            // 创建后重新查询以获取正确的ID
            var createdMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == menu.MenuName && m.ParentId == menu.ParentId);
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

        await MenuRepository.UpdateAsync(existingMenu);
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
        var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成代码", "预览代码", "下载代码", "同步数据库", "导入", "导出", "导入模板", "字段", "表", "数据库", "初始化" };
        var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export", "template", "columns", "tables", "databases", "initialize" };

        // 工作流按钮
        var buttonFlowNames = new[] {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消",
            // 工作流定义操作
            "发布流程", "停用流程", "启用流程", "复制流程", "版本管理", "设计器", "预览流程", "导出流程", "导入流程",
            // 工作流实例操作
            "启动实例", "暂停实例", "恢复实例", "终止实例", "提交实例", "撤回实例", "转办实例", "委托实例", "退回实例", "催办实例", "加签", "减签",
            // 工作流任务操作
            "同意任务", "拒绝任务", "转办任务", "委托任务", "退回任务", "撤销任务", "催办任务", "加签任务", "减签任务", "会签", "串签", "并行签",
            // 工作流节点操作
            "执行节点", "跳过节点", "回退节点", "跳转节点", "分支节点", "合并节点", "条件节点", "循环节点", "定时节点", "消息节点", "邮件节点", "短信节点",
            // 工作流表单操作
            "设计表单", "预览表单", "发布表单", "停用表单", "复制表单", "版本表单", "字段表单", "验证表单", "权限表单", "数据源表单", "模板表单", "主题表单",
            // 工作流变量操作
            "设置变量", "获取变量", "更新变量", "删除变量", "清空变量", "导入变量", "导出变量", "验证变量", "类型变量", "默认值变量", "作用域变量", "生命周期变量",
            // 工作流历史操作
            "查看历史", "导出历史", "清理历史", "归档历史", "恢复历史", "删除历史", "搜索历史", "过滤历史", "统计历史", "分析历史", "报表历史", "图表历史"
        };
        var buttonFlowPerms = new[] {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke",
            // 工作流定义权限
            "publish", "disable", "enable", "copy", "version", "designer", "preview", "export", "import",
            // 工作流实例权限
            "start", "suspend", "resume", "terminate", "submit", "withdraw", "transfer", "delegate", "reject", "urge", "addsign", "subsign",
            // 工作流任务权限
            "approve", "reject", "transfer", "delegate", "return", "cancel", "urge", "addsign", "subsign", "countersign", "serialsign", "parallelsign",
            // 工作流节点权限
            "execute", "skip", "rollback", "jump", "branch", "merge", "condition", "loop", "timer", "message", "email", "sms",
            // 工作流表单权限
            "design", "preview", "publish", "disable", "copy", "version", "field", "validate", "permission", "datasource", "template", "theme",
            // 工作流变量权限
            "set", "get", "update", "delete", "clear", "import", "export", "validate", "type", "default", "scope", "lifecycle",
            // 工作流历史权限
            "view", "export", "clean", "archive", "restore", "delete", "search", "filter", "stats", "analyze", "report", "chart"
        };

        // 日常办公按钮
        var buttonRoutineNames = new[] {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消",
            // 文档操作
            "保存草稿", "删除草稿", "发送", "撤回", "转发", "回复", "标记已读", "标记未读",
            // 阅读操作
            "已读", "未读", "传阅", "签收", "催办", "签阅", "确认", "拒绝",
            // 申请审批操作
            "申请", "审批", "同意", "拒绝", "转办", "委托", "退回", "撤销", "撤回",
            // 库存管理操作
            "入库", "出库", "盘点", "调拨", "报损", "报溢", "请购", "采购", "领用", "归还", "借阅", "续借",
            // 会议管理操作
            "预约", "取消", "确认", "签到", "签退", "开始会议", "结束会议", "暂停会议", "恢复会议", "终止会议",
            // 车辆管理操作
            "用车", "维护", "维修", "加油", "违章",
            // 费用管理操作
            "报销", "统计", "预算", "分摊", "结转",
            // 人事管理操作
            "入职", "离职", "调岗", "加班", "请假", "出差",
            // 医务管理操作
            "档案", "费用",
            // 文件管理操作
            "上传", "下载", "预览", "分享", "归档", "销毁", "版本", "传阅",
            // 其他操作
            "运行", "停止", "重启", "刷新", "重置", "清空", "复制", "粘贴", "剪切", "全选", "反选"
        };
        var buttonRoutinePerms = new[] {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke",
            // 文档权限
            "draft", "deletedraft", "send", "withdraw", "forward", "reply", "markread", "markunread",
            // 阅读权限
            "read", "unread", "circulate", "sign", "urge", "ack", "confirm", "reject",
            // 申请审批权限
            "apply", "approve", "agree", "reject", "transfer", "delegate", "return", "cancel", "withdraw",
            // 库存管理权限
            "inbound", "outbound", "stocktaking", "transfer", "damage", "overflow", "requisition", "purchase", "receive", "return", "borrow", "renew",
            // 会议管理权限
            "booking", "cancel", "confirm", "checkin", "checkout", "start", "end", "pause", "resume", "terminate",
            // 车辆管理权限
            "car", "maintenance", "repair", "fuel", "violation",
            // 费用管理权限
            "reimburse", "stats", "budget", "allocate", "carry",
            // 人事管理权限
            "onboard", "offboard", "transfer", "overtime", "leave", "trip",
            // 医务管理权限
            "archive", "cost",
            // 文件管理权限
            "upload", "download", "preview", "share", "archive", "destroy", "version", "circulate",
            // 其他权限
            "run", "stop", "restart", "refresh", "reset", "clear", "copy", "paste", "cut", "selectall", "invertselect"
        };

        // 人力资源按钮
        var buttonHrmNames = new[] {
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "导入模板", "审计", "撤消",
            // 组织管理操作
            "部门管理", "职位管理", "组织架构", "岗位设置", "权限分配", "组织变更",
            // 员工管理操作
            "员工档案", "员工信息", "员工状态", "员工关系", "员工统计", "员工分析",
            // 招聘管理操作
            "职位发布", "简历筛选", "面试安排", "录用通知", "入职办理", "招聘统计",
            // 培训管理操作
            "培训计划", "培训课程", "培训实施", "培训评估", "培训记录", "培训统计",
            // 考勤管理操作
            "考勤记录", "考勤统计", "考勤规则", "考勤报表", "考勤分析", "考勤设置",
            // 请假管理操作
            "请假申请", "请假审批", "请假统计", "请假类型", "请假规则", "请假记录",
            // 薪酬管理操作
            "薪资设置", "薪资计算", "薪资发放", "薪资调整", "薪资统计", "薪资分析",
            // 绩效管理操作
            "绩效计划", "绩效评估", "绩效面谈", "绩效结果", "绩效统计", "绩效分析",
            // 合同管理操作
            "合同签订", "合同变更", "合同续签", "合同终止", "合同归档", "合同统计",
            // 员工关系操作
            "员工关怀", "员工沟通", "员工活动", "员工调查", "员工反馈", "员工满意度",
            // 人事异动操作
            "调岗申请", "调岗审批", "晋升申请", "晋升审批", "降级处理", "异动记录",
            // 离职管理操作
            "离职申请", "离职审批", "离职面谈", "离职手续", "离职统计", "离职分析",
            // 社保管理操作
            "社保缴纳", "社保查询", "社保统计", "社保调整", "社保报表", "社保分析",
            // 公积金管理操作
            "公积金缴纳", "公积金查询", "公积金统计", "公积金调整", "公积金报表", "公积金分析",
            // 报表统计操作
            "人员报表", "考勤报表", "薪资报表", "绩效报表", "培训报表", "综合统计",
            // 其他操作
            "数据导入", "数据导出", "数据备份", "数据恢复", "系统设置", "权限管理"
        };
        var buttonHrmPerms = new[] {
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "audit", "revoke",
            // 组织管理权限
            "department", "position", "organization", "job", "permission", "change",
            // 员工管理权限
            "profile", "info", "status", "relation", "stats", "analyze",
            // 招聘管理权限
            "post", "resume", "interview", "offer", "onboard", "recruit",
            // 培训管理权限
            "plan", "course", "implement", "evaluate", "record", "training",
            // 考勤管理权限
            "record", "stats", "rule", "report", "analyze", "setting",
            // 请假管理权限
            "apply", "approve", "stats", "type", "rule", "record",
            // 薪酬管理权限
            "setting", "calculate", "pay", "adjust", "stats", "analyze",
            // 绩效管理权限
            "plan", "evaluate", "interview", "result", "stats", "analyze",
            // 合同管理权限
            "sign", "change", "renew", "terminate", "archive", "stats",
            // 员工关系权限
            "care", "communication", "activity", "survey", "feedback", "satisfaction",
            // 人事异动权限
            "transfer", "approve", "promote", "promoteapprove", "demote", "record",
            // 离职管理权限
            "resign", "resignapprove", "interview", "procedure", "stats", "analyze",
            // 社保管理权限
            "pay", "query", "stats", "adjust", "report", "analyze",
            // 公积金管理权限
            "pay", "query", "stats", "adjust", "report", "analyze",
            // 报表统计权限
            "personnel", "attendance", "salary", "performance", "training", "comprehensive",
            // 其他权限
            "import", "export", "backup", "restore", "setting", "permission"
        };

        // 从菜单的权限标识中获取菜单标识
        // 使用Range语法截取第一个冒号和最后一个冒号之间的字符串
        var menuPerm = string.Empty;
        if (!string.IsNullOrEmpty(menu.Perms))
        {
            var parts = menu.Perms.Split(':');
            if (parts.Length > 2)
            {
                // 截取第一个冒号之后到最后一个冒号之前的部分
                menuPerm = string.Join(":", parts[1..^1]).ToLower();
            }
            else if (parts.Length == 2)
            {
                // 只有一个冒号的情况，取第一部分
                menuPerm = parts[0].ToLower();
            }
        }

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

            case "hrm":
                names = buttonHrmNames;
                perms = buttonHrmPerms;
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