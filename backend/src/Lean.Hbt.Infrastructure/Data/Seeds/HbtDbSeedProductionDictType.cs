//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedProductionDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 生产相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 生产相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedProductionDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedProductionDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化生产相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeProductionDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var productionDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "生产订单类型",
                DictType = "prod_order_type",
                OrderNum = 1,
                Status = 0,
                
                Remark = "生产订单类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产状态",
                DictType = "prod_status",
                OrderNum = 2,
                Status = 0,
                
                Remark = "生产状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产工序类型",
                DictType = "prod_process_type",
                OrderNum = 3,
                Status = 0,
                
                Remark = "生产工序类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产资源类型",
                DictType = "prod_resource_type",
                OrderNum = 4,
                Status = 0,
                
                Remark = "生产资源类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产计划类型",
                DictType = "prod_plan_type",
                OrderNum = 5,
                Status = 0,
                
                Remark = "生产计划类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产异常类型",
                DictType = "prod_exception_type",
                OrderNum = 6,
                Status = 0,
                
                Remark = "生产异常类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产工单优先级",
                DictType = "prod_work_order_priority",
                OrderNum = 7,
                Status = 0,
                
                Remark = "生产工单优先级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产质量等级",
                DictType = "prod_quality_level",
                OrderNum = 8,
                Status = 0,
                
                Remark = "生产质量等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in productionDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                
                dictType.CreateBy = "Hbt365";
                dictType.CreateTime = DateTime.Now;
                dictType.UpdateBy = "Hbt365";
                dictType.UpdateTime = DateTime.Now;
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