//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;
using Mapster;
using SqlSugar.Extensions;
using System.IO;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Helpers;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 角色服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtRoleService : IHbtRoleService
    {
        private readonly ILogger<HbtRoleService> _logger;
        private readonly IHbtRepository<HbtRole> _roleRepository;
        private readonly IHbtRepository<HbtRoleMenu> _roleMenuRepository;
        private readonly IHbtRepository<HbtRoleDept> _roleDeptRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="roleRepository">角色仓库</param>
        /// <param name="roleMenuRepository">角色菜单仓库</param>
        /// <param name="roleDeptRepository">角色部门仓库</param>
        public HbtRoleService(
            ILogger<HbtRoleService> logger,
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository,
            IHbtRepository<HbtRoleDept> roleDeptRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleDeptRepository = roleDeptRepository;
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtRoleDto>> GetPagedListAsync(HbtRoleQueryDto query)
        {
            var exp = Expressionable.Create<HbtRole>();

            if (!string.IsNullOrEmpty(query?.RoleName))
                exp.And(x => x.RoleName.Contains(query.RoleName));

            if (!string.IsNullOrEmpty(query?.RoleKey))
                exp.And(x => x.RoleKey.Contains(query.RoleKey));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _roleRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtRoleDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtRoleDto>>()
            };
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回角色详情</returns>
        public async Task<HbtRoleDto> GetAsync(long roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new HbtException($"角色不存在: {roleId}");

            return role.Adapt<HbtRoleDto>();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">角色创建信息</param>
        /// <returns>返回新创建的角色ID</returns>
        public async Task<long> InsertAsync(HbtRoleCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.RoleName))
                throw new HbtException("角色名称不能为空");

            if (string.IsNullOrEmpty(input.RoleKey))
                throw new HbtException("角色标识不能为空");

            // 验证角色名称和标识是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", input.RoleName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", input.RoleKey);

            // 创建角色
            var role = new HbtRole
            {
                RoleName = input.RoleName,
                RoleKey = input.RoleKey,
                OrderNum = input.OrderNum,
                DataScope = input.DataScope,
                Status = input.Status,
                TenantId = input.TenantId,
                Remark = input.Remark ?? string.Empty
            };

            var result = await _roleRepository.InsertAsync(role);
            if (result <= 0)
                throw new HbtException("创建角色失败");

            // 关联菜单
            if (input.MenuIds?.Any() == true)
            {
                var roleMenus = input.MenuIds.Select(menuId => new HbtRoleMenu
                {
                    RoleId = role.Id,
                    MenuId = menuId,
                    TenantId = input.TenantId
                }).ToList();
                await _roleMenuRepository.InsertRangeAsync(roleMenus);
            }

            // 关联部门
            if (input.DeptIds?.Any() == true)
            {
                var roleDepts = input.DeptIds.Select(deptId => new HbtRoleDept
                {
                    RoleId = role.Id,
                    DeptId = deptId,
                    TenantId = input.TenantId
                }).ToList();
                await _roleDeptRepository.InsertRangeAsync(roleDepts);
            }

            return role.Id;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">角色更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateAsync(HbtRoleUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.RoleName))
                throw new HbtException("角色名称不能为空");

            if (string.IsNullOrEmpty(input.RoleKey))
                throw new HbtException("角色标识不能为空");

            var role = await _roleRepository.GetByIdAsync(input.RoleId);
            if (role == null)
                throw new HbtException($"角色不存在: {input.RoleId}");

            // 验证角色名称和标识是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", input.RoleName, input.RoleId);
            await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", input.RoleKey, input.RoleId);

            // 更新角色基本信息
            role.RoleName = input.RoleName;
            role.RoleKey = input.RoleKey;
            role.OrderNum = input.OrderNum;
            role.DataScope = input.DataScope;
            role.Status = input.Status;
            role.Remark = input.Remark ?? string.Empty;

            var result = await _roleRepository.UpdateAsync(role);
            if (result <= 0)
                throw new HbtException("更新角色失败");

            // 更新菜单关联
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.RoleId == role.Id));
            if (input.MenuIds?.Any() == true)
            {
                var roleMenus = input.MenuIds.Select(menuId => new HbtRoleMenu
                {
                    RoleId = role.Id,
                    MenuId = menuId,
                    TenantId = role.TenantId
                }).ToList();
                await _roleMenuRepository.InsertRangeAsync(roleMenus);
            }

            // 更新部门关联
            await _roleDeptRepository.DeleteAsync((Expression<Func<HbtRoleDept, bool>>)(x => x.RoleId == role.Id));
            if (input.DeptIds?.Any() == true)
            {
                var roleDepts = input.DeptIds.Select(deptId => new HbtRoleDept
                {
                    RoleId = role.Id,
                    DeptId = deptId,
                    TenantId = role.TenantId
                }).ToList();
                await _roleDeptRepository.InsertRangeAsync(roleDepts);
            }

            return true;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> DeleteAsync(long roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new HbtException($"角色不存在: {roleId}");

            // 删除角色及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.RoleId == roleId));
            await _roleDeptRepository.DeleteAsync((Expression<Func<HbtRoleDept, bool>>)(x => x.RoleId == roleId));
            var result = await _roleRepository.DeleteAsync(roleId);

            return result > 0;
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] roleIds)
        {
            if (roleIds == null || roleIds.Length == 0)
                return false;

            // 删除角色及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => roleIds.Contains(x.RoleId)));
            await _roleDeptRepository.DeleteAsync((Expression<Func<HbtRoleDept, bool>>)(x => roleIds.Contains(x.RoleId)));
            var result = await _roleRepository.DeleteAsync((Expression<Func<HbtRole, bool>>)(x => roleIds.Contains(x.Id)));

            return result > 0;
        }

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtRoleQueryDto query, string sheetName = "Sheet1")
        {
            var exp = Expressionable.Create<HbtRole>();

            if (!string.IsNullOrEmpty(query?.RoleName))
                exp.And(x => x.RoleName.Contains(query.RoleName));

            if (!string.IsNullOrEmpty(query?.RoleKey))
                exp.And(x => x.RoleKey.Contains(query.RoleKey));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var roles = await _roleRepository.GetListAsync(exp.ToExpression());
            var exportData = roles.Adapt<List<HbtRoleExportDto>>();

            return await HbtExcelHelper.ExportAsync(exportData, sheetName);
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="input">角色状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtRoleStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var role = await _roleRepository.GetByIdAsync(input.RoleId);
            if (role == null)
                throw new HbtException($"角色不存在: {input.RoleId}");

            role.Status = input.Status;
            var result = await _roleRepository.UpdateAsync(role);

            return result > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        public async Task<HbtRoleTemplateDto> GetTemplateAsync()
        {
            return await Task.FromResult(new HbtRoleTemplateDto());
        }

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            if (fileStream == null)
                throw new HbtException("导入文件流不能为空");

            // 1.从Excel导入数据
            var roles = await HbtExcelHelper.ImportAsync<HbtRoleImportDto>(fileStream, sheetName);
            if (roles?.Any() != true)
                return (0, 0);

            int success = 0;
            int fail = 0;

            foreach (var role in roles)
            {
                try
                {
                    if (string.IsNullOrEmpty(role.RoleName) || string.IsNullOrEmpty(role.RoleKey))
                    {
                        _logger.LogWarning($"导入角色失败: 角色名称或角色标识不能为空");
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在
                    try
                    {
                        await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleName", role.RoleName);
                        await HbtValidateUtils.ValidateFieldExistsAsync(_roleRepository, "RoleKey", role.RoleKey);
                    }
                    catch (HbtException ex)
                    {
                        _logger.LogWarning($"导入角色失败: {ex.Message}");
                        fail++;
                        continue;
                    }

                    var entity = new HbtRole
                    {
                        RoleName = role.RoleName,
                        RoleKey = role.RoleKey,
                        OrderNum = role.OrderNum,
                        DataScope = 1, // 默认值 1
                        Status = 0, // 默认值 0 表示正常
                    };

                    await _roleRepository.InsertAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"导入角色失败: {ex.Message}", ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 获取角色导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtRoleTemplateDto>(sheetName);
        }
    }
} 