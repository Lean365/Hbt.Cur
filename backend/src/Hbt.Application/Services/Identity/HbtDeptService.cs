//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDeptService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门服务实现 - 使用仓储工厂模式
//===================================================================

using System.Linq.Expressions;
using Hbt.Cur.Application.Dtos.Identity;
using Hbt.Cur.Domain.Entities.Identity;
using Hbt.Cur.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Hbt.Cur.Application.Services.Identity
{
    /// <summary>
    /// 部门服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtDeptService : HbtBaseService, IHbtDeptService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtDept> DeptRepository => _repositoryFactory.GetAuthRepository<HbtDept>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDeptService(
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtRepositoryFactory repositoryFactory,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtDeptDto>> GetListAsync(HbtDeptQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await DeptRepository.GetPagedListAsync(
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
        /// 获取部门树形结构（支持名称递归查询）
        /// </summary>
        public async Task<List<HbtDeptDto>> GetTreeAsync(HbtDeptQueryDto query)
        {
            
            // 1. 查询所有部门
            var allDepts = await DeptRepository.AsQueryable()
                .OrderBy(d => d.OrderNum)
                .ToListAsync();

            // 2. 找到所有名称匹配的部门
            var matchedDepts = allDepts
                .Where(d => string.IsNullOrEmpty(query.DeptName) || d.DeptName.Contains(query.DeptName))
                .ToList();

            // 3. 递归查找所有父节点
            var resultSet = new HashSet<long>(matchedDepts.Select(d => d.Id));
            void AddParent(long parentId)
            {
                if (parentId == 0) return;
                var parent = allDepts.FirstOrDefault(d => d.Id == parentId);
                if (parent != null && resultSet.Add(parent.Id))
                    AddParent(parent.ParentId);
            }
            foreach (var dept in matchedDepts)
                AddParent(dept.ParentId);

            // 4. 递归查找所有子节点
            void AddChildren(long id)
            {
                var children = allDepts.Where(d => d.ParentId == id).ToList();
                foreach (var child in children)
                {
                    if (resultSet.Add(child.Id))
                        AddChildren(child.Id);
                }
            }
            foreach (var dept in matchedDepts)
                AddChildren(dept.Id);

            // 5. 过滤出所有相关部门
            var filteredDepts = allDepts.Where(d => resultSet.Contains(d.Id)).ToList();
            var dtos = filteredDepts.Adapt<List<HbtDeptDto>>();

            // 6. 构建树形结构
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
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == deptId);
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

            await DeptRepository.CreateAsync(dept);
            return dept.Id;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public async Task<bool> UpdateAsync(HbtDeptUpdateDto request)
        {
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == request.DeptId);
            if (dept == null)
            {
                throw new InvalidOperationException($"Identity.Dept.Operation.UpdateFailed: {request.DeptId}");
            }

            request.Adapt(dept);
            dept.UpdateTime = DateTime.Now;

            return await DeptRepository.UpdateAsync(dept) > 0;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task<bool> DeleteAsync(long deptId)
        {
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == deptId);
            if (dept == null)
            {
                throw new InvalidOperationException($"Identity.Dept.Operation.DeleteFailed: {deptId}");
            }

            return await DeptRepository.DeleteAsync(dept) > 0;
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] deptIds)
        {
            
            // 1.检查是否存在子部门
            var hasChildren = await DeptRepository.AsQueryable()
                .AnyAsync(d => deptIds.Contains(d.ParentId == 0 ? 0 : d.ParentId));
            if (hasChildren)
                throw new HbtException("Identity.Dept.HasChildren");

            // 2.批量删除
            Expression<Func<HbtDept, bool>> condition = d => deptIds.Contains(d.Id);
            var result = await DeptRepository.DeleteAsync(condition);
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

                        await DeptRepository.CreateAsync(dept);
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
            catch (Exception)
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
                var list = await DeptRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<HbtDeptExportDto>>();
                return await HbtExcelHelper.ExportAsync(exportList, sheetName, "部门数据");
            }
            catch (Exception)
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
            var entity = await DeptRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.Status = status;
            await DeptRepository.UpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 获取部门选项列表
        /// </summary>
        /// <returns>部门选项列表</returns>
        public async Task<List<HbtSelectOption>> GetOptionsAsync()
        {
            var depts = await DeptRepository.GetListAsync(x => x.Status == 0);
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
        /// 分配部门用户
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>是否分配成功</returns>
        public async Task<bool> AllocateDeptUsersAsync(long deptId, long[] userIds)
        {
            try
            {
                _logger.Info($"开始分配部门用户 - 部门ID: {deptId}, 用户IDs: {string.Join(",", userIds)}");

                // 1. 验证部门是否存在且状态正常
                var dept = await DeptRepository.GetByIdAsync(deptId);
                if (dept == null)
                    throw new HbtException(L("Identity.Dept.NotFound"));
                if (dept.Status != 0)
                    throw new HbtException(L("Identity.Dept.Disabled"));

                // 2. 获取部门现有关联的用户（包括已删除的）
                var userDeptRepository = _repositoryFactory.GetAuthRepository<HbtUserDept>();
                var existingUsers = await userDeptRepository.GetListAsync(ud => ud.DeptId == deptId);
                _logger.Info($"部门现有关联用户数量: {existingUsers.Count}");

                // 3. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
                var usersToDelete = existingUsers.Where(ud => !userIds.Contains(ud.UserId)).ToList();
                if (usersToDelete.Any())
                {
                    _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                    foreach (var user in usersToDelete)
                    {
                        // 标记删除
                        user.IsDeleted = 1; // 1 表示已删除
                        user.DeleteBy = _currentUser.UserName;
                        user.DeleteTime = DateTime.Now;
                        await userDeptRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联状态更新和删除标记完成");
                }

                // 4. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
                var usersToRestore = existingUsers.Where(ud => userIds.Contains(ud.UserId) && ud.IsDeleted == 1).ToList();
                if (usersToRestore.Any())
                {
                    _logger.Info($"需要恢复的用户关联数量: {usersToRestore.Count}, 用户IDs: {string.Join(",", usersToRestore.Select(d => d.UserId))}");
                    foreach (var user in usersToRestore)
                    {
                        // 取消删除标记
                        user.IsDeleted = 0; // 0 表示未删除
                        user.DeleteBy = null;
                        user.DeleteTime = null;
                        await userDeptRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联状态恢复和删除标记取消完成");
                }

                // 5. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
                var existingUserIds = existingUsers.Select(ud => ud.UserId).ToList();
                var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                    .Select(userId => new HbtUserDept
                    {
                        UserId = userId,
                        DeptId = deptId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (usersToAdd.Any())
                {
                    _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                    await userDeptRepository.CreateRangeAsync(usersToAdd);
                    _logger.Info("用户关联新增完成");
                }

                _logger.Info("部门用户分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配部门用户失败: {ex.Message}", ex);
                throw;
            }
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
            var deptRepository = _repositoryFactory.GetAuthRepository<HbtDept>();
            var parent = await deptRepository.GetByIdAsync(parentId);
            while (parent != null)
            {
                if (parent.Id == deptId)
                    return true;

                if (parent.ParentId == 0)
                    break;

                parent = await deptRepository.GetByIdAsync(parent.ParentId);
            }

            return false;
        }
        /// <summary>
        /// 构建部门查询条件
        /// </summary>
        private static Expression<Func<HbtDept, bool>> QueryExpression(HbtDeptQueryDto query)
        {
            var exp = Expressionable.Create<HbtDept>();

            if (!string.IsNullOrEmpty(query.DeptName))
                exp.And(x => x.DeptName.Contains(query.DeptName));

            if (query.Status.HasValue && query.Status.Value != -1)
                exp.And(x => x.Status == query.Status.Value);

            return exp.ToExpression();
        }
    }
} 