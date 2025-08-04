#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBanner.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 横幅广告实体
//===================================================================

using SqlSugar;

namespace Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 横幅广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtBanner")]
    public class HbtBanner : HbtBaseEntity
    {
        /// <summary>
        /// 横幅标题
        /// </summary>
        [SugarColumn(ColumnName = "BannerTitle", Length = 200, IsNullable = false, ColumnDescription = "横幅标题")]
        public string BannerTitle { get; set; } = string.Empty;

        /// <summary>
        /// 横幅副标题
        /// </summary>
        [SugarColumn(ColumnName = "BannerSubtitle", Length = 500, IsNullable = true, ColumnDescription = "横幅副标题")]
        public string? BannerSubtitle { get; set; }

        /// <summary>
        /// 横幅描述
        /// </summary>
        [SugarColumn(ColumnName = "BannerDescription", Length = 1000, IsNullable = true, ColumnDescription = "横幅描述")]
        public string? BannerDescription { get; set; }

        /// <summary>
        /// 横幅图片URL
        /// </summary>
        [SugarColumn(ColumnName = "BannerImageUrl", Length = 500, IsNullable = false, ColumnDescription = "横幅图片URL")]
        public string BannerImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 横幅链接URL
        /// </summary>
        [SugarColumn(ColumnName = "BannerLinkUrl", Length = 500, IsNullable = true, ColumnDescription = "横幅链接URL")]
        public string? BannerLinkUrl { get; set; }

        /// <summary>
        /// 横幅位置
        /// </summary>
        [SugarColumn(ColumnName = "BannerPosition", Length = 50, IsNullable = false, ColumnDescription = "横幅位置")]
        public string BannerPosition { get; set; } = string.Empty;

        /// <summary>
        /// 横幅类型
        /// </summary>
        [SugarColumn(ColumnName = "BannerType", Length = 50, IsNullable = false, ColumnDescription = "横幅类型")]
        public string BannerType { get; set; } = string.Empty;

        /// <summary>
        /// 横幅状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "横幅状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 横幅审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "BannerAuditStatus", IsNullable = false, ColumnDescription = "横幅审核状态")]
        public int BannerAuditStatus { get; set; } = 0;

        /// <summary>
        /// 横幅审核备注
        /// </summary>
        [SugarColumn(ColumnName = "BannerAuditRemark", Length = 500, IsNullable = true, ColumnDescription = "横幅审核备注")]
        public string? BannerAuditRemark { get; set; }

        /// <summary>
        /// 横幅审核人
        /// </summary>
        [SugarColumn(ColumnName = "BannerAuditorBy", Length = 100, IsNullable = true, ColumnDescription = "横幅审核人")]
        public string? BannerAuditorBy { get; set; }

        /// <summary>
        /// 横幅审核时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerAuditTime", IsNullable = true, ColumnDescription = "横幅审核时间")]
        public DateTime? BannerAuditTime { get; set; }

        /// <summary>
        /// 横幅发布时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerPublishTime", IsNullable = true, ColumnDescription = "横幅发布时间")]
        public DateTime? BannerPublishTime { get; set; }

        /// <summary>
        /// 横幅下线时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerOfflineTime", IsNullable = true, ColumnDescription = "横幅下线时间")]
        public DateTime? BannerOfflineTime { get; set; }

        /// <summary>
        /// 横幅开始时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerStartTime", IsNullable = true, ColumnDescription = "横幅开始时间")]
        public DateTime? BannerStartTime { get; set; }

        /// <summary>
        /// 横幅结束时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerEndTime", IsNullable = true, ColumnDescription = "横幅结束时间")]
        public DateTime? BannerEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "BannerIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int BannerIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "BannerIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int BannerIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "BannerIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int BannerIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "BannerClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int BannerClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "BannerShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int BannerShowCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 横幅编辑人
        /// </summary>
        [SugarColumn(ColumnName = "BannerEditorBy", Length = 100, IsNullable = true, ColumnDescription = "横幅编辑人")]
        public string? BannerEditorBy { get; set; }

        /// <summary>
        /// 横幅编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "BannerEditTime", IsNullable = true, ColumnDescription = "横幅编辑时间")]
        public DateTime? BannerEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "BannerPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal BannerPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "BannerBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int BannerBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "BannerCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal BannerCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "BannerCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal BannerCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "BannerCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal BannerCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "BannerBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal BannerBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "BannerSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal BannerSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "BannerRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal BannerRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "BannerBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int BannerBillingStatus { get; set; } = 0;
    }
} 