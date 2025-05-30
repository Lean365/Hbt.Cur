//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedHrDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 人力资源相关字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 人力资源相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedHrDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedHrDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化人力资源相关字典数据
    /// </summary>
    public async Task<(int, int)> InitializeHrDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 员工类型
            new HbtDictData
            {
                DictType = "sys_employee_type",
                DictLabel = "正式员工",
                DictValue = "REGULAR",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "正式员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_type",
                DictLabel = "试用期员工",
                DictValue = "PROBATION",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "试用期员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_type",
                DictLabel = "临时工",
                DictValue = "TEMPORARY",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "临时工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_type",
                DictLabel = "实习生",
                DictValue = "INTERN",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "实习生",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 员工状态
            new HbtDictData
            {
                DictType = "sys_employee_status",
                DictLabel = "在职",
                DictValue = "ACTIVE",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "在职员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_status",
                DictLabel = "离职",
                DictValue = "RESIGNED",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "离职员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_status",
                DictLabel = "休假",
                DictValue = "ON_LEAVE",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "休假员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_status",
                DictLabel = "停职",
                DictValue = "SUSPENDED",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "停职员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 员工等级
            new HbtDictData
            {
                DictType = "sys_employee_level",
                DictLabel = "高级",
                DictValue = "SENIOR",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "高级员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_level",
                DictLabel = "中级",
                DictValue = "MIDDLE",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "中级员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_employee_level",
                DictLabel = "初级",
                DictValue = "JUNIOR",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "初级员工",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 职位类型
            new HbtDictData
            {
                DictType = "sys_position_type",
                DictLabel = "管理类",
                DictValue = "MANAGEMENT",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "管理类职位",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_type",
                DictLabel = "技术类",
                DictValue = "TECHNICAL",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "技术类职位",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_type",
                DictLabel = "业务类",
                DictValue = "BUSINESS",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "业务类职位",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_type",
                DictLabel = "行政类",
                DictValue = "Hbt365",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "行政类职位",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 职位等级
            new HbtDictData
            {
                DictType = "sys_position_level",
                DictLabel = "总经理",
                DictValue = "GM",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "总经理级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_level",
                DictLabel = "总监",
                DictValue = "DIRECTOR",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "总监级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_level",
                DictLabel = "经理",
                DictValue = "MANAGER",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "经理级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_level",
                DictLabel = "主管",
                DictValue = "SUPERVISOR",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "主管级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_position_level",
                DictLabel = "专员",
                DictValue = "SPECIALIST",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "专员级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 部门类型
            new HbtDictData
            {
                DictType = "sys_department_type",
                DictLabel = "总部",
                DictValue = "HEADQUARTERS",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "总部部门",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_department_type",
                DictLabel = "分公司",
                DictValue = "BRANCH",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "分公司部门",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_department_type",
                DictLabel = "办事处",
                DictValue = "OFFICE",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "办事处部门",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 考勤类型
            new HbtDictData
            {
                DictType = "sys_attendance_type",
                DictLabel = "正常出勤",
                DictValue = "NORMAL",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "正常出勤",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_attendance_type",
                DictLabel = "迟到",
                DictValue = "LATE",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "迟到",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_attendance_type",
                DictLabel = "早退",
                DictValue = "EARLY_LEAVE",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "早退",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_attendance_type",
                DictLabel = "缺勤",
                DictValue = "ABSENT",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "缺勤",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 请假类型
            new HbtDictData
            {
                DictType = "sys_leave_type",
                DictLabel = "年假",
                DictValue = "ANNUAL",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "年假",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_leave_type",
                DictLabel = "事假",
                DictValue = "PERSONAL",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "事假",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_leave_type",
                DictLabel = "病假",
                DictValue = "SICK",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "病假",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_leave_type",
                DictLabel = "产假",
                DictValue = "MATERNITY",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "产假",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_leave_type",
                DictLabel = "婚假",
                DictValue = "MARRIAGE",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "婚假",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 加班类型
            new HbtDictData
            {
                DictType = "sys_overtime_type",
                DictLabel = "工作日加班",
                DictValue = "WORKDAY",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "工作日加班",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_overtime_type",
                DictLabel = "周末加班",
                DictValue = "WEEKEND",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "周末加班",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_overtime_type",
                DictLabel = "节假日加班",
                DictValue = "HOLIDAY",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "节假日加班",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 薪资类型
            new HbtDictData
            {
                DictType = "sys_salary_type",
                DictLabel = "基本工资",
                DictValue = "BASE",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "基本工资",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_salary_type",
                DictLabel = "绩效工资",
                DictValue = "PERFORMANCE",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "绩效工资",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_salary_type",
                DictLabel = "加班工资",
                DictValue = "OVERTIME",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "加班工资",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_salary_type",
                DictLabel = "奖金",
                DictValue = "BONUS",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "奖金",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 社保类型
            new HbtDictData
            {
                DictType = "sys_insurance_type",
                DictLabel = "养老保险",
                DictValue = "PENSION",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "养老保险",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_insurance_type",
                DictLabel = "医疗保险",
                DictValue = "MEDICAL",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "医疗保险",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_insurance_type",
                DictLabel = "失业保险",
                DictValue = "UNEMPLOYMENT",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "失业保险",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_insurance_type",
                DictLabel = "工伤保险",
                DictValue = "INJURY",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "工伤保险",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_insurance_type",
                DictLabel = "生育保险",
                DictValue = "MATERNITY",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "生育保险",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 培训类型
            new HbtDictData
            {
                DictType = "sys_training_type",
                DictLabel = "入职培训",
                DictValue = "ONBOARDING",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "入职培训",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_training_type",
                DictLabel = "技能培训",
                DictValue = "SKILL",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "技能培训",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_training_type",
                DictLabel = "管理培训",
                DictValue = "MANAGEMENT",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "管理培训",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_training_type",
                DictLabel = "安全培训",
                DictValue = "SAFETY",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "安全培训",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 考核类型
            new HbtDictData
            {
                DictType = "sys_assessment_type",
                DictLabel = "月度考核",
                DictValue = "MONTHLY",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "月度考核",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_assessment_type",
                DictLabel = "季度考核",
                DictValue = "QUARTERLY",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "季度考核",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_assessment_type",
                DictLabel = "年度考核",
                DictValue = "ANNUAL",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "年度考核",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 奖惩类型
            new HbtDictData
            {
                DictType = "sys_reward_type",
                DictLabel = "奖励",
                DictValue = "REWARD",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "奖励",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_reward_type",
                DictLabel = "处罚",
                DictValue = "PUNISHMENT",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "处罚",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 合同类型
            new HbtDictData
            {
                DictType = "sys_contract_type",
                DictLabel = "固定期限",
                DictValue = "FIXED",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "固定期限合同",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_contract_type",
                DictLabel = "无固定期限",
                DictValue = "UNLIMITED",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "无固定期限合同",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_contract_type",
                DictLabel = "实习协议",
                DictValue = "INTERNSHIP",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "实习协议",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 人力资源字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "Hbt365";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 人力资源字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}