//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 客户服务和项目管理相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedCsDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedCsDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化客户服务和项目管理相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeCustomerServiceDictDataAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var customerServiceDictData = new List<HbtDictData>
        {
            // 客户类型
            new HbtDictData { DictType = "sys_customer_type", DictLabel = "战略客户", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "战略客户",  },
            new HbtDictData { DictType = "sys_customer_type", DictLabel = "重点客户", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "重点客户",  },
            new HbtDictData { DictType = "sys_customer_type", DictLabel = "普通客户", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "普通客户",  },
            new HbtDictData { DictType = "sys_customer_type", DictLabel = "潜在客户", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "潜在客户",  },

            // 客户等级
            new HbtDictData { DictType = "sys_customer_level", DictLabel = "A级", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "A级客户",  },
            new HbtDictData { DictType = "sys_customer_level", DictLabel = "B级", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "B级客户",  },
            new HbtDictData { DictType = "sys_customer_level", DictLabel = "C级", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "C级客户",  },
            new HbtDictData { DictType = "sys_customer_level", DictLabel = "D级", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "D级客户",  },

            // 服务请求类型
            new HbtDictData { DictType = "sys_service_request_type", DictLabel = "产品咨询", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "产品咨询",  },
            new HbtDictData { DictType = "sys_service_request_type", DictLabel = "技术支持", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "技术支持",  },
            new HbtDictData { DictType = "sys_service_request_type", DictLabel = "投诉建议", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "投诉建议",  },
            new HbtDictData { DictType = "sys_service_request_type", DictLabel = "其他服务", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "其他服务",  },

            // 服务请求状态
            new HbtDictData { DictType = "sys_service_request_status", DictLabel = "待处理", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "待处理",  },
            new HbtDictData { DictType = "sys_service_request_status", DictLabel = "处理中", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "处理中",  },
            new HbtDictData { DictType = "sys_service_request_status", DictLabel = "已完成", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "已完成",  },
            new HbtDictData { DictType = "sys_service_request_status", DictLabel = "已关闭", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "已关闭",  },

            // 服务请求优先级
            new HbtDictData { DictType = "sys_service_request_priority", DictLabel = "紧急", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "紧急",  },
            new HbtDictData { DictType = "sys_service_request_priority", DictLabel = "高", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "高优先级",  },
            new HbtDictData { DictType = "sys_service_request_priority", DictLabel = "中", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "中优先级",  },
            new HbtDictData { DictType = "sys_service_request_priority", DictLabel = "低", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "低优先级",  },

            // 项目类型
            new HbtDictData { DictType = "sys_project_type", DictLabel = "研发项目", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "研发项目",  },
            new HbtDictData { DictType = "sys_project_type", DictLabel = "实施项目", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "实施项目",  },
            new HbtDictData { DictType = "sys_project_type", DictLabel = "维护项目", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "维护项目",  },
            new HbtDictData { DictType = "sys_project_type", DictLabel = "咨询项目", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "咨询项目",  },

            // 项目状态
            new HbtDictData { DictType = "sys_project_status", DictLabel = "规划中", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "规划中",  },
            new HbtDictData { DictType = "sys_project_status", DictLabel = "进行中", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "进行中",  },
            new HbtDictData { DictType = "sys_project_status", DictLabel = "已完成", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "已完成",  },
            new HbtDictData { DictType = "sys_project_status", DictLabel = "已暂停", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "已暂停",  },
            new HbtDictData { DictType = "sys_project_status", DictLabel = "已取消", DictValue = "5", OrderNum = 5, Status = 0,  Remark = "已取消",  },

            // 项目优先级
            new HbtDictData { DictType = "sys_project_priority", DictLabel = "紧急", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "紧急",  },
            new HbtDictData { DictType = "sys_project_priority", DictLabel = "高", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "高优先级",  },
            new HbtDictData { DictType = "sys_project_priority", DictLabel = "中", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "中优先级",  },
            new HbtDictData { DictType = "sys_project_priority", DictLabel = "低", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "低优先级",  },

            // 项目风险等级
            new HbtDictData { DictType = "sys_project_risk_level", DictLabel = "高风险", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "高风险",  },
            new HbtDictData { DictType = "sys_project_risk_level", DictLabel = "中风险", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "中风险",  },
            new HbtDictData { DictType = "sys_project_risk_level", DictLabel = "低风险", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "低风险",  },
            new HbtDictData { DictType = "sys_project_risk_level", DictLabel = "无风险", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "无风险",  },

            // 项目里程碑类型
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "项目启动", DictValue = "1", OrderNum = 1, Status = 0,  Remark = "项目启动",  },
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "需求确认", DictValue = "2", OrderNum = 2, Status = 0,  Remark = "需求确认",  },
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "设计完成", DictValue = "3", OrderNum = 3, Status = 0,  Remark = "设计完成",  },
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "开发完成", DictValue = "4", OrderNum = 4, Status = 0,  Remark = "开发完成",  },
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "测试完成", DictValue = "5", OrderNum = 5, Status = 0,  Remark = "测试完成",  },
            new HbtDictData { DictType = "sys_project_milestone_type", DictLabel = "项目验收", DictValue = "6", OrderNum = 6, Status = 0,  Remark = "项目验收",  }
        };

        foreach (var dictData in customerServiceDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                // 统一处理审计字段和租户
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
                dictData.TenantId = tenantId;
                
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.Status = dictData.Status;
                existingDictData.Remark = dictData.Remark;
                existingDictData.UpdateBy = dictData.UpdateBy;
                existingDictData.UpdateTime = dictData.UpdateTime;
                existingDictData.TenantId = tenantId;
                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化客户服务字典数据
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeCsDictDataAsync(long tenantId)
    {
        var dictDataList = new List<HbtDictData>
        {
            new HbtDictData
            {
                DictType = "sys_customer_type",
                DictLabel = "战略客户",
                DictValue = "1",
                OrderNum = 1,
                Status = 1,
                TenantId = 1,
                Remark = "战略客户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_customer_type",
                DictLabel = "重点客户",
                DictValue = "2",
                OrderNum = 2,
                Status = 1,
                TenantId = 1,
                Remark = "重点客户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        int insertCount = 0;
        int updateCount = 0;

        foreach (var dictData in dictDataList)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                dictData.TenantId = tenantId;
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.Status = dictData.Status;
                existingDictData.Remark = dictData.Remark;
                existingDictData.UpdateBy = dictData.UpdateBy;
                existingDictData.UpdateTime = dictData.UpdateTime;
                existingDictData.TenantId = tenantId;
                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
} 