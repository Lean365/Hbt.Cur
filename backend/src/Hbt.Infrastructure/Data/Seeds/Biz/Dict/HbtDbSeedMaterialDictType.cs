//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMaterialDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 物料相关字典类型种子数据提供类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 物料相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedMaterialDictType
{
    /// <summary>
    /// 获取物料相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetMaterialDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "物料类型",
                DictType = "sys_material_type",
                OrderNum = 1,
                Status = 0,
                Remark = "物料类型字典"
            },
            new HbtDictType
            {
                DictName = "物料组",
                DictType = "sys_material_group",
                OrderNum = 2,
                Status = 0,
                Remark = "物料组字典"
            },
            new HbtDictType
            {
                DictName = "物料分类",
                DictType = "sys_material_category",
                OrderNum = 3,
                Status = 0,
                Remark = "物料分类字典"
            },
            new HbtDictType
            {
                DictName = "物料状态",
                DictType = "sys_material_status",
                OrderNum = 4,
                Status = 0,
                Remark = "物料状态字典"
            },
            new HbtDictType
            {
                DictName = "物料来源",
                DictType = "sys_material_source",
                OrderNum = 5,
                Status = 0,
                Remark = "物料来源字典"
            },
            new HbtDictType
            {
                DictName = "物料计价方式",
                DictType = "sys_material_valuation",
                OrderNum = 6,
                Status = 0,
                Remark = "物料计价方式字典"
            },
            new HbtDictType
            {
                DictName = "物料批次管理",
                DictType = "sys_material_batch",
                OrderNum = 7,
                Status = 0,
                Remark = "物料批次管理字典"
            },
            new HbtDictType
            {
                DictName = "物料序列号管理",
                DictType = "sys_material_serial",
                OrderNum = 8,
                Status = 0,
                Remark = "物料序列号管理字典"
            },
            new HbtDictType
            {
                DictName = "物料库存管理",
                DictType = "sys_material_stock",
                OrderNum = 9,
                Status = 0,
                Remark = "物料库存管理字典"
            },
            new HbtDictType
            {
                DictName = "物料成本核算",
                DictType = "sys_material_cost",
                OrderNum = 10,
                Status = 0,
                Remark = "物料成本核算字典"
            }
        };
    }
}