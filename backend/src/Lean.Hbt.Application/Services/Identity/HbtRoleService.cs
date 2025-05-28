//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 角色服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleService : HbtBaseService, IHbtRoleService
    {
        // 角色仓储接口
        private readonly IHbtRepository<HbtRole> _roleRepository;

        // 用户角色仓储接口
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;


        // 角色菜单仓储接口
        private readonly IHbtRepository<HbtRoleMenu> _roleMenuRepository;

        // 角色部门仓储接口
        private readonly IHbtRepository<HbtRoleDept> _roleDeptRepository;

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="roleRepository">角色仓库</param>
        /// <param name="userRoleRepository">用户角色仓库</param>
        /// <param name="roleMenuRepository">角色菜单仓库</param>
        /// <param name="roleDeptRepository">角色部门仓库</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtRoleService(
            IHbtLogger logger,
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository,
            IHbtRepository<HbtRoleDept> roleDeptRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleDeptRepository = roleDeptRepository;
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtRoleDto>> GetListAsync(HbtRoleQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await _roleRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtRoleDto>
            {
                Rows = result.Rows.Adapt<List<HbtRoleDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回角色详细信息</returns>
        /// <exception cref="HbtException">当角色不存在时抛出异常</exception>
        public async Task<HbtRoleDto> GetByIdAsync(long roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new HbtException(L("Identity.Role.NotFound", roleId));

            return role.Adapt<HbtRoleDto>();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        public async Task<long> CreateAsync(HbtRoleCreateDto input)
        {
            // 验证角色名称是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", input.RoleName);

            // 验证角色编码是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", input.RoleKey);

            var role = input.Adapt<HbtRole>();
            return await _roleRepository.CreateAsync(role) > 0 ? role.Id : throw new HbtException(L("Identity.Role.CreateFailed"));
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtRoleUpdateDto input)
        {
            var role = await _roleRepository.GetByIdAsync(input.RoleId)
                ?? throw new HbtException(L("Identity.Role.NotFound", input.RoleId));

            // 验证租户
            // ValidateTenantId(role.TenantId);

            // 验证角色名称是否已存在
            if (role.RoleName != input.RoleName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", input.RoleName, input.RoleId);

            // 验证角色编码是否已存在
            if (role.RoleKey != input.RoleKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", input.RoleKey, input.RoleId);

            input.Adapt(role);
            return await _roleRepository.UpdateAsync(role) > 0;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId)
                ?? throw new HbtException(L("Identity.Role.NotFound", roleId));

            // 禁止删除admin相关角色
            if (role.RoleKey == "admin" || role.RoleKey == "system_admin")
                throw new HbtException("超级管理员角色不可删除！");

            // 检查是否有用户关联
            var hasUsers = await _userRoleRepository.AsQueryable().AnyAsync(x => x.RoleId == roleId);
            if (hasUsers)
                throw new HbtException(L("Identity.Role.HasUsers"));

            // 删除角色及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.RoleId == roleId));
            return await _roleRepository.DeleteAsync(roleId) > 0;
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] roleIds)
        {
            if (roleIds == null || roleIds.Length == 0)
                throw new HbtException(L("Identity.Role.SelectRequired"));

            foreach (var roleId in roleIds)
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role == null) continue;
                // 禁止删除admin相关角色
                if (role.RoleKey == "admin" || role.RoleKey == "system_admin")
                    throw new HbtException($"超级管理员角色不可删除！(ID: {roleId})");
                var hasUsers = await _userRoleRepository.AsQueryable().AnyAsync(x => x.RoleId == roleId);
                if (hasUsers)
                    throw new HbtException(L("Identity.Role.HasUsersWithId", roleId));
            }

            // 删除角色及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => roleIds.Contains(x.RoleId)));
            return await _roleRepository.DeleteRangeAsync(roleIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtRoleTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtRole")
        {
            try
            {
                var importDtos = await HbtExcelHelper.ImportAsync<HbtRoleImportDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new HbtException(L("Identity.Role.ImportEmpty"));

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        var role = item.Adapt<HbtRole>();
                        role.CreateTime = DateTime.Now;
                        role.Status = 0;

                        // 验证角色名称是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", role.RoleName);
                        // 验证角色编码是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", role.RoleKey);

                        await _roleRepository.CreateAsync(role);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("Identity.Role.Log.ImportFailed", ex.Message), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Role.Log.ImportDataFailed"), ex);
                throw new HbtException(L("Identity.Role.ImportFailed"));
            }
        }

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtRoleQueryDto query, string sheetName = "HbtRole")
        {
            try
            {
                var list = await _roleRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<HbtRoleExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "角色数据");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Role.Log.ExportDataFailed"), ex);
                throw new HbtException(L("Identity.Role.ExportFailed"));
            }
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtRoleStatusDto input)
        {
            var role = await _roleRepository.GetByIdAsync(input.RoleId)
                ?? throw new HbtException(L("Identity.Role.NotFound", input.RoleId));

            input.Adapt(role);
            return await _roleRepository.UpdateAsync(role) > 0;
        }

        /// <summary>
        /// 获取角色选项列表
        /// </summary>
        /// <returns>角色选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var roles = await _roleRepository.AsQueryable()
                .Where(r => r.Status == 0)  // 只获取正常状态的角色
                .OrderBy(r => r.OrderNum)
                .Select(r => new HbtSelectOption
                {
                    Label = r.RoleName,
                    Value = r.Id,

                })
                .ToListAsync();
            return roles;
        }

        /// <summary>
        /// 获取角色已分配的部门列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>部门列表</returns>
        public async Task<List<HbtRoleDeptDto>> GetRoleDeptIdsAsync(long roleId)
        {
            var roleDepts = await _roleDeptRepository.GetListAsync(rd => rd.RoleId == roleId && rd.IsDeleted == 0);
            return roleDepts.Adapt<List<HbtRoleDeptDto>>();
        }

        /// <summary>
        /// 获取角色已分配的菜单列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>菜单列表</returns>
        public async Task<List<HbtRoleMenuDto>> GetRoleMenuIdsAsync(long roleId)
        {
            var roleMenus = await _roleMenuRepository.GetListAsync(rm => rm.RoleId == roleId && rm.IsDeleted == 0);
            return roleMenus.Select(rm => new HbtRoleMenuDto
            {
                Id = rm.Id,
                RoleId = rm.RoleId,
                MenuId = rm.MenuId,
                CreateTime = rm.CreateTime,
                CreateBy = rm.CreateBy
            }).ToList();
        }

        /// <summary>
        /// 分配角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuIds">菜单ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateRoleMenusAsync(long roleId, long[] menuIds)
        {
            try
            {
                _logger.Info($"开始分配角色菜单 - 角色ID: {roleId}, 菜单IDs: {string.Join(",", menuIds)}");

                // 1. 获取角色现有关联的菜单（包括已删除的）
                var existingMenus = await _roleMenuRepository.GetListAsync(rm => rm.RoleId == roleId);
                _logger.Info($"角色现有关联菜单数量: {existingMenus.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的菜单列表中）
                var menusToDelete = existingMenus.Where(rm => !menuIds.Contains(rm.MenuId)).ToList();
                if (menusToDelete.Any())
                {
                    _logger.Info($"需要标记删除的菜单关联数量: {menusToDelete.Count}, 菜单IDs: {string.Join(",", menusToDelete.Select(d => d.MenuId))}");
                    foreach (var menu in menusToDelete)
                    {
                        menu.IsDeleted = 1; // 1 表示已删除
                        menu.DeleteBy = _currentUser.UserName;
                        menu.DeleteTime = DateTime.Now;
                        menu.UpdateBy = _currentUser.UserName;
                        menu.UpdateTime = DateTime.Now;
                        await _roleMenuRepository.UpdateAsync(menu);
                    }
                    _logger.Info("菜单关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的菜单列表中且已存在但被标记为删除）
                var menusToRestore = existingMenus.Where(rm => menuIds.Contains(rm.MenuId) && rm.IsDeleted == 1).ToList();
                if (menusToRestore.Any())
                {
                    _logger.Info($"需要恢复的菜单关联数量: {menusToRestore.Count}, 菜单IDs: {string.Join(",", menusToRestore.Select(d => d.MenuId))}");
                    foreach (var menu in menusToRestore)
                    {
                        menu.IsDeleted = 0; // 0 表示未删除
                        menu.DeleteBy = null;
                        menu.DeleteTime = null;
                        menu.UpdateBy = _currentUser.UserName;
                        menu.UpdateTime = DateTime.Now;
                        await _roleMenuRepository.UpdateAsync(menu);
                    }
                    _logger.Info("菜单关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的菜单列表中且不存在任何记录）
                var existingMenuIds = existingMenus.Select(rm => rm.MenuId).ToList();
                var menusToAdd = menuIds.Where(menuId => !existingMenuIds.Contains(menuId))
                    .Select(menuId => new HbtRoleMenu
                    {
                        RoleId = roleId,
                        MenuId = menuId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (menusToAdd.Any())
                {
                    _logger.Info($"需要新增的菜单关联数量: {menusToAdd.Count}, 菜单IDs: {string.Join(",", menusToAdd.Select(d => d.MenuId))}");
                    await _roleMenuRepository.CreateRangeAsync(menusToAdd);
                    _logger.Info("菜单关联新增完成");
                }

                _logger.Info("角色菜单分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色菜单失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配角色用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateRoleUsersAsync(long roleId, long[] userIds)
        {
            try
            {
                _logger.Info($"开始分配角色用户 - 角色ID: {roleId}, 用户IDs: {string.Join(",", userIds)}");

                // 1. 获取角色现有关联的用户（包括已删除的）
                var existingUsers = await _userRoleRepository.GetListAsync(ur => ur.RoleId == roleId);
                _logger.Info($"角色现有关联用户数量: {existingUsers.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
                var usersToDelete = existingUsers.Where(ur => !userIds.Contains(ur.UserId)).ToList();
                if (usersToDelete.Any())
                {
                    _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                    foreach (var user in usersToDelete)
                    {
                        user.IsDeleted = 1; // 1 表示已删除
                        user.DeleteBy = _currentUser.UserName;
                        user.DeleteTime = DateTime.Now;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await _userRoleRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
                var usersToRestore = existingUsers.Where(ur => userIds.Contains(ur.UserId) && ur.IsDeleted == 1).ToList();
                if (usersToRestore.Any())
                {
                    _logger.Info($"需要恢复的用户关联数量: {usersToRestore.Count}, 用户IDs: {string.Join(",", usersToRestore.Select(d => d.UserId))}");
                    foreach (var user in usersToRestore)
                    {
                        user.IsDeleted = 0; // 0 表示未删除
                        user.DeleteBy = null;
                        user.DeleteTime = null;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await _userRoleRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
                var existingUserIds = existingUsers.Select(ur => ur.UserId).ToList();
                var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                    .Select(userId => new HbtUserRole
                    {
                        RoleId = roleId,
                        UserId = userId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (usersToAdd.Any())
                {
                    _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                    await _userRoleRepository.CreateRangeAsync(usersToAdd);
                    _logger.Info("用户关联新增完成");
                }

                _logger.Info("角色用户分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色用户失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateRoleDeptsAsync(long roleId, long[] deptIds)
        {
            try
            {
                _logger.Info($"开始分配角色部门 - 角色ID: {roleId}, 部门IDs: {string.Join(",", deptIds)}");

                // 1. 获取角色现有关联的部门（包括已删除的）
                var existingDepts = await _roleDeptRepository.GetListAsync(rd => rd.RoleId == roleId);
                _logger.Info($"角色现有关联部门数量: {existingDepts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
                var deptsToDelete = existingDepts.Where(rd => !deptIds.Contains(rd.DeptId)).ToList();
                if (deptsToDelete.Any())
                {
                    foreach (var dept in deptsToDelete)
                    {
                        dept.IsDeleted = 1;
                        dept.DeleteBy = _currentUser.UserName;
                        dept.DeleteTime = DateTime.Now;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await _roleDeptRepository.UpdateAsync(dept);
                    }
                }

                // 3. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
                var deptsToRestore = existingDepts.Where(rd => deptIds.Contains(rd.DeptId) && rd.IsDeleted == 1).ToList();
                if (deptsToRestore.Any())
                {
                    foreach (var dept in deptsToRestore)
                    {
                        dept.IsDeleted = 0;
                        dept.DeleteBy = null;
                        dept.DeleteTime = null;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await _roleDeptRepository.UpdateAsync(dept);
                    }
                }

                // 4. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
                var existingDeptIds = existingDepts.Select(rd => rd.DeptId).ToList();
                var deptsToAdd = deptIds.Where(deptId => !existingDeptIds.Contains(deptId))
                    .Select(deptId => new HbtRoleDept
                    {
                        RoleId = roleId,
                        DeptId = deptId,
                        IsDeleted = 0,
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();
                if (deptsToAdd.Any())
                {
                    await _roleDeptRepository.CreateRangeAsync(deptsToAdd);
                }

                _logger.Info("角色部门分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色部门失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 构建角色查询条件
        /// </summary>
        private Expression<Func<HbtRole, bool>> QueryExpression(HbtRoleQueryDto query)
        {
            var exp = Expressionable.Create<HbtRole>();

            if (!string.IsNullOrEmpty(query.RoleName))
                exp.And(x => x.RoleName.Contains(query.RoleName));

            if (!string.IsNullOrEmpty(query.RoleKey))
                exp.And(x => x.RoleKey.Contains(query.RoleKey));

            if (query.Status.HasValue && query.Status.Value != -1)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }
    }
}