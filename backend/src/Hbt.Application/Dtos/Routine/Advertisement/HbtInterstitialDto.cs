#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInterstitialDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 插屏广告DTO
//===================================================================

namespace Hbt.Cur.Application.Dtos.Routine.Advertisement
{
    /// <summary>
    /// 插屏广告DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class HbtInterstitialDto
    {
        /// <summary>
        /// 插屏ID
        /// </summary>
        public long InterstitialId { get; set; }

        /// <summary>
        /// 插屏标题
        /// </summary>
        public string InterstitialTitle { get; set; } = string.Empty;

        /// <summary>
        /// 插屏副标题
        /// </summary>
        public string? InterstitialSubtitle { get; set; }

        /// <summary>
        /// 插屏描述
        /// </summary>
        public string? InterstitialDescription { get; set; }

        /// <summary>
        /// 插屏图片URL
        /// </summary>
        public string InterstitialImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 插屏链接URL
        /// </summary>
        public string? InterstitialLinkUrl { get; set; }

        /// <summary>
        /// 插屏位置
        /// </summary>
        public string InterstitialPosition { get; set; } = string.Empty;

        /// <summary>
        /// 插屏类型
        /// </summary>
        public string InterstitialType { get; set; } = string.Empty;

        /// <summary>
        /// 插屏状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 插屏审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int InterstitialAuditStatus { get; set; } = 0;

        /// <summary>
        /// 插屏审核备注
        /// </summary>
        public string? InterstitialAuditRemark { get; set; }

        /// <summary>
        /// 插屏审核人
        /// </summary>
        public string? InterstitialAuditorBy { get; set; }

        /// <summary>
        /// 插屏审核时间
        /// </summary>
        public DateTime? InterstitialAuditTime { get; set; }

        /// <summary>
        /// 插屏发布时间
        /// </summary>
        public DateTime? InterstitialPublishTime { get; set; }

        /// <summary>
        /// 插屏下线时间
        /// </summary>
        public DateTime? InterstitialOfflineTime { get; set; }

        /// <summary>
        /// 插屏开始时间
        /// </summary>
        public DateTime? InterstitialStartTime { get; set; }

        /// <summary>
        /// 插屏结束时间
        /// </summary>
        public DateTime? InterstitialEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int InterstitialClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int InterstitialShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        public int InterstitialCloseCount { get; set; } = 0;

        /// <summary>
        /// 跳过次数
        /// </summary>
        public int InterstitialSkipCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 插屏编辑人
        /// </summary>
        public string? InterstitialEditorBy { get; set; }

        /// <summary>
        /// 插屏编辑时间
        /// </summary>
        public DateTime? InterstitialEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal InterstitialPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int InterstitialBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal InterstitialCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal InterstitialCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal InterstitialCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal InterstitialBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal InterstitialSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal InterstitialRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        public int InterstitialBillingStatus { get; set; } = 0;

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
    /// 插屏广告查询DTO
    /// </summary>
    public class HbtInterstitialQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 插屏标题
        /// </summary>
        public string? InterstitialTitle { get; set; }

        /// <summary>
        /// 插屏位置
        /// </summary>
        public string? InterstitialPosition { get; set; }

        /// <summary>
        /// 插屏类型
        /// </summary>
        public string? InterstitialType { get; set; }

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
        public int? InterstitialAuditStatus { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? InterstitialIsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? InterstitialIsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? InterstitialIsHot { get; set; }

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
    /// 插屏广告创建DTO
    /// </summary>
    public class HbtInterstitialCreateDto
    {
        /// <summary>
        /// 插屏标题
        /// </summary>
        public string InterstitialTitle { get; set; } = string.Empty;

        /// <summary>
        /// 插屏副标题
        /// </summary>
        public string? InterstitialSubtitle { get; set; }

        /// <summary>
        /// 插屏描述
        /// </summary>
        public string? InterstitialDescription { get; set; }

        /// <summary>
        /// 插屏图片URL
        /// </summary>
        public string InterstitialImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 插屏链接URL
        /// </summary>
        public string? InterstitialLinkUrl { get; set; }

        /// <summary>
        /// 插屏位置
        /// </summary>
        public string InterstitialPosition { get; set; } = string.Empty;

        /// <summary>
        /// 插屏类型
        /// </summary>
        public string InterstitialType { get; set; } = string.Empty;

        /// <summary>
        /// 插屏状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 插屏开始时间
        /// </summary>
        public DateTime? InterstitialStartTime { get; set; }

        /// <summary>
        /// 插屏结束时间
        /// </summary>
        public DateTime? InterstitialEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal InterstitialPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int InterstitialBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal InterstitialCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal InterstitialCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal InterstitialCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal InterstitialBudget { get; set; } = 0;
    }

    /// <summary>
    /// 插屏广告更新DTO
    /// </summary>
    public class HbtInterstitialUpdateDto
    {
        /// <summary>
        /// 插屏ID
        /// </summary>
        public long InterstitialId { get; set; }

        /// <summary>
        /// 插屏标题
        /// </summary>
        public string InterstitialTitle { get; set; } = string.Empty;

        /// <summary>
        /// 插屏副标题
        /// </summary>
        public string? InterstitialSubtitle { get; set; }

        /// <summary>
        /// 插屏描述
        /// </summary>
        public string? InterstitialDescription { get; set; }

        /// <summary>
        /// 插屏图片URL
        /// </summary>
        public string InterstitialImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 插屏链接URL
        /// </summary>
        public string? InterstitialLinkUrl { get; set; }

        /// <summary>
        /// 插屏位置
        /// </summary>
        public string InterstitialPosition { get; set; } = string.Empty;

        /// <summary>
        /// 插屏类型
        /// </summary>
        public string InterstitialType { get; set; } = string.Empty;

        /// <summary>
        /// 插屏状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 插屏开始时间
        /// </summary>
        public DateTime? InterstitialStartTime { get; set; }

        /// <summary>
        /// 插屏结束时间
        /// </summary>
        public DateTime? InterstitialEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int InterstitialIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal InterstitialPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int InterstitialBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal InterstitialCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal InterstitialCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal InterstitialCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal InterstitialBudget { get; set; } = 0;
    }

    /// <summary>
    /// 插屏广告状态DTO
    /// </summary>
    public class HbtInterstitialStatusDto
    {
        /// <summary>
        /// 插屏ID
        /// </summary>
        public long InterstitialId { get; set; }

        /// <summary>
        /// 插屏状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 插屏广告审核DTO
    /// </summary>
    public class HbtInterstitialAuditDto
    {
        /// <summary>
        /// 插屏ID
        /// </summary>
        public long InterstitialId { get; set; }

        /// <summary>
        /// 插屏审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int InterstitialAuditStatus { get; set; }

        /// <summary>
        /// 插屏审核备注
        /// </summary>
        public string? InterstitialAuditRemark { get; set; }
    }
} 