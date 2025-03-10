using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 用户关联数据初始化类
/// </summary>
public class HbtDbSeedUserRelation
{
    private readonly IHbtRepository<HbtUserDept> _userDeptRepository;
    private readonly IHbtRepository<HbtUserPost> _userPostRepository;
    private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userDeptRepository">用户部门关联仓储</param>
    /// <param name="userPostRepository">用户岗位关联仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedUserRelation(
        IHbtRepository<HbtUserDept> userDeptRepository,
        IHbtRepository<HbtUserPost> userPostRepository,
        IHbtRepository<HbtUserRole> userRoleRepository,
        IHbtLogger logger)
    {
        _userDeptRepository = userDeptRepository;
        _userPostRepository = userPostRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化用户关联数据
    /// </summary>
    public async Task<(int, int)> InitializeUserRelationAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        var defaultUserRelations = new List<(long UserId, long DeptId, long PostId, long[] RoleIds)>
        {
            // 系统管理员 - 总公司 - 董事长 - 系统管理员角色
            (1, 1, 1, new long[] { 1 }),
            // 开发管理员 - IT部门 - CTO - 开发管理员角色
            (2, 5, 19, new long[] { 2, 3 }),
            // 开发工程师 - IT部门 - 正高级工程师 - 开发人员角色
            (3, 5, 50, new long[] { 3 }),
            // 测试工程师 - IT部门 - 高级工程师 - 测试人员角色
            (4, 5, 53, new long[] { 3 }),
            // 普通用户 - 综合管理部 - 事务员 - 普通用户角色
            (5, 2, 64, new long[] { 9 })
        };

        foreach (var relation in defaultUserRelations)
        {
            // 创建用户部门关联
            var userDept = new HbtUserDept
            {
                Id = nextId++,
                UserId = relation.UserId,
                DeptId = relation.DeptId,
                TenantId = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            };

            var existingUserDept = await _userDeptRepository.FirstOrDefaultAsync(ud =>
                ud.UserId == userDept.UserId && ud.DeptId == userDept.DeptId);

            if (existingUserDept == null)
            {
                await _userDeptRepository.InsertAsync(userDept);
                insertCount++;
                _logger.Info($"[创建] 用户部门关联 'UserId:{userDept.UserId}, DeptId:{userDept.DeptId}' 创建成功");
            }
            else
            {
                existingUserDept.TenantId = userDept.TenantId;
                existingUserDept.UpdateBy = "system";
                existingUserDept.UpdateTime = DateTime.Now;

                await _userDeptRepository.UpdateAsync(existingUserDept);
                updateCount++;
                _logger.Info($"[更新] 用户部门关联 'UserId:{existingUserDept.UserId}, DeptId:{existingUserDept.DeptId}' 更新成功");
            }

            // 创建用户岗位关联
            var userPost = new HbtUserPost
            {
                Id = nextId++,
                UserId = relation.UserId,
                PostId = relation.PostId,
                TenantId = 0,
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            };

            var existingUserPost = await _userPostRepository.FirstOrDefaultAsync(up =>
                up.UserId == userPost.UserId && up.PostId == userPost.PostId);

            if (existingUserPost == null)
            {
                await _userPostRepository.InsertAsync(userPost);
                insertCount++;
                _logger.Info($"[创建] 用户岗位关联 'UserId:{userPost.UserId}, PostId:{userPost.PostId}' 创建成功");
            }
            else
            {
                existingUserPost.TenantId = userPost.TenantId;
                existingUserPost.UpdateBy = "system";
                existingUserPost.UpdateTime = DateTime.Now;

                await _userPostRepository.UpdateAsync(existingUserPost);
                updateCount++;
                _logger.Info($"[更新] 用户岗位关联 'UserId:{existingUserPost.UserId}, PostId:{existingUserPost.PostId}' 更新成功");
            }

            // 创建用户角色关联
            foreach (var roleId in relation.RoleIds)
            {
                var userRole = new HbtUserRole
                {
                    Id = nextId++,
                    UserId = relation.UserId,
                    RoleId = roleId,
                    TenantId = 0,
                    CreateBy = "system",
                    CreateTime = DateTime.Now,
                    UpdateBy = "system",
                    UpdateTime = DateTime.Now
                };

                var existingUserRole = await _userRoleRepository.FirstOrDefaultAsync(ur =>
                    ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);

                if (existingUserRole == null)
                {
                    await _userRoleRepository.InsertAsync(userRole);
                    insertCount++;
                    _logger.Info($"[创建] 用户角色关联 'UserId:{userRole.UserId}, RoleId:{userRole.RoleId}' 创建成功");
                }
                else
                {
                    existingUserRole.TenantId = userRole.TenantId;
                    existingUserRole.UpdateBy = "system";
                    existingUserRole.UpdateTime = DateTime.Now;

                    await _userRoleRepository.UpdateAsync(existingUserRole);
                    updateCount++;
                    _logger.Info($"[更新] 用户角色关联 'UserId:{existingUserRole.UserId}, RoleId:{existingUserRole.RoleId}' 更新成功");
                }
            }
        }

        return (insertCount, updateCount);
    }
} 