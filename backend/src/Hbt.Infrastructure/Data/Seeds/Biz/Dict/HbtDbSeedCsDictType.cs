//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 客户服务和项目管理相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedCsDictType
{
    /// <summary>
    /// 获取客户服务和项目管理相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetCustomerServiceDictTypes()
    {
        return new List<HbtDictType>
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
                Remark = "客户类型字典"
            },
            new HbtDictType
            {
                DictName = "客户等级",
                DictType = "sys_customer_level",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 2,
                Status = 0,
                Remark = "客户等级字典"
            },
            new HbtDictType
            {
                DictName = "服务请求类型",
                DictType = "sys_service_request_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 3,
                Status = 0,
                Remark = "服务请求类型字典"
            },
            new HbtDictType
            {
                DictName = "服务请求状态",
                DictType = "sys_service_request_status",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 4,
                Status = 0,
                Remark = "服务请求状态字典"
            },
            new HbtDictType
            {
                DictName = "服务请求优先级",
                DictType = "sys_service_request_priority",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 5,
                Status = 0,
                Remark = "服务请求优先级字典"
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
                Remark = "项目类型字典"
            },
            new HbtDictType
            {
                DictName = "项目状态",
                DictType = "sys_project_status",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 7,
                Status = 0,
                Remark = "项目状态字典"
            },
            new HbtDictType
            {
                DictName = "项目优先级",
                DictType = "sys_project_priority",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 8,
                Status = 0,
                Remark = "项目优先级字典"
            },
            new HbtDictType
            {
                DictName = "项目风险等级",
                DictType = "sys_project_risk_level",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 9,
                Status = 0,
                Remark = "项目风险等级字典"
            },
            new HbtDictType
            {
                DictName = "项目里程碑类型",
                DictType = "sys_project_milestone_type",
                DictCategory=0,
                IsBuiltin=0,
                OrderNum = 10,
                Status = 0,
                Remark = "项目里程碑类型字典"
            }
        };
    }
}