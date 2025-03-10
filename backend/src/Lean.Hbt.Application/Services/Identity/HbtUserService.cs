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
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Identity;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;

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
            IHbtTenantContext tenantContext)
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
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtUserDto>> GetPagedListAsync(HbtUserQueryDto query)
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

            var result = await _userRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtUserDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtUserDto>>()
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户详情</returns>
        public async Task<HbtUserDto> GetAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new HbtException(_localization.L("User.NotFound"));

            return user.Adapt<HbtUserDto>();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">用户创建信息</param>
        /// <returns>返回新创建的用户ID</returns>
        public async Task<long> InsertAsync(HbtUserCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.UserName))
                throw new HbtException(_localization.L("User.Username.Required"));

            if (string.IsNullOrEmpty(input.Password))
                throw new HbtException(_localization.L("User.Password.Required"));

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

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(input.Password))
                throw new HbtException(_localization.L("User.Password.Invalid"));

            // 创建用户
            var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(input.Password);
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

            var result = await _userRepository.InsertAsync(user);
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
                await _userRoleRepository.InsertRangeAsync(userRoles);
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
                await _userPostRepository.InsertRangeAsync(userPosts);
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
            await _userDeptRepository.InsertAsync(userDept);

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

            // 验证租户权限
            if (user.TenantId != input.TenantId)
                throw new HbtException(_localization.L("User.Tenant.Invalid"));

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "PhoneNumber", input.PhoneNumber, input.UserId);
            await HbtValidateUtils.ValidateFieldExistsAsync(_userRepository, "Email", input.Email, input.UserId);

            // 更新用户基本信息
            user.NickName = input.NickName ?? string.Empty;
            user.EnglishName = input.EnglishName ?? string.Empty;
            user.PhoneNumber = input.PhoneNumber ?? string.Empty;
            user.Email = input.Email ?? string.Empty;
            user.Gender = 0;
            user.Avatar = input.Avatar ?? string.Empty;
            user.Remark = input.Remark ?? string.Empty;

            var result = await _userRepository.UpdateAsync(user);
            if (result <= 0)
                throw new HbtException(_localization.L("User.Update.Failed"));

            // 更新角色关联
            await _userRoleRepository.DeleteAsync((Expression<Func<HbtUserRole, bool>>)(x => x.UserId == user.Id));
            if (input.RoleIds?.Any() == true)
            {
                // 验证角色是否属于当前租户
                var roles = await _roleRepository.GetListAsync((Expression<Func<HbtRole, bool>>)(r => input.RoleIds.Contains(r.Id) && r.TenantId == user.TenantId));
                if (roles.Count != input.RoleIds.Count)
                    throw new HbtException(_localization.L("User.Role.Invalid"));

                var userRoles = input.RoleIds.Select(roleId => new HbtUserRole
                {
                    UserId = user.Id,
                    RoleId = roleId,
                    TenantId = user.TenantId
                }).ToList();
                await _userRoleRepository.InsertRangeAsync(userRoles);
            }

            // 更新岗位关联
            await _userPostRepository.DeleteAsync((Expression<Func<HbtUserPost, bool>>)(x => x.UserId == user.Id));
            if (input.PostIds?.Any() == true)
            {
                // 验证岗位是否属于当前租户
                var posts = await _postRepository.GetListAsync((Expression<Func<HbtPost, bool>>)(p => input.PostIds.Contains(p.Id) && p.TenantId == user.TenantId));
                if (posts.Count != input.PostIds.Count)
                    throw new HbtException(_localization.L("User.Post.Invalid"));

                var userPosts = input.PostIds.Select(postId => new HbtUserPost
                {
                    UserId = user.Id,
                    PostId = postId,
                    TenantId = user.TenantId
                }).ToList();
                await _userPostRepository.InsertRangeAsync(userPosts);
            }

            // 更新部门关联
            await _userDeptRepository.DeleteAsync((Expression<Func<HbtUserDept, bool>>)(x => x.UserId == user.Id));
            // 验证部门是否属于当前租户
            var dept = await _deptRepository.GetByIdAsync(input.DeptId);
            if (dept == null || dept.TenantId != user.TenantId)
                throw new HbtException(_localization.L("User.Dept.Invalid"));

            var userDept = new HbtUserDept
            {
                UserId = user.Id,
                DeptId = input.DeptId,
                TenantId = user.TenantId
            };
            await _userDeptRepository.InsertAsync(userDept);

            _logger.Info(_localization.L("User.Updated.Success", user.UserName));
            return result > 0;
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

            // 验证租户权限
            if (user.TenantId != _tenantContext.TenantId)
                throw new HbtException(_localization.L("User.Tenant.Invalid"));

            // 删除用户关联数据
            await _userRoleRepository.DeleteAsync((Expression<Func<HbtUserRole, bool>>)(x => x.UserId == userId));
            await _userPostRepository.DeleteAsync((Expression<Func<HbtUserPost, bool>>)(x => x.UserId == userId));
            await _userDeptRepository.DeleteAsync((Expression<Func<HbtUserDept, bool>>)(x => x.UserId == userId));

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

            // 验证租户权限
            var users = await _userRepository.GetListAsync(u => userIds.Contains(u.Id));
            if (users.Any(u => u.TenantId != _tenantContext.TenantId))
                throw new HbtException(_localization.L("User.Tenant.Invalid"));

            // 删除用户关联数据
            await _userRoleRepository.DeleteAsync((Expression<Func<HbtUserRole, bool>>)(x => userIds.Contains(x.UserId)));
            await _userPostRepository.DeleteAsync((Expression<Func<HbtUserPost, bool>>)(x => userIds.Contains(x.UserId)));
            await _userDeptRepository.DeleteAsync((Expression<Func<HbtUserDept, bool>>)(x => userIds.Contains(x.UserId)));

            // 删除用户
            var result = await _userRepository.DeleteRangeAsync(userIds.Cast<object>().ToList());

            _logger.Info(_localization.L("User.BatchDeleted.Success", string.Join(",", userIds)));
            return result > 0;
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
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

                    await _userRepository.InsertAsync(newUser);
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
    }
}