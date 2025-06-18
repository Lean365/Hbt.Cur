#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTransitionService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流转换服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流转换服务接口
    /// </summary>
    public interface IHbtTransitionService
    {
        /// <summary>
        /// 获取工作流转换列表
        /// </summary>
        /// <param name="DefinitionId">工作流定义ID</param>
        /// <returns>工作流转换列表</returns>
        Task<List<HbtTransitionDto>> GetListAsync(long DefinitionId);

        /// <summary>
        /// 获取工作流转换详情
        /// </summary>
        /// <param name="id">转换ID</param>
        /// <returns>工作流转换详情</returns>
        Task<HbtTransitionDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流转换
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>工作流转换ID</returns>
        Task<long> CreateAsync(HbtTransitionDto input);

        /// <summary>
        /// 更新工作流转换
        /// </summary>
        /// <param name="id">转换ID</param>
        /// <param name="input">更新参数</param>
        Task UpdateAsync(long id, HbtTransitionDto input);

        /// <summary>
        /// 删除工作流转换
        /// </summary>
        /// <param name="id">转换ID</param>
        Task DeleteAsync(long id);


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
        Task<(string fileName, byte[] content)> ExportAsync(HbtTransitionQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流任务导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");
    }
} 