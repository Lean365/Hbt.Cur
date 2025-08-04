//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedNatureDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 企业性质字典类型种子数据提供类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 企业性质字典类型种子数据提供类
/// </summary>
public class HbtDbSeedNatureDictType
{
    /// <summary>
    /// 获取企业性质字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetNatureDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "企业性质",
                DictType = "sys_enterprise_nature",
                OrderNum = 1,
                Status = 0,
                Remark = "企业性质字典"
            }
        };
    }
}