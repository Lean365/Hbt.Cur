//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGeneratorDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成器字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成器字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedGeneratorDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedGeneratorDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 设置字典数据的公共属性
    /// </summary>
    private HbtDictData SetCommonProperties(HbtDictData dictData)
    {
        dictData.CreateBy = "system";
        dictData.CreateTime = DateTime.Now;
        dictData.UpdateBy = "Hbt365";
        dictData.UpdateTime = DateTime.Now;
        return dictData;
    }

    /// <summary>
    /// 初始化代码生成器字典数据
    /// </summary>
    public async Task<(int, int)> InitializeGeneratorDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var generatorDictData = new List<HbtDictData>
        {
            // 数据库类型
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "MySQL", DictValue = "0", OrderNum = 1, Status = 0, CssClass = 1, ListClass = 1, Remark = "MySQL Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "SqlServer", DictValue = "1", OrderNum = 2, Status = 0, CssClass = 2, ListClass = 2, Remark = "Microsoft SQL Server" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "SQLite", DictValue = "2", OrderNum = 3, Status = 0, CssClass = 3, ListClass = 3, Remark = "SQLite Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Oracle", DictValue = "3", OrderNum = 4, Status = 0, CssClass = 4, ListClass = 4, Remark = "Oracle Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "PostgreSQL", DictValue = "4", OrderNum = 5, Status = 0, CssClass = 5, ListClass = 5, Remark = "PostgreSQL Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "达梦", DictValue = "5", OrderNum = 6, Status = 0, CssClass = 6, ListClass = 6, Remark = "DM Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Kdbndp", DictValue = "6", OrderNum = 7, Status = 0, CssClass = 7, ListClass = 7, Remark = "Kdbndp Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Oscar", DictValue = "7", OrderNum = 8, Status = 0, CssClass = 8, ListClass = 8, Remark = "Oscar Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "MySQL Connector", DictValue = "8", OrderNum = 9, Status = 0, CssClass = 9, ListClass = 9, Remark = "MySQL Connector" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Access", DictValue = "9", OrderNum = 10, Status = 0, CssClass = 10, ListClass = 10, Remark = "Microsoft Access" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "OpenGauss", DictValue = "10", OrderNum = 11, Status = 0, CssClass = 11, ListClass = 11, Remark = "OpenGauss Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "QuestDB", DictValue = "11", OrderNum = 12, Status = 0, CssClass = 12, ListClass = 12, Remark = "QuestDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "HG", DictValue = "12", OrderNum = 13, Status = 0, CssClass = 13, ListClass = 13, Remark = "HG Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "ClickHouse", DictValue = "13", OrderNum = 14, Status = 0, CssClass = 14, ListClass = 14, Remark = "ClickHouse Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "GBase", DictValue = "14", OrderNum = 15, Status = 0, CssClass = 15, ListClass = 15, Remark = "GBase Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "ODBC", DictValue = "15", OrderNum = 16, Status = 0, CssClass = 16, ListClass = 16, Remark = "ODBC Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "OceanBase For Oracle", DictValue = "16", OrderNum = 17, Status = 0, CssClass = 17, ListClass = 17, Remark = "OceanBase For Oracle" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "TDengine", DictValue = "17", OrderNum = 18, Status = 0, CssClass = 18, ListClass = 18, Remark = "TDengine Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "GaussDB", DictValue = "18", OrderNum = 19, Status = 0, CssClass = 19, ListClass = 19, Remark = "GaussDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "OceanBase", DictValue = "19", OrderNum = 20, Status = 0, CssClass = 20, ListClass = 20, Remark = "OceanBase Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "TiDB", DictValue = "20", OrderNum = 21, Status = 0, CssClass = 21, ListClass = 21, Remark = "TiDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Vastbase", DictValue = "21", OrderNum = 22, Status = 0, CssClass = 22, ListClass = 22, Remark = "Vastbase Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "PolarDB", DictValue = "22", OrderNum = 23, Status = 0, CssClass = 23, ListClass = 23, Remark = "PolarDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Doris", DictValue = "23", OrderNum = 24, Status = 0, CssClass = 24, ListClass = 24, Remark = "Doris Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Xugu", DictValue = "24", OrderNum = 25, Status = 0, CssClass = 25, ListClass = 25, Remark = "Xugu Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "GoldenDB", DictValue = "25", OrderNum = 26, Status = 0, CssClass = 26, ListClass = 26, Remark = "GoldenDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "TDSQL For PG ODBC", DictValue = "26", OrderNum = 27, Status = 0, CssClass = 27, ListClass = 27, Remark = "TDSQL For PG ODBC" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "TDSQL", DictValue = "27", OrderNum = 28, Status = 0, CssClass = 28, ListClass = 28, Remark = "TDSQL Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "HANA", DictValue = "28", OrderNum = 29, Status = 0, CssClass = 29, ListClass = 29, Remark = "SAP HANA Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "DB2", DictValue = "29", OrderNum = 30, Status = 0, CssClass = 30, ListClass = 30, Remark = "IBM DB2 Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "GaussDB Native", DictValue = "30", OrderNum = 31, Status = 0, CssClass = 31, ListClass = 31, Remark = "GaussDB Native" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "DuckDB", DictValue = "31", OrderNum = 32, Status = 0, CssClass = 32, ListClass = 32, Remark = "DuckDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "MongoDB", DictValue = "32", OrderNum = 33, Status = 0, CssClass = 33, ListClass = 33, Remark = "MongoDB Database" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_db_type", DictLabel = "Custom", DictValue = "900", OrderNum = 34, Status = 0, CssClass = 34, ListClass = 34, Remark = "Custom Database" }),

            // 模板类型
            SetCommonProperties(new HbtDictData { DictType = "gen_tpl_type", DictLabel = "Scriban", DictValue = "0", OrderNum = 1, Status = 0, CssClass = 1, ListClass = 1, Remark = "使用wwwroot/Generator/*.scriban模板" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_tpl_type", DictLabel = "数据库", DictValue = "1", OrderNum = 2, Status = 0, CssClass = 2, ListClass = 2, Remark = "使用HbtGenTemplate数据表中的模板" }),

            // 生成模板
            SetCommonProperties(new HbtDictData { DictType = "gen_template_type", DictLabel = "单表", DictValue = "crud", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "单表（增删改查）" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_template_type", DictLabel = "树表", DictValue = "tree", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "树表（增删改查）" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_template_type", DictLabel = "主子表", DictValue = "sub", OrderNum = 3, Status = 0,  CssClass = 3, ListClass = 3, Remark = "主子表（增删改查）" }),

            // 前端模板
            SetCommonProperties(new HbtDictData { DictType = "gen_frontend_type", DictLabel = "Element Plus", DictValue = "1", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "Element Plus前端模板" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_frontend_type", DictLabel = "Ant Design Vue", DictValue = "2", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "Ant Design Vue前端模板" }),

            // 生成模块
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Audit日志审计", DictValue = "Audit", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "Audit日志审计模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Core系统功能", DictValue = "Core", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "Core系统功能模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Generator代码生成", DictValue = "Generator", OrderNum = 3, Status = 0,  CssClass = 3, ListClass = 3, Remark = "Generator代码生成模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Identity身份认证", DictValue = "Identity", OrderNum = 4, Status = 0,  CssClass = 4, ListClass = 4, Remark = "Identity身份认证模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Routine日常事务", DictValue = "Routine", OrderNum = 5, Status = 0,  CssClass = 5, ListClass = 5, Remark = "Routine日常事务模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "SignalR在线管理", DictValue = "SignalR", OrderNum = 6, Status = 0,  CssClass = 6, ListClass = 6, Remark = "SignalR在线管理模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Workflow工作流", DictValue = "Workflow", OrderNum = 7, Status = 0,  CssClass = 7, ListClass = 7, Remark = "Workflow工作流模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Finance财务管理", DictValue = "Finance", OrderNum = 8, Status = 0,  CssClass = 8, ListClass = 8, Remark = "Finance财务管理模块" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_module_name", DictLabel = "Logistics后勤管理", DictValue = "Logistics", OrderNum = 9, Status = 0,  CssClass = 9, ListClass = 9, Remark = "Logistics后勤管理模块" }),

            // 前端布局
            SetCommonProperties(new HbtDictData { DictType = "gen_frontend_style", DictLabel = "一行一列", DictValue = "12", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "一行一列布局" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_frontend_style", DictLabel = "一行两列", DictValue = "24", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "一行两列布局" }),

            // 按钮样式
            SetCommonProperties(new HbtDictData { DictType = "gen_button_style", DictLabel = "默认样式", DictValue = "1", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "默认按钮样式" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_button_style", DictLabel = "自定义样式", DictValue = "2", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "自定义按钮样式" }),

            // 生成方式
            SetCommonProperties(new HbtDictData { DictType = "gen_type", DictLabel = "自定义路径", DictValue = "0", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "自定义生成路径" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_type", DictLabel = "下载压缩包", DictValue = "1", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "下载压缩包" }),

            // 生成功能
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "新增", DictValue = "add", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "新增功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "修改", DictValue = "update", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "修改功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "删除", DictValue = "delete", OrderNum = 3, Status = 0,  CssClass = 3, ListClass = 3, Remark = "删除功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "批量删除", DictValue = "batchDelete", OrderNum = 4, Status = 0,  CssClass = 4, ListClass = 4, Remark = "批量删除功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "导入", DictValue = "import", OrderNum = 5, Status = 0,  CssClass = 5, ListClass = 5, Remark = "导入功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "模板", DictValue = "template", OrderNum = 6, Status = 0,  CssClass = 6, ListClass = 6, Remark = "模板功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "导出", DictValue = "export", OrderNum = 7, Status = 0,  CssClass = 7, ListClass = 7, Remark = "导出功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "详情", DictValue = "detail", OrderNum = 8, Status = 0,  CssClass = 8, ListClass = 8, Remark = "详情功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "预览", DictValue = "preview", OrderNum = 9, Status = 0,  CssClass = 9, ListClass = 9, Remark = "预览功能" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_function", DictLabel = "打印", DictValue = "print", OrderNum = 10, Status = 0,  CssClass = 10, ListClass = 10, Remark = "打印功能" }),

            // 树表配置
            SetCommonProperties(new HbtDictData { DictType = "gen_tree_config", DictLabel = "树编码字段", DictValue = "treeCode", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "树编码字段" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_tree_config", DictLabel = "树父编码字段", DictValue = "treeParentCode", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "树父编码字段" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_tree_config", DictLabel = "树名称字段", DictValue = "treeName", OrderNum = 3, Status = 0,  CssClass = 3, ListClass = 3, Remark = "树名称字段" }),

            // 主子表配置
            SetCommonProperties(new HbtDictData { DictType = "gen_sub_config", DictLabel = "主表名称", DictValue = "mainTable", OrderNum = 1, Status = 0,  CssClass = 1, ListClass = 1, Remark = "主表名称" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_sub_config", DictLabel = "子表名称", DictValue = "subTable", OrderNum = 2, Status = 0,  CssClass = 2, ListClass = 2, Remark = "子表名称" }),
            SetCommonProperties(new HbtDictData { DictType = "gen_sub_config", DictLabel = "主表外键", DictValue = "mainTableFk", OrderNum = 3, Status = 0,  CssClass = 3, ListClass = 3, Remark = "主表外键" })
        };

        foreach (var dictData in generatorDictData)
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
                // 更新所有字段
                existingDictData.DictType = dictData.DictType;
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.Status = dictData.Status;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
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
