//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDbContext.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 工作流数据库上下文
//===================================================================

using Hbt.Domain.Data;
using Hbt.Domain.Entities.Workflow;
using Hbt.Domain.Interfaces;
using Hbt.Common.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.Reflection;

namespace Hbt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 工作流数据库上下文类
    /// 负责管理工作流数据库连接、事务、实体映射和数据库操作
    /// </summary>
    public class HbtWorkflowDbContext : IHbtWorkflowDbContext
    {
        /// <summary>
        /// SqlSugar数据库客户端实例
        /// </summary>
        private readonly SqlSugarScope _client;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 配置接口
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 数据库配置选项
        /// </summary>
        private readonly HbtDbOptions _dbOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbOptions">数据库配置选项</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="currentUser">当前用户信息</param>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="configuration">配置接口</param>
        public HbtWorkflowDbContext(string connectionString, IOptions<HbtDbOptions> dbOptions, IHbtLogger logger, IHbtCurrentUser currentUser, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _dbOptions = dbOptions.Value;

            var config = new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            _client = new SqlSugarScope(config);
        }

        /// <summary>
        /// 获取数据库客户端实例
        /// </summary>
        public SqlSugarScope Client => _client;

        /// <summary>
        /// 获取Ado对象
        /// </summary>
        public IAdo Ado => _client.Ado;

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public void BeginTran()
        {
            try
            {
                _client.Ado.BeginTran();
            }
            catch (Exception ex)
            {
                _logger.Error("开启事务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                _client.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _logger.Error("提交事务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public void RollbackTran()
        {
            Client.RollbackTran();
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接对象</returns>
        public IDbConnection GetConnection()
        {
            return Client.Ado.Connection;
        }

        /// <summary>
        /// 初始化数据库
        /// 包括创建数据库、创建表、更新表结构等操作
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                _logger.Info("开始初始化工作流数据库");

                // 初始化工作流数据库
                await InitializeWorkflowDatabaseAsync();

                _logger.Info("工作流数据库初始化完成");
            }
            catch (Exception ex)
            {
                _logger.Error("初始化工作流数据库失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 初始化工作流数据库
        /// </summary>
        private async Task InitializeWorkflowDatabaseAsync()
        {
            try
            {
                _logger.Info("开始初始化工作流数据库");

                // 1.创建数据库(如果不存在)
                if (string.IsNullOrEmpty(_client.Ado.Connection.ConnectionString))
                {
                    throw new InvalidOperationException("数据库连接字符串不能为空");
                }

                await Task.Run(() => _client.DbMaintenance.CreateDatabase());
                _logger.Info("工作流数据库检查/创建成功");

                // 2.获取所有Workflow实体类型
                var entityTypes = GetWorkflowEntityTypes();

                // 3.创建/更新表
                await CreateOrUpdateTablesAsync(entityTypes, "工作流数据库");

                _logger.Info("工作流数据库初始化完成");
            }
            catch (Exception ex)
            {
                _logger.Error("初始化工作流数据库失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取所有Workflow实体类型
        /// </summary>
        private Type[] GetWorkflowEntityTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.StartsWith("Lean.Hbt"))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic &&
                           t.Namespace != null &&
                           t.Namespace.StartsWith("Hbt.Domain.Entities.Workflow") &&
                           t.BaseType == typeof(HbtBaseEntity))
                .ToArray();
        }

        /// <summary>
        /// 创建或更新表
        /// </summary>
        private async Task CreateOrUpdateTablesAsync(Type[] entityTypes, string databaseName, ISqlSugarClient? client = null)
        {
            var dbClient = client ?? _client;

            foreach (var entityType in entityTypes)
            {
                try
                {
                    var tableName = dbClient.EntityMaintenance.GetTableName(entityType);
                    var entityInfo = dbClient.EntityMaintenance.GetEntityInfo(entityType);

                    // 检查表是否存在
                    var isTableExists = await Task.Run(() => dbClient.DbMaintenance.IsAnyTable(tableName));
                    if (!isTableExists)
                    {
                        _logger.Info($"[{databaseName}] 新建表 {tableName}");
                        await Task.Run(() => dbClient.CodeFirst.InitTables(entityType));
                        continue;
                    }

                    // 获取数据库中的列信息
                    var dbColumns = await Task.Run(() => dbClient.DbMaintenance.GetColumnInfosByTableName(tableName));
                    // 获取实体中的列信息
                    var entityColumns = entityInfo.Columns;

                    // 比较列差异
                    var shouldUpdate = false;
                    foreach (var entityColumn in entityColumns)
                    {
                        var dbColumn = dbColumns.FirstOrDefault(x => x.DbColumnName.Equals(entityColumn.DbColumnName, StringComparison.OrdinalIgnoreCase));
                        if (dbColumn == null)
                        {
                            shouldUpdate = true;
                            break;
                        }

                        if (entityColumn.IsNullable != dbColumn.IsNullable ||
                            (entityColumn.Length > 0 && entityColumn.Length != dbColumn.Length))
                        {
                            shouldUpdate = true;
                            break;
                        }
                    }

                    // 更新表结构
                    if (shouldUpdate)
                    {
                        _logger.Info($"[{databaseName}] 更新表 {tableName}");
                        await Task.Run(() => dbClient.CodeFirst.InitTables(entityType));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"处理表 {entityType.Name} 时发生错误: {ex.Message}");
                    throw;
                }
            }
        }
    }
}

/// <summary>
/// 工作流数据库上下文接口
/// </summary>
public interface IHbtWorkflowDbContext
{
    /// <summary>
    /// 获取数据库客户端实例
    /// </summary>
    SqlSugarScope Client { get; }

    /// <summary>
    /// 获取Ado对象
    /// </summary>
    IAdo Ado { get; }

    /// <summary>
    /// 开始数据库事务
    /// </summary>
    void BeginTran();

    /// <summary>
    /// 提交数据库事务
    /// </summary>
    void CommitTran();

    /// <summary>
    /// 回滚数据库事务
    /// </summary>
    void RollbackTran();

    /// <summary>
    /// 获取数据库连接
    /// </summary>
    /// <returns>数据库连接对象</returns>
    IDbConnection GetConnection();

    /// <summary>
    /// 初始化数据库
    /// </summary>
    Task InitializeAsync();
} 