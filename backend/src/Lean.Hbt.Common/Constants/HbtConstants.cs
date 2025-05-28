//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtConstants.cs 
// 创建者 : Lean365
// 创建时间: 2024-02-18 02:30
// 版本号 : V1.0.0
// 描述    : 常量定义
//===================================================================

namespace Lean.Hbt.Common.Constants
{
    /// <summary>
    /// 系统常量
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-17
    /// </remarks>
    public static class HbtConstants
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public static class ErrorCodes
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const string Success = "200";

            /// <summary>
            /// 未授权
            /// </summary>
            public const string Unauthorized = "401";

            /// <summary>
            /// 禁止访问
            /// </summary>
            public const string Forbidden = "403";

            /// <summary>
            /// 未找到
            /// </summary>
            public const string NotFound = "404";

            /// <summary>
            /// 服务器错误
            /// </summary>
            public const string ServerError = "500";

            /// <summary>
            /// 无效的租户
            /// </summary>
            public const string InvalidTenant = "TENANT_001";

            /// <summary>
            /// 租户已停用
            /// </summary>
            public const string TenantDisabled = "TENANT_002";

            /// <summary>
            /// 租户不存在
            /// </summary>
            public const string TenantNotFound = "TENANT_003";

            /// <summary>
            /// 无租户访问权限
            /// </summary>
            public const string NoTenantAccess = "TENANT_004";

            /// <summary>
            /// 用户已停用
            /// </summary>
            public const string UserDisabled = "TENANT_005";

            /// <summary>
            /// 验证失败
            /// </summary>
            public const string ValidationFailed = "400";

            /// <summary>
            /// 验证码错误
            /// </summary>
            public const string InvalidCaptcha = "1001";

            /// <summary>
            /// 用户不属于租户
            /// </summary>
            public const string UserNotBelongToTenant = "1003";
        }

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public static class DateTimeFormats
        {
            /// <summary>
            /// 标准日期格式
            /// </summary>
            public const string StandardDate = "yyyy-MM-dd";

            /// <summary>
            /// 标准时间格式
            /// </summary>
            public const string StandardTime = "HH:mm:ss";

            /// <summary>
            /// 标准日期时间格式
            /// </summary>
            public const string StandardDateTime = "yyyy-MM-dd HH:mm:ss";

            /// <summary>
            /// 完整日期时间格式
            /// </summary>
            public const string FullDateTime = "yyyy-MM-dd HH:mm:ss.fff";
        }
    }
} 