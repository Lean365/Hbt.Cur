//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtLanguageDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言数据传输对象
//===================================================================
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;
using Mapster;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 语言基础DTO
    /// </summary>
    public class HbtLanguageDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long LangId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 语言内置（0否 1是）
        /// </summary>
        public int LangBuiltin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 语言查询DTO
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
        public int? Status { get; set; }
    }

    /// <summary>
    /// 语言创建DTO
    /// </summary>
    public class HbtLanguageCreateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [Required(ErrorMessage = "语言代码不能为空")]
        [MaxLength(50, ErrorMessage = "语言代码长度不能超过50个字符")]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        [Required(ErrorMessage = "语言名称不能为空")]
        [MaxLength(100, ErrorMessage = "语言名称长度不能超过100个字符")]
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        [MaxLength(100, ErrorMessage = "语言图标长度不能超过100个字符")]
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [Range(0, 9999, ErrorMessage = "排序号必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        [Range(0, 9999, ErrorMessage = "租户ID必须在0-9999之间")]
        public long TenantId { get; set; }

        /// <summary>
        /// 语言内置（0否 1是）
        /// </summary>
        public int LangBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 语言更新DTO
    /// </summary>
    public class HbtLanguageUpdateDto : HbtLanguageCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "语言ID不能为空")]
        public long LangId { get; set; }
    }

    /// <summary>
    /// 语言导入DTO
    /// </summary>
    public class HbtLanguageImportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }
    }

    /// <summary>
    /// 语言导出DTO
    /// </summary>
    public class HbtLanguageExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 语言模板DTO
    /// </summary>
    public class HbtLanguageTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }
    }

    /// <summary>
    /// 语言状态DTO
    /// </summary>
    public class HbtLanguageStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "语言ID不能为空")]
        public long LangId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }
} 