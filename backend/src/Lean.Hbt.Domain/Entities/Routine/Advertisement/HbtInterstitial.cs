#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInterstitial.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 插屏广告实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 插屏广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtInterstitial")]
    public class HbtInterstitial : HbtBaseEntity
    {
        /// <summary>
        /// 插屏标题
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialTitle", Length = 200, IsNullable = false, ColumnDescription = "插屏标题")]
        public string InterstitialTitle { get; set; } = string.Empty;

        /// <summary>
        /// 插屏副标题
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialSubtitle", Length = 500, IsNullable = true, ColumnDescription = "插屏副标题")]
        public string? InterstitialSubtitle { get; set; }

        /// <summary>
        /// 插屏描述
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialDescription", Length = 1000, IsNullable = true, ColumnDescription = "插屏描述")]
        public string? InterstitialDescription { get; set; }

        /// <summary>
        /// 插屏图片URL
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialImageUrl", Length = 500, IsNullable = false, ColumnDescription = "插屏图片URL")]
        public string InterstitialImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 插屏链接URL
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialLinkUrl", Length = 500, IsNullable = true, ColumnDescription = "插屏链接URL")]
        public string? InterstitialLinkUrl { get; set; }

        /// <summary>
        /// 插屏位置
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialPosition", Length = 50, IsNullable = false, ColumnDescription = "插屏位置")]
        public string InterstitialPosition { get; set; } = string.Empty;

        /// <summary>
        /// 插屏类型
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialType", Length = 50, IsNullable = false, ColumnDescription = "插屏类型")]
        public string InterstitialType { get; set; } = string.Empty;

        /// <summary>
        /// 插屏状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "插屏状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 插屏审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialAuditStatus", IsNullable = false, ColumnDescription = "插屏审核状态")]
        public int InterstitialAuditStatus { get; set; } = 0;

        /// <summary>
        /// 插屏审核备注
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialAuditRemark", Length = 500, IsNullable = true, ColumnDescription = "插屏审核备注")]
        public string? InterstitialAuditRemark { get; set; }

        /// <summary>
        /// 插屏审核人
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialAuditorBy", Length = 100, IsNullable = true, ColumnDescription = "插屏审核人")]
        public string? InterstitialAuditorBy { get; set; }

        /// <summary>
        /// 插屏审核时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialAuditTime", IsNullable = true, ColumnDescription = "插屏审核时间")]
        public DateTime? InterstitialAuditTime { get; set; }

        /// <summary>
        /// 插屏发布时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialPublishTime", IsNullable = true, ColumnDescription = "插屏发布时间")]
        public DateTime? InterstitialPublishTime { get; set; }

        /// <summary>
        /// 插屏下线时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialOfflineTime", IsNullable = true, ColumnDescription = "插屏下线时间")]
        public DateTime? InterstitialOfflineTime { get; set; }

        /// <summary>
        /// 插屏开始时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialStartTime", IsNullable = true, ColumnDescription = "插屏开始时间")]
        public DateTime? InterstitialStartTime { get; set; }

        /// <summary>
        /// 插屏结束时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialEndTime", IsNullable = true, ColumnDescription = "插屏结束时间")]
        public DateTime? InterstitialEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int InterstitialIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int InterstitialIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int InterstitialIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int InterstitialClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int InterstitialShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialCloseCount", IsNullable = false, ColumnDescription = "关闭次数")]
        public int InterstitialCloseCount { get; set; } = 0;

        /// <summary>
        /// 跳过次数
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialSkipCount", IsNullable = false, ColumnDescription = "跳过次数")]
        public int InterstitialSkipCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 插屏编辑人
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialEditorBy", Length = 100, IsNullable = true, ColumnDescription = "插屏编辑人")]
        public string? InterstitialEditorBy { get; set; }

        /// <summary>
        /// 插屏编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialEditTime", IsNullable = true, ColumnDescription = "插屏编辑时间")]
        public DateTime? InterstitialEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal InterstitialPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int InterstitialBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal InterstitialCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal InterstitialCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal InterstitialCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal InterstitialBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal InterstitialSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal InterstitialRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "InterstitialBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int InterstitialBillingStatus { get; set; } = 0;
    }
} 