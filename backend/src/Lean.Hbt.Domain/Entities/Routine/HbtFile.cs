#nullable enable

using Lean.Hbt.Domain.Entities.Identity;
using SqlSugar;

//===================================================================
// 项目名 : Lean.Hbt.Domain.Entities.Routine
// 文件名 : HbtFile.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V1.0.0
// 描述   : 文件实体
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine
{
    /// <summary>
    /// 文件实体
    /// </summary>
    [SugarTable("hbt_routine_file", "文件")]
    public class HbtFile : HbtBaseEntity
    {
        /// <summary>
        /// 文件原名
        /// </summary>
        [SugarColumn(ColumnName = "file_original_name", ColumnDescription = "文件原名", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileOriginalName { get; set; } = string.Empty;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        [SugarColumn(ColumnName = "file_extension", ColumnDescription = "文件扩展名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileExtension { get; set; } = string.Empty;

        /// <summary>
        /// 文件名
        /// </summary>
        [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件路径
        /// </summary>
        [SugarColumn(ColumnName = "file_path", ColumnDescription = "文件路径", Length = 500, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型
        /// </summary>
        [SugarColumn(ColumnName = "file_type", ColumnDescription = "文件类型", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileType { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        [SugarColumn(ColumnName = "file_size", ColumnDescription = "文件大小（字节）", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long FileSize { get; set; }

        /// <summary>
        /// 存储类型（0本地 1云存储）
        /// </summary>
        [SugarColumn(ColumnName = "file_storage_type", ColumnDescription = "存储类型（0本地 1云存储）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FileStorageType { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        [SugarColumn(ColumnName = "file_storage_location", ColumnDescription = "存储位置", Length = 500, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileStorageLocation { get; set; } = string.Empty;

        /// <summary>
        /// 访问地址
        /// </summary>
        [SugarColumn(ColumnName = "file_access_url", ColumnDescription = "访问地址", Length = 500, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileAccessUrl { get; set; } = string.Empty;

        /// <summary>
        /// 文件MD5
        /// </summary>
        [SugarColumn(ColumnName = "file_md5", ColumnDescription = "文件MD5", Length = 32, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 状态（0临时 1正式）
        /// </summary>
        [SugarColumn(ColumnName = "file_status", ColumnDescription = "状态（0临时 1正式）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FileStatus { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        [SugarColumn(ColumnName = "file_download_count", ColumnDescription = "下载次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FileDownloadCount { get; set; }
    }
}