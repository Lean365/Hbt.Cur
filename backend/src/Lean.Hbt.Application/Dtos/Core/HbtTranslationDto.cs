//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtTranslationDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译数据传输对象
//===================================================================
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Core
{
    /// <summary>
    /// 翻译基础DTO
    /// </summary>
    public class HbtTranslationDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long TranslationId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 翻译内置（0否 1是）
        /// </summary>
        public int TransBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

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
    /// 翻译查询DTO
    /// </summary>
    public class HbtTranslationQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string? LangCode { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string? TransKey { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>
        [MaxLength(500, ErrorMessage = "翻译值长度不能超过500个字符")]
        public string? TransValue { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 翻译创建DTO
    /// </summary>
    public class HbtTranslationCreateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        [Required(ErrorMessage = "翻译键不能为空")]
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        [Required(ErrorMessage = "翻译值不能为空")]
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "模块名称不能为空")]
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        [Range(0, 9999, ErrorMessage = "租户ID必须在0-9999之间")]
        public long TenantId { get; set; }

        /// <summary>
        /// 翻译内置（0否 1是）
        /// </summary>
        public int TransBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 翻译更新DTO
    /// </summary>
    public class HbtTranslationUpdateDto : HbtTranslationCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "翻译ID不能为空")]
        public long TranslationId { get; set; }
    }

    /// <summary>
    /// 翻译导入DTO
    /// </summary>
    public class HbtTranslationImportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 翻译导出DTO
    /// </summary>
    public class HbtTranslationExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 翻译模板DTO
    /// </summary>
    public class HbtTranslationTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 翻译状态DTO
    /// </summary>
    public class HbtTranslationStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "翻译ID不能为空")]
        public long TranslationId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }

    /// <summary>
    /// 转置后的翻译数据DTO
    /// </summary>
    public class HbtTransposedDto
    {
        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 各语言的翻译信息
        /// </summary>
        public Dictionary<string, HbtTranslationLangDto> Translations { get; set; } = new Dictionary<string, HbtTranslationLangDto>();
    }

    /// <summary>
    /// 翻译语言DTO
    /// </summary>
    public class HbtTranslationLangDto
    {
        /// <summary>
        /// 翻译ID
        /// </summary>
        public long TranslationId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 转置查询DTO
    /// </summary>
    public class HbtTransposedQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 翻译键
        /// </summary>
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string? TransKey { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>
        [MaxLength(500, ErrorMessage = "翻译值长度不能超过500个字符")]
        public string? TransValue { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }
    }
}