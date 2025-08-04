//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedProductionDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 生产相关字典类型种子数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;
using Hbt.Cur.Domain.IServices.Extensions;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 生产相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedProductionDictType
{
    /// <summary>
    /// 获取生产相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetProductionDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "生产订单类型",
                DictType = "prod_order_type",
                OrderNum = 1,
                Status = 0,
                
                Remark = "生产订单类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产状态",
                DictType = "prod_status",
                OrderNum = 2,
                Status = 0,
                
                Remark = "生产状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产工序类型",
                DictType = "prod_process_type",
                OrderNum = 3,
                Status = 0,
                
                Remark = "生产工序类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产资源类型",
                DictType = "prod_resource_type",
                OrderNum = 4,
                Status = 0,
                
                Remark = "生产资源类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产计划类型",
                DictType = "prod_plan_type",
                OrderNum = 5,
                Status = 0,
                
                Remark = "生产计划类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产异常类型",
                DictType = "prod_exception_type",
                OrderNum = 6,
                Status = 0,
                
                Remark = "生产异常类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产工单优先级",
                DictType = "prod_work_order_priority",
                OrderNum = 7,
                Status = 0,
                
                Remark = "生产工单优先级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "生产质量等级",
                DictType = "prod_quality_level",
                OrderNum = 8,
                Status = 0,
                
                Remark = "生产质量等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
} 