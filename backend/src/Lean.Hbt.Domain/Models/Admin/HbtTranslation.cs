using SqlSugar;

namespace Lean.Hbt.Domain.Models.Admin
{
    /// <summary>
    /// 翻译实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [SugarTable("hbt_translation")]
    public class HbtTranslation
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        [SugarColumn(Length = 10)]
        public string LangCode { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(Length = 100)]
        public string LangKey { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>
        [SugarColumn(Length = 500)]
        public string LangValue { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
} 