#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIsoDocument.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : ISO文档实体类（简化树形结构）
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Document
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
    [SugarIndex("ix_iso_document_status", nameof(DocumentStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_type", nameof(DocumentType), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_level", nameof(DocumentLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_tree_path", nameof(TreePath), OrderByType.Asc, false)]
    public class HbtIsoDocument : HbtBaseEntity
    {
        /// <summary>父级ID</summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>树形路径</summary>
        [SugarColumn(ColumnName = "tree_path", ColumnDescription = "树形路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string TreePath { get; set; } = string.Empty;

        /// <summary>层级深度</summary>
        [SugarColumn(ColumnName = "tree_level", ColumnDescription = "层级深度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TreeLevel { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>是否叶子节点</summary>
        [SugarColumn(ColumnName = "is_leaf", ColumnDescription = "是否叶子节点", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsLeaf { get; set; } = 1;

        /// <summary>文档编号</summary>
        [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>文档名称</summary>
        [SugarColumn(ColumnName = "document_name", ColumnDescription = "文档名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>文档标题</summary>
        [SugarColumn(ColumnName = "document_title", ColumnDescription = "文档标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentTitle { get; set; } = string.Empty;

        /// <summary>ISO标准</summary>
        [SugarColumn(ColumnName = "iso_standard", ColumnDescription = "ISO标准", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IsoStandard { get; set; }

        /// <summary>节点类型(1=集团 2=分公司 3=管理手册 4=部门 5=一阶文件 6=二阶文件 7=三阶文件 8=四阶文件 9=五阶文件 10=具体文档)</summary>
        [SugarColumn(ColumnName = "node_type", ColumnDescription = "节点类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int NodeType { get; set; } = 1;

        /// <summary>文档类型(1=质量手册 2=程序文件 3=作业指导书 4=表格记录 5=外来文件 6=其他)</summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentType { get; set; }

        /// <summary>文档级别(1=一级 2=二级 3=三级 4=四级 5=五级)</summary>
        [SugarColumn(ColumnName = "document_level", ColumnDescription = "文档级别", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentLevel { get; set; }

        /// <summary>文档描述</summary>
        [SugarColumn(ColumnName = "document_description", ColumnDescription = "文档描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentDescription { get; set; }

        /// <summary>文档内容</summary>
        [SugarColumn(ColumnName = "document_content", ColumnDescription = "文档内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentContent { get; set; }

        /// <summary>文档版本</summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentVersion { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        [SugarColumn(ColumnName = "publish_date", ColumnDescription = "发布日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>是否需要培训(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "need_training", ColumnDescription = "是否需要培训", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NeedTraining { get; set; } = 0;

        /// <summary>是否需要考试(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "need_exam", ColumnDescription = "是否需要考试", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NeedExam { get; set; } = 0;

        /// <summary>文档制定人</summary>
        [SugarColumn(ColumnName = "draft_person", ColumnDescription = "文档制定人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftPerson { get; set; }

        /// <summary>文档制定日期</summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "文档制定日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DraftDate { get; set; }

        /// <summary>审核人</summary>
        [SugarColumn(ColumnName = "reviewer", ColumnDescription = "审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Reviewer { get; set; }

        /// <summary>审核日期</summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>审核意见</summary>
        [SugarColumn(ColumnName = "review_comment", ColumnDescription = "审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReviewComment { get; set; }

        /// <summary>批准人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "批准人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>批准日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "批准日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>批准意见</summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "批准意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalComment { get; set; }

        /// <summary>发布人</summary>
        [SugarColumn(ColumnName = "publisher", ColumnDescription = "发布人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Publisher { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        [SugarColumn(ColumnName = "publish_method", ColumnDescription = "发布方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishScope { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>文档状态(0=草稿 1=待审核 2=已审核 3=已发布 4=已作废 5=已归档)</summary>
        [SugarColumn(ColumnName = "document_status", ColumnDescription = "文档状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DocumentStatus { get; set; } = 0;

        /// <summary>是否置顶(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_top", ColumnDescription = "是否置顶", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsTop { get; set; } = 0;

        /// <summary>是否推荐(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_recommended", ColumnDescription = "是否推荐", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRecommended { get; set; } = 0;



        /// <summary>
        /// 父级文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtIsoDocument? Parent { get; set; }

        /// <summary>
        /// 子级文档列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<HbtIsoDocument>? Children { get; set; }
    }
} 