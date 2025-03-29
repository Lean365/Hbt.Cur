#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTemplate.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成模板实体
//===================================================================

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成模板实体
/// </summary>
[SugarTable("hbt_gen_template", "代码生成模板表")]
[SugarIndex("ix_gen_template_name", nameof(TemplateName), OrderByType.Asc, true)]
[SugarIndex("ix_gen_template_tenant", nameof(TenantId), OrderByType.Asc, nameof(TemplateName), OrderByType.Asc, true)]
public class HbtGenTemplate : HbtBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 模板名称
    /// </summary>
    [SugarColumn(ColumnName = "template_name", ColumnDescription = "模板名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// 模板类型
    /// </summary>
    [SugarColumn(ColumnName = "template_type", ColumnDescription = "模板类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TemplateType { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    [SugarColumn(ColumnName = "template_content", ColumnDescription = "模板内容", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; } = string.Empty;

    /// <summary>
    /// 模板分类
    /// </summary>
    [SugarColumn(ColumnName = "template_category", ColumnDescription = "模板分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言
    /// </summary>
    [SugarColumn(ColumnName = "template_language", ColumnDescription = "编程语言", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    [SugarColumn(ColumnName = "template_version", ColumnDescription = "版本号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; } = 1;
 
    #endregion

    #region 生成配置

    /// <summary>
    /// 生成路径
    /// </summary>
    [SugarColumn(ColumnName = "gen_path", ColumnDescription = "生成路径", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "生成路径不能为空")]
    [StringLength(200, ErrorMessage = "生成路径长度不能超过200个字符")]
    public string GenPath { get; set; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "文件名不能为空")]
    [StringLength(100, ErrorMessage = "文件名长度不能超过100个字符")]
    public string FileName { get; set; } = string.Empty;

    #endregion

    #region 状态信息

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;

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

    #endregion
} 