//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedNatureDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 企业性质字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 企业性质字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedNatureDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedNatureDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化企业性质字典数据
    /// </summary>
    public async Task<(int, int)> InitializeNatureDictDataAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "有限责任公司",
                DictValue = "LLC",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "有限责任公司",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "股份有限公司",
                DictValue = "JSC",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "股份有限公司",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "国有独资公司",
                DictValue = "SOE",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "国有独资公司",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "个人独资企业",
                DictValue = "PIE",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "个人独资企业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "合伙企业",
                DictValue = "PE",
                OrderNum = 5,
                Status = 0,
                
                CssClass = 5,
                ListClass = 5,
                Remark = "合伙企业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "外商投资企业",
                DictValue = "FIE",
                OrderNum = 6,
                Status = 0,
                
                CssClass = 6,
                ListClass = 6,
                Remark = "外商投资企业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "港澳台投资企业",
                DictValue = "HMT",
                OrderNum = 7,
                Status = 0,
                
                CssClass = 7,
                ListClass = 7,
                Remark = "港澳台投资企业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "个体工商户",
                DictValue = "IE",
                OrderNum = 8,
                Status = 0,
                
                CssClass = 8,
                ListClass = 8,
                Remark = "个体工商户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "农民专业合作社",
                DictValue = "FPC",
                OrderNum = 9,
                Status = 0,
                
                CssClass = 9,
                ListClass = 9,
                Remark = "农民专业合作社",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_enterprise_nature",
                DictLabel = "其他企业",
                DictValue = "OTHER",
                OrderNum = 10,
                Status = 0,
                
                CssClass = 10,
                ListClass = 10,
                Remark = "其他企业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 企业性质字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "Hbt365";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 企业性质字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 