//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPurchaseDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 采购相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedPurchaseDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedPurchaseDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化采购相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializePurchaseDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 采购订单类型
            new HbtDictData
            {
                DictType = "sys_purchase_order_type",
                DictLabel = "标准采购订单",
                DictValue = "STD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "标准采购订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_type",
                DictLabel = "框架协议",
                DictValue = "FRAME",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "框架协议采购订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_type",
                DictLabel = "寄售采购",
                DictValue = "CONSIGN",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "寄售采购订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购订单状态
            new HbtDictData
            {
                DictType = "sys_purchase_order_status",
                DictLabel = "草稿",
                DictValue = "DRAFT",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "采购订单草稿状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_status",
                DictLabel = "待审批",
                DictValue = "PENDING",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "采购订单待审批状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_status",
                DictLabel = "已审批",
                DictValue = "APPROVED",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "采购订单已审批状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_status",
                DictLabel = "已收货",
                DictValue = "RECEIVED",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "采购订单已收货状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_order_status",
                DictLabel = "已关闭",
                DictValue = "CLOSED",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "采购订单已关闭状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购组
            new HbtDictData
            {
                DictType = "sys_purchase_group",
                DictLabel = "原材料采购组",
                DictValue = "RAW",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "原材料采购组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_group",
                DictLabel = "设备采购组",
                DictValue = "EQUIP",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "设备采购组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_group",
                DictLabel = "服务采购组",
                DictValue = "SERV",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "服务采购组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购类型
            new HbtDictData
            {
                DictType = "sys_purchase_type",
                DictLabel = "常规采购",
                DictValue = "NORMAL",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "常规采购类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_type",
                DictLabel = "紧急采购",
                DictValue = "URGENT",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "紧急采购类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_type",
                DictLabel = "战略采购",
                DictValue = "STRATEGIC",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "战略采购类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购来源
            new HbtDictData
            {
                DictType = "sys_purchase_source",
                DictLabel = "MRP计划",
                DictValue = "MRP",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "MRP计划采购来源",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_source",
                DictLabel = "库存补充",
                DictValue = "STOCK",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "库存补充采购来源",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_source",
                DictLabel = "手动创建",
                DictValue = "MANUAL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "手动创建采购来源",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购条件
            new HbtDictData
            {
                DictType = "sys_purchase_condition",
                DictLabel = "现金折扣",
                DictValue = "CASH",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "现金折扣采购条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_condition",
                DictLabel = "数量折扣",
                DictValue = "QTY",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "数量折扣采购条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_condition",
                DictLabel = "特殊价格",
                DictValue = "SPECIAL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "特殊价格采购条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 采购计划类型
            new HbtDictData
            {
                DictType = "sys_purchase_plan_type",
                DictLabel = "年度计划",
                DictValue = "YEAR",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "年度采购计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_plan_type",
                DictLabel = "季度计划",
                DictValue = "QUARTER",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "季度采购计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_purchase_plan_type",
                DictLabel = "月度计划",
                DictValue = "MONTH",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "月度采购计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 供应商类型
            new HbtDictData
            {
                DictType = "sys_supplier_type",
                DictLabel = "生产供应商",
                DictValue = "PROD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "生产物料供应商",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_type",
                DictLabel = "贸易供应商",
                DictValue = "TRADE",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "贸易供应商",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_type",
                DictLabel = "服务供应商",
                DictValue = "SERV",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "服务供应商",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 供应商等级
            new HbtDictData
            {
                DictType = "sys_supplier_level",
                DictLabel = "战略供应商",
                DictValue = "STRATEGIC",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "战略供应商等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_level",
                DictLabel = "重要供应商",
                DictValue = "IMPORTANT",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "重要供应商等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_level",
                DictLabel = "普通供应商",
                DictValue = "NORMAL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "普通供应商等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 供应商状态
            new HbtDictData
            {
                DictType = "sys_supplier_status",
                DictLabel = "潜在供应商",
                DictValue = "POTENTIAL",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "潜在供应商状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_status",
                DictLabel = "合格供应商",
                DictValue = "QUALIFIED",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "合格供应商状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_supplier_status",
                DictLabel = "黑名单供应商",
                DictValue = "BLACKLIST",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "黑名单供应商状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 采购字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                existingDictData.TenantId = dictData.TenantId;
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "system";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 采购字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 