//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtHistoryService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流历史服务接口
//===================================================================

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流历史服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtHistoryService
    {
        /// <summary>
        /// 获取工作流历史分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtHistoryDto>> GetListAsync(HbtHistoryQueryDto query);

        /// <summary>
        /// 获取工作流历史详情
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>工作流历史详情</returns>
        Task<HbtHistoryDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流历史
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流历史ID</returns>
        Task<long> CreateAsync(HbtHistoryCreateDto input);

        /// <summary>
        /// 更新工作流历史
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtHistoryUpdateDto input);

        /// <summary>
        /// 删除工作流历史
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流历史
        /// </summary>
        /// <param name="ids">工作流历史ID数组</param>
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
        Task<(string fileName, byte[] content)> ExportAsync(HbtHistoryQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流实例的历史记录
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <returns>历史记录列表</returns>
        Task<List<HbtHistoryDto>> GetHistoriesByWorkflowInstanceAsync(long InstanceId);

        /// <summary>
        /// 获取工作流节点的历史记录
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>历史记录列表</returns>
        Task<List<HbtHistoryDto>> GetHistoriesByWorkflowNodeAsync(long workflowNodeId);

        /// <summary>
        /// 获取用户的操作历史记录
        /// </summary>
        /// <param name="operatorId">操作人ID</param>
        /// <returns>历史记录列表</returns>
        Task<List<HbtHistoryDto>> GetHistoriesByOperatorAsync(long operatorId);

        /// <summary>
        /// 清理历史记录
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理数量</returns>
        Task<int> CleanupHistoriesAsync(int days);
    }
}