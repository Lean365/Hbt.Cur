//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPurchaseDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPurchaseDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPurchaseDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedPurchaseDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 采购相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedPurchaseDictType
{
    /// <summary>
    /// 获取采购相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetPurchaseDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "采购订单类型",
                DictType = "sys_purchase_order_type",
                OrderNum = 1,
                Status = 0,

                Remark = "采购订单类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购订单状态",
                DictType = "sys_purchase_order_status",
                OrderNum = 2,
                Status = 0,

                Remark = "采购订单状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购组",
                DictType = "sys_purchase_group",
                OrderNum = 3,
                Status = 0,

                Remark = "采购组字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购类型",
                DictType = "sys_purchase_type",
                OrderNum = 4,
                Status = 0,

                Remark = "采购类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购来源",
                DictType = "sys_purchase_source",
                OrderNum = 5,
                Status = 0,

                Remark = "采购来源字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购条件",
                DictType = "sys_purchase_condition",
                OrderNum = 6,
                Status = 0,

                Remark = "采购条件字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "采购计划类型",
                DictType = "sys_purchase_plan_type",
                OrderNum = 7,
                Status = 0,

                Remark = "采购计划类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "供应商类型",
                DictType = "sys_supplier_type",
                OrderNum = 8,
                Status = 0,

                Remark = "供应商类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "供应商等级",
                DictType = "sys_supplier_level",
                OrderNum = 9,
                Status = 0,

                Remark = "供应商等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "供应商状态",
                DictType = "sys_supplier_status",
                OrderNum = 10,
                Status = 0,

                Remark = "供应商状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}