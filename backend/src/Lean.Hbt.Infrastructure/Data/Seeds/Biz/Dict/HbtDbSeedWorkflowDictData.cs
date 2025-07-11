//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedWorkflowDictData.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
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
            // 表单分类
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "系统", DictValue = "0",CssClass=1,ListClass=1,OrderNum = 1, Status = 0, Remark = "系统表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "人事", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "人事表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "财务", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "财务表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "日常", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "日常表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "后勤", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "后勤表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "IT", DictValue = "5",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "IT表单分类" },
            new HbtDictData { DictType = "workflow_form_category", DictLabel = "其他", DictValue = "6",CssClass=7,ListClass=7, OrderNum = 7, Status = 0, Remark = "其他表单分类" },

            // 表单版本
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "草稿版本（Draft）", DictValue = "v1.0.0-draft",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "表单版本" },
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "待审版本（Pending Review）", DictValue = "v1.0.0-pending",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "表单版本" },
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "已发布版本（Published）", DictValue = "v1.0.0-published",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "表单版本" },
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "已驳回版本（Rejected）", DictValue = "v1.0.0-rejected",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "表单版本" },
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "正式发布版本（Release）", DictValue = "v1.0.0-release",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "表单版本" },
            new HbtDictData { DictType = "workflow_form_version", DictLabel = "归档版本（Archived）", DictValue = "v1.0.0-archived",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "表单版本" },

            // 表单状态
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "草稿 (Draft)", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "表单草稿状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "待提交 (Pending Submission)", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "表单发布状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "已提交 (Submitted)​", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "表单归档状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "审批中 (Under Review/Approval)", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "表单废弃状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "已批准 (Approved)​", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "表单待审核状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "​已驳回 (Rejected)​", DictValue = "5",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "表单已拒绝状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "​已完成 (Completed)​​", DictValue = "6",CssClass=7,ListClass=7, OrderNum = 7, Status = 0, Remark = "表单已归档状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "​已取消 (Cancelled)​​", DictValue = "7",CssClass=8,ListClass=8, OrderNum = 8, Status = 0, Remark = "表单已取消状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "​已关闭 (Closed)​​", DictValue = "8",CssClass=9,ListClass=9, OrderNum = 9, Status = 0, Remark = "表单已关闭状态" },
            new HbtDictData { DictType = "workflow_form_status", DictLabel = "​已归档 (Archived)​​", DictValue = "9",CssClass=10,ListClass=10, OrderNum = 10, Status = 0, Remark = "表单已归档状态" },

            // 活动类型
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "开始活动", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "工作流开始节点" },
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "结束活动", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "工作流结束节点" },
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "审批活动", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "工作流审批节点" },
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "分支活动", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "工作流分支节点" },
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "合并活动", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "工作流合并节点" },
            new HbtDictData { DictType = "workflow_activity_type", DictLabel = "服务活动", DictValue = "6",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "工作流服务节点" },

            // 流程分类
            new HbtDictData { DictType = "workflow_category", DictLabel = "请假流程", DictValue = "leave",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "员工请假审批流程" },
            new HbtDictData { DictType = "workflow_category", DictLabel = "报销流程", DictValue = "expense",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "费用报销审批流程" },
            new HbtDictData { DictType = "workflow_category", DictLabel = "采购流程", DictValue = "purchase",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "采购申请审批流程" },
            new HbtDictData { DictType = "workflow_category", DictLabel = "合同流程", DictValue = "contract",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "合同审批流程" },

            // 流程状态
            new HbtDictData { DictType = "workflow_status", DictLabel = "草稿", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "流程草稿状态" },
            new HbtDictData { DictType = "workflow_status", DictLabel = "运行中", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "流程运行状态" },
            new HbtDictData { DictType = "workflow_status", DictLabel = "已完成", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "流程完成状态" },
            new HbtDictData { DictType = "workflow_status", DictLabel = "已终止", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "流程终止状态" },
            new HbtDictData { DictType = "workflow_status", DictLabel = "已挂起", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "流程挂起状态" },

            // 实例状态
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "未开始", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "实例未开始状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "运行中", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "实例运行状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已挂起", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "实例挂起状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已完成", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "实例完成状态" },
            new HbtDictData { DictType = "workflow_instance_status", DictLabel = "已终止", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "实例终止状态" },

            // 节点类型
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "开始节点", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "流程开始节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "结束节点", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "流程结束节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "审批节点", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "流程审批节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "分支节点", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "流程分支节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "合并节点", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "流程合并节点" },
            new HbtDictData { DictType = "workflow_node_type", DictLabel = "服务节点", DictValue = "6",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "流程服务节点" },

            // 节点状态
            new HbtDictData { DictType = "workflow_node_status", DictLabel = "草稿", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "节点草稿状态" },
            new HbtDictData { DictType = "workflow_node_status", DictLabel = "运行中", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "节点运行状态" },
            new HbtDictData { DictType = "workflow_node_status", DictLabel = "已完成", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "节点完成状态" },
            new HbtDictData { DictType = "workflow_node_status", DictLabel = "已终止", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "节点终止状态" },
            new HbtDictData { DictType = "workflow_node_status", DictLabel = "已挂起", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "节点挂起状态" },

            // 任务类型
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "审批任务", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "审批类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "会签任务", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "会签类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "加签任务", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "加签类型任务" },
            new HbtDictData { DictType = "workflow_task_type", DictLabel = "转办任务", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "转办类型任务" },

            // 任务状态
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "待处理", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "任务待处理状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "处理中", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "任务处理中状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已同意", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "任务已同意状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已拒绝", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "任务已拒绝状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已退回", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "任务已退回状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已转办", DictValue = "5",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "任务已转办状态" },
            new HbtDictData { DictType = "workflow_task_status", DictLabel = "已取消", DictValue = "6",CssClass=7,ListClass=7, OrderNum = 7, Status = 0, Remark = "任务已取消状态" },

            // 任务结果
            new HbtDictData { DictType = "workflow_task_result", DictLabel = "未处理", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "任务未处理结果" },
            new HbtDictData { DictType = "workflow_task_result", DictLabel = "同意", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "任务同意结果" },
            new HbtDictData { DictType = "workflow_task_result", DictLabel = "拒绝", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "任务拒绝结果" },
            new HbtDictData { DictType = "workflow_task_result", DictLabel = "退回", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "任务退回结果" },
            new HbtDictData { DictType = "workflow_task_result", DictLabel = "转办", DictValue = "4",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "任务转办结果" },

            // 任务优先级
            new HbtDictData { DictType = "workflow_task_priority", DictLabel = "低", DictValue = "0",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "低优先级任务" },
            new HbtDictData { DictType = "workflow_task_priority", DictLabel = "普通", DictValue = "1",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "普通优先级任务" },
            new HbtDictData { DictType = "workflow_task_priority", DictLabel = "高", DictValue = "2",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "高优先级任务" },
            new HbtDictData { DictType = "workflow_task_priority", DictLabel = "紧急", DictValue = "3",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "紧急优先级任务" },

            // 优先级
            new HbtDictData { DictType = "workflow_priority", DictLabel = "普通", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "普通优先级" },
            new HbtDictData { DictType = "workflow_priority", DictLabel = "重要", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "重要优先级" },
            new HbtDictData { DictType = "workflow_priority", DictLabel = "紧急", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "紧急优先级" },
            new HbtDictData { DictType = "workflow_priority", DictLabel = "特急", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "特急优先级" },

            // 变量类型
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "字符串", DictValue = "string",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "字符串类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "整数", DictValue = "integer",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "整数类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "浮点数", DictValue = "float",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "浮点数类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "布尔值", DictValue = "boolean",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "布尔值类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "日期", DictValue = "date",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "日期类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "对象", DictValue = "object",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "对象类型变量" },
            new HbtDictData { DictType = "workflow_variable_type", DictLabel = "数组", DictValue = "array",CssClass=7,ListClass=7, OrderNum = 7, Status = 0, Remark = "数组类型变量" },

            // 指派类型
            new HbtDictData { DictType = "workflow_assignee_type", DictLabel = "用户", DictValue = "user",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "指定具体用户" },
            new HbtDictData { DictType = "workflow_assignee_type", DictLabel = "角色", DictValue = "role",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "指定角色" },
            new HbtDictData { DictType = "workflow_assignee_type", DictLabel = "部门", DictValue = "dept",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "指定部门" },
            new HbtDictData { DictType = "workflow_assignee_type", DictLabel = "岗位", DictValue = "post",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "指定岗位" },
            new HbtDictData { DictType = "workflow_assignee_type", DictLabel = "变量", DictValue = "variable",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "从变量中获取" },

            // 表单类型
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "动态表单", DictValue = "dynamic",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "动态生成的表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "外部表单", DictValue = "external",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "外部系统表单" },
            new HbtDictData { DictType = "workflow_form_type", DictLabel = "自定义表单", DictValue = "custom",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "自定义开发的表单" },

            // 流程版本
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本A", DictValue = "A",CssClass=1,ListClass=1, OrderNum = 1, Status = 0, Remark = "初始版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本B", DictValue = "B",CssClass=2,ListClass=2, OrderNum = 2, Status = 0, Remark = "第二版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本C", DictValue = "C",CssClass=3,ListClass=3, OrderNum = 3, Status = 0, Remark = "第三版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本D", DictValue = "D",CssClass=4,ListClass=4, OrderNum = 4, Status = 0, Remark = "第四版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本E", DictValue = "E",CssClass=5,ListClass=5, OrderNum = 5, Status = 0, Remark = "第五版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本F", DictValue = "F",CssClass=6,ListClass=6, OrderNum = 6, Status = 0, Remark = "第六版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本G", DictValue = "G",CssClass=7,ListClass=7, OrderNum = 7, Status = 0, Remark = "第七版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本H", DictValue = "H",CssClass=8,ListClass=8, OrderNum = 8, Status = 0, Remark = "第八版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本I", DictValue = "I",CssClass=9,ListClass=9, OrderNum = 9, Status = 0, Remark = "第九版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本J", DictValue = "J",CssClass=10,ListClass=10, OrderNum = 10, Status = 0, Remark = "第十版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本K", DictValue = "K",CssClass=11,ListClass=11, OrderNum = 11, Status = 0, Remark = "第十一版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本L", DictValue = "L",CssClass=12,ListClass=12, OrderNum = 12, Status = 0, Remark = "第十二版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本M", DictValue = "M",CssClass=13,ListClass=13, OrderNum = 13, Status = 0, Remark = "第十三版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本N", DictValue = "N",CssClass=14,ListClass=14, OrderNum = 14, Status = 0, Remark = "第十四版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本O", DictValue = "O",CssClass=15,ListClass=15, OrderNum = 15, Status = 0, Remark = "第十五版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本P", DictValue = "P",CssClass=16,ListClass=16, OrderNum = 16, Status = 0, Remark = "第十六版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本Q", DictValue = "Q",CssClass=17,ListClass=17,OrderNum = 17, Status = 0, Remark = "第十七版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本R", DictValue = "R",CssClass=18,ListClass=18,OrderNum = 18, Status = 0, Remark = "第十八版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本S", DictValue = "S",CssClass=19,ListClass=19,OrderNum = 19, Status = 0, Remark = "第十九版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本T", DictValue = "T",CssClass=20,ListClass=20,OrderNum = 20, Status = 0, Remark = "第二十版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本U", DictValue = "U",CssClass=21,ListClass=21,OrderNum = 21, Status = 0, Remark = "第二十一版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本V", DictValue = "V",CssClass=22,ListClass=22,OrderNum = 22, Status = 0, Remark = "第二十二版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本W", DictValue = "W",CssClass=23,ListClass=23,OrderNum = 23, Status = 0, Remark = "第二十三版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本X", DictValue = "X",CssClass=24,ListClass=24,OrderNum = 24, Status = 0, Remark = "第二十四版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本Y", DictValue = "Y",CssClass=25,ListClass=25,OrderNum = 25, Status = 0, Remark = "第二十五版本" },
            new HbtDictData { DictType = "workflow_version", DictLabel = "版本Z", DictValue = "Z",CssClass=26,ListClass=26,OrderNum = 26, Status = 0, Remark = "第二十六版本" }
        };
    }
} 