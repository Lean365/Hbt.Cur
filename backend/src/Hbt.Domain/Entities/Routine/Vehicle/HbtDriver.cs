//===================================================================
// 项目名 : Hbt.Cur.Domain.Entities.Routine
// 文件名 : HbtDriver.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 驾驶员管理实体
//===================================================================

using Hbt.Cur.Domain.Entities.Identity;
using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 驾驶员管理实体
    /// </summary>
    [SugarTable("hbt_routine_driver", "驾驶员管理")]
    [SugarIndex("index_driver_employee_id", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("index_driver_license_no", nameof(LicenseNo), OrderByType.Asc)]
    [SugarIndex("index_driver_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_driver_department", nameof(Department), OrderByType.Asc)]
    public class HbtDriver : HbtBaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        [SugarColumn(ColumnName = "employee_no", ColumnDescription = "员工工号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 部门
        /// </summary>
        [SugarColumn(ColumnName = "department", ColumnDescription = "部门", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [SugarColumn(ColumnName = "position", ColumnDescription = "职位", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态（0：在职，1：离职，2：休假，3：停职，4：其他）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "驾驶员状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        [SugarColumn(ColumnName = "license_no", ColumnDescription = "驾驶证号码", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string LicenseNo { get; set; } = string.Empty;

        /// <summary>
        /// 驾驶证类型（0：A1，1：A2，2：A3，3：B1，4：B2，5：C1，6：C2，7：D，8：E，9：F，10：M，11：N，12：P）
        /// </summary>
        [SugarColumn(ColumnName = "license_type", ColumnDescription = "驾驶证类型", IsNullable = false, DefaultValue = "5", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        [SugarColumn(ColumnName = "license_authority", ColumnDescription = "驾驶证发证机关", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        [SugarColumn(ColumnName = "license_issue_date", ColumnDescription = "驾驶证发证日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        [SugarColumn(ColumnName = "license_expiry_date", ColumnDescription = "驾驶证有效期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态（0：正常，1：即将到期，2：已过期，3：被吊销，4：被注销）
        /// </summary>
        [SugarColumn(ColumnName = "license_status", ColumnDescription = "驾驶证状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        [SugarColumn(ColumnName = "license_points", ColumnDescription = "驾驶证扣分", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicensePoints { get; set; }

        /// <summary>
        /// 驾驶证图片
        /// </summary>
        [SugarColumn(ColumnName = "license_images", ColumnDescription = "驾驶证图片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LicenseImages { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [SugarColumn(ColumnName = "id_card_no", ColumnDescription = "身份证号码", Length = 18, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [SugarColumn(ColumnName = "birth_date", ColumnDescription = "出生日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [SugarColumn(ColumnName = "age", ColumnDescription = "年龄", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "phone", ColumnDescription = "联系电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [SugarColumn(ColumnName = "emergency_contact", ColumnDescription = "紧急联系人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        [SugarColumn(ColumnName = "emergency_phone", ColumnDescription = "紧急联系电话", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        [SugarColumn(ColumnName = "home_address", ColumnDescription = "家庭地址", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [SugarColumn(ColumnName = "hire_date", ColumnDescription = "入职日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        [SugarColumn(ColumnName = "resign_date", ColumnDescription = "离职日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        [SugarColumn(ColumnName = "driving_years", ColumnDescription = "驾龄", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验（0：新手，1：一般，2：熟练，3：专家）
        /// </summary>
        [SugarColumn(ColumnName = "driving_experience", ColumnDescription = "驾驶经验", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        [SugarColumn(ColumnName = "drivable_vehicles", ColumnDescription = "可驾驶车型", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分（1-10分）
        /// </summary>
        [SugarColumn(ColumnName = "driving_skill_score", ColumnDescription = "驾驶技能评分", IsNullable = true, ColumnDataType = "decimal(3,1)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分（1-10分）
        /// </summary>
        [SugarColumn(ColumnName = "safety_score", ColumnDescription = "安全驾驶评分", IsNullable = true, ColumnDataType = "decimal(3,1)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分（1-10分）
        /// </summary>
        [SugarColumn(ColumnName = "service_score", ColumnDescription = "服务态度评分", IsNullable = true, ColumnDataType = "decimal(3,1)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分（1-10分）
        /// </summary>
        [SugarColumn(ColumnName = "overall_score", ColumnDescription = "综合评分", IsNullable = true, ColumnDataType = "decimal(3,1)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        [SugarColumn(ColumnName = "accident_count", ColumnDescription = "事故记录次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        [SugarColumn(ColumnName = "violation_count", ColumnDescription = "违章记录次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        [SugarColumn(ColumnName = "complaint_count", ColumnDescription = "投诉记录次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        [SugarColumn(ColumnName = "praise_count", ColumnDescription = "表扬记录次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_full_time", ColumnDescription = "是否专职司机", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "can_drive_hazardous", ColumnDescription = "是否可驾驶危险品车辆", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "can_drive_large", ColumnDescription = "是否可驾驶大型车辆", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "can_drive_passenger", ColumnDescription = "是否可驾驶客车", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        [SugarColumn(ColumnName = "work_area", ColumnDescription = "工作区域", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间（0：白班，1：夜班，2：全天，3：灵活）
        /// </summary>
        [SugarColumn(ColumnName = "work_schedule", ColumnDescription = "工作时间", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资（元/月）
        /// </summary>
        [SugarColumn(ColumnName = "base_salary", ColumnDescription = "基本工资", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资（元/月）
        /// </summary>
        [SugarColumn(ColumnName = "performance_salary", ColumnDescription = "绩效工资", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴（元/月）
        /// </summary>
        [SugarColumn(ColumnName = "allowance", ColumnDescription = "津贴", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资（元/月）
        /// </summary>
        [SugarColumn(ColumnName = "total_salary", ColumnDescription = "总工资", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        [SugarColumn(ColumnName = "bank_account", ColumnDescription = "银行账户", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [SugarColumn(ColumnName = "bank_name", ColumnDescription = "开户银行", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        [SugarColumn(ColumnName = "account_holder", ColumnDescription = "账户持有人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        [SugarColumn(ColumnName = "health_cert_no", ColumnDescription = "健康证号码", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        [SugarColumn(ColumnName = "health_cert_expiry", ColumnDescription = "健康证有效期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 健康证图片
        /// </summary>
        [SugarColumn(ColumnName = "health_cert_images", ColumnDescription = "健康证图片", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? HealthCertImages { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        [SugarColumn(ColumnName = "training_certificates", ColumnDescription = "培训证书", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        [SugarColumn(ColumnName = "skill_certificates", ColumnDescription = "技能证书", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SkillCertificates { get; set; }


    }
} 