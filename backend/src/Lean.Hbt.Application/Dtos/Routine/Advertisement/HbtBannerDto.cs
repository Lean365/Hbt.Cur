#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBannerDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 横幅广告DTO
//===================================================================

namespace Lean.Hbt.Application.Dtos.Routine.Advertisement
{
    /// <summary>
    /// 横幅广告DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class HbtBannerDto
    {
        /// <summary>
        /// 横幅标题
        /// </summary>
        public string BannerTitle { get; set; } = string.Empty;

        /// <summary>
        /// 横幅副标题
        /// </summary>
        public string? BannerSubtitle { get; set; }

        /// <summary>
        /// 横幅描述
        /// </summary>
        public string? BannerDescription { get; set; }

        /// <summary>
        /// 横幅图片URL
        /// </summary>
        public string BannerImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 横幅链接URL
        /// </summary>
        public string? BannerLinkUrl { get; set; }

        /// <summary>
        /// 横幅位置
        /// </summary>
        public string BannerPosition { get; set; } = string.Empty;

        /// <summary>
        /// 横幅类型
        /// </summary>
        public string BannerType { get; set; } = string.Empty;

        /// <summary>
        /// 横幅状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 横幅审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int BannerAuditStatus { get; set; } = 0;

        /// <summary>
        /// 横幅审核备注
        /// </summary>
        public string? BannerAuditRemark { get; set; }

        /// <summary>
        /// 横幅审核人
        /// </summary>
        public string? BannerAuditorBy { get; set; }

        /// <summary>
        /// 横幅审核时间
        /// </summary>
        public DateTime? BannerAuditTime { get; set; }

        /// <summary>
        /// 横幅发布时间
        /// </summary>
        public DateTime? BannerPublishTime { get; set; }

        /// <summary>
        /// 横幅下线时间
        /// </summary>
        public DateTime? BannerOfflineTime { get; set; }

        /// <summary>
        /// 横幅开始时间
        /// </summary>
        public DateTime? BannerStartTime { get; set; }

        /// <summary>
        /// 横幅结束时间
        /// </summary>
        public DateTime? BannerEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int BannerClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int BannerShowCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 横幅编辑人
        /// </summary>
        public string? BannerEditorBy { get; set; }

        /// <summary>
        /// 横幅编辑时间
        /// </summary>
        public DateTime? BannerEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal BannerPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int BannerBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal BannerCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal BannerCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal BannerCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BannerBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal BannerSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal BannerRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        public int BannerBillingStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 横幅广告查询DTO
    /// </summary>
    public class HbtBannerQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 横幅标题
        /// </summary>
        public string? BannerTitle { get; set; }

        /// <summary>
        /// 横幅位置
        /// </summary>
        public string? BannerPosition { get; set; }

        /// <summary>
        /// 横幅类型
        /// </summary>
        public string? BannerType { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? BannerAuditStatus { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? BannerIsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? BannerIsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? BannerIsHot { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 横幅广告创建DTO
    /// </summary>
    public class HbtBannerCreateDto
    {
        /// <summary>
        /// 横幅标题
        /// </summary>
        public string BannerTitle { get; set; } = string.Empty;

        /// <summary>
        /// 横幅副标题
        /// </summary>
        public string? BannerSubtitle { get; set; }

        /// <summary>
        /// 横幅描述
        /// </summary>
        public string? BannerDescription { get; set; }

        /// <summary>
        /// 横幅图片URL
        /// </summary>
        public string BannerImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 横幅链接URL
        /// </summary>
        public string? BannerLinkUrl { get; set; }

        /// <summary>
        /// 横幅位置
        /// </summary>
        public string BannerPosition { get; set; } = string.Empty;

        /// <summary>
        /// 横幅类型
        /// </summary>
        public string BannerType { get; set; } = string.Empty;

        /// <summary>
        /// 横幅状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 横幅开始时间
        /// </summary>
        public DateTime? BannerStartTime { get; set; }

        /// <summary>
        /// 横幅结束时间
        /// </summary>
        public DateTime? BannerEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal BannerPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int BannerBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal BannerCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal BannerCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal BannerCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BannerBudget { get; set; } = 0;
    }

    /// <summary>
    /// 横幅广告更新DTO
    /// </summary>
    public class HbtBannerUpdateDto
    {
        /// <summary>
        /// 横幅ID
        /// </summary>
        public long BannerId { get; set; }

        /// <summary>
        /// 横幅标题
        /// </summary>
        public string BannerTitle { get; set; } = string.Empty;

        /// <summary>
        /// 横幅副标题
        /// </summary>
        public string? BannerSubtitle { get; set; }

        /// <summary>
        /// 横幅描述
        /// </summary>
        public string? BannerDescription { get; set; }

        /// <summary>
        /// 横幅图片URL
        /// </summary>
        public string BannerImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 横幅链接URL
        /// </summary>
        public string? BannerLinkUrl { get; set; }

        /// <summary>
        /// 横幅位置
        /// </summary>
        public string BannerPosition { get; set; } = string.Empty;

        /// <summary>
        /// 横幅类型
        /// </summary>
        public string BannerType { get; set; } = string.Empty;

        /// <summary>
        /// 横幅状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 横幅开始时间
        /// </summary>
        public DateTime? BannerStartTime { get; set; }

        /// <summary>
        /// 横幅结束时间
        /// </summary>
        public DateTime? BannerEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int BannerIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal BannerPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int BannerBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal BannerCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal BannerCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal BannerCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BannerBudget { get; set; } = 0;
    }

    /// <summary>
    /// 横幅广告状态DTO
    /// </summary>
    public class HbtBannerStatusDto
    {
        /// <summary>
        /// 横幅ID
        /// </summary>
        public long BannerId { get; set; }

        /// <summary>
        /// 横幅状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; }
    }

            /// <summary>
        /// 横幅广告审核DTO
        /// </summary>
        public class HbtBannerAuditDto
        {
            /// <summary>
            /// 横幅ID
            /// </summary>
            public long BannerId { get; set; }

            /// <summary>
            /// 横幅审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
            /// </summary>
            public int BannerAuditStatus { get; set; }

            /// <summary>
            /// 横幅审核备注
            /// </summary>
            public string? BannerAuditRemark { get; set; }
        }

        /// <summary>
        /// 横幅广告计费统计DTO
        /// </summary>
        public class HbtBannerBillingStatsDto
        {
            /// <summary>
            /// 横幅ID
            /// </summary>
            public long BannerId { get; set; }

            /// <summary>
            /// 总展示次数
            /// </summary>
            public int TotalShows { get; set; }

            /// <summary>
            /// 总点击次数
            /// </summary>
            public int TotalClicks { get; set; }

            /// <summary>
            /// 总行动次数
            /// </summary>
            public int TotalActions { get; set; }

            /// <summary>
            /// 点击率
            /// </summary>
            public decimal ClickRate { get; set; }

            /// <summary>
            /// 行动率
            /// </summary>
            public decimal ActionRate { get; set; }

            /// <summary>
            /// 总费用
            /// </summary>
            public decimal TotalCost { get; set; }

            /// <summary>
            /// 平均每次展示费用
            /// </summary>
            public decimal AvgShowCost { get; set; }

            /// <summary>
            /// 平均每次点击费用
            /// </summary>
            public decimal AvgClickCost { get; set; }

            /// <summary>
            /// 平均每次行动费用
            /// </summary>
            public decimal AvgActionCost { get; set; }

            /// <summary>
            /// 预算使用率
            /// </summary>
            public decimal BudgetUsageRate { get; set; }
        }
    } 