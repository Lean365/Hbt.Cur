//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedRoutineMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 日常办公菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 日常办公菜单数据初始化类
/// </summary>
public class HbtDbSeedRoutineMenu
{
    /// <summary>
    /// 获取日常办公子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineSecondLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "日程管理", TransKey = "menu.routine.schedule._self", ParentId = parentId, OrderNum = 1, Path = "schedule", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "日程管理目录" },
        new HbtMenu { MenuName = "用车管理", TransKey = "menu.routine.car._self", ParentId = parentId, OrderNum = 2, Path = "car", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "用车管理目录" },
        new HbtMenu { MenuName = "邮件管理", TransKey = "menu.routine.email._self", ParentId = parentId, OrderNum = 3, Path = "email", Component = "", MenuType = 0, Perms = "", Icon = "MailOutlined", Remark = "邮件管理目录" },
        new HbtMenu { MenuName = "会议管理", TransKey = "menu.routine.meeting._self", ParentId = parentId, OrderNum = 4, Path = "meeting", Component = "", MenuType = 0, Perms = "", Icon = "VideoCameraOutlined", Remark = "会议管理目录" },
        new HbtMenu { MenuName = "公告通知", TransKey = "menu.routine.notice._self", ParentId = parentId, OrderNum = 5, Path = "notice", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "公告通知目录" },
        new HbtMenu { MenuName = "人事考勤", TransKey = "menu.routine.hr._self", ParentId = parentId, OrderNum = 6, Path = "hr", Component = "", MenuType = 0, Perms = "", Icon = "UsergroupAddOutlined", Remark = "人事考勤目录" },
        new HbtMenu { MenuName = "费用管理", TransKey = "menu.routine.expense._self", ParentId = parentId, OrderNum = 7, Path = "expense", Component = "", MenuType = 0, Perms = "", Icon = "MoneyCollectOutlined", Remark = "费用管理目录" },
        new HbtMenu { MenuName = "文件管理", TransKey = "menu.routine.file._self", ParentId = parentId, OrderNum = 8, Path = "file", Component = "", MenuType = 0, Perms = "", Icon = "FolderOutlined", Remark = "文件管理目录" },
        new HbtMenu { MenuName = "办公用品", TransKey = "menu.routine.officesupplies._self", ParentId = parentId, OrderNum = 9, Path = "officesupplies", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品管理目录" },
        new HbtMenu { MenuName = "图书管理", TransKey = "menu.routine.book._self", ParentId = parentId, OrderNum = 10, Path = "book", Component = "", MenuType = 0, Perms = "", Icon = "BookOutlined", Remark = "图书管理目录" },
        new HbtMenu { MenuName = "医务管理", TransKey = "menu.routine.medical._self", ParentId = parentId, OrderNum = 11, Path = "medical", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "医务管理目录" }
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
        new HbtMenu { MenuName = "车辆信息", TransKey = "menu.routine.car.info", ParentId = parentId, OrderNum = 1, Path = "info", Component = "routine/car/info/index", MenuType = 1, Perms = "routine:car:info:list", Icon = "IdcardOutlined", Remark = "车辆信息管理" },
        new HbtMenu { MenuName = "用车申请审批", TransKey = "menu.routine.car.application", ParentId = parentId, OrderNum = 2, Path = "application", Component = "routine/car/application/index", MenuType = 1, Perms = "routine:car:application:list", Icon = "FileDoneOutlined", Remark = "用车申请审批" },
        new HbtMenu { MenuName = "用车看板", TransKey = "menu.routine.car.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/car/dashboard/index", MenuType = 1, Perms = "routine:car:dashboard:list", Icon = "DashboardOutlined", Remark = "用车看板" },
        new HbtMenu { MenuName = "维护保养", TransKey = "menu.routine.car.maintenance", ParentId = parentId, OrderNum = 4, Path = "maintenance", Component = "routine/car/maintenance/index", MenuType = 1, Perms = "routine:car:maintenance:list", Icon = "ToolOutlined", Remark = "车辆维护保养" }
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
        new HbtMenu { MenuName = "会议室", TransKey = "menu.routine.meeting.room", ParentId = parentId, OrderNum = 1, Path = "room", Component = "routine/meeting/room/index", MenuType = 1, Perms = "routine:meeting:room:list", Icon = "HomeOutlined", Remark = "会议室管理" },
        new HbtMenu { MenuName = "我的会议", TransKey = "menu.routine.meeting.mymeeting", ParentId = parentId, OrderNum = 2, Path = "mymeeting", Component = "routine/meeting/mymeeting/index", MenuType = 1, Perms = "routine:meeting:mymeeting:list", Icon = "UserOutlined", Remark = "我的会议" },
        new HbtMenu { MenuName = "预约", TransKey = "menu.routine.meeting.booking", ParentId = parentId, OrderNum = 3, Path = "booking", Component = "routine/meeting/booking/index", MenuType = 1, Perms = "routine:meeting:booking:list", Icon = "PlusSquareOutlined", Remark = "会议预约" },
        new HbtMenu { MenuName = "看板", TransKey = "menu.routine.meeting.dashboard", ParentId = parentId, OrderNum = 4, Path = "dashboard", Component = "routine/meeting/dashboard/index", MenuType = 1, Perms = "routine:meeting:dashboard:list", Icon = "DashboardOutlined", Remark = "会议看板" }
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
        new HbtMenu { MenuName = "日常", TransKey = "menu.routine.file.daily._self", ParentId = parentId, OrderNum = 1, Path = "daily", Component = "", MenuType = 0, Perms = "", Icon = "FileTextOutlined", Remark = "日常文件目录" },
        new HbtMenu { MenuName = "ISO", TransKey = "menu.routine.file.iso._self", ParentId = parentId, OrderNum = 2, Path = "iso", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "ISO文件目录" },
        new HbtMenu { MenuName = "公文", TransKey = "menu.routine.file.document._self", ParentId = parentId, OrderNum = 3, Path = "document", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "公文文件目录" }
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
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.hr.recruitment.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "routine/hr/recruitment/approval/index", MenuType = 1, Perms = "routine:recruitment:query", Icon = "CheckSquareOutlined", Remark = "招聘审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.hr.recruitment.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/hr/recruitment/list/index", MenuType = 1, Perms = "routine:recruitment:query", Icon = "BarsOutlined", Remark = "招聘列表清单" }
    };

    /// <summary>
    /// 获取调岗四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineTransferFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.transfer.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/transfer/apply/index", MenuType = 1, Perms = "routine:transfer:query", Icon = "FormOutlined", Remark = "调岗申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.hr.transfer.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "routine/hr/transfer/approval/index", MenuType = 1, Perms = "routine:transfer:query", Icon = "CheckSquareOutlined", Remark = "调岗审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.hr.transfer.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/hr/transfer/list/index", MenuType = 1, Perms = "routine:transfer:query", Icon = "BarsOutlined", Remark = "调岗列表清单" }
    };

    /// <summary>
    /// 获取加班四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineOvertimeFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.overtime.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/overtime/apply/index", MenuType = 1, Perms = "routine:overtime:query", Icon = "FormOutlined", Remark = "加班申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.hr.overtime.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "routine/hr/overtime/approval/index", MenuType = 1, Perms = "routine:overtime:query", Icon = "CheckSquareOutlined", Remark = "加班审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.hr.overtime.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/hr/overtime/list/index", MenuType = 1, Perms = "routine:overtime:query", Icon = "BarsOutlined", Remark = "加班列表清单" }
    };

    /// <summary>
    /// 获取请假四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineLeaveFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.leave.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/leave/apply/index", MenuType = 1, Perms = "routine:leave:query", Icon = "FormOutlined", Remark = "请假申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.hr.leave.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "routine/hr/leave/approval/index", MenuType = 1, Perms = "routine:leave:query", Icon = "CheckSquareOutlined", Remark = "请假审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.hr.leave.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/hr/leave/list/index", MenuType = 1, Perms = "routine:leave:query", Icon = "BarsOutlined", Remark = "请假列表清单" }
    };

    /// <summary>
    /// 获取出差四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineBusinessTripFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.hr.trip.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/hr/trip/apply/index", MenuType = 1, Perms = "routine:trip:query", Icon = "FormOutlined", Remark = "出差申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.hr.trip.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "routine/hr/trip/approval/index", MenuType = 1, Perms = "routine:trip:query", Icon = "CheckSquareOutlined", Remark = "出差审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.hr.trip.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/hr/trip/list/index", MenuType = 1, Perms = "routine:trip:query", Icon = "BarsOutlined", Remark = "出差列表清单" }
    };

    /// <summary>
    /// 获取日常费用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineDailyExpenseFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.expense.daily.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/expense/daily/apply/index", MenuType = 1, Perms = "routine:expense:daily:apply:list", Icon = "FileAddOutlined", Remark = "日常费用申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.expense.daily.approve", ParentId = parentId, OrderNum = 2, Path = "approve", Component = "routine/expense/daily/approve/index", MenuType = 1, Perms = "routine:expense:daily:approve:list", Icon = "CheckSquareOutlined", Remark = "日常费用审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.expense.daily.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/expense/daily/list/index", MenuType = 1, Perms = "routine:expense:daily:list:list", Icon = "BarsOutlined", Remark = "日常费用列表清单" }
    };

    /// <summary>
    /// 获取出差费用四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineTravelExpenseFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "申请", TransKey = "menu.routine.expense.travel.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/expense/travel/apply/index", MenuType = 1, Perms = "routine:expense:travel:apply:list", Icon = "FileAddOutlined", Remark = "出差费用申请" },
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.expense.travel.approve", ParentId = parentId, OrderNum = 2, Path = "approve", Component = "routine/expense/travel/approve/index", MenuType = 1, Perms = "routine:expense:travel:approve:list", Icon = "CheckSquareOutlined", Remark = "出差费用审批" },
        new HbtMenu { MenuName = "列表清单", TransKey = "menu.routine.expense.travel.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/expense/travel/list/index", MenuType = 1, Perms = "routine:expense:travel:list:list", Icon = "BarsOutlined", Remark = "出差费用列表清单" }
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
        new HbtMenu { MenuName = "审批", TransKey = "menu.routine.officesupplies.usage.approve", ParentId = parentId, OrderNum = 2, Path = "approve", Component = "routine/officesupplies/usage/approve/index", MenuType = 1, Perms = "routine:officesupplies:usage:approve:list", Icon = "CheckSquareOutlined", Remark = "办公用品审批管理" },
        new HbtMenu { MenuName = "领用", TransKey = "menu.routine.officesupplies.usage.receive", ParentId = parentId, OrderNum = 3, Path = "receive", Component = "routine/officesupplies/usage/receive/index", MenuType = 1, Perms = "routine:officesupplies:usage:receive:list", Icon = "ExportOutlined", Remark = "办公用品领用管理" }
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
} 