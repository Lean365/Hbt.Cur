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
using Lean.Hbt.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 部门服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtDeptService : HbtBaseService, IHbtDeptService
    {
        private readonly IHbtRepository<HbtDept> _deptRepository;
        private readonly IHbtRepository<HbtUser> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptService(
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtRepository<HbtDept> deptRepository,
            IHbtRepository<HbtUser> userRepository,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _deptRepository = deptRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 构建部门查询条件
        /// </summary>
        private Expression<Func<HbtDept, bool>> QueryExpression(HbtDeptQueryDto query)
        {
            var exp = Expressionable.Create<HbtDept>();

            if (!string.IsNullOrEmpty(query.DeptName))
                exp.And(x => x.DeptName.Contains(query.DeptName));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtDeptDto>> GetListAsync(HbtDeptQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await _deptRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtDeptDto>
            {
                Rows = result.Rows.Adapt<List<HbtDeptDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
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
            var tree = dtos.Where(d => d.ParentId == 0).ToList();
            foreach (var node in tree)
            {
                BuildDeptTree(node, dtos);
            }

            return tree;
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        public async Task<HbtDeptDto> GetByIdAsync(long deptId)
        {
            var dept = await _deptRepository.GetFirstAsync(x => x.Id == deptId);
            if (dept == null)
                throw new HbtException("Identity.Dept.NotFound", deptId.ToString());
            return dept.Adapt<HbtDeptDto>();
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public async Task<long> CreateAsync(HbtDeptCreateDto request)
        {
            var dept = request.Adapt<HbtDept>();
            dept.CreateTime = DateTime.Now;
            dept.Status = 1;

            await _deptRepository.CreateAsync(dept);
            return dept.Id;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public async Task<bool> UpdateAsync(HbtDeptUpdateDto request)
        {
            var dept = await _deptRepository.GetFirstAsync(x => x.Id == request.DeptId);
            if (dept == null)
            {
                throw new InvalidOperationException($"Identity.Dept.Operation.UpdateFailed: {request.DeptId}");
            }

            request.Adapt(dept);
            dept.UpdateTime = DateTime.Now;

            return await _deptRepository.UpdateAsync(dept) > 0;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task<bool> DeleteAsync(long deptId)
        {
            var dept = await _deptRepository.GetFirstAsync(x => x.Id == deptId);
            if (dept == null)
            {
                throw new InvalidOperationException($"Identity.Dept.Operation.DeleteFailed: {deptId}");
            }

            return await _deptRepository.DeleteAsync(dept) > 0;
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] deptIds)
        {
            // 1.检查是否存在子部门
            var hasChildren = await _deptRepository.AsQueryable()
                .AnyAsync(d => deptIds.Contains(d.ParentId == 0 ? 0 : d.ParentId));
            if (hasChildren)
                throw new HbtException("Identity.Dept.HasChildren");

            // 2.批量删除
            Expression<Func<HbtDept, bool>> condition = d => deptIds.Contains(d.Id);
            var result = await _deptRepository.DeleteAsync(condition);
            return result > 0;
        }

        /// <summary>
        /// 导入部门数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtDept")
        {
            try
            {
                var importDtos = await HbtExcelHelper.ImportAsync<HbtDeptTemplateDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new HbtException("Identity.Dept.ImportEmpty");

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        var dept = item.Adapt<HbtDept>();
                        dept.CreateTime = DateTime.Now;
                        dept.Status = 0;

                        await _deptRepository.CreateAsync(dept);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Identity.Dept.Log.ImportFailed", ex.Message, ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("Identity.Dept.Log.ImportDataFailed");
                throw new HbtException("Identity.Dept.ImportFailed");
            }
        }

        /// <summary>
        /// 导出部门数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtDeptQueryDto query, string sheetName = "HbtDept")
        {
            try
            {
                var list = await _deptRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<HbtDeptExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "部门数据");
            }
            catch (Exception ex)
            {
                _logger.Error("Identity.Dept.Log.ExportDataFailed");
                throw new HbtException("Identity.Dept.ExportFailed");
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDeptTemplateDto>(sheetName);
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
        /// 获取部门选项列表
        /// </summary>
        /// <returns>部门选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var depts = await _deptRepository.GetListAsync(x => x.Status == 0);
            return depts.Select(x => new HbtSelectOption
            {
                Label = x.DeptName,
                Value = x.Id
            }).ToList();
        }

        /// <summary>
        /// 生成导入模板
        /// </summary>
        public async Task<(string fileName, byte[] content)> GenerateTemplateAsync(string sheetName = "HbtDept")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDeptImportDto>(sheetName);
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