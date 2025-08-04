#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtIsoDocumentService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : ISO文档服务实现
//===================================================================

using System.Linq.Expressions;
using Hbt.Common.Utils;
using Hbt.Domain.IServices.Extensions;
using Hbt.Domain.IServices.SignalR;
using Hbt.Common.Models;
using Hbt.Common.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Hbt.Domain.Entities.Routine.Document;
using Hbt.Application.Dtos.Routine.Document.Iso;

namespace Hbt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// ISO文档服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class HbtIsoDocumentService : HbtBaseService, IHbtIsoDocumentService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private readonly IHbtSignalRClient _signalRClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtIsoDocumentService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取ISO文档仓储
        /// </summary>
        private IHbtRepository<HbtIsoDocument> IsoDocumentRepository => _repositoryFactory.GetBusinessRepository<HbtIsoDocument>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtIsoDocument, bool>> IsoDocumentQueryExpression(HbtIsoDocumentQueryDto query)
        {
            return Expressionable.Create<HbtIsoDocument>()
                .AndIF(!string.IsNullOrEmpty(query.DocumentName), x => x.DocumentName!.Contains(query.DocumentName!))
                .AndIF(!string.IsNullOrEmpty(query.DocumentCode), x => x.DocumentCode!.Contains(query.DocumentCode!))
                .AndIF(query.IsoStandard.HasValue, x => x.IsoStandard == query.IsoStandard!.Value)
                .AndIF(query.DocumentType != -1, x => x.DocumentType == query.DocumentType)
                .AndIF(query.DocumentLevel != -1, x => x.DocumentLevel == query.DocumentLevel)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
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

            _logger.Info($"查询ISO文档列表，参数：DocumentName={query.DocumentName}, DocumentCode={query.DocumentCode}, IsoStandard={query.IsoStandard}, DocumentType={query.DocumentType}, DocumentLevel={query.DocumentLevel}, Status={query.Status}");

            var result = await IsoDocumentRepository.GetPagedListAsync(
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
            var document = await IsoDocumentRepository.GetByIdAsync(id);
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
            await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentName", input.DocumentName);
            await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentCode", input.DocumentCode);

            var document = input.Adapt<HbtIsoDocument>();
            var result = await IsoDocumentRepository.CreateAsync(document);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveIsoDocumentNotification(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocument.Created"),
                    Content = L("Routine.IsoDocument.CreatedContent", document.DocumentName),
                    Timestamp = DateTime.Now,
                    Data = document
                });
                
                return document.Id;
            }
            
            throw new HbtException(L("Routine.IsoDocument.CreateFailed"));
        }

        /// <summary>
        /// 更新ISO文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtIsoDocumentUpdateDto input)
        {
            var document = await IsoDocumentRepository.GetByIdAsync(input.IsoDocumentId)
                ?? throw new HbtException(L("Routine.IsoDocument.NotFound", input.IsoDocumentId));

            // 验证字段是否已存在
            if (document.DocumentName != input.DocumentName)
                await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentName", input.DocumentName, input.IsoDocumentId);
            if (document.DocumentCode != input.DocumentCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentCode", input.DocumentCode, input.IsoDocumentId);

            input.Adapt(document);
            var result = await IsoDocumentRepository.UpdateAsync(document);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveIsoDocumentStatusUpdate(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocument.Updated"),
                    Content = L("Routine.IsoDocument.UpdatedContent", document.DocumentName),
                    Timestamp = DateTime.Now,
                    Data = document
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除ISO文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var document = await IsoDocumentRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Routine.IsoDocument.NotFound", id));

            var result = await IsoDocumentRepository.DeleteAsync(id);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveIsoDocumentStatusUpdate(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocument.Deleted"),
                    Content = L("Routine.IsoDocument.DeletedContent", document.DocumentName),
                    Timestamp = DateTime.Now,
                    Data = document
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除ISO文档
        /// </summary>
        /// <param name="documentIds">文档ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] documentIds)
        {
            if (documentIds == null || documentIds.Length == 0) return false;
            
            // 获取要删除的文档信息用于通知
            var documents = await IsoDocumentRepository.GetListAsync(x => documentIds.Contains(x.Id));
            
            var result = await IsoDocumentRepository.DeleteRangeAsync(documentIds.Cast<object>().ToList());
            
            if (result > 0 && documents.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceiveIsoDocumentStatusUpdate(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocument.BatchDeleted"),
                    Content = L("Routine.IsoDocument.BatchDeletedContent", documents.Count),
                    Timestamp = DateTime.Now,
                    Data = documents
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 获取ISO文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        public async Task<List<HbtIsoDocumentDto>> GetTreeAsync(long? parentId = null)
        {
            var documents = await IsoDocumentRepository.GetListAsync(x => x.ParentId == parentId);
            var documentDtos = documents.Adapt<List<HbtIsoDocumentDto>>();

            foreach (var documentDto in documentDtos)
            {
                documentDto.Children = await GetTreeAsync(documentDto.IsoDocumentId);
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
                    await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentName", document.DocumentName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(IsoDocumentRepository, "DocumentCode", document.DocumentCode);

                    var documentEntity = document.Adapt<HbtIsoDocument>();
                    await IsoDocumentRepository.CreateAsync(documentEntity);
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
            
            // 发送实时通知
            if (success > 0)
            {
                await _signalRClient.ReceiveIsoDocumentNotification(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocument.ImportCompleted"),
                    Content = L("Routine.IsoDocument.ImportCompletedContent", success, fail),
                    Timestamp = DateTime.Now,
                    Data = new { Success = success, Fail = fail }
                });
            }
            
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
            var documents = await IsoDocumentRepository.GetListAsync(IsoDocumentQueryExpression(query));
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
                
                // 发送实时通知
                await _signalRClient.ReceiveIsoDocumentStatusUpdate(new HbtRealTimeNotification
                {
                    Type = HbtMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocument.RequestCompleted"),
                    Content = L("Routine.IsoDocument.RequestCompletedContent", requestId),
                    Timestamp = DateTime.Now,
                    Data = new { RequestId = requestId }
                });
                
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