//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置服务实现类
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Admin;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 系统配置服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class HbtConfigService : IHbtConfigService
    {
        private readonly IHbtRepository<HbtConfig> _configRepository;
        private readonly IHbtLogger _logger;

        private static readonly HashSet<string> EncryptedKeys = new()
        {
            "connectionStrings.default"
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtConfigService(
            IHbtRepository<HbtConfig> configRepository,
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
        public async Task<HbtPagedResult<HbtConfigDto>> GetListAsync(HbtConfigQueryDto query)
        {
            var exp = Expressionable.Create<HbtConfig>();

            if (!string.IsNullOrEmpty(query?.ConfigName))
                exp.And(x => x.ConfigName.Contains(query.ConfigName));

            if (!string.IsNullOrEmpty(query?.ConfigKey))
                exp.And(x => x.ConfigKey.Contains(query.ConfigKey));

            if (query?.ConfigBuiltin.HasValue == true)
                exp.And(x => x.ConfigBuiltin == query.ConfigBuiltin.Value);

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var result = await _configRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtConfigDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
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
            if (config == null)
                throw new HbtException($"系统配置不存在: {configId}");

            return config.Adapt<HbtConfigDto>();
        }

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        public async Task<long> CreateAsync(HbtConfigCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.ConfigName))
                throw new HbtException("配置名称不能为空");

            if (string.IsNullOrEmpty(input.ConfigKey))
                throw new HbtException("配置键名不能为空");

            if (string.IsNullOrEmpty(input.ConfigValue))
                throw new HbtException("配置键值不能为空");

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey);

            // 创建配置
            var config = new HbtConfig
            {
                ConfigName = input.ConfigName,
                ConfigKey = input.ConfigKey,
                ConfigValue = input.ConfigValue,
                ConfigBuiltin = input.ConfigBuiltin,
                OrderNum = input.OrderNum,
                Status = input.Status
            };

            var result = await _configRepository.CreateAsync(config);
            if (result <= 0)
                throw new HbtException("创建系统配置失败");

            return config.Id;
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtConfigUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var config = await _configRepository.GetByIdAsync(input.ConfigId);
            if (config == null)
                throw new HbtException($"系统配置不存在: {input.ConfigId}");

            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_configRepository, "ConfigKey", input.ConfigKey, input.ConfigId);

            // 更新配置
            config.ConfigName = input.ConfigName;
            config.ConfigKey = input.ConfigKey;
            config.ConfigValue = input.ConfigValue;
            config.ConfigBuiltin = input.ConfigBuiltin;
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
                throw new HbtException($"系统配置不存在: {configId}");

            if (config.ConfigBuiltin == 1) // 1 表示内置参数
                throw new HbtException("系统内置参数不能删除");

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
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "系统配置信息")
        {
            var success = 0;
            var fail = 0;

            try
            {
                // 1.从Excel导入数据
                var configs = await HbtExcelHelper.ImportAsync<HbtConfigDto>(fileStream, sheetName);
                if (configs == null || !configs.Any())
                    return (0, 0);

                // 2.保存数据
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
                        catch (HbtException ex)
                        {
                            _logger.Warn($"导入系统配置失败: {ex.Message}");
                            fail++;
                            continue;
                        }

                        // 创建配置
                        var newConfig = config.Adapt<HbtConfig>();
                        await _configRepository.CreateAsync(newConfig);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"导入系统配置失败：{ex.Message}", ex);
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"导入系统配置数据失败：{ex.Message}", ex);
                throw new HbtException("导入系统配置数据失败");
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtConfigQueryDto query, string sheetName = "系统配置信息")
        {
            var exp = Expressionable.Create<HbtConfig>();

            if (!string.IsNullOrEmpty(query?.ConfigName))
                exp.And(x => x.ConfigName.Contains(query.ConfigName));

            if (!string.IsNullOrEmpty(query?.ConfigKey))
                exp.And(x => x.ConfigKey.Contains(query.ConfigKey));

            if (query?.ConfigBuiltin.HasValue == true)
                exp.And(x => x.ConfigBuiltin == query.ConfigBuiltin.Value);

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _configRepository.GetListAsync(exp.ToExpression());
            var dtos = list.Adapt<List<HbtConfigDto>>();

            return await HbtExcelHelper.ExportAsync(dtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "系统配置信息")
        {
            var template = new List<HbtConfigDto>
            {
                new HbtConfigDto
                {
                    ConfigName = "示例配置",
                    ConfigKey = "example.key",
                    ConfigValue = "示例值",
                    ConfigBuiltin = 1,
                    OrderNum = 1,
                    Status = 0
                }
            };

            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtConfigStatusDto input)
        {
            var config = await _configRepository.GetByIdAsync(input.ConfigId);
            if (config == null)
                throw new HbtException($"系统配置不存在: {input.ConfigId}");

            config.Status = input.Status;
            var result = await _configRepository.UpdateAsync(config);
            return result > 0;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        public async Task<string> GetConfigValueAsync(string configKey)
        {
            var list = await _configRepository.GetListAsync(x => x.ConfigKey == configKey);
            var config = list.FirstOrDefault();
            return config?.ConfigValue;
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        public async Task SetConfigValueAsync(string configKey, string configValue)
        {
            var list = await _configRepository.GetListAsync(x => x.ConfigKey == configKey);
            var config = list.FirstOrDefault();
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
            var list = await _configRepository.GetListAsync();
            var result = new Dictionary<string, string>();

            foreach (var config in list)
            {
                result[config.ConfigKey] = EncryptedKeys.Contains(config.ConfigKey)
                    ? HbtEncryptUtils.AesDecrypt(config.ConfigValue)
                    : config.ConfigValue;
            }

            return result;
        }
    }
}