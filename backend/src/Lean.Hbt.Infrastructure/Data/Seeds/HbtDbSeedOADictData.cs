//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedOADictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : OA相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// OA相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedOADictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedOADictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化OA相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeOADictDataAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var oaDictData = new List<HbtDictData>
        {
            // 会议类型
            new HbtDictData { DictType = "sys_meeting_type", DictLabel = "部门会议", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "部门会议", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_type", DictLabel = "项目会议", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "项目会议", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_type", DictLabel = "周例会", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "周例会", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_type", DictLabel = "月度会议", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "月度会议", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 会议状态
            new HbtDictData { DictType = "sys_meeting_status", DictLabel = "未开始", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "未开始状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_status", DictLabel = "进行中", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "进行中状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_status", DictLabel = "已结束", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "已结束状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_meeting_status", DictLabel = "已取消", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "已取消状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 车辆类型
            new HbtDictData { DictType = "sys_vehicle_type", DictLabel = "轿车", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "轿车", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_vehicle_type", DictLabel = "商务车", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "商务车", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_vehicle_type", DictLabel = "面包车", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "面包车", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 车辆状态
            new HbtDictData { DictType = "sys_vehicle_status", DictLabel = "空闲", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "空闲状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_vehicle_status", DictLabel = "使用中", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "使用中状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_vehicle_status", DictLabel = "维修中", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "维修中状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 日程类型
            new HbtDictData { DictType = "sys_schedule_type", DictLabel = "工作日程", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "工作日程", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_schedule_type", DictLabel = "会议日程", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "会议日程", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_schedule_type", DictLabel = "个人日程", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "个人日程", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 日程优先级
            new HbtDictData { DictType = "sys_schedule_priority", DictLabel = "紧急", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "紧急优先级", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_schedule_priority", DictLabel = "重要", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "重要优先级", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_schedule_priority", DictLabel = "普通", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "普通优先级", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 知识分类
            new HbtDictData { DictType = "sys_knowledge_category", DictLabel = "规章制度", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "规章制度", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_knowledge_category", DictLabel = "技术文档", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "技术文档", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_knowledge_category", DictLabel = "经验分享", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "经验分享", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 知识权限
            new HbtDictData { DictType = "sys_knowledge_permission", DictLabel = "公开", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "公开权限", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_knowledge_permission", DictLabel = "部门可见", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "部门可见", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_knowledge_permission", DictLabel = "私有", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "私有权限", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 联系人分组
            new HbtDictData { DictType = "sys_contact_group", DictLabel = "公司内部", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "公司内部联系人", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_contact_group", DictLabel = "客户", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "客户联系人", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_contact_group", DictLabel = "供应商", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "供应商联系人", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 协作类型
            new HbtDictData { DictType = "sys_collaboration_type", DictLabel = "任务协作", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "任务协作", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_collaboration_type", DictLabel = "项目协作", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "项目协作", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_collaboration_type", DictLabel = "文档协作", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "文档协作", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 协作状态
            new HbtDictData { DictType = "sys_collaboration_status", DictLabel = "进行中", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "进行中状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_collaboration_status", DictLabel = "已完成", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "已完成状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "sys_collaboration_status", DictLabel = "已取消", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "已取消状态", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };

        foreach (var dictData in oaDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
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