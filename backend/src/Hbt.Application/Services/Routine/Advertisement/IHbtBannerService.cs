#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtBannerService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 横幅广告服务接口
//===================================================================

using Hbt.Application.Dtos.Routine.Advertisement;

namespace Hbt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 横幅广告服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface IHbtBannerService
    {
        /// <summary>
        /// 获取横幅广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>横幅广告分页列表</returns>
        Task<HbtPagedResult<HbtBannerDto>> GetListAsync(HbtBannerQueryDto query);

        /// <summary>
        /// 获取横幅广告详情
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>横幅广告详情</returns>
        Task<HbtBannerDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建横幅广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>横幅广告ID</returns>
        Task<long> CreateAsync(HbtBannerCreateDto input);

        /// <summary>
        /// 更新横幅广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtBannerUpdateDto input);

        /// <summary>
        /// 删除横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除横幅广告
        /// </summary>
        /// <param name="bannerIds">横幅广告ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] bannerIds);

        /// <summary>
        /// 更新横幅广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtBannerStatusDto input);

        /// <summary>
        /// 审核横幅广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> AuditAsync(HbtBannerAuditDto input);

        /// <summary>
        /// 发布横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> PublishAsync(long id);

        /// <summary>
        /// 下线横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> OfflineAsync(long id);

        /// <summary>
        /// 置顶横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>是否成功</returns>
        Task<bool> SetTopAsync(long id, bool isTop);

        /// <summary>
        /// 推荐横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns>是否成功</returns>
        Task<bool> SetRecommendAsync(long id, bool isRecommend);

        /// <summary>
        /// 热门横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns>是否成功</returns>
        Task<bool> SetHotAsync(long id, bool isHot);

        /// <summary>
        /// 增加点击次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseClickCountAsync(long id);

        /// <summary>
        /// 增加展示次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseShowCountAsync(long id);

        /// <summary>
        /// 获取热门横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门横幅广告列表</returns>
        Task<List<HbtBannerDto>> GetHotBannersAsync(int count = 10);

        /// <summary>
        /// 获取推荐横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐横幅广告列表</returns>
        Task<List<HbtBannerDto>> GetRecommendBannersAsync(int count = 10);

        /// <summary>
        /// 获取置顶横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>置顶横幅广告列表</returns>
        Task<List<HbtBannerDto>> GetTopBannersAsync(int count = 5);

        /// <summary>
        /// 根据位置获取横幅广告列表
        /// </summary>
        /// <param name="position">横幅位置</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        Task<List<HbtBannerDto>> GetBannersByPositionAsync(string position, int count = 20);

        /// <summary>
        /// 根据类型获取横幅广告列表
        /// </summary>
        /// <param name="type">横幅类型</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        Task<List<HbtBannerDto>> GetBannersByTypeAsync(string type, int count = 20);

        /// <summary>
        /// 计算广告费用
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="actionType">行为类型 (1: 展示, 2: 点击, 3: 行动)</param>
        /// <returns>计算出的费用</returns>
        Task<decimal> CalculateBillingAsync(long id, int actionType);

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="billingStatus">计费状态</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateBillingStatusAsync(long id, int billingStatus);

        /// <summary>
        /// 获取广告计费统计
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>计费统计信息</returns>
        Task<HbtBannerBillingStatsDto> GetBillingStatsAsync(long id);
    }
} 