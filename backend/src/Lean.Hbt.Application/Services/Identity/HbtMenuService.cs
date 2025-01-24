//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtMenuService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单服务实现
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
    /// 菜单服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtMenuService : IHbtMenuService
    {
        private readonly ILogger<HbtMenuService> _logger;
        private readonly IHbtRepository<HbtMenu> _menuRepository;
        private readonly IHbtRepository<HbtRoleMenu> _roleMenuRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="menuRepository">菜单仓库</param>
        /// <param name="roleMenuRepository">角色菜单仓库</param>
        public HbtMenuService(
            ILogger<HbtMenuService> logger,
            IHbtRepository<HbtMenu> menuRepository,
            IHbtRepository<HbtRoleMenu> roleMenuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtMenuDto>> GetPagedListAsync(HbtMenuQueryDto query)
        {
            var exp = Expressionable.Create<HbtMenu>();

            if (!string.IsNullOrEmpty(query.MenuName))
                exp.And(x => x.MenuName.Contains(query.MenuName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _menuRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);

            return new HbtPagedResult<HbtMenuDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.list.Adapt<List<HbtMenuDto>>()
            };
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>返回菜单详情</returns>
        public async Task<HbtMenuDto> GetAsync(long menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {menuId}");

            return menu.Adapt<HbtMenuDto>();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">菜单创建信息</param>
        /// <returns>返回新创建的菜单ID</returns>
        public async Task<long> InsertAsync(HbtMenuCreateDto input)
        {
            // 验证菜单名称是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName);

            // 创建菜单
            var menu = new HbtMenu
            {
                MenuName = input.MenuName,
                TransKey = input.TransKey,
                ParentId = input.ParentId,
                OrderNum = input.OrderNum,
                Path = input.Path,
                Component = input.Component,
                QueryParams = input.QueryParams,
                IsFrame = input.IsFrame,
                IsCache = input.IsCache,
                MenuType = input.MenuType,
                Visible = input.Visible,
                Status = input.Status,
                Perms = input.Perms,
                Icon = input.Icon,
                TenantId = input.TenantId,
                Remark = input.Remark
            };

            var result = await _menuRepository.InsertAsync(menu);
            return menu.Id;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">菜单更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateAsync(HbtMenuUpdateDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {input.MenuId}");

            // 验证菜单名称是否已存在
            if (menu.MenuName != input.MenuName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", input.MenuName);

            // 检查是否存在循环引用
            if (input.ParentId.HasValue && input.ParentId.Value == input.MenuId)
                throw new HbtException("父菜单不能是自己");

            // 更新菜单信息
            menu.MenuName = input.MenuName;
            menu.TransKey = input.TransKey;
            menu.ParentId = input.ParentId;
            menu.OrderNum = input.OrderNum;
            menu.Path = input.Path;
            menu.Component = input.Component;
            menu.QueryParams = input.QueryParams;
            menu.IsFrame = input.IsFrame;
            menu.IsCache = input.IsCache;
            menu.MenuType = input.MenuType;
            menu.Visible = input.Visible;
            menu.Status = input.Status;
            menu.Perms = input.Perms;
            menu.Icon = input.Icon;
            menu.Remark = input.Remark;

            var result = await _menuRepository.UpdateAsync(menu);
            return result > 0;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> DeleteAsync(long menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {menuId}");

            // 检查是否有子菜单
            var hasChildren = await _menuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
            if (hasChildren)
                throw new HbtException("存在子菜单,不允许删除");

            // 删除菜单及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => x.MenuId == menuId));
            var result = await _menuRepository.DeleteAsync(menuId);

            return result > 0;
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单ID列表</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(List<long> menuIds)
        {
            if (menuIds == null || !menuIds.Any())
                throw new HbtException("请选择要删除的菜单");

            // 检查是否有子菜单
            var hasChildren = await _menuRepository.AsQueryable().AnyAsync(x => menuIds.Contains(x.ParentId ?? 0));
            if (hasChildren)
                throw new HbtException("选中的菜单中存在子菜单,不允许删除");

            // 删除菜单及其关联数据
            await _roleMenuRepository.DeleteAsync((Expression<Func<HbtRoleMenu, bool>>)(x => menuIds.Contains(x.MenuId)));
            var result = await _menuRepository.DeleteAsync((Expression<Func<HbtMenu, bool>>)(x => menuIds.Contains(x.Id)));

            return result > 0;
        }

        /// <summary>
        /// 导出菜单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtMenuQueryDto query, string sheetName = "菜单数据")
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtMenu>();

            if (!string.IsNullOrEmpty(query.MenuName))
                predicate.And(m => m.MenuName.Contains(query.MenuName));

            if (query.Status.HasValue)
                predicate.And(m => m.Status == query.Status.Value);

            // 2.查询数据
            var menus = await _menuRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderBy(m => m.OrderNum)
                .ToListAsync();

            // 3.转换并导出
            var exportDtos = menus.Adapt<List<HbtMenuExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 生成菜单导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入模板Excel文件字节数组</returns>
        public async Task<byte[]> GenerateTemplateAsync(string sheetName = "菜单导入模板")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtMenuTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入菜单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入的菜单数据集合</returns>
        public async Task<List<HbtMenuImportDto>> ImportAsync(Stream fileStream, string sheetName = "菜单数据")
        {
            // 1.从Excel导入数据
            var menus = await HbtExcelHelper.ImportAsync<HbtMenuImportDto>(fileStream, sheetName);
            if (!menus.Any())
                return new List<HbtMenuImportDto>();

            // 2.检查菜单名称是否存在
            foreach (var dto in menus)
            {
                await HbtValidateUtils.ValidateFieldExistsAsync(_menuRepository, "MenuName", dto.MenuName);
            }

            // 3.转换为实体并批量插入
            var entities = menus.Select(dto => new HbtMenu
            {
                MenuName = dto.MenuName,
                TransKey = dto.TransKey,
                ParentId = dto.ParentId,
                OrderNum = dto.OrderNum,
                Path = dto.Path,
                Component = dto.Component,
                QueryParams = dto.QueryParams,
                IsFrame = dto.IsFrame == "是" ? HbtYesNo.Yes : HbtYesNo.No,
                IsCache = dto.IsCache == "是" ? HbtYesNo.Yes : HbtYesNo.No,
                MenuType = dto.MenuType == "目录" ? HbtMenuType.Directory :
                          dto.MenuType == "菜单" ? HbtMenuType.Menu :
                          HbtMenuType.Button,
                Visible = dto.Visible == "显示" ? HbtVisible.Show : HbtVisible.Hide,
                Status = dto.Status == "正常" ? HbtStatus.Normal : HbtStatus.Disabled,
                Perms = dto.Perms,
                Icon = dto.Icon
            }).ToList();

            await _menuRepository.InsertRangeAsync(entities);
            return menus;
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="input">菜单状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
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
        /// <param name="input">排序对象</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateOrderAsync(HbtMenuOrderDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(input.MenuId);
            if (menu == null)
                throw new HbtException($"菜单不存在: {input.MenuId}");

            menu.OrderNum = input.OrderNum;
            var result = await _menuRepository.UpdateAsync(menu);

            return result > 0;
        }
    }
} 