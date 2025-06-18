//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtTaskService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流任务服务接口
//===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流任务服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtProcessTaskService
    {
        /// <summary>
        /// 获取工作流任务分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtProcessTaskDto>> GetListAsync(HbtProcessTaskQueryDto query);

        /// <summary>
        /// 根据ID获取工作流任务详情
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <returns>工作流任务详情</returns>
        Task<HbtProcessTaskDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新的工作流任务
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流任务ID</returns>
        Task<long> CreateAsync(HbtTaskCreateDto input);

        /// <summary>
        /// 更新工作流任务信息
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtProcessTaskUpdateDto input);

        /// <summary>
        /// 删除指定工作流任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流任务
        /// </summary>
        /// <param name="ids">任务ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流任务数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出工作流任务数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtProcessTaskQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 更新工作流任务状态
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="status">新状态</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(long taskId, int status);

        /// <summary>
        /// 完成工作流任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="result">处理结果</param>
        /// <param name="comment">处理意见</param>
        /// <returns>是否成功</returns>
        Task<bool> CompleteAsync(long id, int result, string comment);

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="assigneeId">新处理人ID</param>
        /// <param name="comment">转办说明</param>
        /// <returns>是否成功</returns>
        Task<bool> TransferAsync(long id, long assigneeId, string comment);

        /// <summary>
        /// 退回工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">退回说明</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long id, string comment);

        /// <summary>
        /// 撤销工作流任务
        /// </summary>
        /// <param name="id">工作流任务ID</param>
        /// <param name="comment">撤销说明</param>
        /// <returns>是否成功</returns>
        Task<bool> CancelAsync(long id, string comment);

        /// <summary>
        /// 审批通过工作流任务
        /// </summary>
        Task<bool> ApproveTaskAsync(long taskId, string comment);

        /// <summary>
        /// 驳回工作流任务
        /// </summary>
        Task<bool> RejectTaskAsync(long taskId, string comment);

        /// <summary>
        /// 转办工作流任务
        /// </summary>
        Task<bool> TransferTaskAsync(long taskId, long assigneeId, string comment);

        /// <summary>
        /// 获取指定处理人的工作流任务列表
        /// </summary>
        Task<List<HbtProcessTaskDto>> GetTasksByAssigneeAsync(long assigneeId);
    }
} 