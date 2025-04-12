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

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 菜单服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuService : IHbtMenuService
    {
        // 日志记录器
        private readonly ILogger<HbtMenuService> _logger;

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
        public HbtMenuService(
            ILogger<HbtMenuService> logger,
            IHbtRepository<HbtMenu> menuRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository,
            IHbtRepository<HbtUserRole> userRoleRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件，包含页码、每页大小、菜单名称、状态等</param>
        /// <returns>返回分页后的菜单列表</returns>
        public async Task<HbtPagedResult<HbtMenuDto>> GetListAsync(HbtMenuQueryDto query)
        {
            // 构建查询条件
            var exp = Expressionable.Create<HbtMenu>();

            // 根据菜单名称模糊查询
            if (!string.IsNullOrEmpty(query.MenuName))
                exp.And(x => x.MenuName.Contains(query.MenuName));

            // 根据状态精确查询
            if (query?.Status.HasValue == true && query.Status!.Value != -1)
                exp.And(x => x.Status == query.Status.Value);

            var pageIndex = query?.PageIndex ?? 1;
            var pageSize = query?.PageSize ?? 10;

            // 执行分页查询
            var result = await _menuRepository.GetPagedListAsync(
                exp.ToExpression(),
                pageIndex,
                pageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            // 转换为DTO并返回
            return new HbtPagedResult<HbtMenuDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = result.Rows.Adapt<List<HbtMenuDto>>()
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
                throw new HbtException($"菜单不存在: {menuId}");

            return menu.Adapt<HbtMenuDto>();
        }

        /// <summary>
        /// 创建新菜单
        /// </summary>
        /// <param name="input">菜单创建信息，包含菜单名称、路径、图标等</param>
        /// <returns>返回新创建的菜单ID</returns>
        /// <exception cref="HbtException">当菜单名称或翻译键已存在时抛出异常</exception>
        public async Task<long> CreateAsync(HbtMenuCreateDto input)
        {
            // 验证菜单名称是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName);

            // 验证翻译键是否已存在
            if (!string.IsNullOrEmpty(input.TransKey))
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "TransKey", input.TransKey);

            // 创建菜单实体
            var menu = new HbtMenu
            {
                MenuName = input.MenuName,
                TransKey = input.TransKey,
                ParentId = input.ParentId ?? 0,
                OrderNum = input.OrderNum,
                Path = input.Path,
                Component = input.Component,
                QueryParams = input.QueryParams,
                IsExternal = input.IsExternal,
                IsCache = input.IsCache,
                MenuType = input.MenuType,
                Visible = input.Visible,
                Status = input.Status,
                Perms = input.Perms,
                Icon = input.Icon,
                TenantId = input.TenantId,
                Remark = input.Remark
            };

            // 保存菜单并返回ID
            var result = await _menuRepository.CreateAsync(menu);
            return menu.Id;
        }

        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="input">菜单更新信息</param>
        /// <returns>返回是否更新成功</returns>
        /// <exception cref="HbtException">当菜单不存在、父菜单不存在或存在循环引用时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtMenuUpdateDto input)
        {
            // 检查菜单是否存在
            var menu = await _menuRepository.GetByIdAsync(input.MenuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {input.MenuId}");

            // 验证菜单名称是否已存在
            if (menu.MenuName != input.MenuName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName);

            // 验证翻译键是否已存在
            if (!string.IsNullOrEmpty(input.TransKey) && menu.TransKey != input.TransKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "TransKey", input.TransKey);

            // 检查是否存在循环引用
            if (input.ParentId != null && input.ParentId == input.MenuId)
                throw new HbtException("父菜单不能是自己");

            // 检查父菜单是否存在
            if (input.ParentId > 0)
            {
                var parentMenu = await _menuRepository.GetByIdAsync(input.ParentId.Value);
                if (parentMenu == null)
                {
                    throw new HbtException("父菜单不存在");
                }
            }

            // 更新菜单信息
            menu.MenuName = input.MenuName;
            menu.TransKey = input.TransKey;
            menu.ParentId = input.ParentId ?? 0L;
            menu.OrderNum = input.OrderNum;
            menu.Path = input.Path;
            menu.Component = input.Component;
            menu.QueryParams = input.QueryParams;
            menu.IsExternal = input.IsExternal;
            menu.IsCache = input.IsCache;
            menu.MenuType = input.MenuType;
            menu.Visible = input.Visible;
            menu.Status = input.Status;
            menu.Perms = input.Perms;
            menu.Icon = input.Icon;
            menu.Remark = input.Remark;

            // 保存更新
            var result = await _menuRepository.UpdateAsync(menu);
            return result > 0;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">要删除的菜单ID</param>
        /// <returns>返回是否删除成功</returns>
        /// <exception cref="HbtException">当菜单不存在或存在子菜单时抛出异常</exception>
        public async Task<bool> DeleteAsync(long menuId)
        {
            // 检查菜单是否存在
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {menuId}");

            // 检查是否有子菜单
            var hasChildren = await _menuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
            if (hasChildren)
                throw new HbtException("存在子菜单,不允许删除");

            // 删除菜单及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.MenuId == menuId));
            var result = await _menuRepository.DeleteAsync((Expression<Func<HbtMenu, bool>>)(x => x.Id == menuId));

            return result > 0;
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">要删除的菜单ID列表</param>
        /// <returns>返回是否删除成功</returns>
        /// <exception cref="HbtException">当菜单列表为空时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] menuIds)
        {
            if (menuIds == null || menuIds.Length == 0)
                throw new HbtException("请选择要删除的菜单");

            Expression<Func<HbtMenu, bool>> condition = m => menuIds.Contains(m.Id);
            var result = await _menuRepository.DeleteAsync(condition);
            return result > 0;
        }

        /// <summary>
        /// 导出菜单数据到Excel
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回Excel文件的字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtMenuQueryDto query, string sheetName = "菜单数据")
        {
            // 构建查询条件
            var predicate = Expressionable.Create<HbtMenu>();

            if (!string.IsNullOrEmpty(query.MenuName))
                predicate.And(m => m.MenuName.Contains(query.MenuName));

            if (query.Status.HasValue)
                predicate.And(m => m.Status == query.Status.Value);

            // 查询数据
            var menus = await _menuRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderBy(m => m.OrderNum)
                .ToListAsync();

            // 转换并导出
            var exportDtos = menus.Adapt<List<HbtMenuExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 生成菜单导入模板
        /// </summary>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回Excel模板文件的字节数组</returns>
        public async Task<byte[]> GenerateTemplateAsync(string sheetName = "菜单导入模板")
        {
            var template = new List<HbtMenuTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
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
        /// 导入菜单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入的菜单数据集合</returns>
        public async Task<List<HbtMenuImportDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importDtos = await HbtExcelHelper.ImportAsync<HbtMenuImportDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new HbtException("导入数据为空");

                return importDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导入菜单数据失败");
                throw;
            }
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="input">菜单状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
        /// <exception cref="HbtException">当菜单不存在时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(HbtMenuStatusDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {input.MenuId}");

            menu.Status = input.Status;
            var result = await _menuRepository.UpdateAsync(menu);
            return result > 0;
        }

        /// <summary>
        /// 更新菜单排序
        /// </summary>
        /// <param name="input">菜单排序更新信息</param>
        /// <returns>返回是否更新成功</returns>
        /// <exception cref="HbtException">当菜单不存在时抛出异常</exception>
        public async Task<bool> UpdateOrderAsync(HbtMenuOrderDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {input.MenuId}");

            menu.OrderNum = input.OrderNum;
            var result = await _menuRepository.UpdateAsync(menu);
            return result > 0;
        }

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns>返回菜单树形结构列表</returns>
        public async Task<List<HbtMenuDto>> GetTreeAsync()
        {
            try
            {
                _logger.LogInformation("[菜单服务] 开始获取菜单树");

                // 获取所有菜单
                var menus = await _menuRepository.AsQueryable()
                    .OrderBy(m => m.OrderNum)
                    .ToListAsync();

                _logger.LogInformation("[菜单服务] 从数据库获取菜单数: {Count}", menus?.Count ?? 0);

                // 转换为DTO
                var menuDtos = menus.Adapt<List<HbtMenuDto>>();

                _logger.LogInformation("[菜单服务] 转换后的菜单DTO数: {Count}", menuDtos?.Count ?? 0);

                // 构建树形结构 - 使用ParentId == 0作为根节点判断条件
                var tree = menuDtos.Where(m => m.ParentId == 0).ToList();

                //_logger.LogInformation("[菜单服务] 找到根节点数: {Count}, 根节点: {RootNodes}",
                //    tree?.Count ?? 0,
                //    string.Join(", ", tree?.Select(n => $"{n.MenuName}({n.MenuId})") ?? new List<string>()));

                foreach (var node in tree)
                {
                    BuildMenuTree(node, menuDtos);
                }

                _logger.LogInformation("[菜单服务] 菜单树构建完成");
                return tree;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[菜单服务] 获取菜单树时发生错误: {Message}", ex.Message);
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
                    //_logger.LogInformation("[菜单服务] 节点 {NodeName}({NodeId}) 找到 {Count} 个子节点",
                    //    node.MenuName, node.MenuId, children.Count);

                    node.Children = children;
                    foreach (var child in children)
                    {
                        BuildMenuTree(child, allMenus);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[菜单服务] 构建节点 {NodeName}({NodeId}) 的子树时发生错误: {Message}",
                    node.MenuName, node.MenuId, ex.Message);
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
                _logger.LogInformation("[菜单服务] 开始获取用户菜单: UserId={UserId}", userId);

                // 获取用户的角色ID列表
                var roleIdsQuery = _userRoleRepository.AsQueryable()
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.RoleId);

                //_logger.LogInformation("[菜单服务] 用户角色查询SQL: {Sql}",
                //    roleIdsQuery.ToSql());

                var roleIds = await roleIdsQuery.ToListAsync();

                _logger.LogInformation("[菜单服务] 用户角色: Count={Count}, RoleIds={RoleIds}",
                    roleIds?.Count ?? 0,
                    string.Join(",", roleIds ?? new List<long>()));

                if (roleIds == null || !roleIds.Any())
                {
                    _logger.LogWarning("[菜单服务] 用户没有角色");
                    return new List<HbtMenuDto>();
                }

                // 获取角色的菜单ID列表
                var menuIdsQuery = _roleMenuRepository.AsQueryable()
                    .Where(rm => roleIds.Contains(rm.RoleId))
                    .Select(rm => rm.MenuId)
                    .Distinct();

                //_logger.LogInformation("[菜单服务] 角色菜单查询SQL: {Sql}",
                //    menuIdsQuery.ToSql());

                var menuIds = await menuIdsQuery.ToListAsync();

                // _logger.LogInformation("[菜单服务] 角色菜单: Count={Count}, MenuIds={MenuIds}",
                //     menuIds?.Count ?? 0,
                //     string.Join(",", menuIds ?? new List<long>()));

                if (menuIds == null || !menuIds.Any())
                {
                    _logger.LogWarning("[菜单服务] 角色没有关联菜单");
                    return new List<HbtMenuDto>();
                }

                // 获取菜单列表 - 过滤掉按钮类型(MenuType=2)的菜单
                var menusQuery = _menuRepository.AsQueryable()
                    .Where(m => menuIds.Contains(m.Id) && m.Status == 0 && m.MenuType != 2)
                    .OrderBy(m => m.OrderNum);

                // _logger.LogInformation("[菜单服务] 菜单查询SQL: {Sql}",
                //     menusQuery.ToSql());

                var menus = await menusQuery.ToListAsync();

                //_logger.LogInformation("[菜单服务] 获取到的菜单: Count={Count}, MenuNames={MenuNames}, MenuTypes={MenuTypes}",
                //    menus?.Count ?? 0,
                //    string.Join(",", menus?.Select(m => m.MenuName) ?? new List<string>()),
                //    string.Join(",", menus?.Select(m => m.MenuType) ?? new List<int>()));

                // 转换为DTO
                var menuDtos = menus.Adapt<List<HbtMenuDto>>();

                // 构建树形结构
                var tree = menuDtos.Where(m => m.ParentId == 0).ToList();
                foreach (var node in tree)
                {
                    BuildMenuTree(node, menuDtos);
                }

                //_logger.LogInformation("[菜单服务] 构建的菜单树: Count={Count}, RootMenus={RootMenus}",
                //    tree?.Count ?? 0,
                //    string.Join(",", tree?.Select(m => m.MenuName) ?? new List<string>()));

                return tree;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[菜单服务] 获取用户菜单失败: {Message}", ex.Message);
                throw;
            }
        }
    }
}