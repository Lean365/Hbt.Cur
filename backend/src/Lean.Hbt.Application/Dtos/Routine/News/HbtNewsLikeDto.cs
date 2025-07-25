#nullable enable

//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtNewsLikeDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻点赞数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 新闻点赞基础DTO
    /// </summary>
    public class HbtNewsLikeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 点赞ID
        /// </summary>
        [AdaptMember("Id")]
        public long LikeId { get; set; }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型。1=点赞，2=取消点赞。
        /// </summary>
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

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
        /// 是否删除
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

        /// <summary>
        /// 关联的新闻
        /// </summary>
        public HbtNewsDto? News { get; set; }
    }

    /// <summary>
    /// 新闻点赞查询DTO
    /// </summary>
    public class HbtNewsLikeQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeQueryDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long? NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        public int? LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? OrderNum { get; set; }

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
    /// 新闻点赞创建DTO
    /// </summary>
    public class HbtNewsLikeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeCreateDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型。1=点赞，2=取消点赞。
        /// </summary>
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻点赞更新DTO
    /// </summary>
    public class HbtNewsLikeUpdateDto : HbtNewsLikeCreateDto
    {
        /// <summary>
        /// 点赞ID
        /// </summary>
        [AdaptMember("Id")]
        public long LikeId { get; set; }
    }

    /// <summary>
    /// 新闻点赞导入DTO
    /// </summary>
    public class HbtNewsLikeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeImportDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 点赞ID
        /// </summary>
        public new long Id { get; set; }
    }

    /// <summary>
    /// 新闻点赞导出DTO
    /// </summary>
    public class HbtNewsLikeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeExportDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻点赞模板DTO
    /// </summary>
    public class HbtNewsLikeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtNewsLikeTemplateDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻点赞状态DTO
    /// </summary>
    public class HbtNewsLikeStatusDto
    {
        /// <summary>
        /// 点赞ID
        /// </summary>
        [AdaptMember("Id")]
        public long LikeId { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        public int LikeType { get; set; }
    }
}