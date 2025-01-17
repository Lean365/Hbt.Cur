//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtUserService.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-17 16:30
// 版本号 : V1.0.0
// 描述    : 用户服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mapster;
using Lean.Hbt.Application.Identity.Dtos;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IRepositories;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Common.Helpers;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class HbtUserService : IHbtUserService
    {
        private readonly IHbtRepository<HbtUser> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtUserService(IHbtRepository<HbtUser> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        public async Task<HbtPagedResult<HbtUserDto>> GetUserListAsync(HbtUserQueryDto query)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtUser>();
            
            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(u => u.UserName.Contains(query.UserName));
                
            if (!string.IsNullOrEmpty(query.PhoneNumber))
                predicate.And(u => u.PhoneNumber == query.PhoneNumber);
                
            if (query.Status.HasValue)
                predicate.And(u => u.Status == query.Status.Value);

            // 2.查询数据
            var users = await _userRepository.GetPagedListAsync(
                predicate.ToExpression(),
                query.PageIndex,
                query.PageSize);

            // 3.转换并返回
            var dtos = users.Rows.Adapt<List<HbtUserDto>>();
            return new HbtPagedResult<HbtUserDto>
            {
                TotalNum = users.TotalNum,
                PageIndex = users.PageIndex,
                PageSize = users.PageSize,
                Rows = dtos
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        public async Task<HbtUserDto> GetUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new HbtBusinessException($"用户[{id}]不存在");

            return user.Adapt<HbtUserDto>();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public async Task<long> CreateUserAsync(HbtUserCreateDto input)
        {
            // 1.检查用户名是否存在
            var exists = await _userRepository.IsAnyAsync(u => u.UserName == input.UserName);
            if (exists)
                throw new HbtBusinessException("用户名已存在");

            // 2.创建用户实体
            var user = input.Adapt<HbtUser>();
            user.Salt = Guid.NewGuid().ToString("N").Substring(0, 6);
            user.Password = HashHelper.Md5Hash(input.Password + user.Salt);

            // 3.保存用户
            await _userRepository.InsertAsync(user);
            return user.Id;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public async Task<bool> UpdateUserAsync(HbtUserUpdateDto input)
        {
            // 1.获取用户
            var user = await _userRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new HbtBusinessException($"用户[{input.UserId}]不存在");

            // 2.更新用户
            input.Adapt(user);
            return await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public async Task<bool> DeleteUserAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 导入用户
        /// </summary>
        public async Task<string> ImportUsersAsync(List<HbtUserImportDto> users)
        {
            if (!users.Any())
                return "导入数据为空";

            // 1.转换为实体
            var entities = users.Adapt<List<HbtUser>>();
            foreach (var user in entities)
            {
                user.Salt = Guid.NewGuid().ToString("N").Substring(0, 6);
                user.Password = HashHelper.Md5Hash("123456" + user.Salt);
            }

            // 2.批量插入
            await _userRepository.InsertRangeAsync(entities);
            return $"成功导入{users.Count}条数据";
        }

        /// <summary>
        /// 导出用户
        /// </summary>
        public async Task<List<HbtUserExportDto>> ExportUsersAsync(HbtUserQueryDto query)
        {
            // 1.查询数据
            var predicate = Expressionable.Create<HbtUser>();
            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(u => u.UserName.Contains(query.UserName));
            if (!string.IsNullOrEmpty(query.PhoneNumber))
                predicate.And(u => u.PhoneNumber == query.PhoneNumber);
            if (query.Status.HasValue)
                predicate.And(u => u.Status == query.Status.Value);

            var users = await _userRepository.GetListAsync(predicate.ToExpression());

            // 2.转换并返回
            return users.Adapt<List<HbtUserExportDto>>();
        }
    }
} 