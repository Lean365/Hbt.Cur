// -----------------------------------------------------------------------------
// <copyright file="HbtTrainingRecord.cs" company="Hbt365">
//     Copyright (c) Hbt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>TrainingRecord 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Hrm.Training;

/// <summary>
/// 培训记录实体
/// </summary>
[SugarTable("hbt_hrm_training_record", "培训记录表")]
[SugarIndex("ix_training_record_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_training_record_course", nameof(CourseId), OrderByType.Asc)]
public class HbtTrainingRecord : HbtBaseEntity
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 课程ID
    /// </summary>
    [SugarColumn(ColumnName = "course_id", ColumnDescription = "课程ID", ColumnDataType = "bigint", IsNullable = false)]
    public long CourseId { get; set; }

    /// <summary>
    /// 培训时间
    /// </summary>
    [SugarColumn(ColumnName = "training_time", ColumnDescription = "培训时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime TrainingTime { get; set; }

    /// <summary>
    /// 成绩/结果
    /// </summary>
    [SugarColumn(ColumnName = "result", ColumnDescription = "成绩/结果", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Result { get; set; }


    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.HbtEmployee? Employee { get; set; }

    /// <summary>
    /// 培训课程
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(CourseId))]
    public virtual HbtTrainingCourse? Course { get; set; }
}

