//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtPostService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位服务接口
//===================================================================

using System.Threading.Tasks;
using System.IO;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;

namespace Lean.Hbt.Application.Services.Identity
{
    /// <summary>
    /// 岗位服务接口
    /// </summary>
    public interface IHbtPostService
    {
        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>岗位列表</returns>
        Task<HbtPagedResult<HbtPostDto>> GetPagedListAsync(HbtPostQueryDto query);

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        /// <param name="id">岗位ID</param>
        /// <returns>岗位详情</returns>
        Task<HbtPostDto> GetAsync(long id);

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>岗位ID</returns>
        Task<long> InsertAsync(HbtPostCreateDto input);

        /// <summary>
        /// 更新岗位
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtPostUpdateDto input);

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="id">岗位ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        /// <param name="ids">岗位ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 获取岗位选项列表
        /// </summary>
        /// <returns>岗位选项列表</returns>
        Task<List<HbtSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 导入岗位数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入的岗位数据集合</returns>
        Task<List<HbtPostImportDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<byte[]> ExportAsync(HbtPostQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 生成岗位导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<byte[]> GenerateTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 更新岗位状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtPostStatusDto input);
    }
} 