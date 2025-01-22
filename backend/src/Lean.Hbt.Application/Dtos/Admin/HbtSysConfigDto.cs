//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtSysConfigDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 系统配置基础DTO
    /// </summary>
    public class HbtSysConfigDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        public long ConfigId { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int ConfigType { get; set; }

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
    /// 系统配置查询DTO
    /// </summary>
    public class HbtSysConfigQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigQueryDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "配置名称长度不能超过100个字符")]
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [MaxLength(100, ErrorMessage = "配置键名长度不能超过100个字符")]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int? ConfigType { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 系统配置创建DTO
    /// </summary>
    public class HbtSysConfigCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigCreateDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Required(ErrorMessage = "配置名称不能为空")]
        [MaxLength(100, ErrorMessage = "配置名称长度不能超过100个字符")]
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [Required(ErrorMessage = "配置键名不能为空")]
        [MaxLength(100, ErrorMessage = "配置键名长度不能超过100个字符")]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        [Required(ErrorMessage = "配置键值不能为空")]
        [MaxLength(500, ErrorMessage = "配置键值长度不能超过500个字符")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int ConfigType { get; set; }

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
    /// 系统配置更新DTO
    /// </summary>
    public class HbtSysConfigUpdateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigUpdateDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public long ConfigId { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Required(ErrorMessage = "配置名称不能为空")]
        [MaxLength(100, ErrorMessage = "配置名称长度不能超过100个字符")]
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [Required(ErrorMessage = "配置键名不能为空")]
        [MaxLength(100, ErrorMessage = "配置键名长度不能超过100个字符")]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        [Required(ErrorMessage = "配置键值不能为空")]
        [MaxLength(500, ErrorMessage = "配置键值长度不能超过500个字符")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int ConfigType { get; set; }

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
    /// 系统配置导入DTO
    /// </summary>
    public class HbtSysConfigImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigImportDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public string ConfigType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 系统配置导出DTO
    /// </summary>
    public class HbtSysConfigExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigExportDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            ConfigType = string.Empty;
            Status = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public string ConfigType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 系统配置模板DTO
    /// </summary>
    public class HbtSysConfigTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigTemplateDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            ConfigType = string.Empty;
            Status = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public string ConfigType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 系统配置状态DTO
    /// </summary>
    public class HbtSysConfigStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigStatusDto()
        {
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public long ConfigId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }
} 