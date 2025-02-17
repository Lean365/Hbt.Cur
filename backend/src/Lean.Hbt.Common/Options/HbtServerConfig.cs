//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtServerConfig.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 16:00
// 版本号 : V1.0.0
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
        /// 是否启用HTTPS
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
        /// 初始化选项
        /// </summary>
        public HbtInitOptions Init { get; set; } = new();
    }

    /// <summary>
    /// 初始化选项
    /// </summary>
    public class HbtInitOptions
    {
        /// <summary>
        /// 是否初始化数据库
        /// </summary>
        public bool InitDatabase { get; set; } = true;

        /// <summary>
        /// 是否初始化种子数据
        /// </summary>
        public bool InitSeedData { get; set; } = true;

        /// <summary>
        /// 是否启用Swagger
        /// </summary>
        public bool EnableSwagger { get; set; } = true;

        /// <summary>
        /// 是否启用CORS
        /// </summary>
        public bool EnableCors { get; set; } = true;
    }
} 