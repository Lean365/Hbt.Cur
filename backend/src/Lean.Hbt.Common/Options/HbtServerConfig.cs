//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 服务器配置选项
//===================================================================

namespace Lean.Hbt.Common.Options
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    public class HbtServerConfig
    {
        /// <summary>
        /// 是否使用HTTPS
        /// </summary>
        public bool UseHttps { get; set; }

        /// <summary>
        /// HTTP端口
        /// </summary>
        public int HttpPort { get; set; }

        /// <summary>
        /// HTTPS端口
        /// </summary>
        public int HttpsPort { get; set; }

        /// <summary>
        /// 是否启用Swagger
        /// </summary>
        public bool EnableSwagger { get; set; }

        /// <summary>
        /// 是否启用跨域
        /// </summary>
        public bool EnableCors { get; set; }
    }
} 