//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtMailTplService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件模板服务接口
//===================================================================

using System.Threading.Tasks;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine.Email
{
    /// <summary>
    /// 邮件模板服务接口
    /// </summary>
    public interface IHbtMailTplService
    {
        /// <summary>
        /// 获取邮件模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件模板分页列表</returns>
        Task<HbtPagedResult<HbtMailTplDto>> GetListAsync(HbtMailTplQueryDto query);

        /// <summary>
        /// 获取邮件模板详情
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>邮件模板详情</returns>
        Task<HbtMailTplDto> GetByIdAsync(long tmplId);

        /// <summary>
        /// 创建邮件模板
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件模板ID</returns>
        Task<long> CreateAsync(HbtMailTplCreateDto input);

        /// <summary>
        /// 更新邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(long tmplId, HbtMailTplDto input);

        /// <summary>
        /// 删除邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long tmplId);

        /// <summary>
        /// 批量删除邮件模板
        /// </summary>
        /// <param name="tmplIds">模板ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] tmplIds);

        /// <summary>
        /// 导出邮件模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtMailTplQueryDto query, string sheetName = "邮件模板数据");
    }
} 