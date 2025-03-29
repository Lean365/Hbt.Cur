//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 18:40
// 版本号 : V0.0.1
// 描述   : 数据库配置选项
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Common.Options;

/// <summary>
/// 数据库配置选项
/// </summary>
public class HbtDbOptions
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public DbType DbType { get; set; }

    /// <summary>
    /// 是否启用差异日志
    /// </summary>
    public bool EnableDiffLogEvent { get; set; }

    /// <summary>
    /// 最大连接池大小
    /// </summary>
    public int MaxPoolSize { get; set; }

    /// <summary>
    /// 最小连接池大小
    /// </summary>
    public int MinPoolSize { get; set; }

    /// <summary>
    /// 连接超时时间
    /// </summary>
    public int ConnectionTimeout { get; set; }

    /// <summary>
    /// 命令超时时间
    /// </summary>
    public int CommandTimeout { get; set; }

    /// <summary>
    /// 初始化选项
    /// </summary>
    public HbtDbInitOptions Init { get; set; } = new();

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// 当前租户ID
    /// </summary>
    public long TenantId { get; set; }
}

/// <summary>
/// 数据库初始化选项
/// </summary>
public class HbtDbInitOptions
{
    /// <summary>
    /// 是否初始化数据库
    /// </summary>
    public bool InitDatabase { get; set; }

    /// <summary>
    /// 是否初始化种子数据
    /// </summary>
    public bool InitSeedData { get; set; }
}