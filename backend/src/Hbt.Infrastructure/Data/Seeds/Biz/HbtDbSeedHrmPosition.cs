//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrmPosition.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM职位数据初始化类 - 使用仓储工厂模式
//===================================================================

using Hbt.Cur.Domain.Entities.Human.Organization;
using Hbt.Cur.Domain.Repositories;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// HRM职位数据初始化类
/// </summary>
public class HbtDbSeedHrmPosition
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtPosition> PositionRepository => _repositoryFactory.GetBusinessRepository<HbtPosition>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedHrmPosition(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化职位数据
    /// </summary>
    public async Task<(int, int)> InitializePositionAsync()
    {
        int insertCount = 0;
        int updateCount = 0;
        long nextId = 1;

        // 创建职位列表
        var defaultPositions = new List<HbtPosition>();

        // 管理序列职位
        var hrDirector = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-DIR",
            PosName = "人力资源总监",
            EnglishName = "HR Director",
            CategoryId = 1, // 管理序列
            PosLevel = 4, // 高级
            PosSequence = 1, // 管理序列
            Status = 1,
            OrderNum = 1,
            Description = "负责公司人力资源战略规划和全面管理工作",
            Responsibilities = "制定人力资源战略、组织架构设计、人才发展规划、薪酬福利体系设计等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(hrDirector);

        var hrManager = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-MGR",
            PosName = "人力资源经理",
            EnglishName = "HR Manager",
            CategoryId = 1, // 管理序列
            PosLevel = 3, // 高级
            PosSequence = 1, // 管理序列
            Status = 1,
            OrderNum = 2,
            Description = "负责部门人力资源管理工作",
            Responsibilities = "招聘管理、培训管理、绩效管理、员工关系管理等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(hrManager);

        var hrSupervisor = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-SUP",
            PosName = "人力资源主管",
            EnglishName = "HR Supervisor",
            CategoryId = 1, // 管理序列
            PosLevel = 2, // 中级
            PosSequence = 1, // 管理序列
            Status = 1,
            OrderNum = 3,
            Description = "负责具体人力资源模块管理工作",
            Responsibilities = "招聘执行、培训实施、绩效考核、员工关系维护等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(hrSupervisor);

        // 专业序列职位
        var recruitmentSpecialist = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-REC-SPEC",
            PosName = "招聘专员",
            EnglishName = "Recruitment Specialist",
            CategoryId = 3, // 专业序列
            PosLevel = 2, // 中级
            PosSequence = 3, // 专业序列
            Status = 1,
            OrderNum = 4,
            Description = "负责公司招聘工作",
            Responsibilities = "职位发布、简历筛选、面试安排、候选人跟踪等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(recruitmentSpecialist);

        var trainingSpecialist = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-TRAIN-SPEC",
            PosName = "培训专员",
            EnglishName = "Training Specialist",
            CategoryId = 3, // 专业序列
            PosLevel = 2, // 中级
            PosSequence = 3, // 专业序列
            Status = 1,
            OrderNum = 5,
            Description = "负责公司培训工作",
            Responsibilities = "培训需求分析、培训计划制定、培训实施、培训效果评估等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(trainingSpecialist);

        var compensationSpecialist = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-COMP-SPEC",
            PosName = "薪酬专员",
            EnglishName = "Compensation Specialist",
            CategoryId = 3, // 专业序列
            PosLevel = 2, // 中级
            PosSequence = 3, // 专业序列
            Status = 1,
            OrderNum = 6,
            Description = "负责公司薪酬福利管理工作",
            Responsibilities = "薪酬体系设计、薪资核算、福利管理、薪酬分析等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(compensationSpecialist);

        var performanceSpecialist = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-PERF-SPEC",
            PosName = "绩效专员",
            EnglishName = "Performance Specialist",
            CategoryId = 3, // 专业序列
            PosLevel = 2, // 中级
            PosSequence = 3, // 专业序列
            Status = 1,
            OrderNum = 7,
            Description = "负责公司绩效管理工作",
            Responsibilities = "绩效体系设计、绩效考核实施、绩效面谈、绩效改进等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(performanceSpecialist);

        var erSpecialist = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-ER-SPEC",
            PosName = "员工关系专员",
            EnglishName = "Employee Relations Specialist",
            CategoryId = 3, // 专业序列
            PosLevel = 2, // 中级
            PosSequence = 3, // 专业序列
            Status = 1,
            OrderNum = 8,
            Description = "负责公司员工关系管理工作",
            Responsibilities = "员工沟通、冲突处理、离职面谈、员工活动组织等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(erSpecialist);

        // 支持序列职位
        var hrAssistant = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-ASS",
            PosName = "人力资源助理",
            EnglishName = "HR Assistant",
            CategoryId = 6, // 支持序列
            PosLevel = 1, // 初级
            PosSequence = 4, // 操作序列
            Status = 1,
            OrderNum = 9,
            Description = "协助人力资源各项工作",
            Responsibilities = "文件整理、数据录入、会议安排、日常事务处理等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(hrAssistant);

        var hrClerk = new HbtPosition
        {
            Id = nextId++,
            PosCode = "HR-CLERK",
            PosName = "人力资源文员",
            EnglishName = "HR Clerk",
            CategoryId = 6, // 支持序列
            PosLevel = 1, // 初级
            PosSequence = 4, // 操作序列
            Status = 1,
            OrderNum = 10,
            Description = "负责人力资源基础事务工作",
            Responsibilities = "档案管理、考勤统计、社保办理、基础数据维护等",
            CreateBy = "Hbt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Hbt365",
            UpdateTime = DateTime.Now
        };
        defaultPositions.Add(hrClerk);

        // 批量插入或更新数据
        foreach (var position in defaultPositions)
        {
            var existingPosition = await PositionRepository.GetFirstAsync(x => x.PosCode == position.PosCode);
            
            if (existingPosition == null)
            {
                await PositionRepository.CreateAsync(position);
                insertCount++;
                _logger.Info($"[创建] 职位 '{position.PosName}' 创建成功");
            }
            else
            {
                existingPosition.PosName = position.PosName;
                existingPosition.EnglishName = position.EnglishName;
                existingPosition.CategoryId = position.CategoryId;
                existingPosition.PosLevel = position.PosLevel;
                existingPosition.PosSequence = position.PosSequence;
                existingPosition.Status = position.Status;
                existingPosition.OrderNum = position.OrderNum;
                existingPosition.Description = position.Description;
                existingPosition.Responsibilities = position.Responsibilities;
                existingPosition.UpdateBy = "Hbt365";
                existingPosition.UpdateTime = DateTime.Now;
                
                await PositionRepository.UpdateAsync(existingPosition);
                updateCount++;
                _logger.Info($"[更新] 职位 '{position.PosName}' 更新成功");
            }
        }

        _logger.Info($"职位数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
} 