//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOAuthService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : OAuth第三方登录服务实现
//===================================================================

using System.Web;
using Hbt.Cur.Application.Dtos.Identity;
using Hbt.Cur.Common.Models;
using Hbt.Cur.Common.Options;
using Hbt.Cur.Domain.Entities.Identity;
using Hbt.Cur.Domain.IServices.Extensions;
using Hbt.Cur.Domain.IServices.Security;
using Hbt.Cur.Domain.IServices.Caching;
using Hbt.Cur.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SqlSugar;

namespace Hbt.Cur.Application.Services.Identity;

/// <summary>
/// OAuth第三方登录服务实现
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtOAuthService : HbtBaseService, IHbtOAuthService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly HbtOAuthOptions _options;
    private readonly HttpClient _httpClient;
    private readonly IHbtJwtHandler _jwtHandler;
    private readonly IHbtMemoryCache _cache;
    private readonly HbtJwtOptions _jwtOptions;

    /// <summary>
    /// 获取用户仓储
    /// </summary>
    private IHbtRepository<HbtUser> UserRepository => _repositoryFactory.GetAuthRepository<HbtUser>();

    /// <summary>
    /// 获取OAuth账号仓储
    /// </summary>
    private IHbtRepository<HbtOAuth> OAuthRepository => _repositoryFactory.GetAuthRepository<HbtOAuth>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtOAuthService(
        IHbtLogger logger,
        IHbtRepositoryFactory repositoryFactory,
        IOptions<HbtOAuthOptions> options,
        HttpClient httpClient,
        IHbtJwtHandler jwtHandler,
        IHbtMemoryCache cache,
        IOptions<HbtJwtOptions> jwtOptions,
        IHbtCurrentUser currentUser,
        IHttpContextAccessor httpContextAccessor,
        IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _options = options.Value;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _jwtHandler = jwtHandler ?? throw new ArgumentNullException(nameof(jwtHandler));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _jwtOptions = jwtOptions.Value;
    }

    #region 基础CRUD操作

    /// <summary>
    /// 获取OAuth账号分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>OAuth账号分页列表</returns>
    public async Task<HbtPagedResult<HbtOAuthDto>> GetListAsync(HbtOAuthQueryDto query)
    {
        var exp = QueryExpression(query);

        var result = await OAuthRepository.GetPagedListAsync(
            exp,
            query.PageIndex,
            query.PageSize,
            x => x.Id,
            OrderByType.Desc);

        return new HbtPagedResult<HbtOAuthDto>
        {
            Rows = result.Rows.Adapt<List<HbtOAuthDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 根据ID获取OAuth账号详情
    /// </summary>
    /// <param name="id">OAuth账号ID</param>
    /// <returns>OAuth账号详情</returns>
    public async Task<HbtOAuthDto?> GetByIdAsync(long id)
    {
        var entity = await OAuthRepository.GetByIdAsync(id);
        if (entity == null)
            throw new HbtException(L("Identity.OAuth.NotFound"));

        return entity.Adapt<HbtOAuthDto>();
    }

    /// <summary>
    /// 创建OAuth账号
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>OAuth账号ID</returns>
    public async Task<long> CreateAsync(HbtOAuthCreateDto input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        // 检查是否已存在相同的OAuth账号
        var existingOAuth = await OAuthRepository.GetFirstAsync(x => 
            x.Provider == input.Provider && x.OAuthUserId == input.OAuthUserId);

        if (existingOAuth != null)
            throw new HbtException(L("Identity.OAuth.AlreadyExists"));

        var entity = input.Adapt<HbtOAuth>();
        entity.BindTime = DateTime.Now;
        entity.CreateBy = _currentUser.UserName;
        entity.CreateTime = DateTime.Now;

        var result = await OAuthRepository.CreateAsync(entity);
        if (result <= 0)
            throw new HbtException(L("Common.AddFailed"));

        return entity.Id;
    }

    /// <summary>
    /// 更新OAuth账号
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateAsync(HbtOAuthUpdateDto input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        var entity = await OAuthRepository.GetByIdAsync(input.OAuthId);
        if (entity == null)
            throw new HbtException(L("Identity.OAuth.NotFound"));

        // 检查是否已存在相同的OAuth账号（排除当前记录）
        var existingOAuth = await OAuthRepository.GetFirstAsync(x => 
            x.Provider == input.Provider && x.OAuthUserId == input.OAuthUserId && x.Id != input.OAuthId);

        if (existingOAuth != null)
            throw new HbtException(L("Identity.OAuth.AlreadyExists"));

        input.Adapt(entity);
        entity.UpdateBy = _currentUser.UserName;
        entity.UpdateTime = DateTime.Now;

        var result = await OAuthRepository.UpdateAsync(entity);
        if (result <= 0)
            throw new HbtException(L("Common.UpdateFailed"));

        return true;
    }

    /// <summary>
    /// 删除OAuth账号
    /// </summary>
    /// <param name="id">OAuth账号ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await OAuthRepository.GetByIdAsync(id);
        if (entity == null)
            throw new HbtException(L("Identity.OAuth.NotFound"));

        var result = await OAuthRepository.DeleteAsync(entity);
        if (result <= 0)
            throw new HbtException(L("Common.DeleteFailed"));

        return true;
    }

    /// <summary>
    /// 批量删除OAuth账号
    /// </summary>
    /// <param name="ids">OAuth账号ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            throw new HbtException(L("Identity.OAuth.SelectRequired"));

        var result = await OAuthRepository.DeleteRangeAsync(ids.Cast<object>().ToList());
        if (result <= 0)
            throw new HbtException(L("Common.DeleteFailed"));

        return true;
    }

    #endregion

    #region 导入导出操作

    /// <summary>
    /// 导入OAuth账号
    /// </summary>
    /// <param name="importList">导入列表</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int failed, List<string> errors)> ImportAsync(List<HbtOAuthImportDto> importList)
    {
        var success = 0;
        var failed = 0;
        var errors = new List<string>();

        try
        {
            foreach (var importItem in importList)
            {
                try
                {
                    // 检查是否已存在相同的OAuth账号
                    var existingOAuth = await OAuthRepository.GetFirstAsync(x => 
                        x.Provider == importItem.Provider && x.OAuthUserId == importItem.OAuthUserId);

                    if (existingOAuth != null)
                    {
                        failed++;
                        errors.Add($"第{success + failed}行：{L("Identity.OAuth.AlreadyExists")}");
                        continue;
                    }

                    var entity = importItem.Adapt<HbtOAuth>();
                    entity.BindTime = DateTime.Now;
                    entity.CreateBy = _currentUser.UserName;
                    entity.CreateTime = DateTime.Now;

                    await OAuthRepository.CreateAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    failed++;
                    errors.Add($"第{success + failed}行：{ex.Message}");
                }
            }

            return (success, failed, errors);
        }
        catch (Exception ex)
        {
            _logger.Error("导入OAuth账号失败", ex);
            throw new HbtException(L("Common.ImportFailed"));
        }
    }

    /// <summary>
    /// 导出OAuth账号
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出列表</returns>
    public async Task<List<HbtOAuthExportDto>> ExportAsync(HbtOAuthQueryDto query)
    {
        try
        {
            var exp = QueryExpression(query);
            var list = await OAuthRepository.GetListAsync(exp);

            // 获取用户信息
            var userIds = list.Select(x => x.UserId).Distinct().ToList();
            var users = await UserRepository.GetListAsync(u => userIds.Contains(u.Id));
            var userDict = users.ToDictionary(u => u.Id, u => u);

            // 转换为导出DTO
            var exportList = list.Select(x => new HbtOAuthExportDto
            {
                OAuthId = x.Id,
                UserId = x.UserId,
                UserName = userDict.TryGetValue(x.UserId, out var user) ? user.UserName : string.Empty,
                NickName = userDict.TryGetValue(x.UserId, out var user2) ? user2.NickName : string.Empty,
                Provider = x.Provider,
                OAuthUserId = x.OAuthUserId,
                OAuthUserName = x.OAuthUserName,
                OAuthNickName = x.OAuthNickName,
                OAuthEmail = x.OAuthEmail,
                OAuthAvatar = x.OAuthAvatar,
                BindTime = x.BindTime,
                IsPrimary = x.IsPrimary == 1 ? "是" : "否",
                Status = x.Status == 0 ? "正常" : "停用",
                CreateTime = x.CreateTime
            }).ToList();

            return exportList;
        }
        catch (Exception ex)
        {
            _logger.Error("导出OAuth账号失败", ex);
            throw new HbtException(L("Common.ExportFailed"));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>模板数据</returns>
    public async Task<List<HbtOAuthTemplateDto>> GetImportTemplateAsync()
    {
        try
        {
            // 返回示例数据作为模板
            var template = new List<HbtOAuthTemplateDto>
            {
                new HbtOAuthTemplateDto
                {
                    UserId = 1,
                    Provider = "GitHub",
                    OAuthUserId = "12345678",
                    OAuthUserName = "example_user",
                    OAuthNickName = "示例用户",
                    OAuthEmail = "example@github.com",
                    OAuthAvatar = "https://avatars.githubusercontent.com/u/12345678",
                    IsPrimary = 1,
                    Status = 0,
                    Remark = "示例数据"
                }
            };

            return await Task.FromResult(template);
        }
        catch (Exception ex)
        {
            _logger.Error("获取导入模板失败", ex);
            throw new HbtException(L("Common.GetTemplateFailed"));
        }
    }

    #endregion

    #region 状态管理操作

    /// <summary>
    /// 更新OAuth账号状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(HbtOAuthStatusDto input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        var entity = await OAuthRepository.GetByIdAsync(input.OAuthId);
        if (entity == null)
            throw new HbtException(L("Identity.OAuth.NotFound"));

        entity.Status = input.Status;
        entity.Remark = input.Remark;
        entity.UpdateBy = _currentUser.UserName;
        entity.UpdateTime = DateTime.Now;

        var result = await OAuthRepository.UpdateAsync(entity);
        if (result <= 0)
            throw new HbtException(L("Common.UpdateFailed"));

        return true;
    }

    /// <summary>
    /// 批量更新OAuth账号状态
    /// </summary>
    /// <param name="ids">OAuth账号ID集合</param>
    /// <param name="status">状态</param>
    /// <param name="remark">备注</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BatchUpdateStatusAsync(long[] ids, int status, string? remark = null)
    {
        if (ids == null || ids.Length == 0)
            throw new HbtException(L("Identity.OAuth.SelectRequired"));

        var entities = await OAuthRepository.GetListAsync(x => ids.Contains(x.Id));
        if (entities.Count != ids.Length)
            throw new HbtException(L("Identity.OAuth.PartNotFound"));

        foreach (var entity in entities)
        {
            entity.Status = status;
            entity.Remark = remark;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;
        }

        var result = await OAuthRepository.UpdateRangeAsync(entities);
        if (result <= 0)
            throw new HbtException(L("Common.UpdateFailed"));

        return true;
    }

    #endregion

    #region 绑定解绑操作

    /// <summary>
    /// 绑定OAuth账号（使用DTO）
    /// </summary>
    /// <param name="input">绑定对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BindOAuthAccountAsync(HbtOAuthBindDto input)
    {
        try
        {
            return await BindOAuthAccountAsync(input.UserId, input.Provider, input.OAuthUserId, input.OAuthUserInfo);
        }
        catch (Exception ex)
        {
            _logger.Error($"绑定OAuth账号失败，用户ID: {input.UserId}, 提供商: {input.Provider}", ex);
            throw new HbtException(L("Identity.OAuth.BindFailed"));
        }
    }

    /// <summary>
    /// 解绑OAuth账号（使用DTO）
    /// </summary>
    /// <param name="input">解绑对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UnbindOAuthAccountAsync(HbtOAuthUnbindDto input)
    {
        try
        {
            return await UnbindOAuthAccountAsync(input.UserId, input.Provider);
        }
        catch (Exception ex)
        {
            _logger.Error($"解绑OAuth账号失败，用户ID: {input.UserId}, 提供商: {input.Provider}", ex);
            throw new HbtException(L("Identity.OAuth.UnbindFailed"));
        }
    }

    /// <summary>
    /// 绑定OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="provider">提供商名称</param>
    /// <param name="oauthUserId">OAuth用户ID</param>
    /// <param name="oauthUserInfo">OAuth用户信息</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BindOAuthAccountAsync(long userId, string provider, string oauthUserId, object oauthUserInfo)
    {
        try
        {
            var existingAccount = await OAuthRepository.GetFirstAsync(x => 
                x.Provider == provider && x.OAuthUserId == oauthUserId);

            if (existingAccount != null)
            {
                return false; // 该OAuth账号已被绑定
            }

            var account = new HbtOAuth
            {
                UserId = userId,
                Provider = provider,
                OAuthUserId = oauthUserId,
                OAuthUserName = GetOAuthUserName(oauthUserInfo),
                BindTime = DateTime.Now,
                IsPrimary = 0,
                CreateBy = _currentUser.UserName,
                CreateTime = DateTime.Now
            };

            await OAuthRepository.CreateAsync(account);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"绑定OAuth账号失败，用户ID: {userId}, 提供商: {provider}", ex);
            return false;
        }
    }

    /// <summary>
    /// 解绑OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="provider">提供商名称</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UnbindOAuthAccountAsync(long userId, string provider)
    {
        try
        {
            var account = await OAuthRepository.GetFirstAsync(x => 
                x.UserId == userId && x.Provider == provider);

            if (account == null)
            {
                return false;
            }

            await OAuthRepository.DeleteAsync(account);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"解绑OAuth账号失败，用户ID: {userId}, 提供商: {provider}", ex);
            return false;
        }
    }

    #endregion

    /// <summary>
    /// 构建OAuth查询条件
    /// </summary>
    private static Expression<Func<HbtOAuth, bool>> QueryExpression(HbtOAuthQueryDto query)
    {
        var exp = Expressionable.Create<HbtOAuth>();

        if (query.UserId.HasValue)
            exp.And(x => x.UserId == query.UserId.Value);

        if (!string.IsNullOrEmpty(query.Provider))
            exp.And(x => x.Provider.Contains(query.Provider));

        if (!string.IsNullOrEmpty(query.OAuthUserId))
            exp.And(x => x.OAuthUserId.Contains(query.OAuthUserId));

        if (!string.IsNullOrEmpty(query.OAuthUserName))
            exp.And(x => x.OAuthUserName.Contains(query.OAuthUserName));

        if (query.IsPrimary.HasValue)
            exp.And(x => x.IsPrimary == query.IsPrimary.Value);

        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status.Value);

        if (query.BindTimeStart.HasValue)
            exp.And(x => x.BindTime >= query.BindTimeStart.Value);

        if (query.BindTimeEnd.HasValue)
            exp.And(x => x.BindTime <= query.BindTimeEnd.Value);

        return exp.ToExpression();
    }

    #region OAuth登录相关操作

    /// <summary>
    /// 获取支持的登录提供商列表
    /// </summary>
    /// <returns>提供商列表</returns>
    public async Task<List<HbtOAuthProviderDto>> GetProvidersAsync()
    {
        var providers = new List<HbtOAuthProviderDto>();

        if (_options.GitHub.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "GitHub",
                DisplayName = "GitHub",
                Enabled = true,
                IconUrl = "/images/oauth/github.png",
                Scope = _options.GitHub.Scope
            });
        }

        if (_options.Google.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Google",
                DisplayName = "Google",
                Enabled = true,
                IconUrl = "/images/oauth/google.png",
                Scope = _options.Google.Scope
            });
        }

        if (_options.Facebook.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Facebook",
                DisplayName = "Facebook",
                Enabled = true,
                IconUrl = "/images/oauth/facebook.png",
                Scope = _options.Facebook.Scope
            });
        }

        if (_options.Twitter.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Twitter",
                DisplayName = "Twitter",
                Enabled = true,
                IconUrl = "/images/oauth/twitter.png",
                Scope = _options.Twitter.Scope
            });
        }

        if (_options.QQ.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "QQ",
                DisplayName = "QQ",
                Enabled = true,
                IconUrl = "/images/oauth/qq.png",
                Scope = _options.QQ.Scope
            });
        }



        if (_options.Microsoft.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Microsoft",
                DisplayName = "Microsoft",
                Enabled = true,
                IconUrl = "/images/oauth/microsoft.png",
                Scope = _options.Microsoft.Scope
            });
        }

        if (_options.Apple.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Apple",
                DisplayName = "Apple",
                Enabled = true,
                IconUrl = "/images/oauth/apple.png",
                Scope = _options.Apple.Scope
            });
        }

        if (_options.Amazon.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Amazon",
                DisplayName = "Amazon",
                Enabled = true,
                IconUrl = "/images/oauth/amazon.png",
                Scope = _options.Amazon.Scope
            });
        }

        if (_options.DingTalk.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "DingTalk",
                DisplayName = "钉钉",
                Enabled = true,
                IconUrl = "/images/oauth/dingtalk.png",
                Scope = _options.DingTalk.Scope
            });
        }

        if (_options.LinkedIn.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "LinkedIn",
                DisplayName = "LinkedIn",
                Enabled = true,
                IconUrl = "/images/oauth/linkedin.png",
                Scope = _options.LinkedIn.Scope
            });
        }

        if (_options.Weibo.Enabled)
        {
            providers.Add(new HbtOAuthProviderDto
            {
                Name = "Weibo",
                DisplayName = "微博",
                Enabled = true,
                IconUrl = "/images/oauth/weibo.png",
                Scope = _options.Weibo.Scope
            });
        }

        return await Task.FromResult(providers);
    }

    /// <summary>
    /// 开始OAuth授权流程
    /// </summary>
    /// <param name="provider">提供商名称</param>
    /// <param name="redirectUri">回调地址</param>
    /// <returns>授权URL</returns>
    public async Task<string> GetAuthorizationUrlAsync(string provider, string? redirectUri = null)
    {
        if (!_options.Enabled)
            throw new InvalidOperationException("OAuth is disabled");

        var providerOptions = GetProviderOptions(provider);
        if (providerOptions == null)
            throw new InvalidOperationException($"OAuth provider '{provider}' not found or not enabled");

        var state = Guid.NewGuid().ToString();
        var finalRedirectUri = redirectUri ?? GetRedirectUri(providerOptions);

        var queryParams = new Dictionary<string, string>
        {
            ["client_id"] = GetClientId(providerOptions),
            ["redirect_uri"] = finalRedirectUri,
            ["response_type"] = "code",
            ["scope"] = GetScope(providerOptions),
            ["state"] = state
        };

        var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}"));
        return await Task.FromResult($"{GetAuthorizationUrl(providerOptions)}?{queryString}");
    }

    /// <summary>
    /// 处理OAuth回调
    /// </summary>
    /// <param name="provider">提供商名称</param>
    /// <param name="code">授权码</param>
    /// <param name="state">状态参数</param>
    /// <returns>登录结果</returns>
    public async Task<HbtLoginResultDto> HandleCallbackAsync(string provider, string code, string state)
    {
        if (!_options.Enabled)
            throw new InvalidOperationException("OAuth is disabled");

        var providerOptions = GetProviderOptions(provider);
        if (providerOptions == null)
            throw new InvalidOperationException($"OAuth provider '{provider}' not found or not enabled");

        try
        {
            // 获取访问令牌
            var accessToken = await GetAccessTokenAsync(provider, providerOptions, code);

            // 获取用户信息
            var userInfoJson = await GetUserInfoAsync(provider, providerOptions, accessToken);

            var oauthUserId = GetOAuthUserId(userInfoJson, provider);
            var oauthUserInfo = GetOAuthUserInfo(userInfoJson, provider, oauthUserId);

            // 查找或创建用户
            var user = await FindOrCreateUserAsync(oauthUserId, provider, oauthUserInfo);
            if (user == null)
            {
                return new HbtLoginResultDto
                {
                    Success = false
                };
            }

            // 获取用户角色和权限
            var roles = await GetUserRolesAsync(user.Id);
            var permissions = await GetUserPermissionsAsync(user.Id);

            // 生成令牌
            var jwtAccessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

            // 缓存刷新令牌
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));

            _logger.Info("OAuth登录成功: Provider={Provider}, UserId={UserId}", provider, user.Id);

            return new HbtLoginResultDto
            {
                Success = true,
                AccessToken = jwtAccessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
                UserInfo = new HbtUserInfoDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName ?? string.Empty,
                    EnglishName = user.EnglishName ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    RealName = user.RealName ?? string.Empty,
                    UserType = user.UserType,
                    Avatar = user.Avatar ?? string.Empty,
                    Roles = roles,
                    Permissions = permissions
                }
            };
        }
        catch (Exception ex)
        {
            _logger.Error("OAuth callback processing failed for provider: {Provider}", provider, ex);
            return new HbtLoginResultDto
            {
                Success = false
            };
        }
    }

    /// <summary>
    /// 获取用户绑定的OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>绑定的OAuth账号列表</returns>
    public async Task<List<HbtOAuthAccountDto>> GetUserOAuthAccountsAsync(long userId)
    {
        try
        {
            var accounts = await OAuthRepository.GetListAsync(x => x.UserId == userId);
            return accounts.Select(x => new HbtOAuthAccountDto
            {
                Provider = x.Provider,
                OAuthUserId = x.OAuthUserId,
                OAuthUserName = x.OAuthUserName,
                BindTime = x.BindTime,
                IsPrimary = x.IsPrimary == 1
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to get OAuth accounts for user: {UserId}", userId, ex);
            return new List<HbtOAuthAccountDto>();
        }
    }

    #region 私有方法

    /// <summary>
    /// 获取提供商配置
    /// </summary>
    private object? GetProviderOptions(string provider)
    {
        return provider.ToLower() switch
        {
            "github" => _options.GitHub.Enabled ? _options.GitHub : null,
            "google" => _options.Google.Enabled ? _options.Google : null,
            "facebook" => _options.Facebook.Enabled ? _options.Facebook : null,
            "twitter" => _options.Twitter.Enabled ? _options.Twitter : null,
            "qq" => _options.QQ.Enabled ? _options.QQ : null,
            "weibo" => _options.Weibo.Enabled ? _options.Weibo : null,
                            "microsoft" => _options.Microsoft.Enabled ? _options.Microsoft : null,
                "apple" => _options.Apple.Enabled ? _options.Apple : null,
                "amazon" => _options.Amazon.Enabled ? _options.Amazon : null,
                "dingtalk" => _options.DingTalk.Enabled ? _options.DingTalk : null,
                "linkedin" => _options.LinkedIn.Enabled ? _options.LinkedIn : null,
            _ => null
        };
    }

    /// <summary>
    /// 查找或创建用户
    /// </summary>
    private async Task<HbtUser?> FindOrCreateUserAsync(string oauthUserId, string provider, object oauthUserInfo)
    {
        // 先查找是否已有绑定的用户
        var existingAccount = await OAuthRepository.GetFirstAsync(x => 
            x.Provider == provider && x.OAuthUserId == oauthUserId);

        if (existingAccount != null)
        {
            var existingUser = await UserRepository.GetByIdAsync(existingAccount.UserId);
            return existingUser;
        }

        // 创建新用户
        var userName = GetOAuthUserName(oauthUserInfo);
        var email = GetOAuthEmail(oauthUserInfo);

        var newUser = new HbtUser
        {
            UserName = $"{provider}_{oauthUserId}",
            NickName = userName,
            Email = email,
            Avatar = GetOAuthAvatar(oauthUserInfo),
            Status = 1,
            UserType = 3, // OAuth用户类型
            CreateTime = DateTime.Now
        };

        await UserRepository.CreateAsync(newUser);

        // 绑定OAuth账号
        var account = new HbtOAuth
        {
            UserId = newUser.Id,
            Provider = provider,
            OAuthUserId = oauthUserId,
            OAuthUserName = userName,
            BindTime = DateTime.Now,
            IsPrimary = 1
        };

        await OAuthRepository.CreateAsync(account);
        return newUser;
    }

    /// <summary>
    /// 获取访问令牌
    /// </summary>
    private async Task<string> GetAccessTokenAsync(string provider, object providerOptions, string code)
    {
        // 微博需要特殊的令牌获取逻辑
        if (provider.ToLower() == "weibo")
        {
            return await GetWeiboAccessTokenAsync(providerOptions, code);
        }

        // 其他提供商使用标准OAuth流程
        var tokenResponse = await _httpClient.PostAsync(GetTokenUrl(providerOptions), new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["client_id"] = GetClientId(providerOptions),
                ["client_secret"] = GetClientSecret(providerOptions),
                ["code"] = code,
                ["redirect_uri"] = GetRedirectUri(providerOptions),
                ["grant_type"] = "authorization_code"
            }));

        tokenResponse.EnsureSuccessStatusCode();
        var tokenJson = JObject.Parse(await tokenResponse.Content.ReadAsStringAsync());
        return tokenJson["access_token"].ToString();
    }

    /// <summary>
    /// 获取微博访问令牌
    /// </summary>
    private async Task<string> GetWeiboAccessTokenAsync(object providerOptions, string code)
    {
        var tokenResponse = await _httpClient.PostAsync(GetTokenUrl(providerOptions), new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["client_id"] = GetClientId(providerOptions),
                ["client_secret"] = GetClientSecret(providerOptions),
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = GetRedirectUri(providerOptions)
            }));

        tokenResponse.EnsureSuccessStatusCode();
        var responseText = await tokenResponse.Content.ReadAsStringAsync();
        
        // 微博返回的是URL编码格式，需要解析
        var queryParams = HttpUtility.ParseQueryString(responseText);
        var accessToken = queryParams["access_token"];
        
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("Failed to get Weibo access token");
        }
        
        return accessToken;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    private async Task<JObject> GetUserInfoAsync(string provider, object providerOptions, string accessToken)
    {
        // 微博需要特殊的用户信息获取逻辑
        if (provider.ToLower() == "weibo")
        {
            return await GetWeiboUserInfoAsync(providerOptions, accessToken);
        }

        // 其他提供商使用标准OAuth流程
        var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, GetUserInfoUrl(providerOptions));
        userInfoRequest.Headers.Add("Authorization", $"Bearer {accessToken}");
        var userInfoResponse = await _httpClient.SendAsync(userInfoRequest);
        userInfoResponse.EnsureSuccessStatusCode();
        return JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// 获取微博用户信息
    /// </summary>
    private async Task<JObject> GetWeiboUserInfoAsync(object providerOptions, string accessToken)
    {
        var userInfoUrl = $"{GetUserInfoUrl(providerOptions)}?access_token={accessToken}&uid={await GetWeiboUidAsync(providerOptions, accessToken)}";
        var userInfoResponse = await _httpClient.GetAsync(userInfoUrl);
        userInfoResponse.EnsureSuccessStatusCode();
        return JObject.Parse(await userInfoResponse.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// 获取微博用户UID
    /// </summary>
    private async Task<string> GetWeiboUidAsync(object providerOptions, string accessToken)
    {
        var uidUrl = $"https://api.weibo.com/2/account/get_uid.json?access_token={accessToken}";
        var uidResponse = await _httpClient.GetAsync(uidUrl);
        uidResponse.EnsureSuccessStatusCode();
        var uidJson = JObject.Parse(await uidResponse.Content.ReadAsStringAsync());
                return uidJson["uid"]?.ToString() ?? "";
    }    /// <summary>
    /// 获取OAuth用户信息
    /// </summary>
    private object GetOAuthUserInfo(JObject userInfoJson, string provider, string oauthUserId)
    {
        return provider.ToLower() switch
        {
            "github" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["login"]?.ToString() ?? userInfoJson["name"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["avatar_url"]?.ToString() ?? userInfoJson["picture"]?.ToString()
            },
            "google" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["email"]?.ToString() ?? userInfoJson["name"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["picture"]?.ToString()
            },
            "facebook" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["email"]?.ToString() ?? userInfoJson["name"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["picture"]?.ToString()
            },
            "twitter" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["data"]?["username"]?.ToString() ?? userInfoJson["data"]?["name"]?.ToString(),
                NickName = userInfoJson["data"]?["name"]?.ToString(),
                Email = userInfoJson["data"]?["email"]?.ToString(),
                Avatar = userInfoJson["data"]?["profile_image_url"]?.ToString()
            },
            "qq" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["nickname"]?.ToString(),
                NickName = userInfoJson["nickname"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["figureurl_qq_1"]?.ToString() ?? userInfoJson["figureurl_qq_2"]?.ToString()
            },
            "weibo" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["screen_name"]?.ToString(),
                NickName = userInfoJson["screen_name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["avatar_large"]?.ToString() ?? userInfoJson["profile_image_url"]?.ToString()
            },
            "microsoft" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["userPrincipalName"]?.ToString() ?? userInfoJson["displayName"]?.ToString(),
                NickName = userInfoJson["displayName"]?.ToString(),
                Email = userInfoJson["mail"]?.ToString() ?? userInfoJson["userPrincipalName"]?.ToString(),
                Avatar = userInfoJson["photo"]?.ToString()
            },
            "apple" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["sub"]?.ToString() ?? userInfoJson["email"]?.ToString(),
                NickName = userInfoJson["name"]?["firstName"]?.ToString() + " " + userInfoJson["name"]?["lastName"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = ""
            },
            "amazon" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["user_id"]?.ToString() ?? userInfoJson["email"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = ""
            },
            "dingtalk" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["userid"]?.ToString() ?? userInfoJson["name"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["avatar"]?.ToString()
            },
            "linkedin" => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["id"]?.ToString() ?? userInfoJson["localizedFirstName"]?.ToString(),
                NickName = userInfoJson["localizedFirstName"]?.ToString() + " " + userInfoJson["localizedLastName"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = ""
            },
            _ => new
            {
                Provider = provider,
                OAuthUserId = oauthUserId,
                UserName = userInfoJson["login"]?.ToString() ?? userInfoJson["name"]?.ToString(),
                NickName = userInfoJson["name"]?.ToString(),
                Email = userInfoJson["email"]?.ToString(),
                Avatar = userInfoJson["avatar_url"]?.ToString() ?? userInfoJson["picture"]?.ToString()
            }
        };
    }

    /// <summary>
    /// 获取OAuth用户ID
    /// </summary>
    private string GetOAuthUserId(JObject userInfoJson, string provider)
    {
        return provider.ToLower() switch
        {
            "github" => userInfoJson["id"]?.ToString() ?? "",
            "google" => userInfoJson["sub"]?.ToString() ?? "",
            "facebook" => userInfoJson["id"]?.ToString() ?? "",
            "twitter" => userInfoJson["data"]?["id"]?.ToString() ?? "",
            "qq" => userInfoJson["openid"]?.ToString() ?? "",
            "weibo" => userInfoJson["id"]?.ToString() ?? "",
            "microsoft" => userInfoJson["id"]?.ToString() ?? "",
            "apple" => userInfoJson["sub"]?.ToString() ?? "",
            "amazon" => userInfoJson["user_id"]?.ToString() ?? "",
            "dingtalk" => userInfoJson["userid"]?.ToString() ?? "",
            "linkedin" => userInfoJson["id"]?.ToString() ?? "",
            _ => ""
        };
    }

    /// <summary>
    /// 获取OAuth用户名
    /// </summary>
    private string GetOAuthUserName(object oauthUserInfo)
    {
        // 通过反射获取用户名
        var type = oauthUserInfo.GetType();
        var userNameProperty = type.GetProperty("UserName") ?? type.GetProperty("NickName");
        return userNameProperty?.GetValue(oauthUserInfo)?.ToString() ?? "";
    }

    /// <summary>
    /// 获取OAuth用户邮箱
    /// </summary>
    private string GetOAuthEmail(object oauthUserInfo)
    {
        var type = oauthUserInfo.GetType();
        var emailProperty = type.GetProperty("Email");
        return emailProperty?.GetValue(oauthUserInfo)?.ToString() ?? "";
    }

    /// <summary>
    /// 获取OAuth用户头像
    /// </summary>
    private string GetOAuthAvatar(object oauthUserInfo)
    {
        var type = oauthUserInfo.GetType();
        var avatarProperty = type.GetProperty("Avatar");
        return avatarProperty?.GetValue(oauthUserInfo)?.ToString() ?? "";
    }

    /// <summary>
    /// 获取用户角色
    /// </summary>
    private async Task<List<string>> GetUserRolesAsync(long userId)
    {
        try
        {
            return await UserRepository.GetUserRolesAsync(userId);
        }
        catch
        {
            return new List<string>();
        }
    }

    /// <summary>
    /// 获取用户权限
    /// </summary>
    private async Task<List<string>> GetUserPermissionsAsync(long userId)
    {
        try
        {
            return await UserRepository.GetUserPermissionsAsync(userId);
        }
        catch
        {
            return new List<string>();
        }
    }

    private string GetTokenUrl(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.TokenUrl,
            GoogleOptions o => o.TokenUrl,
            FacebookOptions o => o.TokenUrl,
            TwitterOptions o => o.TokenUrl,
            QQOptions o => o.TokenUrl,
            WeiboOptions o => o.TokenUrl,
            MicrosoftOptions o => o.TokenUrl,
            AppleOptions o => o.TokenUrl,
            AmazonOptions o => o.TokenUrl,
            DingTalkOptions o => o.TokenUrl,
            LinkedInOptions o => o.TokenUrl,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetClientId(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.ClientId,
            GoogleOptions o => o.ClientId,
            FacebookOptions o => o.AppId,
            TwitterOptions o => o.ApiKey,
            QQOptions o => o.AppId,
            WeiboOptions o => o.AppKey,
            MicrosoftOptions o => o.ClientId,
            AppleOptions o => o.ClientId,
            AmazonOptions o => o.ClientId,
            DingTalkOptions o => o.AppId,
            LinkedInOptions o => o.ClientId,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetClientSecret(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.ClientSecret,
            GoogleOptions o => o.ClientSecret,
            FacebookOptions o => o.AppSecret,
            TwitterOptions o => o.ApiKeySecret,
            QQOptions o => o.AppKey,
            WeiboOptions o => o.AppSecret,
            MicrosoftOptions o => o.ClientSecret,
            AppleOptions o => o.ClientSecret,
            AmazonOptions o => o.ClientSecret,
            DingTalkOptions o => o.AppSecret,
            LinkedInOptions o => o.ClientSecret,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetRedirectUri(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.RedirectUri,
            GoogleOptions o => o.RedirectUri,
            FacebookOptions o => o.RedirectUri,
            TwitterOptions o => o.RedirectUri,
            QQOptions o => o.RedirectUri,
            WeiboOptions o => o.RedirectUri,
            MicrosoftOptions o => o.RedirectUri,
            AppleOptions o => o.RedirectUri,
            AmazonOptions o => o.RedirectUri,
            DingTalkOptions o => o.RedirectUri,
            LinkedInOptions o => o.RedirectUri,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetUserInfoUrl(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.UserInfoUrl,
            GoogleOptions o => o.UserInfoUrl,
            FacebookOptions o => o.UserInfoUrl,
            TwitterOptions o => o.UserInfoUrl,
            QQOptions o => o.UserInfoUrl,
            WeiboOptions o => o.UserInfoUrl,
            MicrosoftOptions o => o.UserInfoUrl,
            AppleOptions o => o.UserInfoUrl,
            AmazonOptions o => o.UserInfoUrl,
            DingTalkOptions o => o.UserInfoUrl,
            LinkedInOptions o => o.UserInfoUrl,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetScope(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.Scope,
            GoogleOptions o => o.Scope,
            FacebookOptions o => o.Scope,
            TwitterOptions o => o.Scope,
            QQOptions o => o.Scope,
            WeiboOptions o => o.Scope,
            MicrosoftOptions o => o.Scope,
            AppleOptions o => o.Scope,
            AmazonOptions o => o.Scope,
            DingTalkOptions o => o.Scope,
            LinkedInOptions o => o.Scope,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    private string GetAuthorizationUrl(object providerOptions)
    {
        return providerOptions switch
        {
            GitHubOptions o => o.AuthorizationUrl,
            GoogleOptions o => o.AuthorizationUrl,
            FacebookOptions o => o.AuthorizationUrl,
            TwitterOptions o => o.AuthorizationUrl,
            QQOptions o => o.AuthorizationUrl,
            WeiboOptions o => o.AuthorizationUrl,
            MicrosoftOptions o => o.AuthorizationUrl,
            AppleOptions o => o.AuthorizationUrl,
            AmazonOptions o => o.AuthorizationUrl,
            DingTalkOptions o => o.AuthorizationUrl,
            LinkedInOptions o => o.AuthorizationUrl,
            _ => throw new InvalidOperationException("Unsupported provider")
        };
    }

    #endregion

    #endregion
} 