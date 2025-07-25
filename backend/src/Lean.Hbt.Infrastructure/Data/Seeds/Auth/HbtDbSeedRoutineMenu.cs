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
using NLog.Layouts;

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
        new HbtMenu { MenuName = "新闻管理", TransKey = "menu.routine.news._self", ParentId = parentId, OrderNum = 2, Path = "news", Component = "", MenuType = 0, Perms = "", Icon = "GlobalOutlined", Remark = "新闻管理目录" },
        new HbtMenu { MenuName = "日程管理", TransKey = "menu.routine.schedule._self", ParentId = parentId, OrderNum = 3, Path = "schedule", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "日程管理目录" },
        new HbtMenu { MenuName = "用车管理", TransKey = "menu.routine.vehicle._self", ParentId = parentId, OrderNum = 4, Path = "vehicle", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "用车管理目录" },
        new HbtMenu { MenuName = "邮件管理", TransKey = "menu.routine.email._self", ParentId = parentId, OrderNum = 5, Path = "email", Component = "", MenuType = 0, Perms = "", Icon = "MailOutlined", Remark = "邮件管理目录" },
        new HbtMenu { MenuName = "会议管理", TransKey = "menu.routine.meeting._self", ParentId = parentId, OrderNum = 6, Path = "meeting", Component = "", MenuType = 0, Perms = "", Icon = "VideoCameraOutlined", Remark = "会议管理目录" },
        new HbtMenu { MenuName = "公告通知", TransKey = "menu.routine.notice._self", ParentId = parentId, OrderNum = 7, Path = "notice", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "公告通知目录" },
        new HbtMenu { MenuName = "文件管理", TransKey = "menu.routine.document._self", ParentId = parentId, OrderNum = 8, Path = "document", Component = "", MenuType = 0, Perms = "", Icon = "FolderOutlined", Remark = "文件管理目录" },
        new HbtMenu { MenuName = "合同管理", TransKey = "menu.routine.contract._self", ParentId = parentId, OrderNum = 9, Path = "contract", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "合同管理目录" },
        new HbtMenu { MenuName = "项目管理", TransKey = "menu.routine.project._self", ParentId = parentId, OrderNum = 10, Path = "project", Component = "", MenuType = 0, Perms = "", Icon = "ProjectOutlined", Remark = "项目管理目录" },
        new HbtMenu { MenuName = "任务管理", TransKey = "menu.routine.quartz._self", ParentId = parentId, OrderNum = 11, Path = "quartz", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "任务管理目录" },
        new HbtMenu { MenuName = "办公用品", TransKey = "menu.routine.officesupplies._self", ParentId = parentId, OrderNum = 12, Path = "officesupplies", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品管理目录" },
        new HbtMenu { MenuName = "图书管理", TransKey = "menu.routine.book._self", ParentId = parentId, OrderNum = 13, Path = "book", Component = "", MenuType = 0, Perms = "", Icon = "BookOutlined", Remark = "图书管理目录" },
        new HbtMenu { MenuName = "医务管理", TransKey = "menu.routine.medical._self", ParentId = parentId, OrderNum = 14, Path = "medical", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "医务管理目录" }
    };

    /// <summary>
    /// 获取新闻管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineNewsThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "热点新闻", TransKey = "menu.routine.news.hot", ParentId = parentId, OrderNum = 1, Path = "hot", Component = "routine/news/hot/index", MenuType = 1, Perms = "routine:news:hot:list", Icon = "FireOutlined", Remark = "热点新闻管理" },
        new HbtMenu { MenuName = "评论管理", TransKey = "menu.routine.news.comment", ParentId = parentId, OrderNum = 2, Path = "comment", Component = "routine/news/comment/index", MenuType = 1, Perms = "routine:news:comment:list", Icon = "MessageOutlined", Remark = "评论管理" },
        new HbtMenu { MenuName = "点赞管理", TransKey = "menu.routine.news.like", ParentId = parentId, OrderNum = 3, Path = "like", Component = "routine/news/like/index", MenuType = 1, Perms = "routine:news:like:list", Icon = "LikeOutlined", Remark = "新闻点赞管理" }
    };

    /// <summary>
    /// 获取日程管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineScheduleThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "我的日程", TransKey = "menu.routine.schedule.myschedule", ParentId = parentId, OrderNum = 1, Path = "myschedule", Component = "routine/schedule/myschedule/index", MenuType = 1, Perms = "routine:schedule:myschedule:list", Icon = "UserOutlined", Remark = "我的日程" },
        new HbtMenu { MenuName = "团队日程", TransKey = "menu.routine.schedule.team", ParentId = parentId, OrderNum = 2, Path = "team", Component = "routine/schedule/team/index", MenuType = 1, Perms = "routine:schedule:team:list", Icon = "TeamOutlined", Remark = "团队日程" },
        new HbtMenu { MenuName = "日程看板", TransKey = "menu.routine.schedule.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/schedule/dashboard/index", MenuType = 1, Perms = "routine:schedule:dashboard:list", Icon = "DashboardOutlined", Remark = "日程看板" }
    };

    /// <summary>
    /// 获取用车管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineCarThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "用车调度", TransKey = "menu.routine.vehicle.dispatch", ParentId = parentId, OrderNum = 1, Path = "dispatch", Component = "routine/vehicle/dispatch/index", MenuType = 1, Perms = "routine:vehicle:dispatch:list", Icon = "ScheduleOutlined", Remark = "用车调度" },
        new HbtMenu { MenuName = "车辆信息", TransKey = "menu.routine.vehicle.info", ParentId = parentId, OrderNum = 2, Path = "info", Component = "routine/vehicle/info/index", MenuType = 1, Perms = "routine:vehicle:info:list", Icon = "IdcardOutlined", Remark = "车辆信息" },
        new HbtMenu { MenuName = "驾驶员信息", TransKey = "menu.routine.vehicle.driver", ParentId = parentId, OrderNum = 3, Path = "driver", Component = "routine/vehicle/driver/index", MenuType = 1, Perms = "routine:vehicle:driver:list", Icon = "UserOutlined", Remark = "驾驶员信息" },
        new HbtMenu { MenuName = "维保信息", TransKey = "menu.routine.vehicle.maintenance", ParentId = parentId, OrderNum = 4, Path = "maintenance", Component = "routine/vehicle/maintenance/index", MenuType = 1, Perms = "routine:vehicle:maintenance:list", Icon = "ToolOutlined", Remark = "维保信息" }
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
        new HbtMenu { MenuName = "会议室", TransKey = "menu.routine.meeting.room", ParentId = parentId, OrderNum = 4, Path = "room", Component = "routine/meeting/room/index", MenuType = 1, Perms = "routine:meeting:room:list", Icon = "HomeOutlined", Remark = "会议室管理" }
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
    /// 获取文件管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetRoutineFileThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "规章制度", TransKey = "menu.routine.document.regulation._self", ParentId = parentId, OrderNum = 2, Path = "regulation", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "规章制度目录" },
        new HbtMenu { MenuName = "ISO文件", TransKey = "menu.routine.document.iso._self", ParentId = parentId, OrderNum = 4, Path = "iso", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "ISO文件目录" },
        new HbtMenu { MenuName = "公文文件", TransKey = "menu.routine.document.official._self", ParentId = parentId, OrderNum = 5, Path = "official", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "公文文件目录" }
    };

    /// <summary>
    /// 获取文件管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineFileThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "日常文件", TransKey = "menu.routine.document.file._self", ParentId = parentId, OrderNum = 1, Path = "file", Component = "routine/document/file/index", MenuType = 1, Perms = "routine:document:file:list", Icon = "FileTextOutlined", Remark = "日常文件" },
        new HbtMenu { MenuName = "法律法规", TransKey = "menu.routine.document.law._self", ParentId = parentId, OrderNum = 2, Path = "law", Component = "routine/document/law/index", MenuType = 1, Perms = "routine:document:law:list", Icon = "FileProtectOutlined", Remark = "法律法规" }
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
        new HbtMenu { MenuName = "借阅管理", TransKey = "menu.routine.book.usage.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/book/usage/manage/index", MenuType = 1, Perms = "routine:book:usage:manage:list", Icon = "FileTextOutlined", Remark = "借阅管理" },
        new HbtMenu { MenuName = "借阅证", TransKey = "menu.routine.book.usage.card", ParentId = parentId, OrderNum = 2, Path = "card", Component = "routine/book/usage/card/index", MenuType = 1, Perms = "routine:book:usage:card:list", Icon = "IdcardOutlined", Remark = "借阅证管理" }
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
        new HbtMenu { MenuName = "公告管理", TransKey = "menu.routine.notice.announcement.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/notice/announcement/manage/index", MenuType = 1, Perms = "routine:notice:announcement:manage:list", Icon = "FileTextOutlined", Remark = "公告管理" },
        new HbtMenu { MenuName = "公告收发", TransKey = "menu.routine.notice.announcement.sendreceive", ParentId = parentId, OrderNum = 2, Path = "sendreceive", Component = "routine/notice/announcement/sendreceive/index", MenuType = 1, Perms = "routine:notice:announcement:sendreceive:list", Icon = "SendOutlined", Remark = "公告收发" }
    };

    /// <summary>
    /// 获取通知四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineNotificationFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "通知管理", TransKey = "menu.routine.notice.notification.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/notice/notification/manage/index", MenuType = 1, Perms = "routine:notice:notification:manage:list", Icon = "FileTextOutlined", Remark = "通知管理" },
        new HbtMenu { MenuName = "通知收发", TransKey = "menu.routine.notice.notification.sendreceive", ParentId = parentId, OrderNum = 2, Path = "sendreceive", Component = "routine/notice/notification/sendreceive/index", MenuType = 1, Perms = "routine:notice:notification:sendreceive:list", Icon = "SendOutlined", Remark = "通知收发" }
    };



    /// <summary>
    /// 获取日常文件四级子菜单列表
    /// </summary>


    /// <summary>
    /// 获取ISO文件四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineIsoFileFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "ISO管理", TransKey = "menu.routine.document.iso.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/iso/manage/index", MenuType = 1, Perms = "routine:document:iso:manage:list", Icon = "FileTextOutlined", Remark = "ISO文件管理" },
        new HbtMenu { MenuName = "ISO控制", TransKey = "menu.routine.document.iso.control", ParentId = parentId, OrderNum = 2, Path = "control", Component = "routine/document/iso/control/index", MenuType = 1, Perms = "routine:document:iso:control:list", Icon = "ControlOutlined", Remark = "ISO文件控制" }
    };



    /// <summary>
    /// 获取规章制度四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineRegulationFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "制度管理", TransKey = "menu.routine.document.regulation.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/regulation/manage/index", MenuType = 1, Perms = "routine:document:regulation:manage:list", Icon = "FileTextOutlined", Remark = "规章制度管理" },
        new HbtMenu { MenuName = "制度控制", TransKey = "menu.routine.document.regulation.control", ParentId = parentId, OrderNum = 2, Path = "control", Component = "routine/document/regulation/control/index", MenuType = 1, Perms = "routine:document:regulation:control:list", Icon = "ControlOutlined", Remark = "规章制度控制" }
    };

    /// <summary>
    /// 获取公文文件四级子菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineDocumentFileFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "公文管理", TransKey = "menu.routine.document.official.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/official/manage/index", MenuType = 1, Perms = "routine:document:official:manage:list", Icon = "FileTextOutlined", Remark = "公文文件管理" },
        new HbtMenu { MenuName = "公文发布", TransKey = "menu.routine.document.official.issuance", ParentId = parentId, OrderNum = 2, Path = "issuance", Component = "routine/document/official/issuance/index", MenuType = 1, Perms = "routine:document:official:issuance:list", Icon = "SendOutlined", Remark = "公文文件发布" }
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
        new HbtMenu { MenuName = "合同模板", TransKey = "menu.routine.contract.template", ParentId = parentId, OrderNum = 1, Path = "template", Component = "routine/contract/template/index", MenuType = 1, Perms = "routine:contract:template:list", Icon = "FileTextOutlined", Remark = "合同模板目录" },
        new HbtMenu { MenuName = "合同起草", TransKey = "menu.routine.contract.draft", ParentId = parentId, OrderNum = 2, Path = "draft", Component = "routine/contract/draft/index", MenuType = 1, Perms = "routine:contract:draft:list", Icon = "EditOutlined", Remark = "合同起草目录" },
        new HbtMenu { MenuName = "合同审批", TransKey = "menu.routine.contract.approval", ParentId = parentId, OrderNum = 3, Path = "approval", Component = "routine/contract/approval/index", MenuType = 1, Perms = "routine:contract:approval:list", Icon = "CheckSquareOutlined", Remark = "合同审批目录" },
        new HbtMenu { MenuName = "合同执行", TransKey = "menu.routine.contract.execution", ParentId = parentId, OrderNum = 4, Path = "execution", Component = "routine/contract/execution/index", MenuType = 1, Perms = "routine:contract:execution:list", Icon = "PlayCircleOutlined", Remark = "合同执行目录" },
        new HbtMenu { MenuName = "合同归档", TransKey = "menu.routine.contract.archive", ParentId = parentId, OrderNum = 5, Path = "archive", Component = "routine/contract/archive/index", MenuType = 1, Perms = "routine:contract:archive:list", Icon = "InboxOutlined", Remark = "合同归档目录" }
    };



    // ==================== 项目管理菜单 ====================

    /// <summary>
    /// 获取项目管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineProjectThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "项目信息", TransKey = "menu.routine.project.info", ParentId = parentId, OrderNum = 1, Path = "info", Component = "routine/project/info/index", MenuType = 1, Perms = "routine:project:info:list", Icon = "ProjectOutlined", Remark = "项目信息目录" },
        new HbtMenu { MenuName = "项目计划", TransKey = "menu.routine.project.plan", ParentId = parentId, OrderNum = 2, Path = "plan", Component = "routine/project/plan/index", MenuType = 1, Perms = "routine:project:plan:list", Icon = "CalendarOutlined", Remark = "项目计划目录" },
        new HbtMenu { MenuName = "项目任务", TransKey = "menu.routine.project.task", ParentId = parentId, OrderNum = 3, Path = "task", Component = "routine/project/task/index", MenuType = 1, Perms = "routine:project:task:list", Icon = "CheckSquareOutlined", Remark = "项目任务目录" },
        new HbtMenu { MenuName = "项目资源", TransKey = "menu.routine.project.resource", ParentId = parentId, OrderNum = 4, Path = "resource", Component = "routine/project/resource/index", MenuType = 1, Perms = "routine:project:resource:list", Icon = "TeamOutlined", Remark = "项目资源目录" },
        new HbtMenu { MenuName = "项目监控", TransKey = "menu.routine.project.monitor", ParentId = parentId, OrderNum = 5, Path = "monitor", Component = "routine/project/monitor/index", MenuType = 1, Perms = "routine:project:monitor:list", Icon = "DashboardOutlined", Remark = "项目监控目录" }
    };



    // ==================== 任务管理菜单 ====================

    /// <summary>
    /// 获取任务管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineQuartzThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "定时任务", TransKey = "menu.routine.quartz.job", ParentId = parentId, OrderNum = 1, Path = "job", Component = "routine/quartz/job/index", MenuType = 1, Perms = "routine:quartz:job:list", Icon = "ClockCircleOutlined", Remark = "定时任务目录" },
        new HbtMenu { MenuName = "任务调度", TransKey = "menu.routine.quartz.schedule", ParentId = parentId, OrderNum = 2, Path = "schedule", Component = "routine/quartz/schedule/index", MenuType = 1, Perms = "routine:quartz:schedule:list", Icon = "ScheduleOutlined", Remark = "任务调度目录" }
    };



    // ==================== 基础服务菜单 ====================

    /// <summary>
    /// 获取基础服务三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetRoutineCoreThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "编码规则", TransKey = "menu.routine.core.numberrule", ParentId = parentId, OrderNum = 1, Path = "numberrule", Component = "routine/core/numberrule/index", MenuType = 1, Perms = "core:numberrule:list", Icon = "CodeOutlined", Remark = "编码规则菜单" },
        new HbtMenu { MenuName = "系统配置", TransKey = "menu.routine.core.config", ParentId = parentId, OrderNum = 2, Path = "configs", Component = "routine/core/configs/index", MenuType = 1, Perms = "core:config:list", Icon = "ToolOutlined", Remark = "系统配置菜单" },
        new HbtMenu { MenuName = "语言管理", TransKey = "menu.routine.core.language", ParentId = parentId, OrderNum = 3, Path = "language", Component = "routine/core/language/index", MenuType = 1, Perms = "core:language:list", Icon = "TranslationOutlined", Remark = "语言管理菜单" },
        new HbtMenu { MenuName = "字典管理", TransKey = "menu.routine.core.dict", ParentId = parentId, OrderNum = 4, Path = "dict", Component = "routine/core/dict/index", MenuType = 1, Perms = "core:dict:list", Icon = "BookOutlined", Remark = "字典管理菜单" }
    };
} 