#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ss.cs
// 创建者 : ss
// 创建时间: 2025-05-29
// 版本号 : V0.0.1
// 描述   : ss
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities;

/// <summary>
/// ss
/// </summary>
[SugarTable("ss", "ss")]
public class ss : HbtBaseEntity
{
    /// <summary>
    /// s0
    /// </summary>
    [SugarColumn(ColumnName = "Sss0", ColumnDescription = "s0", ColumnDataType = "nvarchar", IsNullable = true)]
    public nvarchar Sss0 { get; set; }
} 