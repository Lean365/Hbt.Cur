//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerMonitorController.cs
// 创建者 : Lean365
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控控制器
//===================================================================

using Hbt.Application.Services.Audit;

namespace Hbt.WebApi.Controllers.Audit;

/// <summary>
/// 服务器监控控制器
/// </summary>
[Route("api/[controller]", Name = "服务监控")]
[ApiModule("audit", "审计日志")]
[ApiController]
[Authorize]
public class HbtServerMonitorController : HbtBaseController
{
    private readonly IHbtServerMonitorService _serverMonitorService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serverMonitorService">服务器监控服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtServerMonitorController(
        IHbtServerMonitorService serverMonitorService,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _serverMonitorService = serverMonitorService;
    }

    /// <summary>
    /// 获取服务器基本信息
    /// </summary>
    /// <returns>服务器基本信息</returns>
    [HttpGet("info")]
    [AllowAnonymous]
    public async Task<IActionResult> GetServerInfoAsync()
    {
        var result = await _serverMonitorService.GetServerInfoAsync();
        return Success(result);
    }

    /// <summary>
    /// 获取网络信息
    /// </summary>
    /// <returns>网络信息列表</returns>
    [HttpGet("network")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNetworkInfoAsync()
    {
        var result = await _serverMonitorService.GetNetworkInfoAsync();
        return Success(result);
    }
}