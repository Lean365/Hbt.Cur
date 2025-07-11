//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedIndDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 国民经济行业分类字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedIndDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 国民经济行业分类字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 工业相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedIndDictType
{
    /// <summary>
    /// 获取工业相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetIndDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "国民经济行业分类",
                DictType = "sys_industry_type",
                OrderNum = 1,
                Status = 0,

                Remark = "国民经济行业分类字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}