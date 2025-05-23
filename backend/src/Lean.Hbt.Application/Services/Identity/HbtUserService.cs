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
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
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
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
        private readonly IHbtRepository<HbtUserPost> _userPostRepository;
        private readonly IHbtRepository<HbtUserDept> _userDeptRepository;
        private readonly IHbtPasswordPolicy _passwordPolicy;
        private readonly IHbtRepository<HbtTenant> _tenantRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserService(
            IHbtLogger logger,
            IHbtRepository<HbtUser> userRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHbtRepository<HbtUserPost> userPostRepository,
            IHbtRepository<HbtUserDept> userDeptRepository,
            IHbtPasswordPolicy passwordPolicy,
            IHbtRepository<HbtTenant> tenantRepository,
            IHbtCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userPostRepository = userPostRepository;
            _userDeptRepository = userDeptRepository;
            _passwordPolicy = passwordPolicy;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtUserDto>> GetListAsync(HbtUserQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await _userRepository.GetPagedListAsync(
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
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

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
                throw new HbtException(L("Identity.User.Username.Required"));

            // 验证租户是否存在且有效
            //var tenant = await _tenantRepository.GetByIdAsync(input.TenantId);
            var tenant = await _tenantRepository.GetFirstAsync(x => x.Id == input.TenantId);
            if (tenant == null)
                throw new HbtException(L("Identity.Tenant.NotFound"));

            if (tenant.Status != 0)
                throw new HbtException(L("Identity.Tenant.Disabled"));

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "UserName", input.UserName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", input.PhoneNumber);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", input.Email);

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

            var result = await _userRepository.CreateAsync(user);
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

                await _userRoleRepository.CreateRangeAsync(userRoles);
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

                await _userPostRepository.CreateRangeAsync(userPosts);
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

                await _userDeptRepository.CreateRangeAsync(userDepts);
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

            var user = await _userRepository.GetByIdAsync(input.UserId)
                ?? throw new HbtException(L("Identity.User.NotFound"));

            // 验证字段是否已存在（排除当前用户）
            if (user.NickName != input.NickName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "NickName", input.NickName, input.UserId);
            if (user.PhoneNumber != input.PhoneNumber)
                await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", input.PhoneNumber, input.UserId);
            if (user.Email != input.Email)
                await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", input.Email, input.UserId);

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

            var result = await _userRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(L("Common.UpdateFailed"));

            // 更新用户角色关联
            if (input.RoleIds != null)
            {
                var userRoles = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id);
                if (userRoles.Any())
                {
                    await _userRoleRepository.DeleteRangeAsync(userRoles);
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

                        await _userRoleRepository.CreateRangeAsync(newUserRoles);
                    }
                }
            }

            // 更新用户岗位关联
            if (input.PostIds != null)
            {
                var userPosts = await _userPostRepository.GetListAsync(up => up.UserId == user.Id);
                if (userPosts.Any())
                {
                    await _userPostRepository.DeleteRangeAsync(userPosts);
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

                        await _userPostRepository.CreateRangeAsync(newUserPosts);
                    }
                }
            }

            // 更新用户部门关联
            if (input.DeptIds != null)
            {
                var userDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == user.Id);
                if (userDepts.Any())
                {
                    await _userDeptRepository.DeleteRangeAsync(userDepts);
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

                        await _userDeptRepository.CreateRangeAsync(newUserDepts);
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
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new HbtException(L("Identity.User.NotFound"));

            // 禁止删除admin用户
            if (user.UserName == "admin")
                throw new HbtException("超级管理员账号不可删除！");

            // 更新用户状态为停用
            user.Status = 1;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;
            await _userRepository.UpdateAsync(user);

            // 删除用户角色关联
            var userRoleIds = (await _userRoleRepository.GetListAsync(ur => ur.UserId == userId)).Select(ur => ur.Id).ToList();
            foreach (var id in userRoleIds)
            {
                await _userRoleRepository.DeleteAsync(id);
            }

            // 删除用户岗位关联
            var userPostIds = (await _userPostRepository.GetListAsync(up => up.UserId == userId)).Select(up => up.Id).ToList();
            foreach (var id in userPostIds)
            {
                await _userPostRepository.DeleteAsync(id);
            }

            // 删除用户部门关联
            var userDeptIds = (await _userDeptRepository.GetListAsync(ud => ud.UserId == userId)).Select(ud => ud.Id).ToList();
            foreach (var id in userDeptIds)
            {
                await _userDeptRepository.DeleteAsync(id);
            }

            // 删除用户
            var result = await _userRepository.DeleteAsync(user);
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
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null) continue;
                // 禁止删除admin用户
                if (user.UserName == "admin")
                    throw new HbtException($"超级管理员账号不可删除！(ID: {userId})");
            }

            // 更新用户状态为停用
            var users = await _userRepository.GetListAsync(u => userIds.Contains(u.Id));
            foreach (var user in users)
            {
                user.Status = 1;
                user.UpdateBy = _currentUser.UserName;
                user.UpdateTime = DateTime.Now;
            }
            await _userRepository.UpdateRangeAsync(users);

            // 删除用户角色关联
            await _userRoleRepository.DeleteAsync((HbtUserRole ur) => userIds.Contains(ur.UserId));

            // 删除用户岗位关联
            await _userPostRepository.DeleteAsync((HbtUserPost up) => userIds.Contains(up.UserId));

            // 删除用户部门关联
            await _userDeptRepository.DeleteAsync((HbtUserDept ud) => userIds.Contains(ud.UserId));

            // 删除用户
            return await _userRepository.DeleteRangeAsync(userIds.Cast<object>().ToList()) > 0;
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
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "UserName", user.UserName);
                        // 校验手机号是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", user.PhoneNumber);
                        // 校验邮箱是否已存在
                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", user.Email);
                        string ss = _passwordPolicy.DefaultPassword;
                        var entity = user.Adapt<HbtUser>();
                        var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(_passwordPolicy.DefaultPassword);
                        entity.Password = hash;
                        entity.Salt = salt;
                        entity.Iterations = iterations;
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.Status = 0;

                        var result = await _userRepository.CreateAsync(entity);
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
            var list = await _userRepository.GetListAsync(QueryExpression(query));
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

            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            user.Status = input.Status;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await _userRepository.UpdateAsync(user);
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

            var user = await _userRepository.GetByIdAsync(input.UserId);
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

            var result = await _userRepository.UpdateAsync(user);
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

            var user = await _userRepository.GetByIdAsync(input.UserId);
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

            var result = await _userRepository.UpdateAsync(user);
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

            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtException(L("Identity.User.NotFound"));

            user.LockEndTime = null;
            user.LockReason = null;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await _userRepository.UpdateAsync(user);
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
            var user = await _userRepository.GetFirstAsync(u => u.UserName == userName);
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
            var user = await _userRepository.GetFirstAsync(u => u.PhoneNumber == phoneNumber);
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
            var users = await _userRepository.AsQueryable()
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
            var userRoles = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
            return userRoles.Adapt<List<HbtUserRoleDto>>();
        }

        /// <summary>
        /// 获取用户已分配的部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>部门列表</returns>
        public async Task<List<HbtUserDeptDto>> GetUserDeptIdsAsync(long userId)
        {
            var userDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
            return userDepts.Adapt<List<HbtUserDeptDto>>();
        }

        /// <summary>
        /// 获取用户已分配的岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>岗位列表</returns>
        public async Task<List<HbtUserPostDto>> GetUserPostIdsAsync(long userId)
        {
            var userPosts = await _userPostRepository.GetListAsync(up => up.UserId == userId && up.IsDeleted == 0);
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
                var existingRoles = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId);
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
                        await _userRoleRepository.UpdateAsync(role);
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
                        await _userRoleRepository.UpdateAsync(role);
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
                    await _userRoleRepository.CreateRangeAsync(rolesToAdd);
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
                var existingDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == userId);
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
                        await _userDeptRepository.UpdateAsync(dept);
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
                        await _userDeptRepository.UpdateAsync(dept);
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
                    await _userDeptRepository.CreateRangeAsync(deptsToAdd);
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
                var existingPosts = await _userPostRepository.GetListAsync(up => up.UserId == userId);
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
                        await _userPostRepository.UpdateAsync(post);
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
                        await _userPostRepository.UpdateAsync(post);
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
                    await _userPostRepository.CreateRangeAsync(postsToAdd);
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