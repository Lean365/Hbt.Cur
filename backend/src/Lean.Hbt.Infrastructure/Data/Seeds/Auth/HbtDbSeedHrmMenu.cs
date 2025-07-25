//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrmMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// HRM菜单数据初始化类
/// </summary>
public class HbtDbSeedHrmMenu
{
    /// <summary>
    /// 获取HRM二级目录菜单
    /// </summary>
    public static List<HbtMenu> GetHrmSecondMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "考勤管理", TransKey = "menu.human.attendance._self", ParentId = parentId, OrderNum = 1, Path = "attendance", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:attendance:list", Icon = "CalendarOutlined", Remark = "考勤管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "福利管理", TransKey = "menu.human.benefit._self", ParentId = parentId, OrderNum = 2, Path = "benefit", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:benefit:list", Icon = "GiftOutlined", Remark = "福利管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工管理", TransKey = "menu.human.employee._self", ParentId = parentId, OrderNum = 3, Path = "employee", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:employee:list", Icon = "UserOutlined", Remark = "员工管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "请假管理", TransKey = "menu.human.leave._self", ParentId = parentId, OrderNum = 4, Path = "leave", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:leave:list", Icon = "ClockCircleOutlined", Remark = "请假管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "组织管理", TransKey = "menu.human.organization._self", ParentId = parentId, OrderNum = 5, Path = "organization", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:organization:list", Icon = "ApartmentOutlined", Remark = "组织管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "绩效管理", TransKey = "menu.human.performance._self", ParentId = parentId, OrderNum = 6, Path = "performance", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:performance:list", Icon = "TrophyOutlined", Remark = "绩效管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "招聘管理", TransKey = "menu.human.recruitment._self", ParentId = parentId, OrderNum = 7, Path = "recruitment", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:recruitment:list", Icon = "UserAddOutlined", Remark = "招聘管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "薪酬管理", TransKey = "menu.human.salary._self", ParentId = parentId, OrderNum = 8, Path = "salary", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:salary:list", Icon = "DollarOutlined", Remark = "薪酬管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训管理", TransKey = "menu.human.training._self", ParentId = parentId, OrderNum = 9, Path = "training", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:training:list", Icon = "BookOutlined", Remark = "培训管理目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "报表统计", TransKey = "menu.human.report._self", ParentId = parentId, OrderNum = 10, Path = "report", Component = "", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 0, Visible = 0, Status = 0, Perms = "hrm:report:list", Icon = "BarChartOutlined", Remark = "HRM报表统计目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取考勤管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmAttendanceThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "排班", TransKey = "menu.human.attendance.schedule", ParentId = parentId, OrderNum = 1, Path = "schedule", Component = "hrm/attendance/schedule/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:schedule:list", Icon = "CalendarOutlined", Remark = "排班管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "考勤明细", TransKey = "menu.human.attendance.detail", ParentId = parentId, OrderNum = 2, Path = "detail", Component = "hrm/attendance/detail/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:detail:list", Icon = "FileTextOutlined", Remark = "考勤明细管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "考勤异常", TransKey = "menu.human.attendance.exception", ParentId = parentId, OrderNum = 3, Path = "exception", Component = "hrm/attendance/exception/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:exception:list", Icon = "ExclamationCircleOutlined", Remark = "考勤异常管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "假期", TransKey = "menu.human.attendance.holiday", ParentId = parentId, OrderNum = 4, Path = "holiday", Component = "hrm/attendance/holiday/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:holiday:list", Icon = "ClockCircleOutlined", Remark = "假期管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "加班", TransKey = "menu.human.attendance.overtime", ParentId = parentId, OrderNum = 5, Path = "overtime", Component = "hrm/attendance/overtime/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:overtime:list", Icon = "FieldTimeOutlined", Remark = "加班管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "调休", TransKey = "menu.human.attendance.compensatory", ParentId = parentId, OrderNum = 6, Path = "compensatory", Component = "hrm/attendance/compensatory/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:attendance:compensatory:list", Icon = "SwapOutlined", Remark = "调休管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取福利管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmBenefitThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "福利项目", TransKey = "menu.human.benefit.project", ParentId = parentId, OrderNum = 1, Path = "project", Component = "hrm/benefit/project/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:benefit:project:list", Icon = "GiftOutlined", Remark = "福利项目管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工福利", TransKey = "menu.human.benefit.employee", ParentId = parentId, OrderNum = 2, Path = "employee", Component = "hrm/benefit/employee/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:benefit:employee:list", Icon = "UserOutlined", Remark = "员工福利管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取员工管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmEmployeeThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "人员信息", TransKey = "menu.human.employee.info", ParentId = parentId, OrderNum = 1, Path = "info", Component = "hrm/employee/info/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:info:list", Icon = "UserOutlined", Remark = "人员信息管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "合同类型", TransKey = "menu.human.employee.contracttype", ParentId = parentId, OrderNum = 2, Path = "contracttype", Component = "hrm/employee/contracttype/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:contracttype:list", Icon = "FileTextOutlined", Remark = "合同类型管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工合同", TransKey = "menu.human.employee.contract", ParentId = parentId, OrderNum = 3, Path = "contract", Component = "hrm/employee/contract/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:contract:list", Icon = "FileTextOutlined", Remark = "员工合同管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工晋升", TransKey = "menu.human.employee.promotion", ParentId = parentId, OrderNum = 4, Path = "promotion", Component = "hrm/employee/promotion/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:promotion:list", Icon = "RiseOutlined", Remark = "员工晋升管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "晋升历史", TransKey = "menu.human.employee.promotionhistory", ParentId = parentId, OrderNum = 5, Path = "promotionhistory", Component = "hrm/employee/promotionhistory/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:promotionhistory:list", Icon = "HistoryOutlined", Remark = "晋升历史管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工离职", TransKey = "menu.human.employee.resignation", ParentId = parentId, OrderNum = 6, Path = "resignation", Component = "hrm/employee/resignation/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:resignation:list", Icon = "LogoutOutlined", Remark = "员工离职管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工调岗", TransKey = "menu.human.employee.transfer", ParentId = parentId, OrderNum = 7, Path = "transfer", Component = "hrm/employee/transfer/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:transfer:list", Icon = "SwapOutlined", Remark = "员工调岗管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "调岗历史", TransKey = "menu.human.employee.transferhistory", ParentId = parentId, OrderNum = 8, Path = "transferhistory", Component = "hrm/employee/transferhistory/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:employee:transferhistory:list", Icon = "HistoryOutlined", Remark = "调岗历史管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取请假管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmLeaveThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "请假类型", TransKey = "menu.human.leave.type", ParentId = parentId, OrderNum = 1, Path = "type", Component = "hrm/leave/type/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:leave:type:list", Icon = "ClockCircleOutlined", Remark = "请假类型管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "员工请假", TransKey = "menu.human.leave.employee", ParentId = parentId, OrderNum = 2, Path = "employee", Component = "hrm/leave/employee/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:leave:employee:list", Icon = "UserOutlined", Remark = "员工请假管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取组织管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmOrganizationThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "职位类别", TransKey = "menu.human.organization.positioncategory", ParentId = parentId, OrderNum = 1, Path = "positioncategory", Component = "hrm/organization/positioncategory/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:organization:positioncategory:list", Icon = "TagsOutlined", Remark = "职位类别管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "公司", TransKey = "menu.human.organization.company", ParentId = parentId, OrderNum = 2, Path = "company", Component = "hrm/organization/company/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:organization:company:list", Icon = "BankOutlined", Remark = "公司管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "部门", TransKey = "menu.human.organization.department", ParentId = parentId, OrderNum = 3, Path = "department", Component = "hrm/organization/department/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:organization:department:list", Icon = "ApartmentOutlined", Remark = "部门管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "职位", TransKey = "menu.human.organization.position", ParentId = parentId, OrderNum = 4, Path = "position", Component = "hrm/organization/position/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:organization:position:list", Icon = "UserSwitchOutlined", Remark = "职位管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取绩效管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmPerformanceThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "考核模板", TransKey = "menu.human.performance.template", ParentId = parentId, OrderNum = 1, Path = "template", Component = "hrm/performance/template/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:performance:template:list", Icon = "FileTextOutlined", Remark = "考核模板管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "考核计划", TransKey = "menu.human.performance.plan", ParentId = parentId, OrderNum = 2, Path = "plan", Component = "hrm/performance/plan/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:performance:plan:list", Icon = "ScheduleOutlined", Remark = "考核计划管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "考核项目", TransKey = "menu.human.performance.assessmentitem", ParentId = parentId, OrderNum = 3, Path = "assessmentitem", Component = "hrm/performance/assessmentitem/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:performance:assessmentitem:list", Icon = "CheckSquareOutlined", Remark = "考核项目管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "绩效考核", TransKey = "menu.human.performance.assessment", ParentId = parentId, OrderNum = 4, Path = "assessment", Component = "hrm/performance/assessment/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:performance:assessment:list", Icon = "TrophyOutlined", Remark = "绩效考核管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取招聘管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmRecruitmentThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "职位申请", TransKey = "menu.human.recruitment.application", ParentId = parentId, OrderNum = 1, Path = "application", Component = "hrm/recruitment/application/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:recruitment:application:list", Icon = "FileTextOutlined", Remark = "职位申请管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "职位发布", TransKey = "menu.human.recruitment.posting", ParentId = parentId, OrderNum = 2, Path = "posting", Component = "hrm/recruitment/posting/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:recruitment:posting:list", Icon = "NotificationOutlined", Remark = "职位发布管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "候选人管理", TransKey = "menu.human.recruitment.candidate", ParentId = parentId, OrderNum = 3, Path = "candidate", Component = "hrm/recruitment/candidate/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:recruitment:candidate:list", Icon = "UserOutlined", Remark = "候选人管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "面试管理", TransKey = "menu.human.recruitment.interview", ParentId = parentId, OrderNum = 4, Path = "interview", Component = "hrm/recruitment/interview/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:recruitment:interview:list", Icon = "UserSwitchOutlined", Remark = "面试管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取薪酬管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmSalaryThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "员工薪资", TransKey = "menu.human.salary.employee", ParentId = parentId, OrderNum = 1, Path = "employee", Component = "hrm/salary/employee/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:employee:list", Icon = "UserOutlined", Remark = "员工薪资管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "公积金", TransKey = "menu.human.salary.housing", ParentId = parentId, OrderNum = 2, Path = "housing", Component = "hrm/salary/housing/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:housing:list", Icon = "HomeOutlined", Remark = "公积金管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "公积金等级", TransKey = "menu.human.salary.housinglevel", ParentId = parentId, OrderNum = 3, Path = "housinglevel", Component = "hrm/salary/housinglevel/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:housinglevel:list", Icon = "BarChartOutlined", Remark = "公积金等级管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "个税", TransKey = "menu.human.salary.tax", ParentId = parentId, OrderNum = 4, Path = "tax", Component = "hrm/salary/tax/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:tax:list", Icon = "CalculatorOutlined", Remark = "个税管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "个税等级", TransKey = "menu.human.salary.taxlevel", ParentId = parentId, OrderNum = 5, Path = "taxlevel", Component = "hrm/salary/taxlevel/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:taxlevel:list", Icon = "BarChartOutlined", Remark = "个税等级管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "薪资结构", TransKey = "menu.human.salary.structure", ParentId = parentId, OrderNum = 6, Path = "structure", Component = "hrm/salary/structure/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:structure:list", Icon = "SettingOutlined", Remark = "薪资结构管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "社保", TransKey = "menu.human.salary.social", ParentId = parentId, OrderNum = 7, Path = "social", Component = "hrm/salary/social/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:social:list", Icon = "SafetyCertificateOutlined", Remark = "社保管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "社保基数", TransKey = "menu.human.salary.socialbase", ParentId = parentId, OrderNum = 8, Path = "socialbase", Component = "hrm/salary/socialbase/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:salary:socialbase:list", Icon = "BarChartOutlined", Remark = "社保基数管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取培训管理三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmTrainingThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "培训计划", TransKey = "menu.human.training.plan", ParentId = parentId, OrderNum = 1, Path = "plan", Component = "hrm/training/plan/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:training:plan:list", Icon = "ScheduleOutlined", Remark = "培训计划管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训类别", TransKey = "menu.human.training.category", ParentId = parentId, OrderNum = 2, Path = "category", Component = "hrm/training/category/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:training:category:list", Icon = "TagsOutlined", Remark = "培训类别管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训课程", TransKey = "menu.human.training.course", ParentId = parentId, OrderNum = 3, Path = "course", Component = "hrm/training/course/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:training:course:list", Icon = "BookOutlined", Remark = "培训课程管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训记录", TransKey = "menu.human.training.record", ParentId = parentId, OrderNum = 4, Path = "record", Component = "hrm/training/record/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:training:record:list", Icon = "FileTextOutlined", Remark = "培训记录管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训评估", TransKey = "menu.human.training.evaluation", ParentId = parentId, OrderNum = 5, Path = "evaluation", Component = "hrm/training/evaluation/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:training:evaluation:list", Icon = "CheckSquareOutlined", Remark = "培训评估管理", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }

    /// <summary>
    /// 获取报表统计三级菜单
    /// </summary>
    public static List<HbtMenu> GetHrmReportThirdMenus(long parentId)
    {
        return new List<HbtMenu>
        {
            new HbtMenu { MenuName = "人员信息", TransKey = "menu.human.report.employeeinfo", ParentId = parentId, OrderNum = 1, Path = "employeeinfo", Component = "hrm/report/employeeinfo/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:report:employeeinfo:list", Icon = "UserOutlined", Remark = "人员信息报表", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "离职清单", TransKey = "menu.human.report.resignation", ParentId = parentId, OrderNum = 2, Path = "resignation", Component = "hrm/report/resignation/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:report:resignation:list", Icon = "LogoutOutlined", Remark = "离职清单报表", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "调岗清单", TransKey = "menu.human.report.transfer", ParentId = parentId, OrderNum = 3, Path = "transfer", Component = "hrm/report/transfer/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:report:transfer:list", Icon = "SwapOutlined", Remark = "调岗清单报表", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "晋升清单", TransKey = "menu.human.report.promotion", ParentId = parentId, OrderNum = 4, Path = "promotion", Component = "hrm/report/promotion/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:report:promotion:list", Icon = "RiseOutlined", Remark = "晋升清单报表", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtMenu { MenuName = "培训清单", TransKey = "menu.human.report.training", ParentId = parentId, OrderNum = 5, Path = "training", Component = "hrm/report/training/index", QueryParams = null, IsExternal = 0, IsCache = 0, MenuType = 1, Visible = 0, Status = 0, Perms = "hrm:report:training:list", Icon = "BookOutlined", Remark = "培训清单报表", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }
}
