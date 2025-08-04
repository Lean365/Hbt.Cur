//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: HbtEmployeeContractDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 员工合同数据传输对象
//===================================================================

using System;

namespace Hbt.Cur.Application.Dtos.Human.Employee
{
    /// <summary>
    /// 员工合同基础DTO
    /// </summary>
    public class HbtEmployeeContractDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 员工合同查询DTO
    /// </summary>
    public class HbtEmployeeContractQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractQueryDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string? ContractNo { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string? ContractType { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 员工合同创建DTO
    /// </summary>
    public class HbtEmployeeContractCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractCreateDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同更新DTO
    /// </summary>
    public class HbtEmployeeContractUpdateDto : HbtEmployeeContractCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractUpdateDto() : base()
        {
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }
    }

    /// <summary>
    /// 员工合同删除DTO
    /// </summary>
    public class HbtEmployeeContractDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同导入DTO
    /// </summary>
    public class HbtEmployeeContractImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractImportDto()
        {
            EmployeeNo = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同导出DTO
    /// </summary>
    public class HbtEmployeeContractExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            ProbationStartDate = string.Empty;
            ProbationEndDate = string.Empty;
            Status = string.Empty;
            CreateTime = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public string? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public string? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 员工合同模板DTO
    /// </summary>
    public class HbtEmployeeContractTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtEmployeeContractTemplateDto()
        {
            EmployeeNo = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            ProbationStartDate = string.Empty;
            ProbationEndDate = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public string? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public string? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 