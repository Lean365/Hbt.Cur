//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 设备相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedEquipmentDictType
{
    /// <summary>
    /// 获取设备相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetEquipmentDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "设备类型",
                DictType = "equip_type",
                OrderNum = 1,
                Status = 0,

                Remark = "设备类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备状态",
                DictType = "equip_status",
                OrderNum = 2,
                Status = 0,

                Remark = "设备状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备维护类型",
                DictType = "equip_maintenance_type",
                OrderNum = 3,
                Status = 0,

                Remark = "设备维护类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备故障类型",
                DictType = "equip_fault_type",
                OrderNum = 4,
                Status = 0,

                Remark = "设备故障类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备保养周期",
                DictType = "equip_maintenance_cycle",
                OrderNum = 5,
                Status = 0,

                Remark = "设备保养周期字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备计量单位",
                DictType = "equip_unit",
                OrderNum = 6,
                Status = 0,

                Remark = "设备计量单位字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}