#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBannerController.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 横幅广告控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Hbt.Application.Services.Routine.Advertisement;
using Lean.Hbt.Application.Dtos.Routine.Advertisement;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Lean.Hbt.WebApi.Controllers.Routine.Advertisement
{
    /// <summary>
    /// 横幅广告控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HbtBannerController : ControllerBase
    {
        private readonly IHbtBannerService _bannerService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bannerService">横幅广告服务</param>
        public HbtBannerController(IHbtBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        /// <summary>
        /// 获取横幅广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>横幅广告分页列表</returns>
        [HttpGet("list")]
        public async Task<HbtApiResult<HbtPagedResult<HbtBannerDto>>> GetList([FromQuery] HbtBannerQueryDto query)
        {
            try
            {
                var result = await _bannerService.GetListAsync(query);
                return HbtApiResult<HbtPagedResult<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<HbtPagedResult<HbtBannerDto>>.Error($"获取横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取横幅广告详情
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>横幅广告详情</returns>
        [HttpGet("{id}")]
        public async Task<HbtApiResult<HbtBannerDto>> GetById(long id)
        {
            try
            {
                var result = await _bannerService.GetByIdAsync(id);
                return HbtApiResult<HbtBannerDto>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<HbtBannerDto>.Error($"获取横幅广告详情失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 创建横幅广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>横幅广告ID</returns>
        [HttpPost]
        public async Task<HbtApiResult<long>> Create([FromBody] HbtBannerCreateDto input)
        {
            try
            {
                var result = await _bannerService.CreateAsync(input);
                return HbtApiResult<long>.Success(result, "创建横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<long>.Error($"创建横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新横幅广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<HbtApiResult<bool>> Update([FromBody] HbtBannerUpdateDto input)
        {
            try
            {
                var result = await _bannerService.UpdateAsync(input);
                return HbtApiResult<bool>.Success(result, "更新横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"更新横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 删除横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        public async Task<HbtApiResult<bool>> Delete(long id)
        {
            try
            {
                var result = await _bannerService.DeleteAsync(id);
                return HbtApiResult<bool>.Success(result, "删除横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"删除横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 批量删除横幅广告
        /// </summary>
        /// <param name="bannerIds">横幅广告ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<HbtApiResult<bool>> BatchDelete([FromBody] long[] bannerIds)
        {
            try
            {
                var result = await _bannerService.BatchDeleteAsync(bannerIds);
                return HbtApiResult<bool>.Success(result, "批量删除横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"批量删除横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新横幅广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        public async Task<HbtApiResult<bool>> UpdateStatus([FromBody] HbtBannerStatusDto input)
        {
            try
            {
                var result = await _bannerService.UpdateStatusAsync(input);
                return HbtApiResult<bool>.Success(result, "更新横幅广告状态成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"更新横幅广告状态失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 审核横幅广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("audit")]
        public async Task<HbtApiResult<bool>> Audit([FromBody] HbtBannerAuditDto input)
        {
            try
            {
                var result = await _bannerService.AuditAsync(input);
                return HbtApiResult<bool>.Success(result, "审核横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"审核横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 发布横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/publish")]
        public async Task<HbtApiResult<bool>> Publish(long id)
        {
            try
            {
                var result = await _bannerService.PublishAsync(id);
                return HbtApiResult<bool>.Success(result, "发布横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"发布横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 下线横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/offline")]
        public async Task<HbtApiResult<bool>> Offline(long id)
        {
            try
            {
                var result = await _bannerService.OfflineAsync(id);
                return HbtApiResult<bool>.Success(result, "下线横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"下线横幅广告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 置顶横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/top")]
        public async Task<HbtApiResult<bool>> SetTop(long id, [FromQuery] bool isTop)
        {
            try
            {
                var result = await _bannerService.SetTopAsync(id, isTop);
                return HbtApiResult<bool>.Success(result, isTop ? "置顶横幅广告成功" : "取消置顶横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"设置横幅广告置顶失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 推荐横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/recommend")]
        public async Task<HbtApiResult<bool>> SetRecommend(long id, [FromQuery] bool isRecommend)
        {
            try
            {
                var result = await _bannerService.SetRecommendAsync(id, isRecommend);
                return HbtApiResult<bool>.Success(result, isRecommend ? "推荐横幅广告成功" : "取消推荐横幅广告成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"设置横幅广告推荐失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 热门横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/hot")]
        public async Task<HbtApiResult<bool>> SetHot(long id, [FromQuery] bool isHot)
        {
            try
            {
                var result = await _bannerService.SetHotAsync(id, isHot);
                return HbtApiResult<bool>.Success(result, isHot ? "设置横幅广告热门成功" : "取消横幅广告热门成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"设置横幅广告热门失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 增加点击次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/click")]
        public async Task<HbtApiResult<bool>> IncreaseClickCount(long id)
        {
            try
            {
                var result = await _bannerService.IncreaseClickCountAsync(id);
                return HbtApiResult<bool>.Success(result, "增加点击次数成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"增加点击次数失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 增加展示次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/show")]
        public async Task<HbtApiResult<bool>> IncreaseShowCount(long id)
        {
            try
            {
                var result = await _bannerService.IncreaseShowCountAsync(id);
                return HbtApiResult<bool>.Success(result, "增加展示次数成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"增加展示次数失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取热门横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门横幅广告列表</returns>
        [HttpGet("hot")]
        public async Task<HbtApiResult<List<HbtBannerDto>>> GetHotBanners([FromQuery] int count = 10)
        {
            try
            {
                var result = await _bannerService.GetHotBannersAsync(count);
                return HbtApiResult<List<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<List<HbtBannerDto>>.Error($"获取热门横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取推荐横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐横幅广告列表</returns>
        [HttpGet("recommend")]
        public async Task<HbtApiResult<List<HbtBannerDto>>> GetRecommendBanners([FromQuery] int count = 10)
        {
            try
            {
                var result = await _bannerService.GetRecommendBannersAsync(count);
                return HbtApiResult<List<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<List<HbtBannerDto>>.Error($"获取推荐横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取置顶横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>置顶横幅广告列表</returns>
        [HttpGet("top")]
        public async Task<HbtApiResult<List<HbtBannerDto>>> GetTopBanners([FromQuery] int count = 5)
        {
            try
            {
                var result = await _bannerService.GetTopBannersAsync(count);
                return HbtApiResult<List<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<List<HbtBannerDto>>.Error($"获取置顶横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据位置获取横幅广告列表
        /// </summary>
        /// <param name="position">横幅位置</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        [HttpGet("position/{position}")]
        public async Task<HbtApiResult<List<HbtBannerDto>>> GetBannersByPosition(string position, [FromQuery] int count = 20)
        {
            try
            {
                var result = await _bannerService.GetBannersByPositionAsync(position, count);
                return HbtApiResult<List<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<List<HbtBannerDto>>.Error($"根据位置获取横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据类型获取横幅广告列表
        /// </summary>
        /// <param name="type">横幅类型</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        [HttpGet("type/{type}")]
        public async Task<HbtApiResult<List<HbtBannerDto>>> GetBannersByType(string type, [FromQuery] int count = 20)
        {
            try
            {
                var result = await _bannerService.GetBannersByTypeAsync(type, count);
                return HbtApiResult<List<HbtBannerDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<List<HbtBannerDto>>.Error($"根据类型获取横幅广告列表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 计算广告费用
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="actionType">行为类型 (1: 展示, 2: 点击, 3: 行动)</param>
        /// <returns>计算出的费用</returns>
        [HttpGet("{id}/calculate-billing")]
        public async Task<HbtApiResult<decimal>> CalculateBilling(long id, [FromQuery] int actionType)
        {
            try
            {
                var result = await _bannerService.CalculateBillingAsync(id, actionType);
                return HbtApiResult<decimal>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<decimal>.Error($"计算广告费用失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="billingStatus">计费状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{id}/billing-status")]
        public async Task<HbtApiResult<bool>> UpdateBillingStatus(long id, [FromQuery] int billingStatus)
        {
            try
            {
                var result = await _bannerService.UpdateBillingStatusAsync(id, billingStatus);
                return HbtApiResult<bool>.Success(result, "更新广告计费状态成功");
            }
            catch (Exception ex)
            {
                return HbtApiResult<bool>.Error($"更新广告计费状态失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取广告计费统计
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>计费统计信息</returns>
        [HttpGet("{id}/billing-stats")]
        public async Task<HbtApiResult<HbtBannerBillingStatsDto>> GetBillingStats(long id)
        {
            try
            {
                var result = await _bannerService.GetBillingStatsAsync(id);
                return HbtApiResult<HbtBannerBillingStatsDto>.Success(result);
            }
            catch (Exception ex)
            {
                return HbtApiResult<HbtBannerBillingStatsDto>.Error($"获取广告计费统计失败：{ex.Message}");
            }
        }
    }
} 