//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbContext.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V.0.0.1
// 描述    : 数据库上下文
//===================================================================

using Lean.Hbt.Domain.Entities.Audit;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Microsoft.Extensions.Options;
using SqlSugar;
using Lean.Hbt.Domain.Entities;
using Lean.Hbt.Infrastructure.Data;  
namespace Lean.Hbt.Infrastructure.Persistence
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class HbtDbContext : IHbtDbContext
    {
        private readonly SqlSugarScope _db;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDbContext(IOptions<ConnectionConfig> options, IHbtLogger logger)
        {
            _logger = logger;
            
            var config = options.Value;
            config.ConfigureExternalServices = new ConfigureExternalServices
            {
                EntityService = (c, p) =>
                {
                    if (c.PropertyType.IsGenericType &&
                        c.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        p.IsIgnore = true;
                    }
                }
            };

            _db = new SqlSugarScope(config);
            
            // SQL日志记录
            _db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.Debug($"SQL: {sql}\nParameters: {_db.Utilities.SerializeObject(pars)}");
            };

            // 添加全局过滤器
            _db.QueryFilter.AddTableFilter<HbtBaseEntity>(it => it.IsDeleted == 0);
        }

        /// <summary>
        /// 获取数据库客户端
        /// </summary>
        public SqlSugarScope Client => _db;

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            try
            {
                _db.Ado.BeginTran();
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
                _db.Ado.CommitTran();
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
            try
            {
                _db.Ado.RollbackTran();
            }
            catch (Exception ex)
            {
                _logger.Error("回滚事务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                // 1.创建数据库(如果不存在)
                _db.DbMaintenance.CreateDatabase();
                _logger.Info("数据库检查/创建成功");

                // 2.创建/更新表
                var entityTypes = new Type[]
                {
                    typeof(HbtUser),
                    typeof(HbtRole),
                    typeof(HbtMenu),
                    typeof(HbtDept),
                    typeof(HbtPost),
                    typeof(HbtUserRole),
                    typeof(HbtUserDept),
                    typeof(HbtUserPost),
                    typeof(HbtRoleMenu),
                    typeof(HbtRoleDept),
                    typeof(HbtTenant),
                    typeof(HbtAuditLog),
                    typeof(HbtLoginLog),
                    typeof(HbtExceptionLogEntity)
                };

                // CodeFirst.InitTables() 会自动处理表的创建和更新
                foreach (var entityType in entityTypes)
                {
                    await Task.Run(() => _db.CodeFirst.InitTables(entityType));
                    _logger.Info($"表 {_db.EntityMaintenance.GetTableName(entityType)} 初始化成功");
                }

                _logger.Info("数据库初始化完成");
            }
            catch (Exception ex)
            {
                _logger.Error("数据库初始化失败", ex);
                throw;
            }
        }
    }
}