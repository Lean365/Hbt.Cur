//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSalesDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 销售相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedSalesDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedSalesDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化销售相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeSalesDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 销售订单类型
            new HbtDictData
            {
                DictType = "sys_sales_order_type",
                DictLabel = "标准销售订单",
                DictValue = "STD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "标准销售订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_type",
                DictLabel = "寄售销售订单",
                DictValue = "CONSIGN",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "寄售销售订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_type",
                DictLabel = "退货销售订单",
                DictValue = "RETURN",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "退货销售订单类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 销售订单状态
            new HbtDictData
            {
                DictType = "sys_sales_order_status",
                DictLabel = "草稿",
                DictValue = "DRAFT",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "销售订单草稿状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_status",
                DictLabel = "待审批",
                DictValue = "PENDING",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "销售订单待审批状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_status",
                DictLabel = "已审批",
                DictValue = "APPROVED",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "销售订单已审批状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_status",
                DictLabel = "已发货",
                DictValue = "SHIPPED",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "销售订单已发货状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_order_status",
                DictLabel = "已关闭",
                DictValue = "CLOSED",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "销售订单已关闭状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 销售组织
            new HbtDictData
            {
                DictType = "sys_sales_org",
                DictLabel = "国内销售组织",
                DictValue = "DOM",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "国内销售组织",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_org",
                DictLabel = "出口销售组织",
                DictValue = "EXP",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "出口销售组织",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_org",
                DictLabel = "电商销售组织",
                DictValue = "ECO",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "电商销售组织",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 分销渠道
            new HbtDictData
            {
                DictType = "sys_distribution_channel",
                DictLabel = "直销",
                DictValue = "DIRECT",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "直销渠道",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_distribution_channel",
                DictLabel = "经销商",
                DictValue = "DEALER",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "经销商渠道",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_distribution_channel",
                DictLabel = "零售商",
                DictValue = "RETAIL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "零售商渠道",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 销售类型
            new HbtDictData
            {
                DictType = "sys_sales_type",
                DictLabel = "常规销售",
                DictValue = "NORMAL",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "常规销售类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_type",
                DictLabel = "促销销售",
                DictValue = "PROMO",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "促销销售类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_type",
                DictLabel = "样品销售",
                DictValue = "SAMPLE",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "样品销售类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 销售条件
            new HbtDictData
            {
                DictType = "sys_sales_condition",
                DictLabel = "现金折扣",
                DictValue = "CASH",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "现金折扣销售条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_condition",
                DictLabel = "数量折扣",
                DictValue = "QTY",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "数量折扣销售条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_condition",
                DictLabel = "特殊价格",
                DictValue = "SPECIAL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "特殊价格销售条件",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 销售计划类型
            new HbtDictData
            {
                DictType = "sys_sales_plan_type",
                DictLabel = "年度计划",
                DictValue = "YEAR",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "年度销售计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_plan_type",
                DictLabel = "季度计划",
                DictValue = "QUARTER",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "季度销售计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_sales_plan_type",
                DictLabel = "月度计划",
                DictValue = "MONTH",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "月度销售计划",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 客户类型
            new HbtDictData
            {
                DictType = "sys_customer_type",
                DictLabel = "生产客户",
                DictValue = "PROD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "生产物料客户",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_type",
                DictLabel = "贸易客户",
                DictValue = "TRADE",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "贸易客户",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_type",
                DictLabel = "零售客户",
                DictValue = "RETAIL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "零售客户",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 客户等级
            new HbtDictData
            {
                DictType = "sys_customer_level",
                DictLabel = "战略客户",
                DictValue = "STRATEGIC",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "战略客户等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_level",
                DictLabel = "重要客户",
                DictValue = "IMPORTANT",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "重要客户等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_level",
                DictLabel = "普通客户",
                DictValue = "NORMAL",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "普通客户等级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 客户状态
            new HbtDictData
            {
                DictType = "sys_customer_status",
                DictLabel = "潜在客户",
                DictValue = "POTENTIAL",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "潜在客户状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_status",
                DictLabel = "活跃客户",
                DictValue = "ACTIVE",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "活跃客户状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_status",
                DictLabel = "休眠客户",
                DictValue = "DORMANT",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "休眠客户状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_status",
                DictLabel = "黑名单客户",
                DictValue = "BLACKLIST",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "黑名单客户状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetInfoAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 销售字典数据 '{dictData.DictLabel}' 创建成功");
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
                _logger.Info($"[更新] 销售字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 