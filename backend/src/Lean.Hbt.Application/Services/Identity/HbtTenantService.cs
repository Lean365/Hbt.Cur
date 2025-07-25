//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务实现 - 使用仓储工厂模式
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.Repositories;
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
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class HbtTenantService : HbtBaseService, IHbtTenantService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtTenantService(
        IHbtLogger logger,
        IHbtRepositoryFactory repositoryFactory,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取租户仓储
    /// </summary>
    private IHbtRepository<HbtTenant> TenantRepository => _repositoryFactory.GetAuthRepository<HbtTenant>();

    /// <summary>
    /// 获取用户仓储
    /// </summary>
    private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();

    /// <summary>
    /// 获取用户租户关联仓储
    /// </summary>
    private IHbtRepository<HbtUserTenant> UserTenantRepository => _repositoryFactory.GetAuthRepository<HbtUserTenant>();

    /// <summary>
    /// 获取租户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>返回分页结果</returns>
    public async Task<HbtPagedResult<HbtTenantDto>> GetListAsync(HbtTenantQueryDto query)
    {
        var exp = QueryExpression(query);

        var result = await TenantRepository.GetPagedListAsync(
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
        var tenant = await TenantRepository.GetByIdAsync(id);
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
        await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantName", input.TenantName);
        await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantCode", input.TenantCode);
        await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "ContactEmail", input.ContactEmail);

        var tenant = input.Adapt<HbtTenant>();
        var result = await TenantRepository.CreateAsync(tenant);
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
        var tenant = await TenantRepository.GetByIdAsync(input.TenantId)
            ?? throw new HbtException(L("Common.NotExists"));

        // 验证字段是否已存在
        if (tenant.TenantName != input.TenantName)
            await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantName", input.TenantName, input.TenantId);
        if (tenant.TenantCode != input.TenantCode)
            await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantCode", input.TenantCode, input.TenantId);
        if (tenant.ContactEmail != input.ContactEmail)
            await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "ContactEmail", input.ContactEmail, input.TenantId);

        input.Adapt(tenant);
        return await TenantRepository.UpdateAsync(tenant) > 0;
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>返回是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var tenant = await TenantRepository.GetByIdAsync(id)
            ?? throw new HbtException(L("Common.NotExists"));

        return await TenantRepository.DeleteAsync(tenant) > 0;
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

        return await TenantRepository.DeleteRangeAsync(tenantIds.Cast<object>().ToList()) > 0;
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

                    await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantName", tenant.TenantName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(TenantRepository, "TenantCode", tenant.TenantCode);

                    var entity = tenant.Adapt<HbtTenant>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = "system";

                    var result = await TenantRepository.CreateAsync(entity);
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
            var list = await TenantRepository.GetListAsync(QueryExpression(query));
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
        var tenant = await TenantRepository.GetByIdAsync(id);
        if (tenant == null)
            throw new HbtException(L("Common.NotExists"));

        tenant.Status = status;
        var result = await TenantRepository.UpdateAsync(tenant);
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
        try
        {
            _logger.Info("[租户服务] 开始获取租户选项列表");
            
            // 在登录前获取租户列表时，需要查询所有租户
            // 这里直接使用数据库查询，不应用租户过滤，因为此时还没有租户上下文
            var tenants = await TenantRepository.SqlSugarClient.Queryable<HbtTenant>()
                .Where(t => t.IsDeleted == 0 && t.Status == 0)
                .ToListAsync();
            
            var options = tenants.Select(t => new HbtSelectOption
            {
                Label = t.TenantName,
                Value = t.ConfigId
            }).ToList();
            
            _logger.Info("[租户服务] 租户选项列表获取成功，共 {Count} 个租户", options.Count);
            return options;
        }
        catch (Exception ex)
        {
            _logger.Error("[租户服务] 获取租户选项列表失败: {Message}", ex.Message);
            throw new HbtException($"获取租户选项列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 根据用户名获取租户选项列表
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>租户选项列表</returns>
    public async Task<List<HbtSelectOption>> GetOptionsByUserNameAsync(string userName)
    {
        try
        {
            _logger.Info($"根据用户名获取租户选项列表 - 用户名: {userName}");

            if (string.IsNullOrEmpty(userName))
            {
                _logger.Warn("用户名不能为空");
                return new List<HbtSelectOption>();
            }

            // 1. 根据用户名查找用户
            var user = await UserRepository.GetFirstAsync(u => u.UserName == userName && u.IsDeleted == 0);
            if (user == null)
            {
                _logger.Warn($"未找到用户: {userName}");
                return new List<HbtSelectOption>();
            }

            // 2. 获取用户关联的租户
            var userTenants = await UserTenantRepository.GetListAsync(ut => 
                ut.UserId == user.Id && 
                ut.IsDeleted == 0); // 只获取正常状态的关联

            if (!userTenants.Any())
            {
                _logger.Info($"用户 {userName} 没有关联的租户");
                return new List<HbtSelectOption>();
            }

            // 3. 获取租户详细信息
            var configIds = userTenants.Select(ut => ut.ConfigId).Where(c => !string.IsNullOrEmpty(c)).ToList();
            var tenants = await TenantRepository.SqlSugarClient.Queryable<HbtTenant>()
                .Where(t => 
                    configIds.Contains(t.ConfigId) && 
                    t.IsDeleted == 0 && 
                    t.Status == 0) // 只获取正常状态的租户
                .ToListAsync();

            // 4. 转换为选项列表
            var options = tenants.Select(t => new HbtSelectOption
            {
                Label = t.TenantName,
                Value = t.ConfigId
            }).ToList();

            _logger.Info($"用户 {userName} 的租户选项列表获取成功，共 {options.Count} 个租户");
            return options;
        }
        catch (Exception ex)
        {
            _logger.Error($"根据用户名获取租户选项列表失败: {ex.Message}", ex);
            throw new HbtException($"获取租户选项列表失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 根据用户ID获取租户选项列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>租户选项列表</returns>
    public async Task<List<HbtSelectOption>> GetOptionsByUserIdAsync(long userId)
    {
        try
        {
            _logger.Info($"根据用户ID获取租户选项列表 - 用户ID: {userId}");

            if (userId <= 0)
            {
                _logger.Warn("用户ID无效");
                return new List<HbtSelectOption>();
            }

            // 1. 验证用户是否存在
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null || user.IsDeleted == 1)
            {
                _logger.Warn($"未找到用户或用户已删除: {userId}");
                return new List<HbtSelectOption>();
            }

            // 2. 获取用户关联的租户
            var userTenants = await UserTenantRepository.GetListAsync(ut => 
                ut.UserId == userId && 
                ut.IsDeleted == 0); // 只获取未删除的关联

            if (!userTenants.Any())
            {
                _logger.Info($"用户ID {userId} 没有关联的租户");
                return new List<HbtSelectOption>();
            }

            // 3. 获取租户详细信息
            var configIds = userTenants.Select(ut => ut.ConfigId).Where(c => !string.IsNullOrEmpty(c)).ToList();
            var tenants = await TenantRepository.SqlSugarClient.Queryable<HbtTenant>()
                .Where(t => 
                    configIds.Contains(t.ConfigId) && 
                    t.IsDeleted == 0 && 
                    t.Status == 0) // 只获取正常状态的租户
                .ToListAsync();

            // 4. 转换为选项列表
            var options = tenants.Select(t => new HbtSelectOption
            {
                Label = t.TenantName,
                Value = t.ConfigId
            }).ToList();

            _logger.Info($"用户ID {userId} 的租户选项列表获取成功，共 {options.Count} 个租户");
            return options;
        }
        catch (Exception ex)
        {
            _logger.Error($"根据用户ID获取租户选项列表失败: {ex.Message}");
            throw new HbtException($"获取租户选项列表失败: {ex.Message}");
        }
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

            var tenant = await TenantRepository.GetByIdAsync(tenantId);
            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"));
            if (tenant.Status != 0)
                throw new HbtException(L("Identity.Tenant.Disabled"));

            // 2. 获取租户现有关联的用户（包括已删除的）
            var existingUsers = await UserTenantRepository.GetListAsync(ut => ut.ConfigId == tenant.ConfigId);
            _logger.Info($"租户现有关联用户数量: {existingUsers.Count}");

            // 3. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
            var usersToDelete = existingUsers.Where(ut => !userIds.Contains(ut.UserId)).ToList();
            if (usersToDelete.Any())
            {
                _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                foreach (var user in usersToDelete)
                {
                    // 标记删除
                    user.IsDeleted = 1; // 1 表示已删除
                    user.DeleteBy = _currentUser.UserName;
                    user.DeleteTime = DateTime.Now;
                    await UserTenantRepository.UpdateAsync(user);
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
                    // 取消删除标记
                    user.IsDeleted = 0; // 0 表示未删除
                    user.DeleteBy = null;
                    user.DeleteTime = null;
                    await UserTenantRepository.UpdateAsync(user);
                }
                _logger.Info("用户关联状态恢复和删除标记取消完成");
            }

            // 5. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
            var existingUserIds = existingUsers.Select(ut => ut.UserId).ToList();
            var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                .Select(userId => new HbtUserTenant
                {
                    UserId = userId,
                    ConfigId = tenant.ConfigId,
                    IsDeleted = 0, // 0 表示未删除
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = _currentUser.UserName,
                    UpdateTime = DateTime.Now
                }).ToList();

            if (usersToAdd.Any())
            {
                _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                await UserTenantRepository.CreateRangeAsync(usersToAdd);
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

            var tenant = await TenantRepository.GetByIdAsync(tenantId);
            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"));

            // 2. 获取租户下的所有用户（包括已删除的）
            var userTenants = await UserTenantRepository.GetListAsync(ut => ut.ConfigId == tenant.ConfigId);

            if (!userTenants.Any())
                return new List<HbtUserDto>();

            // 3. 获取用户详细信息
            var userIds = userTenants.Select(ut => ut.UserId).ToList();
            var users = await UserRepository.GetListAsync(u => userIds.Contains(u.Id));

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
    /// 获取用户关联的租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    public async Task<List<HbtUserTenantDto>> GetUserTenantsAsync(long userId)
    {
        try
        {
            _logger.Info($"获取用户租户列表 - 用户ID: {userId}");

            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            // 2. 获取用户关联的租户
            var userTenants = await UserTenantRepository.GetListAsync(ut => 
                ut.UserId == userId && 
                ut.IsDeleted == 0); // 只获取未删除的关联

            if (!userTenants.Any())
                return new List<HbtUserTenantDto>();

            // 3. 获取租户详细信息
            var configIds = userTenants.Select(ut => ut.ConfigId).Where(c => !string.IsNullOrEmpty(c)).ToList();
            var tenants = await TenantRepository.GetListAsync(t => 
                configIds.Contains(t.ConfigId) && 
                t.IsDeleted == 0 && 
                t.Status == 0); // 只获取正常状态的租户

            // 4. 转换为DTO并返回
            var result = new List<HbtUserTenantDto>();
            foreach (var userTenant in userTenants)
            {
                var tenant = tenants.FirstOrDefault(t => t.ConfigId == userTenant.ConfigId);
                if (tenant != null)
                {
                    result.Add(new HbtUserTenantDto
                    {
                        UserTenantId = userTenant.Id,
                        UserId = userTenant.UserId,
                        ConfigId = userTenant.ConfigId,
                        CreateTime = userTenant.CreateTime,
                        CreateBy = userTenant.CreateBy,
                        UpdateTime = userTenant.UpdateTime,
                        UpdateBy = userTenant.UpdateBy,
                        Remark = userTenant.Remark,
                        TenantName = tenant.TenantName
                    });
                }
            }

            _logger.Info($"用户ID {userId} 的租户列表获取成功，共 {result.Count} 个租户");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"获取用户租户列表失败: {ex.Message}", ex);
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