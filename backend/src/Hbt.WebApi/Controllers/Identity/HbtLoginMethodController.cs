//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtLoginMethodController.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录方式控制器
//===================================================================

using Hbt.Common.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hbt.WebApi.Controllers.Identity;

/// <summary>
/// 登录方式控制器
/// </summary>
/// <remarks>
/// 本控制器负责提供前端登录页面所需的配置信息，包括：
/// 1. 短信登录配置
/// 2. 二维码登录配置
/// 3. OAuth第三方登录配置
/// 4. 登录方式启用状态
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[ApiModule("identity", "身份认证")]
[AllowAnonymous]
public class HbtLoginMethodController : HbtBaseController
{
    private readonly HbtSmsAuthOptions _smsAuthOptions;
    private readonly HbtQrCodeAuthOptions _qrCodeAuthOptions;
    private readonly HbtOAuthOptions _oauthOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="smsAuthOptions">短信认证配置</param>
    /// <param name="qrCodeAuthOptions">二维码认证配置</param>
    /// <param name="oauthOptions">OAuth配置</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public HbtLoginMethodController(
        IOptions<HbtSmsAuthOptions> smsAuthOptions,
        IOptions<HbtQrCodeAuthOptions> qrCodeAuthOptions,
        IOptions<HbtOAuthOptions> oauthOptions,
        IHbtLogger logger,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization) : base(logger, currentUser, localization)
    {
        _smsAuthOptions = smsAuthOptions?.Value ?? throw new ArgumentNullException(nameof(smsAuthOptions));
        _qrCodeAuthOptions = qrCodeAuthOptions?.Value ?? throw new ArgumentNullException(nameof(qrCodeAuthOptions));
        _oauthOptions = oauthOptions?.Value ?? throw new ArgumentNullException(nameof(oauthOptions));
    }

    /// <summary>
    /// 获取登录配置信息
    /// </summary>
    /// <returns>登录配置信息</returns>
    [HttpGet("login-options")]
    [AllowAnonymous]
    public IActionResult GetLoginOptions()
    {
        try
        {
            _logger.Info($"获取登录配置: SmsAuth.Enabled={_smsAuthOptions.Enabled}, QrCodeAuth.Enabled={_qrCodeAuthOptions.Enabled}, OAuth.Enabled={_oauthOptions.Enabled}");
            _logger.Info($"二维码登录配置: QrCodeAuth.Enabled={_qrCodeAuthOptions.Enabled}");
            var loginOptions = new
            {
                // 密码登录（始终启用）
                PasswordLogin = new
                {
                    Enabled = true,
                    Name = "密码登录",
                    Key = "password"
                },
                
                // 短信登录
                SmsLogin = new
                {
                    Enabled = _smsAuthOptions.Enabled,
                    Name = "短信登录",
                    Key = "sms"
                },
                
                // 二维码登录
                QrCodeLogin = new
                {
                    Enabled = _qrCodeAuthOptions.Enabled,
                    Name = "二维码登录",
                    Key = "qrcode",
                    // 二维码登录的子选项
                    Options = new
                    {
                        WeChatLogin = new
                        {
                            Enabled = _qrCodeAuthOptions.EnableWeChatLogin,
                            Name = "微信扫码",
                            Key = "wechat"
                        },
                        AlipayLogin = new
                        {
                            Enabled = _qrCodeAuthOptions.EnableAlipayLogin,
                            Name = "支付宝扫码",
                            Key = "alipay"
                        }
                    }
                },
                
                // OAuth第三方登录
                OAuthLogin = new
                {
                    Enabled = _oauthOptions.Enabled,
                    Name = "第三方登录",
                    Key = "oauth",
                    // OAuth登录的子选项
                    Providers = new
                    {
                        GitHub = new
                        {
                            Enabled = _oauthOptions.GitHub.Enabled,
                            Name = "GitHub",
                            Key = "github",
                            Icon = "github"
                        },
                        Google = new
                        {
                            Enabled = _oauthOptions.Google.Enabled,
                            Name = "Google",
                            Key = "google",
                            Icon = "google"
                        },
                        Facebook = new
                        {
                            Enabled = _oauthOptions.Facebook.Enabled,
                            Name = "Facebook",
                            Key = "facebook",
                            Icon = "facebook"
                        },
                        Twitter = new
                        {
                            Enabled = _oauthOptions.Twitter.Enabled,
                            Name = "Twitter",
                            Key = "twitter",
                            Icon = "twitter"
                        },
                        QQ = new
                        {
                            Enabled = _oauthOptions.QQ.Enabled,
                            Name = "QQ",
                            Key = "qq",
                            Icon = "qq"
                        },
                        Microsoft = new
                        {
                            Enabled = _oauthOptions.Microsoft.Enabled,
                            Name = "Microsoft",
                            Key = "microsoft",
                            Icon = "microsoft"
                        },
                        Apple = new
                        {
                            Enabled = _oauthOptions.Apple.Enabled,
                            Name = "Apple",
                            Key = "apple",
                            Icon = "apple"
                        },
                        Amazon = new
                        {
                            Enabled = _oauthOptions.Amazon.Enabled,
                            Name = "Amazon",
                            Key = "amazon",
                            Icon = "amazon"
                        },
                        DingTalk = new
                        {
                            Enabled = _oauthOptions.DingTalk.Enabled,
                            Name = "钉钉",
                            Key = "dingtalk",
                            Icon = "dingtalk"
                        },
                        LinkedIn = new
                        {
                            Enabled = _oauthOptions.LinkedIn.Enabled,
                            Name = "LinkedIn",
                            Key = "linkedin",
                            Icon = "linkedin"
                        },
                        Weibo = new
                        {
                            Enabled = _oauthOptions.Weibo.Enabled,
                            Name = "微博",
                            Key = "weibo",
                            Icon = "weibo"
                        }
                    }
                }
            };

            return Success(loginOptions);
        }
        catch (Exception ex)
        {
            _logger.Error("获取登录配置信息失败", ex);
            return Error("获取登录配置信息失败");
        }
    }

    /// <summary>
    /// 获取启用的登录方式列表
    /// </summary>
    /// <returns>启用的登录方式列表</returns>
    [HttpGet("enabled-login-methods")]
    [AllowAnonymous]
    public IActionResult GetEnabledLoginMethods()
    {
        try
        {
            var enabledMethods = new List<object>();

            // 密码登录始终启用
            enabledMethods.Add(new
            {
                Key = "password",
                Name = "密码登录",
                Type = "password"
            });

            // 短信登录
            if (_smsAuthOptions.Enabled)
            {
                enabledMethods.Add(new
                {
                    Key = "sms",
                    Name = "短信登录",
                    Type = "sms"
                });
            }

            // 二维码登录 - 使用自己的配置
            _logger.Info($"二维码登录配置检查: QrCodeAuth.Enabled={_qrCodeAuthOptions.Enabled}, EnableWeChatLogin={_qrCodeAuthOptions.EnableWeChatLogin}, EnableAlipayLogin={_qrCodeAuthOptions.EnableAlipayLogin}");
            
            if (_qrCodeAuthOptions.Enabled)
            {
                var qrCodeOptions = new List<object>();
                
                // 使用QrCodeAuth自己的微信配置
                if (_qrCodeAuthOptions.EnableWeChatLogin)
                {
                    qrCodeOptions.Add(new
                    {
                        Key = "wechat",
                        Name = "微信扫码",
                        Type = "qrcode"
                    });
                    _logger.Info("添加微信扫码选项（基于QrCodeAuth.EnableWeChatLogin）");
                }
                
                // 使用QrCodeAuth自己的支付宝配置
                if (_qrCodeAuthOptions.EnableAlipayLogin)
                {
                    qrCodeOptions.Add(new
                    {
                        Key = "alipay",
                        Name = "支付宝扫码",
                        Type = "qrcode"
                    });
                    _logger.Info("添加支付宝扫码选项（基于QrCodeAuth.EnableAlipayLogin）");
                }

                // 只有当有具体的二维码选项时才显示二维码登录tab
                if (qrCodeOptions.Any())
                {
                    enabledMethods.Add(new
                    {
                        Key = "qrcode",
                        Name = "二维码登录",
                        Type = "qrcode",
                        Options = qrCodeOptions
                    });
                    _logger.Info($"二维码登录tab已添加，包含 {qrCodeOptions.Count} 个选项");
                }
                else
                {
                    _logger.Warn("二维码登录配置已启用，但没有启用的扫码选项");
                }
            }
            else
            {
                _logger.Info("二维码登录总开关未启用");
            }

            // OAuth第三方登录 - 根据具体节点的启用情况来判断
            if (_oauthOptions.Enabled)
            {
                var oauthProviders = new List<object>();
                
                if (_oauthOptions.GitHub.Enabled)
                    oauthProviders.Add(new { Key = "github", Name = "GitHub", Icon = "github" });
                if (_oauthOptions.Google.Enabled)
                    oauthProviders.Add(new { Key = "google", Name = "Google", Icon = "google" });
                if (_oauthOptions.Facebook.Enabled)
                    oauthProviders.Add(new { Key = "facebook", Name = "Facebook", Icon = "facebook" });
                if (_oauthOptions.Twitter.Enabled)
                    oauthProviders.Add(new { Key = "twitter", Name = "Twitter", Icon = "twitter" });
                if (_oauthOptions.QQ.Enabled)
                    oauthProviders.Add(new { Key = "qq", Name = "QQ", Icon = "qq" });
                if (_oauthOptions.Microsoft.Enabled)
                    oauthProviders.Add(new { Key = "microsoft", Name = "Microsoft", Icon = "microsoft" });
                if (_oauthOptions.Apple.Enabled)
                    oauthProviders.Add(new { Key = "apple", Name = "Apple", Icon = "apple" });
                if (_oauthOptions.Amazon.Enabled)
                    oauthProviders.Add(new { Key = "amazon", Name = "Amazon", Icon = "amazon" });
                if (_oauthOptions.DingTalk.Enabled)
                    oauthProviders.Add(new { Key = "dingtalk", Name = "钉钉", Icon = "dingtalk" });
                if (_oauthOptions.LinkedIn.Enabled)
                    oauthProviders.Add(new { Key = "linkedin", Name = "LinkedIn", Icon = "linkedin" });
                if (_oauthOptions.Weibo.Enabled)
                    oauthProviders.Add(new { Key = "weibo", Name = "微博", Icon = "weibo" });

                // 只有当有具体的OAuth提供商时才显示第三方登录tab
                if (oauthProviders.Any())
                {
                    enabledMethods.Add(new
                    {
                        Key = "oauth",
                        Name = "第三方登录",
                        Type = "oauth",
                        Providers = oauthProviders
                    });
                }
            }

            return Success(enabledMethods);
        }
        catch (Exception ex)
        {
            _logger.Error("获取启用的登录方式列表失败", ex);
            return Error("获取启用的登录方式列表失败");
        }
    }
} 