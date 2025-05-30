#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtidentitySsss.cs
// 创建者 : admin
// 创建时间: 2025-05-30
// 版本号 : V0.0.1
// 描述   : ssss
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities;

/// <summary>
/// ssss
/// </summary>
[SugarTable("hbtidentityssss", "ssss")]
public class HbtidentitySsss : HbtBaseEntity
{
    /// <summary>
    /// sssss
    /// </summary>
    [SugarColumn(
        ColumnName = "Ssss", 
        ColumnDescription = "sssss", 
        ColumnDataType = "nvarchar", 
        IsNullable = true,
        Length = 64)]
    public nvarchar Ssss { get; set; }
    /// <summary>
    /// wrer
    /// </summary>
    [SugarColumn(
        ColumnName = "Rtwr", 
        ColumnDescription = "wrer", 
        ColumnDataType = "decimal", 
        IsNullable = false,
        Length = 18,
        DecimalDigits = 5)]
    public decimal Rtwr { get; set; }
    /// <summary>
    /// sss
    /// </summary>
    [SugarColumn(
        ColumnName = "Dddate", 
        ColumnDescription = "sss", 
        ColumnDataType = "datetime", 
        IsNullable = true)]
    public datetime Dddate { get; set; }
} 