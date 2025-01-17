//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtDbContext.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 数据库上下文
//===================================================================

using SqlSugar;
using Microsoft.Extensions.Configuration;
using Lean.Hbt.Infrastructure.Logging;

namespace Lean.Hbt.Infrastructure.Persistence
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtDbContext
    {
        /// <summary>
        /// SqlSugar客户端
        /// </summary>
        private readonly ISqlSugarClient _db;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <param name="logger">日志服务</param>
        public HbtDbContext(IConfiguration configuration, IHbtLogger logger)
        {
            _logger = logger;

            // 创建数据库配置
            var connectionConfig = new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.SqlServer,
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (property, column) =>
                    {
                        // 驼峰命名转下划线
                        if (column.IsPrimarykey == false)
                        {
                            column.DbColumnName = UtilMethods.ToUnderLine(property.Name);
                        }
                    }
                }
            };

            // 创建数据库对象
            _db = new SqlSugarClient(connectionConfig);

            // 配置SQL执行日志
            _db.Aop.OnLogExecuting = (sql, parameters) =>
            {
                _logger.Debug($"SQL语句: {sql}");
                if (parameters?.Length > 0)
                {
                    _logger.Debug($"参数: {string.Join(", ", parameters)}");
                }
            };

            // 配置SQL执行异常日志
            _db.Aop.OnError = (ex) =>
            {
                _logger.Error(ex, "SQL执行异常");
            };
        }

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        public ISqlSugarClient DB => _db;

        /// <summary>
        /// 获取仓储对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>仓储对象</returns>
        public SimpleClient<T> GetRepository<T>() where T : class, new()
        {
            return _db.GetSimpleClient<T>();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="action">事务操作</param>
        /// <returns>是否成功</returns>
        public bool UseTran(Action action)
        {
            try
            {
                _db.Ado.BeginTran();
                action();
                _db.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "事务执行异常");
                _db.Ado.RollbackTran();
                return false;
            }
        }

        /// <summary>
        /// 异步开启事务
        /// </summary>
        /// <param name="action">事务操作</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UseTranAsync(Func<Task> action)
        {
            try
            {
                await _db.Ado.BeginTranAsync();
                await action();
                await _db.Ado.CommitTranAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "事务执行异常");
                await _db.Ado.RollbackTranAsync();
                return false;
            }
        }
    }
} 