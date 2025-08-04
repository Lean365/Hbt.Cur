//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrmEmployee.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM员工数据初始化类 - 使用仓储工厂模式
//===================================================================

using Hbt.Cur.Domain.Entities.Human.Employee;
using Hbt.Cur.Domain.Repositories;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// HRM员工数据初始化类
/// </summary>
public class HbtDbSeedHrmEmployee
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtEmployee> EmployeeRepository => _repositoryFactory.GetBusinessRepository<HbtEmployee>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedHrmEmployee(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化员工数据
    /// </summary>
    public async Task<(int, int)> InitializeEmployeeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 创建员工列表
        var defaultEmployees = new List<HbtEmployee>();

        // HR总监
        var hrDirector = new HbtEmployee
        {
            Id = nextId++,
            EmployeeNo = "HR001",
            EmployeeName = "张人力资源",
            EnglishName = "Zhang HR",
            Gender = 1, // 男
            BirthDate = new DateTime(1985, 5, 15),
            IdCard = "110101198505150001",
            Mobile = "13800138001",
            Email = "hr.director@lean365.com",
            DepartmentId = 1, // 人力资源部
            PositionId = 1, // 人力资源总监
            EmployeeType = 1, // 正式员工
            HireDate = new DateTime(2020, 1, 1),
            RegularDate = new DateTime(2020, 4, 1),
            Status = 1, // 在职
            WorkLocation = "北京",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultEmployees.Add(hrDirector);

        // HR经理
        var hrManager = new HbtEmployee
        {
            Id = nextId++,
            EmployeeNo = "HR002",
            EmployeeName = "李人事",
            EnglishName = "Li Personnel",
            Gender = 2, // 女
            BirthDate = new DateTime(1988, 8, 20),
            IdCard = "110101198808200002",
            Mobile = "13800138002",
            Email = "hr.manager@lean365.com",
            DepartmentId = 1, // 人力资源部
            PositionId = 2, // 人力资源经理
            EmployeeType = 1, // 正式员工
            HireDate = new DateTime(2021, 3, 1),
            RegularDate = new DateTime(2021, 6, 1),
            Status = 1, // 在职
            WorkLocation = "北京",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultEmployees.Add(hrManager);

        // 招聘专员
        var recruitmentSpecialist = new HbtEmployee
        {
            Id = nextId++,
            EmployeeNo = "HR003",
            EmployeeName = "王招聘",
            EnglishName = "Wang Recruitment",
            Gender = 2, // 女
            BirthDate = new DateTime(1990, 12, 10),
            IdCard = "110101199012100003",
            Mobile = "13800138003",
            Email = "recruitment@lean365.com",
            DepartmentId = 2, // 招聘组
            PositionId = 4, // 招聘专员
            EmployeeType = 1, // 正式员工
            HireDate = new DateTime(2022, 6, 1),
            RegularDate = new DateTime(2022, 9, 1),
            Status = 1, // 在职
            WorkLocation = "北京",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultEmployees.Add(recruitmentSpecialist);

        // 培训专员
        var trainingSpecialist = new HbtEmployee
        {
            Id = nextId++,
            EmployeeNo = "HR004",
            EmployeeName = "赵培训",
            EnglishName = "Zhao Training",
            Gender = 1, // 男
            BirthDate = new DateTime(1992, 3, 25),
            IdCard = "110101199203250004",
            Mobile = "13800138004",
            Email = "training@lean365.com",
            DepartmentId = 3, // 培训组
            PositionId = 5, // 培训专员
            EmployeeType = 1, // 正式员工
            HireDate = new DateTime(2022, 9, 1),
            RegularDate = new DateTime(2022, 12, 1),
            Status = 1, // 在职
            WorkLocation = "北京",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultEmployees.Add(trainingSpecialist);

        // 薪酬专员
        var compensationSpecialist = new HbtEmployee
        {
            Id = nextId++,
            EmployeeNo = "HR005",
            EmployeeName = "钱薪酬",
            EnglishName = "Qian Compensation",
            Gender = 2, // 女
            BirthDate = new DateTime(1989, 7, 8),
            IdCard = "110101198907080005",
            Mobile = "13800138005",
            Email = "compensation@lean365.com",
            DepartmentId = 4, // 薪酬福利组
            PositionId = 6, // 薪酬专员
            EmployeeType = 1, // 正式员工
            HireDate = new DateTime(2021, 12, 1),
            RegularDate = new DateTime(2022, 3, 1),
            Status = 1, // 在职
            WorkLocation = "北京",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultEmployees.Add(compensationSpecialist);

        // 批量插入或更新数据
        foreach (var employee in defaultEmployees)
        {
            var existingEmployee = await EmployeeRepository.GetFirstAsync(x => x.EmployeeNo == employee.EmployeeNo);
            
            if (existingEmployee == null)
            {
                await EmployeeRepository.CreateAsync(employee);
                insertCount++;
                _logger.Info($"[创建] 员工 '{employee.EmployeeName}' 创建成功");
            }
            else
            {
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.EnglishName = employee.EnglishName;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.Mobile = employee.Mobile;
                existingEmployee.Email = employee.Email;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.PositionId = employee.PositionId;
                existingEmployee.EmployeeType = employee.EmployeeType;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.RegularDate = employee.RegularDate;
                existingEmployee.WorkLocation = employee.WorkLocation;
                existingEmployee.Status = employee.Status;
                existingEmployee.UpdateBy = "Hbt365";
                existingEmployee.UpdateTime = DateTime.Now;
                
                await EmployeeRepository.UpdateAsync(existingEmployee);
                updateCount++;
                _logger.Info($"[更新] 员工 '{employee.EmployeeName}' 更新成功");
            }
        }

        _logger.Info($"员工数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
} 