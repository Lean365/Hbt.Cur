#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenConfig.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成配置实体
//===================================================================

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成配置实体
/// </summary>
[SugarTable("hbt_generator_config", "代码生成配置表")]
[SugarIndex("ix_gen_config_name", nameof(GenConfigName), OrderByType.Asc, true)]
[SugarIndex("ix_gen_config_tenant", nameof(TenantId), OrderByType.Asc, nameof(GenConfigName), OrderByType.Asc, true)]
public class HbtGenConfig : HbtBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(ColumnName = "gen_config_name", ColumnDescription = "配置名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string GenConfigName { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string Author { get; set; } = string.Empty;

    #endregion

    #region 模块配置

    /// <summary>
    /// 模块名
    /// </summary>
    [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 包名
    /// </summary>
    [SugarColumn(ColumnName = "package_name", ColumnDescription = "包名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string PackageName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名
    /// </summary>
    [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名
    /// </summary>
    [SugarColumn(ColumnName = "function_name", ColumnDescription = "功能名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string FunctionName { get; set; } = string.Empty;

    #endregion

    #region 生成配置

    /// <summary>
    /// 生成类型
    /// </summary>
    [SugarColumn(ColumnName = "gen_type", ColumnDescription = "生成类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int GenType { get; set; } = 1;

    /// <summary>
    /// 模板选用方式（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）
    /// </summary>
    [SugarColumn(ColumnName = "gen_template_type", ColumnDescription = "模板选用方式（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int GenTemplateType { get; set; } = 0;

    /// <summary>
    /// 生成路径
    /// </summary>
    [SugarColumn(ColumnName = "gen_path", ColumnDescription = "生成路径", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 选项配置
    /// </summary>
    [SugarColumn(ColumnName = "options", ColumnDescription = "选项配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    #endregion

    #region 系统信息

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TenantId))]
    public HbtTenant? Tenant { get; set; }

    #endregion
}