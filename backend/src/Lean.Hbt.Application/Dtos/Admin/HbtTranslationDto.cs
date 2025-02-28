//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译数据传输对象
//===================================================================
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 翻译数据传输对象
    /// </summary>
    public class HbtTranslationDto
    {
        /// <summary>
        /// 翻译ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.ToString();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 翻译查询对象
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
        /// 模块名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 翻译创建对象
    /// </summary>
    public class HbtTranslationCreateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        [Required(ErrorMessage = "翻译键不能为空")]
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        [Required(ErrorMessage = "翻译值不能为空")]
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "模块名称不能为空")]
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 翻译更新对象
    /// </summary>
    public class HbtTranslationUpdateDto
    {
        /// <summary>
        /// 翻译ID
        /// </summary>
        [Required(ErrorMessage = "翻译ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        [Required(ErrorMessage = "翻译键不能为空")]
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        [Required(ErrorMessage = "翻译值不能为空")]
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "模块名称不能为空")]
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 翻译导入对象
    /// </summary>
    public class HbtTranslationImportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        [Required(ErrorMessage = "翻译键不能为空")]
        [MaxLength(200, ErrorMessage = "翻译键长度不能超过200个字符")]
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        [Required(ErrorMessage = "翻译值不能为空")]
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "模块名称不能为空")]
        [MaxLength(100, ErrorMessage = "模块名称长度不能超过100个字符")]
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; } = null!;
    }

    /// <summary>
    /// 翻译导出对象
    /// </summary>
    public class HbtTranslationExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; } = null!;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 翻译模板对象
    /// </summary>
    public class HbtTranslationTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = null!;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = null!;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; } = null!;
    }

    /// <summary>
    /// 翻译状态更新对象
    /// </summary>
    public class HbtTranslationStatusDto
    {
        /// <summary>
        /// 翻译ID
        /// </summary>
        [Required(ErrorMessage = "翻译ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public HbtStatus Status { get; set; }
    }
}