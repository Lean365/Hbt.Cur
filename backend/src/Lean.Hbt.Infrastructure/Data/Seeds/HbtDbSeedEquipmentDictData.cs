//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedEquipmentDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 设备相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedEquipmentDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedEquipmentDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化设备相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeEquipmentDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var equipmentDictData = new List<HbtDictData>
        {
            // 设备类型
            new HbtDictData { DictType = "equip_type", DictLabel = "生产设备", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "生产设备", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_type", DictLabel = "检测设备", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "检测设备", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_type", DictLabel = "工装夹具", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "工装夹具", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_type", DictLabel = "模具", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "模具", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 设备状态
            new HbtDictData { DictType = "equip_status", DictLabel = "正常", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "设备正常运行", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_status", DictLabel = "维修中", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "设备正在维修", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_status", DictLabel = "保养中", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "设备正在保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_status", DictLabel = "故障", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "设备故障", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_status", DictLabel = "报废", DictValue = "5", OrderNum = 5, Status = 0, TenantId = 0, Remark = "设备报废", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 设备维护类型
            new HbtDictData { DictType = "equip_maintenance_type", DictLabel = "日常保养", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "日常保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_type", DictLabel = "定期保养", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "定期保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_type", DictLabel = "预防性维护", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "预防性维护", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_type", DictLabel = "故障维修", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "故障维修", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 设备故障类型
            new HbtDictData { DictType = "equip_fault_type", DictLabel = "机械故障", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "机械故障", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_fault_type", DictLabel = "电气故障", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "电气故障", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_fault_type", DictLabel = "控制系统故障", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "控制系统故障", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_fault_type", DictLabel = "其他故障", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "其他故障", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 设备保养周期
            new HbtDictData { DictType = "equip_maintenance_cycle", DictLabel = "每日", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "每日保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_cycle", DictLabel = "每周", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "每周保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_cycle", DictLabel = "每月", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "每月保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_cycle", DictLabel = "每季度", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "每季度保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_maintenance_cycle", DictLabel = "每年", DictValue = "5", OrderNum = 5, Status = 0, TenantId = 0, Remark = "每年保养", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 设备计量单位
            new HbtDictData { DictType = "equip_unit", DictLabel = "台", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "台", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_unit", DictLabel = "套", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "套", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_unit", DictLabel = "件", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "件", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "equip_unit", DictLabel = "个", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "个", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now }
        };

        foreach (var dictData in equipmentDictData)
        {
            var existingDictData = await _dictDataRepository.GetInfoAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.Status = dictData.Status;
                existingDictData.Remark = dictData.Remark;
                existingDictData.UpdateBy = dictData.UpdateBy;
                existingDictData.UpdateTime = dictData.UpdateTime;
                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
} 