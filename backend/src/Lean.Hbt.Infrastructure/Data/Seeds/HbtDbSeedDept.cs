//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDept.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 部门数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Infrastructure.Data.Contexts;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 部门数据初始化类
/// </summary>
public class HbtDbSeedDept
{
    private readonly IHbtRepository<HbtDept> _deptRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDept(IHbtRepository<HbtDept> deptRepository, IHbtLogger logger)
    {
        _deptRepository = deptRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化部门数据
    /// </summary>
    public async Task<(int, int)> InitializeDeptAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var rootDept = new HbtDept
        {
            DeptName = "总公司",
            ParentId = 0,
            OrderNum = 1,
            Leader = "管理员",
            Phone = "13800138000",
            Email = "admin@lean365.com",
            Status = 0,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now
        };

        var existingDept = await _deptRepository.FirstOrDefaultAsync(d => d.DeptName == rootDept.DeptName);
        if (existingDept == null)
        {
            await _deptRepository.InsertAsync(rootDept);
            insertCount++;
            _logger.Info($"[创建] 部门 '{rootDept.DeptName}' 创建成功");
        }
        else
        {
            existingDept.DeptName = rootDept.DeptName;
            existingDept.ParentId = rootDept.ParentId;
            existingDept.OrderNum = rootDept.OrderNum;
            existingDept.Leader = rootDept.Leader;
            existingDept.Phone = rootDept.Phone;
            existingDept.Email = rootDept.Email;
            existingDept.Status = rootDept.Status;
            existingDept.TenantId = rootDept.TenantId;
            existingDept.CreateBy = rootDept.CreateBy;
            existingDept.CreateTime = rootDept.CreateTime;
            existingDept.UpdateBy = "system";
            existingDept.UpdateTime = DateTime.Now;

            await _deptRepository.UpdateAsync(existingDept);
            updateCount++;
            _logger.Info($"[更新] 部门 '{existingDept.DeptName}' 更新成功");
        }

        return (insertCount, updateCount);
    }
} 