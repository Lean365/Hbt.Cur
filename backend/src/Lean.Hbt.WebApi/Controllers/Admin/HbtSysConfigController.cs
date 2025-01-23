//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSysConfigController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Infrastructure.Security.Attributes;
using Lean.Hbt.Domain.IServices.Admin;

namespace Lean.Hbt.WebApi.Controllers.Admin
{
    /// <summary>
    /// 系统配置控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class HbtSysConfigController : HbtBaseController
    {
        private readonly IHbtSysConfigService _configService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configService">系统配置服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtSysConfigController(IHbtSysConfigService configService, IHbtLocalizationService localization) : base(localization)
        {
            _configService = configService;
        }

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        [HttpGet]
        [HbtPermission("system:config:list")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtSysConfigQueryDto query)
        {
            var result = await _configService.GetPagedListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>系统配置详情</returns>
        [HttpGet("{configId}")]
        [HbtPermission("system:config:query")]
        public async Task<IActionResult> GetAsync(long configId)
        {
            var result = await _configService.GetAsync(configId);
            return Success(result);
        }

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        [HttpPost]
        [HbtPermission("system:config:insert")]
        public async Task<IActionResult> InsertAsync([FromBody] HbtSysConfigCreateDto input)
        {
            var result = await _configService.InsertAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPermission("system:config:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtSysConfigUpdateDto input)
        {
            var result = await _configService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{configId}")]
        [HbtPermission("system:config:delete")]
        public async Task<IActionResult> DeleteAsync(long configId)
        {
            var result = await _configService.DeleteAsync(configId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [HbtPermission("system:config:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] configIds)
        {
            var result = await _configService.BatchDeleteAsync(configIds);
            return Success(result);
        }

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="configs">系统配置数据列表</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPermission("system:config:import")]
        public async Task<IActionResult> ImportAsync([FromBody] List<HbtSysConfigImportDto> configs)
        {
            var result = await _configService.ImportAsync(configs);
            return Success(result);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        [HttpGet("export")]
        [HbtPermission("system:config:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtSysConfigQueryDto query)
        {
            var result = await _configService.ExportAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        [HttpGet("template")]
        [HbtPermission("system:config:query")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _configService.GetTemplateAsync();
            return Success(result);
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{configId}/status")]
        [HbtPermission("system:config:update")]
        public async Task<IActionResult> UpdateStatusAsync(long configId, [FromQuery] HbtStatus status)
        {
            var input = new HbtSysConfigStatusDto
            {
                ConfigId = configId,
                Status = status
            };
            var result = await _configService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
} 