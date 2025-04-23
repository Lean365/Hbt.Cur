//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置服务实现类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 系统配置服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtConfigService : HbtBaseService, IHbtConfigService
    {
        private readonly IHbtRepository<HbtConfig> _configRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigService(
            IHbtRepository<HbtConfig> configRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _configRepository = configRepository ?? throw new ArgumentNullException(nameof(configRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtConfig, bool>> KpConfigQueryExpression(HbtConfigQueryDto query)
        {
            return Expressionable.Create<HbtConfig>()
                .AndIF(!string.IsNullOrEmpty(query.ConfigName), x => x.ConfigName!.Contains(query.ConfigName!))
                .AndIF(!string.IsNullOrEmpty(query.ConfigKey), x => x.ConfigKey!.Contains(query.ConfigKey!))
                .AndIF(!string.IsNullOrEmpty(query.ConfigValue), x => x.ConfigValue!.Contains(query.ConfigValue!))
                .AndIF(query.IsBuiltin != -1, x => x.IsBuiltin == query.IsBuiltin)
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .AndIF(query.IsEncrypted != -1, x => x.IsEncrypted == query.IsEncrypted)
                .ToExpression();
        }

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        public async Task<HbtPagedResult<HbtConfigDto>> GetListAsync(HbtConfigQueryDto? query)
        {
            query ??= new HbtConfigQueryDto();

            _logger.Info($"查询系统配置列表，参数：ConfigName={query.ConfigName}, ConfigKey={query.ConfigKey}, ConfigValue={query.ConfigValue}, IsBuiltin={query.IsBuiltin}, Status={query.Status}, IsEncrypted={query.IsEncrypted}");

            var result = await _configRepository.GetPagedListAsync(
                KpConfigQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询系统配置列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtConfigDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtConfigDto>>()
            };
        }

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>系统配置详情</returns>
        public async Task<HbtConfigDto> GetByIdAsync(long configId)
        {
            var config = await _configRepository.GetByIdAsync(configId);
            return config == null ? throw new HbtException(L("Core.Config.NotFound", configId)) : config.Adapt<HbtConfigDto>();
        }

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        public async Task<long> CreateAsync(HbtConfigCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigName", input.ConfigName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey);
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigValue", input.ConfigValue);

            var config = input.Adapt<HbtConfig>();
            return await _configRepository.CreateAsync(config) > 0 ? config.Id : throw new HbtException(L("Core.Config.CreateFailed"));
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtConfigUpdateDto input)
        {
            var config = await _configRepository.GetByIdAsync(input.ConfigId)
                ?? throw new HbtException(L("Core.Config.NotFound", input.ConfigId));

            // 验证字段是否已存在
            if (config.ConfigName != input.ConfigName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigName", input.ConfigName, input.ConfigId);
            if (config.ConfigKey != input.ConfigKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey, input.ConfigId);
            if (config.ConfigValue != input.ConfigValue)
                await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigValue", input.ConfigValue, input.ConfigId);

            input.Adapt(config);
            return await _configRepository.UpdateAsync(config) > 0;
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long configId)
        {
            var config = await _configRepository.GetByIdAsync(configId)
                ?? throw new HbtException(L("Core.Config.NotFound", configId));

            if (config.IsBuiltin == 1)
                throw new HbtException(L("Core.Config.CannotDeleteBuiltin"));

            return await _configRepository.DeleteAsync(configId) > 0;
        }

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] configIds)
        {
            if (configIds == null || configIds.Length == 0) return false;
            return await _configRepository.DeleteRangeAsync(configIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtConfig")
        {
            _logger.Info($"开始导入系统配置数据，工作表名称：{sheetName}");
            
            var configs = await HbtExcelHelper.ImportAsync<HbtConfigImportDto>(fileStream, sheetName);
            if (configs == null || !configs.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何配置数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {configs.Count()} 条配置数据");

            var (success, fail) = (0, 0);
            foreach (var config in configs)
            {
                try
                {
                    _logger.Info($"正在处理配置：{config.ConfigName} (Key: {config.ConfigKey})");
                    
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigName", config.ConfigName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", config.ConfigKey);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigValue", config.ConfigValue);

                    await _configRepository.CreateAsync(config.Adapt<HbtConfig>());
                    success++;
                    _logger.Info($"成功导入配置：{config.ConfigName}");
                }
                catch (Exception ex)
                {
                    _logger.Error($"导入配置失败：{config.ConfigName}，错误信息：{ex.Message}", ex);
                    fail++;
                }
            }

            _logger.Info($"配置导入完成，成功：{success} 条，失败：{fail} 条");
            return (success, fail);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtConfigQueryDto query, string sheetName = "HbtConfig")
        {
            var list = await _configRepository.GetListAsync(KpConfigQueryExpression(query));
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtConfigDto>>(), sheetName, L("Core.Config.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtConfig")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtConfigTemplateDto>(sheetName);
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtConfigStatusDto input)
        {
            var config = await _configRepository.GetByIdAsync(input.ConfigId)
                ?? throw new HbtException(L("Core.Config.NotFound", input.ConfigId));

            config.Status = input.Status;
            return await _configRepository.UpdateAsync(config) > 0;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        public async Task<string?> GetConfigValueAsync(string configKey)
        {
            return (await _configRepository.GetFirstAsync(x => x.ConfigKey == configKey))?.ConfigValue;
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        public async Task SetConfigValueAsync(string configKey, string configValue)
        {
            var config = await _configRepository.GetFirstAsync(x => x.ConfigKey == configKey)
                ?? throw new HbtException(L("Core.Config.KeyNotFound", configKey));

            config.ConfigValue = configValue;
            config.UpdateTime = DateTime.Now;
            config.UpdateBy = "system";

            await _configRepository.UpdateAsync(config);
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        public async Task<Dictionary<string, string>> GetAllConfigsAsync()
        {
            var list = await _configRepository.GetListAsync();
            return list.Where(x => x.ConfigKey != null && x.ConfigValue != null)
                      .ToDictionary(
                          x => x.ConfigKey!,
                          x => x.IsEncrypted == 1 ? HbtEncryptUtils.AesDecrypt(x.ConfigValue!) : x.ConfigValue!
                      );
        }
    }
}