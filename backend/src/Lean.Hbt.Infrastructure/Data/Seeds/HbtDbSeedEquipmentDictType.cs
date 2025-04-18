//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 设备相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedEquipmentDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedEquipmentDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化设备相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeEquipmentDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var equipmentDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "设备类型",
                DictType = "equip_type",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "设备类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备状态",
                DictType = "equip_status",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                Remark = "设备状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备维护类型",
                DictType = "equip_maintenance_type",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                Remark = "设备维护类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备故障类型",
                DictType = "equip_fault_type",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                Remark = "设备故障类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备保养周期",
                DictType = "equip_maintenance_cycle",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                Remark = "设备保养周期字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备计量单位",
                DictType = "equip_unit",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                Remark = "设备计量单位字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in equipmentDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                await _dictTypeRepository.CreateAsync(dictType);
                insertCount++;
            }
            else
            {
                existingDictType.DictName = dictType.DictName;
                existingDictType.OrderNum = dictType.OrderNum;
                existingDictType.Status = dictType.Status;
                existingDictType.Remark = dictType.Remark;
                existingDictType.UpdateBy = dictType.UpdateBy;
                existingDictType.UpdateTime = dictType.UpdateTime;
                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
} 