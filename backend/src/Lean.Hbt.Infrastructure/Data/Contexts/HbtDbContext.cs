#nullable disable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbContext.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 数据库上下文
//===================================================================

using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.RealTime;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Infrastructure.Services.Identity;
using Microsoft.Extensions.Options;

namespace Lean.Hbt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class HbtDbContext : IHbtDbContext
    {
        private class ColumnInfoComparer : IEqualityComparer<DbColumnInfo>
        {
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

        private readonly SqlSugarScope _client;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDbContext(IOptions<ConnectionConfig> options, IHbtLogger logger, IHbtUserContext userContext)
        {
            _logger = logger;

            var config = options.Value;
            if (string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new InvalidOperationException("数据库连接字符串不能为空");
            }

            _client = new SqlSugarScope(config);

            // SQL日志记录
            _client.Aop.OnLogExecuting = (sql, parameters) =>
            {
                _logger.Debug(sql);
            };

            // 添加实体插入Aop
            _client.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                if (entityInfo.EntityColumnInfo.IsPrimarykey) return;
                
                var entity = entityInfo.EntityValue as HbtBaseEntity;
                if (entity != null)
                {
                    if (entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        entity.CreateBy = userContext.UserName;
                        entity.CreateTime = DateTime.Now;
                        entity.UpdateBy = userContext.UserName;
                        entity.UpdateTime = DateTime.Now;
                        entity.IsDeleted = 0;
                    }
                    else if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        entity.UpdateBy = userContext.UserName;
                        entity.UpdateTime = DateTime.Now;
                    }
                    else if (entityInfo.OperationType == DataFilterType.DeleteByObject)
                    {
                        // 软删除处理
                        entity.IsDeleted = 1;
                        entity.DeleteBy = userContext.UserName;
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
        /// 获取数据库客户端
        /// </summary>
        public SqlSugarScope Client => _client;

        /// <summary>
        /// 开始事务
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
        /// 提交事务
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
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            Client.RollbackTran();
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IDbConnection GetConnection()
        {
            return Client.Ado.Connection;
        }

        /// <summary>
        /// 初始化数据库
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
                
                _client.DbMaintenance.CreateDatabase();
                _logger.Info("数据库检查/创建成功");

                // 2.获取所有实体类型
                var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName != null && a.FullName.StartsWith("Lean.Hbt"))
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && 
                               t.Namespace != null && 
                               t.Namespace.StartsWith("Lean.Hbt.Domain.Entities") &&
                               t.BaseType == typeof(HbtBaseEntity))
                    .OrderBy(t => t == typeof(HbtDbDiffLog) ? 0 : 1) // 确保日志表首先被创建
                    .ToArray();

                // 3.创建/更新表
                foreach (var entityType in entityTypes)
                {
                    var tableName = _client.EntityMaintenance.GetTableName(entityType);
                    var entityInfo = _client.EntityMaintenance.GetEntityInfo(entityType);

                    // 检查表是否存在
                    var isTableExists = _client.DbMaintenance.IsAnyTable(tableName);
                    if (!isTableExists)
                    {
                        _logger.Info($"[表结构] 新建表 {tableName}");
                        _client.CodeFirst.InitTables(entityType);
                        continue;
                    }

                    // 获取数据库中的列信息
                    var dbColumns = _client.DbMaintenance.GetColumnInfosByTableName(tableName);
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
                        _client.CodeFirst.InitTables(entityType);
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
        /// 获取数据库中的列信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetTableNameFromSql(string sql)
        {
            // 简单的SQL解析，获取表名
            var match = System.Text.RegularExpressions.Regex.Match(sql, @"(?:INSERT\s+INTO|UPDATE|DELETE\s+FROM)\s+([^\s\(]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value.Trim('[', ']', '`') : "Unknown";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetChangeTypeFromSql(string sql)
        {
            if (sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                return "Insert";
            if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase))
                return "Update";
            if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                return "Delete";
            return "Unknown";
        }

        private bool IsDataChangeOperation(string sql)
        {
            sql = sql.TrimStart();
            return sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) ||
                   sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                   sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase);
        }

        private long? GetCurrentTenantId()
        {
            try
            {
                // 从当前上下文获取租户ID
                return HbtTenantContext.CurrentTenantId;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 设置租户ID
        /// </summary>
        private void SetTenantId()
        {
            var currentTenantId = HbtTenantContext.CurrentTenantId;
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
                _client.QueryFilter.AddTableFilter<IHbtTenantEntity>(it => it.TenantId == HbtTenantContext.CurrentTenantId);
            }
        }

        private bool IsTypeEquivalent(string dbType, string entityType)
        {
            // 定义类型映射关系
            var typeMap = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "varchar", new[] { "string", "String" } },
                { "nvarchar", new[] { "string", "String" } },
                { "int", new[] { "int", "Int32" } },
                { "bigint", new[] { "long", "Int64" } },
                { "bit", new[] { "bool", "Boolean" } },
                { "datetime", new[] { "DateTime" } },
                { "decimal", new[] { "decimal", "Decimal" } }
            };

            // 检查类型是否等效
            foreach (var map in typeMap)
            {
                if (dbType.StartsWith(map.Key, StringComparison.OrdinalIgnoreCase) &&
                    map.Value.Contains(entityType, StringComparer.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ShouldCompareLength(string dataType)
        {
            var typesWithLength = new[] { "varchar", "nvarchar", "char", "nchar" };
            return typesWithLength.Any(t => dataType.StartsWith(t, StringComparison.OrdinalIgnoreCase));
        }
    }
}