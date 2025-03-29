//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBaseEntity.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 实体基类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class HbtBaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false, DefaultValue = "GETDATE()")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新者
        /// </summary>
        [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新者", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDeleted { get; set; } = 0;

        /// <summary>
        /// 删除者
        /// </summary>
        [SugarColumn(ColumnName = "delete_by", ColumnDescription = "删除者", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }
    }
}