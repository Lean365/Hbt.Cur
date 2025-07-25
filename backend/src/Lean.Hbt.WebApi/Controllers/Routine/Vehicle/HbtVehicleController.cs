//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtVehicleController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 用车控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Application.Services.Routine;

namespace Lean.Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 用车控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "用车管理")]
    [ApiController]
    [ApiModule("routine", "日常办公")]
    public class HbtVehicleController : HbtBaseController
    {
        private readonly IHbtVehicleService _vehicleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="vehicleService">用车服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtVehicleController(
            IHbtVehicleService vehicleService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, currentUser, localization)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// 获取用车分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用车分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:vehicle:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtVehicleQueryDto query)
        {
            var result = await _vehicleService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取用车详情
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>用车详情</returns>
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetByIdAsync(long vehicleId)
        {
            var result = await _vehicleService.GetByIdAsync(vehicleId);
            return Success(result);
        }

        /// <summary>
        /// 创建用车
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用车ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HbtVehicleCreateDto input)
        {
            var result = await _vehicleService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新用车
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtVehicleUpdateDto input)
        {
            var result = await _vehicleService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除用车
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteAsync(long vehicleId)
        {
            var result = await _vehicleService.DeleteAsync(vehicleId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除用车
        /// </summary>
        /// <param name="vehicleIds">用车ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] vehicleIds)
        {
            var result = await _vehicleService.BatchDeleteAsync(vehicleIds);
            return Success(result);
        }

        /// <summary>
        /// 导入用车数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "用车信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _vehicleService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出用车数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtVehicleQueryDto query, [FromQuery] string sheetName = "用车信息")
        {
            var result = await _vehicleService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "用车信息")
        {
            var result = await _vehicleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 