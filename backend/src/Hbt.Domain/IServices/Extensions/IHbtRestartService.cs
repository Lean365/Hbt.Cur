namespace Hbt.Domain.IServices.Extensions
{
    /// <summary>
    /// 系统重启服务接口
    /// </summary>
    public interface IHbtRestartService
    {
        /// <summary>
        /// 执行系统重启清理
        /// </summary>
        /// <returns>清理结果，true表示成功，false表示失败</returns>
        Task<bool> ExecuteRestartCleanupAsync();
    }
}