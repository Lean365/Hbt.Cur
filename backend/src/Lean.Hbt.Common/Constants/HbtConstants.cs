//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtConstants.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-05 10:00
// 版本号 : V1.0.0
// 描述    : 系统常量定义类
//===================================================================

namespace Lean.Hbt.Common.Constants
{
    /// <summary>
    /// 系统常量定义类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-05
    /// </remarks>
    public static class HbtConstants
    {
        /// <summary>
        /// 缓存相关常量
        /// </summary>
        public static class Cache
        {
            /// <summary>
            /// 缓存键前缀
            /// </summary>
            public const string KeyPrefix = "HbtCache:";

            /// <summary>
            /// 默认缓存过期时间(分钟)
            /// </summary>
            public const int DefaultExpiryMinutes = 30;
        }

        /// <summary>
        /// JWT相关常量
        /// </summary>
        public static class Jwt
        {
            /// <summary>
            /// Token类型
            /// </summary>
            public const string TokenType = "Bearer";

            /// <summary>
            /// 授权请求头名称
            /// </summary>
            public const string AuthorizationHeader = "Authorization";
        }

        /// <summary>
        /// 角色相关常量
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// 管理员角色
            /// </summary>
            public const string Admin = "Admin";

            /// <summary>
            /// 普通用户角色
            /// </summary>
            public const string User = "User";

            /// <summary>
            /// 访客角色
            /// </summary>
            public const string Guest = "Guest";
        }

        /// <summary>
        /// 声明类型常量
        /// </summary>
        public static class ClaimTypes
        {
            /// <summary>
            /// 用户ID声明
            /// </summary>
            public const string UserId = "uid";

            /// <summary>
            /// 用户名声明
            /// </summary>
            public const string UserName = "uname";

            /// <summary>
            /// 租户ID声明
            /// </summary>
            public const string TenantId = "tid";
        }

        /// <summary>
        /// 错误代码常量
        /// </summary>
        public static class ErrorCodes
        {
            /// <summary>
            /// 未授权错误
            /// </summary>
            public const string Unauthorized = "401";

            /// <summary>
            /// 禁止访问错误
            /// </summary>
            public const string Forbidden = "403";

            /// <summary>
            /// 资源未找到错误
            /// </summary>
            public const string NotFound = "404";

            /// <summary>
            /// 数据验证失败错误
            /// </summary>
            public const string ValidationFailed = "400";

            /// <summary>
            /// 服务器内部错误
            /// </summary>
            public const string ServerError = "500";
        }

        /// <summary>
        /// 日期时间格式常量
        /// </summary>
        public static class DateTimeFormats
        {
            /// <summary>
            /// 默认日期格式
            /// </summary>
            public const string DefaultDate = "yyyy-MM-dd";

            /// <summary>
            /// 默认日期时间格式
            /// </summary>
            public const string DefaultDateTime = "yyyy-MM-dd HH:mm:ss";

            /// <summary>
            /// 默认日期时间格式(含毫秒)
            /// </summary>
            public const string DefaultDateTimeWithMs = "yyyy-MM-dd HH:mm:ss.fff";
        }
    }
} 