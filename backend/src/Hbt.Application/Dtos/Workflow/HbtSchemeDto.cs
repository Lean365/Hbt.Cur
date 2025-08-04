//===================================================================
// 项目名: Hbt.Application
// 文件名: HbtSchemeDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工作流定义数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Hbt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流定义基础DTO（与HbtScheme实体字段严格对应）
    /// </summary>
    public class HbtSchemeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
            Version = "1.0";
            SchemeConfig = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long SchemeId { get; set; }

        /// <summary>
        /// 流程定义键
        /// </summary>
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他)
        /// </summary>
        public int SchemeCategory { get; set; }

        /// <summary>
        /// 流程定义版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 流程定义配置(JSON格式)
        /// </summary>
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 表单定义ID
        /// </summary>
        public long? FormId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已停用)
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
    /// 工作流定义查询DTO
    /// </summary>
    public class HbtSchemeQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeQueryDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
        }



        /// <summary>
        /// 流程定义键
        /// </summary>
        public string? SchemeKey { get; set; }

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string? SchemeName { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        public int? SchemeCategory { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 工作流定义创建DTO
    /// </summary>
    public class HbtSchemeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeCreateDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
            Version = "1.0";
            SchemeConfig = string.Empty;
        }

        /// <summary>
        /// 流程定义键
        /// </summary>
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他)
        /// </summary>
        public int SchemeCategory { get; set; }

        /// <summary>
        /// 流程定义版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 流程定义配置(JSON格式)
        /// </summary>
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 表单定义ID
        /// </summary>
        public long? FormId { get; set; }

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
    /// 工作流定义更新DTO
    /// </summary>
    public class HbtSchemeUpdateDto : HbtSchemeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long SchemeId { get; set; }
    }

    /// <summary>
    /// 工作流定义状态DTO
    /// </summary>
    public class HbtSchemeStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long SchemeId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流定义模板DTO
    /// </summary>
    public class HbtSchemeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeTemplateDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
            Version = "1.0";
            SchemeConfig = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 流程定义键
        /// </summary>
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 流程定义配置(JSON格式)
        /// </summary>
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 表单定义ID
        /// </summary>
        public long? FormId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// 工作流定义导入DTO
    /// </summary>
    public class HbtSchemeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeImportDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
            Version = "1.0";
            SchemeConfig = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 流程定义键
        /// </summary>
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 流程定义配置(JSON格式)
        /// </summary>
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 表单定义ID
        /// </summary>
        public long? FormId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// 工作流定义导出DTO
    /// </summary>
    public class HbtSchemeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSchemeExportDto()
        {
            SchemeKey = string.Empty;
            SchemeName = string.Empty;
            Version = string.Empty;
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 流程定义键
        /// </summary>
        public string SchemeKey { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string SchemeName { get; set; } = string.Empty;

        /// <summary>
        /// 流程分类
        /// </summary>
        public string SchemeCategory { get; set; } = string.Empty;

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