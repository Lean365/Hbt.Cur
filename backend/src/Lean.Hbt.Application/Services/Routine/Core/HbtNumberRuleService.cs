//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNumberRuleService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 单号规则服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Core;
using Lean.Hbt.Domain.Entities.Routine.Core;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 单号规则服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtNumberRuleService : HbtBaseService, IHbtNumberRuleService
    {
        private readonly IHbtRepository<HbtNumberRule> _numberRuleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="numberRuleRepository">单号规则仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNumberRuleService(
            IHbtLogger logger,
            IHbtRepository<HbtNumberRule> numberRuleRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _numberRuleRepository = numberRuleRepository;
        }

        /// <summary>
        /// 获取单号规则分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtNumberRuleDto>> GetListAsync(HbtNumberRuleQueryDto query)
        {
            _logger.Info("开始查询单号规则列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await _numberRuleRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            var dtoResult = new HbtPagedResult<HbtNumberRuleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtNumberRuleDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取单号规则详情
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>返回单号规则详情</returns>
        public async Task<HbtNumberRuleDto> GetByIdAsync(long numberRuleId)
        {
            var numberRule = await _numberRuleRepository.GetByIdAsync(numberRuleId);
            if (numberRule == null)
                throw new HbtException(L("NumberRule.NotFound", numberRuleId));

            return numberRule.Adapt<HbtNumberRuleDto>();
        }

        /// <summary>
        /// 创建单号规则
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回单号规则ID</returns>
        public async Task<long> CreateAsync(HbtNumberRuleCreateDto input)
        {
            // 检查规则编号是否已存在
            var existingRule = await _numberRuleRepository.GetFirstAsync(x =>
                x.CompanyCode == input.CompanyCode &&
                x.NumberRuleCode == input.NumberRuleCode);

            if (existingRule != null)
                throw new HbtException(L("NumberRule.CodeExists", input.NumberRuleCode));

            var numberRule = input.Adapt<HbtNumberRule>();
            var result = await _numberRuleRepository.CreateAsync(numberRule);
            return result;
        }

        /// <summary>
        /// 更新单号规则
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtNumberRuleUpdateDto input)
        {
            var numberRule = await _numberRuleRepository.GetByIdAsync(input.NumberRuleId);
            if (numberRule == null)
                throw new HbtException(L("NumberRule.NotFound", input.NumberRuleId));

            // 检查规则编号是否已被其他记录使用
            var existingRule = await _numberRuleRepository.GetFirstAsync(x =>
                x.Id != input.NumberRuleId &&
                x.CompanyCode == input.CompanyCode &&
                x.NumberRuleCode == input.NumberRuleCode);

            if (existingRule != null)
                throw new HbtException(L("NumberRule.CodeExists", input.NumberRuleCode));

            input.Adapt(numberRule);
            var result = await _numberRuleRepository.UpdateAsync(numberRule);
            return result > 0;
        }

        /// <summary>
        /// 删除单号规则
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long numberRuleId)
        {
            var numberRule = await _numberRuleRepository.GetByIdAsync(numberRuleId);
            if (numberRule == null)
                throw new HbtException(L("NumberRule.NotFound", numberRuleId));

            var result = await _numberRuleRepository.DeleteAsync(numberRule);
            return result > 0;
        }

        /// <summary>
        /// 批量删除单号规则
        /// </summary>
        /// <param name="numberRuleIds">单号规则ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] numberRuleIds)
        {
            if (numberRuleIds == null || numberRuleIds.Length == 0)
                throw new HbtException(L("NumberRule.SelectToDelete"));

            var numberRules = await _numberRuleRepository.GetListAsync(x => numberRuleIds.Contains(x.Id));
            if (!numberRules.Any())
                throw new HbtException(L("NumberRule.NotFound"));

            var result = await _numberRuleRepository.DeleteAsync(numberRules);
            return result > 0;
        }

        /// <summary>
        /// 导入单号规则数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "单号规则信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtNumberRuleImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var numberRule = dto.Adapt<HbtNumberRule>();
                    await _numberRuleRepository.CreateAsync(numberRule);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("NumberRule.ImportFailed", dto.NumberRuleName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出单号规则数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtNumberRuleQueryDto query, string sheetName = "单号规则信息")
        {
            var predicate = QueryExpression(query);

            var numberRules = await _numberRuleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.OrderNum)
                .ToListAsync();

            var exportDtos = numberRules.Adapt<List<HbtNumberRuleExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "单号规则信息")
        {
            var template = new List<HbtNumberRuleTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtNumberRule, bool>> QueryExpression(HbtNumberRuleQueryDto query)
        {
            var exp = Expressionable.Create<HbtNumberRule>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.CompanyCode))
                    exp = exp.And(x => x.CompanyCode == query.CompanyCode);

                if (!string.IsNullOrEmpty(query.NumberRuleCode))
                    exp = exp.And(x => x.NumberRuleCode.Contains(query.NumberRuleCode));

                if (!string.IsNullOrEmpty(query.NumberRuleName))
                    exp = exp.And(x => x.NumberRuleName.Contains(query.NumberRuleName));

                if (query.NumberRuleType.HasValue)
                    exp = exp.And(x => x.NumberRuleType == query.NumberRuleType.Value);

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.CreateTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
}