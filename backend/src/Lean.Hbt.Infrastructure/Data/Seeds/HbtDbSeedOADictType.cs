//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedOADictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : OA相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// OA相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedOADictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedOADictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化OA相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeOADictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var oaDictTypes = new List<HbtDictType>
        {
            // 会议相关
            new HbtDictType
            {
                DictName = "会议类型",
                DictType = "sys_meeting_type",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "会议类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "会议状态",
                DictType = "sys_meeting_status",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                Remark = "会议状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 车辆相关
            new HbtDictType
            {
                DictName = "车辆类型",
                DictType = "sys_vehicle_type",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                Remark = "车辆类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "车辆状态",
                DictType = "sys_vehicle_status",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                Remark = "车辆状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 日程相关
            new HbtDictType
            {
                DictName = "日程类型",
                DictType = "sys_schedule_type",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                Remark = "日程类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "日程优先级",
                DictType = "sys_schedule_priority",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                Remark = "日程优先级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 知识相关
            new HbtDictType
            {
                DictName = "知识分类",
                DictType = "sys_knowledge_category",
                OrderNum = 7,
                Status = 0,
                TenantId = 0,
                Remark = "知识分类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "知识权限",
                DictType = "sys_knowledge_permission",
                OrderNum = 8,
                Status = 0,
                TenantId = 0,
                Remark = "知识权限字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 通讯录相关
            new HbtDictType
            {
                DictName = "联系人分组",
                DictType = "sys_contact_group",
                OrderNum = 9,
                Status = 0,
                TenantId = 0,
                Remark = "联系人分组字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 协作相关
            new HbtDictType
            {
                DictName = "协作类型",
                DictType = "sys_collaboration_type",
                OrderNum = 10,
                Status = 0,
                TenantId = 0,
                Remark = "协作类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "协作状态",
                DictType = "sys_collaboration_status",
                OrderNum = 11,
                Status = 0,
                TenantId = 0,
                Remark = "协作状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in oaDictTypes)
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