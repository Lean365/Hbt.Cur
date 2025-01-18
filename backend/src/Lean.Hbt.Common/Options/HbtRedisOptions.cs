//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtRedisOptions.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 17:45
// 版本号 : V1.0.0
// 描述    : Redis配置选项
//===================================================================

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// Redis配置选项
    /// </summary>
    public class HbtRedisOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 默认数据库
        /// </summary>
        public int DefaultDb { get; set; }

        /// <summary>
        /// 连接超时时间(毫秒)
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// 同步超时时间(毫秒)
        /// </summary>
        public int SyncTimeout { get; set; } = 5000;

        /// <summary>
        /// 是否允许管理员操作
        /// </summary>
        public bool AllowAdmin { get; set; }

        /// <summary>
        /// 是否使用SSL
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
} 