//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLanguageDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言数据传输对象
//===================================================================
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;
using Mapster;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 语言数据传输对象
    /// </summary>
    public class HbtLanguageDto
    {
        /// <summary>
        /// 语言ID
        /// </summary>
        [AdaptMember("Id")]
        public long LanguageId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

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
    /// 语言查询对象
    /// </summary>
    public class HbtLanguageQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string? LangCode { get; set; }

        /// <summary>
        /// 语言名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "语言名称长度不能超过100个字符")]
        public string? LangName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 语言创建对象
    /// </summary>
    public class HbtLanguageCreateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        [Required(ErrorMessage = "语言名称不能为空")]
        [MaxLength(100, ErrorMessage = "语言名称长度不能超过100个字符")]
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 语言更新对象
    /// </summary>
    public class HbtLanguageUpdateDto
    {
        /// <summary>
        /// 语言ID
        /// </summary>
        [Required(ErrorMessage = "语言ID不能为空")]
        [AdaptMember("Id")]
        public long LanguageId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        [Required(ErrorMessage = "语言名称不能为空")]
        [MaxLength(100, ErrorMessage = "语言名称长度不能超过100个字符")]
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }

    /// <summary>
    /// 语言导入对象
    /// </summary>
    public class HbtLanguageImportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        [Required(ErrorMessage = "语言名称不能为空")]
        [MaxLength(100, ErrorMessage = "语言名称长度不能超过100个字符")]
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; } = null!;
    }

    /// <summary>
    /// 语言导出对象
    /// </summary>
    public class HbtLanguageExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

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
    /// 语言模板对象
    /// </summary>
    public class HbtLanguageTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = null!;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = null!;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; } = null!;
    }

    /// <summary>
    /// 语言状态更新对象
    /// </summary>
    public class HbtLanguageStatusDto
    {
        /// <summary>
        /// 语言ID
        /// </summary>
        [Required(ErrorMessage = "语言ID不能为空")]
        [AdaptMember("Id")]
        public long LanguageId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public HbtStatus Status { get; set; }
    }
} 