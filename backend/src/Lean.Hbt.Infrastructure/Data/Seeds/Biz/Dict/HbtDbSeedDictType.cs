//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典类型种子数据提供类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 字典类型种子数据提供类
/// </summary>
public class HbtDbSeedDictType
{
    /// <summary>
    /// 获取默认字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetDefaultDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "系统状态",
                DictType = "sys_normal_disable",
                OrderNum = 1,
                Status = 0,
                Remark = "系统状态字典"
            },
            new HbtDictType
            {
                DictName = "是否选项",
                DictType = "sys_yes_no",
                OrderNum = 2,
                Status = 0,
                Remark = "是否选项字典"
            },
            new HbtDictType
            {
                DictName = "性别类型",
                DictType = "sys_gender",
                OrderNum = 3,
                Status = 0,
                Remark = "性别类型字典"
            },
            new HbtDictType
            {
                DictName = "通知类型",
                DictType = "sys_notice_type",
                OrderNum = 4,
                Status = 0,
                Remark = "通知类型字典"
            },
            new HbtDictType
            {
                DictName = "通知状态",
                DictType = "sys_notice_status",
                OrderNum = 5,
                Status = 0,
                Remark = "通知状态字典"
            },
            new HbtDictType
            {
                DictName = "操作类型",
                DictType = "sys_oper_type",
                OrderNum = 6,
                Status = 0,
                Remark = "操作类型字典"
            },
            new HbtDictType
            {
                DictName = "用户类型",
                DictType = "sys_user_type",
                OrderNum = 7,
                Status = 0,
                Remark = "用户类型字典"
            },
            new HbtDictType
            {
                DictName = "是否默认",
                DictType = "sys_is_default",
                OrderNum = 8,
                Status = 0,
                Remark = "是否默认字典"
            },
            new HbtDictType
            {
                DictName = "数据范围",
                DictType = "sys_data_scope",
                OrderNum = 9,
                Status = 0,
                Remark = "数据范围字典"
            },
            new HbtDictType
            {
                DictName = "是否为外链",
                DictType = "sys_IsExternal",
                OrderNum = 10,
                Status = 0,
                Remark = "是否为外链字典"
            },
            new HbtDictType
            {
                DictName = "是否缓存",
                DictType = "sys_is_cache",
                OrderNum = 11,
                Status = 0,
                Remark = "是否缓存字典"
            },
            new HbtDictType
            {
                DictName = "菜单类型",
                DictType = "sys_menu_type",
                OrderNum = 12,
                Status = 0,
                Remark = "菜单类型字典"
            }
        };
    }
}