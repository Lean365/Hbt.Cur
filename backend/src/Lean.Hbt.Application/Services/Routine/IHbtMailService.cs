//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtMailService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 邮件服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 邮件服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtMailService
    {
        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件分页列表</returns>
        Task<HbtPagedResult<HbtMailDto>> GetListAsync(HbtMailQueryDto query);

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>邮件详情</returns>
        Task<HbtMailDto> GetByIdAsync(long mailId);

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件ID</returns>
        Task<long> CreateAsync(HbtMailCreateDto input);

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtMailUpdateDto input);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long mailId);

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="mailIds">邮件ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] mailIds);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>是否成功</returns>
        Task<bool> SendAsync(HbtMailSendDto input);

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>发送结果</returns>
        Task<(int success, int fail)> BatchSendAsync(List<HbtMailSendDto> inputs);

        /// <summary>
        /// 标记邮件为已读
        /// </summary>
        /// <param name="id">邮件ID</param>
        Task<bool> MarkAsReadAsync(long id);

        /// <summary>
        /// 标记所有邮件为已读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsReadAsync(long userId);

        /// <summary>
        /// 标记邮件为未读
        /// </summary>
        /// <param name="id">邮件ID</param>
        Task<bool> MarkAsUnreadAsync(long id);

        /// <summary>
        /// 标记所有邮件为未读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsUnreadAsync(long userId);

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtMailQueryDto query, string sheetName = "邮件信息");
    }
} 