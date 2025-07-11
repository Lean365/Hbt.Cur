//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtLawDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述   : 法律法规数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.Document
{
    /// <summary>
    /// 法律法规基础DTO
    /// </summary>
    public class HbtLawDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLawDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
            LawContent = string.Empty;
            LawVersion = "1.0";
            RevisionVersion = string.Empty;
            IssuingAuthority = string.Empty;
            DocumentNumber = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            LawRemark = string.Empty;
        }

        /// <summary>
        /// 法律法规ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 法律法规ID
        /// </summary>
        [AdaptMember("Id")]
        public long LawId { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法律法规编号</summary>
        public string LawCode { get; set; }

        /// <summary>法律法规名称</summary>
        public string LawName { get; set; }

        /// <summary>法律法规标题</summary>
        public string LawTitle { get; set; }

        /// <summary>法律法规类型(1=法律 2=行政法规 3=部门规章 4=地方性法规 5=其他)</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法律法规级别(1=国家级 2=省级 3=市级 4=县级)</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法律法规内容</summary>
        public string? LawContent { get; set; }

        /// <summary>法律法规版本</summary>
        public string LawVersion { get; set; }

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

        /// <summary>发布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>文号</summary>
        public string? DocumentNumber { get; set; }

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

        /// <summary>法律法规状态(0=草稿 1=已发布 2=已作废 3=已归档)</summary>
        public int LawStatus { get; set; } = 0;

        /// <summary>是否置顶(0=否 1=是)</summary>
        public int IsTop { get; set; } = 0;

        /// <summary>是否推荐(0=否 1=是)</summary>
        public int IsRecommended { get; set; } = 0;

        /// <summary>备注</summary>
        public string? LawRemark { get; set; }

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

        /// <summary>父级法律法规</summary>
        public HbtLawDto? Parent { get; set; }

        /// <summary>子级法律法规列表</summary>
        public List<HbtLawDto>? Children { get; set; }
    }

    /// <summary>
    /// 法律法规查询DTO
    /// </summary>
    public class HbtLawQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLawQueryDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
            IssuingAuthority = string.Empty;
        }

        /// <summary>法律法规编号</summary>
        public string? LawCode { get; set; }

        /// <summary>法律法规名称</summary>
        public string? LawName { get; set; }

        /// <summary>法律法规标题</summary>
        public string? LawTitle { get; set; }

        /// <summary>法律法规类型</summary>
        public int? LawType { get; set; }

        /// <summary>法律法规级别</summary>
        public int? LawLevel { get; set; }

        /// <summary>法律法规状态</summary>
        public int? LawStatus { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>发布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>开始日期</summary>
        public DateTime? StartDate { get; set; }

        /// <summary>结束日期</summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// 法律法规创建DTO
    /// </summary>
    public class HbtLawCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLawCreateDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
            LawContent = string.Empty;
            IssuingAuthority = string.Empty;
            DocumentNumber = string.Empty;
            PublishScope = string.Empty;
            RelatedDocuments = string.Empty;
            RelatedFiles = string.Empty;
            Keywords = string.Empty;
            LawRemark = string.Empty;
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法律法规编号</summary>
        public string LawCode { get; set; }

        /// <summary>法律法规名称</summary>
        public string LawName { get; set; }

        /// <summary>法律法规标题</summary>
        public string LawTitle { get; set; }

        /// <summary>法律法规类型(1=法律 2=行政法规 3=部门规章 4=地方性法规 5=其他)</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法律法规级别(1=国家级 2=省级 3=市级 4=县级)</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法律法规内容</summary>
        public string? LawContent { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>发布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>文号</summary>
        public string? DocumentNumber { get; set; }

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
        public string? LawRemark { get; set; }
    }

    /// <summary>
    /// 法律法规更新DTO
    /// </summary>
    public class HbtLawUpdateDto : HbtLawCreateDto
    {
        /// <summary>法律法规ID</summary>
        [AdaptMember("Id")]
        public long LawId { get; set; }

    }

    /// <summary>
    /// 法律法规导入DTO
    /// </summary>
    public class HbtLawImportDto
    {
        /// <summary>法律法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法律法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法律法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法律法规类型</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法律法规级别</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

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

        /// <summary>法律法规状态</summary>
        public int LawStatus { get; set; } = 0;
    }

    /// <summary>
    /// 法律法规导出DTO
    /// </summary>
    public class HbtLawExportDto
    {
        /// <summary>法律法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法律法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法律法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法律法规类型</summary>
        public int LawType { get; set; }

        /// <summary>法律法规级别</summary>
        public int LawLevel { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

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

        /// <summary>法律法规状态</summary>
        public int LawStatus { get; set; }

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 法律法规模板DTO
    /// </summary>
    public class HbtLawTemplateDto
    {
        /// <summary>法律法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法律法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法律法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法律法规类型</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法律法规级别</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

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

        /// <summary>法律法规状态</summary>
        public int LawStatus { get; set; } = 0;
    }

    /// <summary>
    /// 法律法规状态更新DTO
    /// </summary>
    public class HbtLawStatusDto
    {
        /// <summary>法律法规ID</summary>
        public long LawId { get; set; }

        /// <summary>法律法规状态</summary>
        public int LawStatus { get; set; }
    }
}