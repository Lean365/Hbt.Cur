//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenu.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 菜单实体类
//===================================================================

using Lean.Hbt.Common.Enums;
using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 菜单实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_menu", "菜单表")]
    public class HbtMenu : HbtBaseEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(ColumnName = "menu_name", ColumnDescription = "菜单名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译Key", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父菜单ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [SugarColumn(ColumnName = "path", ColumnDescription = "路由地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        [SugarColumn(ColumnName = "component", ColumnDescription = "组件路径", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Component { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        [SugarColumn(ColumnName = "is_frame", ColumnDescription = "是否为外链（0否 1是）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtYesNo IsFrame { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        [SugarColumn(ColumnName = "menu_type", ColumnDescription = "菜单类型（0目录 1菜单 2按钮）", ColumnDataType = "int", IsNullable = false)]
        public HbtMenuType MenuType { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        [SugarColumn(ColumnName = "visible", ColumnDescription = "显示状态（0显示 1隐藏）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtVisible Visible { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "菜单状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [SugarColumn(ColumnName = "perms", ColumnDescription = "权限标识", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [SugarColumn(ColumnName = "icon", ColumnDescription = "菜单图标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Icon { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant Tenant { get; set; }

        /// <summary>
        /// 角色菜单关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtRoleMenu.MenuId))]
        public List<HbtRoleMenu> RoleMenus { get; set; }

        /// <summary>
        /// 父菜单导航属性
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtMenu Parent { get; set; }

        /// <summary>
        /// 子菜单导航属性
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<HbtMenu> Children { get; set; }
    }
}