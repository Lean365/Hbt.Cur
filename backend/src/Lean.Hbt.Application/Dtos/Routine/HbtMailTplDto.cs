//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtMailTplDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 邮件模板数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Routine
{
    /// <summary>
    /// 邮件模板基础DTO
    /// </summary>
    public class HbtMailTplDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
            TmplSubject = string.Empty;
            TmplBody = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long TmplId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string TmplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string TmplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string TmplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TmplStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// 邮件模板查询DTO
    /// </summary>
    public class HbtMailTplQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplQueryDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
        public string? TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "模板编码长度不能超过50个字符")]
        public string? TmplCode { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int? TmplStatus { get; set; }

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
    /// 邮件模板创建DTO
    /// </summary>
    public class HbtMailTplCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplCreateDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
            TmplSubject = string.Empty;
            TmplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Required(ErrorMessage = "模板名称不能为空")]
        [MaxLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
        public string TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Required(ErrorMessage = "模板编码不能为空")]
        [MaxLength(50, ErrorMessage = "模板编码长度不能超过50个字符")]
        public string TmplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string TmplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string TmplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [MaxLength(500, ErrorMessage = "参数列表长度不能超过500个字符")]
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TmplStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件模板更新DTO
    /// </summary>
    public class HbtMailTplUpdateDto : HbtMailTplCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "模板ID不能为空")]
        public long TmplId { get; set; }
    }

    /// <summary>
    /// 邮件模板导入DTO
    /// </summary>
    public class HbtMailTplImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplImportDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
            TmplSubject = string.Empty;
            TmplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string TmplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string TmplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string TmplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int TmplStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件模板导出DTO
    /// </summary>
    public class HbtMailTplExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplExportDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
            TmplSubject = string.Empty;
            TmplBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string TmplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string TmplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string TmplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int TmplStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 邮件模板导入模板DTO
    /// </summary>
    public class HbtMailTplTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMailTplTemplateDto()
        {
            TmplName = string.Empty;
            TmplCode = string.Empty;
            TmplSubject = string.Empty;
            TmplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TmplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string TmplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string TmplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string TmplBody { get; set; }

        /// <summary>
        /// 是否HTML(0=否,1=是)
        /// </summary>
        public int TmplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? TmplParameters { get; set; }

        /// <summary>
        /// 状态(0=停用,1=启用)
        /// </summary>
        public int TmplStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}