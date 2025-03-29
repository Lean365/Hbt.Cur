#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTable.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成表实体
//===================================================================

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成表实体
/// </summary>
[SugarTable("hbt_gen_table", "代码生成表")]
[SugarIndex("ix_gen_table_name", nameof(TableName), OrderByType.Asc, true)]
[SugarIndex("ix_gen_table_tenant", nameof(TenantId), OrderByType.Asc, nameof(TableName), OrderByType.Asc, true)]
public class HbtGenTable : HbtBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    [SugarColumn(ColumnName = "database_name", ColumnDescription = "数据库名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    [SugarColumn(ColumnName = "table_comment", ColumnDescription = "表描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string TableComment { get; set; } = string.Empty;

    #endregion

    #region 类型信息

    /// <summary>
    /// 实体类名
    /// </summary>
    [SugarColumn(ColumnName = "class_name", ColumnDescription = "实体类名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ClassName { get; set; } = string.Empty;

    /// <summary>
    /// 命名空间
    /// </summary>
    [SugarColumn(ColumnName = "namespace", ColumnDescription = "命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    [SugarColumn(ColumnName = "base_namespace", ColumnDescription = "命名前缀", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// C#类名
    /// </summary>
    [SugarColumn(ColumnName = "csharp_type_name", ColumnDescription = "C#类名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string CsharpTypeName { get; set; } = string.Empty;

    #endregion

    #region 关联信息

    /// <summary>
    /// 关联父表名
    /// </summary>
    [SugarColumn(ColumnName = "parent_table_name", ColumnDescription = "关联父表", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ParentTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    [SugarColumn(ColumnName = "parent_table_fk_name", ColumnDescription = "关联外键", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ParentTableFkName { get; set; }

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 使用的模板
    /// </summary>
    [SugarColumn(ColumnName = "template_type", ColumnDescription = "使用模板", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 生成模块名
    /// </summary>
    [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    [SugarColumn(ColumnName = "function_name", ColumnDescription = "功能名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string Author { get; set; } = string.Empty;

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式
    /// </summary>
    [SugarColumn(ColumnName = "gen_mode", ColumnDescription = "生成方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int GenMode { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    [SugarColumn(ColumnName = "gen_path", ColumnDescription = "存放位置", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 其他生成选项
    /// </summary>
    [SugarColumn(ColumnName = "options", ColumnDescription = "其他选项", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Options { get; set; }

    #endregion

    #region 系统信息

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long TenantId { get; set; } = 0;

    /// <summary>
    /// 租户
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TenantId))]
    public HbtTenant? Tenant { get; set; }

    /// <summary>
    /// 列信息
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtGenColumn.TableId))]
    public List<HbtGenColumn> Columns { get; set; } = new();

    #endregion
} 