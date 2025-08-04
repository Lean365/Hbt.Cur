//===================================================================
// 项目名 : Hbt.Common.Options
// 文件名 : HbtMailOption.cs
// 创建者 : Lean365
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 邮件配置选项
//===================================================================

namespace Hbt.Common.Options
{
    /// <summary>
    /// 邮件配置选项
    /// </summary>
    public class HbtMailOption
    {
        /// <summary>
        /// 配置节点
        /// </summary>
        public const string Position = "HbtMail";

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// 发件人显示名称
        /// </summary>
        public string FromName { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
} 