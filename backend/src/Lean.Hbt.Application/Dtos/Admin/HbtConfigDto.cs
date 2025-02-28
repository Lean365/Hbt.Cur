//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtConfigDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 系统配置基础DTO
    /// </summary>
    public class HbtConfigDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        public long Id { get; set; }

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
        public int ConfigBuiltin { get; set; }

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
    public class HbtConfigQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigQueryDto()
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
        public int? ConfigBuiltin { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 系统配置创建DTO
    /// </summary>
    public class HbtConfigCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigCreateDto()
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
        public int ConfigBuiltin { get; set; }

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
    public class HbtConfigUpdateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigUpdateDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public long Id { get; set; }

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
        public int ConfigBuiltin { get; set; }

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
    public class HbtConfigImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigImportDto()
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
        public string ConfigBuiltin { get; set; }

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
    public class HbtConfigExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigExportDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            ConfigBuiltin = string.Empty;
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
        public string ConfigBuiltin { get; set; }

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
    public class HbtConfigTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigTemplateDto()
        {
            ConfigName = string.Empty;
            ConfigKey = string.Empty;
            ConfigValue = string.Empty;
            ConfigBuiltin = string.Empty;
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
        public string ConfigBuiltin { get; set; }

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
    public class HbtConfigStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigStatusDto()
        {
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public HbtStatus Status { get; set; }
    }
}