//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtUserService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户服务实现类
//===================================================================

#nullable enable

using Hbt.Common.Utils;
using Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;

namespace Hbt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class HbtUserService : HbtBaseService, IHbtUserService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private readonly IHbtPasswordPolicy _passwordPolicy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHbtPasswordPolicy passwordPolicy,
            IHbtCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _passwordPolicy = passwordPolicy ?? throw new ArgumentNullException(nameof(passwordPolicy));
        }

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();

        /// <summary>
        /// 获取用户角色仓储
        /// </summary>
        private IHbtRepository<HbtUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<HbtUserRole>();

        /// <summary>
        /// 获取用户岗位仓储
        /// </summary>
        private IHbtRepository<HbtUserPost> UserPostRepository => _repositoryFactory.GetAuthRepository<HbtUserPost>();

        /// <summary>
        /// 获取用户部门仓储
        /// </summary>
        private IHbtRepository<HbtUserDept> UserDeptRepository => _repositoryFactory.GetAuthRepository<HbtUserDept>();

        /// <summary>
        /// 获取用户租户仓储
        /// </summary>
        private IHbtRepository<HbtUserTenant> UserTenantRepository => _repositoryFactory.GetAuthRepository<HbtUserTenant>();

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtUserDto>> GetListAsync(HbtUserQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await UserRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtUserDto>
            {
                Rows = result.Rows.Adapt<List<HbtUserDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户详情</returns>
        public async Task<HbtUserDto> GetByIdAsync(long userId)
        {
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            // 获取用户角色
            var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            // 获取用户岗位
            var userPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId);
            var postIds = userPosts.Select(up => up.PostId).ToList();

            // 获取用户部门
            var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId);
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
                throw new HbtException(L("Identity.User.Username.Required"));

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "UserName", input.UserName);
            await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", input.PhoneNumber);
            await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", input.Email);

            // 使用默认密码或验证用户提供的密码
            var password = string.IsNullOrEmpty(input.Password) ? _passwordPolicy.DefaultPassword : input.Password;

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(password))
                throw new HbtException(L("Identity.User.Password.Invalid"));

            // 创建用户
            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(password);
            var user = input.Adapt<HbtUser>();
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UserType = 0;
            user.Status = 0;
            user.CreateBy = _currentUser.UserName;
            user.CreateTime = DateTime.Now;

            var result = await UserRepository.CreateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.AddFailed"));

            // 添加用户角色关联
            if (input.RoleIds != null && input.RoleIds.Any())
            {
                var userRoles = input.RoleIds.Select(roleId => new HbtUserRole
                {
                    UserId = user.Id,
                    RoleId = roleId,
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserRoleRepository.CreateRangeAsync(userRoles);
            }

            // 添加用户岗位关联
            if (input.PostIds != null && input.PostIds.Any())
            {
                var userPosts = input.PostIds.Select(postId => new HbtUserPost
                {
                    UserId = user.Id,
                    PostId = postId,
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserPostRepository.CreateRangeAsync(userPosts);
            }

            // 添加用户部门关联
            if (input.DeptIds != null && input.DeptIds.Any())
            {
                var userDepts = input.DeptIds.Select(deptId => new HbtUserDept
                {
                    UserId = user.Id,
                    DeptId = deptId,
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserDeptRepository.CreateRangeAsync(userDepts);
            }

            // 添加用户租户关联
            if (input.ConfigIds != null && input.ConfigIds.Any())
            {
                var userTenants = input.ConfigIds.Select(configId => new HbtUserTenant
                {
                    UserId = user.Id,
                    ConfigId = configId,
                    IsDeleted = 0,
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = _currentUser.UserName,
                    UpdateTime = DateTime.Now
                }).ToList();

                await UserTenantRepository.CreateRangeAsync(userTenants);
            }

            _logger.Info(L("Common.AddSuccess"));
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

            var user = await UserRepository.GetByIdAsync(input.UserId)
                ?? throw new HbtException(L("Identity.User.NotFound"));

            // 验证字段是否已存在（排除当前用户）
            if (user.NickName != input.NickName)
                await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "NickName", input.NickName, input.UserId);
            if (user.PhoneNumber != input.PhoneNumber)
                await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", input.PhoneNumber, input.UserId);
            if (user.Email != input.Email)
                await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", input.Email, input.UserId);

            // 更新用户基本信息
            user.NickName = input.NickName;
            user.EnglishName = input.EnglishName;
            user.PhoneNumber = input.PhoneNumber;
            user.Email = input.Email;
            user.Gender = input.Gender;
            user.Avatar = input.Avatar;
            user.Status = input.Status;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            // 更新用户角色关联
            if (input.RoleIds != null)
            {
                var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == user.Id);
                if (userRoles.Any())
                {
                    await UserRoleRepository.DeleteRangeAsync(userRoles);
                }
                if (input.RoleIds.Any())
                {
                    // 过滤掉已存在的角色关联
                    var existingRoleIds = userRoles.Select(ur => ur.RoleId).ToList();
                    var newRoleIds = input.RoleIds.Except(existingRoleIds).ToList();

                    if (newRoleIds.Any())
                    {
                        var newUserRoles = newRoleIds.Select(roleId => new HbtUserRole
                        {
                            UserId = user.Id,
                            RoleId = roleId,
                            CreateBy = _currentUser.UserName,
                            CreateTime = DateTime.Now
                        }).ToList();

                        await UserRoleRepository.CreateRangeAsync(newUserRoles);
                    }
                }
            }

            // 更新用户岗位关联
            if (input.PostIds != null)
            {
                var userPosts = await UserPostRepository.GetListAsync(up => up.UserId == user.Id);
                if (userPosts.Any())
                {
                    await UserPostRepository.DeleteRangeAsync(userPosts);
                }
                if (input.PostIds.Any())
                {
                    // 过滤掉已存在的岗位关联
                    var existingPostIds = userPosts.Select(up => up.PostId).ToList();
                    var newPostIds = input.PostIds.Except(existingPostIds).ToList();

                    if (newPostIds.Any())
                    {
                        var newUserPosts = newPostIds.Select(postId => new HbtUserPost
                        {
                            UserId = user.Id,
                            PostId = postId,
                            CreateBy = _currentUser.UserName,
                            CreateTime = DateTime.Now
                        }).ToList();

                        await UserPostRepository.CreateRangeAsync(newUserPosts);
                    }
                }
            }

            // 更新用户部门关联
            if (input.DeptIds != null)
            {
                var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == user.Id);
                if (userDepts.Any())
                {
                    await UserDeptRepository.DeleteRangeAsync(userDepts);
                }
                if (input.DeptIds.Any())
                {
                    // 过滤掉已存在的部门关联
                    var existingDeptIds = userDepts.Select(ud => ud.DeptId).ToList();
                    var newDeptIds = input.DeptIds.Except(existingDeptIds).ToList();

                    if (newDeptIds.Any())
                    {
                        var newUserDepts = newDeptIds.Select(deptId => new HbtUserDept
                        {
                            UserId = user.Id,
                            DeptId = deptId,
                            CreateBy = _currentUser.UserName,
                            CreateTime = DateTime.Now
                        }).ToList();

                        await UserDeptRepository.CreateRangeAsync(newUserDepts);
                    }
                }
            }

            _logger.Info(L("Common.UpdateSuccess"));
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> DeleteAsync(long userId)
        {
            var user = await UserRepository.GetByIdAsync(userId)
                ?? throw new HbtException(L("Identity.User.NotFound"));

            // 禁止删除admin用户
            if (user.UserName == "admin")
                throw new HbtException("超级管理员账号不可删除！");

            // 更新用户状态为停用
            user.Status = 1;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;
            await UserRepository.UpdateAsync(user);

            // 删除用户角色关联
            var userRoleIds = (await UserRoleRepository.GetListAsync(ur => ur.UserId == userId)).Select(ur => ur.Id).ToList();
            foreach (var id in userRoleIds)
            {
                await UserRoleRepository.DeleteAsync(id);
            }

            // 删除用户岗位关联
            var userPostIds = (await UserPostRepository.GetListAsync(up => up.UserId == userId)).Select(up => up.Id).ToList();
            foreach (var id in userPostIds)
            {
                await UserPostRepository.DeleteAsync(id);
            }

            // 删除用户部门关联
            var userDeptIds = (await UserDeptRepository.GetListAsync(ud => ud.UserId == userId)).Select(ud => ud.Id).ToList();
            foreach (var id in userDeptIds)
            {
                await UserDeptRepository.DeleteAsync(id);
            }

            // 删除用户租户关联
            var userTenantIds = (await UserTenantRepository.GetListAsync(ut => ut.UserId == userId)).Select(ut => ut.Id).ToList();
            foreach (var id in userTenantIds)
            {
                await UserTenantRepository.DeleteAsync(id);
            }

            // 删除用户
            var result = await UserRepository.DeleteAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.DeleteFailed"));

            return true;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                throw new HbtException(L("Identity.User.SelectRequired"));

            foreach (var userId in userIds)
            {
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null) continue;
                // 禁止删除admin用户
                if (user.UserName == "admin")
                    throw new HbtException($"超级管理员账号不可删除！(ID: {userId})");
            }

            // 更新用户状态为停用
            var users = await UserRepository.GetListAsync(u => userIds.Contains(u.Id));
            foreach (var user in users)
            {
                user.Status = 1;
                user.UpdateBy = _currentUser.UserName;
                user.UpdateTime = DateTime.Now;
            }
            await UserRepository.UpdateRangeAsync(users);

            // 删除用户角色关联
            await UserRoleRepository.DeleteAsync((HbtUserRole ur) => userIds.Contains(ur.UserId));

            // 删除用户岗位关联
            await UserPostRepository.DeleteAsync((HbtUserPost up) => userIds.Contains(up.UserId));

            // 删除用户部门关联
            await UserDeptRepository.DeleteAsync((HbtUserDept ud) => userIds.Contains(ud.UserId));

            // 删除用户租户关联
            await UserTenantRepository.DeleteAsync((HbtUserTenant ut) => userIds.Contains(ut.UserId));

            // 删除用户
            return await UserRepository.DeleteRangeAsync(userIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtUserTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var users = await HbtExcelHelper.ImportAsync<HbtUserImportDto>(fileStream, sheetName);
                if (!users.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var user in users)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(user.UserName))
                        {
                            _logger.Warn("导入用户失败: 用户名不能为空");
                            fail++;
                            continue;
                        }

                        // 校验用户名是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "UserName", user.UserName);
                        // 校验手机号是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", user.PhoneNumber);
                        // 校验邮箱是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", user.Email);
                        string ss = _passwordPolicy.DefaultPassword;
                        var entity = user.Adapt<HbtUser>();
                        var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(_passwordPolicy.DefaultPassword);
                        entity.Password = hash;
                        entity.Salt = salt;
                        entity.Iterations = iterations;
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.Status = 0;

                        var result = await UserRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入用户失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入用户数据失败", ex);
                throw new HbtException("导入用户数据失败");
            }
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件或zip文件</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtUserQueryDto query, string sheetName = "Sheet1")
        {
            var list = await UserRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<HbtUserExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "用户数据");
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="input">用户状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtUserStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            user.Status = input.Status;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码信息</param>
        /// <returns>返回是否重置成功</returns>
        public async Task<bool> ResetPasswordAsync(HbtUserResetPwdDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            // 使用默认密码或验证用户提供的密码
            var password = string.IsNullOrEmpty(input.Password) ? _passwordPolicy.DefaultPassword : input.Password;

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(password))
                throw new HbtException(L("Identity.User.Password.Invalid"));

            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(password);
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码信息</param>
        /// <returns>返回是否修改成功</returns>
        public async Task<bool> ChangePasswordAsync(HbtUserChangePwdDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            // 验证旧密码
            if (!HbtPasswordUtils.VerifyHash(input.OldPassword, user.Password, user.Salt, user.Iterations))
                throw new HbtException(L("Identity.User.Password.Incorrect"));

            // 验证新密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(input.NewPassword))
                throw new HbtException(L("Identity.User.Password.Invalid"));

            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(input.NewPassword);
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="input">解锁用户信息</param>
        /// <returns>返回是否解锁成功</returns>
        public async Task<bool> UnlockUserAsync(HbtUserUnlockDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            user.LockEndTime = null;
            user.LockReason = null;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回用户信息</returns>
        public async Task<HbtUserDto> GetUserByNameAsync(string userName)
        {
            var user = await UserRepository.GetFirstAsync(u => u.UserName == userName);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            return user.Adapt<HbtUserDto>();
        }

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns>返回用户信息</returns>
        public async Task<HbtUserDto> GetUserByPhoneAsync(string phoneNumber)
        {
            var user = await UserRepository.GetFirstAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            return user.Adapt<HbtUserDto>();
        }

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var users = await UserRepository.AsQueryable()
                .Where(u => u.Status == 0)  // 只获取正常状态的用户
                .OrderBy(u => u.Id)
                .Select(u => new HbtSelectOption
                {
                    Label = u.NickName,
                    Value = u.Id,
                })
                .ToListAsync();
            return users;
        }

        /// <summary>
        /// 获取用户已分配的角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色列表</returns>
        public async Task<List<HbtUserRoleDto>> GetUserRoleIdsAsync(long userId)
        {
            var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
            return userRoles.Adapt<List<HbtUserRoleDto>>();
        }

        /// <summary>
        /// 获取用户已分配的部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>部门列表</returns>
        public async Task<List<HbtUserDeptDto>> GetUserDeptIdsAsync(long userId)
        {
            var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
            return userDepts.Adapt<List<HbtUserDeptDto>>();
        }

        /// <summary>
        /// 获取用户已分配的岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>岗位列表</returns>
        public async Task<List<HbtUserPostDto>> GetUserPostIdsAsync(long userId)
        {
            var userPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId && up.IsDeleted == 0);
            return userPosts.Adapt<List<HbtUserPostDto>>();
        }

        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateUserRolesAsync(long userId, long[] roleIds)
        {
            try
            {
                _logger.Info($"开始分配用户角色 - 用户ID: {userId}, 角色IDs: {string.Join(",", roleIds)}");

                // 1. 获取用户现有关联的角色（包括已删除的）
                var existingRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId);
                _logger.Info($"用户现有关联角色数量: {existingRoles.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
                var rolesToDelete = existingRoles.Where(ur => !roleIds.Contains(ur.RoleId)).ToList();
                if (rolesToDelete.Any())
                {
                    _logger.Info($"需要标记删除的角色关联数量: {rolesToDelete.Count}, 角色IDs: {string.Join(",", rolesToDelete.Select(d => d.RoleId))}");
                    foreach (var role in rolesToDelete)
                    {
                        role.IsDeleted = 1; // 1 表示已删除
                        role.DeleteBy = _currentUser.UserName;
                        role.DeleteTime = DateTime.Now;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
                var rolesToRestore = existingRoles.Where(ur => roleIds.Contains(ur.RoleId) && ur.IsDeleted == 1).ToList();
                if (rolesToRestore.Any())
                {
                    _logger.Info($"需要恢复的角色关联数量: {rolesToRestore.Count}, 角色IDs: {string.Join(",", rolesToRestore.Select(d => d.RoleId))}");
                    foreach (var role in rolesToRestore)
                    {
                        role.IsDeleted = 0; // 0 表示未删除
                        role.DeleteBy = null;
                        role.DeleteTime = null;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
                var existingRoleIds = existingRoles.Select(ur => ur.RoleId).ToList();
                var rolesToAdd = roleIds.Where(roleId => !existingRoleIds.Contains(roleId))
                    .Select(roleId => new HbtUserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (rolesToAdd.Any())
                {
                    _logger.Info($"需要新增的角色关联数量: {rolesToAdd.Count}, 角色IDs: {string.Join(",", rolesToAdd.Select(d => d.RoleId))}");
                    await UserRoleRepository.CreateRangeAsync(rolesToAdd);
                    _logger.Info("角色关联新增完成");
                }

                _logger.Info("用户角色分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户角色失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateUserDeptsAsync(long userId, long[] deptIds)
        {
            try
            {
                _logger.Info($"开始分配用户部门 - 用户ID: {userId}, 部门IDs: {string.Join(",", deptIds)}");

                // 1. 获取用户现有关联的部门（包括已删除的）
                var existingDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId);
                _logger.Info($"用户现有关联部门数量: {existingDepts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
                var deptsToDelete = existingDepts.Where(ud => !deptIds.Contains(ud.DeptId)).ToList();
                if (deptsToDelete.Any())
                {
                    _logger.Info($"需要标记删除的部门关联数量: {deptsToDelete.Count}, 部门IDs: {string.Join(",", deptsToDelete.Select(d => d.DeptId))}");
                    foreach (var dept in deptsToDelete)
                    {
                        dept.IsDeleted = 1; // 1 表示已删除
                        dept.DeleteBy = _currentUser.UserName;
                        dept.DeleteTime = DateTime.Now;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                    _logger.Info("部门关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
                var deptsToRestore = existingDepts.Where(ud => deptIds.Contains(ud.DeptId) && ud.IsDeleted == 1).ToList();
                if (deptsToRestore.Any())
                {
                    _logger.Info($"需要恢复的部门关联数量: {deptsToRestore.Count}, 部门IDs: {string.Join(",", deptsToRestore.Select(d => d.DeptId))}");
                    foreach (var dept in deptsToRestore)
                    {
                        dept.IsDeleted = 0; // 0 表示未删除
                        dept.DeleteBy = null;
                        dept.DeleteTime = null;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                    _logger.Info("部门关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
                var existingDeptIds = existingDepts.Select(ud => ud.DeptId).ToList();
                var deptsToAdd = deptIds.Where(deptId => !existingDeptIds.Contains(deptId))
                    .Select(deptId => new HbtUserDept
                    {
                        UserId = userId,
                        DeptId = deptId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (deptsToAdd.Any())
                {
                    _logger.Info($"需要新增的部门关联数量: {deptsToAdd.Count}, 部门IDs: {string.Join(",", deptsToAdd.Select(d => d.DeptId))}");
                    await UserDeptRepository.CreateRangeAsync(deptsToAdd);
                    _logger.Info("部门关联新增完成");
                }

                _logger.Info("用户部门分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户部门失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="postIds">岗位ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateUserPostsAsync(long userId, long[] postIds)
        {
            try
            {
                _logger.Info($"开始分配用户岗位 - 用户ID: {userId}, 岗位IDs: {string.Join(",", postIds)}");

                // 1. 获取用户现有关联的岗位（包括已删除的）
                var existingPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId);
                _logger.Info($"用户现有关联岗位数量: {existingPosts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的岗位列表中）
                var postsToDelete = existingPosts.Where(up => !postIds.Contains(up.PostId)).ToList();
                if (postsToDelete.Any())
                {
                    _logger.Info($"需要标记删除的岗位关联数量: {postsToDelete.Count}, 岗位IDs: {string.Join(",", postsToDelete.Select(d => d.PostId))}");
                    foreach (var post in postsToDelete)
                    {
                        post.IsDeleted = 1; // 1 表示已删除
                        post.DeleteBy = _currentUser.UserName;
                        post.DeleteTime = DateTime.Now;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                    _logger.Info("岗位关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的岗位列表中且已存在但被标记为删除）
                var postsToRestore = existingPosts.Where(up => postIds.Contains(up.PostId) && up.IsDeleted == 1).ToList();
                if (postsToRestore.Any())
                {
                    _logger.Info($"需要恢复的岗位关联数量: {postsToRestore.Count}, 岗位IDs: {string.Join(",", postsToRestore.Select(d => d.PostId))}");
                    foreach (var post in postsToRestore)
                    {
                        post.IsDeleted = 0; // 0 表示未删除
                        post.DeleteBy = null;
                        post.DeleteTime = null;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                    _logger.Info("岗位关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的岗位列表中且不存在任何记录）
                var existingPostIds = existingPosts.Select(up => up.PostId).ToList();
                var postsToAdd = postIds.Where(postId => !existingPostIds.Contains(postId))
                    .Select(postId => new HbtUserPost
                    {
                        UserId = userId,
                        PostId = postId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (postsToAdd.Any())
                {
                    _logger.Info($"需要新增的岗位关联数量: {postsToAdd.Count}, 岗位IDs: {string.Join(",", postsToAdd.Select(d => d.PostId))}");
                    await UserPostRepository.CreateRangeAsync(postsToAdd);
                    _logger.Info("岗位关联新增完成");
                }

                _logger.Info("用户岗位分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户岗位失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配用户租户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="configIds">租户配置ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AllocateUserTenantsAsync(long userId, string[] configIds)
        {
            try
            {
                _logger.Info($"开始分配用户租户 - 用户ID: {userId}, 配置IDs: {string.Join(",", configIds)}");

                // 1. 获取用户现有关联的租户（包括已删除的）
                var existingTenants = await UserTenantRepository.GetListAsync(ut => ut.UserId == userId);
                _logger.Info($"用户现有关联租户数量: {existingTenants.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的租户列表中）
                var tenantsToDelete = existingTenants.Where(ut => !configIds.Contains(ut.ConfigId)).ToList();
                if (tenantsToDelete.Any())
                {
                    _logger.Info($"需要标记删除的租户关联数量: {tenantsToDelete.Count}, 配置IDs: {string.Join(",", tenantsToDelete.Select(d => d.ConfigId))}");
                    foreach (var tenant in tenantsToDelete)
                    {
                        // 标记删除
                        tenant.IsDeleted = 1; // 1 表示已删除
                        tenant.DeleteBy = _currentUser.UserName;
                        tenant.DeleteTime = DateTime.Now;
                        await UserTenantRepository.UpdateAsync(tenant);
                    }
                    _logger.Info("租户关联删除标记完成");
                }

                // 3. 处理需要恢复的关联（在新的租户列表中且已存在但被标记为删除）
                var tenantsToRestore = existingTenants.Where(ut => configIds.Contains(ut.ConfigId) && ut.IsDeleted == 1).ToList();
                if (tenantsToRestore.Any())
                {
                    _logger.Info($"需要恢复的租户关联数量: {tenantsToRestore.Count}, 配置IDs: {string.Join(",", tenantsToRestore.Select(d => d.ConfigId))}");
                    foreach (var tenant in tenantsToRestore)
                    {
                        // 取消删除标记
                        tenant.IsDeleted = 0; // 0 表示未删除
                        tenant.DeleteBy = null;
                        tenant.DeleteTime = null;
                        await UserTenantRepository.UpdateAsync(tenant);
                    }
                    _logger.Info("租户关联删除标记取消完成");
                }

                // 4. 找出需要新增的关联（在新的租户列表中且不存在任何记录）
                var existingTenantIds = existingTenants.Select(ut => ut.ConfigId).ToList();
                var tenantsToAdd = configIds.Where(configId => !existingTenantIds.Contains(configId))
                    .Select(configId => new HbtUserTenant
                    {
                        UserId = userId,
                        ConfigId = configId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (tenantsToAdd.Any())
                {
                    _logger.Info($"需要新增的租户关联数量: {tenantsToAdd.Count}, 配置IDs: {string.Join(",", tenantsToAdd.Select(d => d.ConfigId))}");
                    await UserTenantRepository.CreateRangeAsync(tenantsToAdd);
                    _logger.Info("租户关联新增完成");
                }

                _logger.Info("用户租户分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户租户失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 跨租户更新用户信息（同步到所有相关租户）
        /// </summary>
        /// <param name="input">用户更新信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateUserAcrossTenantsAsync(HbtUserUpdateDto input)
        {
            try
            {
                _logger.Info($"开始跨租户更新用户信息 - 用户ID: {input.UserId}");

                // 1. 更新认证数据库中的用户信息
                var user = await UserRepository.GetByIdAsync(input.UserId)
                    ?? throw new HbtException(L("Identity.User.NotFound", input.UserId));

                // 验证字段是否已存在
                if (user.UserName != input.UserName)
                    await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "UserName", input.UserName, input.UserId);
                if (user.Email != input.Email)
                    await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", input.Email, input.UserId);
                if (user.PhoneNumber != input.PhoneNumber)
                    await HbtValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", input.PhoneNumber, input.UserId);

                // 更新用户信息
                input.Adapt(user);
                var updateResult = await UserRepository.UpdateAsync(user) > 0;

                if (!updateResult)
                {
                    _logger.Error($"更新认证数据库用户信息失败 - 用户ID: {input.UserId}");
                    return false;
                }

                // 2. 获取用户关联的所有租户
                var userTenants = await UserTenantRepository.GetListAsync(ut => ut.UserId == input.UserId);
                var tenantIds = userTenants.Select(ut => ut.ConfigId).ToList();

                _logger.Info($"用户关联的租户数量: {tenantIds.Count}");

                // 3. 同步更新到所有租户数据库
                var syncResults = new List<bool>();
                foreach (var tenantId in tenantIds)
                {
                    try
                    {
                        var syncResult = await SyncUserToTenantAsync(input.UserId, tenantId);
                        syncResults.Add(syncResult);

                        if (syncResult)
                        {
                            _logger.Info($"成功同步用户信息到租户 {tenantId}");
                        }
                        else
                        {
                            _logger.Warn($"同步用户信息到租户 {tenantId} 失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"同步用户信息到租户 {tenantId} 时发生异常: {ex.Message}");
                        syncResults.Add(false);
                    }
                }

                // 4. 记录同步结果
                var successCount = syncResults.Count(r => r);
                var failCount = syncResults.Count(r => !r);

                _logger.Info($"跨租户用户信息更新完成 - 成功: {successCount}, 失败: {failCount}");

                return updateResult;
            }
            catch (Exception ex)
            {
                _logger.Error($"跨租户更新用户信息失败 - 用户ID: {input.UserId}, 错误: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 同步用户信息到指定租户数据库
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="configId">租户配置ID</param>
        /// <returns>是否成功</returns>
        private async Task<bool> SyncUserToTenantAsync(long userId, string configId)
        {
            try
            {
                _logger.Info($"开始同步用户信息到租户 - 用户ID: {userId}, 配置ID: {configId}");

                // 检查用户租户关联是否已存在
                var userTenant = await UserTenantRepository.GetFirstAsync(ut => ut.UserId == userId && ut.ConfigId == configId);
                if (userTenant == null)
                {
                    // 创建用户租户关联
                    userTenant = new HbtUserTenant
                    {
                        UserId = userId,
                        ConfigId = configId,
                        IsDeleted = 0,
                        CreateBy = _currentUser.UserName ?? "System",
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName ?? "System",
                        UpdateTime = DateTime.Now
                    };

                    var createResult = await UserTenantRepository.CreateAsync(userTenant);
                    return createResult > 0;
                }
                else
                {
                    // 更新用户租户关联
                    userTenant.UpdateTime = DateTime.Now;
                    userTenant.UpdateBy = _currentUser.UserName ?? "System";

                    var updateResult = await UserTenantRepository.UpdateAsync(userTenant);
                    return updateResult > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"同步用户信息到租户 {configId} 失败 - 用户ID: {userId}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取用户在指定租户中的信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="configId">租户配置ID</param>
        /// <returns>用户信息</returns>
        public async Task<HbtUserDto?> GetUserInTenantAsync(long userId, string configId)
        {
            try
            {
                // 检查用户租户关联是否存在
                var userTenant = await UserTenantRepository.GetFirstAsync(ut => ut.UserId == userId && ut.ConfigId == configId);
                if (userTenant == null)
                {
                    _logger.Warn($"用户与租户关联不存在 - 用户ID: {userId}, 配置ID: {configId}");
                    return null;
                }

                // 获取用户信息
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Warn($"用户不存在 - 用户ID: {userId}");
                    return null;
                }

                var userDto = user.Adapt<HbtUserDto>();
                return userDto;
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户在租户中的信息失败 - 用户ID: {userId}, 配置ID: {configId}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 更新用户在指定租户中的信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="configId">租户配置ID</param>
        /// <param name="input">用户更新信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateUserInTenantAsync(long userId, string configId, HbtUserUpdateDto input)
        {
            try
            {
                _logger.Info($"开始更新用户在租户中的信息 - 用户ID: {userId}, 配置ID: {configId}");

                // 检查用户租户关联是否存在
                var userTenant = await UserTenantRepository.GetFirstAsync(ut => ut.UserId == userId && ut.ConfigId == configId);
                if (userTenant == null)
                {
                    _logger.Warn($"用户与租户关联不存在 - 用户ID: {userId}, 配置ID: {configId}");
                    return false;
                }

                // 获取用户信息
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    _logger.Warn($"用户不存在 - 用户ID: {userId}");
                    return false;
                }

                // 更新用户信息
                user.UserName = input.UserName;
                user.NickName = input.NickName;
                user.Email = input.Email;
                user.PhoneNumber = input.PhoneNumber;
                user.Avatar = input.Avatar;
                user.Status = input.Status;
                user.UpdateTime = DateTime.Now;
                user.UpdateBy = _currentUser.UserName ?? "System";

                var updateResult = await UserRepository.UpdateAsync(user);

                if (updateResult > 0)
                {
                    _logger.Info($"成功更新用户信息 - 用户ID: {userId}, 配置ID: {configId}");
                }

                return updateResult > 0;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新用户信息失败 - 用户ID: {userId}, 配置ID: {configId}, 错误: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// 构建用户查询条件
        /// </summary>
        private static Expression<Func<HbtUser, bool>> QueryExpression(HbtUserQueryDto query)
        {
            var exp = Expressionable.Create<HbtUser>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.NickName))
                exp.And(x => x.NickName.Contains(query.NickName));

            if (!string.IsNullOrEmpty(query.PhoneNumber))
                exp.And(x => x.PhoneNumber.Contains(query.PhoneNumber));

            if (!string.IsNullOrEmpty(query.Email))
                exp.And(x => x.Email.Contains(query.Email));

            if (query.Status.HasValue && query.Status.Value != -1)
                exp.And(x => x.Status == query.Status.Value);

            if (query.UserType.HasValue && query.UserType.Value != -1)
                exp.And(x => x.UserType == query.UserType.Value);

            if (query.Gender.HasValue && query.Gender.Value != -1)
                exp.And(x => x.Gender == query.Gender.Value);

            if (query.DeptId.HasValue)
                exp.And(x => x.UserDepts != null && x.UserDepts.Any(d => d.DeptId == query.DeptId.Value));

            return exp.ToExpression();
        }
    }
}