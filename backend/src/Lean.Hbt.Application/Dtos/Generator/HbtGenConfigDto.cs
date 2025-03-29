#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfigDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述    : 代码生成配置DTO
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Dtos.Generator;

/// <summary>
/// 代码生成配置DTO
/// </summary>
public class HbtGenConfigDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
        Columns = new List<HbtGenColumnDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 主表ID
    /// </summary>
    public long? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    [StringLength(100, ErrorMessage = "主表外键长度不能超过100个字符")]
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    [Required(ErrorMessage = "生成模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public long? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public bool EnableQuery { get; set; } = true;

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public bool EnableAdd { get; set; } = true;

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public bool EnableEdit { get; set; } = true;

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public bool EnableDelete { get; set; } = true;

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public bool EnableImport { get; set; } = false;

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public bool EnableExport { get; set; } = true;

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public bool EnableBatchDelete { get; set; } = true;

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public bool EnableBatchExport { get; set; } = false;

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public bool EnableTree { get; set; } = false;

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构父字段长度不能超过100个字符")]
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构子字段长度不能超过100个字符")]
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnDto> Columns { get; set; }
}

/// <summary>
/// 代码生成配置查询DTO
/// </summary>
public class HbtGenConfigQueryDto : HbtPagedQuery
{
    /// <summary>
    /// 表名
    /// </summary>
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string? TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string? ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string? BusinessName { get; set; }

    /// <summary>
    /// 生成类型
    /// </summary>
    public int? GenType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 代码生成配置创建DTO
/// </summary>
public class HbtGenConfigCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigCreateDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnCreateDto>();
    }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 主表ID
    /// </summary>
    public long? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    [StringLength(100, ErrorMessage = "主表外键长度不能超过100个字符")]
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    [Required(ErrorMessage = "生成模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public long? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public bool EnableQuery { get; set; } = true;

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public bool EnableAdd { get; set; } = true;

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public bool EnableEdit { get; set; } = true;

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public bool EnableDelete { get; set; } = true;

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public bool EnableImport { get; set; } = false;

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public bool EnableExport { get; set; } = true;

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public bool EnableBatchDelete { get; set; } = true;

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public bool EnableBatchExport { get; set; } = false;

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public bool EnableTree { get; set; } = false;

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构父字段长度不能超过100个字符")]
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构子字段长度不能超过100个字符")]
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnCreateDto> Columns { get; set; }
}

/// <summary>
/// 代码生成配置更新DTO
/// </summary>
public class HbtGenConfigUpdateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigUpdateDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        Columns = new List<HbtGenColumnUpdateDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    [Required(ErrorMessage = "模块名称不能为空")]
    [StringLength(50, ErrorMessage = "模块名称长度不能超过50个字符")]
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [Required(ErrorMessage = "业务名称不能为空")]
    [StringLength(50, ErrorMessage = "业务名称长度不能超过50个字符")]
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    [Required(ErrorMessage = "包名不能为空")]
    [StringLength(100, ErrorMessage = "包名长度不能超过100个字符")]
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "作者不能为空")]
    [StringLength(50, ErrorMessage = "作者长度不能超过50个字符")]
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    [Required(ErrorMessage = "生成类型不能为空")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 主表ID
    /// </summary>
    public long? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    [StringLength(100, ErrorMessage = "主表外键长度不能超过100个字符")]
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    [Required(ErrorMessage = "生成模板类型不能为空")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public long? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public bool EnableQuery { get; set; } = true;

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public bool EnableAdd { get; set; } = true;

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public bool EnableEdit { get; set; } = true;

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public bool EnableDelete { get; set; } = true;

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public bool EnableImport { get; set; } = false;

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public bool EnableExport { get; set; } = true;

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public bool EnableBatchDelete { get; set; } = true;

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public bool EnableBatchExport { get; set; } = false;

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public bool EnableTree { get; set; } = false;

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构父字段长度不能超过100个字符")]
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    [StringLength(100, ErrorMessage = "树形结构子字段长度不能超过100个字符")]
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 1;

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnUpdateDto> Columns { get; set; }
}

/// <summary>
/// 代码生成配置导入DTO
/// </summary>
public class HbtGenConfigImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigImportDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        GenType = "1";
        TemplateType = "1";
        EnableQuery = "1";
        EnableAdd = "1";
        EnableEdit = "1";
        EnableDelete = "1";
        EnableImport = "0";
        EnableExport = "1";
        EnableBatchDelete = "1";
        EnableBatchExport = "0";
        EnableTree = "0";
        Status = "1";
        Columns = new List<HbtGenColumnImportDto>();
    }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    public string GenType { get; set; }

    /// <summary>
    /// 主表ID
    /// </summary>
    public string? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    public string TemplateType { get; set; }

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public string? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public string EnableQuery { get; set; }

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public string EnableAdd { get; set; }

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public string EnableEdit { get; set; }

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public string EnableDelete { get; set; }

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public string EnableImport { get; set; }

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public string EnableExport { get; set; }

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public string EnableBatchDelete { get; set; }

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public string EnableBatchExport { get; set; }

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public string EnableTree { get; set; }

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnImportDto> Columns { get; set; }
}

/// <summary>
/// 代码生成配置导出DTO
/// </summary>
public class HbtGenConfigExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigExportDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        CreateTime = DateTime.Now;
        Columns = new List<HbtGenColumnExportDto>();
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    public int GenType { get; set; }

    /// <summary>
    /// 主表ID
    /// </summary>
    public long? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    public int TemplateType { get; set; }

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public long? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public bool EnableQuery { get; set; }

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public bool EnableAdd { get; set; }

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public bool EnableEdit { get; set; }

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public bool EnableDelete { get; set; }

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public bool EnableImport { get; set; }

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public bool EnableExport { get; set; }

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public bool EnableBatchDelete { get; set; }

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public bool EnableBatchExport { get; set; }

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public bool EnableTree { get; set; }

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string UpdateBy { get; set; }

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnExportDto> Columns { get; set; }
}

/// <summary>
/// 代码生成配置模板DTO
/// </summary>
public class HbtGenConfigTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenConfigTemplateDto()
    {
        TableName = string.Empty;
        ModuleName = string.Empty;
        BusinessName = string.Empty;
        PackageName = string.Empty;
        Author = string.Empty;
        GenType = "1";
        TemplateType = "1";
        EnableQuery = "1";
        EnableAdd = "1";
        EnableEdit = "1";
        EnableDelete = "1";
        EnableImport = "0";
        EnableExport = "1";
        EnableBatchDelete = "1";
        EnableBatchExport = "0";
        EnableTree = "0";
        Status = "1";
        Columns = new List<HbtGenColumnTemplateDto>();
    }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string BusinessName { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    public string PackageName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 生成类型（1：单表，2：主从表）
    /// </summary>
    public string GenType { get; set; }

    /// <summary>
    /// 主表ID
    /// </summary>
    public string? ParentTableId { get; set; }

    /// <summary>
    /// 主表外键
    /// </summary>
    public string? ParentTableFk { get; set; }

    /// <summary>
    /// 生成模板类型（1：基础模板，2：自定义模板）
    /// </summary>
    public string TemplateType { get; set; }

    /// <summary>
    /// 自定义模板ID
    /// </summary>
    public string? CustomTemplateId { get; set; }

    /// <summary>
    /// 是否生成查询条件
    /// </summary>
    public string EnableQuery { get; set; }

    /// <summary>
    /// 是否生成新增功能
    /// </summary>
    public string EnableAdd { get; set; }

    /// <summary>
    /// 是否生成修改功能
    /// </summary>
    public string EnableEdit { get; set; }

    /// <summary>
    /// 是否生成删除功能
    /// </summary>
    public string EnableDelete { get; set; }

    /// <summary>
    /// 是否生成导入功能
    /// </summary>
    public string EnableImport { get; set; }

    /// <summary>
    /// 是否生成导出功能
    /// </summary>
    public string EnableExport { get; set; }

    /// <summary>
    /// 是否生成批量删除功能
    /// </summary>
    public string EnableBatchDelete { get; set; }

    /// <summary>
    /// 是否生成批量导出功能
    /// </summary>
    public string EnableBatchExport { get; set; }

    /// <summary>
    /// 是否生成树形结构
    /// </summary>
    public string EnableTree { get; set; }

    /// <summary>
    /// 树形结构父字段
    /// </summary>
    public string? TreeParentField { get; set; }

    /// <summary>
    /// 树形结构子字段
    /// </summary>
    public string? TreeChildField { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 列配置列表
    /// </summary>
    public List<HbtGenColumnTemplateDto> Columns { get; set; }
} 