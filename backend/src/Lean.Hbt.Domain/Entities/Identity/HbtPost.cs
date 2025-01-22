#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPost.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:45
// 版本号 : V.0.0.1
// 描述    : 岗位实体类
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 岗位实体
    /// </summary>
    [SugarTable("hbt_post", "岗位表")]
    [SugarIndex("ix_post_code", nameof(PostCode), OrderByType.Asc, true)]
    [SugarIndex("ix_tenant_post", nameof(TenantId), OrderByType.Asc, nameof(PostCode), OrderByType.Asc, true)]
    public class HbtPost : HbtBaseEntity
    {
        /// <summary>
        /// 岗位编码
        /// </summary>
        [SugarColumn(ColumnName = "post_code", ColumnDescription = "岗位编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string PostCode { get; set; } = string.Empty;

        /// <summary>
        /// 岗位名称
        /// </summary>
        [SugarColumn(ColumnName = "post_name", ColumnDescription = "岗位名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string PostName { get; set; } = string.Empty;

        /// <summary>
        /// 显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; } = HbtStatus.Normal;

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 用户岗位关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserPost.PostId))]
        public List<HbtUserPost>? UserPosts { get; set; }
    }
}