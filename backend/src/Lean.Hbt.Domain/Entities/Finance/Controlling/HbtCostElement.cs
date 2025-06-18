#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCostElement.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 成本要素实体类
//===================================================================

namespace Lean.Hbt.Domain.Entities.Finance.Controlling
{
    /// <summary>
    /// 成本要素实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_controlling_cost_element", "成本要素表")]
    [SugarIndex("ix_cost_element_code", nameof(CostElementCode), OrderByType.Asc, true)]
    public class HbtCostElement : HbtBaseEntity
    {

        /// <summary>
        /// 成本要素编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CostElementCode { get; set; }

        /// <summary>
        /// 成本要素名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_name", ColumnDescription = "成本要素名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CostElementName { get; set; }

        /// <summary>
        /// 成本要素类型
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_type", ColumnDescription = "成本要素类型", ColumnDataType = "int", IsNullable = false)]
        public int CostElementType { get; set; }

        /// <summary>
        /// 上级成本要素ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级成本要素ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级成本要素
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtCostElement? Parent { get; set; }

        /// <summary>
        /// 成本要素分类
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_category", ColumnDescription = "成本要素分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? CostElementCategory { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "计量单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Unit { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }


    }
}