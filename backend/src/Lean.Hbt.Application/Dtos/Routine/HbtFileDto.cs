//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtFileDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 文件数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Lean.Hbt.Application.Dtos.Routine
{
    /// <summary>
    /// 文件基础DTO
    /// </summary>
    public class HbtFileDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long FileId { get; set; }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        public int FileStatus { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }

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
    /// 文件查询DTO
    /// </summary>
    public class HbtFileQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileQueryDto()
        {
            FileOriginalName = string.Empty;
            FileType = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        [MaxLength(255, ErrorMessage = "文件原名长度不能超过255个字符")]
        public string? FileOriginalName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [MaxLength(50, ErrorMessage = "文件类型长度不能超过50个字符")]
        public string? FileType { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int? FileStorageType { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        public int? FileStatus { get; set; }

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
    /// 文件创建DTO
    /// </summary>
    public class HbtFileCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileCreateDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        [Required(ErrorMessage = "文件原名不能为空")]
        [MaxLength(255, ErrorMessage = "文件原名长度不能超过255个字符")]
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        [Required(ErrorMessage = "文件扩展名不能为空")]
        [MaxLength(50, ErrorMessage = "文件扩展名长度不能超过50个字符")]
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required(ErrorMessage = "文件名不能为空")]
        [MaxLength(255, ErrorMessage = "文件名长度不能超过255个字符")]
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Required(ErrorMessage = "文件路径不能为空")]
        [MaxLength(500, ErrorMessage = "文件路径长度不能超过500个字符")]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        [MaxLength(50, ErrorMessage = "文件类型长度不能超过50个字符")]
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        [Required(ErrorMessage = "文件大小不能为空")]
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        [Required(ErrorMessage = "存储位置不能为空")]
        [MaxLength(500, ErrorMessage = "存储位置长度不能超过500个字符")]
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        [Required(ErrorMessage = "访问地址不能为空")]
        [MaxLength(500, ErrorMessage = "访问地址长度不能超过500个字符")]
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        [MaxLength(32, ErrorMessage = "文件MD5长度不能超过32个字符")]
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        public int FileStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 文件更新DTO
    /// </summary>
    public class HbtFileUpdateDto : HbtFileCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "文件ID不能为空")]
        public long FileId { get; set; }
    }

    /// <summary>
    /// 文件导入DTO
    /// </summary>
    public class HbtFileImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileImportDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        public int FileStatus { get; set; }
    }

    /// <summary>
    /// 文件导出DTO
    /// </summary>
    public class HbtFileExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileExportDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int FileStatus { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 文件导入模板DTO
    /// </summary>
    public class HbtFileTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileTemplateDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型(0=本地,1=云存储)
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态(0=临时,1=正式)
        /// </summary>
        public int FileStatus { get; set; }
    }

    /// <summary>
    /// 文件上传DTO
    /// </summary>
    public class HbtFileUploadDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtFileUploadDto()
        {
            FileOriginalName = string.Empty;
            FileExtension = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
            FileType = string.Empty;
            FileStorageLocation = string.Empty;
            FileAccessUrl = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string FileOriginalName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        public string FileStorageLocation { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string FileAccessUrl { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        public int FileStatus { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream? File { get; set; }
    }
}