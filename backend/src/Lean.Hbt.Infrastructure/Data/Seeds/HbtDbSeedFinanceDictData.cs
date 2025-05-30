//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFinanceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 财务相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedFinanceDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedFinanceDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化财务相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeFinanceDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 利润中心
            new HbtDictData
            {
                DictType = "sys_profit_center",
                DictLabel = "销售部门",
                DictValue = "SALES",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "销售部门利润中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_profit_center",
                DictLabel = "生产部门",
                DictValue = "PROD",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "生产部门利润中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_profit_center",
                DictLabel = "研发部门",
                DictValue = "R&D",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "研发部门利润中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本中心
            new HbtDictData
            {
                DictType = "sys_cost_center",
                DictLabel = "生产车间",
                DictValue = "WORKSHOP",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "生产车间成本中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_center",
                DictLabel = "管理部门",
                DictValue = "Hbt365",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "管理部门成本中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_center",
                DictLabel = "研发部门",
                DictValue = "R&D",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "研发部门成本中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 工作中心
            new HbtDictData
            {
                DictType = "sys_work_center",
                DictLabel = "机加工中心",
                DictValue = "MACHINE",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "机加工工作中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_work_center",
                DictLabel = "装配中心",
                DictValue = "ASSEMBLY",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "装配工作中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_work_center",
                DictLabel = "测试中心",
                DictValue = "TEST",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "测试工作中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 评估类
            new HbtDictData
            {
                DictType = "sys_valuation_class",
                DictLabel = "原材料",
                DictValue = "RAW",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "原材料评估类",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_valuation_class",
                DictLabel = "半成品",
                DictValue = "SEMI",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "半成品评估类",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_valuation_class",
                DictLabel = "产成品",
                DictValue = "FINISH",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "产成品评估类",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 价格控制类
            new HbtDictData
            {
                DictType = "sys_price_control",
                DictLabel = "标准价格",
                DictValue = "STD",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "标准价格控制",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_price_control",
                DictLabel = "移动平均价",
                DictValue = "MAP",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "移动平均价格控制",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_price_control",
                DictLabel = "实际成本",
                DictValue = "ACT",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "实际成本价格控制",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本核算方法
            new HbtDictData
            {
                DictType = "sys_cost_method",
                DictLabel = "标准成本法",
                DictValue = "STD",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "标准成本核算方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_method",
                DictLabel = "实际成本法",
                DictValue = "ACT",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "实际成本核算方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_method",
                DictLabel = "作业成本法",
                DictValue = "ABC",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "作业成本核算方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本要素
            new HbtDictData
            {
                DictType = "sys_cost_element",
                DictLabel = "直接材料",
                DictValue = "DM",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "直接材料成本要素",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_element",
                DictLabel = "直接人工",
                DictValue = "DL",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "直接人工成本要素",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_element",
                DictLabel = "制造费用",
                DictValue = "MOH",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "制造费用成本要素",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本对象
            new HbtDictData
            {
                DictType = "sys_cost_object",
                DictLabel = "产品",
                DictValue = "PROD",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "产品成本对象",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_object",
                DictLabel = "订单",
                DictValue = "ORDER",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "订单成本对象",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_object",
                DictLabel = "项目",
                DictValue = "PROJ",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "项目成本对象",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本分配方法
            new HbtDictData
            {
                DictType = "sys_cost_allocation",
                DictLabel = "直接分配",
                DictValue = "DIRECT",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "直接分配方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_allocation",
                DictLabel = "阶梯分配",
                DictValue = "STEP",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "阶梯分配方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_allocation",
                DictLabel = "交互分配",
                DictValue = "RECIP",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "交互分配方法",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 成本中心类型
            new HbtDictData
            {
                DictType = "sys_cost_center_type",
                DictLabel = "生产型",
                DictValue = "PROD",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "生产型成本中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_center_type",
                DictLabel = "服务型",
                DictValue = "SERV",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "服务型成本中心",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_cost_center_type",
                DictLabel = "管理型",
                DictValue = "Hbt365",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "管理型成本中心",
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
                _logger.Info($"[创建] 财务字典数据 '{dictData.DictLabel}' 创建成功");
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
                _logger.Info($"[更新] 财务字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}