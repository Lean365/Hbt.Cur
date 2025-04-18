//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedMaterialDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 物料相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 物料相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedMaterialDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedMaterialDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化物料相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeMaterialDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 物料类型
            new HbtDictData
            {
                DictType = "sys_material_type",
                DictLabel = "原材料",
                DictValue = "RAW",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "原材料物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_type",
                DictLabel = "半成品",
                DictValue = "SEMI",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "半成品物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_type",
                DictLabel = "成品",
                DictValue = "FINISHED",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "成品物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_type",
                DictLabel = "贸易商品",
                DictValue = "TRADE",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "贸易商品物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_type",
                DictLabel = "服务",
                DictValue = "SERVICE",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "服务物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料组
            new HbtDictData
            {
                DictType = "sys_material_group",
                DictLabel = "金属材料",
                DictValue = "METAL",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "金属材料组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_group",
                DictLabel = "化工材料",
                DictValue = "CHEMICAL",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "化工材料组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_group",
                DictLabel = "电子元件",
                DictValue = "ELECTRONIC",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "电子元件组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_group",
                DictLabel = "包装材料",
                DictValue = "PACKAGING",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "包装材料组",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料分类
            new HbtDictData
            {
                DictType = "sys_material_category",
                DictLabel = "标准件",
                DictValue = "STANDARD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "标准件分类",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_category",
                DictLabel = "定制件",
                DictValue = "CUSTOM",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "定制件分类",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_category",
                DictLabel = "外购件",
                DictValue = "PURCHASED",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "外购件分类",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_category",
                DictLabel = "自制件",
                DictValue = "MANUFACTURED",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "自制件分类",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料状态
            new HbtDictData
            {
                DictType = "sys_material_status",
                DictLabel = "开发中",
                DictValue = "DEVELOPING",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "物料开发中",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_status",
                DictLabel = "已发布",
                DictValue = "RELEASED",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "物料已发布",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_status",
                DictLabel = "已冻结",
                DictValue = "FROZEN",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "物料已冻结",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_status",
                DictLabel = "已废弃",
                DictValue = "OBSOLETE",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "物料已废弃",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料来源
            new HbtDictData
            {
                DictType = "sys_material_source",
                DictLabel = "自制",
                DictValue = "SELF",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "自制物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_source",
                DictLabel = "外购",
                DictValue = "PURCHASE",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "外购物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_source",
                DictLabel = "委外",
                DictValue = "OUTSOURCE",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "委外物料",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料计价方式
            new HbtDictData
            {
                DictType = "sys_material_valuation",
                DictLabel = "标准成本",
                DictValue = "STANDARD",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "标准成本计价",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_valuation",
                DictLabel = "移动平均",
                DictValue = "MOVING_AVG",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "移动平均计价",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_valuation",
                DictLabel = "先进先出",
                DictValue = "FIFO",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "先进先出计价",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_valuation",
                DictLabel = "后进先出",
                DictValue = "LIFO",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "后进先出计价",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料批次管理
            new HbtDictData
            {
                DictType = "sys_material_batch",
                DictLabel = "批次管理",
                DictValue = "YES",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "启用批次管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_batch",
                DictLabel = "非批次管理",
                DictValue = "NO",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "不启用批次管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料序列号管理
            new HbtDictData
            {
                DictType = "sys_material_serial",
                DictLabel = "序列号管理",
                DictValue = "YES",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "启用序列号管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_serial",
                DictLabel = "非序列号管理",
                DictValue = "NO",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "不启用序列号管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料库存管理
            new HbtDictData
            {
                DictType = "sys_material_stock",
                DictLabel = "库存管理",
                DictValue = "YES",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "启用库存管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_stock",
                DictLabel = "非库存管理",
                DictValue = "NO",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "不启用库存管理",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 物料成本核算
            new HbtDictData
            {
                DictType = "sys_material_cost",
                DictLabel = "成本核算",
                DictValue = "YES",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "启用成本核算",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_material_cost",
                DictLabel = "非成本核算",
                DictValue = "NO",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "不启用成本核算",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 物料字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                existingDictData.TenantId = dictData.TenantId;
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "system";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 物料字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 