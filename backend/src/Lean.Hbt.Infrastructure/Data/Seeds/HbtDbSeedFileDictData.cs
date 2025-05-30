//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictData.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V1.0.0
// 描述   : 文件相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 文件相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedFileDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储 </param>
    /// <param name="logger">日志记录器 </param>
    public HbtDbSeedFileDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化文件相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeFileDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var fileDictData = new List<HbtDictData>
        {
            // 文件路径
            new HbtDictData { DictType = "file_path", DictLabel = "图片目录", DictValue = "uploads/images", OrderNum = 1, Status = 0,  Remark = "图片上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "文件目录", DictValue = "uploads/files", OrderNum = 2, Status = 0,  Remark = "通用文件上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "文档目录", DictValue = "uploads/documents", OrderNum = 3, Status = 0,  Remark = "文档上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_path", DictLabel = "视频目录", DictValue = "uploads/videos", OrderNum = 4, Status = 0,  Remark = "视频上传目录", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 存储位置
            new HbtDictData { DictType = "file_storage_location", DictLabel = "默认存储位置", DictValue = "default", OrderNum = 1, Status = 0,  Remark = "默认存储位置", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_location", DictLabel = "备用存储位置1", DictValue = "backup1", OrderNum = 2, Status = 0,  Remark = "备用存储位置1", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_location", DictLabel = "备用存储位置2", DictValue = "backup2", OrderNum = 3, Status = 0,  Remark = "备用存储位置2", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },

            // 存储类型
            new HbtDictData { DictType = "file_storage_type", DictLabel = "本地存储", DictValue = "local", OrderNum = 1, Status = 0,  Remark = "本地存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "阿里云OSS", DictValue = "aliyun", OrderNum = 2, Status = 0,  Remark = "阿里云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "腾讯云COS", DictValue = "tencent", OrderNum = 3, Status = 0,  Remark = "腾讯云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_storage_type", DictLabel = "七牛云存储", DictValue = "qiniu", OrderNum = 4, Status = 0,  Remark = "七牛云对象存储", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            //文件命名规则
            new HbtDictData { DictType = "file_name_rule", DictLabel = "原文件名", DictValue = "original", OrderNum = 1, Status = 0,  Remark = "原文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_name_rule", DictLabel = "随机文件名", DictValue = "random", OrderNum = 2, Status = 0,  Remark = "随机文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now },
            new HbtDictData { DictType = "file_name_rule", DictLabel = "时间戳文件名", DictValue = "timestamp", OrderNum = 3, Status = 0,  Remark = "时间戳文件名", CreateBy = "Hbt365", CreateTime = DateTime.Now, UpdateBy = "Hbt365", UpdateTime = DateTime.Now }

        };

        foreach (var dictData in fileDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(x => x.DictType == dictData.DictType && x.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
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
                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}