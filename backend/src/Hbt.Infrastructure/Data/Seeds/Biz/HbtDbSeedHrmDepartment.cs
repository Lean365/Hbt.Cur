//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrmDepartment.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM部门数据初始化类 - 使用仓储工厂模式
//===================================================================

using Hbt.Domain.Entities.Human.Organization;
using Hbt.Domain.Repositories;

namespace Hbt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// HRM部门数据初始化类
/// </summary>
public class HbtDbSeedHrmDepartment
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtDepartment> DepartmentRepository => _repositoryFactory.GetBusinessRepository<HbtDepartment>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedHrmDepartment(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化HRM部门数据
    /// </summary>
    public async Task<(int, int)> InitializeDepartmentAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 创建HRM部门列表
        var defaultDepartments = new List<HbtDepartment>();

        // 人力资源部
        var hrDepartment = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR",
            DeptName = "人力资源部",
            EnglishName = "Human Resources Department",
            ParentId = null,
            DeptLevel = 1,
            DeptType = 2,
            OrderNum = 1,
            Phone = "13800138001",
            Email = "hr@lean365.com",
            Status = 1,
            Description = "负责公司人力资源管理工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(hrDepartment);

        // 招聘组
        var recruitmentGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-REC",
            DeptName = "招聘组",
            EnglishName = "Recruitment Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 1,
            Phone = "13800138002",
            Email = "recruitment@lean365.com",
            Status = 1,
            Description = "负责公司招聘工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(recruitmentGroup);

        // 培训组
        var trainingGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-TRAIN",
            DeptName = "培训组",
            EnglishName = "Training Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 2,
            Phone = "13800138003",
            Email = "training@lean365.com",
            Status = 1,
            Description = "负责公司培训工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(trainingGroup);

        // 薪酬福利组
        var compensationGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-COMP",
            DeptName = "薪酬福利组",
            EnglishName = "Compensation & Benefits Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 3,
            Phone = "13800138004",
            Email = "compensation@lean365.com",
            Status = 1,
            Description = "负责公司薪酬福利管理工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(compensationGroup);

        // 绩效管理组
        var performanceGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-PERF",
            DeptName = "绩效管理组",
            EnglishName = "Performance Management Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 4,
            Phone = "13800138005",
            Email = "performance@lean365.com",
            Status = 1,
            Description = "负责公司绩效管理工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(performanceGroup);

        // 员工关系组
        var employeeRelationGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-ER",
            DeptName = "员工关系组",
            EnglishName = "Employee Relations Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 5,
            Phone = "13800138006",
            Email = "er@lean365.com",
            Status = 1,
            Description = "负责公司员工关系管理工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(employeeRelationGroup);

        // 组织发展组
        var organizationDevelopmentGroup = new HbtDepartment
        {
            Id = nextId++,
            DeptCode = "HR-OD",
            DeptName = "组织发展组",
            EnglishName = "Organization Development Group",
            ParentId = hrDepartment.Id,
            DeptLevel = 2,
            DeptType = 4,
            OrderNum = 6,
            Phone = "13800138007",
            Email = "od@lean365.com",
            Status = 1,
            Description = "负责公司组织发展工作",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultDepartments.Add(organizationDevelopmentGroup);

        // 批量插入或更新数据
        foreach (var department in defaultDepartments)
        {
            var existingDepartment = await DepartmentRepository.GetFirstAsync(x => x.DeptCode == department.DeptCode);
            
            if (existingDepartment == null)
            {
                await DepartmentRepository.CreateAsync(department);
                insertCount++;
                _logger.Info($"[创建] HRM部门 '{department.DeptName}' 创建成功");
            }
            else
            {
                existingDepartment.DeptName = department.DeptName;
                existingDepartment.EnglishName = department.EnglishName;
                existingDepartment.ParentId = department.ParentId;
                existingDepartment.DeptLevel = department.DeptLevel;
                existingDepartment.DeptType = department.DeptType;
                existingDepartment.OrderNum = department.OrderNum;
                existingDepartment.Phone = department.Phone;
                existingDepartment.Email = department.Email;
                existingDepartment.Status = department.Status;
                existingDepartment.Description = department.Description;
                existingDepartment.UpdateBy = "Hbt365";
                existingDepartment.UpdateTime = DateTime.Now;
                
                await DepartmentRepository.UpdateAsync(existingDepartment);
                updateCount++;
                _logger.Info($"[更新] HRM部门 '{department.DeptName}' 更新成功");
            }
        }

        _logger.Info($"HRM部门数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
} 