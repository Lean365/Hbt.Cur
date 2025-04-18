//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMaterialDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 物料相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 物料相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedMaterialDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedMaterialDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化物料相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeMaterialDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "物料类型",
                DictType = "sys_material_type",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "物料类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料组",
                DictType = "sys_material_group",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                Remark = "物料组字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料分类",
                DictType = "sys_material_category",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                Remark = "物料分类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料状态",
                DictType = "sys_material_status",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                Remark = "物料状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料来源",
                DictType = "sys_material_source",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                Remark = "物料来源字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料计价方式",
                DictType = "sys_material_valuation",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                Remark = "物料计价方式字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料批次管理",
                DictType = "sys_material_batch",
                OrderNum = 7,
                Status = 0,
                TenantId = 0,
                Remark = "物料批次管理字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料序列号管理",
                DictType = "sys_material_serial",
                OrderNum = 8,
                Status = 0,
                TenantId = 0,
                Remark = "物料序列号管理字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料库存管理",
                DictType = "sys_material_stock",
                OrderNum = 9,
                Status = 0,
                TenantId = 0,
                Remark = "物料库存管理字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "物料成本核算",
                DictType = "sys_material_cost",
                OrderNum = 10,
                Status = 0,
                TenantId = 0,
                Remark = "物料成本核算字典",
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
                _logger.Info($"[创建] 物料字典类型 '{dictType.DictName}' 创建成功");
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
                _logger.Info($"[更新] 物料字典类型 '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}