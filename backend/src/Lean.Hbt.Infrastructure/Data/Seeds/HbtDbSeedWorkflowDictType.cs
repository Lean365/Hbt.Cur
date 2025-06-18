//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedWorkflowDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedWorkflowDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeWorkflowDictTypeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType { DictName = "表单分类", DictType = "workflow_form_category", OrderNum = 1, Status = 0, Remark = "工作流表单分类字典" },
            new HbtDictType { DictName = "表单版本", DictType = "workflow_form_version", OrderNum = 2, Status = 0, Remark = "工作流表单版本字典" },
            new HbtDictType { DictName = "表单状态", DictType = "workflow_form_status", OrderNum = 3, Status = 0, Remark = "工作流表单状态字典" },
            new HbtDictType { DictName = "活动类型", DictType = "workflow_activity_type", OrderNum = 1, Status = 0, Remark = "工作流活动类型字典" },
            new HbtDictType { DictName = "流程分类", DictType = "workflow_category", OrderNum = 2, Status = 0, Remark = "工作流分类字典" },
            new HbtDictType { DictName = "流程状态", DictType = "workflow_status", OrderNum = 3, Status = 0, Remark = "工作流状态字典" },
            new HbtDictType { DictName = "实例状态", DictType = "workflow_instance_status", OrderNum = 4, Status = 0, Remark = "工作流实例状态字典" },
            new HbtDictType { DictName = "节点类型", DictType = "workflow_node_type", OrderNum = 5, Status = 0, Remark = "工作流节点类型字典" },
            new HbtDictType { DictName = "节点状态", DictType = "workflow_node_status", OrderNum = 6, Status = 0, Remark = "工作流节点状态字典" },
            new HbtDictType { DictName = "任务类型", DictType = "workflow_task_type", OrderNum = 6, Status = 0, Remark = "工作流任务类型字典" },
            new HbtDictType { DictName = "任务状态", DictType = "workflow_task_status", OrderNum = 7, Status = 0, Remark = "工作流任务状态字典" },
            new HbtDictType { DictName = "优先级", DictType = "workflow_priority", OrderNum = 8, Status = 0, Remark = "工作流优先级字典" },
            new HbtDictType { DictName = "变量类型", DictType = "workflow_variable_type", OrderNum = 9, Status = 0, Remark = "工作流变量类型字典" },
            new HbtDictType { DictName = "指派类型", DictType = "workflow_assignee_type", OrderNum = 10, Status = 0, Remark = "工作流指派类型字典" },
            new HbtDictType { DictName = "表单类型", DictType = "workflow_form_type", OrderNum = 11, Status = 0, Remark = "工作流表单类型字典" },
            new HbtDictType { DictName = "流程版本", DictType = "workflow_version", OrderNum = 12, Status = 0, Remark = "工作流版本字典（A-Z）" }
        };

        foreach (var dictType in defaultDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(d => d.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                dictType.CreateBy = "Hbt365";
                dictType.CreateTime = DateTime.Now;
                dictType.UpdateBy = "Hbt365";
                dictType.UpdateTime = DateTime.Now;
                await _dictTypeRepository.CreateAsync(dictType);
                insertCount++;
                _logger.Info($"已添加工作流字典类型: {dictType.DictName}");
            }
            else
            {
                existingDictType.DictName = dictType.DictName;
                existingDictType.OrderNum = dictType.OrderNum;
                existingDictType.Status = dictType.Status;
                existingDictType.Remark = dictType.Remark;
                existingDictType.UpdateBy = "Hbt365";
                existingDictType.UpdateTime = DateTime.Now;
                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
                _logger.Info($"已更新工作流字典类型: {dictType.DictName}");
            }
        }

        return (insertCount, updateCount);
    }
} 