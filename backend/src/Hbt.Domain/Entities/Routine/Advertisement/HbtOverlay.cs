#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOverlay.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 覆盖广告实体
//===================================================================

using SqlSugar;

namespace Hbt.Cur.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 覆盖广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtOverlay", "覆盖广告表")]
    public class HbtOverlay : HbtBaseEntity
    {
        /// <summary>
        /// 覆盖广告标题
        /// </summary>
        [SugarColumn(ColumnName = "OverlayTitle", IsNullable = false, ColumnDescription = "覆盖广告标题")]
        public string OverlayTitle { get; set; } = string.Empty;

        /// <summary>
        /// 覆盖广告副标题
        /// </summary>
        [SugarColumn(ColumnName = "OverlaySubtitle", IsNullable = true, ColumnDescription = "覆盖广告副标题")]
        public string? OverlaySubtitle { get; set; }

        /// <summary>
        /// 覆盖广告描述
        /// </summary>
        [SugarColumn(ColumnName = "OverlayDescription", IsNullable = true, ColumnDescription = "覆盖广告描述")]
        public string? OverlayDescription { get; set; }

        /// <summary>
        /// 覆盖广告图片
        /// </summary>
        [SugarColumn(ColumnName = "OverlayImage", IsNullable = false, ColumnDescription = "覆盖广告图片")]
        public string OverlayImage { get; set; } = string.Empty;

        /// <summary>
        /// 覆盖广告链接
        /// </summary>
        [SugarColumn(ColumnName = "OverlayLink", IsNullable = true, ColumnDescription = "覆盖广告链接")]
        public string? OverlayLink { get; set; }

        /// <summary>
        /// 覆盖广告类型
        /// </summary>
        [SugarColumn(ColumnName = "OverlayType", IsNullable = false, ColumnDescription = "覆盖广告类型")]
        public string OverlayType { get; set; } = string.Empty;

        /// <summary>
        /// 覆盖位置 (top, bottom, left, right, center, fullscreen)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayPosition", IsNullable = false, ColumnDescription = "覆盖位置")]
        public string OverlayPosition { get; set; } = "center";

        /// <summary>
        /// 覆盖广告宽度
        /// </summary>
        [SugarColumn(ColumnName = "OverlayWidth", IsNullable = false, ColumnDescription = "覆盖广告宽度")]
        public int OverlayWidth { get; set; } = 400;

        /// <summary>
        /// 覆盖广告高度
        /// </summary>
        [SugarColumn(ColumnName = "OverlayHeight", IsNullable = false, ColumnDescription = "覆盖广告高度")]
        public int OverlayHeight { get; set; } = 300;

        /// <summary>
        /// 背景遮罩透明度 (0-100)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayMaskOpacity", IsNullable = false, ColumnDescription = "背景遮罩透明度")]
        public int OverlayMaskOpacity { get; set; } = 50;

        /// <summary>
        /// 是否可关闭
        /// </summary>
        [SugarColumn(ColumnName = "OverlayClosable", IsNullable = false, ColumnDescription = "是否可关闭")]
        public int OverlayClosable { get; set; } = 1;

        /// <summary>
        /// 是否点击遮罩关闭
        /// </summary>
        [SugarColumn(ColumnName = "OverlayMaskClosable", IsNullable = false, ColumnDescription = "是否点击遮罩关闭")]
        public int OverlayMaskClosable { get; set; } = 1;

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayDelay", IsNullable = false, ColumnDescription = "显示延迟")]
        public int OverlayDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒，0表示一直显示）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayDuration", IsNullable = false, ColumnDescription = "显示时长")]
        public int OverlayDuration { get; set; } = 0;

        /// <summary>
        /// 动画效果 (fade, slide, zoom, none)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayAnimation", IsNullable = false, ColumnDescription = "动画效果")]
        public string OverlayAnimation { get; set; } = "fade";

        /// <summary>
        /// 覆盖广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "覆盖广告状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 覆盖广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayAuditStatus", IsNullable = false, ColumnDescription = "覆盖广告审核状态")]
        public int OverlayAuditStatus { get; set; } = 0;

        /// <summary>
        /// 覆盖广告审核备注
        /// </summary>
        [SugarColumn(ColumnName = "OverlayAuditRemark", IsNullable = true, ColumnDescription = "覆盖广告审核备注")]
        public string? OverlayAuditRemark { get; set; }

        /// <summary>
        /// 覆盖广告审核人
        /// </summary>
        [SugarColumn(ColumnName = "OverlayAuditorBy", IsNullable = true, ColumnDescription = "覆盖广告审核人")]
        public string? OverlayAuditorBy { get; set; }

        /// <summary>
        /// 覆盖广告审核时间
        /// </summary>
        [SugarColumn(ColumnName = "OverlayAuditTime", IsNullable = true, ColumnDescription = "覆盖广告审核时间")]
        public DateTime? OverlayAuditTime { get; set; }

        /// <summary>
        /// 覆盖广告发布人
        /// </summary>
        [SugarColumn(ColumnName = "OverlayPublisherBy", IsNullable = true, ColumnDescription = "覆盖广告发布人")]
        public string? OverlayPublisherBy { get; set; }

        /// <summary>
        /// 覆盖广告发布时间
        /// </summary>
        [SugarColumn(ColumnName = "OverlayPublishTime", IsNullable = true, ColumnDescription = "覆盖广告发布时间")]
        public DateTime? OverlayPublishTime { get; set; }

        /// <summary>
        /// 覆盖广告下线时间
        /// </summary>
        [SugarColumn(ColumnName = "OverlayOfflineTime", IsNullable = true, ColumnDescription = "覆盖广告下线时间")]
        public DateTime? OverlayOfflineTime { get; set; }

        /// <summary>
        /// 覆盖广告编辑人
        /// </summary>
        [SugarColumn(ColumnName = "OverlayEditorBy", IsNullable = true, ColumnDescription = "覆盖广告编辑人")]
        public string? OverlayEditorBy { get; set; }

        /// <summary>
        /// 覆盖广告编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "OverlayEditTime", IsNullable = true, ColumnDescription = "覆盖广告编辑时间")]
        public DateTime? OverlayEditTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [SugarColumn(ColumnName = "OverlayIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int OverlayIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        [SugarColumn(ColumnName = "OverlayIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int OverlayIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        [SugarColumn(ColumnName = "OverlayIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int OverlayIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "OverlayShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int OverlayShowCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "OverlayClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int OverlayClickCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "OverlayCloseCount", IsNullable = false, ColumnDescription = "关闭次数")]
        public int OverlayCloseCount { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal OverlayPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int OverlayBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal OverlayCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal OverlayCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal OverlayCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal OverlayBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "OverlaySpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal OverlaySpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "OverlayRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal OverlayRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "OverlayBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int OverlayBillingStatus { get; set; } = 0;
    }
} 