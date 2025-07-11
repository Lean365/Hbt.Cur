#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFileService.cs
// 创建者 : Lean365
// 创建时间: 2024-06-09
// 版本号 : V1.0.0
// 描述    : 文件服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Routine.Document;
using Lean.Hbt.Application.Dtos.Routine.Document;

namespace Lean.Hbt.Application.Services.Routine.Document
{
    /// <summary>
    /// 文件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-06-09
    /// </remarks>
    public class HbtFileService : HbtBaseService, IHbtFileService
    {
        private readonly IHbtRepository<HbtFile> _fileRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileRepository">文件仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtFileService(
            IHbtRepository<HbtFile> fileRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtFile, bool>> FileQueryExpression(HbtFileQueryDto query)
        {
            return Expressionable.Create<HbtFile>()
                .AndIF(!string.IsNullOrEmpty(query.FileOriginalName), x => x.FileOriginalName!.Contains(query.FileOriginalName!))
                .AndIF(!string.IsNullOrEmpty(query.FileName), x => x.FileName!.Contains(query.FileName!))
                .AndIF(!string.IsNullOrEmpty(query.FileType), x => x.FileType!.Contains(query.FileType!))
                .AndIF(query.FileStorageType != -1, x => x.FileStorageType == query.FileStorageType)
                .AndIF(query.FileStatus != -1, x => x.FileStatus == query.FileStatus)
                .ToExpression();
        }

        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件分页列表</returns>
        public async Task<HbtPagedResult<HbtFileDto>> GetListAsync(HbtFileQueryDto? query)
        {
            query ??= new HbtFileQueryDto();

            _logger.Info($"查询文件列表，参数：FileOriginalName={query.FileOriginalName}, FileName={query.FileName}, FileType={query.FileType}, FileStorageType={query.FileStorageType}, FileStatus={query.FileStatus}");

            var result = await _fileRepository.GetPagedListAsync(
                FileQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询文件列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtFileDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtFileDto>>()
            };
        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件详情</returns>
        public async Task<HbtFileDto> GetByIdAsync(long id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            return file == null ? throw new HbtException(L("Routine.File.NotFound", id)) : file.Adapt<HbtFileDto>();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件ID</returns>
        public async Task<long> CreateAsync(HbtFileCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_fileRepository, "FileName", input.FileName);

            var file = input.Adapt<HbtFile>();
            return await _fileRepository.CreateAsync(file) > 0 ? file.Id : throw new HbtException(L("Routine.File.CreateFailed"));
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtFileUpdateDto input)
        {
            var file = await _fileRepository.GetByIdAsync(input.FileId)
                ?? throw new HbtException(L("Routine.File.NotFound", input.FileId));

            // 验证字段是否已存在
            if (file.FileName != input.FileName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_fileRepository, "FileName", input.FileName, input.FileId);

            input.Adapt(file);
            return await _fileRepository.UpdateAsync(file) > 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var file = await _fileRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Routine.File.NotFound", id));

            return await _fileRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] fileIds)
        {
            if (fileIds == null || fileIds.Length == 0) return false;
            return await _fileRepository.DeleteRangeAsync(fileIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtFile")
        {
            _logger.Info($"开始导入文件数据，工作表名称：{sheetName}");
            
            var files = await HbtExcelHelper.ImportAsync<HbtFileImportDto>(fileStream, sheetName);
            if (files == null || !files.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何文件数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {files.Count()} 条文件数据");

            var (success, fail) = (0, 0);
            foreach (var file in files)
            {
                try
                {
                    _logger.Info($"正在处理文件：{file.FileName}");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_fileRepository, "FileName", file.FileName);

                    var fileEntity = file.Adapt<HbtFile>();
                    await _fileRepository.CreateAsync(fileEntity);
                    success++;
                    
                    _logger.Info($"成功导入文件：{file.FileName}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入文件失败：{file.FileName}，错误：{ex.Message}");
                }
            }

            _logger.Info($"文件数据导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtFileQueryDto query, string sheetName = "HbtFile")
        {
            var files = await _fileRepository.GetListAsync(FileQueryExpression(query));
            var exportData = files.Adapt<List<HbtFileExportDto>>();
            var result = await HbtExcelHelper.ExportAsync(exportData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtFile")
        {
            var templateData = new List<HbtFileTemplateDto>();
            var result = await HbtExcelHelper.ExportAsync(templateData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 更新文件状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtFileStatusDto input)
        {
            var file = await _fileRepository.GetByIdAsync(input.FileId)
                ?? throw new HbtException(L("Routine.File.NotFound", input.FileId));

            file.FileStatus = input.FileStatus;
            return await _fileRepository.UpdateAsync(file) > 0;
        }
    }
} 