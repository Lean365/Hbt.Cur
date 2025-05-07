//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenuService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Domain.Entities.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 菜单服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuService : HbtBaseService, IHbtMenuService
    {
        // 菜单仓储接口
        private readonly IHbtRepository<HbtMenu> _menuRepository;

        // 角色菜单仓储接口
        private readonly IHbtRepository<HbtRoleMenu> _roleMenuRepository;

        // 用户角色仓储接口
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="menuRepository">菜单仓库</param>
        /// <param name="roleMenuRepository">角色菜单仓库</param>
        /// <param name="userRoleRepository">用户角色仓库</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtMenuService(
            IHbtLogger logger,
            IHbtRepository<HbtMenu> menuRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 构建菜单查询条件
        /// </summary>
        private Expression<Func<HbtMenu, bool>> HbtMenuQueryExpression(HbtMenuQueryDto query)
        {
            var exp = Expressionable.Create<HbtMenu>();

            if (!string.IsNullOrEmpty(query.MenuName))
                exp.And(x => x.MenuName.Contains(query.MenuName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件，包含页码、每页大小、菜单名称、状态等</param>
        /// <returns>返回分页后的菜单列表</returns>
        public async Task<HbtPagedResult<HbtMenuDto>> GetListAsync(HbtMenuQueryDto query)
        {
            var exp = HbtMenuQueryExpression(query);

            var result = await _menuRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtMenuDto>
            {
                Rows = result.Rows.Adapt<List<HbtMenuDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>返回菜单详细信息</returns>
        /// <exception cref="HbtException">当菜单不存在时抛出异常</exception>
        public async Task<HbtMenuDto> GetByIdAsync(long menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
                throw new HbtException(L("Identity.Menu.NotFound", menuId));

            return menu.Adapt<HbtMenuDto>();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>菜单ID</returns>
        public async Task<long> CreateAsync(HbtMenuCreateDto input)
        {
            // 验证菜单名称是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName);

            // 验证翻译键是否已存在
            if (!string.IsNullOrEmpty(input.TransKey))
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "TransKey", input.TransKey);

            var menu = input.Adapt<HbtMenu>();
            return await _menuRepository.CreateAsync(menu) > 0 ? menu.Id : throw new HbtException(L("Identity.Menu.CreateFailed"));
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtMenuUpdateDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId)
                ?? throw new HbtException(L("Identity.Menu.NotFound", input.MenuId));

            // 验证菜单名称是否已存在
            if (menu.MenuName != input.MenuName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName, input.MenuId);

            // 验证翻译键是否已存在
            if (!string.IsNullOrEmpty(input.TransKey) && menu.TransKey != input.TransKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "TransKey", input.TransKey, input.MenuId);

            // 检查是否存在循环引用
            if (input.ParentId != null && input.ParentId == input.MenuId)
                throw new HbtException(L("Identity.Menu.CannotBeParentOfItself"));

            // 检查父菜单是否存在
            if (input.ParentId > 0)
            {
                var parentMenu = await _menuRepository.GetByIdAsync(input.ParentId.Value);
                if (parentMenu == null)
                    throw new HbtException(L("Identity.Menu.ParentNotFound"));
            }

            input.Adapt(menu);
            return await _menuRepository.UpdateAsync(menu) > 0;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId)
                ?? throw new HbtException(L("Identity.Menu.NotFound", menuId));

            // 检查是否有子菜单
            var hasChildren = await _menuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
            if (hasChildren)
                throw new HbtException(L("Identity.Menu.HasChildren"));

            // 删除菜单及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.MenuId == menuId));
            return await _menuRepository.DeleteAsync(menuId) > 0;
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] menuIds)
        {
            if (menuIds == null || menuIds.Length == 0)
                throw new HbtException(L("Identity.Menu.SelectRequired"));

            // 检查是否有子菜单
            foreach (var menuId in menuIds)
            {
                var hasChildren = await _menuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
                if (hasChildren)
                    throw new HbtException(L("Identity.Menu.HasChildrenWithId", menuId));
            }

            // 删除菜单及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => menuIds.Contains(x.MenuId)));
            return await _menuRepository.DeleteRangeAsync(menuIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入菜单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtMenu")
        {
            try
            {
                var importDtos = await HbtExcelHelper.ImportAsync<HbtMenuImportDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new HbtException(L("Identity.Menu.ImportEmpty"));

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        var menu = item.Adapt<HbtMenu>();
                        menu.CreateTime = DateTime.Now;
                        menu.Status = 0;

                        await _menuRepository.CreateAsync(menu);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("Identity.Menu.Log.ImportFailed", ex.Message), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Menu.Log.ImportDataFailed"), ex);
                throw new HbtException(L("Identity.Menu.ImportFailed"));
            }
        }

        /// <summary>
        /// 导出菜单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtMenuQueryDto query, string sheetName = "HbtMenu")
        {
            try
            {
                var list = await _menuRepository.GetListAsync(HbtMenuQueryExpression(query));
                var exportList = list.Adapt<List<HbtMenuExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "菜单数据");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Menu.Log.ExportDataFailed"), ex);
                throw new HbtException(L("Identity.Menu.ExportFailed"));
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtMenu")
        {
            var (fileName, content) = await HbtExcelHelper.GenerateTemplateAsync<HbtMenuImportDto>(sheetName);
            return (fileName, content);
        }

        /// <summary>
        /// 获取菜单选项列表
        /// </summary>
        /// <returns>菜单选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var menus = await _menuRepository.AsQueryable()
                .Where(m => m.Status == 0)  // 只获取正常状态的菜单
                .OrderBy(m => m.OrderNum)
                .Select(m => new HbtSelectOption
                {
                    Label = m.MenuName,
                    Value = m.Id,
                    Disabled = false
                })
                .ToListAsync();
            return menus;
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtMenuStatusDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId)
                ?? throw new HbtException(L("Identity.Menu.NotFound", input.MenuId));

            input.Adapt(menu);
            return await _menuRepository.UpdateAsync(menu) > 0;
        }

        /// <summary>
        /// 更新菜单排序
        /// </summary>
        /// <param name="input">排序更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateOrderAsync(HbtMenuOrderDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId)
                ?? throw new HbtException(L("Identity.Menu.NotFound", input.MenuId));

            input.Adapt(menu);
            return await _menuRepository.UpdateAsync(menu) > 0;
        }

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns>返回菜单树形结构列表</returns>
        public async Task<List<HbtMenuDto>> GetTreeAsync()
        {
            try
            {
                _logger.Info("[菜单服务] 开始获取菜单树");

                // 获取所有菜单
                var menus = await _menuRepository.AsQueryable()
                    .OrderBy(m => m.OrderNum)
                    .ToListAsync();

                _logger.Info($"[菜单服务] 从数据库获取菜单数: {menus?.Count ?? 0}");

                // 转换为DTO
                var menuDtos = menus.Adapt<List<HbtMenuDto>>();

                _logger.Info($"[菜单服务] 转换后的菜单DTO数: {menuDtos?.Count ?? 0}");

                // 构建树形结构 - 使用ParentId == 0作为根节点判断条件
                var tree = menuDtos.Where(m => m.ParentId == 0).ToList();

                _logger.Info("[菜单服务] 菜单树构建完成");
                return tree;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 获取菜单树时发生错误: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 递归构建菜单树
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="allMenus">所有菜单列表</param>
        private void BuildMenuTree(HbtMenuDto node, List<HbtMenuDto> allMenus)
        {
            try
            {
                var children = allMenus.Where(m => m.ParentId == node.MenuId).ToList();
                if (children.Any())
                {
                    node.Children = children;
                    foreach (var child in children)
                    {
                        BuildMenuTree(child, allMenus);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 构建节点 {node.MenuName}({node.MenuId}) 的子树时发生错误: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取当前用户的菜单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户有权限访问的菜单树形结构</returns>
        public async Task<List<HbtMenuDto>> GetCurrentUserMenusAsync(long userId)
        {
            try
            {
                _logger.Info($"[菜单服务] 开始获取用户菜单: UserId={userId}");

                // 获取用户的角色ID列表
                var roleIdsQuery = _userRoleRepository.AsQueryable()
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.RoleId);

                var roleIds = await roleIdsQuery.ToListAsync();

                _logger.Info($"[菜单服务] 用户角色: Count={roleIds?.Count ?? 0}, RoleIds={string.Join(",", roleIds ?? new List<long>())}");

                if (roleIds == null || !roleIds.Any())
                {
                    _logger.Warn("[菜单服务] 用户没有角色");
                    return new List<HbtMenuDto>();
                }

                // 获取角色的菜单ID列表
                var menuIdsQuery = _roleMenuRepository.AsQueryable()
                    .Where(rm => roleIds.Contains(rm.RoleId))
                    .Select(rm => rm.MenuId)
                    .Distinct();

                var menuIds = await menuIdsQuery.ToListAsync();

                if (menuIds == null || !menuIds.Any())
                {
                    _logger.Warn("[菜单服务] 角色没有关联菜单");
                    return new List<HbtMenuDto>();
                }

                // 获取菜单列表 - 过滤掉按钮类型(MenuType=2)的菜单
                var menusQuery = _menuRepository.AsQueryable()
                    .Where(m => menuIds.Contains(m.Id) && m.Status == 0 && m.MenuType != 2)
                    .OrderBy(m => m.OrderNum);

                var menus = await menusQuery.ToListAsync();

                // 转换为DTO
                var menuDtos = menus.Adapt<List<HbtMenuDto>>();

                // 构建树形结构
                var tree = menuDtos.Where(m => m.ParentId == 0).ToList();
                foreach (var node in tree)
                {
                    BuildMenuTree(node, menuDtos);
                }

                return tree;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 获取用户菜单失败: {ex.Message}", ex);
                throw;
            }
        }
    }
}