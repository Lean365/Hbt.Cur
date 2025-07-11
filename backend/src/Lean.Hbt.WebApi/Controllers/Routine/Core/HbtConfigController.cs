//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Core;
using Lean.Hbt.Application.Services.Core;

namespace Lean.Hbt.WebApi.Controllers.Routine.Core
{
    /// <summary>
    /// 系统配置控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "系统配置")]
    [ApiController]
    [ApiModule("core", "系统管理")]
    public class HbtConfigController : HbtBaseController
    {
        private readonly IHbtConfigService _configService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configService">系统配置服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtConfigController(
            IHbtConfigService configService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _configService = configService;
        }

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("core:config:list")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtConfigQueryDto query)
        {
            var result = await _configService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>系统配置详情</returns>
        [HttpGet("{configId}")]
        [HbtPerm("core:config:query")]
        public async Task<IActionResult> GetByIdAsync(long configId)
        {
            var result = await _configService.GetByIdAsync(configId);
            return Success(result);
        }

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        [HttpPost]
        [HbtPerm("core:config:create")]
        public async Task<IActionResult> CreateAsync([FromBody] HbtConfigCreateDto input)
        {
            var result = await _configService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [HbtPerm("core:config:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtConfigUpdateDto input)
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
        [HbtPerm("core:config:delete")]
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
        [HbtPerm("core:config:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] configIds)
        {
            var result = await _configService.BatchDeleteAsync(configIds);
            return Success(result);
        }

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("core:config:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _configService.ImportAsync(stream, "HbtConfig");
            return Success(new { success, fail }, _localization.L("Core.Config.Import.Success"));
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("core:config:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtConfigQueryDto query)
        {
            var result = await _configService.ExportAsync(query, "HbtConfig");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("core:config:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _configService.GetTemplateAsync("HbtConfig");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{configId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HbtPerm("core:config:update")]
        public async Task<IActionResult> UpdateStatusAsync(long configId, [FromQuery] int status)
        {
            var input = new HbtConfigStatusDto
            {
                ConfigId = configId,
                Status = status
            };
            var result = await _configService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}