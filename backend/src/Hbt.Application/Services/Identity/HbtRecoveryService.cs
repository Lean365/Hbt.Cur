//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtPasswordRecoveryService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 找回密码服务实现
//===================================================================

using Hbt.Application.Dtos.Identity;
using Hbt.Common.Exceptions;
using Hbt.Common.Helpers;
using Hbt.Common.Options;
using Hbt.Common.Utils;
using Hbt.Domain.Entities.Identity;
using Hbt.Domain.Repositories;
using Hbt.Domain.IServices.Security;
using Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Hbt.Application.Services.Identity
{
    /// <summary>
    /// 找回密码服务实现
    /// </summary>
    public class HbtRecoveryService : HbtBaseService, IHbtRecoveryService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;
        private readonly IHbtCaptchaService _captchaService;
        private readonly IHbtPasswordPolicy _passwordPolicy;
        private readonly IMemoryCache _memoryCache;
        private readonly HbtMailOption _mailOption;

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtRecoveryService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHbtCaptchaService captchaService,
            IHbtPasswordPolicy passwordPolicy,
            IMemoryCache memoryCache,
            IOptions<HbtMailOption> mailOption,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory;
            _captchaService = captchaService;
            _passwordPolicy = passwordPolicy;
            _memoryCache = memoryCache;
            _mailOption = mailOption.Value;
        }

        /// <summary>
        /// 验证用户身份
        /// </summary>
        public async Task<HbtIdentityVerificationResponse> VerifyIdentityAsync(HbtIdentityVerificationRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始验证用户身份: {UserName}", request.UserName);

                // 1. 验证验证码
                if (!await _captchaService.ValidateSliderAsync(request.CaptchaToken, request.Captcha))
                {
                    throw new HbtException(L("Identity.Auth.Captcha.Invalid"));
                }

                // 2. 查询用户是否存在
                var user = await UserRepository.GetFirstAsync(u => u.UserName == request.UserName);
                if (user == null)
                {
                    throw new HbtException(L("Identity.User.NotFound"));
                }

                // 3. 验证邮箱是否匹配
                if (user.Email != request.Email)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.EmailMismatch"));
                }

                // 4. 验证用户状态
                if (user.Status != 0)
                {
                    throw new HbtException(L("Identity.User.Disabled"));
                }

                // 5. 生成验证Token
                var verificationToken = GenerateVerificationToken();
                var cacheKey = $"password_recovery_verification_{verificationToken}";
                
                // 6. 缓存验证信息（10分钟有效期）
                var verificationData = new
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    ExpireTime = DateTime.UtcNow.AddMinutes(10)
                };
                
                _memoryCache.Set(cacheKey, verificationData, TimeSpan.FromMinutes(10));

                _logger.Info("[找回密码] 用户身份验证成功: {UserName}", request.UserName);

                return new HbtIdentityVerificationResponse
                {
                    Success = true,
                    VerificationToken = verificationToken,
                    Message = L("Identity.PasswordRecovery.IdentityVerified")
                };
            }
            catch (HbtException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 用户身份验证失败: {UserName}", request.UserName, ex);
                throw new HbtException(L("Identity.PasswordRecovery.IdentityVerificationFailed"));
            }
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        public async Task<HbtSendVerificationCodeResponse> SendVerificationCodeAsync(HbtSendVerificationCodeRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始发送验证码: {UserName}, {Email}", request.UserName, request.Email);

                // 1. 查询用户是否存在
                var user = await UserRepository.GetFirstAsync(u => u.UserName == request.UserName);
                if (user == null)
                {
                    throw new HbtException(L("Identity.User.NotFound"));
                }

                // 2. 验证邮箱是否匹配
                if (user.Email != request.Email)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.EmailMismatch"));
                }

                // 3. 生成验证码
                var verificationCode = GenerateVerificationCode();
                var cacheKey = $"password_recovery_code_{request.UserName}_{request.Email}";
                
                // 4. 检查是否在冷却期内
                if (_memoryCache.TryGetValue(cacheKey, out _))
                {
                    throw new HbtException(L("Identity.PasswordRecovery.CodeCooldown"));
                }

                // 5. 缓存验证码（10分钟有效期）
                _memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromMinutes(10));

                // 6. 发送邮件
                var emailSubject = L("Identity.PasswordRecovery.EmailSubject");
                var emailBody = GenerateVerificationEmailBody(user.UserName, verificationCode);
                
                var sendResult = await HbtMailHelper.SendHtmlAsync(
                    request.Email,
                    emailSubject,
                    emailBody
                );

                if (!sendResult)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.EmailSendFailed"));
                }

                _logger.Info("[找回密码] 验证码发送成功: {UserName}, {Email}", request.UserName, request.Email);

                return new HbtSendVerificationCodeResponse
                {
                    Success = true,
                    Message = L("Identity.PasswordRecovery.CodeSent"),
                    ExpirationMinutes = 10
                };
            }
            catch (HbtException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 发送验证码失败: {UserName}, {Email}", request.UserName, request.Email, ex);
                throw new HbtException(L("Identity.PasswordRecovery.CodeSendFailed"));
            }
        }

        /// <summary>
        /// 验证邮箱验证码
        /// </summary>
        public async Task<HbtEmailVerificationResponse> VerifyEmailCodeAsync(HbtEmailVerificationRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始验证邮箱验证码: {UserName}", request.UserName);

                // 1. 查询用户是否存在
                var user = await UserRepository.GetFirstAsync(u => u.UserName == request.UserName);
                if (user == null)
                {
                    throw new HbtException(L("Identity.User.NotFound"));
                }

                // 2. 验证邮箱是否匹配
                if (user.Email != request.Email)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.EmailMismatch"));
                }

                // 3. 验证验证码
                var cacheKey = $"password_recovery_code_{request.UserName}_{request.Email}";
                if (!_memoryCache.TryGetValue(cacheKey, out string? cachedCode) || cachedCode != request.VerificationCode)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.InvalidCode"));
                }

                // 4. 生成重置Token
                var resetToken = GenerateResetToken();
                var resetCacheKey = $"password_recovery_reset_{resetToken}";
                
                // 5. 缓存重置信息（30分钟有效期）
                var resetData = new
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    ExpireTime = DateTime.UtcNow.AddMinutes(30)
                };
                
                _memoryCache.Set(resetCacheKey, resetData, TimeSpan.FromMinutes(30));

                // 6. 清除验证码缓存
                _memoryCache.Remove(cacheKey);

                _logger.Info("[找回密码] 邮箱验证码验证成功: {UserName}", request.UserName);

                return new HbtEmailVerificationResponse
                {
                    Success = true,
                    ResetToken = resetToken,
                    Message = L("Identity.PasswordRecovery.EmailVerified")
                };
            }
            catch (HbtException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 邮箱验证码验证失败: {UserName}", request.UserName, ex);
                throw new HbtException(L("Identity.PasswordRecovery.EmailVerificationFailed"));
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        public async Task<HbtPasswordResetResponse> ResetPasswordAsync(HbtPasswordResetRequest request)
        {
            try
            {
                _logger.Info("[找回密码] 开始重置密码: {UserName}", request.UserName);

                // 1. 验证重置Token
                var resetCacheKey = $"password_recovery_reset_{request.ResetToken}";
                if (!_memoryCache.TryGetValue(resetCacheKey, out dynamic? resetData))
                {
                    throw new HbtException(L("Identity.PasswordRecovery.InvalidResetToken"));
                }

                // 2. 查询用户
                var user = await UserRepository.GetByIdAsync(resetData.UserId);
                if (user == null)
                {
                    throw new HbtException(L("Identity.User.NotFound"));
                }

                // 3. 验证用户名和邮箱是否匹配
                if (user.UserName != request.UserName || user.Email != request.Email)
                {
                    throw new HbtException(L("Identity.PasswordRecovery.InvalidUserInfo"));
                }

                // 4. 验证密码复杂度
                if (!_passwordPolicy.ValidatePasswordComplexity(request.NewPassword))
                {
                    throw new HbtException(L("Identity.User.Password.Invalid"));
                }

                // 5. 更新密码
                var (hash, salt, iterations) = HbtPasswordUtils.CreateHash(request.NewPassword);
                user.Password = hash;
                user.Salt = salt;
                user.Iterations = iterations;
                user.LastPasswordChangeTime = DateTime.Now;
                user.UpdateBy = user.UserName; // 用户自己重置密码
                user.UpdateTime = DateTime.Now;

                var result = await UserRepository.UpdateAsync(user);
                if (result <= 0)
                {
                    throw new HbtException(L("Common.UpdateFailed"));
                }

                // 6. 清除重置Token缓存
                _memoryCache.Remove(resetCacheKey);

                _logger.Info("[找回密码] 密码重置成功: {UserName}", request.UserName);

                return new HbtPasswordResetResponse
                {
                    Success = true,
                    Message = L("Identity.PasswordRecovery.PasswordResetSuccess")
                };
            }
            catch (HbtException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("[找回密码] 密码重置失败: {UserName}", request.UserName, ex);
                throw new HbtException(L("Identity.PasswordRecovery.PasswordResetFailed"));
            }
        }

        /// <summary>
        /// 生成验证Token
        /// </summary>
        private string GenerateVerificationToken()
        {
            return HbtUtils.GenerateGuid();
        }

        /// <summary>
        /// 生成重置Token
        /// </summary>
        private string GenerateResetToken()
        {
            return HbtUtils.GenerateGuid();
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// 生成验证邮件内容
        /// </summary>
        private string GenerateVerificationEmailBody(string userName, string verificationCode)
        {
            return $@"
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>密码找回验证码</title>
                </head>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #2c3e50;'>密码找回验证码</h2>
                        <p>尊敬的 {userName}：</p>
                        <p>您正在找回密码，验证码为：</p>
                        <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; text-align: center; margin: 20px 0;'>
                            <h1 style='color: #007bff; font-size: 32px; margin: 0; letter-spacing: 5px;'>{verificationCode}</h1>
                        </div>
                        <p><strong>验证码有效期：10分钟</strong></p>
                        <p>如果这不是您的操作，请忽略此邮件。</p>
                        <p>此邮件由系统自动发送，请勿回复。</p>
                        <hr style='border: none; border-top: 1px solid #eee; margin: 30px 0;'>
                        <p style='color: #666; font-size: 12px;'>Lean.Hbt 系统</p>
                    </div>
                </body>
                </html>";
        }
    }
} 