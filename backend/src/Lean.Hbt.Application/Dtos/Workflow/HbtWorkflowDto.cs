//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowStartDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流启动DTO
//===================================================================

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流启动DTO
    /// </summary>
    public class HbtWorkflowStartDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 工作流变量
        /// </summary>
        public Dictionary<string, object>? Variables { get; set; }
    }

    /// <summary>
    /// 工作流仪表盘统计DTO（基于工作流实例Status状态统计）
    /// </summary>
    public class HbtWorkflowDashboardStatsDto
    {
        /// <summary>
        /// 工作流定义数量
        /// </summary>
        public int DefinitionCount { get; set; }
        /// <summary>
        /// 实例数量
        /// </summary>
        public int InstanceCount { get; set; }
        /// <summary>
        /// 运行中数量（Status = 1）
        /// </summary>
        public int RunningCount { get; set; }
        /// <summary>
        /// 已完成数量（Status = 3）
        /// </summary>
        public int EndedCount { get; set; }
        /// <summary>
        /// 任务数量
        /// </summary>
        public int TaskCount { get; set; }
    }

    /// <summary>
    /// 工作流最近活动DTO
    /// </summary>
    public class HbtWorkflowActivityDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 用户任务状态统计DTO（基于Status状态统计）
    /// </summary>
    public class HbtTaskStatusStatsDto
    {
        // Status相关统计（任务状态）
        /// <summary>
        /// 待处理数量（Status = 0）
        /// </summary>
        public int PendingCount { get; set; }

        /// <summary>
        /// 处理中数量（Status = 1）
        /// </summary>
        public int ProcessingCount { get; set; }

        /// <summary>
        /// 已同意数量（Status = 2）
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 已拒绝数量（Status = 3）
        /// </summary>
        public int RejectedCount { get; set; }

        /// <summary>
        /// 已退回数量（Status = 4）
        /// </summary>
        public int ReturnedCount { get; set; }

        /// <summary>
        /// 已转办数量（Status = 5）
        /// </summary>
        public int TransferredCount { get; set; }

        /// <summary>
        /// 已取消数量（Status = 6）
        /// </summary>
        public int CancelledCount { get; set; }

        // 兼容性字段（保持向后兼容）
        /// <summary>
        /// 待办数量（兼容性字段，等于PendingCount）
        /// </summary>
        public int TodoCount { get; set; }

        /// <summary>
        /// 待处理数量（兼容性字段，等于ProcessingCount）
        /// </summary>
        public int WaitCount { get; set; }

        /// <summary>
        /// 已完成数量（兼容性字段，等于ApprovedCount）
        /// </summary>
        public int DoneCount { get; set; }

        /// <summary>
        /// 已读数量（兼容性字段，等于CancelledCount）
        /// </summary>
        public int ReadCount { get; set; }

        // 其他统计
        /// <summary>
        /// 催办数量
        /// </summary>
        public int UrgeCount { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 即将到期数量（3天内到期）
        /// </summary>
        public int DueSoonCount { get; set; }

        /// <summary>
        /// 已逾期数量
        /// </summary>
        public int OverdueCount { get; set; }

        /// <summary>
        /// 高优先级数量
        /// </summary>
        public int HighPriorityCount { get; set; }
    }

    /// <summary>
    /// 用户任务结果统计DTO（基于Result结果统计）
    /// </summary>
    public class HbtTaskResultStatsDto
    {
        /// <summary>
        /// 未处理数量（Result = 0）
        /// </summary>
        public int UnprocessedCount { get; set; }

        /// <summary>
        /// 同意数量（Result = 1）
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 拒绝数量（Result = 2）
        /// </summary>
        public int RejectedCount { get; set; }

        /// <summary>
        /// 退回数量（Result = 3）
        /// </summary>
        public int ReturnedCount { get; set; }

        /// <summary>
        /// 转办数量（Result = 4）
        /// </summary>
        public int TransferredCount { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 工作流实例选项DTO
    /// </summary>
    public class HbtInstanceOptionDto
    {
        /// <summary>
        /// 实例ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long DefinitionId { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string DefinitionName { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}