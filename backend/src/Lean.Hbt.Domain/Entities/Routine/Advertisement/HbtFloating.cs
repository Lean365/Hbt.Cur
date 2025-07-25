#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtFloating.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 浮动广告实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 浮动广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtFloating", "浮动广告表")]
    public class HbtFloating : HbtBaseEntity
    {
        /// <summary>
        /// 浮动广告标题
        /// </summary>
        [SugarColumn(ColumnName = "FloatingTitle", IsNullable = false, ColumnDescription = "浮动广告标题")]
        public string FloatingTitle { get; set; } = string.Empty;

        /// <summary>
        /// 浮动广告副标题
        /// </summary>
        [SugarColumn(ColumnName = "FloatingSubtitle", IsNullable = true, ColumnDescription = "浮动广告副标题")]
        public string? FloatingSubtitle { get; set; }

        /// <summary>
        /// 浮动广告描述
        /// </summary>
        [SugarColumn(ColumnName = "FloatingDescription", IsNullable = true, ColumnDescription = "浮动广告描述")]
        public string? FloatingDescription { get; set; }

        /// <summary>
        /// 浮动广告图片
        /// </summary>
        [SugarColumn(ColumnName = "FloatingImage", IsNullable = false, ColumnDescription = "浮动广告图片")]
        public string FloatingImage { get; set; } = string.Empty;

        /// <summary>
        /// 浮动广告链接
        /// </summary>
        [SugarColumn(ColumnName = "FloatingLink", IsNullable = true, ColumnDescription = "浮动广告链接")]
        public string? FloatingLink { get; set; }

        /// <summary>
        /// 浮动广告类型
        /// </summary>
        [SugarColumn(ColumnName = "FloatingType", IsNullable = false, ColumnDescription = "浮动广告类型")]
        public string FloatingType { get; set; } = string.Empty;

        /// <summary>
        /// 浮动位置 (top-left, top-right, bottom-left, bottom-right, center)
        /// </summary>
        [SugarColumn(ColumnName = "FloatingPosition", IsNullable = false, ColumnDescription = "浮动位置")]
        public string FloatingPosition { get; set; } = "bottom-right";

        /// <summary>
        /// 浮动广告宽度
        /// </summary>
        [SugarColumn(ColumnName = "FloatingWidth", IsNullable = false, ColumnDescription = "浮动广告宽度")]
        public int FloatingWidth { get; set; } = 200;

        /// <summary>
        /// 浮动广告高度
        /// </summary>
        [SugarColumn(ColumnName = "FloatingHeight", IsNullable = false, ColumnDescription = "浮动广告高度")]
        public int FloatingHeight { get; set; } = 200;

        /// <summary>
        /// 浮动广告透明度 (0-100)
        /// </summary>
        [SugarColumn(ColumnName = "FloatingOpacity", IsNullable = false, ColumnDescription = "浮动广告透明度")]
        public int FloatingOpacity { get; set; } = 80;

        /// <summary>
        /// 是否可关闭
        /// </summary>
        [SugarColumn(ColumnName = "FloatingClosable", IsNullable = false, ColumnDescription = "是否可关闭")]
        public int FloatingClosable { get; set; } = 1;

        /// <summary>
        /// 是否可拖拽
        /// </summary>
        [SugarColumn(ColumnName = "FloatingDraggable", IsNullable = false, ColumnDescription = "是否可拖拽")]
        public int FloatingDraggable { get; set; } = 1;

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingDelay", IsNullable = false, ColumnDescription = "显示延迟")]
        public int FloatingDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒，0表示一直显示）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingDuration", IsNullable = false, ColumnDescription = "显示时长")]
        public int FloatingDuration { get; set; } = 0;

        /// <summary>
        /// 浮动广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "浮动广告状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 浮动广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "FloatingAuditStatus", IsNullable = false, ColumnDescription = "浮动广告审核状态")]
        public int FloatingAuditStatus { get; set; } = 0;

        /// <summary>
        /// 浮动广告审核备注
        /// </summary>
        [SugarColumn(ColumnName = "FloatingAuditRemark", IsNullable = true, ColumnDescription = "浮动广告审核备注")]
        public string? FloatingAuditRemark { get; set; }

        /// <summary>
        /// 浮动广告审核人
        /// </summary>
        [SugarColumn(ColumnName = "FloatingAuditorBy", IsNullable = true, ColumnDescription = "浮动广告审核人")]
        public string? FloatingAuditorBy { get; set; }

        /// <summary>
        /// 浮动广告审核时间
        /// </summary>
        [SugarColumn(ColumnName = "FloatingAuditTime", IsNullable = true, ColumnDescription = "浮动广告审核时间")]
        public DateTime? FloatingAuditTime { get; set; }

        /// <summary>
        /// 浮动广告发布人
        /// </summary>
        [SugarColumn(ColumnName = "FloatingPublisherBy", IsNullable = true, ColumnDescription = "浮动广告发布人")]
        public string? FloatingPublisherBy { get; set; }

        /// <summary>
        /// 浮动广告发布时间
        /// </summary>
        [SugarColumn(ColumnName = "FloatingPublishTime", IsNullable = true, ColumnDescription = "浮动广告发布时间")]
        public DateTime? FloatingPublishTime { get; set; }

        /// <summary>
        /// 浮动广告下线时间
        /// </summary>
        [SugarColumn(ColumnName = "FloatingOfflineTime", IsNullable = true, ColumnDescription = "浮动广告下线时间")]
        public DateTime? FloatingOfflineTime { get; set; }

        /// <summary>
        /// 浮动广告编辑人
        /// </summary>
        [SugarColumn(ColumnName = "FloatingEditorBy", IsNullable = true, ColumnDescription = "浮动广告编辑人")]
        public string? FloatingEditorBy { get; set; }

        /// <summary>
        /// 浮动广告编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "FloatingEditTime", IsNullable = true, ColumnDescription = "浮动广告编辑时间")]
        public DateTime? FloatingEditTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [SugarColumn(ColumnName = "FloatingIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int FloatingIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        [SugarColumn(ColumnName = "FloatingIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int FloatingIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        [SugarColumn(ColumnName = "FloatingIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int FloatingIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "FloatingShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int FloatingShowCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "FloatingClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int FloatingClickCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "FloatingCloseCount", IsNullable = false, ColumnDescription = "关闭次数")]
        public int FloatingCloseCount { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal FloatingPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "FloatingBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int FloatingBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal FloatingCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal FloatingCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal FloatingCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal FloatingBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal FloatingSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "FloatingRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal FloatingRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "FloatingBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int FloatingBillingStatus { get; set; } = 0;
    }
} 