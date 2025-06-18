//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : IHbtNodeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流节点服务接口
//===================================================================

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流节点服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtNodeService
    {
        /// <summary>
        /// 获取工作流节点分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtNodeDto>> GetListAsync(HbtNodeQueryDto query);

        /// <summary>
        /// 获取工作流节点详情
        /// </summary>
        /// <param name="id">工作流节点ID</param>
        /// <returns>工作流节点详情</returns>
        Task<HbtNodeDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流节点
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流节点ID</returns>
        Task<long> CreateAsync(HbtNodeCreateDto input);

        /// <summary>
        /// 更新工作流节点
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtNodeUpdateDto input);

        /// <summary>
        /// 删除工作流节点
        /// </summary>
        /// <param name="id">工作流节点ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流节点
        /// </summary>
        /// <param name="ids">工作流节点ID数组</param>
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
        Task<(string fileName, byte[] content)> ExportAsync(HbtNodeQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流定义的所有节点
        /// </summary>
        /// <param name="DefinitionId">工作流定义ID</param>
        /// <returns>节点列表</returns>
        Task<List<HbtNodeDto>> GetNodesByWorkflowDefinitionAsync(long DefinitionId);

        /// <summary>
        /// 获取节点的子节点列表
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <returns>子节点列表</returns>
        Task<List<HbtNodeDto>> GetChildNodesAsync(long nodeId);

        /// <summary>
        /// 更新节点排序
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="sort">排序号</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateSortAsync(long id, int sort);
    }
}