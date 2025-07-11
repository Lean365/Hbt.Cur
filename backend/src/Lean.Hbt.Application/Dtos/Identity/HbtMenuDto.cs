//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenuDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 菜单基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            QueryParams = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
            Children = new List<HbtMenuDto>();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [AdaptMember("Id")]
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string QueryParams { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否显示（0显示 1隐藏）
        /// </summary>
        public int Visible { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        public int IsExternal { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public int IsCache { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<HbtMenuDto> Children { get; set; }
    }

    /// <summary>
    /// 菜单查询对象
    /// </summary>
    public class HbtMenuQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? MenuName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否显示（0显示 1隐藏）
        /// </summary>
        public int? Visible { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        public int? MenuType { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        public int? IsExternal { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public int? IsCache { get; set; }
    }

    /// <summary>
    /// 菜单创建对象
    /// </summary>
    public class HbtMenuCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuCreateDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            QueryParams = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>

        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string? QueryParams { get; set; }

        /// <summary>
        /// 是否为外链
        /// </summary>
        public int IsExternal { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public int Visible { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public int IsCache { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 菜单更新对象
    /// </summary>
    public class HbtMenuUpdateDto : HbtMenuCreateDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [AdaptMember("Id")]
        public long MenuId { get; set; }
    }

    /// <summary>
    /// 菜单导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuExportDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            QueryParams = string.Empty;
            Perms = string.Empty;
            Icon = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string QueryParams { get; set; }

        /// <summary>
        /// 是否为外链
        /// </summary>
        public int IsExternal { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public int Visible { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 菜单状态更新对象
    /// </summary>
    public class HbtMenuStatusDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [AdaptMember("Id")]
        public long MenuId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 菜单排序DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuOrderDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [AdaptMember("Id")]
        public long MenuId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 菜单导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuImportDto()
        {
            MenuName = string.Empty;
            TransKey = string.Empty;
            Path = string.Empty;
            Component = string.Empty;
            QueryParams = string.Empty;
            IsExternal = 0;
            MenuType = 0;
            Visible = 0;
            Status = 0;
            Perms = string.Empty;
            Icon = string.Empty;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        [Required(ErrorMessage = "路由参数不能为空")]
        public string QueryParams { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        [Required(ErrorMessage = "是否为外链不能为空")]
        public int IsExternal { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        [Required(ErrorMessage = "菜单类型不能为空")]
        public int MenuType { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        [Required(ErrorMessage = "显示状态不能为空")]
        public int Visible { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "菜单状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [Required(ErrorMessage = "是否缓存不能为空")]
        public int IsCache { get; set; }
    }

    /// <summary>
    /// 菜单导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtMenuTemplateDto()
        {
            MenuName = "系统管理";
            TransKey = "system";
            Path = "/system";
            Component = "Layout";
            QueryParams = string.Empty;
            IsExternal = 0;
            MenuType = 0;
            Visible = 0;
            Status = 0;
            Perms = "identity:*:*";
            Icon = "system";
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string MenuName { get; set; }

        /// <summary>
        /// 翻译Key
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        [Required(ErrorMessage = "路由参数不能为空")]
        public string QueryParams { get; set; }

        /// <summary>
        /// 是否为外链（0否 1是）
        /// </summary>
        [Required(ErrorMessage = "是否为外链不能为空")]
        public int IsExternal { get; set; }

        /// <summary>
        /// 菜单类型（0目录 1菜单 2按钮）
        /// </summary>
        [Required(ErrorMessage = "菜单类型不能为空")]
        public int MenuType { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        [Required(ErrorMessage = "显示状态不能为空")]
        public int Visible { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "菜单状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Perms { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [Required(ErrorMessage = "是否缓存不能为空")]
        public int IsCache { get; set; }
    }
}