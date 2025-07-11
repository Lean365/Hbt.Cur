//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRoutineMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 日常办公菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRoutineMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 日常办公菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 日常办公菜单数据初始化类
/// </summary>
public class HbtDbSeedRoutineMenu
{
    /// <summary>
    /// 获取日常办公子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineSecondLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "基础服务", TransKey = "menu.routine.core._self", ParentId = parentId, OrderNum = 1, Path = "core", Component = "", MenuType = 0, Perms = "", Icon = "SettingOutlined", Remark = "基础服务目录" },
        new HbtMenu { MenuName = "日程管理", TransKey = "menu.routine.schedule._self", ParentId = parentId, OrderNum = 2, Path = "schedule", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "日程管理目录" },
        new HbtMenu { MenuName = "用车管理", TransKey = "menu.routine.car._self", ParentId = parentId, OrderNum = 3, Path = "car", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "用车管理目录" },
        new HbtMenu { MenuName = "邮件管理", TransKey = "menu.routine.email._self", ParentId = parentId, OrderNum = 4, Path = "email", Component = "", MenuType = 0, Perms = "", Icon = "MailOutlined", Remark = "邮件管理目录" },
        new HbtMenu { MenuName = "会议管理", TransKey = "menu.routine.meeting._self", ParentId = parentId, OrderNum = 5, Path = "meeting", Component = "", MenuType = 0, Perms = "", Icon = "VideoCameraOutlined", Remark = "会议管理目录" },
        new HbtMenu { MenuName = "公告通知", TransKey = "menu.routine.notice._self", ParentId = parentId, OrderNum = 6, Path = "notice", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "公告通知目录" },
        new HbtMenu { MenuName = "人事考勤", TransKey = "menu.routine.hr._self", ParentId = parentId, OrderNum = 7, Path = "hr", Component = "", MenuType = 0, Perms = "", Icon = "UsergroupAddOutlined", Remark = "人事考勤目录" },
        new HbtMenu { MenuName = "费用管理", TransKey = "menu.routine.expense._self", ParentId = parentId, OrderNum = 8, Path = "expense", Component = "", MenuType = 0, Perms = "", Icon = "MoneyCollectOutlined", Remark = "费用管理目录" },
        new HbtMenu { MenuName = "文件管理", TransKey = "menu.routine.file._self", ParentId = parentId, OrderNum = 9, Path = "file", Component = "", MenuType = 0, Perms = "", Icon = "FolderOutlined", Remark = "文件管理目录" },
        new HbtMenu { MenuName = "合同管理", TransKey = "menu.routine.contract._self", ParentId = parentId, OrderNum = 10, Path = "contract", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "合同管理目录" },
        new HbtMenu { MenuName = "项目管理", TransKey = "menu.routine.project._self", ParentId = parentId, OrderNum = 11, Path = "project", Component = "", MenuType = 0, Perms = "", Icon = "ProjectOutlined", Remark = "项目管理目录" },
        new HbtMenu { MenuName = "任务管理", TransKey = "menu.routine.quartz._self", ParentId = parentId, OrderNum = 12, Path = "quartz", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "任务管理目录" },
        new HbtMenu { MenuName = "办公用品", TransKey = "menu.routine.officesupplies._self", ParentId = parentId, OrderNum = 13, Path = "officesupplies", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品管理目录" },
        new HbtMenu { MenuName = "图书管理", TransKey = "menu.routine.book._self", ParentId = parentId, OrderNum = 14, Path = "book", Component = "", MenuType = 0, Perms = "", Icon = "BookOutlined", Remark = "图书管理目录" },
        new HbtMenu { MenuName = "医务管理", TransKey = "menu.routine.medical._self", ParentId = parentId, OrderNum = 15, Path = "medical", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "医务管理目录" }
    };

    /// <summary>
    /// 获取日程管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineScheduleThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "我的日程", TransKey = "menu.routine.schedule.myschedule", ParentId = parentId, OrderNum = 1, Path = "myschedule", Component = "routine/schedule/myschedule/index", MenuType = 1, Perms = "routine:schedule:myschedule:list", Icon = "UserOutlined", Remark = "我的日程" },
        new HbtMenu { MenuName = "日程看板", TransKey = "menu.routine.schedule.dashboard", ParentId = parentId, OrderNum = 2, Path = "dashboard", Component = "routine/schedule/dashboard/index", MenuType = 1, Perms = "routine:schedule:dashboard:list", Icon = "DashboardOutlined", Remark = "日程看板" }
    };

    /// <summary>
    /// 获取用车管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineCarThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "我的用车", TransKey = "menu.routine.car.my", ParentId = parentId, OrderNum = 1, Path = "my", Component = "routine/car/my/index", MenuType = 1, Perms = "routine:car:my:list", Icon = "UserOutlined", Remark = "我的用车" },
        new HbtMenu { MenuName = "用车申请", TransKey = "menu.routine.car.application", ParentId = parentId, OrderNum = 2, Path = "application", Component = "routine/car/application/index", MenuType = 1, Perms = "routine:car:application:list", Icon = "FileAddOutlined", Remark = "用车申请" },
        new HbtMenu { MenuName = "用车看板", TransKey = "menu.routine.car.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/car/dashboard/index", MenuType = 1, Perms = "routine:car:dashboard:list", Icon = "DashboardOutlined", Remark = "用车看板" },
        new HbtMenu { MenuName = "车管管理", TransKey = "menu.routine.car.management._self", ParentId = parentId, OrderNum = 4, Path = "management", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "车管管理目录" }
    };

    /// <summary>
    /// 获取邮件管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineEmailThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "收件箱", TransKey = "menu.routine.email.inbox", ParentId = parentId, OrderNum = 1, Path = "inbox", Component = "routine/email/inbox/index", MenuType = 1, Perms = "routine:email:inbox:list", Icon = "InboxOutlined", Remark = "收件箱" },
        new HbtMenu { MenuName = "草稿箱", TransKey = "menu.routine.email.drafts", ParentId = parentId, OrderNum = 2, Path = "drafts", Component = "routine/email/drafts/index", MenuType = 1, Perms = "routine:email:drafts:list", Icon = "EditOutlined", Remark = "草稿箱" },
        new HbtMenu { MenuName = "已发送", TransKey = "menu.routine.email.sent", ParentId = parentId, OrderNum = 3, Path = "sent", Component = "routine/email/sent/index", MenuType = 1, Perms = "routine:email:sent:list", Icon = "SendOutlined", Remark = "已发送邮件" },
        new HbtMenu { MenuName = "垃圾箱", TransKey = "menu.routine.email.trash", ParentId = parentId, OrderNum = 4, Path = "trash", Component = "routine/email/trash/index", MenuType = 1, Perms = "routine:email:trash:list", Icon = "DeleteOutlined", Remark = "垃圾箱" },
        new HbtMenu { MenuName = "模板", TransKey = "menu.routine.email.template", ParentId = parentId, OrderNum = 5, Path = "template", Component = "routine/email/template/index", MenuType = 1, Perms = "routine:email:template:list", Icon = "FileTextOutlined", Remark = "邮件模板" }
    };

    /// <summary>
    /// 获取会议管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineMeetingThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "我的会议", TransKey = "menu.routine.meeting.mymeeting", ParentId = parentId, OrderNum = 1, Path = "mymeeting", Component = "routine/meeting/mymeeting/index", MenuType = 1, Perms = "routine:meeting:mymeeting:list", Icon = "UserOutlined", Remark = "我的会议" },
        new HbtMenu { MenuName = "预约申请", TransKey = "menu.routine.meeting.booking", ParentId = parentId, OrderNum = 2, Path = "booking", Component = "routine/meeting/booking/index", MenuType = 1, Perms = "routine:meeting:booking:list", Icon = "PlusSquareOutlined", Remark = "会议预约申请" },
        new HbtMenu { MenuName = "会议看板", TransKey = "menu.routine.meeting.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/meeting/dashboard/index", MenuType = 1, Perms = "routine:meeting:dashboard:list", Icon = "DashboardOutlined", Remark = "会议看板" },
        new HbtMenu { MenuName = "会议室管理", TransKey = "menu.routine.meeting.room", ParentId = parentId, OrderNum = 4, Path = "room", Component = "routine/meeting/room/index", MenuType = 1, Perms = "routine:meeting:room:list", Icon = "HomeOutlined", Remark = "会议室管理" }
    };

    /// <summary>
    /// 获取公告通知三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineNoticeThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "消息", TransKey = "menu.routine.notice.message._self", ParentId = parentId, OrderNum = 1, Path = "message", Component = "", MenuType = 0, Perms = "", Icon = "MessageOutlined", Remark = "消息目录" },
        new HbtMenu { MenuName = "公告", TransKey = "menu.routine.notice.announcement._self", ParentId = parentId, OrderNum = 2, Path = "announcement", Component = "", MenuType = 0, Perms = "", Icon = "SoundOutlined", Remark = "公告目录" },
        new HbtMenu { MenuName = "通知", TransKey = "menu.routine.notice.notification._self", ParentId = parentId, OrderNum = 3, Path = "notification", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "通知目录" }
    };

    /// <summary>
    /// 获取人事考勤三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineHrThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "招聘", TransKey = "menu.routine.hr.recruitment._self", ParentId = parentId, OrderNum = 1, Path = "recruitment", Component = "", MenuType = 0, Perms = "", Icon = "UserAddOutlined", Remark = "招聘管理目录" },
        new HbtMenu { MenuName = "调岗", TransKey = "menu.routine.hr.transfer._self", ParentId = parentId, OrderNum = 2, Path = "transfer", Component = "", MenuType = 0, Perms = "", Icon = "SwapOutlined", Remark = "调岗管理目录" },
        new HbtMenu { MenuName = "加班", TransKey = "menu.routine.hr.overtime._self", ParentId = parentId, OrderNum = 3, Path = "overtime", Component = "", MenuType = 0, Perms = "", Icon = "FieldTimeOutlined", Remark = "加班管理目录" },
        new HbtMenu { MenuName = "请假", TransKey = "menu.routine.hr.leave._self", ParentId = parentId, OrderNum = 4, Path = "leave", Component = "", MenuType = 0, Perms = "", Icon = "ClockCircleOutlined", Remark = "请假管理目录" },
        new HbtMenu { MenuName = "出差", TransKey = "menu.routine.hr.trip._self", ParentId = parentId, OrderNum = 5, Path = "trip", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "出差管理目录" }
    };

    /// <summary>
    /// 获取费用管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineExpenseThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "日常费用", TransKey = "menu.routine.expense.daily._self", ParentId = parentId, OrderNum = 1, Path = "daily", Component = "", MenuType = 0, Perms = "", Icon = "DollarOutlined", Remark = "日常费用目录" },
        new HbtMenu { MenuName = "出差费用", TransKey = "menu.routine.expense.travel._self", ParentId = parentId, OrderNum = 2, Path = "travel", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "出差费用目录" }
    };

    /// <summary>
    /// 获取文件管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineFileThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "新闻管理", TransKey = "menu.routine.file.news._self", ParentId = parentId, OrderNum = 1, Path = "news", Component = "", MenuType = 0, Perms = "", Icon = "GlobalOutlined", Remark = "新闻管理目录" },
        new HbtMenu { MenuName = "规章制度", TransKey = "menu.routine.file.regulation._self", ParentId = parentId, OrderNum = 2, Path = "regulation", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "规章制度目录" },
        new HbtMenu { MenuName = "日常文件", TransKey = "menu.routine.file.daily._self", ParentId = parentId, OrderNum = 3, Path = "daily", Component = "", MenuType = 0, Perms = "", Icon = "FileTextOutlined", Remark = "日常文件目录" },
        new HbtMenu { MenuName = "ISO文件", TransKey = "menu.routine.file.iso._self", ParentId = parentId, OrderNum = 4, Path = "iso", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "ISO文件目录" },
        new HbtMenu { MenuName = "公文文件", TransKey = "menu.routine.file.document._self", ParentId = parentId, OrderNum = 5, Path = "document", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "公文文件目录" }
    };

    /// <summary>
    /// 获取办公用品管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineOfficeSuppliesThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "库存", TransKey = "menu.routine.officesupplies.inventory._self", ParentId = parentId, OrderNum = 1, Path = "inventory", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品库存管理目录" },
        new HbtMenu { MenuName = "领用", TransKey = "menu.routine.officesupplies.usage._self", ParentId = parentId, OrderNum = 2, Path = "usage", Component = "", MenuType = 0, Perms = "", Icon = "ExportOutlined", Remark = "办公用品领用管理目录" }
    };

    /// <summary>
    /// 获取图书管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineBookThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "库存", TransKey = "menu.routine.book.inventory._self", ParentId = parentId, OrderNum = 1, Path = "inventory", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "图书库存管理目录" },
        new HbtMenu { MenuName = "领用", TransKey = "menu.routine.book.usage._self", ParentId = parentId, OrderNum = 2, Path = "usage", Component = "", MenuType = 0, Perms = "", Icon = "ExportOutlined", Remark = "图书领用管理目录" }
    };

    /// <summary>
    /// 获取医务管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineMedicalThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "药品", TransKey = "menu.routine.medical.medicine._self", ParentId = parentId, OrderNum = 1, Path = "medicine", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "药品管理目录" },
        new HbtMenu { MenuName = "领用", TransKey = "menu.routine.medical.usage._self", ParentId = parentId, OrderNum = 2, Path = "usage", Component = "", MenuType = 0, Perms = "", Icon = "ExportOutlined", Remark = "医务领用管理目录" }
    };

    /// <summary>
    /// 获取图书库存四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineBookInventoryFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "请购", TransKey = "menu.routine.book.inventory.requisition", ParentId = parentId, OrderNum = 1, Path = "requisition", Component = "routine/book/inventory/requisition/index", MenuType = 1, Perms = "routine:book:inventory:requisition:list", Icon = "FileAddOutlined", Remark = "图书请购管理" },
        new HbtMenu { MenuName = "入库", TransKey = "menu.routine.book.inventory.inbound", ParentId = parentId, OrderNum = 2, Path = "inbound", Component = "routine/book/inventory/inbound/index", MenuType = 1, Perms = "routine:book:inventory:inbound:list", Icon = "InboxOutlined", Remark = "图书入库管理" },
        new HbtMenu { MenuName = "清单", TransKey = "menu.routine.book.inventory.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/book/inventory/list/index", MenuType = 1, Perms = "routine:book:inventory:list:list", Icon = "BarsOutlined", Remark = "图书清单管理" },
        new HbtMenu { MenuName = "盘点", TransKey = "menu.routine.book.inventory.stocktaking", ParentId = parentId, OrderNum = 4, Path = "stocktaking", Component = "routine/book/inventory/stocktaking/index", MenuType = 1, Perms = "routine:book:inventory:stocktaking:list", Icon = "ReconciliationOutlined", Remark = "图书盘点管理" }
    };

    /// <summary>
    /// 获取图书领用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineBookUsageFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "借阅证", TransKey = "menu.routine.book.usage.card", ParentId = parentId, OrderNum = 1, Path = "card", Component = "routine/book/usage/card/index", MenuType = 1, Perms = "routine:book:usage:card:list", Icon = "IdcardOutlined", Remark = "借阅证管理" },
        new HbtMenu { MenuName = "借阅", TransKey = "menu.routine.book.usage.borrow", ParentId = parentId, OrderNum = 2, Path = "borrow", Component = "routine/book/usage/borrow/index", MenuType = 1, Perms = "routine:book:usage:borrow:list", Icon = "ExportOutlined", Remark = "图书借阅管理" },
        new HbtMenu { MenuName = "归还", TransKey = "menu.routine.book.usage.return", ParentId = parentId, OrderNum = 3, Path = "return", Component = "routine/book/usage/return/index", MenuType = 1, Perms = "routine:book:usage:return:list", Icon = "ImportOutlined", Remark = "图书归还管理" }
    };

    /// <summary>
    /// 获取消息四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineMessageFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "我的消息", TransKey = "menu.routine.notice.message.mymessages", ParentId = parentId, OrderNum = 1, Path = "mymessages", Component = "routine/notice/message/mymessages/index", MenuType = 1, Perms = "routine:notice:message:mymessages:list", Icon = "UserOutlined", Remark = "我的消息" },
        new HbtMenu { MenuName = "消息列表", TransKey = "menu.routine.notice.message.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/notice/message/list/index", MenuType = 1, Perms = "routine:notice:message:list:list", Icon = "BarsOutlined", Remark = "消息列表" }
    };

    /// <summary>
    /// 获取公告四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineAnnouncementFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "公告签收", TransKey = "menu.routine.notice.announcement.signoff", ParentId = parentId, OrderNum = 1, Path = "signoff", Component = "routine/notice/announcement/signoff/index", MenuType = 1, Perms = "routine:notice:announcement:signoff:list", Icon = "CheckSquareOutlined", Remark = "公告签收" },
        new HbtMenu { MenuName = "公告列表", TransKey = "menu.routine.notice.announcement.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/notice/announcement/list/index", MenuType = 1, Perms = "routine:notice:announcement:list:list", Icon = "BarsOutlined", Remark = "公告列表" }
    };

    /// <summary>
    /// 获取通知四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineNotificationFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "通知签阅", TransKey = "menu.routine.notice.notification.ack", ParentId = parentId, OrderNum = 1, Path = "ack", Component = "routine/notice/notification/ack/index", MenuType = 1, Perms = "routine:notice:notification:ack:list", Icon = "FileDoneOutlined", Remark = "通知签阅" },
        new HbtMenu { MenuName = "通知列表", TransKey = "menu.routine.notice.notification.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/notice/notification/list/index", MenuType = 1, Perms = "routine:notice:notification:list:list", Icon = "BarsOutlined", Remark = "通知列表" }
    };

    /// <summary>
    /// 获取招聘四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineRecruitmentFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.recruitment.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/recruitment/apply/index", MenuType = 1, Perms = "routine:recruitment:query", Icon = "FormOutlined", Remark = "招聘申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.hr.recruitment.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/hr/recruitment/list/index", MenuType = 1, Perms = "routine:recruitment:query", Icon = "BarsOutlined", Remark = "招聘列表" }
    };

    /// <summary>
    /// 获取调岗四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineTransferFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.transfer.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/transfer/apply/index", MenuType = 1, Perms = "routine:transfer:query", Icon = "FormOutlined", Remark = "调岗申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.hr.transfer.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/hr/transfer/list/index", MenuType = 1, Perms = "routine:transfer:query", Icon = "BarsOutlined", Remark = "调岗列表" }
    };

    /// <summary>
    /// 获取加班四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineOvertimeFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.overtime.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/overtime/apply/index", MenuType = 1, Perms = "routine:overtime:query", Icon = "FormOutlined", Remark = "加班申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.hr.overtime.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/hr/overtime/list/index", MenuType = 1, Perms = "routine:overtime:query", Icon = "BarsOutlined", Remark = "加班列表" }
    };

    /// <summary>
    /// 获取请假四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineLeaveFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.leave.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/leave/apply/index", MenuType = 1, Perms = "routine:leave:query", Icon = "FormOutlined", Remark = "请假申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.hr.leave.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/hr/leave/list/index", MenuType = 1, Perms = "routine:leave:query", Icon = "BarsOutlined", Remark = "请假列表" }
    };

    /// <summary>
    /// 获取出差四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineBusinessTripFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.trip.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/trip/apply/index", MenuType = 1, Perms = "routine:trip:query", Icon = "FormOutlined", Remark = "出差申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.hr.trip.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/hr/trip/list/index", MenuType = 1, Perms = "routine:trip:query", Icon = "BarsOutlined", Remark = "出差列表" }
    };

    /// <summary>
    /// 获取日常费用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineDailyExpenseFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.expense.daily.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/expense/daily/apply/index", MenuType = 1, Perms = "routine:expense:daily:apply:list", Icon = "FileAddOutlined", Remark = "日常费用申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.expense.daily.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/expense/daily/list/index", MenuType = 1, Perms = "routine:expense:daily:list:list", Icon = "BarsOutlined", Remark = "日常费用列表" }
    };

    /// <summary>
    /// 获取出差费用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineTravelExpenseFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.expense.travel.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/expense/travel/apply/index", MenuType = 1, Perms = "routine:expense:travel:apply:list", Icon = "FileAddOutlined", Remark = "出差费用申请" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.expense.travel.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/expense/travel/list/index", MenuType = 1, Perms = "routine:expense:travel:list:list", Icon = "BarsOutlined", Remark = "出差费用列表" }
    };

    /// <summary>
    /// 获取日常文件四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineDailyFileFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.file.daily.list", ParentId = parentId, OrderNum = 1, Path = "list", Component = "routine/file/daily/list/index", MenuType = 1, Perms = "routine:file:daily:list:list", Icon = "BarsOutlined", Remark = "日常文件列表清单" }
    };

    /// <summary>
    /// 获取ISO文件四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineIsoFileFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "版本", TransKey = "menu.routine.file.iso.version", ParentId = parentId, OrderNum = 1, Path = "version", Component = "routine/file/iso/version/index", MenuType = 1, Perms = "routine:file:iso:version:list", Icon = "HistoryOutlined", Remark = "ISO文件版本管理" },
        new HbtMenu { MenuName = "签收", TransKey = "menu.routine.file.iso.signoff", ParentId = parentId, OrderNum = 2, Path = "signoff", Component = "routine/file/iso/signoff/index", MenuType = 1, Perms = "routine:file:iso:signoff:list", Icon = "CheckSquareOutlined", Remark = "ISO文件签收管理" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.file.iso.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/file/iso/list/index", MenuType = 1, Perms = "routine:file:iso:list:list", Icon = "BarsOutlined", Remark = "ISO文件列表清单" }
    };

    /// <summary>
    /// 获取新闻四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineNewsFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "新闻列表", TransKey = "menu.routine.file.news.list", ParentId = parentId, OrderNum = 1, Path = "list", Component = "routine/file/news/list/index", MenuType = 1, Perms = "routine:file:news:list:list", Icon = "BarsOutlined", Remark = "新闻列表管理" },
    };

    /// <summary>
    /// 获取规章制度四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineRegulationFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "制度发布", TransKey = "menu.routine.file.regulation.publish", ParentId = parentId, OrderNum = 1, Path = "publish", Component = "routine/file/regulation/publish/index", MenuType = 1, Perms = "routine:file:regulation:publish:list", Icon = "SendOutlined", Remark = "规章制度发布" },
        new HbtMenu { MenuName = "制度签收", TransKey = "menu.routine.file.regulation.signoff", ParentId = parentId, OrderNum = 2, Path = "signoff", Component = "routine/file/regulation/signoff/index", MenuType = 1, Perms = "routine:file:regulation:signoff:list", Icon = "CheckSquareOutlined", Remark = "规章制度签收" },
        new HbtMenu { MenuName = "制度列表", TransKey = "menu.routine.file.regulation.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/file/regulation/list/index", MenuType = 1, Perms = "routine:file:regulation:list:list", Icon = "BarsOutlined", Remark = "规章制度列表" }
    };

    /// <summary>
    /// 获取公文文件四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineDocumentFileFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "版本", TransKey = "menu.routine.file.document.version", ParentId = parentId, OrderNum = 1, Path = "version", Component = "routine/file/document/version/index", MenuType = 1, Perms = "routine:file:document:version:list", Icon = "HistoryOutlined", Remark = "公文文件版本管理" },
        new HbtMenu { MenuName = "签收", TransKey = "menu.routine.file.document.signoff", ParentId = parentId, OrderNum = 2, Path = "signoff", Component = "routine/file/document/signoff/index", MenuType = 1, Perms = "routine:file:document:signoff:list", Icon = "CheckSquareOutlined", Remark = "公文文件签收管理" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.file.document.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/file/document/list/index", MenuType = 1, Perms = "routine:file:document:list:list", Icon = "BarsOutlined", Remark = "公文文件列表清单" }
    };

    /// <summary>
    /// 获取办公用品库存四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineOfficeSuppliesInventoryFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "请购", TransKey = "menu.routine.officesupplies.inventory.requisition", ParentId = parentId, OrderNum = 1, Path = "requisition", Component = "routine/officesupplies/inventory/requisition/index", MenuType = 1, Perms = "routine:officesupplies:inventory:requisition:list", Icon = "FileAddOutlined", Remark = "办公用品请购管理" },
        new HbtMenu { MenuName = "入库", TransKey = "menu.routine.officesupplies.inventory.inbound", ParentId = parentId, OrderNum = 2, Path = "inbound", Component = "routine/officesupplies/inventory/inbound/index", MenuType = 1, Perms = "routine:officesupplies:inventory:inbound:list", Icon = "InboxOutlined", Remark = "办公用品入库管理" },
        new HbtMenu { MenuName = "盘点", TransKey = "menu.routine.officesupplies.inventory.stocktaking", ParentId = parentId, OrderNum = 3, Path = "stocktaking", Component = "routine/officesupplies/inventory/stocktaking/index", MenuType = 1, Perms = "routine:officesupplies:inventory:stocktaking:list", Icon = "ReconciliationOutlined", Remark = "办公用品盘点管理" }
    };

    /// <summary>
    /// 获取办公用品领用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineOfficeSuppliesUsageFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.officesupplies.usage.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/officesupplies/usage/apply/index", MenuType = 1, Perms = "routine:officesupplies:usage:apply:list", Icon = "FileAddOutlined", Remark = "办公用品申请管理" },
        new HbtMenu { MenuName = "列表", TransKey = "menu.routine.officesupplies.usage.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/officesupplies/usage/list/index", MenuType = 1, Perms = "routine:officesupplies:usage:list:list", Icon = "BarsOutlined", Remark = "办公用品领用列表" }
    };

    /// <summary>
    /// 获取药品管理四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineMedicineFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "请购", TransKey = "menu.routine.medical.medicine.requisition", ParentId = parentId, OrderNum = 1, Path = "requisition", Component = "routine/medical/medicine/requisition/index", MenuType = 1, Perms = "routine:medical:medicine:requisition:list", Icon = "FileAddOutlined", Remark = "药品请购管理" },
        new HbtMenu { MenuName = "入库", TransKey = "menu.routine.medical.medicine.inbound", ParentId = parentId, OrderNum = 2, Path = "inbound", Component = "routine/medical/medicine/inbound/index", MenuType = 1, Perms = "routine:medical:medicine:inbound:list", Icon = "InboxOutlined", Remark = "药品入库管理" },
        new HbtMenu { MenuName = "清单", TransKey = "menu.routine.medical.medicine.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/medical/medicine/list/index", MenuType = 1, Perms = "routine:medical:medicine:list:list", Icon = "BarsOutlined", Remark = "药品清单管理" },
        new HbtMenu { MenuName = "盘点", TransKey = "menu.routine.medical.medicine.stocktaking", ParentId = parentId, OrderNum = 4, Path = "stocktaking", Component = "routine/medical/medicine/stocktaking/index", MenuType = 1, Perms = "routine:medical:medicine:stocktaking:list", Icon = "ReconciliationOutlined", Remark = "药品盘点管理" }
    };

    /// <summary>
    /// 获取医务领用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineMedicalUsageFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "档案", TransKey = "menu.routine.medical.usage.archive", ParentId = parentId, OrderNum = 1, Path = "archive", Component = "routine/medical/usage/archive/index", MenuType = 1, Perms = "routine:medical:usage:archive:list", Icon = "FileTextOutlined", Remark = "医务档案管理" },
        new HbtMenu { MenuName = "领药", TransKey = "menu.routine.medical.usage.receive", ParentId = parentId, OrderNum = 2, Path = "receive", Component = "routine/medical/usage/receive/index", MenuType = 1, Perms = "routine:medical:usage:receive:list", Icon = "ExportOutlined", Remark = "医务领药管理" },
        new HbtMenu { MenuName = "费用", TransKey = "menu.routine.medical.usage.cost", ParentId = parentId, OrderNum = 3, Path = "cost", Component = "routine/medical/usage/cost/index", MenuType = 1, Perms = "routine:medical:usage:cost:list", Icon = "MoneyCollectOutlined", Remark = "医务费用管理" }
    };

    // ==================== 合同管理菜单 ====================

    /// <summary>
    /// 获取合同管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "合同模板", TransKey = "menu.routine.contract.template._self", ParentId = parentId, OrderNum = 1, Path = "template", Component = "", MenuType = 0, Perms = "", Icon = "FileTextOutlined", Remark = "合同模板目录" },
        new HbtMenu { MenuName = "合同起草", TransKey = "menu.routine.contract.draft._self", ParentId = parentId, OrderNum = 2, Path = "draft", Component = "", MenuType = 0, Perms = "", Icon = "EditOutlined", Remark = "合同起草目录" },
        new HbtMenu { MenuName = "合同审批", TransKey = "menu.routine.contract.approval._self", ParentId = parentId, OrderNum = 3, Path = "approval", Component = "", MenuType = 0, Perms = "", Icon = "CheckSquareOutlined", Remark = "合同审批目录" },
        new HbtMenu { MenuName = "合同执行", TransKey = "menu.routine.contract.execution._self", ParentId = parentId, OrderNum = 4, Path = "execution", Component = "", MenuType = 0, Perms = "", Icon = "PlayCircleOutlined", Remark = "合同执行目录" },
        new HbtMenu { MenuName = "合同归档", TransKey = "menu.routine.contract.archive._self", ParentId = parentId, OrderNum = 5, Path = "archive", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "合同归档目录" }
    };

    /// <summary>
    /// 获取合同模板四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractTemplateFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "模板管理", TransKey = "menu.routine.contract.template.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/contract/template/manage/index", MenuType = 1, Perms = "routine:contract:template:manage:list", Icon = "FileTextOutlined", Remark = "合同模板管理" },
        new HbtMenu { MenuName = "模板分类", TransKey = "menu.routine.contract.template.category", ParentId = parentId, OrderNum = 2, Path = "category", Component = "routine/contract/template/category/index", MenuType = 1, Perms = "routine:contract:template:category:list", Icon = "FolderOutlined", Remark = "合同模板分类" }
    };

    /// <summary>
    /// 获取合同起草四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractDraftFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "起草申请", TransKey = "menu.routine.contract.draft.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/contract/draft/apply/index", MenuType = 1, Perms = "routine:contract:draft:apply:list", Icon = "FileAddOutlined", Remark = "合同起草申请" },
        new HbtMenu { MenuName = "我的起草", TransKey = "menu.routine.contract.draft.my", ParentId = parentId, OrderNum = 2, Path = "my", Component = "routine/contract/draft/my/index", MenuType = 1, Perms = "routine:contract:draft:my:list", Icon = "UserOutlined", Remark = "我的合同起草" }
    };

    /// <summary>
    /// 获取合同审批四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractApprovalFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "待审批", TransKey = "menu.routine.contract.approval.pending", ParentId = parentId, OrderNum = 1, Path = "pending", Component = "routine/contract/approval/pending/index", MenuType = 1, Perms = "routine:contract:approval:pending:list", Icon = "ClockCircleOutlined", Remark = "待审批合同" },
        new HbtMenu { MenuName = "已审批", TransKey = "menu.routine.contract.approval.approved", ParentId = parentId, OrderNum = 2, Path = "approved", Component = "routine/contract/approval/approved/index", MenuType = 1, Perms = "routine:contract:approval:approved:list", Icon = "CheckCircleOutlined", Remark = "已审批合同" },
        new HbtMenu { MenuName = "审批记录", TransKey = "menu.routine.contract.approval.record", ParentId = parentId, OrderNum = 3, Path = "record", Component = "routine/contract/approval/record/index", MenuType = 1, Perms = "routine:contract:approval:record:list", Icon = "HistoryOutlined", Remark = "审批记录" }
    };

    /// <summary>
    /// 获取合同执行四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractExecutionFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "执行跟踪", TransKey = "menu.routine.contract.execution.track", ParentId = parentId, OrderNum = 1, Path = "track", Component = "routine/contract/execution/track/index", MenuType = 1, Perms = "routine:contract:execution:track:list", Icon = "RadarChartOutlined", Remark = "合同执行跟踪" },
        new HbtMenu { MenuName = "变更管理", TransKey = "menu.routine.contract.execution.change", ParentId = parentId, OrderNum = 2, Path = "change", Component = "routine/contract/execution/change/index", MenuType = 1, Perms = "routine:contract:execution:change:list", Icon = "SwapOutlined", Remark = "合同变更管理" },
        new HbtMenu { MenuName = "付款管理", TransKey = "menu.routine.contract.execution.payment", ParentId = parentId, OrderNum = 3, Path = "payment", Component = "routine/contract/execution/payment/index", MenuType = 1, Perms = "routine:contract:execution:payment:list", Icon = "MoneyCollectOutlined", Remark = "合同付款管理" }
    };

    /// <summary>
    /// 获取合同归档四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineContractArchiveFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "归档管理", TransKey = "menu.routine.contract.archive.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/contract/archive/manage/index", MenuType = 1, Perms = "routine:contract:archive:manage:list", Icon = "InboxOutlined", Remark = "合同归档管理" },
        new HbtMenu { MenuName = "查询统计", TransKey = "menu.routine.contract.archive.query", ParentId = parentId, OrderNum = 2, Path = "query", Component = "routine/contract/archive/query/index", MenuType = 1, Perms = "routine:contract:archive:query:list", Icon = "SearchOutlined", Remark = "合同查询统计" }
    };

    // ==================== 项目管理菜单 ====================

    /// <summary>
    /// 获取项目管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "项目信息", TransKey = "menu.routine.project.info._self", ParentId = parentId, OrderNum = 1, Path = "info", Component = "", MenuType = 0, Perms = "", Icon = "ProjectOutlined", Remark = "项目信息目录" },
        new HbtMenu { MenuName = "项目计划", TransKey = "menu.routine.project.plan._self", ParentId = parentId, OrderNum = 2, Path = "plan", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "项目计划目录" },
        new HbtMenu { MenuName = "项目任务", TransKey = "menu.routine.project.task._self", ParentId = parentId, OrderNum = 3, Path = "task", Component = "", MenuType = 0, Perms = "", Icon = "CheckSquareOutlined", Remark = "项目任务目录" },
        new HbtMenu { MenuName = "项目资源", TransKey = "menu.routine.project.resource._self", ParentId = parentId, OrderNum = 4, Path = "resource", Component = "", MenuType = 0, Perms = "", Icon = "TeamOutlined", Remark = "项目资源目录" },
        new HbtMenu { MenuName = "项目监控", TransKey = "menu.routine.project.monitor._self", ParentId = parentId, OrderNum = 5, Path = "monitor", Component = "", MenuType = 0, Perms = "", Icon = "DashboardOutlined", Remark = "项目监控目录" }
    };

    /// <summary>
    /// 获取项目信息四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectInfoFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "项目列表", TransKey = "menu.routine.project.info.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/project/info/list/index", MenuType = 1, Perms = "routine:project:info:list:list", Icon = "BarsOutlined", Remark = "项目列表" },
    };

    /// <summary>
    /// 获取项目计划四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectPlanFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "计划请求", TransKey = "menu.routine.project.plan.request", ParentId = parentId, OrderNum = 2, Path = "request", Component = "routine/project/plan/request/index", MenuType = 1, Perms = "routine:project:plan:request:list", Icon = "CheckSquareOutlined", Remark = "项目计划请求" },
        new HbtMenu { MenuName = "甘特图", TransKey = "menu.routine.project.plan.gantt", ParentId = parentId, OrderNum = 3, Path = "gantt", Component = "routine/project/plan/gantt/index", MenuType = 1, Perms = "routine:project:plan:gantt:list", Icon = "BarChartOutlined", Remark = "项目甘特图" }
    };

    /// <summary>
    /// 获取项目任务四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectTaskFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "任务分配", TransKey = "menu.routine.project.task.assign", ParentId = parentId, OrderNum = 1, Path = "assign", Component = "routine/project/task/assign/index", MenuType = 1, Perms = "routine:project:task:assign:list", Icon = "UserAddOutlined", Remark = "项目任务分配" },
        new HbtMenu { MenuName = "任务跟踪", TransKey = "menu.routine.project.task.track", ParentId = parentId, OrderNum = 2, Path = "track", Component = "routine/project/task/track/index", MenuType = 1, Perms = "routine:project:task:track:list", Icon = "RadarChartOutlined", Remark = "项目任务跟踪" },
        new HbtMenu { MenuName = "任务看板", TransKey = "menu.routine.project.task.board", ParentId = parentId, OrderNum = 3, Path = "board", Component = "routine/project/task/board/index", MenuType = 1, Perms = "routine:project:task:board:list", Icon = "AppstoreOutlined", Remark = "项目任务看板" }
    };

    /// <summary>
    /// 获取项目资源四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectResourceFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "人员管理", TransKey = "menu.routine.project.resource.personnel", ParentId = parentId, OrderNum = 1, Path = "personnel", Component = "routine/project/resource/personnel/index", MenuType = 1, Perms = "routine:project:resource:personnel:list", Icon = "TeamOutlined", Remark = "项目人员管理" },
        new HbtMenu { MenuName = "设备管理", TransKey = "menu.routine.project.resource.equipment", ParentId = parentId, OrderNum = 2, Path = "equipment", Component = "routine/project/resource/equipment/index", MenuType = 1, Perms = "routine:project:resource:equipment:list", Icon = "ToolOutlined", Remark = "项目设备管理" },
        new HbtMenu { MenuName = "预算管理", TransKey = "menu.routine.project.resource.budget", ParentId = parentId, OrderNum = 3, Path = "budget", Component = "routine/project/resource/budget/index", MenuType = 1, Perms = "routine:project:resource:budget:list", Icon = "MoneyCollectOutlined", Remark = "项目预算管理" }
    };

    /// <summary>
    /// 获取项目监控四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectMonitorFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "进度监控", TransKey = "menu.routine.project.monitor.progress", ParentId = parentId, OrderNum = 1, Path = "progress", Component = "routine/project/monitor/progress/index", MenuType = 1, Perms = "routine:project:monitor:progress:list", Icon = "LineChartOutlined", Remark = "项目进度监控" },
        new HbtMenu { MenuName = "质量监控", TransKey = "menu.routine.project.monitor.quality", ParentId = parentId, OrderNum = 2, Path = "quality", Component = "routine/project/monitor/quality/index", MenuType = 1, Perms = "routine:project:monitor:quality:list", Icon = "SafetyOutlined", Remark = "项目质量监控" },
        new HbtMenu { MenuName = "风险监控", TransKey = "menu.routine.project.monitor.risk", ParentId = parentId, OrderNum = 3, Path = "risk", Component = "routine/project/monitor/risk/index", MenuType = 1, Perms = "routine:project:monitor:risk:list", Icon = "WarningOutlined", Remark = "项目风险监控" }
    };

    // ==================== 任务管理菜单 ====================

    /// <summary>
    /// 获取任务管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineQuartzThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "定时任务", TransKey = "menu.routine.quartz.job._self", ParentId = parentId, OrderNum = 1, Path = "job", Component = "", MenuType = 0, Perms = "", Icon = "ClockCircleOutlined", Remark = "定时任务目录" },
        new HbtMenu { MenuName = "任务调度", TransKey = "menu.routine.quartz.schedule._self", ParentId = parentId, OrderNum = 2, Path = "schedule", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "任务调度目录" }
    };

    /// <summary>
    /// 获取定时任务四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineQuartzJobFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "任务配置", TransKey = "menu.routine.quartz.job.config", ParentId = parentId, OrderNum = 1, Path = "config", Component = "routine/quartz/job/config/index", MenuType = 1, Perms = "routine:quartz:job:config:list", Icon = "SettingOutlined", Remark = "定时任务配置" },
        new HbtMenu { MenuName = "任务列表", TransKey = "menu.routine.quartz.job.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/quartz/job/list/index", MenuType = 1, Perms = "routine:quartz:job:list:list", Icon = "BarsOutlined", Remark = "定时任务列表" },
        new HbtMenu { MenuName = "任务状态", TransKey = "menu.routine.quartz.job.status", ParentId = parentId, OrderNum = 3, Path = "status", Component = "routine/quartz/job/status/index", MenuType = 1, Perms = "routine:quartz:job:status:list", Icon = "PlayCircleOutlined", Remark = "定时任务状态" }
    };

    /// <summary>
    /// 获取任务调度四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineQuartzScheduleFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "调度配置", TransKey = "menu.routine.quartz.schedule.config", ParentId = parentId, OrderNum = 1, Path = "config", Component = "routine/quartz/schedule/config/index", MenuType = 1, Perms = "routine:quartz:schedule:config:list", Icon = "ToolOutlined", Remark = "任务调度配置" },
        new HbtMenu { MenuName = "调度监控", TransKey = "menu.routine.quartz.schedule.monitor", ParentId = parentId, OrderNum = 2, Path = "monitor", Component = "routine/quartz/schedule/monitor/index", MenuType = 1, Perms = "routine:quartz:schedule:monitor:list", Icon = "DashboardOutlined", Remark = "任务调度监控" },
        new HbtMenu { MenuName = "调度统计", TransKey = "menu.routine.quartz.schedule.stats", ParentId = parentId, OrderNum = 3, Path = "stats", Component = "routine/quartz/schedule/stats/index", MenuType = 1, Perms = "routine:quartz:schedule:stats:list", Icon = "BarChartOutlined", Remark = "任务调度统计" }
    };



    // ==================== 基础服务菜单 ====================

    /// <summary>
    /// 获取车管管理四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineCarManagementFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "车辆信息", TransKey = "menu.routine.car.management.info", ParentId = parentId, OrderNum = 1, Path = "info", Component = "routine/car/management/info/index", MenuType = 1, Perms = "routine:car:management:info:list", Icon = "IdcardOutlined", Remark = "车辆信息管理" },
        new HbtMenu { MenuName = "维保信息", TransKey = "menu.routine.car.management.maintenance", ParentId = parentId, OrderNum = 2, Path = "maintenance", Component = "routine/car/management/maintenance/index", MenuType = 1, Perms = "routine:car:management:maintenance:list", Icon = "ToolOutlined", Remark = "维保信息管理" }
    };

    /// <summary>
    /// 获取基础服务三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineCoreThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "编码规则", TransKey = "menu.routine.core.numberrule", ParentId = parentId, OrderNum = 1, Path = "numberrule", Component = "core/numberrule/index", MenuType = 1, Perms = "core:numberrule:list", Icon = "CodeOutlined", Remark = "编码规则菜单" },
        new HbtMenu { MenuName = "系统配置", TransKey = "menu.routine.core.config", ParentId = parentId, OrderNum = 2, Path = "configs", Component = "core/configs/index", MenuType = 1, Perms = "core:config:list", Icon = "ToolOutlined", Remark = "系统配置菜单" },
        new HbtMenu { MenuName = "语言管理", TransKey = "menu.routine.core.language", ParentId = parentId, OrderNum = 3, Path = "language", Component = "core/language/index", MenuType = 1, Perms = "core:language:list", Icon = "TranslationOutlined", Remark = "语言管理菜单" },
        new HbtMenu { MenuName = "字典管理", TransKey = "menu.routine.core.dict", ParentId = parentId, OrderNum = 4, Path = "dict", Component = "core/dict/index", MenuType = 1, Perms = "core:dict:list", Icon = "BookOutlined", Remark = "字典管理菜单" }
    };
} 