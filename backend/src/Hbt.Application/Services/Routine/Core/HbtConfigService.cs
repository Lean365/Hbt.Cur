#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 系统配置服务实现 - 使用仓储工厂模式
//===================================================================

using System.Linq.Expressions;
using Hbt.Common.Utils;
using Hbt.Domain.IServices.Extensions;
using Hbt.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Hbt.Domain.Entities.Routine.Core;
using Hbt.Application.Dtos.Routine.Core;

namespace Hbt.Application.Services.Core
{
    /// <summary>
    /// 系统配置服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtConfigService : HbtBaseService, IHbtConfigService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取配置仓储
        /// </summary>
        private IHbtRepository<HbtConfig> ConfigRepository => _repositoryFactory.GetBusinessRepository<HbtConfig>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtConfigService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
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

            var result = await ConfigRepository.GetPagedListAsync(
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
            var config = await ConfigRepository.GetByIdAsync(configId);
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
            await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigName", input.ConfigName);
            await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigKey", input.ConfigKey);
            await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigValue", input.ConfigValue);

            var config = input.Adapt<HbtConfig>();
            return await ConfigRepository.CreateAsync(config) > 0 ? config.Id : throw new HbtException(L("Core.Config.CreateFailed"));
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtConfigUpdateDto input)
        {
            var config = await ConfigRepository.GetByIdAsync(input.ConfigId)
                ?? throw new HbtException(L("Core.Config.NotFound", input.ConfigId));

            // 验证字段是否已存在
            if (config.ConfigName != input.ConfigName)
                await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigName", input.ConfigName, input.ConfigId);
            if (config.ConfigKey != input.ConfigKey)
                await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigKey", input.ConfigKey, input.ConfigId);
            if (config.ConfigValue != input.ConfigValue)
                await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigValue", input.ConfigValue, input.ConfigId);

            input.Adapt(config);
            return await ConfigRepository.UpdateAsync(config) > 0;
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long configId)
        {
            var config = await ConfigRepository.GetByIdAsync(configId)
                ?? throw new HbtException(L("Core.Config.NotFound", configId));

            if (config.IsBuiltin == 1)
                throw new HbtException(L("Core.Config.CannotDeleteBuiltin"));

            return await ConfigRepository.DeleteAsync(configId) > 0;
        }

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] configIds)
        {
            if (configIds == null || configIds.Length == 0) return false;
            return await ConfigRepository.DeleteRangeAsync(configIds.Cast<object>().ToList()) > 0;
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
                    await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigName", config.ConfigName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigKey", config.ConfigKey);
                    await HbtValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "ConfigValue", config.ConfigValue);

                    var entity = config.Adapt<HbtConfig>();
                    await ConfigRepository.CreateAsync(entity);
                    success++;
                    _logger.Info($"成功导入配置：{config.ConfigName}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入配置失败：{config.ConfigName}, 错误：{ex.Message}");
                }
            }

            _logger.Info($"配置导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtConfigQueryDto query, string sheetName = "HbtConfig")
        {
            var list = await ConfigRepository.GetListAsync(KpConfigQueryExpression(query));
            return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtConfigExportDto>>(), sheetName, L("Core.Config.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtConfig")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtConfigImportDto>(sheetName);
        }

        /// <summary>
        /// 更新配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtConfigStatusDto input)
        {
            var config = await ConfigRepository.GetByIdAsync(input.ConfigId)
                ?? throw new HbtException(L("Core.Config.NotFound", input.ConfigId));

            config.Status = input.Status;
            return await ConfigRepository.UpdateAsync(config) > 0;
        }

        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置值</returns>
        public async Task<string?> GetConfigValueAsync(string configKey)
        {
            var config = await ConfigRepository.GetFirstAsync(x => x.ConfigKey == configKey && x.Status == 0);
            return config?.ConfigValue;
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <returns>是否成功</returns>
        public async Task SetConfigValueAsync(string configKey, string configValue)
        {
            var config = await ConfigRepository.GetFirstAsync(x => x.ConfigKey == configKey);
            if (config == null)
            {
                config = new HbtConfig
                {
                    ConfigKey = configKey,
                    ConfigValue = configValue,
                    ConfigName = configKey,
                    Status = 0,
                    IsBuiltin = 0,
                    IsEncrypted = 0,
                    OrderNum = 0
                };
                await ConfigRepository.CreateAsync(config);
            }
            else
            {
                config.ConfigValue = configValue;
                await ConfigRepository.UpdateAsync(config);
            }
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns>配置字典</returns>
        public async Task<Dictionary<string, string>> GetAllConfigsAsync()
        {
            var configs = await ConfigRepository.GetListAsync(x => x.Status == 0);
            return configs.ToDictionary(x => x.ConfigKey, x => x.ConfigValue);
        }
    }
}