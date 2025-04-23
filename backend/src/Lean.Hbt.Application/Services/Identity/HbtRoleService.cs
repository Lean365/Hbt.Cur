//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRoleService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices.Extensions;
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

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="roleRepository">角色仓库</param>
        /// <param name="userRoleRepository">用户角色仓库</param>
        /// <param name="roleMenuRepository">角色菜单仓库</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        public HbtRoleService(
            IHbtLogger logger,
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository,
            IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 构建角色查询条件
        /// </summary>
        private Expression<Func<HbtRole, bool>> HbtRoleQueryExpression(HbtRoleQueryDto query)
        {
            var exp = Expressionable.Create<HbtRole>();

            if (!string.IsNullOrEmpty(query.RoleName))
                exp.And(x => x.RoleName.Contains(query.RoleName));

            if (!string.IsNullOrEmpty(query.RoleKey))
                exp.And(x => x.RoleKey.Contains(query.RoleKey));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtRoleDto>> GetListAsync(HbtRoleQueryDto query)
        {
            var exp = HbtRoleQueryExpression(query);

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

            // 检查是否有用户关联
            foreach (var roleId in roleIds)
            {
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
                var list = await _roleRepository.GetListAsync(HbtRoleQueryExpression(query));
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
                    Disabled = false
                })
                .ToListAsync();
            return roles;
        }
    }
}