//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtWorkflowDefinitionDto.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流定义数据传输对象
//===================================================================

using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流定义状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionStatusDto
    {
        /// <summary>
        /// 工作流定义ID
        /// </summary>
        public long WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public HbtWorkflowStatus Status { get; set; }

        /// <summary>
        /// 可用操作列表
        /// </summary>
        public List<string> AvailableOperations { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get; set; }
    }

    /// <summary>
    /// 工作流定义基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionDto
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public int WorkflowVersion { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public HbtWorkflowStatus WorkflowStatus { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<HbtWorkflowNodeDto> WorkflowNodes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }

    /// <summary>
    /// 工作流定义查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public HbtWorkflowStatus? WorkflowStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工作流定义创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionCreateDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<HbtWorkflowNodeCreateDto> WorkflowNodes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流定义更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionUpdateDto
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public HbtWorkflowStatus WorkflowStatus { get; set; }

        /// <summary>
        /// 工作流节点列表
        /// </summary>
        public List<HbtWorkflowNodeUpdateDto> WorkflowNodes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流定义导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionImportDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; }

        /// <summary>
        /// 工作流配置
        /// </summary>
        public string WorkflowConfig { get; set; }

        /// <summary>
        /// 工作流节点列表(JSON格式)
        /// </summary>
        public string WorkflowNodes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工作流定义导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionExportDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 工作流版本
        /// </summary>
        public int WorkflowVersion { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public string WorkflowStatusName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 工作流定义模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowDefinitionTemplateDto
    {
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// 工作流分类
        /// </summary>
        public string WorkflowCategory { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
} 