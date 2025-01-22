#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictData.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典数据实体
//===================================================================
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Admin
{
    /// <summary>
    /// 字典数据实体
    /// </summary>
    [SugarTable("hbt_sys_dict_data", "字典数据表")]
    public class HbtDictData : HbtBaseEntity
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        [SugarColumn(ColumnName = "dict_label", ColumnDescription = "字典标签", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [SugarColumn(ColumnName = "dict_value", ColumnDescription = "字典键值", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        [SugarColumn(ColumnName = "ext_label", ColumnDescription = "扩展标签", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        [SugarColumn(ColumnName = "ext_value", ColumnDescription = "扩展键值", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TransKey { get; set; }

        /// <summary>
        /// 字典排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "字典排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 样式属性（其他样式扩展）
        /// </summary>
        [SugarColumn(ColumnName = "css_class", ColumnDescription = "样式属性", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        [SugarColumn(ColumnName = "list_class", ColumnDescription = "表格回显样式", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictData()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            OrderNum = 0;
            Status = HbtStatus.Normal;
        }
    }
}