//===================================================================
// 项目名: Hbt.Cur.Application
// 文件名: HbtLeaveDto.cs
// 创建者: Lean365
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 请假数据传输对象
//===================================================================

using System;

namespace Hbt.Cur.Application.Dtos.Human.Leave
{
    /// <summary>
    /// 请假基础DTO
    /// </summary>
    public class HbtLeaveDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveDto()
        {
            LeaveType = string.Empty;
            Reason = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 请假ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态(1=待审批 2=已通过 3=已拒绝)
        /// </summary>
        public int ApprovalStatus { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

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
    /// 请假查询DTO
    /// </summary>
    public class HbtLeaveQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveQueryDto()
        {
            LeaveType = string.Empty;
            Reason = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string? LeaveType { get; set; }

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int? ApprovalStatus { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string? Reason { get; set; }
    }

    /// <summary>
    /// 请假创建DTO
    /// </summary>
    public class HbtLeaveCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveCreateDto()
        {
            LeaveType = string.Empty;
            Reason = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态(1=待审批 2=已通过 3=已拒绝)
        /// </summary>
        public int ApprovalStatus { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 请假更新DTO
    /// </summary>
    public class HbtLeaveUpdateDto : HbtLeaveCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveUpdateDto() : base()
        {
        }

        /// <summary>
        /// 请假ID
        /// </summary>
        [AdaptMember("Id")]
        public long LeaveId { get; set; }
    }

    /// <summary>
    /// 请假删除DTO
    /// </summary>
    public class HbtLeaveDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 请假ID
        /// </summary>
        [AdaptMember("Id")]
        public long LeaveId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 请假导入DTO
    /// </summary>
    public class HbtLeaveImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveImportDto()
        {
            EmployeeNo = string.Empty;
            LeaveType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            LeaveDays = string.Empty;
            Reason = string.Empty;
            ApprovalStatus = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假天数
        /// </summary>
        public string LeaveDays { get; set; } = string.Empty;

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 请假导出DTO
    /// </summary>
    public class HbtLeaveExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            LeaveType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            LeaveDays = string.Empty;
            Reason = string.Empty;
            ApprovalStatus = string.Empty;
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
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假天数
        /// </summary>
        public string LeaveDays { get; set; } = string.Empty;

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 请假模板DTO
    /// </summary>
    public class HbtLeaveTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLeaveTemplateDto()
        {
            EmployeeNo = string.Empty;
            LeaveType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            LeaveDays = string.Empty;
            Reason = string.Empty;
            ApprovalStatus = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假天数
        /// </summary>
        public string LeaveDays { get; set; } = string.Empty;

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 