//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务管理菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 财务管理菜单数据初始化类
/// </summary>
public class HbtDbSeedFinanceMenu
{
    /// <summary>
    /// 获取财务管理二级目录列表
    /// </summary>
    public static List<HbtMenu> GetFinanceSecondLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "管理会计", TransKey = "menu.finance.accounting._self", ParentId = parentId, OrderNum = 1, Path = "accounting", Component = "", MenuType = 0, Perms = "", Icon = "FundOutlined", Remark = "管理会计子目录" },
        new HbtMenu { MenuName = "控制会计", TransKey = "menu.finance.controlling._self", ParentId = parentId, OrderNum = 2, Path = "controlling", Component = "", MenuType = 0, Perms = "", Icon = "ControlOutlined", Remark = "控制会计子目录" },
        new HbtMenu { MenuName = "全面预算", TransKey = "menu.finance.budget._self", ParentId = parentId, OrderNum = 3, Path = "budget", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "全面预算子目录" }
    };

    /// <summary>
    /// 获取管理会计三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetFinanceManagementThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "公司信息", TransKey = "menu.finance.accounting.company", ParentId = parentId, OrderNum = 1, Path = "company", Component = "finance/accounting/company/index", MenuType = 1, Perms = "finance:accounting:company:list", Icon = "BankOutlined", Remark = "公司信息管理" },
        new HbtMenu { MenuName = "银行信息", TransKey = "menu.finance.accounting.bank", ParentId = parentId, OrderNum = 2, Path = "bank", Component = "finance/accounting/bank/index", MenuType = 1, Perms = "finance:accounting:bank:list", Icon = "BankOutlined", Remark = "银行信息管理" },
        new HbtMenu { MenuName = "会计科目", TransKey = "menu.finance.accounting.account", ParentId = parentId, OrderNum = 3, Path = "account", Component = "finance/accounting/account/index", MenuType = 1, Perms = "finance:accounting:account:list", Icon = "BookOutlined", Remark = "会计科目管理" },
        new HbtMenu { MenuName = "公司科目", TransKey = "menu.finance.accounting.companyaccount", ParentId = parentId, OrderNum = 4, Path = "companyaccount", Component = "finance/accounting/companyaccount/index", MenuType = 1, Perms = "finance:accounting:companyaccount:list", Icon = "BankOutlined", Remark = "公司科目管理" },
        new HbtMenu { MenuName = "固定资产", TransKey = "menu.finance.accounting.fixedasset", ParentId = parentId, OrderNum = 5, Path = "fixedasset", Component = "finance/accounting/fixedasset/index", MenuType = 1, Perms = "finance:accounting:fixedasset:list", Icon = "SafetyCertificateOutlined", Remark = "固定资产管理" },
        new HbtMenu { MenuName = "总账", TransKey = "menu.finance.accounting.ledger", ParentId = parentId, OrderNum = 6, Path = "ledger", Component = "finance/accounting/ledger/index", MenuType = 1, Perms = "finance:accounting:ledger:list", Icon = "BookOutlined", Remark = "总账管理" },
        new HbtMenu { MenuName = "应付", TransKey = "menu.finance.accounting.payable", ParentId = parentId, OrderNum = 7, Path = "payable", Component = "finance/accounting/payable/index", MenuType = 1, Perms = "finance:accounting:payable:list", Icon = "TagOutlined", Remark = "应付管理" }
    };

    /// <summary>
    /// 获取控制会计三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetFinanceControlThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "利润中心", TransKey = "menu.finance.controlling.profitcenter", ParentId = parentId, OrderNum = 1, Path = "profitcenter", Component = "finance/controlling/profitcenter/index", MenuType = 1, Perms = "finance:controlling:profitcenter:list", Icon = "RiseOutlined", Remark = "利润中心管理" },
        new HbtMenu { MenuName = "成本中心", TransKey = "menu.finance.controlling.costcenter", ParentId = parentId, OrderNum = 2, Path = "costcenter", Component = "finance/controlling/costcenter/index", MenuType = 1, Perms = "finance:controlling:costcenter:list", Icon = "ClusterOutlined", Remark = "成本中心管理" },
        new HbtMenu { MenuName = "成本要素", TransKey = "menu.finance.controlling.costelement", ParentId = parentId, OrderNum = 3, Path = "costelement", Component = "finance/controlling/costelement/index", MenuType = 1, Perms = "finance:controlling:costelement:list", Icon = "FundOutlined", Remark = "成本要素管理" }
    };

    /// <summary>
    /// 获取全面预算三级目录列表
    /// </summary>
    public static List<HbtMenu> GetBudgetThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "预算编制", TransKey = "menu.finance.budget.formulation._self", ParentId = parentId, OrderNum = 1, Path = "formulation", Component = "", MenuType = 0, Perms = "", Icon = "FormOutlined", Remark = "预算编制目录" },
        new HbtMenu { MenuName = "预算控制", TransKey = "menu.finance.budget.control._self", ParentId = parentId, OrderNum = 2, Path = "control", Component = "", MenuType = 0, Perms = "", Icon = "ControlOutlined", Remark = "预算控制目录" }
    };

    /// <summary>
    /// 获取预算编制四级目录列表
    /// </summary>
    public static List<HbtMenu> GetFormulationFourthLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "销售预算", TransKey = "menu.finance.budget.formulation.sales._self", ParentId = parentId, OrderNum = 1, Path = "sales", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingCartOutlined", Remark = "销售预算目录" },
        new HbtMenu { MenuName = "生产预算", TransKey = "menu.finance.budget.formulation.production._self", ParentId = parentId, OrderNum = 2, Path = "production", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "生产预算目录" },
        new HbtMenu { MenuName = "成本预算", TransKey = "menu.finance.budget.formulation.cost._self", ParentId = parentId, OrderNum = 3, Path = "cost", Component = "", MenuType = 0, Perms = "", Icon = "AccountBookOutlined", Remark = "成本预算目录" },
        new HbtMenu { MenuName = "费用预算", TransKey = "menu.finance.budget.formulation.expense._self", ParentId = parentId, OrderNum = 4, Path = "expense", Component = "", MenuType = 0, Perms = "", Icon = "MoneyCollectOutlined", Remark = "费用预算目录" },
        new HbtMenu { MenuName = "财务预算", TransKey = "menu.finance.budget.formulation.financial._self", ParentId = parentId, OrderNum = 5, Path = "financial", Component = "", MenuType = 0, Perms = "", Icon = "AccountBookOutlined", Remark = "财务预算目录" }
    };

    /// <summary>
    /// 获取预算控制四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetBudgetControlFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "预算看板", TransKey = "menu.finance.budget.control.dashboard", ParentId = parentId, OrderNum = 1, Path = "dashboard", Component = "finance/budget/control/dashboard/index", MenuType = 1, Perms = "finance:budget:control:dashboard:list", Icon = "DashboardOutlined", Remark = "预算看板" },
        new HbtMenu { MenuName = "预算审批", TransKey = "menu.finance.budget.control.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "finance/budget/control/approval/index", MenuType = 1, Perms = "finance:budget:control:approval:list", Icon = "CheckSquareOutlined", Remark = "预算审批" }
    };

    /// <summary>
    /// 获取销售预算五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetSalesBudgetFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "销售成本", TransKey = "menu.finance.budget.formulation.sales.cost", ParentId = parentId, OrderNum = 1, Path = "cost", Component = "finance/budget/formulation/sales/cost/index", MenuType = 1, Perms = "finance:budget:formulation:sales:cost:list", Icon = "MoneyCollectOutlined", Remark = "销售成本预算" },
        new HbtMenu { MenuName = "销售滚动", TransKey = "menu.finance.budget.formulation.sales.rolling", ParentId = parentId, OrderNum = 2, Path = "rolling", Component = "finance/budget/formulation/sales/rolling/index", MenuType = 1, Perms = "finance:budget:formulation:sales:rolling:list", Icon = "InteractionOutlined", Remark = "销售滚动预算" }
    };

    /// <summary>
    /// 获取生产预算五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionBudgetFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "生产辅料", TransKey = "menu.finance.budget.formulation.production.auxiliary", ParentId = parentId, OrderNum = 1, Path = "auxiliary", Component = "finance/budget/formulation/production/auxiliary/index", MenuType = 1, Perms = "finance:budget:formulation:production:auxiliary:list", Icon = "ToolOutlined", Remark = "生产辅料预算" },
        new HbtMenu { MenuName = "生产人工", TransKey = "menu.finance.budget.formulation.production.labor", ParentId = parentId, OrderNum = 2, Path = "labor", Component = "finance/budget/formulation/production/labor/index", MenuType = 1, Perms = "finance:budget:formulation:production:labor:list", Icon = "TeamOutlined", Remark = "生产人工预算" },
        new HbtMenu { MenuName = "生产制造", TransKey = "menu.finance.budget.formulation.production.manufacturing", ParentId = parentId, OrderNum = 3, Path = "manufacturing", Component = "finance/budget/formulation/production/manufacturing/index", MenuType = 1, Perms = "finance:budget:formulation:production:manufacturing:list", Icon = "BuildOutlined", Remark = "生产制造预算" }
    };

    /// <summary>
    /// 获取成本预算五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetCostBudgetFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "直接材料", TransKey = "menu.finance.budget.formulation.cost.directmaterial", ParentId = parentId, OrderNum = 1, Path = "directmaterial", Component = "finance/budget/formulation/cost/directmaterial/index", MenuType = 1, Perms = "finance:budget:formulation:cost:directmaterial:list", Icon = "InboxOutlined", Remark = "直接材料成本预算" },
        new HbtMenu { MenuName = "直接人工", TransKey = "menu.finance.budget.formulation.cost.directlabor", ParentId = parentId, OrderNum = 2, Path = "directlabor", Component = "finance/budget/formulation/cost/directlabor/index", MenuType = 1, Perms = "finance:budget:formulation:cost:directlabor:list", Icon = "TeamOutlined", Remark = "直接人工成本预算" },
        new HbtMenu { MenuName = "制造费用", TransKey = "menu.finance.budget.formulation.cost.manufacturing", ParentId = parentId, OrderNum = 3, Path = "manufacturing", Component = "finance/budget/formulation/cost/manufacturing/index", MenuType = 1, Perms = "finance:budget:formulation:cost:manufacturing:list", Icon = "BuildOutlined", Remark = "制造费用预算" }
    };

    /// <summary>
    /// 获取费用预算五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetExpenseBudgetFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "销售费用", TransKey = "menu.finance.budget.formulation.expense.sales", ParentId = parentId, OrderNum = 1, Path = "sales", Component = "finance/budget/formulation/expense/sales/index", MenuType = 1, Perms = "finance:budget:formulation:expense:sales:list", Icon = "ShoppingCartOutlined", Remark = "销售费用预算" },
        new HbtMenu { MenuName = "管理费用", TransKey = "menu.finance.budget.formulation.expense.management", ParentId = parentId, OrderNum = 2, Path = "management", Component = "finance/budget/formulation/expense/management/index", MenuType = 1, Perms = "finance:budget:formulation:expense:management:list", Icon = "SettingOutlined", Remark = "管理费用预算" },
        new HbtMenu { MenuName = "财务费用", TransKey = "menu.finance.budget.formulation.expense.financial", ParentId = parentId, OrderNum = 3, Path = "financial", Component = "finance/budget/formulation/expense/financial/index", MenuType = 1, Perms = "finance:budget:formulation:expense:financial:list", Icon = "AccountBookOutlined", Remark = "财务费用预算" }
    };

    /// <summary>
    /// 获取财务预算五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetFinancialBudgetFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "现金流量", TransKey = "menu.finance.budget.formulation.financial.cashflow", ParentId = parentId, OrderNum = 1, Path = "cashflow", Component = "finance/budget/formulation/financial/cashflow/index", MenuType = 1, Perms = "finance:budget:formulation:financial:cashflow:list", Icon = "MoneyCollectOutlined", Remark = "现金流量预算" },
        new HbtMenu { MenuName = "资产负债表", TransKey = "menu.finance.budget.formulation.financial.balancesheet", ParentId = parentId, OrderNum = 2, Path = "balancesheet", Component = "finance/budget/formulation/financial/balancesheet/index", MenuType = 1, Perms = "finance:budget:formulation:financial:balancesheet:list", Icon = "AccountBookOutlined", Remark = "资产负债表预算" },
        new HbtMenu { MenuName = "利润表", TransKey = "menu.finance.budget.formulation.financial.income", ParentId = parentId, OrderNum = 3, Path = "income", Component = "finance/budget/formulation/financial/income/index", MenuType = 1, Perms = "finance:budget:formulation:financial:income:list", Icon = "RiseOutlined", Remark = "利润表预算" }
    };
} 