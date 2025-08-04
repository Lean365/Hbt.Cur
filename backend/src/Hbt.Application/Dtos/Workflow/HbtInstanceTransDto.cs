//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtInstanceTransDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工作流实例流转历史数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例流转历史DTO
    /// </summary>
    public class HbtInstanceTransDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 流转历史ID
        /// </summary>
        [AdaptMember("Id")]
        public long Id { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int StartNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string StartNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string ToNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int ToNodeType { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string ToNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 转化状态
        /// </summary>
        public int TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 转化时间
        /// </summary>
        public DateTime TransTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 工作流实例流转历史查询DTO
    /// </summary>
    public class HbtInstanceTransQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransQueryDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string? StartNodeId { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string? StartNodeName { get; set; }

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int? StartNodeType { get; set; }

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string? ToNodeId { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string? ToNodeName { get; set; }

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int? ToNodeType { get; set; }

        /// <summary>
        /// 转化状态
        /// </summary>
        public int? TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int? IsFinish { get; set; }

        /// <summary>
        /// 创建人（用于查询我的待办/已办）
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 工作流实例流转历史创建DTO
    /// </summary>
    public class HbtInstanceTransCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransCreateDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int StartNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string StartNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string ToNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int ToNodeType { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string ToNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 转化状态
        /// </summary>
        public int TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 转化时间
        /// </summary>
        public DateTime TransTime { get; set; }
    }

    /// <summary>
    /// 工作流实例流转历史模板DTO
    /// </summary>
    public class HbtInstanceTransTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransTemplateDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int StartNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string StartNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string ToNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int ToNodeType { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string ToNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 转化状态
        /// </summary>
        public int TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 转化时间
        /// </summary>
        public DateTime TransTime { get; set; }
    }

    /// <summary>
    /// 工作流实例流转历史导入DTO
    /// </summary>
    public class HbtInstanceTransImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransImportDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int StartNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string StartNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string ToNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int ToNodeType { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string ToNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 转化状态
        /// </summary>
        public int TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 转化时间
        /// </summary>
        public DateTime TransTime { get; set; }
    }

    /// <summary>
    /// 工作流实例流转历史导出DTO
    /// </summary>
    public class HbtInstanceTransExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtInstanceTransExportDto()
        {
            StartNodeId = string.Empty;
            StartNodeName = string.Empty;
            ToNodeId = string.Empty;
            ToNodeName = string.Empty;
        }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int StartNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string StartNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string ToNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点类型
        /// </summary>
        public int ToNodeType { get; set; }

        /// <summary>
        /// 目标节点名称
        /// </summary>
        public string ToNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 转化状态
        /// </summary>
        public int TransState { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsFinish { get; set; }

        /// <summary>
        /// 转化时间
        /// </summary>
        public DateTime TransTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 