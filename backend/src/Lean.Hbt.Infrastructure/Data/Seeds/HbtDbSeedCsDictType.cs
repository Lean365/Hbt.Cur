//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 客户服务和项目管理相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedCsDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedCsDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化客户服务和项目管理相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeCustomerServiceDictTypeAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var customerServiceDictTypes = new List<HbtDictType>
        {
            // 客户服务相关
            new HbtDictType
            {
                DictName = "客户类型",
                DictType = "sys_customer_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 1,
                Status = 0,
                Remark = "客户类型字典",

            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 2,
                Status = 0,
                Remark = "客户等级字典",

            },
            new HbtDictType
            {
                DictName = "服务请求类型",
                DictType = "sys_service_request_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 3,
                Status = 0,
                Remark = "服务请求类型字典",

            },
            new HbtDictType
            {
                DictName = "服务请求状态",
                DictType = "sys_service_request_status",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 4,
                Status = 0,
                Remark = "服务请求状态字典",

            },
            new HbtDictType
            {
                DictName = "服务请求优先级",
                DictType = "sys_service_request_priority",
                                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 5,
                Status = 0,

                Remark = "服务请求优先级字典",

            },

            // 项目管理相关
            new HbtDictType
            {
                DictName = "项目类型",
                DictType = "sys_project_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 6,
                Status = 0,
                Remark = "项目类型字典",

            },
            new HbtDictType
            {
                DictName = "项目状态",
                DictType = "sys_project_status",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 7,
                Status = 0,
                Remark = "项目状态字典",

            },
            new HbtDictType
            {
                DictName = "项目优先级",
                DictType = "sys_project_priority",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 8,
                Status = 0,
                Remark = "项目优先级字典",

            },
            new HbtDictType
            {
                DictName = "项目风险等级",
                DictType = "sys_project_risk_level",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 9,
                Status = 0,
                Remark = "项目风险等级字典",

            },
            new HbtDictType
            {
                DictName = "项目里程碑类型",
                DictType = "sys_project_milestone_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 10,
                Status = 0,
                Remark = "项目里程碑类型字典",

            }
        };

        foreach (var dictType in customerServiceDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                // 统一处理审计字段和租户
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

    /// <summary>
    /// 初始化客户服务字典类型数据
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeCsDictTypeAsync(long tenantId)
    {
        var dictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "客户类型",
                DictType = "sys_customer_type",
                OrderNum = 1,
                Status = 1,

                Remark = "客户类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                OrderNum = 2,
                Status = 1,

                Remark = "客户等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        int insertCount = 0;
        int updateCount = 0;

        foreach (var dictType in dictTypes)
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
                existingDictType.CreateBy = "Hbt365";
                existingDictType.CreateTime = DateTime.Now;
                existingDictType.UpdateBy = "Hbt365";
                existingDictType.UpdateTime = DateTime.Now;
                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}