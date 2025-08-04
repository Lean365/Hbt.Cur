//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSalesDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSalesDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSalesDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedSalesDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典类型种子数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 销售相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedSalesDictType
{
    /// <summary>
    /// 获取销售相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetSalesDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "销售订单类型",
                DictType = "sys_sales_order_type",
                OrderNum = 1,
                Status = 0,

                Remark = "销售订单类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "销售订单状态",
                DictType = "sys_sales_order_status",
                OrderNum = 2,
                Status = 0,

                Remark = "销售订单状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "销售组织",
                DictType = "sys_sales_org",
                OrderNum = 3,
                Status = 0,

                Remark = "销售组织字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "分销渠道",
                DictType = "sys_distribution_channel",
                OrderNum = 4,
                Status = 0,

                Remark = "分销渠道字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "销售类型",
                DictType = "sys_sales_type",
                OrderNum = 5,
                Status = 0,

                Remark = "销售类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "销售条件",
                DictType = "sys_sales_condition",
                OrderNum = 6,
                Status = 0,

                Remark = "销售条件字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "销售计划类型",
                DictType = "sys_sales_plan_type",
                OrderNum = 7,
                Status = 0,

                Remark = "销售计划类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户类型",
                DictType = "sys_customer_type",
                OrderNum = 8,
                Status = 0,

                Remark = "客户类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                OrderNum = 9,
                Status = 0,

                Remark = "客户等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户状态",
                DictType = "sys_customer_status",
                OrderNum = 10,
                Status = 0,

                Remark = "客户状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}