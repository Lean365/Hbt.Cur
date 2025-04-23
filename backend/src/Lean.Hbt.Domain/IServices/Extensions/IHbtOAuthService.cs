namespace Lean.Hbt.Domain.IServices.Extensions
{
    /// <summary>
    /// OAuth服务接口
    /// </summary>
    public interface IHbtOAuthService
    {
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="provider">提供商</param>
        /// <param name="state">状态</param>
        /// <returns>授权地址</returns>
        Task<string> GetAuthorizationUrlAsync(string provider, string state);

        /// <summary>
        /// 处理OAuth回调
        /// </summary>
        /// <param name="provider">提供商</param>
        /// <param name="code">授权码</param>
        /// <param name="state">状态</param>
        /// <returns>用户信息</returns>
        Task<HbtOAuthUserInfo> HandleCallbackAsync(string provider, string code, string state);
    }

    /// <summary>
    /// OAuth用户信息
    /// </summary>
    public class HbtOAuthUserInfo
    {
        /// <summary>
        /// 提供商
        /// </summary>
        public string? Provider { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string? OpenId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
    }
}