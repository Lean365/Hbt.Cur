//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNumberGeneratorService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 通用单号生成器服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hbt.Common.Models;
using Hbt.Common.Exceptions;
using Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Hbt.Common.Utils;
using Hbt.Domain.Utils;
using Hbt.Common.Constants;
using Hbt.Domain.Entities.Routine.Core;
using Hbt.Application.Dtos.Routine.Core;

namespace Hbt.Application.Services.Core
{
    /// <summary>
    /// 通用单号生成器服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtNumberGeneratorService : HbtBaseService, IHbtNumberGeneratorService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private readonly IHbtDbContext _dbContext;

        /// <summary>
        /// 获取单号规则仓储
        /// </summary>
        private IHbtRepository<HbtNumberRule> NumberRuleRepository => _repositoryFactory.GetBusinessRepository<HbtNumberRule>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNumberGeneratorService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _dbContext = dbContext;
        }

        /// <summary>
        /// 根据规则生成单号
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <returns>生成的单号</returns>
        public async Task<string> GenerateNumberAsync(string numberRuleCode, string companyCode)
        {
            return await GenerateNumberAsync(numberRuleCode, companyCode, null);
        }

        /// <summary>
        /// 根据规则生成单号（带自定义参数）
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>生成的单号</returns>
        public async Task<string> GenerateNumberAsync(string numberRuleCode, string companyCode, HbtNumberGeneratorParams customParams)
        {
            var numberRule = await GetRuleAsync(numberRuleCode, companyCode);
            if (numberRule == null)
                throw new HbtException(L("NumberRule.NotFound", numberRuleCode));

            // 检查是否需要重置序列号
            if (ShouldResetSequence(numberRule))
            {
                numberRule.CurrentSequence = numberRule.SequenceStart;
                numberRule.LastResetDate = DateTime.Today;
            }
            else
            {
                numberRule.CurrentSequence += numberRule.SequenceStep;
            }

            numberRule.LastUsedTime = DateTime.Now;
            numberRule.UsageCount++;

            // 更新规则
            var ruleEntity = await NumberRuleRepository.GetByIdAsync(numberRule.NumberRuleId);
            if (ruleEntity != null)
            {
                ruleEntity.CurrentSequence = numberRule.CurrentSequence;
                ruleEntity.LastResetDate = numberRule.LastResetDate;
                ruleEntity.LastUsedTime = numberRule.LastUsedTime;
                ruleEntity.UsageCount = numberRule.UsageCount;
                await NumberRuleRepository.UpdateAsync(ruleEntity);
            }

            // 生成单号
            var number = GenerateNumberByRule(numberRule, customParams);

            // 检查重复
            if (numberRule.AllowDuplicate == 0)
            {
                var maxRetries = 10;
                var retryCount = 0;
                while (await IsNumberExistsAsync(number, "", "") && retryCount < maxRetries)
                {
                    retryCount++;
                    numberRule.CurrentSequence += numberRule.SequenceStep;
                    number = GenerateNumberByRule(numberRule, customParams);
                }

                if (retryCount >= maxRetries)
                    throw new HbtException(L("NumberGenerator.MaxRetriesExceeded"));
            }

            return number;
        }

        /// <summary>
        /// 验证单号是否已存在
        /// </summary>
        /// <param name="number">单号</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <returns>是否存在</returns>
        public async Task<bool> IsNumberExistsAsync(string number, string tableName, string columnName)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                return false;

            try
            {
                var sql = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @number";
                var count = await _dbContext.Ado.GetIntAsync(sql, new { number });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("验证单号是否存在时发生错误 - 表名: {TableName}, 字段名: {ColumnName}, 单号: {Number}", 
                    tableName, columnName, number, ex);
                return false;
            }
        }

        /// <summary>
        /// 获取规则信息
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <returns>规则信息</returns>
        public async Task<HbtNumberRuleDto> GetRuleAsync(string numberRuleCode, string companyCode)
        {
            var numberRule = await NumberRuleRepository.GetFirstAsync(x => 
                x.CompanyCode == companyCode && 
                x.NumberRuleCode == numberRuleCode &&
                x.Status == 0);

            return numberRule?.Adapt<HbtNumberRuleDto>();
        }

        /// <summary>
        /// 检查是否需要重置序列号
        /// </summary>
        /// <param name="numberRule">单号规则</param>
        /// <returns>是否需要重置</returns>
        private bool ShouldResetSequence(HbtNumberRuleDto numberRule)
        {
            if (numberRule.SequenceResetRule == 1) // 不重置
                return false;

            if (numberRule.LastResetDate == null)
                return true;

            var today = DateTime.Today;
            var lastReset = numberRule.LastResetDate.Value.Date;

            switch (numberRule.SequenceResetRule)
            {
                case 2: // 按年重置
                    return lastReset.Year != today.Year;
                case 3: // 按月重置
                    return lastReset.Year != today.Year || lastReset.Month != today.Month;
                case 4: // 按日重置
                    return lastReset != today;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 根据规则生成单号
        /// </summary>
        /// <param name="numberRule">单号规则</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>生成的单号</returns>
        private string GenerateNumberByRule(HbtNumberRuleDto numberRule, HbtNumberGeneratorParams customParams)
        {
            var now = customParams?.CustomDate ?? DateTime.Now;
            var number = numberRule.NumberFormatTemplate;

            // 替换前缀
            var prefix = customParams?.CustomPrefix ?? numberRule.NumberPrefix;
            if (!string.IsNullOrEmpty(prefix))
                number = number.Replace("{PREFIX}", prefix);

            // 替换后缀
            var suffix = customParams?.CustomSuffix ?? numberRule.NumberSuffix;
            if (!string.IsNullOrEmpty(suffix))
                number = number.Replace("{SUFFIX}", suffix);

            // 替换日期
            var dateStr = now.ToString(numberRule.DateFormat);
            number = number.Replace("{DATE}", dateStr);

            // 替换序列号
            var sequence = customParams?.CustomSequence ?? numberRule.CurrentSequence;
            var sequenceStr = sequence.ToString().PadLeft(numberRule.SequenceLength, '0');
            number = number.Replace("{SEQUENCE}", sequenceStr);

            // 替换公司代码
            if (numberRule.IncludeCompanyCode == 1)
                number = number.Replace("{COMPANY}", numberRule.CompanyCode);

            // 替换部门代码
            if (numberRule.IncludeDepartmentCode == 1 && !string.IsNullOrEmpty(customParams?.DepartmentCode))
                number = number.Replace("{DEPARTMENT}", customParams.DepartmentCode);

            // 替换用户代码
            if (numberRule.IncludeUserCode == 1 && !string.IsNullOrEmpty(customParams?.UserCode))
                number = number.Replace("{USER}", customParams.UserCode);

            // 替换时间组件
            if (numberRule.IncludeYear == 1)
                number = number.Replace("{YEAR}", now.Year.ToString());
            if (numberRule.IncludeMonth == 1)
                number = number.Replace("{MONTH}", now.Month.ToString().PadLeft(2, '0'));
            if (numberRule.IncludeDay == 1)
                number = number.Replace("{DAY}", now.Day.ToString().PadLeft(2, '0'));
            if (numberRule.IncludeHour == 1)
                number = number.Replace("{HOUR}", now.Hour.ToString().PadLeft(2, '0'));
            if (numberRule.IncludeMinute == 1)
                number = number.Replace("{MINUTE}", now.Minute.ToString().PadLeft(2, '0'));
            if (numberRule.IncludeSecond == 1)
                number = number.Replace("{SECOND}", now.Second.ToString().PadLeft(2, '0'));
            if (numberRule.IncludeMillisecond == 1)
                number = number.Replace("{MILLISECOND}", now.Millisecond.ToString().PadLeft(3, '0'));

            // 替换随机数
            if (numberRule.IncludeRandom == 1)
            {
                var randomStr = customParams?.CustomRandom;
                if (string.IsNullOrEmpty(randomStr))
                {
                    var random = new Random();
                    randomStr = random.Next(0, (int)Math.Pow(10, numberRule.RandomLength)).ToString().PadLeft(numberRule.RandomLength, '0');
                }
                number = number.Replace("{RANDOM}", randomStr);
            }

            // 替换校验位
            if (numberRule.IncludeCheckDigit == 1)
            {
                var checkDigit = CalculateCheckDigit(number, numberRule.CheckDigitAlgorithm);
                number = number.Replace("{CHECK}", checkDigit.ToString());
            }

            return number;
        }

        /// <summary>
        /// 计算校验位
        /// </summary>
        /// <param name="number">单号</param>
        /// <param name="algorithm">算法</param>
        /// <returns>校验位</returns>
        private int CalculateCheckDigit(string number, int algorithm)
        {
            switch (algorithm)
            {
                case 1: // 简单求和
                    return number.Sum(c => c - '0') % 10;
                case 2: // 加权求和
                    var sum = 0;
                    for (int i = 0; i < number.Length; i++)
                    {
                        sum += (number[i] - '0') * (i + 1);
                    }
                    return sum % 10;
                case 3: // 模运算
                    var num = long.Parse(number);
                    return (int)(num % 10);
                default:
                    return 0;
            }
        }
    }
} 