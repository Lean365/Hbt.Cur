//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedNatureDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 企业性质字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 企业性质字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedNatureDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedNatureDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化企业性质字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeNatureDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "企业性质",
                DictType = "sys_enterprise_nature",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "企业性质字典",
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
                _logger.Info($"[创建] 企业性质字典类型 '{dictType.DictName}' 创建成功");
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
                _logger.Info($"[更新] 企业性质字典类型 '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 