//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.IServices.Security;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Application.Dtos.Identity;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Common.Exceptions;

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtLoginService : IHbtLoginService
{
    private readonly ILogger<HbtLoginService> _logger;
    private readonly IHbtRepository<HbtUser> _userRepository;
    private readonly IHbtRepository<HbtTenant> _tenantRepository;
    private readonly IHbtLoginPolicy _loginPolicy;
    private readonly IHbtCaptchaService _captchaService;
    private readonly IHbtJwtHandler _jwtHandler;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLoginService(
        ILogger<HbtLoginService> logger,
        IHbtRepository<HbtUser> userRepository,
        IHbtRepository<HbtTenant> tenantRepository,
        IHbtLoginPolicy loginPolicy,
        IHbtCaptchaService captchaService,
        IHbtJwtHandler jwtHandler)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _loginPolicy = loginPolicy;
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
    }

    /// <summary>
    /// 登录
    /// </summary>
    public async Task<HbtLoginResultDto> LoginAsync(HbtLoginDto loginDto)
    {
        // 1. 验证租户
        var tenant = await _tenantRepository.FirstOrDefaultAsync(x => x.Id == loginDto.TenantId);
        if (tenant == null)
            throw new HbtException($"租户不存在: {loginDto.TenantId}");
        if (tenant.Status != HbtStatus.Normal)
            throw new HbtException("租户已被禁用");

        // 2. 验证用户
        var user = await _userRepository.FirstOrDefaultAsync(x => 
            x.TenantId == loginDto.TenantId && 
            x.UserName == loginDto.UserName);
        if (user == null)
            throw new HbtException("用户名或密码错误");
        if (user.Status != HbtStatus.Normal)
            throw new HbtException("用户已被禁用");

        // 3. 验证登录策略
        if (!await _loginPolicy.ValidateLoginAttemptAsync(loginDto.UserName))
            throw new HbtException("账号已被锁定,请稍后再试");

        // 4. 验证验证码
        if (!string.IsNullOrEmpty(loginDto.CaptchaToken))
        {
            var captchaResult = await _captchaService.ValidateSliderAsync(loginDto.CaptchaToken, 0);
            if (!captchaResult)
                throw new HbtException("验证码验证失败");
        }

        // 5. 验证密码
        if (!HbtPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations))
        {
            await _loginPolicy.RecordFailedLoginAsync(loginDto.UserName);
            throw new HbtException("用户名或密码错误");
        }

        // 6. 生成令牌
        var accessToken = await _jwtHandler.CreateAccessToken(user.Id, user.UserName, new[] { "User" });
        var refreshToken = await _jwtHandler.CreateRefreshToken(user.Id);

        // 7. 记录登录成功
        await _loginPolicy.RecordSuccessfulLoginAsync(loginDto.UserName);

        // 8. 返回登录结果
        return new HbtLoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 7200,
            UserInfo = new HbtUserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName ?? string.Empty,
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
                Roles = new List<string> { "User" },
                Permissions = new List<string>()
            }
        };
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public async Task<HbtLoginResultDto> RefreshTokenAsync(string refreshToken)
    {
        // 1. 验证刷新令牌
        var userId = _jwtHandler.GetUserIdFromToken(refreshToken);
        if (userId <= 0)
            throw new HbtException("无效的刷新令牌");

        // 2. 获取用户信息
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new HbtException("用户不存在");
        if (user.Status != HbtStatus.Normal)
            throw new HbtException("用户已被禁用");

        // 3. 获取租户信息
        var tenant = await _tenantRepository.GetByIdAsync(user.TenantId);
        if (tenant == null)
            throw new HbtException("租户不存在");
        if (tenant.Status != HbtStatus.Normal)
            throw new HbtException("租户已被禁用");

        // 4. 生成新令牌
        var accessToken = await _jwtHandler.CreateAccessToken(user.Id, user.UserName, new[] { "User" });
        var newRefreshToken = await _jwtHandler.CreateRefreshToken(user.Id);

        // 5. 返回登录结果
        return new HbtLoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = 7200,
            UserInfo = new HbtUserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName ?? string.Empty,
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
                Roles = new List<string> { "User" },
                Permissions = new List<string>()
            }
        };
    }

    /// <summary>
    /// 登出
    /// </summary>
    public Task<bool> LogoutAsync(long userId)
    {
        // TODO: 清理用户会话
        return Task.FromResult(true);
    }
} 