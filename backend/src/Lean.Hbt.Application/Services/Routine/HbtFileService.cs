//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFileService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 文件服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine;
using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 文件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtFileService : IHbtFileService
    {
        private readonly ILogger<HbtFileService> _logger;
        private readonly IHbtRepository<HbtFile> _fileRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="fileRepository">文件仓储</param>
        public HbtFileService(
            ILogger<HbtFileService> logger,
            IHbtRepository<HbtFile> fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtFileDto>> GetPagedListAsync(HbtFileQueryDto query)
        {
            var exp = Expressionable.Create<HbtFile>();

            if (!string.IsNullOrEmpty(query?.FileOriginalName))
                exp.And(x => x.FileOriginalName.Contains(query.FileOriginalName));

            if (!string.IsNullOrEmpty(query?.FileType))
                exp.And(x => x.FileType.Contains(query.FileType));

            if (query?.FileStorageType.HasValue == true)
                exp.And(x => x.FileStorageType == query.FileStorageType.Value);

            if (query?.FileStatus.HasValue == true)
                exp.And(x => x.FileStatus == query.FileStatus.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await _fileRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtFileDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtFileDto>>()
            };
        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>返回文件详情</returns>
        public async Task<HbtFileDto> GetAsync(long fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null)
                throw new HbtException($"文件不存在: {fileId}");

            return file.Adapt<HbtFileDto>();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回文件ID</returns>
        public async Task<long> InsertAsync(HbtFileCreateDto input)
        {
            var file = input.Adapt<HbtFile>();
            var result = await _fileRepository.InsertAsync(file);
            return result;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtFileUpdateDto input)
        {
            var file = await _fileRepository.GetByIdAsync(input.FileId);
            if (file == null)
                throw new HbtException($"文件不存在: {input.FileId}");

            input.Adapt(file);
            var result = await _fileRepository.UpdateAsync(file);
            return result > 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null)
                throw new HbtException($"文件不存在: {fileId}");

            var result = await _fileRepository.DeleteAsync(file);
            return result > 0;
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] fileIds)
        {
            if (fileIds == null || fileIds.Length == 0)
                throw new HbtException("请选择要删除的文件");

            Expression<Func<HbtFile, bool>> predicate = x => fileIds.Contains(x.Id);
            var result = await _fileRepository.DeleteAsync(predicate);
            return result > 0;
        }

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "文件信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtFileImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var file = dto.Adapt<HbtFile>();
                    await _fileRepository.InsertAsync(file);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"导入文件数据失败: {dto.FileOriginalName}");
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtFileQueryDto query, string sheetName = "文件信息")
        {
            var predicate = Expressionable.Create<HbtFile>();

            if (!string.IsNullOrEmpty(query?.FileOriginalName))
                predicate.And(x => x.FileOriginalName.Contains(query.FileOriginalName));

            if (!string.IsNullOrEmpty(query?.FileType))
                predicate.And(x => x.FileType.Contains(query.FileType));

            if (query?.FileStorageType.HasValue == true)
                predicate.And(x => x.FileStorageType == query.FileStorageType.Value);

            if (query?.FileStatus.HasValue == true)
                predicate.And(x => x.FileStatus == query.FileStatus.Value);

            if (query?.StartTime.HasValue == true)
                predicate.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                predicate.And(x => x.CreateTime <= query.EndTime.Value);

            var files = await _fileRepository.AsQueryable()
                .Where(predicate.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            var exportDtos = files.Adapt<List<HbtFileExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回Excel文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "文件信息")
        {
            var template = new List<HbtFileTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="fileName">文件名</param>
        /// <returns>返回上传结果</returns>
        public async Task<HbtFileDto> UploadAsync(Stream file, string fileName)
        {
            // 1. 生成文件信息
            var fileInfo = new FileInfo(fileName);
            var fileEntity = new HbtFile
            {
                FileOriginalName = fileName,
                FileExtension = fileInfo.Extension,
                FileName = $"{Guid.NewGuid():N}{fileInfo.Extension}",
                FileType = fileInfo.Extension.TrimStart('.').ToLower(),
                FileSize = file.Length,
                FileStorageType = 0, // 本地存储
                FileStatus = 1, // 正式文件
                FileDownloadCount = 0
            };

            // 2. 保存文件
            var savePath = Path.Combine("uploads", DateTime.Now.ToString("yyyy/MM/dd"));
            Directory.CreateDirectory(savePath);
            fileEntity.FilePath = Path.Combine(savePath, fileEntity.FileName);
            fileEntity.FileStorageLocation = savePath;
            fileEntity.FileAccessUrl = $"/api/file/download/{fileEntity.Id}";

            using (var fileStream = new FileStream(fileEntity.FilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // 3. 保存文件信息到数据库
            await _fileRepository.InsertAsync(fileEntity);

            return fileEntity.Adapt<HbtFileDto>();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>返回文件流</returns>
        public async Task<(Stream FileStream, string FileName, string ContentType)> DownloadAsync(long fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null)
                throw new HbtException($"文件不存在: {fileId}");

            if (!System.IO.File.Exists(file.FilePath))
                throw new HbtException($"文件不存在: {file.FilePath}");

            // 更新下载次数
            file.FileDownloadCount++;
            await _fileRepository.UpdateAsync(file);

            var fileStream = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read);
            var contentType = GetContentType(file.FileExtension);

            return (fileStream, file.FileOriginalName, contentType);
        }

        /// <summary>
        /// 获取文件的Content-Type
        /// </summary>
        private string GetContentType(string fileExtension)
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