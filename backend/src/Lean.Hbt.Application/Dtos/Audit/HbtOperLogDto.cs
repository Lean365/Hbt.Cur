//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOperLogDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Hbt.Application.Dtos.Audit
{
    /// <summary>
    /// 操作日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogDto()
        {
            OperModule = string.Empty;
            OperType = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperLocation = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long OperLogId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 操作模块
        /// </summary>
        [AdaptMember("OperModule")]
        public string OperModule { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除）
        /// </summary>
        [AdaptMember("OperType")]
        public string OperType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [AdaptMember("OperTableName")]
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        [AdaptMember("OperBusinessKey")]
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [AdaptMember("OperRequestMethod")]
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [AdaptMember("OperRequestParam")]
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        [AdaptMember("OperResponseParam")]
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        [AdaptMember("OperDuration")]
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        [AdaptMember("OperErrorMsg")]
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [AdaptMember("OperIpAddress")]
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        [AdaptMember("OperLocation")]
        public string? OperLocation { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        [AdaptMember("OperStatus")]
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

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

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 操作日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogQueryDto()
        {
            UserName = string.Empty;
            OperModule = string.Empty;
            OperType = string.Empty;
            OperTableName = string.Empty;
            OperIpAddress = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        [MaxLength(50, ErrorMessage = "操作模块长度不能超过50个字符")]
        public string OperModule { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [MaxLength(20, ErrorMessage = "操作类型长度不能超过20个字符")]
        public string OperType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string OperTableName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public int? OperStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 操作日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogExportDto()
        {
            UserName = string.Empty;
            OperModule = string.Empty;
            OperType = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperLocation = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        public string? OperLocation { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 操作日志创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogCreateDto()
        {
            OperModule = string.Empty;
            OperType = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperLocation = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        [Required(ErrorMessage = "操作模块不能为空")]
        [MaxLength(50, ErrorMessage = "操作模块长度不能超过50个字符")]
        public string OperModule { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除）
        /// </summary>
        [Required(ErrorMessage = "操作类型不能为空")]
        [MaxLength(20, ErrorMessage = "操作类型长度不能超过20个字符")]
        public string OperType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Required(ErrorMessage = "表名不能为空")]
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        [Required(ErrorMessage = "业务主键不能为空")]
        [MaxLength(50, ErrorMessage = "业务主键长度不能超过50个字符")]
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [Required(ErrorMessage = "请求方法不能为空")]
        [MaxLength(100, ErrorMessage = "请求方法长度不能超过100个字符")]
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required(ErrorMessage = "IP地址不能为空")]
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        [MaxLength(255, ErrorMessage = "操作地点长度不能超过255个字符")]
        public string? OperLocation { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 操作日志更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtOperLogUpdateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtOperLogUpdateDto()
        {
            OperModule = string.Empty;
            OperType = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperLocation = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID不能为空")]
        public long OperLogId { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        [MaxLength(50, ErrorMessage = "操作模块长度不能超过50个字符")]
        public string OperModule { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除）
        /// </summary>
        [MaxLength(20, ErrorMessage = "操作类型长度不能超过20个字符")]
        public string OperType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(100, ErrorMessage = "表名长度不能超过100个字符")]
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        [MaxLength(50, ErrorMessage = "业务主键长度不能超过50个字符")]
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [MaxLength(100, ErrorMessage = "请求方法长度不能超过100个字符")]
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作地点
        /// </summary>
        [MaxLength(255, ErrorMessage = "操作地点长度不能超过255个字符")]
        public string? OperLocation { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}