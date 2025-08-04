#nullable enable

using SqlSugar;
using Hbt.Cur.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIsoDocument.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : ISO文档实体类（简化树形结构）
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Cur.Domain.Entities.Routine.Document
{
    /// <summary>
    /// ISO文档实体类（简化树形结构）
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录ISO质量管理体系文档，支持复杂组织架构的树形层级结构
    /// </remarks>
    [SugarTable("hbt_routine_iso_document", "ISO文档表")]
    [SugarIndex("ix_iso_document_code", nameof(DocumentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_iso_document_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_type", nameof(DocumentType), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_level", nameof(DocumentLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    public class HbtIsoDocument : HbtBaseEntity
    {
        /// <summary>
        /// 父级ID。用于实现树形结构，0表示根节点。
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准。文档对应的ISO标准编号。
        /// </summary>
        [SugarColumn(ColumnName = "iso_standard", ColumnDescription = "ISO标准", ColumnDataType = "int", IsNullable = true)]
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型。1=质量手册，2=程序文件，3=作业指导书，4=表格记录，5=外来文件，6=其他。
        /// </summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号。ISO文档的唯一编号。
        /// </summary>
        [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文档名称。ISO文档的名称。
        /// </summary>
        [SugarColumn(ColumnName = "document_name", ColumnDescription = "文档名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>
        /// 文档级别。1=一级，2=二级，3=三级，4=四级，5=五级。
        /// </summary>
        [SugarColumn(ColumnName = "document_level", ColumnDescription = "文档级别", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述。ISO文档的详细描述。
        /// </summary>
        [SugarColumn(ColumnName = "document_description", ColumnDescription = "文档描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容。ISO文档的具体内容。
        /// </summary>
        [SugarColumn(ColumnName = "document_content", ColumnDescription = "文档内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本。ISO文档的版本号。
        /// </summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期。ISO文档的生效日期。
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期。ISO文档的失效日期。
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号。用于自定义文档在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;     

        /// <summary>
        /// 状态。0=草稿，1=待审核，2=已审核，3=已发布，4=已作废，5=已归档。
        /// </summary>
        [SugarColumn(ColumnName = "document_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;   

        /// <summary>
        /// 重要程度。1=一般，2=重要，3=非常重要。
        /// </summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制。0=否，1=是。
        /// </summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开。0=否，1=是。
        /// </summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门。制定ISO文档的部门。
        /// </summary>
        [SugarColumn(ColumnName = "draft_department", ColumnDescription = "制定部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人。制定ISO文档的人员。
        /// </summary>
        [SugarColumn(ColumnName = "draft_by", ColumnDescription = "文档制定人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期。ISO文档的制定日期。
        /// </summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "文档制定日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人。审核ISO文档的人员。
        /// </summary>
        [SugarColumn(ColumnName = "reviewer", ColumnDescription = "审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期。ISO文档的审核日期。
        /// </summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见。审核人对ISO文档的意见。
        /// </summary>
        [SugarColumn(ColumnName = "review_comment", ColumnDescription = "审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表。管理代表对ISO文档的批准。
        /// </summary>
        [SugarColumn(ColumnName = "management_representative", ColumnDescription = "管理代表", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期。管理代表批准ISO文档的日期。
        /// </summary>
        [SugarColumn(ColumnName = "management_approval_date", ColumnDescription = "管理代表批准日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见。管理代表对ISO文档的批准意见。
        /// </summary>
        [SugarColumn(ColumnName = "management_approval_comment", ColumnDescription = "管理代表批准意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期。ISO文档的发布日期。
        /// </summary>
        [SugarColumn(ColumnName = "publish_date", ColumnDescription = "发布日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人。发布ISO文档的人员。
        /// </summary>
        [SugarColumn(ColumnName = "publisher", ColumnDescription = "发布人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式。1=内部发布，2=全员发布，3=指定部门发布。
        /// </summary>
        [SugarColumn(ColumnName = "publish_method", ColumnDescription = "发布方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围。ISO文档的发布范围。
        /// </summary>
        [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档。与ISO文档相关的其他文档。
        /// </summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件。与ISO文档相关的文件。
        /// </summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词。ISO文档的关键词，用于搜索。
        /// </summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>
        /// 阅读次数。ISO文档被阅读的次数。
        /// </summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>
        /// 下载次数。ISO文档被下载的次数。
        /// </summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;


        /// <summary>
        /// 父级文档。该文档的父级文档。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtIsoDocument? Parent { get; set; }

        /// <summary>
        /// 子级文档列表。该文档的子级文档列表。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<HbtIsoDocument>? Children { get; set; }
    }
} 