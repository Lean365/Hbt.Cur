//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedProductionDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 生产相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 生产相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedProductionDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedProductionDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化生产相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeProductionDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var productionDictData = new List<HbtDictData>
        {
            // 生产订单类型
            new HbtDictData { DictType = "prod_order_type", DictLabel = "标准生产订单", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "标准生产订单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_order_type", DictLabel = "返工生产订单", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "返工生产订单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_order_type", DictLabel = "样品生产订单", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "样品生产订单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产状态
            new HbtDictData { DictType = "prod_status", DictLabel = "计划中", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "生产计划中", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_status", DictLabel = "生产中", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "正在生产", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_status", DictLabel = "已完成", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "生产完成", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_status", DictLabel = "已取消", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "生产取消", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_status", DictLabel = "已暂停", DictValue = "5", OrderNum = 5, Status = 0, TenantId = 0, Remark = "生产暂停", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产工序类型
            new HbtDictData { DictType = "prod_process_type", DictLabel = "加工工序", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "加工工序", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_process_type", DictLabel = "装配工序", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "装配工序", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_process_type", DictLabel = "检验工序", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "检验工序", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_process_type", DictLabel = "包装工序", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "包装工序", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产资源类型
            new HbtDictData { DictType = "prod_resource_type", DictLabel = "设备", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "生产设备", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_resource_type", DictLabel = "工装", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "工装夹具", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_resource_type", DictLabel = "模具", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "生产模具", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_resource_type", DictLabel = "人员", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "生产人员", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产计划类型
            new HbtDictData { DictType = "prod_plan_type", DictLabel = "主生产计划", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "主生产计划", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_plan_type", DictLabel = "物料需求计划", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "物料需求计划", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_plan_type", DictLabel = "能力需求计划", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "能力需求计划", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产异常类型
            new HbtDictData { DictType = "prod_exception_type", DictLabel = "设备故障", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "设备故障异常", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_exception_type", DictLabel = "质量问题", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "质量问题异常", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_exception_type", DictLabel = "物料短缺", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "物料短缺异常", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_exception_type", DictLabel = "人员缺勤", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "人员缺勤异常", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产工单优先级
            new HbtDictData { DictType = "prod_work_order_priority", DictLabel = "紧急", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "紧急工单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_work_order_priority", DictLabel = "高", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "高优先级工单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_work_order_priority", DictLabel = "中", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "中优先级工单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_work_order_priority", DictLabel = "低", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "低优先级工单", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },

            // 生产质量等级
            new HbtDictData { DictType = "prod_quality_level", DictLabel = "优等品", DictValue = "1", OrderNum = 1, Status = 0, TenantId = 0, Remark = "优等品", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_quality_level", DictLabel = "合格品", DictValue = "2", OrderNum = 2, Status = 0, TenantId = 0, Remark = "合格品", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_quality_level", DictLabel = "次品", DictValue = "3", OrderNum = 3, Status = 0, TenantId = 0, Remark = "次品", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "prod_quality_level", DictLabel = "废品", DictValue = "4", OrderNum = 4, Status = 0, TenantId = 0, Remark = "废品", CreateBy = "system", CreateTime = DateTime.Now, UpdateBy = "system", UpdateTime = DateTime.Now }
        };

        foreach (var dictData in productionDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
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