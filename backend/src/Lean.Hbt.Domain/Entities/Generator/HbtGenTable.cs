#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTable.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成表实体
//===================================================================

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

namespace Lean.Hbt.Domain.Entities.Generator;

/// <summary>
/// 代码生成表实体
/// </summary>
[SugarTable("hbt_generator_table", "代码生成表")]
[SugarIndex("ix_gen_table_name", nameof(TableName), OrderByType.Asc, true)]
[SugarIndex("ix_gen_table_tenant", nameof(TenantId), OrderByType.Asc, nameof(TableName), OrderByType.Asc, true)]
public class HbtGenTable : HbtBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    [SugarColumn(ColumnName = "database_name", ColumnDescription = "数据库名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    [SugarColumn(ColumnName = "table_comment", ColumnDescription = "表描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 关联父表名
    /// </summary>
    [SugarColumn(ColumnName = "sub_table_name", ColumnDescription = "关联父表", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    [SugarColumn(ColumnName = "sub_table_fk_name", ColumnDescription = "关联外键", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段
    /// </summary>
    [SugarColumn(ColumnName = "tree_code", ColumnDescription = "树编码字段", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TreeCode { get; set; } = string.Empty;

    /// <summary>
    /// 树名称字段
    /// </summary>
    [SugarColumn(ColumnName = "tree_name", ColumnDescription = "树名称字段", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TreeName { get; set; } = string.Empty;

    /// <summary>
    /// 树父编码字段
    /// </summary>
    [SugarColumn(ColumnName = "tree_parent_code", ColumnDescription = "树父编码字段", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TreeParentCode { get; set; } = string.Empty;

    #endregion

    #region 类型信息
        /// <summary>
    /// 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用HbtGenTemplate数据表中的模板）
    /// </summary>
    [SugarColumn(ColumnName = "tpl_type", ColumnDescription = "模板类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "0")]
    public string TplType { get; set; } = "0";

    /// <summary>
    /// 使用的模板（crud单表操作 tree树表操作 sub主子表操作）
    /// </summary>
    [SugarColumn(ColumnName = "tpl_category", ColumnDescription = "使用模板", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "crud")]
    public string TplCategory { get; set; } = "crud";

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    [SugarColumn(ColumnName = "base_namespace", ColumnDescription = "命名前缀", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体命名空间
    /// </summary>
    [SugarColumn(ColumnName = "entity_namespace", ColumnDescription = "实体命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string EntityNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体类名
    /// </summary>
    [SugarColumn(ColumnName = "entity_class_name", ColumnDescription = "实体类名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象命名空间
    /// </summary>
    [SugarColumn(ColumnName = "dto_namespace", ColumnDescription = "对象命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DtoNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 对象类名
    /// </summary>
    [SugarColumn(ColumnName = "dto_class_name", ColumnDescription = "对象类名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DtoClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象类型
    /// </summary>
    [SugarColumn(ColumnName = "dto_type", ColumnDescription = "对象类型", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DtoType { get; set; } = string.Empty;

    /// <summary>
    /// 服务命名空间
    /// </summary>
    [SugarColumn(ColumnName = "service_namespace", ColumnDescription = "服务命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ServiceNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 服务接口类名称
    /// </summary>
    [SugarColumn(ColumnName = "iservice_class_name", ColumnDescription = "服务接口类名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string IServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类名称
    /// </summary>
    [SugarColumn(ColumnName = "service_class_name", ColumnDescription = "服务类名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    [SugarColumn(ColumnName = "irepository_namespace", ColumnDescription = "仓储接口命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string IRepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    [SugarColumn(ColumnName = "irepository_class_name", ColumnDescription = "仓储接口类名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string IRepositoryClassName { get; set; } = string.Empty;    

    /// <summary>
    /// 仓储命名空间
    /// </summary>
    [SugarColumn(ColumnName = "repository_namespace", ColumnDescription = "仓储命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string RepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储类名称
    /// </summary>
    [SugarColumn(ColumnName = "repository_class_name", ColumnDescription = "仓储类名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string RepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 控制器命名空间
    /// </summary>
    [SugarColumn(ColumnName = "controller_namespace", ColumnDescription = "控制器命名空间", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ControllerNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 控制器类名称
    /// </summary>
    [SugarColumn(ColumnName = "controller_class_name", ColumnDescription = "控制器类名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ControllerClassName { get; set; } = string.Empty;

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 生成模块名
    /// </summary>
    [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string ModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    [SugarColumn(ColumnName = "function_name", ColumnDescription = "功能名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string FunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string Author { get; set; } = string.Empty;

    #endregion

    #region 生成选项

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）
    /// </summary>
    [SugarColumn(ColumnName = "gen_type", ColumnDescription = "生成方式", Length = 1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "0")]
    public string GenType { get; set; } = "0";

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    [SugarColumn(ColumnName = "gen_path", ColumnDescription = "存放位置", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "/")]
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_menu_id", ColumnDescription = "上级菜单ID", ColumnDataType = "bigint", IsNullable = false)]
    public long ParentMenuId { get; set; }

    /// <summary>
    /// 自动生成菜单
    /// </summary>
    [SugarColumn(ColumnName = "generate_menu", ColumnDescription = "自动生成菜单", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int GenerateMenu { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    [SugarColumn(ColumnName = "sort_type", ColumnDescription = "排序类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "asc")]
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    [SugarColumn(ColumnName = "sort_field", ColumnDescription = "排序字段", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    [SugarColumn(ColumnName = "perms_prefix", ColumnDescription = "权限前缀", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PermsPrefix { get; set; } = string.Empty;


    /// <summary>
    /// 前端模板 1、element ui 2、element plus
    /// </summary>
    [SugarColumn(ColumnName = "front_tpl", ColumnDescription = "前端模板", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int FrontTpl { get; set; } = 2;

    /// <summary>
    /// 前端样式 12,24
    /// </summary>
    [SugarColumn(ColumnName = "front_style", ColumnDescription = "前端样式", ColumnDataType = "int", IsNullable = false, DefaultValue = "24")]
    public int FrontStyle { get; set; } = 24;

        /// <summary>
    /// 操作按钮样式
    /// </summary>
    [SugarColumn(ColumnName = "btn_style", ColumnDescription = "操作按钮样式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 代码生成选项
    /// </summary>
    [SugarColumn(ColumnName = "options", ColumnDescription = "代码生成选项", IsJson = true)]
    public CodeOptions? Options { get; set; }

    /// <summary>
    /// 状态（1：停用，0：正常）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    #endregion

    #region 系统信息

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
    /// 列信息
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(HbtGenColumn.TableId))]
    public List<HbtGenColumn> Columns { get; set; } = new();

    /// <summary>
    /// 子表信息
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public HbtGenTable? SubTable { get; set; }

    #endregion
}

/// <summary>
/// 代码生成选项
/// </summary>
public class CodeOptions
{
    /// <summary>
    /// 是否启用SQL差异
    /// </summary>
    public int IsSqlDiff { get; set; } = 1;

    /// <summary>
    /// 是否使用雪花id
    /// </summary>
    public int IsSnowflakeId { get; set; } = 1;

    /// <summary>
    /// 是否生成仓储层
    /// </summary>
    public int IsRepository { get; set; }

    /// <summary>
    /// CRUD功能组
    /// </summary>
    public int[] CrudGroup { get; set; } = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
} 