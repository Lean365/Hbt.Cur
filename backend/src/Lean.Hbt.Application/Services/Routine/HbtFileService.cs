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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 文件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtFileService : HbtBaseService, IHbtFileService
    {
        private readonly IHbtRepository<HbtFile> _fileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="fileRepository">文件仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="webHostEnvironment">Web主机环境</param>
        public HbtFileService(
            IHbtLogger logger,
            IHbtRepository<HbtFile> fileRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization,
            IWebHostEnvironment webHostEnvironment) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _fileRepository = fileRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtFileDto>> GetListAsync(HbtFileQueryDto query)
        {
            _logger.Info("开始查询文件列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await _fileRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new HbtPagedResult<HbtFileDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtFileDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>返回文件详情</returns>
        public async Task<HbtFileDto> GetByIdAsync(long fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file == null)
                throw new HbtException(L("File.NotFound", fileId));

            return file.Adapt<HbtFileDto>();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回文件ID</returns>
        public async Task<long> CreateAsync(HbtFileCreateDto input)
        {
            var file = input.Adapt<HbtFile>();
            var result = await _fileRepository.CreateAsync(file);
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
                throw new HbtException(L("File.NotFound", input.FileId));

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
                throw new HbtException(L("File.NotFound", fileId));

            // 删除物理文件
            if (!string.IsNullOrEmpty(file.FileName))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, file.FilePath);
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("删除物理文件失败 - 文件路径: {FilePath}", filePath, ex);
                        throw new HbtException(L("File.DeletePhysicalFileFailed", filePath), HbtConstants.ErrorCodes.ServerError, ex);
                    }
                }
            }

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
                throw new HbtException(L("File.SelectToDelete"));

            var files = await _fileRepository.GetListAsync(x => fileIds.Contains(x.Id));
            if (files == null || !files.Any())
                throw new HbtException(L("File.NotFound"));

            // 删除物理文件
            foreach (var file in files)
            {
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, file.FilePath);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("删除物理文件失败 - 文件路径: {FilePath}", filePath, ex);
                            throw new HbtException(L("File.DeletePhysicalFileFailed", filePath), HbtConstants.ErrorCodes.ServerError, ex);
                        }
                    }
                }
            }

            var result = await _fileRepository.DeleteRangeAsync(files);
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
                    await _fileRepository.CreateAsync(file);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("File.ImportFailed", dto.FileOriginalName), ex);
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
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtFileQueryDto query, string sheetName = "文件信息")
        {
            var predicate = QueryExpression(query);

            var files = await _fileRepository.AsQueryable()
                .Where(predicate)
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
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "文件信息")
        {
            var template = new List<HbtFileTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="input">上传参数</param>
        /// <returns>返回上传结果</returns>
        public async Task<HbtFileDto> UploadAsync(Stream file, HbtFileUploadDto input)
        {
            if (file == null)
                throw new HbtException(L("File.FileStreamRequired"));

            if (input == null)
                throw new HbtException(L("File.UploadParamRequired"));

            if (string.IsNullOrEmpty(input.FileOriginalName))
                throw new HbtException(L("File.FileNameRequired"));

            if (string.IsNullOrEmpty(input.FileExtension))
                throw new HbtException(L("File.FileExtensionRequired"));

            if (string.IsNullOrEmpty(input.FileType))
                throw new HbtException(L("File.FileTypeRequired"));

            // 检查文件名是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_fileRepository, "FileOriginalName", input.FileOriginalName);

            // 1. 生成文件信息
            var fileEntity = new HbtFile
            {
                FileOriginalName = input.FileOriginalName,
                FileExtension = input.FileExtension,
                FileName = input.FileName,
                FilePath = input.FilePath,
                FileType = input.FileType,
                FileSize = input.FileSize,
                FileStorageType = input.FileStorageType,
                FileStorageLocation = input.FileStorageLocation,
                FileAccessUrl = input.FileAccessUrl,
                FileMd5 = input.FileMd5,
                FileStatus = input.FileStatus,
                FileDownloadCount = 0
            };

            // 2. 保存文件信息到数据库
            var fileId = await _fileRepository.CreateAsync(fileEntity);
            fileEntity.Id = fileId;

            // 3. 保存文件
            var savePath = Path.Combine(_webHostEnvironment.WebRootPath, input.FilePath);
            Directory.CreateDirectory(savePath);
            fileEntity.FilePath = Path.Combine(input.FilePath, fileEntity.FileName);
            fileEntity.FileStorageLocation = savePath;
            fileEntity.FileAccessUrl = $"/api/file/download/{fileEntity.Id}";
            using (var fileStream = new FileStream(Path.Combine(savePath, fileEntity.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // 4. 更新文件信息
            await _fileRepository.UpdateAsync(fileEntity);

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
                throw new HbtException(L("File.NotFound", fileId));

            if (!System.IO.File.Exists(file.FilePath))
                throw new HbtException(L("File.NotFound", file.FilePath));

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

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtFile, bool>> QueryExpression(HbtFileQueryDto query)
        {
            var exp = Expressionable.Create<HbtFile>();

            if (!string.IsNullOrEmpty(query?.FileOriginalName))
                exp.And(x => x.FileOriginalName.Contains(query.FileOriginalName));

            if (!string.IsNullOrEmpty(query?.FileType))
                exp.And(x => x.FileType.Contains(query.FileType));

            if (query?.FileStorageType.HasValue == true && query.FileStorageType.Value != -1)
                exp.And(x => x.FileStorageType == query.FileStorageType.Value);

            if (query?.FileStatus.HasValue == true && query.FileStatus.Value != -1)
                exp.And(x => x.FileStatus == query.FileStatus.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }
    }
} 