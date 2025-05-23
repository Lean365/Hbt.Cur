#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGeneratorDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成器字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成器字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedGeneratorDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedGeneratorDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 设置字典类型的公共属性
    /// </summary>
    private HbtDictType SetCommonProperties(HbtDictType dictType)
    {
        dictType.CreateBy = "system";
        dictType.CreateTime = DateTime.Now;
        dictType.UpdateBy = "Hbt365";
        dictType.UpdateTime = DateTime.Now;
        return dictType;
    }

    /// <summary>
    /// 初始化代码生成器字典类型
    /// </summary>
    public async Task<(int, int)> InitializeGeneratorDictTypeAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var generatorDictTypes = new List<HbtDictType>
        {
            SetCommonProperties(new HbtDictType { DictName = "生成模板", DictType = "gen_template_type", OrderNum = 1, Status = 0,  Remark = "代码生成模板类型" }),
            SetCommonProperties(new HbtDictType { DictName = "前端模板", DictType = "gen_frontend_type", OrderNum = 2, Status = 0,  Remark = "前端模板类型" }),
            SetCommonProperties(new HbtDictType { DictName = "生成模块", DictType = "gen_module_name", OrderNum = 3, Status = 0,  Remark = "代码生成模块名称" }),
            SetCommonProperties(new HbtDictType { DictName = "前端布局", DictType = "gen_frontend_style", OrderNum = 4, Status = 0,  Remark = "前端页面布局" }),
            SetCommonProperties(new HbtDictType { DictName = "按钮样式", DictType = "gen_button_style", OrderNum = 5, Status = 0,  Remark = "按钮样式类型" }),
            SetCommonProperties(new HbtDictType { DictName = "生成方式", DictType = "gen_type", OrderNum = 6, Status = 0,  Remark = "代码生成方式" }),
            SetCommonProperties(new HbtDictType { DictName = "生成功能", DictType = "gen_function", OrderNum = 7, Status = 0,  Remark = "代码生成功能" }),
            SetCommonProperties(new HbtDictType { DictName = "树表配置", DictType = "gen_tree_config", OrderNum = 8, Status = 0,  Remark = "树表配置" }),
            SetCommonProperties(new HbtDictType { DictName = "主子表配置", DictType = "gen_sub_config", OrderNum = 9, Status = 0,  Remark = "主子表配置" })
        };

        foreach (var dictType in generatorDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                dictType.TenantId = tenantId;
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
                existingDictType.TenantId = tenantId;
                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
