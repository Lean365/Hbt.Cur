// -----------------------------------------------------------------------------
// <copyright file="HbtSalaryStructure.cs" company="Hbt365">
//     Copyright (c) Hbt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>SalaryStructure 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Human.Salary;

/// <summary>
/// 薪资结构实体
/// </summary>
[SugarTable("hbt_hrm_salary_structure", "薪资结构表")]
[SugarIndex("ix_salary_structure_code", nameof(StructureCode), OrderByType.Asc, true)]
public class HbtSalaryStructure : HbtBaseEntity
{
    /// <summary>
    /// 结构编码
    /// </summary>
    [SugarColumn(ColumnName = "structure_code", ColumnDescription = "结构编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string StructureCode { get; set; } = string.Empty;

    /// <summary>
    /// 结构名称
    /// </summary>
    [SugarColumn(ColumnName = "structure_name", ColumnDescription = "结构名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string StructureName { get; set; } = string.Empty;

    /// <summary>
    /// 结构描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "结构描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }
}
