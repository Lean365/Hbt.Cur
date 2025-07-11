//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 系统配置服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtConfigService
    {
        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        Task<HbtPagedResult<HbtConfigDto>> GetListAsync(HbtConfigQueryDto query);

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <returns>系统配置详情</returns>
        Task<HbtConfigDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        Task<long> CreateAsync(HbtConfigCreateDto input);

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtConfigUpdateDto input);

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] configIds);

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtConfigQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtConfigStatusDto input);
    }
}