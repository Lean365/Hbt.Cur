//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtOAuthService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : OAuth第三方登录服务接口
//===================================================================

using Hbt.Cur.Application.Dtos.Identity;

namespace Hbt.Cur.Application.Services.Identity;

/// <summary>
/// OAuth第三方登录服务接口
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public interface IHbtOAuthService
{
    #region 基础CRUD操作

    /// <summary>
    /// 获取OAuth账号分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>OAuth账号分页列表</returns>
    Task<HbtPagedResult<HbtOAuthDto>> GetListAsync(HbtOAuthQueryDto query);

    /// <summary>
    /// 根据ID获取OAuth账号详情
    /// </summary>
    /// <param name="id">OAuth账号ID</param>
    /// <returns>OAuth账号详情</returns>
    Task<HbtOAuthDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建OAuth账号
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>OAuth账号ID</returns>
    Task<long> CreateAsync(HbtOAuthCreateDto input);

    /// <summary>
    /// 更新OAuth账号
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(HbtOAuthUpdateDto input);

    /// <summary>
    /// 删除OAuth账号
    /// </summary>
    /// <param name="id">OAuth账号ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除OAuth账号
    /// </summary>
    /// <param name="ids">OAuth账号ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    #endregion

    #region 导入导出操作

    /// <summary>
    /// 导入OAuth账号
    /// </summary>
    /// <param name="importList">导入列表</param>
    /// <returns>导入结果</returns>
    Task<(int success, int failed, List<string> errors)> ImportAsync(List<HbtOAuthImportDto> importList);

    /// <summary>
    /// 导出OAuth账号
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出列表</returns>
    Task<List<HbtOAuthExportDto>> ExportAsync(HbtOAuthQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>模板数据</returns>
    Task<List<HbtOAuthTemplateDto>> GetImportTemplateAsync();

    #endregion

    #region 状态管理操作

    /// <summary>
    /// 更新OAuth账号状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(HbtOAuthStatusDto input);

    /// <summary>
    /// 批量更新OAuth账号状态
    /// </summary>
    /// <param name="ids">OAuth账号ID集合</param>
    /// <param name="status">状态</param>
    /// <param name="remark">备注</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchUpdateStatusAsync(long[] ids, int status, string? remark = null);

    #endregion

    #region 绑定解绑操作

    /// <summary>
    /// 绑定OAuth账号（使用DTO）
    /// </summary>
    /// <param name="input">绑定对象</param>
    /// <returns>是否成功</returns>
    Task<bool> BindOAuthAccountAsync(HbtOAuthBindDto input);

    /// <summary>
    /// 解绑OAuth账号（使用DTO）
    /// </summary>
    /// <param name="input">解绑对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UnbindOAuthAccountAsync(HbtOAuthUnbindDto input);

    #endregion

    #region OAuth登录相关操作

    /// <summary>
    /// 获取支持的登录提供商列表
    /// </summary>
    /// <returns>提供商列表</returns>
    Task<List<HbtOAuthProviderDto>> GetProvidersAsync();

    /// <summary>
    /// 开始OAuth授权流程
    /// </summary>
    /// <param name="provider">提供商名称</param>
    /// <param name="redirectUri">回调地址</param>
    /// <returns>授权URL</returns>
    Task<string> GetAuthorizationUrlAsync(string provider, string? redirectUri = null);

    /// <summary>
    /// 处理OAuth回调
    /// </summary>
    /// <param name="provider">提供商名称</param>
    /// <param name="code">授权码</param>
    /// <param name="state">状态参数</param>
    /// <returns>登录结果</returns>
    Task<HbtLoginResultDto> HandleCallbackAsync(string provider, string code, string state);

    /// <summary>
    /// 绑定OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="provider">提供商名称</param>
    /// <param name="oauthUserId">OAuth用户ID</param>
    /// <param name="oauthUserInfo">OAuth用户信息</param>
    /// <returns>是否成功</returns>
    Task<bool> BindOAuthAccountAsync(long userId, string provider, string oauthUserId, object oauthUserInfo);

    /// <summary>
    /// 解绑OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="provider">提供商名称</param>
    /// <returns>是否成功</returns>
    Task<bool> UnbindOAuthAccountAsync(long userId, string provider);

    /// <summary>
    /// 获取用户绑定的OAuth账号
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>绑定的OAuth账号列表</returns>
    Task<List<HbtOAuthAccountDto>> GetUserOAuthAccountsAsync(long userId);

    #endregion
}

/// <summary>
/// OAuth提供商信息DTO
/// </summary>
public class HbtOAuthProviderDto
{
    /// <summary>
    /// 提供商名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 提供商显示名称
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 图标URL
    /// </summary>
    public string IconUrl { get; set; } = string.Empty;

    /// <summary>
    /// 授权范围
    /// </summary>
    public string Scope { get; set; } = string.Empty;
}

/// <summary>
/// OAuth账号信息DTO
/// </summary>
public class HbtOAuthAccountDto
{
    /// <summary>
    /// 提供商名称
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// OAuth用户ID
    /// </summary>
    public string OAuthUserId { get; set; } = string.Empty;

    /// <summary>
    /// OAuth用户名
    /// </summary>
    public string OAuthUserName { get; set; } = string.Empty;

    /// <summary>
    /// 绑定时间
    /// </summary>
    public DateTime BindTime { get; set; }

    /// <summary>
    /// 是否为主要账号
    /// </summary>
    public bool IsPrimary { get; set; }
} 