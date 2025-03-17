//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSelectOption.cs
// 创建者 : Lean365
// 创建时间: 2024-03-21 10:30
// 版本号 : V0.0.1
// 描述   : 下拉选择框选项
//===================================================================

namespace Lean.Hbt.Common.Models
{
    /// <summary>
    /// 下拉选择框选项
    /// </summary>
    public class HbtSelectOption
    {
        /// <summary>
        /// 选项标签
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// 选项值
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }
    }
} 