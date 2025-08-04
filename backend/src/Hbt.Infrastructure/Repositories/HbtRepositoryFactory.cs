//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRepositoryFactory.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 仓储工厂实现类 - 支持多库模式
//===================================================================

using Hbt.Cur.Domain.Repositories;
using Hbt.Cur.Infrastructure.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Hbt.Cur.Infrastructure.Repositories;

/// <summary>
/// 仓储工厂实现类
/// 支持多库模式，为不同数据库提供专门的仓储获取方法
/// </summary>
public class HbtRepositoryFactory : IHbtRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public HbtRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取认证数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>认证数据库仓储</returns>
    public IHbtRepository<TEntity> GetAuthRepository<TEntity>() where TEntity : class, new()
    {
        var IdentityDBContext = _serviceProvider.GetRequiredService<HbtIdentityDBContext>();
        var currentUser = _serviceProvider.GetRequiredService<IHbtCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<IHbtLogger>();
        return new HbtRepository<TEntity>(IdentityDBContext.Client, currentUser, logger);
    }

    /// <summary>
    /// 获取业务数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>业务数据库仓储</returns>
    public IHbtRepository<TEntity> GetBusinessRepository<TEntity>() where TEntity : class, new()
    {
        var businessDbContext = _serviceProvider.GetRequiredService<HbtBusinessDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<IHbtCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<IHbtLogger>();
        return new HbtRepository<TEntity>(businessDbContext.Client, currentUser, logger);
    }

    /// <summary>
    /// 获取工作流数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>工作流数据库仓储</returns>
    public IHbtRepository<TEntity> GetWorkflowRepository<TEntity>() where TEntity : class, new()
    {
        var workflowDbContext = _serviceProvider.GetRequiredService<HbtWorkflowDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<IHbtCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<IHbtLogger>();
        return new HbtRepository<TEntity>(workflowDbContext.Client, currentUser, logger);
    }

    /// <summary>
    /// 获取代码生成数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>代码生成数据库仓储</returns>
    public IHbtRepository<TEntity> GetGeneratorRepository<TEntity>() where TEntity : class, new()
    {
        var generatorDbContext = _serviceProvider.GetRequiredService<HbtGeneratorDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<IHbtCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<IHbtLogger>();
        return new HbtRepository<TEntity>(generatorDbContext.Client, currentUser, logger);
    }
} 