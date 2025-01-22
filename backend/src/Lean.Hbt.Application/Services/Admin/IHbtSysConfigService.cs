//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtSysConfigService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统配置服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Admin;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 系统配置服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtSysConfigService
    {
        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统配置分页列表</returns>
        Task<HbtPagedResult<HbtSysConfigDto>> GetPagedListAsync(HbtSysConfigQueryDto query);

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>系统配置详情</returns>
        Task<HbtSysConfigDto> GetAsync(long configId);

        /// <summary>
        /// 创建系统配置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>配置ID</returns>
        Task<long> InsertAsync(HbtSysConfigCreateDto input);

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtSysConfigUpdateDto input);

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long configId);

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="configIds">配置ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] configIds);

        /// <summary>
        /// 导入系统配置数据
        /// </summary>
        /// <param name="configs">系统配置数据列表</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(List<HbtSysConfigImportDto> configs);

        /// <summary>
        /// 导出系统配置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据列表</returns>
        Task<List<HbtSysConfigExportDto>> ExportAsync(HbtSysConfigQueryDto query);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        Task<HbtSysConfigTemplateDto> GetTemplateAsync();

        /// <summary>
        /// 更新系统配置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtSysConfigStatusDto input);
    }
} 