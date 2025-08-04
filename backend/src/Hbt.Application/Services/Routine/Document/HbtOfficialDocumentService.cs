#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOfficialDocumentService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 公文文档服务实现
//===================================================================

using System.Linq.Expressions;
using Hbt.Common.Utils;
using Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Hbt.Domain.Entities.Routine.Document;
using Hbt.Application.Dtos.Routine.Document;

namespace Hbt.Application.Services.Routine.Document
{
    /// <summary>
    /// 公文文档服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class HbtOfficialDocumentService : HbtBaseService, IHbtOfficialDocumentService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtOfficialDocumentService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取公文文档仓储
        /// </summary>
        private IHbtRepository<HbtOfficialDocument> OfficialDocumentRepository => _repositoryFactory.GetBusinessRepository<HbtOfficialDocument>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtOfficialDocument, bool>> OfficialDocumentQueryExpression(HbtOfficialDocumentQueryDto query)
        {
            return Expressionable.Create<HbtOfficialDocument>()
                .AndIF(!string.IsNullOrEmpty(query.DocumentTitle), x => x.DocumentTitle!.Contains(query.DocumentTitle!))
                .AndIF(!string.IsNullOrEmpty(query.DocumentNumber), x => x.DocumentNumber!.Contains(query.DocumentNumber!))
                .AndIF(!string.IsNullOrEmpty(query.IssuingDepartment), x => x.IssuingDepartment!.Contains(query.IssuingDepartment!))
                .AndIF(query.DocumentType != -1, x => x.DocumentType == query.DocumentType)
                .AndIF(query.DocumentLevel != -1, x => x.DocumentLevel == query.DocumentLevel)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取公文文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>公文文档分页列表</returns>
        public async Task<HbtPagedResult<HbtOfficialDocumentDto>> GetListAsync(HbtOfficialDocumentQueryDto? query)
        {
            query ??= new HbtOfficialDocumentQueryDto();

            _logger.Info($"查询公文文档列表，参数：DocumentTitle={query.DocumentTitle}, DocumentNumber={query.DocumentNumber}, IssuingDepartment={query.IssuingDepartment}, DocumentType={query.DocumentType}, DocumentLevel={query.DocumentLevel}, Status={query.Status}");

            var result = await OfficialDocumentRepository.GetPagedListAsync(
                OfficialDocumentQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询公文文档列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtOfficialDocumentDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtOfficialDocumentDto>>()
            };
        }

        /// <summary>
        /// 获取公文文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>公文文档详情</returns>
        public async Task<HbtOfficialDocumentDto> GetByIdAsync(long id)
        {
            var document = await OfficialDocumentRepository.GetByIdAsync(id);
            return document == null ? throw new HbtException(L("Routine.OfficialDocument.NotFound", id)) : document.Adapt<HbtOfficialDocumentDto>();
        }

        /// <summary>
        /// 创建公文文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        public async Task<long> CreateAsync(HbtOfficialDocumentCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentTitle", input.DocumentTitle);
            await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentNumber", input.DocumentNumber);

            var document = input.Adapt<HbtOfficialDocument>();
            return await OfficialDocumentRepository.CreateAsync(document) > 0 ? document.Id : throw new HbtException(L("Routine.OfficialDocument.CreateFailed"));
        }

        /// <summary>
        /// 更新公文文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtOfficialDocumentUpdateDto input)
        {
            var document = await OfficialDocumentRepository.GetByIdAsync(input.OfficialDocumentId)
                ?? throw new HbtException(L("Routine.OfficialDocument.NotFound", input.OfficialDocumentId));

            // 验证字段是否已存在
            if (document.DocumentTitle != input.DocumentTitle)
                await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentTitle", input.DocumentTitle, input.OfficialDocumentId);
            if (document.DocumentNumber != input.DocumentNumber)
                await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentNumber", input.DocumentNumber, input.OfficialDocumentId);

            input.Adapt(document);
            return await OfficialDocumentRepository.UpdateAsync(document) > 0;
        }

        /// <summary>
        /// 删除公文文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var document = await OfficialDocumentRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Routine.OfficialDocument.NotFound", id));

            return await OfficialDocumentRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除公文文档
        /// </summary>
        /// <param name="documentIds">文档ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] documentIds)
        {
            if (documentIds == null || documentIds.Length == 0) return false;
            return await OfficialDocumentRepository.DeleteRangeAsync(documentIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取公文文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        public async Task<List<HbtOfficialDocumentDto>> GetTreeAsync(long? parentId = null)
        {
            var documents = await OfficialDocumentRepository.GetListAsync(x => x.ParentId == parentId);
            var documentDtos = documents.Adapt<List<HbtOfficialDocumentDto>>();

            foreach (var documentDto in documentDtos)
            {
                documentDto.Children = await GetTreeAsync(documentDto.OfficialDocumentId);
            }

            return documentDtos;
        }

        /// <summary>
        /// 导入公文文档数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtOfficialDocument")
        {
            _logger.Info($"开始导入公文文档数据，工作表名称：{sheetName}");
            
            var documents = await HbtExcelHelper.ImportAsync<HbtOfficialDocumentImportDto>(fileStream, sheetName);
            if (documents == null || !documents.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何公文文档数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {documents.Count()} 条公文文档数据");

            var (success, fail) = (0, 0);
            foreach (var document in documents)
            {
                try
                {
                    _logger.Info($"正在处理公文文档：{document.DocumentTitle} (Number: {document.DocumentNumber})");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentTitle", document.DocumentTitle);
                    await HbtValidateUtils.ValidateFieldExistsAsync(OfficialDocumentRepository, "DocumentNumber", document.DocumentNumber);

                    var documentEntity = document.Adapt<HbtOfficialDocument>();
                    await OfficialDocumentRepository.CreateAsync(documentEntity);
                    success++;
                    
                    _logger.Info($"成功导入公文文档：{document.DocumentTitle}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入公文文档失败：{document.DocumentTitle}，错误：{ex.Message}");
                }
            }

            _logger.Info($"公文文档数据导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出公文文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtOfficialDocumentQueryDto query, string sheetName = "HbtOfficialDocument")
        {
            var documents = await OfficialDocumentRepository.GetListAsync(OfficialDocumentQueryExpression(query));
            var exportData = documents.Adapt<List<HbtOfficialDocumentExportDto>>();
            var result = await HbtExcelHelper.ExportAsync(exportData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtOfficialDocument")
        {
            var templateData = new List<HbtOfficialDocumentTemplateDto>();
            var result = await HbtExcelHelper.ExportAsync(templateData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 处理公文文档请求审批完成后的自动更新
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <returns>更新结果</returns>
        public Task<HbtApiResult<bool>> ProcessRequestCompletionAsync(long requestId)
        {
            try
            {
                _logger.Info($"开始处理公文文档请求审批完成，请求ID：{requestId}");
                
                // TODO: 实现公文文档请求审批完成后的自动更新逻辑
                // 1. 根据请求ID获取请求信息
                // 2. 更新相关公文文档状态
                // 3. 发送通知等
                
                _logger.Info($"成功处理公文文档请求审批完成，请求ID：{requestId}");
                return Task.FromResult(new HbtApiResult<bool> { Code = 200, Data = true });
            }
            catch (Exception ex)
            {
                _logger.Error($"处理公文文档请求审批完成失败，请求ID：{requestId}，错误：{ex.Message}");
                return Task.FromResult(new HbtApiResult<bool> { Code = 500, Msg = ex.Message });
            }
        }


    }
} 