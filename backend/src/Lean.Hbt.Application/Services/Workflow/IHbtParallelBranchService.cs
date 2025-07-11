//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtParallelBranchService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流并行分支服务接口
//===================================================================
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流并行分支服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public interface IHbtParallelBranchService
    {
        /// <summary>
        /// 获取并行分支分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtParallelBranchDto>> GetListAsync(HbtParallelBranchQueryDto query);

        /// <summary>
        /// 获取并行分支详情
        /// </summary>
        /// <param name="id">并行分支ID</param>
        /// <returns>并行分支详情</returns>
        Task<HbtParallelBranchDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建并行分支
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的并行分支ID</returns>
        Task<long> CreateAsync(HbtParallelBranchCreateDto input);

        /// <summary>
        /// 更新并行分支
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtParallelBranchUpdateDto input);

        /// <summary>
        /// 删除并行分支
        /// </summary>
        /// <param name="id">并行分支ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除并行分支
        /// </summary>
        /// <param name="ids">并行分支ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入并行分支数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出并行分支数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtParallelBranchQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取并行分支导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");
    }
} 