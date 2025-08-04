#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPopupDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 弹窗广告DTO
//===================================================================

namespace Hbt.Application.Dtos.Routine.Advertisement
{
    /// <summary>
    /// 弹窗广告DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class HbtPopupDto
    {
        /// <summary>
        /// 弹窗ID
        /// </summary>
        public long PopupId { get; set; }

        /// <summary>
        /// 弹窗标题
        /// </summary>
        public string PopupTitle { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗副标题
        /// </summary>
        public string? PopupSubtitle { get; set; }

        /// <summary>
        /// 弹窗描述
        /// </summary>
        public string? PopupDescription { get; set; }

        /// <summary>
        /// 弹窗图片URL
        /// </summary>
        public string PopupImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗链接URL
        /// </summary>
        public string? PopupLinkUrl { get; set; }

        /// <summary>
        /// 弹窗位置
        /// </summary>
        public string PopupPosition { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗类型
        /// </summary>
        public string PopupType { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 弹窗审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int PopupAuditStatus { get; set; } = 0;

        /// <summary>
        /// 弹窗审核备注
        /// </summary>
        public string? PopupAuditRemark { get; set; }

        /// <summary>
        /// 弹窗审核人
        /// </summary>
        public string? PopupAuditorBy { get; set; }

        /// <summary>
        /// 弹窗审核时间
        /// </summary>
        public DateTime? PopupAuditTime { get; set; }

        /// <summary>
        /// 弹窗发布时间
        /// </summary>
        public DateTime? PopupPublishTime { get; set; }

        /// <summary>
        /// 弹窗下线时间
        /// </summary>
        public DateTime? PopupOfflineTime { get; set; }

        /// <summary>
        /// 弹窗开始时间
        /// </summary>
        public DateTime? PopupStartTime { get; set; }

        /// <summary>
        /// 弹窗结束时间
        /// </summary>
        public DateTime? PopupEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsHot { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int PopupClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int PopupShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        public int PopupCloseCount { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 弹窗编辑人
        /// </summary>
        public string? PopupEditorBy { get; set; }

        /// <summary>
        /// 弹窗编辑时间
        /// </summary>
        public DateTime? PopupEditTime { get; set; }

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal PopupPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int PopupBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal PopupCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal PopupCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal PopupCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal PopupBudget { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal PopupSpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal PopupRemainingAmount { get; set; } = 0;

        /// <summary>
        /// 计费状态 (0: 未计费, 1: 计费中, 2: 已暂停, 3: 已结束)
        /// </summary>
        public int PopupBillingStatus { get; set; } = 0;

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
    /// 弹窗广告查询DTO
    /// </summary>
    public class HbtPopupQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 弹窗标题
        /// </summary>
        public string? PopupTitle { get; set; }

        /// <summary>
        /// 弹窗位置
        /// </summary>
        public string? PopupPosition { get; set; }

        /// <summary>
        /// 弹窗类型
        /// </summary>
        public string? PopupType { get; set; }

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
        public int? PopupAuditStatus { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? PopupIsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? PopupIsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? PopupIsHot { get; set; }

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
    /// 弹窗广告创建DTO
    /// </summary>
    public class HbtPopupCreateDto
    {
        /// <summary>
        /// 弹窗标题
        /// </summary>
        public string PopupTitle { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗副标题
        /// </summary>
        public string? PopupSubtitle { get; set; }

        /// <summary>
        /// 弹窗描述
        /// </summary>
        public string? PopupDescription { get; set; }

        /// <summary>
        /// 弹窗图片URL
        /// </summary>
        public string PopupImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗链接URL
        /// </summary>
        public string? PopupLinkUrl { get; set; }

        /// <summary>
        /// 弹窗位置
        /// </summary>
        public string PopupPosition { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗类型
        /// </summary>
        public string PopupType { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 弹窗开始时间
        /// </summary>
        public DateTime? PopupStartTime { get; set; }

        /// <summary>
        /// 弹窗结束时间
        /// </summary>
        public DateTime? PopupEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal PopupPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int PopupBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal PopupCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal PopupCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal PopupCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal PopupBudget { get; set; } = 0;
    }

    /// <summary>
    /// 弹窗广告更新DTO
    /// </summary>
    public class HbtPopupUpdateDto
    {
        /// <summary>
        /// 弹窗ID
        /// </summary>
        public long PopupId { get; set; }

        /// <summary>
        /// 弹窗标题
        /// </summary>
        public string PopupTitle { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗副标题
        /// </summary>
        public string? PopupSubtitle { get; set; }

        /// <summary>
        /// 弹窗描述
        /// </summary>
        public string? PopupDescription { get; set; }

        /// <summary>
        /// 弹窗图片URL
        /// </summary>
        public string PopupImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗链接URL
        /// </summary>
        public string? PopupLinkUrl { get; set; }

        /// <summary>
        /// 弹窗位置
        /// </summary>
        public string PopupPosition { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗类型
        /// </summary>
        public string PopupType { get; set; } = string.Empty;

        /// <summary>
        /// 弹窗状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 弹窗开始时间
        /// </summary>
        public DateTime? PopupStartTime { get; set; }

        /// <summary>
        /// 弹窗结束时间
        /// </summary>
        public DateTime? PopupEndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int PopupIsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告价格（元）
        /// </summary>
        public decimal PopupPrice { get; set; } = 0;

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int PopupBillingType { get; set; } = 4;

        /// <summary>
        /// 千次展示价格（CPM）
        /// </summary>
        public decimal PopupCpmPrice { get; set; } = 0;

        /// <summary>
        /// 点击价格（CPC）
        /// </summary>
        public decimal PopupCpcPrice { get; set; } = 0;

        /// <summary>
        /// 行动价格（CPA）
        /// </summary>
        public decimal PopupCpaPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal PopupBudget { get; set; } = 0;
    }

    /// <summary>
    /// 弹窗广告状态DTO
    /// </summary>
    public class HbtPopupStatusDto
    {
        /// <summary>
        /// 弹窗ID
        /// </summary>
        public long PopupId { get; set; }

        /// <summary>
        /// 弹窗状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 弹窗广告审核DTO
    /// </summary>
    public class HbtPopupAuditDto
    {
        /// <summary>
        /// 弹窗ID
        /// </summary>
        public long PopupId { get; set; }

        /// <summary>
        /// 弹窗审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int PopupAuditStatus { get; set; }

        /// <summary>
        /// 弹窗审核备注
        /// </summary>
        public string? PopupAuditRemark { get; set; }
    }
} 