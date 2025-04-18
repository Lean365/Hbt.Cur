//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtTranslationService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务实现类
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Domain.Entities.Admin;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 翻译服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtTranslationService : IHbtTranslationService
    {
        private readonly IHbtRepository<HbtTranslation> _translationRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="translationRepository">翻译仓储</param>
        /// <param name="logger">日志接口</param>
        public HbtTranslationService(
            IHbtRepository<HbtTranslation> translationRepository,
            IHbtLogger logger)
        {
            _translationRepository = translationRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtTranslationDto>> GetListAsync(HbtTranslationQueryDto query)
        {
            var exp = Expressionable.Create<HbtTranslation>();

            if (!string.IsNullOrEmpty(query.LangCode))
                exp.And(x => x.LangCode == query.LangCode);

            if (!string.IsNullOrEmpty(query.TransKey))
                exp.And(x => x.TransKey.Contains(query.TransKey));

            if (!string.IsNullOrEmpty(query.ModuleName))
                exp.And(x => x.ModuleName == query.ModuleName);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _translationRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtTranslationDto>
            {
                Rows = result.Rows.Adapt<List<HbtTranslationDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        public async Task<HbtTranslationDto> GetByIdAsync(long TransId)
        {
            var translation = await _translationRepository.GetByIdAsync(TransId);
            if (translation == null)
                throw new HbtException("翻译不存在");

            return translation.Adapt<HbtTranslationDto>();
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        public async Task<long> CreateAsync(HbtTranslationCreateDto input)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_translationRepository, "TransKey", input.TransKey);

            var translation = input.Adapt<HbtTranslation>();
            translation.Status = 0; // 0表示正常状态

            var result = await _translationRepository.CreateAsync(translation);
            return result > 0 ? translation.Id : 0;
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        public async Task<bool> UpdateAsync(HbtTranslationUpdateDto input)
        {
            var translation = await _translationRepository.GetByIdAsync(input.TransId);
            if (translation == null)
                throw new HbtException("翻译不存在");

            if (translation.TransKey != input.TransKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_translationRepository, "TransKey", input.TransKey, input.TransId);

            translation.LangCode = input.LangCode;
            translation.TransKey = input.TransKey;
            translation.TransValue = input.TransValue;
            translation.ModuleName = input.ModuleName;
            translation.Remark = input.Remark;
            translation.Status = input.Status;

            return await _translationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        public async Task<bool> DeleteAsync(long TransId)
        {
            var translation = await _translationRepository.GetByIdAsync(TransId);
            if (translation == null)
                throw new HbtException("翻译不存在");

            return await _translationRepository.DeleteAsync(translation) > 0;
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] TransIds)
        {
            if (TransIds == null || TransIds.Length == 0)
                return false;

            var entities = await _translationRepository.GetListAsync(x => TransIds.Contains(x.Id));
            return await _translationRepository.DeleteRangeAsync(entities) > 0;
        }

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "翻译信息")
        {
            var success = 0;
            var fail = 0;

            try
            {
                // 1.从Excel导入数据
                var translations = await HbtExcelHelper.ImportAsync<HbtTranslationDto>(fileStream, sheetName);
                if (translations == null || !translations.Any())
                    return (0, 0);

                // 2.保存数据
                foreach (var item in translations)
                {
                    try
                    {
                        var translation = item.Adapt<HbtTranslation>();
                        await _translationRepository.CreateAsync(translation);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"导入翻译失败：{ex.Message}", ex);
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"导入翻译数据失败：{ex.Message}", ex);
                throw new HbtException("导入翻译数据失败");
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtTranslationQueryDto query, string sheetName = "翻译信息")
        {
            var exp = Expressionable.Create<HbtTranslation>();

            if (!string.IsNullOrEmpty(query.LangCode))
                exp.And(x => x.LangCode == query.LangCode);

            if (!string.IsNullOrEmpty(query.TransKey))
                exp.And(x => x.TransKey.Contains(query.TransKey));

            if (!string.IsNullOrEmpty(query.ModuleName))
                exp.And(x => x.ModuleName == query.ModuleName);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _translationRepository.GetListAsync(exp.ToExpression());
            var dtos = list.Adapt<List<HbtTranslationDto>>();

            return await HbtExcelHelper.ExportAsync(dtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "翻译信息")
        {
            var template = new List<HbtTranslationDto>
            {
                new HbtTranslationDto
                {
                    LangCode = "zh-CN",
                    TransKey = "example.key",
                    TransValue = "示例文本",
                    ModuleName = "common",
                    Status = 0, // 0表示正常状态
                    Remark = "示例备注"
                }
            };

            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 更新翻译状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(HbtTranslationStatusDto input)
        {
            var translation = await _translationRepository.GetByIdAsync(input.TransId);
            if (translation == null)
                throw new HbtException("翻译不存在");

            translation.Status = input.Status;
            return await _translationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        public async Task<string> GetTransValueAsync(string langCode, string transKey)
        {
            var translation = await _translationRepository.GetFirstAsync(x => x.LangCode == langCode && x.TransKey == transKey);
            return translation?.TransValue ?? string.Empty;
        }

        /// <summary>
        /// 获取指定模块的翻译列表
        /// </summary>
        public async Task<List<HbtTranslationDto>> GetListByModuleAsync(string langCode, string moduleName)
        {
            var list = await _translationRepository.GetListAsync(x => x.LangCode == langCode && x.ModuleName == moduleName);
            return list.Adapt<List<HbtTranslationDto>>();
        }

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        public async Task<HbtPagedResult<Dictionary<string, string>>> GetTransposedDataAsync(HbtTranslationQueryDto query)
        {
            // 1. 构建查询条件
            var exp = Expressionable.Create<HbtTranslation>();

            if (!string.IsNullOrEmpty(query.ModuleName))
                exp.And(x => x.ModuleName == query.ModuleName);

            if (!string.IsNullOrEmpty(query.TransKey))
                exp.And(x => x.TransKey.Contains(query.TransKey));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            // 2. 获取所有语言代码
            var allLangCodes = await _translationRepository.GetListAsync(x => true);
            var langCodes = allLangCodes.Select(x => x.LangCode).Distinct().ToList();

            // 3. 获取分页数据
            var result = await _translationRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            // 4. 转换为字典格式
            var transposedData = result.Rows.GroupBy(x => x.TransKey)
                .Select(g => new Dictionary<string, string>
                {
                    ["transKey"] = g.Key,
                    // 添加每个语言的翻译
                }.Concat(g.ToDictionary(x => x.LangCode, x => x.TransValue))
                 .ToDictionary(x => x.Key, x => x.Value))
                .ToList();

            return new HbtPagedResult<Dictionary<string, string>>
            {
                Rows = transposedData,
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }
    }
}