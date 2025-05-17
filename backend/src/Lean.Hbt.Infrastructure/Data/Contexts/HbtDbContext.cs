#nullable disable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbContext.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 数据库上下文，负责数据库连接和操作的核心类
//===================================================================

using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Infrastructure.Services.Identity;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 数据库上下文类
    /// 负责管理数据库连接、事务、实体映射和数据库操作
    /// 实现了多租户、软删除、审计日志等核心功能
    /// </summary>
    public class HbtDbContext : IHbtDbContext
    {
        /// <summary>
        /// 数据库列信息比较器
        /// 用于比较数据库列的定义是否相同
        /// </summary>
        private class ColumnInfoComparer : IEqualityComparer<DbColumnInfo>
        {
            /// <summary>
            /// 比较两个数据库列信息是否相等
            /// </summary>
            public bool Equals(DbColumnInfo x, DbColumnInfo y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                return string.Equals(x.DbColumnName, y.DbColumnName, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(x.DataType, y.DataType, StringComparison.OrdinalIgnoreCase) &&
                       x.Length == y.Length &&
                       x.Scale == y.Scale &&
                       x.DecimalDigits == y.DecimalDigits &&
                       x.IsNullable == y.IsNullable &&
                       string.Equals(x.DefaultValue?.ToString(), y.DefaultValue?.ToString(), StringComparison.OrdinalIgnoreCase);
            }

            /// <summary>
            /// 获取数据库列信息的哈希码
            /// </summary>
            public int GetHashCode(DbColumnInfo obj)
            {
                if (obj == null) return 0;
                var hashCode = new HashCode();
                hashCode.Add(obj.DbColumnName?.ToLowerInvariant());
                hashCode.Add(obj.DataType?.ToLowerInvariant());
                hashCode.Add(obj.Length);
                hashCode.Add(obj.Scale);
                hashCode.Add(obj.DecimalDigits);
                hashCode.Add(obj.IsNullable);
                hashCode.Add(obj.DefaultValue?.ToString()?.ToLowerInvariant());
                return hashCode.ToHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is DbColumnInfo other)
                {
                    return Equals(this, other);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return GetType().GetHashCode();
            }
        }

        /// <summary>
        /// SqlSugar数据库客户端实例
        /// </summary>
        private readonly SqlSugarScope _client;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库连接配置选项</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="currentUser">当前用户信息</param>
        public HbtDbContext(IOptions<ConnectionConfig> options, IHbtLogger logger, IHbtCurrentUser currentUser)
        {
            _logger = logger;

            var config = options.Value;
            if (string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new InvalidOperationException("数据库连接字符串不能为空");
            }

            _client = new SqlSugarScope(config);

            // 配置SQL执行日志记录
            _client.Aop.OnLogExecuting = (sql, parameters) =>
            {
                _logger.Debug(sql);
            };

            // 配置实体操作拦截器
            _client.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                if (entityInfo.EntityColumnInfo.IsPrimarykey) return;

                var entity = entityInfo.EntityValue as HbtBaseEntity;
                if (entity != null)
                {
                    if (entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        // 插入操作时设置审计字段
                        entity.CreateBy = currentUser.UserName;
                        entity.CreateTime = DateTime.Now;
                        entity.UpdateBy = currentUser.UserName;
                        entity.UpdateTime = DateTime.Now;
                        entity.IsDeleted = 0;
                    }
                    else if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        // 更新操作时设置审计字段
                        entity.UpdateBy = currentUser.UserName;
                        entity.UpdateTime = DateTime.Now;
                    }
                    else if (entityInfo.OperationType == DataFilterType.DeleteByObject)
                    {
                        // 软删除处理
                        entity.IsDeleted = 1;
                        entity.DeleteBy = currentUser.UserName;
                        entity.DeleteTime = DateTime.Now;
                        // 将删除操作转换为更新操作
                        entityInfo.OperationType = DataFilterType.UpdateByObject;
                    }
                }
            };

            // 添加全局过滤器，过滤已删除数据
            _client.QueryFilter.AddTableFilter<HbtBaseEntity>(it => it.IsDeleted == 0);

            // 初始化租户过滤器
            AddTenantFilter();
            SetTenantId();
        }

        /// <summary>
        /// 获取数据库客户端实例
        /// </summary>
        public SqlSugarScope Client => _client;

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
                // 1.创建数据库(如果不存在)
                if (string.IsNullOrEmpty(_client.Ado.Connection.ConnectionString))
                {
                    throw new InvalidOperationException("数据库连接字符串不能为空");
                }

                await Task.Run(() => _client.DbMaintenance.CreateDatabase());
                _logger.Info("数据库检查/创建成功");

                // 2.获取所有实体类型
                var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName != null && a.FullName.StartsWith("Lean.Hbt"))
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic &&
                               t.Namespace != null &&
                               t.Namespace.StartsWith("Lean.Hbt.Domain.Entities") &&
                               t.BaseType == typeof(HbtBaseEntity))
                    .OrderBy(t => t == typeof(HbtSqlDiffLog) ? 0 : 1) // 确保日志表首先被创建
                    .ToArray();

                // 3.创建/更新表
                foreach (var entityType in entityTypes)
                {
                    var tableName = _client.EntityMaintenance.GetTableName(entityType);
                    var entityInfo = _client.EntityMaintenance.GetEntityInfo(entityType);

                    // 检查表是否存在
                    var isTableExists = await Task.Run(() => _client.DbMaintenance.IsAnyTable(tableName));
                    if (!isTableExists)
                    {
                        _logger.Info($"[表结构] 新建表 {tableName}");
                        await Task.Run(() => _client.CodeFirst.InitTables(entityType));
                        continue;
                    }

                    // 获取数据库中的列信息
                    var dbColumns = await Task.Run(() => _client.DbMaintenance.GetColumnInfosByTableName(tableName));
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
                        _logger.Info($"[表结构] 更新表 {tableName}");
                        await Task.Run(() => _client.CodeFirst.InitTables(entityType));
                    }
                }

                _logger.Info("数据库初始化完成");
            }
            catch (Exception ex)
            {
                _logger.Error("初始化数据库失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 设置租户ID
        /// 在插入数据时自动设置租户ID
        /// </summary>
        private void SetTenantId()
        {
            var currentTenantId = HbtCurrentTenant.CurrentTenantId;
            if (currentTenantId.HasValue)
            {
                _client.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    if (entityInfo.EntityColumnInfo.IsPrimarykey)
                        return;

                    var entity = entityInfo.EntityValue as IHbtTenantEntity;
                    if (entity != null && entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        entity.TenantId = currentTenantId.Value;
                    }
                };
            }
        }

        /// <summary>
        /// 添加租户过滤器
        /// 为所有实现了IHbtTenantEntity接口的实体添加租户过滤
        /// </summary>
        private void AddTenantFilter()
        {
            // 获取所有实现了 IHbtTenantEntity 接口的实体类型
            var tenantEntities = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IHbtTenantEntity).IsAssignableFrom(t))
                .ToList();

            // 为每个实体类型添加租户过滤器
            foreach (var entityType in tenantEntities)
            {
                _client.QueryFilter.AddTableFilter<IHbtTenantEntity>(it => it.TenantId == HbtCurrentTenant.CurrentTenantId);
            }
        }
    }
}