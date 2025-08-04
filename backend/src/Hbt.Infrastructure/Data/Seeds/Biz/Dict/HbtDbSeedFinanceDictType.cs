//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 财务相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedFinanceDictType
{
    /// <summary>
    /// 获取财务相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetFinanceDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "利润中心",
                DictType = "sys_profit_center",
                OrderNum = 1,
                Status = 0,
                Remark = "利润中心字典"
            },
            new HbtDictType
            {
                DictName = "成本中心",
                DictType = "sys_cost_center",
                OrderNum = 2,
                Status = 0,
                Remark = "成本中心字典"
            },
            new HbtDictType
            {
                DictName = "工作中心",
                DictType = "sys_work_center",
                OrderNum = 3,
                Status = 0,
                Remark = "工作中心字典"
            },
            new HbtDictType
            {
                DictName = "评估类",
                DictType = "sys_valuation_class",
                OrderNum = 4,
                Status = 0,
                Remark = "评估类字典"
            },
            new HbtDictType
            {
                DictName = "价格控制类",
                DictType = "sys_price_control",
                OrderNum = 5,
                Status = 0,
                Remark = "价格控制类字典"
            },
            new HbtDictType
            {
                DictName = "成本核算方法",
                DictType = "sys_cost_method",
                OrderNum = 6,
                Status = 0,
                Remark = "成本核算方法字典"
            },
            new HbtDictType
            {
                DictName = "成本要素",
                DictType = "sys_cost_element",
                OrderNum = 7,
                Status = 0,
                Remark = "成本要素字典"
            }
        };
    }
}