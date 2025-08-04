#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译实体
//===================================================================



#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslation.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译实体
//===================================================================
using Hbt.Cur.Domain.Entities.Identity;

namespace Hbt.Cur.Domain.Entities.Routine.Core
{
    /// <summary>
    /// 翻译实体
    /// </summary>
    [SugarTable("hbt_routine_core_translation", "翻译信息")]
    public class HbtTranslation : HbtBaseEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 语言代码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        [SugarColumn(ColumnName = "trans_value", ColumnDescription = "翻译值", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransValue { get; set; } = string.Empty;
        
        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
                
        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;


    }
}