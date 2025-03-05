//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtDbContext.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 数据库上下文接口
//===================================================================

using SqlSugar;
using System.Data;

namespace Lean.Hbt.Domain.Data;

/// <summary>
/// 数据库上下文接口
/// </summary>
public interface IHbtDbContext
{
    /// <summary>
    /// 获取SqlSugar客户端
    /// </summary>
    SqlSugarScope Client { get; }

    /// <summary>
    /// 开启事务
    /// </summary>
    void BeginTran();

    /// <summary>
    /// 提交事务
    /// </summary>
    void CommitTran();

    /// <summary>
    /// 回滚事务
    /// </summary>
    void RollbackTran();

    /// <summary>
    /// 获取数据库连接
    /// </summary>
    /// <returns>数据库连接</returns>
    IDbConnection GetConnection();
} 