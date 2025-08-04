//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFileHelper.cs
// 创建者 : Lean365
// 创建时间: 2024-04-27
// 版本号 : V0.0.1
// 描述   : 文件操作帮助类
//===================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using NLog;

namespace Hbt.Cur.Common.Helpers
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public static class HbtFileHelper
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Logger.Info("文件删除成功: {FilePath}", filePath);
                    return true;
                }
                Logger.Warn("文件不存在: {FilePath}", filePath);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "文件删除失败: {FilePath}", filePath);
                return false;
            }
        }

        /// <summary>
        /// 异步删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> DeleteFileAsync(string filePath)
        {
            return await Task.Run(() => DeleteFile(filePath));
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除子目录和文件</param>
        /// <returns>是否成功</returns>
        public static bool DeleteDirectory(string directoryPath, bool recursive = true)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return false;

            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, recursive);
                    Logger.Info("目录删除成功: {DirectoryPath}", directoryPath);
                    return true;
                }
                Logger.Warn("目录不存在: {DirectoryPath}", directoryPath);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "目录删除失败: {DirectoryPath}", directoryPath);
                return false;
            }
        }

        /// <summary>
        /// 异步删除目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除子目录和文件</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> DeleteDirectoryAsync(string directoryPath, bool recursive = true)
        {
            return await Task.Run(() => DeleteDirectory(directoryPath, recursive));
        }

        /// <summary>
        /// 批量删除文件和目录
        /// </summary>
        /// <param name="paths">路径集合</param>
        /// <returns>成功删除的数量</returns>
        public static int DeletePaths(string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return 0;

            var successCount = 0;
            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path))
                    continue;

                if (File.Exists(path))
                {
                    if (DeleteFile(path))
                        successCount++;
                }
                else if (Directory.Exists(path))
                {
                    if (DeleteDirectory(path, true))
                        successCount++;
                }
                else
                {
                    Logger.Warn("路径不存在: {Path}", path);
                }
            }
            return successCount;
        }

        /// <summary>
        /// 异步批量删除文件和目录
        /// </summary>
        /// <param name="paths">路径集合</param>
        /// <returns>成功删除的数量</returns>
        public static async Task<int> DeletePathsAsync(string[] paths)
        {
            return await Task.Run(() => DeletePaths(paths));
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetFileSize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return 0;

            return new FileInfo(filePath).Length;
        }

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="size">文件大小（字节）</param>
        /// <returns>格式化后的文件大小</returns>
        public static string FormatFileSize(long size)
        {
            if (size < 1024)
                return $"{size} B";
            if (size < 1024 * 1024)
                return $"{(size / 1024.0):F2} KB";
            if (size < 1024 * 1024 * 1024)
                return $"{(size / (1024.0 * 1024)):F2} MB";
            return $"{(size / (1024.0 * 1024 * 1024)):F2} GB";
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return Path.GetExtension(fileName).ToLower();
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>文件类型</returns>
        public static string GetFileType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" => "image",
                ".pdf" => "pdf",
                ".doc" or ".docx" => "word",
                ".xls" or ".xlsx" => "excel",
                ".ppt" or ".pptx" => "powerpoint",
                ".txt" => "text",
                ".zip" or ".rar" or ".7z" => "archive",
                _ => "other"
            };
        }

        /// <summary>
        /// 获取文件的Content-Type
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>Content-Type</returns>
        public static string GetContentType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }
    }
} 