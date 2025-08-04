using Hbt.Cur.Common.Options;

namespace Hbt.Cur.Domain.IServices.Extensions
{
    /// <summary>
    /// 日志归档服务接口
    /// </summary>
    public interface IHbtLogArchiveService
    {
        /// <summary>
        /// 获取日志归档配置
        /// </summary>
        Task<HbtLogArchiveOptions> GetConfigAsync();

        /// <summary>
        /// 执行日志归档
        /// </summary>
        Task ArchiveAsync();

        /// <summary>
        /// 获取归档文件列表
        /// </summary>
        Task<List<string>> GetArchiveFilesAsync();

        /// <summary>
        /// 删除归档文件
        /// </summary>
        Task DeleteArchiveFileAsync(string fileName);
    }
}