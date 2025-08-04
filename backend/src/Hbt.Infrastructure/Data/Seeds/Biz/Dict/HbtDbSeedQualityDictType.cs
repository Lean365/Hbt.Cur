//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedQualityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedQualityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedQualityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedQualityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典类型种子数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 质量相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedQualityDictType
{
    /// <summary>
    /// 获取质量相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetQualityDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "检验类型",
                DictType = "sys_inspection_type",
                OrderNum = 1,
                Status = 0,

                Remark = "质量检验类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "检验状态",
                DictType = "sys_inspection_status",
                OrderNum = 2,
                Status = 0,

                Remark = "质量检验状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "检验结果",
                DictType = "sys_inspection_result",
                OrderNum = 3,
                Status = 0,

                Remark = "质量检验结果字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "不合格类型",
                DictType = "sys_defect_type",
                OrderNum = 4,
                Status = 0,

                Remark = "不合格类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "不合格等级",
                DictType = "sys_defect_level",
                OrderNum = 5,
                Status = 0,

                Remark = "不合格等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "不合格处理方式",
                DictType = "sys_defect_disposition",
                OrderNum = 6,
                Status = 0,

                Remark = "不合格处理方式字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "质量等级",
                DictType = "sys_quality_level",
                OrderNum = 7,
                Status = 0,

                Remark = "质量等级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "质量特性",
                DictType = "sys_quality_characteristic",
                OrderNum = 8,
                Status = 0,

                Remark = "质量特性字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "质量工具",
                DictType = "sys_quality_tool",
                OrderNum = 9,
                Status = 0,

                Remark = "质量工具字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "质量成本类型",
                DictType = "sys_quality_cost_type",
                OrderNum = 10,
                Status = 0,

                Remark = "质量成本类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}