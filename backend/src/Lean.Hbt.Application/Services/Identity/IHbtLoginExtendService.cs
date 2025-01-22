#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLoginExtendService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录扩展信息服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 登录扩展信息服务接口
    /// </summary>
    public interface IHbtLoginExtendService
    {
        /// <summary>
        /// 获取登录扩展信息分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页列表</returns>
        Task<HbtPagedResult<HbtLoginExtendDto>> GetPagedListAsync(HbtLoginExtendPageRequest query);

        /// <summary>
        /// 导出登录扩展信息
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>文件字节数组</returns>
        Task<byte[]> ExportAsync(HbtLoginExtendExportRequest request);

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        /// <param name="request">更新请求</param>
        /// <returns>更新后的登录扩展信息</returns>
        Task<HbtLoginExtendDto> UpdateLoginInfoAsync(HbtLoginExtendUpdateRequest request);

        /// <summary>
        /// 更新用户离线信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>更新后的登录扩展信息</returns>
        Task<HbtLoginExtendDto> UpdateOfflineInfoAsync(long userId);

        /// <summary>
        /// 更新用户在线时段
        /// </summary>
        /// <param name="request">在线时段更新请求</param>
        /// <returns>更新后的登录扩展信息</returns>
        Task<HbtLoginExtendDto> UpdateOnlinePeriodAsync(HbtOnlinePeriodUpdateRequest request);

        /// <summary>
        /// 获取用户登录扩展信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登录扩展信息</returns>
        Task<HbtLoginExtendDto?> GetByUserIdAsync(long userId);
    }
} 