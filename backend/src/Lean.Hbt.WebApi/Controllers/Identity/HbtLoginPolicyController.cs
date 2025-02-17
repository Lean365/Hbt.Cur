using System.ComponentModel;
using System.Threading.Tasks;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.IServices.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lean.Hbt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 登录策略控制器
    /// </summary>
    [Route("api/login-policy")]
    [ApiController]
    [Authorize]
    public class HbtLoginPolicyController : HbtBaseController
    {
        private readonly ILogger<HbtLoginPolicyController> _logger;
        private readonly IHbtLoginPolicy _loginPolicy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtLoginPolicyController(
            ILogger<HbtLoginPolicyController> logger,
            IHbtLoginPolicy loginPolicy,
            IHbtLocalizationService localization) : base(localization)
        {
            _logger = logger;
            _loginPolicy = loginPolicy;
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>解锁结果</returns>
        [HttpPost("unlock/{userId}")]
        [Description("解锁用户")]
        public async Task<HbtApiResult<bool>> UnlockUserAsync(long userId)
        {
            _logger.LogInformation("正在解锁用户: {UserId}", userId);
            await _loginPolicy.RecordSuccessfulLoginAsync(userId.ToString());
            return HbtApiResult<bool>.Success(true);
        }

        /// <summary>
        /// 获取用户登录尝试剩余次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余尝试次数</returns>
        [HttpGet("attempts/{userName}")]
        [Description("获取用户登录尝试剩余次数")]
        public async Task<HbtApiResult<int>> GetRemainingAttemptsAsync(string userName)
        {
            _logger.LogInformation("正在获取用户登录尝试剩余次数: {UserName}", userName);
            var remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(userName);
            return HbtApiResult<int>.Success(remainingAttempts);
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>剩余锁定时间(秒)</returns>
        [HttpGet("lockout/{userName}")]
        [Description("获取账户锁定剩余时间")]
        public async Task<HbtApiResult<int>> GetLockoutRemainingSecondsAsync(string userName)
        {
            _logger.LogInformation("正在获取用户账户锁定剩余时间: {UserName}", userName);
            var remainingSeconds = await _loginPolicy.GetLockoutRemainingSecondsAsync(userName);
            return HbtApiResult<int>.Success(remainingSeconds);
        }
    }
} 