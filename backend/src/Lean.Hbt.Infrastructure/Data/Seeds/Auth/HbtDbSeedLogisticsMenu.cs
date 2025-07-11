//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedLogisticsMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 后勤管理菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedLogisticsMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 后勤管理菜单数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 后勤管理菜单数据初始化类
/// </summary>
public class HbtDbSeedLogisticsMenu
{
    /// <summary>
    /// 获取后勤管理二级目录列表
    /// </summary>
    public static List<HbtMenu> GetLogisticsSecondLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "物料管理", TransKey = "menu.logistics.material._self", ParentId = parentId, OrderNum = 1, Path = "material", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "物料管理目录" },
        new HbtMenu { MenuName = "生产管理", TransKey = "menu.logistics.production._self", ParentId = parentId, OrderNum = 2, Path = "production", Component = "", MenuType = 0, Perms = "", Icon = "ExperimentOutlined", Remark = "生产管理目录" },
        new HbtMenu { MenuName = "质量管理", TransKey = "menu.logistics.quality._self", ParentId = parentId, OrderNum = 3, Path = "quality", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "质量管理目录" },
        new HbtMenu { MenuName = "销售管理", TransKey = "menu.logistics.sales._self", ParentId = parentId, OrderNum = 4, Path = "sales", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingCartOutlined", Remark = "销售管理目录" },
        new HbtMenu { MenuName = "设备管理", TransKey = "menu.logistics.equipment._self", ParentId = parentId, OrderNum = 5, Path = "equipment", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "设备管理目录" },
        new HbtMenu { MenuName = "客服管理", TransKey = "menu.logistics.service._self", ParentId = parentId, OrderNum = 6, Path = "service", Component = "", MenuType = 0, Perms = "", Icon = "CustomerServiceOutlined", Remark = "客服管理目录" }
    };
    /// <summary>
    /// 获取物料管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetMaterialThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "物料管理", TransKey = "menu.logistics.material.manage._self", ParentId = parentId, OrderNum = 1, Path = "material", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "物料管理目录" },
        new HbtMenu { MenuName = "采购管理", TransKey = "menu.logistics.material.purchase._self", ParentId = parentId, OrderNum = 2, Path = "purchase", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingOutlined", Remark = "采购管理目录" }
    };

    /// <summary>
    /// 获取生产管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetProductionThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "设计变更", TransKey = "menu.logistics.production.change._self", ParentId = parentId, OrderNum = 1, Path = "change", Component = "", MenuType = 0, Perms = "", Icon = "SwapOutlined", Remark = "设计变更管理目录" },
        new HbtMenu { MenuName = "制造管理", TransKey = "menu.logistics.production.oph._self", ParentId = parentId, OrderNum = 2, Path = "oph", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "制造管理目录" },
        new HbtMenu { MenuName = "SOP管理", TransKey = "menu.logistics.production.sop._self", ParentId = parentId, OrderNum = 3, Path = "sop", Component = "", MenuType = 0, Perms = "", Icon = "FileTextOutlined", Remark = "SOP管理目录" },
        new HbtMenu { MenuName = "技联管理", TransKey = "menu.logistics.production.techcontact._self", ParentId = parentId, OrderNum = 4, Path = "techcontact", Component = "", MenuType = 0, Perms = "", Icon = "MessageOutlined", Remark = "技联管理目录" }
    };
    /// <summary>
    /// 获取生产管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "物料清单", TransKey = "menu.logistics.production.bom", ParentId = parentId, OrderNum = 1, Path = "bom", Component = "logistics/production/bom/index", MenuType = 1, Perms = "logistics:production:bom:list", Icon = "UnorderedListOutlined", Remark = "物料清单管理" },
        new HbtMenu { MenuName = "工作中心", TransKey = "menu.logistics.production.workcenter", ParentId = parentId, OrderNum = 2, Path = "workcenter", Component = "logistics/production/workcenter/index", MenuType = 1, Perms = "logistics:production:workcenter:list", Icon = "ClusterOutlined", Remark = "工作中心管理" },
        new HbtMenu { MenuName = "生产订单", TransKey = "menu.logistics.production.order", ParentId = parentId, OrderNum = 3, Path = "order", Component = "logistics/production/order/index", MenuType = 1, Perms = "logistics:production:order:list", Icon = "FileTextOutlined", Remark = "生产订单管理" },
        new HbtMenu { MenuName = "看板", TransKey = "menu.logistics.production.kanban", ParentId = parentId, OrderNum = 4, Path = "kanban", Component = "logistics/production/kanban/index", MenuType = 1, Perms = "logistics:production:kanban:list", Icon = "DashboardOutlined", Remark = "看板管理" }
    };

    /// <summary>
    /// 获取销售管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetSalesThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "顾客", TransKey = "menu.logistics.sales.customer", ParentId = parentId, OrderNum = 1, Path = "customer", Component = "logistics/sales/customer/index", MenuType = 1, Perms = "logistics:sales:customer:list", Icon = "UserOutlined", Remark = "顾客管理" },
        new HbtMenu { MenuName = "客户", TransKey = "menu.logistics.sales.client", ParentId = parentId, OrderNum = 2, Path = "client", Component = "logistics/sales/client/index", MenuType = 1, Perms = "logistics:sales:client:list", Icon = "TeamOutlined", Remark = "客户管理" },
        new HbtMenu { MenuName = "销售价格", TransKey = "menu.logistics.sales.price", ParentId = parentId, OrderNum = 3, Path = "price", Component = "logistics/sales/price/index", MenuType = 1, Perms = "logistics:sales:price:list", Icon = "MoneyCollectOutlined", Remark = "销售价格管理" },
        new HbtMenu { MenuName = "销售订单", TransKey = "menu.logistics.sales.order", ParentId = parentId, OrderNum = 4, Path = "order", Component = "logistics/sales/order/index", MenuType = 1, Perms = "logistics:sales:order:list", Icon = "ShoppingCartOutlined", Remark = "销售订单管理" }
    };

    /// <summary>
    /// 获取质量管理三级菜单列表
    /// </summary>
    public static List<HbtMenu> GetQualityThirdMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "检验项目", TransKey = "menu.logistics.quality.item", ParentId = parentId, OrderNum = 1, Path = "item", Component = "logistics/quality/item/index", MenuType = 1, Perms = "logistics:quality:item:list", Icon = "CheckSquareOutlined", Remark = "检验项目管理" },
        new HbtMenu { MenuName = "收货检验", TransKey = "menu.logistics.quality.receiving", ParentId = parentId, OrderNum = 2, Path = "receiving", Component = "logistics/quality/receiving/index", MenuType = 1, Perms = "logistics:quality:receiving:list", Icon = "InboxOutlined", Remark = "收货检验管理" },
        new HbtMenu { MenuName = "过程检验", TransKey = "menu.logistics.quality.process", ParentId = parentId, OrderNum = 3, Path = "process", Component = "logistics/quality/process/index", MenuType = 1, Perms = "logistics:quality:process:list", Icon = "ExperimentOutlined", Remark = "过程检验管理" },
        new HbtMenu { MenuName = "入库检验", TransKey = "menu.logistics.quality.storage", ParentId = parentId, OrderNum = 4, Path = "storage", Component = "logistics/quality/storage/index", MenuType = 1, Perms = "logistics:quality:storage:list", Icon = "DatabaseOutlined", Remark = "入库检验管理" },
        new HbtMenu { MenuName = "退货检验", TransKey = "menu.logistics.quality.return", ParentId = parentId, OrderNum = 5, Path = "return", Component = "logistics/quality/return/index", MenuType = 1, Perms = "logistics:quality:return:list", Icon = "RollbackOutlined", Remark = "退货检验管理" }
    };

    /// <summary>
    /// 获取设备管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetEquipmentThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "设备数据", TransKey = "menu.logistics.equipment.master._self", ParentId = parentId, OrderNum = 1, Path = "master", Component = "", MenuType = 0, Perms = "", Icon = "DatabaseOutlined", Remark = "设备数据目录" },
        new HbtMenu { MenuName = "维保管理", TransKey = "menu.logistics.equipment.maintenance._self", ParentId = parentId, OrderNum = 2, Path = "maintenance", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "设备维保管理目录" }
    };

    /// <summary>
    /// 获取客服管理三级目录列表
    /// </summary>
    public static List<HbtMenu> GetServiceThirdLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "客户服务", TransKey = "menu.logistics.service.cs._self", ParentId = parentId, OrderNum = 1, Path = "service", Component = "", MenuType = 0, Perms = "", Icon = "CustomerServiceOutlined", Remark = "客户服务目录" },
        new HbtMenu { MenuName = "客诉管理", TransKey = "menu.logistics.service.cc._self", ParentId = parentId, OrderNum = 2, Path = "complaint", Component = "", MenuType = 0, Perms = "", Icon = "WarningOutlined", Remark = "客诉管理目录" }
    };


    /// <summary>
    /// 获取物料目录四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetMaterialMaterialFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "工厂信息", TransKey = "menu.logistics.material.manage.plant", ParentId = parentId, OrderNum = 1, Path = "plant", Component = "logistics/material/material/plant/index", MenuType = 1, Perms = "logistics:material:material:plant:list", Icon = "ClusterOutlined", Remark = "工厂信息管理" },
        new HbtMenu { MenuName = "物料信息", TransKey = "menu.logistics.material.manage.master", ParentId = parentId, OrderNum = 2, Path = "master", Component = "logistics/material/material/master/index", MenuType = 1, Perms = "logistics:material:material:master:list", Icon = "InfoCircleOutlined", Remark = "物料信息管理" },
        new HbtMenu { MenuName = "工厂物料", TransKey = "menu.logistics.material.manage.plant.master", ParentId = parentId, OrderNum = 3, Path = "plantmaster", Component = "logistics/material/material/plantmaster/index", MenuType = 1, Perms = "logistics:material:material:plantmaster:list", Icon = "ClusterOutlined", Remark = "工厂物料管理" }
    };

    /// <summary>
    /// 获取采购目录四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetMaterialPurchaseFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "卖方", TransKey = "menu.logistics.material.purchase.vendor", ParentId = parentId, OrderNum = 1, Path = "vendor", Component = "logistics/material/purchase/vendor/index", MenuType = 1, Perms = "logistics:material:purchase:vendor:list", Icon = "ShopOutlined", Remark = "卖方管理" },
        new HbtMenu { MenuName = "供应商", TransKey = "menu.logistics.material.purchase.supplier", ParentId = parentId, OrderNum = 2, Path = "supplier", Component = "logistics/material/purchase/supplier/index", MenuType = 1, Perms = "logistics:material:purchase:supplier:list", Icon = "ShopOutlined", Remark = "供应商管理" },
        new HbtMenu { MenuName = "采购价格", TransKey = "menu.logistics.material.purchase.price", ParentId = parentId, OrderNum = 3, Path = "price", Component = "logistics/material/purchase/price/index", MenuType = 1, Perms = "logistics:material:purchase:price:list", Icon = "MoneyCollectOutlined", Remark = "采购价格管理" },
        new HbtMenu { MenuName = "采购申请", TransKey = "menu.logistics.material.purchase.requisition", ParentId = parentId, OrderNum = 4, Path = "requisition", Component = "logistics/material/purchase/requisition/index", MenuType = 1, Perms = "logistics:material:purchase:requisition:list", Icon = "FileAddOutlined", Remark = "采购申请管理" },
        new HbtMenu { MenuName = "采购订单", TransKey = "menu.logistics.material.purchase.order", ParentId = parentId, OrderNum = 5, Path = "order", Component = "logistics/material/purchase/order/index", MenuType = 1, Perms = "logistics:material:purchase:order:list", Icon = "ShoppingOutlined", Remark = "采购订单管理" }
    };

    /// <summary>
    /// 获取设计变更四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionChangeFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "设变实施", TransKey = "menu.logistics.production.change.implementation", ParentId = parentId, OrderNum = 1, Path = "implementation", Component = "logistics/production/change/implementation/index", MenuType = 1, Perms = "logistics:production:change:implementation:list", Icon = "CheckCircleOutlined", Remark = "设变实施管理" },
        new HbtMenu { MenuName = "投入批次", TransKey = "menu.logistics.production.change.batch", ParentId = parentId, OrderNum = 2, Path = "batch", Component = "logistics/production/change/batch/index", MenuType = 1, Perms = "logistics:production:change:batch:list", Icon = "InboxOutlined", Remark = "投入批次管理" },
        new HbtMenu { MenuName = "物料确认", TransKey = "menu.logistics.production.change.material", ParentId = parentId, OrderNum = 3, Path = "material", Component = "logistics/production/change/material/index", MenuType = 1, Perms = "logistics:production:change:material:list", Icon = "CheckSquareOutlined", Remark = "物料确认管理" },
        new HbtMenu { MenuName = "设变查询", TransKey = "menu.logistics.production.change.query", ParentId = parentId, OrderNum = 4, Path = "query", Component = "logistics/production/change/query/index", MenuType = 1, Perms = "logistics:production:change:query:list", Icon = "SearchOutlined", Remark = "设变查询管理" },
        new HbtMenu { MenuName = "旧品管制", TransKey = "menu.logistics.production.change.oldproduct", ParentId = parentId, OrderNum = 5, Path = "oldproduct", Component = "logistics/production/change/oldproduct/index", MenuType = 1, Perms = "logistics:production:change:oldproduct:list", Icon = "StopOutlined", Remark = "旧品管制管理" },
        new HbtMenu { MenuName = "SOP确认", TransKey = "menu.logistics.production.change.sop", ParentId = parentId, OrderNum = 6, Path = "sop", Component = "logistics/production/change/sop/index", MenuType = 1, Perms = "logistics:production:change:sop:list", Icon = "FileTextOutlined", Remark = "SOP确认管理" },
        new HbtMenu { MenuName = "技术联络", TransKey = "menu.logistics.production.change.techcontact", ParentId = parentId, OrderNum = 7, Path = "techcontact", Component = "logistics/production/change/techcontact/index", MenuType = 1, Perms = "logistics:production:change:techcontact:list", Icon = "MessageOutlined", Remark = "技术联络管理" }
    };

    /// <summary>
    /// 获取设计变更四级目录列表
    /// </summary>
    public static List<HbtMenu> GetProductionChangeFourthLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "设变录入", TransKey = "menu.logistics.production.change.input._self", ParentId = parentId, OrderNum = 1, Path = "input", Component = "", MenuType = 0, Perms = "", Icon = "EditOutlined", Remark = "设变录入目录" }
    };

    /// <summary>
    /// 获取制造管理四级目录列表
    /// </summary>
    public static List<HbtMenu> GetProductionOphFourthLevelMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "制一课", TransKey = "menu.logistics.production.oph.workshop1._self", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "制一课管理目录" },
        new HbtMenu { MenuName = "制二课", TransKey = "menu.logistics.production.oph.workshop2._self", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "制二课管理目录" }
    };

    /// <summary>
    /// 获取设变录入五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionChangeInputFifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "技术课", TransKey = "menu.logistics.production.change.input.gijutsu", ParentId = parentId, OrderNum = 1, Path = "gijutsu", Component = "logistics/production/change/input/gijutsu/index", MenuType = 1, Perms = "logistics:production:change:input:gijutsu:list", Icon = "ToolOutlined", Remark = "技术课设变录入" },
        new HbtMenu { MenuName = "生管课", TransKey = "menu.logistics.production.change.input.seikan", ParentId = parentId, OrderNum = 2, Path = "seikan", Component = "logistics/production/change/input/seikan/index", MenuType = 1, Perms = "logistics:production:change:input:seikan:list", Icon = "ClusterOutlined", Remark = "生管课设变录入" },
        new HbtMenu { MenuName = "采购课", TransKey = "menu.logistics.production.change.input.koubai", ParentId = parentId, OrderNum = 3, Path = "koubai", Component = "logistics/production/change/input/koubai/index", MenuType = 1, Perms = "logistics:production:change:input:koubai:list", Icon = "ShoppingOutlined", Remark = "采购课设变录入" },
        new HbtMenu { MenuName = "受检课", TransKey = "menu.logistics.production.change.input.uketsuke", ParentId = parentId, OrderNum = 4, Path = "uketsuke", Component = "logistics/production/change/input/uketsuke/index", MenuType = 1, Perms = "logistics:production:change:input:uketsuke:list", Icon = "CheckSquareOutlined", Remark = "受检课设变录入" },
        new HbtMenu { MenuName = "部管课", TransKey = "menu.logistics.production.change.input.bukan", ParentId = parentId, OrderNum = 5, Path = "bukan", Component = "logistics/production/change/input/bukan/index", MenuType = 1, Perms = "logistics:production:change:input:bukan:list", Icon = "ApartmentOutlined", Remark = "部管课设变录入" },
        new HbtMenu { MenuName = "制二课", TransKey = "menu.logistics.production.change.input.seizou2", ParentId = parentId, OrderNum = 6, Path = "seizou2", Component = "logistics/production/change/input/seizou2/index", MenuType = 1, Perms = "logistics:production:change:input:seizou2:list", Icon = "ClusterOutlined", Remark = "制二课设变录入" },
        new HbtMenu { MenuName = "制一课", TransKey = "menu.logistics.production.change.input.seizou1", ParentId = parentId, OrderNum = 7, Path = "seizou1", Component = "logistics/production/change/input/seizou1/index", MenuType = 1, Perms = "logistics:production:change:input:seizou1:list", Icon = "ClusterOutlined", Remark = "制一课设变录入" },
        new HbtMenu { MenuName = "品管课", TransKey = "menu.logistics.production.change.input.hinkan", ParentId = parentId, OrderNum = 8, Path = "hinkan", Component = "logistics/production/change/input/hinkan/index", MenuType = 1, Perms = "logistics:production:change:input:hinkan:list", Icon = "SafetyCertificateOutlined", Remark = "品管课设变录入" },
        new HbtMenu { MenuName = "制技课", TransKey = "menu.logistics.production.change.input.seizougijutsu", ParentId = parentId, OrderNum = 9, Path = "seizougijutsu", Component = "logistics/production/change/input/seizougijutsu/index", MenuType = 1, Perms = "logistics:production:change:input:seizougijutsu:list", Icon = "ToolOutlined", Remark = "制技课设变录入" }
    };

    /// <summary>
    /// 获取制一课五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionOphWorkshop1FifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "EPP生产", TransKey = "menu.logistics.production.oph.workshop1.epp", ParentId = parentId, OrderNum = 1, Path = "epp", Component = "logistics/production/oph/workshop1/epp/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:epp:list", Icon = "ClusterOutlined", Remark = "EPP生产管理" },
        new HbtMenu { MenuName = "生产OPH", TransKey = "menu.logistics.production.oph.workshop1.oph", ParentId = parentId, OrderNum = 2, Path = "oph", Component = "logistics/production/oph/workshop1/oph/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:oph:list", Icon = "ClusterOutlined", Remark = "生产OPH管理" },
        new HbtMenu { MenuName = "生产改修", TransKey = "menu.logistics.production.oph.workshop1.modify", ParentId = parentId, OrderNum = 3, Path = "modify", Component = "logistics/production/oph/workshop1/modify/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:modify:list", Icon = "ToolOutlined", Remark = "生产改修管理" },
        new HbtMenu { MenuName = "生产返工", TransKey = "menu.logistics.production.oph.workshop1.rework", ParentId = parentId, OrderNum = 4, Path = "rework", Component = "logistics/production/oph/workshop1/rework/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:rework:list", Icon = "ReloadOutlined", Remark = "生产返工管理" },
        new HbtMenu { MenuName = "EPP不良", TransKey = "menu.logistics.production.oph.workshop1.eppDefect", ParentId = parentId, OrderNum = 5, Path = "eppDefect", Component = "logistics/production/oph/workshop1/eppDefect/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:eppDefect:list", Icon = "WarningOutlined", Remark = "EPP不良管理" },
        new HbtMenu { MenuName = "生产不良", TransKey = "menu.logistics.production.oph.workshop1.productionDefect", ParentId = parentId, OrderNum = 6, Path = "productionDefect", Component = "logistics/production/oph/workshop1/productionDefect/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:productionDefect:list", Icon = "WarningOutlined", Remark = "生产不良管理" },
        new HbtMenu { MenuName = "改修不良", TransKey = "menu.logistics.production.oph.workshop1.modifyDefect", ParentId = parentId, OrderNum = 7, Path = "modifyDefect", Component = "logistics/production/oph/workshop1/modifyDefect/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:modifyDefect:list", Icon = "WarningOutlined", Remark = "改修不良管理" },
        new HbtMenu { MenuName = "返工不良", TransKey = "menu.logistics.production.oph.workshop1.reworkDefect", ParentId = parentId, OrderNum = 8, Path = "reworkDefect", Component = "logistics/production/oph/workshop1/reworkDefect/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:reworkDefect:list", Icon = "WarningOutlined", Remark = "返工不良管理" },
        new HbtMenu { MenuName = "EPP工数", TransKey = "menu.logistics.production.oph.workshop1.eppWorktime", ParentId = parentId, OrderNum = 9, Path = "eppWorktime", Component = "logistics/production/oph/workshop1/eppWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:eppWorktime:list", Icon = "FieldTimeOutlined", Remark = "EPP工数管理" },
        new HbtMenu { MenuName = "生产工数", TransKey = "menu.logistics.production.oph.workshop1.productionWorktime", ParentId = parentId, OrderNum = 10, Path = "productionWorktime", Component = "logistics/production/oph/workshop1/productionWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:productionWorktime:list", Icon = "FieldTimeOutlined", Remark = "生产工数管理" },
        new HbtMenu { MenuName = "修改工数", TransKey = "menu.logistics.production.oph.workshop1.modifyWorktime", ParentId = parentId, OrderNum = 11, Path = "modifyWorktime", Component = "logistics/production/oph/workshop1/modifyWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:modifyWorktime:list", Icon = "FieldTimeOutlined", Remark = "修改工数管理" },
        new HbtMenu { MenuName = "返工工数", TransKey = "menu.logistics.production.oph.workshop1.reworkWorktime", ParentId = parentId, OrderNum = 12, Path = "reworkWorktime", Component = "logistics/production/oph/workshop1/reworkWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:reworkWorktime:list", Icon = "FieldTimeOutlined", Remark = "返工工数管理" },
        new HbtMenu { MenuName = "生产报表", TransKey = "menu.logistics.production.oph.workshop1.productionReport", ParentId = parentId, OrderNum = 13, Path = "productionReport", Component = "logistics/production/oph/workshop1/productionReport/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:productionReport:list", Icon = "BarChartOutlined", Remark = "生产报表" },
        new HbtMenu { MenuName = "不良集计", TransKey = "menu.logistics.production.oph.workshop1.defectSummary", ParentId = parentId, OrderNum = 14, Path = "defectSummary", Component = "logistics/production/oph/workshop1/defectSummary/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:defectSummary:list", Icon = "PieChartOutlined", Remark = "不良集计" },
        new HbtMenu { MenuName = "工时报表", TransKey = "menu.logistics.production.oph.workshop1.worktimeReport", ParentId = parentId, OrderNum = 15, Path = "worktimeReport", Component = "logistics/production/oph/workshop1/worktimeReport/index", MenuType = 1, Perms = "logistics:production:oph:workshop1:worktimeReport:list", Icon = "ClockCircleOutlined", Remark = "工时报表" }
    };

    /// <summary>
    /// 获取制二课五级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionOphWorkshop2FifthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "EPP生产", TransKey = "menu.logistics.production.oph.workshop2.epp", ParentId = parentId, OrderNum = 1, Path = "epp", Component = "logistics/production/oph/workshop2/epp/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:epp:list", Icon = "ClusterOutlined", Remark = "EPP生产管理" },
        new HbtMenu { MenuName = "生产OPH", TransKey = "menu.logistics.production.oph.workshop2.oph", ParentId = parentId, OrderNum = 2, Path = "oph", Component = "logistics/production/oph/workshop2/oph/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:oph:list", Icon = "ClusterOutlined", Remark = "生产OPH管理" },
        new HbtMenu { MenuName = "生产改修", TransKey = "menu.logistics.production.oph.workshop2.modify", ParentId = parentId, OrderNum = 3, Path = "modify", Component = "logistics/production/oph/workshop2/modify/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:modify:list", Icon = "ToolOutlined", Remark = "生产改修管理" },
        new HbtMenu { MenuName = "生产返工", TransKey = "menu.logistics.production.oph.workshop2.rework", ParentId = parentId, OrderNum = 4, Path = "rework", Component = "logistics/production/oph/workshop2/rework/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:rework:list", Icon = "ReloadOutlined", Remark = "生产返工管理" },
        new HbtMenu { MenuName = "EPP检查", TransKey = "menu.logistics.production.oph.workshop2.eppInspection", ParentId = parentId, OrderNum = 5, Path = "eppInspection", Component = "logistics/production/oph/workshop2/eppInspection/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:eppInspection:list", Icon = "SearchOutlined", Remark = "EPP检查管理" },
        new HbtMenu { MenuName = "生产检查", TransKey = "menu.logistics.production.oph.workshop2.productionInspection", ParentId = parentId, OrderNum = 6, Path = "productionInspection", Component = "logistics/production/oph/workshop2/productionInspection/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:productionInspection:list", Icon = "SearchOutlined", Remark = "生产检查管理" },
        new HbtMenu { MenuName = "改修检查", TransKey = "menu.logistics.production.oph.workshop2.modifyInspection", ParentId = parentId, OrderNum = 7, Path = "modifyInspection", Component = "logistics/production/oph/workshop2/modifyInspection/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:modifyInspection:list", Icon = "SearchOutlined", Remark = "改修检查管理" },
        new HbtMenu { MenuName = "返工检查", TransKey = "menu.logistics.production.oph.workshop2.reworkInspection", ParentId = parentId, OrderNum = 8, Path = "reworkInspection", Component = "logistics/production/oph/workshop2/reworkInspection/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:reworkInspection:list", Icon = "SearchOutlined", Remark = "返工检查管理" },
        new HbtMenu { MenuName = "EPP修理", TransKey = "menu.logistics.production.oph.workshop2.eppRepair", ParentId = parentId, OrderNum = 9, Path = "eppRepair", Component = "logistics/production/oph/workshop2/eppRepair/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:eppRepair:list", Icon = "ToolOutlined", Remark = "EPP修理管理" },
        new HbtMenu { MenuName = "生产修理", TransKey = "menu.logistics.production.oph.workshop2.productionRepair", ParentId = parentId, OrderNum = 10, Path = "productionRepair", Component = "logistics/production/oph/workshop2/productionRepair/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:productionRepair:list", Icon = "ToolOutlined", Remark = "生产修理管理" },
        new HbtMenu { MenuName = "改修修理", TransKey = "menu.logistics.production.oph.workshop2.modifyRepair", ParentId = parentId, OrderNum = 11, Path = "modifyRepair", Component = "logistics/production/oph/workshop2/modifyRepair/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:modifyRepair:list", Icon = "ToolOutlined", Remark = "改修修理管理" },
        new HbtMenu { MenuName = "返工修理", TransKey = "menu.logistics.production.oph.workshop2.reworkRepair", ParentId = parentId, OrderNum = 12, Path = "reworkRepair", Component = "logistics/production/oph/workshop2/reworkRepair/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:reworkRepair:list", Icon = "ToolOutlined", Remark = "返工修理管理" },
        new HbtMenu { MenuName = "EPP工数", TransKey = "menu.logistics.production.oph.workshop2.eppWorktime", ParentId = parentId, OrderNum = 13, Path = "eppWorktime", Component = "logistics/production/oph/workshop2/eppWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:eppWorktime:list", Icon = "FieldTimeOutlined", Remark = "EPP工数管理" },
        new HbtMenu { MenuName = "生产工数", TransKey = "menu.logistics.production.oph.workshop2.productionWorktime", ParentId = parentId, OrderNum = 14, Path = "productionWorktime", Component = "logistics/production/oph/workshop2/productionWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:productionWorktime:list", Icon = "FieldTimeOutlined", Remark = "生产工数管理" },
        new HbtMenu { MenuName = "修改工数", TransKey = "menu.logistics.production.oph.workshop2.modifyWorktime", ParentId = parentId, OrderNum = 15, Path = "modifyWorktime", Component = "logistics/production/oph/workshop2/modifyWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:modifyWorktime:list", Icon = "FieldTimeOutlined", Remark = "修改工数管理" },
        new HbtMenu { MenuName = "返工工数", TransKey = "menu.logistics.production.oph.workshop2.reworkWorktime", ParentId = parentId, OrderNum = 16, Path = "reworkWorktime", Component = "logistics/production/oph/workshop2/reworkWorktime/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:reworkWorktime:list", Icon = "FieldTimeOutlined", Remark = "返工工数管理" },
        new HbtMenu { MenuName = "生产报表", TransKey = "menu.logistics.production.oph.workshop2.productionReport", ParentId = parentId, OrderNum = 17, Path = "productionReport", Component = "logistics/production/oph/workshop2/productionReport/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:productionReport:list", Icon = "BarChartOutlined", Remark = "生产报表" },
        new HbtMenu { MenuName = "不良集计", TransKey = "menu.logistics.production.oph.workshop2.defectSummary", ParentId = parentId, OrderNum = 18, Path = "defectSummary", Component = "logistics/production/oph/workshop2/defectSummary/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:defectSummary:list", Icon = "PieChartOutlined", Remark = "不良集计" },
        new HbtMenu { MenuName = "工时报表", TransKey = "menu.logistics.production.oph.workshop2.worktimeReport", ParentId = parentId, OrderNum = 19, Path = "worktimeReport", Component = "logistics/production/oph/workshop2/worktimeReport/index", MenuType = 1, Perms = "logistics:production:oph:workshop2:worktimeReport:list", Icon = "ClockCircleOutlined", Remark = "工时报表" }
    };

    // ==================== 设备管理菜单 ====================

    /// <summary>
    /// 获取设备主数据四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetEquipmentMasterFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "设备清单", TransKey = "menu.logistics.equipment.master.list", ParentId = parentId, OrderNum = 1, Path = "inventory", Component = "logistics/equipment/master/inventory/index", MenuType = 1, Perms = "logistics:equipment:master:list:list", Icon = "DatabaseOutlined", Remark = "设备清单管理" },
        new HbtMenu { MenuName = "功能位置", TransKey = "menu.logistics.equipment.master.location", ParentId = parentId, OrderNum = 2, Path = "location", Component = "logistics/equipment/master/location/index", MenuType = 1, Perms = "logistics:equipment:master:location:list", Icon = "EnvironmentOutlined", Remark = "功能位置管理" },
        new HbtMenu { MenuName = "物料关联", TransKey = "menu.logistics.equipment.master.material", ParentId = parentId, OrderNum = 3, Path = "material", Component = "logistics/equipment/master/material/index", MenuType = 1, Perms = "logistics:equipment:master:material:list", Icon = "LinkOutlined", Remark = "物料关联管理" }
    };

    /// <summary>
    /// 获取设备维保管理四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetEquipmentMaintenanceFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "维保工单", TransKey = "menu.logistics.equipment.maintenance.workorder", ParentId = parentId, OrderNum = 1, Path = "workorder", Component = "logistics/equipment/maintenance/workorder/index", MenuType = 1, Perms = "logistics:equipment:maintenance:workorder:list", Icon = "FileTextOutlined", Remark = "维保工单管理" },
        new HbtMenu { MenuName = "分配", TransKey = "menu.logistics.equipment.maintenance.assign", ParentId = parentId, OrderNum = 2, Path = "assign", Component = "logistics/equipment/maintenance/assign/index", MenuType = 1, Perms = "logistics:equipment:maintenance:assign:list", Icon = "UserAddOutlined", Remark = "维保工单分配" },
        new HbtMenu { MenuName = "执行", TransKey = "menu.logistics.equipment.maintenance.execute", ParentId = parentId, OrderNum = 3, Path = "execute", Component = "logistics/equipment/maintenance/execute/index", MenuType = 1, Perms = "logistics:equipment:maintenance:execute:list", Icon = "PlayCircleOutlined", Remark = "维保工单执行" }
    };

    // ==================== SOP管理菜单 ====================

    /// <summary>
    /// 获取SOP管理四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionSopFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "制一", TransKey = "menu.logistics.production.sop.workshop1", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "logistics/production/sop/workshop1/index", MenuType = 1, Perms = "logistics:production:sop:workshop1:list", Icon = "ClusterOutlined", Remark = "制一课SOP管理" },
        new HbtMenu { MenuName = "制二", TransKey = "menu.logistics.production.sop.workshop2", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "logistics/production/sop/workshop2/index", MenuType = 1, Perms = "logistics:production:sop:workshop2:list", Icon = "ClusterOutlined", Remark = "制二课SOP管理" }
    };

    // ==================== 技联管理菜单 ====================

    /// <summary>
    /// 获取技联管理四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetProductionTechcontactFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "EPP联络", TransKey = "menu.logistics.production.techcontact.epp", ParentId = parentId, OrderNum = 1, Path = "epp", Component = "logistics/production/techcontact/epp/index", MenuType = 1, Perms = "logistics:production:techcontact:epp:list", Icon = "MessageOutlined", Remark = "EPP联络管理" },
        new HbtMenu { MenuName = "工程设变", TransKey = "menu.logistics.production.techcontact.engineering", ParentId = parentId, OrderNum = 2, Path = "engineering", Component = "logistics/production/techcontact/engineering/index", MenuType = 1, Perms = "logistics:production:techcontact:engineering:list", Icon = "SwapOutlined", Remark = "工程设变管理" },
        new HbtMenu { MenuName = "外部联络", TransKey = "menu.logistics.production.techcontact.external", ParentId = parentId, OrderNum = 3, Path = "external", Component = "logistics/production/techcontact/external/index", MenuType = 1, Perms = "logistics:production:techcontact:external:list", Icon = "GlobalOutlined", Remark = "外部联络管理" }
    };

    // ==================== 服务管理菜单 ====================

    /// <summary>
    /// 获取客户服务四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetServiceCustomerServiceFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "服务项目", TransKey = "menu.logistics.service.cs.item", ParentId = parentId, OrderNum = 1, Path = "item", Component = "logistics/service/service/item/index", MenuType = 1, Perms = "logistics:service:cs:item:list", Icon = "AppstoreOutlined", Remark = "服务项目管理" },
        new HbtMenu { MenuName = "服务合同", TransKey = "menu.logistics.service.cs.contract", ParentId = parentId, OrderNum = 2, Path = "contract", Component = "logistics/service/service/contract/index", MenuType = 1, Perms = "logistics:service:cs:contract:list", Icon = "FileTextOutlined", Remark = "服务合同管理" },
        new HbtMenu { MenuName = "服务请求", TransKey = "menu.logistics.service.cs.request", ParentId = parentId, OrderNum = 3, Path = "request", Component = "logistics/service/service/request/index", MenuType = 1, Perms = "logistics:service:cs:request:list", Icon = "QuestionCircleOutlined", Remark = "服务请求管理" },
        new HbtMenu { MenuName = "服务工单", TransKey = "menu.logistics.service.cs.workorder", ParentId = parentId, OrderNum = 4, Path = "workorder", Component = "logistics/service/service/workorder/index", MenuType = 1, Perms = "logistics:service:cs:workorder:list", Icon = "FileTextOutlined", Remark = "服务工单管理" },
        new HbtMenu { MenuName = "工时记录", TransKey = "menu.logistics.service.cs.timesheet", ParentId = parentId, OrderNum = 5, Path = "timesheet", Component = "logistics/service/service/timesheet/index", MenuType = 1, Perms = "logistics:service:cs:timesheet:list", Icon = "FieldTimeOutlined", Remark = "工时记录管理" },
        new HbtMenu { MenuName = "物料消耗", TransKey = "menu.logistics.service.cs.consumption", ParentId = parentId, OrderNum = 6, Path = "consumption", Component = "logistics/service/service/consumption/index", MenuType = 1, Perms = "logistics:service:cs:consumption:list", Icon = "InboxOutlined", Remark = "物料消耗管理" },
        new HbtMenu { MenuName = "外协服务", TransKey = "menu.logistics.service.cs.outsourcing", ParentId = parentId, OrderNum = 7, Path = "outsourcing", Component = "logistics/service/service/outsourcing/index", MenuType = 1, Perms = "logistics:service:cs:outsourcing:list", Icon = "TeamOutlined", Remark = "外协服务管理" }
    };

    // ==================== 客诉管理菜单 ====================

    /// <summary>
    /// 获取客户投诉四级菜单列表
    /// </summary>
    public static List<HbtMenu> GetServiceCustomerComplaintFourthMenus(long parentId) => new List<HbtMenu> {
        new HbtMenu { MenuName = "质量通知单", TransKey = "menu.logistics.service.cc.notice", ParentId = parentId, OrderNum = 1, Path = "notice", Component = "logistics/service/complaint/notice/index", MenuType = 1, Perms = "logistics:service:cc:notice:list", Icon = "NotificationOutlined", Remark = "质量通知单管理" },
        new HbtMenu { MenuName = "客户主数据标记", TransKey = "menu.logistics.service.cc.mark", ParentId = parentId, OrderNum = 2, Path = "mark", Component = "logistics/service/complaint/mark/index", MenuType = 1, Perms = "logistics:service:cc:mark:list", Icon = "TagOutlined", Remark = "客户主数据标记管理" },
        new HbtMenu { MenuName = "原因分析", TransKey = "menu.logistics.service.cc.analysis", ParentId = parentId, OrderNum = 3, Path = "analysis", Component = "logistics/service/complaint/analysis/index", MenuType = 1, Perms = "logistics:service:cc:analysis:list", Icon = "FundOutlined", Remark = "原因分析管理" },
        new HbtMenu { MenuName = "纠正措施", TransKey = "menu.logistics.service.cc.corrective", ParentId = parentId, OrderNum = 4, Path = "corrective", Component = "logistics/service/complaint/corrective/index", MenuType = 1, Perms = "logistics:service:cc:corrective:list", Icon = "ToolOutlined", Remark = "纠正措施管理" },
        new HbtMenu { MenuName = "退换货执行", TransKey = "menu.logistics.service.cc.return", ParentId = parentId, OrderNum = 5, Path = "return", Component = "logistics/service/complaint/return/index", MenuType = 1, Perms = "logistics:service:cc:return:list", Icon = "RollbackOutlined", Remark = "退换货执行管理" }
    };


} 