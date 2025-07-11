#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIsoDocumentService.cs
// 创建者 : Lean365
// 创建时间: 2024-06-09
// 版本号 : V1.0.0
// 描述    : ISO文档服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Routine.Document;
using Lean.Hbt.Application.Dtos.Routine.Document.Iso;

namespace Lean.Hbt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// ISO文档服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-06-09
    /// </remarks>
    public class HbtIsoDocumentService : HbtBaseService, IHbtIsoDocumentService
    {
        private readonly IHbtRepository<HbtIsoDocument> _isoDocumentRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isoDocumentRepository">ISO文档仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtIsoDocumentService(
            IHbtRepository<HbtIsoDocument> isoDocumentRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _isoDocumentRepository = isoDocumentRepository ?? throw new ArgumentNullException(nameof(isoDocumentRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtIsoDocument, bool>> IsoDocumentQueryExpression(HbtIsoDocumentQueryDto query)
        {
            return Expressionable.Create<HbtIsoDocument>()
                .AndIF(!string.IsNullOrEmpty(query.DocumentName), x => x.DocumentName!.Contains(query.DocumentName!))
                .AndIF(!string.IsNullOrEmpty(query.DocumentCode), x => x.DocumentCode!.Contains(query.DocumentCode!))
                .AndIF(!string.IsNullOrEmpty(query.IsoStandard), x => x.IsoStandard!.Contains(query.IsoStandard!))
                .AndIF(query.DocumentType != -1, x => x.DocumentType == query.DocumentType)
                .AndIF(query.DocumentLevel != -1, x => x.DocumentLevel == query.DocumentLevel)
                .AndIF(query.DocumentStatus != -1, x => x.DocumentStatus == query.DocumentStatus)
                .ToExpression();
        }

        /// <summary>
        /// 获取ISO文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>ISO文档分页列表</returns>
        public async Task<HbtPagedResult<HbtIsoDocumentDto>> GetListAsync(HbtIsoDocumentQueryDto? query)
        {
            query ??= new HbtIsoDocumentQueryDto();

            _logger.Info($"查询ISO文档列表，参数：DocumentName={query.DocumentName}, DocumentCode={query.DocumentCode}, IsoStandard={query.IsoStandard}, DocumentType={query.DocumentType}, DocumentLevel={query.DocumentLevel}, DocumentStatus={query.DocumentStatus}");

            var result = await _isoDocumentRepository.GetPagedListAsync(
                IsoDocumentQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询ISO文档列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtIsoDocumentDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtIsoDocumentDto>>()
            };
        }

        /// <summary>
        /// 获取ISO文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>ISO文档详情</returns>
        public async Task<HbtIsoDocumentDto> GetByIdAsync(long id)
        {
            var document = await _isoDocumentRepository.GetByIdAsync(id);
            return document == null ? throw new HbtException(L("Routine.IsoDocument.NotFound", id)) : document.Adapt<HbtIsoDocumentDto>();
        }

        /// <summary>
        /// 创建ISO文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        public async Task<long> CreateAsync(HbtIsoDocumentCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentName", input.DocumentName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentCode", input.DocumentCode);

            var document = input.Adapt<HbtIsoDocument>();
            return await _isoDocumentRepository.CreateAsync(document) > 0 ? document.Id : throw new HbtException(L("Routine.IsoDocument.CreateFailed"));
        }

        /// <summary>
        /// 更新ISO文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtIsoDocumentUpdateDto input)
        {
            var document = await _isoDocumentRepository.GetByIdAsync(input.DocumentId)
                ?? throw new HbtException(L("Routine.IsoDocument.NotFound", input.DocumentId));

            // 验证字段是否已存在
            if (document.DocumentName != input.DocumentName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentName", input.DocumentName, input.DocumentId);
            if (document.DocumentCode != input.DocumentCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentCode", input.DocumentCode, input.DocumentId);

            input.Adapt(document);
            return await _isoDocumentRepository.UpdateAsync(document) > 0;
        }

        /// <summary>
        /// 删除ISO文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var document = await _isoDocumentRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Routine.IsoDocument.NotFound", id));

            return await _isoDocumentRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除ISO文档
        /// </summary>
        /// <param name="documentIds">文档ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] documentIds)
        {
            if (documentIds == null || documentIds.Length == 0) return false;
            return await _isoDocumentRepository.DeleteRangeAsync(documentIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取ISO文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        public async Task<List<HbtIsoDocumentDto>> GetTreeAsync(long? parentId = null)
        {
            var documents = await _isoDocumentRepository.GetListAsync(x => x.ParentId == parentId);
            var documentDtos = documents.Adapt<List<HbtIsoDocumentDto>>();

            foreach (var documentDto in documentDtos)
            {
                documentDto.Children = await GetTreeAsync(documentDto.Id);
            }

            return documentDtos;
        }

        /// <summary>
        /// 导入ISO文档数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtIsoDocument")
        {
            _logger.Info($"开始导入ISO文档数据，工作表名称：{sheetName}");
            
            var documents = await HbtExcelHelper.ImportAsync<HbtIsoDocumentImportDto>(fileStream, sheetName);
            if (documents == null || !documents.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何ISO文档数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {documents.Count()} 条ISO文档数据");

            var (success, fail) = (0, 0);
            foreach (var document in documents)
            {
                try
                {
                    _logger.Info($"正在处理ISO文档：{document.DocumentName} (Code: {document.DocumentCode})");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentName", document.DocumentName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_isoDocumentRepository, "DocumentCode", document.DocumentCode);

                    var documentEntity = document.Adapt<HbtIsoDocument>();
                    await _isoDocumentRepository.CreateAsync(documentEntity);
                    success++;
                    
                    _logger.Info($"成功导入ISO文档：{document.DocumentName}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入ISO文档失败：{document.DocumentName}，错误：{ex.Message}");
                }
            }

            _logger.Info($"ISO文档数据导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出ISO文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtIsoDocumentQueryDto query, string sheetName = "HbtIsoDocument")
        {
            var documents = await _isoDocumentRepository.GetListAsync(IsoDocumentQueryExpression(query));
            var exportData = documents.Adapt<List<HbtIsoDocumentExportDto>>();
            var result = await HbtExcelHelper.ExportAsync(exportData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtIsoDocument")
        {
            var templateData = new List<HbtIsoDocumentTemplateDto>();
            var result = await HbtExcelHelper.ExportAsync(templateData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 处理ISO文档请求审批完成后的自动更新
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <returns>更新结果</returns>
        public async Task<HbtApiResult<bool>> ProcessRequestCompletionAsync(long requestId)
        {
            try
            {
                _logger.Info($"开始处理ISO文档请求审批完成，请求ID：{requestId}");
                
                // TODO: 实现ISO文档请求审批完成后的自动更新逻辑
                // 1. 根据请求ID获取请求信息
                // 2. 更新相关ISO文档状态
                // 3. 发送通知等
                
                _logger.Info($"成功处理ISO文档请求审批完成，请求ID：{requestId}");
                return new HbtApiResult<bool> { Code = 200, Msg = "操作成功", Data = true };
            }
            catch (Exception ex)
            {
                _logger.Error($"处理ISO文档请求审批完成失败，请求ID：{requestId}，错误：{ex.Message}");
                return new HbtApiResult<bool> { Code = 500, Msg = ex.Message, Data = false };
            }
        }
    }
} 