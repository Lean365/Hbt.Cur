//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "系统状态",
                DictType = "sys_status",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "系统状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否选项",
                DictType = "sys_yes_no",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "是否选项字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "性别类型",
                DictType = "sys_gender",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "性别类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知类型",
                DictType = "sys_notice_type",
                OrderNum = 4,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "通知类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知状态",
                DictType = "sys_notice_status",
                OrderNum = 5,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "通知状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "操作类型",
                DictType = "sys_oper_type",
                OrderNum = 6,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "操作类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "日志级别",
                DictType = "sys_log_level",
                OrderNum = 7,
                Status = HbtStatus.Normal,
                TenantId = 0,
                Remark = "日志级别字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in defaultDictTypes)
        {
            var existingDictType = await _dictTypeRepository.FirstOrDefaultAsync(d => d.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                await _dictTypeRepository.InsertAsync(dictType);
                insertCount++;
                _logger.Info($"[创建] 字典类型 '{dictType.DictName}' 创建成功");
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
                _logger.Info($"[更新] 字典类型 '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 