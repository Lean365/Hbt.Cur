//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: HbtFormDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 表单数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Cur.Application.Dtos.Workflow
{
    /// <summary>
    /// 表单基础DTO（与HbtForm实体字段严格对应）
    /// </summary>
    public class HbtFormDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long FormId { get; set; }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已作废)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

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
    /// 表单查询DTO
    /// </summary>
    public class HbtFormQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormQueryDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string? FormKey { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string? FormName { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public int? FormType { get; set; }

        /// <summary>
        /// 表单分类
        /// </summary>
        public int? FormCategory { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 表单创建DTO
    /// </summary>
    public class HbtFormCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormCreateDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long? InstanceId { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 表单更新DTO
    /// </summary>
    public class HbtFormUpdateDto : HbtFormCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long FormId { get; set; }
    }

    /// <summary>
    /// 表单状态DTO
    /// </summary>
    public class HbtFormStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long FormId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已作废)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 表单模板DTO
    /// </summary>
    public class HbtFormTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormTemplateDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// 表单导入DTO
    /// </summary>
    public class HbtFormImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormImportDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// 表单导出DTO
    /// </summary>
    public class HbtFormExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFormExportDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = string.Empty;
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型
        /// </summary>
        public string FormType { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类
        /// </summary>
        public string FormCategory { get; set; } = string.Empty;

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}