//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtConfigController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Application.Services.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Infrastructure.Security.Attributes;

namespace Lean.Hbt.WebApi.Controllers.Admin
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
    [ApiModule("admin", "系统管理")]
    public class HbtConfigController : HbtBaseController
    {
        private readonly IHbtConfigService _configService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configService">系统配置服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtConfigController(IHbtConfigService configService, IHbtLocalizationService localization) : base(localization)
        {
            _configService = configService;
        }

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        [HttpGet]
        [HbtPerm("admin:config:list")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] HbtConfigQueryDto query)
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
        [HbtPerm("admin:config:query")]
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
        [HbtPerm("admin:config:create")]
        public async Task<IActionResult> InsertAsync([FromBody] HbtConfigCreateDto input)
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
        [HbtPerm("admin:config:update")]
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
        [HbtPerm("admin:config:delete")]
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
        [HbtPerm("admin:config:delete")]
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
        [HbtPerm("admin:config:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "系统配置信息")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var result = await _configService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [HbtPerm("admin:config:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtConfigQueryDto query, [FromQuery] string sheetName = "系统配置信息")
        {
            var result = await _configService.ExportAsync(query, sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"系统配置数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [HbtPerm("admin:config:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "系统配置信息")
        {
            var result = await _configService.GetTemplateAsync(sheetName);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"系统配置导入模板_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="Id">配置ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{configId}/status")]
        [HbtPerm("admin:config:update")]
        public async Task<IActionResult> UpdateStatusAsync(long Id, [FromQuery] HbtStatus status)
        {
            var input = new HbtConfigStatusDto
            {
                Id = Id,
                Status = status
            };
            var result = await _configService.UpdateStatusAsync(input);
            return Success(result);
        }
    }
}