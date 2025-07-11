//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtInstanceService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流实例服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtInstanceService
    {
        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtInstanceDto>> GetListAsync(HbtInstanceQueryDto query);

        /// <summary>
        /// 获取工作流实例详情
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>工作流实例详情</returns>
        Task<HbtInstanceDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流实例
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流实例ID</returns>
        Task<long> CreateAsync(HbtInstanceCreateDto input);

        /// <summary>
        /// 更新工作流实例
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtInstanceUpdateDto input);

        /// <summary>
        /// 删除工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        /// <param name="ids">工作流实例ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流实例
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入成功和失败的数量</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtInstance");

        /// <summary>
        /// 导出工作流实例
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceQueryDto query, string sheetName = "Instance");

        /// <summary>
        /// 获取工作流实例导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Instance");

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtInstanceStatusDto input);

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> SubmitAsync(long id);

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>是否成功</returns>
        Task<bool> WithdrawAsync(long id);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>是否成功</returns>
        Task<bool> TerminateAsync(long id, string reason);

        /// <summary>
        /// 获取工作流实例选项列表
        /// </summary>
        /// <returns>工作流实例选项列表</returns>
        Task<List<HbtSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 获取当前用户的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户的工作流实例列表</returns>
        Task<List<HbtInstanceDto>> GetCurrentUserInstancesAsync(int? status = null, int limit = 20);

        /// <summary>
        /// 获取当前用户发起的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户发起的工作流实例列表</returns>
        Task<List<HbtInstanceDto>> GetCurrentUserInitiatedInstancesAsync(int? status = null, int limit = 20);

        /// <summary>
        /// 获取当前用户参与的工作流实例
        /// </summary>
        /// <param name="status">状态筛选</param>
        /// <param name="limit">限制数量</param>
        /// <returns>当前用户参与的工作流实例列表</returns>
        Task<List<HbtInstanceDto>> GetCurrentUserParticipatedInstancesAsync(int? status = null, int limit = 20);

        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="input">启动参数</param>
        /// <returns>工作流实例ID</returns>
        Task<long> StartWorkflowAsync(HbtWorkflowStartDto input);

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        Task<bool> SuspendWorkflowAsync(long instanceId);

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作结果</returns>
        Task<bool> ResumeWorkflowAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态</returns>
        Task<object> GetWorkflowStatusAsync(long instanceId);

        /// <summary>
        /// 获取可用转换
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用转换列表</returns>
        Task<List<object>> GetAvailableTransitionsAsync(long instanceId);

        /// <summary>
        /// 执行工作流转换
        /// </summary>
        /// <param name="input">转换执行参数</param>
        /// <returns>转换结果</returns>
        Task<bool> ExecuteTransitionAsync(HbtTransitionExecuteDto input);

        /// <summary>
        /// 获取工作流仪表盘统计数据
        /// </summary>
        /// <returns>仪表盘统计数据</returns>
        Task<object> GetDashboardStatsAsync();

        /// <summary>
        /// 获取最近活动
        /// </summary>
        /// <returns>最近活动列表</returns>
        Task<List<object>> GetRecentActivitiesAsync();

        /// <summary>
        /// 获取用户流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态筛选</param>
        /// <returns>用户流程实例列表</returns>
        Task<List<HbtInstanceDto>> GetUserWorkflowsAsync(long userId, int? status = null);

        /// <summary>
        /// 获取用户发起的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户发起的流程实例列表</returns>
        Task<List<HbtInstanceDto>> GetUserInitiatedWorkflowsAsync(long userId);

        /// <summary>
        /// 获取用户参与的流程实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户参与的流程实例列表</returns>
        Task<List<HbtInstanceDto>> GetUserParticipatedWorkflowsAsync(long userId);
    }
} 