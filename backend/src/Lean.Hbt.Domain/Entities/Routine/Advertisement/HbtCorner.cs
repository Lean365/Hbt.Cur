#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtCorner.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 角落广告实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 角落广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtCorner", "角落广告表")]
    public class HbtCorner : HbtBaseEntity
    {
        /// <summary>
        /// 角落广告标题
        /// </summary>
        [SugarColumn(ColumnName = "CornerTitle", IsNullable = false, ColumnDescription = "角落广告标题")]
        public string CornerTitle { get; set; } = string.Empty;

        /// <summary>
        /// 角落广告副标题
        /// </summary>
        [SugarColumn(ColumnName = "CornerSubtitle", IsNullable = true, ColumnDescription = "角落广告副标题")]
        public string? CornerSubtitle { get; set; }

        /// <summary>
        /// 角落广告描述
        /// </summary>
        [SugarColumn(ColumnName = "CornerDescription", IsNullable = true, ColumnDescription = "角落广告描述")]
        public string? CornerDescription { get; set; }

        /// <summary>
        /// 角落广告图片
        /// </summary>
        [SugarColumn(ColumnName = "CornerImage", IsNullable = false, ColumnDescription = "角落广告图片")]
        public string CornerImage { get; set; } = string.Empty;

        /// <summary>
        /// 角落广告链接
        /// </summary>
        [SugarColumn(ColumnName = "CornerLink", IsNullable = true, ColumnDescription = "角落广告链接")]
        public string? CornerLink { get; set; }

        /// <summary>
        /// 角落广告类型
        /// </summary>
        [SugarColumn(ColumnName = "CornerType", IsNullable = false, ColumnDescription = "角落广告类型")]
        public string CornerType { get; set; } = string.Empty;

        /// <summary>
        /// 角落位置 (top-left, top-right, bottom-left, bottom-right)
        /// </summary>
        [SugarColumn(ColumnName = "CornerPosition", IsNullable = false, ColumnDescription = "角落位置")]
        public string CornerPosition { get; set; } = "bottom-right";

        /// <summary>
        /// 角落广告宽度
        /// </summary>
        [SugarColumn(ColumnName = "CornerWidth", IsNullable = false, ColumnDescription = "角落广告宽度")]
        public int CornerWidth { get; set; } = 150;

        /// <summary>
        /// 角落广告高度
        /// </summary>
        [SugarColumn(ColumnName = "CornerHeight", IsNullable = false, ColumnDescription = "角落广告高度")]
        public int CornerHeight { get; set; } = 150;

        /// <summary>
        /// 角落广告透明度 (0-100)
        /// </summary>
        [SugarColumn(ColumnName = "CornerOpacity", IsNullable = false, ColumnDescription = "角落广告透明度")]
        public int CornerOpacity { get; set; } = 90;

        /// <summary>
        /// 是否可关闭
        /// </summary>
        [SugarColumn(ColumnName = "CornerClosable", IsNullable = false, ColumnDescription = "是否可关闭")]
        public int CornerClosable { get; set; } = 1;

        /// <summary>
        /// 是否可最小化
        /// </summary>
        [SugarColumn(ColumnName = "CornerMinimizable", IsNullable = false, ColumnDescription = "是否可最小化")]
        public int CornerMinimizable { get; set; } = 1;

        /// <summary>
        /// 最小化后宽度
        /// </summary>
        [SugarColumn(ColumnName = "CornerMinWidth", IsNullable = false, ColumnDescription = "最小化后宽度")]
        public int CornerMinWidth { get; set; } = 50;

        /// <summary>
        /// 最小化后高度
        /// </summary>
        [SugarColumn(ColumnName = "CornerMinHeight", IsNullable = false, ColumnDescription = "最小化后高度")]
        public int CornerMinHeight { get; set; } = 50;

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "CornerDelay", IsNullable = false, ColumnDescription = "显示延迟")]
        public int CornerDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒，0表示一直显示）
        /// </summary>
        [SugarColumn(ColumnName = "CornerDuration", IsNullable = false, ColumnDescription = "显示时长")]
        public int CornerDuration { get; set; } = 0;

        /// <summary>
        /// 是否自动隐藏
        /// </summary>
        [SugarColumn(ColumnName = "CornerAutoHide", IsNullable = false, ColumnDescription = "是否自动隐藏")]
        public int CornerAutoHide { get; set; } = 0;

        /// <summary>
        /// 自动隐藏延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "CornerAutoHideDelay", IsNullable = false, ColumnDescription = "自动隐藏延迟")]
        public int CornerAutoHideDelay { get; set; } = 5;

        /// <summary>
        /// 角落广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "角落广告状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 角落广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "CornerAuditStatus", IsNullable = false, ColumnDescription = "角落广告审核状态")]
        public int CornerAuditStatus { get; set; } = 0;

        /// <summary>
        /// 角落广告审核备注
        /// </summary>
        [SugarColumn(ColumnName = "CornerAuditRemark", IsNullable = true, ColumnDescription = "角落广告审核备注")]
        public string? CornerAuditRemark { get; set; }

        /// <summary>
        /// 角落广告审核人
        /// </summary>
        [SugarColumn(ColumnName = "CornerAuditorBy", IsNullable = true, ColumnDescription = "角落广告审核人")]
        public string? CornerAuditorBy { get; set; }

        /// <summary>
        /// 角落广告审核时间
        /// </summary>
        [SugarColumn(ColumnName = "CornerAuditTime", IsNullable = true, ColumnDescription = "角落广告审核时间")]
        public DateTime? CornerAuditTime { get; set; }

        /// <summary>
        /// 角落广告发布人
        /// </summary>
        [SugarColumn(ColumnName = "CornerPublisherBy", IsNullable = true, ColumnDescription = "角落广告发布人")]
        public string? CornerPublisherBy { get; set; }

        /// <summary>
        /// 角落广告发布时间
        /// </summary>
        [SugarColumn(ColumnName = "CornerPublishTime", IsNullable = true, ColumnDescription = "角落广告发布时间")]
        public DateTime? CornerPublishTime { get; set; }

        /// <summary>
        /// 角落广告下线时间
        /// </summary>
        [SugarColumn(ColumnName = "CornerOfflineTime", IsNullable = true, ColumnDescription = "角落广告下线时间")]
        public DateTime? CornerOfflineTime { get; set; }

        /// <summary>
        /// 角落广告编辑人
        /// </summary>
        [SugarColumn(ColumnName = "CornerEditorBy", IsNullable = true, ColumnDescription = "角落广告编辑人")]
        public string? CornerEditorBy { get; set; }

        /// <summary>
        /// 角落广告编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "CornerEditTime", IsNullable = true, ColumnDescription = "角落广告编辑时间")]
        public DateTime? CornerEditTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [SugarColumn(ColumnName = "CornerIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int CornerIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        [SugarColumn(ColumnName = "CornerIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int CornerIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        [SugarColumn(ColumnName = "CornerIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int CornerIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "CornerShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int CornerShowCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "CornerClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int CornerClickCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "CornerCloseCount", IsNullable = false, ColumnDescription = "关闭次数")]
        public int CornerCloseCount { get; set; } = 0;

        /// <summary>
        /// 最小化次数
        /// </summary>
        [SugarColumn(ColumnName = "CornerMinimizeCount", IsNullable = false, ColumnDescription = "最小化次数")]
        public int CornerMinimizeCount { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "CornerPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal CornerPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "CornerBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int CornerBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "CornerCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal CornerCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "CornerCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal CornerCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "CornerCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal CornerCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "CornerBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal CornerBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "CornerSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal CornerSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "CornerRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal CornerRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "CornerBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int CornerBillingStatus { get; set; } = 0;
    }
} 