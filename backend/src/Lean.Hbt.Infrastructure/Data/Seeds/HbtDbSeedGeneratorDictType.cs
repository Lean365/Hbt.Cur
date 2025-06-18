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
    public async Task<(int, int)> InitializeGeneratorDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var generatorDictTypes = new List<HbtDictType>
        {
            SetCommonProperties(new HbtDictType { DictName = "数据库类型", DictType = "gen_db_type", OrderNum = 0, Status = 0,  Remark = "代码生成数据库类型" }),
            SetCommonProperties(new HbtDictType { DictName = "生成代码类型", DictType = "gen_code_type", OrderNum = 1, Status = 0,  Remark = "代码生成类型（0：前端代码，1：后端代码，2：SQL代码，3：其他）" }),
            SetCommonProperties(new HbtDictType { DictName = "生成模板分类", DictType = "gen_template_category", OrderNum = 2, Status = 0,  Remark = "代码生成模板分类（1：实体类，2：数据访问层，3：服务层，4：控制器，5：API，6：类型，7：视图，8：翻译，9：其他）" }),
            SetCommonProperties(new HbtDictType { DictName = "模板类型", DictType = "gen_tpl_type", OrderNum = 3, Status = 0,  Remark = "代码生成模板类型（0使用wwwroot/Generator/*.scriban模板 1使用HbtGenTemplate数据表中的模板）" }),
            SetCommonProperties(new HbtDictType { DictName = "生成模板", DictType = "gen_template_type", OrderNum = 4, Status = 0,  Remark = "代码生成模板类型" }),
            SetCommonProperties(new HbtDictType { DictName = "前端模板", DictType = "gen_frontend_type", OrderNum = 5, Status = 0,  Remark = "前端模板类型" }),
            SetCommonProperties(new HbtDictType { DictName = "生成模块", DictType = "gen_module_name", OrderNum = 6, Status = 0,  Remark = "代码生成模块名称" }),
            SetCommonProperties(new HbtDictType { DictName = "前端布局", DictType = "gen_frontend_style", OrderNum = 7, Status = 0,  Remark = "前端页面布局" }),
            SetCommonProperties(new HbtDictType { DictName = "按钮样式", DictType = "gen_button_style", OrderNum = 8, Status = 0,  Remark = "按钮样式类型" }),
            SetCommonProperties(new HbtDictType { DictName = "生成方式", DictType = "gen_method", OrderNum = 9, Status = 0,  Remark = "代码生成方式" }),
            SetCommonProperties(new HbtDictType { DictName = "生成功能", DictType = "gen_function", OrderNum = 10, Status = 0,  Remark = "代码生成功能" }),
            SetCommonProperties(new HbtDictType { DictName = "树表配置", DictType = "gen_tree_config", OrderNum = 11, Status = 0,  Remark = "树表配置" }),
            SetCommonProperties(new HbtDictType { DictName = "主子表配置", DictType = "gen_sub_config", OrderNum = 12, Status = 0,  Remark = "主子表配置" }),
            SetCommonProperties(new HbtDictType { DictName = "表前缀类型", DictType = "gen_table_prefix", OrderNum = 13, Status = 0,  Remark = "数据表前缀类型字典" }),
            SetCommonProperties(new HbtDictType { DictName = "项目名称", DictType = "gen_project_name", OrderNum = 14, Status = 0,  Remark = "项目名称" }),
            SetCommonProperties(new HbtDictType { DictName = "显示类型", DictType = "gen_display_type", OrderNum = 15, Status = 0,  Remark = "字段显示类型（文本框、下拉框、日期框等）" }),
            SetCommonProperties(new HbtDictType { DictName = "查询类型", DictType = "gen_query_type", OrderNum = 16, Status = 0,  Remark = "字段查询类型（等于、模糊、范围等）" }),
            SetCommonProperties(new HbtDictType { DictName = "数据库字段类型", DictType = "gen_db_column_type", OrderNum = 17, Status = 0,  Remark = "数据库字段类型" }),
            SetCommonProperties(new HbtDictType { DictName = "C#字段类型", DictType = "gen_cs_column_type", OrderNum = 18, Status = 0,  Remark = "C#字段类型" }),
            SetCommonProperties(new HbtDictType { DictName = "编程语言类型", DictType = "gen_programming_language", OrderNum = 19, Status = 0,  Remark = "代码生成支持的编程语言类型" }),
            SetCommonProperties(new HbtDictType { DictName = "前端框架类型", DictType = "gen_frontend_framework", OrderNum = 20, Status = 0,  Remark = "代码生成支持的前端框架类型" }),
            SetCommonProperties(new HbtDictType { DictName = "后端框架类型", DictType = "gen_backend_framework", OrderNum = 21, Status = 0,  Remark = "代码生成支持的后端框架类型" }),
            SetCommonProperties(new HbtDictType { DictName = "ORM框架类型", DictType = "gen_orm_framework", OrderNum = 22, Status = 0,  Remark = "代码生成支持的ORM框架类型" })
            
        };

        foreach (var dictType in generatorDictTypes)
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
