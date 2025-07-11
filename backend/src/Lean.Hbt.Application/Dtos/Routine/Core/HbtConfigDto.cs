//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtConfigDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.Core
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
            Remark = string.Empty;
        }

        /// <summary>
        /// 配置ID
        /// </summary>
        [AdaptMember("Id")]
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
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

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
            ConfigValue = string.Empty;
        }

        /// <summary>
        /// 配置名称
        /// </summary>

        public string? ConfigName { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>

        public string? ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>

        public string? ConfigValue { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>

        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>

        public int IsEncrypted { get; set; } = 0;
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
            Remark = string.Empty;
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
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 系统配置更新DTO
    /// </summary>
    public class HbtConfigUpdateDto : HbtConfigCreateDto
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [AdaptMember("Id")]
        public long ConfigId { get; set; }
    }

    /// <summary>
    /// 系统配置导入DTO
    /// </summary>
    public class HbtConfigImportDto : HbtBaseEntity
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
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 重写Id属性，使其不可设置
        /// </summary>
        public new long Id { get; }
    }

    /// <summary>
    /// 系统配置导出DTO
    /// </summary>
    public class HbtConfigExportDto : HbtBaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigExportDto()
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
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 系统配置模板DTO
    /// </summary>
    public class HbtConfigTemplateDto : HbtBaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigTemplateDto()
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
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 系统配置状态DTO
    /// </summary>
    public class HbtConfigStatusDto
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [AdaptMember("Id")]
        public long ConfigId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }
}