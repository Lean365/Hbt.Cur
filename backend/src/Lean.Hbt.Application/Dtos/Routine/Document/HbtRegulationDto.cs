//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtRegulationDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述   : 规章制度数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.Document
{
    /// <summary>
    /// 规章制度基础DTO
    /// </summary>
    public class HbtRegulationDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRegulationDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
            RegulationContent = string.Empty;
            RegulationVersion = "1.0";
            RevisionVersion = string.Empty;
            Creator = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            RegulationRemark = string.Empty;
        }

        /// <summary>
        /// 规章制度ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 规章制度ID
        /// </summary>
        [AdaptMember("Id")]
        public long RegulationId { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; }

        /// <summary>规章制度类型(1=管理制度 2=操作规程 3=质量标准 4=安全制度 5=其他)</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; }

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

        /// <summary>规章制度状态(0=草稿 1=已发布 2=已作废 3=已归档)</summary>
        public int RegulationStatus { get; set; } = 0;

        /// <summary>是否置顶(0=否 1=是)</summary>
        public int IsTop { get; set; } = 0;

        /// <summary>是否推荐(0=否 1=是)</summary>
        public int IsRecommended { get; set; } = 0;

        /// <summary>备注</summary>
        public string? RegulationRemark { get; set; }

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

        /// <summary>父级规章制度</summary>
        public HbtRegulationDto? Parent { get; set; }

        /// <summary>子级规章制度列表</summary>
        public List<HbtRegulationDto>? Children { get; set; }
    }

    /// <summary>
    /// 规章制度查询DTO
    /// </summary>
    public class HbtRegulationQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRegulationQueryDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
            IssuingDepartment = string.Empty;
            Creator = string.Empty;
        }

        /// <summary>规章制度编号</summary>
        public string? RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string? RegulationTitle { get; set; }

        /// <summary>制定部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>规章制度类型</summary>
        public int? RegulationType { get; set; }

        /// <summary>规章制度级别</summary>
        public int? RegulationLevel { get; set; }

        /// <summary>规章制度状态</summary>
        public int? RegulationStatus { get; set; }

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
    /// 规章制度创建DTO
    /// </summary>
    public class HbtRegulationCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRegulationCreateDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
            RegulationContent = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            RegulationRemark = string.Empty;
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; }

        /// <summary>规章制度类型(1=管理制度 2=操作规程 3=质量标准 4=安全制度 5=其他)</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度内容</summary>
        public string? RegulationContent { get; set; }

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

        /// <summary>备注</summary>
        public string? RegulationRemark { get; set; }
    }

    /// <summary>
    /// 规章制度更新DTO
    /// </summary>
    public class HbtRegulationUpdateDto : HbtRegulationCreateDto
    {
        /// <summary>规章制度ID</summary>
        [AdaptMember("Id")]
        public long RegulationId { get; set; }


    }

    /// <summary>
    /// 规章制度导入DTO
    /// </summary>
    public class HbtRegulationImportDto
    {
        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>制定部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; } = 1;

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

        /// <summary>规章制度状态</summary>
        public int RegulationStatus { get; set; } = 0;
    }

    /// <summary>
    /// 规章制度导出DTO
    /// </summary>
    public class HbtRegulationExportDto
    {
        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>制定部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; }

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; }

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

        /// <summary>规章制度状态</summary>
        public int RegulationStatus { get; set; }

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 规章制度模板DTO
    /// </summary>
    public class HbtRegulationTemplateDto
    {
        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>制定部门</summary>
        public string? IssuingDepartment { get; set; }

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; } = 1;

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

        /// <summary>规章制度状态</summary>
        public int RegulationStatus { get; set; } = 0;
    }
}