#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtSplash.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 开屏广告实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 开屏广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtSplash", "开屏广告表")]
    public class HbtSplash : HbtBaseEntity
    {
        /// <summary>
        /// 开屏广告标题
        /// </summary>
        [SugarColumn(ColumnName = "SplashTitle", IsNullable = false, ColumnDescription = "开屏广告标题")]
        public string SplashTitle { get; set; } = string.Empty;

        /// <summary>
        /// 开屏广告副标题
        /// </summary>
        [SugarColumn(ColumnName = "SplashSubtitle", IsNullable = true, ColumnDescription = "开屏广告副标题")]
        public string? SplashSubtitle { get; set; }

        /// <summary>
        /// 开屏广告描述
        /// </summary>
        [SugarColumn(ColumnName = "SplashDescription", IsNullable = true, ColumnDescription = "开屏广告描述")]
        public string? SplashDescription { get; set; }

        /// <summary>
        /// 开屏广告图片
        /// </summary>
        [SugarColumn(ColumnName = "SplashImage", IsNullable = false, ColumnDescription = "开屏广告图片")]
        public string SplashImage { get; set; } = string.Empty;

        /// <summary>
        /// 开屏广告链接
        /// </summary>
        [SugarColumn(ColumnName = "SplashLink", IsNullable = true, ColumnDescription = "开屏广告链接")]
        public string? SplashLink { get; set; }

        /// <summary>
        /// 开屏广告类型
        /// </summary>
        [SugarColumn(ColumnName = "SplashType", IsNullable = false, ColumnDescription = "开屏广告类型")]
        public string SplashType { get; set; } = string.Empty;

        /// <summary>
        /// 开屏广告位置
        /// </summary>
        [SugarColumn(ColumnName = "SplashPosition", IsNullable = false, ColumnDescription = "开屏广告位置")]
        public string SplashPosition { get; set; } = "fullscreen";

        /// <summary>
        /// 开屏广告宽度
        /// </summary>
        [SugarColumn(ColumnName = "SplashWidth", IsNullable = false, ColumnDescription = "开屏广告宽度")]
        public int SplashWidth { get; set; } = 1920;

        /// <summary>
        /// 开屏广告高度
        /// </summary>
        [SugarColumn(ColumnName = "SplashHeight", IsNullable = false, ColumnDescription = "开屏广告高度")]
        public int SplashHeight { get; set; } = 1080;

        /// <summary>
        /// 显示时长（秒）
        /// </summary>
        [SugarColumn(ColumnName = "SplashDuration", IsNullable = false, ColumnDescription = "显示时长")]
        public int SplashDuration { get; set; } = 3;

        /// <summary>
        /// 是否可跳过
        /// </summary>
        [SugarColumn(ColumnName = "SplashSkippable", IsNullable = false, ColumnDescription = "是否可跳过")]
        public int SplashSkippable { get; set; } = 1;

        /// <summary>
        /// 跳过延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "SplashSkipDelay", IsNullable = false, ColumnDescription = "跳过延迟")]
        public int SplashSkipDelay { get; set; } = 1;

        /// <summary>
        /// 是否显示倒计时
        /// </summary>
        [SugarColumn(ColumnName = "SplashShowCountdown", IsNullable = false, ColumnDescription = "是否显示倒计时")]
        public int SplashShowCountdown { get; set; } = 1;

        /// <summary>
        /// 倒计时位置 (top-left, top-right, bottom-left, bottom-right, center)
        /// </summary>
        [SugarColumn(ColumnName = "SplashCountdownPosition", IsNullable = false, ColumnDescription = "倒计时位置")]
        public string SplashCountdownPosition { get; set; } = "top-right";

        /// <summary>
        /// 倒计时样式
        /// </summary>
        [SugarColumn(ColumnName = "SplashCountdownStyle", IsNullable = false, ColumnDescription = "倒计时样式")]
        public string SplashCountdownStyle { get; set; } = "default";

        /// <summary>
        /// 开屏广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "开屏广告状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 开屏广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "SplashAuditStatus", IsNullable = false, ColumnDescription = "开屏广告审核状态")]
        public int SplashAuditStatus { get; set; } = 0;

        /// <summary>
        /// 开屏广告审核备注
        /// </summary>
        [SugarColumn(ColumnName = "SplashAuditRemark", IsNullable = true, ColumnDescription = "开屏广告审核备注")]
        public string? SplashAuditRemark { get; set; }

        /// <summary>
        /// 开屏广告审核人
        /// </summary>
        [SugarColumn(ColumnName = "SplashAuditorBy", IsNullable = true, ColumnDescription = "开屏广告审核人")]
        public string? SplashAuditorBy { get; set; }

        /// <summary>
        /// 开屏广告审核时间
        /// </summary>
        [SugarColumn(ColumnName = "SplashAuditTime", IsNullable = true, ColumnDescription = "开屏广告审核时间")]
        public DateTime? SplashAuditTime { get; set; }

        /// <summary>
        /// 开屏广告发布人
        /// </summary>
        [SugarColumn(ColumnName = "SplashPublisherBy", IsNullable = true, ColumnDescription = "开屏广告发布人")]
        public string? SplashPublisherBy { get; set; }

        /// <summary>
        /// 开屏广告发布时间
        /// </summary>
        [SugarColumn(ColumnName = "SplashPublishTime", IsNullable = true, ColumnDescription = "开屏广告发布时间")]
        public DateTime? SplashPublishTime { get; set; }

        /// <summary>
        /// 开屏广告下线时间
        /// </summary>
        [SugarColumn(ColumnName = "SplashOfflineTime", IsNullable = true, ColumnDescription = "开屏广告下线时间")]
        public DateTime? SplashOfflineTime { get; set; }

        /// <summary>
        /// 开屏广告编辑人
        /// </summary>
        [SugarColumn(ColumnName = "SplashEditorBy", IsNullable = true, ColumnDescription = "开屏广告编辑人")]
        public string? SplashEditorBy { get; set; }

        /// <summary>
        /// 开屏广告编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "SplashEditTime", IsNullable = true, ColumnDescription = "开屏广告编辑时间")]
        public DateTime? SplashEditTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [SugarColumn(ColumnName = "SplashIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int SplashIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        [SugarColumn(ColumnName = "SplashIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int SplashIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        [SugarColumn(ColumnName = "SplashIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int SplashIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "SplashShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int SplashShowCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "SplashClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int SplashClickCount { get; set; } = 0;

        /// <summary>
        /// 跳过次数
        /// </summary>
        [SugarColumn(ColumnName = "SplashSkipCount", IsNullable = false, ColumnDescription = "跳过次数")]
        public int SplashSkipCount { get; set; } = 0;

        /// <summary>
        /// 完成次数
        /// </summary>
        [SugarColumn(ColumnName = "SplashCompleteCount", IsNullable = false, ColumnDescription = "完成次数")]
        public int SplashCompleteCount { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "SplashPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal SplashPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "SplashBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int SplashBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "SplashCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal SplashCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "SplashCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal SplashCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "SplashCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal SplashCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "SplashBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal SplashBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "SplashSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal SplashSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "SplashRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal SplashRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "SplashBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int SplashBillingStatus { get; set; } = 0;
    }
} 