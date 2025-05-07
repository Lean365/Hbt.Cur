//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Application.Services.Core;

namespace Lean.Hbt.WebApi.Controllers.Core
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
        /// <param name="localization">本地化服务</param>
        /// <param name="logger">日志服务</param>
        public HbtConfigController(
            IHbtConfigService configService,
            IHbtLocalizationService localization,
            IHbtLogger logger) : base(localization, logger)
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
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [HbtPerm("core:config:import")]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file, [FromForm] string sheetName)
        {
            try
            {
                _logger.Info($"开始导入配置，文件名：{file?.FileName}，大小：{file?.Length}，工作表：{sheetName}");

                if (file == null || file.Length == 0)
                {
                    _logger.Warn("上传的文件为空");
                    return BadRequest(_localization.L("FileUploadEmpty"));
                }

                // 验证文件类型
                var allowedExtensions = new[] { ".xlsx", ".xls" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    _logger.Warn($"不支持的文件类型：{fileExtension}");
                    return BadRequest(_localization.L("FileTypeNotSupported"));
                }

                // 验证文件大小（10MB）
                const int maxFileSize = 10 * 1024 * 1024;
                if (file.Length > maxFileSize)
                {
                    _logger.Warn($"文件大小超过限制：{file.Length} 字节");
                    return BadRequest(_localization.L("FileSizeExceeded"));
                }

                // 使用ExcelPackage读取文件
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var (successCount, failureCount) = await _configService.ImportAsync(stream, sheetName);
                _logger.Info($"导入完成，成功：{successCount}，失败：{failureCount}");
                return Ok(new { 
                    success = true,
                    message = $"导入完成，成功：{successCount}，失败：{failureCount}",
                    successCount, 
                    failureCount 
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"导入配置失败：{ex.Message}", ex);
                return StatusCode(500, new { 
                    success = false,
                    message = _localization.L("ImportFailed"),
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("core:config:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtConfigQueryDto query, [FromQuery] string sheetName = "HbtConfig")
        {
            var (fileName, content) = await _configService.ExportAsync(query, sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("core:config:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "HbtConfig")
        {
            var (fileName, content) = await _configService.GetTemplateAsync(sheetName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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