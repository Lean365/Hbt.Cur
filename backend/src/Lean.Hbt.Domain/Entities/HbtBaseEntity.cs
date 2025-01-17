//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBaseEntity.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述    : 基础实体类
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public abstract class HbtBaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键ID", ColumnDataType = "bigint")]
        public long Id { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建者ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者ID
        /// </summary>
        [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新者ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除者ID
        /// </summary>
        [SugarColumn(ColumnName = "delete_by", ColumnDescription = "删除者ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除标志（0代表存在 1代表删除）
        /// </summary>
        [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "删除标志（0代表存在 1代表删除）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Remark { get; set; }
    }
} 