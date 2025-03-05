//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeptService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 部门服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptService : IHbtDeptService
    {
        private readonly ILogger<HbtDeptService> _logger;
        private readonly IHbtRepository<HbtDept> _deptRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptService(
            ILogger<HbtDeptService> logger,
            IHbtRepository<HbtDept> deptRepository)
        {
            _logger = logger;
            _deptRepository = deptRepository;
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtDeptDto>> GetPagedListAsync(HbtDeptQueryDto query)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<HbtDept>();

            if (!string.IsNullOrEmpty(query.DeptName))
                predicate.And(d => d.DeptName.Contains(query.DeptName));

            if (query.Status.HasValue)
                predicate.And(d => d.Status == query.Status.Value);

            // 2.查询数据
            var result = await _deptRepository.GetPagedListAsync(
                predicate.ToExpression(),
                query.PageIndex,
                query.PageSize);

            // 3.转换并返回
            var dtos = result.list.Adapt<List<HbtDeptDto>>();
            return new HbtPagedResult<HbtDeptDto>
            {
                TotalNum = result.total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = dtos
            };
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        public async Task<List<HbtDeptDto>> GetTreeAsync(int status)
        {
            // 1.查询所有部门
            var predicate = Expressionable.Create<HbtDept>();
            predicate.And(d => d.Status == status);

            var depts = await _deptRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderBy(d => d.OrderNum)
                .ToListAsync();

            // 2.转换为DTO
            var dtos = depts.Adapt<List<HbtDeptDto>>();

            // 3.构建树形结构
            var tree = dtos.Where(d => d.ParentId == null).ToList();
            foreach (var node in tree)
            {
                BuildDeptTree(node, dtos);
            }

            return tree;
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        public async Task<HbtDeptDto> GetAsync(long id)
        {
            var dept = await _deptRepository.GetByIdAsync(id);
            if (dept == null)
                throw new HbtException("部门不存在");

            return dept.Adapt<HbtDeptDto>();
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public async Task<long> InsertAsync(HbtDeptCreateDto input)
        {
            // 1.检查部门名称是否存在
            var exists = await _deptRepository.AsQueryable()
                .AnyAsync(d => d.DeptName == input.DeptName && d.ParentId == input.ParentId);
            if (exists)
                throw new HbtException("部门名称已存在");

            // 2.创建部门实体
            var dept = input.Adapt<HbtDept>();

            // 3.保存部门
            var result = await _deptRepository.InsertAsync(dept);
            if (result <= 0)
                throw new HbtException("创建部门失败");

            return dept.Id;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public async Task<bool> UpdateAsync(HbtDeptUpdateDto input)
        {
            // 1.获取部门
            var dept = await _deptRepository.GetByIdAsync(input.DeptId);
            if (dept == null)
                throw new HbtException("部门不存在");

            // 2.检查部门名称是否存在
            var exists = await _deptRepository.AsQueryable()
                .AnyAsync(d => d.Id != input.DeptId && d.DeptName == input.DeptName && d.ParentId == input.ParentId);
            if (exists)
                throw new HbtException("部门名称已存在");

            // 3.检查上级部门是否正确
            if (input.ParentId != null)
            {
                if (input.ParentId == input.DeptId)
                    throw new HbtException("上级部门不能是自己");

                var parent = await _deptRepository.GetByIdAsync(input.ParentId);
                if (parent == null)
                    throw new HbtException("上级部门不存在");

                // 检查是否会形成循环
                if (await HasCircularReference(input.DeptId, input.ParentId))
                    throw new HbtException("不能选择子部门作为上级部门");
            }

            // 4.更新部门
            input.Adapt(dept);
            var result = await _deptRepository.UpdateAsync(dept);
            return result > 0;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            // 1.检查是否存在子部门
            var hasChildren = await _deptRepository.AsQueryable()
                .AnyAsync(d => d.ParentId == id);
            if (hasChildren)
                throw new HbtException("存在子部门，无法删除");

            // 2.删除部门
            var result = await _deptRepository.DeleteAsync(id);
            return result > 0;
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        public async Task<bool> BatchDeleteAsync(List<long> ids)
        {
            // 1.检查是否存在子部门
            var hasChildren = await _deptRepository.AsQueryable()
                .AnyAsync(d => ids.Contains(d.ParentId == 0 ? 0 : d.ParentId));
            if (hasChildren)
                throw new HbtException("选中的部门中存在子部门，无法删除");

            // 2.批量删除
            Expression<Func<HbtDept, bool>> condition = d => ids.Contains(d.Id);
            var result = await _deptRepository.DeleteAsync(condition);
            return result > 0;
        }

        /// <summary>
        /// 导入部门
        /// </summary>
        public async Task<List<HbtDeptTemplateDto>> ImportAsync(Stream fileStream, string sheetName = "部门数据")
        {
            try
            {
                var importDtos = await HbtExcelHelper.ImportAsync<HbtDeptTemplateDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new HbtException("导入数据为空");

                return importDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导入部门数据失败");
                throw;
            }
        }

        /// <summary>
        /// 导出部门
        /// </summary>
        /// <param name="data">要导出的数据</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(IEnumerable<HbtDeptExportDto> data, string sheetName = "部门信息")
        {
            try
            {
                return await HbtExcelHelper.ExportAsync(data, sheetName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导出部门数据失败");
                throw;
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        public async Task<byte[]> GenerateTemplateAsync(string sheetName = "部门导入模板")
        {
            try
            {
                return await HbtExcelHelper.GenerateTemplateAsync<HbtDeptTemplateDto>(sheetName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取部门导入模板失败");
                throw;
            }
        }

        /// <summary>
        /// 更新部门状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(long id, int status)
        {
            var entity = await _deptRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.Status = status;
            await _deptRepository.UpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 构建部门树
        /// </summary>
        private void BuildDeptTree(HbtDeptDto parent, List<HbtDeptDto> depts)
        {
            parent.Children = depts
                .Where(d => d.ParentId == parent.DeptId)
                .OrderBy(d => d.OrderNum)
                .ToList();

            foreach (var child in parent.Children)
            {
                BuildDeptTree(child, depts);
            }
        }

        /// <summary>
        /// 检查是否存在循环引用
        /// </summary>
        private async Task<bool> HasCircularReference(long deptId, long parentId)
        {
            var parent = await _deptRepository.GetByIdAsync(parentId);
            while (parent != null)
            {
                if (parent.Id == deptId)
                    return true;

                if (parent.ParentId == 0)
                    break;

                parent = await _deptRepository.GetByIdAsync(parent.ParentId);
            }

            return false;
        }
    }
}