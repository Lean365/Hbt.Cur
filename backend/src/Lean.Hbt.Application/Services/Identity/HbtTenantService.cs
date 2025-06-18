//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Data;
using System.Data.Common;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-20
/// </remarks>
public class HbtTenantService : HbtBaseService, IHbtTenantService
{
    private readonly IHbtRepository<HbtTenant> _repository;
    private readonly IHbtRepository<HbtUserTenant> _userTenantRepository;
    private readonly IHbtRepository<HbtUser> _userRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantService(
        IHbtRepository<HbtTenant> repository,
        IHbtRepository<HbtUserTenant> userTenantRepository,
        IHbtRepository<HbtUser> userRepository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _userTenantRepository = userTenantRepository ?? throw new ArgumentNullException(nameof(userTenantRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }


    /// <summary>
    /// 获取租户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回分页结果</returns>
    public async Task<HbtPagedResult<HbtTenantDto>> GetListAsync(HbtTenantQueryDto query)
    {
        var exp = QueryExpression(query);

        var result = await _repository.GetPagedListAsync(
            exp,
            query.PageIndex,
            query.PageSize,
            x => x.Id,
            OrderByType.Asc);

        return new HbtPagedResult<HbtTenantDto>
        {
            Rows = result.Rows.Adapt<List<HbtTenantDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回租户详情</returns>
    public async Task<HbtTenantDto> GetByIdAsync(long id)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException(L("Common.NotExists"));

        return tenant.Adapt<HbtTenantDto>();
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">租户创建信息</param>
    /// <returns>返回新创建的租户ID</returns>
    public async Task<long> CreateAsync(HbtTenantCreateDto input)
    {
        // 验证字段是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode);
        await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail);

        var tenant = input.Adapt<HbtTenant>();
        var result = await _repository.CreateAsync(tenant);
        if (result > 0)
            _logger.Info(L("Common.AddSuccess"));

        return tenant.Id;
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">租户更新信息</param>
    /// <returns>返回是否更新成功</returns>
    public async Task<bool> UpdateAsync(HbtTenantUpdateDto input)
    {
        var tenant = await _repository.GetByIdAsync(input.TenantId)
            ?? throw new HbtException(L("Common.NotExists"));

        // 验证字段是否已存在
        if (tenant.TenantName != input.TenantName)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", input.TenantName, input.TenantId);
        if (tenant.TenantCode != input.TenantCode)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", input.TenantCode, input.TenantId);
        if (tenant.ContactEmail != input.ContactEmail)
            await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "ContactEmail", input.ContactEmail, input.TenantId);

        input.Adapt(tenant);
        return await _repository.UpdateAsync(tenant) > 0;
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var tenant = await _repository.GetByIdAsync(id)
            ?? throw new HbtException(L("Common.NotExists"));

        return await _repository.DeleteAsync(tenant) > 0;
    }

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] tenantIds)
    {
        if (tenantIds == null || tenantIds.Length == 0)
            throw new HbtException("请选择要删除的租户");

        return await _repository.DeleteRangeAsync(tenantIds.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtTenantTemplateDto>(sheetName);
    }

    /// <summary>
    /// 导入租户数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var tenants = await HbtExcelHelper.ImportAsync<HbtTenantImportDto>(fileStream, sheetName);
            if (!tenants.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var tenant in tenants)
            {
                try
                {
                    if (string.IsNullOrEmpty(tenant.TenantName) || string.IsNullOrEmpty(tenant.TenantCode))
                    {
                        _logger.Warn("导入租户失败: 租户名称或租户编码不能为空");
                        fail++;
                        continue;
                    }

                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantName", tenant.TenantName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_repository, "TenantCode", tenant.TenantCode);

                    var entity = tenant.Adapt<HbtTenant>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = "system";

                    var result = await _repository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入租户失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入租户数据失败", ex);
            throw new HbtException("导入租户数据失败");
        }
    }

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(HbtTenantQueryDto query, string sheetName = "Sheet1")
    {
        try
        {
            var list = await _repository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<HbtTenantExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "租户数据");
        }
        catch (Exception ex)
        {
            _logger.Error("导出租户数据失败", ex);
            throw new HbtException("导出租户数据失败");
        }
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>更新后的租户状态信息</returns>
    public async Task<HbtTenantStatusDto> UpdateStatusAsync(long id, int status)
    {
        var tenant = await _repository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException(L("Common.NotExists"));

        tenant.Status = status;
        var result = await _repository.UpdateAsync(tenant);
        if (result <= 0)
            throw new HbtException(L("Common.UpdateFailed"));

        return new HbtTenantStatusDto
        {
            TenantId = tenant.Id,
            Status = tenant.Status
        };
    }

    /// <summary>
    /// 获取租户选项列表
    /// </summary>
    /// <returns>租户选项列表</returns>
    public async Task<List<HbtSelectOption>> GetOptionsAsync()
    {
        var tenants = await _repository.AsQueryable()
            .Where(t => t.Status == 0)  // 只获取正常状态的租户
            .OrderBy(t => t.Id)
            .Select(t => new HbtSelectOption
            {
                Label = t.TenantName,
                Value = t.Id,
            })
            .ToListAsync();
        return tenants;
    }

    /// <summary>
    /// 测试数据库连接
    /// </summary>
    /// <param name="connectionInfo">数据库连接信息</param>
    /// <returns>连接测试结果</returns>
    public async Task<bool> TestDbConnectionAsync(HbtDbConnectDto connectionInfo)
    {
        try
        {
            // 构建连接字符串
            var connectionString = BuildConnectionString(connectionInfo);
            
            // 创建 SqlSugar 连接配置
            var dbType = connectionInfo.DbType.ToLower() switch
            {
                "mysql" or "mariadb" or "tidb" or "oceanbase" => SqlSugar.DbType.MySql,
                "sqlserver" => SqlSugar.DbType.SqlServer,
                "postgresql" or "gaussdb" or "opengauss" => SqlSugar.DbType.PostgreSQL,
                "oracle" => SqlSugar.DbType.Oracle,
                "sqlite" => SqlSugar.DbType.Sqlite,
                "dm" => SqlSugar.DbType.Dm,
                "kingbasees" => SqlSugar.DbType.PostgreSQL,
                _ => throw new HbtException($"不支持的数据库类型: {connectionInfo.DbType}")
            };

            // 创建 SqlSugar 连接
            using var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = dbType,
                IsAutoCloseConnection = true
            });

            // 测试连接
            return await Task.FromResult(db.Ado.IsValidConnection());
        }
        catch (Exception ex)
        {
            _logger.Error($"测试数据库连接失败: {ex.Message}", ex);
            throw new HbtException($"测试数据库连接失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 构建数据库连接字符串
    /// </summary>
    private static string BuildConnectionString(HbtDbConnectDto info)
    {
        return info.DbType.ToLower() switch
        {
            "mysql" or "mariadb" or "tidb" or "oceanbase" => $"Server={info.Host};Port={info.Port};Database={info.Database};User={info.Username};Password={info.Password};{info.Options}",
            "sqlserver" => $"Server={info.Host},{info.Port};Database={info.Database};User Id={info.Username};Password={info.Password};{info.Options}",
            "postgresql" or "gaussdb" or "opengauss" => $"Host={info.Host};Port={info.Port};Database={info.Database};Username={info.Username};Password={info.Password};{info.Options}",
            "oracle" => $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={info.Host})(PORT={info.Port}))(CONNECT_DATA=(SERVICE_NAME={info.Database})));User Id={info.Username};Password={info.Password};{info.Options}",
            "sqlite" => $"Data Source={info.Database};Version=3;{info.Options}",
            "dm" => $"Server={info.Host};Port={info.Port};Database={info.Database};User={info.Username};Password={info.Password};{info.Options}",
            "kingbasees" => $"Server={info.Host};Port={info.Port};Database={info.Database};User={info.Username};Password={info.Password};{info.Options}",
            _ => throw new HbtException($"不支持的数据库类型: {info.DbType}")
        };
    }

    /// <summary>
    /// 分配租户用户
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AllocateTenantUsersAsync(long tenantId, long[] userIds)
    {
        try
        {
            _logger.Info($"开始分配租户用户 - 租户ID: {tenantId}, 用户IDs: {string.Join(",", userIds)}");

            // 1. 验证租户是否存在且状态正常
            var tenant = await _repository.GetByIdAsync(tenantId);
            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"));
            if (tenant.Status != 0)
                throw new HbtException(L("Identity.Tenant.Disabled"));

            // 2. 获取租户现有关联的用户（包括已删除的）
            var existingUsers = await _userTenantRepository.GetListAsync(ut => ut.TenantId == tenantId);
            _logger.Info($"租户现有关联用户数量: {existingUsers.Count}");

            // 3. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
            var usersToDelete = existingUsers.Where(ut => !userIds.Contains(ut.UserId)).ToList();
            if (usersToDelete.Any())
            {
                _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                foreach (var user in usersToDelete)
                {
                    // 先更新状态为禁用
                    user.Status = 1; // 1 表示禁用状态
                    user.UpdateBy = _currentUser.UserName;
                    user.UpdateTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(user);

                    // 然后再标记删除
                    user.IsDeleted = 1; // 1 表示已删除
                    user.DeleteBy = _currentUser.UserName;
                    user.DeleteTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(user);
                }
                _logger.Info("用户关联状态更新和删除标记完成");
            }

            // 4. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
            var usersToRestore = existingUsers.Where(ut => userIds.Contains(ut.UserId) && ut.IsDeleted == 1).ToList();
            if (usersToRestore.Any())
            {
                _logger.Info($"需要恢复的用户关联数量: {usersToRestore.Count}, 用户IDs: {string.Join(",", usersToRestore.Select(d => d.UserId))}");
                foreach (var user in usersToRestore)
                {
                    // 先恢复状态为正常
                    user.Status = 0; // 0 表示正常状态
                    user.UpdateBy = _currentUser.UserName;
                    user.UpdateTime = DateTime.Now;
                    await _userTenantRepository.UpdateAsync(user);

                    // 然后取消删除标记
                    user.IsDeleted = 0; // 0 表示未删除
                    user.DeleteBy = null;
                    user.DeleteTime = null;
                    await _userTenantRepository.UpdateAsync(user);
                }
                _logger.Info("用户关联状态恢复和删除标记取消完成");
            }

            // 5. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
            var existingUserIds = existingUsers.Select(ut => ut.UserId).ToList();
            var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                .Select(userId => new HbtUserTenant
                {
                    UserId = userId,
                    TenantId = tenantId,
                    Status = 0, // 0 表示正常状态
                    IsDeleted = 0, // 0 表示未删除
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = _currentUser.UserName,
                    UpdateTime = DateTime.Now
                }).ToList();

            if (usersToAdd.Any())
            {
                _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                await _userTenantRepository.CreateRangeAsync(usersToAdd);
                _logger.Info("用户关联新增完成");
            }

            _logger.Info("租户用户分配完成");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"分配租户用户失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取租户下的用户列表
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <returns>用户列表</returns>
    public async Task<List<HbtUserDto>> GetTenantUsersAsync(long tenantId)
    {
        try
        {
            _logger.Info($"获取租户用户列表 - 租户ID: {tenantId}");

            // 1. 验证租户是否存在
            var tenant = await _repository.GetByIdAsync(tenantId);
            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"));

            // 2. 获取租户下的所有用户（包括已删除的）
            var userTenants = await _userTenantRepository.GetListAsync(ut => ut.TenantId == tenantId);

            if (!userTenants.Any())
                return new List<HbtUserDto>();

            // 3. 获取用户详细信息
            var userIds = userTenants.Select(ut => ut.UserId).ToList();
            var users = await _userRepository.GetListAsync(u => userIds.Contains(u.Id));

            // 4. 转换为DTO并返回
            return users.Adapt<List<HbtUserDto>>();
        }
        catch (Exception ex)
        {
            _logger.Error($"获取租户用户列表失败: {ex.Message}", ex);
            throw;
        }

    }
    /// <summary>
    /// 构建租户查询条件
    /// </summary>
    private Expression<Func<HbtTenant, bool>> QueryExpression(HbtTenantQueryDto query)
    {
        var exp = Expressionable.Create<HbtTenant>();

        if (!string.IsNullOrEmpty(query.TenantName))
            exp.And(x => x.TenantName.Contains(query.TenantName));

        if (!string.IsNullOrEmpty(query.TenantCode))
            exp.And(x => x.TenantCode.Contains(query.TenantCode));

        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status.Value);

        return exp.ToExpression();
    }

}