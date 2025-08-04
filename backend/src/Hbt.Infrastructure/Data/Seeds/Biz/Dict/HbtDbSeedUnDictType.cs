//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedUnDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典类型种子数据提供类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 字典类型种子数据提供类
/// </summary>
public class HbtDbSeedUnDictType
{
    /// <summary>
    /// 获取字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetUnDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "计量单位",
                DictType = "sys_unit",
                OrderNum = 4,
                Status = 0,
                Remark = "计量单位字典"
            }
        };
    }
}