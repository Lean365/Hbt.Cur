//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 财务相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedFinanceDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedFinanceDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化财务相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeFinanceDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "利润中心",
                DictType = "sys_profit_center",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "利润中心字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本中心",
                DictType = "sys_cost_center",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                Remark = "成本中心字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作中心",
                DictType = "sys_work_center",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                Remark = "工作中心字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "评估类",
                DictType = "sys_valuation_class",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                Remark = "评估类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "价格控制类",
                DictType = "sys_price_control",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                Remark = "价格控制类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本核算方法",
                DictType = "sys_cost_method",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                Remark = "成本核算方法字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本要素",
                DictType = "sys_cost_element",
                OrderNum = 7,
                Status = 0,
                TenantId = 0,
                Remark = "成本要素字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本对象",
                DictType = "sys_cost_object",
                OrderNum = 8,
                Status = 0,
                TenantId = 0,
                Remark = "成本对象字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本分配方法",
                DictType = "sys_cost_allocation",
                OrderNum = 9,
                Status = 0,
                TenantId = 0,
                Remark = "成本分配方法字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "成本中心类型",
                DictType = "sys_cost_center_type",
                OrderNum = 10,
                Status = 0,
                TenantId = 0,
                Remark = "成本中心类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in defaultDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(d => d.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                await _dictTypeRepository.CreateAsync(dictType);
                insertCount++;
                _logger.Info($"[创建] 财务字典类型 '{dictType.DictName}' 创建成功");
            }
            else
            {
                existingDictType.DictName = dictType.DictName;
                existingDictType.DictType = dictType.DictType;
                existingDictType.DictBuiltin = dictType.DictBuiltin;
                existingDictType.OrderNum = dictType.OrderNum;
                existingDictType.Status = dictType.Status;
                existingDictType.TenantId = dictType.TenantId;
                existingDictType.Remark = dictType.Remark;
                existingDictType.CreateBy = dictType.CreateBy;
                existingDictType.CreateTime = dictType.CreateTime;
                existingDictType.UpdateBy = "system";
                existingDictType.UpdateTime = DateTime.Now;

                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
                _logger.Info($"[更新] 财务字典类型 '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 