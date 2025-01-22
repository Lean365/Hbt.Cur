using System.Threading;

namespace Lean.Hbt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 租户上下文,用于存储当前租户信息
    /// </summary>
    public class HbtTenantContext
    {
        private static AsyncLocal<long?> _currentTenantId = new AsyncLocal<long?>();

        /// <summary>
        /// 获取当前租户ID
        /// </summary>
        public static long? CurrentTenantId
        {
            get => _currentTenantId.Value;
            set => _currentTenantId.Value = value;
        }

        /// <summary>
        /// 清除当前租户信息
        /// </summary>
        public static void Clear()
        {
            CurrentTenantId = null;
        }
    }
} 