#nullable enable

//===================================================================
// 项目名 : Hbt.WebApi
// 文件名 : HbtNewsCommentAuditController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论审核控制器
//===================================================================

namespace Hbt.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻评论审核控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-01
    /// </remarks>
    [Route("api/[controller]", Name = "新闻评论审核")]
    [ApiController]
    [ApiModule("routine", "新闻管理")]
    public class HbtNewsCommentAuditController : ControllerBase
    {
        /// <summary>
        /// 评论服务
        /// </summary>
        private readonly IHbtNewsCommentService _commentService;

        /// <summary>
        /// 审核工作流服务
        /// </summary>
        private readonly IHbtNewsCommentAuditWorkflowService _auditWorkflowService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commentService">评论服务</param>
        /// <param name="auditWorkflowService">审核工作流服务</param>
        public HbtNewsCommentAuditController(
            IHbtNewsCommentService commentService,
            IHbtNewsCommentAuditWorkflowService auditWorkflowService)
        {
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _auditWorkflowService = auditWorkflowService ?? throw new ArgumentNullException(nameof(auditWorkflowService));
        }

        /// <summary>
        /// 获取待审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>待审核评论列表</returns>
        [HttpGet("pending")]
        public async Task<HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>> GetPendingAuditList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _commentService.GetPendingAuditListAsync(pageIndex, pageSize);
                return new HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>
                {
                    Code = 200,
                    Msg = "获取待审核评论列表成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>
                {
                    Code = 500,
                    Msg = $"获取待审核评论列表失败：{ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获取已审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>已审核评论列表</returns>
        [HttpGet("audited")]
        public async Task<HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>> GetAuditedList(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _commentService.GetAuditedListAsync(pageIndex, pageSize);
                return new HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>
                {
                    Code = 200,
                    Msg = "获取已审核评论列表成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<HbtPagedResult<HbtNewsCommentDto>>
                {
                    Code = 500,
                    Msg = $"获取已审核评论列表失败：{ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>审核结果</returns>
        [HttpPost("audit")]
        public async Task<HbtApiResult<bool>> AuditComment([FromBody] HbtNewsCommentAuditDto input)
        {
            try
            {
                var result = await _commentService.AuditAsync(input);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "审核评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"审核评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 批量审核评论
        /// </summary>
        /// <param name="input">批量审核对象</param>
        /// <returns>审核结果</returns>
        [HttpPost("batch-audit")]
        public async Task<HbtApiResult<bool>> BatchAuditComments([FromBody] HbtNewsCommentBatchAuditDto input)
        {
            try
            {
                var result = await _commentService.BatchAuditAsync(input);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "批量审核评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"批量审核评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 通过评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns>审核结果</returns>
        [HttpPost("approve/{commentId}")]
        public async Task<HbtApiResult<bool>> ApproveComment(
            [FromRoute] long commentId,
            [FromQuery] string? auditRemark = null)
        {
            try
            {
                var result = await _commentService.ApproveAsync(commentId, auditRemark);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "通过评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"通过评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 拒绝评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns>审核结果</returns>
        [HttpPost("reject/{commentId}")]
        public async Task<HbtApiResult<bool>> RejectComment(
            [FromRoute] long commentId,
            [FromQuery] string? auditRemark = null)
        {
            try
            {
                var result = await _commentService.RejectAsync(commentId, auditRemark);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "拒绝评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"拒绝评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 批量通过评论
        /// </summary>
        /// <param name="commentIds">评论ID集合</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns>审核结果</returns>
        [HttpPost("batch-approve")]
        public async Task<HbtApiResult<bool>> BatchApproveComments(
            [FromBody] long[] commentIds,
            [FromQuery] string? auditRemark = null)
        {
            try
            {
                var input = new HbtNewsCommentBatchAuditDto
                {
                    CommentIds = commentIds,
                    CommentStatus = 1, // 已通过
                    AuditRemark = auditRemark ?? "批量审核通过",
                    AuditType = 1 // 责任编辑审核
                };

                var result = await _commentService.BatchAuditAsync(input);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "批量通过评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"批量通过评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 批量拒绝评论
        /// </summary>
        /// <param name="commentIds">评论ID集合</param>
        /// <param name="auditRemark">审核备注</param>
        /// <returns>审核结果</returns>
        [HttpPost("batch-reject")]
        public async Task<HbtApiResult<bool>> BatchRejectComments(
            [FromBody] long[] commentIds,
            [FromQuery] string? auditRemark = null)
        {
            try
            {
                var input = new HbtNewsCommentBatchAuditDto
                {
                    CommentIds = commentIds,
                    CommentStatus = 2, // 已拒绝
                    AuditRemark = auditRemark ?? "批量审核拒绝",
                    AuditType = 1 // 责任编辑审核
                };

                var result = await _commentService.BatchAuditAsync(input);
                return new HbtApiResult<bool>
                {
                    Code = 200,
                    Msg = "批量拒绝评论成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"批量拒绝评论失败：{ex.Message}",
                    Data = false
                };
            }
        }

        /// <summary>
        /// 获取审核统计信息
        /// </summary>
        /// <returns>审核统计信息</returns>
        [HttpGet("statistics")]
        public async Task<HbtApiResult<AuditStatistics>> GetAuditStatistics()
        {
            try
            {
                var result = await _auditWorkflowService.GetAuditStatisticsAsync();
                return new HbtApiResult<AuditStatistics>
                {
                    Code = 200,
                    Msg = "获取审核统计信息成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<AuditStatistics>
                {
                    Code = 500,
                    Msg = $"获取审核统计信息失败：{ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获取审核员工作量统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>审核员工作量统计</returns>
        [HttpGet("auditor-workload")]
        public async Task<HbtApiResult<List<AuditorWorkload>>> GetAuditorWorkload(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _auditWorkflowService.GetAuditorWorkloadAsync(startDate, endDate);
                return new HbtApiResult<List<AuditorWorkload>>
                {
                    Code = 200,
                    Msg = "获取审核员工作量统计成功",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new HbtApiResult<List<AuditorWorkload>>
                {
                    Code = 500,
                    Msg = $"获取审核员工作量统计失败：{ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// 处理评论审核工作流
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns>处理结果</returns>
        [HttpPost("workflow/{commentId}")]
        public async Task<HbtApiResult<bool>> ProcessAuditWorkflow([FromRoute] long commentId)
        {
            try
            {
                var result = await _auditWorkflowService.ProcessCommentAuditWorkflowAsync(commentId);
                return result;
            }
            catch (Exception ex)
            {
                return new HbtApiResult<bool>
                {
                    Code = 500,
                    Msg = $"处理审核工作流失败：{ex.Message}",
                    Data = false
                };
            }
        }
    }
}