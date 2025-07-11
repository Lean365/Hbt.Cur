//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowDictType.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 工作流相关字典类型种子数据提供类
/// </summary>
public class HbtDbSeedWorkflowDictType
{
    /// <summary>
    /// 获取工作流相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<HbtDictType> GetWorkflowDictTypes()
    {
        return new List<HbtDictType>
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
            new HbtDictType { DictName = "任务类型", DictType = "workflow_task_type", OrderNum = 7, Status = 0, Remark = "工作流任务类型字典" },
            new HbtDictType { DictName = "任务状态", DictType = "workflow_task_status", OrderNum = 8, Status = 0, Remark = "工作流任务状态字典" },
            new HbtDictType { DictName = "任务结果", DictType = "workflow_task_result", OrderNum = 9, Status = 0, Remark = "工作流任务结果字典" },
            new HbtDictType { DictName = "任务优先级", DictType = "workflow_task_priority", OrderNum = 10, Status = 0, Remark = "工作流任务优先级字典" },
            new HbtDictType { DictName = "优先级", DictType = "workflow_priority", OrderNum = 11, Status = 0, Remark = "工作流优先级字典" },
            new HbtDictType { DictName = "变量类型", DictType = "workflow_variable_type", OrderNum = 12, Status = 0, Remark = "工作流变量类型字典" },
            new HbtDictType { DictName = "指派类型", DictType = "workflow_assignee_type", OrderNum = 13, Status = 0, Remark = "工作流指派类型字典" },
            new HbtDictType { DictName = "表单类型", DictType = "workflow_form_type", OrderNum = 14, Status = 0, Remark = "工作流表单类型字典" },
            new HbtDictType { DictName = "流程版本", DictType = "workflow_version", OrderNum = 15, Status = 0, Remark = "工作流版本字典（A-Z）" }
        };
    }
} 