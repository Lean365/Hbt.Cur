//===================================================================
// 项目名 : Hbt.Application
// 文件名 : HbtFileDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述   : 文件数据传输对象
//===================================================================

namespace Hbt.Application.Dtos.Routine.Document
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
        }

        /// <summary>
        /// 文件ID
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
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }



        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

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
            FileName = string.Empty;
            FileType = string.Empty;
        }

        /// <summary>
        /// 文件原名
        /// </summary>
        public string? FileOriginalName { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string? FileType { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        public int FileStorageType { get; set; }

        /// <summary>
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }
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
            Remark = string.Empty;
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
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 文件更新DTO
    /// </summary>
    public class HbtFileUpdateDto : HbtFileCreateDto
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long FileId { get; set; }
    }

    /// <summary>
    /// 文件导入DTO
    /// </summary>
    public class HbtFileImportDto : HbtBaseEntity
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
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }



        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 文件ID
        /// </summary>
        public new long Id { get; set; }
    }

    /// <summary>
    /// 文件导出DTO
    /// </summary>
    public class HbtFileExportDto : HbtBaseEntity
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
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 文件模板DTO
    /// </summary>
    public class HbtFileTemplateDto : HbtBaseEntity
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
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int FileDownloadCount { get; set; }



        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 文件状态DTO
    /// </summary>
    public class HbtStatusDto
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long FileId { get; set; }

        /// <summary>
        /// 状态。文件的状态（0=临时，1=正式）。
        /// </summary>
        public int Status { get; set; }
    }


}