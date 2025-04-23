//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtFileService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 文件服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Application.Dtos.Routine;

namespace Lean.Hbt.Application.Services.Routine
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtFileService
    {
        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件分页列表</returns>
        Task<HbtPagedResult<HbtFileDto>> GetListAsync(HbtFileQueryDto query);

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件详情</returns>
        Task<HbtFileDto> GetByIdAsync(long fileId);

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件ID</returns>
        Task<long> CreateAsync(HbtFileCreateDto input);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtFileUpdateDto input);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long fileId);

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] fileIds);

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "文件信息");

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtFileQueryDto query, string sheetName = "文件信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "文件信息");

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="fileName">文件名</param>
        /// <returns>上传结果</returns>
        Task<HbtFileDto> UploadAsync(Stream file, string fileName);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件流</returns>
        Task<(Stream FileStream, string FileName, string ContentType)> DownloadAsync(long fileId);
    }
} 