//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictData.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典数据种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 文件相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedFileDictData
{
    /// <summary>
    /// 获取文件相关字典数据
    /// </summary>
    /// <returns>文件相关字典数据列表</returns>
    public List<HbtDictData> GetFileDictData()
    {
        return new List<HbtDictData>
        {
            // 文件路径
            new HbtDictData { DictType = "file_path", DictLabel = "图片目录", DictValue = "uploads/images",CssClass=1,ListClass=1, OrderNum = 1, Status = 0,  Remark = "图片上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "文件目录", DictValue = "uploads/files",CssClass=2,ListClass=2, OrderNum = 2, Status = 0,  Remark = "通用文件上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "文档目录", DictValue = "uploads/documents",CssClass=3,ListClass=3, OrderNum = 3, Status = 0,  Remark = "文档上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "视频目录", DictValue = "uploads/videos",CssClass=4,ListClass=4, OrderNum = 4, Status = 0,  Remark = "视频上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 存储位置
            new HbtDictData { DictType = "file_storage_location", DictLabel = "默认存储位置", DictValue = "default",CssClass=1,ListClass=1, OrderNum = 1, Status = 0,  Remark = "默认存储位置", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_location", DictLabel = "备用存储位置1", DictValue = "backup1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0,  Remark = "备用存储位置1", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_location", DictLabel = "备用存储位置2", DictValue = "backup2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0,  Remark = "备用存储位置2", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 存储类型
            new HbtDictData { DictType = "file_storage_type", DictLabel = "本地存储", DictValue = "local",CssClass=1,ListClass=1, OrderNum = 1, Status = 0,  Remark = "本地存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "阿里云OSS", DictValue = "aliyun",CssClass=2,ListClass=2, OrderNum = 2, Status = 0,  Remark = "阿里云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "腾讯云COS", DictValue = "tencent",CssClass=3,ListClass=3, OrderNum = 3, Status = 0,  Remark = "腾讯云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "七牛云存储", DictValue = "qiniu",CssClass=4,ListClass=4, OrderNum = 4, Status = 0,  Remark = "七牛云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            //文件命名规则
            new HbtDictData { DictType = "file_name_rule", DictLabel = "原文件名", DictValue = "original",CssClass=1,ListClass=1, OrderNum = 1, Status = 0,  Remark = "原文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_name_rule", DictLabel = "随机文件名", DictValue = "random",CssClass=2,ListClass=2, OrderNum = 2, Status = 0,  Remark = "随机文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_name_rule", DictLabel = "时间戳文件名", DictValue = "timestamp",CssClass=3,ListClass=3, OrderNum = 3, Status = 0,  Remark = "时间戳文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }
        };
    }
}