//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化字典数据
    /// </summary>
    public async Task<(int, int)> InitializeDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 系统状态
            new HbtDictData
            {
                DictType = "sys_status",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "success",
                ListClass = "success",
                Remark = "正常状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_status",
                DictLabel = "停用",
                DictValue = "1",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "danger",
                ListClass = "danger",
                Remark = "停用状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否选项
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "primary",
                ListClass = "primary",
                Remark = "是",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "info",
                ListClass = "info",
                Remark = "否",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 性别类型
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "男",
                DictValue = "1",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "primary",
                ListClass = "primary",
                Remark = "男性",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "女",
                DictValue = "2",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "danger",
                ListClass = "danger",
                Remark = "女性",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "未知",
                DictValue = "0",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "info",
                ListClass = "info",
                Remark = "未知性别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 通知类型
            new HbtDictData
            {
                DictType = "sys_notice_type",
                DictLabel = "通知",
                DictValue = "1",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "warning",
                ListClass = "warning",
                Remark = "通知类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_type",
                DictLabel = "公告",
                DictValue = "2",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "success",
                ListClass = "success",
                Remark = "公告类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 通知状态
            new HbtDictData
            {
                DictType = "sys_notice_status",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "success",
                ListClass = "success",
                Remark = "正常状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_status",
                DictLabel = "关闭",
                DictValue = "1",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "danger",
                ListClass = "danger",
                Remark = "关闭状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 操作类型
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "其他",
                DictValue = "0",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "info",
                ListClass = "info",
                Remark = "其他操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "查询",
                DictValue = "1",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "primary",
                ListClass = "primary",
                Remark = "查询操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "新增",
                DictValue = "2",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "success",
                ListClass = "success",
                Remark = "新增操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "修改",
                DictValue = "3",
                OrderNum = 4,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "warning",
                ListClass = "warning",
                Remark = "修改操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "删除",
                DictValue = "4",
                OrderNum = 5,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "danger",
                ListClass = "danger",
                Remark = "删除操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导出",
                DictValue = "5",
                OrderNum = 6,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "warning",
                ListClass = "warning",
                Remark = "导出操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导入",
                DictValue = "6",
                OrderNum = 7,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "warning",
                ListClass = "warning",
                Remark = "导入操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 日志级别
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "调试",
                DictValue = "1",
                OrderNum = 1,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "info",
                ListClass = "info",
                Remark = "调试级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "信息",
                DictValue = "2",
                OrderNum = 2,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "primary",
                ListClass = "primary",
                Remark = "信息级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "警告",
                DictValue = "3",
                OrderNum = 3,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "warning",
                ListClass = "warning",
                Remark = "警告级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "错误",
                DictValue = "4",
                OrderNum = 4,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "danger",
                ListClass = "danger",
                Remark = "错误级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "致命",
                DictValue = "5",
                OrderNum = 5,
                Status = HbtStatus.Normal,
                TenantId = 0,
                CssClass = "dark",
                ListClass = "dark",
                Remark = "致命级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.FirstOrDefaultAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.InsertAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                // 更新所有字段
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
                _logger.Info($"[更新] 字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 