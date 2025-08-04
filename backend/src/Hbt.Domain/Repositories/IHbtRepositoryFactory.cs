//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtRepositoryFactory.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 仓储工厂接口 - 支持多库模式
//===================================================================

namespace Hbt.Cur.Domain.Repositories;

/// <summary>
/// 仓储工厂接口
/// 支持多库模式，为不同数据库提供专门的仓储获取方法
/// </summary>
public interface IHbtRepositoryFactory
{
    /// <summary>
    /// 获取认证数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>认证数据库仓储</returns>
    IHbtRepository<TEntity> GetAuthRepository<TEntity>() where TEntity : class, new();

    /// <summary>
    /// 获取业务数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>业务数据库仓储</returns>
    IHbtRepository<TEntity> GetBusinessRepository<TEntity>() where TEntity : class, new();

    /// <summary>
    /// 获取工作流数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>工作流数据库仓储</returns>
    IHbtRepository<TEntity> GetWorkflowRepository<TEntity>() where TEntity : class, new();

    /// <summary>
    /// 获取代码生成数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>代码生成数据库仓储</returns>
    IHbtRepository<TEntity> GetGeneratorRepository<TEntity>() where TEntity : class, new();
} 