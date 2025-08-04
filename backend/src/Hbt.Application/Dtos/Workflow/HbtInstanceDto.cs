//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtInstanceDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工作流实例数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例基础DTO（与HbtInstance实体字段严格对应）
    /// </summary>
    public class HbtInstanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceDto()
        {
            InstanceTitle = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceId { get; set; }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 工作流实例查询DTO
    /// </summary>
    public class HbtInstanceQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceQueryDto()
        {
            InstanceTitle = string.Empty;
            BusinessKey = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string? InstanceTitle { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long? SchemeId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long? InitiatorId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int? Priority { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public int? Urgency { get; set; }
    }

    /// <summary>
    /// 工作流实例创建DTO
    /// </summary>
    public class HbtInstanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceCreateDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工作流实例更新DTO
    /// </summary>
    public class HbtInstanceUpdateDto : HbtInstanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceId { get; set; }
    }

    /// <summary>
    /// 工作流实例状态DTO
    /// </summary>
    public class HbtInstanceStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long InstanceId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流启动DTO
    /// </summary>
    public class HbtInstanceStartDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceStartDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }
    }

    /// <summary>
    /// 工作流实例模板DTO
    /// </summary>
    public class HbtInstanceTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTemplateDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }
    }

    /// <summary>
    /// 工作流实例导入DTO
    /// </summary>
    public class HbtInstanceImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceImportDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }
    }

    /// <summary>
    /// 工作流实例导出DTO
    /// </summary>
    public class HbtInstanceExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceExportDto()
        {
            InstanceTitle = string.Empty;
            BusinessKey = string.Empty;
            Status = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; } = string.Empty;

        /// <summary>
        /// 发起人ID
        /// </summary>
        public long InitiatorId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority { get; set; } = string.Empty;

        /// <summary>
        /// 紧急程度
        /// </summary>
        public string Urgency { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 