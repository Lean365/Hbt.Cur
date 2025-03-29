#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowScheduledTaskDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定时任务DTO
//===================================================================

using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流定时任务DTO
    /// </summary>
    public class HbtWorkflowScheduledTaskDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowScheduledTaskDto()
        {
            WorkflowScheduledTaskId = 0;
            WorkflowInstanceId = 0;
            NodeId = 0;
            TaskType = 0;
            ScheduledTime = DateTime.Now;
            ExecutedTime = null;
            Status = 0;
            RetryCount = 0;
            MaxRetryCount = 3;
            ErrorMessage = string.Empty;
            TaskParameters = string.Empty;
        }

        /// <summary>
        /// 任务ID
        /// </summary>
        [AdaptMember("Id")]
        public long WorkflowScheduledTaskId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long WorkflowInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public long NodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// 实际执行时间
        /// </summary>
        public DateTime? ExecutedTime { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 任务参数(JSON格式)
        /// </summary>
        public string? TaskParameters { get; set; }

        /// <summary>
        /// 工作流实例标题
        /// </summary>
        public string? WorkflowTitle { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string? NodeName { get; set; }
    }
}