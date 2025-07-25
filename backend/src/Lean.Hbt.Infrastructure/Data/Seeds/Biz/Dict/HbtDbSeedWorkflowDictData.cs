//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowDictData.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 工作流字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 工作流字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedWorkflowDictData
{
    /// <summary>
    /// 获取工作流相关字典数据
    /// </summary>
    /// <returns>工作流相关字典数据列表</returns>
    public List<HbtDictData> GetWorkflowDictData()
    {
        return new List<HbtDictData>
        {
            // 表单类型 (workflow_form_type)
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "请假申请", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "员工请假申请表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "报销申请", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "费用报销申请表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "采购申请", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "采购申请表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "合同审批", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "合同审批表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "其他类型表单" },

            // 表单分类 (workflow_form_category)
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "日常事务", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "日常事务相关表单" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "会计核算", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "财务相关表单" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "后勤管理", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "采购相关表单" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "人力资源", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "合同相关表单" }, 
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "其他分类表单" },

            // 表单状态 (workflow_form_status)
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "表单草稿状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "已发布", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "表单已发布状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "已停用", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "表单已停用状态" },

            // 流程分类 (workflow_scheme_category)
            new HbtDictData { DictType = "workflow_scheme_category", DictLabel = "人事流程", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "人事相关流程" },
            new HbtDictData { DictType = "workflow_scheme_category", DictLabel = "财务流程", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "财务相关流程" },
            new HbtDictData { DictType = "workflow_scheme_category", DictLabel = "采购流程", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "采购相关流程" },
            new HbtDictData { DictType = "workflow_scheme_category", DictLabel = "合同流程", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "合同相关流程" },
            new HbtDictData { DictType = "workflow_scheme_category", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "其他分类流程" },

            // 流程定义状态 (workflow_scheme_status)
            new HbtDictData { DictType = "workflow_scheme_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "流程定义草稿状态" },
            new HbtDictData { DictType = "workflow_scheme_status", DictLabel = "已发布", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "流程定义已发布状态" },
            new HbtDictData { DictType = "workflow_scheme_status", DictLabel = "已停用", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "流程定义已停用状态" },

            // 流程实例状态 (workflow_instance_status)
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "流程实例草稿状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "运行中", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "流程实例运行中状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已完成", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "流程实例已完成状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已终止", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "流程实例已终止状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已暂停", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "流程实例已暂停状态" },

            // 优先级 (workflow_instance_priority)
            new HbtDictData { DictType = "workflow_instance_priority", DictLabel = "低", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "低优先级" },
            new HbtDictData { DictType = "workflow_instance_priority", DictLabel = "普通", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "普通优先级" },
            new HbtDictData { DictType = "workflow_instance_priority", DictLabel = "高", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "高优先级" },
            new HbtDictData { DictType = "workflow_instance_priority", DictLabel = "紧急", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "紧急优先级" },
            new HbtDictData { DictType = "workflow_instance_priority", DictLabel = "特急", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "特急优先级" },

            // 紧急程度 (workflow_instance_urgency)
            new HbtDictData { DictType = "workflow_instance_urgency", DictLabel = "普通", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "普通紧急程度" },
            new HbtDictData { DictType = "workflow_instance_urgency", DictLabel = "加急", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "加急紧急程度" },
            new HbtDictData { DictType = "workflow_instance_urgency", DictLabel = "特急", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "特急紧急程度" },

            // 实体操作类型 (workflow_instance_oper_type)
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "提交", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "提交操作" },
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "审批", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "审批操作" },
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "驳回", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "驳回操作" },
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "转办", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "转办操作" },
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "终止", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "终止操作" },
            new HbtDictData { DictType = "workflow_instance_oper_type", DictLabel = "撤回", DictValue = "6", CssClass = 6, ListClass = 6, OrderNum = 6, Status = 0, Remark = "撤回操作" },

            // 流转类型 (workflow_instance_trans_type)
            new HbtDictData { DictType = "workflow_instance_trans_type", DictLabel = "正常流转", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "正常流转" },
            new HbtDictData { DictType = "workflow_instance_trans_type", DictLabel = "驳回流转", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "驳回流转" },
            new HbtDictData { DictType = "workflow_instance_trans_type", DictLabel = "转办流转", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "转办流转" },
            new HbtDictData { DictType = "workflow_instance_trans_type", DictLabel = "终止流转", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "终止流转" },

            // 流转结果 (workflow_instance_trans_result)
            new HbtDictData { DictType = "workflow_instance_trans_result", DictLabel = "失败", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "流转失败" },
            new HbtDictData { DictType = "workflow_instance_trans_result", DictLabel = "成功", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "流转成功" },

            // 节点类型 (workflow_node_type)
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "开始", DictValue = "start", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "开始节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "审批", DictValue = "approval", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "审批节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "分支", DictValue = "branch", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "分支节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "并行", DictValue = "parallel", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "并行节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "汇聚", DictValue = "join", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "汇聚节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "结束", DictValue = "end", CssClass = 6, ListClass = 6, OrderNum = 6, Status = 0, Remark = "结束节点" },

            // 审批人类型 (workflow_approver_type)
            new HbtDictData { DictType = "workflow_approver_type", DictLabel = "指定用户", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "指定具体用户" },
            new HbtDictData { DictType = "workflow_approver_type", DictLabel = "角色", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "指定角色" },
            new HbtDictData { DictType = "workflow_approver_type", DictLabel = "部门", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "指定部门" },
            new HbtDictData { DictType = "workflow_approver_type", DictLabel = "动态", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "动态指定" },

            // 任务类型 (workflow_task_type)
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "审批任务", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "审批类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "会签任务", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "会签类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "加签任务", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "加签类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "转办任务", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "转办类型任务" },

            // 任务状态 (workflow_task_status)
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "待处理", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "任务待处理状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "处理中", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "任务处理中状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已同意", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "任务已同意状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已拒绝", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "任务已拒绝状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已退回", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "任务已退回状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已转办", DictValue = "5", CssClass = 6, ListClass = 6, OrderNum = 6, Status = 0, Remark = "任务已转办状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已取消", DictValue = "6", CssClass = 7, ListClass = 7, OrderNum = 7, Status = 0, Remark = "任务已取消状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已过期", DictValue = "7", CssClass = 8, ListClass = 8, OrderNum = 8, Status = 0, Remark = "任务已过期状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已失败", DictValue = "8", CssClass = 9, ListClass = 9, OrderNum = 9, Status = 0, Remark = "任务已失败状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已跳过", DictValue = "9", CssClass = 10, ListClass = 10, OrderNum = 10, Status = 0, Remark = "任务已跳过状态" },

            // 转化状态 (workflow_instance_trans_status)
            new HbtDictData { DictType = "workflow_instance_trans_status", DictLabel = "未转化", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1, Status = 0, Remark = "工作流未开始转化" },
            new HbtDictData { DictType = "workflow_instance_trans_status", DictLabel = "转化中", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2, Status = 0, Remark = "工作流正在转化中" },
            new HbtDictData { DictType = "workflow_instance_trans_status", DictLabel = "已转化", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3, Status = 0, Remark = "工作流转化完成" },
            new HbtDictData { DictType = "workflow_instance_trans_status", DictLabel = "转化失败", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4, Status = 0, Remark = "工作流转化失败" },
            new HbtDictData { DictType = "workflow_instance_trans_status", DictLabel = "已取消", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5, Status = 0, Remark = "工作流转化已取消" }
        };
    }
} 