#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRegulationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 规章制度服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Routine.Document;
using Lean.Hbt.Application.Dtos.Routine.Document;

namespace Lean.Hbt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// 规章制度服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class HbtRegulationService : HbtBaseService, IHbtRegulationService
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
        public HbtRegulationService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取规章制度仓储
        /// </summary>
        private IHbtRepository<HbtRegulation> RegulationRepository => _repositoryFactory.GetBusinessRepository<HbtRegulation>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtRegulation, bool>> RegulationQueryExpression(HbtRegulationQueryDto query)
        {
            return Expressionable.Create<HbtRegulation>()
                .AndIF(!string.IsNullOrEmpty(query.RegulationTitle), x => x.RegulationTitle!.Contains(query.RegulationTitle!))
                .AndIF(!string.IsNullOrEmpty(query.RegulationCode), x => x.RegulationCode!.Contains(query.RegulationCode!))
                .AndIF(!string.IsNullOrEmpty(query.PublishDepartment), x => x.PublishDepartment!.Contains(query.PublishDepartment!))
                .AndIF(query.RegulationType != -1, x => x.RegulationType == query.RegulationType)
                .AndIF(query.RegulationLevel != -1, x => x.RegulationLevel == query.RegulationLevel)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取规章制度分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>规章制度分页列表</returns>
        public async Task<HbtPagedResult<HbtRegulationDto>> GetListAsync(HbtRegulationQueryDto? query)
        {
            query ??= new HbtRegulationQueryDto();

            _logger.Info($"查询规章制度列表，参数：RegulationTitle={query.RegulationTitle}, RegulationCode={query.RegulationCode}, PublishDepartment={query.PublishDepartment}, RegulationType={query.RegulationType}, RegulationLevel={query.RegulationLevel}, Status={query.Status}");

            var result = await RegulationRepository.GetPagedListAsync(
                RegulationQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询规章制度列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtRegulationDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtRegulationDto>>()
            };
        }

        /// <summary>
        /// 获取规章制度详情
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>规章制度详情</returns>
        public async Task<HbtRegulationDto> GetByIdAsync(long id)
        {
            var regulation = await RegulationRepository.GetByIdAsync(id);
            return regulation == null ? throw new HbtException(L("Routine.Regulation.NotFound", id)) : regulation.Adapt<HbtRegulationDto>();
        }

        /// <summary>
        /// 创建规章制度
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>规章制度ID</returns>
        public async Task<long> CreateAsync(HbtRegulationCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationTitle", input.RegulationTitle);
            await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationCode", input.RegulationCode);

            var regulation = input.Adapt<HbtRegulation>();
            return await RegulationRepository.CreateAsync(regulation) > 0 ? regulation.Id : throw new HbtException(L("Routine.Regulation.CreateFailed"));
        }

        /// <summary>
        /// 更新规章制度
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtRegulationUpdateDto input)
        {
            var regulation = await RegulationRepository.GetByIdAsync(input.RegulationId)
                ?? throw new HbtException(L("Routine.Regulation.NotFound", input.RegulationId));

            // 验证字段是否已存在
            if (regulation.RegulationTitle != input.RegulationTitle)
                await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationTitle", input.RegulationTitle, input.RegulationId);
            if (regulation.RegulationCode != input.RegulationCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationCode", input.RegulationCode, input.RegulationId);

            input.Adapt(regulation);
            return await RegulationRepository.UpdateAsync(regulation) > 0;
        }

        /// <summary>
        /// 删除规章制度
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var regulation = await RegulationRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("Routine.Regulation.NotFound", id));

            return await RegulationRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除规章制度
        /// </summary>
        /// <param name="regulationIds">规章制度ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] regulationIds)
        {
            if (regulationIds == null || regulationIds.Length == 0) return false;
            return await RegulationRepository.DeleteRangeAsync(regulationIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取规章制度树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        public async Task<List<HbtRegulationDto>> GetTreeAsync(long? parentId = null)
        {
            var regulations = await RegulationRepository.GetListAsync(x => x.ParentId == parentId);
            var regulationDtos = regulations.Adapt<List<HbtRegulationDto>>();

            foreach (var regulationDto in regulationDtos)
            {
                regulationDto.Children = await GetTreeAsync(regulationDto.RegulationId);
            }

            return regulationDtos;
        }

        /// <summary>
        /// 导入规章制度数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtRegulation")
        {
            _logger.Info($"开始导入规章制度数据，工作表名称：{sheetName}");
            
            var regulations = await HbtExcelHelper.ImportAsync<HbtRegulationImportDto>(fileStream, sheetName);
            if (regulations == null || !regulations.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何规章制度数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {regulations.Count()} 条规章制度数据");

            var (success, fail) = (0, 0);
            foreach (var regulation in regulations)
            {
                try
                {
                    _logger.Info($"正在处理规章制度：{regulation.RegulationTitle} (Code: {regulation.RegulationCode})");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationTitle", regulation.RegulationTitle);
                    await HbtValidateUtils.ValidateFieldExistsAsync(RegulationRepository, "RegulationCode", regulation.RegulationCode);

                    var regulationEntity = regulation.Adapt<HbtRegulation>();
                    await RegulationRepository.CreateAsync(regulationEntity);
                    success++;
                    
                    _logger.Info($"成功导入规章制度：{regulation.RegulationTitle}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入规章制度失败：{regulation.RegulationTitle}，错误：{ex.Message}");
                }
            }

            _logger.Info($"规章制度数据导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出规章制度数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtRegulationQueryDto query, string sheetName = "HbtRegulation")
        {
            var regulations = await RegulationRepository.GetListAsync(RegulationQueryExpression(query));
            var exportData = regulations.Adapt<List<HbtRegulationExportDto>>();
            var result = await HbtExcelHelper.ExportAsync(exportData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtRegulation")
        {
            var templateData = new List<HbtRegulationTemplateDto>();
            var result = await HbtExcelHelper.ExportAsync(templateData, sheetName);
            return (result.fileName, result.content);
        }
    }
} 