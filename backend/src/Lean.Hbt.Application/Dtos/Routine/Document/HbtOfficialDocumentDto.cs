//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtOfficialDocumentDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述   : 公文文档数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.Document
{
    /// <summary>
    /// 公文文档基础DTO
    /// </summary>
    public class HbtOfficialDocumentDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOfficialDocumentDto()
        {
            DocumentCode = string.Empty;
            DocumentNumber = string.Empty;
            DocumentTitle = string.Empty;
            DocumentDescription = string.Empty;
            DocumentContent = string.Empty;
            DocumentPdfPath = string.Empty;
            DocumentVersion = "1.0";
            RevisionVersion = string.Empty;
            Creator = string.Empty;
            IssuingDepartment = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            DocumentRemark = string.Empty;
        }

        /// <summary>
        /// 文档ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 文档ID
        /// </summary>
        [AdaptMember("Id")]
        public long DocumentId { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>文档编号</summary>
        public string DocumentCode { get; set; }

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string DocumentTitle { get; set; }

        /// <summary>文档类型(1=通知 2=公告 3=决定 4=报告 5=请示 6=批复 7=函 8=纪要 9=其他)</summary>
        public int DocumentType { get; set; } = 1;

        /// <summary>文档级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int DocumentLevel { get; set; } = 1;

        /// <summary>文档描述</summary>
        public string? DocumentDescription { get; set; }

        /// <summary>文档内容</summary>
        public string? DocumentContent { get; set; }

        /// <summary>文档PDF文件路径</summary>
        public string? DocumentPdfPath { get; set; }

        /// <summary>文档版本</summary>
        public string DocumentVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>是否需要培训(0=否 1=是)</summary>
        public int NeedTraining { get; set; } = 0;

        /// <summary>是否需要考试(0=否 1=是)</summary>
        public int NeedExam { get; set; } = 0;

        /// <summary>创建人</summary>
        public string? Creator { get; set; }

        /// <summary>创建日期</summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>相关文档</summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>文档状态(0=草稿 1=已发布 2=已作废 3=已归档)</summary>
        public int DocumentStatus { get; set; } = 0;

        /// <summary>是否置顶(0=否 1=是)</summary>
        public int IsTop { get; set; } = 0;

        /// <summary>是否推荐(0=否 1=是)</summary>
        public int IsRecommended { get; set; } = 0;

        /// <summary>备注</summary>
        public string? DocumentRemark { get; set; }

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>更新者</summary>
        public string? UpdateBy { get; set; }

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>是否删除（0未删除 1已删除）</summary>
        public int IsDeleted { get; set; }

        /// <summary>删除者</summary>
        public string? DeleteBy { get; set; }

        /// <summary>删除时间</summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>父级文档</summary>
        public HbtOfficialDocumentDto? Parent { get; set; }

        /// <summary>子级文档列表</summary>
        public List<HbtOfficialDocumentDto>? Children { get; set; }

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }
    }

    /// <summary>
    /// 公文文档查询DTO
    /// </summary>
    public class HbtOfficialDocumentQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOfficialDocumentQueryDto()
        {
            DocumentCode = string.Empty;
            DocumentNumber = string.Empty;
            DocumentTitle = string.Empty;
            IssuingDepartment = string.Empty;
            Creator = string.Empty;
        }

        /// <summary>文档编号</summary>
        public string? DocumentCode { get; set; }

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string? DocumentTitle { get; set; }

        /// <summary>文档类型</summary>
        public int? DocumentType { get; set; }

        /// <summary>文档级别</summary>
        public int? DocumentLevel { get; set; }

        /// <summary>文档状态</summary>
        public int? DocumentStatus { get; set; }

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>创建人</summary>
        public string? Creator { get; set; }

        /// <summary>开始日期</summary>
        public DateTime? StartDate { get; set; }

        /// <summary>结束日期</summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// 公文文档创建DTO
    /// </summary>
    public class HbtOfficialDocumentCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOfficialDocumentCreateDto()
        {
            DocumentNumber = string.Empty;
            DocumentTitle = string.Empty;
            DocumentDescription = string.Empty;
            DocumentContent = string.Empty;
            DocumentPdfPath = string.Empty;
            IssuingDepartment = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            DocumentRemark = string.Empty;
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string DocumentTitle { get; set; }

        /// <summary>文档类型(1=通知 2=公告 3=决定 4=报告 5=请示 6=批复 7=函 8=纪要 9=其他)</summary>
        public int DocumentType { get; set; } = 1;

        /// <summary>文档级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int DocumentLevel { get; set; } = 1;

        /// <summary>文档描述</summary>
        public string? DocumentDescription { get; set; }

        /// <summary>文档内容</summary>
        public string? DocumentContent { get; set; }

        /// <summary>文档PDF文件路径</summary>
        public string? DocumentPdfPath { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>相关文档</summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>备注</summary>
        public string? DocumentRemark { get; set; }
    }

    /// <summary>
    /// 公文文档更新DTO
    /// </summary>
    public class HbtOfficialDocumentUpdateDto : HbtOfficialDocumentCreateDto
    {
        /// <summary>文档ID</summary>
        [AdaptMember("Id")]
        public long DocumentId { get; set; }

    }

    /// <summary>
    /// 公文文档导入DTO
    /// </summary>
    public class HbtOfficialDocumentImportDto
    {
        /// <summary>文档编号</summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string DocumentTitle { get; set; } = string.Empty;

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>文档类型</summary>
        public int DocumentType { get; set; } = 1;

        /// <summary>文档级别</summary>
        public int DocumentLevel { get; set; } = 1;

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>是否需要培训</summary>
        public int NeedTraining { get; set; } = 0;

        /// <summary>是否需要考试</summary>
        public int NeedExam { get; set; } = 0;

        /// <summary>文档状态</summary>
        public int DocumentStatus { get; set; } = 0;
    }

    /// <summary>
    /// 公文文档导出DTO
    /// </summary>
    public class HbtOfficialDocumentExportDto
    {
        /// <summary>文档编号</summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string DocumentTitle { get; set; } = string.Empty;

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>文档类型</summary>
        public int DocumentType { get; set; }

        /// <summary>文档级别</summary>
        public int DocumentLevel { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; }

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; }

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; }

        /// <summary>是否需要培训</summary>
        public int NeedTraining { get; set; }

        /// <summary>是否需要考试</summary>
        public int NeedExam { get; set; }

        /// <summary>文档状态</summary>
        public int DocumentStatus { get; set; }

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 公文文档模板DTO
    /// </summary>
    public class HbtOfficialDocumentTemplateDto
    {
        /// <summary>文档编号</summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>文档文号</summary>
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        public string DocumentTitle { get; set; } = string.Empty;

        /// <summary>发文部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>文档类型</summary>
        public int DocumentType { get; set; } = 1;

        /// <summary>文档级别</summary>
        public int DocumentLevel { get; set; } = 1;

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>是否需要培训</summary>
        public int NeedTraining { get; set; } = 0;

        /// <summary>是否需要考试</summary>
        public int NeedExam { get; set; } = 0;

        /// <summary>文档状态</summary>
        public int DocumentStatus { get; set; } = 0;
    }
}
