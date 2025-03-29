//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户服务实现类
//===================================================================

#nullable enable

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class HbtUserService : IHbtUserService
    {
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
        private readonly IHbtRepository<HbtUserPost> _userPostRepository;
        private readonly IHbtRepository<HbtUserDept> _userDeptRepository;
        private readonly IHbtPasswordPolicy _passwordPolicy;
        private readonly IHbtLogger _logger;
        private readonly IHbtLocalizationService _localization;
        private readonly IHbtRepository<HbtTenant> _tenantRepository;
        private readonly IHbtRepository<HbtRole> _roleRepository;
        private readonly IHbtRepository<HbtPost> _postRepository;
        private readonly IHbtRepository<HbtDept> _deptRepository;
        private readonly IHbtTenantContext _tenantContext;
        private readonly IHbtCurrentUser _currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserService(
            IHbtRepository<HbtUser> userRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHbtRepository<HbtUserPost> userPostRepository,
            IHbtRepository<HbtUserDept> userDeptRepository,
            IHbtPasswordPolicy passwordPolicy,
            IHbtLogger logger,
            IHbtLocalizationService localization,
            IHbtRepository<HbtTenant> tenantRepository,
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtPost> postRepository,
            IHbtRepository<HbtDept> deptRepository,
            IHbtTenantContext tenantContext,
            IHbtCurrentUser currentUser)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userPostRepository = userPostRepository;
            _userDeptRepository = userDeptRepository;
            _passwordPolicy = passwordPolicy;
            _logger = logger;
            _localization = localization;
            _tenantRepository = tenantRepository;
            _roleRepository = roleRepository;
            _postRepository = postRepository;
            _deptRepository = deptRepository;
            _tenantContext = tenantContext;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtUserDto>> GetListAsync(HbtUserQueryDto query)
        {
            var exp = Expressionable.Create<HbtUser>();

            if (!string.IsNullOrEmpty(query?.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query?.PhoneNumber))
                exp.And(x => x.PhoneNumber.Contains(query.PhoneNumber));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            if (query?.DeptId.HasValue == true)
                exp.And(x => x.UserDepts != null && x.UserDepts.Any(d => d.DeptId == query.DeptId.Value));

            if (query?.UserType.HasValue == true)
                exp.And(x => x.UserType == query.UserType.Value);

            var result = await _userRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtUserDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtUserDto>>()
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户详情</returns>
        public async Task<HbtUserDto> GetByIdAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 获取用户角色
            var userRoles = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            // 获取用户岗位
            var userPosts = await _userPostRepository.GetListAsync(up => up.UserId == userId);
            var postIds = userPosts.Select(up => up.PostId).ToList();

            // 获取用户部门
            var userDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == userId);
            var deptIds = userDepts.Select(ud => ud.DeptId).ToList();

            var userDto = user.Adapt<HbtUserDto>();
            userDto.RoleIds = roleIds;
            userDto.PostIds = postIds;
            userDto.DeptIds = deptIds;

            return userDto;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">用户创建信息</param>
        /// <returns>返回新创建的用户ID</returns>
        public async Task<long> CreateAsync(HbtUserCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.UserName))
                throw new HbtException(_localization.L("User.Username.Required"));

            // 验证租户是否存在且有效
            var tenant = await _tenantRepository.GetByIdAsync(input.TenantId);
            if (tenant == null)
                throw new HbtException(_localization.L("Tenant.NotFound"));

            if (tenant.Status != 0)
                throw new HbtException(_localization.L("Tenant.Disabled"));

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "UserName", input.UserName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", input.PhoneNumber);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", input.Email);

            // 使用默认密码或验证用户提供的密码
            var password = string.IsNullOrEmpty(input.Password) ? _passwordPolicy.DefaultPassword : input.Password;

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(password))
                throw new HbtException(_localization.L("User.Password.Invalid"));

            // 创建用户
            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(password);
            var user = new HbtUser
            {
                UserName = input.UserName,
                NickName = input.NickName ?? string.Empty,
                EnglishName = input.EnglishName ?? string.Empty,
                UserType = 0,
                Password = hash,
                Salt = salt,
                Iterations = iterations,
                PhoneNumber = input.PhoneNumber ?? string.Empty,
                Email = input.Email ?? string.Empty,
                Gender = 0,
                Avatar = input.Avatar ?? string.Empty,
                Status = 0,
                TenantId = _tenantContext.TenantId,
                Remark = input.Remark ?? string.Empty
            };

            var result = await _userRepository.CreateAsync(user);
            if (result <= 0)
                throw new HbtException(_localization.L("User.Create.Failed"));

            // 关联角色
            if (input.RoleIds?.Any() == true)
            {
                // 验证角色是否属于当前租户
                var roles = await _roleRepository.GetListAsync(r => input.RoleIds.Contains(r.Id) && r.TenantId == input.TenantId);
                if (roles.Count != input.RoleIds.Count)
                    throw new HbtException(_localization.L("User.Role.Invalid"));

                var userRoles = input.RoleIds.Select(roleId => new HbtUserRole
                {
                    UserId = user.Id,
                    RoleId = roleId,
                    TenantId = input.TenantId
                }).ToList();
                await _userRoleRepository.CreateRangeAsync(userRoles);
            }

            // 关联岗位
            if (input.PostIds?.Any() == true)
            {
                // 验证岗位是否属于当前租户
                var posts = await _postRepository.GetListAsync(p => input.PostIds.Contains(p.Id) && p.TenantId == input.TenantId);
                if (posts.Count != input.PostIds.Count)
                    throw new HbtException(_localization.L("User.Post.Invalid"));

                var userPosts = input.PostIds.Select(postId => new HbtUserPost
                {
                    UserId = user.Id,
                    PostId = postId,
                    TenantId = input.TenantId
                }).ToList();
                await _userPostRepository.CreateRangeAsync(userPosts);
            }

            // 关联部门
            // 验证部门是否属于当前租户
            var dept = await _deptRepository.GetByIdAsync(input.DeptId);
            if (dept == null || dept.TenantId != input.TenantId)
                throw new HbtException(_localization.L("User.Dept.Invalid"));

            var userDept = new HbtUserDept
            {
                UserId = user.Id,
                DeptId = input.DeptId,
                TenantId = input.TenantId
            };
            await _userDeptRepository.CreateAsync(userDept);

            _logger.Info(_localization.L("User.Created.Success", input.UserName));
            return user.Id;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">用户更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateAsync(HbtUserUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", input.PhoneNumber, input.UserId);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", input.Email, input.UserId);

            // 更新用户基本信息
            user.NickName = input.NickName ?? string.Empty;
            user.EnglishName = input.EnglishName ?? string.Empty;
            user.PhoneNumber = input.PhoneNumber ?? string.Empty;
            user.Email = input.Email ?? string.Empty;
            user.Gender = input.Gender;
            user.Avatar = input.Avatar ?? string.Empty;
            user.Status = input.Status;
            user.Remark = input.Remark ?? string.Empty;

            var result = await _userRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(_localization.L("User.Update.Failed"));

            // 更新角色关联
            _logger.Info($"[用户角色] 开始更新用户 {user.UserName}(ID:{user.Id}) 的角色关联");
            try
            {
                // 获取现有的角色关联
                var existingRoles = await _userRoleRepository.AsQueryable()
                    .Where(x => x.UserId == user.Id && x.IsDeleted == 0)
                    .ToListAsync();

                var existingRoleIds = existingRoles.Select(x => x.RoleId).ToList();
                var newRoleIds = input.RoleIds ?? new List<long>();

                // 找出需要删除的角色关联（在新列表中不存在的）
                var rolesToDelete = existingRoles.Where(x => !newRoleIds.Contains(x.RoleId)).ToList();
                if (rolesToDelete.Any())
                {
                    await _userRoleRepository.DeleteRangeAsync(rolesToDelete);
                }

                // 找出需要新增的角色关联（在现有列表中不存在的）
                var rolesToAdd = newRoleIds.Where(x => !existingRoleIds.Contains(x))
                    .Select(roleId => new HbtUserRole
                    {
                        UserId = user.Id,
                        RoleId = roleId,
                        TenantId = user.TenantId,
                        CreateTime = DateTime.Now,
                        CreateBy = _currentUser.UserName ?? "Hbt365",
                        IsDeleted = 0
                    }).ToList();

                if (rolesToAdd.Any())
                {
                    await _userRoleRepository.CreateRangeAsync(rolesToAdd);
                }

                _logger.Info($"[用户角色] 更新成功: 删除 {rolesToDelete.Count} 个角色, 新增 {rolesToAdd.Count} 个角色");
            }
            catch (Exception ex)
            {
                _logger.Error($"[用户角色] 更新失败: {ex.Message}");
                _logger.Error($"[用户角色] 异常详情: {ex}");
                throw;
            }

            // 更新岗位关联
            _logger.Info($"[用户岗位] 开始更新用户 {user.UserName}(ID:{user.Id}) 的岗位关联");
            try
            {
                // 获取现有的岗位关联
                var existingPosts = await _userPostRepository.AsQueryable()
                    .Where(x => x.UserId == user.Id && x.IsDeleted == 0)
                    .ToListAsync();

                var existingPostIds = existingPosts.Select(x => x.PostId).ToList();
                var newPostIds = input.PostIds ?? new List<long>();

                // 找出需要删除的岗位关联（在新列表中不存在的）
                var postsToDelete = existingPosts.Where(x => !newPostIds.Contains(x.PostId)).ToList();
                if (postsToDelete.Any())
                {
                    await _userPostRepository.DeleteRangeAsync(postsToDelete);
                }

                // 找出需要新增的岗位关联（在现有列表中不存在的）
                var postsToAdd = newPostIds.Where(x => !existingPostIds.Contains(x))
                    .Select(postId => new HbtUserPost
                    {
                        UserId = user.Id,
                        PostId = postId,
                        TenantId = user.TenantId,
                        CreateTime = DateTime.Now,
                        CreateBy = _currentUser.UserName ?? "Hbt365",
                        IsDeleted = 0
                    }).ToList();

                if (postsToAdd.Any())
                {
                    await _userPostRepository.CreateRangeAsync(postsToAdd);
                }

                _logger.Info($"[用户岗位] 更新成功: 删除 {postsToDelete.Count} 个岗位, 新增 {postsToAdd.Count} 个岗位");
            }
            catch (Exception ex)
            {
                _logger.Error($"[用户岗位] 更新失败: {ex.Message}");
                _logger.Error($"[用户岗位] 异常详情: {ex}");
                throw;
            }

            // 更新部门关联
            _logger.Info($"[用户部门] 开始更新用户 {user.UserName}(ID:{user.Id}) 的部门关联");
            try
            {
                // 获取现有的部门关联
                var existingDepts = await _userDeptRepository.AsQueryable()
                    .Where(x => x.UserId == user.Id && x.IsDeleted == 0)
                    .ToListAsync();

                var existingDeptIds = existingDepts.Select(x => x.DeptId).ToList();
                var newDeptIds = input.DeptIds ?? new List<long>();

                // 找出需要删除的部门关联（在新列表中不存在的）
                var deptsToDelete = existingDepts.Where(x => !newDeptIds.Contains(x.DeptId)).ToList();
                if (deptsToDelete.Any())
                {
                    await _userDeptRepository.DeleteRangeAsync(deptsToDelete);
                }

                // 找出需要新增的部门关联（在现有列表中不存在的）
                var deptsToAdd = newDeptIds.Where(x => !existingDeptIds.Contains(x))
                    .Select(deptId => new HbtUserDept
                    {
                        UserId = user.Id,
                        DeptId = deptId,
                        TenantId = user.TenantId,
                        CreateTime = DateTime.Now,
                        CreateBy = _currentUser.UserName ?? "Hbt365",
                        IsDeleted = 0
                    }).ToList();

                if (deptsToAdd.Any())
                {
                    await _userDeptRepository.CreateRangeAsync(deptsToAdd);
                }

                _logger.Info($"[用户部门] 更新成功: 删除 {deptsToDelete.Count} 个部门, 新增 {deptsToAdd.Count} 个部门");
            }
            catch (Exception ex)
            {
                _logger.Error($"[用户部门] 更新失败: {ex.Message}");
                _logger.Error($"[用户部门] 异常详情: {ex}");
                throw;
            }

            _logger.Info(_localization.L("User.Updated.Success", user.UserName));
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> DeleteAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 删除用户关联数据
            Expression<Func<HbtUserRole, bool>> roleExp = x => x.UserId == userId;
            Expression<Func<HbtUserPost, bool>> postExp = x => x.UserId == userId;
            Expression<Func<HbtUserDept, bool>> deptExp = x => x.UserId == userId;

            await _userRoleRepository.DeleteAsync(roleExp);
            await _userPostRepository.DeleteAsync(postExp);
            await _userDeptRepository.DeleteAsync(deptExp);

            // 删除用户
            var result = await _userRepository.DeleteAsync(userId);

            _logger.Info(_localization.L("User.Deleted.Success", user.UserName));
            return result > 0;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                throw new HbtException(_localization.L("User.BatchDelete.Empty"));

            // 删除用户关联数据
            Expression<Func<HbtUserRole, bool>> batchRoleExp = x => userIds.Contains(x.UserId);
            Expression<Func<HbtUserPost, bool>> batchPostExp = x => userIds.Contains(x.UserId);
            Expression<Func<HbtUserDept, bool>> batchDeptExp = x => userIds.Contains(x.UserId);

            await _userRoleRepository.DeleteAsync(batchRoleExp);
            await _userPostRepository.DeleteAsync(batchPostExp);
            await _userDeptRepository.DeleteAsync(batchDeptExp);

            // 删除用户
            var result = await _userRepository.DeleteRangeAsync(userIds.Cast<object>().ToList());

            _logger.Info(_localization.L("User.BatchDeleted.Success", string.Join(",", userIds)));
            return result > 0;
        }

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var users = await _userRepository.AsQueryable()
                .Where(u => u.Status == 0)  // 只获取正常状态的用户
                .OrderBy(u => u.UserName)
                .Select(u => new HbtSelectOption
                {
                    Label = u.UserName,
                    Value = u.Id,
                    Disabled = false
                })
                .ToListAsync();
            return users;
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入成功和失败的数量</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            var users = await HbtExcelHelper.ImportAsync<HbtUserImportDto>(fileStream, sheetName);
            if (users == null || !users.Any())
                return (0, 0);

            int success = 0;
            int fail = 0;

            foreach (var user in users)
            {
                try
                {
                    if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                    {
                        _logger.Warn(_localization.L("User.Import.Empty"));
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在
                    try
                    {
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "UserName", user.UserName);
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", user.PhoneNumber == null ? string.Empty : user.PhoneNumber);
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", user.Email == null ? string.Empty : user.Email);
                    }
                    catch (HbtException ex)
                    {
                        _logger.Warn($"{_localization.L("User.Import.Failed")}: {ex.Message}");
                        fail++;
                        continue;
                    }

                    // 创建用户
                    var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(user.Password);
                    var newUser = new HbtUser
                    {
                        UserName = user.UserName,
                        NickName = user.NickName ?? string.Empty,
                        EnglishName = user.EnglishName ?? string.Empty,
                        UserType = int.Parse(user.UserType),
                        Password = hash,
                        Salt = salt,
                        Iterations = iterations,
                        PhoneNumber = user.PhoneNumber ?? string.Empty,
                        Email = user.Email ?? string.Empty,
                        Gender = 0,
                        Avatar = user.Avatar ?? string.Empty,
                        Status = 0,
                        TenantId = _tenantContext.TenantId,
                        Remark = user.Remark ?? string.Empty
                    };

                    await _userRepository.CreateAsync(newUser);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error($"{_localization.L("User.Import.Failed")}: {ex.Message}", ex);
                    fail++;
                }
            }

            _logger.Info(_localization.L("User.Imported.Success", success, fail));
            return (success, fail);
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtUserQueryDto query, string sheetName = "Sheet1")
        {
            var exp = Expressionable.Create<HbtUser>();

            if (!string.IsNullOrEmpty(query?.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query?.PhoneNumber))
                exp.And(x => x.PhoneNumber.Contains(query.PhoneNumber));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            if (query?.DeptId.HasValue == true)
                exp.And(x => x.UserDepts != null && x.UserDepts.Any(d => d.DeptId == query.DeptId.Value));

            if (query?.UserType.HasValue == true)
                exp.And(x => x.UserType == query.UserType.Value);

            var users = await _userRepository.GetListAsync(exp.ToExpression());
            var exportData = users.Adapt<List<HbtUserExportDto>>();

            return await HbtExcelHelper.ExportAsync(exportData, sheetName);
        }

        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtUserTemplateDto>(sheetName);
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="input">用户状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtUserStatusDto input)
        {
            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            user.Status = input.Status;
            var result = await _userRepository.UpdateAsync(user);

            _logger.Info(_localization.L("User.Status.Updated.Success", user.UserName, input.Status));
            return result > 0;
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">密码重置信息</param>
        /// <returns>返回是否重置成功</returns>
        public async Task<bool> ResetPasswordAsync(HbtUserResetPwdDto input)
        {
            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(input.Password))
                throw new HbtException(_localization.L("User.Password.Invalid"));

            // 重置密码
            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(input.Password);
            user.Password = hash;
            user.Salt = salt;

            var result = await _userRepository.UpdateAsync(user);

            _logger.Info(_localization.L("User.Password.Reset.Success", user.UserName));
            return result > 0;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">密码修改信息</param>
        /// <returns>返回是否修改成功</returns>
        public async Task<bool> ChangePasswordAsync(HbtUserChangePwdDto input)
        {
            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 验证旧密码
            if (!HbtPasswordUtils.VerifyHash(input.OldPassword, user.Password, user.Salt, user.Iterations))
                throw new HbtException(_localization.L("User.Password.Old.Invalid"));

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(input.NewPassword))
                throw new HbtException(_localization.L("User.Password.Invalid"));

            // 修改密码
            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(input.NewPassword);
            user.Password = hash;
            user.Salt = salt;

            var result = await _userRepository.UpdateAsync(user);

            _logger.Info(_localization.L("User.Password.Changed.Success", user.UserName));
            return result > 0;
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="input">解锁用户信息</param>
        /// <returns>返回是否解锁成功</returns>
        public async Task<bool> UnlockUserAsync(HbtUserUnlockDto input)
        {
            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            // 验证租户权限
            if (user.TenantId != _tenantContext.TenantId)
                throw new HbtException(_localization.L("User.Tenant.Invalid"));

            // 更新用户锁定状态
            user.IsLock = input.IsLock;
            user.ErrorLimit = input.ErrorLimit;
            user.LoginCount = 0; // 重置登录错误次数
            user.Remark = input.Remark;

            var result = await _userRepository.UpdateAsync(user);

            _logger.Info(_localization.L("User.Unlocked.Success", user.UserName));
            return result > 0;
        }

        public async Task<HbtUserDto> GetUserByNameAsync(string userName)
        {
            var user = await _userRepository.GetInfoAsync(x => x.UserName == userName);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"), HbtConstants.ErrorCodes.NotFound);

            return user.Adapt<HbtUserDto>();
        }

        public async Task<HbtUserDto> GetUserByPhoneAsync(string phoneNumber)
        {
            var user = await _userRepository.GetInfoAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"), HbtConstants.ErrorCodes.NotFound);

            return user.Adapt<HbtUserDto>();
        }
    }
}