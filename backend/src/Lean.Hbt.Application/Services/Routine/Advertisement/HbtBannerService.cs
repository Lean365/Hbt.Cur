#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBannerService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 横幅广告服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Advertisement;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Enums;
using Mapster;
using SqlSugar;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Routine.Advertisement;

namespace Lean.Hbt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 横幅广告服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class HbtBannerService : HbtBaseService, IHbtBannerService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtBannerService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取横幅广告仓储
        /// </summary>
        private IHbtRepository<HbtBanner> BannerRepository => _repositoryFactory.GetBusinessRepository<HbtBanner>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtBanner, bool>> BannerQueryExpression(HbtBannerQueryDto query)
        {
            return Expressionable.Create<HbtBanner>()
                .AndIF(!string.IsNullOrEmpty(query.BannerTitle), x => x.BannerTitle.Contains(query.BannerTitle!))
                .AndIF(!string.IsNullOrEmpty(query.BannerPosition), x => x.BannerPosition == query.BannerPosition)
                .AndIF(!string.IsNullOrEmpty(query.BannerType), x => x.BannerType == query.BannerType)
                .AndIF(!string.IsNullOrEmpty(query.Keyword), x => 
                    x.BannerTitle.Contains(query.Keyword!) ||
                    x.BannerSubtitle != null && x.BannerSubtitle.Contains(query.Keyword!) ||
                    x.BannerDescription != null && x.BannerDescription.Contains(query.Keyword!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(query.BannerAuditStatus.HasValue, x => x.BannerAuditStatus == query.BannerAuditStatus!.Value)
                .AndIF(query.BannerIsTop.HasValue, x => x.BannerIsTop == query.BannerIsTop!.Value)
                .AndIF(query.BannerIsRecommend.HasValue, x => x.BannerIsRecommend == query.BannerIsRecommend!.Value)
                .AndIF(query.BannerIsHot.HasValue, x => x.BannerIsHot == query.BannerIsHot!.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取横幅广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>横幅广告分页列表</returns>
        public async Task<HbtPagedResult<HbtBannerDto>> GetListAsync(HbtBannerQueryDto query)
        {
            query ??= new HbtBannerQueryDto();

            _logger.Info($"查询横幅广告列表，参数：BannerTitle={query.BannerTitle}, BannerPosition={query.BannerPosition}, Keyword={query.Keyword}");

            var result = await BannerRepository.GetPagedListAsync(
                BannerQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Desc);

            _logger.Info($"查询横幅广告列表，共获取到 {result.TotalNum} 条记录");

            return new HbtPagedResult<HbtBannerDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtBannerDto>>()
            };
        }

        /// <summary>
        /// 获取横幅广告详情
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>横幅广告详情</returns>
        public async Task<HbtBannerDto> GetByIdAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            return banner.Adapt<HbtBannerDto>();
        }

        /// <summary>
        /// 创建横幅广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>横幅广告ID</returns>
        public async Task<long> CreateAsync(HbtBannerCreateDto input)
        {
            var banner = input.Adapt<HbtBanner>();
            banner.CreateBy = _currentUser.UserName ?? "system";
            banner.CreateTime = DateTime.Now;

            // 如果状态为已发布，设置发布时间
            if (banner.Status == 1)
            {
                banner.BannerPublishTime = DateTime.Now;
            }

            // 初始化计费相关字段
            banner.BannerRemainingAmount = banner.BannerBudget;

            var result = await BannerRepository.CreateAsync(banner);
            return result > 0 ? banner.Id : throw new HbtException("创建横幅广告失败");
        }

        /// <summary>
        /// 更新横幅广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtBannerUpdateDto input)
        {
            var existingBanner = await BannerRepository.GetByIdAsync(input.BannerId);
            if (existingBanner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {input.BannerId} 不存在");
            }

            var banner = input.Adapt<HbtBanner>();
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;
            banner.BannerEditorBy = _currentUser.UserName ?? "system";
            banner.BannerEditTime = DateTime.Now;

            // 如果状态从草稿变为已发布，设置发布时间
            if (existingBanner.Status == 0 && banner.Status == 1)
            {
                banner.BannerPublishTime = DateTime.Now;
            }

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 删除横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.DeleteBy = _currentUser.UserName ?? "system";
            banner.DeleteTime = DateTime.Now;
            banner.IsDeleted = 1;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 批量删除横幅广告
        /// </summary>
        /// <param name="bannerIds">横幅广告ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] bannerIds)
        {
            var bannerList = await BannerRepository.GetListAsync(x => bannerIds.Contains(x.Id));
            if (!bannerList.Any())
            {
                return false;
            }

            var currentUser = _currentUser.UserName ?? "system";
            var currentTime = DateTime.Now;

            foreach (var banner in bannerList)
            {
                banner.DeleteBy = currentUser;
                banner.DeleteTime = currentTime;
                banner.IsDeleted = 1;
            }

            var result = await BannerRepository.DeleteRangeAsync(bannerIds.Cast<object>().ToList());
            return result > 0;
        }

        /// <summary>
        /// 更新横幅广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtBannerStatusDto input)
        {
            var banner = await BannerRepository.GetByIdAsync(input.BannerId);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {input.BannerId} 不存在");
            }

            banner.Status = input.Status;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            // 如果状态为已发布，设置发布时间
            if (banner.Status == 1 && !banner.BannerPublishTime.HasValue)
            {
                banner.BannerPublishTime = DateTime.Now;
            }

            // 如果状态为已下线，设置下线时间
            if (banner.Status == 2)
            {
                banner.BannerOfflineTime = DateTime.Now;
            }

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 审核横幅广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AuditAsync(HbtBannerAuditDto input)
        {
            var banner = await BannerRepository.GetByIdAsync(input.BannerId);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {input.BannerId} 不存在");
            }

            banner.BannerAuditStatus = input.BannerAuditStatus;
            banner.BannerAuditRemark = input.BannerAuditRemark;
            banner.BannerAuditorBy = _currentUser.UserName ?? "system";
            banner.BannerAuditTime = DateTime.Now;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 发布横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> PublishAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.Status = 1;
            banner.BannerPublishTime = DateTime.Now;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 下线横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> OfflineAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.Status = 2;
            banner.BannerOfflineTime = DateTime.Now;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 置顶横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetTopAsync(long id, bool isTop)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.BannerIsTop = isTop ? 1 : 0;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 推荐横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetRecommendAsync(long id, bool isRecommend)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.BannerIsRecommend = isRecommend ? 1 : 0;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 热门横幅广告
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetHotAsync(long id, bool isHot)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.BannerIsHot = isHot ? 1 : 0;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 增加点击次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseClickCountAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null) return false;
            
            banner.BannerClickCount++;
            
            // 计算并更新费用
            var cost = await CalculateBillingAsync(id, 2); // 点击
            banner.BannerSpentAmount += cost;
            banner.BannerRemainingAmount = banner.BannerBudget - banner.BannerSpentAmount;
            
            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 增加展示次数
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseShowCountAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null) return false;
            
            banner.BannerShowCount++;
            
            // 计算并更新费用
            var cost = await CalculateBillingAsync(id, 1); // 展示
            banner.BannerSpentAmount += cost;
            banner.BannerRemainingAmount = banner.BannerBudget - banner.BannerSpentAmount;
            
            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 获取热门横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门横幅广告列表</returns>
        public async Task<List<HbtBannerDto>> GetHotBannersAsync(int count = 10)
        {
            var bannerList = await BannerRepository.GetListAsync(
                x => x.BannerIsHot == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return bannerList.Take(count).OrderByDescending(x => x.BannerClickCount).Adapt<List<HbtBannerDto>>();
        }

        /// <summary>
        /// 获取推荐横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐横幅广告列表</returns>
        public async Task<List<HbtBannerDto>> GetRecommendBannersAsync(int count = 10)
        {
            var bannerList = await BannerRepository.GetListAsync(
                x => x.BannerIsRecommend == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return bannerList.Take(count).OrderByDescending(x => x.BannerClickCount).Adapt<List<HbtBannerDto>>();
        }

        /// <summary>
        /// 获取置顶横幅广告列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>置顶横幅广告列表</returns>
        public async Task<List<HbtBannerDto>> GetTopBannersAsync(int count = 5)
        {
            var bannerList = await BannerRepository.GetListAsync(
                x => x.BannerIsTop == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return bannerList.Take(count).OrderByDescending(x => x.OrderNum).Adapt<List<HbtBannerDto>>();
        }

        /// <summary>
        /// 根据位置获取横幅广告列表
        /// </summary>
        /// <param name="position">横幅位置</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        public async Task<List<HbtBannerDto>> GetBannersByPositionAsync(string position, int count = 20)
        {
            var bannerList = await BannerRepository.GetListAsync(
                x => x.BannerPosition == position && x.Status == 1 && x.IsDeleted == 0
            );

            return bannerList.Take(count).OrderByDescending(x => x.CreateTime).Adapt<List<HbtBannerDto>>();
        }

        /// <summary>
        /// 根据类型获取横幅广告列表
        /// </summary>
        /// <param name="type">横幅类型</param>
        /// <param name="count">获取数量</param>
        /// <returns>横幅广告列表</returns>
        public async Task<List<HbtBannerDto>> GetBannersByTypeAsync(string type, int count = 20)
        {
            var bannerList = await BannerRepository.GetListAsync(
                x => x.BannerType == type && x.Status == 1 && x.IsDeleted == 0
            );

            return bannerList.Take(count).OrderByDescending(x => x.CreateTime).Adapt<List<HbtBannerDto>>();
        }

        /// <summary>
        /// 计算广告费用
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="actionType">行为类型 (1: 展示, 2: 点击, 3: 行动)</param>
        /// <returns>计算出的费用</returns>
        public async Task<decimal> CalculateBillingAsync(long id, int actionType)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null) return 0;

            return banner.BannerBillingType switch
            {
                1 => actionType == 1 ? banner.BannerCpmPrice / 1000 : 0, // CPM按千次展示
                2 => actionType == 2 ? banner.BannerCpcPrice : 0, // CPC按点击
                3 => actionType == 3 ? banner.BannerCpaPrice : 0, // CPA按行动
                4 => banner.BannerPrice, // 固定价格
                _ => 0
            };
        }

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <param name="billingStatus">计费状态</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateBillingStatusAsync(long id, int billingStatus)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            banner.BannerBillingStatus = billingStatus;
            banner.UpdateBy = _currentUser.UserName ?? "system";
            banner.UpdateTime = DateTime.Now;

            var result = await BannerRepository.UpdateAsync(banner);
            return result > 0;
        }

        /// <summary>
        /// 获取广告计费统计
        /// </summary>
        /// <param name="id">横幅广告ID</param>
        /// <returns>计费统计信息</returns>
        public async Task<HbtBannerBillingStatsDto> GetBillingStatsAsync(long id)
        {
            var banner = await BannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                throw HbtException.NotFound($"横幅广告ID {id} 不存在");
            }

            var stats = new HbtBannerBillingStatsDto
            {
                BannerId = id,
                TotalShows = banner.BannerShowCount,
                TotalClicks = banner.BannerClickCount,
                TotalActions = 0, // 暂时设为0，后续可以添加行动统计
                TotalCost = banner.BannerSpentAmount,
                BudgetUsageRate = banner.BannerBudget > 0 ? (banner.BannerSpentAmount / banner.BannerBudget) * 100 : 0
            };

            // 计算点击率和行动率
            stats.ClickRate = stats.TotalShows > 0 ? (decimal)stats.TotalClicks / stats.TotalShows * 100 : 0;
            stats.ActionRate = stats.TotalClicks > 0 ? (decimal)stats.TotalActions / stats.TotalClicks * 100 : 0;

            // 计算平均费用
            stats.AvgShowCost = stats.TotalShows > 0 ? stats.TotalCost / stats.TotalShows : 0;
            stats.AvgClickCost = stats.TotalClicks > 0 ? stats.TotalCost / stats.TotalClicks : 0;
            stats.AvgActionCost = stats.TotalActions > 0 ? stats.TotalCost / stats.TotalActions : 0;

            return stats;
        }
    }
} 