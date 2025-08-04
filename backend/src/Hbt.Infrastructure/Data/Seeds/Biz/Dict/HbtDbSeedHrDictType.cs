//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 人力资源相关字典类型种子数据提供类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 人力资源相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedHrDictType
{
    /// <summary>
    /// 获取人力资源相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetHrDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "员工类型",
                DictType = "sys_employee_type",
                OrderNum = 1,
                Status = 0,
                Remark = "员工类型字典"
            },
            new HbtDictType
            {
                DictName = "员工状态",
                DictType = "sys_employee_status",
                OrderNum = 2,
                Status = 0,
                Remark = "员工状态字典"
            },
            new HbtDictType
            {
                DictName = "员工等级",
                DictType = "sys_employee_level",
                OrderNum = 3,
                Status = 0,
                Remark = "员工等级字典"
            },
            new HbtDictType
            {
                DictName = "职位类型",
                DictType = "sys_position_type",
                OrderNum = 4,
                Status = 0,
                Remark = "职位类型字典"
            },
            new HbtDictType
            {
                DictName = "职位等级",
                DictType = "sys_position_level",
                OrderNum = 5,
                Status = 0,
                Remark = "职位等级字典"
            },
            new HbtDictType
            {
                DictName = "部门类型",
                DictType = "sys_department_type",
                OrderNum = 6,
                Status = 0,
                Remark = "部门类型字典"
            },
            new HbtDictType
            {
                DictName = "考勤类型",
                DictType = "sys_attendance_type",
                OrderNum = 7,
                Status = 0,
                Remark = "考勤类型字典"
            }
        };
    }
}