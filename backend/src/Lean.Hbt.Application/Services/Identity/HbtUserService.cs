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
using Lean.Hbt.Common.Constants;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Extensions;
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
        private readonly IHbtRepository<HbtRole> _roleRepository;
        private readonly IHbtRepository<HbtPost> _postRepository;
        private readonly IHbtRepository<HbtDept> _deptRepository;
        private readonly IHbtCurrentTenant _currentTenant;
        private readonly IHbtCurrentUser _currentUser;

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
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtPost> postRepository,
            IHbtRepository<HbtDept> deptRepository,
            IHbtCurrentTenant tenantContext,
            IHbtCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userPostRepository = userPostRepository;
            _userDeptRepository = userDeptRepository;
            _passwordPolicy = passwordPolicy;
            _tenantRepository = tenantRepository;
            _roleRepository = roleRepository;
            _postRepository = postRepository;
            _deptRepository = deptRepository;
            _currentTenant = tenantContext;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 构建用户查询条件
        /// </summary>
        private Expression<Func<HbtUser, bool>> HbtUserQueryExpression(HbtUserQueryDto query)
        {
            var exp = Expressionable.Create<HbtUser>();

            if (!string.IsNullOrEmpty(query.UserName))
                exp.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.NickName))
                exp.And(x => x.NickName.Contains(query.NickName));

            if (!string.IsNullOrEmpty(query.PhoneNumber))
                exp.And(x => x.PhoneNumber.Contains(query.PhoneNumber));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.DeptId.HasValue)
                exp.And(x => x.UserDepts != null && x.UserDepts.Any(d => d.DeptId == query.DeptId.Value));

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtUserDto>> GetListAsync(HbtUserQueryDto query)
        {
            var exp = HbtUserQueryExpression(query);

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
            var tenant = await _tenantRepository.GetByIdAsync(input.TenantId);
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
                await _userRoleRepository.DeleteAsync((HbtUserRole ur) => ur.UserId == user.Id);
                if (input.RoleIds.Any())
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
            }

            // 更新用户岗位关联
            if (input.PostIds != null)
            {
                await _userPostRepository.DeleteAsync((HbtUserPost up) => up.UserId == user.Id);
                if (input.PostIds.Any())
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
            }

            // 更新用户部门关联
            if (input.DeptIds != null)
            {
                await _userDeptRepository.DeleteAsync((HbtUserDept ud) => ud.UserId == user.Id);
                if (input.DeptIds.Any())
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

            // 删除用户角色关联
            await _userRoleRepository.DeleteAsync((HbtUserRole ur) => ur.UserId == userId);

            // 删除用户岗位关联
            await _userPostRepository.DeleteAsync((HbtUserPost up) => up.UserId == userId);

            // 删除用户部门关联
            await _userDeptRepository.DeleteAsync((HbtUserDept ud) => ud.UserId == userId);

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
                throw new HbtException("请选择要删除的用户");

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

                        await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "UserName", user.UserName);

                        var entity = user.Adapt<HbtUser>();
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
        /// <returns>Excel文件</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtUserQueryDto query, string sheetName = "Sheet1")
        {
            try
            {
                var list = await _userRepository.GetListAsync(HbtUserQueryExpression(query));
                var exportList = list.Adapt<List<HbtUserExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "用户数据");
            }
            catch (Exception ex)
            {
                _logger.Error("导出用户数据失败", ex);
                throw new HbtException("导出用户数据失败");
            }
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
                    Disabled = false
                })
                .ToListAsync();
            return users;
        }
    }
}