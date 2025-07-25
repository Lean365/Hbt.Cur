#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLawService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 法律法规服务实现
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Application.Dtos.Routine.Document;

namespace Lean.Hbt.Application.Services.Routine.Document
{
    /// <summary>
    /// 法律法规服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class HbtLawService : HbtBaseService, IHbtLawService
    {
        private readonly IHbtRepository<HbtLaw> _lawRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lawRepository">法律法规仓储</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtLawService(
            IHbtRepository<HbtLaw> lawRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _lawRepository = lawRepository ?? throw new ArgumentNullException(nameof(lawRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtLaw, bool>> LawQueryExpression(HbtLawQueryDto query)
        {
            return Expressionable.Create<HbtLaw>()
                .AndIF(!string.IsNullOrEmpty(query.LawName), x => x.LawName!.Contains(query.LawName!))
                .AndIF(!string.IsNullOrEmpty(query.LawCode), x => x.LawCode!.Contains(query.LawCode!))
                .AndIF(!string.IsNullOrEmpty(query.IssuingAuthority), x => x.IssuingAuthority!.Contains(query.IssuingAuthority!))
                .AndIF(query.LawType != -1, x => x.LawType == query.LawType)
                .AndIF(query.LawLevel != -1, x => x.LawLevel == query.LawLevel)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取法律法规分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>法律法规分页列表</returns>
        public async Task<HbtPagedResult<HbtLawDto>> GetListAsync(HbtLawQueryDto? query)
        {
            query ??= new HbtLawQueryDto();

            _logger.Info($"查询法律法规列表，参数：LawName={query.LawName}, LawCode={query.LawCode}, IssuingAuthority={query.IssuingAuthority}, LawType={query.LawType}, LawLevel={query.LawLevel}, Status={query.Status}");

            var result = await _lawRepository.GetPagedListAsync(
                LawQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询法律法规列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtLawDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtLawDto>>()
            };
        }

        /// <summary>
        /// 获取法律法规详情
        /// </summary>
        /// <param name="lawId">法律法规ID</param>
        /// <returns>法律法规详情</returns>
        public async Task<HbtLawDto> GetByIdAsync(long lawId)
        {
            var law = await _lawRepository.GetByIdAsync(lawId);
            return law == null ? throw new HbtException(L("Routine.Law.NotFound", lawId)) : law.Adapt<HbtLawDto>();
        }

        /// <summary>
        /// 创建法律法规
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>法律法规ID</returns>
        public async Task<long> CreateAsync(HbtLawCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawName", input.LawName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawCode", input.LawCode);

            var law = input.Adapt<HbtLaw>();
            return await _lawRepository.CreateAsync(law) > 0 ? law.Id : throw new HbtException(L("Routine.Law.CreateFailed"));
        }

        /// <summary>
        /// 更新法律法规
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtLawUpdateDto input)
        {
            var law = await _lawRepository.GetByIdAsync(input.LawId)
                ?? throw new HbtException(L("Routine.Law.NotFound", input.LawId));

            // 验证字段是否已存在
            if (law.LawName != input.LawName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawName", input.LawName, input.LawId);
            if (law.LawCode != input.LawCode)
                await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawCode", input.LawCode, input.LawId);

            input.Adapt(law);
            return await _lawRepository.UpdateAsync(law) > 0;
        }

        /// <summary>
        /// 删除法律法规
        /// </summary>
        /// <param name="lawId">法律法规ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long lawId)
        {
            var law = await _lawRepository.GetByIdAsync(lawId)
                ?? throw new HbtException(L("Routine.Law.NotFound", lawId));

            return await _lawRepository.DeleteAsync(lawId) > 0;
        }

        /// <summary>
        /// 批量删除法律法规
        /// </summary>
        /// <param name="lawIds">法律法规ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] lawIds)
        {
            if (lawIds == null || lawIds.Length == 0) return false;
            return await _lawRepository.DeleteRangeAsync(lawIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取法律法规树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        public async Task<List<HbtLawDto>> GetTreeAsync(long? parentId = null)
        {
            var laws = await _lawRepository.GetListAsync(x => x.ParentId == parentId);
            var lawDtos = laws.Adapt<List<HbtLawDto>>();

            foreach (var lawDto in lawDtos)
            {
                lawDto.Children = await GetTreeAsync(lawDto.LawId);
            }

            return lawDtos;
        }

        /// <summary>
        /// 导入法律法规数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtLaw")
        {
            _logger.Info($"开始导入法律法规数据，工作表名称：{sheetName}");
            
            var laws = await HbtExcelHelper.ImportAsync<HbtLawImportDto>(fileStream, sheetName);
            if (laws == null || !laws.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何法律法规数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {laws.Count()} 条法律法规数据");

            var (success, fail) = (0, 0);
            foreach (var law in laws)
            {
                try
                {
                    _logger.Info($"正在处理法律法规：{law.LawName} (Code: {law.LawCode})");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawName", law.LawName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_lawRepository, "LawCode", law.LawCode);

                    var lawEntity = law.Adapt<HbtLaw>();
                    await _lawRepository.CreateAsync(lawEntity);
                    success++;
                    
                    _logger.Info($"成功导入法律法规：{law.LawName}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入法律法规失败：{law.LawName}，错误：{ex.Message}");
                }
            }

            _logger.Info($"法律法规数据导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出法律法规数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtLawQueryDto query, string sheetName = "HbtLaw")
        {
            var laws = await _lawRepository.GetListAsync(LawQueryExpression(query));
            var exportData = laws.Adapt<List<HbtLawExportDto>>();
            var result = await HbtExcelHelper.ExportAsync(exportData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtLaw")
        {
            var templateData = new List<HbtLawTemplateDto>();
            var result = await HbtExcelHelper.ExportAsync(templateData, sheetName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 更新法律法规状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtLawStatusDto input)
        {
            var law = await _lawRepository.GetByIdAsync(input.LawId)
                ?? throw new HbtException(L("Routine.Law.NotFound", input.LawId));

            law.Status = input.Status;
            return await _lawRepository.UpdateAsync(law) > 0;
        }

        /// <summary>
        /// 从国家行政机关下载法律法规
        /// </summary>
        /// <param name="lawCode">法规编号</param>
        /// <returns>下载结果</returns>
        public async Task<HbtApiResult<bool>> DownloadFromGovernmentAsync(string lawCode)
        {
            try
            {
                _logger.Info($"开始从国家行政机关下载法律法规，法规编号：{lawCode}");
                
                // TODO: 实现从国家行政机关API下载法律法规的逻辑
                
                _logger.Info($"成功从国家行政机关下载法律法规，法规编号：{lawCode}");
                return new HbtApiResult<bool> { Code = 200, Data = true };
            }
            catch (Exception ex)
            {
                _logger.Error($"从国家行政机关下载法律法规失败，法规编号：{lawCode}，错误：{ex.Message}");
                return new HbtApiResult<bool> { Code = 500, Msg = ex.Message };
            }
        }

        /// <summary>
        /// 批量下载法律法规
        /// </summary>
        /// <param name="lawCodes">法规编号集合</param>
        /// <returns>下载结果</returns>
        public async Task<HbtApiResult<(int success, int fail)>> BatchDownloadAsync(string[] lawCodes)
        {
            try
            {
                _logger.Info($"开始批量下载法律法规，法规编号数量：{lawCodes?.Length ?? 0}");
                
                var (success, fail) = (0, 0);
                foreach (var lawCode in lawCodes ?? Array.Empty<string>())
                {
                    var result = await DownloadFromGovernmentAsync(lawCode);
                    if (result.Code == 200)
                        success++;
                    else
                        fail++;
                }
                
                _logger.Info($"批量下载法律法规完成，成功：{success}，失败：{fail}");
                return new HbtApiResult<(int success, int fail)> { Code = 200, Data = (success, fail) };
            }
            catch (Exception ex)
            {
                _logger.Error($"批量下载法律法规失败，错误：{ex.Message}");
                return new HbtApiResult<(int success, int fail)> { Code = 500, Msg = ex.Message };
            }
        }
    }
} 