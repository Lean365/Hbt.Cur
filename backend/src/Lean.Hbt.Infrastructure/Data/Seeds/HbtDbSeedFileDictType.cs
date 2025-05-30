//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedFileDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-04-28
// 版本号 : V1.0.0
// 描述   : 文件相关字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 文件相关字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedFileDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedFileDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化文件相关字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeFileDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var fileDictTypes = new List<HbtDictType>
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

        foreach (var dictType in fileDictTypes)
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
                existingDictType.UpdateBy = dictType.UpdateBy;
                existingDictType.UpdateTime = dictType.UpdateTime;
                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}