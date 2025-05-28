//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFileController.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 文件控制器
//===================================================================

using Lean.Hbt.Application.Dtos.Routine;
using Lean.Hbt.Application.Services.Routine;

namespace Lean.Hbt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 文件控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "文件管理")]
    [ApiController]
    [ApiModule("routine", "日常办公")]
    public class HbtFileController : HbtBaseController
    {
        private readonly IHbtFileService _fileService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileService">文件服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtFileController(
            IHbtFileService fileService,
            IHbtLogger logger,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, currentUser, currentTenant, localization)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件分页列表</returns>
        [HttpGet("list")]
        [HbtPerm("routine:file:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] HbtFileQueryDto query)
        {
            var result = await _fileService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件详情</returns>
        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetByIdAsync(long fileId)
        {
            var result = await _fileService.GetByIdAsync(fileId);
            return Success(result);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] HbtFileCreateDto input)
        {
            var result = await _fileService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] HbtFileUpdateDto input)
        {
            var result = await _fileService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteAsync(long fileId)
        {
            var result = await _fileService.DeleteAsync(fileId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] fileIds)
        {
            var result = await _fileService.BatchDeleteAsync(fileIds);
            return Success(result);
        }

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "文件信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _fileService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] HbtFileQueryDto query, [FromQuery] string sheetName = "文件信息")
        {
            var result = await _fileService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "文件信息")
        {
            var result = await _fileService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input">上传对象</param>
        /// <returns>上传结果</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] HbtFileUploadDto input)
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return Error("未找到上传的文件");
            }

            using var stream = file.OpenReadStream();
            input.File = stream;            
            var result = await _fileService.UploadAsync(stream, input);
            return Success(result);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件流</returns>
        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> DownloadAsync(long fileId)
        {
            var (fileStream, fileName, contentType) = await _fileService.DownloadAsync(fileId);
            return File(fileStream, contentType, fileName);
        }
    }
}