//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSysConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置服务实现类
//===================================================================

using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices;
using Mapster;
using SqlSugar;
using Lean.Hbt.Domain.Utils;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 系统配置服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtSysConfigService : IHbtSysConfigService
    {
        private readonly IHbtRepository<HbtSysConfig> _configRepository;
        private readonly IHbtLogger _logger;
        private static readonly HashSet<string> EncryptedKeys = new()
        {
            "connectionStrings.default"
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtSysConfigService(
            IHbtRepository<HbtSysConfig> configRepository,
            IHbtLogger logger)
        {
            _configRepository = configRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        public async Task<HbtPagedResult<HbtSysConfigDto>> GetPagedListAsync(HbtSysConfigQueryDto query)
        {
            var exp = Expressionable.Create<HbtSysConfig>();

            if (!string.IsNullOrEmpty(query?.ConfigName))
                exp.And(x => x.ConfigName.Contains(query.ConfigName));

            if (!string.IsNullOrEmpty(query?.ConfigKey))
                exp.And(x => x.ConfigKey.Contains(query.ConfigKey));

            if (query?.ConfigType.HasValue == true)
                exp.And(x => x.ConfigType == query.ConfigType.Value);

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _configRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtSysConfigDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtSysConfigDto>>()
            };
        }

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>系统配置详情</returns>
        public async Task<HbtSysConfigDto> GetAsync(long configId)
        {
            var config = await _configRepository.GetByIdAsync(configId);
            if (config == null)
                throw new HbtBusinessException($"系统配置不存在: {configId}");

            return config.Adapt<HbtSysConfigDto>();
        }

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        public async Task<long> InsertAsync(HbtSysConfigCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.ConfigName))
                throw new HbtBusinessException("配置名称不能为空");

            if (string.IsNullOrEmpty(input.ConfigKey))
                throw new HbtBusinessException("配置键名不能为空");

            if (string.IsNullOrEmpty(input.ConfigValue))
                throw new HbtBusinessException("配置键值不能为空");

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey);

            // 创建配置
            var config = new HbtSysConfig
            {
                ConfigName = input.ConfigName,
                ConfigKey = input.ConfigKey,
                ConfigValue = input.ConfigValue,
                ConfigType = input.ConfigType,
                OrderNum = input.OrderNum,
                Status = input.Status
            };

            var result = await _configRepository.InsertAsync(config);
            if (result <= 0)
                throw new HbtBusinessException("创建系统配置失败");

            return config.Id;
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtSysConfigUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var config = await _configRepository.GetByIdAsync(input.ConfigId);
            if (config == null)
                throw new HbtBusinessException($"系统配置不存在: {input.ConfigId}");

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey, input.ConfigId);

            // 更新配置
            config.ConfigName = input.ConfigName;
            config.ConfigKey = input.ConfigKey;
            config.ConfigValue = input.ConfigValue;
            config.ConfigType = input.ConfigType;
            config.OrderNum = input.OrderNum;
            config.Status = input.Status;

            var result = await _configRepository.UpdateAsync(config);
            return result > 0;
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long configId)
        {
            var config = await _configRepository.GetByIdAsync(configId);
            if (config == null)
                throw new HbtBusinessException($"系统配置不存在: {configId}");

            var result = await _configRepository.DeleteAsync(configId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] configIds)
        {
            if (configIds == null || configIds.Length == 0)
                return false;

            var result = await _configRepository.DeleteRangeAsync(configIds.Cast<object>().ToList());
            return result > 0;
        }

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="configs">系统配置数据列表</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(List<HbtSysConfigImportDto> configs)
        {
            if (configs == null)
            {
                return (0, 0);
            }

            int success = 0;
            int fail = 0;

            foreach (var config in configs)
            {
                try
                {
                    if (string.IsNullOrEmpty(config.ConfigName) || string.IsNullOrEmpty(config.ConfigKey) || string.IsNullOrEmpty(config.ConfigValue))
                    {
                        _logger.Warn($"导入系统配置失败: 配置名称、键名或键值不能为空");
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在
                    try
                    {
                        await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", config.ConfigKey);
                    }
                    catch (HbtBusinessException ex)
                    {
                        _logger.Warn($"导入系统配置失败: {ex.Message}");
                        fail++;
                        continue;
                    }

                    // 创建配置
                    var newConfig = new HbtSysConfig
                    {
                        ConfigName = config.ConfigName,
                        ConfigKey = config.ConfigKey,
                        ConfigValue = config.ConfigValue,
                        ConfigType = int.TryParse(config.ConfigType, out var configType) ? configType : 0,
                        OrderNum = config.OrderNum,
                        Status = Enum.TryParse<HbtStatus>(config.Status, out var status) ? status : HbtStatus.Normal
                    };

                    await _configRepository.InsertAsync(newConfig);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error($"导入系统配置失败: {ex.Message}", ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        public async Task<List<HbtSysConfigExportDto>> ExportAsync(HbtSysConfigQueryDto query)
        {
            var exp = Expressionable.Create<HbtSysConfig>();

            if (!string.IsNullOrEmpty(query?.ConfigName))
                exp.And(x => x.ConfigName.Contains(query.ConfigName));

            if (!string.IsNullOrEmpty(query?.ConfigKey))
                exp.And(x => x.ConfigKey.Contains(query.ConfigKey));

            if (query?.ConfigType.HasValue == true)
                exp.And(x => x.ConfigType == query.ConfigType.Value);

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var configs = await _configRepository.GetListAsync(exp.ToExpression());
            return configs.Adapt<List<HbtSysConfigExportDto>>();
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        public Task<HbtSysConfigTemplateDto> GetTemplateAsync()
        {
            return Task.FromResult(new HbtSysConfigTemplateDto());
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtSysConfigStatusDto input)
        {
            var config = await _configRepository.GetByIdAsync(input.ConfigId);
            if (config == null)
                throw new HbtBusinessException($"系统配置不存在: {input.ConfigId}");

            config.Status = input.Status;
            var result = await _configRepository.UpdateAsync(config);
            return result > 0;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        public async Task<string> GetConfigValueAsync(string configKey)
        {
            var configs = await _configRepository.GetListAsync(x => x.ConfigKey == configKey);
            var config = configs.FirstOrDefault();
            return config?.ConfigValue;
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        public async Task SetConfigValueAsync(string configKey, string configValue)
        {
            var configs = await _configRepository.GetListAsync(x => x.ConfigKey == configKey);
            var config = configs.FirstOrDefault();
            if (config == null)
                throw new HbtException($"未找到配置项: {configKey}");

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
            var configs = await _configRepository.GetListAsync();
            var result = new Dictionary<string, string>();

            foreach (var config in configs)
            {
                // 如果是加密配置,则解密后返回
                result[config.ConfigKey] = EncryptedKeys.Contains(config.ConfigKey)
                    ? HbtEncryptUtils.AesDecrypt(config.ConfigValue)
                    : config.ConfigValue;
            }

            return result;
        }
    }
} 