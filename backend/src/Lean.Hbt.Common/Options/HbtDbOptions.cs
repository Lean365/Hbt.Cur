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
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 当前租户ID
    /// </summary>
    public long TenantId { get; set; }
} 