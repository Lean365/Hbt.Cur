//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtDictTypeDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典类型数据传输对象
//===================================================================
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;
using Mapster;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 字典类型基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long DictTypeId { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        public int DictCategory { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int DictBuiltin { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public long TenantId { get; set; }

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
    /// 字典类型查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "字典名称长度不能超过100个字符")]
        public string? DictName { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string? DictType { get; set; }

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        public int? DictCategory { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 字典类型创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeCreateDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [Required(ErrorMessage = "字典名称不能为空")]
        [MaxLength(100, ErrorMessage = "字典名称长度不能超过100个字符")]
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        [Required(ErrorMessage = "字典类别不能为空")]
        public int DictCategory { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int DictBuiltin { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

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
        /// 租户ID
        /// </summary>
        [Required(ErrorMessage = "租户ID不能为空")]
        [Range(0, 9999, ErrorMessage = "租户ID必须在0-9999之间")]
        public long TenantId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典类型更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeUpdateDto : HbtDictTypeCreateDto
    {
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "字典类型ID不能为空")]
[AdaptMember("Id")]
    public long DictTypeId { get; set; }
    }

    /// <summary>
    /// 字典类型导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeImportDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        public int DictCategory { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 字典类型导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeExportDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        public int DictCategory { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

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
    /// 字典类型模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeTemplateDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典类别（0系统 1SQL）
        /// </summary>
        public int DictCategory { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 字典类型状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDictTypeStatusDto
    {
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "字典类型ID不能为空")]
[AdaptMember("Id")]
    public long DictTypeId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }
    }
} 