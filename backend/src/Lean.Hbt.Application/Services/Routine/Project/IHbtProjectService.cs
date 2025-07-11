//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtProjectService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 项目服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine.Project
{
    /// <summary>
    /// 项目服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtProjectService
    {
        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>项目分页列表</returns>
        Task<HbtPagedResult<HbtProjectDto>> GetListAsync(HbtProjectQueryDto query);

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>项目详情</returns>
        Task<HbtProjectDto> GetByIdAsync(long projectId);

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>项目ID</returns>
        Task<long> CreateAsync(HbtProjectCreateDto input);

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtProjectUpdateDto input);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long projectId);

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="projectIds">项目ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] projectIds);

        /// <summary>
        /// 导入项目数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "项目信息");

        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtProjectQueryDto query, string sheetName = "项目信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "项目信息");
    }
} 