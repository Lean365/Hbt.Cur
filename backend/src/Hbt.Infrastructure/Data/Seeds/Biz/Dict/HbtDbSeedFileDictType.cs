//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典类型种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典类型种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 文件相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedFileDictType
{
    /// <summary>
    /// 获取文件相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetFileDictTypes()
    {
        return new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "文件路径",
                DictType = "file_path",
                OrderNum = 1,
                Status = 1,

                Remark = "文件上传路径数据字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "存储位置",
                DictType = "file_storage_location",
                OrderNum = 2,
                Status = 1,

                Remark = "文件存储物理位置数据字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "存储类型",
                DictType = "file_storage_type",
                OrderNum = 3,
                Status = 1,

                Remark = "本地/云存储类型数据字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "文件命名",
                DictType = "file_name_rule",
                OrderNum = 4,
                Status = 1,

                Remark = "文件命名规则数据字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}