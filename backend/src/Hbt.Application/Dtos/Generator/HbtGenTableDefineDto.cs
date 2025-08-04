#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefineDto.cs
// 创建者 : Claude
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述   : 代码生成表定义DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Hbt.Cur.Application.Dtos.Generator;

/// <summary>
/// 代码生成表定义DTO
/// </summary>
public class HbtGenTableDefineDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineDto()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenTableDefineId { get; set; }

    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    [StringLength(500, ErrorMessage = "连接字符串长度不能超过500个字符")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    public int IsGenTable { get; set; } = 0;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

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

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义查询DTO
/// </summary>
public class HbtGenTableDefineQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 表名
    /// </summary>
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string? TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string? TableComment { get; set; }

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    public int? IsGenTable { get; set; }
}

/// <summary>
/// 代码生成表定义创建DTO
/// </summary>
public class HbtGenTableDefineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineCreateDto()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    [StringLength(500, ErrorMessage = "连接字符串长度不能超过500个字符")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(200, ErrorMessage = "备注长度不能超过200个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义更新DTO
/// </summary>
public class HbtGenTableDefineUpdateDto : HbtGenTableDefineCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键不能为空")]
    [AdaptMember("Id")]
    public long GenTableDefineId { get; set; }
}

/// <summary>
/// 代码生成表定义导入DTO
/// </summary>
public class HbtGenTableDefineImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineImportDto()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    [StringLength(500, ErrorMessage = "连接字符串长度不能超过500个字符")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    public int IsGenTable { get; set; } = 0;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义导出DTO
/// </summary>
public class HbtGenTableDefineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineExportDto()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    [StringLength(500, ErrorMessage = "连接字符串长度不能超过500个字符")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    public int IsGenTable { get; set; } = 0;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义模板DTO
/// </summary>
public class HbtGenTableDefineTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineTemplateDto()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        TableName = string.Empty;
        TableComment = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnDefineTemplateDto>();
    }

    /// <summary>
    /// 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite）
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    [StringLength(500, ErrorMessage = "连接字符串长度不能超过500个字符")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    [StringLength(100, ErrorMessage = "数据库名称长度不能超过100个字符")]
    public string DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    [Required(ErrorMessage = "表描述不能为空")]
    [StringLength(200, ErrorMessage = "表描述长度不能超过200个字符")]
    public string TableComment { get; set; }

    /// <summary>
    /// 是否生成数据表（0：未生成，1：已生成）
    /// </summary>
    public int IsGenTable { get; set; } = 0;

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 字段定义列表
    /// </summary>
    public List<HbtGenColumnDefineTemplateDto> Columns { get; set; }
}

/// <summary>
/// 代码生成表定义初始化DTO
/// </summary>
public class HbtGenTableDefineInitializeDto
{
    /// <summary>
    /// 表定义ID
    /// </summary>
    [Required(ErrorMessage = "表定义ID不能为空")]
    public long GenTableDefineId { get; set; }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public int DbType { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [Required(ErrorMessage = "连接字符串不能为空")]
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 数据库名称
    /// </summary>
    [Required(ErrorMessage = "数据库名称不能为空")]
    public string DatabaseName { get; set; } = string.Empty;
}