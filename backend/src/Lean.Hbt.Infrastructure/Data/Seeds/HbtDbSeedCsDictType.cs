//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

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
    public async Task<(int, int)> InitializeCustomerServiceDictTypeAsync()
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
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                Remark = "客户类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                Remark = "客户等级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "服务请求类型",
                DictType = "sys_service_request_type",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                Remark = "服务请求类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "服务请求状态",
                DictType = "sys_service_request_status",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                Remark = "服务请求状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "服务请求优先级",
                DictType = "sys_service_request_priority",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                Remark = "服务请求优先级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 项目管理相关
            new HbtDictType
            {
                DictName = "项目类型",
                DictType = "sys_project_type",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                Remark = "项目类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "项目状态",
                DictType = "sys_project_status",
                OrderNum = 7,
                Status = 0,
                TenantId = 0,
                Remark = "项目状态字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "项目优先级",
                DictType = "sys_project_priority",
                OrderNum = 8,
                Status = 0,
                TenantId = 0,
                Remark = "项目优先级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "项目风险等级",
                DictType = "sys_project_risk_level",
                OrderNum = 9,
                Status = 0,
                TenantId = 0,
                Remark = "项目风险等级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "项目里程碑类型",
                DictType = "sys_project_milestone_type",
                OrderNum = 10,
                Status = 0,
                TenantId = 0,
                Remark = "项目里程碑类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in customerServiceDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
            if (existingDictType == null)
            {
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
    public async Task<(int insertCount, int updateCount)> InitializeCsDictTypeAsync()
    {
        var dictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "客户类型",
                DictType = "sys_customer_type",
                OrderNum = 1,
                Status = 1,
                TenantId = 1,
                Remark = "客户类型字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                OrderNum = 2,
                Status = 1,
                TenantId = 1,
                Remark = "客户等级字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
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