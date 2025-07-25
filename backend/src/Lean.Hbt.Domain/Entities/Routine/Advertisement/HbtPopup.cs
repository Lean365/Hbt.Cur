#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPopup.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 弹窗广告实体
//===================================================================

using SqlSugar;

namespace Lean.Hbt.Domain.Entities.Routine.Advertisement
{
    /// <summary>
    /// 弹窗广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("HbtPopup")]
    public class HbtPopup : HbtBaseEntity
    {
        /// <summary>
        /// 弹窗标题
        /// </summary>
        [SugarColumn(ColumnName = "PopupTitle", Length = 200, IsNullable = false, ColumnDescription = "弹窗标题")]
        public string PopupTitle { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗副标题
        /// </summary>
        [SugarColumn(ColumnName = "PopupSubtitle", Length = 500, IsNullable = true, ColumnDescription = "弹窗副标题")]
        public string? PopupSubtitle { get; set; }

        /// <summary>
        /// 弹窗描述
        /// </summary>
        [SugarColumn(ColumnName = "PopupDescription", Length = 1000, IsNullable = true, ColumnDescription = "弹窗描述")]
        public string? PopupDescription { get; set; }

        /// <summary>
        /// 弹窗图片URL
        /// </summary>
        [SugarColumn(ColumnName = "PopupImageUrl", Length = 500, IsNullable = false, ColumnDescription = "弹窗图片URL")]
        public string PopupImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗链接URL
        /// </summary>
        [SugarColumn(ColumnName = "PopupLinkUrl", Length = 500, IsNullable = true, ColumnDescription = "弹窗链接URL")]
        public string? PopupLinkUrl { get; set; }

        /// <summary>
        /// 弹窗位置
        /// </summary>
        [SugarColumn(ColumnName = "PopupPosition", Length = 50, IsNullable = false, ColumnDescription = "弹窗位置")]
        public string PopupPosition { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗类型
        /// </summary>
        [SugarColumn(ColumnName = "PopupType", Length = 50, IsNullable = false, ColumnDescription = "弹窗类型")]
        public string PopupType { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false, ColumnDescription = "弹窗状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 弹窗审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "PopupAuditStatus", IsNullable = false, ColumnDescription = "弹窗审核状态")]
        public int PopupAuditStatus { get; set; } = 0;

        /// <summary>
        /// 弹窗审核备注
        /// </summary>
        [SugarColumn(ColumnName = "PopupAuditRemark", Length = 500, IsNullable = true, ColumnDescription = "弹窗审核备注")]
        public string? PopupAuditRemark { get; set; }

        /// <summary>
        /// 弹窗审核人
        /// </summary>
        [SugarColumn(ColumnName = "PopupAuditorBy", Length = 100, IsNullable = true, ColumnDescription = "弹窗审核人")]
        public string? PopupAuditorBy { get; set; }

        /// <summary>
        /// 弹窗审核时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupAuditTime", IsNullable = true, ColumnDescription = "弹窗审核时间")]
        public DateTime? PopupAuditTime { get; set; }

        /// <summary>
        /// 弹窗发布时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupPublishTime", IsNullable = true, ColumnDescription = "弹窗发布时间")]
        public DateTime? PopupPublishTime { get; set; }

        /// <summary>
        /// 弹窗下线时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupOfflineTime", IsNullable = true, ColumnDescription = "弹窗下线时间")]
        public DateTime? PopupOfflineTime { get; set; }

        /// <summary>
        /// 弹窗开始时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupStartTime", IsNullable = true, ColumnDescription = "弹窗开始时间")]
        public DateTime? PopupStartTime { get; set; }

        /// <summary>
        /// 弹窗结束时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupEndTime", IsNullable = true, ColumnDescription = "弹窗结束时间")]
        public DateTime? PopupEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "PopupIsTop", IsNullable = false, ColumnDescription = "是否置顶")]
        public int PopupIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "PopupIsRecommend", IsNullable = false, ColumnDescription = "是否推荐")]
        public int PopupIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "PopupIsHot", IsNullable = false, ColumnDescription = "是否热门")]
        public int PopupIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "PopupClickCount", IsNullable = false, ColumnDescription = "点击次数")]
        public int PopupClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "PopupShowCount", IsNullable = false, ColumnDescription = "展示次数")]
        public int PopupShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "PopupCloseCount", IsNullable = false, ColumnDescription = "关闭次数")]
        public int PopupCloseCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNum", IsNullable = false, ColumnDescription = "排序号")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 弹窗编辑人
        /// </summary>
        [SugarColumn(ColumnName = "PopupEditorBy", Length = 100, IsNullable = true, ColumnDescription = "弹窗编辑人")]
        public string? PopupEditorBy { get; set; }

        /// <summary>
        /// 弹窗编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "PopupEditTime", IsNullable = true, ColumnDescription = "弹窗编辑时间")]
        public DateTime? PopupEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        [SugarColumn(ColumnName = "PopupPrice", IsNullable = false, ColumnDescription = "广告价格")]
        public decimal PopupPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "PopupBillingType", IsNullable = false, ColumnDescription = "计费方式")]
        public int PopupBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        [SugarColumn(ColumnName = "PopupCpmPrice", IsNullable = false, ColumnDescription = "千次展示价格")]
        public decimal PopupCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        [SugarColumn(ColumnName = "PopupCpcPrice", IsNullable = false, ColumnDescription = "点击价格")]
        public decimal PopupCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        [SugarColumn(ColumnName = "PopupCpaPrice", IsNullable = false, ColumnDescription = "行动价格")]
        public decimal PopupCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "PopupBudget", IsNullable = false, ColumnDescription = "预算金额")]
        public decimal PopupBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "PopupSpentAmount", IsNullable = false, ColumnDescription = "已消耗金额")]
        public decimal PopupSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "PopupRemainingAmount", IsNullable = false, ColumnDescription = "剩余金额")]
        public decimal PopupRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        [SugarColumn(ColumnName = "PopupBillingStatus", IsNullable = false, ColumnDescription = "计费状态")]
        public int PopupBillingStatus { get; set; } = 0;
    }
} 